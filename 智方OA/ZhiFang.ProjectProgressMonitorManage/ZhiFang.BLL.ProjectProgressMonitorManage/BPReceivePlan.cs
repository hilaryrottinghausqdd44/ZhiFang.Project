using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.IBLL.OA;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPReceivePlan : BaseBLL<PReceivePlan>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPReceivePlan
    {
        public IBSCOperation IBSCOperation { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public IDPContractDao IDPContractDao { get; set; }
        public override bool Add()
        {
            this.Entity.UnReceiveAmount = this.Entity.ReceivePlanAmount - this.Entity.ReceiveAmount;
            return base.Add();
        }
        //public BaseResultDataValue PReceivePlanAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction)
        //{
        //    BaseResultDataValue brb = new BaseResultDataValue();
        //    //if (Entity.Status.ToString() == PReceivePlanStatus.暂存.Key || Entity.Status.ToString() == PReceivePlanStatus.审核通过.Key)
        //    //{
        //    //    if (base.Add())
        //    //    {
        //    //        IBSCOperation.AddOperationEntityStatus(this.Entity);
        //    //        if (Entity.Status.ToString() == PReceivePlanStatus.Key)
        //    //            PReceivePlanStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
        //    //        return brb;
        //    //    }
        //    //    else
        //    //    {
        //    //        brb.ErrorInfo = "PReceivePlanAdd.Add错误！";
        //    //        brb.success = false;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    brb.ErrorInfo = "PReceivePlanAdd.Add错误！状态：" + Entity.Status;
        //    //    brb.success = false;
        //    //}
        //    return brb;
        //}

        public BaseResultDataValue AddPReceivePlanList(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<PReceivePlan> prplist)
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            if (prplist != null && prplist.Count > 0)
            {
                PContract pc = IDPContractDao.Get(prplist[0].PContractID.Value);
                if (pc != null)
                {
                    Double tmpsum = prplist.Sum(a => a.ReceivePlanAmount);
                    if (pc.Amount != tmpsum)
                    {
                        ZhiFang.Common.Log.Log.Error("AddPReceivePlanList错误！收款计划总额：" + tmpsum + "同合同总额：" + pc.Amount + "不相等！");
                        throw new Exception("收款计划总额同合同总额不相等！");
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("未找到相关的合同！合同ID：" + prplist[0].PContractID.Value);
                    throw new Exception("未找到相关的合同！合同ID："+ prplist[0].PContractID.Value);
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("AddPReceivePlanList错误！收款计划列表为空！");
                throw new Exception("AddPReceivePlanList.Add错误！收款计划列表为空！");
            }

            foreach (PReceivePlan prp in prplist)
            {
                if (prp.Status.ToString() == PReceivePlanStatus.执行中.Key)
                {
                    if (DBDao.Save(prp))
                    {
                        IBSCOperation.AddOperationEntityStatus(prp);
                        //PReceivePlanStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("AddPReceivePlanList.Save错误！");
                        throw new Exception("AddPReceivePlanList.Save错误！");
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("AddPReceivePlanList.Save错误！状态：" + prp.Status);
                    throw new Exception("AddPReceivePlanList.Save错误！状态：" + prp.Status);
                }
            }
            brb.success = true;
            brb.ErrorInfo = "";

            return brb;
        }

        public BaseResultDataValue ChangeApplyPReceivePlan(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, List<PReceivePlan> prplist, long PPReceivePlanID)
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            PReceivePlan pprp = DBDao.Get(PPReceivePlanID);
            if (pprp == null)
            {
                ZhiFang.Common.Log.Log.Error("ChangeApplyPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "为空！");
                throw new Exception("ChangeApplyPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "为空！");
            }
            if (prplist != null && prplist.Count > 0)
            {
                CheckChangePReceivePlan(pprp, prplist);
            }

            foreach (PReceivePlan prp in prplist)
            {
                if (prp.Status.ToString() == PReceivePlanStatus.暂存.Key)
                {
                    if (DBDao.Save(prp))
                    {
                        IBSCOperation.AddOperationEntityStatus(prp);
                        //PReceivePlanStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("ChangePReceivePlan.Add错误！");
                        throw new Exception("ChangePReceivePlan.Add错误！");
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("ChangePReceivePlan.Add错误！状态：" + prp.Status);
                    throw new Exception("ChangePReceivePlan.Add错误！状态：" + prp.Status);
                }
            }
            if (DBDao.UpdateByHql(" update PReceivePlan set Status=" + PReceivePlanStatus.变更申请.Key + " where Id=" + pprp.Id) <= 0)
            {
                ZhiFang.Common.Log.Log.Error("ChangePReceivePlan.UpdateByHql错误！");
                throw new Exception("ChangePReceivePlan.UpdateByHql错误！");
            }
            else
            {
                IBSCOperation.AddOperationEntityStatus(pprp);
                PReceivePlanStatusMessagePush(pushWeiXinMessageAction, pprp.Id, PReceivePlanStatus.变更申请.Key, pprp);
            }
            brb.success = true;
            brb.ErrorInfo = "";

            return brb;
        }

        public BaseResultDataValue ChangeSubmitPReceivePlan(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long PPReceivePlanID)
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            PReceivePlan pprp = DBDao.Get(PPReceivePlanID);
            if (pprp == null)
            {
                ZhiFang.Common.Log.Log.Error("ChangeSubmitPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "为空！");
                throw new Exception("ChangeSubmitPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "为空！");
            }
            if (pprp.Status.ToString() != PReceivePlanStatus.变更申请.Key)
            {
                ZhiFang.Common.Log.Log.Error("ChangeSubmitPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "的状态：" + PReceivePlanStatus.GetStatusDic()[pprp.Status.ToString()].Name + "！");
                throw new Exception("ChangeSubmitPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "的状态：" + PReceivePlanStatus.GetStatusDic()[pprp.Status.ToString()].Name + "！");
            }

            IList<PReceivePlan> prplist = DBDao.GetListByHQL(" PPReceivePlanID= " + PPReceivePlanID + " and IsUse=true ");
            if (prplist != null && prplist.Count > 0)
            {
                CheckChangePReceivePlan(pprp, prplist);
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("ChangeSubmitPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "的子收款计划为空！");
                throw new Exception("ChangeSubmitPReceivePlan错误！原始收款计划ID：" + PPReceivePlanID + "的子收款计划为空！");
            }

            if (DBDao.UpdateByHql(" update PReceivePlan set Status=" + PReceivePlanStatus.已变更.Key + " where Id=" + PPReceivePlanID) > 0 && DBDao.UpdateByHql(" update PReceivePlan set Status=" + PReceivePlanStatus.执行中.Key + " where PPReceivePlanID=" + PPReceivePlanID) > 0)
            {
                IBSCOperation.AddOperationEntityStatus(pprp);
                PReceivePlanStatusMessagePush(pushWeiXinMessageAction, pprp.Id, PReceivePlanStatus.执行中.Key, pprp);
                brb.success = true;
                brb.ErrorInfo = "";
                return brb;
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("ChangePReceivePlan.UpdateByHql错误！");
                throw new Exception("ChangePReceivePlan.UpdateByHql错误！");
            }
        }

        public BaseResultDataValue UnChangeSubmitPReceivePlan(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long PPReceivePlanID)
        {
            BaseResultDataValue brb = new BaseResultDataValue();
            PReceivePlan pprp = DBDao.Get(PPReceivePlanID);
            if (DBDao.UpdateByHql(" update PReceivePlan set Status=" + PReceivePlanStatus.变更退回.Key + " where Id=" + PPReceivePlanID) > 0)
            {
                IBSCOperation.AddOperationEntityStatus(pprp);
                PReceivePlanStatusMessagePush(pushWeiXinMessageAction, pprp.Id, PReceivePlanStatus.变更退回.Key, pprp);
                brb.success = true;
                brb.ErrorInfo = "";
                return brb;
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("UnChangeSubmitPReceivePlan.UpdateByHql错误！");
                throw new Exception("UnChangeSubmitPReceivePlan.UpdateByHql错误！");
            }
        }

        private void PReceivePlanStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, PReceivePlan entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            #region 变更申请
            if (StatusId.Trim() == PReceivePlanStatus.变更申请.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id in( " + RoleList.商务助理.Key + ") ");
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                message = "您收到了收款计划变更申请（合同名称：" + entity.PContractName + "，收款阶段：" + entity.ReceiveGradationName + "，金额：" + entity.ReceivePlanAmount + "，收款负责人：" + entity.ReceiveManName + "）。";
            }
            #endregion
            #region 变更审核通过
            if (StatusId.Trim() == PReceivePlanStatus.已变更.Key)
            {
                receiveidlist.Add(entity.ReceiveManID.Value);
                message = "您的收款计划变更申请（合同名称：" + entity.PContractName + "，收款阶段：" + entity.ReceiveGradationName + "，金额：" + entity.ReceivePlanAmount + "）,已被" + entity.ReviewMan + "定为'变更审核通过'状态。";
            }
            #endregion
            #region 变更审核退回
            if (StatusId.Trim() == PReceivePlanStatus.变更退回.Key)
            {
                receiveidlist.Add(entity.ReceiveManID.Value);
                message = "您的收款计划变更申请（合同名称：" + entity.PContractName + "，收款阶段：" + entity.ReceiveGradationName + "，金额：" + entity.ReceivePlanAmount + "）,已被" + entity.ReviewMan + "定为'变更审核退回'状态。";
            }
            #endregion
            if (receiveidlist.Count > 0)
            {

                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到收款管理模块信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：收款管理" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = entity.ReceiveManName});
                string tmpdatetime = (entity.ReceiveDate.HasValue) ? entity.ReceiveDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "preceiveplan", "");
            }
        }
        bool CheckChangePReceivePlan(PReceivePlan pprp, IList<PReceivePlan> prplist)
        {            
            if (prplist != null && prplist.Count > 0)
            {
                Double tmpsum = prplist.Sum(a => a.ReceivePlanAmount);
                if (pprp.UnReceiveAmount != tmpsum)
                {
                    ZhiFang.Common.Log.Log.Error("CheckChangePReceivePlan错误！新收款计划总额：" + tmpsum + "同原始收款计划未收总额：" + pprp.UnReceiveAmount + "不相等！");
                    throw new Exception("CheckChangePReceivePlan错误!新收款计划总额同原始收款计划未收总额不相等！");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("CheckChangePReceivePlan错误！原始收款计划ID：" + pprp.Id + "的子收款计划为空！");
                throw new Exception("CheckChangePReceivePlan错误！原始收款计划ID：" + pprp.Id + "的子收款计划为空！");
                return false;
            }
        }

        public bool DelPReceivePlan(long longPReceivePlanID)
        {
            if (DBDao.UpdateByHql(" update PReceivePlan set IsUse=false where  Id=" + longPReceivePlanID) > 0)
            {
                SCOperation scop = new SCOperation();
                scop.BobjectID = longPReceivePlanID;
                scop.BusinessModuleCode = "PReceivePlan";
                scop.Memo = "删除收款计划ID=" + longPReceivePlanID;
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid != null && empid.Trim() != "")
                    scop.CreatorID = long.Parse(empid);
                if (empname != null && empname.Trim() != "")
                    scop.CreatorName = empname;
                IBSCOperation.Entity = scop;
                 IBSCOperation.Add();
                return true;
            }
            return false;
        }

        public BaseResultTree<PReceivePlan> SearchListTreeByHQL(string where)
        {

            IList<PReceivePlan> PReceivePlanllist = DBDao.GetListByHQL(where);
            BaseResultTree<PReceivePlan> tmpbrtree = new BaseResultTree<PReceivePlan>();
            if (PReceivePlanllist == null || PReceivePlanllist.Count <= 0)
            {
                return null;
            }
            var parentprplist = PReceivePlanllist.Where(a => a.PPReceivePlanID == null).OrderBy(a=>a.DataAddTime);
            List<tree<PReceivePlan>> tmplisttree = new List<tree<PReceivePlan>>();
            for (int i = 0; i < parentprplist.Count(); i++)
            {
                tree<PReceivePlan> tmptree = new tree<PReceivePlan>();
                tmptree.text = parentprplist.ElementAt(i).ReceiveGradationName;
                tmptree.tid = parentprplist.ElementAt(i).Id.ToString();
                tmptree.pid = "0";
                tmptree.objectType = "PReceivePlan";
                tmptree.value = parentprplist.ElementAt(i);
                tmptree.Tree = GetChildTree(parentprplist.ElementAt(i).Id, PReceivePlanllist);
                tmptree.leaf = !(tmptree.Tree != null && tmptree.Tree.Length > 0);


                tmplisttree.Add(tmptree);
            }
            tmpbrtree.Tree = tmplisttree;
            return tmpbrtree;
        }

        public BaseResultTree<PReceivePlan> AdvSearchListTreeByHQL(string where)
        {

            IList<PReceivePlan> PReceivePlanllist = DBDao.GetListByHQL(where);
            BaseResultTree<PReceivePlan> tmpbrtree = new BaseResultTree<PReceivePlan>();
            if (PReceivePlanllist == null || PReceivePlanllist.Count <= 0)
            {
                return null;
            }
            List<string> PContractIDlist = new List<string>();
            foreach (var e in PReceivePlanllist)
            {
                PContractIDlist.Add(e.PContractID.ToString());
            }
            IList<PContract> PContractList= IDPContractDao.GetListByHQL(" PContractID in (" + string.Join(",", PContractIDlist.ToArray()) + ") ");
            if (PContractList == null || PContractList.Count <= 0)
            {
                return null;
            }
            foreach (var e in PReceivePlanllist)
            {
                var pcontract=PContractList.Where(a => a.Id == e.PContractID);
                if (pcontract != null && pcontract.Count() > 0)
                {
                    e.ContractSignDateTime = pcontract.ElementAt(0).SignDate;
                }
            }
            var parentprplist = PReceivePlanllist.Where(a => a.PPReceivePlanID == null).OrderBy(a => a.DataAddTime);
            List<tree<PReceivePlan>> tmplisttree = new List<tree<PReceivePlan>>();
            for (int i = 0; i < parentprplist.Count(); i++)
            {
                tree<PReceivePlan> tmptree = new tree<PReceivePlan>();
                tmptree.text = parentprplist.ElementAt(i).ReceiveGradationName;
                tmptree.tid = parentprplist.ElementAt(i).Id.ToString();
                tmptree.pid = "0";
                tmptree.objectType = "PReceivePlan";
                tmptree.value = parentprplist.ElementAt(i);
                tmptree.Tree = GetChildTree(parentprplist.ElementAt(i).Id, PReceivePlanllist);
                tmptree.leaf = !(tmptree.Tree != null && tmptree.Tree.Length > 0);


                tmplisttree.Add(tmptree);
            }
            tmpbrtree.Tree = tmplisttree;
            return tmpbrtree;
        }
        tree<PReceivePlan>[] GetChildTree(long pid,IList<PReceivePlan> PReceivePlanllist)
        {
            var prplist = PReceivePlanllist.Where(a => a.PPReceivePlanID == pid).OrderBy(a => a.DataAddTime);;
            List<tree<PReceivePlan>> tmplisttree = new List<tree<PReceivePlan>>();
            if (prplist != null && prplist.Count() > 0)
            {
                for (int i = 0; i < prplist.Count(); i++)
                {
                    tree<PReceivePlan> tmptree = new tree<PReceivePlan>();
                    tmptree.text = prplist.ElementAt(i).ReceiveGradationName;
                    tmptree.tid = prplist.ElementAt(i).Id.ToString();
                    tmptree.pid = pid.ToString();
                    tmptree.objectType = "PReceivePlan";
                    tmptree.value = prplist.ElementAt(i);
                    tmptree.Tree = GetChildTree(prplist.ElementAt(i).Id, PReceivePlanllist);
                    tmptree.leaf = !(tmptree.Tree != null && tmptree.Tree.Length > 0);
                    tmplisttree.Add(tmptree);
                }
            }
            else
            {
                return null;
            }
            return tmplisttree.ToArray() ;
        }

        public BaseResultDataValue AdvSearchTotalListTreeByHQL(string where, string fields)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (fields != null && fields.Length > 0 && fields.Split(',')[0].ToString().ToLower().Trim() == "unreceiveamount")
            {
                if (where != null && where.Trim() != "")
                {
                    where += " and (Status=" + PReceivePlanStatus.执行中.Key + " or Status=" + PReceivePlanStatus.变更申请.Key + " or Status=" + PReceivePlanStatus.变更退回.Key + " or Status=" + PReceivePlanStatus.收款完成.Key + " )";
                }
                else
                {
                    where = "(Status = " + PReceivePlanStatus.执行中.Key + " or Status = " + PReceivePlanStatus.变更申请.Key + " or Status = " + PReceivePlanStatus.变更退回.Key + " or Status = " + PReceivePlanStatus.收款完成.Key + " )";
                }
            }
            object result = DBDao.GetTotalByHQL(where, fields);
            brdv.ResultDataValue = ZhiFang.Common.Public.JsonHelp.JsonDotNetSerializer(result);
            return brdv;
        }
    }
}