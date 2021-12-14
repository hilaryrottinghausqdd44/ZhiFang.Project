using Newtonsoft.Json;
using System;
using System.Collections;
using System.Runtime.Serialization;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.RBAC
{
    #region BTDMacroCommand

    /// <summary>
    /// BTDMacroCommand object for NHibernate mapped table 'BT_D_MacroCommand'.
    /// </summary>
    [DataContract]
    [DataDesc(CName = "宏命令", ClassCName = "BTDMacroCommand", ShortCode = "BTDMacroCommand", Desc = "宏命令")]
    public class BTDMacroCommand : BaseEntity
    {
        #region Member Variables

        protected string _cName;
        protected string _eName;
        protected string _macroInfo;
        protected string _macroCode;
        protected string _classCode;
        protected string _typeName;
        protected string _typeCode;
        protected string _creator;
        protected string _modifier;
        protected string _pinYinZiTou;
        protected DateTime? _dataUpdateTime;
        protected ArrayList _Parameter;


        #endregion

        #region Constructors

        public BTDMacroCommand() { }

        public BTDMacroCommand(long labID, string cName, string eName, string macroInfo, string macroCode, string classCode, string creator, string modifier, string pinYinZiTou, DateTime dataAddTime, DateTime dataUpdateTime, byte[] dataTimeStamp)
        {
            this._labID = labID;
            this._cName = cName;
            this._eName = eName;
            this._macroInfo = macroInfo;
            this._macroCode = macroCode;
            this._classCode = classCode;
            this._creator = creator;
            this._modifier = modifier;
            this._pinYinZiTou = pinYinZiTou;
            this._dataAddTime = dataAddTime;
            this._dataUpdateTime = dataUpdateTime;
            this._dataTimeStamp = dataTimeStamp;
        }

        #endregion

        #region Public Properties


        [DataMember]
        [DataDesc(CName = "宏命令中文名", ShortCode = "CName", Desc = "宏命令中文名", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "宏命令英文名", ShortCode = "EName", Desc = "宏命令英文名", ContextType = SysDic.All, Length = 100)]
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
        [DataDesc(CName = "宏命令简介", ShortCode = "MacroInfo", Desc = "宏命令简介", ContextType = SysDic.All, Length = 16)]
        public virtual string MacroInfo
        {
            get { return _macroInfo; }
            set
            {
                _macroInfo = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "宏命令编码", ShortCode = "MacroCode", Desc = "宏命令编码", ContextType = SysDic.All, Length = 50)]
        public virtual string MacroCode
        {
            get { return _macroCode; }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for MacroCode", value, value.ToString());
                _macroCode = value;
            }
        }

        [DataMember]
        [DataDesc(CName = "类代码", ShortCode = "ClassCode", Desc = "类代码", ContextType = SysDic.All, Length = 50)]
        public virtual string ClassCode
        {
            get
            {
                switch (this.MacroCode)
                {
                    case "$TheDay:+0$":
                        if (Parameter != null && Parameter.Count > 0 && Parameter[0] != null && StringPlus.CheckIsNumber(Parameter[0].ToString()))
                        {
                            _classCode = "'" + DateTime.Today.AddDays(int.Parse(Parameter[0].ToString())).ToString("yyyy-MM-dd") + "'";
                        }
                        break;
                    case "$TheMonth:+0$":
                        if (Parameter != null && Parameter.Count > 0 && Parameter[0] != null && StringPlus.CheckIsNumber(Parameter[0].ToString()))
                        {
                            _classCode = "'" + DateTime.Today.AddMonths(int.Parse(Parameter[0].ToString())).ToString("yyyy-MM-dd").Substring(0, 7) + "-01'";
                        }
                        break;
                    case "$TheYear:+0$":
                        if (Parameter != null && Parameter.Count > 0 && Parameter[0] != null && StringPlus.CheckIsNumber(Parameter[0].ToString()))
                        {
                            _classCode = "'" + DateTime.Today.AddYears(int.Parse(Parameter[0].ToString())).Year + "-01-01'";
                        }
                        break;
                    case "$GetDay:P1$":
                        if (Parameter != null && Parameter.Count > 0 && Parameter[0] != null)
                        {
                            _classCode = "day(" + Parameter[0].ToString() + ")";
                        }
                        break;
                    case "$GetMonth:P1$":
                        if (Parameter != null && Parameter.Count > 0 && Parameter[0] != null)
                        {
                            _classCode = "month(" + Parameter[0].ToString() + ")";
                        }
                        break;
                    case "$GetYear:P1$":
                        if (Parameter != null && Parameter.Count > 0 && Parameter[0] != null)
                        {
                            _classCode = "year(" + Parameter[0].ToString() + ")";
                        }
                        break;
                    case "$AddDay:+0,P1$":
                        if (Parameter != null && Parameter.Count > 1 && Parameter[0] != null && Parameter[1] != null && StringPlus.CheckIsNumber(Parameter[0].ToString()))
                        {
                            _classCode = "" + Parameter[1].ToString() + "" + Parameter[0];
                        }
                        break;
                    case "$AddMonth:+0,P1$":
                        if (Parameter != null && Parameter.Count > 1 && Parameter[0] != null && Parameter[1] != null && StringPlus.CheckIsNumber(Parameter[0].ToString()))
                        {
                            _classCode = "add_months(" + Parameter[1].ToString() + "," + Parameter[0] + ")";
                        }
                        break;
                    default: break;
                }
                return _classCode;
            }
            set
            {
                if (value != null && value.Length > 50)
                    throw new ArgumentOutOfRangeException("Invalid value for ClassCode", value, value.ToString());
                _classCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "宏命令类型名", ShortCode = "TypeName", Desc = "宏命令类型名", ContextType = SysDic.All, Length = 100)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "宏命令编码", ShortCode = "TypeCode", Desc = "宏命令编码", ContextType = SysDic.All, Length = 100)]
        public virtual string TypeCode
        {
            get { return _typeCode; }
            set
            {
                _typeCode = value;
            }
        }
        [DataMember]
        [DataDesc(CName = "创建者", ShortCode = "Creator", Desc = "创建者", ContextType = SysDic.All, Length = 20)]
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
        [DataDesc(CName = "修改者", ShortCode = "Modifier", Desc = "修改者", ContextType = SysDic.All, Length = 20)]
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
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "数据更新时间", ShortCode = "DataUpdateTime", Desc = "数据更新时间", ContextType = SysDic.All, Length = 8)]
        public virtual DateTime? DataUpdateTime
        {
            get { return _dataUpdateTime; }
            set { _dataUpdateTime = value; }
        }
        public virtual ArrayList Parameter
        {
            get { return _Parameter; }
            set { _Parameter = value; }
        }

        #endregion
    }
    #endregion
}