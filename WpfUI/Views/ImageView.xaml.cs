using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : Page
    {
        private Note currentNote;
        private string currentPassword;
        public ImageView(Note note, string password = "")
        {
            InitializeComponent();
            currentNote = note;
            currentPassword = password;
            imageBox.Source = ConvertHelper.ByteArrayToBitmapImage(NoteCryptionHelper.DecryptFile(note.Content, password));
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.instance.BackToNotesList();
        }
    }
}
