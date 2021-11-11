using CodeChallenge.Domain.Entities.Common.Interfaces;

namespace CodeChallenge.Domain.Entities.Common
{
    public class EntityBasicRoot<TKey> : IEntity<TKey>
    {
        public EntityBasicRoot(){}

        public TKey Id { get; set; }
    }
}
