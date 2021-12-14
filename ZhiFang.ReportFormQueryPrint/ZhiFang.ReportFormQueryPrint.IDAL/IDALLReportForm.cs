using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
    public interface IDALLReportForm
    {
        /// <summary>
        /// 骨髓项目表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetMarrowItemList(string FormNo);
        /// <summary>
        /// 微生物项目表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetMicroItemList(string FormNo);
        /// <summary>
        /// 微生物项目表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetMicroItemGroupList(string FormNo);

        /// <summary>
        /// 返回报告单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromInfo(string FormNo);
        /// <summary>
        /// 历史对比
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetReportValue(string []FormNo);

        /// <summary>
        /// 历史对比
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetReportValue(string[] p, string aa);

        /// <summary>
        /// 返回报告单信息(实体类)
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        Model.ReportForm GetFromInfoModel(string FormNo);

        /// <summary>
        /// 返回报告单内项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromItemList(string FormNo);


        /// <summary>
        /// 返回报告单内项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetReportItemList(string FormNo);

        /// <summary>
        /// 返回报告单所在小组信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromPGroupInfo(int SectionNo);
        /// <summary>
        /// 返回报告单所内的图片列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromGraphList(string FormNo);
        /// <summary>
        /// 查询报告单个数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        int GetCountFormFull(string strWhere);
        /// <summary>
        /// 查询报告单
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetList_FormFull(string fields, string strWhere);
        /// <summary>
        /// 查询普通项目
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetList_ItemFull(string strWhere);
        /// <summary>
        /// 查询微生物项目
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetList_MicroFull(string strWhere);
        /// <summary>
        /// 查询骨髓病理细胞学项目
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        DataSet GetList_MarrowFull(string strWhere);

        /// <summary>
        /// 多项目历史对比
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        DataSet ResultMhistory(string ReportFromID, string PatNo, string Where);
        /// <summary>
        /// 根据病历号和日期查询报告单
        /// </summary>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        DataSet ResultDataTimeMhistory(string PatNo, string Where);

        DataSet SampleStateTailAfter(string ReportFormID);
    }
}
