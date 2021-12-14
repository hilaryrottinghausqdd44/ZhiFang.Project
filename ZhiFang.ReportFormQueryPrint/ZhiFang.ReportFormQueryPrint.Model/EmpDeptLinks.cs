using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 实体类EmpDeptLinks 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
 
	public class EmpDeptLinks
	{
		public EmpDeptLinks()
		{}
		#region Model
		private long  _edlid;
		private long _userno;
		private long _deptno;
		private string _usercname;
		private string _shortcode;
		private string _deptcname;
		private DateTime? _dataaddtime;
		private DateTime? _dataupdatetime;
		

        
        public long EDLID
        {
			set{ _edlid = value;}
			get{return _edlid; }
		}

      
        public long UserNo
        {
			set{ _userno = value;}
			get{return _userno; }
		}

        
        public long DeptNo
        {
			set{ _deptno = value;}
			get{return _deptno; }
		}

        
        public string UserCName
        {
			set{ _usercname = value;}
			get{return _usercname; }
		}

        
        public string ShortCode
        {
			set{ _shortcode = value;}
			get{return _shortcode; }
		}

        
        public string DeptCName
        {
			set{ _deptcname = value;}
			get{return _deptcname; }
		}

        
        public DateTime? DataAddTime
        {
			set{ _dataaddtime = value;}
			get{return _dataaddtime; }
		}

        
        public DateTime? DataUpdateTime
        {
			set{ _dataupdatetime = value;}
			get{return _dataupdatetime; }
		}
		#endregion Model

	}
}

