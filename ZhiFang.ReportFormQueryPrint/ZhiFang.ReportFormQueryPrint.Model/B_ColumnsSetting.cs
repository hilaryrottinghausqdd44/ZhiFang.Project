using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
	#region BColumnsSetting

	public class BColumnsSetting 
	{
        #region Member Variables
        protected long _CTID; 
        protected string _cName;
        protected string _showName;
        protected int _width;
        protected int _orderNo;
        protected bool _OrderFlag;
        protected int _OrderDesc;
        protected string _OrderMode;
        protected string _site;
        protected string _appType;
        protected DateTime? _dataAddTime;
        protected DateTime? _dataUpdateTime;
        protected bool _isShow;
        protected long _colID;
        protected string _columnName;       
        protected bool _isUse;
        protected string _render;


        #endregion


        #region Public Properties

        public virtual bool OrderFlag
        {
            get { return _OrderFlag; }
            set
            {
                _OrderFlag = value;
            }
        }
        public virtual int OrderDesc
        {
            get { return _OrderDesc; }
            set
            {
                _OrderDesc = value;
            }
        }

        public virtual string OrderMode
        {
            get { return _OrderMode; }
            set
            {
                _OrderMode = value;
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

       public virtual int Width
		{
			get { return _width; }
			set { _width = value; }
		}

         public virtual int OrderNo
		{
			get { return _orderNo; }
			set { _orderNo = value; }
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

        public virtual DateTime? DataAddTime
        {
            get { return _dataAddTime; }
            set { _dataAddTime = value; }
        }
        
        public virtual DateTime? DataUpdateTime
		{
			get { return _dataUpdateTime; }
			set { _dataUpdateTime = value; }
		}

       public virtual bool IsShow
		{
			get { return _isShow; }
			set { _isShow = value; }
		}

         public virtual long ColID
		{
			get { return _colID; }
			set { _colID = value; }
		}
        public virtual long CTID
        {
            get { return _CTID; }
            set { _CTID = value; }
        }
        
        public virtual string ColumnName
		{
			get { return _columnName; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for ColumnName", value, value.ToString());
				_columnName = value;
			}
		}

        public virtual bool IsUse
        {
            get { return _isUse; }
            set
            {
                _isUse = value;
            }
        }
        public virtual string Render
        {
            get { return _render; }
            set
            {
                _render = value;
            }
        }

        #endregion
    }
	#endregion
}