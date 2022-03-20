using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.Helpers;

namespace WpfUI.Models
{
    internal class NotesViewModel
    {
        public NotesViewModel(Note note)
        {
            this.ID = note.ID;
            this.Name = note.Name;
            this.Type = note.Type;
            this.Image = "pack://application:,,,/images/" + (Type == "text" ? "text.png" : NoteImageHelper.GetFileImage(Name));
            this.IsFile = note.Type == "file";
        }
        public NotesViewModel(Folder folder)
        {
            this.ID = folder.ID;
            this.Name = folder.Name;
            this.Type = "folder";
            this.Image = "pack://application:,,,/images/folder.png";
            this.IsFolder = true;
        }
        public NotesViewModel()
        { }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public bool IsFolder { get; set; } = false;
        public bool IsFile { get; set; } = false;
    }
}
