
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.RBAC;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  class BGMGroupType : BaseBLL<GMGroupType>, ZhiFang.Digitlab.IBLL.Business.IBGMGroupType
	{
        public IBRBACModule IBRBACModule { get; set; }
        public BaseResultTree GetGroupTreeByEmpId(long empid)
        {
            BaseResultTree brt = new BaseResultTree();
            brt.success = true;
            List<tree> listTree = new List<tree>();

            try
            {
                if (empid > 0)
                {
                    BaseResultTree brtg = (BaseResultTree)IBRBACModule.SearchTestGroupModuleByHREmpID(empid);
                    IList<GMGroupType> bgmgt = DBDao.LoadAll();
                    foreach (var gt in bgmgt)
                    {
                        //小组类型（检验大组）
                        if (gt.GMGroupList.Count > 0)
                        {
                            tree gttree = new Entity.tree();
                            gttree.text = gt.Name;
                            gttree.url = null;
                            gttree.icon = null;
                            gttree.tid = gt.Id.ToString();
                            gttree.pid = "0";
                            gttree.value = gt.Id.ToString();
                            gttree.expanded = true;
                            
                            List<tree> lt = new List<tree>();
                            foreach (var g in gt.GMGroupList)
                            {
                                //检验小组
                                if (brtg.Tree.Where(a => a.value.ToString().Substring(a.value.ToString().IndexOf("GroupID") + 8, a.value.ToString().Length - (a.value.ToString().IndexOf("GroupID") + 9)) == g.Id.ToString()).Count() > 0)
                                {
                                    tree tree = new Entity.tree();
                                    tree.text = g.Name;
                                    tree.url = null;
                                    tree.icon = null;
                                    tree.tid = g.Id.ToString();
                                    tree.pid = gt.Id.ToString();
                                    tree.value = g.Id.ToString();
                                    tree.expanded = true;
                                    lt.Add(tree);
                                }
                            }
                            gttree.Tree = lt;
                            listTree.Add(gttree);
                        }
                    }
                }
                else
                {
                    IList<GMGroupType> bgmgt = DBDao.LoadAll();
                    foreach (var gt in bgmgt)
                    {
                        //小组类型（检验大组）
                        if (gt.GMGroupList.Count > 0)
                        {
                            tree gttree = new Entity.tree();
                            gttree.text = gt.Name;
                            gttree.url = null;
                            gttree.icon = null;
                            gttree.tid = gt.Id.ToString();
                            gttree.pid = "0";
                            gttree.value = gt.Id.ToString();
                            gttree.expanded = true;
                            
                            List<tree> lt = new List<tree>();
                            foreach (var g in gt.GMGroupList)
                            {
                                tree tree = new Entity.tree();
                                tree.text = g.Name;
                                tree.url = null;
                                tree.icon = null;
                                tree.tid = g.Id.ToString();
                                tree.pid = gt.Id.ToString();
                                tree.value = g.Id.ToString();
                                tree.expanded = true;
                                lt.Add(tree);
                            }
                            gttree.Tree = lt;
                            listTree.Add(gttree);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                brt.success = false;
                brt.ErrorInfo = ex.Message;
            }
            brt.Tree = listTree;
            return brt;
        }
	}
}