using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;
using System.Text.RegularExpressions;


namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisEquipForm : BaseBLL<LisEquipForm>, ZhiFang.IBLL.LabStar.IBLisEquipForm
    {
        public SysCookieValue SysCookieValue { get; set; }

        IBLBItem IBLBItem { get; set; }
        IBLBEquip IBLBEquip { get; set; }
        IBLBSection IBLBSection { get; set; }
        IBLisTestForm IBLisTestForm { get; set; }
        IBLisTestItem IBLisTestItem { get; set; }
        IBLisEquipItem IBLisEquipItem { get; set; }
        IBLisEquipGraph IBLisEquipGraph { get; set; }
        IBLisTestGraph IBLisTestGraph { get; set; }
        IBLisOperate IBLisOperate { get; set; }
        IBLBRight IBLBRight { get; set; }
        IBLisEquipComLog IBLisEquipComLog { get; set; }
        IBLBEquipResultTH IBLBEquipResultTH { get; set; }
        IBLBEquipSection IBLBEquipSection { get; set; }

        public IList<LisEquipForm> QueryLisEquipForm(string strHqlWhere, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            return (DBDao as IDLisEquipFormDao).QueryLisEquipFormDao(strHqlWhere, listEntityName);
        }

        public EntityList<LisEquipForm> QueryLisEquipForm(string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            return (DBDao as IDLisEquipFormDao).QueryLisEquipFormDao(strHqlWhere, order, start, count, listEntityName);
        }

        ///// <summary>
        ///// 仪器结果上传存储
        ///// </summary>
        ///// <param name="labID">实验室ID</param>
        ///// <param name="equipResultType">仪器结果类型</param>
        ///// <param name="equipResultInfo">仪器结果Json字符串</param>
        ///// <returns></returns>
        //public BaseResultDataValue AppendLisEquipItemResultByUpLoadInfo(long labID, string equipResultType, string equipResultInfo, SendSysMessageDelegate sendCommDataMsg)
        //{
        //    BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
        //    if (string.IsNullOrWhiteSpace(equipResultInfo))
        //    {
        //        baseResultDataValue.success = false;
        //        baseResultDataValue.Code = -1;
        //        baseResultDataValue.ErrorInfo = "错误信息：上传的仪器检验结果文件信息为空！";
        //        return baseResultDataValue;
        //    }

        //    if (equipResultType == "Common_Result")
        //    {
        //        IList<EquipResult> listEquipResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipResult>>(equipResultInfo);
        //        baseResultDataValue = AddLisEquipItemResult_Common(labID, listEquipResult, sendCommDataMsg);
        //    }
        //    else if (equipResultType == "Memo_Result")
        //    {
        //        IList<EquipMemoResult> listEquipMemoResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipMemoResult>>(equipResultInfo);
        //        if (listEquipMemoResult != null && listEquipMemoResult.Count > 0)
        //        {
        //            baseResultDataValue = AddLisEquipItemResult_Memo(labID, listEquipMemoResult, sendCommDataMsg);
        //        }
        //    }
        //    else if (equipResultType == "Graph_Result")
        //    {
        //        IList<EquipGraphResult> listEquipGraphResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipGraphResult>>(equipResultInfo);
        //        if (listEquipGraphResult != null && listEquipGraphResult.Count > 0)
        //        {
        //            baseResultDataValue = AppendLisEquipItemResult_Graph(labID, listEquipGraphResult, sendCommDataMsg);
        //        }
        //    }
        //    else if (equipResultType == "QC_Result")
        //    {
        //        IList<EquipQCResult> listEquipQCResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<IList<EquipQCResult>>(equipResultInfo);
        //        if (listEquipQCResult != null && listEquipQCResult.Count > 0)
        //        {
        //            baseResultDataValue = AddLisEquipItemResult_QC(labID, listEquipQCResult, sendCommDataMsg);
        //        }
        //    }
        //    return baseResultDataValue;
        //}

        public BaseResultDataValue JudgeCommResultValidity(IList<EquipResult> listEquipResult)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            return baseResultDataValue;
        }

        /// <summary>
        /// 常规检验结果上传处理
        /// </summary>
        /// <param name="labID">实验室ID</param>
        /// <param name="listEquipResult">检验结果实体列表</param>
        /// <param name="computerInfo">客户端相关信息</param>
        /// <param name="sendCommDataMsg">消息委托</param>
        /// <returns></returns>
        public BaseResultDataValue AddLisEquipItemResult_Common(long labID, IList<EquipResult> listEquipResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listEquipResult != null && listEquipResult.Count > 0)
            {
                #region 检验结果分组,分组目的是查询存在多少个不同样本号
                var listGroupByItem = from EquipResult in listEquipResult
                                      group EquipResult by
                                  new
                                  {
                                      EquipNo = EquipResult.EquipNo,
                                      ReceiveDate = EquipResult.ReceiveDate,
                                      TestTypeNo = EquipResult.TestTypeNo,
                                      SampleNo = EquipResult.SampleNo,
                                      SectionNo = EquipResult.SectionNo,
                                      SerialNo = EquipResult.SerialNo
                                  } into g
                                      select new EquipResultGroupBy()
                                      {
                                          EquipNo = g.Key.EquipNo,
                                          ReceiveDate = g.Key.ReceiveDate,
                                          TestTypeNo = g.Key.TestTypeNo,
                                          SampleNo = g.Key.SampleNo,
                                          SectionNo = g.Key.SectionNo,
                                          SerialNo = g.Key.SerialNo,
                                          LongGUID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong()
                                      };
                IList<EquipResultGroupBy> listEquipResultGroupBy = listGroupByItem.ToList();
                #endregion

                Dictionary<long, LBEquip> dicLBEquip = new Dictionary<long, LBEquip>();
                Dictionary<long, LBSection> dicLBSection = new Dictionary<long, LBSection>();
                Dictionary<long, IList<string>> dicEmpID = new Dictionary<long, IList<string>>();
                Dictionary<long, LisEquipForm> dicLisEquipForm = new Dictionary<long, LisEquipForm>();
                Dictionary<long, IList<LisEquipItem>> dicLisEquipItemList = new Dictionary<long, IList<LisEquipItem>>();
                IBLisEquipComLog.AddLisEquipComLog(0, "开始解释通讯文件：" + computerInfo.ComFileName + ",共有" + listEquipResult.Count + "条检验结果数据！", computerInfo);
                foreach (EquipResultGroupBy groupByItem in listEquipResultGroupBy)
                {
                    IList<string> listEmpID = null;
                    IBLisEquipComLog.AddLisEquipComLog(0, "解释通讯文件：" + computerInfo.ComFileName + ",样本号为【" + groupByItem.SampleNo + "】的检验结果数据！", computerInfo);

                    #region 根据仪器编码获取仪器信息
                    LBEquip lbEquip = null;
                    if (dicLBEquip.ContainsKey(groupByItem.EquipNo))
                        lbEquip = dicLBEquip[groupByItem.EquipNo];
                    else
                    {
                        IList<LBEquip> listLBEquip = IBLBEquip.SearchListByHQL(" lbequip.EquipNo=" + groupByItem.EquipNo);
                        if (listLBEquip == null || listLBEquip.Count == 0)
                            listLBEquip = null;
                        else
                            lbEquip = listLBEquip[0];
                        dicLBEquip.Add(groupByItem.EquipNo, lbEquip);
                    }
                    if (lbEquip == null)
                    {
                        IBLisEquipComLog.AddLisEquipComLog(0, "无法根据仪器编码找到对应的仪器信息，放弃解释此样本的检验数据！仪器编码：" + groupByItem.EquipNo, computerInfo);
                        continue;
                    }
                    computerInfo.SEquipID = lbEquip.Id;
                    computerInfo.SEquipName = lbEquip.CName;
                    #endregion

                    #region 根据小组编码获取小组信息
                    LBSection lbSection = null;
                    if (dicLBSection.ContainsKey(groupByItem.SectionNo))
                        lbSection = dicLBSection[groupByItem.SectionNo];
                    else if (lbEquip.LBSection != null)
                    {
                        lbSection = lbEquip.LBSection;
                        dicLBSection.Add(groupByItem.SectionNo, lbSection);
                    }
                    else
                    {
                        IList<LBSection> listLBSection = IBLBSection.SearchListByHQL(" lbsection.SectionNo=" + groupByItem.SectionNo);
                        if (listLBSection == null || listLBSection.Count == 0)
                            listLBSection = null;
                        else
                        {
                            lbSection = listLBSection[0];
                            dicLBSection.Add(groupByItem.SectionNo, lbSection);
                        }
                    }
                    if (lbSection == null)
                    {
                        IBLisEquipComLog.AddLisEquipComLog(0, "无法根据小组编码找到对应的小组信息，放弃解释此样本的检验数据！小组编码：" + groupByItem.SectionNo, computerInfo);
                        continue;
                    }
                    else
                    {
                        if (dicEmpID.ContainsKey(lbSection.Id))
                            listEmpID = dicEmpID[lbSection.Id];
                        else
                        {
                            listEmpID = IBLBRight.QueryEmpIDBySectionID(lbSection.Id);
                            if (listEmpID != null && listEmpID.Count > 0)
                            {
                                listEmpID = listEmpID.Distinct().ToList();
                                dicEmpID.Add(lbSection.Id, listEmpID);
                            }
                        }
                    }
                    computerInfo.SSectionID = lbSection.Id;
                    computerInfo.SSectionName = lbSection.CName;
                    #endregion

                    try
                    {
                        IList<LisEquipForm> listLisEquipForm = QueryLisEquipFormByEquipResultInfo(lbEquip.Id, (DateTime)groupByItem.ReceiveDate, groupByItem.SerialNo, groupByItem.SampleNo, computerInfo);

                        LisEquipForm lisEquipForm = null;
                        LisTestForm testForm = null;
                        if (listLisEquipForm == null || listLisEquipForm.Count == 0)
                        {
                            lisEquipForm = new LisEquipForm();
                            IBLisEquipComLog.AddLisEquipComLog(0, "仪器样本单不存在，新增仪器样本单！ID:" + lisEquipForm.Id, computerInfo);
                            lisEquipForm.LabID = labID;
                            lisEquipForm.LBEquip = lbEquip;
                            lisEquipForm.ETestDate = groupByItem.ReceiveDate;
                            lisEquipForm.ESampleNo = groupByItem.SampleNo;
                            lisEquipForm.EBarCode = groupByItem.SerialNo;
                            lisEquipForm.ESampleNoForOrder = LisCommonMethod.DisposeSampleNo(lisEquipForm.ESampleNo);
                            int tempFlag = AddLisTestFormByLisEquipForm(lisEquipForm, lbSection, computerInfo, listEmpID, sendCommDataMsg, ref testForm);
                            lisEquipForm.LisTestForm = testForm;
                            if (tempFlag == -1)//仪器样本单对应多个检验样本单
                                continue;

                            this.Entity = lisEquipForm;
                            if (this.Add())
                                IBLisEquipComLog.AddLisEquipComLog(0, "新增仪器样本单！ID:" + lisEquipForm.Id, computerInfo);
                        }
                        else
                        {
                            lisEquipForm = listLisEquipForm[0];
                            IBLisEquipComLog.AddLisEquipComLog(0, "仪器样本单已存在，修改仪器样本单！ID:" + lisEquipForm.Id, computerInfo);
                            if (lisEquipForm.LabID <= 0)
                                lisEquipForm.LabID = labID;
                            lisEquipForm.IExamine += 1;
                            lisEquipForm.IsExamined = true;
                            lisEquipForm.DataUpdateTime = DateTime.Now;
                            if (lisEquipForm.LisTestForm == null)
                            {
                                int tempFlag = AddLisTestFormByLisEquipForm(lisEquipForm, lbSection, computerInfo, listEmpID, sendCommDataMsg, ref testForm);
                                lisEquipForm.LisTestForm = testForm;
                                if (tempFlag == -1)//仪器样本单对应多个检验样本单
                                    continue;
                            }
                            this.Entity = lisEquipForm;
                            this.Edit();
                        }
                        dicLisEquipForm.Add(groupByItem.LongGUID, lisEquipForm);

                        #region 解释检验项目结果并生成仪器项目结果
                        IList<LisEquipItem> tempLisEquipItemList = new List<LisEquipItem>();
                        dicLisEquipItemList.Add(groupByItem.LongGUID, tempLisEquipItemList);
                        IList<EquipResult> tempEquipResultList = listEquipResult.Where(p => p.ReceiveDate == groupByItem.ReceiveDate).ToList();
                        Dictionary<long, LBItem> dicLBItem = new Dictionary<long, LBItem>();
                        foreach (var equipResult in tempEquipResultList)
                        {
                            LBItem lbItem = null;
                            if (dicLBItem.ContainsKey(equipResult.ItemNo))
                                lbItem = dicLBItem[equipResult.ItemNo];
                            else
                            {
                                IList<LBItem> listLBItem = IBLBItem.SearchListByHQL(" lbitem.ItemNo=" + equipResult.ItemNo);
                                if (listLBItem == null || listLBItem.Count == 0)
                                    lbItem = null;
                                else
                                    lbItem = listLBItem[0];
                                dicLBItem.Add(equipResult.ItemNo, lbItem);
                            }
                            if (lbItem == null)
                            {
                                IBLisEquipComLog.AddLisEquipComLog(0, "根据项目编码找不到对应的检验项目！ItemNo:" + equipResult.ItemNo, computerInfo);
                                continue;
                            }

                            IList<LisEquipItem> listLisEquipItem = IBLisEquipItem.SearchListByHQL(" lisequipitem.LisEquipForm.Id=" + lisEquipForm.Id + " and lisequipitem.LBItem.Id=" + lbItem.Id);

                            LisEquipItem lisEquipItem = new LisEquipItem();
                            lisEquipItem.LabID = labID;
                            lisEquipItem.LisEquipForm = lisEquipForm;
                            lisEquipItem.BItemResultFlag = true;
                            if (listLisEquipItem != null && listLisEquipItem.Count > 0)
                            {
                                listLisEquipItem = listLisEquipItem.OrderByDescending(p => p.IExamine).ToList();
                                lisEquipItem.IExamine = listLisEquipItem[0].IExamine + 1;
                                IBLisEquipComLog.AddLisEquipComLog(0, "检验项目已存在检验结果，该项目检验次数加1，项目名称:" + lbItem.CName, computerInfo);
                            }
                            else
                                lisEquipItem.IExamine = 1;
                            lisEquipItem.ETestDate = groupByItem.ReceiveDate;
                            lisEquipItem.LBItem = lbItem;
                            if (!string.IsNullOrWhiteSpace(equipResult.OriginalValue))
                            {
                                lisEquipItem.EReportValue = equipResult.OriginalValue;
                                lisEquipItem.EOriginalValue = equipResult.OriginalValue;
                            }
                            else
                            {
                                lisEquipItem.EReportValue = equipResult.OriginalDesc;
                                lisEquipItem.EOriginalValue = equipResult.OriginalDesc;
                            }
                            lisEquipItem.ETestDate = equipResult.ItemDate;
                            lisEquipItem.TestTime = equipResult.ItemTime;
                            IBLisEquipItem.Entity = lisEquipItem;
                            if (IBLisEquipItem.Add())
                            {
                                IBLisEquipComLog.AddLisEquipComLog(0, "新增仪器样本项目结果！项目名称:" + lbItem.CName, computerInfo);
                                if (listLisEquipItem != null && listLisEquipItem.Count > 0)
                                {
                                    IBLisEquipComLog.AddLisEquipComLog(0, "检验项目已存在检验结果的记录，采用标志置为否，项目名称:" + lbItem.CName, computerInfo);
                                    listLisEquipItem = listLisEquipItem.Where(p => p.BItemResultFlag).ToList();
                                    //仪器旧项目结果的采用标志置为否
                                    foreach (LisEquipItem equipItem in listLisEquipItem)
                                    {
                                        equipItem.BItemResultFlag = false;
                                        IBLisEquipItem.Entity = equipItem;
                                        IBLisEquipItem.Edit();
                                    }
                                }
                            }
                            tempLisEquipItemList.Add(lisEquipItem);
                        }
                        #endregion 
                    }
                    catch (Exception ex)
                    {
                        SendEquipResultMsgVOMSG(-1, lbEquip, groupByItem, "检验数据导入", ex.Message, listEmpID, computerInfo, sendCommDataMsg);
                    }
                }

                foreach (KeyValuePair<long, LisEquipForm> kv in dicLisEquipForm)
                {
                    LisEquipForm equipForm = kv.Value;
                    LisTestForm testForm = kv.Value.LisTestForm;
                    IList<string> listEmpID = null;
                    if (dicEmpID.ContainsKey(testForm.LBSection.Id))
                        listEmpID = dicEmpID[testForm.LBSection.Id];
                    try
                    {
                        AddLisItemResultByEquipResult(kv.Value.LisTestForm, null, kv.Value, dicLisEquipItemList[kv.Key], "", false, false, false, null, "", "", computerInfo);
                        SendEquipResultMsgVOMSG(0, equipForm, testForm, "检验数据导入", "", listEmpID, computerInfo, sendCommDataMsg);
                    }
                    catch (Exception ex)
                    {
                        SendEquipResultMsgVOMSG(-1, equipForm, testForm, "检验数据导入", ex.Message, listEmpID, computerInfo, sendCommDataMsg);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.Code = -2;
                baseResultDataValue.ErrorInfo = "上传的仪器常规检验结果列表为空！";

            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddLisEquipItemResult_Memo(long comFileID, long labID, IList<EquipMemoResult> listEquipResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listEquipResult != null && listEquipResult.Count > 0)
            {
                IBLisEquipComLog.AddLisEquipComLog(0, "开始解释通讯文件：" + computerInfo.ComFileName + ",共有" + listEquipResult.Count + "条检验数据！", computerInfo);

                #region 分组 
                var listGroupByItem = from EquipResult in listEquipResult
                                      group EquipResult by
                                  new
                                  {
                                      ReceiveDate = EquipResult.ReceiveDate,
                                      TestTypeNo = EquipResult.TestTypeNo,
                                      SampleNo = EquipResult.SampleNo,
                                      SectionNo = EquipResult.SectionNo,
                                      SerialNo = EquipResult.SerialNo
                                  } into g
                                      select new EquipResultGroupBy()
                                      {
                                          ReceiveDate = g.Key.ReceiveDate,
                                          TestTypeNo = g.Key.TestTypeNo,
                                          SampleNo = g.Key.SampleNo,
                                          SectionNo = g.Key.SectionNo,
                                          SerialNo = g.Key.SerialNo,
                                          LongGUID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong()
                                      };
                IList<EquipResultGroupBy> listEquipResultGroupBy = listGroupByItem.ToList();
                #endregion

                Dictionary<long, LBSection> dicLBSection = new Dictionary<long, LBSection>();
                Dictionary<long, IList<string>> dicEmpID = new Dictionary<long, IList<string>>();
                foreach (EquipResultGroupBy groupByItem in listEquipResultGroupBy)
                {
                    IBLisEquipComLog.AddLisEquipComLog(0, "解释通讯文件：" + computerInfo.ComFileName + ",样本号为【" + groupByItem.SampleNo + "】的检验结果数据！", computerInfo);
                    IList<string> listEmpID = null;
                    LBSection lbSection = null;
                    if (dicLBSection.ContainsKey(groupByItem.SectionNo))
                        lbSection = dicLBSection[groupByItem.SectionNo];
                    else
                    {
                        IList<LBSection> listLBSection = IBLBSection.SearchListByHQL(" lbsection.SectionNo=" + groupByItem.SectionNo);
                        if (listLBSection == null || listLBSection.Count == 0)
                            listLBSection = null;
                        else
                        {
                            lbSection = listLBSection[0];
                            dicLBSection.Add(groupByItem.SectionNo, lbSection);
                        }
                    }
                    if (lbSection == null)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("无法根据小组编码找到对应的小组信息！小组编码：" + groupByItem.SectionNo);
                        continue;
                    }
                    else
                    {
                        if (dicEmpID.ContainsKey(lbSection.Id))
                            listEmpID = dicEmpID[lbSection.Id];
                        else
                        {
                            listEmpID = IBLBRight.QueryEmpIDBySectionID(lbSection.Id);
                            if (listEmpID != null && listEmpID.Count > 0)
                            {
                                listEmpID = listEmpID.Distinct().ToList();
                                dicEmpID.Add(lbSection.Id, listEmpID);
                            }
                        }
                    }
                    try
                    {
                        IList<LisEquipForm> listLisEquipForm = QueryLisEquipFormByEquipResultInfo(0, (DateTime)groupByItem.ReceiveDate, groupByItem.SerialNo, groupByItem.SampleNo, computerInfo);

                        IList<EquipMemoResult> tempMemoResultList = listEquipResult.Where(p => p.ReceiveDate == groupByItem.ReceiveDate && p.SerialNo == groupByItem.SerialNo && p.SampleNo == groupByItem.SampleNo).ToList();

                        if (tempMemoResultList == null || tempMemoResultList.Count == 0)
                            continue;

                        LisEquipForm lisEquipForm = null;
                        LisTestForm testForm = null;
                        if (listLisEquipForm == null || listLisEquipForm.Count == 0)
                        {
                            lisEquipForm = new LisEquipForm();
                            lisEquipForm.LabID = labID;
                            lisEquipForm.ETestDate = groupByItem.ReceiveDate;
                            lisEquipForm.ESampleNo = groupByItem.SampleNo;
                            lisEquipForm.EBarCode = groupByItem.SerialNo;
                            lisEquipForm.EResultComment = tempMemoResultList[0].FormComment;
                            lisEquipForm.ESampleNoForOrder = LisCommonMethod.DisposeSampleNo(lisEquipForm.ESampleNo);
                            int tempFlag = AddLisTestFormByLisEquipForm(lisEquipForm, lbSection, computerInfo, listEmpID, sendCommDataMsg, ref testForm);
                            lisEquipForm.LisTestForm = testForm;
                            if (tempFlag == -1)//仪器样本单对应多个检验样本单
                                continue;
                            lisEquipForm.LisTestForm = testForm;
                            this.Entity = lisEquipForm;
                            this.Add();
                        }
                        else
                        {
                            lisEquipForm = listLisEquipForm[0];
                            if (lisEquipForm.LabID <= 0)
                                lisEquipForm.LabID = labID;
                            lisEquipForm.IExamine += 1;
                            lisEquipForm.IsExamined = true;
                            lisEquipForm.DataUpdateTime = DateTime.Now;
                            lisEquipForm.EResultComment = tempMemoResultList[0].FormComment;
                            if (lisEquipForm.LisTestForm == null)
                            {
                                int tempFlag = AddLisTestFormByLisEquipForm(lisEquipForm, lbSection, computerInfo, listEmpID, sendCommDataMsg, ref testForm);
                                lisEquipForm.LisTestForm = testForm;
                                if (tempFlag == -1)//仪器样本单对应多个检验样本单
                                    continue;
                            }
                            else
                                testForm = EditLisTestFormTestCommentByLisEquipForm(lisEquipForm, lisEquipForm.LisTestForm);
                            lisEquipForm.LisTestForm = testForm;
                            this.Entity = lisEquipForm;
                            this.Edit();
                        }
                        SendEquipResultMsgVOMSG(0, lisEquipForm, testForm, "检验数据导入", "", listEmpID, computerInfo, sendCommDataMsg);
                    }
                    catch(Exception ex)
                    {
                        SendEquipResultMsgVOMSG(-1, null, groupByItem, "检验数据导入", ex.Message, listEmpID, computerInfo, sendCommDataMsg);
                    }
                }
            }//
            return baseResultDataValue;
        }

        public BaseResultDataValue AddLisEquipItemResult_QC(long comFileID, long labID, IList<EquipQCResult> listEquipQCResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listEquipQCResult != null && listEquipQCResult.Count > 0)
            {
                string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("IEQAPlatformServiceUrl");
                if (string.IsNullOrEmpty(url))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "IEQA平台服务地址不能为空！";
                    ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
                    return baseResultDataValue;
                }
                else
                    url += "/ServerWCF/QCExternalService.svc/QC_ES_UpLoadEquipQCResultInfo";
                try
                {
                    if (listEquipQCResult != null && listEquipQCResult.Count > 0)
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;//日期序列化为“/Data()/”格式，否则无法反序列化
                        string StrJson = Newtonsoft.Json.JsonConvert.SerializeObject(listEquipQCResult, Newtonsoft.Json.Formatting.None, settings);
                        string para = "{\"labID\":\"" + labID + "\",\"listEquipQCResult\":" + StrJson + "}";
                        baseResultDataValue = ZhiFang.LabStar.Common.HTTPRequest.WebRequestHttpPost(url, para, "application/json", "");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "服务请求异常：" + ex.Message;
                    ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AppendLisEquipItemResult_Graph(long comFileID, long labID, IList<EquipGraphResult> listEquipGraphResult, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listEquipGraphResult != null && listEquipGraphResult.Count > 0)
            {
                #region 分组 
                var listGroupByItem = from EquipGraphResult in listEquipGraphResult
                                      group EquipGraphResult by
                                  new
                                  {
                                      EquipNo = EquipGraphResult.EquipNo,
                                      ReceiveDate = EquipGraphResult.ReceiveDate,
                                      TestTypeNo = EquipGraphResult.TestTypeNo,
                                      SampleNo = EquipGraphResult.SampleNo,
                                      SectionNo = EquipGraphResult.SectionNo,
                                      SerialNo = EquipGraphResult.SerialNo
                                  } into g
                                      select new EquipResultGroupBy()
                                      {
                                          EquipNo = g.Key.EquipNo,
                                          ReceiveDate = g.Key.ReceiveDate,
                                          TestTypeNo = g.Key.TestTypeNo,
                                          SampleNo = g.Key.SampleNo,
                                          SectionNo = g.Key.SectionNo,
                                          SerialNo = g.Key.SerialNo,
                                          LongGUID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong()
                                      };
                IList<EquipResultGroupBy> listEquipResultGroupBy = listGroupByItem.ToList();
                #endregion

                Dictionary<long, LBEquip> dicLBEquip = new Dictionary<long, LBEquip>();
                Dictionary<long, LBSection> dicLBSection = new Dictionary<long, LBSection>();
                Dictionary<long, LisEquipForm> dicLisEquipForm = new Dictionary<long, LisEquipForm>();
                Dictionary<long, IList<string>> dicEmpID = new Dictionary<long, IList<string>>();
                Dictionary<long, IList<LisEquipGraph>> dicLisEquipGraphList = new Dictionary<long, IList<LisEquipGraph>>();
                foreach (EquipResultGroupBy groupByItem in listEquipResultGroupBy)
                {
                    IList<string> listEmpID = null;

                    #region 根据仪器编码获取仪器信息
                    LBEquip lbEquip = null;
                    if (dicLBEquip.ContainsKey(groupByItem.EquipNo))
                        lbEquip = dicLBEquip[groupByItem.EquipNo];
                    else
                    {
                        IList<LBEquip> listLBEquip = IBLBEquip.SearchListByHQL(" lbequip.EquipNo=" + groupByItem.EquipNo);
                        if (listLBEquip == null || listLBEquip.Count == 0)
                            listLBEquip = null;
                        else
                            lbEquip = listLBEquip[0];
                        dicLBEquip.Add(groupByItem.EquipNo, lbEquip);
                    }
                    if (lbEquip == null)
                    {
                        IBLisEquipComLog.AddLisEquipComLog(0, "无法根据仪器编码找到对应的仪器信息！仪器编码：" + groupByItem.EquipNo, computerInfo);
                        continue;
                    }
                    computerInfo.SEquipID = lbEquip.Id;
                    computerInfo.SEquipName = lbEquip.CName;
                    #endregion

                    #region 根据小组编码获取小组信息
                    LBSection lbSection = null;
                    if (dicLBSection.ContainsKey(groupByItem.SectionNo))
                        lbSection = dicLBSection[groupByItem.SectionNo];
                    else if (lbEquip.LBSection != null)
                    {
                        lbSection = lbEquip.LBSection;
                        dicLBSection.Add(groupByItem.SectionNo, lbSection);
                    }
                    else
                    {
                        IList<LBSection> listLBSection = IBLBSection.SearchListByHQL(" lbsection.SectionNo=" + groupByItem.SectionNo);
                        if (listLBSection == null || listLBSection.Count == 0)
                            listLBSection = null;
                        else
                        {
                            lbSection = listLBSection[0];
                            dicLBSection.Add(groupByItem.SectionNo, lbSection);
                        }
                    }
                    if (lbSection == null)
                    {
                        IBLisEquipComLog.AddLisEquipComLog(0, "无法根据小组编码找到对应的小组信息！小组编码：" + groupByItem.SectionNo, computerInfo);
                        continue;
                    }
                    else
                    {
                        if (dicEmpID.ContainsKey(lbSection.Id))
                            listEmpID = dicEmpID[lbSection.Id];
                        else
                        {
                            listEmpID = IBLBRight.QueryEmpIDBySectionID(lbSection.Id);
                            if (listEmpID != null && listEmpID.Count > 0)
                            {
                                listEmpID = listEmpID.Distinct().ToList();
                                dicEmpID.Add(lbSection.Id, listEmpID);
                            }
                        }
                    }
                    computerInfo.SSectionID = lbSection.Id;
                    computerInfo.SSectionName = lbSection.CName;
                    #endregion
                    try
                    {

                        IList<LisEquipForm> listLisEquipForm = QueryLisEquipFormByEquipResultInfo(lbEquip.Id, (DateTime)groupByItem.ReceiveDate, groupByItem.SerialNo, groupByItem.SampleNo, computerInfo);

                        LisEquipForm lisEquipForm = null;
                        LisTestForm testForm = null;
                        if (listLisEquipForm == null || listLisEquipForm.Count == 0)
                        {
                            lisEquipForm = new LisEquipForm();
                            IBLisEquipComLog.AddLisEquipComLog(0, "仪器样本单不存在，新增仪器样本单！ID:" + lisEquipForm.Id, computerInfo);
                            lisEquipForm.LabID = labID;
                            lisEquipForm.LBEquip = lbEquip;
                            lisEquipForm.ETestDate = groupByItem.ReceiveDate;
                            lisEquipForm.ESampleNo = groupByItem.SampleNo;
                            lisEquipForm.EBarCode = groupByItem.SerialNo;
                            lisEquipForm.ESampleNoForOrder = LisCommonMethod.DisposeSampleNo(lisEquipForm.ESampleNo);
                            int tempFlag = AddLisTestFormByLisEquipForm(lisEquipForm, lbSection, computerInfo, listEmpID, sendCommDataMsg, ref testForm);
                            lisEquipForm.LisTestForm = testForm;
                            if (tempFlag == -1)//仪器样本单对应多个检验样本单
                                continue;
                            this.Entity = lisEquipForm;
                            this.Add();
                        }
                        else
                        {
                            lisEquipForm = listLisEquipForm[0];
                            IBLisEquipComLog.AddLisEquipComLog(0, "仪器样本单已存在，修改仪器样本单！ID:" + lisEquipForm.Id, computerInfo);
                            if (lisEquipForm.LabID <= 0)
                                lisEquipForm.LabID = labID;
                            lisEquipForm.IExamine += 1;
                            lisEquipForm.IsExamined = true;
                            lisEquipForm.DataUpdateTime = DateTime.Now;
                            if (lisEquipForm.LisTestForm == null)
                            {
                                int tempFlag = AddLisTestFormByLisEquipForm(lisEquipForm, lbSection, computerInfo, listEmpID, sendCommDataMsg, ref testForm);
                                lisEquipForm.LisTestForm = testForm;
                                if (tempFlag == -1)//仪器样本单对应多个检验样本单
                                    continue;
                            }
                            this.Entity = lisEquipForm;
                            this.Edit();
                        }
                        dicLisEquipForm.Add(groupByItem.LongGUID, lisEquipForm);
                        IList<LisEquipGraph> tempLisEquipGraphList = new List<LisEquipGraph>();
                        dicLisEquipGraphList.Add(groupByItem.LongGUID, tempLisEquipGraphList);
                        IList<EquipGraphResult> tempEquipGraphResultList = listEquipGraphResult.Where(p => p.ReceiveDate == groupByItem.ReceiveDate).ToList();

                        foreach (var equipGraphResult in tempEquipGraphResultList)
                        {

                            IList<LisEquipGraph> listLisEquipGraph = IBLisEquipGraph.SearchListByHQL(" lisequipgraph.LisEquipForm.Id=" + lisEquipForm.Id);

                            LisEquipGraph lisEquipGraph = new LisEquipGraph();
                            lisEquipGraph.LabID = labID;
                            lisEquipGraph.LisEquipForm = lisEquipForm;
                            if (listLisEquipGraph != null && listLisEquipGraph.Count > 0)
                            {
                                listLisEquipGraph = listLisEquipGraph.OrderByDescending(p => p.IExamine).ToList();
                                lisEquipGraph.IExamine = listLisEquipGraph[0].IExamine + 1;
                            }
                            else
                                lisEquipGraph.IExamine = 1;

                            lisEquipGraph.GraphName = equipGraphResult.GraphName;
                            lisEquipGraph.GraphType = equipGraphResult.GraphType;
                            lisEquipGraph.GTestDate = equipGraphResult.ReceiveDate;
                            string graphBase64 = "";
                            if (equipGraphResult.GraphBase64 != "")
                                graphBase64 = "data:image/gif;base64," + equipGraphResult.GraphBase64;
                            BaseResultDataValue isAddAndEdit = IBLisTestGraph.AppendLisTestGraphToDataBase(null, lisEquipGraph.LabID, graphBase64, graphBase64);
                            if (isAddAndEdit.success)
                                lisEquipGraph.GraphDataID = long.Parse(isAddAndEdit.ResultDataValue);
                            IBLisEquipGraph.Entity = lisEquipGraph;
                            IBLisEquipGraph.Add();
                            tempLisEquipGraphList.Add(lisEquipGraph);
                        }
                    }
                    catch (Exception ex)
                    {
                        SendEquipResultMsgVOMSG(-1, lbEquip, groupByItem, "检验数据导入", ex.Message, listEmpID, computerInfo, sendCommDataMsg);
                    }
                }
                foreach (KeyValuePair<long, LisEquipForm> kv in dicLisEquipForm)
                {
                    LisEquipForm equipForm = kv.Value;
                    LisTestForm testForm = kv.Value.LisTestForm;

                    IList<string> listEmpID = null;
                    if (dicEmpID.ContainsKey(testForm.LBSection.Id))
                        listEmpID = dicEmpID[testForm.LBSection.Id];
                    try
                    {
                        AddLisTestGraphByLisEquipGraph(kv.Value.LisTestForm, dicLisEquipGraphList[kv.Key], null, "");

                        SendEquipResultMsgVOMSG(0, equipForm, testForm, "检验数据导入", "", listEmpID, computerInfo, sendCommDataMsg);
                    }
                    catch (Exception ex)
                    {
                        SendEquipResultMsgVOMSG(-1, equipForm, testForm, "检验数据导入", ex.Message, listEmpID, computerInfo, sendCommDataMsg);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddLisTestGraphByLisEquipGraph(LisTestForm lisTestForm, IList<LisEquipGraph> listLisEquipGraph, long? empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (lisTestForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取检验单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            if (listLisEquipGraph == null || listLisEquipGraph.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取仪器检验单的图片结果信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            IList<LisTestGraph> listLisTestGraph = IBLisTestGraph.SearchListByHQL(" listestgraph.LisTestForm.Id=" + lisTestForm.Id);
            foreach (LisEquipGraph lisEquipGraph in listLisEquipGraph)
            {
                IList<LisTestGraph> tempList = listLisTestGraph.Where(p => p.GTestDate == lisEquipGraph.GTestDate
                                              && p.GraphName == lisEquipGraph.GraphName && p.GraphType == lisEquipGraph.GraphType).ToList();

                if (tempList != null && tempList.Count > 0)
                {
                    LisTestGraph lisTestGraph = tempList[0];
                    lisTestGraph.GraphDataID = lisEquipGraph.GraphDataID;
                    lisTestGraph.IExamine = lisEquipGraph.IExamine;

                    IBLisTestGraph.Entity = lisTestGraph;
                    IBLisTestGraph.Edit();
                }
                else
                {
                    LisTestGraph lisTestGraph = new LisTestGraph();
                    lisTestGraph.LabID = lisEquipGraph.LabID;
                    lisTestGraph.LisTestForm = lisTestForm;
                    lisTestGraph.MainStatusID = int.Parse(TestItemMainStatus.正常.Key);//检验项目主状态
                    lisTestGraph.StatusID = int.Parse(TestItemStatusID.仪器检验出结果.Key); //过程状态标志
                    lisTestGraph.GTestDate = lisEquipGraph.GTestDate;
                    lisTestGraph.GraphType = lisEquipGraph.GraphType;
                    lisTestGraph.GraphName = lisEquipGraph.GraphName;
                    lisTestGraph.GraphDataID = lisEquipGraph.GraphDataID;
                    lisTestGraph.IExamine = lisEquipGraph.IExamine;

                    IBLisTestGraph.Entity = lisTestGraph;
                    IBLisTestGraph.Add();
                }
            }
            return baseResultDataValue;
        }

        private void SendEquipResultMsgVOMSG(int successFlag, LisEquipForm lisEquipForm, LisTestForm lisTestForm, string dataProcessType, string errorInfo, IList<string> listEmpID, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg)
        {
            if (listEmpID != null)
            {
                EquipResultMsgVO equipResultMsgVO = new EquipResultMsgVO();
                {
                    equipResultMsgVO.ResultMsgName = "通讯";
                    if (lisEquipForm != null)
                    {
                        equipResultMsgVO.EquipFormID = lisEquipForm.Id.ToString();
                        if (lisEquipForm.LBEquip != null)
                        {
                            equipResultMsgVO.EquipID = lisEquipForm.LBEquip.Id.ToString();
                            equipResultMsgVO.EquipName = lisEquipForm.LBEquip.CName;
                        }
                        equipResultMsgVO.BarCode = lisEquipForm.EBarCode;
                        equipResultMsgVO.GSampleNo = lisEquipForm.ESampleNo;
                        equipResultMsgVO.GTestDate = lisEquipForm.ETestDate.Value.ToString("yyyy-MM-dd");
                    }
                    if (lisEquipForm != null)
                    {
                        equipResultMsgVO.TestFormID = lisTestForm.Id.ToString();
                        if (lisTestForm.LBSection != null)
                        {
                            equipResultMsgVO.SectionID = lisTestForm.LBSection.Id.ToString();
                            equipResultMsgVO.SectionName = lisTestForm.LBSection.CName;
                        }
                    }
                    equipResultMsgVO.DataProcessType = dataProcessType;
                    equipResultMsgVO.SuccessFlag = successFlag;
                    switch (successFlag)
                    {
                        case 0:
                            equipResultMsgVO.SuccessHint = "成功";
                            break;
                        case -1:
                            equipResultMsgVO.SuccessHint = "失败";
                            break;
                        case -2:
                            equipResultMsgVO.SuccessHint = "放弃处理";
                            break;
                    }

                    equipResultMsgVO.ErrorInfo = errorInfo;
                    equipResultMsgVO.OperTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd");
                };
                sendCommDataMsg(equipResultMsgVO, SysMessageType.通讯结果消息.Value.Code, lisTestForm.LBSection.Id.ToString(), listEmpID);
                IBLisEquipComLog.AddLisEquipComLog(9, "发送仪器处理消息：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(equipResultMsgVO), computerInfo);
            }
        }
        private void SendEquipResultMsgVOMSG(int successFlag, LBEquip lbEquip, EquipResultGroupBy groupByItem, string dataProcessType, string errorInfo, IList<string> listEmpID, ClientComputerInfo computerInfo, SendSysMessageDelegate sendCommDataMsg)
        {
            EquipResultMsgVO equipResultMsgVO = new EquipResultMsgVO()
            {
                ResultMsgName = "通讯",
                GSampleNo = groupByItem.SampleNo,
                GTestDate = groupByItem.ReceiveDate.Value.ToString("yyyy-MM-dd"),
                ErrorInfo = errorInfo,
                OperTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd")
            };
            equipResultMsgVO.DataProcessType = dataProcessType;
            equipResultMsgVO.SuccessFlag = successFlag;
            switch (successFlag)
            {
                case 0:
                    equipResultMsgVO.SuccessHint = "成功";
                    break;
                case -1:
                    equipResultMsgVO.SuccessHint = "失败";
                    break;
                case -2:
                    equipResultMsgVO.SuccessHint = "放弃处理";
                    break;
            }
            string setionID = "";
            if (lbEquip != null)
            {
                equipResultMsgVO.EquipID = lbEquip.Id.ToString();
                equipResultMsgVO.EquipName = lbEquip.CName;
                setionID = lbEquip.LBSection == null ? "" : lbEquip.LBSection.Id.ToString();
            }
            sendCommDataMsg(equipResultMsgVO, SysMessageType.通讯结果消息.Value.Code, setionID, listEmpID);
            IBLisEquipComLog.AddLisEquipComLog(9, "发送仪器处理消息：" + ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(equipResultMsgVO), computerInfo);
        }

        private int AddLisTestFormByLisEquipForm(LisEquipForm lisEquipForm, LBSection lbSection, ClientComputerInfo computerInfo, IList<string> listEmpID, SendSysMessageDelegate sendCommDataMsg, ref LisTestForm testForm)
        {

            IList<LisTestForm> listTestForm = QueryLisTestFormByLisEquipForm(lisEquipForm, lbSection, computerInfo);
            if (listTestForm == null || listTestForm.Count == 0)
            {
                testForm = AddLisTestFormByLisEquipForm(lisEquipForm, lbSection);
                IBLisEquipComLog.AddLisEquipComLog(0, "根据仪器样本单新增检验样本单！新增检验样本单ID:" + testForm.Id, computerInfo);
            }
            else if (listTestForm.Count == 1)
            {
                testForm = listTestForm[0];
                IBLisEquipComLog.AddLisEquipComLog(0, "仪器样本单已存在检验样本单！检验样本单ID:" + testForm.Id, computerInfo);
            }
            else if (listTestForm.Count > 1)
            {
                testForm = listTestForm[0];
                SendEquipResultMsgVOMSG(-2, lisEquipForm, testForm, "对应的检验单数量=" + listTestForm.Count + "，放弃处理", "", listEmpID, computerInfo, sendCommDataMsg);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 通过仪器样本单新增检验单
        /// </summary>
        /// <param name="lisEquipForm">仪器样本单</param>
        /// <returns></returns>
        public LisTestForm AddLisTestFormByLisEquipForm(LisEquipForm lisEquipForm, LBSection lbSection = null)
        {
            LisTestForm lisTestForm = new LisTestForm();
            lisTestForm.EquipFormID = lisEquipForm.Id;
            lisTestForm.GTestDate = (DateTime)lisEquipForm.ETestDate;
            lisTestForm.GSampleNo = lisEquipForm.ESampleNo;
            lisTestForm.GSampleNoForOrder = lisEquipForm.ESampleNoForOrder;
            if (lbSection != null)
                lisTestForm.LBSection = lbSection;
            else if (lisEquipForm.LBEquip != null)
                lisTestForm.LBSection = lisEquipForm.LBEquip.LBSection;
            lisTestForm.MainStatusID = int.Parse(TestFormMainStatus.检验中.Key);
            lisTestForm.StatusID = int.Parse(TestFormStatusID.检验单生成.Key);
            lisTestForm.ISource = int.Parse(TestFormSource.仪器.Key);
            lisTestForm.TestComment = lisEquipForm.EResultComment;
            lisTestForm.TestTime = DateTime.Now;
            lisTestForm.TestEndTime = DateTime.Now;
            IBLisTestForm.Entity = lisTestForm;
            IBLisTestForm.Add();

            return lisTestForm;
        }

        public IList<LisEquipForm> QueryLisEquipFormByEquipResultInfo(long equipID, DateTime receiveDate, string serialNo, string sampleNo, ClientComputerInfo computerInfo)
        {
            IList<LisEquipForm> listLisEquipForm = null;
            string strEquipFormHql = " lisequipform.ETestDate=\'" + receiveDate.ToString("yyyy-MM-dd") + "\'";
            if (equipID > 0)
                strEquipFormHql += " and lisequipform.LBEquip.Id = " + equipID;
            //如果条码号存在，先根据条码号找仪器样本单
            if (!string.IsNullOrWhiteSpace(serialNo))
            {            
                string tempHQL = strEquipFormHql + " and lisequipform.EBarCode=\'" + serialNo + "\'";
                IBLisEquipComLog.AddLisEquipComLog(0, "开始查找仪器样本单！查询条件：" + tempHQL, computerInfo);
                listLisEquipForm = this.QueryLisEquipForm(tempHQL, "");
            }
            //如果根据条码号找不到仪器样本单，则根据样本号查找
            if ((listLisEquipForm == null || listLisEquipForm.Count == 0) && (!string.IsNullOrWhiteSpace(sampleNo)))
            {
                string tempHQL = strEquipFormHql + " and lisequipform.ESampleNo=\'" + sampleNo + "\'";
                IBLisEquipComLog.AddLisEquipComLog(0, "开始查找仪器样本单！查询条件：" + tempHQL, computerInfo);
                listLisEquipForm = this.QueryLisEquipForm(tempHQL, "");
            }
            return listLisEquipForm;
        }

        /// <summary>
        /// 通过仪器样本单新增检验单
        /// </summary>
        /// <param name="lisEquipForm">仪器样本单</param>
        /// <returns></returns>
        public IList<LisTestForm> QueryLisTestFormByLisEquipForm(LisEquipForm lisEquipForm, LBSection lbSection, ClientComputerInfo computerInfo)
        {
            if (lbSection == null)
                lbSection = lisEquipForm.LBEquip.LBSection;
            string strTestFormHql = " listestform.GTestDate=\'" + ((DateTime)lisEquipForm.ETestDate).ToString("yyyy-MM-dd") + "\'";
            if (lbSection != null)
                strTestFormHql += " and listestform.LBSection.Id=" + lbSection.Id;
            IList<LisTestForm> listLisTestForm = null;
            //如果条码号存在，先根据条码号找检验样本单
            if (!string.IsNullOrWhiteSpace(lisEquipForm.EBarCode))
            {
                string tempHQL = strTestFormHql + " and listestform.BarCode=\'" + lisEquipForm.EBarCode + "\'";
                IBLisEquipComLog.AddLisEquipComLog(0, "开始查找仪器样本单！查询条件：" + tempHQL, computerInfo);
                listLisTestForm = IBLisTestForm.SearchListByHQL(tempHQL);
            }
            //如果根据条码号找不到检验样本单，则根据样本号查找
            if ((listLisTestForm == null || listLisTestForm.Count == 0) && (!string.IsNullOrWhiteSpace(lisEquipForm.ESampleNo)))
            {
                string tempHQL = strTestFormHql + " and listestform.GSampleNo=\'" + lisEquipForm.ESampleNo + "\'";
                IBLisEquipComLog.AddLisEquipComLog(0, "开始查找仪器样本单！查询条件：" + tempHQL, computerInfo);
                listLisTestForm = IBLisTestForm.SearchListByHQL(tempHQL);
            }
            return listLisTestForm;
        }

        /// <summary>
        /// 通过仪器样本单编辑检验单结果备注
        /// </summary>
        /// <param name="lisEquipForm">仪器样本单</param>
        /// /// <param name="lisEquipForm">检验单</param>
        /// <returns></returns>
        public LisTestForm EditLisTestFormTestCommentByLisEquipForm(LisEquipForm lisEquipForm, LisTestForm lisTestForm)
        {
            lisTestForm.EquipFormID = lisEquipForm.Id;
            lisTestForm.TestComment = lisEquipForm.EResultComment;
            if (lisTestForm.TestTime == null)
                lisTestForm.TestTime = DateTime.Now;
            lisTestForm.TestEndTime = DateTime.Now;
            IBLisTestForm.Entity = lisTestForm;
            IBLisTestForm.Edit();
            return lisTestForm;
        }

        /// </summary>
        /// 提取仪器结果--检验单和仪器样本单都已存在
        /// </summary>
        /// <param name="testFormID">检验检验单ID</param>
        /// <param name="equipFormID">仪器检验单ID</param>
        /// <param name="equipItemID">要提取仪器项目ID串，为空提取全部仪器项目结果</param>
        /// <param name="changeSampleNo">是否改变检验检验单样本号</param>
        /// <param name="changeTestFormID">是否改变仪器检验单对应的检验检验单</param>
        /// <param name="isDelAuotAddItem">是否删除检验单中仪器自增项目</param>
        /// <param name="empID">操作人ID</param>
        /// <param name="empName">操作人</param>
        /// <returns></returns>
        public BaseResultDataValue AddLisItemResultByEquipResult(long testFormID, long equipFormID, string equipItemID, bool changeSampleNo, bool changeTestFormID, bool isDelAuotAddItem, long? empID, string empName, string reCheckMemoInfo, ClientComputerInfo computerInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm lisTestForm = IBLisTestForm.Get(testFormID);//获取检验单
            if (lisTestForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取检验单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            if (lisTestForm.MainStatusID != int.Parse(TestFormMainStatus.检验中.Key))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验单状态为【" + TestFormMainStatus.检验中.Value.Name + "】时,才能提取结果！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            //获取检验单项目
            IList<LisTestItem> listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.MainStatusID>=0 and listestitem.LisTestForm.Id=" + testFormID);
            if (listLisTestItem == null)
                listLisTestItem = new List<LisTestItem>();
            //获取仪器样本单
            LisEquipForm lisEquipForm = this.Get(equipFormID);
            if (lisEquipForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取仪器检验单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            //获取仪器样本单项目
            IList<LisEquipItem> listLisEquipItem = IBLisEquipItem.SearchListByHQL(" lisequipitem.LisEquipForm.Id=" + equipFormID);
            if (listLisEquipItem == null || listLisEquipItem.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取仪器检验单的项目结果信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            return AddLisItemResultByEquipResult(lisTestForm, listLisTestItem, lisEquipForm, listLisEquipItem, equipItemID, changeSampleNo, changeTestFormID, isDelAuotAddItem, empID, empName, reCheckMemoInfo, computerInfo);
        }

        /// </summary>
        /// 提取仪器结果
        /// </summary>
        /// <param name="equipFormID">仪器检验单ID</param>
        /// <param name="equipItemID">要提取仪器项目ID串，为空提取全部仪器项目结果</param>
        /// <param name="changeSampleNo">是否改变检验检验单样本号</param>
        /// <param name="changeTestFormID">是否改变仪器检验单对应的检验检验单</param>
        /// <param name="isDelAuotAddItem">是否删除检验单中仪器自增项目</param>
        /// <param name="empID">操作人ID</param>
        /// <param name="empName">操作人</param>
        /// <returns></returns>
        public BaseResultDataValue AddLisItemResultByEquipResult(long equipFormID, string equipItemID, bool changeSampleNo, bool changeTestFormID, bool isDelAuotAddItem, long? empID, string empName, string reCheckMemoInfo, ClientComputerInfo computerInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            //获取仪器样本单
            LisEquipForm lisEquipForm = this.Get(equipFormID);
            if (lisEquipForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取仪器检验单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            //获取仪器样本单项目
            IList<LisEquipItem> listLisEquipItem = IBLisEquipItem.SearchListByHQL(" lisequipitem.LisEquipForm.Id=" + equipFormID);
            if (listLisEquipItem == null || listLisEquipItem.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取仪器检验单的项目结果信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            LisTestForm lisTestForm = lisEquipForm.LisTestForm;
            IList<LisTestItem> listLisTestItem = null;
            if (lisTestForm == null)
            {
                lisTestForm = AddLisTestFormByLisEquipForm(lisEquipForm);
                listLisTestItem = new List<LisTestItem>();
            }
            else
            {
                //获取检验单项目
                listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.MainStatusID>=0 and listestitem.LisTestForm.Id=" + lisTestForm.Id);
                if (listLisTestItem == null)
                    listLisTestItem = new List<LisTestItem>();
            }

            return AddLisItemResultByEquipResult(lisTestForm, listLisTestItem, lisEquipForm, listLisEquipItem, equipItemID, changeSampleNo, changeTestFormID, isDelAuotAddItem, empID, empName, reCheckMemoInfo, computerInfo);
        }

        public BaseResultDataValue AddLisItemResultByEquipResult(LisTestForm lisTestForm, IList<LisTestItem> listLisTestItem, LisEquipForm lisEquipForm, IList<LisEquipItem> listLisEquipItem, string equipItemID, bool changeSampleNo, bool changeTestFormID, bool isDelAuotAddItem, long? empID, string empName, string reCheckMemoInfo, ClientComputerInfo computerInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IBLisEquipComLog.AddLisEquipComLog(0, "通过仪器样本项目结果增加或更新检验单项目结果！", computerInfo);
            if (lisEquipForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取仪器检验单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            if (listLisEquipItem == null || listLisEquipItem.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取仪器检验单的项目结果信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            if (lisTestForm == null)
                lisTestForm = lisEquipForm.LisTestForm;

            if (lisTestForm == null)
            {
                lisTestForm = AddLisTestFormByLisEquipForm(lisEquipForm);
                listLisTestItem = new List<LisTestItem>();
            }
            else if (listLisTestItem == null || listLisTestItem.Count == 0)
            {
                //获取检验单项目
                listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.MainStatusID>=0 and listestitem.LisTestForm.Id=" + lisTestForm.Id);
                if (listLisTestItem == null)
                    listLisTestItem = new List<LisTestItem>();
            }

            //按仪器项目分组，因为一个项目可传递多个结果
            var listEquipItem = from EquipItem in listLisEquipItem
                                group EquipItem by
                            new
                            {
                                ItemID = EquipItem.LBItem.Id
                            } into g
                                select new
                                {
                                    ItemID = g.Key.ItemID
                                };

            if (isDelAuotAddItem)
            {   //删除LisTestItem项目结果列表中仪器增加的项目结果
                for (int i = listLisTestItem.Count - 1; i >= 0; i--)
                {
                    if (listLisTestItem[i].ISource == int.Parse(TestItemSource.仪器增加.Key))
                    {
                        IBLisTestForm.DeleteBatchLisTestItem(listLisTestItem[i].Id);
                        listLisTestItem.Remove(listLisTestItem[i]);
                    }
                }
            }
            IList<string> listEquipItemID = new List<string>();
            if (equipItemID != null && equipItemID.Trim().Length > 0)
                listEquipItemID = equipItemID.Split(',').ToList();

            foreach (var tempEquipItem in listEquipItem)
            {

                if (listEquipItemID.Count > 0 && (!listEquipItemID.Contains(tempEquipItem.ItemID.ToString())))
                    continue;

                IList<LisEquipItem> tempEquipItemList = listLisEquipItem.Where(p => p.LBItem.Id == tempEquipItem.ItemID).OrderByDescending(p => p.IExamine).ToList();
                LisEquipItem equipItem = tempEquipItemList[0];
                IList<LisTestItem> listTestItem = listLisTestItem.Where(p => p.LBItem.Id == tempEquipItem.ItemID).ToList();

                bool isAdd = (listTestItem == null || listTestItem.Count == 0);
                if (!isAdd)
                {
                    baseResultDataValue = EditLisTestItemByLisEquipItem(listTestItem[0], equipItem, ref lisTestForm, reCheckMemoInfo);
                    IBLisEquipComLog.AddLisEquipComLog(0, "根据仪器样本项目结果更新检验单项目结果，项目名称:" + equipItem.LBItem.CName, computerInfo);
                }
                else
                {
                    LisTestItem testItem = new LisTestItem
                    {
                        LisTestForm = lisTestForm
                    };
                    baseResultDataValue = AddLisTestItemByLisEquipItem(lisTestForm, testItem, equipItem, empID, empName);
                    if (baseResultDataValue.success)
                    {
                        listLisTestItem.Add(testItem);
                        IBLisEquipComLog.AddLisEquipComLog(0, "根据仪器样本项目结果新增检验单项目结果，项目名称:" + equipItem.LBItem.CName, computerInfo);
                    }
                }
            }
            if (changeTestFormID)
            {
                this.Entity = lisEquipForm;
                lisEquipForm.LisTestForm = lisTestForm;
                this.Edit();
            }

            lisTestForm.EquipFormID = lisEquipForm.Id;
            if (changeSampleNo)
            {
                lisTestForm.GSampleNo = lisEquipForm.ESampleNo;
                lisTestForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(lisTestForm.GSampleNo);
            }
            if (lisTestForm.TestTime == null)
                lisTestForm.TestTime = DateTime.Now;
            lisTestForm.TestEndTime = DateTime.Now;
            IList<LBItem> itemList = listLisTestItem.Select(p=>p.LBItem).ToList();
            lisTestForm.LBSection = EquipResultSectionReplace(null, lisEquipForm.LBEquip, lisTestForm.LBSection, itemList, lisTestForm.GSampleNo, lisTestForm.GSampleNoForOrder);
            IBLisTestForm.Entity = lisTestForm;
            baseResultDataValue.success = IBLisTestForm.Edit();

            IBLisTestForm.EditLisTestItemAfterTreatment(lisTestForm, listLisTestItem);

            return baseResultDataValue;
        }

        public BaseResultDataValue AddBatchExtractEquipResult(string testFormIDList, string equipFormIDList, bool isChangeSampleNo, bool isDelAuotAddItem, long? empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (string.IsNullOrWhiteSpace(testFormIDList))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验单ID列表参数不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            if (string.IsNullOrWhiteSpace(equipFormIDList))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "仪器检验单ID列表参数不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            string[] testFormIDArray = testFormIDList.Split(',');
            string[] equipFormIDArray = equipFormIDList.Split(',');
            if (testFormIDArray.Length != equipFormIDArray.Length)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验单和仪器检验单数量不匹配，不能执行提取！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            for (int i = 0; i < testFormIDArray.Length; i++)
            {
                baseResultDataValue = AddLisItemResultByEquipResult(long.Parse(testFormIDArray[i]), long.Parse(equipFormIDArray[i]), "", isChangeSampleNo, true, isDelAuotAddItem, empID, empName, "", null);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lisTestItem"></param>
        /// <param name="lisEquipItem"></param>
        /// <returns></returns>
        public BaseResultDataValue AddLisTestItemByLisEquipItem(LisTestForm lisTestForm, LisTestItem lisTestItem, LisEquipItem lisEquipItem, long? empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (lisTestItem != null && lisEquipItem != null)
            {
                lisTestItem.LabID = lisEquipItem.LabID;
                //lisTestItem.LisBarCodeItem = ;
                //lisTestItem.LisOrderItem = ;
                //lisTestItem.OrdersItemID = ;
                //lisTestItem.BarCodesItemID = ;
                //lisTestItem.LisTestForm = ;
                //lisTestItem.PLBItem = ;
                lisTestItem.LBItem = lisEquipItem.LBItem;//检验项目
                lisTestItem.GTestDate = lisTestItem.LisTestForm.GTestDate;//检测日期
                lisTestItem.TestTime = lisEquipItem.TestTime; //检验时间
                lisTestItem.MainStatusID = int.Parse(TestItemMainStatus.正常.Key);//检验项目主状态
                lisTestItem.StatusID = int.Parse(TestItemStatusID.仪器检验出结果.Key); //过程状态标志
                lisTestItem.ISource = int.Parse(TestItemSource.仪器增加.Key);//检验项目来源
                lisTestItem.IExamine = lisEquipItem.IExamine;//检查次数
                lisTestItem.EquipID = lisEquipItem.LisEquipForm.LBEquip.Id;//仪器ID  
                lisTestItem.ValueType = lisEquipItem.TestType; //结果类型
                //lisTestItem.ReportStatusID = null; //结果与报告状态标志

                lisTestItem.ResultStatus = null; //检验结果状态,需要在结果后处理之后赋值（参考范围判断后得到）
                lisTestItem.BAlarmColor = false; //采用特殊提示色,需要在结果后处理之后赋值（参考范围判断后得到）
                lisTestItem.AlarmColor = ""; //结果警示特殊颜色,需要在结果后处理之后赋值（参考范围判断后得到） 
                lisTestItem.IsReport = false; //是否报告项目,需要在结果后处理之后赋值（参考范围判断后得到）
                lisTestItem.TestMethod = null; //检测方法,需要在结果后处理之后赋值（参考范围判断后得到）
                lisTestItem.Unit = null; //结果单位,需要在结果后处理之后赋值（参考范围判断后得到）
                lisTestItem.RefRange = null; //参考范围,需要在结果后处理之后赋值（参考范围判断后得到）
                lisTestItem.RedoDesc = null; //复检原因
                lisTestItem.PreResultID = null; //前值结果ID（历史对比后得到）
                lisTestItem.PreGTestDate = null; //前值检验日期（历史对比后得到）
                lisTestItem.PreValue = null; //前报告值（历史对比后得到）
                lisTestItem.PreValueComp = null; //前值对比（历史对比后得到）
                lisTestItem.PreCompStatus = null; //前值对比状态（历史对比后得到）
                lisTestItem.HisResultCount = 0; //历史结果数量（历史对比后得到）
                lisTestItem.PreTestItemID2 = null; ////前值结果ID2（历史对比后得到）
                lisTestItem.PreGTestDate2 = null; ////前值检验日期2（历史对比后得到）
                lisTestItem.PreValue2 = null; //前报告值2（历史对比后得到）
                lisTestItem.PreTestItemID3 = null; //前值结果ID3（历史对比后得到）
                lisTestItem.PreGTestDate3 = null; //前值检验日期3（历史对比后得到）
                lisTestItem.PreValue3 = null; //前报告值3（历史对比后得到）
                lisTestItem.AlarmLevel = 0; //结果警示界别
                lisTestItem.AlarmInfo = null; //结果警示信息
                lisTestItem.OperaterID = empID; //操作人ID
                lisTestItem.DispOrder = 0; //排序 
                lisTestItem.IResultState = 1; //结果编辑状态 默认0：无结果（或项目默认结果），1：仪器结果，2：人工编辑结果 
                lisTestItem.ICommState = 2; //通讯状态 默认0：未进行通讯，1：双向发送，2：获取结果
                lisTestItem.OriglValue = lisEquipItem.EOriginalValue; //仪器原始数值
                lisTestItem.EquipResultID = lisEquipItem.Id; //仪器样本项目ID
                lisTestItem.EReportValue = lisEquipItem.EReportValue; //仪器结果
                lisTestItem.EResultStatus = lisEquipItem.EResultStatus; //仪器结果状态
                lisTestItem.EResultAlarm = lisEquipItem.EResultAlarm; //仪器结果警告
                lisTestItem.ETestComment = lisEquipItem.ETestComment; //仪器结果备注
                lisTestItem.EReagentInfo = lisEquipItem.EReagentInfo; //仪器试剂信息
                lisTestItem.EAlarmState = lisEquipItem.EAlarmState; //仪器结果警示状态
                lisTestItem.ESend = lisEquipItem.ESend; //仪器审核信息
                lisTestItem.IDoWith = 0; //默认为0，仪器结果后处理之后赋值
                string[] arrayReportValue = EquipResultReplace(null, lisEquipItem.LisEquipForm.LBEquip, lisTestItem.LBItem, lisEquipItem.EReportValue, lisTestForm.GSampleTypeID, lisTestForm.Age, lisTestForm.GenderID, lisTestForm.CollectTime, lisTestForm.AgeUnitID);
                if (arrayReportValue[0] == "无效")
                {            
                    IBLisOperate.AddLisOperate(lisTestItem, TestItemOperateType.仪器检验出结果.Value, "检验单项目结果无效", SysCookieValue);
                }
                else
                {
                    lisTestItem.ReportValue = arrayReportValue[0];//报告值
                    lisTestItem.ResultComment = lisEquipItem.ETestComment; //结果说明     
                    int decimalBit = 0;
                    if (lisTestItem.LBItem != null)
                        decimalBit = lisTestItem.LBItem.Prec;
                    string[] arrayRV = LisCommonMethod.DisposeReportValue(lisTestItem.ReportValue, decimalBit);
                    lisTestItem.ReportValue = arrayRV[0] + arrayReportValue[1];
                    if (!string.IsNullOrEmpty(arrayRV[1]))
                        lisTestItem.QuanValue = double.Parse(arrayRV[1]);//定量结果
                    else
                        lisTestItem.QuanValue = null;
                    lisTestItem.QuanValue2 = null; //定量辅助结果2
                    lisTestItem.QuanValue3 = null; //定量辅助结果3
                    IBLisTestItem.Entity = lisTestItem;
                    baseResultDataValue.success = IBLisTestItem.Add();
                    IBLisOperate.AddLisOperate(lisTestItem, TestItemOperateType.仪器检验出结果.Value, "检验单项目导入", SysCookieValue);
                }
            }
            else
            {
                baseResultDataValue.success = false;
            }
            return baseResultDataValue;
        }

        ///// <summary>
        ///// 根据仪器样本项目结果更新样本单项目结果
        ///// </summary>
        ///// <param name="lisTestItem">样本项目信息</param>
        ///// <param name="lisEquipItem">仪器项目信息</param>
        ///// <returns></returns>
        //public BaseResultDataValue EditLisTestItemByLisEquipItem(LisTestItem lisTestItem, LisEquipItem lisEquipItem, ref LisTestForm lisTestForm)
        //{
        //    return EditLisTestItemByLisEquipItem(lisTestItem, lisEquipItem, ref lisTestForm, "");
        //}

        /// <summary>
        /// 根据仪器样本项目结果更新样本单项目结果
        /// </summary>
        /// <param name="lisTestItem">样本项目信息</param>
        /// <param name="lisEquipItem">仪器项目信息</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestItemByLisEquipItem(LisTestItem lisTestItem, LisEquipItem lisEquipItem, ref LisTestForm lisTestForm, string reCheckMemoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (lisTestItem != null && lisEquipItem != null)
            {
                lisTestItem.LBItem = lisEquipItem.LBItem;//检验项目
                lisTestItem.GTestDate = lisTestItem.LisTestForm.GTestDate;//检测日期
                lisTestItem.TestTime = lisEquipItem.TestTime; //检验时间
                lisTestItem.MainStatusID = int.Parse(TestItemMainStatus.正常.Key);//检验项目主状态
                lisTestItem.StatusID = int.Parse(TestItemStatusID.仪器检验出结果.Key); //过程状态标志
                lisTestItem.ISource = int.Parse(TestItemSource.仪器增加.Key);//检验项目来源
                lisTestItem.IExamine = lisEquipItem.IExamine;//检查次数
                lisTestItem.EquipID = lisEquipItem.LisEquipForm.LBEquip.Id;//仪器ID  
                lisTestItem.ValueType = lisEquipItem.TestType; //结果类型

                lisTestItem.IResultState = 1; //结果编辑状态 默认0：无结果（或项目默认结果），1：仪器结果，2：人工编辑结果 
                lisTestItem.ICommState = 2; //通讯状态 默认0：未进行通讯，1：双向发送，2：获取结果
                lisTestItem.OriglValue = lisEquipItem.EOriginalValue; //仪器原始数值
                lisTestItem.EquipResultID = lisEquipItem.Id; //仪器样本项目ID
                lisTestItem.EReportValue = lisEquipItem.EReportValue; //仪器结果
                lisTestItem.EResultStatus = lisEquipItem.EResultStatus; //仪器结果状态
                lisTestItem.EResultAlarm = lisEquipItem.EResultAlarm; //仪器结果警告
                lisTestItem.ETestComment = lisEquipItem.ETestComment; //仪器结果备注
                lisTestItem.EReagentInfo = lisEquipItem.EReagentInfo; //仪器试剂信息
                lisTestItem.EAlarmState = lisEquipItem.EAlarmState; //仪器结果警示状态
                lisTestItem.ESend = lisEquipItem.ESend; //仪器审核信息
                lisTestItem.IDoWith = 0; //默认为0，仪器结果后处理之后赋值
                
                string[] arrayReportValue = EquipResultReplace(null, lisEquipItem.LisEquipForm.LBEquip, lisTestItem.LBItem, lisEquipItem.EReportValue, lisTestForm.GSampleTypeID, lisTestForm.Age, lisTestForm.GenderID, lisTestForm.CollectTime, lisTestForm.AgeUnitID);
                if (arrayReportValue[0] == "无效")
                {
                    lisTestItem.MainStatusID = int.Parse(TestItemMainStatus.删除作废.Key);
                    IBLisOperate.AddLisOperate(lisTestItem, TestItemOperateType.删除.Value, "检验单项目结果无效", SysCookieValue);
                }
                else
                {
                    string oldReportValue = lisTestItem.ReportValue;
                    lisTestItem.ReportValue = arrayReportValue[0];//报告值
                    lisTestItem.ResultComment = lisEquipItem.ETestComment; //结果说明     
                    int decimalBit = 0;
                    if (lisTestItem.LBItem != null)
                        decimalBit = lisTestItem.LBItem.Prec;
                    string[] arrayRV = LisCommonMethod.DisposeReportValue(lisTestItem.ReportValue, decimalBit);
                    lisTestItem.ReportValue = arrayRV[0] + arrayReportValue[1];
                    if (!string.IsNullOrEmpty(arrayRV[1]))
                        lisTestItem.QuanValue = double.Parse(arrayRV[1]);//定量结果
                    else
                        lisTestItem.QuanValue = null;
                    lisTestItem.QuanValue2 = null; //定量辅助结果2
                    lisTestItem.QuanValue3 = null; //定量辅助结果3
                    #region 复检判断
                    if (!string.IsNullOrWhiteSpace(oldReportValue))
                    {
                        if (oldReportValue != lisTestItem.ReportValue)
                        {
                            lisTestItem.RedoDesc = reCheckMemoInfo;
                            lisTestItem.RedoStatus = 1;//复检标志
                            lisTestForm.RedoStatus = 1;
                            if (string.IsNullOrWhiteSpace(lisTestItem.RedoValues))
                                lisTestItem.RedoValues = lisTestItem.ReportValue;
                            else
                            {
                                string[] tempArray = lisTestItem.RedoValues.Split(',');
                                if (!tempArray.Contains(lisTestItem.ReportValue))
                                    lisTestItem.RedoValues += "," + lisTestItem.ReportValue;
                            }
                            IBLisOperate.AddLisOperate(lisTestItem, TestItemOperateType.复检.Value, "检验单项目复检", SysCookieValue);
                        }
                    }
                    #endregion
                }
                IBLisTestItem.Entity = lisTestItem;
                baseResultDataValue.success = IBLisTestItem.Edit();
            }
            else
            {
                baseResultDataValue.success = false;
            }
            return baseResultDataValue;
        }

        #region 仪器结果值替换
        private string[] EquipResultReplace(IList<LBEquipResultTH> listEquipResultTH, LBEquip equip, LBItem item, string reportValue, long? sampleTypeID, double? age, long? genderID, DateTime? collectTime, long? ageUnitID)
        {
            string[] arrayResult = new string[] { reportValue, "" };
            if (listEquipResultTH == null)
                listEquipResultTH = IBLBEquipResultTH.SearchListByHQL("lbequipresultth.LBEquip.Id=" + equip.Id);
            if (listEquipResultTH == null || listEquipResultTH.Count == 0)
                return arrayResult;
            #region 过滤满足条件的结果替换值记录
            IList<LBEquipResultTH> tempList = listEquipResultTH.Where(p => (p.LBItem == null || p.LBItem.Id == item.Id) &&
                (p.SampleTypeID == null || sampleTypeID == null || p.SampleTypeID == sampleTypeID) &&
                (p.GenderID == null || genderID == null || p.GenderID == genderID) &&
                (p.AgeUnitID == null || ageUnitID == null || p.AgeUnitID == ageUnitID) &&
                ((age == null || age <= 0) || (p.LowAge == null || p.LowAge <= age) && (p.HighAge == null || p.HighAge >= age)) &&
                ((collectTime == null) || (p.BCollectTime == null || p.BCollectTime <= collectTime) && (p.ECollectTime == null || p.ECollectTime >= collectTime)) &&
                (p.SourceValue != null && p.SourceValue.Length > 0)
                ).OrderByDescending(p => p.LBItem).ThenBy(p => p.DispOrder).ToList();
            #endregion
            foreach (LBEquipResultTH equipResultTH in tempList)
            {
                if (equipResultTH.CalcType == EquipResultReplaceCompSymbol.完全等于.Value.Name)
                {
                    if (reportValue == equipResultTH.SourceValue)
                    {
                        reportValue = equipResultTH.ReportValue;
                        arrayResult[0] = equipResultTH.ReportValue.Replace("原始值", reportValue);
                        arrayResult[1] = getAppValue(equipResultTH.AppValue);                       
                    }
                    break;
                }
                else if (equipResultTH.CalcType == EquipResultReplaceCompSymbol.部分等于部分替换.Value.Name)
                {
                    if (reportValue.IndexOf(equipResultTH.SourceValue) >= 0)
                    {
                        reportValue = reportValue.Replace(equipResultTH.SourceValue, equipResultTH.ReportValue);
                        arrayResult[0] = equipResultTH.ReportValue.Replace("原始值", reportValue);
                        arrayResult[1] = getAppValue(equipResultTH.AppValue);                      
                    }
                    break;
                }
                else if (equipResultTH.CalcType == EquipResultReplaceCompSymbol.部分等于全部替换.Value.Name)
                {
                    if (reportValue.IndexOf(equipResultTH.SourceValue) >= 0)
                    {
                        reportValue = equipResultTH.ReportValue;
                        arrayResult[0] = equipResultTH.ReportValue.Replace("原始值", reportValue);
                        arrayResult[1] = getAppValue(equipResultTH.AppValue);
                    }
                    break;
                }
                else
                {
                    double doubleReportValue = 0;
                    double doubleSourceValue = 0;
                    if (double.TryParse(reportValue, out doubleReportValue) && double.TryParse(equipResultTH.SourceValue, out doubleSourceValue))
                    {
                        if ((equipResultTH.CalcType == EquipResultReplaceCompSymbol.等于.Value.Name && doubleReportValue == doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.大于等于.Value.Name && doubleReportValue >= doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.小于等于.Value.Name && doubleReportValue <= doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.大于.Value.Name && doubleReportValue > doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.小于.Value.Name && doubleReportValue < doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.大于等于小于等于.Value.Name && doubleReportValue >= doubleSourceValue && doubleReportValue <= doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.大于小于.Value.Name && doubleReportValue > doubleSourceValue && doubleReportValue < doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.大于等于小于.Value.Name && doubleReportValue >= doubleSourceValue && doubleReportValue < doubleSourceValue)
                        || (equipResultTH.CalcType == EquipResultReplaceCompSymbol.大于小于等于.Value.Name && doubleReportValue > doubleSourceValue && doubleReportValue <= doubleSourceValue))
                        {
                            reportValue = equipResultTH.ReportValue;
                            arrayResult[0] = equipResultTH.ReportValue.Replace("原始值", reportValue);
                            arrayResult[1] = getAppValue(equipResultTH.AppValue);
                            break;
                        }
                    }
                }
            }
            return arrayResult;
        }

        private string getAppValue(string appValue)
        {
            if (!string.IsNullOrWhiteSpace(appValue))
            {
                Regex rgx = new Regex("(?<=[(（])[^（）()]*(?=[)）])");
                string numberStr = rgx.Match(appValue).Value;
                if (!string.IsNullOrWhiteSpace(numberStr))
                {
                    appValue = appValue.Replace(numberStr, "");
                    numberStr = numberStr.Replace("n", "").Replace("N", "");
                    int charCount = 0;
                    if (int.TryParse(numberStr, out charCount))
                    {
                        appValue = appValue.Replace("(", "").Replace(")", "").Replace("（", "").Replace("）", "");
                        appValue = appValue.PadLeft(charCount + appValue.Length, ' ');
                    }
                }
            }
            return appValue;
        }
        #endregion

        #region 仪器结果小组替换
        private LBSection EquipResultSectionReplace(IList<LBEquipSection> listEquipSection, LBEquip equip, LBSection section, IList<LBItem> itemList, string sampleNo, string sampleNoForOrder)
        {
            if (listEquipSection == null)
                listEquipSection = IBLBEquipSection.SearchListByHQL("lbequipsection.LBEquip.Id=" + equip.Id);
            if (listEquipSection == null || listEquipSection.Count == 0)
                return section;

            IList<LBEquipSection> tempList = listEquipSection.Where(p => (p.LBItem == null || itemList.Where(s => s.Id == p.LBItem.Id).Count()>0) &&
                (string.IsNullOrWhiteSpace(p.SampleNoCode) || sampleNo.IndexOf(p.SampleNoCode) >= 0)
                ).OrderByDescending(p => p.LBItem).ThenBy(p => p.DispOrder).ToList();
            if (string.IsNullOrWhiteSpace(sampleNoForOrder))
                sampleNoForOrder = LisCommonMethod.DisposeSampleNo(sampleNo); ;
            foreach (LBEquipSection equipSection in tempList)
            {
                string beginSampleNo = equipSection.CompValue1;
                if (!string.IsNullOrWhiteSpace(beginSampleNo))
                    beginSampleNo = LisCommonMethod.DisposeSampleNo(beginSampleNo);
                string endSampleNo = equipSection.CompValue2;
                if (!string.IsNullOrWhiteSpace(endSampleNo))
                    endSampleNo = LisCommonMethod.DisposeSampleNo(endSampleNo);
                if ((string.IsNullOrWhiteSpace(beginSampleNo) || string.Compare(beginSampleNo, sampleNoForOrder) <= 0) &&
                    (string.IsNullOrWhiteSpace(endSampleNo) || string.Compare(endSampleNo, sampleNoForOrder) >= 0))
                {
                    section = equipSection.LBSection;
                    break;
                }
            }
            return section;
        }

        #endregion 
    }
}