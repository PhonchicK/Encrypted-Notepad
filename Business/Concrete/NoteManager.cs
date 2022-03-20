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
    public class NoteManager : INoteService
    {
        private INoteDal noteDal;
        public NoteManager(INoteDal _noteDal)
        {
            noteDal = _noteDal;
        }
        public void Add(Note note)
        {
            noteDal.Add(note);
        }

        public void Delete(Note note)
        {
            noteDal.Delete(note);
        }

        public List<Note> GetAll()
        {
            return noteDal.GetAll();
        }

        public List<Note> GetAllByFolderID(int folderId)
        {
            return noteDal.GetAll(n => n.FolderID == folderId);
        }

        public List<Note> GetAllTopFolder()
        {
            return noteDal.GetAll(n => n.FolderID == null);
        }

        public Note GetByID(int id)
        {
            return noteDal.Get(n => n.ID == id);
        }

        public void Update(Note note)
        {
            noteDal.Update(note);
        }
    }
}
