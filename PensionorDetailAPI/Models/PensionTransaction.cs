using System;
using System.Collections.Generic;

#nullable disable

namespace PensionorDetailAPI.Models
{
    public partial class PensionTransaction
    {
        public int TransactionNum { get; set; }
        public string Name { get; set; }
        public string Pan { get; set; }
        public string AadhaarNo { get; set; }
        public double? PensionAmount { get; set; }
        public int? BankType { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public int? PensionType { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual PensionerDetail AadhaarNoNavigation { get; set; }
    }
}
