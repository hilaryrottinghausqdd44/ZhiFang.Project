using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using ZhiFang.Tools;

namespace ZhiFang.Model
{
    //文档交流表（不附带附件）
    public class N_News_Interaction
    {

        /// <summary>
        /// 平台客户ID
        /// </summary>		
        private long _labid;
        public long LabID
        {
            get { return _labid; }
            set { _labid = value; }
        }
        /// <summary>
        /// 互动主键
        /// </summary>		
        private long _interactionid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long InteractionID
        {
            get { return _interactionid; }
            set { _interactionid = value; }
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
        /// 内容
        /// </summary>		
        private string _contents;
        public string Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }
        /// <summary>
        /// 发件人
        /// </summary>		
        private long _senderid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long SenderID
        {
            get { return _senderid; }
            set { _senderid = value; }
        }
        /// <summary>
        /// 发件人姓名
        /// </summary>		
        private string _sendername;
        public string SenderName
        {
            get { return _sendername; }
            set { _sendername = value; }
        }
        /// <summary>
        /// 接收人
        /// </summary>		
        private long _receiverid;
        [JsonConverter(typeof(JsonConvertClass))]
        public long ReceiverID
        {
            get { return _receiverid; }
            set { _receiverid = value; }
        }
        /// <summary>
        /// 收件人姓名
        /// </summary>		
        private string _receivername;
        public string ReceiverName
        {
            get { return _receivername; }
            set { _receivername = value; }
        }
        /// <summary>
        /// 是否带附件
        /// </summary>		
        private bool _hasattachment;
        public bool HasAttachment
        {
            get { return _hasattachment; }
            set { _hasattachment = value; }
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
        /// 是否使用
        /// </summary>		
        private bool _isuse;
        public bool IsUse
        {
            get { return _isuse; }
            set { _isuse = value; }
        }
        /// <summary>
        /// 新增时间
        /// </summary>		
        private DateTime _dataaddtime;
        public DateTime DataAddTime
        {
            get { return _dataaddtime; }
            set { _dataaddtime = value; }
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

