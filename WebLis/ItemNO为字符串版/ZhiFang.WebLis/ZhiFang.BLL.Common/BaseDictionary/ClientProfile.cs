using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IBLL.Common;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class ClientProfile : IBClientProfile, IBSynchData
    {
        IDAL.IDClientProfile dal = DALFactory.DalFactory<IDAL.IDClientProfile>.GetDal("ClientProfile", ZhiFang.Common.Dictionary.DBSource.LisDB());
        public ClientProfile()
		{}
        public bool Exists(string ClIENTControlNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(string ClIENTControlNo)
        {
            throw new NotImplementedException();
        }

        public int DeleteList(string Idlist)
        {
            throw new NotImplementedException();
        }

        public Model.ClientProfile GetModel(string ClientProfile)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.ClientProfile model)
        {
            throw new NotImplementedException();
        }

        public int Add(Model.ClientProfile model)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.ClientProfile model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetList(Model.ClientProfile model)
        {
            return dal.GetList(model);
        }

        public System.Data.DataSet GetAllList()
        {
            return dal.GetAllList();
        }

        public int AddUpdateByDataSet(System.Data.DataSet ds)
        {
            throw new NotImplementedException();
        }

        public bool Exists(System.Collections.Hashtable ht)
        {
            throw new NotImplementedException();
        }

        public int AddByDataRow(System.Data.DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int UpdateByDataRow(System.Data.DataRow dr)
        {
            throw new NotImplementedException();
        }


        public System.Data.DataSet GetClientNo()
        {
            return dal.GetClientNo();
        }


        public bool Add(List<Model.ClientProfile> l)
        {
            return dal.Add(l);
        }


        public bool AddList(List<Model.ClientProfile> l)
        {
            return dal.AddList(l);
        }
    }
}
