using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{
    #region GoodsQualification

    /// <summary>
    /// GoodsQualification object for NHibernate mapped table 'GoodsQualification'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "资质证件管理", ClassCName = "GoodsQualification", ShortCode = "GoodsQualification", Desc = "资质证件管理")]
    public class GoodsQualification : BaseEntity
    {
        #region Member Variables
        protected string _cenOrgCName;
        protected string _compCName;
        protected string _cName;
        protected string _registerNo;
        protected DateTime? _registerDate;
        protected DateTime? _registerInvalidDate;
        protected string _registerFilePath;
        protected string _fileType;
        protected string _fileName;
        protected string _fileExt;
        protected int _classType;
        protected int _dispOrder;
        protected int _visible;
        protected string _authorizeCName;
        protected string _telephone;
        protected string _memo;
        protected DateTime? _dataUpdateTime;
        protected CenOrg _cenOrg;
        protected CenOrg _comp;
        #endregion

        #region Constructors

        public GoodsQualification() { }

        public GoodsQualification(long labID, long cenOrgID, string cenOrgCName, string compCName, string cName, string eName, string shortCode, string goodsLotNo, string registerNo, DateTime registerDate, DateTime registerInvalidDate, string registerFilePath, int dispOrder, int visible, int classType, string telephone, string empName, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cenOrgCName = cenOrgCName;
            this._compCName = compCName;
            this._cName = cName;
            this._registerNo = registerNo;
            this._registerDate = registerDate;
            this._registerInvalidDate = registerInvalidDate;
            this._registerFilePath = registerFilePath;
            this._dispOrder = dispOrder;
            this._visible = visible;
            this._classType = classType;
            this._telephone = telephone;
            this._authorizeCName = empName;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "所用医院", ShortCode = "CenOrgCName", Desc = "所用医院", ContextType = SysDic.All, Length = 200)]
        public virtual string CenOrgCName
        {
            get { return _cenOrgCName; }
            set { _cenOrgCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商名称", ShortCode = "CompCName", Desc = "供应商名称", ContextType = SysDic.All, Length = 200)]
        public virtual string CompCName
        {
            get { return _compCName; }
            set { _compCName = value; }
        }

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "产品中文名", ContextType = SysDic.All, Length = 200)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "注册证编号", ShortCode = "RegisterNo", Desc = "注册证编号", ContextType = SysDic.All, Length = 200)]
        public virtual string RegisterNo
        {
            get { return _registerNo; }
            set { _registerNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册日期", ShortCode = "RegisterDate", Desc = "注册日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterDate
        {
            get { return _registerDate; }
            set { _registerDate = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "注册证有效期", ShortCode = "RegisterInvalidDate", Desc = "注册证有效期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? RegisterInvalidDate
        {
            get { return _registerInvalidDate; }
            set { _registerInvalidDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "注册文件路径", ShortCode = "RegisterFilePath", Desc = "注册文件路径", ContextType = SysDic.All, Length = 500)]
        public virtual string RegisterFilePath
        {
            get { return _registerFilePath; }
            set { _registerFilePath = value; }
        }
        [DataMember]
        [DataDesc(CName = "文件名称", ShortCode = "FileName", Desc = "文件名称", ContextType = SysDic.All, Length = 200)]
        public virtual string FileName
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        [DataMember]
        [DataDesc(CName = "文件后缀名", ShortCode = "FileExt", Desc = "文件后缀名", ContextType = SysDic.All, Length = 50)]
        public virtual string FileExt
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        [DataMember]
        [DataDesc(CName = "文件类型", ShortCode = "FileType", Desc = "文件类型", ContextType = SysDic.All, Length = 200)]
        public virtual string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "Visible", Desc = "是否使用", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        [DataMember]
        [DataDesc(CName = "类型", ShortCode = "ClassType", Desc = "类型", ContextType = SysDic.All, Length = 4)]
        public virtual int ClassType
        {
            get { return _classType; }
            set { _classType = value; }
        }

        [DataMember]
        [DataDesc(CName = "授权人", ShortCode = "AuthorizeCName", Desc = "授权人", ContextType = SysDic.All, Length = 50)]
        public virtual string AuthorizeCName
        {
            get { return _authorizeCName; }
            set { _authorizeCName = value; }
        }
        [DataMember]
        [DataDesc(CName = "联系电话", ShortCode = "Telephone", Desc = "联系电话", ContextType = SysDic.All, Length = 20)]
        public virtual string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
        [DataMember]
        [DataDesc(CName = "备注", ShortCode = "Memo", Desc = "备注", ContextType = SysDic.All, Length = 50)]
        public virtual string Memo
        {
            get { return _memo; }
            set { _memo = value; }
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
        [DataDesc(CName = "机构", ShortCode = "CenOrg", Desc = "")]
        public virtual CenOrg CenOrg
        {
            get { return _cenOrg; }
            set { _cenOrg = value; }
        }

        [DataMember]
        [DataDesc(CName = "供应商", ShortCode = "Comp", Desc = "")]
        public virtual CenOrg Comp
        {
            get { return _comp; }
            set { _comp = value; }
        }

        #endregion
    }
    #endregion
}