using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    //N_News_Type
    public class N_News_Type
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
        /// DictID
        /// </summary>		
        private long _Typeid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long TypeID
        {
            get { return _Typeid; }
            set { _Typeid = value; }
        }        
        /// <summary>
        /// DictTypeID
        /// </summary>		
        private long _dicttypeid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long DictTypeID
        {
            get { return _dicttypeid; }
            set { _dicttypeid = value; }
        }
        /// <summary>
        /// SName
        /// </summary>		
        private string _sname;
        public string SName
        {
            get { return _sname; }
            set { _sname = value; }
        }
        /// <summary>
        /// Shortcode
        /// </summary>		
        private string _shortcode;
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }
        /// <summary>
        /// PinYinZiTou
        /// </summary>		
        private string _pinyinzitou;
        public string PinYinZiTou
        {
            get { return _pinyinzitou; }
            set { _pinyinzitou = value; }
        }
        /// <summary>
        /// CName
        /// </summary>		
        private string _cname;
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// DispOrder
        /// </summary>		
        private int? _disporder;
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }
        /// <summary>
        /// IsUse
        /// </summary>		
        private bool? _isuse;
        public bool? IsUse
        {
            get { return _isuse; }
            set { _isuse = value; }
        }
        /// <summary>
        /// Memo
        /// </summary>		
        private string _memo;
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        /// <summary>
        /// DataAddTime
        /// </summary>		
        private DateTime? _dataaddtime;
        [JsonConverter(typeof(JsonConvertClass))]
        public DateTime? DataAddTime
        {
            get { return _dataaddtime; }
            set { _dataaddtime = value; }
        }
        /// <summary>
        /// DataTimeStamp
        /// </summary>		
        private DateTime _datatimestamp;
        public DateTime DataTimeStamp
        {
            get { return _datatimestamp; }
            set { _datatimestamp = value; }
        }
        /// <summary>
        /// StandCode
        /// </summary>		
        private string _standcode;
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }
        /// <summary>
        /// DeveCode
        /// </summary>		
        private string _devecode;
        public string DeveCode
        {
            get { return _devecode; }
            set { _devecode = value; }
        }

    }
}

