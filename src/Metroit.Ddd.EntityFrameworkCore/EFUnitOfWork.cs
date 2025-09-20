using Metroit.Ddd.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Metroit.Ddd.EntityFrameworkCore
{
    /// <summary>
    /// Entity Framework Coreを使用したユニットオブワークの実装を提供します。
    /// </summary>
    public class EFUnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly T _dbContext;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="dbContext"><see cref="DbContext"/> オブジェクト。</param>
        public EFUnitOfWork(T dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// ユニットオブワークを開始します。
        /// </summary>
        public IUnitOfWork Begin()
        {
            _transaction = _dbContext.Database.BeginTransaction();
            return this;
        }

        /// <summary>
        /// ユニットオブワークを開始します。
        /// </summary>
        public async Task<IUnitOfWork> BeginAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
            return this;
        }

        /// <summary>
        /// ユニットオブワークを完了します。
        /// </summary>
        public void Complete()
        {
            _transaction.Commit();
        }

        /// <summary>
        /// ユニットオブワークを完了します。
        /// </summary>
        public Task CompleteAsync()
        {
            return _transaction.CommitAsync();
        }

        /// <summary>
        /// ユニットオブワークをキャンセルします。
        /// </summary>
        public void Cancel()
        {
            _transaction.Rollback();
        }

        /// <summary>
        /// ユニットオブワークをキャンセルします。
        /// </summary>
        public Task CancelAsync()
        {
            return _transaction.RollbackAsync();
        }

        private bool _disposed = false;

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        /// <param name="disposing">マネージドリソースの解放を行うかどうか。</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        /// ファイナライザです。リソースを解放します。
        /// </summary>
        ~EFUnitOfWork()
        {
            Dispose(false);
        }
    }
}