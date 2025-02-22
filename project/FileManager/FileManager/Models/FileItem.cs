using System;
using System.IO;

namespace FileManager.Models
{
    public class FileItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime Modified { get; set; }
        public bool IsDirectory { get; set; }

        public FileItem(FileInfo file)
        {
            Name = file.Name;
            Path = file.FullName;
            Size = file.Length;
            Modified = file.LastWriteTime;
            IsDirectory = false;
        }

        public FileItem(DirectoryInfo directory)
        {
            Name = directory.Name;
            Path = directory.FullName;
            Size = 0;
            Modified = directory.LastWriteTime;
            IsDirectory = true;
        }
    }
}
