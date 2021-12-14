using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;

namespace ZhiFang.Entity.ReagentSys.Client
{
    #region ReaDeptGoods

    /// <summary>
    /// ReaDeptGoods object for NHibernate mapped table 'Rea_DeptGoods'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "部门货品表", ClassCName = "ReaDeptGoods", ShortCode = "ReaDeptGoods", Desc = "部门货品表")]
    public class ReaDeptGoods : BaseEntity
    {
        #region Member Variables

        //protected string _goodsCName;
        protected string _deptName;
        protected int _dispOrder;
        protected string _memo;
        protected bool _visible;
        protected long? _createrID;
        protected string _createrName;
        //protected HRDept _hRDept;
        protected long? _deptID;
        protected ReaGoods _reaGoods;

        #endregion

        #region Constructors

        public ReaDeptGoods() { }

        public ReaDeptGoods(long labID, string deptName, int dispOrder, string memo, bool visible, long createrID, string createrName, DateTime dataAddTime, byte[] dataTimeStamp, long? deptID, ReaGoods reaGoods)
        {
            this._labID = labID;
            //this._goodsCName = goodsCName;
            this._deptName = deptName;
            this._dispOrder = dispOrder;
            this._memo = memo;
            this._visible = visible;
            this._createrID = createrID;
            this._createrName = createrName;
            this._dataAddTime = dataAddTime;
            this._dataTimeStamp = dataTimeStamp;
            this._deptID = deptID;
            this._reaGoods = reaGoods;
        }

        #endregion

        #region Public Properties

        //      [DataMember]
        //      [DataDesc(CName = "产品中文名", ShortCode = "GoodsCName", Desc = "产品中文名", ContextType = SysDic.All, Length = 200)]
        //      public virtual string GoodsCName
        //{
        //	get { return _goodsCName; }
        //	set { _goodsCName = value; }
        //}

        [DataMember]
        [DataDesc(CName = "部门名称", ShortCode = "DeptName", Desc = "部门名称", ContextType = SysDic.All, Length = 50)]
        public virtual string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = -1)]
        public virtual string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "创建者ID", ShortCode = "CreaterID", Desc = "创建者ID", ContextType = SysDic.All, Length = 8)]
        public virtual long? CreaterID
        {
            get { return _createrID; }
            set { _createrID = value; }
        }

        [DataMember]
        [DataDesc(CName = "创建者姓名", ShortCode = "CreaterName", Desc = "创建者姓名", ContextType = SysDic.All, Length = 50)]
        public virtual string CreaterName
        {
            get { return _createrName; }
            set { _createrName = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "部门ID", ShortCode = "DeptID", Desc = "", ContextType = SysDic.All, Length = 8)]
        public virtual long? DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "部门", ShortCode = "HRDept", Desc = "部门")]
        //public virtual HRDept HRDept
        //{
        //    get { return _hRDept; }
        //    set { _hRDept = value; }
        //}

        [DataMember]
        [DataDesc(CName = "平台货品表", ShortCode = "ReaGoods", Desc = "平台货品表")]
        public virtual ReaGoods ReaGoods
        {
            get { return _reaGoods; }
            set { _reaGoods = value; }
        }
        #endregion

        #region 自定义属性

        protected ReaGoodsCurrentQtyVO _currentQtyVO;

        [DataMember]
        [DataDesc(CName = "获取采购申请货品库存数量VO", ShortCode = "CurrentQtyVO", Desc = "获取采购申请货品库存数量VO")]
        public virtual ReaGoodsCurrentQtyVO CurrentQtyVO
        {
            get { return _currentQtyVO; }
            set { _currentQtyVO = value; }
        }

        #endregion
    }
    #endregion
}