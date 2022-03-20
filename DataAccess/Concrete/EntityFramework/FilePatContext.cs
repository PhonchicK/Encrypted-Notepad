using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class FilePatContext : DbContext
    {
        public FilePatContext() : base("name=FilePatDatabase")
        {
            Database.SetInitializer<FilePatContext>(new CreateDatabaseIfNotExists<FilePatContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>()
                .HasMany(c => c.Notes)
                .WithOptional(c => c.Folder)
                .HasForeignKey(c => c.FolderID)
                .WillCascadeOnDelete(false);
        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Folder> Folders { get; set; }
    }
}
