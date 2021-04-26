using PensionDisbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursement.Providers
{
    public interface IPensionerProvider
    {
        public PensionerDetail GetData(string AadhaarNumber);
    }
}
