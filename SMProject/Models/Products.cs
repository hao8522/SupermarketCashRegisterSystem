using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Products
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; }
        public int Discount { get; set; }
        public int CategoryId { get; set; }



        //extension
        public int Num { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }


        public string CategoryName { get; set; }

    }
}
