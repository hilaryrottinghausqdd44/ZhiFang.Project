using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
   public interface IBReportData
    {
       int UpLoadReportDataFromBytes(byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg,out long OutReportformIndexID);
       /// <summary>
       /// 根据实验室字典的编码获取中心端的编码
       /// </summary>
       /// <param name="tableName">关系表的名称</param>
       /// <param name="labCname">实验室字典的名称</param>
       /// <param name="SourceOrgID">送检单位</param>
       /// <param name="str"></param>
       /// <returns></returns>
       DataSet GetCentNo(string tableName, List<string> labNo, string SourceOrgID, string str);
       /// <summary>
       /// 根据实验室字典的名称获取实验室字典的编码
       /// </summary>
       /// <param name="tableName">字典表名</param>
       /// <param name="labCname">实验室字典的名称</param>
       /// <param name="SourceOrgID">送检单位</param>
       /// <param name="str"></param>
       /// <returns></returns>
       DataSet GetLabNo(string tableName, List<string> labCname, string SourceOrgID, string str);
       bool CheckLabNo(DataSet ds, string DestiOrgID, out string ReturnDescription);
       DataSet GetLabControlNo(string tableName, List<string> CenterNO, string SourceOrgID, string str);
       bool CheckCenterNo(DataSet ds, string SourceOrgID, out string ReturnDescription);
       /// <summary>
       /// 返回中心小组编码
       /// </summary>
       /// <param name="tableName"></param>
       /// <param name="SourceOrgID"></param>
       /// <param name="LabNo">实验室</param>
       /// <returns></returns>
       string GetControl(string tableName, string SourceOrgID, string LabNo);

       /// <summary>
       /// 传入实验室号返回中心小组
       /// </summary>
       /// <param name="LaLabCode">实验室编码</param>
       /// <returns></returns>
       DataSet GetPGroup(string LaLabCode);

       DataSet Getapply(string strWhere);
    }
}
