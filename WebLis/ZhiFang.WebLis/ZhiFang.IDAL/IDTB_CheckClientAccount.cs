using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层TB_CheckClientAccount
    /// </summary>
    public interface IDTB_CheckClientAccount
    {
        #region  成员方法
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
        bool DeleteList(string idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.TB_CheckClientAccount GetModel(int id);
        ZhiFang.Model.TB_CheckClientAccount DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere, ZhiFang.Model.TB_CheckClientAccount model);
        DataSet GetListByPage(string strWhere, ZhiFang.Model.TB_CheckClientAccount model, int startIndex, int endIndex);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
        #region  MethodEx

        #endregion  MethodEx
    }
}
