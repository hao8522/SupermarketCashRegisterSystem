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
                        ProductId= objReader["ProductId"].ToString(),
                        ProductName= objReader["ProductName"].ToString(),
                        UnitPrice = Convert.ToDecimal(objReader["UnitPrice"]),
                        Discount= Convert.ToInt32(objReader["Discount"])
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
    }
}
