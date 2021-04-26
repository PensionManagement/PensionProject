using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProcessPensionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProcessPensionAPI.Provider
{
    public class PensionerDetailProvider : IPensionerDetailProvider
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerDetailProvider));


        public HttpResponseMessage PensionDetail(string aadhar)
        {
            _log4net.Info("Request to Get pensioner detail from PensionerDetail API for aadhaar" + aadhar);
            HttpResponseMessage response = new();
            string uri = "https://localhost:44340/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    response = client.GetAsync("api/PensionerDetail/" + aadhar).Result;
                }
                catch
                {
                    _log4net.Info("Error in  hitting PensionerDetailAPI from ProcessPensionApi");

                    return null;
                }
            }
            _log4net.Info("Successfully get response from PensionerDetailApi");
            return response;
        }
        public PensionerDetail GetPensionerInfo(string aadhar)
        {
            PensionerDetail res;
            HttpResponseMessage response = PensionDetail(aadhar);
            if (response == null)
            {
                res = null;
                return res;
            }
            string responseValue = response.Content.ReadAsStringAsync().Result;
            res = JsonConvert.DeserializeObject<PensionerDetail>(responseValue);
            if (res == null)
            {
                return null;
            }
            return res;
        }
        public HttpResponseMessage GetDisbursementMessage(ProcessPensionInput pensionDetail)
        {
            _log4net.Info("Request to Get response from GetDisbursement API for aadhaar" + pensionDetail.AadhaarNo);
            HttpResponseMessage response = new();
            string uri = "http://localhost:47201/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                StringContent content = new(JsonConvert.SerializeObject(pensionDetail), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    response = client.PostAsync("api/Disbursement", content).Result;
                }
                catch 
                {
                    _log4net.Info("Error in  hitting DisbursementApi from ProcessPensionApi");
                    //response = null;
                }
            }
            _log4net.Info("Successfully get response from DisbursementApi");

            return response;
        }

        public ProcessPensionResponse GetDisbursementResponse(ProcessPensionInput pensionDetail)
        {
            ProcessPensionResponse res;
            HttpResponseMessage response = GetDisbursementMessage(pensionDetail);
            if (response == null)
            {
                res = null;
                return res;
            }
            string responseValue = response.Content.ReadAsStringAsync().Result;
            res = JsonConvert.DeserializeObject<ProcessPensionResponse>(responseValue);
            if (res == null)
            {
                return null;
            }
            return res;
        }

      
        public HttpResponseMessage PostTransaction(PensionTransaction pensionDetail)
        {
            _log4net.Info("Request to Get response from GetDisbursement API for aadhaar" + pensionDetail.AadhaarNo);
            PensionerDetail pensioner = GetPensionerInfo(pensionDetail.AadhaarNo);
            pensionDetail.BankAccountNo = pensioner.BankAccountNo;
            pensionDetail.BankName = pensioner.BankName;
            pensionDetail.BankType = pensioner.BankType;
            HttpResponseMessage response = new();
            string uri = "https://localhost:44340/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                StringContent content = new(JsonConvert.SerializeObject(pensionDetail), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    response = client.PostAsync("api/PensionerDetail/PostTransaction", content).Result;
                }
                catch
                {
                    _log4net.Info("Error in  hitting DisbursementApi from ProcessPensionApi");
                    //response = null;
                }
            }
            _log4net.Info("Successfully get response from DisbursementApi");

            return response;
        }


        public PensionTransaction PostTrans(PensionTransaction PenIn)
        {
            PensionTransaction res;
            HttpResponseMessage response = PostTransaction(PenIn);
            if (response == null)
            {
                res = null;
                return res;
            }
            string responseValue = response.Content.ReadAsStringAsync().Result;
            res = JsonConvert.DeserializeObject<PensionTransaction>(responseValue);
            if (res == null)
            {
                return null;
            }
            return res;
        }





    }
}
