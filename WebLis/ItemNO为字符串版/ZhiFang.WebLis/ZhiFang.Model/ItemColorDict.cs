using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    //[Serializable]
    public partial class ItemColorDict
    {
        public ItemColorDict()
        { }
        #region Model
        private int _colorid;
        private string _colorname;
        private string _colorvalue;
        /// <summary>
        /// 
        /// </summary>
        public int ColorID
        {
            set { _colorid = value; }
            get { return _colorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ColorName
        {
            set { _colorname = value; }
            get { return _colorname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ColorValue
        {
            set { _colorvalue = value; }
            get { return _colorvalue; }
        }
        /// <summary>
		/// DataTimeStamp
        /// </summary>                      
		private DateTime? _DataTimeStamp;
        //[DataMember]
        public DateTime? DataTimeStamp
        {
            get { return _DataTimeStamp; }
            set { _DataTimeStamp = value; }
        }

        #endregion Model
    }
}
