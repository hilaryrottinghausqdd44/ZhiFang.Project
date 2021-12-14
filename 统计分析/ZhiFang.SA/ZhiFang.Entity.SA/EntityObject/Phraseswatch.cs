using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.SA
{
	#region Phraseswatch

	/// <summary>
	/// Phraseswatch object for NHibernate mapped table 'Phraseswatch'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "", ClassCName = "Phraseswatch", ShortCode = "Phraseswatch", Desc = "")]
	public class Phraseswatch : BaseEntity
	{
		#region Member Variables
		
        protected string _cname;
        protected string _shortname;
        protected string _shortcode;
        protected int _visible;
        protected int _disporder;
        protected string _hisordercode;
        protected long _pjype;
		

		#endregion

		#region Constructors

		public Phraseswatch() { }

		public Phraseswatch( string cname, string shortname, string shortcode, int visible, int disporder, string hisordercode, long pjype )
		{
			this._cname = cname;
			this._shortname = shortname;
			this._shortcode = shortcode;
			this._visible = visible;
			this._disporder = disporder;
			this._hisordercode = hisordercode;
			this._pjype = pjype;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "", ShortCode = "Cname", Desc = "", ContextType = SysDic.All, Length = 50)]
        public virtual string Cname
		{
			get { return _cname; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Cname", value, value.ToString());
				_cname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortname", Desc = "", ContextType = SysDic.All, Length = 40)]
        public virtual string Shortname
		{
			get { return _shortname; }
			set
			{
				if ( value != null && value.Length > 40)
					throw new ArgumentOutOfRangeException("Invalid value for Shortname", value, value.ToString());
				_shortname = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Shortcode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Shortcode
		{
			get { return _shortcode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Shortcode", value, value.ToString());
				_shortcode = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Visible", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Disporder", Desc = "", ContextType = SysDic.All, Length = 4)]
        public virtual int Disporder
		{
			get { return _disporder; }
			set { _disporder = value; }
		}

        [DataMember]
        [DataDesc(CName = "", ShortCode = "Hisordercode", Desc = "", ContextType = SysDic.All, Length = 20)]
        public virtual string Hisordercode
		{
			get { return _hisordercode; }
			set
			{
				if ( value != null && value.Length > 20)
					throw new ArgumentOutOfRangeException("Invalid value for Hisordercode", value, value.ToString());
				_hisordercode = value;
			}
		}

        [JsonConverter(typeof(JsonConvertClass))]
        [DataDesc(CName = "Pjype", ShortCode = "Pjype", Desc = "Pjype", ContextType = SysDic.Number, Length = 8)]
        public virtual long Pjype
		{
			get { return _pjype; }
			set { _pjype = value; }
		}

		
		#endregion
	}
	#endregion
}