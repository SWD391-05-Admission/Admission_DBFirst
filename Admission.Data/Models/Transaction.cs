using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Desciption { get; set; }
        public int WalletId { get; set; }

        public virtual Wallet Wallet { get; set; }
    }
}
