
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
    public class BBSampleType : BaseBLL<BSampleType>, ZhiFang.Digitlab.IBLL.Business.IBBSampleType
	{

        #region IBBSampleType 成员

        public BaseResultTree GetBSampleTypeTree()
        {
            BaseResultTree brt = new BaseResultTree();
            brt.success = true;
            List<tree> listTree = new List<tree>();

            try
            {
                IList<BSampleType> bgmgt = DBDao.LoadAll();
                foreach (var gt in bgmgt)
                {
                    tree gttree = new Entity.tree();
                    if (gt.ParSampleTypeID == null || gt.ParSampleTypeID.Value == 0)
                    {
                        gttree.text = gt.Name;
                        gttree.url = null;
                        gttree.icon = null;
                        gttree.tid = gt.Id.ToString();
                        gttree.pid = "0";
                        gttree.value = gt.Id.ToString();
                        gttree.expanded = true;
                        gttree.Tree = GetSampleTree(gt.Id);
                    }
                    listTree.Add(gttree);
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
        
        private List<tree> GetSampleTree(long p)
        {
            List<tree> lt = new List<tree>();
            IList<BSampleType> sampletypelist= DBDao.GetListByHQL(" bsampletype.ParSampleTypeID=" + p);
            if (sampletypelist != null && sampletypelist.Count > 0)
            {
                foreach (var a in sampletypelist)
                {
                    tree tmptree = new tree();
                    tmptree.text = a.Name;
                    tmptree.url = null;
                    tmptree.icon = null;
                    tmptree.tid = a.Id.ToString();
                    tmptree.pid = p.ToString();
                    tmptree.value = a.Id.ToString();
                    tmptree.expanded = true;
                    List<tree> ltt = GetSampleTree(a.Id);
                    tmptree.Tree = ltt;
                    lt.Add(tmptree);
                }
            }
            else
            {
                return null;
            }
            return lt;
        }

        public bool _judgeIsContainSampletype(long pSampleTypeID, long cSampleTypeID)
        {
            bool tempResult = false;
            IList<BSampleType> listSampleType = DBDao.GetListByHQL(" bsampletype.ParSampleTypeID=" + pSampleTypeID);
            if (listSampleType.Count > 0)
            {
                foreach (BSampleType bSampleType in listSampleType)
                {
                    tempResult = (bSampleType.Id == cSampleTypeID);
                    if (tempResult)
                        break;
                }
                if (!tempResult)
                { 
                    foreach (BSampleType bSampleType in listSampleType)
                    {
                        tempResult = _judgeIsContainSampletype(bSampleType.Id, cSampleTypeID);
                        if (tempResult)
                            break;
                    }                
                }
            }
            return tempResult;
        }

        #endregion
    }
}