using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class CostDistributionModel
    {

        public int ProductCode { get; set; }
        public string BranchName { get; set; }
        public string ChequeName { get; set; }
        public string UOM { get; set; }
        public double Quantity { get; set; }
        public string DRNumbers { get; set; }
        public string SOL { get; set; }
        public string RC { get; set; }
        public double PrintingCost { get; set; }
        public double DocStampCost { get; set; }
        public string Multiplier { get; set; }
        public string Location { get; set; }



    }
}
