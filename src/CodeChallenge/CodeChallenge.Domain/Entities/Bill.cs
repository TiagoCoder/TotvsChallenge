using CodeChallenge.Domain.Entities.Common;

namespace CodeChallenge.Domain.Entities
{
    public class Bill : EntityBasicRoot<int>
    {
        public decimal Value { get; set; }
    }
}
