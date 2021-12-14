using System;
namespace ZhiFang.ReportFormQueryPrint.Model
{
	/// <summary>
	/// NRequestForm:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class NRequestItem
    {
		public NRequestItem()
		{}
        private string _serialno;
		private int? _paritemno;
		private string _orderno;
		private int? _receiveflag;
		private string _sampletypeno;
        private string _barcode;
        private int? _itemno;
        private int? _Samplinggroupno;
        private DateTime? _FlagDateDelete;
        private DateTime? _ItemDate;
        private string _zdy1;
        private string _zdy2;
        private string _zdy3;
        private string _zdy4;
        private string _zdy5;
        private string _SerialNoParent;
        private string _OldSerialNo;
        private string _uniteName;
        private string _uniteItemNo;
        private int? _IsCheckFee;
        private float? _charge;
        private int? _StateFlag;
        private string _PItemCName;
        private string _PItemNo;
        private int? _IsNurseDo;
        private int? _ItemDispenseFlag;
        private int? _PrepaymentFlag;
        private int? _Balance;
        private string _sampleno;
        private int? _chargeflag;
        private string _NRequestItemNo;
        private DateTime? _ReceiveDate;
        private int? _SectionNo;
        private int? _TestTypeNo;
        private string _OldNRequestItemNo;
        private int? _CheckFlag;
        private DateTime? _CheckTime;
        private string _Mergeno;
        private string _OldParItemno;
        private int? _NItemID;
        private int? _DispOrder;
        private DateTime? _FormFlagDateDelete;
        private string _ReportDateMemo;
        private string _ReportDateGroup;
        private string _GroupItemList;
        private int? _ItemGroupNo;
        private int? _iAutoUnion;
        private string _AutoUnionSNo;
        public string AutoUnionSNo
        {
            set { _AutoUnionSNo = value; }
            get { return _AutoUnionSNo; }
        }
        public string GroupItemList
        {
            set { _GroupItemList = value; }
            get { return _GroupItemList; }
        }
        public string ReportDateGroup
        {
            set { _ReportDateGroup = value; }
            get { return _ReportDateGroup; }
        }
        public string ReportDateMemo
        {
            set { _ReportDateMemo = value; }
            get { return _ReportDateMemo; }
        }

        public int? iAutoUnion
        {
            set { _iAutoUnion = value; }
            get { return _iAutoUnion; }
        }
        public int? ItemGroupNo
        {
            set { _ItemGroupNo = value; }
            get { return _ItemGroupNo; }
        }
        public DateTime? FormFlagDateDelete
        {
            set { _FormFlagDateDelete = value; }
            get { return _FormFlagDateDelete; }
        }
        public int? DispOrder
        {
            set { _DispOrder = value; }
            get { return _DispOrder; }
        }
        public int? NItemID
        {
            set { _NItemID = value; }
            get { return _NItemID; }
        }
        public string Mergeno
        {
            set { _Mergeno = value; }
            get { return _Mergeno; }
        }
        public string OldParItemno
        {
            set { _OldParItemno = value; }
            get { return _OldParItemno; }
        }

        public DateTime? CheckTime
        {
            set { _CheckTime = value; }
            get { return _CheckTime; }
        }
        public int? CheckFlag
        {
            set { _CheckFlag = value; }
            get { return _CheckFlag; }
        }
        public string OldNRequestItemNo
        {
            set { _OldNRequestItemNo = value; }
            get { return _OldNRequestItemNo; }
        }
        public int? TestTypeNo
        {
            set { _TestTypeNo = value; }
            get { return _TestTypeNo; }
        }
        public int? SectionNo
        {
            set { _SectionNo = value; }
            get { return _SectionNo; }
        }
        public DateTime? ReceiveDate
        {
            set { _ReceiveDate = value; }
            get { return _ReceiveDate; }
        }
        public string NRequestItemNo
        {
            set { _NRequestItemNo = value; }
            get { return _NRequestItemNo; }
        }
        public int? chargeflag
        {
            set { _chargeflag = value; }
            get { return _chargeflag; }
        }
        public string sampleno
        {
            set { _sampleno = value; }
            get { return _sampleno; }
        }

        public string PItemCName
        {
            set { _PItemCName = value; }
            get { return _PItemCName; }
        }
        public string PItemNo
        {
            set { _PItemNo = value; }
            get { return _PItemNo; }
        }
        public int? IsNurseDo
        {
            set { _IsNurseDo = value; }
            get { return _IsNurseDo; }
        }
        public int? Balance
        {
            set { _Balance = value; }
            get { return _Balance; }
        }
        public int? PrepaymentFlag
        {
            set { _PrepaymentFlag = value; }
            get { return _PrepaymentFlag; }
        }
        public int? ItemDispenseFlag
        {
            set { _ItemDispenseFlag = value; }
            get { return _ItemDispenseFlag; }
        }


        public int? IsCheckFee
        {
            set { _IsCheckFee = value; }
            get { return _IsCheckFee; }
        }
        public int? StateFlag
        {
            set { _StateFlag = value; }
            get { return _StateFlag; }
        }
        public float? charge
        {
            set { _charge = value; }
            get { return _charge; }
        }

        public string uniteName
        {
            set { _uniteName = value; }
            get { return _uniteName; }
        }
        public string uniteItemNo
        {
            set { _uniteItemNo = value; }
            get { return _uniteItemNo; }
        }
        public string OldSerialNo
        {
            set { _OldSerialNo = value; }
            get { return _OldSerialNo; }
        }
        public string SerialNoParent
        {
            set { _SerialNoParent = value; }
            get { return _SerialNoParent; }
        }
        public string zdy5
        {
            set { _zdy5 = value; }
            get { return _zdy5; }
        }
        public string zdy4
        {
            set { _zdy4 = value; }
            get { return _zdy4; }
        }
        public string zdy3
        {
            set { _zdy3 = value; }
            get { return _zdy3; }
        }
        public string zdy2
        {
            set { _zdy2 = value; }
            get { return _zdy2; }
        }
        public string zdy1
        {
            set { _zdy1 = value; }
            get { return _zdy1; }
        }
        public DateTime? ItemDate
        {
            set { _ItemDate = value; }
            get { return _ItemDate; }
        }
        public DateTime? FlagDateDelete
        {
            set { _FlagDateDelete = value; }
            get { return _FlagDateDelete; }
        }

        public int? Samplinggroupno
        {
            set { _Samplinggroupno = value; }
            get { return _Samplinggroupno; }
        }
        public int? itemno
        {
            set { _itemno = value; }
            get { return _itemno; }
        }
        public string barcode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        public string sampletypeno
        {
            set { _sampletypeno = value; }
            get { return _sampletypeno; }
        }
        public int? RECEIVEFLAG
        {
            set { _receiveflag = value; }
            get { return _receiveflag; }
        }
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        public int? ParItemNo
        {
            set { _paritemno = value; }
            get { return _paritemno; }
        }
        public string SerialNo
        {
            set { _serialno = value; }
            get { return _serialno; }
        }
       
		

	}
}

