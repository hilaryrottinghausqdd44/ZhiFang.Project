using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.BLL.Construction
{
    public class BBTDAppComponents : ZhiFang.Digitlab.BLL.BaseBLL<BTDAppComponents>, ZhiFang.Digitlab.IBLL.Construction.IBBTDAppComponents
    {
        public IDAO.IDBaseDao<BTDAppComponentsRef,long> BTDAppComponentsRefDBDao { get; set; }
        public IDAO.IDBaseDao<BTDAppComponentsOperate, long> BTDAppComponentsOperateDBDao { get; set; }
        public bool Add(IList<BTDAppComponentsRef> listacr)
        {
            if (listacr != null)
            {
                foreach (var tmp in listacr)
                {
                    tmp.BTDAppComponents = this.Entity;
                }
            }
            if (Entity.BTDAppComponentsOperateList != null)
            {
                foreach (var tmp in Entity.BTDAppComponentsOperateList)
                {
                    tmp.BTDAppComponents = this.Entity;
                }
            }

            Entity.BTDAppComponentsRefList = listacr;
            bool a = DBDao.Save(this.Entity);
            return a;
        }
        public bool AddAndJSFile(IList<BTDAppComponentsRef> listacr)
        {
            if (listacr != null)
            {
                foreach (var tmp in listacr)
                {
                    tmp.BTDAppComponents = this.Entity;
                }
            }
            if (Entity.BTDAppComponentsOperateList != null)
            {
                foreach (var tmp in Entity.BTDAppComponentsOperateList)
                {
                    tmp.BTDAppComponents = this.Entity;
                }
            }
            string tmpclasscode = this.Entity.ClassCode;
            string tmpdesigncode = this.Entity.DesignCode;
            this.Entity.ClassCode = "";
            this.Entity.DesignCode = "";
            Entity.BTDAppComponentsRefList = listacr;
            bool a = DBDao.Save(this.Entity);
            bool b = false;
            bool c = false;
            if (a)
            {
                string tmppath = GetFilePath.WebDiretory(ConfigHelper.GetConfigString("ExtjsClassFileDir"));
                if (FilesHelper.CheckAndCreatDir(tmppath + "\\ClassCode\\"))
                {
                    b = FilesHelper.SaveTextFile(tmppath + "\\ClassCode\\" + Entity.ModuleOperCode, tmpclasscode, TextFileType.JS);
                }
                if (FilesHelper.CheckAndCreatDir(tmppath + "\\DesignCode\\"))
                {
                    c = FilesHelper.SaveTextFile(tmppath + "\\DesignCode\\" + Entity.ModuleOperCode, tmpdesigncode, TextFileType.TXT);
                }
            }
            else
            {
                return false;
            }
            return b && c;
        }

        public bool Edit(IList<BTDAppComponentsRef> listacr)
        {
            //var tmp=DBDao.Get(Entity.Id);

            //tmp.AppType = Entity.AppType;
            //tmp.BTDModuleType = Entity.BTDModuleType;
            //tmp.BuildType = Entity.BuildType;
            //tmp.ClassCode = Entity.ClassCode;
            //tmp.CName = Entity.CName;
            //tmp.Creator = Entity.Creator;
            //tmp.DataAddTime = Entity.DataAddTime;
            //tmp.DataTimeStamp = Entity.DataTimeStamp;
            //tmp.DataUpdateTime = Entity.DataUpdateTime;
            //tmp.DesignCode = Entity.DesignCode;
            //tmp.EName = Entity.EName;
            //tmp.ExecuteCode = Entity.ExecuteCode;
            //tmp.InitParameter = Entity.InitParameter;
            //tmp.LabID = Entity.LabID;
            //tmp.Modifier = Entity.Modifier;
            //tmp.ModuleOperCode = Entity.ModuleOperCode;
            //tmp.ModuleOperInfo = Entity.ModuleOperInfo;
            //tmp.PinYinZiTou = Entity.PinYinZiTou;

            //if (listacr != null)
            //{
            //    for (int i = tmp.BTDAppComponentsRefList.Count-1; i>=0 ; i--)
            //    {
            //        //if (listacr.Where(t => t.Id == tmp.BTDAppComponentsRefList[i].Id).Count() <= 0)
            //        //{
            //            tmp.BTDAppComponentsRefList.RemoveAt(i);
            //        //}
            //    }
            //}
            //BTDAppComponentsRefDBDao.DeleteByHql("From BTDAppComponentsRef a where a.BTDAppComponents.Id = " + Entity.Id);
            if (listacr != null)
            {
                foreach (var tmp in listacr)
                {
                    tmp.BTDAppComponents = this.Entity;
                }
            }
            Entity.BTDAppComponentsRefList = listacr;
            bool a = DBDao.Update(Entity);
            //tmp.BTDAppComponentsRefList = listacr;
            //bool a = DBDao.Update(tmp);
            //BTDAppComponentsRefDBDao.Flush();
            //IList<BTDAppComponentsRef> aaa = BTDAppComponentsRefDBDao.GetListByHQL(" btdappcomponentsref.BTDAppComponents.Id=" + Entity.Id, -1, -1).list;

            //if (listacr != null)
            //{
            //    foreach (var tmp in aaa)
            //    {
            //        if (listacr.Where(t => t.Id == tmp.Id).Count() <= 0)
            //        {
            //            BTDAppComponentsRefDBDao.Delete(tmp.Id);
            //        }
            //    }
            //}
            return a;
        }

        public override bool Edit()
        {
            if (Entity.BTDAppComponentsRefList != null)
            {
                foreach (var tmp in Entity.BTDAppComponentsRefList)
                {
                    tmp.BTDAppComponents = this.Entity;
                }
            }
            if (Entity.BTDAppComponentsOperateList != null)
            {
                foreach (var tmp in Entity.BTDAppComponentsOperateList)
                {
                    tmp.BTDAppComponents = this.Entity;
                }
            }
            bool a = DBDao.Update(Entity);
            BTDAppComponentsRefDBDao.Flush();
            BTDAppComponentsOperateDBDao.Flush();
            return a;
        }

        public IList<BTDAppComponentsRef> SearchRefAppByID(long BTDAppComponentsID)
        {
            List<BTDAppComponentsRef> a = new List<BTDAppComponentsRef>();
            if (this.SearchRefAppByID(BTDAppComponentsID, ref a, new List<long>() { BTDAppComponentsID}))
            {
                return a;
            }
            else
            {
                throw new Exception("应用组件存在循环引用！");
            }
        }
        public bool SearchRefAppByID(long BTDAppComponentsID, ref List<BTDAppComponentsRef> a,List<long> RefIdList)
        {
            try
            {
                BTDAppComponents p = this.Get(BTDAppComponentsID);
                if (p.BTDAppComponentsRefList != null)
                {
                    for (int i = 0; i < p.BTDAppComponentsRefList.Count; i++)
                    {
                        if (RefIdList.Where(t => t == p.BTDAppComponentsRefList[i].RefAppComID.Value).Count()>0)
                        {
                            return false;
                        }
                        else
                        {
                            if (a.Where(t => t.BTDAppComponents.Id == p.BTDAppComponentsRefList[i].BTDAppComponents.Id && t.RefAppComID == p.BTDAppComponentsRefList[i].RefAppComID).Count() <= 0)
                            {
                                a.Add(p.BTDAppComponentsRefList[i]);
                                RefIdList.Add(p.BTDAppComponentsRefList[i].RefAppComID.Value);
                            }
                        }
                        SearchRefAppByID(p.BTDAppComponentsRefList[i].RefAppComID.Value, ref a,RefIdList);
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public EntityList<BTDAppComponents> SearchRefBTDAppComponentsByHQLAndId(string strHqlWhere, string Order, int page, int count, long AppComID)
        {
            List<long> refidlist = new List<long>();
            StringBuilder appidlist = new StringBuilder();
            if (SearchCitedAppByID(AppComID, ref refidlist))
            {
                foreach (var appid in refidlist)
                {
                    appidlist.Append(" and  btdappcomponents.Id<> " + appid);
                }
            }
            if (strHqlWhere != null && strHqlWhere.Trim().Length > 0)
            {
                strHqlWhere += appidlist.ToString();
            }
            else
            {
                strHqlWhere = " 1=1 " + appidlist.ToString();
            }
            EntityList<BTDAppComponents> el = DBDao.GetListByHQL(strHqlWhere, Order, page, count);
            return el;
        }
        /// <summary>
        /// 查询此应用被那些应用引用
        /// </summary>
        /// <param name="BTDAppComponentsID"></param>
        /// <param name="a"></param>
        /// <param name="RefIdList"></param>
        /// <returns></returns>
        public bool SearchCitedAppByID(long BTDAppComponentsID, ref List<long> RefIdList)
        {
            try
            {
                EntityList<BTDAppComponentsRef> p = BTDAppComponentsRefDBDao.GetListByHQL(" btdappcomponentsref.RefAppComID=" + BTDAppComponentsID, -1, -1);
                if (p!=null && p.list != null)
                {
                    for (int i = 0; i < p.count; i++)
                    {
                        RefIdList.Add(p.list[i].Id);
                        SearchCitedAppByID(p.list[i].Id, ref RefIdList);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public BaseResultTree SearchBTDAppComponentsFrameTree(long longBTDAppComponentsID, int treeDataConfig)
        {
            //longBTDAppComponentsID = 2;
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            List<tree> tempListTree = new List<tree>();
            try
            {
                string tempWhereStr = "";//为空查整个部门表
                if (longBTDAppComponentsID > 0)
                    tempWhereStr = " btdappcomponents.Id=" + longBTDAppComponentsID.ToString();
                EntityList<BTDAppComponents> tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);

                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    foreach (BTDAppComponents tempBTDAppComponents in tempEntityList.list)
                    {
                        tree tempTree = new tree();
                        tempTree.text = tempBTDAppComponents.CName;
                        tempTree.tid = tempBTDAppComponents.Id.ToString();
                        tempTree.pid = longBTDAppComponentsID.ToString();
                        //tempTree.objectType = tempBTDAppComponents.GetType().Name;
                        tempTree.objectType = "BTDAppComponents";
                        tempTree.expanded = true;
                        string tempKeyWord = tempBTDAppComponents.ModuleOperCode;
                        tempTree.Tree = GetChildTree(tempBTDAppComponents.BTDAppComponentsRefList, treeDataConfig, tempKeyWord);
                        tempTree.leaf = (tempTree.Tree.Count <= 0);
                        tempTree.iconCls = tempTree.leaf ? "appImg16" : "appsImg16";
                        if ((treeDataConfig > 0) || (tempBTDAppComponents.BTDAppComponentsOperateList != null
                            && tempBTDAppComponents.BTDAppComponentsOperateList.Count > 0))
                        {
                            GetBTDAppComponentsOperate(tempTree, tempBTDAppComponents.BTDAppComponentsOperateList, tempKeyWord);
                            tempListTree.Add(tempTree);
                        }
                        else if (!tempTree.leaf)
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
                throw new Exception("SearchBTDAppComponentsFrameTree:" + ex.Message);
            }
            return tempBaseResultTree;
        }

        public List<tree> GetChildTree(IList<BTDAppComponentsRef> btdAppComponentsRefList, int treeDataConfig, string strKeyWord)
        {
            List<tree> tempListTree = new List<tree>();
            try
            {
                if ((btdAppComponentsRefList != null) && (btdAppComponentsRefList.Count > 0))
                {
                    foreach (BTDAppComponentsRef tempBTDAppComponentsRef in btdAppComponentsRefList)
                    {
                        string tempKeyWord = strKeyWord;
                        if (tempBTDAppComponentsRef.RefAppComID != null)
                        {
                            BTDAppComponents tempBTDAppComponents = this.Get((long)tempBTDAppComponentsRef.RefAppComID);
                            if (tempBTDAppComponents != null)
                            {
                                tempKeyWord += "." + tempBTDAppComponentsRef.RefAppComIncID;
                                tree tempTree = new tree();
                                tempTree.text = tempBTDAppComponents.CName;
                                tempTree.tid = tempBTDAppComponents.Id.ToString();
                                tempTree.expanded = true;
                                if (tempBTDAppComponentsRef.BTDAppComponents != null)
                                    tempTree.pid = tempBTDAppComponentsRef.BTDAppComponents.Id.ToString();
                                tempTree.objectType = "BTDAppComponents";
                                tempTree.Tree = GetChildTree(tempBTDAppComponents.BTDAppComponentsRefList, treeDataConfig, tempKeyWord);
                                tempTree.leaf = (tempTree.Tree.Count <= 0);
                                tempTree.iconCls = tempTree.leaf ? "appImg16" : "appsImg16";
                                if ((treeDataConfig > 0) || (tempBTDAppComponents.BTDAppComponentsOperateList != null
                                    && tempBTDAppComponents.BTDAppComponentsOperateList.Count > 0))
                                {
                                    GetBTDAppComponentsOperate(tempTree, tempBTDAppComponents.BTDAppComponentsOperateList, tempKeyWord);
                                    tempListTree.Add(tempTree);
                                }
                                else if (!tempTree.leaf)
                                    tempListTree.Add(tempTree);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetChildTree:" + ex.Message);
            }
            return tempListTree;
        }

        public void GetBTDAppComponentsOperate(tree treeBTDAppComponents, IList<BTDAppComponentsOperate> btdAppComponentsOperateList, string strKeyWord)
        {
            try
            {
                foreach (BTDAppComponentsOperate tempAppOperate in btdAppComponentsOperateList)
                {
                    if (tempAppOperate != null)
                    {
                        tree tempAppOperateTree = new tree();
                        tempAppOperateTree.text = tempAppOperate.AppComOperateName + "(" + strKeyWord + "." + tempAppOperate.AppComOperateKeyWord + ")";
                        tempAppOperateTree.tid = tempAppOperate.Id.ToString();
                        tempAppOperateTree.pid = treeBTDAppComponents.tid;
                        tempAppOperateTree.objectType = "BTDAppComponentsOperate";
                        tempAppOperateTree.expanded = false;
                        tempAppOperateTree.leaf = true;
                        tempAppOperateTree.iconCls = tempAppOperateTree.leaf ? "operateImg16" : "operatesImg16";
                        if (treeBTDAppComponents.Tree == null)
                            treeBTDAppComponents.Tree = new List<tree>();
                        treeBTDAppComponents.Tree.Insert(0, tempAppOperateTree);//操作排序在前面
                        treeBTDAppComponents.leaf = false;
                        treeBTDAppComponents.iconCls = treeBTDAppComponents.leaf ? "appImg16" : "appsImg16";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetBTDAppComponentsOperate:" + ex.Message);
            }
        }
        /// <summary>
        /// 应用列表树
        /// </summary>
        /// <param name="longBTDAppComponentsID"></param>
        /// <returns></returns>
        public BaseResultTree<BTDAppComponents> SearchBTDAppComponentsListTree(long longBTDAppComponentsID)
        {
            BaseResultTree<BTDAppComponents> tempBaseResultTree = new BaseResultTree<BTDAppComponents>();
            EntityList<BTDAppComponents> tempEntityList = new EntityList<BTDAppComponents>();
            try
            {
                string tempWhereStr = "";
                //等于0取全部应用
                if (longBTDAppComponentsID > 0)
                    tempWhereStr = " btdappcomponents.Id=" + longBTDAppComponentsID.ToString();
                List<tree<BTDAppComponents>> tempListTree = new List<tree<BTDAppComponents>>();
                tempEntityList = this.SearchListByHQL(tempWhereStr, -1, -1);
                if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                {
                    Dictionary<string, BTDAppComponents> NoteBTDAppComponentsList = new Dictionary<string, BTDAppComponents>();
                    foreach (BTDAppComponents tempBTDAppComponents in tempEntityList.list)
                    {
                        tree<BTDAppComponents> tempTree = new tree<BTDAppComponents>();
                        tempTree.expanded = true;
                        tempTree.text = tempBTDAppComponents.CName;
                        tempTree.tid = tempBTDAppComponents.Id.ToString();
                        tempTree.pid = longBTDAppComponentsID.ToString();
                        tempTree.objectType = "BTDAppComponents";
                        tempTree.value = tempBTDAppComponents;
                        if (!NoteBTDAppComponentsList.ContainsKey(tempBTDAppComponents.Id.ToString()))
                            NoteBTDAppComponentsList.Add(tempBTDAppComponents.Id.ToString(), tempBTDAppComponents);
                        tempTree.Tree = GetChildTreeList(tempBTDAppComponents.BTDAppComponentsRefList, NoteBTDAppComponentsList);
                        tempTree.leaf = (tempTree.Tree.Length <= 0);
                        tempTree.iconCls = tempTree.leaf ? "appImg16" : "appsImg16";
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

        public tree<BTDAppComponents>[] GetChildTreeList(IList<BTDAppComponentsRef> tempBTDAppComponentsRefList, Dictionary<string, BTDAppComponents> NoteBTDAppComponentsList)
        {
            List<tree<BTDAppComponents>> tempListTree = new List<tree<BTDAppComponents>>();
            if (tempBTDAppComponentsRefList != null && tempBTDAppComponentsRefList.Count > 0)
            {
                try
                {
                    //EntityList<BTDAppComponentsRef> tempEntityList = BTDAppComponentsRefDBDao.GetListByHQL(" btdappcomponentsref.BTDAppComponents.Id=" + ParentID.ToString(), -1, -1);
                    //if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                    //{
                    foreach (BTDAppComponentsRef tempBTDAppComponentsRef in tempBTDAppComponentsRefList)
                    {
                        tree<BTDAppComponents> tempTree = new tree<BTDAppComponents>();
                        //Log.Info("___GetTree21:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + "___" + tempBTDAppComponentsRef.RefAppComID.ToString(), DateTimeFormatInfo.InvariantInfo));
                        BTDAppComponents tempEntity = null;
                        if (NoteBTDAppComponentsList.ContainsKey(tempBTDAppComponentsRef.RefAppComID.ToString()))
                            tempEntity = NoteBTDAppComponentsList[tempBTDAppComponentsRef.RefAppComID.ToString()];
                        else
                            tempEntity = this.Get((long)tempBTDAppComponentsRef.RefAppComID);
                        //Log.Info("___GetTree22:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + "___" + tempBTDAppComponentsRef.RefAppComID.ToString(), DateTimeFormatInfo.InvariantInfo));
                        if (tempEntity != null)
                        {
                            tempTree.expanded = true;
                            tempTree.text = tempEntity.CName;
                            tempTree.tid = tempEntity.Id.ToString();
                            tempTree.pid = tempBTDAppComponentsRef.BTDAppComponents.Id.ToString();
                            tempTree.objectType = "BTDAppComponents";
                            tempTree.value = tempEntity;
                            tempTree.Tree = GetChildTreeList(tempEntity.BTDAppComponentsRefList, NoteBTDAppComponentsList);
                            tempTree.leaf = (tempTree.Tree.Length <= 0);
                            tempTree.iconCls = tempTree.leaf ? "appImg16" : "appsImg16";
                            tempListTree.Add(tempTree);
                        }
                    }
                }
                //}
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return tempListTree.ToArray();
        }

        public BaseResultTree<BTDAppComponents> SearchBTDAppComponentsRefTree(long longBTDAppComponentsID)
        {
            BaseResultTree<BTDAppComponents> tempBaseResultTree = new BaseResultTree<BTDAppComponents>();
            EntityList<BTDAppComponents> tempEntityList = new EntityList<BTDAppComponents>();
            try
            {
                var tempEntity = this.Get(longBTDAppComponentsID);
                if (tempEntity != null)
                {
                    List<tree<BTDAppComponents>> tempListTree = new List<tree<BTDAppComponents>>();
                    tempEntityList = this.SearchListByHQL("", -1, -1);
                    if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))
                    {
                        foreach (BTDAppComponents tempBTDAppComponents in tempEntityList.list)
                        {
                            bool IsAddTree = false;
                            Dictionary<string, BTDAppComponents> NoteBTDAppComponentsList = new Dictionary<string, BTDAppComponents>();
                            tree<BTDAppComponents> tempTree = new tree<BTDAppComponents>();
                            tempTree.expanded = true;
                            tempTree.text = tempBTDAppComponents.CName;
                            tempTree.tid = tempBTDAppComponents.Id.ToString();
                            tempTree.pid = "";
                            tempTree.objectType = "BTDAppComponents";
                            tempTree.value = tempBTDAppComponents;
                            if (!NoteBTDAppComponentsList.ContainsKey(tempBTDAppComponents.Id.ToString()))
                                NoteBTDAppComponentsList.Add(tempBTDAppComponents.Id.ToString(), tempBTDAppComponents);
                            tempTree.Tree = GetChildTreeListByLeafID(tempBTDAppComponents.BTDAppComponentsRefList, NoteBTDAppComponentsList, longBTDAppComponentsID, ref IsAddTree);
                            tempTree.leaf = (tempTree.Tree.Length <= 0);
                            tempTree.iconCls = tempTree.leaf ? "appImg16" : "appsImg16";
                            if (IsAddTree)
                                tempListTree.Add(tempTree);
                        }
                    }
                    tempBaseResultTree.Tree = tempListTree;
                    tempBaseResultTree.success = true;
                }
                else
                {
                    tempBaseResultTree.success = false;
                    tempBaseResultTree.ErrorInfo = "此应用组件【" + longBTDAppComponentsID.ToString() + "】在数据库中不存在！";
                }
            }
            catch (Exception ex)
            {
                tempBaseResultTree.success = false;
                tempBaseResultTree.ErrorInfo = ex.Message;
            }
            return tempBaseResultTree;
        }

        public tree<BTDAppComponents>[] GetChildTreeListByLeafID(IList<BTDAppComponentsRef> tempBTDAppComponentsRefList, Dictionary<string, BTDAppComponents> NoteBTDAppComponentsList, long leafID, ref bool IsAddTree)
        {
            List<tree<BTDAppComponents>> tempListTree = new List<tree<BTDAppComponents>>();
            if (tempBTDAppComponentsRefList != null && tempBTDAppComponentsRefList.Count > 0)
            {
                try
                {

                    //EntityList<BTDAppComponentsRef> tempEntityList = BTDAppComponentsRefDBDao.GetListByHQL(" btdappcomponentsref.BTDAppComponents.Id=" + ParentID.ToString(), -1, -1);
                    //if ((tempEntityList != null) && (tempEntityList.list != null) && (tempEntityList.list.Count > 0))

                    //{
                    foreach (BTDAppComponentsRef tempBTDAppComponentsRef in tempBTDAppComponentsRefList)
                    {
                        tree<BTDAppComponents> tempTree = new tree<BTDAppComponents>();
                        BTDAppComponents tempEntity = null;
                        if (NoteBTDAppComponentsList.ContainsKey(tempBTDAppComponentsRef.RefAppComID.ToString()))
                            tempEntity = NoteBTDAppComponentsList[tempBTDAppComponentsRef.RefAppComID.ToString()];
                        else
                            tempEntity = this.Get((long)tempBTDAppComponentsRef.RefAppComID);
                        if (tempEntity != null)
                        {
                            tempTree.expanded = true;
                            tempTree.text = tempEntity.CName;
                            tempTree.tid = tempEntity.Id.ToString();
                            tempTree.pid = tempBTDAppComponentsRef.BTDAppComponents.Id.ToString();
                            tempTree.objectType = "BTDAppComponents";
                            tempTree.value = tempEntity;
                            tempTree.leaf = true;
                            IsAddTree = (tempEntity.Id == leafID);
                            if (!IsAddTree)
                            {
                                tempTree.Tree = GetChildTreeListByLeafID(tempEntity.BTDAppComponentsRefList, NoteBTDAppComponentsList, leafID, ref IsAddTree);
                                tempTree.leaf = (tempTree.Tree.Length <= 0);
                            }
                            tempTree.iconCls = tempTree.leaf ? "appImg16" : "appsImg16";
                            if (IsAddTree)
                                tempListTree.Add(tempTree);
                        }
                    }
                    //}
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return tempListTree.ToArray();
        }
        /// <summary>
        /// 根据应用组件ID判断组件是否被引用
        /// </summary>
        /// <param name="longBTDAppComponentsID"></param>
        /// <returns></returns>
        public bool JudgeBTDAppComponentsIsRef(long longBTDAppComponentsID)
        {
            EntityList<BTDAppComponentsRef> tempBTDAppComponentsRef = BTDAppComponentsRefDBDao.GetListByHQL(" btdappcomponentsref.RefAppComID=" + longBTDAppComponentsID.ToString(), -1, -1);
            return (tempBTDAppComponentsRef == null || tempBTDAppComponentsRef.list == null || tempBTDAppComponentsRef.list.Count == 0);
            //return (tempBTDAppComponentsRef == null || tempBTDAppComponentsRef.count == 0);
        }

        public bool DelJSFile(string ModuleOperCode)
        {
            bool a = false;
            bool b = false;
            string tmppath = GetFilePath.WebDiretory(ConfigHelper.GetConfigString("ExtjsClassFileDir"));
            a = FilesHelper.DelDirFile(tmppath + "\\ClassCode\\", Entity.ModuleOperCode + "." + TextFileType.JS.ToString());
            b = FilesHelper.DelDirFile(tmppath + "\\DesignCode\\", Entity.ModuleOperCode + "." + TextFileType.TXT.ToString());
            return a && b;
        }

        public bool UpdateAndJSFile(string[] strParas,string ModuleOperCode, string ClassCode, string DesignCode)
        {
            bool updateflag = DBDao.Update(strParas);

            if (updateflag)
            {
                if (ModuleOperCode.Trim() != "")
                {
                    bool a = false;
                    bool b = false;
                    bool c = false;
                    bool d = false;
                    string tmppath = GetFilePath.WebDiretory(ConfigHelper.GetConfigString("ExtjsClassFileDir"));
                    a = FilesHelper.DelDirFile(tmppath + "\\ClassCode\\", ModuleOperCode + "." + TextFileType.JS.ToString());
                    //ZhiFang.Common.Log.Log.Debug("删除JSFile！：" + tmppath + "\\ClassCode\\" + Entity.ModuleOperCode);
                    b = FilesHelper.DelDirFile(tmppath + "\\DesignCode\\", ModuleOperCode + "." + TextFileType.TXT.ToString());
                    //ZhiFang.Common.Log.Log.Debug("删除JSFile！：" + tmppath + "\\DesignCode\\" + Entity.ModuleOperCode);

                    if (FilesHelper.CheckAndCreatDir(tmppath + "\\ClassCode\\"))
                    {
                        c = FilesHelper.SaveTextFile(tmppath + "\\ClassCode\\" + ModuleOperCode, ClassCode, TextFileType.JS);
                    }
                    if (FilesHelper.CheckAndCreatDir(tmppath + "\\DesignCode\\"))
                    {
                        d = FilesHelper.SaveTextFile(tmppath + "\\DesignCode\\" + ModuleOperCode, DesignCode, TextFileType.TXT);
                    }
                    return a && b && c && d;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
