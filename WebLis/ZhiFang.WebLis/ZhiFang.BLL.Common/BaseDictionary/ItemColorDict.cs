using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Data;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class ItemColorDict : IBItemColorDict
    {
        IDAL.IDItemColorDict dal;
        public ItemColorDict()
        {
            dal = DALFactory.DalFactory<IDAL.IDItemColorDict>.GetDal("ItemColorDict", ZhiFang.Common.Dictionary.DBSource.LisDB());
        }

        public System.Data.DataSet GetList(Model.ItemColorDict model)
        {
            return dal.GetList(model);
        }

        public int Delete(int ColorID)
        {
            return dal.Delete(ColorID);
        }
        public bool Exists(int ColorID)
        {
            return dal.Exists(ColorID);
        }
        public int Add(Model.ItemColorDict model)
        {
            //throw new NotImplementedException();
            return dal.Add(model);
        }
        public Model.ItemColorDict GetModel(int colorId)
        {
            return dal.GetModel(colorId);
        }
        public int Update(Model.ItemColorDict model)
        {
            return dal.Update(model);
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.ItemColorDict model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            //throw new NotImplementedException();
            return dal.GetAllList();
        }

        public System.Data.DataSet GetListByPage(Model.ItemColorDict t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public Model.ItemColorDict GetModelByColorName(string colorName)
        {
            return dal.GetModelByColorName(colorName);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.ItemColorDict> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.ItemColorDict> modelList = new List<ZhiFang.Model.ItemColorDict>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.ItemColorDict model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.ItemColorDict();
                    if (dt.Columns.Contains("ColorID") && dt.Rows[n]["ColorID"].ToString() != "")
                    {
                        model.ColorID = int.Parse(dt.Rows[n]["ColorID"].ToString());
                    }
                    if (dt.Columns.Contains("ColorName") && dt.Rows[n]["ColorName"].ToString() != "")
                    {
                        model.ColorName = dt.Rows[n]["ColorName"].ToString();
                    }
                    if (dt.Columns.Contains("ColorValue") && dt.Rows[n]["ColorValue"].ToString() != "")
                    {
                        model.ColorValue = dt.Rows[n]["ColorValue"].ToString();
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
    }
}
