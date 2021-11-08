using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.Infrastructure.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.Property(p => p.Value).HasPrecision(5, 2);

            builder.HasData
                (
                    new Bill
                    {
                        Id = 1,
                        Value = 100.00M
                    },
                    new Bill
                    {
                        Id = 2,
                        Value = 50.00M
                    },
                    new Bill
                    {
                        Id = 3,
                        Value = 20.00M
                    },
                    new Bill
                    {
                        Id = 4,
                        Value = 10.00M
                    }
                ); ;
        }
    }
}
