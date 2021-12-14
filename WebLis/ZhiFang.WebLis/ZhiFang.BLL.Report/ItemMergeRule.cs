using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Report;
using System.Data.SqlClient;
using System.Data;

namespace ZhiFang.BLL.Report
{
    public partial class ItemMergeRule : IBItemMergeRule
    {
        IDAL.IDItemMergeRule dal = DALFactory.DalFactory<IDAL.IDItemMergeRule>.GetDalByClassName("ItemMergeRule");
        public System.Data.DataSet GetList(Model.ItemMergeRule model)
        {
            return dal.GetList(model);
        }

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }
        
        public int Add(Model.ItemMergeRule model)
        {
            return dal.Add(model);
        }
        public int Delete(string Id)
        {
            return dal.DeleteList(Id);
        }

        public int Update(Model.ItemMergeRule t)
        {
            throw new NotImplementedException();
        }

        public List<Model.ItemMergeRule> GetModelList(Model.ItemMergeRule t)
        {
            throw new NotImplementedException();
        }

        public List<Model.ItemMergeRule> DataTableToList(System.Data.DataTable dt)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
        }


        public System.Data.DataSet GetListByPage(Model.ItemMergeRule model, int nowPageNum, int nowPageSize)
        {
            return dal.GetListByPage(model, nowPageNum, nowPageSize);
        }


        public System.Data.DataSet GetList(string SuperGroupNo, string ClientNo)
        {
            return dal.GetList(SuperGroupNo, ClientNo);
        }


        public bool Add(List<Model.ItemMergeRule> l)
        {
            return dal.Add(l);
        }


        public DataSet GetListItem(Model.ItemMergeRule model)
        {
            return dal.GetListItem(model);
        }


        public bool AddList(List<Model.ItemMergeRule> l)
        {
            return dal.AddList(l);
        }


        public DataSet GetItemNo(Model.ClientProfile ClientProfile, Model.ItemMergeRule ItemMergeRule)
        {
            return dal.GetItemNo(ClientProfile, ItemMergeRule);
        }
    }
}
