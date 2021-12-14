using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.Entity
{
    #region EPBEquip

    /// <summary>
    /// EPBEquip object for NHibernate mapped table 'EP_B_Equip'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "仪器表", ClassCName = "EPBEquip", ShortCode = "EPBEquip", Desc = "仪器表")]
    public class EPBEquip : BaseEntity
    {
        #region Member Variables

        protected string _useCode;
        protected string _standCode;
        protected string _deveCode;
        protected string _equipChannel;
        protected string _cName;
        protected string _eName;
        protected string _sName;
        protected string _shortcode;
        protected string _pinYinZiTou;
        protected string _comment;
        protected long _groupID;
        protected string _computer;
        protected string _comProgram;
        protected EPBEquipDoubleDir _doubleDir;
        //protected EPBEquipType _equipType;
        protected EPEquipType _ePEquipType;
        protected string _licenceKey;
        protected string _licenceType;
        protected string _sQH;
        protected int _sNo;
        protected DateTime? _licenceDate;
        protected bool _isUse;
        protected int _dispOrder;
        protected DateTime? _dataUpdateTime;
        protected IList<EPModule> _ePModuleList;
        protected IList<GMGroupEquip> _gMGroupEquipList;
        protected IList<EPEquipItem> _ePEquipItemList;
        protected int _equipResultType;

        #endregion

        #region Constructors

        public EPBEquip() { }

        public EPBEquip(long labID, string useCode, string standCode, string deveCode, string cName, string eName, string sName, string shortcode, string pinYinZiTou, string comment, long groupID, string computer, string comProgram, EPBEquipDoubleDir doubleDir, string licenceKey, string licenceType, string sQH, int sNo, DateTime licenceDate, bool isUse, int dispOrder, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp, EPEquipType ePEquipType)
        {
            this._labID = labID;
            this._useCode = useCode;
            this._standCode = standCode;
            this._deveCode = deveCode;
            this._cName = cName;
            this._eName = eName;
            this._sName = sName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
            this._comment = comment;
            this._groupID = groupID;
            this._computer = computer;
            this._comProgram = comProgram;
            this._doubleDir = doubleDir;
            this._licenceKey = licenceKey;
            this._licenceType = licenceType;
            this._sQH = sQH;
            this._sNo = sNo;
            this._licenceDate = licenceDate;
            this._isUse = isUse;
            this._dispOrder = dispOrder;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
            this._ePEquipType = ePEquipType;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "代码", ShortCode = "UseCode", Desc = "代码", ContextType = SysDic.All, Length = 50)]
        public virtual string UseCode
        {
            get { return _useCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for UseCode", value, value.ToString());
                _useCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "标准代码", ShortCode = "StandCode", Desc = "标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string StandCode
        {
            get { return _standCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for StandCode", value, value.ToString());
                _standCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "开发商标准代码", ShortCode = "DeveCode", Desc = "开发商标准代码", ContextType = SysDic.All, Length = 50)]
        public virtual string DeveCode
        {
            get { return _deveCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for DeveCode", value, value.ToString());
                _deveCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "仪器通讯标识", ShortCode = "EquipChannel", Desc = "仪器通讯标识", ContextType = SysDic.All, Length = 50)]
        public virtual string EquipChannel
        {
            get { return _equipChannel; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EquipChannel", value, value.ToString());
                _equipChannel = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "CName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string CName
        {
            get { return _cName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
                _cName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "英文名称", ShortCode = "EName", Desc = "英文名称", ContextType = SysDic.All, Length = 50)]
        public virtual string EName
        {
            get { return _eName; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for EName", value, value.ToString());
                _eName = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "SName", Desc = "简称", ContextType = SysDic.All, Length = 50)]
        public virtual string SName
        {
            get { return _sName; }
            set
            {
                if (value != null && value.Length > 50)
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
                if (value != null && value.Length > 20)
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
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for PinYinZiTou", value, value.ToString());
                _pinYinZiTou = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "描述", ShortCode = "Comment", Desc = "描述", ContextType = SysDic.All, Length = 16)]
        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "对应检验小组", ShortCode = "GroupID", Desc = "对应检验小组", ContextType = SysDic.All, Length = 4)]
        public virtual long GroupID
        {
            get { return _groupID; }
            set { _groupID = value; }
        }

        [DataMember]
        [DataDesc(CName = "计算机", ShortCode = "Computer", Desc = "计算机", ContextType = SysDic.All, Length = 20)]
        public virtual string Computer
        {
            get { return _computer; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Computer", value, value.ToString());
                _computer = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "程序名", ShortCode = "ComProgram", Desc = "程序名", ContextType = SysDic.All, Length = 20)]
        public virtual string ComProgram
        {
            get { return _comProgram; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for ComProgram", value, value.ToString());
                _comProgram = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "是否双向通讯", ShortCode = "DoubleDir", Desc = "是否双向通讯", ContextType = SysDic.All, Length = 4)]
        public virtual EPBEquipDoubleDir DoubleDir
        {
            get { return _doubleDir; }
            set { _doubleDir = value; }
        }

        //[DataMember]
        //[DataDesc(CName = "仪器类型", ShortCode = "EquipType", Desc = "仪器类型", ContextType = SysDic.All, Length = 4)]
        //public virtual EPBEquipType EquipType
        //{
        //    get { return _equipType; }
        //    set { _equipType = value; }
        //}
        [DataMember]
        [DataDesc(CName = "仪器类型", ShortCode = "EPEquipType", Desc = "")]
        public virtual EPEquipType EPEquipType
        {
            get { return _ePEquipType; }
            set { _ePEquipType = value; }
        }

        [DataMember]
        [DataDesc(CName = "授权码", ShortCode = "LicenceKey", Desc = "授权码", ContextType = SysDic.All, Length = 30)]
        public virtual string LicenceKey
        {
            get { return _licenceKey; }
            set
            {
                if (value != null && value.Length > 30)
                    throw new ArgumentOutOfRangeException("Invalid value for LicenceKey", value, value.ToString());
                _licenceKey = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "授权类型", ShortCode = "LicenceType", Desc = "授权类型", ContextType = SysDic.All, Length = 25)]
        public virtual string LicenceType
        {
            get { return _licenceType; }
            set
            {
                if (value != null && value.Length > 25)
                    throw new ArgumentOutOfRangeException("Invalid value for LicenceType", value, value.ToString());
                _licenceType = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "授权号", ShortCode = "SQH", Desc = "授权号", ContextType = SysDic.All, Length = 4)]
        public virtual string SQH
        {
            get { return _sQH; }
            set
            {
                if (value != null && value.Length > 4)
                    throw new ArgumentOutOfRangeException("Invalid value for SQH", value, value.ToString());
                _sQH = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "授权用序号", ShortCode = "SNo", Desc = "授权用序号", ContextType = SysDic.All, Length = 4)]
        public virtual int SNo
        {
            get { return _sNo; }
            set { _sNo = value; }
        }

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "授权日期", ShortCode = "LicenceDate", Desc = "授权日期", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? LicenceDate
        {
            get { return _licenceDate; }
            set { _licenceDate = value; }
        }

        [DataMember]
        [DataDesc(CName = "是否使用", ShortCode = "IsUse", Desc = "是否使用", ContextType = SysDic.All, Length = 1)]
        public virtual bool IsUse
        {
            get { return _isUse; }
            set { _isUse = value; }
        }

        [DataMember]
        [DataDesc(CName = "显示次序", ShortCode = "DispOrder", Desc = "显示次序", ContextType = SysDic.All, Length = 4)]
        public virtual int DispOrder
        {
            get { return _dispOrder; }
            set { _dispOrder = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器结果类型:0-生免类,1-微生物类", ShortCode = "EquipResultType", Desc = "仪器结果类型:0-生免类,1-微生物类", ContextType = SysDic.All, Length = 4)]
        public virtual int EquipResultType
        {
            get { return _equipResultType; }
            set { _equipResultType = value; }
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
        [DataDesc(CName = "仪器模块", ShortCode = "EPModuleList", Desc = "仪器模块")]
        public virtual IList<EPModule> EPModuleList
        {
            get
            {
                if (_ePModuleList == null)
                {
                    _ePModuleList = new List<EPModule>();
                }
                return _ePModuleList;
            }
            set { _ePModuleList = value; }
        }

        [DataMember]
        [DataDesc(CName = "小组仪器", ShortCode = "GMGroupEquipList", Desc = "小组仪器")]
        public virtual IList<GMGroupEquip> GMGroupEquipList
        {
            get
            {
                if (_gMGroupEquipList == null)
                {
                    _gMGroupEquipList = new List<GMGroupEquip>();
                }
                return _gMGroupEquipList;
            }
            set { _gMGroupEquipList = value; }
        }

        [DataMember]
        [DataDesc(CName = "仪器项目关系表", ShortCode = "EPEquipItemList", Desc = "仪器项目关系表")]
        public virtual IList<EPEquipItem> EPEquipItemList
        {
            get
            {
                if (_ePEquipItemList == null)
                {
                    _ePEquipItemList = new List<EPEquipItem>();
                }
                return _ePEquipItemList;
            }
            set { _ePEquipItemList = value; }
        }

        #endregion
    }
    #endregion
}
