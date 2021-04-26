using PensionDisbursement.Models;
using PensionDisbursement.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursement.Repository
{
    public class PensionerRepository:IPensionerRepository
    {
        private readonly IPensionerProvider _provider;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerRepository));
        ProcessPensionResponse processPensionResponse = new ProcessPensionResponse();
        public double amount,finalamount;
        public PensionerRepository(IPensionerProvider provider)
        {
            _provider = provider;
        }
        public PensionerDetail GetDetail(string AadhaarNumber)
        {
            PensionerDetail pensionerDetail = null;

            _log4net.Info("Requesting for Info");



            pensionerDetail = _provider.GetData(AadhaarNumber);
            _log4net.Info("Information Fetched");
            return pensionerDetail;
        }

        public int status(PensionerDetail pensionerDetail, ProcessPensionInput input)
        {
            if (input.PensionAmount == 0)
                return 21;
            
            if ( pensionerDetail.PensionType == 1 )
            {
               amount = (double)pensionerDetail.Salary*0.8 + (double)pensionerDetail.Allowance;

                
            }
            else if(pensionerDetail.PensionType == 2)
            {
                amount = (double)pensionerDetail.Salary * 0.5 + (double)pensionerDetail.Allowance;
            }
            if(pensionerDetail.BankType==1)
            {
                finalamount = amount - 500;
            }
            else if (pensionerDetail.BankType == 2)
            {
                finalamount = amount -550;
            }   

            if (pensionerDetail.BankType == 1)
            {
                if (input.PensionAmount == finalamount && input.BankCharge == 500)
                {
                    return 10;
                }
            }
            if (pensionerDetail.BankType == 2)
            {
                if (input.PensionAmount == finalamount && input.BankCharge == 550)
                {
                    return 10;
                }
            }
           
             if (input.PensionAmount==amount&&pensionerDetail.BankType==1)
            {
                return 20;
            }
            if(input.PensionAmount == amount&& pensionerDetail.BankType == 2)
            {
                return 20;
            }
            return 21;

            
        }

        
    }
}
