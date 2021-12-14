using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.WebAssist
{
    /// <summary>
    /// 按申请信息建立病区与科室关系
    /// </summary>
    [DataContract]
    public class WarpAndDeptVO
    {
        public WarpAndDeptVO() { }

        protected long _id;
        protected int _deptNo;
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
        /// 
        /// </summary>
        [DataMember]
        public virtual string WardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public virtual string HisWardNo { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DeptNo
        {
            get { return _deptNo; }
            set { _deptNo = value; }
        }

        public WarpAndDeptVO(int deptNo, string wardNo, string hisWardNo)
        {
            this.DeptNo = deptNo;
            this.WardNo = wardNo;
            this.HisWardNo = hisWardNo;
        }
    }
}
