using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{
    [DataContract]
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
    [DataContract]
    public class UserOrderItemVO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long ItemID { get; set; }

        [DataMember]
        public string ItemNo { get; set; }
    }
    [DataContract]
    public class ConsumerUserOrderFormVO
    {
        public ConsumerUserOrderFormVO() { }

        /// <summary>
        /// 消费码
        /// </summary>
        [DataMember]
        public string PayCode { get; set; }

        /// <summary>
        /// 采样单位ID
        /// </summary>
        [DataMember]
        public string WeblisSourceOrgID { get; set; }

        /// <summary>
        /// 采样单位名称
        /// </summary>
        [DataMember]
        public string WeblisSourceOrgName { get; set; }

        /// <summary>
        /// 采样单位所属区域ID
        /// </summary>
        [DataMember]
        public string ConsumerAreaID { get; set; }
    }

}
