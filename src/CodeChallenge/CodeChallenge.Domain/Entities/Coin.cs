using CodeChallenge.Domain.Entities.Common;

namespace CodeChallenge.Domain.Entities
{
    public class Coin : EntityBasicRoot<int>
    {
        public decimal Value { get; set; }
    }
}
