using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IDAL {
	/// <summary>
	/// 接口层IDFolkTypeControl	
	/// </summary>
	public interface IDFolkTypeControl:IDataBase<ZhiFang.Model.FolkTypeControl>
	{
		#region  成员方法
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string FolkControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string FolkControlNo);
		
				int DeleteList(string Idlist );
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.FolkTypeControl GetModel(string FolkControlNo);

        /// <summary>
        /// 根据实验室编码、中心端项目编码集合 查找项目是否已对照(集合中有一个项目未对照就返回false)
        /// </summary>
        /// <param name="l">中心端项目编码集合</param>
        /// <param name="LabCode">实验室编码</param>
        /// <returns>true/false</returns>
        bool CheckIncludeLabCode(List<string> l, string LabCode);
        /// <summary>
        /// 根据实验室编码、实验室项目编码集合 查找项目是否已对照(集合中有一个项目未对照就返回false)
        /// </summary>
        /// <param name="l">实验室项目编码集合</param>
        /// <param name="LabCode">实验室编码</param>
        /// <returns>true/false</returns>
        bool CheckIncludeCenterCode(List<string> l, string LabCode);

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
		#endregion  成员方法
        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.FolkTypeControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.FolkTypeControl model, int nowPageNum, int nowPageSize);
        #endregion
	} 
}