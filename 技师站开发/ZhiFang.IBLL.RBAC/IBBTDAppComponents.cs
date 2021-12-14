using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.IBLL.RBAC
{
    public interface IBBTDAppComponents : ZhiFang.IBLL.Base.IBGenericManager<BTDAppComponents>
    {
        /// <summary>
        /// 添加数据和应用引用关系
        /// </summary>
        /// <param name="listacr">引用应用数组</param>
        /// <returns></returns>
        bool Add(IList<BTDAppComponentsRef> listacr);
        /// <summary>
        /// 添加数据并保存应用文件
        /// </summary>
        /// <param name="listacr">引用应用数组</param>
        /// <returns></returns>
        bool AddAndJSFile(IList<BTDAppComponentsRef> listacr);
        /// <summary>
        /// 修改数据和应用引用关系
        /// </summary>
        /// <param name="listacr">引用应用数组</param>
        /// <returns></returns>
        bool Edit(IList<BTDAppComponentsRef> listacr);
        /// <summary>
        /// 查询引用应用列表
        /// </summary>
        /// <param name="BTDAppComponentsID">主应用ID</param>
        /// <returns></returns>
        IList<BTDAppComponentsRef> SearchRefAppByID(long BTDAppComponentsID);
        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="AppComID">主应用ID</param>
        /// <returns></returns>
        EntityList<BTDAppComponents> SearchRefBTDAppComponentsByHQLAndId(string strHqlWhere, string Order, int page, int count, long AppComID);

        /// <summary>
        /// 根据应用组件ID查询单列树
        /// </summary>
        /// <param name="longHRDeptID">部门ID</param>
        /// 应用组件ID等于0时 查询所有应用组件
        /// <returns>BaseResultTree</returns>
        BaseResultTree SearchBTDAppComponentsFrameTree(long longBTDAppComponentsID, int treeDataConfig);

        /// <summary>
        /// 根据应用ID查询列表树
        /// </summary>
        /// <param name="longBTDAppComponentsID">应用ID</param>
        /// 应用ID等于0时 查询所有应用
        /// <returns>BaseResultTree</returns>
        BaseResultTree<BTDAppComponents> SearchBTDAppComponentsListTree(long longBTDAppComponentsID);

        BaseResultTree<BTDAppComponents> SearchBTDAppComponentsRefTree(long longBTDAppComponentsID);
        /// <summary>
        /// 根据应用组件ID判断组件是否被引用
        /// </summary>
        /// <param name="longBTDAppComponentsID"></param>
        /// <returns></returns>
        bool JudgeBTDAppComponentsIsRef(long longBTDAppComponentsID);
        /// <summary>
        /// 根据应用组件编码删除JS文件
        /// </summary>
        /// <param name="ModuleOperCode">应用组件编码</param>
        /// <returns></returns>
        bool DelJSFile(string ModuleOperCode);
        /// <summary>
        /// 更新应用组件并更新JS文件
        /// </summary>
        /// <param name="strParas">更新字段参数</param>
        /// <param name="ModuleOperCode">应用组件编码</param>
        /// <param name="ClassCode">类代码</param>
        /// <param name="DesignCode">还原代码</param>
        /// <returns></returns>
        bool UpdateAndJSFile(string[] strParas, string ModuleOperCode, string ClassCode, string DesignCode);
    }
}
