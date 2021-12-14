using System;
using System.Data;
using System.Collections.Generic;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层B_TestItemControl
    /// </summary>
    public interface IBResultTestItemControl : IBBase<Model.ResultTestItemControl>, IBTransCodeControl
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ItemNo, string ControlLabNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int Id);
        int DeleteList(string Idlist);
        int Delete(string ItemControlNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.ResultTestItemControl GetModel(string ItemNo, string LabCode, string LabItemNo);
        /// <summary>
        /// 得到中心端对照代码
        /// </summary>
        string GetCenterCode(string LabCode, string LabItemNo);
        /// <summary>
        /// 得到客户端对照代码
        /// </summary>
        string GetClientCode(string LabCode, string ItemNo);
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

        List<ZhiFang.Model.TestItem> ControlDataTableToList(DataTable dt, int ControlLabNo);
        #endregion  成员方法

        DataSet GetCenterCodeMapList(string hiddenClient, string itemlist);
        /// <summary>
        /// 通过实验室编码和申请单内部号返回实验室项目映射（包括组套项目和检验细项）
        /// </summary>
        /// <param name="LabCode">实验室编码</param>
        /// <param name="NRequestFormNo">申请单内部编号</param>
        /// <returns></returns>
        DataSet GetLabItemCodeMapListByNRequestLabCodeAndFormNo(string LabCode, string NRequestFormNo);

        #region 项目字典对照
        DataSet GetListByPage(ZhiFang.Model.ResultTestItemControl model, int nowPageNum, int nowPageSize);

        DataSet B_lab_GetListByPage(ZhiFang.Model.ResultTestItemControl model, int nowPageNum, int nowPageSize);
        DataSet B_lab_GetResultListByPage(ZhiFang.Model.ResultTestItemControl model, int nowPageNum, int nowPageSize);

        List<ZhiFang.Model.ResultTestItemControl> DataTableToList(DataTable dt);
        #endregion
    }
}