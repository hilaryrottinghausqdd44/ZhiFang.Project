using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.SA;
using ZhiFang.IBLL.SA;

namespace ZhiFang.BLL.SA
{
    /// <summary>
    ///
    /// </summary>
    public class BBDictTree : BaseBLL<BDictTree>, IBBDictTree
    {
        /// <summary>
        /// 树的排序字段
        /// </summary>
        private string orderByFiles = " ParentID,DispOrder,DataAddTime ";

        /// <summary>
        /// 最大层数为字符串,逗号分隔
        /// 1.最大层数为空时,取所有层数
        /// 2.最大层数为空,如果最大层数长度为1,所有的IDS节点都采用这个层数作为节点的最大层数
        /// 3.最大层数的数组长度与IDS节点数组长度不一样时,报错,提示传入的参数长度不一致,请参考服务说明
        /// 4.最大层数从传入IDS节点的节点id开始算起,如ids=111,只获取111及111下的一级节点数据,111下的一级节点数据全为子节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idListStr"></param>
        /// <param name="maxLevelStr">最大层数,与idListStr对应</param>
        /// <returns></returns>
        public BaseResultTree<BDictTree> SearchBDictTreeListTree(string id, string idListStr, string maxLevelStr)
        {
            BaseResultTree<BDictTree> tempBaseResultTree = new BaseResultTree<BDictTree>();
            EntityList<BDictTree> tempEntityList = new EntityList<BDictTree>();
            List<tree<BDictTree>> tempListTree = new List<tree<BDictTree>>();
            ;
            bool isBreak = false;//是否停止执行
            int maxLayers = 0;

            string tempWhereStr = "";
            if (!String.IsNullOrEmpty(idListStr) && idListStr != "0")
            {
                tempWhereStr = " bdicttree.Id in(" + idListStr.ToString().TrimEnd(',') + ")";
            }
            else
            {
                if (!String.IsNullOrEmpty(id) || id.ToLower().Trim() == "root")
                {
                    idListStr = "0";
                }
                if (!string.IsNullOrEmpty(idListStr))
                {
                    tempWhereStr = " bdicttree.ParentID=" + idListStr;// + idListStr.ToString();
                }
            }

            if (String.IsNullOrEmpty(tempWhereStr))
            {
                isBreak = true;
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = "查询条件信息为空,停止查询.";
            }

            #region 树节点长度与最大层数的长度处理
            Dictionary<string, int> idsAndLayersDictionary = new Dictionary<string, int>();

            if (!String.IsNullOrEmpty(maxLevelStr))
            {
                bool success = true;
                string errorInfo = "";
                IDSAndMaxLayersProcess(idListStr, maxLevelStr, ref success, ref errorInfo, ref isBreak, ref idsAndLayersDictionary);
                tempBaseResultTree.success = success;
                tempBaseResultTree.ErrorInfo = errorInfo;
            }
            #endregion

            if (isBreak == false)
            {
                try
                {
                    tempEntityList = this.SearchListByHQL(tempWhereStr, this.orderByFiles, -1, -1);
                    if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                    {
                        foreach (BDictTree tempBDictTree in tempEntityList.list)
                        {
                            tree<BDictTree> tempTree = new tree<BDictTree>();
                            tempTree.text = tempBDictTree.CName;
                            tempTree.tid = tempBDictTree.Id.ToString();
                            tempTree.pid = tempBDictTree.ParentID.ToString();
                            tempTree.objectType = "BDictTree";
                            tempTree.value = tempBDictTree;
                            tempTree.expanded = true;
                            //最大层数值处理
                            if (idsAndLayersDictionary.Count > 0)
                            {
                                if (idsAndLayersDictionary.ContainsKey(tempBDictTree.Id.ToString()))
                                {
                                    maxLayers = idsAndLayersDictionary[tempBDictTree.Id.ToString()];
                                }
                            }

                            tempTree.Tree = GetChildTreeLists(tempBDictTree.Id, 0, maxLayers);
                            if (maxLayers == 1)
                            {
                                tempTree.leaf = true;
                            }
                            else
                            {
                                tempTree.leaf = (tempTree.Tree.Length <= 0);
                            }
                            maxLayers = 0;
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
        /// 根据某一节点id获取该节点及节点的子孙节点信息
        /// </summary>
        /// <param name="bDictTreeId">节点Id</param>
        /// <param name="maxLevelStr">最大层数</param>
        /// <returns></returns>
        public BaseResultTree SearchBDictTree(string idListStr, string maxLevelStr)
        {
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            bool isBreak = false;
            int maxLayers = 0;
            string tempWhereStr = "";
            if (!String.IsNullOrEmpty(idListStr) && idListStr != "0")
            {
                tempWhereStr = " bdicttree.Id in(" + idListStr.ToString().TrimEnd(',') + ")";
            }
            else if (!String.IsNullOrEmpty(idListStr) && idListStr == "0")
            {
                tempWhereStr = " bdicttree.ParentID=0";
            }

            if (String.IsNullOrEmpty(tempWhereStr))
            {
                isBreak = true;
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = "查询条件信息为空,停止查询.";
            }

            #region 树节点长度与最大层数的长度处理
            Dictionary<string, int> idsAndLayersDictionary = new Dictionary<string, int>();

            if (!String.IsNullOrEmpty(maxLevelStr))
            {
                bool success = true;
                string errorInfo = "";
                IDSAndMaxLayersProcess(idListStr, maxLevelStr, ref success, ref errorInfo, ref isBreak, ref idsAndLayersDictionary);
                tempBaseResultTree.success = success;
                tempBaseResultTree.ErrorInfo = errorInfo;
                tempBaseResultTree.Tree = null;
            }
            #endregion

            if (isBreak == false)
            {
                try
                {
                    EntityList<BDictTree> tempEntityList = this.SearchListByHQL(tempWhereStr, this.orderByFiles, -1, -1);
                    if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                    {
                        foreach (BDictTree tempBDictTree in tempEntityList.list)
                        {
                            tree tempTree = new tree();
                            tempTree.text = tempBDictTree.CName;
                            tempTree.tid = tempBDictTree.Id.ToString();
                            tempTree.pid = tempBDictTree.ParentID.ToString();
                            tempTree.objectType = "BDictTree";
                            tempTree.expanded = true;
                            //最大层数值处理
                            if (idsAndLayersDictionary.Count > 0)
                            {
                                if (idsAndLayersDictionary.ContainsKey(tempBDictTree.Id.ToString()))
                                {
                                    maxLayers = idsAndLayersDictionary[tempBDictTree.Id.ToString()];
                                }
                            }
                            tempTree.Tree = GetChildTree(tempBDictTree.Id);
                            if (maxLayers == 1)
                            {
                                tempTree.leaf = true;
                            }
                            else
                            {
                                tempTree.leaf = (tempTree.Tree.Count <= 0);
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
        public tree<BDictTree>[] GetChildTreeLists(long ParentID, int levels, int maxLayers)
        {
            List<tree<BDictTree>> tempListTree = new List<tree<BDictTree>>();
            EntityList<BDictTree> tempEntityList = null;
            tree<BDictTree> tempTree = new tree<BDictTree>();
            int level = levels + 1;
            //maxLayers为0时获取ParentID所有节点
            if ((levels > 0 && maxLayers != 0) && level >= maxLayers)
            {
                return tempListTree.ToArray();
            }
            try
            {
                tempEntityList = this.SearchListByHQL(" bdicttree.ParentID=" + ParentID.ToString(), this.orderByFiles, -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (BDictTree tempBDictTree in tempEntityList.list)
                    {
                        tempTree = new tree<BDictTree>();
                        tempTree.text = tempBDictTree.CName;
                        tempTree.tid = tempBDictTree.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.objectType = "BDictTree";
                        tempTree.value = tempBDictTree;

                        tempTree.Tree = GetChildTreeLists(tempBDictTree.Id, level, maxLayers);
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

        /// <summary>
        /// 获取某一父节点下的所有子节点信息
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public List<tree> GetChildTree(long ParentID)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                EntityList<BDictTree> tempEntityList = this.SearchListByHQL(" bdicttree.ParentID=" + ParentID.ToString(), this.orderByFiles, -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (BDictTree tempHRDept in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempHRDept.CName;
                        tempTree.tid = tempHRDept.Id.ToString();
                        tempTree.pid = ParentID.ToString();
                        tempTree.objectType = "BDictTree";
                        tempTree.Tree = GetChildTree(tempHRDept.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        //tempTree.icon = tempTree.leaf ? "dictionary.PNG" : "package.PNG";
                        tempTree.iconCls = tempTree.leaf ? "tree-child-16" : "tree-parent-16";
                        tempListTree.Add(tempTree);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tempListTree;
        }

        private void IDSAndMaxLayersProcess(string idListStr, string maxLevelStr, ref bool success, ref string errorInfo, ref bool isBreak, ref Dictionary<string, int> idsAndLayersDictionary)
        {
            if (isBreak == false && !String.IsNullOrEmpty(maxLevelStr) && !String.IsNullOrEmpty(idListStr))
            {
                string[] tempMaxLArrStr = maxLevelStr.Split(',');
                string[] tempIdArrStr = idListStr.Split(',');
                if (isBreak == false && tempMaxLArrStr.Length > 1 && tempMaxLArrStr.Length != tempIdArrStr.Length)
                {
                    isBreak = true;
                    success = false;
                    errorInfo = "传入的参数长度不一致,请参考服务说明.";
                }

                //最大层数数组长度为1,所有的IDS节点都采用这个层数作为节点的最大层数
                if (isBreak == false && tempMaxLArrStr.Length == 1)
                {
                    foreach (string pid in tempIdArrStr)
                    {
                        if (!idsAndLayersDictionary.ContainsKey(pid))
                        {
                            idsAndLayersDictionary.Add(pid, int.Parse(tempMaxLArrStr[0]));
                        }
                    }
                }

                //最大层数数组长度与IDS节点数组长度一致
                if (isBreak == false && tempMaxLArrStr.Length == tempIdArrStr.Length)
                {
                    for (int i = 0; i < tempIdArrStr.Length; i++)
                    {
                        string pid = tempIdArrStr[i];
                        if (!idsAndLayersDictionary.ContainsKey(pid))
                        {
                            idsAndLayersDictionary.Add(pid, int.Parse(tempMaxLArrStr[i]));
                        }
                    }
                }

            }
        }
        #region 列表数据格式
        public EntityList<BDictTree> SearchBDictTreeAndChildTreeByHQL(int page, int limit, string where, string sort, bool isSearchChild)
        {
            EntityList<BDictTree> tempEntityList = new EntityList<BDictTree>();
            IList<BDictTree> tempList = this.SearchListByHQL(where);
            List<BDictTree> tempResultList = new List<BDictTree>();
            if (isSearchChild == true)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    BDictTree tempEntity = tempList[i];
                    List<BDictTree> tempChildList = GetChildList(tempEntity.Id);
                    if ((tempChildList != null) && (tempChildList.Count > 0))
                    {
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
                tempResultList = (List<BDictTree>)tempList;
                tempResultList = tempResultList.Distinct().ToList();
            }

            tempEntityList.count = tempResultList.Count;
            //分页处理
            if (tempResultList.Count > 0)
            {
                tempResultList = tempResultList.OrderBy(s => s.ParentID).ToList();
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
            if (tempEntityList.list == null) tempEntityList.list = new List<BDictTree>();
            return tempEntityList;
        }
        public List<BDictTree> GetChildList(long ParentID)
        {
            List<BDictTree> tempList = new List<BDictTree>();
            try
            {
                IList<BDictTree> tempChildList = this.SearchListByHQL(" bdicttree.ParentID=" + ParentID);

                if ((tempChildList != null) && (tempChildList.Count > 0))
                {
                    for (int i = 0; i < tempChildList.Count; i++)
                    {
                        BDictTree tempEntity = tempChildList[i];
                        List<BDictTree> tempChild2 = GetChildList(tempEntity.Id);
                        if ((tempChild2 != null) && (tempChild2.Count > 0))
                        {
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
    }
}