using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecnoGo.Layers.UI;

namespace TecnoGo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogin ofrmLogin = new FrmLogin();

            ofrmLogin.Show();

            if (ofrmLogin.DialogResult == DialogResult.OK)
            {
                Application.Run(new FrmPrincipal());
            }
        }
    }
}
