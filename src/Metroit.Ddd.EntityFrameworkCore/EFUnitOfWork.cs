using Metroit.Ddd.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Metroit.Ddd.EntityFrameworkCore
{
    /// <summary>
    /// Entity Framework Coreを使用したユニットオブワークの実装を提供します。
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="dbContext"><see cref="DbContext"/> オブジェクト。</param>
        public EFUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// ユニットオブワークを開始します。
        /// </summary>
        public void Begin() => _dbContext.Database.BeginTransaction();

        /// <summary>
        /// ユニットオブワークを完了します。
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Complete() => _dbContext.Database.CommitTransaction();

        /// <summary>
        /// ユニットオブワークをキャンセルします。
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Cancel() => _dbContext.Database.RollbackTransaction();

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