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
using WpfUI.Models;
using WpfUI.Views;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private INoteService noteService;
        NotesList notesList;
        public MainWindow()
        {
            noteService = InstanceFactory.GetInstance<INoteService>();
            notesList = new NotesList();
            InitializeComponent();
        }
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
        #endregion

        private void NotesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var item = (NotesListView.SelectedItem as NotesViewModel);
        }

        private void NotesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //(sender as ListBox).SelectedItem = null;
        }
    }
}
