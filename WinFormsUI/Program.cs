using System;
using System.Windows.Forms;
using Test1ApplicationCore;

namespace WinFormsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new IdViewForm();
            var presenter = new LongIdentifiePresenter(view);

            Application.Run(view);
        }
    }
}
