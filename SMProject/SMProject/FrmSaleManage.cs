﻿using System;
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
        private List<Products> productsList = new List<Products>();
        private ProductService objProService = new ProductService();
        private BindingSource bs = new BindingSource();

        public FrmSaleManage()
        {
            InitializeComponent();
            this.lblSalePerson.Text = Program.objCurrentPerson.SPName;
            this.lblSerialNum.Text = this.CreateSerialNumber();
            this.dgvProdutList.AutoGenerateColumns = false;
        }

        private string CreateSerialNumber()
        {
            string serialNumber = SQLHelper.GetServerTime().ToString("yyyyMMddHHmmssms");
            Random r = new Random();
            serialNumber += r.Next(10, 99);

            return serialNumber;

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

        private void txtProductId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (!ValidaeInput()) return;
             
                // check product by product id
                var pList = from p in this.productsList 
                            where p.ProductId.Equals(this.txtProductId.Text.Trim())
                            select p;


                if (pList.Count() == 0)
                {
                    AddNewProductToList();
                }
                else
                {
                    // if product is exist, add quanity and subtotal
                    Products objProduct = pList.FirstOrDefault<Products>();
                    objProduct.Quantity += Convert.ToInt32(this.txtQuantity.Text.Trim());
                    objProduct.SubTotal = objProduct.Quantity * objProduct.UnitPrice;

                    if (objProduct.Discount != 0)
                    {

                        objProduct.SubTotal *= (Convert.ToDecimal(objProduct.Discount) / 10);
                    }
                }


                // display product
                this.bs.DataSource = this.productsList;
                this.dgvProdutList.DataSource = null;
                this.dgvProdutList.DataSource = this.bs;

                // caculate subtotal
                this.lblTotalMoney.Text = (from p in this.productsList select p.SubTotal).Sum().ToString();

                this.txtProductId.Clear();
                this.txtQuantity.Text = "1";
                this.txtDiscount.Text = "0";
                this.txtUnitPrice.Text = "0.0";
                this.lblReceivedMoney.Text = "0";
                this.lblReturnMoney.Text = "0.0";
                this.txtProductId.Focus();
            }
            else if (e.KeyValue == 38)  // move up
            {
                if (this.dgvProdutList.RowCount == 0 || this.dgvProdutList.RowCount == 1) return;

                this.bs.MovePrevious();
               
            }
            else if (e.KeyValue == 40)  // move down
            {
                if (this.dgvProdutList.RowCount == 0 || this.dgvProdutList.RowCount == 1) return;
                this.bs.MoveNext();
            }
            else if (e.KeyValue == 46)   // delete current line
            {
                if (this.dgvProdutList.RowCount == 0) return;
                this.bs.RemoveCurrent();
                this.dgvProdutList.DataSource = null;
                this.dgvProdutList.DataSource = this.bs;
            }
        }

        #region Add Product to List
        private void AddNewProductToList()
        {
            Products objProducts = objProService.GetProductById(this.txtProductId.Text.Trim());

            if (objProducts == null)
            {
                objProducts = new Products(){

                        ProductId= this.txtProductId.Text.Trim(),
                        ProductName="Unknow Item",
                        UnitPrice= Convert.ToDecimal(this.txtUnitPrice.Text.Trim()),
                        Discount= Convert.ToInt32(this.txtDiscount.Text.Trim())

                };


            }
            else
            {

                // if the product is exist, get discount and unit price
                this.txtDiscount.Text = objProducts.Discount.ToString();
                this.txtUnitPrice.Text = objProducts.UnitPrice.ToString();
            }

            // get product quanitity and subtotal
            objProducts.Quantity = Convert.ToInt32(this.txtQuantity.Text.Trim());
            objProducts.SubTotal = Convert.ToDecimal(objProducts.Quantity) * objProducts.UnitPrice;


            if (objProducts.Discount != 0)
            {
                objProducts.SubTotal *= (Convert.ToDecimal(objProducts.Discount) / 10);
            }

            objProducts.Num = this.productsList.Count + 1;
            this.productsList.Add(objProducts);



        }
        #endregion


        #region product validation

        private bool ValidaeInput()
        {
            if (this.txtProductId.Text.Trim().Length == 0) return false;



            return true;
            
        }

        #endregion

        private void dgvProdutList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.lblTotalMoney.Text = (from p in this.productsList select p.SubTotal).Sum().ToString();

            for (int i = 0; i < this.productsList.Count; i++)
            {
                this.productsList[i].Num = i + 1;
            }
        }

    }
}
