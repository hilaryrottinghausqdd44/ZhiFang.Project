using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity
{
	#region BParameter
	/// <summary>
	/// BParameter object for NHibernate mapped table 'B_Parameter'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "参数表", ClassCName = "BParameter", ShortCode = "BParameter", Desc = "参数表")]
    public class BParameter : BaseEntity
	{
		#region Member Variables


        protected BNodeTable _bNodeTable;
        protected string _name;
        protected string _sName;
        protected string _paraType;
        protected string _paraValue;
        protected string _paraDesc;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected bool _isUse;
        protected DateTime? _dataUpdateTime;

        protected string _paraNo;
        protected long? _groupNo;
        protected int _dispOrder;
        

		#endregion

		#region Constructors

		public BParameter() { }

        public BParameter(long labID, BNodeTable nodeID, string name, string sName, string paraType, string paraValue, string paraDesc, string shortcode, string pinYinZiTou, bool isUse, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
		{
			this._labID = labID;
			this._bNodeTable = nodeID;
			this._name = name;
			this._sName = sName;
			this._paraType = paraType;
			this._paraValue = paraValue;
			this._paraDesc = paraDesc;
			this._shortcode = shortcode;
			this._pinYinZiTou = pinYinZiTou;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataUpdateTime = dataUpdateTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties



        [DataMember]
        [DataDesc(CName = "站点", ShortCode = "BNodeTable", Desc = "站点", ContextType = SysDic.All, Length = 8)]
        public virtual BNodeTable BNodeTable
		{
            get { return _bNodeTable; }
            set { _bNodeTable = value; }
		}

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
                if (value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 40)]
        public virtual string SName
		{
			get { return _sName; }
			set
			{
                if (value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参数类型", ShortCode = "ParaType", Desc = "参数类型", ContextType = SysDic.All, Length = 40)]
        public virtual string ParaType
		{
			get { return _paraType; }
			set
			{
				if ( value != null && value.Length > 100)
					throw new ArgumentOutOfRangeException("Invalid value for ParaType", value, value.ToString());
				_paraType = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参数值", ShortCode = "ParaValue", Desc = "参数值", ContextType = SysDic.All, Length = 50)]
        public virtual string ParaValue
		{
			get { return _paraValue; }
			set
			{
				if ( value != null && value.Length > 8000)
					throw new ArgumentOutOfRangeException("Invalid value for ParaValue", value, value.ToString());
				_paraValue = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "参数说明", ShortCode = "ParaDesc", Desc = "参数说明", ContextType = SysDic.All, Length = 16)]
        public virtual string ParaDesc
		{
			get { return _paraDesc; }
			set
			{
                if (value != null && value.Length > 1000)
					throw new ArgumentOutOfRangeException("Invalid value for ParaDesc", value, value.ToString());
				_paraDesc = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "快捷码", ShortCode = "Shortcode", Desc = "快捷码", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "PinYinZiTou", Desc = "汉字拼音字头", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
		{
			get { return _pinYinZiTou; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
				_pinYinZiTou = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
		{
			get { return _isUse; }
			set { _isUse = value; }
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
        [DataDesc(CName = "参数编号", ShortCode = "ParaNo", Desc = "参数编号", ContextType = SysDic.All, Length = 8)]
        public virtual string ParaNo
        {
            get { return _paraNo; }
            set { _paraNo = value; }
        }

        [DataMember]
        [DataDesc(CName = "检验小组编号", ShortCode = "GroupNo", Desc = "检验小组编号", ContextType = SysDic.All, Length = 40)]
        public virtual long? GroupNo
        {
            get { return _groupNo; }
            set
            {
                _groupNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "显示序号", ShortCode = "DispOrder", Desc = "显示序号", ContextType = SysDic.All, Length = 40)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set
            {
                _dispOrder = value;
            }
        }

		#endregion
	}
	#endregion
}