using System.IO;

namespace Metroit.DDD.Domain.ValueObjects
{
    /// <summary>
    /// ファイルパスを提供します。
    /// </summary>
    public class FilePath : FileSystemPath
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="file">ファイルパス。</param>
        public FilePath(string file) : base(file) { }

        ///// <summary>
        ///// ファイルが存在するかどうかを確認します。
        ///// </summary>
        ///// <returns>存在する場合は true, それ以外は false を返却します。</returns>
        //public override bool Exists()
        //{
        //    return File.Exists(Value);
        //}

        ///// <summary>
        ///// ファイルが存在しない場合、新しいファイルを作成します。
        ///// </summary>
        //public override void Create()
        //{
        //    if (!Exists())
        //    {
        //        File.Create(Value);
        //    }
        //}

        ///// <summary>
        ///// 指定されたファイルを削除します。
        ///// </summary>
        //public override void Delete()
        //{
        //    if (Exists())
        //    {
        //        File.Delete(Value);
        //    }
        //}
    }
}
