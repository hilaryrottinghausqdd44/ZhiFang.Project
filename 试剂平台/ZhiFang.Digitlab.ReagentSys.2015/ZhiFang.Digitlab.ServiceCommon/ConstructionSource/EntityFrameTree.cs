using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.Digitlab.ServiceCommon
{
    public class EntityFrameTree
    {
        public string text { get; set; }

        public string InteractionField { get; set; }

        public bool leaf { get; set; }

        public string icon { get; set; }

        public List<EntityFrameTree> Tree { get; set; }

        public string tid { get; set; }

        //public bool checked { get; set; }

        public string FieldClass { get; set; }
    }
}