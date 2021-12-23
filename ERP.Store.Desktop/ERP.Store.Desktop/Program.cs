using System;
using System.Windows.Forms;

namespace ERP.Store.Desktop
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.Login.frmLogin());
            //Application.Run(new Forms.Employees.frmRegisterNewEmployee());
        }
    }
}
