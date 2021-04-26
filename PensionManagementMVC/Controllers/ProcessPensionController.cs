using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionManagementMVC.Models;
using PensionorDetailAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementMVC.Controllers
{
    public class ProcessPensionController : Controller
    {

        public ActionResult GetPensioner()
        {
            /*if (TempData.Peek("isloggedin").ToString()== "yes")
            {*/
                return View();
            /*}
            return RedirectToAction("Login", "Login");*/
            
        }
        [HttpPost]
        public async Task<ActionResult> GetPensioner(ProcessPension pension1)
        {
            if (ModelState.IsValid)
            {
                Pension pension = new();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pension1), Encoding.UTF8, "application/json");


                    using (var response = await httpClient.PostAsync("http://localhost:23578/api/ProcessPension/PensionDetail", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pension = JsonConvert.DeserializeObject<Pension>(apiResponse);
                        TempData["ResponsePension"] = JsonConvert.SerializeObject(pension);
                        TempData["Pensioner"] = JsonConvert.SerializeObject(pension1);
                    }

                }
                if (pension.PensionAmount!=null)
                {
                    return RedirectToAction("PensionDetail");
                }
                ViewBag.PensionerInputError = "Invalid Pensioner Details";
            }
            return View();
        }
        
        public ActionResult PensionDetail(Pension pension)
        {
            string strUser = TempData.Peek("ResponsePension").ToString();
            pension = JsonConvert.DeserializeObject<Pension>(strUser);
            return View(pension);
        }
        public ActionResult PensionNext()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PensionNext(BankDetails bankDetails)
        {
            if (ModelState.IsValid)
            {
                string strUser = TempData.Peek("ResponsePension").ToString();
                Pension pension = JsonConvert.DeserializeObject<Pension>(strUser);
                string pensioner = TempData.Peek("Pensioner").ToString();
                ProcessPension processpension = JsonConvert.DeserializeObject<ProcessPension>(pensioner);


                ProcessPensionInput processPensionInput = new ProcessPensionInput();
                processPensionInput.AadhaarNo = processpension.AadhaarNo;
                processPensionInput.PensionAmount = (double)pension.PensionAmount;
                if (bankDetails.BankType == 1)
                {
                    processPensionInput.BankCharge = 500;
                }
                else
                {
                    processPensionInput.BankCharge = 550;
                }

                string apiResponse;
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(processPensionInput), Encoding.UTF8, "application/json");


                    using (var response = await httpClient.PostAsync("http://localhost:23578/api/ProcessPension/ProcessPension", content))
                    {
                        apiResponse = await response.Content.ReadAsStringAsync();

                    }
                    if (apiResponse != "" || apiResponse != null)
                    {
                        if (apiResponse == "10")
                        {
                            return RedirectToAction("ResultPage", "ProcessPension", new { msg = "You have sucessfully Got pension" });
                        }
                        if (apiResponse == "20")
                        {
                            TempData["errorResponse"] = "Pensioner Values not match";
                            return RedirectToAction("ResultPage", "ProcessPension", new { msg = "Pensioner Values not match" });


                        }
                        if (apiResponse == "21")
                        {
                            TempData["errorResponse"] = "Some Error Occured";
                            return RedirectToAction("ResultPage", "ProcessPension", new { msg = "Some Error Occured please try again with valid data" });

                        }
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> ResultPage(string msg)
        {
            string strUser = TempData.Peek("ResponsePension").ToString();
            Pension pension = JsonConvert.DeserializeObject<Pension>(strUser);
            string strUser1 = TempData.Peek("Pensioner").ToString();
            ProcessPension pension1 = JsonConvert.DeserializeObject<ProcessPension>(strUser1);
            Message message = new Message();
            message.message = msg;
            message.PensionAmount = (double)pension.PensionAmount;
            if (message.message== "You have sucessfully Got pension")
            {
                PensionTransaction trans = new PensionTransaction();
                trans.Name = pension.Name;
                trans.Pan = pension.PAN;
                trans.PensionAmount = pension.PensionAmount;
                trans.PensionType = pension.PensionType;
                trans.TransactionDate = DateTime.Today;
                trans.AadhaarNo = pension1.AadhaarNo;
                trans.PensionAmount = (double)pension.PensionAmount;

                PensionTransaction pen = new PensionTransaction();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(trans), Encoding.UTF8, "application/json");


                    using (var response = await httpClient.PostAsync("http://localhost:23578/api/ProcessPension/PostTransaction", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        pen = JsonConvert.DeserializeObject<PensionTransaction>(apiResponse);

                    }

                }
            }
            return View(message);
        }
        public async Task<bool> AadharCheck(string Aadhaarno)
       {
            string apiResponse;
            //string validateaadhar;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:23578/api/ProcessPension/IsValidAadhaar/" + Aadhaarno))
                {
                     apiResponse = await response.Content.ReadAsStringAsync();
                    //validateaadhar = JsonConvert.DeserializeObject<String>(apiResponse);
                }

            }
            if (apiResponse == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool DateValidation(ProcessPension pension)
        {
            if (pension.DOB.Year>=1999)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
