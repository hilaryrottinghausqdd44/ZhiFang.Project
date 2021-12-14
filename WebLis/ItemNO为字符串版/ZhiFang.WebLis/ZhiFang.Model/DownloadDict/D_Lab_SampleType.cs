using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [DataContract]
    public class D_Lab_SampleType : DownloadDictBase
    {
        public D_Lab_SampleType()
        { }
        [DataMember]
        public string SampleTypeID { get; set; }
        [DataMember]
        public string LabSampleTypeNo { get; set; }

        //      /// <summary>
        ///// HisOrderCode
        //      /// </summary>
        //      private string _hisordercode;
        //      [DataMember]
        //      public string HisOrderCode
        //      {
        //          get { return _hisordercode; }
        //          set { _hisordercode = value; }
        //      }
    }
}
