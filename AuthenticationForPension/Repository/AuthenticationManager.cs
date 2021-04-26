using AuthenticationForPension.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationForPension.Repository
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly PensionManagementDBContext users = new PensionManagementDBContext();
        private readonly string tokenKey;
        private PensionManagementDBContext _context;
        public AuthenticationManager(PensionManagementDBContext context)
        {
            _context = context;
        }
        public AuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }

        public PensionerDetail GetPensioner(string pan, string Aadhar)
        {

            PensionerDetail val = users.PensionerDetails.Where(x => x.Pan == pan && x.AadhaarNo == Aadhar).Select(x => x).SingleOrDefault();
            return val;
        }
        public string Verify(string aadhar,string name)
        {
            PensionerDetail user= users.PensionerDetails.FirstOrDefault(x => x.AadhaarNo == aadhar && x.Name == name);
          if(user!=null)
            {
                if (user.Password == null)
                {
                    return ("No");
                }

                return ("Yes");
            }
            return ("Not Found");
        }

        public PensionerDetail AddPassword(string aadhar, AddPassword addPassword)
        {
            PensionerDetail user =  users.PensionerDetails.FirstOrDefault(x => x.AadhaarNo == aadhar);
            if (user != null)
            {
                if (user.Password == null)
                {
                    user.Password = addPassword.Password;
                    users.SaveChanges();
                }
               
            }

            return (user);
        }

        public string Authenticate(string aadhar, string password)
        {

            if (!users.PensionerDetails.Any(u => u.AadhaarNo == aadhar && u.Password == password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, aadhar)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
