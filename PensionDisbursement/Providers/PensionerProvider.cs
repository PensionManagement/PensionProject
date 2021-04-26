using Newtonsoft.Json;
using PensionDisbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PensionDisbursement.Providers
{
    public class PensionerProvider:IPensionerProvider
    {
        public PensionerDetail pensionerDetail = null;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerProvider));
        public PensionerDetail GetData(string AadhaarNo)
        {
            string BaseUrl = "https://localhost:44340/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                //_log4net.Debug("Connecting with PensionDetails");
                try
                {
                    response = client.GetAsync("api/PensionerDetail/" + AadhaarNo).Result;
                }
                catch (Exception e)
                {
                    //_log4net.Debug("Exception Occured " + e);

                    response = null;

                }

                if
                    (response != null)
                {
                    // _log4net.Debug("Connecting Sucessfull");

                    var ObjResponse = response.Content.ReadAsStringAsync().Result;
                    pensionerDetail = JsonConvert.DeserializeObject<PensionerDetail>(ObjResponse);
                    return pensionerDetail;
                }
                return pensionerDetail;

            }
        }
    }
}
