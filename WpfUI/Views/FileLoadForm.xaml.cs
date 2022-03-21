using Business.Abstract;
using Business.DependencyResolvers.Ninject;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Entities.Concrete;
using WpfUI.Helpers;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for FileLoadForm.xaml
    /// </summary>
    public partial class FileLoadForm : Window
    {
        //Variables
        private List<string> files = new List<string>();
        private string password;
        private int? folderId;

        //Services
        private INoteService noteService;

        public FileLoadForm(List<string> _files, string _password, int? _folderId = null)
        {
            InitializeComponent();

            noteService = InstanceFactory.GetInstance<INoteService>();

            files = _files;
            password = _password;
            folderId = _folderId;
        }

        #region Form Methods
        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFiles();
        }
        #endregion

        private async void LoadFiles()
        {
            foreach(var item in files)
            {
                fileStatus.Content = Path.GetFileName(item);
                byte[] fileBytes = File.ReadAllBytes(item);
                noteService.Add(new Note()
                {
                    Name = Path.GetFileName(item),
                    Password = PasswordHelper.EncryptPassword(password),
                    Content = NoteCryptionHelper.EncryptFile(fileBytes),
                    FolderID = folderId,
                    Type = "file"
                });

                fileLoadigProgress.Value += 100 / files.Count; 
            }
            this.DialogResult = true;
        }
    }
}
