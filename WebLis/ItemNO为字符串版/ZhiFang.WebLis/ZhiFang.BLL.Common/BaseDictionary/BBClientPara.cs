using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public class BBClientPara: IBLL.Common.BaseDictionary.IBBClientPara
    {
        IDAL.IDBClientPara dal;
        public BBClientPara()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDBClientPara>.GetDal("B_ClientPara", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDBClientPara>.GetDal("B_ClientPara", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }

        public int Add(B_ClientPara entity)
        {
            entity.ParameterID = GUIDHelp.GetGUIDLong();
           return dal.Add(entity);
        }

        public int Edit(B_ClientPara entity)
        {
            return dal.Update(entity);
        }

        public int Delete(long Id)
        {
            return dal.Delete(Id);
        }

        public int Delete(long ClientNo, string ParaCode)
        {
            return dal.Delete(ClientNo, ParaCode);
        }

        public B_ClientPara GetModel(long ClientNo, string ParaCode)
        {
            return dal.Search(ClientNo, ParaCode);
        }

        public DataSet SearchGroupByParaNo(string where)
        {
            return dal.SearchGroupByParaNo(where);
        }

        public List<B_ClientPara> SearchByParaNo(string paraNo)
        {
            return dal.SearchByParaNo(paraNo);
        }

        public List<B_ClientPara> SearchBBClientParaGroupByName(string name)
        {
            return dal.SearchBBClientParaGroupByName(name);
        }

        public List<B_ClientPara> SearchBBClientParaByParaNoAndLabIDAndLabName(string paraNo, string labID, string labName)
        {
            return dal.SearchBBClientParaByParaNoAndLabIDAndLabName(paraNo, labID, labName);
        }
    }
}
