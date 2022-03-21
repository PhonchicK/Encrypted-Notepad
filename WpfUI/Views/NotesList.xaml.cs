using Business.Abstract;
using Business.DependencyResolvers.Ninject;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        }
        private void NotesListContextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            NotesViewModel note = (NotesListView.SelectedItem as NotesViewModel);
            if (MessageBox.Show("Do you want to delete " + note.Name, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (!note.IsFolder)
                {
                    noteService.Delete(new Note() { ID = note.ID });
                    LoadNotes();
                }
                else
                {
                    folderService.Delete(new Folder() { ID = note.ID });
                    LoadNotes();
                }
            }
        }
        private void NotesListContextMenuNewButton_Click(object sender, RoutedEventArgs e)
        {
            switch((sender as MenuItem).Header)
            {
                case "Note": NewNote(); break;
                case "Folder": NewFolder(); break;
            }
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
        #endregion

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            BackToParent();
        }
    }
}
