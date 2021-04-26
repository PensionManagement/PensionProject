using AuthenticationForPension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationForPension.Repository
{
    public interface IAuthenticationManager
    {
        public string Authenticate(string aadhar, string password);

        public string Verify(string aadhar, string name);
       // public string AddPassword(string aadhar, string password);
        //IEnumerable<PensionerDetail> GetPensionerDetails();
        PensionerDetail AddPassword(string aadhar,AddPassword addPassword);



    }
}
