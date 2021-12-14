using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.UiModel
{
    public class UserOrderFormVO
    {
        public UserOrderFormVO() { }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DoctorName { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public string SexName { get; set; }

        [DataMember]
        public string PatNo { get; set; }

        [DataMember]
        public string DeptName { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public string DoctorMemo { get; set; }

        [DataMember]
        public string AreaID { get; set; }

        [DataMember]
        public List<UserOrderItemVO> UserOrderItem { get; set; }
    }
    public class UserOrderItemVO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long ItemID { get; set; }

        [DataMember]
        public string ItemNo { get; set; }
    }
}
