using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL.Other
{
    public interface IDapplybase
    {
         DataSet GetApply(string strWhere);
    }
}
