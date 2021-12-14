using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region BTDAppComponents

    /// <summary>
    /// BTDAppComponents object for NHibernate mapped table 'BT_D_AppComponents'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "应用组件", ClassCName = "", ShortCode = "YYZJ", Desc = "应用组件")]
    public class BTDAppComponents : BaseEntityService
    {
        #region Member Variables
        protected string _cName;
        protected string _eName;
        protected string _moduleOperCode;
        protected string _moduleOperInfo;
        protected string _initParameter;
        protected int _appType;
        protected int _buildType;
        protected string _executeCode;
        protected string _designCode;
        protected string _classCode;
        protected string _creator;
        protected string _modifier;
        protected string _pinYinZiTou;
        protected DateTime? _dataUpdateTime;
        protected BTDModuleType _bTDModuleType;
        protected IList<BTDAppComponentsRef> _bTDAppComponentsRefList;
        protected IList<BTDAppComponentsOperate> _bTDAppComponentsOperateList;
        protected IList<RBACModule> _rBACModuleList;


        #endregion

        #region Constructors

        public BTDAppComponents() { }

        public BTDAppComponents(long labID, string cName, string eName, string moduleOperCode, string moduleOperInfo, string initParameter, int appType, int buildType, string executeCode, string designCode, string classCode, string creator, string modifier, string pinYinZiTou, DateTime? dataAddTime, DateTime? dataUpdateTime, byte[] dataTimeStamp, BTDModuleType bTDModuleType)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._moduleOperCode = moduleOperCode;
            this._moduleOperInfo = moduleOperInfo;
            this._initParameter = initParameter;
            this._appType = appType;
            this._buildType = buildType;
            this._executeCode = executeCode;
            this._designCode = designCode;
            this._classCode = classCode;
            this._creator = creator;
            this._modifier = modifier;
            this._pinYinZiTou = pinYinZiTou;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._bTDModuleType = bTDModuleType;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "MC", Desc = "名称", ContextType = SysDic.NText, Length = 100)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "YWMC", Desc = "英文名称", ContextType = SysDic.NText, Length = 100)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 100)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "功能编码", ShortCode = "GNBM", Desc = "功能编码", ContextType = SysDic.NText, Length = 20)]
        public virtual string ModuleOperCode
        {
            get { return _moduleOperCode; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ModuleOperCode", value, value.ToString());
                _moduleOperCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "功能简介", ShortCode = "GNJJ", Desc = "功能简介", ContextType = SysDic.NText)]
        public virtual string ModuleOperInfo
        {
            get { return _moduleOperInfo; }
            set
            {
                if (value != null && value.Length > 1000000)
                    throw new ArgumentOutOfRangeException("Invalid value for ModuleOperInfo", value, value.ToString());
                _moduleOperInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "初始化参数", ShortCode = "CSHCS", Desc = "初始化参数", ContextType = SysDic.NText, Length = 200)]
        public virtual string InitParameter
        {
            get { return _initParameter; }
            set
            {
                if (value != null && value.Length > 200)
                    throw new ArgumentOutOfRangeException("Invalid value for InitParameter", value, value.ToString());
                _initParameter = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "应用类型", ShortCode = "YYLX", Desc = "应用类型", ContextType = SysDic.Number, Length = 4)]
        public virtual int AppType
        {
            get { return _appType; }
            set { _appType = value; }
        }

        [DataMember]
        [DataDesc(CName = "构建类型", ShortCode = "GJLX", Desc = "构建类型", ContextType = SysDic.Number, Length = 4)]
        public virtual int BuildType
        {
            get { return _buildType; }
            set { _buildType = value; }
        }

        [DataMember]
        [DataDesc(CName = "执行代码", ShortCode = "ZXDM", Desc = "执行代码", ContextType = SysDic.NText)]
        public virtual string ExecuteCode
        {
            get { return _executeCode; }
            set
            {
                //if ( value != null && value.Length > 1000000)
                //    throw new ArgumentOutOfRangeException("Invalid value for ExecuteCode", value, value.ToString());
                _executeCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "设计代码", ShortCode = "SJDM", Desc = "设计代码", ContextType = SysDic.NText)]
        public virtual string DesignCode
        {
            get { return _designCode; }
            set
            {
                //if ( value != null && value.Length > 1000000)
                //    throw new ArgumentOutOfRangeException("Invalid value for DesignCode", value, value.ToString());
                _designCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "类代码", ShortCode = "SJDM", Desc = "设计代码", ContextType = SysDic.NText)]
        public virtual string ClassCode
        {
            get { return _classCode; }
            set
            {
                _classCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "CJZ", Desc = "创建者", ContextType = SysDic.NText, Length = 20)]
        public virtual string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        [DataMember]
        [DataDesc(CName = "修改者", ShortCode = "XGZ", Desc = "修改者", ContextType = SysDic.NText, Length = 20)]
        public virtual string Modifier
        {
            get { return _modifier; }
            set { _modifier = value; }
        }

        [DataMember]
        [DataDesc(CName = "汉字拼音字头", ShortCode = "HZPYZT", Desc = "汉字拼音字头", ContextType = SysDic.NText, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "SJGXSJ", Desc = "数据更新时间", ContextType = SysDic.DateTime)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }

        [DataMember]
        [DataDesc(CName = "应用类型", ShortCode = "YYLX", Desc = "应用类型")]
        public virtual BTDModuleType BTDModuleType
        {
            get { return _bTDModuleType; }
            set { _bTDModuleType = value; }
        }

        [DataMember]
        [DataDesc(CName = "应用组件引用关系", ShortCode = "BTDAppComponentsRefList", Desc = "应用组件引用关系")]
        public virtual IList<BTDAppComponentsRef> BTDAppComponentsRefList
        {
            get
            {
                if (_bTDAppComponentsRefList == null)
                {
                    _bTDAppComponentsRefList = new List<BTDAppComponentsRef>();
                }
                return _bTDAppComponentsRefList;
            }
            set { _bTDAppComponentsRefList = value; }
        }
        [DataMember]
        [DataDesc(CName = "程序操作包括按钮及非按钮操作（如下拉列表等）", ShortCode = "BTDAppComponentsOperateList", Desc = "程序操作包括按钮及非按钮操作（如下拉列表等）")]
        public virtual IList<BTDAppComponentsOperate> BTDAppComponentsOperateList
        {
            get
            {
                if (_bTDAppComponentsOperateList == null)
                {
                    _bTDAppComponentsOperateList = new List<BTDAppComponentsOperate>();
                }
                return _bTDAppComponentsOperateList;
            }
            set { _bTDAppComponentsOperateList = value; }
        }

        [DataMember]
        [DataDesc(CName = "模块列表", ShortCode = "RBACModuleList", Desc = "模块列表")]
        public virtual IList<RBACModule> RBACModuleList
        {
            get
            {
                if (_rBACModuleList == null)
                {
                    _rBACModuleList = new List<RBACModule>();
                }
                return _rBACModuleList;
            }
            set { _rBACModuleList = value; }
        }
        #endregion
    }
    #endregion
}