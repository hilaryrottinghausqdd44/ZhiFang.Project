using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ZhiFang.Model
{
    /// <summary>
    /// TB_CheckClientAccount:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [DataContract]
    public partial class TB_CheckClientAccount
    {
        public TB_CheckClientAccount()
        { }
        #region Model
        private int _id;
        private int? _monthid;
        private string _monthname;
        private string _clientname;
        private string _status;
        private string _remark;
        private DateTime? _checkdate;
        private string _filepath;
        private DateTime? _createdate;
        private string _reply;
        private string _clientno;
        private string _auditstatus;
        private string _downloadfile = "下载";
        private string _count;
        private string _sumprice;
        private string _filepathitem;
        private string _downloadfileitem = "项目对帐";
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? monthid
        {
            set { _monthid = value; }
            get { return _monthid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string monthname
        {
            set { _monthname = value; }
            get { return _monthname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clientname
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? checkdate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string filepath
        {
            set { _filepath = value; }
            get { return _filepath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createdate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string reply
        {
            set { _reply = value; }
            get { return _reply; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string clientno
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string auditstatus
        {
            set { _auditstatus = value; }
            get { return _auditstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string downloadfile
        {
            set { _downloadfile = value; }
            get { return _downloadfile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sumprice
        {
            set { _sumprice = value; }
            get { return _sumprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string filepathitem
        {
            set { _filepathitem = value; }
            get { return _filepathitem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string downloadfileitem
        {
            set { _downloadfileitem = value; }
            get { return _downloadfileitem; }
        }
        #endregion Model

    }
}

