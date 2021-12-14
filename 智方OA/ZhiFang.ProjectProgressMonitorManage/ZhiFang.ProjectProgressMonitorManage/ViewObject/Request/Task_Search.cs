using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.ProjectProgressMonitorManage.ViewObject.Request
{
    public class Task_Search:ZhiFang.Entity.ProjectProgressMonitorManage.PTask 
    {
        private DateTime? _EstiStartTimeB;
        private DateTime? _EstiStartTimeE;
        private DateTime? _EstiEndTimeB;
        private DateTime? _EstiEndTimeE;
        private DateTime? _StartTimeB;
        private DateTime? _StartTimeE;
        private DateTime? _EndTimeB;
        private DateTime? _EndTimeE;


        #region 查询计划开始时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划开始时间_开始", ShortCode = "EstiStartTimeB", Desc = "查询计划开始时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiStartTimeB
        {
            get { return _EstiStartTimeB; }
            set { _EstiStartTimeB = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划开始时间_结束", ShortCode = "EstiStartTimeE", Desc = "查询计划开始时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiStartTimeE
        {
            get { return _EstiStartTimeE; }
            set { _EstiStartTimeE = value; }
        }
        #endregion

        #region 查询计划结束时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划结束时间_开始", ShortCode = "EstiEndTimeB", Desc = "查询计划结束时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiEndTimeB
        {
            get { return _EstiEndTimeB; }
            set { _EstiEndTimeB = value; }
        }
        


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询计划结束时间_结束", ShortCode = "EstiEndTimeE", Desc = "查询计划结束时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EstiEndTimeE
        {
            get { return _EstiEndTimeE; }
            set { _EstiEndTimeE = value; }
        }
        #endregion

        #region 查询实际开始时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际开始时间_开始", ShortCode = "StartTimeB", Desc = "查询实际开始时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartTimeB
        {
            get { return _StartTimeB; }
            set { _StartTimeB = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际开始时间_结束", ShortCode = "StartTimeE", Desc = "查询实际开始时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? StartTimeE
        {
            get { return _StartTimeE; }
            set { _StartTimeE = value; }
        }
        #endregion

        #region 查询实际结束时间范围
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际结束时间_开始", ShortCode = "EndTimeB", Desc = "查询实际结束时间_开始", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTimeB
        {
            get { return _EndTimeB; }
            set { _EndTimeB = value; }
        }



        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "查询实际结束时间_结束", ShortCode = "EndTimeE", Desc = "查询实际结束时间_结束", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? EndTimeE
        {
            get { return _EndTimeE; }
            set { _EndTimeE = value; }
        }
        #endregion
    }
}
