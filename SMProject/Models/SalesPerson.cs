using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class SalesPerson
    {
        public int SalesPersonId { get; set; }
        public string SPName { get; set; }
        public string LoginPwd { get; set; }

        // extention

        public int LoginLogId { get; set; }
    }
}
