using PensionorDetailAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionorDetailAPI.Repository
{
    public class PensionerDetailRepo : IPensionerDetailRepo
    {
        private readonly PensionManagementDBContext context;
        public PensionerDetailRepo()
        {

        }
        public PensionerDetailRepo(PensionManagementDBContext _context)
        {
            context = _context;
        }
        public PensionerDetail Getaadhar(string Aadhar)
        {
            PensionerDetail value = context.PensionerDetails.Where(x => x.AadhaarNo == Aadhar).Select(x => x).SingleOrDefault();
            return value;
            //throw new NotImplementedException();
        }
        public PensionerDetail GetPensionerByAadhar(string Aadhar)
        {
            PensionerDetail value = context.PensionerDetails.Find(Aadhar);
            return value;
            //throw new NotImplementedException();
        }

        public IEnumerable<PensionerDetail> GetPensionerDetail()
        {
            return context.PensionerDetails.ToList();
            //throw new NotImplementedException();
        }

        public async Task<PensionTransaction> PostTransaction(PensionTransaction pension)
        {
            PensionTransaction pensionTransaction = null;
            if (pension == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                pensionTransaction = new PensionTransaction()
                {
                    TransactionNum = pension.TransactionNum,
                    Name = pension.Name,
                    Pan = pension.Pan,
                    AadhaarNo = pension.AadhaarNo,
                    PensionAmount = pension.PensionAmount,
                    BankType = pension.BankType,
                    BankName = pension.BankName,
                    BankAccountNo = pension.BankAccountNo,
                    PensionType = pension.PensionType,
                    TransactionDate = pension.TransactionDate,
                };
                await context.PensionTransactions.AddAsync(pensionTransaction);
                await context.SaveChangesAsync();
            }
            return pensionTransaction;
           
        }
    }
}
