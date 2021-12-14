using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.DAL.RBAC
{
    public class BaseDALLisDB 
    {
        public DBUtility.IDBConnection DbHelperSQL = DBUtility.DBFactory.CreateDB("RMSDB");
       
    }
}
