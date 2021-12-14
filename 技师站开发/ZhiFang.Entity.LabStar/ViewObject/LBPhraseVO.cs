using Newtonsoft.Json;
using System.Runtime.Serialization;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.LabStar
{
    #region LBPhraseVO

    [DataContract]
    [DataDesc(CName = "", ClassCName = "LBPhraseVO", ShortCode = "LBPhraseVO", Desc = "")]
    public class LBPhraseVO
    {
        #region Member Variables

        protected string _phraseType;
        protected string _typeName;
        protected string _typeCode;
        protected string _cName;
        protected string _shortcode;
        protected string _pinYinZiTou;



        #endregion

        #region Constructors

        public LBPhraseVO() { }

        public LBPhraseVO(string phraseType, string typeName, string typeCode, string cName, string shortcode, string pinYinZiTou)
        {
            this._phraseType = phraseType;
            this._typeName = typeName;
            this._typeCode = typeCode;
            this._cName = cName;
            this._shortcode = shortcode;
            this._pinYinZiTou = pinYinZiTou;
        }

        #endregion

        #region Public Properties

        [DataMember]
        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "主键ID", ShortCode = "Id", Desc = "主键ID", ContextType = SysDic.Number, Length = 8)]
        public virtual long Id { get; set; }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PhraseType", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PhraseType
        {
            get { return _phraseType; }
            set { _phraseType = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeName", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "TypeCode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string TypeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "CName", Desc = "", ContextType = SysDic.All, Length = 500)]
        public virtual string CName
        {
            get { return _cName; }
            set { _cName = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Shortcode
        {
            get { return _shortcode; }
            set { _shortcode = value; }
        }

        [DataMember]
        [DataDesc(CName = "", ShortCode = "PinYinZiTou", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string PinYinZiTou
        {
            get { return _pinYinZiTou; }
            set { _pinYinZiTou = value; }
        }

        #endregion
    }
    #endregion
}