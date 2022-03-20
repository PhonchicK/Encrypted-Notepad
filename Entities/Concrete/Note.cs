using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Note : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }

        public int? FolderID { get; set; }

        public virtual Folder Folder { get; set; }
    }
}
