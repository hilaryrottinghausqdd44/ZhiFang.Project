using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBGetListByTimeStampe
    {
        DataSet GetListByTimeStampe(string LabCode, DateTime dTimeStampe);
    }
}
