using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Domain.Entities.Common.Interfaces
{
    public interface IEntity<TKey>
    {
        [Key]
        TKey Id { get; set; }
    }
}
