using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class ClientListModel
    {
        public string ClientCode { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string AttentionTo { get; set; }
        public string Princes_DESC { get; set; }
        public string TIN { get; set; }
        public decimal WithholdingTaxPercentage { get; set; }
        public string DataBaseName { get; set; }
        public string SalesInvoiceTempTable { get; set; }
        public string SalesInvoiceFinishedTable { get; set; }
        public string PurchaseOrderFinishedTable { get; set; }
        public string PriceListTable { get; set; }
        public string DRTempTable { get; set; }

        public string DocStampTempTable { get; set; } // Added By ET Jan. 22, 2021
        public string BranchesTable { get; set; } // Added By ET Jan. 27, 2021

    }
}
