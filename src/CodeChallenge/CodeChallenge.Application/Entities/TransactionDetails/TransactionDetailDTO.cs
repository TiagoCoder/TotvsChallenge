using CodeChallenge.Domain.Enumerations;

namespace CodeChallenge.Application.Entities.TransactionDetails
{
    public class TransactionDetailDTO
    {
        #region Properties
        /// <summary>
        /// Tipo de pagamento
        /// </summary>
        /// <example>Bill</example>
        public PaymentTypes Type { get; set; }
        /// <summary>
        /// Valor a adicionar
        /// </summary>
        /// <example>€20.00</example>
        public decimal Value { get; set; }
        /// <summary>
        /// Nº de vezes que o valor se repete
        /// </summary>
        /// <example>2</example>
        public int Quantity { get; set; }
        #endregion
    }
}
