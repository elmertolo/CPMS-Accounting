using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMS_Accounting.Models
{
    public class UserListModel
    {
        public int Number { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Level { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Lockout { get; set; }

    }
}
