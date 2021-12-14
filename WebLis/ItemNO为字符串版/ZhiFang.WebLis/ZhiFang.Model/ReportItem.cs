using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    /// <summary>
    /// 实体类ReportItem 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ReportItem
    {
        public ReportItem()
        { }
        #region Model
        private DateTime? _receivedate;
        private int? _sectionno;
        private int? _testtypeno;
        private string _sampleno;
        private int? _paritemno;
        private int? _itemno;
        private decimal? _originalvalue;
        private decimal? _reportvalue;
        private string _originaldesc;
        private string _reportdesc;
        private int? _statusno;
        private string _refrange;
        private int? _equipno;
        private int? _modified;
        private DateTime? _itemdate;
        private DateTime? _itemtime;
        private int? _ismatch;
        private string _resultstatus;
        private string _hisvalue;
        private string _hiscomp;
        private int? _isreceive;
        private string _countnodesitemsource;
        private string _unit;
        private string _formno;
        private int? _plateno;
        private int? _positionno;
        private int? _tollitemno;
        private string _itemdesc;
        private string _oldserialno;
        private string _equipcommmemo;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReceiveDate
        {
            set { _receivedate = value; }
            get { return _receivedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SectionNo
        {
            set { _sectionno = value; }
            get { return _sectionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TestTypeNo
        {
            set { _testtypeno = value; }
            get { return _testtypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SampleNo
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParItemNo
        {
            set { _paritemno = value; }
            get { return _paritemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ItemNo
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OriginalValue
        {
            set { _originalvalue = value; }
            get { return _originalvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ReportValue
        {
            set { _reportvalue = value; }
            get { return _reportvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OriginalDesc
        {
            set { _originaldesc = value; }
            get { return _originaldesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReportDesc
        {
            set { _reportdesc = value; }
            get { return _reportdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusNo
        {
            set { _statusno = value; }
            get { return _statusno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RefRange
        {
            set { _refrange = value; }
            get { return _refrange; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EquipNo
        {
            set { _equipno = value; }
            get { return _equipno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Modified
        {
            set { _modified = value; }
            get { return _modified; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ItemDate
        {
            set { _itemdate = value; }
            get { return _itemdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ItemTime
        {
            set { _itemtime = value; }
            get { return _itemtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsMatch
        {
            set { _ismatch = value; }
            get { return _ismatch; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ResultStatus
        {
            set { _resultstatus = value; }
            get { return _resultstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HisValue
        {
            set { _hisvalue = value; }
            get { return _hisvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HisComp
        {
            set { _hiscomp = value; }
            get { return _hiscomp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isReceive
        {
            set { _isreceive = value; }
            get { return _isreceive; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CountNodesItemSource
        {
            set { _countnodesitemsource = value; }
            get { return _countnodesitemsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FormNo
        {
            set { _formno = value; }
            get { return _formno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PlateNo
        {
            set { _plateno = value; }
            get { return _plateno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PositionNo
        {
            set { _positionno = value; }
            get { return _positionno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TollItemNo
        {
            set { _tollitemno = value; }
            get { return _tollitemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemdesc
        {
            set { _itemdesc = value; }
            get { return _itemdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldSerialNo
        {
            set { _oldserialno = value; }
            get { return _oldserialno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EquipCommMemo
        {
            set { _equipcommmemo = value; }
            get { return _equipcommmemo; }
        }
        #endregion Model

    }
}
