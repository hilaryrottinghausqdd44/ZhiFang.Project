using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{

    [Serializable]
    public partial class NRequestItemVO
    {
        public NRequestItemVO()
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
        [DataMember]
        public string Weblisflag
        {
            get { return weblisflag; }
            set { weblisflag = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? NRequestItemNo
        {
            set { _nrequestitemno = value; }
            get { return _nrequestitemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? ItemSource
        {
            set { _itemsource = value; }
            get { return _itemsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long? NRequestFormNo
        {
            set { _nrequestformno = value; }
            get { return _nrequestformno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long? BarCodeFormNo
        {
            set { _barcodeformno = value; }
            get { return _barcodeformno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? FormNo
        {
            set { _formno = value; }
            get { return _formno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? TollItemNo
        {
            set { _tollitemno = value; }
            get { return _tollitemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ParItemNo
        {
            set { _paritemno = value; }
            get { return _paritemno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? IsCheckFee
        {
            set { _ischeckfee = value; }
            get { return _ischeckfee; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? ReceiveFlag
        {
            set { _receiveflag = value; }
            get { return _receiveflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal? HISCharge
        {
            set { _hischarge = value; }
            get { return _hischarge; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal? ItemCharge
        {
            set { _itemcharge = value; }
            get { return _itemcharge; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? SampleTypeNo
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string zdy1
        {
            set { _zdy1 = value; }
            get { return _zdy1; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string zdy2
        {
            set { _zdy2 = value; }
            get { return _zdy2; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string SerialNo
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string zdy3
        {
            set { _zdy3 = value; }
            get { return _zdy3; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string zdy4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string zdy5
        {
            set { _zdy5 = value; }
            get { return _zdy5; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? DeleteFlag
        {
            set { _deleteflag = value; }
            get { return _deleteflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OldSerialNo
        {
            set { _oldserialno = value; }
            get { return _oldserialno; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CountNodesItemSource
        {
            set { _countnodesitemsource = value; }
            get { return _countnodesitemsource; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? ReportFlag
        {
            set { _reportflag = value; }
            get { return _reportflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? PartFlag
        {
            set { _partflag = value; }
            get { return _partflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string WebLisOrgID
        {
            set { _weblisorgid = value; }
            get { return _weblisorgid; }
        }
        /// <summary>
        /// 被录入送检单位编号
        /// </summary>
        [DataMember]
        public string WebLisSourceOrgID
        {
            set { _weblissourceorgid = value; }
            get { return _weblissourceorgid; }
        }
        /// <summary>
        /// 被录入送检单位名称
        /// </summary>
        [DataMember]
        public string WebLisSourceOrgName
        {
            set { _weblissourceorgname = value; }
            get { return _weblissourceorgname; }
        }
        /// <summary>
        /// 录入医疗机构编号
        /// </summary>
        [DataMember]
        public string ClientNo
        {
            set { _clientno = value; }
            get { return _clientno; }
        }
        /// <summary>
        /// 录入医疗机构名称
        /// </summary>
        [DataMember]
        public string ClientName
        {
            set { _clientname = value; }
            get { return _clientname; }
        }
        [DataMember]
        public string CombiItemNo
        {
            set { _combiitemno = value; }
            get { return _combiitemno; }
        }

        private string sampletype;

        [DataMember]
        public string SampleType
        {
            get { return sampletype; }
            set { sampletype = value; }
        }
        private string checktype;

        [DataMember]
        public string CheckType
        {
            get { return checktype; }
            set { checktype = value; }
        }
        private string checktypename;

        [DataMember]
        public string CheckTypeName
        {
            get { return checktypename; }
            set { checktypename = value; }
        }

        [DataMember] 
        public int? LabCombiItemNo { get; set; }

        [DataMember] 
        public string LabParItemNo { get; set; }

        [DataMember] 
        public System.Collections.Generic.List<NRequestItemVO> CombiItemDetailList { get; set; }

        [DataMember] 
        public decimal Price { get; set; }
        [DataMember] 
        public object LABNREQUESTFORMNO { get; set; }
        [DataMember] 
        public object ComboId { get; set; }
        [DataMember] 
        public object ComboName { get; set; }
        [DataMember] 
        public object ItemPrice { get; set; }
        [DataMember]
        public object ItemAgio { get; set; }
        [DataMember]
        public object ItemAgioPrice { get; set; }
        [DataMember]
        public object TransitFlag { get; set; }
        [DataMember]
        public object ModifyFlag { get; set; }
        [DataMember]
        public object IsFree { get; set; }
        [DataMember]
        public object IsFreeStatus { get; set; }
        [DataMember]
        public object IsLocked { get; set; }
        [DataMember]
        public object IsLockedDate { get; set; }
        [DataMember]
        public object IsReportSend { get; set; }
        [DataMember]
        public object SenderTime1 { get; set; }
        [DataMember]
        public object SenderTime1er { get; set; }
        [DataMember]
        public object ParItemCode { get; set; }
        [DataMember]
        public object ParItemName { get; set; }
        [DataMember]
        public object WebLisFlag { get; set; }
        [DataMember]
        public object Collecter { get; set; }
        [DataMember]
        public object CollectTime { get; set; }
        [DataMember]
        public object FlagDateDelete { get; set; }
        [DataMember]
        public object SenderTime2er { get; set; }
        [DataMember]
        public object CheckDater { get; set; }
        [DataMember]
        public object SenderTime2 { get; set; }
        [DataMember]
        public object CheckDate { get; set; }
        [DataMember]
        public object ReportPrintDate { get; set; }
        [DataMember]
        public object IsLocker { get; set; }
        [DataMember]
        public object ItemType { get; set; }
        [DataMember]
        public object TestDate { get; set; }
        [DataMember]
        public object TestDater { get; set; }
        [DataMember]
        public object CollectDate { get; set; }
        [DataMember]
        public object ReportSendDate { get; set; }
        #endregion Model

    }

}
