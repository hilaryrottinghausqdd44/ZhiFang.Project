using System;
namespace ZhiFang.Model
{
    /// <summary>
    /// NRequestItem:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class NRequestItem
    {
        public NRequestItem()
        { }
        #region Model
        private int? _nrequestitemno;
        private int? _itemsource;
        private long? _nrequestformno;
        private long? _barcodeformno;
        private int? _formno;
        private int? _tollitemno;
        private string _paritemno;
        private int? _ischeckfee;
        private int? _receiveflag;
        private decimal? _hischarge;
        private decimal? _itemcharge;
        private int? _sampletypeno;
        private string _zdy1;
        private string _zdy2;
        private string _serialno;
        private string _zdy3;
        private string _zdy4;
        private string _zdy5;
        private int? _deleteflag;
        private string _oldserialno;
        private string _countnodesitemsource;
        private int? _reportflag;
        private int? _partflag = 0;
        private string _weblisorgid;
        private string _weblissourceorgid;
        private string _weblissourceorgname;
        private string _clientno;
        private string _clientname;
        private string _combiitemno;
        private string weblisflag;

        public string Weblisflag
        {
            get { return weblisflag; }
            set { weblisflag = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NRequestItemNo
        {
            set { _nrequestitemno = value; }
            get { return _nrequestitemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ItemSource
        {
            set { _itemsource = value; }
            get { return _itemsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? NRequestFormNo
        {
            set { _nrequestformno = value; }
            get { return _nrequestformno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? BarCodeFormNo
        {
            set { _barcodeformno = value; }
            get { return _barcodeformno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FormNo
        {
            set { _formno = value; }
            get { return _formno; }
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
        public string ParItemNo
        {
            set { _paritemno = value; }
            get { return _paritemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsCheckFee
        {
            set { _ischeckfee = value; }
            get { return _ischeckfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReceiveFlag
        {
            set { _receiveflag = value; }
            get { return _receiveflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HISCharge
        {
            set { _hischarge = value; }
            get { return _hischarge; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ItemCharge
        {
            set { _itemcharge = value; }
            get { return _itemcharge; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SampleTypeNo
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy1
        {
            set { _zdy1 = value; }
            get { return _zdy1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy2
        {
            set { _zdy2 = value; }
            get { return _zdy2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SerialNo
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy3
        {
            set { _zdy3 = value; }
            get { return _zdy3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zdy5
        {
            set { _zdy5 = value; }
            get { return _zdy5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DeleteFlag
        {
            set { _deleteflag = value; }
            get { return _deleteflag; }
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
        public string CountNodesItemSource
        {
            set { _countnodesitemsource = value; }
            get { return _countnodesitemsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReportFlag
        {
            set { _reportflag = value; }
            get { return _reportflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PartFlag
        {
            set { _partflag = value; }
            get { return _partflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WebLisOrgID
        {
            set { _weblisorgid = value; }
            get { return _weblisorgid; }
        }
        /// <summary>
        /// 被录入送检单位编号
        /// </summary>
        public string WebLisSourceOrgID
        {
            set { _weblissourceorgid = value; }
            get { return _weblissourceorgid; }
        }
        /// <summary>
        /// 被录入送检单位名称
        /// </summary>
        public string WebLisSourceOrgName
        {
            set { _weblissourceorgname = value; }
            get { return _weblissourceorgname; }
        }
        /// <summary>
        /// 录入医疗机构编号
        /// </summary>
        public string ClientNo
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 录入医疗机构名称
        /// </summary>
        public string ClientName
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        public string CombiItemNo
        {
            set { _combiitemno = value; }
            get { return _combiitemno; }
        }
       
        private string sampletype;

        public string SampleType
        {
            get { return sampletype; }
            set { sampletype = value; }
        }
        private string checktype;

        public string CheckType
        {
            get { return checktype; }
            set { checktype = value; }
        }
        private string checktypename;

        public string CheckTypeName
        {
            get { return checktypename; }
            set { checktypename = value; }
        }

        public int? LabCombiItemNo{ get; set; }
      
        public string LabParItemNo { get; set; }
       
        public System.Collections.Generic.List<NRequestItem> CombiItemDetailList { get; set; }

        public decimal Price { get; set; }
        #endregion Model

    }
}

