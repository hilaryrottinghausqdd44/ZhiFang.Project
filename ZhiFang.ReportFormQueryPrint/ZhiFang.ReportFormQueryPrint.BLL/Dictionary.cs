using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BDictionary 
    {
        #region Dictionary 成员

        public static System.Data.DataSet Department()
        {
            BLL.BDepartment bd = new BDepartment();
            return bd.GetAllList();
        }

        public static System.Data.DataSet Doctor()
        {
            BDoctor bd = new BDoctor();
            return bd.GetAllList();
        }

        public static System.Data.DataSet District()
        {
            BDistrict bd = new BDistrict();
            return bd.GetAllList();
        }

        public static System.Data.DataSet SickType()
        {
            BSickType bs = new BSickType();
            return bs.GetAllList();
        }
        #endregion
    }
}
