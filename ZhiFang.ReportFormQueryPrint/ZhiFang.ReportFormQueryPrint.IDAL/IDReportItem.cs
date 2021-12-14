using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDReportItem : IDataBase<Model.ReportItem>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ItemNo, string FormNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int ItemNo, string FormNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.ReportItem GetModel(int ItemNo, string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportItemList(string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportItemFullList(string FormNo);
        /// <summary>
        /// 多项目历史对比
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetReportItemCNameList(string FormNo);

        DataSet getTestItemItemDescByitem(string ItemNo);

        DataSet GetReportItemFullByReportFormId(string reportFormId);

        int UpdateReportItemFull(ReportItemFull t);

        DataSet GetReportItemList_DataSet(List<string> FormNo);

        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表并按照sortFields集合里的字段排序，sortFields元素格式：字段名,排序方式
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <param name="sortFields">sortFields</param>
        /// <returns></returns>
        DataTable GetReportItemFullListAndSort(string FormNo, List<string> sortFields);

        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表并按照sortFields集合里的字段排序，sortFields元素格式：字段名,排序方式
        /// 执行存储过程
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <param name="sortFields">sortFields</param>
        /// <returns></returns>
        DataTable GetProcReportItemQueryDataSource(string FormNo, List<string> sortFields);

        /// <summary>
        /// 根据FormNo和传入的where条件，查询符合条件的item的数量
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <param name="where">where</param>
        /// <returns></returns>
        int GetReportItemFullListWhereCount(string FormNo,string where);
        DataSet GetReportItemListSort_DataSet(List<string> FormNo, string sortFields);
    }
}
