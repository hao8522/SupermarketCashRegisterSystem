using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMProject
{
    public partial class FrmBalance : Form
    {
        
        public FrmBalance(string totalMoney)
        {
            InitializeComponent();

            this.lblTotalMoney.Text = totalMoney;
            this.txtRealReceive.Text = totalMoney;
            this.txtRealReceive.SelectAll();
            this.txtRealReceive.Focus();
        }

        private void txtMemberId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                // check out
                if (this.txtMemberId.Text.Trim().Length == 0)
                {
                    this.Tag = this.txtRealReceive.Text.Trim();
                }
                else
                {
                    // member id
                    this.Tag = this.txtRealReceive.Text.Trim() + "|" + this.txtMemberId.Text.Trim();

                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (e.KeyValue == 115)
            {
                // key F4 ,give up check out
                this.Tag = "F4";
                this.Close();
            }
            else
            {
                // key F5   not enough money  pay some money
                this.Tag = "F5";
                this.Close();
            }
        }     
    }
}
