using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class SalesInvoiceModel
    {
        public double SalesInvoiceNumber { get; set; }
        public string Batch { get; set; }
        public int Quantity { get; set; }
        public double LineTotalAmount { get; set; }
        public string CheckName { get; set; }
        public string DRList { get; set; }
        public DateTime SalesInvoiceDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Checktype { get; set; }
        public double UnitPrice { get; set; }
        public string PreparedBy { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }
        /// <summary>
        /// NA_01252021 update. added PO Number Tracking
        /// </summary>
        public int PurchaseOrderNumber { get; set; }

        public double RemainingQuantity { get; set; }
        public string Location { get; set; }
        public string ProductCode { get; set; }

        //03192021 Enhancement - Sales Invoice Reprint
        public string UOM { get; set; }


    }
}
