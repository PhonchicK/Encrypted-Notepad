using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFolderService
    {
        List<Folder> GetAll();
        List<Folder> GetAllByFolderID(int folderId);
        Folder GetByID(int id);
        void Add(Folder folder);
        void Update(Folder folder);
        void Delete(Folder folder);
    }
}
