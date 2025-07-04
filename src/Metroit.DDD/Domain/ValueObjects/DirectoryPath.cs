using System.IO;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// ディレクトリパスを提供します。
    /// </summary>
    public class DirectoryPath : FileSystemPath
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="directory">ディレクトリパス。</param>
        public DirectoryPath(string directory) : base(directory) { }

        ///// <summary>
        ///// ディレクトリが存在するかどうかを確認します。
        ///// </summary>
        ///// <returns>存在する場合は true, それ以外は false を返却します。</returns>
        //public override bool Exists()
        //{
        //    return Directory.Exists(Value);
        //}

        ///// <summary>
        ///// ディレクトリが存在しない場合、新しいディレクトリを作成します。
        ///// </summary>
        //public override void Create()
        //{
        //    if (!Exists())
        //    {
        //        Directory.CreateDirectory(Value);
        //    }
        //}

        ///// <summary>
        ///// 指定されたディレクトリを削除します。
        ///// </summary>
        //public override void Delete()
        //{
        //    if (Exists())
        //    {
        //        Directory.Delete(Value, true);
        //    }
        //}
    }
}
