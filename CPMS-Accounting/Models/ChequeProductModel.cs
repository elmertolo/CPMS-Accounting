using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    class ChequeProductModel
    {
        public int ProductCode { get; set; }
        public string  ProductName { get; set; }
        public int Flag { get; set; }
        public DateTime DateModified { get; set; }
    }
}
