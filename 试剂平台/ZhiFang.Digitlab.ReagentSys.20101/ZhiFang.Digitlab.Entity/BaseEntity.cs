using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
    [DataContract]
    public class BaseEntity
    {
        protected long _id;
        protected long _labID=0;
        protected DateTime? _dataAddTime;
        protected byte[] _dataTimeStamp;

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

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "实验室ID", ShortCode = "SYSID", Desc = "实验室ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long LabID
        {
            get { return _labID; }
            set { _labID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据加入时间", ShortCode = "DataAddTime", Desc = "数据加入时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataAddTime
        {
            get 
            {
                //if (_dataAddTime == null || _dataAddTime <= DateTime.Parse("1900-01-01"))
                if (_dataAddTime == null)
                    _dataAddTime = DateTime.Now;
                return _dataAddTime; 
            }
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
