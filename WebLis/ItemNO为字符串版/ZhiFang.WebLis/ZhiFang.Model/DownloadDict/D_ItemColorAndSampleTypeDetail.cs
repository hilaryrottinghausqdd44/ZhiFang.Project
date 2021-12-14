using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model.DownloadDict
{
    [Serializable]
    public class D_ItemColorAndSampleTypeDetail
    {
        public D_ItemColorAndSampleTypeDetail()
        { }
        public string Id { get; set; }
        public string ColorID { get; set; }
        public string ColorValue { get; set; }
        public string ColorName { get; set; }
        public string SampleTypeNo { get; set; }
        public string SampleTypeName { get; set; }
    }
}
