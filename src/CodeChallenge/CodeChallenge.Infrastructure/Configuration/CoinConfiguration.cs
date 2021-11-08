using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.Infrastructure.Configuration
{
    public class CoinConfiguration : IEntityTypeConfiguration<Coin>
    {
        public void Configure(EntityTypeBuilder<Coin> builder)
        {
            builder.Property(p => p.Value).HasPrecision(5, 2);

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
