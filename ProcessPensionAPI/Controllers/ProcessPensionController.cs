using Microsoft.AspNetCore.Mvc;
using ProcessPensionAPI.Models;
using ProcessPensionAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessPensionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProcessPensionController));

        private readonly IProcessPensionRepo repo;
        public ProcessPensionController(IProcessPensionRepo _repo)
        {
            repo = _repo;
        }
        [HttpGet("IsValidAadhaar/{aadhaar}")]
        public string IsAadharExists(string aadhaar)
        {
            return repo.IsAadhaarExists(aadhaar);
        }
        [HttpPost("PensionDetail")]
        public Pension GetPensioner(PensionerInput pensionerInput)
        {
            _log4net.Info("PensionDetail post method is invoked for aadhaar" + pensionerInput.AadhaarNo);

            return repo.GetPension(pensionerInput);
        }
        [HttpPost("ProcessPension")]
        public int GetPension(ProcessPensionInput pensionInput)
        {
            _log4net.Info("ProcessPension post method is invoked for aadhaar" + pensionInput.AadhaarNo);

            return repo.PostProcessPension(pensionInput);
        }

        [HttpPost("PostTransaction")]
        public PensionTransaction PostTrans(PensionTransaction PenIn)
        {
            _log4net.Info("PensionTransaction post method is invoked for aadhaar" +PenIn.AadhaarNo);

            return repo.PostPension(PenIn);
        }



        /*// POST api/<ProcessPensionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProcessPensionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProcessPensionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
