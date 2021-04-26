using AuthenticationForPension.Models;
using AuthenticationForPension.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationForPension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthenticationController));

        private readonly IAuthenticationManager manager;
        public AuthenticationController(IAuthenticationManager manager)
        {
            this.manager = manager;
        }
        [HttpGet]
        public string Get()
        {
            _log4net.Info(" Sample GET Method is Invoked");

            return "Hello";
        }


        [AllowAnonymous]
        [HttpPost("VerifyUser")]
        public IActionResult VerifyUser([FromBody] Verify user)
        {
            _log4net.Info(" Http VerifyUser is Invoked");

            string obj = manager.Verify(user.AadhaarNo, user.Name);
 
            return Ok(obj);
        }

        [HttpPut("{aadhar}")]
        public async Task<IActionResult> AddPassword(string aadhar, AddPassword addPassword)
        {
            _log4net.Info("Pensioner table with " + aadhar + "get edited");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var editedUser = manager.AddPassword(aadhar, addPassword);

            return Ok(editedUser);
        }




        [AllowAnonymous]
        [HttpPost("AuthenicateUser")]
        public IActionResult AuthenticateUser([FromBody] AddPassword user)
        {
           _log4net.Info(" Http Authentication request Initiated");
            var token = manager.Authenticate(user.AadhaarNo, user.Password);
            if (token == null)
                return Ok(null);
            return Ok(token);
        }
    }
}
