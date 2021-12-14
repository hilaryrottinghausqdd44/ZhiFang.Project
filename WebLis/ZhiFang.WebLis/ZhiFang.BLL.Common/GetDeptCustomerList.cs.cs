using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Common;
using System.Data;

namespace ZhiFang.BLL.Common
{
    public class GetDeptCustomerList:IBGetLabCustomerList
    {
        #region IGetDeptCustomerList 成员

        /// <summary>
        /// 获取本部门下的所有客户的列表
        /// </summary>
        public DataSet GetDeptCustomerLst(string strDeptID)
        {
            return new DataSet();
        }

        #endregion
    }
}
