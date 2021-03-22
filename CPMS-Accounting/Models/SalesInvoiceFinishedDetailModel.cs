using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class SalesInvoiceFinishedDetailModel
    {
        public string ProductCode { get; set; }
        public int SalesInvoiceNumber { get; set; }
        public int PurchaseOrderNumber { get; set; }
        public int PurchaseOrderBalance { get; set; }
        public string BatchName { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public string CheckName { get; set; }
        public string Location { get; set; }
        public string DRList { get; set; }
        public double LineTotalAmount { get; set; }
        public double UnitPrice { get; set; }
        public string CheckType { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime SalesInvoiceDate { get; set; }




    }
}
