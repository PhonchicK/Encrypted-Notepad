using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FolderManager : IFolderService
    {
        private IFolderDal folderDal;
        public FolderManager(IFolderDal _folderDal)
        {
            folderDal = _folderDal;
        }
        public void Add(Folder folder)
        {
            folderDal.Add(folder);
        }

        public void Delete(Folder folder)
        {
            folderDal.Delete(folder);
        }

        public List<Folder> GetAll()
        {
            return folderDal.GetAll();
        }

        public Folder GetByID(int id)
        {
            return folderDal.Get(f => f.ID == id);
        }

        public void Update(Folder folder)
        {
            folderDal.Update(folder);
        }
    }
}
