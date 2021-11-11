using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.Infrastructure.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        /// <summary>
        /// Configures the entity injected.
        /// Seeds data
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            // Sets the decimal precision for the Value property
            builder.Property(p => p.Value).HasPrecision(5, 2);

            // Seeds data on first app inicialization
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
