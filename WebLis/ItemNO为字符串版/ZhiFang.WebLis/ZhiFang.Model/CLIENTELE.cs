using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace ZhiFang.Model
{
	/// <summary>
	/// 实体类CLIENTELE 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[DataContract]
	public class CLIENTELE
	{
		public CLIENTELE()
		{}
		#region Model
		private string _clientno;
		private string _cname;
		private string _ename;
		private string _shortcode;
		private int? _isuse;
		private string _linkman;
		private string _phonenum1;
		private string _address;
		private string _mailno;
		private string _email;
		private string _principal;
		private string _phonenum2;
		private int? _clienttype;
		private int? _bmanno;
		private string _romark;
		private int? _titletype;
		private int? _uploadtype;
		private int? _printtype;
		private int? _inputdatatype;
		private int? _reportpagetype;
		private string _clientarea;
		private string _clientstyle;
		private string _faxno;
		private int? _autofax;
		private string _clientreporttitle;
		private int? _isprintitem;
		private string _czdy1;
		private string _czdy2;
		private string _czdy3;
		private string _czdy4;
		private string _czdy5;
		private string _czdy6;

        private int _clientid;
        private string _linkmanposition;
        private string _linkman1;
        private string _linkmanposition1;
        private string _clientcode;
        private string _cwaccountdate;
        private string _nfclienttype;
        private string _weblissourceorgid;
        private DateTime? _dtimestampe;
        private DateTime? _addtime;
        private string _standcode;
        private string _zfstandcode;
        private int? _useflag;
        private int? _areaid;
        /// <summary>
        /// CLIENTID
        /// </summary>
		[DataMember]
        public int CLIENTID
        {
            get { return _clientid; }
            set { _clientid = value; }
        }

        /// <summary>
        /// ClIENTNO
        /// </summary>
		[DataMember]
        public string ClIENTNO
        {
            get { return _clientno; }
            set { _clientno = value; }
        }

        /// <summary>
        /// CNAME
        /// </summary>
		[DataMember]
        public string CNAME
        {
            get { return _cname; }
            set { _cname = value; }
        }

        /// <summary>
        /// ENAME
        /// </summary>
		[DataMember]
        public string ENAME
        {
            get { return _ename; }
            set { _ename = value; }
        }

        /// <summary>
        /// SHORTCODE
        /// </summary>
		[DataMember]
        public string SHORTCODE
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        /// <summary>
        /// ISUSE
        /// </summary>
		[DataMember]
        public int? ISUSE
        {
            get { return _isuse; }
            set { _isuse = value; }
        }

        /// <summary>
        /// LINKMAN
        /// </summary>
		[DataMember]
        public string LINKMAN
        {
            get { return _linkman; }
            set { _linkman = value; }
        }

        /// <summary>
        /// PHONENUM1
        /// </summary>
		[DataMember]
        public string PHONENUM1
        {
            get { return _phonenum1; }
            set { _phonenum1 = value; }
        }

        /// <summary>
        /// ADDRESS
        /// </summary>
		[DataMember]
        public string ADDRESS
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// MAILNO
        /// </summary>
		[DataMember]
        public string MAILNO
        {
            get { return _mailno; }
            set { _mailno = value; }
        }

        /// <summary>
        /// EMAIL
        /// </summary>
		[DataMember]
        public string EMAIL
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// PRINCIPAL
        /// </summary>
		[DataMember]
        public string PRINCIPAL
        {
            get { return _principal; }
            set { _principal = value; }
        }

        /// <summary>
        /// PHONENUM2
        /// </summary>
		[DataMember]
        public string PHONENUM2
        {
            get { return _phonenum2; }
            set { _phonenum2 = value; }
        }

        /// <summary>
        /// CLIENTTYPE
        /// </summary>
		[DataMember]
        public int? CLIENTTYPE
        {
            get { return _clienttype; }
            set { _clienttype = value; }
        }

        /// <summary>
        /// bmanno
        /// </summary>
		[DataMember]
        public int? bmanno
        {
            get { return _bmanno; }
            set { _bmanno = value; }
        }

        /// <summary>
        /// romark
        /// </summary>
		[DataMember]
        public string romark
        {
            get { return _romark; }
            set { _romark = value; }
        }

        /// <summary>
        /// titletype
        /// </summary>
		[DataMember]
        public int? titletype
        {
            get { return _titletype; }
            set { _titletype = value; }
        }

        /// <summary>
        /// uploadtype
        /// </summary>
		[DataMember]
        public int? uploadtype
        {
            get { return _uploadtype; }
            set { _uploadtype = value; }
        }

        /// <summary>
        /// printtype
        /// </summary>
		[DataMember]
        public int? printtype
        {
            get { return _printtype; }
            set { _printtype = value; }
        }

        /// <summary>
        /// InputDataType
        /// </summary>
		[DataMember]
        public int? InputDataType
        {
            get { return _inputdatatype; }
            set { _inputdatatype = value; }
        }

        /// <summary>
        /// reportpagetype
        /// </summary>
		[DataMember]
        public int? reportpagetype
        {
            get { return _reportpagetype; }
            set { _reportpagetype = value; }
        }

        /// <summary>
        /// clientarea
        /// </summary>
		[DataMember]
        public string clientarea
        {
            get { return _clientarea; }
            set { _clientarea = value; }
        }

        /// <summary>
        /// clientstyle
        /// </summary>
		[DataMember]
        public string clientstyle
        {
            get { return _clientstyle; }
            set { _clientstyle = value; }
        }

        /// <summary>
        /// FaxNo
        /// </summary>
		[DataMember]
        public string FaxNo
        {
            get { return _faxno; }
            set { _faxno = value; }
        }

        /// <summary>
        /// AutoFax
        /// </summary>
		[DataMember]
        public int? AutoFax
        {
            get { return _autofax; }
            set { _autofax = value; }
        }

        /// <summary>
        /// ClientReportTitle
        /// </summary>
		[DataMember]
        public string ClientReportTitle
        {
            get { return _clientreporttitle; }
            set { _clientreporttitle = value; }
        }

        /// <summary>
        /// IsPrintItem
        /// </summary>
		[DataMember]
        public int? IsPrintItem
        {
            get { return _isprintitem; }
            set { _isprintitem = value; }
        }

        /// <summary>
        /// CZDY1
        /// </summary>
		[DataMember]
        public string CZDY1
        {
            get { return _czdy1; }
            set { _czdy1 = value; }
        }

        /// <summary>
        /// CZDY2
        /// </summary>
		[DataMember]
        public string CZDY2
        {
            get { return _czdy2; }
            set { _czdy2 = value; }
        }

        /// <summary>
        /// CZDY3
        /// </summary>
		[DataMember]
        public string CZDY3
        {
            get { return _czdy3; }
            set { _czdy3 = value; }
        }

        /// <summary>
        /// CZDY4
        /// </summary>
		[DataMember]
        public string CZDY4
        {
            get { return _czdy4; }
            set { _czdy4 = value; }
        }

        /// <summary>
        /// CZDY5
        /// </summary>
		[DataMember]
        public string CZDY5
        {
            get { return _czdy5; }
            set { _czdy5 = value; }
        }

        /// <summary>
        /// CZDY6
        /// </summary>
		[DataMember]
        public string CZDY6
        {
            get { return _czdy6; }
            set { _czdy6 = value; }
        }

        /// <summary>
        /// LinkManPosition
        /// </summary>
		[DataMember]
        public string LinkManPosition
        {
            get { return _linkmanposition; }
            set { _linkmanposition = value; }
        }

        /// <summary>
        /// LinkMan1
        /// </summary>
		[DataMember]
        public string LinkMan1
        {
            get { return _linkman1; }
            set { _linkman1 = value; }
        }

        /// <summary>
        /// LinkManPosition1
        /// </summary>
		[DataMember]
        public string LinkManPosition1
        {
            get { return _linkmanposition1; }
            set { _linkmanposition1 = value; }
        }

        /// <summary>
        /// clientcode
        /// </summary>
		[DataMember]
        public string clientcode
        {
            get { return _clientcode; }
            set { _clientcode = value; }
        }

        /// <summary>
        /// CWAccountDate
        /// </summary>
		[DataMember]
        public string CWAccountDate
        {
            get { return _cwaccountdate; }
            set { _cwaccountdate = value; }
        }

        /// <summary>
        /// NFClientType
        /// </summary>
		[DataMember]
        public string NFClientType
        {
            get { return _nfclienttype; }
            set { _nfclienttype = value; }
        }

        /// <summary>
        /// WebLisSourceOrgId
        /// </summary>
		[DataMember]
        public string WebLisSourceOrgId
        {
            get { return _weblissourceorgid; }
            set { _weblissourceorgid = value; }
        }

        /// <summary>
        /// DTimeStampe
        /// </summary>
		[DataMember]
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }

        /// <summary>
        /// AddTime
        /// </summary>
		[DataMember]
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }

        /// <summary>
        /// StandCode
        /// </summary>
		[DataMember]
        public string StandCode
        {
            get { return _standcode; }
            set { _standcode = value; }
        }

        /// <summary>
        /// ZFStandCode
        /// </summary>
		[DataMember]
        public string ZFStandCode
        {
            get { return _zfstandcode; }
            set { _zfstandcode = value; }
        }

        /// <summary>
        /// UseFlag
        /// </summary>
		[DataMember]
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }

        /// <summary>
        /// 中心--实验室  对照关系使用
        /// </summary>
        private string _labcode;
		[DataMember]
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }
        /// <summary>
        /// RelationName
        /// </summary>
        private string _relationname;
		[DataMember]
        public string RelationName
        {
            get { return _relationname; }
            set { _relationname = value; }
        }
        /// <summary>
        /// 办事处
        /// </summary>
        private string _groupname;
		[DataMember]
        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }  

		[DataMember]
        public string ClienteleLikeKey { get; set; }

        public SortedList<string, string[]> OrganizationsList { set; get; }

        private string _orderfield = "ClIENTNO";//排序
		[DataMember]
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }

		private string _searchlikekey;//模糊查询字段
		[DataMember]
		public string SearchLikeKey
		{
			get { return _searchlikekey; }
			set { _searchlikekey = value; }
		}

        private int? _outsideclientno;
		[DataMember]
        public int? OutSideClientNo
        {
            get { return _outsideclientno; }
            set { _outsideclientno = value; }
        }

        [DataMember]
        public int? AreaID
        {
            get { return _areaid; }
            set { _areaid = value; }
        }

		#endregion Model

	}
}

