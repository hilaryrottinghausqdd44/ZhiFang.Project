using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBGetLabCustomerList
    {
        /// <summary>
        /// 获取本部门下的所有客户的列表
        /// </summary>
        DataSet GetDeptCustomerLst(string strDeptID);
    }
}
