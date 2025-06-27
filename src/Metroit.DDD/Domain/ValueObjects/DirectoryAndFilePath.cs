using System;
using System.IO;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// ディレクトリパスとファイル名を提供します。
    /// </summary>
    public class DirectoryAndFilePath : FileSystemPath
    {
        /// <summary>
        /// ディレクトリパスを取得します。
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// ファイル名を取得します。
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="directory">ディレクトリパス。</param>
        /// <param name="fileName">ファイル名。</param>
        public DirectoryAndFilePath(string directory, string fileName) : base(Path.Combine(directory, fileName))
        {
            Directory = directory ?? throw new ArgumentNullException(nameof(directory));
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }

        /// <summary>
        /// ファイルが存在するかどうかを確認します。
        /// </summary>
        /// <returns>存在する場合は true, それ以外は false を返却します。</returns>
        public override bool Exists()
        {
            return File.Exists(Value);
        }

        /// <summary>
        /// ファイルが存在しない場合、新しいファイルを作成します。
        /// </summary>
        public override void Create()
        {
            if (!Exists())
            {
                File.Create(Value);
            }
        }

        /// <summary>
        /// 指定されたファイルを削除します。
        /// </summary>
        public override void Delete()
        {
            if (Exists())
            {
                File.Delete(Value);
            }
        }
    }
}
