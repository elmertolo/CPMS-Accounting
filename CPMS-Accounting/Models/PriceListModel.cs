using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class PriceListModel
    {
        public string ProductCode { get; set; }
        public string BankCode { get; set; }
        public string ChequeDescription { get; set; }
        public string ChkType { get; set; }
        public double DocStampPrice { get; set; }
        public double UnitPrice{ get; set; }

    }
}
