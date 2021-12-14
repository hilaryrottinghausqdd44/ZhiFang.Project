using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL.Other
{
    public interface IDB_Lab_Base
    {
        /// <summary>
        /// 根据实验室端的名称查找对应的实验室端的编码
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="labCname"></param>
        /// <param name="SourceOrgID"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        DataSet GetLabNo(string tableName, List<string> labCname, string SourceOrgID, string str);
        /// <summary>
        /// 根据实验室字典的编码获取中心端的编码
        /// </summary>
        /// <param name="tableName">关系表的表名</param>
        /// <param name="labCname">实验室字典的名称</param>
        /// <param name="SourceOrgID">送检单位</param>
        /// <param name="str"></param>
        /// <returns></returns>
        DataSet GetCentNo(string tableName, List<string> labNo, string SourceOrgID, string str);
        bool CheckLabNo(string tableName, List<string> labNo, string SourceOrgID, string str);
        int GetTotalCount(string tableName, string SourceOrgID,string LabNo,string str);
    
        /// <summary>
        /// 根据中心端的编码获取实验室字典的编码
        /// </summary>
        /// <param name="tableName">关系表的表名</param>
        /// <param name="CenterNo">中心端的编码</param>
        /// <param name="SourceOrgID"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        DataSet GetLabControlNo(string tableName, List<string> CenterNo, string SourceOrgID, string str);
        /// <summary>
        /// 检验实验室的编码是否已经做过对照
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="CenterNo"></param>
        /// <param name="SourceOrgID"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        bool CheckCenterNo(string tableName, List<string> CenterNo, string SourceOrgID, string str);
        int GetTotalCenterCount(string tableName, string SourceOrgID,string LabNo, string str);
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
        /// <param name="LabCode">实验室编号</param>
        /// <returns></returns>
        DataSet GetPGroup(string LabCode);
    }
}
