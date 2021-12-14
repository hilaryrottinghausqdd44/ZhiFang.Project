using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace ZhiFang.Entity.Base
{
    [DataContract]
    public class BaseEntity
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
                if (_id <= 0)
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
                try
                {
                    if (_labID <= 0)
                    {
                        string IsLabFlag = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag);
                        if (!string.IsNullOrWhiteSpace(IsLabFlag) && IsLabFlag.Trim() == "1")
                        {
                            string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
                            if (labid != null && labid.Trim() != "")
                            {
                                return long.Parse(labid);
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Error("未找到所属实验室标识！");
                                throw new Exception("未找到所属实验室标识！");
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Error("未找到所属实验室标记");
                            throw new Exception("未找到所属实验室标记");
                        }
                    }
                    else
                    {
                        return _labID;
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("获取实验室ID信息错误：" + ex.Message);
                    throw ex;
                }
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
