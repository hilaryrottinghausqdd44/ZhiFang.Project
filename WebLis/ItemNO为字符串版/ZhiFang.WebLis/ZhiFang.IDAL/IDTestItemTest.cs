using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ZhiFang.IDAL
{
    public interface IDTestItemTest : IDataBase<Model.TestItemTest>, IDataPage<Model.TestItemTest>
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
        Model.TestItemTest GetModel(string ItemNo);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetListLike(Model.TestItemTest model);
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
        DataSet GetList(ZhiFang.Model.TestItemTest model, string flag);
        #endregion  成员方法
    }
}
