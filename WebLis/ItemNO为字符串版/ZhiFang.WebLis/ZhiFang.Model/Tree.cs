using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Model
{
    
    public class TreeLeaf
    {
        #region extjs
        public virtual string text { get; set; }
         
        public virtual bool expanded { get; set; }
         
        public virtual bool leaf { get; set; }
         
        public virtual string icon { get; set; }
         
        public virtual string iconCls { get; set; }
         
        public virtual string url { get; set; }
         
        public virtual string tid { get; set; }
         
        public virtual string pid { get; set; }
         
        public virtual string objectType { get; set; }
         
        public virtual object value { get; set; }
        #endregion
        #region EasyUI
        public virtual string id { get; set; }

        public virtual string state { get; set; }        

        public virtual string @checked { get; set; }

        public virtual string Target { get; set; }

        public virtual string attributes { get; set; }
        #endregion

    }

    public class TreeLeaf<T>
    {
        #region extjs
        public virtual string text { get; set; }
         
        public virtual bool expanded { get; set; }
         
        public virtual bool leaf { get; set; }
         
        public virtual string icon { get; set; }
         
        public virtual string iconCls { get; set; }
         
        public virtual string url { get; set; }
         
        public virtual string tid { get; set; }
         
        public virtual string pid { get; set; }
         
        public virtual string objectType { get; set; }
         
        public virtual T value { get; set; }
        #endregion
        #region EasyUI
        public virtual string id { get; set; }

        public virtual string state { get; set; }

        public virtual string @checked { get; set; }

        public virtual string Target { get; set; }

        public virtual string attributes { get; set; }
        #endregion
    }

    public class tree : TreeLeaf
    {
        #region extjs
        public virtual List<tree> Tree { get; set; }
        #endregion
        #region EasyUI
        public virtual List<tree> children { get; set; }
        #endregion
    }

    public class tree<T> : TreeLeaf<T>
    {
        #region extjs
        public virtual tree<T>[] Tree { get; set; }
        #endregion
        #region EasyUI
        public virtual tree<T>[] children { get; set; }
        #endregion
    }
}
