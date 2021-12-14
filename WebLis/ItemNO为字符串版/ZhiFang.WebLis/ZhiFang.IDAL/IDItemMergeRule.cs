using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDItemMergeRule : IDataBase<Model.ItemMergeRule>, IDataPage<ZhiFang.Model.ItemMergeRule>
    {
        DataSet GetList(ZhiFang.Model.ItemMergeRule model);
        DataSet GetList(string SuperGroupNo, string ClientNo);
        int DeleteList(string IDlist);
        bool Add(List<Model.ItemMergeRule> l);
        DataSet GetListItem(Model.ItemMergeRule model);
        bool AddList(List<Model.ItemMergeRule> l);
        DataSet GetItemNo(Model.ClientProfile ClientProfile, Model.ItemMergeRule ItemMergeRule);
    }
}
