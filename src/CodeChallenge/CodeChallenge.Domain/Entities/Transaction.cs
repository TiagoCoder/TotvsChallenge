using CodeChallenge.Domain.Entities.Common;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Domain.Entities
{
    public class Transaction : EntityBasicRoot<int>
    {
        public Transaction()
        {
            TransactionDetail = new HashSet<TransactionDetail>();
        }
        public decimal TotalValue { get; set; }

        public decimal ValueGiven { get; set; }

        public DateTime TransactionDate { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetail { get; set; }
    }
}
