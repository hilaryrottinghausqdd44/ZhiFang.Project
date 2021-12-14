using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;

namespace ZhiFang.BLL.Report
{
    public class Dictionary : ZhiFang.IBLL.Report.Dictionary
    {
        #region Dictionary 成员

        public System.Data.DataSet Department()
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBDepartment bd = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBDepartment>.GetBLL("Department");
            return bd.GetAllList();
        }

        public System.Data.DataSet Doctor()
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBDoctor bd = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBDoctor>.GetBLL("Doctor");
            return bd.GetAllList();
        }

        public System.Data.DataSet District()
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBDistrict bd = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBDistrict>.GetBLL("District");
            return bd.GetAllList();
        }

        public System.Data.DataSet SickType()
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBSickType bd = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBSickType>.GetBLL();
            return bd.GetAllList();
        }

        #endregion

        #region Dictionary 成员


        public DataSet Client()
        {
            ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE bc = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL();
            return bc.GetAllList();
        }

        #endregion
    }
}
