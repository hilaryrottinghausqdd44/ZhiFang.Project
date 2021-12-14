using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZhiFang.Model
{
    //B_Lab_TestItem
    [DataContract]
    public class Lab_TestItem
    {
        public Lab_TestItem() { }
        private string _pitemnos;//组套项目用，当前项目的所有父级
        [DataMember]
        public string PItemNos
        {
            get { return _pitemnos; }
            set { _pitemnos = value; }
        }
        private string _searchkey;//组套项目用，查询值(项目编号/项目名称/项目简称)
        [DataMember]
        public string SearchKey
        {
            get { return _searchkey; }
            set { _searchkey = value; }
        }
        private string _orderfield = "ItemID";//排序
        [DataMember]
        public string OrderField
        {
            get { return _orderfield; }
            set { _orderfield = value; }
        }
        /// <summary>
        /// ItemID
        /// </summary>		
        private int _itemid;
        [DataMember]
        public int ItemID
        {
            get { return _itemid; }
            set { _itemid = value; }
        }

        //是否对照
        private string _controlstatus;
        [DataMember]
        public string ControlStatus
        {
            get { return _controlstatus; }
            set { _controlstatus = value; }
        }


        /// <summary>
        /// LabCode
        /// </summary>		
        private string _labcode;
        [DataMember]
        public string LabCode
        {
            get { return _labcode; }
            set { _labcode = value; }
        }
        /// <summary>
        /// LabItemNo digitlab2009和digitlab8使用LabItemNo；digitlab66和weblis使用ItemNo
        /// </summary>		
        private string _labitemno;
        [DataMember]
        public string LabItemNo
        {
            get { return _labitemno; }
            set { _labitemno = value; }
        }
        /// <summary>
        /// CName
        /// </summary>		
        private string _cname;
        [DataMember]
        public string CName
        {
            get { return _cname; }
            set { _cname = value; }
        }
        /// <summary>
        /// EName
        /// </summary>		
        private string _ename;
        [DataMember]
        public string EName
        {
            get { return _ename; }
            set { _ename = value; }
        }
        /// <summary>
        /// ShortName
        /// </summary>		
        private string _shortname;
        [DataMember]
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; }
        }
        /// <summary>
        /// ShortCode
        /// </summary>		
        private string _shortcode;
        [DataMember]
        public string ShortCode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }
        /// <summary>
        /// DiagMethod
        /// </summary>		
        private string _diagmethod;
        [DataMember]
        public string DiagMethod
        {
            get { return _diagmethod; }
            set { _diagmethod = value; }
        }
        /// <summary>
        /// Unit
        /// </summary>		
        private string _unit;
        [DataMember]
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        /// <summary>
        /// IsCalc
        /// </summary>		
        private int? _iscalc;
        [DataMember]
        public int? IsCalc
        {
            get { return _iscalc; }
            set { _iscalc = value; }
        }
        /// <summary>
        /// Visible
        /// </summary>		
        private int? _visible;
        [DataMember]
        public int? Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        /// <summary>
        /// DispOrder
        /// </summary>		
        private int? _disporder;
        [DataMember]
        public int? DispOrder
        {
            get { return _disporder; }
            set { _disporder = value; }
        }
        /// <summary>
        /// Prec
        /// </summary>		
        private int? _prec;
        [DataMember]
        public int? Prec
        {
            get { return _prec; }
            set { _prec = value; }
        }
        /// <summary>
        /// IsProfile
        /// </summary>		
        private int _isprofile;
        [DataMember]
        public int IsProfile
        {
            get { return _isprofile; }
            set { _isprofile = value; }
        }
        /// <summary>
        /// OrderNo
        /// </summary>		
        private string _orderno;
        [DataMember]
        public string OrderNo
        {
            get { return _orderno; }
            set { _orderno = value; }
        }
        /// <summary>
        /// StandardCode
        /// </summary>		
        private string _standardcode;
        [DataMember]
        public string StandardCode
        {
            get { return _standardcode; }
            set { _standardcode = value; }
        }
        /// <summary>
        /// ItemDesc
        /// </summary>		
        private string _itemdesc;
        [DataMember]
        public string ItemDesc
        {
            get { return _itemdesc; }
            set { _itemdesc = value; }
        }
        /// <summary>
        /// FWorkLoad
        /// </summary>		
        private decimal? _fworkload;
        [DataMember]
        public decimal? FWorkLoad
        {
            get { return _fworkload; }
            set { _fworkload = value; }
        }
        /// <summary>
        /// Secretgrade
        /// </summary>		
        private int? _secretgrade;
        [DataMember]
        public int? Secretgrade
        {
            get { return _secretgrade; }
            set { _secretgrade = value; }
        }
        /// <summary>
        /// Cuegrade
        /// </summary>		
        private int? _cuegrade;
        [DataMember]
        public int? Cuegrade
        {
            get { return _cuegrade; }
            set { _cuegrade = value; }
        }
        /// <summary>
        /// IsDoctorItem
        /// </summary>		
        private int? _isdoctoritem;
        [DataMember]
        public int? IsDoctorItem
        {
            get { return _isdoctoritem; }
            set { _isdoctoritem = value; }
        }
        /// <summary>
        /// IschargeItem
        /// </summary>		
        private int? _ischargeitem;
        [DataMember]
        public int? IschargeItem
        {
            get { return _ischargeitem; }
            set { _ischargeitem = value; }
        }
        /// <summary>
        /// HisDispOrder
        /// </summary>		
        private int? _hisdisporder;
        [DataMember]
        public int? HisDispOrder
        {
            get { return _hisdisporder; }
            set { _hisdisporder = value; }
        }
        /// <summary>
        /// IsNurseItem
        /// </summary>		
        private string _isnurseitem;
        [DataMember]
        public string IsNurseItem
        {
            get { return _isnurseitem; }
            set { _isnurseitem = value; }
        }
        /// <summary>
        /// IsCombiItem
        /// </summary>		
        private int? _iscombiitem;
        [DataMember]
        public int? IsCombiItem
        {
            get { return _iscombiitem; }
            set { _iscombiitem = value; }
        }
        /// <summary>
        /// IsTestItem
        /// </summary>		
        private int? _istestitem;
        [DataMember]
        public int? IsTestItem
        {
            get { return _istestitem; }
            set { _istestitem = value; }
        }
        /// <summary>
        /// IsHistoryItem
        /// </summary>		
        private int? _ishistoryitem;
        [DataMember]
        public int? IsHistoryItem
        {
            get { return _ishistoryitem; }
            set { _ishistoryitem = value; }
        }
        /// <summary>
        /// DTimeStampe
        /// </summary>		
        private DateTime? _dtimestampe;
        [DataMember]
        public DateTime? DTimeStampe
        {
            get { return _dtimestampe; }
            set { _dtimestampe = value; }
        }
        /// <summary>
        /// AddTime
        /// </summary>		
        private DateTime? _addtime;

        [DataMember]
        public DateTime? AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// UseFlag
        /// </summary>		
        private int? _useflag;
        [DataMember]
        public int? UseFlag
        {
            get { return _useflag; }
            set { _useflag = value; }
        }
        /// <summary>
        /// Color
        /// </summary>
        private string _color;
        [DataMember]
        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }
        /// <summary>
        /// LabSuperGroupNo
        /// </summary>
        private int? _labSuperGroupNo;
        [DataMember]
        public int? LabSuperGroupNo
        {
            get { return _labSuperGroupNo; }
            set { _labSuperGroupNo = value; }

        }

        [DataMember]
        public string StandCode { get; set; }

        [DataMember]
        public string ZFStandCode { get; set; }

        [DataMember]
        public int? SpecTypeNo { get; set; }

        [DataMember]
        public string LowPrice { get; set; }

        [DataMember]
        public string SpecialSection { get; set; }

        [DataMember]
        public string SpecialType { get; set; }

        //[DataMember]
        //public decimal? Price { get; set; }

        [DataMember]
        public int? SuperGroupNo { get; set; }

        /// <summary>
        /// digitlab2009和digitlab8使用LabItemNo；digitlab66和weblis使用ItemNo
        /// </summary>
        [DataMember]
        public string ItemNo { get; set; }

        [DataMember]
        public decimal? Price { get; set; }

        [DataMember]
        public string code_1 { get; set; }

        [DataMember]
        public string code_2 { get; set; }

        [DataMember]
        public string code_3 { get; set; }

        [DataMember]
        public int? DefaultSType { get; set; }

        [DataMember]
        public string SpecName { get; set; }

        [DataMember]
        public string zdy1 { get; set; }

        [DataMember]
        public string zdy2 { get; set; }

        public Common.Dictionary.TestItemSuperGroupClass? TestItemSuperGroupClass { get; set; }

        [DataMember]
        public string TestItemLikeKey { get; set; }

        private string _IfUseLike;//GetList方法里是否使用模糊查询：0或空为 不使用；1为 使用
        [DataMember]
        public decimal? MarketPrice { get; set; }
        [DataMember]
        public decimal? GreatMasterPrice { get; set; }
        [DataMember]
        public string Pic { get; set; }

        [DataMember]
        public string IfUseLike
        {
            get { return _IfUseLike; }
            set { _IfUseLike = value; }
        }
        [DataMember]
        public decimal? BonusPercent { get; set; }

        /// <summary>
        /// 体检标记
        /// </summary>		
        private int? _physicalFlag;
        [DataMember]
        public int? PhysicalFlag
        {
            get { return _physicalFlag; }
            set { _physicalFlag = value; }
        }

    }
}

