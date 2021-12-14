using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.OA
{
	#region ATAttendanceEventType

	/// <summary>
	/// ATAttendanceEventType object for NHibernate mapped table 'AT_AttendanceEventType'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "考勤事件类型表", ClassCName = "ATAttendanceEventType", ShortCode = "ATAttendanceEventType", Desc = "考勤事件类型表")]
	public class ATAttendanceEventType : BaseEntityService
    {
		#region Member Variables
		
        protected string _name;
        protected string _sName;
        protected string _shortcode;
        protected long? _aTEventTypeGroupID;
        protected bool _isNeedInptutMemo;
        protected string _pinYinZiTou;
        protected int _dispOrder;
        protected string _comment;
        protected bool _isUse;
		protected IList<ATEmpAttendanceEventLog> _aTEmpAttendanceEventLogList; 

		#endregion

		#region Constructors

		public ATAttendanceEventType() { }

		public ATAttendanceEventType( long labID, string name, string sName, string shortcode, long aTEventTypeGroupID, bool isNeedInptutMemo, string pinYinZiTou, int dispOrder, string comment, bool isUse, DateTime dataAddTime, byte[] dataTimeStamp )
		{
			this._labID = labID;
			this._name = name;
			this._sName = sName;
			this._shortcode = shortcode;
			this._aTEventTypeGroupID = aTEventTypeGroupID;
			this._isNeedInptutMemo = isNeedInptutMemo;
			this._pinYinZiTou = pinYinZiTou;
			this._dispOrder = dispOrder;
			this._comment = comment;
			this._isUse = isUse;
			this._dataAddTime = dataAddTime;
			this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "Name", Desc = "名称", ContextType = SysDic.All, Length = 40)]
        public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 40)
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
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for SName", value, value.ToString());
				_sName = value;
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "0:打卡;1:请假;2:外出;3:出差;4:加班", ShortCode = "ATEventTypeGroupID", Desc = "0:打卡;1:请假;2:外出;3:出差;4:加班", ContextType = SysDic.All, Length = 8)]
		public virtual long? ATEventTypeGroupID
		{
			get { return _aTEventTypeGroupID; }
			set { _aTEventTypeGroupID = value; }
		}

        [DataMember]
        [DataDesc(CName = "是否需要填写说明", ShortCode = "IsNeedInptutMemo", Desc = "是否需要填写说明", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsNeedInptutMemo
		{
			get { return _isNeedInptutMemo; }
			set { _isNeedInptutMemo = value; }
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
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
		{
			get { return _dispOrder; }
			set { _dispOrder = value; }
		}

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Comment", Desc = "备注", ContextType = SysDic.All, Length = 300)]
        public virtual string Comment
		{
			get { return _comment; }
			set
			{
				if ( value != null && value.Length > 300)
					throw new ArgumentOutOfRangeException("Invalid value for Comment", value, value.ToString());
				_comment = value;
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
        [DataDesc(CName = "员工考勤事件日志表", ShortCode = "ATEmpAttendanceEventLogList", Desc = "员工考勤事件日志表")]
		public virtual IList<ATEmpAttendanceEventLog> ATEmpAttendanceEventLogList
		{
			get
			{
				if (_aTEmpAttendanceEventLogList==null)
				{
					_aTEmpAttendanceEventLogList = new List<ATEmpAttendanceEventLog>();
				}
				return _aTEmpAttendanceEventLogList;
			}
			set { _aTEmpAttendanceEventLogList = value; }
		}

        
		#endregion
	}
	#endregion
}