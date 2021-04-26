using Moq;
using NUnit.Framework;
using PensionDisbursement.Models;
using PensionDisbursement.Providers;
using System;
using System.Net;
using System.Net.Http;

namespace PensionDisburementapiNunit
{
    
        public class Tests
        {
            Mock<IPensionerProvider> pro = new Mock<IPensionerProvider>();
            PensionerProvider _pro = new PensionerProvider();

            HttpResponseMessage responseMessage;
            HttpResponseMessage testResponse;
            HttpStatusCode statusCode = HttpStatusCode.OK;
        
            PensionerDetail pensionDetail;
            [SetUp]
            public void Setup()
            {
                
                responseMessage = new HttpResponseMessage(statusCode);
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
            [TestCase("111122223333")]
            public void PensionerDetailByAadhar_Returns_object(string aadhar)
            {
                PensionerDetail resp = null;

                pro.Setup(p => p.GetData(aadhar)).Returns(pensionDetail);
                resp = pro.Object.GetData(aadhar);
                if (resp.AadhaarNo == aadhar) testResponse = new HttpResponseMessage(HttpStatusCode.OK);

                Assert.IsTrue(testResponse.StatusCode == HttpStatusCode.OK);
            }
            [TestCase("110022993333")]
            public void PensionerDetail_InvalidAadhar_Returns_null(string aadhar)
            {
                PensionerDetail resp = null;

                pro.Setup(p => p.GetData(aadhar)).Returns(pensionDetail);
                resp = pro.Object.GetData(aadhar);
                if (resp.AadhaarNo == aadhar) testResponse = new HttpResponseMessage(HttpStatusCode.OK);

                Assert.IsTrue(testResponse.StatusCode == HttpStatusCode.NotFound);
            }
            
        }
    }
