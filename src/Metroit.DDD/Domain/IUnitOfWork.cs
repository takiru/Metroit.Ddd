using System;

namespace Metroit.Ddd.Domain
{
    /// <summary>
    /// ユニットオブワークを提供します。
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// ユニットオブワークを開始します。
        /// </summary>
        void Begin();

        /// <summary>
        /// ユニットオブワークを完了します。
        /// </summary>
        void Complete();

        /// <summary>
        /// ユニットオブワークをキャンセルします。
        /// </summary>
        void Cancel();
    }
}
