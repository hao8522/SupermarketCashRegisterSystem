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
   public  class SalesPersonsService
    {
       public SalesPerson UserLogin(SalesPerson objSales)
       {
           string sql = "select SPName from SalesPerson where SalesPersonId=@SalesPersonId and LoginPwd=@LoginPwd ";

           SqlParameter[] param = new SqlParameter[]{

               new SqlParameter("@SalesPersonId ",objSales.SalesPersonId),
                  new SqlParameter("@LoginPwd",objSales.LoginPwd),
           };

           SqlDataReader objReader = SQLHelper.GetReader(sql,param);
           SalesPerson sP = null;

           if (objReader.Read())
           {

               sP = new SalesPerson()
               {
                   SPName = objReader["SPName"].ToString()
               };
           }
           else
           {
               sP = null;
           }


           // method 2
           //object result = SQLHelper.GetSingleResult(sql, param);

           //if (result == null)
           //{
           //    return null;
           //}
           //else
           //{
           //    objSales.SPName = result.ToString();
           //}

           //return objSales;

           objReader.Close();

           return sP;
       }
    }
}
