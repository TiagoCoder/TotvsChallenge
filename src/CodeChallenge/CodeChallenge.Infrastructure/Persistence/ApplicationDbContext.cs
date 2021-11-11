using CodeChallenge.Application.Common.Interfaces;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        #region Constructor
        public ApplicationDbContext(DbContextOptions options)
            : base(options) { }
        #endregion

        #region DbSets
        public DbSet<Bill> Bills { get; set; }

        public DbSet<Coin> Coins { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        #endregion

        #region Generic Methods
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
                //_logger.LogError("[Database::Update::Exception] An error ocurred while updating data.", dbException);
                throw;
            }
            catch (Exception ex)
            {
                // _logger.LogError("[Database::Exception] An unexpected error ocurred.", ex);
                throw;
            }
        }

        #endregion

        #region Seeds Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new CoinConfiguration());
        }
        #endregion
    }
}
