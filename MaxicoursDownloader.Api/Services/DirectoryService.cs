using MaxicoursDownloader.Api.Contracts;
using System;
using System.IO;
using System.Linq;

namespace MaxicoursDownloader.Api.Services
{
    public class DirectoryService : IDirectoryService
    {
        public bool Create(string path)
        {
            if (Directory.Exists(path))
                return true;

            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception)
            {
                return false;
            }

            if (!Directory.Exists(path))
                return false;

            return true;

        }

        public bool Delete(string path)
        {
            if (!Directory.Exists(path))
                return true;

            var files = Directory.GetFiles(path);
            var dirs = Directory.GetDirectories(path);

            // Delete the files in the current directory
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            // Then, delete the sub directories
            foreach (var dir in dirs)
            {
                Delete(dir);
            }

            // Finally, delete the base directory
            try
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                //var security = Directory.GetAccessControl(path);
                //security.AddAccessRule(new FileSystemAccessRule(@"Domain\Olivier", FileSystemRights.FullControl, AccessControlType.Allow));
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception)
                {
                }
            }

            if (Directory.Exists(path))
                return false;

            return false;
        }

        public int GetNbFiles(string path, string pattern)
        {
            return Directory.EnumerateFiles(path, pattern, SearchOption.AllDirectories).Count();
        }
    }
}
