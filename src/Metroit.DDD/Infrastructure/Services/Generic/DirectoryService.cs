using Metroit.DDD.Application.Services.Generic;
using Metroit.DDD.Domain.ValueObjects;
using System;
using System.IO;

namespace Metroit.DDD.Infrastructure.Services.Generic
{
    /// <summary>
    /// ディレクトリ操作を提供します。
    /// </summary>
    public class DirectoryService<T> : IStorageService<T> where T : ISingleValueObject
    {
        /// <summary>
        /// 指定されたパスが存在するかを確認します。
        /// </summary>
        /// <param name="path">存在を確認するパス。</param>
        /// <returns>存在すれば true、それ以外は false を返却します。</returns>
        public virtual bool Exists(T path) => Directory.Exists(path.Value.ToString());

        /// <summary>
        /// 指定されたパスを作成します。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        public virtual void Create(T path)
        {
            Directory.CreateDirectory(path.Value.ToString());
        }

        /// <summary>
        /// 指定されたパスを作成しようとします。
        /// </summary>
        /// <param name="path">作成するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        public bool TryCreate(T path)
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
        public void Delete(T path)
        {
            Delete(path, false);
        }

        /// <summary>
        /// 指定されたパスを削除します。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        public virtual void Delete(T path, bool recursive)
        {
            Directory.Delete(path.Value.ToString(), recursive);
        }

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        public bool TryDelete(T path)
        {
            return TryDelete(path, false);
        }

        /// <summary>
        /// 指定されたパスを削除しようとします。
        /// </summary>
        /// <param name="path">削除するパス。</param>
        /// <returns>作成できた場合は true, それ以外は false を返却します。</returns>
        /// <param name="recursive">階層のあるパスのとき、下の階層まで削除するかどうか。</param>
        public virtual bool TryDelete(T path, bool recursive)
        {
            try
            {
                Directory.Delete(path.Value.ToString(), recursive);
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
        public virtual DateTime GetCreationTime(T path)
        {
            var f = new DirectoryInfo(path.Value.ToString());
            return f.CreationTime;
        }

        /// <summary>
        /// パスの更新日時を取得します。
        /// </summary>
        /// <param name="path">更新日時を取得するパス。</param>
        /// <returns>更新日時。</returns>
        public virtual DateTime GetLastWriteTime(T path)
        {
            var f = new DirectoryInfo(path.Value.ToString());
            return f.LastWriteTime;
        }

        /// <summary>
        /// パスのルートディレクトリを取得します。
        /// </summary>
        /// <param name="path">ルートディレクトリを含むパス。</param>
        /// <returns>ルートディレクトリ。</returns>
        public virtual string GetPathRoot(T path)
        {
            return Path.GetPathRoot(path.Value.ToString());
        }

        /// <summary>
        /// パスの絶対パスを取得します。
        /// </summary>
        /// <param name="path">絶対パスを取得するパス。</param>
        /// <returns>絶対パス。</returns>
        public virtual string GetFullPath(T path)
        {
            return Path.GetFullPath(path.Value.ToString());
        }

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        /// <summary>
        /// 相対パスとベースパスから絶対パスを取得します。
        /// </summary>
        /// <param name="path"><paramref name="basePath"/> に連結する相対パス。</param>
        /// <param name="basePath">絶対パスの先頭。</param>
        /// <returns>絶対パス。</returns>
        public virtual string GetFullPath(T path, T basePath)
        {
            return Path.GetFullPath(path.Value.ToString(), basePath.Value.ToString());
        }

        /// <summary>
        /// あるパスから別のパスへの相対パスを返します。
        /// </summary>
        /// <param name="relativeTo">結果の基準となるソースパス。</param>
        /// <param name="path">ターゲットパス。</param>
        /// <returns>相対パス。</returns>
        public virtual string GetRelativePath(T relativeTo, T path)
        {
            return Path.GetRelativePath(relativeTo.Value.ToString(), path.Value.ToString());
        }
#endif
    }
}
