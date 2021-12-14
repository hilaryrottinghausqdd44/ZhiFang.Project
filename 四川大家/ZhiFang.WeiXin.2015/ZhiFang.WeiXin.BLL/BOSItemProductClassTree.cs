
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSItemProductClassTree : ZhiFang.BLL.Base.BaseBLL<OSItemProductClassTree>, ZhiFang.WeiXin.IBLL.IBOSItemProductClassTree
    {
        private string orderByFiles = "ositemproductclasstree.AreaID,ositemproductclasstree.PItemProductClassTreeID";// 
        #region 基本树数据格式获取
        /// <summary>
        /// 依区域Id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResultTree<OSItemProductClassTree> SearchOSItemProductClassTreeById(string id, string areaID)
        {
            BaseResultTree<OSItemProductClassTree> tempBaseResultTree = new BaseResultTree<OSItemProductClassTree>();
            if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty(areaID))
                return tempBaseResultTree;
            EntityList<OSItemProductClassTree> tempEntityList = new EntityList<OSItemProductClassTree>();
            List<tree<OSItemProductClassTree>> tempListTree = new List<tree<OSItemProductClassTree>>();
            string tempWhereStr = "";
            //树节点的Id
            if (String.IsNullOrEmpty(id))
                id = "0";
            if (id == "0")
                tempWhereStr = "ositemproductclasstree.PItemProductClassTreeID=" + id;
            else
                tempWhereStr = "ositemproductclasstree.Id=" + id;
            //区域Id
            if (!String.IsNullOrEmpty(areaID))
            {
                string areaIDStr = " ositemproductclasstree.AreaID=" + areaID;
                tempWhereStr = (tempWhereStr.Length > 0 ? tempWhereStr + " and " + areaIDStr : areaIDStr);
            }
            try
            {
                tempEntityList = this.SearchListByHQL(tempWhereStr, this.orderByFiles, -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (OSItemProductClassTree tempBOSItemProductClassTree in tempEntityList.list)
                    {
                        tree<OSItemProductClassTree> tempTree = new tree<OSItemProductClassTree>();
                        tempTree.text = tempBOSItemProductClassTree.CName;
                        tempTree.tid = tempBOSItemProductClassTree.Id.ToString();
                        tempTree.pid = tempBOSItemProductClassTree.PItemProductClassTreeID.ToString();
                        tempTree.objectType = "OSItemProductClassTree";
                        tempTree.value = tempBOSItemProductClassTree;
                        tempTree.expanded = true;

                        tempTree.Tree = GetChildTree(tempBOSItemProductClassTree.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0) ? true : false;
                        //tempTree.icon = tempTree.leaf ? "dictionary.PNG" : "package.PNG";
                        tempTree.iconCls = tempTree.leaf ? "tree-child-16" : "tree-parent-16";
                        tempListTree.Add(tempTree);
                    }
                }
                tempBaseResultTree.Tree = tempListTree;
                tempBaseResultTree.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = ex.Message;
            }
            return tempBaseResultTree;
        }
        public tree<OSItemProductClassTree>[] GetChildTree(long ParentID)
        {
            List<tree<OSItemProductClassTree>> tempListTree = new List<tree<OSItemProductClassTree>>();
            EntityList<OSItemProductClassTree> tempEntityList = null;
            tree<OSItemProductClassTree> tempTree = new tree<OSItemProductClassTree>();

            try
            {
                tempEntityList = this.SearchListByHQL(" ositemproductclasstree.PItemProductClassTreeID=" + ParentID.ToString(), this.orderByFiles, -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (OSItemProductClassTree tempEntity in tempEntityList.list)
                    {
                        tempTree = new tree<OSItemProductClassTree>();
                        tempTree.text = tempEntity.CName;
                        tempTree.tid = tempEntity.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.objectType = "OSItemProductClassTree";
                        tempTree.value = tempEntity;

                        tempTree.Tree = GetChildTree(tempEntity.Id);
                        //是否叶节点处理
                        tempTree.leaf = (tempTree.Tree.Length <= 0) ? true : false;
                        //tempTree.iconCls = "tree-parent-16 ";
                        tempTree.iconCls = tempTree.leaf ? "tree-child-16" : "tree-parent-16";
                        tempListTree.Add(tempTree);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree.ToArray();
        }

        #endregion
        #region 列表数据格式获取
        public EntityList<OSItemProductClassTree> SearchOSItemProductClassTreeAndChildTreeByHQL(int page, int limit, string where, string sort, bool isSearchChild)
        {
            EntityList<OSItemProductClassTree> tempEntityList = new EntityList<OSItemProductClassTree>();
            IList<OSItemProductClassTree> tempList = this.SearchListByHQL(where);
            List<OSItemProductClassTree> tempResultList = new List<OSItemProductClassTree>();
            if (isSearchChild == true)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    OSItemProductClassTree tempEntity = tempList[i];
                    tempEntity.IsLeaf = true;
                    List<OSItemProductClassTree> tempChildList = GetChildList(tempEntity.Id);
                    if ((tempChildList != null) && (tempChildList.Count > 0))
                    {
                        tempEntity.IsLeaf = false;
                        if (!tempResultList.Contains(tempEntity))
                            tempResultList.Add(tempEntity);
                        foreach (var item in tempChildList)
                        {
                            if (!tempResultList.Contains(item))
                                tempResultList.Add(item);
                        }
                    }
                    else
                    {
                        if (!tempResultList.Contains(tempEntity))
                            tempResultList.Add(tempEntity);
                    }
                }
            }
            else
            {
                tempResultList = (List<OSItemProductClassTree>)tempList;
                tempResultList = tempResultList.Distinct().ToList();
            }

            tempEntityList.count = tempResultList.Count;
            //分页处理
            if (tempResultList.Count > 0)
            {
                tempResultList = tempResultList.OrderByDescending(s => s.AreaID).ThenBy(s => s.PItemProductClassTreeID).ToList();
                if (limit > 0 && limit < tempResultList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = tempResultList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        tempResultList = list.ToList();
                    }
                }
            }
            tempEntityList.list = tempResultList;
            if (tempEntityList.list == null) tempEntityList.list = new List<OSItemProductClassTree>();
            return tempEntityList;
        }
        public List<OSItemProductClassTree> GetChildList(long ParentID)
        {
            List<OSItemProductClassTree> tempList = new List<OSItemProductClassTree>();
            try
            {
                IList<OSItemProductClassTree> tempChildList = this.SearchListByHQL(" ositemproductclasstree.PItemProductClassTreeID=" + ParentID.ToString());

                if ((tempChildList != null) && (tempChildList.Count > 0))
                {
                    for (int i = 0; i < tempChildList.Count; i++)
                    {
                        OSItemProductClassTree tempEntity = tempChildList[i];
                        tempEntity.IsLeaf = true;
                        List<OSItemProductClassTree> tempChild2 = GetChildList(tempEntity.Id);
                        if ((tempChild2 != null) && (tempChild2.Count > 0))
                        {
                            tempEntity.IsLeaf = false;
                            if (!tempList.Contains(tempEntity))
                                tempList.Add(tempEntity);
                            foreach (var item in tempChild2)
                            {
                                if (!tempList.Contains(item))
                                    tempList.Add(item);
                            }
                        }
                        else
                        {
                            if (!tempList.Contains(tempEntity))
                                tempList.Add(tempEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempList;
        }

        #endregion
        #region 检测项目产品分类树关系定制
        /// <summary>
        /// 根据某一节点id获取该节点及节点的子孙节点ID字符串值(123,1222,132131)信息
        /// </summary>
        /// <param name="id">某一节点id</param>
        /// <param name="areaID">区域Id</param>
        /// <param name="maxlevel">最大层数</param>
        /// <returns></returns>
        public string GetIDStrByMaxLayers(string id, string areaID, bool isSearchChild, string maxlevel)
        {
            StringBuilder idStr = new StringBuilder();
            if (String.IsNullOrEmpty(id))
                id = "0";

            //当前节点的最大层数
            int maxLayers = 0;
            if (!String.IsNullOrEmpty(maxlevel))
                maxLayers = Int32.Parse(maxlevel);
            string tempWhereStr = "";
            //树节点的Id
            if (String.IsNullOrEmpty(id))
                id = "0";
            if (id == "0")
                tempWhereStr = "ositemproductclasstree.PItemProductClassTreeID=" + id;
            else
                tempWhereStr = "ositemproductclasstree.Id=" + id;
            if (!String.IsNullOrEmpty(areaID))
                tempWhereStr = tempWhereStr + " and ositemproductclasstree.AreaID='" + areaID + "'";

            EntityList<OSItemProductClassTree> tempEntityList = new EntityList<OSItemProductClassTree>();
            tempEntityList = this.SearchListByHQL(tempWhereStr, this.orderByFiles, -1, -1);
            if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
            {
                foreach (OSItemProductClassTree tempEntity in tempEntityList.list)
                {
                    idStr.Append(tempEntity.Id.ToString());
                    idStr.Append(",");
                    if (isSearchChild)
                    {
                        StringBuilder idStr2 = GetChildIdStr(tempEntity.Id, 0, maxLayers);
                        if (!String.IsNullOrEmpty(idStr2.ToString()))
                        {
                            string tempIdStr = idStr2.ToString().TrimEnd(',');
                            idStr.Append(tempIdStr);
                            idStr.Append(",");
                        }
                    }
                    maxLayers = 0;
                }
            }
            //ZhiFang.Common.Log.Log.Debug("OSItemProductClassTree.IDStr:" + idStr.ToString().TrimEnd(','));
            return idStr.ToString().TrimEnd(',');
        }
        /// <summary>
        ///  根据某父一节点id获取该节点及节点的子孙节点ID字符串值(123,1222,132131)信息
        /// </summary>
        /// <param name="parentId">某一节点id</param>
        /// <param name="levels">当前层数</param>
        /// <param name="maxlevel">最大层数</param>
        /// <returns></returns>
        private StringBuilder GetChildIdStr(long parentId, int levels, int maxLayers)
        {
            StringBuilder idStr = new StringBuilder();
            EntityList<OSItemProductClassTree> tempEntityList = null;
            int level = levels + 1;
            //maxLayers为0时获取ParentID所有节点
            if ((levels > 0 && maxLayers != 0) && level >= maxLayers)
            {
                return idStr;
            }
            tempEntityList = this.SearchListByHQL(" ositemproductclasstree.PItemProductClassTreeID=" + parentId, this.orderByFiles, -1, -1);
            if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
            {
                foreach (OSItemProductClassTree tempEntity in tempEntityList.list)
                {
                    idStr.Append(tempEntity.Id.ToString());
                    idStr.Append(",");
                    StringBuilder idStr2 = GetChildIdStr(tempEntity.Id, level, maxLayers);
                    if (!String.IsNullOrEmpty(idStr2.ToString()))
                    {
                        string tempIdStr = idStr2.ToString().TrimEnd(',');
                        idStr.Append(tempIdStr);
                        idStr.Append(",");
                    }
                }
            }
            return idStr;
        }
        /// <summary>
        /// 根据某一节点id获取该节点及节点的子孙节点信息
        /// </summary>
        /// <param name="id">某一节点id</param>
        /// <param name="areaID">区域Id</param>
        /// <param name="maxlevel">最大层数</param>
        /// <returns></returns>
        public BaseResultTree<OSItemProductClassTree> SearchOSItemProductClassTreeByIdAndHQL(string id, string where, string areaID, string maxlevel)
        {
            BaseResultTree<OSItemProductClassTree> tempBaseResultTree = new BaseResultTree<OSItemProductClassTree>();
            if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty(areaID))
                return tempBaseResultTree;
            EntityList<OSItemProductClassTree> tempEntityList = new EntityList<OSItemProductClassTree>();
            List<tree<OSItemProductClassTree>> tempListTree = new List<tree<OSItemProductClassTree>>();
            //是否停止执行
            bool isBreak = false;
            //当前节点的最大层数
            int maxLayers = 0;
            if (!String.IsNullOrEmpty(maxlevel))
                maxLayers = Int32.Parse(maxlevel);
            string tempWhereStr = "";
            //树节点的Id
            if (String.IsNullOrEmpty(id))
                id = "0";
            if (id == "0")
                tempWhereStr = "ositemproductclasstree.PItemProductClassTreeID=" + id;
            else
                tempWhereStr = "ositemproductclasstree.Id=" + id;

            if (!String.IsNullOrEmpty(where))
                tempWhereStr = (tempWhereStr.Length > 0 ? tempWhereStr + " and " + where : where);

            //区域Id
            if (!String.IsNullOrEmpty(areaID))
            {
                string areaIDStr = " ositemproductclasstree.AreaID=" + areaID;
                tempWhereStr = (tempWhereStr.Length > 0 ? tempWhereStr + " and " + areaIDStr : areaIDStr);
            }

            if (String.IsNullOrEmpty(tempWhereStr))
            {
                isBreak = true;
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = "查询条件信息为空,停止查询.";
            }
            if (isBreak == false)
            {
                try
                {
                    tempEntityList = this.SearchListByHQL(tempWhereStr, this.orderByFiles, -1, -1);
                    if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                    {
                        foreach (OSItemProductClassTree tempOSItemProductClassTree in tempEntityList.list)
                        {
                            tree<OSItemProductClassTree> tempTree = new tree<OSItemProductClassTree>();
                            tempTree.text = tempOSItemProductClassTree.CName;
                            tempTree.tid = tempOSItemProductClassTree.Id.ToString();
                            tempTree.pid = tempOSItemProductClassTree.PItemProductClassTreeID.ToString();
                            tempTree.objectType = "OSItemProductClassTree";
                            tempTree.value = tempOSItemProductClassTree;
                            tempTree.expanded = true;
                            tempTree.Tree = GetChildTreeLists(tempOSItemProductClassTree.Id, 0, maxLayers);
                            if (maxLayers == 1)
                            {
                                tempTree.leaf = true;
                            }
                            else
                            {
                                tempTree.leaf = (tempTree.Tree.Length <= 0);
                            }
                            //tempTree.icon = tempTree.leaf ? "dictionary.PNG" : "package.PNG";
                            tempTree.iconCls = tempTree.leaf ? "tree-child-16" : "tree-parent-16";
                            tempListTree.Add(tempTree);
                        }
                    }
                    tempBaseResultTree.Tree = tempListTree;
                    tempBaseResultTree.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultTree.success = false;
                    tempBaseResultTree.ErrorInfo = ex.Message;
                }
            }
            return tempBaseResultTree;
        }
        /// <summary>
        ///  根据某一节点id获取该节点及节点的子孙节点信息
        /// </summary>
        /// <param name="parentId">某一节点id</param>
        /// <param name="levels">当前层数</param>
        /// <param name="maxlevel">最大层数</param>
        /// <returns></returns>
        private tree<OSItemProductClassTree>[] GetChildTreeLists(long parentId, int levels, int maxLayers)
        {
            List<tree<OSItemProductClassTree>> tempListTree = new List<tree<OSItemProductClassTree>>();
            EntityList<OSItemProductClassTree> tempEntityList = null;
            tree<OSItemProductClassTree> tempTree = new tree<OSItemProductClassTree>();
            int level = levels + 1;
            //maxLayers为0时获取ParentID所有节点
            if ((levels > 0 && maxLayers != 0) && level >= maxLayers)
            {
                return tempListTree.ToArray();
            }
            try
            {
                tempEntityList = this.SearchListByHQL(" ositemproductclasstree.PItemProductClassTreeID=" + parentId, this.orderByFiles, -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (OSItemProductClassTree tempOSItemProductClassTree in tempEntityList.list)
                    {
                        tempTree = new tree<OSItemProductClassTree>();
                        tempTree.text = tempOSItemProductClassTree.CName;
                        tempTree.tid = tempOSItemProductClassTree.Id.ToString();
                        tempTree.pid = parentId.ToString();
                        tempTree.objectType = "OSItemProductClassTree";
                        tempTree.value = tempOSItemProductClassTree;

                        tempTree.Tree = GetChildTreeLists(tempOSItemProductClassTree.Id, level, maxLayers);
                        //最大层数的是否叶节点处理
                        tempTree.leaf = (tempTree.Tree.Length <= 0) ? true : false;
                        tempTree.iconCls = tempTree.leaf ? "tree-child-16" : "tree-parent-16";
                        //tempTree.icon = tempTree.leaf ? "dictionary.PNG" : "package.PNG";
                        tempListTree.Add(tempTree);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree.ToArray();
        }
        #endregion

    }
}