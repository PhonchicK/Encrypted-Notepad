using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace WpfUI.Helpers
{
    public static class FileHelper
    {
        public static string TempDirectoryName = "FilePat";
        public static string TempFolderPath = Path.Combine(Path.GetTempPath(), TempDirectoryName);

        public static void SaveAndOpenFile(byte[] data, string fileName)
        {
            ControlDirectory();

            File.WriteAllBytes(TempFolderPath + "/" + fileName, data);

            Process.Start(TempFolderPath + "/" + fileName);
        }

        public static void ControlDirectory()
        {
            if(!Directory.Exists(TempFolderPath))
            {
                Directory.CreateDirectory(TempFolderPath);
            }
        }

        public static void ClearDirectory()
        {
            foreach(string item in Directory.GetFiles(TempFolderPath))
            {
                File.Delete(item);
            }
        }
    }
}
