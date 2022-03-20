using Business.DependencyResolvers.Ninject;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Components;

namespace FormUI
{
    static class Program
    {
        public static IKernel Kernel { get; set; }
        public static MetroStyleManager styleManager;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Kernel = new StandardKernel();
            Kernel.Load(new BusinessModule());

            styleManager = new MetroStyleManager()
            {
                Theme = MetroFramework.MetroThemeStyle.Dark,
                Style = MetroFramework.MetroColorStyle.Pink
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
