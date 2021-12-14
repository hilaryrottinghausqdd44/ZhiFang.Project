using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 实体类District 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class District
    {
        public District()
        { }
        #region Model
        private long _districtno;
        private string _cname;
        private string _shortname;
        private string _shortcode;
        private int? _visible;
        private int? _disporder;
        private string _hisordercode;
        /// <summary>
        /// 
        /// </summary>
        public long DistrictNo
        {
            set { _districtno = value; }
            get { return _districtno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CName
        {
            set { _cname = value; }
            get { return _cname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortName
        {
            set { _shortname = value; }
            get { return _shortname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortCode
        {
            set { _shortcode = value; }
            get { return _shortcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Visible
        {
            set { _visible = value; }
            get { return _visible; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? DispOrder
        {
            set { _disporder = value; }
            get { return _disporder; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HisOrderCode
        {
            set { _hisordercode = value; }
            get { return _hisordercode; }
        }
        #endregion Model

    }
}
