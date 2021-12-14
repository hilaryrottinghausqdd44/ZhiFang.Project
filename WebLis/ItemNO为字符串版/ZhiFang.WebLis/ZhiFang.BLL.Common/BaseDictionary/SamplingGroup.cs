using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Data;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class SamplingGroup : IBSynchData, IBSamplingGroup
    {
        IDAL.IDSamplingGroup dal = DALFactory.DalFactory<IDAL.IDSamplingGroup>.GetDal("SamplingGroup", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public SamplingGroup()
		{}
        public bool Exists(string SampleTypeControlNo)
        {
            throw new NotImplementedException();
        }

        public int Delete(string SampleTypeControlNo)
        {
            throw new NotImplementedException();
        }

        public int DeleteList(string Idlist)
        {
            throw new NotImplementedException();
        }

        public Model.SamplingGroup GetModel(string SamplingGroupNo)
        {
            throw new NotImplementedException();
        }

        public int Add(Model.SamplingGroup model)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.SamplingGroup model)
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.SamplingGroup model)
        {
            return dal.GetList(model);
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.SamplingGroup model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
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
    }
}
