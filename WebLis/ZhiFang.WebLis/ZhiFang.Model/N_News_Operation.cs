using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    //文档操作记录表
    public class N_News_Operation
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
        /// 文件操作记录主键ID
        /// </summary>		
        private long _fileoperationid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long FileOperationID
        {
            get { return _fileoperationid; }
            set { _fileoperationid = value; }
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
        /// 操作类型
        /// </summary>		
        private long _type;
        [JsonConverter(typeof(JsonConvertClass))]
        public long Type
        {
            get { return _type; }
            set { _type = value; }
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

