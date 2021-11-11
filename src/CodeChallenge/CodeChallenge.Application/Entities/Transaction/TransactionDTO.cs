using CodeChallenge.Application.Entities.TransactionDetails;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Application.Entities.Transaction
{
    public class TransactionDTO
    {
        #region Properties
        public decimal TotalValue { get; set; }

        public decimal ValueGiven { get; set; }

        public DateTime TransactionDate { get; set; }

        public List<TransactionDetailDTO> TransactionDetail { get; set; }
        #endregion

        #region Constructor
        public TransactionDTO()
        {
            TransactionDetail = new List<TransactionDetailDTO>();
        }
        #endregion
    }
}
