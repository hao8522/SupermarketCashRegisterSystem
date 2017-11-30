using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Models;
using DAL;


namespace SMProject
{
    public partial class FrmSaleManage : Form
    {


        #region  frame login and drag

        private Point mouseOff;
        private bool leftFlag;
        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); 
                leftFlag = true;                 
            }
        }
        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y); 
                Location = mouseSet;
            }
        }
        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }

        #endregion

        private SalesPersonsService objSalesPerService = new SalesPersonsService();
        public FrmSaleManage()
        {
            InitializeComponent();
         
        }

        private void FrmSaleManage_FormClosing(object sender, FormClosingEventArgs e)
        {

           DialogResult result= MessageBox.Show("Are you sure to logout?","Warning",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);

           if (result == DialogResult.Cancel)
           {
               e.Cancel = true;
           }
           else
           {

               DateTime dt = SQLHelper.GetServerTime();

               objSalesPerService.WriteExitLog(Program.objCurrentPerson.LoginLogId,dt);
           }


        }
     
    }
}
