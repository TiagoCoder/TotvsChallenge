using System.Collections.Generic;

namespace CodeChallenge.Application.Entities
{
    public class ChangeDTO
    {
        public List<int> Bills { get; set; }

        public List<decimal> Coins { get; set; }
    }
}
