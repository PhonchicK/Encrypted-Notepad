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
        public NotesList()
        {
            InitializeComponent();
            noteService = InstanceFactory.GetInstance<INoteService>();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<NotesViewModel> notesViewModels = new List<NotesViewModel>();
            foreach (var item in noteService.GetAllTopFolder())
            {
                notesViewModels.Add(new NotesViewModel(item));
            }

            NotesListView.ItemsSource = notesViewModels;
        }

        private void NotesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NotesListView.SelectedItem == null)
                return;

        }
    }
}
