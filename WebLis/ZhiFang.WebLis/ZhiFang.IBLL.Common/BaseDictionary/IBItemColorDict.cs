using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBItemColorDict : IBBase<ZhiFang.Model.ItemColorDict>, IBDataPage<ZhiFang.Model.ItemColorDict>
    {
        DataSet GetList(Model.ItemColorDict model);
        Model.ItemColorDict GetModelByColorName(string colorName);
        bool Exists(int ColorID);
        int Delete(int ColorID);
        System.Data.DataSet GetAllList();
        Model.ItemColorDict GetModel(int colorId);
        int Update(Model.ItemColorDict model);
        int Add(Model.ItemColorDict model);
        List<ZhiFang.Model.ItemColorDict> DataTableToList(DataTable dt);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
