using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    class ChequeTypesModel
    {
        public string Type { get; set; }
        public string ChequeName { get; set; }
        public string Description { get; set; }
        public DateTime DateModified { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
    }
}
