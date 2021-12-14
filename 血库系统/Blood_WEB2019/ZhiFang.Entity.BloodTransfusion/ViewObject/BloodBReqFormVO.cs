using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.BloodTransfusion
{
    /// <summary>
    /// 申请主单VO
    /// </summary>
    [DataContract]
    public class BloodBReqFormVO
    {
        public BloodBReqFormVO() { }

        protected long _id;
        protected int? _deptNo;

        /// <summary>
        /// 虚拟Id
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id
        {
            get
            {
                if (_id <= 0)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                return _id;
            }
            set { _id = value; }
        }

        /// <summary>
        /// 登记号
        /// </summary>
        [DataMember]
        public virtual string AdmID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string PatNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string CName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual DateTime? Birthday { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public virtual string AgeALL { get; set; }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int? DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string Bed { get; set; }

        public BloodBReqFormVO(string admID, string patNo, string cName, string sex, DateTime birthday, string ageALL, int? deptNo, string bed)
        {
            this.AdmID = admID;
            this.PatNo = patNo;
            this.CName = cName;
            this.Sex = sex;
            this.Birthday = birthday;
            this.AgeALL = ageALL;
            this.DeptNo = deptNo;
            this.Bed = bed;
        }
    }
}
