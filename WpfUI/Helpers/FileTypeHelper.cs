using System;
using System.Collections.Generic;
using System.IO;

namespace WpfUI.Helpers
{
    public static class FileTypeHelper
    {
        public static List<FileType> fileTypes = new List<FileType>
        {
            new FileType()
            {
                Type = "image",
                Extensions = new List<string>
                {
                    ".png",
                    ".jpg",
                    ".jpeg"
                }
            },
            new FileType()
            {
                Type = "video",
                Extensions = new List<string>
                {
                    ".mp4",
                    ".mov",
                    ".flv",
                    ".avi",
                    ".mkv"
                }
            },
            new FileType()
            {
                Type = "sound",
                Extensions = new List<string>
                {
                    ".m4a",
                    ".mp3",
                    ".wav",
                    ".aac"
                }
            }
        };
        public static string GetFileType(string fileName)
        {
            foreach (FileType type in fileTypes)
            {
                if (type.Extensions.Contains(Path.GetExtension(fileName)))
                    return type.Type;
            }
            return "file";
        }
    }

    public class FileType
    {
        public string Type { get; set; }
        public List<string> Extensions { get; set; }
    }
}
