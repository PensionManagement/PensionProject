using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PensionDisbursement.Models;
using PensionDisbursement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisbursementController : ControllerBase
    {
        private readonly IPensionerRepository _repo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(DisbursementController));
        ProcessPensionResponse processPensionResponse = new ProcessPensionResponse();
        PensionerDetail pensionerDetail = null;

        /*public DisbursementController()
        {

        }*/
        public DisbursementController(IPensionerRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult DisbursePension(ProcessPensionInput processPensionInput)
        {
            _log4net.Info("Pension Disbursement started");
            _log4net.Info("Person with Aadhar Number " + processPensionInput.AadhaarNo);

            pensionerDetail = _repo.GetDetail(processPensionInput.AadhaarNo);


            _log4net.Info("PensionerDetail Present Name:" + pensionerDetail.Name);



            processPensionResponse.ProcessPensionStatusCode = _repo.status(pensionerDetail, processPensionInput);
            _log4net.Info(processPensionResponse.ProcessPensionStatusCode);
            return Ok(processPensionResponse);





        }
    }
}
