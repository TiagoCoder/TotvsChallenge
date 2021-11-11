using CodeChallenge.Application.Entities.Messages.Response;
using CodeChallenge.Application.Entities.Transaction;
using System.Net;

namespace CodeChallenge.Application.Helpers
{
    public class ResponseMessageFormatterHelper
    {
        #region FormatResponse
        public static Response<string> FormatResponse(TransactionDTO transaction)
        {
            // Inicializa a string de retorno
            string result = "Entregar ";

            // Define o valor máximo de iterações
            int totalCount = transaction.TransactionDetail.Count;

            // Inicio do Loop
            for (int count = 0; count < totalCount; count++)
            {
                // Guarda o valor corresponde à iteração
                var detail = transaction.TransactionDetail[count];

                // Formata a string de retorno
                result += $"{detail.Quantity} {(detail.Type.Equals("BILL") ? "nota" : "moeda")} de R${detail.Value}";

                // Se a iteração é a ultima
                if ((count + 1) == totalCount && (count + 1) != 1)
                {
                    result += "e ";
                }
                // Adiciona um ponto final à string de retorno
                else if ((count = 1) == totalCount)
                {
                    result += ".";
                }
                // Se a iteração não é a ultima
                else
                {
                    result += ", ";
                }
            }

            // Inicializa um novo objeto do tipo Response e iguala a propriedade Result à string formatada
            var response = new Response<string>()
            {
                Success = true,
                HttpStatusCode = HttpStatusCode.OK,
                Result = result
            };

            return response;
        }
        #endregion
    }
}
