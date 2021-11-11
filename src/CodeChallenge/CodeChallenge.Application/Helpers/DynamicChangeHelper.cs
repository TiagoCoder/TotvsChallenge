using CodeChallenge.Application.Entities.TransactionDetails;
using CodeChallenge.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Application.Helpers
{
    public static class DynamicChangeHelper
    {
        #region GetChange
        public static List<TransactionDetailDTO> GetChange(decimal[] values, decimal totalAmount, PaymentTypes type)
        {
            #region Variables Inicialization
            //Inicializa a lista a retornar
            List<TransactionDetailDTO> change = new(); ;

            // Organiza o array do valor mais pequeno ao maior
            Array.Sort(values);

            // Inicializa uma lista temporaria onde vão ser inseridos os valores
            // Usa-se um dicionario por questões de performance
            Dictionary<decimal, int> valuesToReturn = new();

            // Inicializa a variavel keyValue
            decimal keyValue = 0.00M;

            // Define o valor maximo dos valores disponiveis
            decimal maxValue = values[^1];

            // Define o valor minimo dos valores disponiveis
            decimal minValue = values[0];

            // Define o valor máximo do contador
            int maxCount = values.Length -1 ;

            // Inicializa o contador com o valor 0
            int count = 0;
            #endregion

            #region Lógica para valores superiores ao valor maximo disponível
            while (totalAmount >= maxValue)
            {
                if (totalAmount - maxValue >= maxValue || totalAmount - maxValue >= minValue)
                {
                    AddValueOrIncrementQuantity<decimal>(valuesToReturn, maxValue);

                    totalAmount = RemoveAmount(totalAmount, maxValue);
                }
                else if (totalAmount - maxValue == 0.00M)
                {
                    AddValueOrIncrementQuantity<decimal>(valuesToReturn, maxValue);

                    totalAmount = RemoveAmount(totalAmount, maxValue);
                }
            }
            #endregion

            #region Lógica para quando o montante é inferior ao valor máximo disponível
            while (totalAmount >= minValue)
            {
                if (totalAmount - values[count] == 0.00M)
                {
                    keyValue = values[count];

                    AddValueOrIncrementQuantity<decimal>(valuesToReturn, keyValue);

                    totalAmount = RemoveAmount(totalAmount, keyValue);
                }
                else if (totalAmount - values[count] < minValue)
                {
                    if(totalAmount - values[count] > 0.00M && totalAmount - values[count] < minValue)
                    {
                        keyValue = values[count];
                    }
                    else
                    {
                        keyValue = values[count - 1];
                    }

                    AddValueOrIncrementQuantity<decimal>(valuesToReturn, keyValue);

                    totalAmount = RemoveAmount(totalAmount, keyValue);

                    count = 0;
                }
                else
                {
                    count++;
                }
            }
            #endregion

            #region Insere os valores a utilizar na lista de retorno
            change = BulkInsert(valuesToReturn, change, type);
            #endregion

            return change;
        }
        #endregion

        #region Metodos Auxiliares
        private static void AddValueOrIncrementQuantity<T>(this Dictionary<T, int> dictionary, T key)
        {
            if (!dictionary.TryGetValue(key, out int count))
            {
                dictionary.Add(key, 1);
            }
            else
            {
                dictionary[key] = count + 1;
            }
        }

        private static decimal RemoveAmount(decimal amount, decimal key)
        {
            return amount - key;
        }

        private static List<TransactionDetailDTO> BulkInsert(Dictionary<decimal,int> keyValuePairs1, List<TransactionDetailDTO> transactionDetails, PaymentTypes paymentType)
        {
            foreach (var keyValuePair in keyValuePairs1)
            {
                transactionDetails.Add( new TransactionDetailDTO
                {
                    Type = paymentType,
                    Value = keyValuePair.Key,
                    Quantity = keyValuePair.Value
                });
            }

            return transactionDetails;
        }
        #endregion
    }
}
