using System;

namespace Metroit.Ddd.Application.Services
{
    /// <summary>
    /// ストレージ操作を提供します。
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// 指定されたパスが存在するかを確認します。
        /// </summary>
        /// <param name="path">存在を確認するパス。</param>
        /// <returns>存在すれば true、それ以外は false を返却します。</returns>
        bool Exists(string path);

        /// <summary>
        /// 指定されたパスを作成します。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        void Create(string path);

        /// <summary>
        /// 指定されたパスを作成しようとします。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        bool TryCreate(string path);

        /// <summary>
        /// 指定されたパスを削除します。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        void Delete(string path);

        /// <summary>
        /// 指定されたパスを削除します。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        void Delete(string path, bool recursive);

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        bool TryDelete(string path);

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        bool TryDelete(string path, bool recursive);

        /// <summary>
        /// パスの作成日時を取得します。
        /// </summary>
        /// <param name="path">作成日時を取得するパス。</param>
        /// <returns>作成日時。</returns>
        DateTime GetCreationTime(string path);

        /// <summary>
        /// パスの更新日時を取得します。
        /// </summary>
        /// <param name="path">更新日時を取得するパス。</param>
        /// <returns>更新日時。</returns>
        DateTime GetLastWriteTime(string path);

        /// <summary>
        /// パスのルートパスを取得します。
        /// </summary>
        /// <param name="path">ルートパスルートパスを含むパス。</param>
        /// <returns>ルートパス。</returns>
        string GetPathRoot(string path);

        /// <summary>
        /// パスの絶対パスを取得します。
        /// </summary>
        /// <param name="path">絶対パスを取得するパス。</param>
        /// <returns>絶対パス。</returns>
        string GetFullPath(string path);

        /// <summary>
        /// パスの親となるパスを取得します。
        /// </summary>
        /// <param name="path">親のパスを含むパス。</param>
        /// <returns>親のパス。</returns>
        string GetParentPath(string path);

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>
        /// 相対パスとベースパスから絶対パスを取得します。
        /// </summary>
        /// <param name="path"><paramref name="basePath"/> に連結する相対パス。</param>
        /// <param name="basePath">絶対パスの先頭。</param>
        /// <returns>絶対パス。</returns>
        string GetFullPath(string path, string basePath);

        /// <summary>
        /// あるパスから別のパスへの相対パスを返します。
        /// </summary>
        /// <param name="relativeTo">結果の基準となるソースパス。</param>
        /// <param name="path">ターゲットパス。</param>
        /// <returns>相対パス。</returns>
        string GetRelativePath(string relativeTo, string path);
#endif
    }
}
