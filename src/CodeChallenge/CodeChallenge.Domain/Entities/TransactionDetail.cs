using CodeChallenge.Domain.Entities.Common;
using CodeChallenge.Domain.Enumerations;

namespace CodeChallenge.Domain.Entities
{
    public class TransactionDetail : EntityBasicRoot<int>
    {
        public int TransactionId { get; set; }

        public PaymentTypes Type { get; set; }

        public decimal Value { get; set; }

        public int Quantity { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}