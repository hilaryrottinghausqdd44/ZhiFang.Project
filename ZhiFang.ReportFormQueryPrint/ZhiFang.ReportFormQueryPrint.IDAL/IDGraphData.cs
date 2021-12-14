using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    /// <summary>
    /// 接口层GraphData
    /// </summary>
    public interface IDGraphData : IDAL.IDataBase<Model.GraphData>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Model.GraphData model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Model.GraphData model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.GraphData GetModel(DateTime ReceiveDate, int SectionNo, int TestTypeNo, string SampleNo, string GraphName, int GraphNo);
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
        DataSet GetListByReportFormID(string reportFormID);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
    }
}
