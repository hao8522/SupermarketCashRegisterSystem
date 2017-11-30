using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using Models;
using DAL;



namespace SMProject
{
    public partial class FrmLogin : Form
    {
        private SalesPersonsService objSalesPersonService = new SalesPersonsService();
        public FrmLogin()
        {
            InitializeComponent();
        }
        //Login button
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region data validation

            if (this.txtLoginId.Text.Trim().Length == 0 && this.txtLoignPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("LoginId or Password can not be null","Warning");
                return;
            }



            #endregion

            #region database connection

            SalesPerson objPerson = new SalesPerson()
            {
                SalesPersonId = Convert.ToInt32(this.txtLoginId.Text.Trim()),
                LoginPwd = this.txtLoignPwd.Text.Trim()
            };

          

            try
            {
                objPerson = objSalesPersonService.UserLogin(objPerson);


                if (objPerson == null)
                {
                    MessageBox.Show("The LoginId or Password is incorrect ","Warning");
                    return;
                }
                else
                {
                    Program.objCurrentPerson = objPerson;

                    Program.objCurrentPerson.LoginLogId = objSalesPersonService.WriteLoginLog(
                        
                      new LoginLogs()
                    {
                        LoginId = Convert.ToInt32(this.txtLoginId.Text.Trim()),
                        SPName= objPerson.SPName,
                        ServerName= Dns.GetHostName(),

                    });

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Sales Person Login error:"+ex.Message);
            }


            #endregion

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
