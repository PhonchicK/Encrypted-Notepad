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
using System.Windows.Shapes;

namespace WpfUI.Views
{
    /// <summary>
    /// Interaction logic for PasswordForm.xaml
    /// </summary>
    public partial class PasswordForm : Window
    {
        public bool CanEmpty { get; set; } = false;
        public string Password { get; set; }
        public PasswordForm(bool canEmpty = false)
        {
            InitializeComponent();
            CanEmpty = canEmpty;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordBox.Password) && !CanEmpty)
                return;
            Password = passwordBox.Password;
            this.DialogResult = true;
        }
    }
}
