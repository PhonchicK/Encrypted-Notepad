using Business.Abstract;
using Business.DependencyResolvers.Ninject;
using Entities.Concrete;
using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfUI.Helpers;
using WpfUI.Models;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for NotesList.xaml
    /// </summary>
    public partial class NotesList : Page
    {
        //Variables
        private Folder folder;//in of folder
        private string folderPassword = "";

        //Forms
        private PasswordForm passwordForm;
        private NamePasswordForm namePasswordForm;
        private FileLoadForm fileLoadForm;

        //Dialogs
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        //Services
        private INoteService noteService;
        private IFolderService folderService;

        public NotesList()
        {
            InitializeComponent();
            noteService = InstanceFactory.GetInstance<INoteService>();
            folderService = InstanceFactory.GetInstance<IFolderService>();
        }

        #region Page Methods
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNotes();
        }
        #endregion

        #region Note & Folder Methods
        public void LoadNotes()
        {
            backButton.Visibility = folder != null ? Visibility.Visible : Visibility.Hidden;
            //if not in folder, back button is hide

            List<NotesViewModel> notesViewModels = new List<NotesViewModel>();
            if (folder != null)
            {
                foreach (var item in noteService.GetAllByFolderID(folder.ID))
                {
                    notesViewModels.Add(new NotesViewModel(item));
                }
            }
            else
            {
                foreach (var item in folderService.GetAll())
                {
                    notesViewModels.Add(new NotesViewModel(item));
                }
                foreach (var item in noteService.GetAllTopFolder())
                {
                    notesViewModels.Add(new NotesViewModel(item));
                }
            }

            NotesListView.ItemsSource = notesViewModels;
        }
        private void OpenFolder(NotesViewModel model)
        {
            if (model.HavePassword)
            {
                passwordForm = new PasswordForm();
                if (passwordForm.ShowDialog().GetValueOrDefault(false))
                {
                    if (PasswordHelper.PasswordControl(model.Password, passwordForm.Password))
                    {
                        folder = folderService.GetByID(model.ID);
                        folderPassword = passwordForm.Password;

                        LoadNotes();
                    }
                }
            }
            else
            {
                folder = folderService.GetByID(model.ID);
                folderPassword = "";
                LoadNotes();
            }
        }
        private void OpenNote(NotesViewModel model)
        {
            if (model.HavePassword)
            {
                passwordForm = new PasswordForm();
                if (passwordForm.ShowDialog().GetValueOrDefault(false))
                {
                    Note note = noteService.GetByID(model.ID);
                    if (PasswordHelper.PasswordControl(note.Password, passwordForm.Password))
                        MainWindow.instance.OpenNote(note, passwordForm.Password);
                }
            }
            else
            {
                Note note = noteService.GetByID(model.ID);
                MainWindow.instance.OpenNote(note);
            }
        }
        private void DeleteNote(NotesViewModel model)
        {
            if (MessageBox.Show("Do you want to delete " + model.Name, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!model.IsFolder)
                {
                    noteService.Delete(new Note() { ID = model.ID });
                    LoadNotes();
                }
                else
                {
                    folderService.Delete(new Folder() { ID = model.ID });
                    LoadNotes();
                }
            }
        }
        private void NewNote()
        {
            namePasswordForm = new NamePasswordForm();
            if (namePasswordForm.ShowDialog().GetValueOrDefault(false))
            {
                noteService.Add(new Note()
                {
                    Name = namePasswordForm.Name,
                    Password = PasswordHelper.EncryptPassword(namePasswordForm.Password),
                    Type = "text",
                    FolderID = folder?.ID
                });
                LoadNotes();
            }
        }
        private void NewFolder()
        {
            namePasswordForm = new NamePasswordForm();
            if (namePasswordForm.ShowDialog().GetValueOrDefault(false))
            {
                folderService.Add(new Folder()
                {
                    Name = namePasswordForm.Name,
                    Password = PasswordHelper.EncryptPassword(namePasswordForm.Password),
                });
                LoadNotes();
            }
        }
        private void NewFile(List<string> files = null)
        {
            if(files == null)
            {
                openFileDialog = new OpenFileDialog()
                {
                    Multiselect = true,
                    RestoreDirectory = true
                };
                if(openFileDialog.ShowDialog().GetValueOrDefault(false))
                {
                    files = openFileDialog.FileNames.ToList();
                }
                else
                {
                    return;
                }
            }
            //if files is null, select new files

            string password = "";

            if (folder == null)
            {
                passwordForm = new PasswordForm(true);
                if (passwordForm.ShowDialog().GetValueOrDefault(false))
                {
                    password = passwordForm.Password;
                }
                else
                    return;
            }
            else
            {
                password = folderPassword;
            }
            //if in folder, password is folder's password

            fileLoadForm = new FileLoadForm(files, password,
                        folder?.ID);
            fileLoadForm.ShowDialog();

            LoadNotes();
        }
        private void DownloadFile(NotesViewModel model)
        {
            if (model == null)
                return;

            string password = "";
            if (model.HavePassword)
            {
                passwordForm = new PasswordForm();
                if (passwordForm.ShowDialog().GetValueOrDefault(false))
                {
                    if (PasswordHelper.PasswordControl(model.Password, passwordForm.Password))
                        password = passwordForm.Password;
                    else
                        return;
                }
                else
                    return;
            }
            saveFileDialog = new SaveFileDialog()
            {
                FileName = model.Name,
                Title = "Save " + model.Name
            };
            if (saveFileDialog.ShowDialog().GetValueOrDefault(false))
            {
                Note fileNote = noteService.GetByID(model.ID);
                File.WriteAllBytes(saveFileDialog.FileName, NoteCryptionHelper.DecryptFile(fileNote.Content, password));
            }
            else
                return;
        }
        #endregion

        #region Context Menu Methods
        private void NotesListView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            NotesViewModel note = (NotesListView.SelectedItem as NotesViewModel);
            NotesListContextMenuDeleteButton.IsEnabled = note != null;
            NotesListContextMenuOpenButton.IsEnabled = note != null;
            NotesListContextMenuDownloadButton.IsEnabled = note != null ? note.IsFile : false;
        }
        private void NotesListContextMenuOpenButton_Click(object sender, RoutedEventArgs e)
        {
            NotesViewModel note = (NotesListView.SelectedItem as NotesViewModel);
            if (note.IsFolder)
            {
                OpenFolder(note);
            }
            else
            {
                OpenNote(note);
            }
        }
        private void NotesListContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteNote(NotesListView.SelectedItem as NotesViewModel);
        }
        private void NotesListContextMenuNewButton_Click(object sender, RoutedEventArgs e)
        {
            switch((sender as MenuItem).Header)
            {
                case "Note": NewNote(); break;
                case "Folder": NewFolder(); break;
                case "File": NewFile(); break;
            }
        }
        private void NotesListContextMenuDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            DownloadFile(NotesListView.SelectedItem as NotesViewModel);
        }
        #endregion

        #region Notes List View Methods
        private void BackToParent()
        {
            folder = null;
            folderPassword = "";
            LoadNotes();
        }
        private void NotesListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if a list view item not under of cursor, selected item set null
            var elem = NotesListView.InputHitTest(e.GetPosition(NotesListView));
            if (elem is FrameworkElement && (elem as FrameworkElement).DataContext == null)
            {
                NotesListView.SelectedItem = null;
            }
        }
        private void NotesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NotesViewModel note = (NotesListView.SelectedItem as NotesViewModel);
            if (note == null)
                return;
            if (note.IsFolder)
            {
                OpenFolder(note);
            }
            else
            {
                OpenNote(note);
            }
        }
        private void NotesListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effects = DragDropEffects.Move;
        }
        private void NotesListView_Drop(object sender, DragEventArgs e)
        {
            var dropped = ((string[])e.Data.GetData(DataFormats.FileDrop));
            var files = dropped.ToList();
            NewFile(files);
        }
        private void NotesListView_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Enter:
                    OpenNote(NotesListView.SelectedItem as NotesViewModel);
                    break;
                case Key.Delete:
                    DeleteNote(NotesListView.SelectedItem as NotesViewModel);
                    break;
            }
        }
        #endregion

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            BackToParent();
        }
    }
}
