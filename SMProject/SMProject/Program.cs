using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


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
            Application.Run(new FrmSaleManage());
        }



    }
}
