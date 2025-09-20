using System;
using System.Threading.Tasks;

namespace Metroit.Ddd.Application.Interfaces
{
    /// <summary>
    /// ユニットオブワークを提供します。
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// ユニットオブワークを開始します。
        /// </summary>
        IUnitOfWork Begin();

        /// <summary>
        /// ユニットオブワークを開始します。
        /// </summary>
        Task<IUnitOfWork> BeginAsync();

        /// <summary>
        /// ユニットオブワークを完了します。
        /// </summary>
        void Complete();

        /// <summary>
        /// ユニットオブワークを完了します。
        /// </summary>
        Task CompleteAsync();

        /// <summary>
        /// ユニットオブワークをキャンセルします。
        /// </summary>
        void Cancel();

        /// <summary>
        /// ユニットオブワークをキャンセルします。
        /// </summary>
        Task CancelAsync();
    }
}
