using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    //文档阅读记录表
    public class N_News_ReadingLog
    {

        /// <summary>
        /// 实验室ID
        /// </summary>		
        private long _labid;
        public long LabID
        {
            get { return _labid; }
            set { _labid = value; }
        }
        /// <summary>
        /// 文档阅读记录主键ID
        /// </summary>		
        private long _filereadinglogid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long FileReadingLogID
        {
            get { return _filereadinglogid; }
            set { _filereadinglogid = value; }
        }
        /// <summary>
        /// 文档主键ID
        /// </summary>		
        private long _fileid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long FileID
        {
            get { return _fileid; }
            set { _fileid = value; }
        }
        /// <summary>
        /// 阅读者ID
        /// </summary>		
        private long _readerid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long ReaderID
        {
            get { return _readerid; }
            set { _readerid = value; }
        }
        /// <summary>
        /// 阅读者姓名
        /// </summary>		
        private string _readername;
        public string ReaderName
        {
            get { return _readername; }
            set { _readername = value; }
        }
        /// <summary>
        /// 阅读时长，毫秒数
        /// </summary>		
        private int _readtimes;
        public int ReadTimes
        {
            get { return _readtimes; }
            set { _readtimes = value; }
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
        /// 创建者
        /// </summary>		
        private long _creatorid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long CreatorID
        {
            get { return _creatorid; }
            set { _creatorid = value; }
        }
        /// <summary>
        /// 创建者姓名
        /// </summary>		
        private string _creatorname;
        public string CreatorName
        {
            get { return _creatorname; }
            set { _creatorname = value; }
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
        private DateTime? _dataupdatetime;
        public DateTime? DataUpdateTime
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

