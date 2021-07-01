using System;
using System.Collections.Generic;

#nullable disable

namespace IceCream.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountPayments = new HashSet<AccountPayment>();
            FeedbackFormulas = new HashSet<FeedbackFormula>();
            Formulas = new HashSet<Formula>();
            InvoiceAccounts = new HashSet<InvoiceAccount>();
            ServiceAccounts = new HashSet<ServiceAccount>();
        }

        public int AccId { get; set; }
        public string AccUsername { get; set; }
        public string AccPassword { get; set; }
        public string AccAvatar { get; set; }
        public string AccEmail { get; set; }
        public string AccPhone { get; set; }
        public string AccFullname { get; set; }
        public string AccGender { get; set; }
        public DateTime? AccDob { get; set; }
        public string AccAddr { get; set; }
        public byte? AccRole { get; set; }
        public int? AccMoney { get; set; }
        public byte? AccStatus { get; set; }

        public virtual ICollection<AccountPayment> AccountPayments { get; set; }
        public virtual ICollection<FeedbackFormula> FeedbackFormulas { get; set; }
        public virtual ICollection<Formula> Formulas { get; set; }
        public virtual ICollection<InvoiceAccount> InvoiceAccounts { get; set; }
        public virtual ICollection<ServiceAccount> ServiceAccounts { get; set; }
    }
}
