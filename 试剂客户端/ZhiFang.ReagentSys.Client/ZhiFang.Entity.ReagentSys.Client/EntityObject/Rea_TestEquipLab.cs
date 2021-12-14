using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaTestEquipLab

    /// <summary>
    /// TestEquipLab object for NHibernate mapped table 'ReaTestEquipLab'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaTestEquipLab", ShortCode = "ReaTestEquipLab", Desc = "")]
    public class ReaTestEquipLab : BaseEntity
    {
        #region Member Variables

        protected long? _testProdEquipID;
        protected long? _prodOrgID;
        protected long? _compOrgID;
        protected long? _testEquipTypeID;
        protected string _cName;
        protected string _eName;
        protected string _shortCode;
        protected string _memo;
        protected int _visible;
        protected int _dispOrder;
        protected string _lisCode;
        protected DateTime? _dataUpdateTime;
        protected long? _deptID;
        protected string _deptName;

        #endregion

        #region Constructors

        public ReaTestEquipLab() { }

        public ReaTestEquipLab(long testProdEquipID, long labID, long prodOrgID, long compOrgID, long testEquipTypeID, string cName, string eName, string shortCode, string memo, int visible, int dispOrder, string lisCode, DateTime dataUpdateTime, DateTime dataAddTime, byte[] dataTimeStamp)
        {
            this._testProdEquipID = testProdEquipID;
            this._labID = labID;
            this._prodOrgID = prodOrgID;
            this._compOrgID = compOrgID;
            this._testEquipTypeID = testEquipTypeID;
            this._cName = cName;
            this._eName = eName;
            this._shortCode = shortCode;
            this._memo = memo;
            this._visible = visible;
            this._dispOrder = dispOrder;
            this._lisCode = lisCode;
            this._dataUpdateTime = dataUpdateTime;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties
        [DataMember]
        [DataDesc(CName = "所属部门", ShortCode = "DeptName", Desc = "所属部门", ContextType = SysDic.All, Length = 200)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                _deptName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器所属部门ID", ShortCode = "DeptID", Desc = "仪器所属部门ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "厂商仪器ID(关联厂商检验仪器)", ShortCode = "TestProdEquipID", Desc = "厂商仪器ID(关联厂商检验仪器)", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestProdEquipID
        {
            get { return _testProdEquipID; }
            set { _testProdEquipID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器厂商ID", ShortCode = "ProdOrgID", Desc = "仪器厂商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? ProdOrgID
        {
            get { return _prodOrgID; }
            set { _prodOrgID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "仪器供应商ID", ShortCode = "CompOrgID", Desc = "仪器供应商ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CompOrgID
        {
            get { return _compOrgID; }
            set { _compOrgID = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "检验仪器分类ID", ShortCode = "TestEquipTypeID", Desc = "检验仪器分类ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? TestEquipTypeID
        {
            get { return _testEquipTypeID; }
            set { _testEquipTypeID = value; }
        }

        [DataMember]
        [DataDesc(CName = "中文名称", ShortCode = "CName", Desc = "中文名称", ContextType = SysDic.All, Length = 300)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 300)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 200)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "ShortCode", Desc = "代码", ContextType = SysDic.All, Length = 100)]
        public virtual string ShortCode
        {
            get { return _shortCode; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ShortCode", value, value.ToString());
                _shortCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 500)]
        public virtual string Memo
        {
            get { return _memo; }
            set
            {
                if (value != null && value.Length > 500)
                    throw new ArgumentOutOfRangeException("Invalid value for Memo", value, value.ToString());
                _memo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "LIS仪器编号", ShortCode = "LisCode", Desc = "LIS仪器编号", ContextType = SysDic.All, Length = 100)]
        public virtual string LisCode
        {
            get { return _lisCode; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for LisCode", value, value.ToString());
                _lisCode = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "修改时间", ShortCode = "DataUpdateTime", Desc = "修改时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}