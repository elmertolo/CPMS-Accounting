using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class SalesInvoiceFinishedModel
    {
        public string ClientCode { get; set; }
        public double SalesInvoiceNumber { get; set; }
        public DateTime SalesInvoiceDateTime { get; set; }
        public string GeneratedBy { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovedBy { get; set; }
        public double TotalAmount { get; set; }
        public double VatAmount { get; set; }
        public double NetOfVatAmount { get; set; }


    }
}
