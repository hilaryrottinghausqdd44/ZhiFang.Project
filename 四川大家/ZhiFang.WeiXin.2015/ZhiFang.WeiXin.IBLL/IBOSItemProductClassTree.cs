

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
    /// <summary>
    ///
    /// </summary>
    public interface IBOSItemProductClassTree : ZhiFang.IBLL.Base.IBGenericManager<OSItemProductClassTree>
    {
        /// <summary>
        /// 通过区域Id查询检测项目产品分类树
        /// </summary>
        /// <param name="id"></param>
        /// <param name="areaID"></param>
        /// <returns></returns>
        BaseResultTree<OSItemProductClassTree> SearchOSItemProductClassTreeById(string id, string areaID);
        /// <summary>
        /// 根据某一节点id获取该节点及节点的子孙节
        /// </summary>
        /// <param name="id">节点id</param>
        /// <param name="areaID">服务层传入</param>
        /// <param name="maxlevel">最大层数</param>
        /// <returns></returns>
        BaseResultTree<OSItemProductClassTree> SearchOSItemProductClassTreeByIdAndHQL(string id, string where, string areaID, string maxlevel);
        /// <summary>
        /// 查询检测项目产品分类树及其所有子孙节点树信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isSearchChild"></param>
        /// <returns></returns>
        EntityList<OSItemProductClassTree> SearchOSItemProductClassTreeAndChildTreeByHQL(int page, int limit, string where, string sort, bool isSearchChild);

        /// <summary>
        /// 根据某一节点id获取该节点及节点的子孙节点ID字符串值(123,1222,132131)信息
        /// </summary>
        /// <param name="id">某一节点id</param>
        /// <param name="areaID">区域Id</param>
        /// <param name="maxlevel">最大层数</param>
        /// <returns></returns>
        string GetIDStrByMaxLayers(string id, string areaID, bool isSearchChild, string maxlevel);
        
    }
}