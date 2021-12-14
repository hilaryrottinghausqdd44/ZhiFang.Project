using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBItemMergeRule : IBLLBase<Model.ItemMergeRule>
    {
        DataSet GetList(Model.ItemMergeRule model);
        DataSet GetListByPage(ZhiFang.Model.ItemMergeRule model, int nowPageNum, int nowPageSize);
        DataSet GetList(string SuperGroupNo, string ClientNo);
        int Delete(string Id);
        bool Add(List<Model.ItemMergeRule> l);
        DataSet GetListItem(Model.ItemMergeRule model);
        bool AddList(List<Model.ItemMergeRule> l);
        DataSet GetItemNo(Model.ClientProfile ClientProfile, Model.ItemMergeRule ItemMergeRule);
    }
}
