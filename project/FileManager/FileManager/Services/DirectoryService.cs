using System;
using System.IO;

namespace FileManager.Services
{
    public class DirectoryService
    {
        public bool CreateDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string[] GetDrives()
        {
            return Directory.GetLogicalDrives();
        }
    }
}
