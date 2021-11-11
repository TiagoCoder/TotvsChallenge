using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.Infrastructure.Configuration
{
    /// <summary>
    /// Configures the entity injected.
    /// Seeds data
    /// </summary>
    /// <param name="builder"></param>
    public class CoinConfiguration : IEntityTypeConfiguration<Coin>
    {
        public void Configure(EntityTypeBuilder<Coin> builder)
        {
            // Sets the decimal precision for the Value property
            builder.Property(p => p.Value).HasPrecision(5, 2);

            // Seeds data on first app inicialization
            builder.HasData
                (
                    new Coin
                    {
                        Id = 1,
                        Value = 0.50M
                    },
                    new Coin
                    {
                        Id = 2,
                        Value = 0.10M
                    },
                    new Coin
                    {
                        Id = 3,
                        Value = 0.05M
                    },
                    new Bill
                    {
                        Id = 4,
                        Value = 0.01M
                    }
                );
        }
    }
}
