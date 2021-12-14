using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    [Serializable]
    public partial class barCodeSeq
    {
        public barCodeSeq() { }

        public string LabCode { get; set; }
        public int LastNum { get; set; }
        public DateTime Date { get; set; }
    }
}
