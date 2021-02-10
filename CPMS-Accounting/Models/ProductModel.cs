using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    class ProductModel
    {
        [Required]
        public string  ProductCode { get; set; }
        public string BankCode { get; set; }
        public string ChequeName { get; set; }
        public string Description { get; set; }
        [Required]
        public string ChkType { get; set; }
        [Required]
        public double UnitPrice  { get; set; }
        [Required]
        public double DocStampPrice { get; set; }
        public DateTime DateModified { get; set; }
        public string Unit { get; set; }
        public int BalanceQuantity { get; set; }
        [Required]
        public string DeliveryLocation { get; set; }

    }
}
