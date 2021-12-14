
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.WebAssist;
using Newtonsoft.Json.Linq;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using System.IO;
using ZhiFang.WebAssist.Common;
using System.Data;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BGKSampleRequestForm : BaseBLL<GKSampleRequestForm>, ZhiFang.IBLL.WebAssist.IBGKSampleRequestForm
    {
        IDSCRecordPhraseDao IDSCRecordPhraseDao { get; set; }
        IDSCRecordTypeDao IDSCRecordTypeDao { get; set; }
        IDSCRecordTypeItemDao IDSCRecordTypeItemDao { get; set; }
        IDSCRecordDtlDao IDSCRecordDtlDao { get; set; }
        IDDepartmentDao IDDepartmentDao { get; set; }

        IBSCRecordDtl IBSCRecordDtl { get; set; }
        IBSCOperation IBSCOperation { get; set; }
        IBSCBarCodeRules IBSCBarCodeRules { get; set; }
        IDTestItemDao IDTestItemDao { get; set; }
        IDGKDeptAutoCheckLinkDao IDGKDeptAutoCheckLinkDao { get; set; }
        IBMEGroupSampleForm IBMEGroupSampleForm { get; set; }
        IDSCRecordItemLinkDao IDSCRecordItemLinkDao { get; set; }
        IDPUserDao IDPUserDao { get; set; }

        #region 登记及修改
        public BaseResultDataValue AddGKSampleRequestFormAndDtl(GKSampleRequestForm entity, IList<SCRecordDtl> dtlEntityList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };


            entity.SCRecordType = IDSCRecordTypeDao.Get(entity.SCRecordType.Id);
            //生成一维条码
            long nextBarCode = -1;
            entity.BarCode = IBSCBarCodeRules.GetNextBarCode(entity.LabID, SCBarCodeRulesType.院感登记.Key, ref nextBarCode);

            if (!entity.MonitorType.HasValue)
            {
                Department department = IDDepartmentDao.Get(int.Parse(entity.DeptId.ToString()));
                if (department != null)
                    entity.MonitorType = department.MonitorType;
            }
            //“感控监测类型”：1:感控监测;2:科室监测;当前申请科室是否需要自动核收
            if (entity.MonitorType == 2 && entity.DeptId.HasValue)
            {
                IList<GKDeptAutoCheckLink> linkList = IDGKDeptAutoCheckLinkDao.GetListByHQL("gkdeptautochecklink.Department.Id=" + entity.DeptId.Value);
                if (linkList.Count > 0)
                {
                    entity.IsAutoReceive = true;
                }
            }
            long id2 = -1;
            long.TryParse(entity.BarCode, out id2);
            if (id2 > -1)
            {
                entity.Id = id2;
            }

            entity.Visible = true;
            entity.ReqDocNo = entity.BarCode;
            if (!entity.StatusID.HasValue)
                entity.StatusID = int.Parse(GKSampleFormStatus.暂存.Key);
            entity.PrintCount = 0;
            entity.ReceiveFlag = false;//核收标志
            entity.ResultFlag = false;//结果回写标志
            entity.Archived = false;
            entity.BacteriaTotal = "";//细菌总数
            entity.CreatorID = empID;
            entity.CreatorName = empName;
            entity.DispOrder = 0;

            entity.CName = dtlEntityList[0].ItemResult;//

            this.Entity = entity;
            bool result = this.Add();
            if (result == false)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("监测科室为:{0},监测类型为:{1},新增院感登记保存失败!", entity.DeptCName, entity.SCRecordType.CName);
                return brdv;
            }

            if (entity.DataTimeStamp == null)
            {
                entity.DataTimeStamp = dataTimeStamp;
            }
            brdv = IBSCRecordDtl.AddSCRecordDtlListOfGK(entity, ref dtlEntityList, empID, empName);
            if (brdv.success == false)
                throw new Exception(brdv.ErrorInfo);

            if (entity.StatusID.Value != long.Parse(GKSampleFormStatus.暂存.Key))
            {
                AddSCOperation(entity, empID, empName, entity.StatusID.Value, entity.Memo);
            }

            if (entity.StatusID.Value == long.Parse(GKSampleFormStatus.已提交.Key) && entity.IsAutoReceive == true)
            {
                SaveMEGroupSampleForm(entity, dtlEntityList, empID, empName);
            }
            return brdv;
        }

        public BaseResultBool EditGKSampleRequestFormAndDtl(GKSampleRequestForm entity, string[] tempArray, IList<SCRecordDtl> dtlEntityList, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();
            entity.SCRecordType = IDSCRecordTypeDao.Get(entity.SCRecordType.Id);

            GKSampleRequestForm oldEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();

            if (oldEntity.StatusID.Value == long.Parse(GKSampleFormStatus.已归档.Key))
            {
                brdv.success = false;
                brdv.ErrorInfo = "院感申请单ID：" + entity.Id + ",已归档,不能操作！";
                return brdv;
            }

            if (!EditGKSampleFormStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                brdv.ErrorInfo = "院感申请单ID：" + entity.Id + "的状态为：" + GKSampleFormStatus.GetStatusDic()[oldEntity.StatusID.ToString()].Name + "！";
                return brdv;
            }

            this.Entity = entity;
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                //更新记录明细
                if (dtlEntityList != null) brdv = IBSCRecordDtl.EditSCRecordDtlOfGKList(entity, ref dtlEntityList);

                if (entity.StatusID.Value != long.Parse(GKSampleFormStatus.暂存.Key))
                {
                    AddSCOperation(entity, empID, empName, entity.StatusID.Value, entity.Memo);
                }
            }

            if (entity.StatusID.Value == long.Parse(GKSampleFormStatus.已提交.Key) && oldEntity.IsAutoReceive == true)
            {
                BaseResultDataValue brdv2 = SaveMEGroupSampleForm(entity, dtlEntityList, empID, empName);
                brdv.success = brdv2.success;
                brdv.ErrorInfo = brdv2.ErrorInfo;
            }
            return brdv;
        }
        public BaseResultBool EditGKSampleRequestForm(GKSampleRequestForm entity, string[] tempArray, long empID, string empName)
        {
            BaseResultBool brdv = new BaseResultBool();

            GKSampleRequestForm oldEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();

            if (oldEntity.StatusID.Value == long.Parse(GKSampleFormStatus.已归档.Key))
            {
                brdv.success = false;
                brdv.ErrorInfo = "院感申请单ID：" + entity.Id + ",已归档,不能操作！";
                return brdv;
            }

            if (!EditGKSampleFormStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                brdv.success = false;
                brdv.ErrorInfo = "院感申请单ID：" + entity.Id + "的状态为：" + GKSampleFormStatus.GetStatusDic()[oldEntity.StatusID.ToString()].Name + "！";
                return brdv;
            }

            this.Entity = entity;
            tempArray = tmpa.ToArray();
            brdv.success = this.Update(tempArray);
            return brdv;
        }
        bool EditGKSampleFormStatusCheck(GKSampleRequestForm entity, GKSampleRequestForm serverEntity, List<string> tmpa, long empID, string empName)
        {
            #region 暂存
            if (entity.StatusID.ToString() == GKSampleFormStatus.暂存.Key)
            {
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.暂存.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 提交申请
            if (entity.StatusID.ToString() == GKSampleFormStatus.已提交.Key)
            {
                //审核应用时,可以先编辑保存状态为已申请的申请单
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.暂存.Key && serverEntity.StatusID.ToString() != GKSampleFormStatus.已提交.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 已核收
            if (entity.StatusID.ToString() == GKSampleFormStatus.已核收.Key)
            {
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.已提交.Key)
                {
                    return false;
                }
                entity.ReceiveFlag = true;
                entity.ReceiveDate = DateTime.Now;
                tmpa.Add("ReceiveFlag=" + entity.ReceiveFlag + " ");
                tmpa.Add("ReceiveDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion

            #region 已检验
            if (entity.StatusID.ToString() == GKSampleFormStatus.已检验.Key)
            {
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.已核收.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 已返结果
            if (entity.StatusID.ToString() == GKSampleFormStatus.已返结果.Key)
            {
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.已检验.Key)
                {
                    return false;
                }
                entity.ResultFlag = true;
                tmpa.Add("ResultFlag=" + entity.ResultFlag + " ");
            }
            #endregion

            #region 已评价
            if (entity.StatusID.ToString() == GKSampleFormStatus.已评价.Key)
            {
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.已核收.Key && serverEntity.StatusID.ToString() != GKSampleFormStatus.已检验.Key && serverEntity.StatusID.ToString() != GKSampleFormStatus.已返结果.Key)
                {
                    return false;
                }
                entity.EvaluatorFlag = true;
                entity.EvaluatorId = empID;
                entity.Evaluators = empName;
                entity.EvaluationDate = DateTime.Now;
                tmpa.Add("EvaluatorFlag=" + entity.EvaluatorFlag + " ");
                tmpa.Add("EvaluatorId=" + entity.EvaluatorId + " ");
                tmpa.Add("Evaluators='" + entity.Evaluators + "'");
                tmpa.Add("EvaluationDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion

            #region 已归档
            if (entity.StatusID.ToString() == GKSampleFormStatus.已归档.Key)
            {
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.已评价.Key)
                {
                    return false;
                }
                entity.Archived = true;
                tmpa.Add("Archived=" + entity.Archived + " ");
            }

            #endregion

            #region 作废
            if (entity.StatusID.ToString() == GKSampleFormStatus.作废.Key)
            {
                if (serverEntity.StatusID.ToString() != GKSampleFormStatus.暂存.Key && (serverEntity.StatusID.ToString() != GKSampleFormStatus.已提交.Key))
                {
                    return false;
                }

                entity.ObsoleteID = empID;
                entity.ObsoleteName = empName;
                entity.ObsoleteTime = DateTime.Now;
                tmpa.Add("ObsoleteID=" + entity.ObsoleteID + " ");
                tmpa.Add("ObsoleteName='" + entity.ObsoleteName + "'");
                tmpa.Add("ObsoleteTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            #endregion

            return true;
        }
        private void AddSCOperation(GKSampleRequestForm entity, long empID, string empName, long status, string memo)
        {
            SCOperation sco = new SCOperation();
            long id = entity.Id;
            if (id <= 0) return;

            sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            sco.LabID = entity.LabID;
            sco.BobjectID = id;
            sco.CreatorID = empID;
            sco.CreatorName = empName;
            sco.BusinessModuleCode = "GKSampleRequestForm";
            sco.Memo = memo;
            sco.IsUse = true;
            sco.Type = status;
            sco.DataUpdateTime = DateTime.Now;
            sco.TypeName = GKSampleFormStatus.GetStatusDic()[status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }

        private BaseResultDataValue SaveMEGroupSampleForm(GKSampleRequestForm entity, IList<SCRecordDtl> dtlEntityList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            brdv = IBMEGroupSampleForm.SaveByDeptAutoCheck(ref entity, dtlEntityList, empID, empName);
            if (brdv.success == true)
            {
                //entity = this.Get(entity.Id);
                entity.StatusID = long.Parse(GKSampleFormStatus.已核收.Key);
                entity.ReceiveDate = DateTime.Now;
                entity.ReceiveFlag = true;
                //核收人等于默认检验者
                string code3 = JSONConfigHelp.GetString(SysConfig.GKSYS.Key, "LISMainTesterNo");
                if (!string.IsNullOrEmpty(code3))
                {
                    int id2 = -100;
                    int.TryParse(code3, out id2);
                    PUser puser = IDPUserDao.Get(id2);
                    if (puser != null)
                    {
                        entity.ReceiveId = puser.Id;
                        entity.ReceiveCName = puser.CName;
                    }

                }

                AddSCOperation(entity, empID, empName, entity.StatusID.Value, entity.Memo);
            }
            return brdv;
        }

        #endregion

        #region 获取信息
        public BaseResultDataValue GetGKWarningAlertInfo(long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            JObject jresult = new JObject();

            //待评估处理
            string where1 = "gksamplerequestform.ResultFlag=1 and gksamplerequestform.EvaluatorFlag=0";
            int result1 = ((IDGKSampleRequestFormDao)base.DBDao).GetListCountByHQL(where1);

            //待归档处理
            string where2 = "gksamplerequestform.EvaluatorFlag=1 and gksamplerequestform.Archived=0";
            int result2 = ((IDGKSampleRequestFormDao)base.DBDao).GetListCountByHQL(where2);

            jresult.Add("ToCommentCount", result1);
            jresult.Add("ToBeArchivedCount", result2);
            baseResultDataValue.ResultDataValue = jresult.ToString();
            //ZhiFang.Common.Log.Log.Info("院感待处理信息:" + jresult.ToString());

            return baseResultDataValue;
        }
        public EntityList<GKSampleRequestForm> SearchGKSampleRequestFormAndDtlByHQL(string strHqlWhere, string sort, int page, int count, bool isGetTestItem)
        {
            EntityList<GKSampleRequestForm> tempEntityList = new EntityList<GKSampleRequestForm>();
            tempEntityList.list = new List<GKSampleRequestForm>();

            tempEntityList = this.SearchListByHQL(strHqlWhere, sort, page, count);
            IList<SCRecordItemLink> linkList = IDSCRecordItemLinkDao.LoadAll();

            for (int i = 0; i < tempEntityList.list.Count; i++)
            {
                GKSampleRequestForm entity = tempEntityList.list[i]; ;
                GetDocInfoAndDtlInfo(linkList, isGetTestItem, true, ref entity);
                tempEntityList.list[i] = entity;
            }
            return tempEntityList;
        }
        public EntityList<GKSampleRequestForm> SearchGKSampleRequestFormAndDtlByHQL(string strHqlWhere, string sort, int page, int count, bool isGetTestItem, bool isGetDtl)
        {
            EntityList<GKSampleRequestForm> tempEntityList = new EntityList<GKSampleRequestForm>();
            tempEntityList.list = new List<GKSampleRequestForm>();

            tempEntityList = this.SearchListByHQL(strHqlWhere, sort, page, count);
            IList<SCRecordItemLink> linkList = IDSCRecordItemLinkDao.LoadAll();

            for (int i = 0; i < tempEntityList.list.Count; i++)
            {
                GKSampleRequestForm entity = tempEntityList.list[i]; ;
                GetDocInfoAndDtlInfo(linkList, isGetTestItem, isGetDtl, ref entity);
                tempEntityList.list[i] = entity;
            }
            return tempEntityList;

        }
        public GKSampleRequestForm GetGKSampleRequestFormAndDtlById(long id)
        {
            GKSampleRequestForm entity = this.Get(id);
            IList<SCRecordItemLink> linkList = IDSCRecordItemLinkDao.LoadAll();
            GetDocInfoAndDtlInfo(linkList, false, true, ref entity);
            return entity;
        }
        private void GetDocInfoAndDtlInfo(IList<SCRecordItemLink> linkList, bool isGetTestItem, bool isGetDtl, ref GKSampleRequestForm docEntity)
        {
            docEntity.RecordTypeCName = docEntity.SCRecordType.CName;
            if (isGetTestItem == true && !string.IsNullOrEmpty(docEntity.SCRecordType.TypeCode))
            {
                int testItemId = 0;
                int.TryParse(docEntity.SCRecordType.TypeCode, out testItemId);
                TestItem testItem = IDTestItemDao.Get(testItemId);
                if (testItem != null) docEntity.TestItemCName = testItem.CName;
            }
            if (docEntity.Judgment == GKSampleFormJudgment.合格.Key || docEntity.Judgment == "1" ||
            docEntity.Judgment == "true" || docEntity.Judgment == "合格")
            {
                docEntity.JudgmentInfo = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.合格.Key].Name;
            }
            else if (docEntity.Judgment == GKSampleFormJudgment.不合格.Key || docEntity.Judgment == "0" || docEntity.Judgment == "false" || docEntity.Judgment == "不合格")
            {
                docEntity.JudgmentInfo = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.不合格.Key].Name;
            }
            else
            {
                docEntity.JudgmentInfo = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.未评估.Key].Name;
            }

            if (!isGetDtl) return;

            IList<SCRecordDtl> dtlList = IBSCRecordDtl.SearchListByHQL("screcorddtl.BObjectID=" + docEntity.Id);
            dtlList = dtlList.OrderBy(p => p.DispOrder).ToList();
            JObject iobject = new JObject();
            JArray jarray = new JArray();

            IList<SCRecordDtl> dtlNewList = new List<SCRecordDtl>();
            long typeId = docEntity.SCRecordType.Id;
            var linkList2 = linkList.Where(p => p.SCRecordType.Id == typeId).OrderBy(p => p.DispOrder).ToList();
            foreach (var item in linkList2)
            {
                var list3 = dtlList.Where(p => p.SCRecordType.Id == item.SCRecordType.Id && p.SCRecordTypeItem.Id == item.SCRecordTypeItem.Id);
                if (list3 != null || list3.Count() > 0)
                {
                    SCRecordDtl dtl = list3.ElementAt(0);
                    dtl.DispOrder = item.DispOrder;
                    dtl.SCRecordItemLink = item;
                    dtlNewList.Add(dtl);
                }
            }

            dtlNewList = dtlNewList.OrderBy(p => p.DispOrder).ToList();

            for (int i = 0; i < dtlNewList.Count(); i++)
            {
                SCRecordDtl item = dtlNewList[i];
                JObject jresult = new JObject();
                jresult.Add("BObjectID", item.Id.ToString());//监测类型记录项明细的主键Id
                jresult.Add("Id", item.SCRecordTypeItem.Id.ToString());
                jresult.Add("CName", item.SCRecordTypeItem.CName);
                jresult.Add("DispOrder", item.DispOrder.ToString());
                jresult.Add("ItemResult", item.ItemResult);
                jresult.Add("IsBillVisible", item.SCRecordItemLink.IsBillVisible);//是否开单可见

                switch (i)
                {
                    case 0:
                        docEntity.ItemResult1 = item.ItemResult;
                        jresult.Add("dataIndex", "GKSampleRequestForm_ItemResult1");
                        break;
                    case 1:
                        docEntity.ItemResult2 = item.ItemResult;
                        jresult.Add("dataIndex", "GKSampleRequestForm_ItemResult2");
                        break;
                    case 2:
                        docEntity.ItemResult3 = item.ItemResult;
                        jresult.Add("dataIndex", "GKSampleRequestForm_ItemResult3");
                        break;
                    case 3:
                        docEntity.ItemResult4 = item.ItemResult;
                        jresult.Add("dataIndex", "GKSampleRequestForm_ItemResult4");
                        break;
                    default:
                        break;
                }

                jarray.Add(jresult);
            }
            docEntity.DtlJArray = jarray.ToString().Replace(Environment.NewLine, "").Replace(" ", "");
            //ZhiFang.Common.Log.Log.Debug("DtlJArray:" + entity.DtlJArray);
        }

        public EntityList<GKOfDeptEvaluation> SearchGKListByHQLInfectionOfDept(string docVO, string where, string sort, int page, int limit)
        {
            EntityList<GKOfDeptEvaluation> tempEntityList = new EntityList<GKOfDeptEvaluation>();
            tempEntityList.list = new List<GKOfDeptEvaluation>();

            if (string.IsNullOrEmpty(where))
                where = "1=1";
            //评估标志为已评估的才统计
            if (!where.Contains("gksamplerequestform.EvaluatorFlag"))
                where += " and gksamplerequestform.EvaluatorFlag=1 ";
            //ZhiFang.Common.Log.Log.Debug("SearchGKListByHQLInfectionOfDept.SearchGKSampleRequestFormAndDtlByHQL.Start");
            IList<GKSampleRequestForm> excelList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, false, false).list;
            //ZhiFang.Common.Log.Log.Debug("SearchGKListByHQLInfectionOfDept.SearchGKSampleRequestFormAndDtlByHQL.End");

            var groupByDeptList = excelList.GroupBy(p => p.DeptId);
            //ZhiFang.Common.Log.Log.Debug("GroupByDeptList.End");

            int dispOrder = 0;
            foreach (var groupByDept in groupByDeptList)
            {
                dispOrder += dispOrder;
                GKOfDeptEvaluation entity = new GKOfDeptEvaluation();
                entity.DeptId = groupByDept.ElementAt(0).DeptId;
                entity.DeptCName = groupByDept.ElementAt(0).DeptCName;
                entity.DispOrder = dispOrder;

                var groupBySCRecordTypeList = groupByDept.GroupBy(p => p.SCRecordType.Id);
                //ZhiFang.Common.Log.Log.Debug("GroupBySCRecordTypeList.End");

                foreach (var groupBySCRecordType in groupBySCRecordTypeList)
                {
                    switch (groupBySCRecordType.Key)
                    {
                        case 11://手卫生
                            entity.HandHSamplesCount = groupBySCRecordType.Count();
                            entity.HandHQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.HandHSamplesCount.HasValue && entity.HandHSamplesCount > 0)
                            {
                                entity.HandHQualifiedRate = Math.Round(entity.HandHQualifiedCount.Value / entity.HandHSamplesCount.Value, 2) * 100;
                            }
                            break;
                        case 12://空气培养
                            entity.AirCSamplesCount = groupBySCRecordType.Count();
                            entity.AirCQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.AirCSamplesCount.HasValue && entity.AirCSamplesCount > 0)
                            {
                                entity.AirCQualifiedRate = Math.Round(entity.AirCQualifiedCount.Value / entity.AirCSamplesCount.Value, 2) * 100;
                            }
                            break;
                        case 13://物体表面
                            entity.SurfaceSamplesCount = groupBySCRecordType.Count();
                            entity.SurfaceQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.SurfaceSamplesCount.HasValue && entity.SurfaceSamplesCount > 0)
                            {
                                entity.SurfaceQualifiedRate = Math.Round(entity.SurfaceQualifiedCount.Value / entity.SurfaceSamplesCount.Value, 2) * 100;
                            }
                            break;

                        case 14://消毒剂
                            entity.DisinfectantSamplesCount = groupBySCRecordType.Count();
                            entity.DisinfectantQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.DisinfectantSamplesCount.HasValue && entity.DisinfectantSamplesCount > 0)
                            {
                                entity.DisinfectantQualifiedRate = Math.Round(entity.DisinfectantQualifiedCount.Value / entity.DisinfectantSamplesCount.Value, 2) * 100;
                            }
                            break;
                        case 15://透析液及透析用水
                            entity.DialysateSamplesCount = groupBySCRecordType.Count();
                            entity.DialysateQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.DialysateSamplesCount.HasValue && entity.DialysateSamplesCount > 0)
                            {
                                entity.DialysateQualifiedRate = Math.Round(entity.DialysateQualifiedCount.Value / entity.DialysateSamplesCount.Value, 2) * 100;
                            }
                            break;
                        case 16://医疗器材
                            entity.MedicalESamplesCount = groupBySCRecordType.Count();
                            entity.MedicalEQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.MedicalESamplesCount.HasValue && entity.MedicalESamplesCount > 0)
                            {
                                entity.MedicalEQualifiedRate = Math.Round(entity.MedicalEQualifiedCount.Value / entity.MedicalESamplesCount.Value, 2) * 100;
                            }
                            break;
                        case 17://污水
                            entity.SewageSamplesCount = groupBySCRecordType.Count();
                            entity.SewageQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.SewageSamplesCount.HasValue && entity.SewageSamplesCount > 0)
                            {
                                entity.SewageQualifiedRate = Math.Round(entity.SewageQualifiedCount.Value / entity.SewageSamplesCount.Value, 2) * 100;
                            }
                            break;
                        case 18://其他
                            entity.OtherSamplesCount = groupBySCRecordType.Count();
                            entity.OtherQualifiedCount = groupBySCRecordType.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                            if (entity.OtherSamplesCount.HasValue && entity.OtherSamplesCount > 0)
                            {
                                entity.OtherQualifiedRate = Math.Round(entity.OtherQualifiedCount.Value / entity.OtherSamplesCount.Value, 2) * 100;
                            }
                            break;
                        default:
                            break;
                    }
                }
                //ZhiFang.Common.Log.Log.Debug("GroupBySCRecordTypeList.foreach.End");
                entity.SumSamplesCount = groupByDept.Count();
                entity.SumQualifiedCount = groupByDept.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                if (entity.SumSamplesCount.HasValue && entity.SumSamplesCount > 0)
                {
                    entity.SumQualifiedRate = Math.Round(entity.SumQualifiedCount.Value / entity.SumSamplesCount.Value, 2) * 100;
                }

                tempEntityList.list.Add(entity);
            }
            //ZhiFang.Common.Log.Log.Debug("SearchGKListByHQLInfectionOfDept.SearchGKSampleRequestFormAndDtlByHQL.GroupByDeptList.foreach.End");
            tempEntityList.count = tempEntityList.list.Count;

            //分页处理
            if (tempEntityList.list.Count > 0)
            {
                if (limit > 0 && limit < tempEntityList.list.Count)
                {
                    tempEntityList.list = tempEntityList.list.OrderBy(s => s.DispOrder).ToList();
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = tempEntityList.list.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        tempEntityList.list = list.ToList();
                    }
                }
            }

            excelList.Clear();

            return tempEntityList;
        }
        public EntityList<GKOfDeptEvaluation> SearchGKListByHQLInfectionOfQuarterly(string docVO, string where, string sort, int page, int limit)
        {
            EntityList<GKOfDeptEvaluation> tempEntityList = new EntityList<GKOfDeptEvaluation>();
            tempEntityList.list = new List<GKOfDeptEvaluation>();
            string year = "";

            JObject jsonObjVO = null;
            if (string.IsNullOrEmpty(docVO)) return tempEntityList;

            jsonObjVO = JObject.Parse(docVO);
            year = jsonObjVO["Year"].ToString();

            if (string.IsNullOrEmpty(year)) return tempEntityList;

            if (string.IsNullOrEmpty(where))
                where = " 1=1 ";
            //评估标志为已评估的才统计
            if (!where.Contains("gksamplerequestform.EvaluatorFlag"))
                where += " and gksamplerequestform.EvaluatorFlag=1 ";

            if (!string.IsNullOrEmpty(year))
                where += string.Format(" and gksamplerequestform.DataAddTime>='{0}' and gksamplerequestform.DataAddTime<='{1}'", year + "-01-01 00:00:00", year + "-12-30 23:59:59");

            IList<GKSampleRequestForm> excelList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, false, false).list;

            //第一季度
            DateTime startDate1 = DateTime.Parse(year + "-01-01 00:00:00");
            DateTime endDate2 = DateTime.Parse(year + "-03-31 23:59:59");
            var list1 = excelList.Where(p => p.DataAddTime >= startDate1 && p.DataAddTime <= endDate2).ToList();
            GKOfDeptEvaluation entity = new GKOfDeptEvaluation();
            GetInfectionOfQuarterly(list1, ref entity);
            entity.DispOrder = 1;
            entity.Quarterly = "1-3月";
            tempEntityList.list.Add(entity);

            //第二季度
            startDate1 = DateTime.Parse(year + "-04-01 00:00:00");
            endDate2 = DateTime.Parse(year + "-06-30 23:59:59");
            var list2 = excelList.Where(p => p.DataAddTime >= startDate1 && p.DataAddTime <= endDate2).ToList();
            GKOfDeptEvaluation entity2 = new GKOfDeptEvaluation();
            GetInfectionOfQuarterly(list2, ref entity2);
            entity2.DispOrder = 2;
            entity2.Quarterly = "4-6月";
            tempEntityList.list.Add(entity2);

            //第三季度
            startDate1 = DateTime.Parse(year + "-07-01 00:00:00");
            endDate2 = DateTime.Parse(year + "-09-30 23:59:59");
            var list3 = excelList.Where(p => p.DataAddTime >= startDate1 && p.DataAddTime <= endDate2).ToList();
            GKOfDeptEvaluation entity3 = new GKOfDeptEvaluation();
            GetInfectionOfQuarterly(list3, ref entity3);
            entity3.DispOrder = 3;
            entity3.Quarterly = "7-9月";
            tempEntityList.list.Add(entity3);

            //第四季度
            startDate1 = DateTime.Parse(year + "-10-01 00:00:00");
            endDate2 = DateTime.Parse(year + "-12-30 23:59:59");
            var list4 = excelList.Where(p => p.DataAddTime >= startDate1 && p.DataAddTime <= endDate2).ToList();
            GKOfDeptEvaluation entity4 = new GKOfDeptEvaluation();
            GetInfectionOfQuarterly(list4, ref entity4);
            entity4.DispOrder = 4;
            entity4.Quarterly = "10-12月";
            tempEntityList.list.Add(entity4);

            //全年
            GKOfDeptEvaluation entity5 = new GKOfDeptEvaluation();
            GetInfectionOfQuarterly(excelList, ref entity5);
            entity5.DispOrder = 5;
            entity5.Quarterly = "全年";
            tempEntityList.list.Add(entity5);

            excelList.Clear();

            return tempEntityList;
        }
        private void GetInfectionOfQuarterly(IList<GKSampleRequestForm> excelList, ref GKOfDeptEvaluation entity)
        {
            var groupByList2 = excelList.GroupBy(p => p.SCRecordType.Id);
            foreach (var groupBy2 in groupByList2)
            {
                switch (groupBy2.Key)
                {
                    case 11://手卫生
                        entity.HandHSamplesCount = groupBy2.Count();
                        entity.HandHQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.HandHSamplesCount.HasValue && entity.HandHSamplesCount > 0)
                        {
                            entity.HandHQualifiedRate = Math.Round(entity.HandHQualifiedCount.Value / entity.HandHSamplesCount.Value, 2) * 100;
                        }
                        break;
                    case 12://空气培养
                        entity.AirCSamplesCount = groupBy2.Count();
                        entity.AirCQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.AirCSamplesCount.HasValue && entity.AirCSamplesCount > 0)
                        {
                            entity.AirCQualifiedRate = Math.Round(entity.AirCQualifiedCount.Value / entity.AirCSamplesCount.Value, 2) * 100;
                        }
                        break;
                    case 13://物体表面
                        entity.SurfaceSamplesCount = groupBy2.Count();
                        entity.SurfaceQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.SurfaceSamplesCount.HasValue && entity.SurfaceSamplesCount > 0)
                        {
                            entity.SurfaceQualifiedRate = Math.Round(entity.SurfaceQualifiedCount.Value / entity.SurfaceSamplesCount.Value, 2) * 100;
                        }
                        break;

                    case 14://消毒剂
                        entity.DisinfectantSamplesCount = groupBy2.Count();
                        entity.DisinfectantQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.DisinfectantSamplesCount.HasValue && entity.DisinfectantSamplesCount > 0)
                        {
                            entity.DisinfectantQualifiedRate = Math.Round(entity.DisinfectantQualifiedCount.Value / entity.DisinfectantSamplesCount.Value, 2) * 100;
                        }
                        break;
                    case 15://透析液及透析用水
                        entity.DialysateSamplesCount = groupBy2.Count();
                        entity.DialysateQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.DialysateSamplesCount.HasValue && entity.DialysateSamplesCount > 0)
                        {
                            entity.DialysateQualifiedRate = Math.Round(entity.DialysateQualifiedCount.Value / entity.DialysateSamplesCount.Value, 2) * 100;
                        }
                        break;
                    case 16://医疗器材
                        entity.MedicalESamplesCount = groupBy2.Count();
                        entity.MedicalEQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.MedicalESamplesCount.HasValue && entity.MedicalESamplesCount > 0)
                        {
                            entity.MedicalEQualifiedRate = Math.Round(entity.MedicalEQualifiedCount.Value / entity.MedicalESamplesCount.Value, 2) * 100;
                        }
                        break;
                    case 17://污水
                        entity.SewageSamplesCount = groupBy2.Count();
                        entity.SewageQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.SewageSamplesCount.HasValue && entity.SewageSamplesCount > 0)
                        {
                            entity.SewageQualifiedRate = Math.Round(entity.SewageQualifiedCount.Value / entity.SewageSamplesCount.Value, 2) * 100;
                        }
                        break;
                    case 18://其他
                        entity.OtherSamplesCount = groupBy2.Count();
                        entity.OtherQualifiedCount = groupBy2.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
                        if (entity.OtherSamplesCount.HasValue && entity.OtherSamplesCount > 0)
                        {
                            entity.OtherQualifiedRate = Math.Round(entity.OtherQualifiedCount.Value / entity.OtherSamplesCount.Value, 2) * 100;
                        }
                        break;
                    default:
                        break;
                }
            }
            entity.SumSamplesCount = excelList.Count();
            entity.SumQualifiedCount = excelList.Where(p => p.Judgment == GKSampleFormJudgment.合格.Key).Count();
            if (entity.SumSamplesCount.HasValue && entity.SumSamplesCount > 0)
            {
                entity.SumQualifiedRate = Math.Round(entity.SumQualifiedCount.Value / entity.SumSamplesCount.Value, 2) * 100;
            }
            //return entity;
        }
        public EntityList<GKOfDeptEvaluation> SearchGKListByHQLInfectionOfEvaluation(string docVO, string where, string sort, int page, int count)
        {
            EntityList<GKOfDeptEvaluation> tempEntityList = new EntityList<GKOfDeptEvaluation>();
            tempEntityList.list = new List<GKOfDeptEvaluation>();

            EntityList<GKSampleRequestForm> excelList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, page, count, false, false);
            tempEntityList.count = excelList.count;

            foreach (var item in excelList.list)
            {
                GKOfDeptEvaluation vo = new GKOfDeptEvaluation();
                vo.MonitoringDate = item.DataAddTime.Value.ToString("MM-dd");
                vo.DeptCName = item.DeptCName;
                vo.TestResult = item.TestResult;

                if (item.Judgment == GKSampleFormJudgment.合格.Key || item.Judgment == "1" || item.Judgment == "true" || item.Judgment == "合格")
                {
                    vo.TestEvaluation = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.合格.Key].Name;
                }
                else if (item.Judgment == GKSampleFormJudgment.不合格.Key || item.Judgment == "0" || item.Judgment == "false" || item.Judgment == "不合格")
                {
                    vo.TestEvaluation = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.不合格.Key].Name;
                }
                else
                {
                    vo.TestEvaluation = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.未评估.Key].Name;
                }
                vo.TestCName = item.Sampler;
                vo.Memo = item.Memo;

                IList<SCRecordDtl> dtlList = IDSCRecordDtlDao.GetListByHQL("screcorddtl.BObjectID=" + item.Id);
                SCRecordDtl dtl = dtlList.OrderBy(p => p.DispOrder).ElementAt(0);

                //医疗器材名称:取的是监测类型的第一项记录项显示名称;
                if (dtl != null) vo.MedicalEquipment = dtl.ItemResult;
                //场所为空气培养，取房间记录项名称,记录项编码为“601041”
                if (item.SCRecordType.Id == 12)
                {
                    vo.Place = dtl.ItemResult;
                }

                tempEntityList.list.Add(vo);
            }

            excelList.list.Clear();

            return tempEntityList;
        }
        #endregion

        #region PDF/Excel
        public Stream SearchGKSampleRequestFormOfPdfByHQL(long labId, string labCName, string breportType, string groupType, string docVO, string where, string sort, string frx, ref string fileName)
        {
            Stream stream = null;
            if (string.IsNullOrEmpty(sort)) sort = "";

            if (string.IsNullOrWhiteSpace(docVO) && string.IsNullOrWhiteSpace(where))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }

            JObject jsonObjVO = null;
            if (!string.IsNullOrEmpty(docVO)) jsonObjVO = JObject.Parse(docVO);

            ExportExcelCommand excelCommand = ToExcelHelpByBLL.CreateExportExcelCommand("", "");
            InfectionDocVO docVO2 = GetInfectionDocVO(jsonObjVO);
            string fileName2 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string pdfFileName = "";
            if (groupType == GKBarcodeReportType.院感登记清单.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "院感登记清单.frx";
                pdfFileName = " 院感登记清单" + "_" + fileName2 + ".pdf";
                fileName = " 院感登记清单.pdf";
                IList<GKSampleRequestForm> dtlList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, false).list;
                excelCommand.EEC_RecTotal = dtlList.Count;
                stream = CreatePdfReportOfFrxByGKForm(labId, labCName, excelCommand, docVO2, dtlList, frx, pdfFileName);
            }
            else if (groupType == GKBarcodeReportType.院感标本送检清单.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "院感标本送检清单.frx";
                pdfFileName = " 院感标本送检清单" + "_" + fileName2 + ".pdf";
                fileName = " 院感标本送检清单.pdf";
                IList<GKSampleRequestForm> dtlList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, true).list;
                excelCommand.EEC_RecTotal = dtlList.Count;
                stream = CreatePdfReportOfFrxByGKForm(labId, labCName, excelCommand, docVO2, dtlList, frx, pdfFileName);
            }
            else if (groupType == GKBarcodeReportType.环境卫生学监测报告.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "环境卫生学监测报告.frx";
                pdfFileName = " 环境卫生学监测报告" + "_" + fileName2 + ".pdf";
                fileName = " 环境卫生学监测报告.pdf";
                IList<GKSampleRequestForm> dtlList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, false).list;
                excelCommand.EEC_RecTotal = dtlList.Count;
                stream = CreatePdfReportOfFrxByGKForm(labId, labCName, excelCommand, docVO2, dtlList, frx, pdfFileName);
            }
            else if (groupType == GKBarcodeReportType.环境卫生学监测报告按监测类型分组.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "环境卫生学监测报告按监测类型分组.frx";
                pdfFileName = " 环境卫生学监测报告" + "_" + fileName2 + ".pdf";
                fileName = " 环境卫生学监测报告.pdf";
                IList<GKSampleRequestForm> dtlList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, false).list;
                excelCommand.EEC_RecTotal = dtlList.Count;
                stream = CreatePdfReportOfFrxByGKForm(labId, labCName, excelCommand, docVO2, dtlList, frx, pdfFileName);
            }
            else if (groupType == GKBarcodeReportType.按科室.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "按科室.frx";
                pdfFileName = " 按科室" + "_" + fileName2 + ".pdf";
                fileName = " 按科室.pdf";
                IList<GKOfDeptEvaluation> dtlList = this.SearchGKListByHQLInfectionOfDept(docVO, where, sort, -1, -1).list;
                excelCommand.EEC_RecTotal = dtlList.Count;
                stream = CreatePdfReportOfFrxByDocVO(labId, labCName, excelCommand, docVO2, dtlList, frx, pdfFileName);
            }
            else if (groupType == GKBarcodeReportType.按季度.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "按季度.frx";
                pdfFileName = " 按季度" + "_" + fileName2 + ".pdf";
                fileName = " 按季度.pdf";
                IList<GKOfDeptEvaluation> dtlList = this.SearchGKListByHQLInfectionOfQuarterly(docVO, where, sort, -1, -1).list;
                excelCommand.EEC_RecTotal = dtlList.Count;
                stream = CreatePdfReportOfFrxByDocVO(labId, labCName, excelCommand, docVO2, dtlList, frx, pdfFileName);
            }
            else if (groupType == GKBarcodeReportType.评价报告表.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "评价报告表.frx";
                pdfFileName = " 评价报告表" + "_" + fileName2 + ".pdf";
                fileName = " 评价报告表.pdf";
                IList<GKOfDeptEvaluation> dtlList = this.SearchGKListByHQLInfectionOfEvaluation(docVO, where, sort, -1, -1).list;
                excelCommand.EEC_RecTotal = dtlList.Count;

                docVO2.SumSamplesCount = dtlList.Count();
                docVO2.SumQualifiedCount = dtlList.Where(p => p.TestEvaluation == GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.合格.Key].Name).Count();
                if (docVO2.SumSamplesCount.HasValue && docVO2.SumSamplesCount > 0)
                {
                    docVO2.SumQualifiedRate = Math.Round(docVO2.SumQualifiedCount.Value / docVO2.SumSamplesCount.Value, 2) * 100;
                }
                docVO2.HygieneInfo = string.Format("医务人员{0}人，合格{1}人，合格率{2}%。", docVO2.SumSamplesCount, docVO2.SumQualifiedCount, docVO2.SumQualifiedRate);
                stream = CreatePdfReportOfFrxByDocVO(labId, labCName, excelCommand, docVO2, dtlList, frx, pdfFileName);
            }
            //fileName = pdfFileName;

            return stream;
        }
        private Stream CreatePdfReportOfFrxByGKForm(long labId, string labCName, ExportExcelCommand excelCommand, InfectionDocVO docVO, IList<GKSampleRequestForm> dtlList, string frx, string pdfFileName)
        {
            Stream stream = null;

            var groupByList = dtlList.GroupBy(p => p.DeptId);
            if (groupByList.Count() == 1)
            {
                docVO.DeptCName = dtlList[0].DeptCName;
            }
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.WebAssist";

            List<InfectionDocVO> docList = new List<InfectionDocVO>();
            docList.Add(docVO);

            DataTable docDt = ReportBTemplateHelp.ToDataTable<InfectionDocVO>(docList, null);
            docDt.TableName = "InfectionDocVO";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<GKSampleRequestForm>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "GKSampleRequestForm";
                dataSet.Tables.Add(dtDtl);
            }

            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, pdfFileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.院感登记.Key].Name, frx, false);

            return stream;
        }
        private Stream CreatePdfReportOfFrxByDocVO(long labId, string labCName, ExportExcelCommand excelCommand, InfectionDocVO docVO, IList<GKOfDeptEvaluation> dtlList, string frx, string pdfFileName)
        {
            var groupByList = dtlList.GroupBy(p => p.DeptId);
            if (groupByList.Count() == 1)
            {
                docVO.DeptCName = dtlList[0].DeptCName;
            }
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.WebAssist";

            List<InfectionDocVO> docList = new List<InfectionDocVO>();
            docList.Add(docVO);
            DataTable docDTVO = ReportBTemplateHelp.ToDataTable<InfectionDocVO>(docList, null);
            docDTVO.TableName = "InfectionDocVO";
            dataSet.Tables.Add(docDTVO);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<GKOfDeptEvaluation>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "GKOfDeptEvaluation";
                dataSet.Tables.Add(dtDtl);
            }

            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, pdfFileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.院感登记.Key].Name, frx, false);

            return stream;
        }
        public Stream SearchGKSampleRequestFormOfExcelByHql(long labId, string labCName, string breportType, string groupType, string docVO, string where, string sort, string frx, ref string fileName)
        {
            Stream stream = null;
            if (string.IsNullOrEmpty(sort)) sort = "";

            JObject jsonObjVO = null;
            if (!string.IsNullOrEmpty(docVO)) jsonObjVO = JObject.Parse(docVO);

            string saveFullPath = "";
            ExportExcelCommand excelCommand = ToExcelHelpByBLL.CreateExportExcelCommand("", "");
            InfectionDocVO docVO2 = GetInfectionDocVO(jsonObjVO);
            string fileName2 = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string fileExt = frx.Substring(frx.LastIndexOf("."));

            if (groupType == GKBarcodeReportType.院感登记清单.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "院感登记清单.xlsx";
                string excelFile = fileName2 + "_" + frx;

                IList<GKSampleRequestForm> excelList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, false).list;
                excelCommand.EEC_RecTotal = excelList.Count;
                stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<InfectionDocVO, GKSampleRequestForm>(docVO2, excelList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            }
            else if (groupType == GKBarcodeReportType.院感标本送检清单.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "院感标本送检清单.xlsx";
                string excelFile = fileName2 + "_" + frx;
                IList<GKSampleRequestForm> excelList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, true).list;
                excelCommand.EEC_RecTotal = excelList.Count;
                stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<InfectionDocVO, GKSampleRequestForm>(docVO2, excelList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            }
            else if (groupType == GKBarcodeReportType.环境卫生学监测报告.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "环境卫生学监测报告.xlsx";
                string excelFile = fileName2 + "_" + frx;
                IList<GKSampleRequestForm> excelList = this.SearchGKSampleRequestFormAndDtlByHQL(where, sort, -1, -1, false).list;
                excelCommand.EEC_RecTotal = excelList.Count;
                stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<InfectionDocVO, GKSampleRequestForm>(docVO2, excelList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            }
            else if (groupType == GKBarcodeReportType.按科室.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "按科室.xlsx";
                string excelFile = fileName2 + "_" + frx;
                IList<GKOfDeptEvaluation> excelList = this.SearchGKListByHQLInfectionOfDept(docVO, where, sort, -1, -1).list;
                excelCommand.EEC_RecTotal = excelList.Count;
                stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<InfectionDocVO, GKOfDeptEvaluation>(docVO2, excelList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            }
            else if (groupType == GKBarcodeReportType.按季度.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "按季度.xlsx";
                string excelFile = fileName2 + "_" + frx;
                IList<GKOfDeptEvaluation> excelList = this.SearchGKListByHQLInfectionOfQuarterly(docVO, where, sort, -1, -1).list;
                excelCommand.EEC_RecTotal = excelList.Count;
                stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<InfectionDocVO, GKOfDeptEvaluation>(docVO2, excelList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            }
            else if (groupType == GKBarcodeReportType.评价报告表.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "评价报告表.xlsx";
                string excelFile = fileName2 + "_" + frx;
                IList<GKOfDeptEvaluation> excelList = this.SearchGKListByHQLInfectionOfEvaluation(docVO, where, sort, -1, -1).list;
                excelCommand.EEC_RecTotal = excelList.Count;

                docVO2.SumSamplesCount = excelList.Count();
                docVO2.SumQualifiedCount = excelList.Where(p => p.TestEvaluation == GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.合格.Key].Name).Count();
                if (docVO2.SumSamplesCount.HasValue && docVO2.SumSamplesCount > 0)
                {
                    docVO2.SumQualifiedRate = Math.Round(docVO2.SumQualifiedCount.Value / docVO2.SumSamplesCount.Value, 2) * 100;
                }
                //
                docVO2.HygieneInfo = string.Format("医务人员{0}人，合格{1}人，合格率{2}%。", docVO2.SumSamplesCount, docVO2.SumQualifiedCount, docVO2.SumQualifiedRate);
                stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<InfectionDocVO, GKOfDeptEvaluation>(docVO2, excelList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            }
            //string fileExt = frx.Substring(frx.LastIndexOf("."));
            fileName = frx;// orderDoc.DeptName + fileExt;

            return stream;
        }

        private InfectionDocVO GetInfectionDocVO(JObject jsonObjVO)
        {
            InfectionDocVO vo = new InfectionDocVO();

            #region 监测级别
            var monitorType = "";
            JToken jtoken = null;
            jsonObjVO.TryGetValue("MonitorType", out jtoken);
            if (jtoken != null) monitorType = jsonObjVO["MonitorType"].ToString();

            if (monitorType == MonitorType.感控监测.Key)
            {
                vo.MonitorType = "□全院   ■院感科监测   □科室监测";
            }
            else if (monitorType == MonitorType.科室监测.Key)
            {
                vo.MonitorType = "□全院   □院感科监测   ■科室监测";
            }
            else
            {
                vo.MonitorType = "■全院   □院感科监测   □科室监测";
            }
            #endregion

            #region 监测类型处理
            var recordTypeCName = "";
            jsonObjVO.TryGetValue("RecordTypeCName", out jtoken);
            if (jtoken != null) recordTypeCName = jsonObjVO["RecordTypeCName"].ToString();

            if (string.IsNullOrEmpty(recordTypeCName))
            {
                var recordTypeNo = "";
                jsonObjVO.TryGetValue("RecordTypeNo", out jtoken);
                if (jtoken != null) recordTypeNo = jsonObjVO["RecordTypeNo"].ToString();
                if (!string.IsNullOrEmpty(recordTypeNo))
                {
                    long id2 = -1;
                    long.TryParse(recordTypeNo, out id2);
                    SCRecordType recordType = IDSCRecordTypeDao.Get(id2);
                    if (recordType != null) vo.RecordTypeCName = recordType.CName;
                }
            }
            #endregion

            #region 日期时间处理
            jsonObjVO.TryGetValue("Year", out jtoken);
            //统计年
            if (jtoken != null) vo.Year = jsonObjVO["Year"].ToString();

            jsonObjVO.TryGetValue("Year", out jtoken);
            if (jtoken != null) vo.StartDate = jsonObjVO["StartDate"].ToString();
            jsonObjVO.TryGetValue("Year", out jtoken);
            if (jtoken != null) vo.EndDate = jsonObjVO["EndDate"].ToString();

            //时间范围
            if (!string.IsNullOrEmpty(vo.StartDate) && !string.IsNullOrEmpty(vo.EndDate))
            {
                vo.DateRange = vo.StartDate + "-" + vo.EndDate;
            }

            //统计年月 2020年1-9月
            if (!string.IsNullOrEmpty(vo.Year) && !string.IsNullOrEmpty(vo.StartDate) && !string.IsNullOrEmpty(vo.EndDate))
            {
                vo.YMRange = vo.Year + "年" + vo.StartDate.Split('-')[1] + "-" + vo.EndDate.Split('-')[1] + "月";
            }

            //季度处理
            if (!string.IsNullOrEmpty(vo.YMRange))
            {
                if (vo.YMRange.Contains("01-03月"))
                {
                    vo.Quarterly = "第一季度";
                }
                else if (vo.YMRange.Contains("04-06月"))
                {
                    vo.Quarterly = "第二季度";
                }
                else if (vo.YMRange.Contains("07-09月"))
                {
                    vo.Quarterly = "第三季度";
                }
                else if (vo.YMRange.Contains("010-12月"))
                {
                    vo.Quarterly = "第四季度";
                }
            }
            //统计年加季度
            if (!string.IsNullOrEmpty(vo.Year) && !string.IsNullOrEmpty(vo.Quarterly))
            {
                vo.YearQuarterly = vo.Year + " " + vo.Quarterly;
            }

            #endregion

            return vo;
        }

        #endregion

    }
}