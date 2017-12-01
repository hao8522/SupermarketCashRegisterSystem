using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ProductService
    {
        #region Get Product By Id

        public Products GetProductById(string productId)
        {
            string sql = "select ProductId,ProductName,UnitPrice,Discount from Products where ProductId=@ProductId";

            SqlParameter[] param = new SqlParameter[]{

                new SqlParameter("@ProductId",productId)
            };

            Products objPro = null;

            SqlDataReader objReader = SQLHelper.GetReader(sql, param);

            try
            {
                if (objReader.Read())
                {
                    objPro = new Products()
                    {
                        ProductId = objReader["ProductId"].ToString(),
                        ProductName = objReader["ProductName"].ToString(),
                        UnitPrice = Convert.ToDecimal(objReader["UnitPrice"]),
                        Discount = Convert.ToInt32(objReader["Discount"])
                    };
                }

                objReader.Close();
                return objPro;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion



        #region Save Product Info
        public bool SaveSaleInfo(SalesListMain objSaleList, Members objMembers)
        {
            List<string> sqlList = new List<string>();

            string mainSql = "insert into SaleList(SerialNum,TotalMoney,RealReceive,ReturnMoney,SalesPersonId) values('{0}','{1}','{2}','{3}','{4}')";

            mainSql= string.Format(mainSql,objSaleList.SerialNum,objSaleList.TotalMoney,objSaleList.RealReceive,objSaleList.ReturnMoney,objSaleList.SalesPersonId);
         
            sqlList.Add(mainSql);


            // insert data to sale list detail

            foreach(SalesListDetail detail in objSaleList.ListDetail){

                string detailSql = "insert into SalesListDetail(SerialNum,ProductId,ProductName,UnitPrice,Discount,Quantity,SubTotalMoney)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";

                detailSql = string.Format(detailSql,detail.SerialNum,detail.ProductId,detail.ProductName,detail.UnitPrice,detail.Discount,detail.Quantity,detail.SubTotalMoney);

                sqlList.Add(detailSql);

                string updateSql = "update ProductInventory set TotalCount=TotalCount-'{0}' where ProductId='{0}' ";
               
                updateSql= string.Format(updateSql,detail.Quantity,detail.ProductId);

                sqlList.Add(updateSql);
            }

            if (objMembers != null)
            {
                string pointSql = "update SMMembers set Points=Points+'{0}' where MemberId='{1}'";

                pointSql = string.Format(pointSql, objMembers.Points, objMembers.MemberId);
                sqlList.Add(pointSql);
            }

          

            return SQLHelper.UpdateByTransaction(sqlList);
        }

        #endregion
    }
}
