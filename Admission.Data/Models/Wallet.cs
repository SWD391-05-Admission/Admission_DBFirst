using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int Amount { get; set; }
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
