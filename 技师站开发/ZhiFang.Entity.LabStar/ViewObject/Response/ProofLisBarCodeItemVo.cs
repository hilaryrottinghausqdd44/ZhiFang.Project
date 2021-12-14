using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    [DataDesc(CName = "取单凭证样本项目列表VO", ClassCName = "ProofLisBarCodeItemVo", ShortCode = "ProofLisBarCodeItemVo", Desc = "")]
    public class ProofLisBarCodeItemVo
    {
		#region LisBarCodeItem
		protected DateTime? _partitionDate;
		protected long? _barCodesItemID;
		protected long? _ordersItemID;
		protected int _barCodeItemFlag;
		protected string _reportDateDesc;
		protected int _receiveFlag;
		protected DateTime? _collectTime;
		protected DateTime? _inceptTime;
		protected DateTime? _checkTime;
		protected int _itemDispenseFlag;
		protected int _isItemSplitReceive;
		protected long? _itemStatusID;
		protected long _barCodeFormID;
        protected long? _barCodeItemID;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long? BarCodeItemID
        {
            get { return _barCodeItemID; }
            set { _barCodeItemID = value; }
        }
        [DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		public virtual long BarCodeFormID
		{
			get { return _barCodeFormID; }
			set { _barCodeFormID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "分区日期", ShortCode = "PartitionDate", Desc = "分区日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? PartitionDate
		{
			get { return _partitionDate; }
			set { _partitionDate = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样项目ID", ShortCode = "BarCodesItemID", Desc = "采样项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? BarCodesItemID
		{
			get { return _barCodesItemID; }
			set { _barCodesItemID = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "医嘱项目ID", ShortCode = "OrdersItemID", Desc = "医嘱项目ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? OrdersItemID
		{
			get { return _ordersItemID; }
			set { _ordersItemID = value; }
		}

		[DataMember]
		[DataDesc(CName = "条码项目标志", ShortCode = "BarCodeItemFlag", Desc = "条码项目标志", ContextType = SysDic.All, Length = 4)]
		public virtual int BarCodeItemFlag
		{
			get { return _barCodeItemFlag; }
			set { _barCodeItemFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "取单时间描述", ShortCode = "ReportDateDesc", Desc = "取单时间描述", ContextType = SysDic.All, Length = 8)]
		public virtual string ReportDateDesc
		{
			get { return _reportDateDesc; }
			set { _reportDateDesc = value; }
		}

		[DataMember]
		[DataDesc(CName = "核收标志", ShortCode = "ReceiveFlag", Desc = "核收标志", ContextType = SysDic.All, Length = 4)]
		public virtual int ReceiveFlag
		{
			get { return _receiveFlag; }
			set { _receiveFlag = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "采样时间", ShortCode = "CollectTime", Desc = "采样时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CollectTime
		{
			get { return _collectTime; }
			set { _collectTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "签收时间", ShortCode = "InceptTime", Desc = "签收时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? InceptTime
		{
			get { return _inceptTime; }
			set { _inceptTime = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "审核时间", ShortCode = "CheckTime", Desc = "审核时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? CheckTime
		{
			get { return _checkTime; }
			set { _checkTime = value; }
		}

		[DataMember]
		[DataDesc(CName = "项目分发标志", ShortCode = "ItemDispenseFlag", Desc = "项目分发标志", ContextType = SysDic.All, Length = 4)]
		public virtual int ItemDispenseFlag
		{
			get { return _itemDispenseFlag; }
			set { _itemDispenseFlag = value; }
		}

		[DataMember]
		[DataDesc(CName = "是否组合项目拆分子项核收", ShortCode = "IsItemSplitReceive", Desc = "是否组合项目拆分子项核收", ContextType = SysDic.All, Length = 4)]
		public virtual int IsItemSplitReceive
		{
			get { return _isItemSplitReceive; }
			set { _isItemSplitReceive = value; }
		}

		[DataMember]
		[JsonConverter(typeof(JsonConvertClass))]
		[DataDesc(CName = "状态标志", ShortCode = "ItemStatusID", Desc = "状态标志", ContextType = SysDic.All, Length = 8)]
		public virtual long? ItemStatusID
		{
			get { return _itemStatusID; }
			set { _itemStatusID = value; }
		}
		#endregion

		#region LisOrderItem
		protected string _orderFormNo;
		protected DateTime? _orderDate;
		protected int _orderItemExecFlag;
		protected int _isCancelled;
		protected int _isPriceItem;
		protected int _isCheckFee;
		protected double _charge;
		protected string _removeHost;
		protected string _remover;
		protected DateTime? _removeTime;
		protected long? _sampleTypeID;
		protected string _collectPart;
		protected string _hisItemNo;
		protected string _hisItemName;
		protected string _hisSampleTypeNo;
		protected string _hisSampleTypeName;
        protected long? _orderItemID;
        protected string _itemName;

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        public virtual long? OrderItemID
        {
            get { return _orderItemID; }
            set { _orderItemID = value; }
        }
        [DataMember]
        public virtual string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }


        [DataMember]
        [DataDesc(CName = "医嘱单号", ShortCode = "OrderFormNo", Desc = "医嘱单号", ContextType = SysDic.All, Length = 100)]
        public virtual string OrderFormNo
        {
            get { return _orderFormNo; }
            set { _orderFormNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "医嘱日期", ShortCode = "OrderDate", Desc = "医嘱日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "医嘱项目执行标志", ShortCode = "OrderItemExecFlag", Desc = "医嘱项目执行标志", ContextType = SysDic.All, Length = 4)]
        public virtual int OrderItemExecFlag
        {
            get { return _orderItemExecFlag; }
            set { _orderItemExecFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经作废", ShortCode = "IsCancelled", Desc = "是否已经作废", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCancelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否增补收费项目", ShortCode = "IsPriceItem", Desc = "是否增补收费项目", ContextType = SysDic.All, Length = 4)]
        public virtual int IsPriceItem
        {
            get { return _isPriceItem; }
            set { _isPriceItem = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否已经收费", ShortCode = "IsCheckFee", Desc = "是否已经收费", ContextType = SysDic.All, Length = 4)]
        public virtual int IsCheckFee
        {
            get { return _isCheckFee; }
            set { _isCheckFee = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "费用金额", ShortCode = "Charge", Desc = "费用金额", ContextType = SysDic.All, Length = 8)]
        public virtual double Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataMember]
        [DataDesc(CName = "退费站点", ShortCode = "RemoveHost", Desc = "退费站点", ContextType = SysDic.All, Length = 100)]
        public virtual string RemoveHost
        {
            get { return _removeHost; }
            set { _removeHost = value; }
        }

        [DataMember]
        [DataDesc(CName = "退费人", ShortCode = "Remover", Desc = "退费人", ContextType = SysDic.All, Length = 100)]
        public virtual string Remover
        {
            get { return _remover; }
            set { _remover = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "退费时间", ShortCode = "RemoveTime", Desc = "退费时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RemoveTime
        {
            get { return _removeTime; }
            set { _removeTime = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "样本类型ID", ShortCode = "SampleTypeID", Desc = "样本类型ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? SampleTypeID
        {
            get { return _sampleTypeID; }
            set { _sampleTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "采样部位", ShortCode = "CollectPart", Desc = "采样部位", ContextType = SysDic.All, Length = 100)]
        public virtual string CollectPart
        {
            get { return _collectPart; }
            set { _collectPart = value; }
        }

        [DataMember]
        [DataDesc(CName = "HIS项目编号", ShortCode = "HisItemNo", Desc = "HIS项目编号", ContextType = SysDic.All, Length = 100)]
        public virtual string HisItemNo
        {
            get { return _hisItemNo; }
            set { _hisItemNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "HIS项目名称", ShortCode = "HisItemName", Desc = "HIS项目名称", ContextType = SysDic.All, Length = 100)]
        public virtual string HisItemName
        {
            get { return _hisItemName; }
            set { _hisItemName = value; }
        }

        [DataMember]
        [DataDesc(CName = "His样本类型编号", ShortCode = "HisSampleTypeNo", Desc = "His样本类型编号", ContextType = SysDic.All, Length = 100)]
        public virtual string HisSampleTypeNo
        {
            get { return _hisSampleTypeNo; }
            set { _hisSampleTypeNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "His样本类型", ShortCode = "HisSampleTypeName", Desc = "His样本类型", ContextType = SysDic.All, Length = 100)]
        public virtual string HisSampleTypeName
        {
            get { return _hisSampleTypeName; }
            set { _hisSampleTypeName = value; }
        }

        #endregion

    }
}
