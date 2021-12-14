using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    public class DownloadDictEntity<T>
    {
        public virtual int Total { get; set; }
        public virtual IList<T> DictEntity { get; set; }
        /// <summary>
		/// 客户端当次同步字典里最大的时间戳值
        /// </summary>
        private string maxDataTimeStamp;
        //[DataMember]
        public virtual string MaxDataTimeStamp
        {
            get { return maxDataTimeStamp; }
            set { maxDataTimeStamp = value; }
        }
    }
}
