using Metroit.DDD.Domain.ValueObjects;
using System;

namespace Metroit.DDD.Application.Services.Generic
{
    /// <summary>
    /// ストレージ操作を提供します。
    /// </summary>
    public interface IStorageService<T> where T : ISingleValueObject
    {
        /// <summary>
        /// 指定されたパスが存在するかを確認します。
        /// </summary>
        /// <param name="path">存在を確認するパス。</param>
        /// <returns>存在すれば true、それ以外は false を返却します。</returns>
        bool Exists(T path);

        /// <summary>
        /// 指定されたパスを作成します。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        void Create(T path);

        /// <summary>
        /// 指定されたパスを作成しようとします。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        bool TryCreate(T path);

        /// <summary>
        /// 指定されたパスを削除します。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        void Delete(T path);

        /// <summary>
        /// 指定されたパスを削除します。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        void Delete(T path, bool recursive);

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        bool TryDelete(T path);

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        bool TryDelete(T path, bool recursive);

        /// <summary>
        /// パスの作成日時を取得します。
        /// </summary>
        /// <param name="path">作成日時を取得するパス。</param>
        /// <returns>作成日時。</returns>
        DateTime GetCreationTime(T path);

        /// <summary>
        /// パスの更新日時を取得します。
        /// </summary>
        /// <param name="path">更新日時を取得するパス。</param>
        /// <returns>更新日時。</returns>
        DateTime GetLastWriteTime(T path);

        /// <summary>
        /// パスのルートディレクトリを取得します。
        /// </summary>
        /// <param name="path">ルートディレクトリを含むパス。</param>
        /// <returns>ルートディレクトリ。</returns>
        string GetPathRoot(T path);

        /// <summary>
        /// パスの絶対パスを取得します。
        /// </summary>
        /// <param name="path">絶対パスを取得するパス。</param>
        /// <returns>絶対パス。</returns>
        string GetFullPath(T path);

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>
        /// 相対パスとベースパスから絶対パスを取得します。
        /// </summary>
        /// <param name="path"><paramref name="basePath"/> に連結する相対パス。</param>
        /// <param name="basePath">絶対パスの先頭。</param>
        /// <returns>絶対パス。</returns>
        string GetFullPath(T path, T basePath);

        /// <summary>
        /// あるパスから別のパスへの相対パスを返します。
        /// </summary>
        /// <param name="relativeTo">結果の基準となるソースパス。</param>
        /// <param name="path">ターゲットパス。</param>
        /// <returns>相対パス。</returns>
        string GetRelativePath(T relativeTo, T path);
#endif
    }
}
