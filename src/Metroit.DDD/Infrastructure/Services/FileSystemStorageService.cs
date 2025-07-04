using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metroit.DDD.Infrastructure.Services
{
    public class FileSystemStorageService
    {
        public bool ExistsFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("ファイルパスは必須です。", nameof(filePath));
            }
            return System.IO.File.Exists(filePath);
        }
    }
}
