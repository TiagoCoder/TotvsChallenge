using CodeChallenge.Application.Entities.TransactionDetails;
using CodeChallenge.Domain.Enumerations;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Application.Helpers
{
    public static class DynamicChangeHelper
    {

        public static List<TransactionDetailDTO> GetChange(decimal[] values, decimal totalAmount, PaymentTypes type)
        {
            //Inicializa a classe ChangeDTO
            List<TransactionDetailDTO> change = new(); ;

            // Organiza o array do valor mais pequeno ao maior
            Array.Sort(values);

            // Inicializa a lista de notas a retornar
            // Usa-se um dicionario por questões de performance
            Dictionary<decimal, int> valuesToReturn = new();

            // Enquanto existir um valor maior que a nota mais pequena existente continua a processar

            decimal keyValue = 0.00M;

            decimal maxValue = values[^1];
            decimal minValue = values[0];

            int maxCount = values.Length -1 ;

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

            int count = 0;

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

            // Conta quantas vezes a mesma nota é usada como troco e adiciona o valor à estrutura de retorno
            change = BulkInsert(valuesToReturn, change, type);

            // retorna o troco que contêm notas e moedas ou apenas notas
            return change;
        }
        public static void AddValueOrIncrementQuantity<T>(this Dictionary<T, int> dictionary, T key)
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

        public static decimal RemoveAmount(decimal amount, decimal key)
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
    }
}
