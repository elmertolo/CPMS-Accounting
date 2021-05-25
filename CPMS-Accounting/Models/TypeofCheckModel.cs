using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CPMS_Accounting.Models
{
    class TypeofCheckModel
    {
        public List<OrderModel> Regular_Personal { get; set; }
        public List<OrderModel> Regular_Commercial { get; set; }
        public List<OrderModel> Regular_Commercial_Direct { get; set; }
        public List<OrderModel> Regular_Personal_Direct { get; set; }
        public List<OrderModel> Regular_Commercial_Provincial { get; set; }
        public List<OrderModel> Regular_Personal_Provincial { get; set; }
        public List<OrderModel> ManagersCheck { get; set; }
        public List<OrderModel> ManagersCheck_Direct { get; set; }
        public List<OrderModel> ManagersCheck_Provincial { get; set; }
        public List<OrderModel> ExecutiveOnline_Direct { get; set; }
        public List<OrderModel> ExecutiveOnline_Provincial { get; set; }
        public List<OrderModel> DragonBlue_Commercial_Direct { get; set; }
        public List<OrderModel> DragonBlue_Personal_Direct { get; set; }
        public List<OrderModel> DragonBlue_Commercial_Provicial { get; set; }
        public List<OrderModel> DragonBlue_Personal_Provincial { get; set; }
        public List<OrderModel> DragonYellow_Commercial_Direct { get; set; }
        public List<OrderModel> DragonYellow_Personal_Direct { get; set; }
        public List<OrderModel> DragonYellow_Commercial_Provincial { get; set; }
        public List<OrderModel> DragonYellow_Personal_Provincial { get; set; }
        public List<OrderModel> Reca_Commercial_Direct { get; set; }
        public List<OrderModel> Reca_Personal_Direct { get; set; }
        public List<OrderModel> Reca_Commercial_Provincial { get; set; }
        public List<OrderModel> Reca_Personal_Provincial { get; set; }
        public List<OrderModel> Online_Commercial_Direct { get; set; }
        public List<OrderModel> Online_Personal_Direct { get; set; }
        public List<OrderModel> Online_Commercial_Provincial { get; set; }
        public List<OrderModel> Online_Personal_Provincial { get; set; }
        public List<OrderModel> Customized_Direct { get; set; }
        public List<OrderModel> Customized_Provincial { get; set; }
        public List<OrderingModel> Ordering_Regular_Personal { get; set; }
        public List<OrderingModel> Ordering_Regular_Commercial { get; set; }
        public List<OrderingModel> Ordering_DragonBlue_Personal { get; set; }
        public List<OrderingModel> Ordering_DragonBlue_Commercial { get; set; }
        public List<OrderingModel> Ordering_DragonYellow_Personal { get; set; }
        public List<OrderingModel> Ordering_DragonYellow_Commercial { get; set; }
        public List<OrderingModel> Ordering_Reca_Personal { get; set; }
        public List<OrderingModel> Ordering_Reca_Commercial { get; set; }
        public List<OrderingModel> Ordering_Online_Personal { get; set; }
        public List<OrderingModel> Ordering_Online_Commercial { get; set; }
        public List<OrderingModel> Ordering_Customized { get; set; }


    }
}
