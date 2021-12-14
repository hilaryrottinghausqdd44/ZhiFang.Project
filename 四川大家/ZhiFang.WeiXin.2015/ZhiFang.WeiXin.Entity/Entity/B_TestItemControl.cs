using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
	#region BLabTestItem

	/// <summary>
	/// BLabTestItem object for NHibernate mapped table 'B_LabTestItem'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "项目对照表", ClassCName = "BTestItemControl", ShortCode = "BTestItemControl", Desc = "")]
	public class BTestItemControl : BaseEntity
	{
		#region Member Variables
		
        protected string _ItemControlNo;
        protected string _ItemNo;
        protected string _ControlLabNo;
        protected string _ControlItemNo;
        protected int? _UseFlag;


        #endregion

        #region Constructors

        public BTestItemControl() { }

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "ControlLabNo+ItemNo+ControlItemNo", ShortCode = "ItemControlNo", Desc = "ControlLabNo+ItemNo+ControlItemNo", ContextType = SysDic.All, Length = 300)]
        public virtual string ItemControlNo
        {
			get { return _ItemControlNo; }
			set
			{
                _ItemControlNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "中心端项目编码", ShortCode = "ItemNo", Desc = "中心端项目编码", ContextType = SysDic.All, Length = 100)]
        public virtual string ItemNo
		{
			get { return _ItemNo; }
			set
			{
                _ItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "实验室编号", ShortCode = "ControlLabNo", Desc = "实验室编号", ContextType = SysDic.All, Length = 4000)]
        public virtual string ControlLabNo
        {
			get { return _ControlLabNo; }
			set
			{
                _ControlLabNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "实验室项目编码", ShortCode = "ControlItemNo", Desc = "实验室项目编码", ContextType = SysDic.All, Length = 400)]
        public virtual string ControlItemNo
        {
			get { return _ControlItemNo; }
			set
			{
                _ControlItemNo = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "使用标记", ShortCode = "UseFlag", Desc = "使用标记", ContextType = SysDic.All, Length = 400)]
        public virtual int? UseFlag
        {
			get { return _UseFlag; }
			set
			{
                _UseFlag = value;
			}
		}
        
        #endregion
    }
	#endregion
}