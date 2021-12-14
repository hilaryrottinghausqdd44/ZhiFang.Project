using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
	#region BSelectSetting
    public class BSearchSetting
    {
        #region Member Variables
        protected long _STID;
        protected string _jsCode;
        protected string _type;
        protected string _selectName;
        protected string _xtype;
        protected string _mark;
        protected string _listeners;
        protected string _cName;
        protected string _showName;
        protected int _textWidth;
        protected int _width;
        protected int _showOrderNo;
        protected string _site;
        protected string _appType;
        protected DateTime? _DataAddTime; 
        protected DateTime? _dataUpdateTime;
        protected bool _isShow;
        protected long _sID;


        #endregion



        #region Public Properties

 public virtual string JsCode
		{
			get { return _jsCode; }
			set
			{_jsCode = value;
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
			get { return _listeners; }
			set
			{
				_listeners = value;
			}
		}

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

        public virtual string ShowName
		{
			get { return _showName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ShowName", value, value.ToString());
				_showName = value;
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

        public virtual int ShowOrderNo
		{
			get { return _showOrderNo; }
			set { _showOrderNo = value; }
		}
        

         public virtual string Site
		{
			get { return _site; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Site", value, value.ToString());
				_site = value;
			}
		}

        public virtual string AppType
		{
			get { return _appType; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for AppType", value, value.ToString());
				_appType = value;
			}
		}

       public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

        public virtual DateTime? DataAddTime
        {
            get { return _DataAddTime; }
            set { _DataAddTime = value; }
        }
        
        public virtual bool IsShow
		{
			get { return _isShow; }
			set { _isShow = value; }
		}

        public virtual long SID
		{
			get { return _sID; }
			set { _sID = value; }
		}
        public virtual long STID
        {
            get { return _STID; }
            set { _STID = value; }
        }
        

        #endregion
    }
	#endregion
}