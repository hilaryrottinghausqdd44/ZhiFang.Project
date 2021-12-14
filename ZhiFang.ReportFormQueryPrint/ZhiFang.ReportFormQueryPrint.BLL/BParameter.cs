using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.IDAL;
using System.Data;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BBParameter
    {
        private readonly IDBParameter dal = DalFactory<IDBParameter>.GetDal("BParameter");
        public int Add(Model.BParameter t)
        {
            return dal.Add(t);
        }

        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        public DataSet GetList(Model.BParameter t)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            throw new NotImplementedException();
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }
        bool Exists(string strWhere)
        {
            return dal.Exists(strWhere);
        }
        public int Update(Model.BParameter t)
        {
            int flag = 0;
            DataSet ds = GetList("SName='" + t.SName + "' and ParaNo='" + t.ParaNo + "'");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                t.ParameterID = long.Parse(ds.Tables[0].Rows[0]["ParameterID"].ToString());
                flag = dal.Update(t);
            }
            else
            {
                flag = dal.Add(t);
            }

            return flag;
        }

        public int deleteBySName(string appType) {
            return dal.deleteBySName(appType);
        }

        public DataSet GetSeniorPublicSetting(string SName, string ParaNo) {

            return dal.GetSeniorPublicSetting(SName, ParaNo);
        }
    }
}

