using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfUI.Helpers
{
    internal static class NoteImageHelper
    {
        private static List<FileExtensions> filesAndExtensions = new List<FileExtensions>()
        { 
            new FileExtensions()
            {
                Image = "image.png",
                Extensions = new List<string>()
                {
                    ".png",
                    ".jpg",
                    ".jpeg"
                }
            },
            new FileExtensions()
            {
                Image = "sound.png",
                Extensions = new List<string>()
                {
                    ".wav",
                    ".mp3",
                    ".m4a"
                }
            },
            new FileExtensions()
            {
                Image = "video.png",
                Extensions = new List<string>()
                {
                    ".avi",
                    ".wmv",
                    ".mp4",
                    ".mpg",
                    ".mpeg",
                    ".m4v"
                }
            }
        };

        internal static string GetFileImage(string name)
        {
            foreach (var item in filesAndExtensions)
            {
                if(item.Extensions.Contains(Path.GetExtension(name)))
                {
                    return item.Image;
                }
            }
            return "file.png";
        }
    }
    internal class FileExtensions
    {
        public string Image { get; set; }
        public List<string> Extensions { get; set; }
    }
}
