using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaMonthUsageStatisticsDtl

    /// <summary>
    /// ReaMonthUsageStatisticsDtl object for NHibernate mapped table 'Rea_MonthUsageStatisticsDtl'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "ReaMonthUsageStatisticsDtl", ShortCode = "ReaMonthUsageStatisticsDtl", Desc = "")]
    public class ReaMonthUsageStatisticsDtl : BaseEntity
    {
        #region Member Variables
        protected long _deptID;
        protected string _deptName;
        protected long _docID;
        protected string _goodsName;
        protected string _goodsUnit;
        protected string _prodGoodsNo;
        protected string _reaGoodsNo;
        protected string _cenOrgGoodsNo;
        protected string _unitMemo;
        protected double _outQty;
        protected int _dispOrder;
        protected bool _visible;
        protected DateTime _dataUpdateTime;


        #endregion

        #region Constructors

        public ReaMonthUsageStatisticsDtl() { }

        public ReaMonthUsageStatisticsDtl(long labID, long docID, long deptID, string goodsName, string goodsUnit, string prodGoodsNo, string reaGoodsNo, string cenOrgGoodsNo, string unitMemo, double outQty, int dispOrder, bool visible, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._docID = docID;
            this._deptID = deptID;
            this._goodsName = goodsName;
            this._goodsUnit = goodsUnit;
            this._prodGoodsNo = prodGoodsNo;
            this._reaGoodsNo = reaGoodsNo;
            this._cenOrgGoodsNo = cenOrgGoodsNo;
            this._unitMemo = unitMemo;
            this._outQty = outQty;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DeptName", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for DeptName", value, value.ToString());
                _deptName = value;
            }
        }
        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DocID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long DocID
        {
            get { return _docID; }
            set { _docID = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsName", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsName
        {
            get { return _goodsName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for GoodsName", value, value.ToString());
                _goodsName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "GoodsUnit", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsUnit
        {
            get { return _goodsUnit; }
            set
            {
                _goodsUnit = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ProdGoodsNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ProdGoodsNo
        {
            get { return _prodGoodsNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ProdGoodsNo", value, value.ToString());
                _prodGoodsNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ReaGoodsNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string ReaGoodsNo
        {
            get { return _reaGoodsNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for ReaGoodsNo", value, value.ToString());
                _reaGoodsNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CenOrgGoodsNo", Desc = "", ContextType = SysDic.All, Length = 100)]
        public virtual string CenOrgGoodsNo
        {
            get { return _cenOrgGoodsNo; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CenOrgGoodsNo", value, value.ToString());
                _cenOrgGoodsNo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "UnitMemo", Desc = "", ContextType = SysDic.All, Length = 200)]
        public virtual string UnitMemo
        {
            get { return _unitMemo; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for UnitMemo", value, value.ToString());
                _unitMemo = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "OutQty", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual double OutQty
        {
            get { return _outQty; }
            set { _outQty = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "DispOrder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "", ShortCode = "DataUpdateTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }


        #endregion
    }
    #endregion
}