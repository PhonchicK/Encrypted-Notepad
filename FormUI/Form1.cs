using Business.Abstract;
using Business.DependencyResolvers.Ninject;
using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI
{
    public partial class Form1 : MetroForm
    {
        INoteService noteService;
        private void UpdateTheme()
        {
            this.StyleManager = Program.styleManager;
            this.StyleManager.Owner = this;
        }
        public Form1()
        {
            noteService = InstanceFactory.GetInstance<INoteService>();
            InitializeComponent();
            UpdateTheme();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
