using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region QCItem

	/// <summary>
	/// QCItem object for NHibernate mapped table 'QCItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "QCItem", ShortCode = "QCItem", Desc = "")]
	public class QCItem : BaseEntity
	{
		#region Member Variables
		
        protected long? _testItemNo;
        protected long? _equipNo;
        protected int _isLog;
        protected string _qCItemDesc;
        protected int _isMicro;
        protected int _qCDataType;
        protected string _qCDataTypeName;
		protected QCMaterial _qCMaterial;
		protected IList<QCData> _qCDataList; 
		//protected QCItemMMemo _qCItemMMemo;
		//protected IList<QCItemTime> _qCItemTimeList; 

		#endregion

		#region Constructors

		public QCItem() { }

		public QCItem( long testItemNo, long equipNo, int isLog, string qCItemDesc, int isMicro, int qCDataType, string qCDataTypeName, QCMaterial qCMaterial )
		{
			this._testItemNo = testItemNo;
			this._equipNo = equipNo;
			this._isLog = isLog;
			this._qCItemDesc = qCItemDesc;
			this._isMicro = isMicro;
			this._qCDataType = qCDataType;
			this._qCDataTypeName = qCDataTypeName;
			this._qCMaterial = qCMaterial;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "TestItemNo", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? TestItemNo
		{
			get { return _testItemNo; }
			set { _testItemNo = value; }
		}

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "EquipNo", Desc = "", ContextType = SysDic.All, Length = 8)]
		public virtual long? EquipNo
		{
			get { return _equipNo; }
			set { _equipNo = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsLog", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsLog
		{
			get { return _isLog; }
			set { _isLog = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCItemDesc", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string QCItemDesc
		{
			get { return _qCItemDesc; }
			set
			{
				if ( value != null && value.Length > 200)
					throw new ArgumentOutOfRangeException("Invalid value for QCItemDesc", value, value.ToString());
				_qCItemDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "IsMicro", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int IsMicro
		{
			get { return _isMicro; }
			set { _isMicro = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCDataType", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int QCDataType
		{
			get { return _qCDataType; }
			set { _qCDataType = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCDataTypeName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string QCDataTypeName
		{
			get { return _qCDataTypeName; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for QCDataTypeName", value, value.ToString());
				_qCDataTypeName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCMaterial", Desc = "")]
		public virtual QCMaterial QCMaterial
		{
			get { return _qCMaterial; }
			set { _qCMaterial = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "QCDataList", Desc = "")]
		public virtual IList<QCData> QCDataList
		{
			get
			{
				if (_qCDataList==null)
				{
					_qCDataList = new List<QCData>();
				}
				return _qCDataList;
			}
			set { _qCDataList = value; }
		}

       

        
		#endregion
	}
	#endregion
}