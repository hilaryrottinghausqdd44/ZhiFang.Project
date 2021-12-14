using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.ReportFormQueryPrint.Model.VO
{
    public class webconfigVo
    {
        public webconfigVo()
        { }
        #region Model
        private string _key;
        private string _value;
        /// <summary>
        /// 
        /// </summary>
        public string key
        {
            set { _key = value; }
            get { return _key; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string value
        {
            set { _value = value; }
            get { return _value; }
        }
        #endregion Model
    }
}