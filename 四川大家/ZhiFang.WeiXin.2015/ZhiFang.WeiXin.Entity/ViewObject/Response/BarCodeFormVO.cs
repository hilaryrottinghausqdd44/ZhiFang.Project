using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.WeiXin.Entity.ViewObject.Response
{

    [Serializable]
    public partial class BarCodeFormVO
    {
        public BarCodeFormVO()
        { }
        #region Model
        private long? _barcodeformno;
        private string _barcode;
        private int? _samplinggroupno;
        private int? _isprep;
        private int? _isspiltitem;
        private int? _isaffirm;
        private int? _receiveflag;
        private decimal? _samplecap;
        private string _clienthost;
        private string _collecter;
        private int? _collecterid;
        private DateTime? _collectdate;
        private DateTime? _collecttime;
        private string _refuseuser;
        private string _refuseopinion;
        private string _refusereason;
        private DateTime? _refusetime;
        private int? _signflag;
        private string _incepter;
        private DateTime? _incepttime;
        private DateTime? _inceptdate;
        private string _receiveman;
        private DateTime? _receivedate;
        private DateTime? _receivetime;
        private string _printinfo;
        private int? _printcount;
        private int? _dr2flag;
        private DateTime? _flagdatedelete;
        private int? _dispenseflag;
        private string _serialscantime;
        private int? _barcodesource;
        private int? _deleteflag;
        private int? _sendoffflag;
        private string _sendoffman;
        private string _emsman;
        private DateTime? _sendoffdate;
        private string _reportsignman;
        private DateTime? _reportsigndate;
        private string _refuseincepter;
        private string _refuseinceptermemo;
        private int? _reportflag;
        private string _sendoffmemo;
        private int? _sampletypeno;
        private string _sampletype;

        public string SampleType
        {
            get { return _sampletype; }
            set { _sampletype = value; }
        }
        private string _samplesendno;
        private int? _weblisflag;
        private DateTime? _weblisoptime;
        private string _webliser;
        private string _weblisdescript;
        private string _weblisorgid;
        private int? _weblisisreply;
        private string _weblisreplydate;
        private string _weblissourceorgid;
        private DateTime? _weblisuploadtime;
        private int? _weblisuploadstatus;
        private int? _weblisuploadteststatus;
        private string _weblisuploader;
        private string _weblisuploaddes;
        private string _weblissourceorgname;
        private string _clientno;
        private string _clientname;
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
        public string BarCode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SamplingGroupNo
        {
            set { _samplinggroupno = value; }
            get { return _samplinggroupno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsPrep
        {
            set { _isprep = value; }
            get { return _isprep; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isSpiltItem
        {
            set { _isspiltitem = value; }
            get { return _isspiltitem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? IsAffirm
        {
            set { _isaffirm = value; }
            get { return _isaffirm; }
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
        public decimal? SampleCap
        {
            set { _samplecap = value; }
            get { return _samplecap; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientHost
        {
            set { _clienthost = value; }
            get { return _clienthost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Collecter
        {
            set { _collecter = value; }
            get { return _collecter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CollecterID
        {
            set { _collecterid = value; }
            get { return _collecterid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CollectDate
        {
            set { _collectdate = value; }
            get { return _collectdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CollectTime
        {
            set { _collecttime = value; }
            get { return _collecttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refuseUser
        {
            set { _refuseuser = value; }
            get { return _refuseuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refuseopinion
        {
            set { _refuseopinion = value; }
            get { return _refuseopinion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refusereason
        {
            set { _refusereason = value; }
            get { return _refusereason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? refuseTime
        {
            set { _refusetime = value; }
            get { return _refusetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? signflag
        {
            set { _signflag = value; }
            get { return _signflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string incepter
        {
            set { _incepter = value; }
            get { return _incepter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? inceptTime
        {
            set { _incepttime = value; }
            get { return _incepttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? inceptDate
        {
            set { _inceptdate = value; }
            get { return _inceptdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveMan
        {
            set { _receiveman = value; }
            get { return _receiveman; }
        }
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
        public DateTime? ReceiveTime
        {
            set { _receivetime = value; }
            get { return _receivetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PrintInfo
        {
            set { _printinfo = value; }
            get { return _printinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PrintCount
        {
            set { _printcount = value; }
            get { return _printcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Dr2Flag
        {
            set { _dr2flag = value; }
            get { return _dr2flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? FlagDateDelete
        {
            set { _flagdatedelete = value; }
            get { return _flagdatedelete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DispenseFlag
        {
            set { _dispenseflag = value; }
            get { return _dispenseflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SerialScanTime
        {
            set { _serialscantime = value; }
            get { return _serialscantime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BarCodeSource
        {
            set { _barcodesource = value; }
            get { return _barcodesource; }
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
        public int? SendOffFlag
        {
            set { _sendoffflag = value; }
            get { return _sendoffflag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SendOffMan
        {
            set { _sendoffman = value; }
            get { return _sendoffman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EMSMan
        {
            set { _emsman = value; }
            get { return _emsman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SendOffDate
        {
            set { _sendoffdate = value; }
            get { return _sendoffdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReportSignMan
        {
            set { _reportsignman = value; }
            get { return _reportsignman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReportSignDate
        {
            set { _reportsigndate = value; }
            get { return _reportsigndate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RefuseIncepter
        {
            set { _refuseincepter = value; }
            get { return _refuseincepter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RefuseIncepterMemo
        {
            set { _refuseinceptermemo = value; }
            get { return _refuseinceptermemo; }
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
        public string SendOffMemo
        {
            set { _sendoffmemo = value; }
            get { return _sendoffmemo; }
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
        public string SampleSendNo
        {
            set { _samplesendno = value; }
            get { return _samplesendno; }
        }
        /// <summary>
        /// 外送流程标志
        /// </summary>
        public int? WebLisFlag
        {
            set { _weblisflag = value; }
            get { return _weblisflag; }
        }
        /// <summary>
        /// 外送流程标志更新时间
        /// </summary>
        public DateTime? WebLisOpTime
        {
            set { _weblisoptime = value; }
            get { return _weblisoptime; }
        }
        /// <summary>
        /// 外送流程标志更新人
        /// </summary>
        public string WebLiser
        {
            set { _webliser = value; }
            get { return _webliser; }
        }
        /// <summary>
        /// 流程操作说明
        /// </summary>
        public string WebLisDescript
        {
            set { _weblisdescript = value; }
            get { return _weblisdescript; }
        }
        /// <summary>
        /// 外送单位编号
        /// </summary>
        public string WebLisOrgID
        {
            set { _weblisorgid = value; }
            get { return _weblisorgid; }
        }
        /// <summary>
        /// 是否重试
        /// </summary>
        public int? WebLisIsReply
        {
            set { _weblisisreply = value; }
            get { return _weblisisreply; }
        }
        /// <summary>
        /// 下次重试执行时间
        /// </summary>
        public string WebLisReplyDate
        {
            set { _weblisreplydate = value; }
            get { return _weblisreplydate; }
        }
        /// <summary>
        /// 被录入送检单位编号
        /// </summary>
        public string WebLisSourceOrgId
        {
            set { _weblissourceorgid = value; }
            get { return _weblissourceorgid; }
        }
        /// <summary>
        /// 报告获取时间
        /// </summary>
        public DateTime? WebLisUploadTime
        {
            set { _weblisuploadtime = value; }
            get { return _weblisuploadtime; }
        }
        /// <summary>
        /// 报告获取
        /// </summary>
        public int? WebLisUploadStatus
        {
            set { _weblisuploadstatus = value; }
            get { return _weblisuploadstatus; }
        }
        /// <summary>
        /// 报告下载当时状态
        /// </summary>
        public int? WebLisUploadTestStatus
        {
            set { _weblisuploadteststatus = value; }
            get { return _weblisuploadteststatus; }
        }
        /// <summary>
        /// 报告下载人
        /// </summary>
        public string WebLisUploader
        {
            set { _weblisuploader = value; }
            get { return _weblisuploader; }
        }
        /// <summary>
        /// 报告下载描述
        /// </summary>
        public string WebLisUploadDes
        {
            set { _weblisuploaddes = value; }
            get { return _weblisuploaddes; }
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
        /// 录入医辽机构编号
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
        public string CollectDateStart { get; set; }

        public string CollectDateEnd { get; set; }

        private string weblisorgname;

        public string WebLisOrgName
        {
            get { return weblisorgname; }
            set { weblisorgname = value; }
        }


        public string Color { get; set; }
        public string ItemName { get; set; }
        public string ItemNo { get; set; }
        //样本类型名称
        public string SampleTypeName { get; set; }


        //外送订单编号
        public string OrderNo { get; set; }
        public string LabItemName { get; set; }
        public string LabItemNo { get; set; }

        public List<string> ClientNoList { get; set; }
        #endregion Model

    }

    [Serializable]
    [DataContract]
    public class BarCodeFormResult
    {
        [DataMember]
        public string BarCode { get; set; }
        [DataMember]
        public long BarCodeFormNo { get; set; }
        [DataMember]
        public string Color { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string ItemNo { get; set; }
        [DataMember]
        public string SampleTypeName { get; set; }
        [DataMember]
        public string SampleTypeNo { get; set; }
        [DataMember]
        public string WebLisFlag { get; set; }
    }

}
