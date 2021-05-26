using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    class OrderingModel
    {
        public int ID { get; set; }
        public string Batch { get; set; }
        public string BRSTN { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string AccountName2 { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string ChkType { get; set; }
        public string CheckName { get; set; }
        public string BookType { get; set; }
        public int Style { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryBrstn { get; set; }
        public string DeliveryBranch { get; set; }
        public string PickUpRc { get; set; }
        public string Channel { get; set; }
        public int OrdQuantiy { get; set; }
        public string StartingSerial { get; set; }
        public string EndingSerial { get; set; }
        public string   Delivery0 { get; set; }
        public DateTime OrderDate { get; set; }
        public string outputFolder { get; set; }
        public string DeliveryBranchCode { get; set; }

        public string BranchCode { get; set; }
    }
}
