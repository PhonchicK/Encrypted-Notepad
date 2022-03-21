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
    /// Interaction logic for NamePasswordForm.xaml
    /// </summary>
    public partial class NamePasswordForm : Window
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public NamePasswordForm(string name = "", string password = "")
        {
            InitializeComponent();
            this.Name = name;
            this.Password = password;
            nameBox.Text = name;
            passwordBox.Password = password;
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Name = nameBox.Text;
            this.Password = passwordBox.Password;

            if(!string.IsNullOrWhiteSpace(nameBox.Text))
            {
                this.DialogResult = true;
            }
        }
    }
}
