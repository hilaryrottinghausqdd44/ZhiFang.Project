
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BPhrasesWatchClass : BaseBLL<PhrasesWatchClass>, ZhiFang.IBLL.BloodTransfusion.IBPhrasesWatchClass
    {
        #region 类型树信息
        public BaseResultTree SearchPhrasesWatchClassTreeById(long id)
        {
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (id > 0)
                    tempWhereStr = string.Format(" phraseswatchclass.Id={0}", id);
                else
                    tempWhereStr = string.Format(" phraseswatchclass.ParentID={0}", id);
                EntityList<PhrasesWatchClass> tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (PhrasesWatchClass tempModel in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        //tempTree.name = tempModel.CName;
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = id.ToString();
                        //tempTree.objectType = tempPhrasesWatchClass.GetType().Name;
                        tempTree.objectType = "PhrasesWatchClass";
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTree(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
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
        public List<tree> GetChildTree(long parentID)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                EntityList<PhrasesWatchClass> tempEntityList = this.SearchListByHQL(" phraseswatchclass.ParentID=" + parentID.ToString(), " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (PhrasesWatchClass tempModel in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        //tempTree.name = tempModel.CName;
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = parentID.ToString();
                        tempTree.objectType = "PhrasesWatchClass";
                        tempTree.Tree = GetChildTree(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
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
        public BaseResultTree<PhrasesWatchClass> SearchPhrasesWatchClassListTreeById(long id)
        {
            BaseResultTree<PhrasesWatchClass> tempBaseResultTree = new BaseResultTree<PhrasesWatchClass>();
            EntityList<PhrasesWatchClass> tempEntityList = new EntityList<PhrasesWatchClass>();
            try
            {
                string tempWhereStr = "";
                if (id > 0)
                    tempWhereStr = string.Format(" phraseswatchclass.Id={0}", id);
                else
                    tempWhereStr = string.Format(" phraseswatchclass.ParentID={0}", id);
                List<tree<PhrasesWatchClass>> tempListTree = new List<tree<PhrasesWatchClass>>();
                ;
                tempEntityList = this.SearchListByHQL(tempWhereStr, " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (PhrasesWatchClass tempModel in tempEntityList.list)
                    {
                        tree<PhrasesWatchClass> tempTree = new tree<PhrasesWatchClass>();
                        //tempTree.name = tempModel.CName;
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = id.ToString();
                        tempTree.objectType = "PhrasesWatchClass";
                        tempTree.value = tempModel;
                        tempTree.expanded = true;
                        tempTree.Tree = GetChildTreeList(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
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
        public tree<PhrasesWatchClass>[] GetChildTreeList(long parentID)
        {
            List<tree<PhrasesWatchClass>> tempListTree = new List<tree<PhrasesWatchClass>>();
            try
            {
                EntityList<PhrasesWatchClass> tempEntityList = this.SearchListByHQL(" phraseswatchclass.ParentID=" + parentID.ToString(), " DispOrder ", -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (PhrasesWatchClass tempModel in tempEntityList.list)
                    {
                        tree<PhrasesWatchClass> tempTree = new tree<PhrasesWatchClass>();
                        //tempTree.name = tempModel.CName;
                        tempTree.text = tempModel.CName;
                        tempTree.tid = tempModel.Id.ToString();
                        tempTree.pid = parentID.ToString();
                        tempTree.objectType = "PhrasesWatchClass";
                        tempTree.value = tempModel;
                        tempTree.Tree = GetChildTreeList(tempModel.Id);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempTree.iconCls = tempTree.leaf ? "orgImg16" : "orgsImg16";
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

        public EntityList<PhrasesWatchClass> SearchPhrasesWatchClassAndChildListByHQL(string where, string sort, int page, int limit, bool isSearchChild)
        {
            EntityList<PhrasesWatchClass> tempEntityList = new EntityList<PhrasesWatchClass>();
            if (string.IsNullOrEmpty(sort))
                sort = "phraseswatchclass.ParentID ASC and phraseswatchclass DispOrder ASC";
            EntityList<PhrasesWatchClass> tempEntityList2 = this.SearchListByHQL(where, sort, 0, 0);
            IList<PhrasesWatchClass> tempList = tempEntityList2.list;

            List<PhrasesWatchClass> tempResultList = new List<PhrasesWatchClass>();
            if (isSearchChild == true)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    PhrasesWatchClass tempEntity = tempList[i];
                    List<PhrasesWatchClass> tempChildList = GetChildList(tempEntity.Id, sort);
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
                tempResultList = (List<PhrasesWatchClass>)tempList;
                tempResultList = tempResultList.Distinct().ToList();
            }

            tempEntityList.count = tempResultList.Count;
            //分页处理
            if (tempResultList.Count > 0)
            {
                tempResultList = tempResultList.OrderBy(s => s.ParentID).OrderBy(s => s.DispOrder).ToList();
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
            if (tempEntityList.list == null) tempEntityList.list = new List<PhrasesWatchClass>();
            return tempEntityList;
        }
        public List<PhrasesWatchClass> GetChildList(long parentID, string sort)
        {
            List<PhrasesWatchClass> tempList = new List<PhrasesWatchClass>();
            try
            {
                EntityList<PhrasesWatchClass> tempEntityList2 = this.SearchListByHQL(" phraseswatchclass.ParentID=" + parentID, sort, 0, 0);
                IList<PhrasesWatchClass> tempChildList = tempEntityList2.list;

                if ((tempChildList != null) && (tempChildList.Count > 0))
                {
                    for (int i = 0; i < tempChildList.Count; i++)
                    {
                        PhrasesWatchClass tempEntity = tempChildList[i];
                        List<PhrasesWatchClass> tempChild2 = GetChildList(tempEntity.Id, sort);
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