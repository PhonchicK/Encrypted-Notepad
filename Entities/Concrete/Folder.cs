using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Folder : IEntity
    {
        public Folder()
        {
            Notes = new HashSet<Note>();
            Folders = new HashSet<Folder>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public int? ParentFolderID { get; set; }
        public virtual Folder ParentFolder { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }
    }
}
