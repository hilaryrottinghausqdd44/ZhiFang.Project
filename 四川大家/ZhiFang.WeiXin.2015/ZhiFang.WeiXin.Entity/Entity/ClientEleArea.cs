using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.Entity
{
    #region ClientEleArea

    /// <summary>
    /// ClientEleArea object for NHibernate mapped table 'ClientEleArea'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "区域字典", ClassCName = "ClientEleArea", ShortCode = "ClientEleArea", Desc = "区域字典")]
	public class ClientEleArea : BaseEntity
	{
		#region Member Variables
        protected string _areaCName;
        protected string _areaShortName;
        protected int _clientNo;
       // protected DateTime? _addTime;

        #endregion

        #region Constructors

        public ClientEleArea() { }

		public ClientEleArea(string areaCName, string areaShortName, int clientNo,DateTime addTime, byte[] dataTimeStamp)
		{
            this._areaCName = areaCName;
            this._areaShortName = areaShortName;
            this._clientNo = clientNo;
          //  this._addTime = addTime;
         //   this._dataTimeStamp = dataTimeStamp;
		}

		#endregion

		#region Public Properties

        [DataMember]
        [DataDesc(CName = "名称", ShortCode = "AreaCName", Desc = "名称", ContextType = SysDic.All, Length = 50)]
        public virtual string AreaCName
        {
			get { return _areaCName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AreaCName", value, value.ToString());
                _areaCName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "简称", ShortCode = "AreaShortName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string AreaShortName
        {
			get { return _areaShortName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AreaShortName", value, value.ToString());
                _areaShortName = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "ClientNo", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int ClientNo
        {
            get { return _clientNo; }
            set { _clientNo = value; }
        }

        //[DataMember]
        //[JsonConverter(typeof(JsonConvertClass))]
        //[DataDesc(CName = "", ShortCode = "AddTime", Desc = "", ContextType = SysDic.All, Length = 8)]
        //public virtual DateTime? AddTime
        //{
        //    get { return _addTime; }
        //    set { _addTime = value; }
        //}
        #endregion
    }
    #endregion
}