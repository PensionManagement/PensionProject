using ProcessPensionAPI.Models;
using ProcessPensionAPI.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionAPI.Repository
{
    public class ProcessPensionRepo : IProcessPensionRepo
    {

        private readonly IPensionerDetailProvider provider;
        public ProcessPensionRepo(IPensionerDetailProvider _provider)
        {
            provider = _provider;
        }
        public Pension GetPension(PensionerInput pensionerInput)
        {
            double? pensionamount =0;

            Pension pension = new();
            PensionerDetail pensioner=provider.GetPensionerInfo(pensionerInput.AadhaarNo);
            if (pensioner.PensionType == 1)
            {
                 pensionamount = pensioner.Salary * 0.8+pensioner.Allowance;
            }
            else if (pensioner.PensionType == 2)
            {
                pensionamount = pensioner.Salary * 0.5 + pensioner.Allowance;
            }
            if (pensioner.BankType==1)
            {
                pensionamount -=500;
            }
            if (pensioner.BankType == 2)
            {
                pensionamount -=550;
            }
            if (pensionerInput.AadhaarNo==pensioner.AadhaarNo && pensionerInput.DOB==pensioner.Dob && pensionerInput.Name==pensioner.Name && pensionerInput.PensionType==pensioner.PensionType && pensionerInput.PAN==pensioner.Pan)
            {
                pension.Name = pensioner.Name;
                pension.DOB = pensioner.Dob;
                pension.PAN = pensioner.Pan;
                pension.PensionAmount = pensionamount;
                pension.PensionType = pensioner.PensionType;
            }
            
            return pension;
        }

        public string IsAadhaarExists(string aadhaar)
        {
            PensionerDetail pensioner=provider.GetPensionerInfo(aadhaar);
            if (pensioner.AadhaarNo!=null && pensioner.AadhaarNo!="" )
            {
                return "Yes";
            }
            return "No";
        }

        public PensionTransaction PostPension(PensionTransaction pensionerOutput)
        {
            PensionTransaction penT = provider.PostTrans(pensionerOutput);
            return penT;
        }

        public int PostProcessPension(ProcessPensionInput processPensionInput)
        {

            ProcessPensionResponse response = provider.GetDisbursementResponse(processPensionInput);
            int responsecode=response.ProcessPensionStatusCode;

            return responsecode;
        }
    }
}
