using NUnit.Framework;
using AuthenticationForPension;
using AuthenticationForPension.Controllers;
using System.Collections.Generic;
using AuthenticationForPension.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using AuthenticationForPension.Repository;

namespace TestPension
{
    public class Tests
    {
        List<PensionerDetail> penss = new List<PensionerDetail>();
        IQueryable<PensionerDetail> testingdata;
        Mock<DbSet<PensionerDetail>> mockSet;
        Mock<PensionManagementDBContext> pensioncontextmock;
        //Arrange

        [SetUp]
        public void Setup()
        {
            penss = new List<PensionerDetail>()
            {
                new PensionerDetail{Name = "Valli", Pan ="BCFPN1234F" ,
                    AadhaarNo = "789622223453" ,Salary = 120000, Allowance = 5000,
                    BankAccountNo = "963506789876", BankName = "RBL",
                    BankType = 1 , PensionType = 2 },
            };
            testingdata = penss.AsQueryable();
            mockSet = new Mock<DbSet<PensionerDetail>>();
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.Provider).Returns(testingdata.Provider);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.Expression).Returns(testingdata.Expression);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.ElementType).Returns(testingdata.ElementType);
            mockSet.As<IQueryable<PensionerDetail>>().Setup(m => m.GetEnumerator()).Returns(testingdata.GetEnumerator());
            var p = new DbContextOptions<PensionManagementDBContext>();
            pensioncontextmock = new Mock<PensionManagementDBContext>(p);

            pensioncontextmock.Setup(x => x.PensionerDetails).Returns(mockSet.Object);
 
        }

        [Test]
        public void CheckTest()
        {
           // AuthenticationManager obj = new AuthenticationManager(pensioncontextmock.Object);
            var Auth = new AuthenticationManager(pensioncontextmock.Object);
            var val1 = Auth.GetPensioner("BCFPN1234F", "789622223453");
            string name = val1.Name;
            Assert.AreEqual("Demo", name);
        }



    }
}




