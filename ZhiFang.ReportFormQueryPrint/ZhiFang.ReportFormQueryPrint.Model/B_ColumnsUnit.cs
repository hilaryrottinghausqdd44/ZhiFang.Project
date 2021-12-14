using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.ReportFormQueryPrint.Model
{
	#region BColumnsUnit

	public class BColumnsUnit 
	{
        #region Member Variables
        protected long _ColID;
        protected string _cName;
        protected string _columnName;
        protected string _type;
        protected int _width;
        protected DateTime? _dataUpdateTime;
        protected string _render;
		

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

        public virtual string Render
		{
			get { return _render; }
			set
			{_render = value;
			}
		}
        public virtual long ColID
        {
            get { return _ColID; }
            set
            {_ColID = value;
            }
        }
       
        #endregion
    }
	#endregion
}