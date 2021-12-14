using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
	/// <summary>
	/// 接口层IBSampleTypeControl	
	/// </summary>
    public interface IBSampleTypeControl : IBBase<ZhiFang.Model.SampleTypeControl>, IBTransCodeControl
	{
		#region  成员方法	
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string SampleTypeControlNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		int Delete(string SampleTypeControlNo);
				
		int DeleteList(string Idlist );
				/// <summary>
		/// 得到一个对象实体
		/// </summary>
		ZhiFang.Model.SampleTypeControl GetModel(string SampleTypeControlNo);
		
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
        List<ZhiFang.Model.SampleType> ControlDataTableToList(DataTable dt, int ControlLabNo);
		#endregion  成员方法

        #region 字典对照
        DataSet GetListByPage(ZhiFang.Model.SampleTypeControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.SampleTypeControl model, int nowPageNum, int nowPageSize);

        List<ZhiFang.Model.SampleTypeControl> DataTableToList(DataTable dt);
        #endregion
	} 
}