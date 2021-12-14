using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IDAL;
using System.Data;
namespace ZhiFang.BLL.Common.BaseDictionary
{

    public partial class LocationbarCodePrintPamater : IBLocationbarCodePrintPamater
    {
        IDAL.IDLocationbarCodePrintPamater dal;

        public LocationbarCodePrintPamater()
        {
            dal = DALFactory.DalFactory<IDAL.IDLocationbarCodePrintPamater>.GetDal("LocationbarCodePrintPamater", ZhiFang.Common.Dictionary.DBSource.LisDB());
        }

        public bool Exists(long Id)
        {
            return dal.Exists(Id);
        }

        public bool Add(Model.LocationbarCodePrintPamater model)
        {
            return dal.Add(model);
        }

        public bool Update(Model.LocationbarCodePrintPamater model)
        {
            return dal.Update(model);
        }

        public bool Delete(long Id)
        {
            return dal.Delete(Id);
        }
        public bool Delete(string AccountId)
        {
            return dal.Delete(AccountId);
        }
        public Model.LocationbarCodePrintPamater GetModel(long Id)
        {
            return dal.GetModel(Id);
        }

        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        public List<ZhiFang.Model.LocationbarCodePrintPamater> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.LocationbarCodePrintPamater> modelList = new List<ZhiFang.Model.LocationbarCodePrintPamater>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.LocationbarCodePrintPamater model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.LocationbarCodePrintPamater();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = long.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["AccountId"].ToString() != "")
                    {
                        model.AccountId = dt.Rows[n]["AccountId"].ToString();
                    }
                    if (dt.Rows[n]["ParaMeter"].ToString() != "")
                    {
                        model.ParaMeter = dt.Rows[n]["ParaMeter"].ToString();
                    }

                    if (dt.Rows[n]["CreateDateTime"].ToString() != "")
                    {
                        model.CreateDateTime = DateTime.Parse(dt.Rows[n]["CreateDateTime"].ToString());

                    }
                    if (dt.Rows[n]["UpdateDateTime"].ToString() != "")
                    {
                        model.UpdateDateTime = DateTime.Parse(dt.Rows[n]["UpdateDateTime"].ToString());

                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public Model.LocationbarCodePrintPamater GetModel(string AccountId)
        {
            return dal.GetModel(AccountId);
        }


        #region IBLocationbarCodePrintPamater 成员


        public Model.LocationbarCodePrintPamater GetAdminPara()
        {            
            DataSet ds=dal.GetList(" 1=1 ");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return this.DataTableToList(ds.Tables[0])[0];
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
