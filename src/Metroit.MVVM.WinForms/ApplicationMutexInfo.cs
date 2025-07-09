using System;

namespace Metroit.MVVM.WinForms
{
    /// <summary>
    /// アプリケーションミューテックスを行うときに使用する値のセットを提供します。
    /// </summary>
    public class ApplicationMutexInfo
    {
        private string _name;

        /// <summary>
        /// ミューテックスの名前を設定または取得します。
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Name));
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("The mutex name is empty.", nameof(Name));
                }
                _name = value;
            }
        }

        /// <summary>
        /// ミューテックスがロックできなかったときの振る舞いを設定または取得します。
        /// </summary>
        public ApplicationMutexBehavior CanNotLockedBehavior { get; set; } = ApplicationMutexBehavior.Shutdown;

        /// <summary>
        /// <see cref="CanNotLockedBehavior"/> が <see cref="ApplicationMutexBehavior.Shutdown"/> のとき、シャットダウン前に行う制御を取得または設定します。
        /// </summary>
        public Action ShuttingDown { get; set; } = null;

        /// <summary>
        /// <see cref="CanNotLockedBehavior"/> が <see cref="ApplicationMutexBehavior.Shutdown"/> のとき、シャットダウンした結果を取得または設定します。
        /// </summary>
        public int ShutdownExitCode { get; set; } = -1;

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="name">ミューテックス名。</param>
        public ApplicationMutexInfo(string name)
        {
            Name = name;
        }
    }
}
