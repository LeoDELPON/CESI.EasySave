using System;
using System.Collections.Generic;
using System.IO;

namespace CESI.BS.EasySave.DAL
{
    public class FileCompare : IEqualityComparer<FileInfo>
    {
        private FileCompare() { }

        public bool Equals(FileInfo f1, FileInfo f2)
        {
            return (f1.Name == f2.Name &&
                    f1.Length == f2.Length);
        }

        public int GetHashCode(FileInfo fi)
        {
            string s = $"{fi.Name}{fi.Length}";
            return s.GetHashCode();
        }

        private static readonly Lazy<FileCompare> lazy = new Lazy<FileCompare>(() => new FileCompare());
        public static FileCompare Instance { get { return lazy.Value; } }
    }
}
