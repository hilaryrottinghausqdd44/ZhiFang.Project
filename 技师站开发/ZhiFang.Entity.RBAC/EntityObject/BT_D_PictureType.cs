using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region BTDPictureType

    /// <summary>
    /// BTDPictureType object for NHibernate mapped table 'BT_D_PictureType'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "图片类型", ClassCName = "", ShortCode = "TPLX", Desc = "图片类型")]
    public class BTDPictureType : BaseEntityService
    {
        #region Member Variables


        protected string _cname;
        protected string _pinYinZiTou;
        protected string _creator;
        protected string _modifier;
        protected DateTime? _dataUpdateTime;
        protected IList<BTDPictureTypeCon> _bTDPictureTypeCons;

        #endregion

        #region Constructors

        public BTDPictureType() { }

        public BTDPictureType(long labID, string cname, string pinYinZiTou, string creator, string modifier, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cname = cname;
            this._pinYinZiTou = pinYinZiTou;
            this._creator = creator;
            this._modifier = modifier;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "MC", Desc = "名称", ContextType = SysDic.NText, Length = 50)]
        public virtual string Cname
        {
            get { return _cname; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for Cname", value, value.ToString());
                _cname = value;
            }
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
        [DataDesc(CName = "创建者", ShortCode = "CJZ", Desc = "创建者", ContextType = SysDic.NText, Length = 20)]
        public virtual string Creator
        {
            get { return _creator; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Creator", value, value.ToString());
                _creator = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "修改者", ShortCode = "XGZ", Desc = "修改者", ContextType = SysDic.NText, Length = 20)]
        public virtual string Modifier
        {
            get { return _modifier; }
            set
            {
                if (value != null && value.Length > 20)
                    throw new ArgumentOutOfRangeException("Invalid value for Modifier", value, value.ToString());
                _modifier = value;
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
        [DataDesc(CName = "图片类型关联列表", ShortCode = "TPLXGLLB", Desc = "图片类型关联列表", ContextType = SysDic.List)]
        public virtual IList<BTDPictureTypeCon> BTDPictureTypeConsList
        {
            get
            {
                if (_bTDPictureTypeCons == null)
                {
                    _bTDPictureTypeCons = new List<BTDPictureTypeCon>();
                }
                return _bTDPictureTypeCons;
            }
            set { _bTDPictureTypeCons = value; }
        }

        #endregion
    }
    #endregion
}