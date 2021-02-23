using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class UserLevelModel
    {
        public int Number { get; set; }
        public string Code { get; set; }
        public string  Name { get; set; }


        public int IsAllowedOnDr { get; set; }
        public bool IsDrCreateAllowed { get; set; }
        public bool IsDrEditAllowed { get; set; }
        public bool IsDrDeleteAllowed { get; set; }

        
        public int IsAllowedOnUm { get; set; }
        public bool IsUmCreateAllowed { get; set; }
        public bool IsUmEditAllowed { get; set; }
        public bool IsUmDeleteAllowed { get; set; }



        public int IsAllowedOnUl { get; set; }
        public bool IsUlCreateAllowed { get; set; }
        public bool IsUlEditAllowed { get; set; }
        public bool IsUlDeleteAllowed { get; set; }



        public int IsAllowedOnSi { get; set; }
        public bool IsSiCreateAllowed { get; set; }
        public bool IsSiEditAllowed { get; set; }
        public bool IsSiDeleteAllowed { get; set; }



        public int IsAllowedOnPo { get; set; }
        public bool IsPoCreateAllowed { get; set; }
        public bool IsPoEditAllowed { get; set; }
        public bool IsPoDeleteAllowed { get; set; }



        public int IsAllowedOnPm { get; set; }
        public bool IsPmCreateAllowed { get; set; }
        public bool IsPmEditAllowed { get; set; }
        public bool IsPmDeleteAllowed { get; set; }



        public int IsAllowedOnDc { get; set; }
        public bool IsDcCreateAllowed { get; set; }
        public bool IsDcEditAllowed { get; set; }
        public bool IsDcDeleteAllowed { get; set; }


    }
}
