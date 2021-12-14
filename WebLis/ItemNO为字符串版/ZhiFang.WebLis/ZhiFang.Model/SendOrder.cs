using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    /// <summary>
    /// 外送订单实体类
    /// </summary>
    public partial class SendOrder
    {
        public SendOrder() { }

        private string _orderno;
        private string _createman;
        private string _createdate;
        private int? _samplenum;
        private int? _status;
        private string _note;
        private string _labcode;
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateMan
        {
            set { _createman = value; }
            get { return _createman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }

        public string OrderStartDate { get; set; }
        public string OrderEndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleNum
        {
            set { _samplenum = value; }
            get { return _samplenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LabCode
        {
            set
            {
                _labcode = value;

            }
            get { return _labcode; }
        }

        public int IsConfirm
        {
            get;
            set;
        }
        public List<string> BarCodeList
        {
            get;
            set;
        }
        public List<string> BarCodeFormNoList { get; set; }

        /// <summary>
        /// 送检单位名称
        /// </summary>
        public string SendLabCodeName { get; set; }
    }
}
