using ProcessPensionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProcessPensionAPI.Provider
{
    public interface IPensionerDetailProvider
    {
        public HttpResponseMessage PensionDetail(string aadhar);
        public PensionerDetail GetPensionerInfo(string aadhar);
        public HttpResponseMessage GetDisbursementMessage(ProcessPensionInput pensionDetail);
        public ProcessPensionResponse GetDisbursementResponse(ProcessPensionInput pensionDetail);
        public HttpResponseMessage PostTransaction(PensionTransaction pensionDetail);
        public PensionTransaction PostTrans(PensionTransaction PenIn);




    }
}
