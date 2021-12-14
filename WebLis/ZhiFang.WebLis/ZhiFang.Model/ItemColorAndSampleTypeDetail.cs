using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    [Serializable]
    public partial class ItemColorAndSampleTypeDetail
    {
        public ItemColorAndSampleTypeDetail()
        { }

        public string ColorID { get; set; }
        public string ColorName { get; set; }
        public string SampleTypeNo { get; set; }
        public string SampleTypeName { get; set; }

    }
}
