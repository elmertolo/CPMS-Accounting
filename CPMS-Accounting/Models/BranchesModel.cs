using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    class BranchesModel
    {
        public string BRSTN { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Address6 { get; set; }
        public int BranchCode { get; set; }
        public int OldBranchCode { get; set; }
        public int Flag { get; set; }
        //04122021 Added new fields in replacement for cpc files.
        public string Location { get; set; }
        public string ContactPerson { get; set; }


    }
}
