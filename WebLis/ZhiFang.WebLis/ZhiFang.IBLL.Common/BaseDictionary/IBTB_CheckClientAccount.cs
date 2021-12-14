using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBTB_CheckClientAccount : IBBase<ZhiFang.Model.TB_CheckClientAccount>, IBDataPage<ZhiFang.Model.TB_CheckClientAccount>
    {
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
         bool Exists(int id);

        /// <summary>
        /// 增加一条数据
        /// </summary>
         int Add(ZhiFang.Model.TB_CheckClientAccount model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
         bool Update(ZhiFang.Model.TB_CheckClientAccount model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
         bool Delete(int id);
        /// <summary>
        /// 删除一条数据
        /// </summary>
         bool DeleteList(string idlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
         ZhiFang.Model.TB_CheckClientAccount GetModel(int id);

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
         ZhiFang.Model.TB_CheckClientAccount GetModelByCache(int id);

        /// <summary>
        /// 获得数据列表
        /// </summary>
         DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
         DataSet GetList(int Top, string strWhere, string filedOrder);
        /// <summary>
        /// 获得数据列表
        /// </summary>
         List<ZhiFang.Model.TB_CheckClientAccount> GetModelList(string strWhere);
        /// <summary>
        /// 获得数据列表
        /// </summary>
         List<ZhiFang.Model.TB_CheckClientAccount> DataTableToList(DataTable dt);

        /// <summary>
        /// 获得数据列表
        /// </summary>
         DataSet GetAllList();

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
         int GetRecordCount(string strWhere, ZhiFang.Model.TB_CheckClientAccount model);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
         DataSet GetListByPage(string strWhere, ZhiFang.Model.TB_CheckClientAccount model, int startIndex, int endIndex);
           #endregion  BasicMethod
    }
}
