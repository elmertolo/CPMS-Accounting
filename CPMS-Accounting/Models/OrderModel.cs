using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    class OrderModel
    {
        public string BRSTN { get; set; }
        public string AccountNo { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Batch { get; set; }
        public string StartingSerial { get; set; }
        public string EndingSerial { get; set; }
        public string BranchName { get; set; }
        public string ChkType { get; set; }
        public string AccountNoWithHypen { get; set; }
        public string ChequeName { get; set; }

        public string BranchCode { get; set; }
        public string OldBranchCode { get; set; }
        public int Segment { get; set; }
        public int  Block { get; set; }
        public string Location { get; set; }
        public string Name3 { get; set; }
        public int PONumber { get; set; }
        public int Quantity { get; set; }
        public string BankCode { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Address6 { get; set; }
        public string DeliveryTo { get; set; }
        public string ProductCode { get; set; }
        public string ProductType { get; set; }

    }
}
