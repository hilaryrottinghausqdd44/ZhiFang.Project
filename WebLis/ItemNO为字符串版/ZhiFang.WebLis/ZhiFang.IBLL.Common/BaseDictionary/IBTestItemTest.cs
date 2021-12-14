using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBTestItemTest : IBBase<Model.TestItemTest>, IBDataPage<Model.TestItemTest>
    {

        #region  成员方法

        bool Exists(string ItemNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string ItemNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.TestItemTest GetModel(string ItemNo);
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        DataSet GetList(ZhiFang.Model.TestItemTest model, string flag);
        int GetTotalCount();

        /// <summary>
        /// DataTable转换为List
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>List</returns>
        List<ZhiFang.Model.TestItemTest> DataTableToList(DataTable dt);
        #endregion  成员方法

        DataSet GetListLike(Model.TestItemTest ti_m);

        DataSet GetList(int p, int PageIndex, Model.TestItemTest testItem);

        int GetListCount(global::ZhiFang.Common.Dictionary.TestItemSuperGroupClass globalZhiFangCommonDictionaryTestItemSuperGroupClass);

        int GetListCount(Model.TestItemTest testItem);
        DataSet getItemCName(string ItemNo);
    }
}
