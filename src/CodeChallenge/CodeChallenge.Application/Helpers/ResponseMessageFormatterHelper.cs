using CodeChallenge.Application.Entities.Messages.Response;
using CodeChallenge.Application.Entities.Transaction;
using System.Net;

namespace CodeChallenge.Application.Helpers
{
    public class ResponseMessageFormatterHelper
    {
        public static Response<string> FormatResponse(TransactionDTO transaction)
        {
            string result = "Entregar ";

            int totalCount = transaction.TransactionDetail.Count;

            for (int count = 0; count < totalCount; count++)
            {
                var detail = transaction.TransactionDetail[count];

                result += $"{detail.Quantity} {(detail.Type.Equals("BILL") ? "nota" : "moeda")} de R${detail.Value}";

                if ((count + 1) == totalCount && (count + 1) != 1)
                {
                    result += "e ";
                }
                else if ((count = 1) == totalCount)
                {
                    result += ".";
                }
                else
                {
                    result += ", ";
                }
            }

            var response = new Response<string>()
            {
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Result = result
            };

            return response;
        }
    }
}
