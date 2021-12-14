using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZhiFang.Entity.Base
{
    [DataContract]
    public class BaseEntityService
    {
        protected long _id;
        protected long _labID = 0;
        protected DateTime? _dataAddTime;
        protected byte[] _dataTimeStamp;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id
        {
            get
            {
                //发现血库的部分字典表的主键值是从0开始的,所以将原来的id <= 0判断调整为id < 0
                if (_id < 0 && _id != -99)
                    _id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                return _id;
            }
            set { _id = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "平台客户ID", ShortCode = "SYSID", Desc = "平台客户ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long LabID
        {
            get
            {
                return _labID;
            }
            set { _labID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据加入时间", ShortCode = "DataAddTime", Desc = "数据加入时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataAddTime
        {
            get { return _dataAddTime; }
            set { _dataAddTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "时间戳", ShortCode = "SJC", Desc = "时间戳", ContextType = SysDic.All, Length = 8)]
        public virtual byte[] DataTimeStamp
        {
            get { return _dataTimeStamp; }
            set { _dataTimeStamp = value; }
        }
    }
}
