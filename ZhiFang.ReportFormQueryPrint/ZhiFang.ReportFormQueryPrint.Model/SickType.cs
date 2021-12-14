using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 实体类SickType 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SickType
    {
        public SickType()
        { }
        #region Model
        private long _sicktypeno;
        private string _cname;
        private string _shortcode;
        private int? _disporder;
        private string _hisordercode;
        
        /// <summary>
        /// 
        /// </summary>
        public long SickTypeNo
        {
            set { _sicktypeno = value; }
            get { return _sicktypeno; }
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
        public string ShortCode
        {
            set { _shortcode = value; }
            get { return _shortcode; }
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
