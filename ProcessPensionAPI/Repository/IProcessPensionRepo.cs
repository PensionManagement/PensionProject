using ProcessPensionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionAPI.Repository
{
    public interface IProcessPensionRepo
    {
        Pension GetPension(PensionerInput pensionerInput);

        PensionTransaction PostPension(PensionTransaction pensionerOutput);

        int PostProcessPension(ProcessPensionInput processPensionInput);
        string IsAadhaarExists(string aadhaar);
    }
}
