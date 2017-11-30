using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Models;

namespace SMProject
{
    static class Program
    {
        /// <summary>
        /// main program
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmLogin objFrmLogin = new FrmLogin();
            DialogResult result = objFrmLogin.ShowDialog();

            if (result == DialogResult.OK)
            {
                Application.Run(new FrmSaleManage());
            }
            else
            {
                Application.Exit();
            }

            
        }


        public static SalesPerson objCurrentPerson = null;

    }
}
