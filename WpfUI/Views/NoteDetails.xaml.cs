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

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for NoteDetails.xaml
    /// </summary>
    public partial class NoteDetails : Page
    {
        //Variables
        private Note currentNote;
        private string currentPassword;

        //Forms
        NamePasswordForm namePasswordForm;

        //Services
        private INoteService noteService;

        public NoteDetails(Note note, string password = "")
        {
            currentNote = note;
            currentPassword = password;

            InitializeComponent();

            noteService = InstanceFactory.GetInstance<INoteService>();

            LoadNote();
        }
        private string GetContentText()
        {
            return new TextRange(textBoxContent.Document.ContentStart, textBoxContent.Document.ContentEnd).Text;
        }
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.S:
                    if (Keyboard.Modifiers == ModifierKeys.Control)//Ctrl + S
                        SaveNote();
                    break;
            }
        }

        #region Note Methods
        private void LoadNote()
        {
            textBoxContent.Document.Blocks.Clear();
            textBoxContent.AppendText(NoteCryptionHelper.DecryptText(currentNote.Content, currentPassword));
        }
        private void SaveNote()
        {
            currentNote.Content = NoteCryptionHelper.EncryptText(GetContentText(), currentPassword);
            noteService.Update(currentNote);
        }
        private void EditNote()
        {
            namePasswordForm = new NamePasswordForm(currentNote.Name, currentPassword);
            if(namePasswordForm.ShowDialog().GetValueOrDefault(false))
            {
                currentNote.Name = namePasswordForm.Name;
                currentNote.Password = PasswordHelper.EncryptPassword(namePasswordForm.Password);
                currentPassword = namePasswordForm.Password;
                currentNote.Content = NoteCryptionHelper.EncryptText(GetContentText(), currentPassword);

                noteService.Update(currentNote);
            }
        }
        private void DeleteNote()
        {
            if (MessageBox.Show("Do you want to delete " + currentNote.Name, 
                "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                noteService.Delete(currentNote);
                MainWindow.instance.notesList.LoadNotes();
                MainWindow.instance.BackToNotesList();
            }
        }
        #endregion

        #region TopBar Buttons
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.BackToNotesList();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveNote();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditNote();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteNote();
        }
        private void ResetChangesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadNote();
        }
        #endregion
    }
}
