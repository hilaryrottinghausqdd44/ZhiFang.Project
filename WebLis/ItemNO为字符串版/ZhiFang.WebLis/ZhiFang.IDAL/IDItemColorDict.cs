using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDItemColorDict : IDataBase<Model.ItemColorDict>, IDataPage<Model.ItemColorDict>
    {
        //得到一个对象实体
        DataSet GetList(ZhiFang.Model.ItemColorDict model);
        Model.ItemColorDict GetModelByColorName(string colorName);

        bool Exists(int ColorID);
        int Delete(int ColorID);
        System.Data.DataSet GetAllList();
        Model.ItemColorDict GetModel(int colorId);
        int Update(Model.ItemColorDict model);
        int Add(Model.ItemColorDict model);
        DataSet GetList(int Top, string strWhere, string filedOrder);
    }
}
