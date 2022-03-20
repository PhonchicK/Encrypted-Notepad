using Business.Abstract;
using Business.DependencyResolvers.Ninject;
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
using WpfUI.Models;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for NotesList.xaml
    /// </summary>
    public partial class NotesList : Page
    {
        private INoteService noteService;
        private IFolderService folderService;
        public NotesList()
        {
            InitializeComponent();
            noteService = InstanceFactory.GetInstance<INoteService>();
            folderService = InstanceFactory.GetInstance<IFolderService>();
        }

        private void LoadNotes()
        {
            List<NotesViewModel> notesViewModels = new List<NotesViewModel>();
            foreach (var item in noteService.GetAllTopFolder())
            {
                notesViewModels.Add(new NotesViewModel(item));
            }
            foreach (var item in folderService.GetAll())
            {
                notesViewModels.Add(new NotesViewModel(item));
            }

            NotesListView.ItemsSource = notesViewModels;
        }
        private NotesViewModel GetSelectedNote(Point point)
        {
            var elem = NotesListView.InputHitTest(point);
            //Your ListView or DataGrid will have set the DataContext to your bound item 
            if (elem is FrameworkElement && (elem as FrameworkElement).DataContext != null)
            {
                return (elem as FrameworkElement).DataContext as NotesViewModel;
            }
            return null;
        }

        #region Page Methods
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNotes();
        }
        #endregion

        private void NotesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NotesViewModel note = GetSelectedNote(e.GetPosition(NotesListView));
            if (note == null)
                return;
        }

        private void NotesListView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            NotesViewModel note = GetSelectedNote(NotesListView.TranslatePoint(new Point(e.CursorLeft, e.CursorTop),NotesListView));
            NotesListContextMenuDeleteButton.IsEnabled = note != null;
            NotesListContextMenuOpenButton.IsEnabled = note != null;
            NotesListContextMenuDownloadButton.IsEnabled = note != null ? note.IsFile : false;
        }
    }
}
