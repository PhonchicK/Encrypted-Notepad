using Business.Abstract;
using Business.DependencyResolvers.Ninject;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfUI.Views;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Variables
        public static MainWindow instance;

        //Forms
        public NotesList notesList;
        private NoteDetails noteDetails;
        private ImageView imageView;

        //Services
        private INoteService noteService;

        public MainWindow()
        {
            noteService = InstanceFactory.GetInstance<INoteService>();

            instance = this;
            notesList = new NotesList();

            InitializeComponent();
        }

        public void OpenFile(Note note, string password = "")
        {
            FileHelper.SaveAndOpenFile(NoteCryptionHelper.DecryptFile(note.Content, password), note.Name);
        }

        #region Navigation Methods
        public void BackToNotesList()
        {
            MainFrame.Navigate(notesList);
        }

        public void OpenNote(Note note, string password = "")
        {
            if (note.Type == "text")
            {
                noteDetails = new NoteDetails(note, password);
                MainFrame.Navigate(noteDetails);
            }
            else
            {
                switch(FileTypeHelper.GetFileType(note.Name))
                {
                    case "image":
                        imageView = new ImageView(note, password);
                        MainFrame.Navigate(imageView);
                        break;

                    default:
                        OpenFile(note, password);
                        break;
                }
            }
        }
        #endregion
        #region Form Methods
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(notesList);
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Forward
                || e.NavigationMode == NavigationMode.Back
                || e.NavigationMode == NavigationMode.Refresh)
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}
