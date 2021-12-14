using System;
using System.Data;
namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    /// <summary>
    /// 接口层SC_Operation
    /// </summary>
    public interface ISC_Operation
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long SCOperationID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(ZhiFang.ReportFormQueryPrint.Model.SC_Operation model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(ZhiFang.ReportFormQueryPrint.Model.SC_Operation model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(long SCOperationID);
        bool DeleteList(string SCOperationIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.ReportFormQueryPrint.Model.SC_Operation GetModel(long SCOperationID);
        ZhiFang.ReportFormQueryPrint.Model.SC_Operation DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
        #region  MethodEx

        #endregion  MethodEx
    }
}
