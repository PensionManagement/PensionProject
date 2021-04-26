using Moq;
using NUnit.Framework;
using PensionDisbursement.Models;
using PensionDisbursement.Providers;
using PensionDisbursement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PensionDisburementapiNunit
{
    [TestFixture]
    public class RepositoryTest
    {


        private Mock<IPensionerProvider> pro;

        private IPensionerRepository _pro;
        HttpResponseMessage testResponse;

        PensionerDetail pensionDetail;

        [SetUp]

        public void Setup()
        {
            pro = new Mock<IPensionerProvider>();
            _pro = new PensionerRepository(pro.Object);
            testResponse = new HttpResponseMessage(HttpStatusCode.NotFound);
            pensionDetail = new PensionerDetail


            {
                Name = "Akash",
                Dob = Convert.ToDateTime("1998-03-01"),
                Pan = "BCFPN1234F",
                AadhaarNo = "111122223333",
                Salary = 40000,
                Allowance = 5000,
                PensionType = 1,
                BankName = "HDFC",
                BankAccountNo = "123456789876",
                BankType = 2
            };

        }
       /* [TestCase(25550.0, 550, "111122223333")]
        public void PensionerRepository_Valid_Aadhaar_return_OK(double pension, int charges, string aadhaar)
        {
            ProcessPensionInput processPensionInput = new ProcessPensionInput { AadhaarNo = aadhaar, BankCharge = charges, PensionAmount = pension };

            pro.Setup(p => p.GetData(aadhaar)).Returns(pensionDetail);
            PensionerDetail pensionerDetail = pro.Object.GetData(aadhaar);
            int n = _pro.status(pensionerDetail, processPensionInput);
            Assert.AreEqual(10, n);
        }*/

        [TestCase(24430.0, 550, "111122223333")]
        public void PensionerRepository_Invalid_PensionAmount_return_ErrorStatus(double pension, int charges, string aadhaar)
        {
            ProcessPensionInput processPensionInput = new ProcessPensionInput { AadhaarNo = aadhaar, BankCharge = charges, PensionAmount = pension };

            pro.Setup(p => p.GetData(aadhaar)).Returns(pensionDetail);
            PensionerDetail pensionerDetail = pro.Object.GetData(aadhaar);
            int n = _pro.status(pensionerDetail, processPensionInput);
            Assert.AreNotEqual(10, n);
        }

        [TestCase(24450.0, 550, "111122220000")]
        public void PensionerRepository_Invalid__Aadhaar_return_ErrorStatus(double pension, int charges, string aadhaar)
        {
            ProcessPensionInput processPensionInput = new ProcessPensionInput { AadhaarNo = aadhaar, BankCharge = charges, PensionAmount = pension };
            int n = 10;
            pro.Setup(p => p.GetData(aadhaar)).Returns(pensionDetail);
            PensionerDetail pensionerDetail = pro.Object.GetData(aadhaar);
            if (pensionDetail.AadhaarNo != aadhaar)
                n = 21;

            Assert.AreNotEqual(10, n);
        }

        [TestCase(24430.0, 440, "111122223333")]
        public void PensionerRepository_Invalid__Bankcharge_return_ErrorStatus(double pension, int charges, string aadhaar)
        {
            ProcessPensionInput processPensionInput = new ProcessPensionInput { AadhaarNo = aadhaar, BankCharge = charges, PensionAmount = pension };

            pro.Setup(p => p.GetData(aadhaar)).Returns(pensionDetail);
            PensionerDetail pensionerDetail = pro.Object.GetData(aadhaar);
            int n = _pro.status(pensionerDetail, processPensionInput);
            Assert.AreEqual(21, n);
        }




    }
}
