using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
	#region BSelectUnit

	public class BSearchUnit
    {
        #region Member Variables
        protected long _SID;
        protected string _cName;
        protected string _selectName;
        protected string _type;
        protected int _textWidth;
        protected int _width;
        protected DateTime? _dataUpdateTime;
        protected string _xtype;
        protected string _mark;
        protected string _llisteners;
        protected string _jsCode;
       

        #endregion



        #region Public Properties


        public virtual string CName
		{
			get { return _cName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for CName", value, value.ToString());
				_cName = value;
			}
		}

       public virtual string SelectName
		{
			get { return _selectName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for SelectName", value, value.ToString());
				_selectName = value;
			}
		}

        public virtual string Type
		{
			get { return _type; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Type", value, value.ToString());
				_type = value;
			}
		}

        public virtual int TextWidth
		{
			get { return _textWidth; }
			set { _textWidth = value; }
		}

         public virtual int Width
		{
			get { return _width; }
			set { _width = value; }
		}

         public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

         public virtual string Xtype
		{
			get { return _xtype; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Xtype", value, value.ToString());
				_xtype = value;
			}
		}

        public virtual string Mark
		{
			get { return _mark; }
			set
			{
				if ( value != null && value.Length > 10)
					throw new ArgumentOutOfRangeException("Invalid value for Mark", value, value.ToString());
				_mark = value;
			}
		}

         public virtual string Listeners
		{
			get { return _llisteners; }
			set
			{
				_llisteners = value;
			}
		}

         public virtual string JsCode
		{
			get { return _jsCode; }
			set
			{
				_jsCode = value;
			}
		}

        public virtual long SID
        {
            get { return _SID; }
            set
            {_SID = value;
            }
        }
      
        #endregion
    }
	#endregion
}