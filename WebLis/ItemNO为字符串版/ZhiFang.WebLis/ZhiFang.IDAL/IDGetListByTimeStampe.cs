using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDGetListByTimeStampe
    {
        DataSet GetListByTimeStampe(string LabCode, int dTimeStampe);
    }
}
