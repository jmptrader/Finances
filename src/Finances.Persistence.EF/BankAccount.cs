//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finances.Persistence.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class BankAccount
    {
        public BankAccount()
        {
            this.BalanceDateBankAccounts = new HashSet<BalanceDateBankAccount>();
        }
    
        public int BankAccountId { get; set; }
        public string Name { get; set; }
        public int BankId { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountOwner { get; set; }
        public bool PaysTaxableInterest { get; set; }
        public string LoginURL { get; set; }
        public string LoginID { get; set; }
        public string PasswordHint { get; set; }
        public System.DateTime OpenedDate { get; set; }
        public Nullable<System.DateTime> ClosedDate { get; set; }
        public Nullable<decimal> InitialRate { get; set; }
        public Nullable<System.DateTime> MilestoneDate { get; set; }
        public string MilestoneNotes { get; set; }
        public string Notes { get; set; }
        public System.DateTime RecordCreatedDateTime { get; set; }
        public System.DateTime RecordUpdatedDateTime { get; set; }
    
        public virtual Bank Bank { get; set; }
        public virtual ICollection<BalanceDateBankAccount> BalanceDateBankAccounts { get; set; }
    }
}
