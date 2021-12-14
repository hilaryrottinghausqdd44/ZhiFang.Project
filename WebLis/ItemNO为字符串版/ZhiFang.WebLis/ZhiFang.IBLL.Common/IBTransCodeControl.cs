using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBTransCodeControl
    {
        /// <summary>
        /// 根据中心端项的编码获取客户端项的编码
        /// </summary>
        /// <param name="LabCode">实验室编码</param>
        /// <param name="CenterNo">中心端项的编码</param>
        /// <returns>客户端项的编码</returns>
        string GetLabCodeNo(string LabCode, string CenterNo);

        /// <summary>
        /// 根据客户端实验室编码和客户端项的编码获取中心端项的编码
        /// </summary>
        ///<param name="LabCode">实验室编码</param>
        ///<param name="LabPrimaryNo">实验室项的编码</param>
        /// <returns>中心端项的编码</returns>
        string GetCenterNo(string LabCode,string LabPrimaryNo);

        /// <summary>
        /// 根据中心端项的编码集合和客户端项的编码获取客户端项的编码
        /// </summary>
        /// <param name="LabCode">实验室编码</param>
        /// <param name="CenterNo">中心端项的编码集合</param>
        /// <returns>包含实验室编码、中心项目编码和客户端项目编码的结果集</returns>
        DataSet GetLabCodeNo(string LabCode, List<string> CenterNoList);

        /// <summary>
        /// 根据客户端实验室编码集合和客户端项的编码获取中心端项的编码
        /// </summary>
        ///<param name="LabCode">实验室编码</param>
        ///<param name="LabPrimaryNo">实验室项的编码集合</param>
        /// <returns>包含实验室编码、中心项目编码和客户端项目编码的结果集</returns>
        DataSet GetCenterNo(string LabCode, List<string> LabPrimaryNoList);

    }
}
