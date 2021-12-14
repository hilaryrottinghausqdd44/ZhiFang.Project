using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层B_TestItem
    /// </summary>
    public interface IDTestItem : IDataBase<Model.TestItem>, IDataPage<Model.TestItem>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ItemNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string ItemNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.TestItem GetModel(string ItemNo);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListLike(Model.TestItem model);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        DataSet getItemCName(string ItemNo);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        /// <summary>
        /// 根据实体获取DataSet
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="flag">1：组套外项目；2：组套内项目</param>
        /// <returns>DateSet</returns>
        DataSet GetList(ZhiFang.Model.TestItem model, string flag);

        //DataSet getTestItemByCombiItem(string superGroup);

        //DataSet getItemDetailByItemId(string itemId);
        int Add(List<ZhiFang.Model.TestItem> modelList);
        int Update(List<ZhiFang.Model.TestItem> modelList);

        DataSet GetItemListByColor(string colorvalue,string where, string orderby, int startIndex, int endIndex);

        int GetItemListByColorCount(string colorvalue, string where);
        int SetItemColorByItemNoList(string colorvalue, List<string> itemnolist);
        #endregion  成员方法
    }
}