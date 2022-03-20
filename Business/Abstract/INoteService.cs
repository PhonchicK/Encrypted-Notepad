using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INoteService
    {
        List<Note> GetAll();
        List<Note> GetAllTopFolder();
        List<Note> GetAllByFolderID(int folderId);
        Note GetByID(int id);
        void Add(Note note);
        void Update(Note note);
        void Delete(Note note);
    }
}
