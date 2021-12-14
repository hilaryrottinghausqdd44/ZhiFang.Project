using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    //N_NewsAreaClientLink
    public class N_NewsAreaClientLink
    {

        /// <summary>
        /// LabId
        /// </summary>		
        private long _labid;
        public long LabId
        {
            get { return _labid; }
            set { _labid = value; }
        }
        /// <summary>
        /// NewsAreaClientLinkId
        /// </summary>		
        private long _newsareaclientlinkid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long NewsAreaClientLinkId
        {
            get { return _newsareaclientlinkid; }
            set { _newsareaclientlinkid = value; }
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
        /// ClientNo
        /// </summary>		
        private long _clientno;
        [JsonConverter(typeof(JsonConvertClass))]
        public long ClientNo
        {
            get { return _clientno; }
            set { _clientno = value; }
        }
        /// <summary>
        /// ClientName
        /// </summary>		
        private string _clientname;
        public string ClientName
        {
            get { return _clientname; }
            set { _clientname = value; }
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

