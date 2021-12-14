using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Digitlab.Entity
{
   public class DataDescAttribute : Attribute
    {
       public virtual string CName { get; set; }
       public virtual string ClassCName { get; set; }
       public virtual string ShortCode { get; set; }
       public virtual string Desc { get; set; }
       public virtual SysDic ContextType { get; set; }
       public virtual int Length { get; set; }
    }
}
