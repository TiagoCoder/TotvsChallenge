using CodeChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CodeChallenge.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        #region Methods

        /// <summary>
        /// Insere novos registos na base de dados. Este método já invoca o SaveChangesAsync().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>int</returns>
        Task<int> InsertAsync<T>(T entity, CancellationToken cancellationToken) where T : class;

        /// <summary>
        /// Atualiza registos existentes na base de dados. Este método já invoca o SaveChangesAsync().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>int</returns>
        Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken) where T : class;

        /// <summary>
        /// Elimina registos da base de dados. Não deve ser usado com regularidade. Este método já invoca o SaveChangesAsync().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>int</returns>
        Task<int> DeleteAsync<T>(T entity, CancellationToken cancellationToken) where T : class;

        /// <summary>
        /// Retira o tracking de uma entidade no EntityFramework Core. Faz com que essa entidade não seja marcada para alterações durante o SaveChangesAsync().
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void Detach<T>(T entity) where T : class;

        /// <summary>
        /// Realiza e efetiva todas as operações de base de dados registadas no EntityFramework Core.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        #endregion

        #region DbSets
        public DbSet<Bill> Bills { get; set; }

        public DbSet<Coin> Coins { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        #endregion
    }
}
