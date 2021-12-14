using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    //N_NewsAreaLink
    public class N_NewsAreaLink
    {

        /// <summary>
        /// LabID
        /// </summary>		
        private long _labid;
        public long LabID
        {
            get { return _labid; }
            set { _labid = value; }
        }
        /// <summary>
        /// NewsAreaLinkId
        /// </summary>		
        private long _newsarealinkid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long NewsAreaLinkId
        {
            get { return _newsarealinkid; }
            set { _newsarealinkid = value; }
        }
        /// <summary>
        /// NewsAreaId
        /// </summary>		
        private long _newsareaid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long NewsAreaId
        {
            get { return _newsareaid; }
            set { _newsareaid = value; }
        }
        /// <summary>
        /// NewsAreaName
        /// </summary>		
        private string _newsareaname;
        public string NewsAreaName
        {
            get { return _newsareaname; }
            set { _newsareaname = value; }
        }
        /// <summary>
        /// NewsId
        /// </summary>		
        private long _newsid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long NewsId
        {
            get { return _newsid; }
            set { _newsid = value; }
        }
        /// <summary>
        /// NewsIdName
        /// </summary>		
        private string _newsidname;
        public string NewsIdName
        {
            get { return _newsidname; }
            set { _newsidname = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        /// <summary>
        /// DispOrder
        /// </summary>		
        private int _disporder;
        public int DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }
        /// <summary>
        /// 是否使用
        /// </summary>		
        private bool _isuse;
        public bool IsUse
        {
            get { return _isuse; }
            set { _isuse = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        private DateTime _dataaddtime;
        public DateTime DataAddTime
        {
            get { return _dataaddtime; }
            set { _dataaddtime = value; }
        }
        /// <summary>
        /// 数据修改时间
        /// </summary>		
        private DateTime _dataupdatetime;
        public DateTime DataUpdateTime
        {
            get { return _dataupdatetime; }
            set { _dataupdatetime = value; }
        }
        /// <summary>
        /// 时间戳
        /// </summary>		
        private DateTime _datatimestamp;
        public DateTime DataTimeStamp
        {
            get { return _datatimestamp; }
            set { _datatimestamp = value; }
        }

    }
}

