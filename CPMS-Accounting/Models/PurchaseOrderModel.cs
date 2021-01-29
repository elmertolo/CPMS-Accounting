using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class PurchaseOrderModel
    {
        public int PurchaseOrderNumber { get; set; }
        public DateTime PurchaseOrderDateTime { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string ChequeName { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double Docstamp { get; set; }
        public string CheckType { get; set; }
        public string ClientCode { get; set; }
        public string GeneratedBy { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }

    }
}
