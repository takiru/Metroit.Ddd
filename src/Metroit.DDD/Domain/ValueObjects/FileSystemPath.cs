using Metroit.DDD.Domain.Annotations;
using System;
using System.IO;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// ファイルシステムのパスを表す抽象クラスです。
    /// </summary>
    [VORequired(ErrorMessage = "{0} は必須です。")]
    public class FileSystemPath : SingleValueObject<string>
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="path">ファイルシステムのパス。</param>
        public FileSystemPath(string path) : base(path)
        {
            //Value = path ?? throw new ArgumentNullException(nameof(path), "パスは必須です。");
        }

        ///// <summary>
        ///// ファイルシステムのパスが存在するかどうかを確認します。
        ///// </summary>
        ///// <returns>存在する場合は true, それ以外は false を返却します。</returns>
        //public abstract bool Exists();

        ///// <summary>
        ///// ファイルシステムのパスが存在しない場合、新しいファイルシステムのパスを作成します。
        ///// </summary>
        //public abstract void Create();

        ///// <summary>
        ///// 指定されたファイルシステムのパスを削除します。
        ///// </summary>
        //public abstract void Delete();

        /// <summary>
        /// ファイルシステムのフルパスを取得します。
        /// </summary>
        /// <returns>ファイルシステムのフルパス。</returns>
        public string GetFullPath()
        {
            return Path.GetFullPath(Value);
        }

        ///// <summary>
        ///// ファイルシステムのパスを文字列として返します。
        ///// </summary>
        ///// <returns>ファイルシステムのパス。</returns>
        //public override string ToString() => Value;

//#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
//        /// <summary>
//        /// 指定されたベースパスからのフルパスを取得します。
//        /// </summary>
//        /// <param name="basePath">ベースパス。</param>
//        /// <returns>ファイルシステムのパスのフルパス。</returns>
//        /// <exception cref="ArgumentNullException">ベースパスが null です。</exception>
//        public string GetFullPath(string basePath)
//        {
//            if (string.IsNullOrEmpty(basePath))
//            {
//                throw new ArgumentNullException(nameof(basePath), "ベースパスは必須です。");
//            }
//            return Path.GetFullPath(Value, basePath);
//        }

//        /// <summary>
//        /// 指定されたベースパスからの相対パスを取得します。
//        /// </summary>
//        /// <param name="basePath">ベースパス。</param>
//        /// <returns>ファイルシステムのパスの相対パス。</returns>
//        /// <exception cref="ArgumentNullException">ベースパスが null です。</exception>
//        public string GetRelativePath(string basePath)
//        {
//            if (string.IsNullOrEmpty(basePath))
//            {
//                throw new ArgumentNullException(nameof(basePath), "ベースパスは必須です。");
//            }
//            return Path.GetRelativePath(basePath, Value);
//        }
//#endif
    }
}
