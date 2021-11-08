using CodeChallenge.Application.Entities;
using System;
using System.Collections.Generic;

namespace CodeChallenge.Application.Helpers
{
    public class DynamicChange
    {

        public static ChangeDTO GetBillsChange(int[] bills, int[] coins, decimal totalAmount)
        {
            //Inicializa a classe ChangeDTO
            ChangeDTO change = new();

            // Organiza o array do valor mais pequeno ao maior
            Array.Sort(bills);
            Array.Sort(coins);

            // Guarda o valor decimal numa váriavel de forma a ser usada no futuro
            decimal coinsAmount = Math.Truncate(totalAmount);

            int amount = (int)totalAmount;

            // Enquanto existir um valor maior que a nota mais pequena existente continua a processar
            while (amount > bills[0])
            {
                // Itera pelo numero de notas
                for (int j = 1; j < bills.Length; j++)
                {
                    // Se o valor total for menor que a nota em iteração, adicionamos o valor anterior à lista de resultados
                    if (amount - bills[j] < 0)
                    {
                        amount -= bills[j - 1];
                        change.Bills.Add(bills[j - 1]);
                    }
                }
            }
            // Se existir ainda troco que apenas seja possivel processar através de moedas, chamamos o método que trata de retornar essa informação
            if (amount < bills[0])
            {
                // Adiciona o valor inteiro que sobra ao valor decimal retirado previamente.
                coinsAmount += amount;

                // Igualamos a propriedade Coins o resultado da função getCoinsChange
                change.Coins = GetCoinsChange(coins, coinsAmount);
            }

            // retorna o troco que contêm notas e moedas ou apenas notas
            return change;
        }

        private static List<decimal> GetCoinsChange(int[] coins, decimal amount)
        {
            // Inicializa a lista de decimais a retornar
            List<decimal> results = new List<decimal>();

            // Organiza o array do valor mais pequeno ao maior
            Array.Sort(coins);

            // Enquanto existir troco continua a processar
            while (amount > 0.00M)
            {
                // Itera pelo numero de moedas
                for (int j = 1; j < coins.Length; j++)
                {
                    // Se o valor total for menor que a moeda em iteração, adicionamos o valor anterior à lista de resultados
                    if (amount - coins[j] < 0)
                    {
                        amount -= coins[j - 1];
                        results.Add(coins[j - 1]);
                    }
                }
            }
            return results;
        }
    }
}
