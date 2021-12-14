using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_ItemColorDict
    {
        public D_ItemColorDict()
        { }
        [DataMember]
        public string ColorID { get; set; }
        [DataMember]
        public string ColorValue { get; set; }
        [DataMember]
        public string ColorName { get; set; }
        ///// <summary>
        ///// 最大时间戳(字典同步时暂时不需要)
        ///// </summary>                      
        //private string _maxdataTimeStamp;
        //[DataMember]
        //public string MaxDataTimeStamp
        //{
        //    get { return _maxdataTimeStamp; }
        //    set { _maxdataTimeStamp = value; }
        //}
    }
}
