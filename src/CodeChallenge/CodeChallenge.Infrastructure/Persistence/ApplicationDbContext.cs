using CodeChallenge.Application.Common;
using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(DbContextOptions options,
            ILogger<ApplicationDbContext> logger
            )
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<Coin> Coins { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        public async Task<int> InsertAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            var dbSet = Set<T>();

            dbSet.Add(entity);

            return await SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Entry(entity).State = EntityState.Modified;

            return await SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            Entry(entity).State = EntityState.Deleted;

            return await SaveChangesAsync(cancellationToken);
        }

        public void Detach<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Detached;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbException)
            {
                _logger.LogError("[Database::Update::Exception] An error ocurred while updating data.", dbException);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("[Database::Exception] An unexpected error ocurred.", ex);
                throw;
            }
        }
    }
}
