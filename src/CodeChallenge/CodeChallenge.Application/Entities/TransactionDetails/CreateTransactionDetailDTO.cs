using CodeChallenge.Domain.Enumerations;

namespace CodeChallenge.Application.Entities.TransactionDetails
{
    public class CreateTransactionDetailDTO
    {
        public PaymentTypes Type { get; set; }

        public decimal Value { get; set; }

        public int Quantity { get; set; }
    }
}
