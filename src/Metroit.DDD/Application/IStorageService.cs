using Metroit.DDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metroit.DDD.Application
{
    public interface IStorageService
    {
        /// <summary>
        /// 指定されたパスのファイルが存在するかを確認します。
        /// </summary>
        /// <param name="path">ファイルパス（ドメインエンティティが保持しているパスなど）</param>
        /// <returns>ファイルが存在すれば true、存在しなければ false</returns>
        bool Exists(string path);

        void Delete(string path);

        /// <summary>
        /// ファイルを削除します。<br/>
        /// 削除に失敗しても例外はスローしません。
        /// </summary>
        /// <param name="path">ファイル。</param>
        void DeleteNotRaiseException(string path);
    }

    public interface IStorageService<T> where T : FileSystemPath
    {
        /// <summary>
        /// 指定されたパスのファイルが存在するかを確認します。
        /// </summary>
        /// <param name="path">ファイルパス（ドメインエンティティが保持しているパスなど）</param>
        /// <returns>ファイルが存在すれば true、存在しなければ false</returns>
        bool Exists(T path);
    }
}
