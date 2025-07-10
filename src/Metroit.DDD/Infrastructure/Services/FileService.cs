using Metroit.DDD.Application.Services;
using System;
using System.IO;

namespace Metroit.DDD.Infrastructure.Services
{
    /// <summary>
    /// ファイル操作を提供します。
    /// </summary>
    public class FileService : IStorageService
    {
        /// <summary>
        /// 指定されたパスが存在するかを確認します。
        /// </summary>
        /// <param name="path">存在を確認するパス。</param>
        /// <returns>存在すれば true、それ以外は false を返却します。</returns>
        public virtual bool Exists(string path) => File.Exists(path);

        /// <summary>
        /// 指定されたパスを作成します。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        public virtual void Create(string path)
        {
            File.Create(path);
        }

        /// <summary>
        /// 指定されたパスを作成しようとします。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        public bool TryCreate(string path)
        {
            try
            {
                Create(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 指定されたパスを削除します。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        public void Delete(string path)
        {
            Delete(path, false);
        }

        /// <summary>
        /// 指定されたパスを削除します。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        public virtual void Delete(string path, bool recursive)
        {
            File.Delete(path);
        }

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        public bool TryDelete(string path)
        {
            return TryDelete(path, false);
        }

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        public virtual bool TryDelete(string path, bool recursive)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// パスの作成日時を取得します。
        /// </summary>
        /// <param name="path">作成日時を取得するパス。</param>
        /// <returns>作成日時。</returns>
        public virtual DateTime GetCreationTime(string path)
        {
            var f = new FileInfo(path);
            return f.CreationTime;
        }

        /// <summary>
        /// パスの更新日時を取得します。
        /// </summary>
        /// <param name="path">更新日時を取得するパス。</param>
        /// <returns>更新日時。</returns>
        public virtual DateTime GetLastWriteTime(string path)
        {
            var f = new FileInfo(path);
            return f.LastWriteTime;
        }

        /// <summary>
        /// パスのルートパスを取得します。
        /// </summary>
        /// <param name="path">ルートパスルートパスを含むパス。</param>
        /// <returns>ルートパス。</returns>
        public virtual string GetPathRoot(string path)
        {
            return Path.GetPathRoot(path);
        }

        /// <summary>
        /// パスの絶対パスを取得します。
        /// </summary>
        /// <param name="path">絶対パスを取得するパス。</param>
        /// <returns>絶対パス。</returns>
        public virtual string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }

        /// <summary>
        /// パスの親となるパスを取得します。
        /// </summary>
        /// <param name="path">親のパスを含むパス。</param>
        /// <returns>親のパス。</returns>
        public virtual string GetParentPath(string path)
        {
            return Path.GetDirectoryName(path);
        }

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>
        /// 相対パスとベースパスから絶対パスを取得します。
        /// </summary>
        /// <param name="path"><paramref name="basePath"/> に連結する相対パス。</param>
        /// <param name="basePath">絶対パスの先頭。</param>
        /// <returns>絶対パス。</returns>
        public virtual string GetFullPath(string path, string basePath)
        {
            return Path.GetFullPath(path, basePath);
        }

        /// <summary>
        /// あるパスから別のパスへの相対パスを返します。
        /// </summary>
        /// <param name="relativeTo">結果の基準となるソースパス。</param>
        /// <param name="path">ターゲットパス。</param>
        /// <returns>相対パス。</returns>
        public virtual string GetRelativePath(string relativeTo, string path)
        {
            return Path.GetRelativePath(relativeTo, path);
        }
#endif
    }
}
