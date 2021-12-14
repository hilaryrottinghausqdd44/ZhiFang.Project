using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region MEEquipSampleForm

	/// <summary>
	/// MEEquipSampleForm object for NHibernate mapped table 'ME_EquipSampleForm'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "仪器样本单", ClassCName = "MEEquipSampleForm", ShortCode = "MEEquipSampleForm", Desc = "仪器样本单")]
	public class MEEquipSampleForm : BaseEntity
	{
		#region Member Variables
		
        protected long? _groupSampleFormID;
        protected string _equipModuleCode;
        protected string _eBarCode;
        protected DateTime? _eTestDate;
        protected string _eSampleNo;
        protected string _eRack;
        protected int _ePosition;
        protected int _eFinishCode;
        protected string _eFinishInfo;
        protected string _eResultComment;
        protected DateTime? _dataUpdateTime;
		protected EPBEquip _ePBEquip;
		protected IList<MEEquipSampleItem> _mEEquipSampleItemList;
        private int _eItemResultFlag;
        private string _eItemResultInfo;
        private bool _eBatchEndFlag;
        private string _eBatchEndInfo; 

		#endregion

		#region Constructors

		public MEEquipSampleForm() { }

        public MEEquipSampleForm(long labID, long groupSampleFormID, string equipModuleCode, string eBarCode, DateTime eTestDate, string eSampleNo, string eRack, int ePosition, int eFinishCode, string eFinishInfo, string eResultComment, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPBEquip ePBEquip)
		{
			this._labID = labID;
			this._groupSampleFormID = groupSampleFormID;
			this._equipModuleCode = equipModuleCode;
			this._eTestDate = eTestDate;
			this._eSampleNo = eSampleNo;
			this._eRack = eRack;
			this._ePosition = ePosition;
			this._eFinishCode = eFinishCode;
			this._eFinishInfo = eFinishInfo;
			this._eResultComment = eResultComment;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
			this._ePBEquip = ePBEquip;
            this._eBarCode = eBarCode;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "小组样本单ID", ShortCode = "GroupSampleFormID", Desc = "小组样本单ID", ContextType = SysDic.All, Length = 8)]
		public virtual long? GroupSampleFormID
		{
			get { return _groupSampleFormID; }
			set { _groupSampleFormID = value; }
		}

        [DataMember]
        [DataDesc(CName = "模块Code", ShortCode = "EquipModuleCode", Desc = "模块Code", ContextType = SysDic.All, Length = 20)]
        public virtual string EquipModuleCode
		{
			get { return _equipModuleCode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for EquipModuleCode", value, value.ToString());
				_equipModuleCode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "条码号", ShortCode = "EBarCode", Desc = "条码号", ContextType = SysDic.All, Length = 20)]
        public virtual string EBarCode
        {
            get { return _eBarCode; }
            set
            {
                _eBarCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器检验日期", ShortCode = "ETestDate", Desc = "仪器检验日期", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? ETestDate
		{
			get { return _eTestDate; }
			set { _eTestDate = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器样本号", ShortCode = "ESampleNo", Desc = "仪器样本号", ContextType = SysDic.All, Length = 20)]
        public virtual string ESampleNo
		{
			get { return _eSampleNo; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ESampleNo", value, value.ToString());
				_eSampleNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器架子号", ShortCode = "ERack", Desc = "仪器架子号", ContextType = SysDic.All, Length = 20)]
        public virtual string ERack
		{
			get { return _eRack; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for ERack", value, value.ToString());
				_eRack = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器位置号", ShortCode = "EPosition", Desc = "仪器位置号", ContextType = SysDic.All, Length = 4)]
        public virtual int EPosition
		{
			get { return _ePosition; }
			set { _ePosition = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器检测完结标志", ShortCode = "EFinishCode", Desc = "仪器检测完结标志", ContextType = SysDic.All, Length = 4)]
        public virtual int EFinishCode
		{
			get { return _eFinishCode; }
			set { _eFinishCode = value; }
		}

        [DataMember]
        [DataDesc(CName = "检测完结说明", ShortCode = "EFinishInfo", Desc = "检测完结说明", ContextType = SysDic.All, Length = 50)]
        public virtual string EFinishInfo
		{
			get { return _eFinishInfo; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for EFinishInfo", value, value.ToString());
				_eFinishInfo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "仪器结果备注", ShortCode = "EResultComment", Desc = "仪器结果备注", ContextType = SysDic.All, Length = 16)]
        public virtual string EResultComment
		{
			get { return _eResultComment; }
			set
			{
				_eResultComment = value;
			}
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
		public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器结果提取状态", ShortCode = "EItemResultFlag", Desc = "仪器结果提取状态", ContextType = SysDic.All, Length = 4)]
        public virtual int EItemResultFlag
        {
            get { return _eItemResultFlag; }
            set { _eItemResultFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "检测结果提取说明", ShortCode = "EItemResultInfo", Desc = "检测结果提取说明", ContextType = SysDic.All, Length = 50)]
        public virtual string EItemResultInfo
        {
            get { return _eItemResultInfo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EItemResultInfo", value, value.ToString());
                _eItemResultInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器批结标记", ShortCode = "EBatchEndFlag", Desc = "仪器批结标记", ContextType = SysDic.All, Length = 4)]
        public virtual bool EBatchEndFlag
        {
            get { return _eBatchEndFlag; }
            set { _eBatchEndFlag = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器批结说明", ShortCode = "EBatchEndInfo", Desc = "仪器批结说明", ContextType = SysDic.All, Length = 50)]
        public virtual string EBatchEndInfo
        {
            get { return _eBatchEndInfo; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EBatchEndInfo", value, value.ToString());
                _eBatchEndInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器表", ShortCode = "EPBEquip", Desc = "仪器表")]
		public virtual EPBEquip EPBEquip
		{
			get { return _ePBEquip; }
			set { _ePBEquip = value; }
		}

        [DataMember]
        [DataDesc(CName = "仪器样本项目", ShortCode = "MEEquipSampleItemList", Desc = "仪器样本项目")]
		public virtual IList<MEEquipSampleItem> MEEquipSampleItemList
		{
			get
			{
				if (_mEEquipSampleItemList==null)
				{
					_mEEquipSampleItemList = new List<MEEquipSampleItem>();
				}
				return _mEEquipSampleItemList;
			}
			set { _mEEquipSampleItemList = value; }
		}

        
		#endregion
	}
	#endregion
}