using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.Entity.LIIP;
using ZhiFang.IBLL.LabStar;
using ZhiFang.IDAO.LabStar;
using ZhiFang.LabStar.Common;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary
    public class BLisTestForm : BaseBLL<LisTestForm>, ZhiFang.IBLL.LabStar.IBLisTestForm
    {

        public SysCookieValue SysCookieValue { get; set; }

        IBLisTestItem IBLisTestItem { get; set; }
        IBLisPatient IBLisPatient { get; set; }
        IBLBSection IBLBSection { get; set; }
        IBLBSectionItem IBLBSectionItem { get; set; }
        IBLBItem IBLBItem { get; set; }
        IBLBItemGroup IBLBItemGroup { get; set; }
        IBLBItemCalc IBLBItemCalc { get; set; }
        IBLBItemTimeW IBLBItemTimeW { get; set; }
        IBLBItemCalcFormula IBLBItemCalcFormula { get; set; }
        IDLBQCItemDao IDLBQCItemDao { get; set; }
        IBLisOperate IBLisOperate { get; set; }
        IBBPara IBBPara { get; set; }
        IBBParaItem IBBParaItem { get; set; }
        IBLisTestFormMsg IBLisTestFormMsg { get; set; }
        IBLisTestFormMsgItem IBLisTestFormMsgItem { get; set; }
        IBLisBarCodeForm IBLisBarCodeForm { get; set; }
        IBLisBarCodeItem IBLisBarCodeItem { get; set; }
        IBLBRight IBLBRight { get; set; }
        IBLBSampleType IBLBSampleType { get; set; }

        private readonly long Two25 = (long)System.Math.Pow(2, 25);

        public string TestFormCommoWhereHQL = " and listestform.MainStatusID>=0 ";

        public string TestItemCommoWhereHQL = " and listestitem.MainStatusID>=0 ";

        /// <summary>
        /// 根据当前样本号计算下一个样本号（计算出的样本号不存在）
        /// 例如：当前样本号为10，已存在的样本号为10、11、12，则返回13
        /// 例如：当前样本号为10，已存在的样本号为10、11、13，则返回12
        /// </summary>
        /// <param name="sectionID">检验小组ID</param>
        /// <param name="testDate">检验日期</param>
        /// <param name="curSampleNo">当前样本号</param>
        /// <returns></returns>
        public BaseResultDataValue CreateNewSampleNoByOldSampleNo(long sectionID, string testDate, string curSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int intSampleNo = 0;
            string headSampleNo = "";
            int numLength = 0;
            testDate = DateTime.Now.ToString("yyyy-MM-dd");
            IList<LisTestForm> listLisTestForm = null;
            if (curSampleNo == null || curSampleNo.Trim().Length == 0)
            {
                listLisTestForm = this.SearchListByHQL(" listestform.GTestDate=\'" + testDate + "\'" + " and listestform.LBSection.Id=" + sectionID).OrderByDescending(p => p.GSampleNoForOrder).ToList();
                if (listLisTestForm != null && listLisTestForm.Count > 0)
                    curSampleNo = listLisTestForm[0].GSampleNo;
                else
                {
                    curSampleNo = "1";
                    baseResultDataValue.ResultDataValue = curSampleNo;
                    return baseResultDataValue;
                }

            }
            LisCommonMethod.GetIntSampleNo(curSampleNo, ref headSampleNo, ref intSampleNo, ref numLength);
            listLisTestForm = this.SearchListByHQL(" listestform.MainStatusID>=0 and listestform.GTestDate=\'" + testDate + "\'" + " and listestform.LBSection.Id=" + sectionID + " and listestform.GSampleNo=\'" + curSampleNo + "\'");
            string newSampleNo = curSampleNo;
            while (listLisTestForm != null && listLisTestForm.Count > 0)
            {
                intSampleNo++;
                newSampleNo = LisCommonMethod.GetNewSampleNo(headSampleNo, intSampleNo, numLength);
                listLisTestForm = this.SearchListByHQL(" listestform.MainStatusID>=0 and listestform.GTestDate=\'" + testDate + "\'" + " and listestform.LBSection.Id=" + sectionID + " and listestform.GSampleNo=\'" + newSampleNo + "\'");
            }
            baseResultDataValue.ResultDataValue = newSampleNo;
            return baseResultDataValue;
        }

        /// <summary>
        /// 根据当前样本号顺序计算下一个样板号
        /// 例如：当前样本号为10，则返回11；当前样本号为A099，则返回A100
        /// </summary>
        /// <param name="sectionID">检验小组ID</param>
        /// <param name="testDate">检验日期</param>
        /// <param name="curSampleNo">当前样本号</param>
        /// <returns></returns>
        public BaseResultDataValue QueryNextSampleNoByCurSampleNo(string curSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int intSampleNo = 0;
            string headSampleNo = "";
            int numLength = 0;
            if (curSampleNo == null || curSampleNo.Trim().Length == 0)
            {
                curSampleNo = "1";
                baseResultDataValue.ResultDataValue = curSampleNo;
                return baseResultDataValue;
            }
            LisCommonMethod.GetIntSampleNo(curSampleNo, ref headSampleNo, ref intSampleNo, ref numLength);
            intSampleNo++;
            baseResultDataValue.ResultDataValue = LisCommonMethod.GetNewSampleNo(headSampleNo, intSampleNo, numLength);
            return baseResultDataValue;
        }

        public IList<string> BatchCreateNewSampleNoByOldSampleNo(string curSampleNo, int SampleNoCount)
        {
            IList<string> listSampleNo = new List<string>();
            int intSampleNo = 0;
            string headSampleNo = "";
            int numLength = 0;

            listSampleNo.Add(curSampleNo);
            LisCommonMethod.GetIntSampleNo(curSampleNo, ref headSampleNo, ref intSampleNo, ref numLength);
            for (int i = 0; i < SampleNoCount - 1; i++)
            {
                intSampleNo++;
                listSampleNo.Add(LisCommonMethod.GetNewSampleNo(headSampleNo, intSampleNo, numLength));
            }

            return listSampleNo;
        }

        /// <summary>
        /// 新增检验单
        /// 检验单和检验单项目都不存在
        /// </summary>
        /// <param name="testForm">检验单实体</param>
        /// <param name="listTestItem">检验单项目列表</param>
        /// <param name="isCreateSampleNo">是否创建样本号</param>
        /// <returns></returns>
        public BaseResultDataValue AddSingleLisTestForm(LisTestForm testForm, IList<LisTestItem> listTestItem, bool isCreateSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.Code = -1;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestFormIsEmpty;
                return baseResultDataValue;
            }
            if (testForm.LBSection == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.Code = -2;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_SectionIsEmpty;
                return baseResultDataValue;
            }
            if (testForm.GTestDate == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.Code = -3;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestDateIsEmpty;
                return baseResultDataValue;
            }
            if (testForm.GSampleNo == null || testForm.GSampleNo.Trim().Length == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.Code = -4;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_SampleNoIsEmpty;
                return baseResultDataValue;
            }

            DateTime testDate = (DateTime)testForm.GTestDate;
            IList<LisTestForm> listLisTestForm = this.SearchListByHQL(" listestform.GTestDate=\'" + testDate.ToString("yyyy-MM-dd") + "\'" + TestFormCommoWhereHQL +
                " and listestform.LBSection.Id=" + testForm.LBSection.Id + " and listestform.GSampleNo=\'" + testForm.GSampleNo + "\'");
            if (listLisTestForm != null && listLisTestForm.Count > 0)
            {
                if (!isCreateSampleNo)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.Code = 1;
                    baseResultDataValue.ErrorInfo = string.Format(TestFormHintVar.Hint_SampleNoIsExist, testForm.GSampleNo);
                    baseResultDataValue.ResultDataValue = "{\"GSampleNo\":\"" + testForm.GSampleNo + "\"}";
                    return baseResultDataValue;
                }
                else
                {
                    testForm.GSampleNo = CreateNewSampleNoByOldSampleNo(testForm.LBSection.Id, testDate.ToString("yyyy-MM-dd"), testForm.GSampleNo).ResultDataValue;
                }
            }
            else
            {
                int intSampleNo = 0;
                string headSampleNo = "";
                int numLength = 0;
                LisCommonMethod.GetIntSampleNo(testForm.GSampleNo, ref headSampleNo, ref intSampleNo, ref numLength);
                testForm.GSampleNo = LisCommonMethod.GetNewSampleNo(headSampleNo, intSampleNo, numLength);
            }
            testForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(testForm.GSampleNo);
            testForm.MainStatusID = int.Parse(TestFormMainStatus.检验中.Key);
            testForm.StatusID = int.Parse(TestFormStatusID.检验单生成.Key);
            testForm.ISource = int.Parse(TestFormSource.LIS.Key);
            testForm.FormInfoStatus = JudgeLisTestFormInfoStatus(testForm);
            this.Entity = testForm;
            baseResultDataValue.success = this.Add();
            if (baseResultDataValue.success)
            {
                IBLisOperate.AddLisOperate(testForm, TestFormOperateType.检验单生成.Value, "检验单生成", SysCookieValue);
                baseResultDataValue = AddBatchLisTestItem(testForm, listTestItem, "", false) ;
                if (testForm.LisPatient != null)
                {
                    IBLisPatient.Entity = testForm.LisPatient;
                    baseResultDataValue.success = IBLisPatient.Add();
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量新增检验单
        /// </summary>
        /// <param name="sampleInfo">样本信息json串</param>
        /// <param name="testDate">检验日期</param>
        /// <param name="sectionID">小组ID</param>
        /// <param name="startSampleNo">开始样本号</param>
        /// <param name="sampleCount">样本数量</param>
        /// <returns></returns>
        public BaseResultDataValue AddBatchLisTestForm(string sampleInfo, string testDate, long sectionID, string startSampleNo, int sampleCount)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (startSampleNo != null && startSampleNo.Trim().Length > 0 && sampleCount > 0)
            {
                int intSampleNo = 0;
                string headSampleNo = "";
                int numLength = 0;
                LisCommonMethod.GetIntSampleNo(startSampleNo, ref headSampleNo, ref intSampleNo, ref numLength);
                DateTime checkDate = DateTime.Parse(testDate);
                LBSection section = IBLBSection.Get(sectionID);
                JObject jsonObject = JObject.Parse(sampleInfo);
                if (jsonObject != null)
                {
                    string gSampleTypeID = jsonObject["GSampleTypeID"] != null ? jsonObject["GSampleTypeID"].ToString() : "";
                    string gSampleType = jsonObject["GSampleType"] != null ? jsonObject["GSampleType"].ToString() : "";
                    string gSampleInfo = jsonObject["GSampleInfo"] != null ? jsonObject["GSampleInfo"].ToString() : "";
                    string formMemo = jsonObject["FormMemo"] != null ? jsonObject["FormMemo"].ToString() : "";
                    string sampleSpecialDesc = jsonObject["SampleSpecialDesc"] != null ? jsonObject["SampleSpecialDesc"].ToString() : "";
                    string testComment = jsonObject["TestComment"] != null ? jsonObject["TestComment"].ToString() : "";
                    string testInfo = jsonObject["TestInfo"] != null ? jsonObject["TestInfo"].ToString() : "";
                    string mainTesterId = jsonObject["MainTesterId"] != null ? jsonObject["MainTesterId"].ToString() : "";
                    string mainTester = jsonObject["MainTester"] != null ? jsonObject["MainTester"].ToString() : "";
                    for (int i = 0; i < sampleCount; i++)
                    {
                        IList<LisTestForm> litLisTestForm = this.SearchListByHQL(" listestform.GTestDate=\'" + testDate + "\'" + TestFormCommoWhereHQL +
                            " and listestform.LBSection.Id=" + sectionID + " and listestform.GSampleNo=\'" + intSampleNo + "\'");
                        if (litLisTestForm == null || litLisTestForm.Count == 0)
                        {
                            LisTestForm lisTestForm = new LisTestForm
                            {
                                LBSection = section,
                                GTestDate = checkDate,
                                GSampleType = gSampleType,
                                GSampleNo = LisCommonMethod.GetNewSampleNo(headSampleNo, intSampleNo, numLength),
                                MainStatusID = int.Parse(TestFormMainStatus.检验中.Key),
                                StatusID = int.Parse(TestFormStatusID.检验单生成.Key),
                                ISource = int.Parse(TestFormSource.LIS.Key),
                                GSampleInfo = gSampleInfo,
                                FormMemo = formMemo,
                                SampleSpecialDesc = sampleSpecialDesc,
                                TestComment = testComment,
                                TestInfo = testInfo,
                                MainTester = mainTester
                            };
                            if (gSampleTypeID != "")
                                lisTestForm.GSampleTypeID = long.Parse(gSampleTypeID);
                            lisTestForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(lisTestForm.GSampleNo);
                            if (mainTesterId != "")
                                lisTestForm.MainTesterId = long.Parse(mainTesterId);
                            this.Entity = lisTestForm;
                            baseResultDataValue.success = this.Add();
                            if (baseResultDataValue.success)
                            {
                                IBLisOperate.AddLisOperate(lisTestForm, TestFormOperateType.检验单生成.Value, "检验单生成", SysCookieValue);
                                AddBatchLisTestItemBySectionItem(lisTestForm, section);
                            }
                        }
                        intSampleNo++;
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：起始样本号或样本数量参数必须大于0！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 根据检验小组项目新增检验单项目
        /// </summary>
        /// <param name="testForm">检验单</param>
        /// <param name="section">检验小组</param>
        /// <returns></returns>
        public BaseResultDataValue AddBatchLisTestItemBySectionItem(LisTestForm testForm, LBSection section)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testForm != null && section != null)
            {
                IList<LBSectionItem> listSectionItem = IBLBSectionItem.SearchListByHQL(" lbsectionitem.LBSection.Id = " + section.Id + " and lbsectionitem.IsDefult=1 and lbsectionitem.LBItem.IsUse=1");
                if (listSectionItem == null && listSectionItem.Count==0)
                {
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                //获取检验单已有检验项目
                IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + testForm.Id + TestItemCommoWhereHQL);
                foreach (LBSectionItem sectionItem in listSectionItem)
                {
                    LBItem tempItem = sectionItem.LBItem;
                    LBItem tempParItem = null;
                    IList<LBItem> listAddItem = new List<LBItem>();
                    if (tempItem.GroupType == 2)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("新增小组默认项目【" + tempItem.CName + "】：该项目为组套项目，不能新增！");
                        continue;
                    }
                    else if (tempItem.GroupType == 1)
                    {
                        IList<LBItemGroup> listLBItemGroup = IBLBItemGroup.QueryLBItemGroup(" lbgroup.Id=" + tempItem.Id, "LBItemGroup_LBItem_Id,LBItemGroup_LBGroup_Id");
                        listAddItem = listLBItemGroup.Select(p => p.LBItem).ToList();
                        tempParItem = tempItem;
                    }
                    else
                    {
                        listAddItem.Add(tempItem);
                    }

                    foreach (LBItem item in listAddItem)
                    {
                        IList<LisTestItem> tempList = listTestItem.Where(p => p.LBItem.Id == item.Id).ToList();
                        if (tempList == null || tempList.Count == 0)
                        {
                            LisTestItem addEntity = new LisTestItem
                            {
                                LBItem = item,
                                LisTestForm = testForm,
                                GTestDate = testForm.GTestDate,
                                MainStatusID = testForm.MainStatusID,
                                StatusID = int.Parse(TestItemStatusID.检验单生成.Key),
                                ISource = int.Parse(TestItemSource.手工增加.Key)
                            };
                            if (!string.IsNullOrWhiteSpace(sectionItem.DefultValue))
                            {
                                addEntity.ReportValue = sectionItem.DefultValue;
                                addEntity = DisposeTestItemReportValue(addEntity);
                            }
                            addEntity.PLBItem = tempParItem;
                            if (addEntity.PLBItem == null && sectionItem.GroupItemID != null)
                                addEntity.PLBItem = IBLBItem.Get(sectionItem.GroupItemID.Value);
                            if (sectionItem.EquipID != null)
                                addEntity.EquipID = sectionItem.EquipID;
                            addEntity.TestTime = DateTime.Now;
                            IBLisTestItem.Entity = addEntity;
                            if (IBLisTestItem.Add())
                            {
                                listTestItem.Add(addEntity);
                            }
                        }
                        else
                        {
                            LisTestItem editEntity = tempList[0];
                            if (string.IsNullOrWhiteSpace(editEntity.ReportValue) && (!string.IsNullOrWhiteSpace(sectionItem.DefultValue)))
                            {
                                editEntity.ReportValue = sectionItem.DefultValue;
                                editEntity = DisposeTestItemReportValue(editEntity);
                            }
                            editEntity.PLBItem = tempParItem;
                            if (editEntity.PLBItem == null && sectionItem.GroupItemID != null)
                                editEntity.PLBItem = IBLBItem.Get(sectionItem.GroupItemID.Value);
                            if (sectionItem.EquipID != null)
                                editEntity.EquipID = sectionItem.EquipID;
                            editEntity.DataUpdateTime = DateTime.Now;
                            IBLisTestItem.Entity = editEntity;
                            IBLisTestItem.Edit();
                        }
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：起始样本号或样本数量参数必须大于0！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量更新检验单
        /// </summary>
        /// <param name="listArray"></param>
        /// <returns></returns>
        public BaseResultDataValue UpdateBatchLisTestForm(IList<string[]> listArray)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listArray != null && listArray.Count > 0)
            {
                foreach (string[] array in listArray)
                    baseResultDataValue.success = this.Update(array);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 更新检验单信息
        /// </summary>
        /// <param name="testForm">检验单</param>
        /// <param name="testFormFields">要更新的检验单属性</param>
        /// <param name="patientFields">要更新的患者属性</param>
        /// <returns></returns>
        public BaseResultDataValue LisTestFormEditByField(LisTestForm testForm, string testFormFields, string patientFields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            LisTestForm lisTestForm = this.Get(testForm.Id);
            long sectionID = lisTestForm.LBSection.Id;
            string[] listFormField = testFormFields.Split(',');
            if (!string.IsNullOrEmpty(testForm.GSampleNo))
            {
                if (testForm.GSampleNo != lisTestForm.GSampleNo)
                {
                    DateTime testDate = (DateTime)testForm.GTestDate;
                    IList<LisTestForm> listLisTestForm = this.SearchListByHQL(" listestform.GTestDate=\'" + testDate.ToString("yyyy-MM-dd") + "\'" + TestFormCommoWhereHQL +
                        " and listestform.LBSection.Id=" + sectionID + " and listestform.GSampleNo=\'" + testForm.GSampleNo + "\'");
                    if (listLisTestForm != null && listLisTestForm.Count > 0)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.Code = 1;
                        baseResultDataValue.ErrorInfo = string.Format(TestFormHintVar.Hint_SampleNoIsExist, testForm.GSampleNo);
                        return baseResultDataValue;
                    }
                }
                if (listFormField.Contains("GSampleNo", StringComparer.OrdinalIgnoreCase))
                {
                    if (!listFormField.Contains("GSampleNoForOrder", StringComparer.OrdinalIgnoreCase))
                        testFormFields += ",GSampleNoForOrder";
                    testForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(testForm.GSampleNo);
                }
            }

            //if (testForm.LisPatient != null)
            //{
            //    LisPatient lisPatient = IBLisPatient.Get(testForm.LisPatient.Id);
            //    if (lisPatient == null)
            //    {
            //        IBLisPatient.Entity = testForm.LisPatient;
            //        baseResultDataValue.success = IBLisPatient.Add();
            //    }
            //    else
            //    {
            //        if ((patientFields != null) && (patientFields.Trim().Length > 0))
            //        {
            //            patientFields += ",DataUpdateTime";
            //            testForm.DataUpdateTime = DateTime.Now;
            //            lisPatient = ClassMapperHelp.EntityCopyByProperty(testForm.LisPatient, lisPatient, patientFields);
            //            IBLisPatient.Entity = lisPatient;
            //            baseResultDataValue.success = IBLisPatient.Edit();
            //        }
            //    }
            //}

            //if (!testFormFields.Contains("FormInfoStatus"))
            //    testFormFields += ",FormInfoStatus";


            //lisTestForm = ClassMapperHelp.EntityCopyByProperty(testForm, lisTestForm, listFormField);
            //lisTestForm.FormInfoStatus = JudgeLisTestFormInfoStatus(lisTestForm);
            //this.Entity = lisTestForm;
            //baseResultDataValue.success = this.Edit();
            //return baseResultDataValue;
            if (testForm.LisPatient != null)
            {
                LisPatient lisPatient = IBLisPatient.Get(testForm.LisPatient.Id);
                if (lisPatient == null)
                {
                    IBLisPatient.Entity = testForm.LisPatient;
                    baseResultDataValue.success = IBLisPatient.Add();
                }
                else
                {
                    if ((patientFields != null) && (patientFields.Trim().Length > 0))
                    {
                        patientFields += ",DataUpdateTime";
                        testForm.DataUpdateTime = DateTime.Now;
                        string[] tempArray = ClassMapperHelp.GetUpdateFieldValueStr(testForm.LisPatient, patientFields);
                        if (tempArray != null)
                        {
                            baseResultDataValue.success = IBLisPatient.Update(tempArray);
                        }
                    }
                }
            }

            if (!testFormFields.Contains("FormInfoStatus"))
                testFormFields += ",FormInfoStatus";

            testForm.FormInfoStatus = JudgeLisTestFormInfoStatus(testForm);
            string[] testFormFieldValue = ClassMapperHelp.GetUpdateFieldValueStr(testForm, testFormFields);
            baseResultDataValue.success = this.Update(testFormFieldValue);
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量删除检验单
        /// </summary>
        /// <param name="delIDList">检验单ID列表字符串</param>
        /// <param name="isReceiveDelete">核收的检验单是否删除</param>
        /// <param name="isResultDelete">存在项目结果的检验单是否删除</param>
        /// <returns></returns>
        public BaseResultDataValue DeleteBatchLisTestForm(string delIDList, bool isReceiveDelete, bool isResultDelete)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string[] listID = delIDList.Split(',');
            int deleteFlag = int.Parse(TestFormMainStatus.删除作废.Key);
            foreach (string id in listID)
            {
                bool deleteHintFlag = false;
                string deleteHintInfo = "";
                LisTestForm lisTestForm = this.Get(long.Parse(id));
                if (lisTestForm == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验单为空，删除失败！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    return baseResultDataValue;
                }
                if (lisTestForm != null && lisTestForm.MainStatusID.ToString() == TestFormMainStatus.检验中.Key)
                {
                    string testFormSource = lisTestForm.ISource.ToString();
                    if (!isReceiveDelete)
                    {
                        if (testFormSource == TestFormSource.人工核收.Key || testFormSource == TestFormSource.分发核收.Key || testFormSource == TestFormSource.通讯核收.Key)
                        {
                            deleteHintFlag = true;
                            deleteHintInfo = "检验单为核收的检验单";
                        }
                    }
                    IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + id + TestItemCommoWhereHQL);
                    if (listTestItem != null && listTestItem.Count > 0)
                    {
                        if (!isResultDelete)
                        {
                            IList<LisTestItem> tempList = listTestItem.Where(p => p.ReportValue != null && p.ReportValue.Trim() != "").ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                deleteHintFlag = (deleteHintFlag || true);
                                if (deleteHintInfo == "")
                                    deleteHintInfo = "检验单中项目已有检验结果，建议不要删除！";
                                else
                                    deleteHintInfo += "，且项目已有检验结果，建议不要删除！";
                            }
                            if (deleteHintFlag)
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.Code = 9;
                                baseResultDataValue.ErrorInfo = deleteHintInfo;
                                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                                return baseResultDataValue;
                            }
                        }
                        foreach (LisTestItem testItem in listTestItem)
                        {
                            testItem.MainStatusID = deleteFlag;
                            IBLisTestItem.Entity = testItem;
                            IBLisTestItem.Edit();
                        }
                    }
                    else if (deleteHintFlag)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.Code = 9;
                        baseResultDataValue.ErrorInfo = deleteHintInfo + "，建议不要删除！";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                        return baseResultDataValue;
                    }

                    lisTestForm.MainStatusID = deleteFlag;
                    this.Entity = lisTestForm;
                    baseResultDataValue.success = this.Edit();
                    IBLisOperate.AddLisOperate(lisTestForm, TestFormOperateType.删除.Value, "检验单删除", SysCookieValue);
                    if (!baseResultDataValue.success)
                    {
                        baseResultDataValue.ErrorInfo = "检验单删除失败！";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验单为【" + TestFormMainStatus.GetStatusDic()[lisTestForm.MainStatusID.ToString()].Name + "】状态，不能删除！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QueryLisTestFormIsDelete(string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string[] listID = delIDList.Split(',');
            string resultStr = "";
            foreach (string id in listID)
            {
                LisTestForm lisTestForm = this.Get(long.Parse(id));
                if (lisTestForm != null && lisTestForm.MainStatusID.ToString() == TestFormMainStatus.检验中.Key)
                {
                    string isReceiveStr = ",\"核收标志\":\"否\"";
                    string testFormSource = lisTestForm.ISource.ToString();
                    if (testFormSource == TestFormSource.人工核收.Key || testFormSource == TestFormSource.分发核收.Key || testFormSource == TestFormSource.通讯核收.Key)
                    {
                        baseResultDataValue.success = false;
                        isReceiveStr = ",\"核收标志\":\"是\"";
                    }
                    string isResultStr = ",\"存在检验结果\":\"否\"";
                    IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + id + TestItemCommoWhereHQL);
                    listTestItem = listTestItem.Where(p => p.ReportValue != null && p.ReportValue.Trim() != "").ToList();
                    if (listTestItem != null && listTestItem.Count > 0)
                    {
                        baseResultDataValue.success = false;
                        isResultStr = ",\"存在检验结果\":\"是\"";
                    }

                    if (!baseResultDataValue.success)
                    {

                        if (resultStr == "")
                            resultStr = "{\"检验单ID\":\"" + lisTestForm.Id + "\"" + ",\"检验日期\":\"" + lisTestForm.GTestDate.ToString("yyyy-MM-dd") + "\"" + ",\"样本号\":\"" + lisTestForm.GSampleNo + "\"" + ",\"姓名\":\"" + lisTestForm.CName + "\"" + isReceiveStr + isResultStr + "}";
                        else
                            resultStr += "," + "{\"检验单ID\":\"" + lisTestForm.Id + "\"" + ",\"检验日期\":\"" + lisTestForm.GTestDate.ToString("yyyy-MM-dd") + "\"" + ",\"样本号\":\"" + lisTestForm.GSampleNo + "\"" + ",\"姓名\":\"" + lisTestForm.CName + "\"" + isReceiveStr + isResultStr + "}";
                    }
                }
            }
            if (resultStr != "")
                baseResultDataValue.ErrorInfo = "[" + resultStr + "]";
            return baseResultDataValue;
        }

        /// <summary>
        /// 撤销删除的检验单
        /// </summary>
        /// <param name="testFormID"></param>
        /// <returns></returns>
        public BaseResultDataValue EditBatchLisTestFormDeleteCancel(long testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm lisTestForm = this.Get(testFormID);
            if (lisTestForm != null && lisTestForm.MainStatusID.ToString() == TestFormMainStatus.删除作废.Key)
            {
                lisTestForm.MainStatusID = int.Parse(TestFormMainStatus.检验中.Key);
                this.Entity = lisTestForm;
                baseResultDataValue.success = this.Edit();
                if (baseResultDataValue.success)
                {
                    IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID);
                    if (listTestItem != null && listTestItem.Count > 0)
                    {
                        foreach (LisTestItem testItem in listTestItem)
                        {
                            testItem.MainStatusID = lisTestForm.MainStatusID;
                            IBLisTestItem.Entity = testItem;
                            IBLisTestItem.Edit();
                        }
                    }
                    if (baseResultDataValue.success)
                        IBLisOperate.AddLisOperate(lisTestForm, TestFormOperateType.删除恢复.Value, "检验单删除恢复", SysCookieValue);
                    else
                    {
                        baseResultDataValue.ErrorInfo = "检验单删除恢复失败！";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    }
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量新增检验单项目
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="listAddTestItem">检验单项目实体列表</param>
        /// <param name="isRepPItem">是否替换组合项目</param>
        /// <returns></returns>
        public BaseResultDataValue AddBatchLisTestItem(long testFormID, IList<LisTestItem> listAddTestItem, string testItemFileds, bool isRepPItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listAddTestItem != null && listAddTestItem.Count > 0)
            {
                LisTestForm lisTestForm = this.Get(testFormID);
                AddBatchLisTestItem(lisTestForm, listAddTestItem, testItemFileds, isRepPItem);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量新增检验单项目
        /// </summary>
        /// <param name="lisTestForm">检验单</param>
        /// <param name="listAddTestItem">检验单项目实体列表</param>
        /// <param name="isRepPItem">是否替换组合项目</param>
        /// <returns></returns>
        public BaseResultDataValue AddBatchLisTestItem(LisTestForm lisTestForm, IList<LisTestItem> listAddTestItem, string testItemFileds, bool isRepPItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listAddTestItem != null && listAddTestItem.Count > 0)
            {
                if (lisTestForm != null)
                {
                    IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + lisTestForm.Id + TestItemCommoWhereHQL);
                    bool isEmptyTestItem = (listTestItem == null || listTestItem.Count == 0);
                    foreach (LisTestItem testItem in listAddTestItem)
                    {
                        LisTestItem tempLisTestItem = null;
                        bool isExistItem = false;
                        if (!isEmptyTestItem)
                        {
                            IList<LisTestItem> tempList = listTestItem.Where(p => p.LBItem.Id == testItem.LBItem.Id).ToList();
                            isExistItem = tempList != null && tempList.Count > 0;
                            if (isExistItem)
                                tempLisTestItem = tempList[0];
                        }
                        if (isEmptyTestItem || (!isExistItem))
                        {
                            LisTestItem addEntity = new LisTestItem
                            {
                                ReportValue = testItem.ReportValue,
                                LBItem = testItem.LBItem,
                                PLBItem = testItem.PLBItem,
                                LisTestForm = lisTestForm,
                                GTestDate = lisTestForm.GTestDate,
                                MainStatusID = lisTestForm.MainStatusID,
                                StatusID = int.Parse(TestFormStatusID.检验单生成.Key),
                                ISource = int.Parse(TestFormSource.LIS.Key)
                            };
                            addEntity = ClassMapperHelp.EntityCopyByProperty(testItem, addEntity, testItemFileds);
                            addEntity = DisposeTestItemReportValue(addEntity);
                            IBLisTestItem.Entity = addEntity;
                            if (IBLisTestItem.Add())
                                listTestItem.Add(testItem);
                        }
                        else if (isExistItem && tempLisTestItem != null)
                        {
                            if (isRepPItem || tempLisTestItem.MainStatusID == int.Parse(TestItemMainStatus.删除作废.Key))
                            {
                                if (isRepPItem)
                                    tempLisTestItem.PLBItem = testItem.PLBItem;
                                tempLisTestItem.ReportValue = testItem.ReportValue;
                                tempLisTestItem.GTestDate = lisTestForm.GTestDate;
                                tempLisTestItem.MainStatusID = lisTestForm.MainStatusID;
                                tempLisTestItem = ClassMapperHelp.EntityCopyByProperty(testItem, tempLisTestItem, testItemFileds);
                                tempLisTestItem = DisposeTestItemReportValue(tempLisTestItem);
                                IBLisTestItem.Entity = tempLisTestItem;
                                IBLisTestItem.Edit();
                            }
                        }
                    }
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量新增检验单项目结果---多项目录入
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="listAddTestItem">新增的检验项目结果列表</param>
        /// <param name="isAddItem"></param>
        /// <returns></returns>
        public BaseResultDataValue AddBatchLisTestItemResult(long testFormID, IList<LisTestItem> listAddTestItem, bool isAddItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listAddTestItem != null && listAddTestItem.Count > 0)
            {
                LisTestForm lisTestForm = this.Get(testFormID);
                if (lisTestForm != null)
                {
                    //获取检验单已有检验项目
                    IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + testFormID + TestItemCommoWhereHQL);
                    Dictionary<long, LBItem> dicItem = new Dictionary<long, LBItem>();
                    foreach (LisTestItem testItem in listAddTestItem)
                    {
                        IList<LisTestItem> tempList = listTestItem.Where(p => p.LBItem.Id == testItem.LBItem.Id).ToList();
                        if (tempList == null || tempList.Count == 0)
                        {
                            if (isAddItem && testItem.ReportValue != null && testItem.ReportValue.Trim().Length > 0)
                            {
                                if (testItem.LBItem != null)
                                {
                                    if (!dicItem.ContainsKey(testItem.LBItem.Id))
                                    {
                                        testItem.LBItem = IBLBItem.Get(testItem.LBItem.Id);
                                        dicItem.Add(testItem.LBItem.Id, testItem.LBItem);
                                    }
                                    else
                                        testItem.LBItem = dicItem[testItem.LBItem.Id];
                                }
                                LisTestItem addEntity = new LisTestItem
                                {
                                    LBItem = testItem.LBItem,
                                    PLBItem = testItem.PLBItem,
                                    LisTestForm = lisTestForm,
                                    GTestDate = lisTestForm.GTestDate,
                                    MainStatusID = lisTestForm.MainStatusID,
                                    StatusID = int.Parse(TestFormStatusID.检验单生成.Key),
                                    ISource = int.Parse(TestFormSource.LIS.Key)
                                };
                                addEntity.ReportValue = testItem.ReportValue;
                                addEntity = DisposeTestItemReportValue(addEntity);
                                addEntity.TestTime = DateTime.Now;
                                IBLisTestItem.Entity = addEntity;
                                if (IBLisTestItem.Add())
                                {
                                    listTestItem.Add(addEntity);
                                }
                            }
                        }
                        else
                        {
                            LisTestItem tempLisTestItem = tempList[0];
                            if (testItem.ReportValue != null && testItem.ReportValue.Trim().Length > 0 && testItem.ReportValue.Trim() != "空" && testItem.ReportValue.Trim() != "删除")
                            {
                                tempLisTestItem.ReportValue = testItem.ReportValue;
                                tempLisTestItem = DisposeTestItemReportValue(tempLisTestItem);
                                tempLisTestItem.GTestDate = lisTestForm.GTestDate;
                                tempLisTestItem.MainStatusID = lisTestForm.MainStatusID;
                                tempLisTestItem.TestTime = DateTime.Now;
                                IBLisTestItem.Entity = tempLisTestItem;
                                IBLisTestItem.Edit();
                            }
                            else if (testItem.ReportValue.Trim() == "空")
                            {
                                tempLisTestItem.ReportValue = "";
                                tempLisTestItem.QuanValue = null;
                                tempLisTestItem.QuanValue2 = null;
                                tempLisTestItem.QuanValue3 = null;
                                tempLisTestItem.ResultStatus = "";
                                tempLisTestItem.ResultStatusCode = "";
                                tempLisTestItem.ResultComment = "";
                                tempLisTestItem.BAlarmColor = false;
                                tempLisTestItem.AlarmColor = "";
                                tempLisTestItem.TestTime = null;
                                IBLisTestItem.Entity = tempLisTestItem;
                                IBLisTestItem.Edit();
                            }
                            else if (testItem.ReportValue.Trim() == "删除")
                            {
                                DeleteBatchLisTestItem(tempLisTestItem.Id);
                                listTestItem.Remove(tempLisTestItem);
                            }
                        }
                    }
                    EditLisTestFormTestTime(lisTestForm, listTestItem);
                    EditLisTestItemAfterTreatment(lisTestForm, listTestItem);
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 处理项目报告结果值
        /// </summary>
        /// <param name="testItem"></param>
        private LisTestItem DisposeTestItemReportValue(LisTestItem testItem)
        {
            if (testItem.ReportValue != null && testItem.ReportValue.Trim().Length > 0 && testItem.ReportValue.Trim() != "空" && testItem.ReportValue.Trim() != "删除")
            {
                int decimalBit = 2;
                if (testItem.LBItem != null)
                    decimalBit = testItem.LBItem.Prec;
                string[] arrayRV = LisCommonMethod.DisposeReportValue(testItem.ReportValue, decimalBit);
                testItem.ReportValue = arrayRV[0];
                if (!string.IsNullOrEmpty(arrayRV[1]))
                    testItem.QuanValue = double.Parse(arrayRV[1]);
                else
                    testItem.QuanValue = null; 
            }
            return testItem;
        }

        /// <summary>
        /// 批量新增检验单项目结果---单项目录入
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="listAddTestItem">新增的检验项目结果列表</param>
        /// <param name="isAddItem"></param>
        /// <returns></returns>
        public BaseResultDataValue AddBatchLisTestItemResult(IList<LisTestItem> listAddTestItem, bool isAddItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listAddTestItem == null || listAddTestItem.Count == 0)
                return baseResultDataValue;
            Dictionary<long, LBItem> dicItem = new Dictionary<long, LBItem>();
            foreach (LisTestItem testItem in listAddTestItem)
            {
                LisTestForm lisTestForm = null;
                if (testItem.LisTestForm != null)
                    lisTestForm = this.Get(testItem.LisTestForm.Id);
                if (lisTestForm != null)
                {
                    //获取检验单已有检验项目
                    IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + lisTestForm.Id + TestItemCommoWhereHQL);
                    IList<LisTestItem> tempList = listTestItem.Where(p => p.LBItem.Id == testItem.LBItem.Id).ToList();

                    if (tempList == null || tempList.Count == 0)
                    {
                        if (isAddItem && testItem.ReportValue != null && testItem.ReportValue.Trim().Length > 0)
                        {
                            if (testItem.LBItem != null)
                            {
                                if (!dicItem.ContainsKey(testItem.LBItem.Id))
                                {
                                    testItem.LBItem = IBLBItem.Get(testItem.LBItem.Id);
                                    dicItem.Add(testItem.LBItem.Id, testItem.LBItem);
                                }
                                else
                                    testItem.LBItem = dicItem[testItem.LBItem.Id];
                            }
                            LisTestItem addEntity = new LisTestItem
                            {
                                ReportValue = testItem.ReportValue,
                                LBItem = testItem.LBItem,
                                PLBItem = testItem.PLBItem,
                                LisTestForm = lisTestForm,
                                GTestDate = lisTestForm.GTestDate,
                                MainStatusID = lisTestForm.MainStatusID,
                                StatusID = int.Parse(TestFormStatusID.检验单生成.Key),
                                ISource = int.Parse(TestFormSource.LIS.Key)
                            };
                            addEntity = DisposeTestItemReportValue(addEntity);
                            addEntity.TestTime = DateTime.Now;
                            IBLisTestItem.Entity = addEntity;
                            if (IBLisTestItem.Add())
                            {
                                listTestItem.Add(addEntity);
                            }
                        }
                    }
                    else
                    {
                        LisTestItem tempLisTestItem = tempList[0];
                        if (testItem.ReportValue != null && testItem.ReportValue.Trim().Length > 0 && testItem.ReportValue.Trim() != "空" && testItem.ReportValue.Trim() != "删除")
                        {
                            tempLisTestItem.ReportValue = testItem.ReportValue;
                            tempLisTestItem = DisposeTestItemReportValue(tempLisTestItem); 
                            tempLisTestItem.GTestDate = lisTestForm.GTestDate;
                            tempLisTestItem.MainStatusID = lisTestForm.MainStatusID;
                            tempLisTestItem.TestTime = DateTime.Now;
                            IBLisTestItem.Entity = tempLisTestItem;
                            IBLisTestItem.Edit();
                        }
                        else if (testItem.ReportValue.Trim() == "空")
                        {
                            tempLisTestItem.ReportValue = "";
                            tempLisTestItem.QuanValue = null;
                            tempLisTestItem.QuanValue2 = null;
                            tempLisTestItem.QuanValue3 = null;
                            tempLisTestItem.ResultStatus = "";
                            tempLisTestItem.ResultStatusCode = "";
                            tempLisTestItem.ResultComment = "";
                            tempLisTestItem.BAlarmColor = false;
                            tempLisTestItem.AlarmColor = "";
                            tempLisTestItem.TestTime = null;
                            IBLisTestItem.Entity = tempLisTestItem;
                            IBLisTestItem.Edit();
                        }
                        else if (testItem.ReportValue.Trim() == "删除")
                        {
                            DeleteBatchLisTestItem(tempLisTestItem.Id);
                            listTestItem.Remove(tempLisTestItem);
                        }

                    }
                    EditLisTestFormTestTime(lisTestForm, listTestItem);
                    EditLisTestItemAfterTreatment(lisTestForm, listTestItem);
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量修改检验单项目结果
        /// </summary>
        /// <param name="testFormID"></param>
        /// <param name="listTestItemResult"></param>
        /// <returns></returns>
        public BaseResultDataValue EditBatchLisTestItemResult(long testFormID, IList<LisTestItem> listTestItemResult, string testItemFileds)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listTestItemResult != null && listTestItemResult.Count > 0)
            {
                foreach (LisTestItem entity in listTestItemResult)
                {
                    LisTestItem testItem = IBLisTestItem.Get(entity.Id);
                    testItem.ReportValue = entity.ReportValue;
                    testItem = ClassMapperHelp.EntityCopyByProperty(entity, testItem, testItemFileds);
                    testItem = DisposeTestItemReportValue(testItem);
                    if (testItem.ReportValue != null && testItem.TestTime == null)
                        testItem.TestTime = DateTime.Now;
                    else if (testItem.ReportValue == null)
                    {
                        testItem.TestTime = null;
                        testItem.QuanValue = null;
                    }
                    testItem.DataUpdateTime = DateTime.Now;
                    
                    IBLisTestItem.Entity = testItem;
                    if (!IBLisTestItem.Edit())
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "项目结果编辑失败！项目：" + testItem.LBItem != null ? testItem.LBItem.CName : "";
                    }
                }
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 删除检验单项目
        /// </summary>
        /// <param name="delIDList">检验项目单ID</param>
        /// <returns></returns>
        public BaseResultDataValue DeleteBatchLisTestItem(long testFormID, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string[] listID = delIDList.Split(',');
            foreach (string testItemID in listID)
            {
                baseResultDataValue = DeleteBatchLisTestItem(long.Parse(testItemID));
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteBatchLisTestItem(long testItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestItem lisTestItem = IBLisTestItem.Get(testItemID);
            if (lisTestItem != null)
            {
                baseResultDataValue.success = IBLisTestItem.RemoveByHQL(testItemID);
                if (baseResultDataValue.success)
                {
                    string testItemInfo = "{\"BarCode\":\"" + lisTestItem.LisTestForm.BarCode + "\"" +
                        ",\"GSampleNo\":\"" + lisTestItem.LisTestForm.GSampleNo + "\"" +
                        ",\"GTestDate\":\"" + lisTestItem.LisTestForm.GTestDate.ToString("yyyy-MM-dd") + "\"" +
                        ",\"PatName\":\"" + lisTestItem.LisTestForm.CName + "\"" +
                        ",\"ItemName\":\"" + lisTestItem.LBItem.CName + "\"" +
                        ",\"Shortcode\":\"" + lisTestItem.LBItem.Shortcode + "\"" +
                        ",\"ReportValue\":\"" + lisTestItem.ReportValue + "\"" +
                        ",\"QuanValue\":\"" + lisTestItem.QuanValue + "\"" +
                        "}";
                    LisTestForm testForm = lisTestItem.LisTestForm;
                    IBLisOperate.AddLisOperate(testForm, TestItemOperateType.删除.Value, "检验单项目删除", testItemInfo, SysCookieValue);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "要删除的项目信息已不存在！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 删除检验单项目
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="itemIDList">检验项目单ID</param>
        /// <param name="isDelNoResultItem">仅删除结果为空的项目</param>
        /// <param name="isDelNoOrderItem">仅删除非医嘱项目</param>
        /// <returns></returns>
        public BaseResultDataValue DeleteBatchLisTestItem(long testFormID, string itemIDList, bool isDelNoResultItem, bool isDelNoOrderItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<LisTestItem> listLisTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + testFormID);
            string[] listItemID = itemIDList.Split(',');
            foreach (string itemID in listItemID)
            {
                IList<LisTestItem> tempList = listLisTestItem.Where(p => p.LBItem.Id == long.Parse(itemID)).ToList();
                if (tempList != null && tempList.Count > 0)
                {
                    bool delFlag = true;
                    if (isDelNoResultItem && tempList[0].ReportValue != null && tempList[0].ReportValue.Trim().Length > 0)
                        delFlag = false;
                    if (isDelNoOrderItem && tempList[0].OrdersItemID != null && tempList[0].OrdersItemID > 0)
                        delFlag = false;
                    if (delFlag)
                        baseResultDataValue = DeleteBatchLisTestItem(tempList[0]);
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteBatchLisTestItem(LisTestItem lisTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (lisTestItem != null)
            {
                string testItemInfo = "{\"BarCode\":\"" + lisTestItem.LisTestForm.BarCode + "\"" +
                    ",\"GSampleNo\":\"" + lisTestItem.LisTestForm.GSampleNo + "\"" +
                    ",\"GTestDate\":\"" + lisTestItem.LisTestForm.GTestDate.ToString("yyyy-MM-dd") + "\"" +
                    ",\"PatName\":\"" + lisTestItem.LisTestForm.CName + "\"" +
                    ",\"ItemName\":\"" + lisTestItem.LBItem.CName + "\"" +
                    ",\"Shortcode\":\"" + lisTestItem.LBItem.Shortcode + "\"" +
                    ",\"ReportValue\":\"" + lisTestItem.ReportValue + "\"" +
                    ",\"QuanValue\":\"" + lisTestItem.QuanValue + "\"" +
                    "}";
                IBLisTestItem.Entity = lisTestItem;
                baseResultDataValue.success = IBLisTestItem.Remove();
                if (baseResultDataValue.success)
                {
                    IBLisOperate.AddLisOperate(lisTestItem, TestItemOperateType.删除.Value, "检验单项目删除", testItemInfo, SysCookieValue);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "要删除的项目信息已不存在！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormTestTime(long testFormID)
        {
            return EditLisTestFormTestTime(this.Get(testFormID));
        }

        private BaseResultDataValue EditLisTestFormTestTime(LisTestForm testForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testForm != null)
            {
                IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + testForm.Id + TestItemCommoWhereHQL);
                baseResultDataValue = EditLisTestFormTestTime(testForm, listTestItem);
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue EditLisTestFormTestTime(LisTestForm testForm, IList<LisTestItem> listTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testForm != null && listTestItem != null && listTestItem.Count > 0)
            {
                IList<LisTestItem> tempList = listTestItem.Where(p => p.ReportValue != null && p.ReportValue.Length > 0).ToList();
                if (tempList != null && tempList.Count > 0)
                {
                    if (testForm.TestTime == null)
                        testForm.TestTime = DateTime.Now;
                    testForm.TestEndTime = DateTime.Now;
                }
                else
                {
                    testForm.TestTime = null;
                    testForm.TestEndTime = null;
                }
                this.Entity = testForm;
                baseResultDataValue.success = this.Edit();
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormTestTimeByDelTestItem(long testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm testForm = this.Get(testFormID);
            if (testForm != null)
            {
                IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + testForm.Id + TestItemCommoWhereHQL);
                if (listTestItem == null || listTestItem.Count == 0)
                {
                    testForm.TestTime = null;
                    testForm.TestEndTime = null;
                }
                else
                {
                    IList<LisTestItem> tempList = listTestItem.Where(p => p.ReportValue != null && p.ReportValue.Length > 0).ToList();
                    if (tempList == null || tempList.Count == 0)
                    {
                        testForm.TestTime = null;
                        testForm.TestEndTime = null;
                    }
                }
                this.Entity = testForm;
                baseResultDataValue.success = this.Edit();
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单项目后处理
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestItemAfterTreatment(long testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm lisTestForm = this.Get(testFormID);
            IList<LisTestItem> listLisTestItem = null;
            if (lisTestForm != null)
                listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID + TestItemCommoWhereHQL);
            return EditLisTestItemAfterTreatment(lisTestForm, listLisTestItem);

        }

        /// <summary>
        /// 检验单项目后处理
        /// </summary>
        /// <param name="lisTestForm">检验单</param>
        /// <param name="listLisTestItem">检验单项目</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestItemAfterTreatment(LisTestForm testForm, IList<LisTestItem> listTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testForm != null && listTestItem != null)
            {
                EditJudgeLisTestFormTestAllStatus(testForm, listTestItem);
                GetLisTestFormCalcItem(testForm, listTestItem);
                EditLisTestItemResultHistoryCompareByTestItem(testForm, ref listTestItem);
                IBLisTestItem.EditLisTestItemReferenceValueRange(this, testForm, ref listTestItem);
                EditJudgeLisTestFormZFSysCheckStatus(testForm, listTestItem);
                AddLisTestFormPanicValueMsg(testForm, listTestItem);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 判断检验单信息基本完成状态
        /// </summary>
        /// <param name="testForm"></param>
        /// <returns></returns>
        private int JudgeLisTestFormInfoStatus(LisTestForm testForm)
        {
            if (testForm.CName != null && testForm.CName.Trim().Length > 0 &&
                testForm.GenderID != null && testForm.GenderID >= 0 &&
                testForm.GSampleTypeID != null && testForm.GSampleTypeID >= 0)
                return 1;
            else
                return 0;
        }


        /// <summary>
        /// 判断检验项目是否检测完成
        /// 说明：检验下属检验项目都有结果（辅助项目、计算项目除外且项目状态正常）
        /// </summary>
        /// <param name="lisTestForm">检验单信息</param>
        /// <param name="listLisTestItem">检验单项目列表信息</param>
        /// <returns></returns>    
        private BaseResultDataValue EditJudgeLisTestFormTestAllStatus(LisTestForm testForm, IList<LisTestItem> listLisTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listLisTestItem != null && listLisTestItem.Count > 0)
            {
                IList<LisTestItem> list = listLisTestItem.Where(p => p.MainStatusID == 0 && (!p.LBItem.IsPartItem) && (!p.LBItem.IsCalcItem) && (p.ReportValue == null || p.ReportValue.Trim().Length == 0)).ToList();
                testForm.TestAllStatus = (list == null || list.Count == 0) ? 1 : 0;

            }
            this.Entity = testForm;
            baseResultDataValue.success = this.Edit();
            return baseResultDataValue;
        }

        public IList<LisTestForm> QueryLisTestForm(string strHqlWhere, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            return (DBDao as IDLisTestFormDao).QueryLisTestFormDao(strHqlWhere, listEntityName);
        }

        public EntityList<LisTestForm> QueryLisTestForm(string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            return (DBDao as IDLisTestFormDao).QueryLisTestFormDao(strHqlWhere, order, start, count, listEntityName);
        }

        public EntityPageList<LisTestForm> QueryLisTestFormCurPage(long id, string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            return (DBDao as IDLisTestFormDao).QueryLisTestFormCurPageDao(id, strHqlWhere, order, start, count, listEntityName);
        }

        public EntityList<LisTestForm> QueryLisTestFormBySampleNo(string beginSampleNo, string endSampleNo, string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            string strHqlSampleNo = "";
            if (!string.IsNullOrWhiteSpace(beginSampleNo))
                strHqlSampleNo = " and listestform.GSampleNoForOrder>=\'" + LisCommonMethod.DisposeSampleNo(beginSampleNo) + "\'";
            if (!string.IsNullOrWhiteSpace(endSampleNo))
                strHqlSampleNo += " and listestform.GSampleNoForOrder<=\'" + LisCommonMethod.DisposeSampleNo(endSampleNo) + "\'";
            if (!string.IsNullOrWhiteSpace(strHqlWhere))
                strHqlWhere += strHqlSampleNo;
            else if (!string.IsNullOrWhiteSpace(strHqlSampleNo))
                strHqlWhere = " 1=1 " + strHqlSampleNo;

            return (DBDao as IDLisTestFormDao).QueryLisTestFormDao(strHqlWhere, order, start, count, listEntityName);
        }

        public EntityList<LisTestForm> QueryLisTestFormAndItemNameList(string beginSampleNo, string endSampleNo, string strHqlWhere, string order, int start, int count, string fields, int isOrderItem, int itemNameType)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            string strHqlSampleNo = "";
            if (!string.IsNullOrWhiteSpace(beginSampleNo))
                strHqlSampleNo = " and listestform.GSampleNoForOrder>=\'" + LisCommonMethod.DisposeSampleNo(beginSampleNo) + "\'" + TestFormCommoWhereHQL;
            if (!string.IsNullOrWhiteSpace(endSampleNo))
                strHqlSampleNo += " and listestform.GSampleNoForOrder<=\'" + LisCommonMethod.DisposeSampleNo(endSampleNo) + "\'" + TestFormCommoWhereHQL;
            if (!string.IsNullOrWhiteSpace(strHqlWhere))
                strHqlWhere += strHqlSampleNo;
            else if (!string.IsNullOrWhiteSpace(strHqlSampleNo))
                strHqlWhere = " 1=1 " + strHqlSampleNo;

            EntityList<LisTestForm> entityList = (DBDao as IDLisTestFormDao).QueryLisTestFormDao(strHqlWhere, order, start, count, listEntityName);
            if (entityList != null && entityList.count > 0 && entityList.list != null)
            {
                string strFormIDList = String.Join(",", entityList.list.Select(x => x.Id).ToArray());
                IList<LisTestItem> listLisTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id in (" + strFormIDList + ")" + TestItemCommoWhereHQL);
                if (listLisTestItem != null && listLisTestItem.Count > 0)
                {
                    foreach (LisTestForm lisTestForm in entityList.list)
                    {
                        IList<LisTestItem> tempList = null;
                        if (isOrderItem == 1)
                        {
                            tempList = listLisTestItem.Where(p => p.LisTestForm.Id == lisTestForm.Id && p.PLBItem != null).ToList();
                            if (itemNameType == 1)
                                lisTestForm.ItemNameList = String.Join(",", tempList.Select(x => x.PLBItem.SName).ToArray());
                            else
                                lisTestForm.ItemNameList = String.Join(",", tempList.Select(x => x.PLBItem.CName).ToArray());
                        }
                        else
                        {
                            tempList = listLisTestItem.Where(p => p.LisTestForm.Id == lisTestForm.Id && p.LBItem != null).ToList();
                            if (itemNameType == 1)
                                lisTestForm.ItemNameList = String.Join(",", tempList.Select(x => x.LBItem.SName).ToArray());
                            else
                                lisTestForm.ItemNameList = String.Join(",", tempList.Select(x => x.LBItem.CName).ToArray());
                        }
                    }
                }
            }
            return entityList;
        }

        public EntityList<LisTestForm> QueryWillConfirmLisTestForm(string beginSampleNo, string endSampleNo, string itemResultFlag, string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            string strHqlSampleNo = "";
            if (!string.IsNullOrWhiteSpace(beginSampleNo))
                strHqlSampleNo = " and listestform.GSampleNoForOrder>=\'" + LisCommonMethod.DisposeSampleNo(beginSampleNo) + "\'" + TestFormCommoWhereHQL;
            if (!string.IsNullOrWhiteSpace(endSampleNo))
                strHqlSampleNo += " and listestform.GSampleNoForOrder<=\'" + LisCommonMethod.DisposeSampleNo(endSampleNo) + "\'" + TestFormCommoWhereHQL;
            if (!string.IsNullOrWhiteSpace(strHqlWhere))
                strHqlWhere += strHqlSampleNo;
            else if (!string.IsNullOrWhiteSpace(strHqlSampleNo))
                strHqlWhere = " 1=1 " + strHqlSampleNo;

            EntityList<LisTestForm> entityList = (DBDao as IDLisTestFormDao).QueryLisTestFormDao(strHqlWhere, order, start, count, listEntityName);
            if (entityList != null && entityList.count > 0 && entityList.list != null)
            {
                string strFormIDList = String.Join(",", entityList.list.Select(x => x.Id).ToArray());

                IList<LisTestItem> listLisTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id in (" + strFormIDList + ")" + TestItemCommoWhereHQL);
                if (listLisTestItem != null && listLisTestItem.Count > 0)
                {
                    string[] arrayFlag = itemResultFlag.Split(',');
                    if (arrayFlag.Length == 3)
                    {
                        IList<LisTestItem> tempResultList = null;
                        bool delFlag = false;
                        for (int i = entityList.list.Count - 1; i >= 0; i--)
                        {
                            IList<LisTestItem> tempList = listLisTestItem.Where(p => p.LisTestForm.Id == entityList.list[i].Id).ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                if (arrayFlag[0] == "1")
                                {
                                    tempResultList = tempList.Where(p => p.ReportValue != null && (p.ReportValue.IndexOf("+") >= 0 || p.ReportValue.IndexOf("阳") >= 0)).ToList();
                                    delFlag = (tempResultList != null && tempResultList.Count > 0);
                                }
                                if ((!delFlag) && arrayFlag[1] == "1")
                                {
                                    tempResultList = tempList.Where(p => p.ReportValue != null && p.ReportValue.IndexOf("异常") >= 0 && (!(p.ReportValue.IndexOf("无异常") >= 0 || p.ReportValue.IndexOf("没有异常") >= 0))).ToList();
                                    delFlag = delFlag || (tempResultList != null && tempResultList.Count > 0);
                                }
                                if ((!delFlag) && arrayFlag[2] == "1")
                                {
                                    tempResultList = tempList.Where(p => p.ResultStatusCode != null && (p.ResultStatusCode == "HH" || p.ResultStatusCode == "LL")).ToList();
                                    delFlag = delFlag || (tempResultList != null && tempResultList.Count > 0);
                                }
                                if (delFlag)
                                    entityList.list.Remove(entityList.list[i]);
                            }
                            else
                                entityList.list.Remove(entityList.list[i]);
                        }
                    }
                }
            }
            return entityList;
        }

        public EntityList<LisTestForm> QueryLisTestFormPrintList(string strHqlWhere, string order, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            return (DBDao as IDLisTestFormDao).QueryLisTestFormDao(strHqlWhere, order, 0, 0, listEntityName);
        }

        /// <summary>
        /// 检验单计算项目处理
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue GetLisTestFormCalcItem(long testFormID)
        {
            LisTestForm lisTestForm = this.Get(testFormID);
            IList<LisTestItem> listLisTestItem = null;
            if (lisTestForm != null)
                listLisTestItem = IBLisTestItem.SearchListByHQL("  listestitem.LisTestForm.Id=" + testFormID + TestItemCommoWhereHQL);
            return GetLisTestFormCalcItem(lisTestForm, listLisTestItem);
        }

        /// <summary>
        /// 检验单计算项目处理
        /// </summary>
        /// <param name="lisTestForm">检验单实体</param>
        /// <param name="listLisTestItem">检验单项目实体列表</param>
        /// <returns></returns>
        public BaseResultDataValue GetLisTestFormCalcItem(LisTestForm lisTestForm, IList<LisTestItem> listLisTestItem)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (lisTestForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "检验单计算项目判断或计算错误：检验单信息为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            if (listLisTestItem == null || listLisTestItem.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "检验单计算项目判断或计算错误：检验单项目信息为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            ZhiFang.LabStar.Common.LogHelp.Info("开始获取小组【" + lisTestForm.LBSection.CName + "】计算项目");
            IList<LBSectionItem> listSectionCalcItem = IBLBSectionItem.SearchListByHQL(" lbsectionitem.IsUse=1 and lbsectionitem.LBSection.Id=" + lisTestForm.LBSection.Id +
                " and lbsectionitem.LBItem.IsCalcItem=1 and lbsectionitem.LBItem.IsUse=1");
            ZhiFang.LabStar.Common.LogHelp.Info("结束获取小组【" + lisTestForm.LBSection.CName + "】计算项目");
            if (listSectionCalcItem != null && listSectionCalcItem.Count > 0)
            {
                string strCalcItemName = String.Join(",", listSectionCalcItem.Select(x => x.LBItem.CName).ToArray());
                ZhiFang.LabStar.Common.LogHelp.Info("获取到小组【" + lisTestForm.LBSection.CName + "】的计算项目：" + strCalcItemName);
                //过滤出样本项目中的计算项目
                IList<LisTestItem> listLisTestCalcItem = listLisTestItem.Where(p => p.LBItem.IsCalcItem).ToList<LisTestItem>();
                ZhiFang.LabStar.Common.LogHelp.Info("检验单中的计算项目：" + String.Join(",", listLisTestCalcItem.Select(x => x.LBItem.CName).ToArray()));
                //计算项目不能嵌套，要过滤掉样本项目中的计算项目
                IList<LisTestItem> listLisTestCommonItem = listLisTestItem.Where(p => (!p.LBItem.IsCalcItem)).ToList<LisTestItem>();
                ZhiFang.LabStar.Common.LogHelp.Info("检验单中的非计算项目：" + String.Join(",", listLisTestCommonItem.Select(x => x.LBItem.CName).ToArray()));
                //得到需要判断的计算项目列表listNeedJudgeCalcItem，根据检验单中的非计算项目判断是否需要增加这些计算项目   
                var listNeedJudgeCalcItem = listSectionCalcItem.Where(p =>
                {
                    if (listLisTestCalcItem.Any(s => s.LBItem.Id == p.LBItem.Id)) return false;
                    return true;
                }).ToList();
                ZhiFang.LabStar.Common.LogHelp.Info("小组中需要判断的计算项目：" + String.Join(",", listNeedJudgeCalcItem.Select(x => x.LBItem.CName).ToArray()));
                foreach (var sectionCalcItem in listNeedJudgeCalcItem)
                {
                    IList<LBItemCalc> listLBItemCalc = IBLBItemCalc.SearchListByHQL(" lbitemcalc.LBCalcItem.Id=" + sectionCalcItem.LBItem.Id);
                    if (listLBItemCalc != null && listLBItemCalc.Count > 0)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + sectionCalcItem.LBItem.CName + "】需要参与计算的项目：" + String.Join(",", listLBItemCalc.Select(x => x.LBItem.CName).ToArray()));
                        //判断参与计算的的项目是否都包含在检验项目中
                        bool isContains = listLBItemCalc.All(a => listLisTestCommonItem.Any(b => b.LBItem.Id == a.LBItem.Id));
                        if (isContains)
                        {
                            LisTestItem testItem = IBLisTestItem.AddLisTestCalcItem(lisTestForm, listLisTestItem[0], sectionCalcItem.LBItem);
                            if (testItem != null)
                                listLisTestItem.Add(testItem);
                            //新增计算项目
                            ZhiFang.LabStar.Common.LogHelp.Info("新增计算项目【" + sectionCalcItem.LBItem.CName + "】");
                        }
                        else
                            ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + sectionCalcItem.LBItem.CName + "】参与计算的项目没有全部包含于检验单项目中");
                    }
                    else
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + sectionCalcItem.LBItem.CName + "】没有设置参与计算的项目");
                        continue;
                    }
                }
            }
            else
            {
                ZhiFang.LabStar.Common.LogHelp.Info("小组【" + lisTestForm.LBSection.CName + "】没有设置计算项目或者已禁用掉设置的计算项目");
            }
            return EditLisTestFormCalcItemValue(lisTestForm, listLisTestItem);
        }

        /// <summary>
        /// 计算检验单中计算项目的值
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestFormCalcItemValue(long testFormID)
        {
            LisTestForm lisTestForm = this.Get(testFormID);
            IList<LisTestItem> listLisTestItem = null;
            if (lisTestForm != null)
                listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID + TestItemCommoWhereHQL);
            return EditLisTestFormCalcItemValue(lisTestForm, listLisTestItem);
        }

        /// <summary>
        /// 计算检验单中计算项目的值
        /// </summary>
        /// <param name="lisTestForm">检验单实体</param>
        /// <param name="listLisTestItem">检验单项目实体列表</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestFormCalcItemValue(LisTestForm lisTestForm, IList<LisTestItem> listLisTestItem)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (lisTestForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "检验单计算项目计算错误：检验单信息为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            if (listLisTestItem == null || listLisTestItem.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "检验单【" + lisTestForm.BarCode + "】计算项目计算错误：检验单项目信息为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            ZhiFang.LabStar.Common.LogHelp.Info("判断检验单【" + lisTestForm.BarCode + "】是否包含计算项目");
            IList<LisTestItem> listLisTestCalcItem = listLisTestItem.Where(p => p.LBItem.IsCalcItem).ToList<LisTestItem>();
            foreach (LisTestItem calcItem in listLisTestCalcItem)
            {
                string itemName = calcItem.LBItem.CName;
                ZhiFang.LabStar.Common.LogHelp.Info("检验单【" + lisTestForm.BarCode + "】计算项目：" + itemName);
                IList<LBItemCalcFormula> listItemCalcFormula = IBLBItemCalcFormula.SearchListByHQL(" lbitemcalcformula.IsUse=1 and lbitemcalcformula.LBItem.Id=" + calcItem.LBItem.Id);
                if (listItemCalcFormula == null || listItemCalcFormula.Count == 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】没有设置计算公式相关信息");
                    continue;
                }
                else
                    ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】设置了" + listItemCalcFormula.Count + "条计算公式信息");
                //根据条件（样本类型、小组、年龄单位、年龄、性别、体重）过滤计算公式
                ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】根据条件（样本类型、小组、年龄单位、年龄、性别、体重）过滤计算公式");
                listItemCalcFormula = listItemCalcFormula.Where(p => (p.SampleTypeID == null || p.SampleTypeID == 0 || p.SampleTypeID == lisTestForm.GSampleTypeID)
                    && (p.SectionID == null || p.SectionID == 0 || p.SectionID == lisTestForm.LBSection.Id)
                    && (p.AgeUnitID == null || p.AgeUnitID == 0 || p.SectionID == lisTestForm.AgeUnitID)
                    && ((p.LAge == 0 && p.HAge == 0) || (lisTestForm.Age == 0 || (p.LAge <= lisTestForm.Age && p.HAge >= lisTestForm.Age)))
                    && (p.GenderID == null || p.GenderID == 0 || p.GenderID == lisTestForm.GenderID)
                    && ((p.LWeight == 0 && p.UWeight == 0) || (lisTestForm.PatWeight == 0 || (p.LWeight <= lisTestForm.PatWeight && p.UWeight >= lisTestForm.PatWeight)))
                    ).ToList();
                if (listItemCalcFormula == null || listItemCalcFormula.Count == 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】根据设置的条件没有查找到对应的计算公式");
                    continue;
                }
                else
                    ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】根据设置的条件查找到" + listItemCalcFormula.Count + "条对应的计算公式");
                //判断符合上述过滤条件的计算公式是否满足计算公式条件
                ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】判断符计算公式是否满足计算公式条件");
                IList<LBItemCalcFormula> listWillCalcFormula = new List<LBItemCalcFormula>();
                foreach (LBItemCalcFormula calcFormula in listItemCalcFormula)
                {
                    string strFormulaCondition = calcFormula.FormulaCondition;
                    #region 计算公式条件判断
                    if (strFormulaCondition != null && strFormulaCondition.Trim().Length > 0)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件：" + strFormulaCondition);
                        bool isQuantity = LisCommonMethod.JudgeFormulaConditionIsQuantity(strFormulaCondition);
                        IList<string> listContent = LisCommonMethod.GetContentByExpression(strFormulaCondition, @"(?<=\[)[^\[\]]+(?=\])");
                        bool isAdd = false;//是否增加满足条件的公式
                        bool isNeedCalc = false;//是否按计算条件公式计算
                        foreach (string strCommon in listContent)
                        {
                            var tempList = listLisTestItem.Where(p => p.LBItem.Id.ToString() == strCommon).ToList();
                            if (tempList == null || tempList.Count == 0)
                            {
                                ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件【" + calcFormula.FormulaConditionInfo + "】项目值替换失败：项目ID为" + strCommon);
                                isNeedCalc = false;
                                break;
                            }
                            else if (tempList[0].QuanValue != null)//参与计算的项目值不为空时，计算公式才有效
                            {
                                isNeedCalc = true;
                                if (isQuantity)
                                    strFormulaCondition = strFormulaCondition.Replace("[" + strCommon + "]", tempList[0].QuanValue.ToString());
                                else
                                    strFormulaCondition = strFormulaCondition.Replace("[" + strCommon + "]", tempList[0].ReportValue);
                                ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件【" + calcFormula.FormulaConditionInfo + "】项目值替换：" + strFormulaCondition);
                            }
                            else
                                isNeedCalc = false;

                        }//foreach
                        if (isNeedCalc)
                        {
                            if (isQuantity)//定量计算条件公式
                            {
                                isAdd = (bool)(new System.Data.DataTable().Compute(strFormulaCondition, ""));
                                ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件【" + calcFormula.FormulaConditionInfo + "】定量计算公式：" + strFormulaCondition + "，结果值：" + (isAdd ? "True" : "False"));
                            }
                            else//定性计算条件公式
                            {
                                //注意if判断顺序，“不等于”必须在“等于”前面
                                int index = strFormulaCondition.IndexOf("不等于");
                                if (index >= 0)
                                {
                                    string itemValue = strFormulaCondition.Substring(0, index);
                                    string tempValue = strFormulaCondition.Substring(index + 3, strFormulaCondition.Length - index - 3);
                                    isAdd = (!(itemValue != tempValue));
                                }
                                else if (strFormulaCondition.IndexOf("等于") >= 0)
                                {

                                    string itemValue = strFormulaCondition.Substring(0, index);
                                    string tempValue = strFormulaCondition.Substring(index + 2, strFormulaCondition.Length - index - 2);
                                    isAdd = (itemValue == tempValue);
                                }
                                else if (strFormulaCondition.IndexOf("不包含") >= 0)
                                {
                                    string itemValue = strFormulaCondition.Substring(0, index);
                                    string tempValue = strFormulaCondition.Substring(index + 2, strFormulaCondition.Length - index - 2);
                                    isAdd = (!itemValue.Contains(tempValue));
                                }
                                else if (strFormulaCondition.IndexOf("包含") >= 0)
                                {
                                    string itemValue = strFormulaCondition.Substring(0, index);
                                    string tempValue = strFormulaCondition.Substring(index + 3, strFormulaCondition.Length - index - 3);
                                    isAdd = itemValue.Contains(tempValue);
                                }
                                ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件【" + calcFormula.FormulaConditionInfo + "】定性计算公式：" + strFormulaCondition + "，结果值：" + (isAdd ? "True" : "False"));
                            }
                        }
                        if (isAdd)
                            listWillCalcFormula.Add(calcFormula);
                    }
                    else//如果计算条件公式为空，默认满足条件
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件为空");
                        listWillCalcFormula.Add(calcFormula);
                    }
                    #endregion 计算公式条件判断
                }//foreach listItemCalcFormula
                #region 计算项目值计算
                if (listWillCalcFormula.Count > 0)
                {
                    listWillCalcFormula = listWillCalcFormula.OrderBy(p => p.DispOrder).ToList();
                    //取出要替换的项目
                    LBItemCalcFormula formula = listWillCalcFormula[0];
                    string strCalcFormula = formula.CalcFormula;
                    int calcType = formula.CalcType;//计算类型。0：数值计算 1：仅替换结果得到报告值
                    ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式：" + strCalcFormula + "---" + listWillCalcFormula[0].CalcFormulaInfo);
                    IList<string> listItemID = LisCommonMethod.GetContentByExpression(strCalcFormula, @"(?<=\[)[^\[\]]+(?=\])");
                    bool isCalc = false;
                    foreach (string strItemID in listItemID)
                    {
                        var tempList = listLisTestItem.Where(p => p.LBItem.Id.ToString() == strItemID).ToList();
                        if (tempList == null || tempList.Count == 0)
                        {
                            ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式【" + formula.CalcFormula + "】项目值替换失败：项目ID为" + strItemID);
                            isCalc = false;
                            break;
                        }
                        else if (calcType == 0)//参与计算的项目值不为空时，计算公式才有效
                        {
                            isCalc = (tempList[0].QuanValue != null);
                            if (isCalc)
                                strCalcFormula = strCalcFormula.Replace("[" + strItemID + "]", tempList[0].QuanValue.ToString());
                            ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件【" + formula.CalcFormula + "】定量计算公式：" + strCalcFormula + "，结果值：" + (isCalc ? "True" : "False"));
                        }
                        else if (calcType == 1)//参与计算的项目值不为空时，计算公式才有效
                        {
                            isCalc = (tempList[0].ReportValue != null && tempList[0].ReportValue.Length > 0);
                            if (isCalc)
                                strCalcFormula = strCalcFormula.Replace("[" + strItemID + "]", tempList[0].ReportValue);
                            ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + itemName + "】的计算公式条件【" + formula.CalcFormula + "】定性计算公式：" + strCalcFormula + "，结果值：" + (isCalc ? "True" : "False"));
                        }
                        else
                            isCalc = false;
                    }//foreach listItemID
                    if (isCalc)
                    {
                        if (calcType == 0)
                            calcItem.ReportValue = LisCommonMethod.CalcFormulaByJScript(strCalcFormula);
                        else
                            calcItem.ReportValue = strCalcFormula;
                        IBLisTestItem.Entity = DisposeTestItemReportValue(calcItem);
                        IBLisTestItem.Edit();
                    }
                }
                #endregion
            }//foreach listLisTestCalcItem
            return brdv;
        }

        /// <summary>
        /// 判断检验单中计算项目是否需要删除
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue DeleteLisTestFormCalcItem(long testFormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            LisTestForm lisTestForm = this.Get(testFormID);
            IList<LisTestItem> listLisTestItem = null;
            if (lisTestForm != null)
                listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID + TestItemCommoWhereHQL);

            if (lisTestForm == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "检验单计算项目删除判断错误：检验单信息为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            if (listLisTestItem == null || listLisTestItem.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "检验单【" + lisTestForm.BarCode + "】计算项目删除判断错误：检验单项目信息为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            ZhiFang.LabStar.Common.LogHelp.Info("判断检验单【" + lisTestForm.BarCode + "】是否包含计算项目");
            IList<LisTestItem> listLisTestCalcItem = listLisTestItem.Where(p => p.LBItem.IsCalcItem).ToList<LisTestItem>();
            if (listLisTestCalcItem == null || listLisTestCalcItem.Count == 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Info("检验单【" + lisTestForm.BarCode + "】不包含计算项目！");
                return brdv;
            }
            IList<LisTestItem> listLisTestCommonItem = listLisTestItem.Where(p => (!p.LBItem.IsCalcItem)).ToList<LisTestItem>();
            for (int i = listLisTestCalcItem.Count - 1; i >= 0; i--)
            {
                LisTestItem calcItem = listLisTestCalcItem[i];
                IList<LBItemCalc> listLBItemCalc = IBLBItemCalc.SearchListByHQL(" lbitemcalc.LBCalcItem.Id=" + calcItem.LBItem.Id);
                if (listLBItemCalc != null && listLBItemCalc.Count > 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + calcItem.LBItem.CName + "】需要参与计算的项目：" + String.Join(",", listLBItemCalc.Select(x => x.LBItem.CName).ToArray()));
                    //判断参与计算的的项目是否都包含在检验项目中
                    bool isContains = listLBItemCalc.All(a => listLisTestCommonItem.Any(b => b.LBItem.Id == a.LBItem.Id));
                    if (!isContains)
                    {
                        IBLisTestItem.Entity = calcItem;
                        IBLisTestItem.Remove();
                    }
                }
                else
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("计算项目【" + calcItem.LBItem.CName + "】没有设置参与计算的项目");
                    continue;
                }
            }
            return brdv;
        }

        public BaseResultDataValue EditLisTestFormInfoMerge(long fromTestFormID, LisTestForm toTestForm, string strTestItemID, int mergeType, bool isSampleNoMerge, bool isSerialNoMerge, bool isDelFormTestItem, bool isDelFormTestForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            bool isNewTestForm = false;
            LisTestForm fromTestForm = this.Get(fromTestFormID);//源样本
            LisTestForm targetTestForm = this.Get(toTestForm.Id);//目标样本
            if (fromTestForm == null || fromTestForm.MainStatusID < 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：源样本信息为空或样本此时状态不符合合并要求！";
                return baseResultDataValue;
            }
            if (targetTestForm == null)
            {
                if (mergeType == 2)
                    mergeType = 3;
                targetTestForm = new LisTestForm
                {
                    LabID = fromTestForm.LabID,
                    Id = toTestForm.Id,
                    GSampleNo = toTestForm.GSampleNo,
                    GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(toTestForm.GSampleNo),
                    GTestDate = toTestForm.GTestDate,
                    LBSection = toTestForm.LBSection,
                    MainStatusID = int.Parse(TestFormMainStatus.检验中.Key)
                };
                isNewTestForm = true;
            }
            if (targetTestForm.MainStatusID != 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：只有处于检验中的目标样本才能合并！";
                return baseResultDataValue;
            }
            if (mergeType == 1)
            {
                baseResultDataValue = EditLisTestFormMerge(ref fromTestForm, ref targetTestForm, isNewTestForm, isSampleNoMerge, isSerialNoMerge);
                EditLisTestItemAfterTreatment(targetTestForm.Id);
            }
            else if (mergeType == 2)
                baseResultDataValue = EditLisTestItemMerge(fromTestForm, targetTestForm, isNewTestForm, strTestItemID, isDelFormTestItem, isDelFormTestForm);
            else if (mergeType == 3)
            {
                EditLisTestFormMerge(ref fromTestForm, ref targetTestForm, isNewTestForm, isSampleNoMerge, isSerialNoMerge);
                baseResultDataValue = EditLisTestItemMerge(fromTestForm, targetTestForm, isNewTestForm, strTestItemID, isDelFormTestItem, isDelFormTestForm);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormMerge(ref LisTestForm fromTestForm, ref LisTestForm toTestForm, bool isNewTestForm, bool isSampleNoMerge, bool isSerialNoMerge)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (isNewTestForm)
            {
                if (toTestForm.LBSection == null)
                    toTestForm.LBSection = fromTestForm.LBSection;
                if (toTestForm.GTestDate < DateTime.Parse("1901-01-01"))
                    toTestForm.GTestDate = fromTestForm.GTestDate;
            }
            if (isSampleNoMerge)//样本号信息合并
            {
                toTestForm.GSampleNo = fromTestForm.GSampleNo;
                toTestForm.GSampleNoForOrder = fromTestForm.GSampleNoForOrder;
            }
            if (isSerialNoMerge)//条码号信息合并
            {
                toTestForm.LisOrderForm = fromTestForm.LisOrderForm;
                toTestForm.SampleForm = fromTestForm.SampleForm;
                toTestForm.LisPatient = fromTestForm.LisPatient;
                toTestForm.ISource = fromTestForm.ISource;
                toTestForm.GSampleInfo = fromTestForm.GSampleInfo;
                toTestForm.SampleSpecialDesc = fromTestForm.SampleSpecialDesc;
                toTestForm.FormMemo = fromTestForm.FormMemo;
                toTestForm.SickTypeID = fromTestForm.SickTypeID;
                toTestForm.GSampleTypeID = fromTestForm.GSampleTypeID;
                toTestForm.Age = fromTestForm.Age;
                toTestForm.AgeUnitID = fromTestForm.AgeUnitID;
                toTestForm.BarCode = fromTestForm.BarCode;
                toTestForm.TestType = fromTestForm.TestType;
                toTestForm.PatNo = fromTestForm.PatNo;
                toTestForm.CName = fromTestForm.CName;
                toTestForm.GenderID = fromTestForm.GenderID;
                toTestForm.PatWeight = fromTestForm.PatWeight;
                toTestForm.DeptID = fromTestForm.DeptID;
                toTestForm.DistrictID = fromTestForm.DistrictID;
                toTestForm.UrgentState = fromTestForm.UrgentState;
                toTestForm.Testaim = fromTestForm.Testaim;
                toTestForm.TestComment = fromTestForm.TestComment;
            }
            this.Entity = toTestForm;
            if (isNewTestForm)
                baseResultDataValue.success = this.Add();
            else
                baseResultDataValue.success = this.Edit();
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestItemMerge(LisTestForm fromTestForm, LisTestForm toTestForm, bool isNewTestForm, string strTestItemID, bool isDelFormTestItem, bool isDelFormTestForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<LisTestItem> fromTestItemList = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + fromTestForm.Id + TestItemCommoWhereHQL);//源样本项目
            IList<LisTestItem> toTestItemList = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + toTestForm.Id + TestItemCommoWhereHQL);//目标样本项目
            string[] listTestItemID = strTestItemID.Split(',');
            foreach (string testItemID in listTestItemID)
            {
                bool isAdd = false;
                LisTestItem fromTestItem = null;
                LisTestItem toTestItem = null;
                IList<LisTestItem> tempList = fromTestItemList.Where(p => p.Id == long.Parse(testItemID)).ToList();
                fromTestItem = (tempList != null && tempList.Count > 0) ? tempList[0] : null;
                if (fromTestItem == null)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("样本合并：无法找到ID为【" + testItemID + "】的样本项目信息！");
                    continue;
                }
                tempList = toTestItemList.Where(p => p.LBItem.Id == fromTestItem.LBItem.Id).ToList();
                toTestItem = (tempList != null && tempList.Count > 0) ? tempList[0] : null;
                if (toTestItem == null)
                {
                    isAdd = true;
                    toTestItem = new LisTestItem
                    {
                        LisTestForm = toTestForm,
                        LisOrderItem = fromTestItem.LisOrderItem,
                        LisBarCodeItem = fromTestItem.LisBarCodeItem,
                        OrdersItemID = fromTestItem.OrdersItemID,
                        BarCodesItemID = fromTestItem.BarCodesItemID,
                        LBItem = fromTestItem.LBItem,
                        PLBItem = fromTestItem.PLBItem,
                        MainStatusID = fromTestItem.MainStatusID,
                        StatusID = fromTestItem.StatusID,
                        ReportStatusID = fromTestItem.ReportStatusID,
                        ISource = fromTestItem.ISource
                    };
                    toTestItemList.Add(toTestItem);
                }
                toTestItem.IExamine = fromTestItem.IExamine;
                toTestItem.OriglValue = fromTestItem.OriglValue;
                toTestItem.ValueType = fromTestItem.ValueType;
                toTestItem.ReportValue = fromTestItem.ReportValue;
                toTestItem.ReportInfo = fromTestItem.ReportInfo;
                toTestItem.ResultComment = fromTestItem.ResultComment;
                toTestItem.ResultStatus = fromTestItem.ResultStatus;
                toTestItem.QuanValue = fromTestItem.QuanValue;
                toTestItem.BAlarmColor = fromTestItem.BAlarmColor;
                toTestItem.AlarmColor = fromTestItem.AlarmColor;
                toTestItem.IsReport = fromTestItem.IsReport;
                toTestItem.ESend = fromTestItem.ESend;
                toTestItem.QuanValue2 = fromTestItem.QuanValue2;
                toTestItem.QuanValue3 = fromTestItem.QuanValue3;
                toTestItem.TestMethod = fromTestItem.TestMethod;
                toTestItem.Unit = fromTestItem.Unit;
                toTestItem.RefRange = fromTestItem.RefRange;
                toTestItem.EResultStatus = fromTestItem.EResultStatus;
                toTestItem.EResultAlarm = fromTestItem.EResultAlarm;
                toTestItem.GTestDate = toTestItem.LisTestForm.GTestDate;//检测日期与主单一致
                IBLisTestItem.Entity = toTestItem;
                if (isAdd)
                    baseResultDataValue.success = IBLisTestItem.Add();
                else
                    baseResultDataValue.success = IBLisTestItem.Edit();
                if (isDelFormTestItem && baseResultDataValue.success)
                {
                    if (IBLisTestItem.Remove(long.Parse(testItemID)))
                        fromTestItemList.Remove(fromTestItem);
                }
            }
            if (isDelFormTestForm && fromTestForm.MainStatusID == 0 && fromTestItemList.Count == 0)
            {
                this.Remove(fromTestForm.Id);
            }
            if (!isDelFormTestForm)
                EditLisTestItemAfterTreatment(fromTestForm.Id);
            EditLisTestItemAfterTreatment(toTestForm.Id);
            return baseResultDataValue;
        }

        public EntityList<LBMergeItemFormVO> QueryItemMergeFormInfo(long itemID, string beginDate, string endDate)
        {
            EntityList<LBMergeItemFormVO> entityList = new EntityList<LBMergeItemFormVO>();
            string listItemID = "";
            EntityList<LBItemGroup> listLBItemGroup = IBLBItemGroup.QueryLBItemGroup(" lbgroup.Id=" + itemID, "", 0, 0, "LBItemGroup_LBItem_Id,LBItemGroup_LBGroup_Id");
            if (listLBItemGroup != null && listLBItemGroup.count > 0)
            {
                foreach (LBItemGroup itemGroup in listLBItemGroup.list)
                {
                    listItemID += "," + itemGroup.LBItem.Id.ToString();
                }
                listItemID = listItemID.Remove(0, 1);
                string strHqlWhere = " listestform.MainStatusID=0 and listestitem.LBItem.Id in (" + listItemID + ")" + TestFormCommoWhereHQL +
                                     " and listestform.GTestDate>=\'" + beginDate + "\' and listestform.GTestDate<=\'" + endDate + "\'";
                IList<LBMergeItemFormVO> list = (this.DBDao as IDLisTestFormDao).QueryItemMergeFormInfoDao(strHqlWhere);
                list = list.Where(p => p.ItemCount > 1).ToList();
                if (list != null && list.Count > 0)
                {
                    //样本合并状态（25）处理,状态为25修改为1，否则为0
                    list = list.Select(p => { if ((p.IsMerge & Two25) == Two25) p.IsMerge = 1; else p.IsMerge = 0; return p; }).ToList();
                    var patNoList = list.Where(p => p.IsMerge == 1).GroupBy(p => p.PatNo).ToList();
                    if (patNoList != null && patNoList.Count > 0)
                    {
                        foreach (var entity in patNoList)
                        {
                            var tempList = list.Where(p => p.PatNo == entity.Key).ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                list = list.Select(p => { if (p.PatNo == entity.Key) p.IsMerge = 1; return p; }).ToList();
                                list = list.GroupBy(p => new { p.PatNo, p.CName, p.IsMerge }).Select(g => new LBMergeItemFormVO { PatNo = g.Key.PatNo, CName = g.Key.CName, IsMerge = g.Key.IsMerge }).ToList();
                            }
                        }
                    }
                    entityList.count = list.Count;
                    entityList.list = list;
                }
            }
            return entityList;
        }

        public EntityList<LBMergeItemVO> QueryItemMergeItemInfo(long itemID, string patNo, string cName, string beginDate, string endDate, string isMerge)
        {
            EntityList<LBMergeItemVO> entityList = new EntityList<LBMergeItemVO>();
            string listItemID = "";
            EntityList<LBItemGroup> listLBItemGroup = IBLBItemGroup.QueryLBItemGroup(" lbgroup.Id=" + itemID, "", 0, 0, "LBItemGroup_LBItem_Id,LBItemGroup_LBGroup_Id");
            if (listLBItemGroup != null && listLBItemGroup.count > 0)
            {
                listLBItemGroup.list = listLBItemGroup.list.OrderBy(p => p.LBItem.DispOrder).ToList();
                foreach (LBItemGroup itemGroup in listLBItemGroup.list)
                {
                    listItemID += "," + itemGroup.LBItem.Id.ToString();
                }
                listItemID = listItemID.Remove(0, 1);
                string strHQLMerge = "";
                if (isMerge == "1")
                    strHQLMerge = " and listestform.StatusID is not null and ((listestform.StatusID &" + Two25 + ")=" + Two25 + ")";
                string strHqlWhere = " listestform.MainStatusID=0 and listestform.GTestDate>=\'" + beginDate + "\' and listestform.GTestDate<=\'" + endDate + "\'" + TestFormCommoWhereHQL + strHQLMerge +
                                     " and listestform.PatNo=\'" + patNo + "\' and listestform.CName=\'" + cName + "\'" +
                                     " and listestitem.LBItem.Id in (" + listItemID + ")";
                IList<LBMergeItemVO> listVO = (this.DBDao as IDLisTestFormDao).QueryItemMergeItemInfoDao(strHqlWhere);
                if (listVO != null && listVO.Count > 0)
                {

                    var tempGroupBy = listVO.GroupBy(p => p.LisTestItem.LBItem.Id).Select(g => new { name = g.Key, count = g.Count() });
                    bool isSameItem = (tempGroupBy.Count() == 1 && tempGroupBy.First().count == listVO.Count);
                    int i = 0;
                    foreach (LBMergeItemVO vo in listVO)
                    {
                        if (isSameItem)
                        {
                            if (i < listLBItemGroup.list.Count)
                            {
                                vo.ParItemID = itemID;
                                vo.ParItemName = listLBItemGroup.list[i].LBGroup.CName;
                                vo.ChangeItemID = listLBItemGroup.list[i].LBItem.Id;
                                vo.ChangeItemName = listLBItemGroup.list[i].LBItem.CName;
                                vo.LBChangeItem = listLBItemGroup.list[i].LBItem;
                                vo.ChangeItemDispOrder = listLBItemGroup.list[i].LBItem.DispOrder;
                            }
                            else
                                break;
                            i++;
                        }
                        else
                        {
                            IList<LBItemGroup> tempList = listLBItemGroup.list.Where(p => p.LBItem.Id == vo.LisTestItem.LBItem.Id && p.LBGroup.Id == itemID).ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                vo.ParItemID = itemID;
                                vo.ParItemName = tempList[0].LBGroup.CName;
                                vo.ChangeItemID = tempList[0].LBItem.Id;
                                vo.ChangeItemName = tempList[0].LBItem.CName;
                                vo.LBChangeItem = tempList[0].LBItem;
                                vo.ChangeItemDispOrder = tempList[0].LBItem.DispOrder;
                            }
                        }
                    }
                    listVO = listVO.OrderBy(p => p.ChangeItemDispOrder).ToList();//按样本号排序
                    listVO[0].IsMerge = 1;//默认第一个项目样本为合并目标样本
                    entityList.count = listVO.Count;
                    entityList.list = listVO;
                }
            }
            return entityList;
        }

        public BaseResultDataValue EditMergeItemInfo(long toTestFormID, string strLisTestItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //if (listLBMergeItemVO == null || listLBMergeItemVO.Count == 0)
            //{
            //    baseResultDataValue.success = false;
            //    baseResultDataValue.ErrorInfo = "错误信息：没有选择要合并的项目信息！";
            //    return baseResultDataValue;
            //}
            LisTestForm toTestForm = this.Get(toTestFormID);//目标样本
            if (toTestForm == null || toTestForm.MainStatusID != 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：目标样本信息为空或样本此时状态不符合合并要求！";
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo + "样本ID：" + toTestFormID);
                return baseResultDataValue;
            }
            ZhiFang.LabStar.Common.LogHelp.Info("成功获取到样本信息，样本条码号：" + toTestForm.BarCode);
            toTestForm.StatusID = toTestForm.StatusID != null ? (toTestForm.StatusID | Two25) : toTestForm.StatusID; ;//检验样本合并状态
            IList<LisTestItem> listLisTestItem = IBLisTestItem.QueryLisTestItem(" listestform.Id=" + toTestFormID + TestItemCommoWhereHQL);
            string[] listLisTestItemID = strLisTestItemID.Split(',');
            Dictionary<long, IList<LisTestItem>> dicLisFormItem = new Dictionary<long, IList<LisTestItem>>();//记录合并项目的样本及其项目信息
            Dictionary<long, IList<long>> dicLisFormMergeItem = new Dictionary<long, IList<long>>();//记录样本中合并的项目ID
            //foreach (LBMergeItemVO vo in listLBMergeItemVO)
            foreach (string Id in listLisTestItemID)
            {
                //LisTestItem lisTestItem = IBLisTestItem.Get(vo.LisTestItem.Id);
                //如果原样本中包括将要合并的项目，则不合并此项目
                IList<LisTestItem> tempList = listLisTestItem.Where(p => p.LBItem.Id == long.Parse(Id)).ToList();
                if (tempList != null && tempList.Count > 0)
                    continue;
                //LisTestItem lisTestItem = IBLisTestItem.Get(long.Parse(Id));
                IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.Id=" + long.Parse(Id) + TestItemCommoWhereHQL);
                if (listTestItem != null && listTestItem.Count > 0)
                {
                    LisTestItem lisTestItem = listTestItem[0];
                    if (!dicLisFormItem.ContainsKey(lisTestItem.LisTestForm.Id))
                    {
                        IList<LisTestItem> list = IBLisTestItem.QueryLisTestItem(" listestform.Id=" + lisTestItem.LisTestForm.Id + TestItemCommoWhereHQL);
                        dicLisFormItem.Add(lisTestItem.LisTestForm.Id, list);
                    }
                    if (!dicLisFormMergeItem.ContainsKey(lisTestItem.LisTestForm.Id))
                    {
                        IList<long> list = new List<long>
                        {
                            lisTestItem.LBItem.Id
                        };
                        dicLisFormMergeItem.Add(lisTestItem.LisTestForm.Id, list);
                    }
                    else
                    {
                        dicLisFormMergeItem[lisTestItem.LisTestForm.Id].Add(lisTestItem.LBItem.Id);
                    }

                    LisTestItem newEntity = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisTestItem, LisTestItem>(lisTestItem);
                    newEntity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    newEntity.LisTestForm = toTestForm;
                    newEntity.MainStatusID = int.Parse(TestItemMainStatus.正常.Key); ;
                    newEntity.StatusID = toTestForm.StatusID;
                    newEntity.GTestDate = toTestForm.GTestDate;
                    IBLisTestItem.Entity = newEntity;
                    if (IBLisTestItem.Add())
                        listLisTestItem.Add(newEntity);
                }
            }
            this.Entity = toTestForm;
            baseResultDataValue.success = this.Edit();
            EditMergeFormFlag(dicLisFormItem, dicLisFormMergeItem);
            return baseResultDataValue;
        }

        public BaseResultDataValue EditMergeItemInfo(IList<LBMergeItemVO> listLBMergeItemVO)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listLBMergeItemVO == null || listLBMergeItemVO.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：没有选择要合并的项目信息！";
                return baseResultDataValue;
            }
            IList<LBMergeItemVO> tempMergeForm = listLBMergeItemVO.Where(p => p.IsMerge == 1).ToList();
            if (tempMergeForm == null || tempMergeForm.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：没有指定要合并项目的样本信息！";
                return baseResultDataValue;
            }
            LisTestForm toTestForm = this.Get(tempMergeForm[0].LisTestItem.LisTestForm.Id);//目标样本
            if (toTestForm == null || toTestForm.MainStatusID != 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：目标样本信息为空或样本此时状态不符合合并要求！";
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo + "样本ID：" + toTestForm.Id);
                return baseResultDataValue;
            }
            ZhiFang.LabStar.Common.LogHelp.Info("成功获取到样本信息，样本条码号：" + toTestForm.BarCode);
            toTestForm.StatusID = toTestForm.StatusID != null ? (toTestForm.StatusID | Two25) : toTestForm.StatusID; //检验样本合并状态
            IList<LisTestItem> listLisTestItem = IBLisTestItem.QueryLisTestItem(" listestform.Id=" + toTestForm.Id + TestItemCommoWhereHQL);
            Dictionary<long, IList<LisTestItem>> dicLisFormItem = new Dictionary<long, IList<LisTestItem>>();//记录合并项目的样本及其项目信息
            Dictionary<long, IList<long>> dicLisFormMergeItem = new Dictionary<long, IList<long>>();//记录样本中合并的项目ID
            foreach (LBMergeItemVO vo in listLBMergeItemVO)
            {
                //如果原样本中包括将要合并的项目，则不合并此项目
                IList<LisTestItem> tempList = listLisTestItem.Where(p => p.LBItem.Id == vo.ChangeItemID).ToList();
                if (tempList != null && tempList.Count > 0)
                    continue;
                IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.Id=" + vo.LisTestItem.Id + TestItemCommoWhereHQL);
                if (listTestItem != null && listTestItem.Count > 0)
                {
                    LisTestItem lisTestItem = listTestItem[0];
                    if (!dicLisFormItem.ContainsKey(lisTestItem.LisTestForm.Id))
                    {
                        IList<LisTestItem> list = IBLisTestItem.QueryLisTestItem(" listestform.Id=" + lisTestItem.LisTestForm.Id + TestItemCommoWhereHQL);
                        dicLisFormItem.Add(lisTestItem.LisTestForm.Id, list);
                    }
                    if (!dicLisFormMergeItem.ContainsKey(lisTestItem.LisTestForm.Id))
                    {
                        IList<long> list = new List<long>
                        {
                            lisTestItem.LBItem.Id
                        };
                        dicLisFormMergeItem.Add(lisTestItem.LisTestForm.Id, list);
                    }
                    else
                    {
                        dicLisFormMergeItem[lisTestItem.LisTestForm.Id].Add(lisTestItem.LBItem.Id);
                    }

                    LisTestItem newEntity = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisTestItem, LisTestItem>(lisTestItem);
                    newEntity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    newEntity.LisTestForm = toTestForm;
                    if (vo.LBChangeItem.Id != newEntity.LBItem.Id)
                        newEntity.LBItem = vo.LBChangeItem;
                    newEntity.MainStatusID = int.Parse(TestItemMainStatus.正常.Key);
                    newEntity.StatusID = toTestForm.StatusID;
                    newEntity.GTestDate = toTestForm.GTestDate;
                    IBLisTestItem.Entity = newEntity;
                    if (IBLisTestItem.Add())
                        listLisTestItem.Add(newEntity);
                }
            }
            this.Entity = toTestForm;
            baseResultDataValue.success = this.Edit();
            EditMergeFormFlag(dicLisFormItem, dicLisFormMergeItem);
            return baseResultDataValue;
        }

        private BaseResultDataValue EditMergeFormFlag(Dictionary<long, IList<LisTestItem>> dicLisFormItem, Dictionary<long, IList<long>> dicLisFormMergeItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            bool isAllMerge = false;
            foreach (KeyValuePair<long, IList<LisTestItem>> kv in dicLisFormItem)
            {
                IList<LisTestItem> listTestItem = kv.Value;
                IList<long> listMergeItemID = dicLisFormMergeItem[kv.Key];

                if (listTestItem.Count > listMergeItemID.Count)
                {
                    isAllMerge = false;
                }
                else if (listTestItem.Count == listMergeItemID.Count)
                {
                    if (listTestItem.Count == 1)
                    {
                        isAllMerge = (listTestItem[0].LBItem.Id == listMergeItemID[0]);
                    }
                    else if (listTestItem.Count > 1)
                    {
                        IList<long> listItemId = new List<long>();
                        foreach (LisTestItem entity in listTestItem)
                            listItemId.Add(entity.LBItem.Id);
                        isAllMerge = Enumerable.SequenceEqual(listItemId, listMergeItemID);
                        var q = from a in listItemId join b in listMergeItemID on a equals b select a;
                        isAllMerge = (q.Count() == listMergeItemID.Count);
                    }
                }
                int? mainStatusID = null;
                if (isAllMerge)
                {
                    mainStatusID = int.Parse(TestFormMainStatus.合并.Key);
                    listTestItem[0].LisTestForm.MainStatusID = (int)mainStatusID;//合并检验单，删除检验单备份
                    this.Entity = listTestItem[0].LisTestForm;
                    this.Edit();
                }
                foreach (LisTestItem entity in listTestItem)
                {
                    if (listMergeItemID.Contains(entity.LBItem.Id))
                    {
                        if (mainStatusID != null)
                            entity.MainStatusID = int.Parse(TestItemMainStatus.删除作废.Key);//合并检验单，删除检验单项目
                        entity.StatusID = entity.StatusID != null ? (entity.StatusID | Two25) : entity.StatusID;
                        IBLisTestItem.Entity = entity;
                        IBLisTestItem.Edit();
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultBool CheckSampleConvertStatus(long testFormID, long QCMatID)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            IList<LisTestItem> lisTestItems = IBLisTestItem.SearchListByHQL("LisTestForm.Id=" + testFormID + TestItemCommoWhereHQL);
            List<long> itemids = new List<long>();
            if (lisTestItems.Count <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "项目没有结果！";
            }
            else
            {
                lisTestItems.ToList().ForEach(a =>
                {
                    itemids.Add(a.LBItem.Id);
                    if (a.ReportValue == null || a.ReportValue == "")
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "有项目结果为空！";
                    }
                });
            }
            List<long> qcitemids = new List<long>();
            IDLBQCItemDao.GetListByHQL("LBQCMaterial.Id=" + QCMatID).ToList().ForEach(a =>
            {
                if (a.QCDataType != 1) //0：靶值标准差， 1：定性 2：值范围
                {
                    var item = lisTestItems.Where(b => b.LBItem.Id == a.LBItem.Id);
                    if (item.Count() > 0 && !item.First().QuanValue.HasValue)
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo += "样本项目：" + item.First().LBItem.CName + "没有定量结果 ";
                    }
                }

                qcitemids.Add(a.LBItem.Id);
            });
            if (itemids.Except(qcitemids).ToList().Count > 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo += "质控项目和样本项目不相同！";
            }


            return baseResultBool;
        }

        public BaseResult SampleConvertQCMaterial(long testFormID, long QCMatID)
        {
            BaseResult baseResult = new BaseResult();
            IList<LisTestItem> lisTestItems = IBLisTestItem.SearchListByHQL("LisTestForm.Id=" + testFormID + TestItemCommoWhereHQL);
            var dd = IDLBQCItemDao.GetListByHQL("LBQCMaterial.Id=" + testFormID);
            IDLBQCItemDao.GetListByHQL("LBQCMaterial.Id=" + testFormID).ToList().ForEach(a =>
            {
                lisTestItems.Where(item => item.LBItem.Id == a.LBItem.Id);
            });

            return baseResult;
        }

        public BaseResultDataValue EditLisTestFormBatchCheckByTestFormID(string testFormIDList, long empID, string empName, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrWhiteSpace(testFormIDList))
                return baseResultDataValue;
            string[] arrayTestFormID = testFormIDList.Split(',');
            foreach (string testFormID in arrayTestFormID)
                baseResultDataValue = EditLisTestFormCheckByTestFormID(long.Parse(testFormID), empID, empName, memoInfo);
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormCheckByTestFormID(long testFormID, long empID, string empName, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm lisTestForm = this.Get(testFormID);
            if (lisTestForm != null)
            {
                if (lisTestForm.MainStatusID.ToString() != TestFormMainStatus.初审.Key)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验单为【" + TestFormMainStatus.GetStatusDic()[lisTestForm.MainStatusID.ToString()].Name + "】状态，不能终审！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo + "条码号：" + lisTestForm.BarCode);
                    return baseResultDataValue;
                }
                lisTestForm.Checker = empName;
                lisTestForm.CheckerID = empID;
                lisTestForm.CheckInfo = memoInfo;
                lisTestForm.CheckTime = DateTime.Now;
                lisTestForm.StatusID = int.Parse(TestFormStatusID.终审.Key);
                lisTestForm.MainStatusID = int.Parse(TestFormMainStatus.终审.Key);
                this.Entity = lisTestForm;
                baseResultDataValue.success = this.Edit();
                if (baseResultDataValue.success)
                    IBLisOperate.AddLisOperate(lisTestForm, TestFormOperateType.终审.Value, "检验单终审", SysCookieValue);
                else
                {
                    baseResultDataValue.ErrorInfo = "检验单终审失败！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestFormNoExistByID;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormCheckCancelByTestFormID(long testFormID, long empID, string empName, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm lisTestForm = this.Get(testFormID);
            if (lisTestForm != null)
            {
                if (lisTestForm.MainStatusID.ToString() != TestFormMainStatus.终审.Key)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format(TestFormHintVar.Hint_TestFormState, TestFormMainStatus.GetStatusDic()[lisTestForm.MainStatusID.ToString()].Name, "反审");
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo + "条码号：" + lisTestForm.BarCode);
                    return baseResultDataValue;
                }
                AddisTestFormCheckCancelBackUp(lisTestForm);
                lisTestForm.StatusID = int.Parse(TestFormStatusID.反审.Key);
                lisTestForm.MainStatusID = int.Parse(TestFormMainStatus.检验中.Key);
                this.Entity = lisTestForm;
                baseResultDataValue.success = this.Edit();
                if (baseResultDataValue.success)
                    IBLisOperate.AddLisOperate(lisTestForm, TestFormOperateType.反审.Value, "检验单反审", SysCookieValue);
                else
                {
                    baseResultDataValue.ErrorInfo = "检验单反审失败！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestFormNoExistByID;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddisTestFormCheckCancelBackUp(long testFormID)
        {
            return AddisTestFormCheckCancelBackUp(this.Get(testFormID));
        }

        public BaseResultDataValue AddisTestFormCheckCancelBackUp(LisTestForm lisTestForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (lisTestForm != null)
            {
                LisTestForm newLisTestForm = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LisTestForm, LisTestForm>(lisTestForm);
                newLisTestForm.MainStatusID = int.Parse(TestFormMainStatus.反审备份.Key);
                newLisTestForm.OldTestFormID = lisTestForm.Id;
                this.Entity = newLisTestForm;
                if (this.Add())
                {
                    IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + lisTestForm.Id + TestItemCommoWhereHQL);
                    if (listTestItem != null && listTestItem.Count > 0)
                    {
                        foreach (LisTestItem lisTestItem in listTestItem)
                        {
                            LisTestItem newLisTestItem = ZhiFang.LabStar.Common.ClassMapperHelp.GetEntityMapper<LisTestItem, LisTestItem>(lisTestItem);
                            newLisTestItem.LisTestForm = newLisTestForm;
                            IBLisTestItem.Entity = newLisTestItem;
                            IBLisTestItem.Add();
                        }
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestFormNoExistByID;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormBatchConfirmByTestFormID(string testFormIDList, long empID, string empName, long? mainTesterId, string mainTester, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrWhiteSpace(testFormIDList))
                return baseResultDataValue;
            string[] arrayTestFormID = testFormIDList.Split(',');
            foreach (string testFormID in arrayTestFormID)
                baseResultDataValue = EditLisTestFormConfirmByTestFormID(long.Parse(testFormID), empID, empName, mainTesterId, mainTester, memoInfo, true);
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormConfirmByTestFormID(long testFormID, long empID, string empName, long? mainTesterId, string mainTester, string memoInfo, bool isCheckTestFormInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm lisTestForm = this.Get(testFormID);
            if (lisTestForm != null)
            {
                if (lisTestForm.MainStatusID.ToString() != TestFormMainStatus.检验中.Key)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验单为【" + TestFormMainStatus.GetStatusDic()[lisTestForm.MainStatusID.ToString()].Name + "】状态，不能确认！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo + "条码号：" + lisTestForm.BarCode);
                    return baseResultDataValue;
                }
                if (isCheckTestFormInfo)
                {
                    if (lisTestForm.FormInfoStatus != 1)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "检验单必要信息不完整（姓名，性别，样本类型），不能确认！";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo + "条码号：" + lisTestForm.BarCode);
                        return baseResultDataValue;
                    }
                    if (lisTestForm.TestAllStatus != 1)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "项目检测未完成，不能确认！";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo + "条码号：" + lisTestForm.BarCode);
                        return baseResultDataValue;
                    }
                    if (lisTestForm.ZFSysCheckStatus != 1)
                    {
                        baseResultDataValue.Code = 9;
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "{\"ErrorInfo\": \"智能审核未通过，不能确认！\",\"list\": " + lisTestForm.ZFSysCheckInfo + "}";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                        return baseResultDataValue;
                    }
                }

                if (lisTestForm.MainTesterId != null || lisTestForm.MainTesterId == 0)
                {
                    lisTestForm.MainTesterId = mainTesterId;
                    lisTestForm.MainTester = mainTester;
                }
                lisTestForm.Confirmer = empName;
                lisTestForm.ConfirmerId = empID;
                lisTestForm.ConfirmInfo = memoInfo;
                lisTestForm.ConfirmTime = DateTime.Now;
                lisTestForm.StatusID = int.Parse(TestFormStatusID.初审.Key);
                lisTestForm.MainStatusID = int.Parse(TestFormMainStatus.初审.Key);
                this.Entity = lisTestForm;
                baseResultDataValue.success = this.Edit();
                if (baseResultDataValue.success)
                {
                    IBLisOperate.AddLisOperate(lisTestForm, TestFormOperateType.初审.Value, isCheckTestFormInfo ? "检验单确认(检查信息是否完整)" : "检验单确认(不检查信息是否完整)", SysCookieValue);
                    IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + lisTestForm.Id + TestItemCommoWhereHQL);
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "检验单确认失败！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestFormNoExistByID;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormConfirmCancelByTestFormID(long testFormID, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm lisTestForm = this.Get(testFormID);
            if (lisTestForm != null)
            {
                if (lisTestForm.MainStatusID.ToString() != TestFormMainStatus.初审.Key)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验单为【" + TestFormMainStatus.GetStatusDic()[lisTestForm.MainStatusID.ToString()].Name + "】状态，不能取消确认！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo + "条码号：" + lisTestForm.BarCode);
                    return baseResultDataValue;
                }
                lisTestForm.MainStatusID = int.Parse(TestFormMainStatus.检验中.Key);
                this.Entity = lisTestForm;
                baseResultDataValue.success = this.Edit();
                if (baseResultDataValue.success)
                    IBLisOperate.AddLisOperate(lisTestForm, null, "检验单取消确认", memoInfo, SysCookieValue);
                else
                {
                    baseResultDataValue.ErrorInfo = "检验单取消确认失败！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestFormNoExistByID;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormSampleNoByTargetSampleNo(long sectionID, string curTestDate, string curMinSampleNo, int sampleCount, string targetTestDate, string targetMinSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            string strCurSampleNoList = "";
            IList<string> listSampleNo = BatchCreateNewSampleNoByOldSampleNo(curMinSampleNo, sampleCount);
            strCurSampleNoList = string.Join(",", listSampleNo.ToArray());
            strCurSampleNoList = "\'" + strCurSampleNoList.Replace(",", "\',\'") + "\'";
            IList<LisTestForm> tempCurList = this.SearchListByHQL(" listestform.LBSection.Id=" + sectionID + " and listestform.GTestDate=\'" + curTestDate + "\'" +
                " and listestform.GSampleNo in (" + strCurSampleNoList + ")" + TestFormCommoWhereHQL);
            IList<LisTestForm> tempList = tempCurList.Where(p => p.MainStatusID > 0).ToList();
            if (tempList != null && tempList.Count > 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "源检验单列表中存在已审核的检验单，不能执行修改！";
                return baseResultDataValue;
            }

            string strSampleNoList = "";
            IList<string> listTargetSampleNo = BatchCreateNewSampleNoByOldSampleNo(targetMinSampleNo, sampleCount);
            foreach (string tempSampleNo in listTargetSampleNo)
            {
                if (!listSampleNo.Contains(tempSampleNo))
                {
                    if (strSampleNoList == "")
                        strSampleNoList = tempSampleNo;
                    else
                        strSampleNoList += "," + tempSampleNo;

                }
            }
            strSampleNoList = "\'" + strSampleNoList.Replace(",", "\',\'") + "\'";
            IList<LisTestForm> tempTargetList = this.SearchListByHQL(" listestform.LBSection.Id=" + sectionID + " and listestform.GTestDate=\'" + targetTestDate + "\'" +
                " and listestform.GSampleNo in (" + strSampleNoList + ")" + TestFormCommoWhereHQL);
            if (tempTargetList != null && tempTargetList.Count > 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "目标检验单列表中存在样本号重复的检验单，不能执行修改！";
                return baseResultDataValue;
            }

            if (listTargetSampleNo.Count == listSampleNo.Count)
            {
                tempCurList = tempCurList.OrderBy(p => p.GSampleNoForOrder).ToList();
                for (int i = 0; i < tempCurList.Count; i++)
                {
                    string oldSampleNo = tempCurList[i].GSampleNo;
                    tempCurList[i].GTestDate = DateTime.Parse(targetTestDate);
                    tempCurList[i].GSampleNo = listTargetSampleNo[i];
                    tempCurList[i].GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(listTargetSampleNo[i]);
                    this.Entity = tempCurList[i];
                    baseResultDataValue.success = this.Edit();
                    if (baseResultDataValue.success)
                        IBLisOperate.AddLisOperate(tempCurList[i], null, "样本号批量修改：" + oldSampleNo + "→" + tempCurList[i].GSampleNo, SysCookieValue);
                }
            }
            return baseResultDataValue;
        }

        #region 检验单复检和取消复检


        public BaseResultDataValue EditLisTestItemReCheck(long testFormID, IList<LisTestItem> listReCheckTestItem, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            LisTestForm testForm = this.Get(testFormID);
            if (testForm != null)
            {
                IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID);
                if (listTestItem != null && listTestItem.Count > 0)
                {
                    if (listReCheckTestItem != null && listReCheckTestItem.Count > 0)
                    {
                        foreach (LisTestItem reCheckTestItem in listReCheckTestItem)
                        {
                            IList<LisTestItem> tempList = listTestItem.Where(p => p.Id == reCheckTestItem.Id).ToList();
                            if (tempList != null && tempList.Count > 0)
                            {
                                LisTestItem lisTestItem = tempList[0];
                                lisTestItem.RedoStatus = 1;//复检标志
                                lisTestItem.RedoDesc = reCheckTestItem.RedoDesc; //复检原因
                                if (string.IsNullOrWhiteSpace(lisTestItem.RedoValues))
                                    lisTestItem.RedoValues = lisTestItem.ReportValue;
                                else
                                {
                                    string[] tempArray = lisTestItem.RedoValues.Split(',');
                                    if (!tempArray.Contains(lisTestItem.ReportValue))
                                        lisTestItem.RedoValues += "," + lisTestItem.ReportValue;
                                }
                                lisTestItem.ReportValue = reCheckTestItem.ReportValue; //报告值
                                IBLisTestItem.Entity = lisTestItem;
                                if (IBLisTestItem.Edit())
                                {
                                    ZhiFang.LabStar.Common.LogHelp.Info("检验单项目【" + lisTestItem.LBItem.CName + "】复检！");
                                    IBLisOperate.AddLisOperate(lisTestItem, TestItemOperateType.复检.Value, "检验单项目复检", SysCookieValue);
                                }
                            }
                            else
                                ZhiFang.LabStar.Common.LogHelp.Info("未找到检验单项目信息，不能复检！检验单项目ID：" + reCheckTestItem.Id);
                        }

                    }
                    else
                    {
                        foreach (LisTestItem testItem in listTestItem)
                        {
                            testItem.RedoStatus = 1;//复检标志
                            testItem.RedoDesc = memoInfo; //复检原因
                            IBLisTestItem.Entity = testItem;
                            if (IBLisTestItem.Edit())
                            {
                                ZhiFang.LabStar.Common.LogHelp.Info("检验单项目【" + testItem.LBItem.CName + "】复检！");
                                IBLisOperate.AddLisOperate(testItem, TestItemOperateType.复检.Value, "检验单项目复检", SysCookieValue);
                            }
                        }
                    }
                    IList<LisTestItem> tempRedoList = listTestItem.Where(p => p.RedoStatus == 1).ToList();
                    if (tempRedoList != null && tempRedoList.Count > 0)
                    {
                        testForm.RedoStatus = 1;
                        this.Entity = testForm;
                        baseResultDataValue.success = this.Edit();
                        if (baseResultDataValue.success)
                            IBLisOperate.AddLisOperate(testForm, TestFormOperateType.复检.Value, "检验单复检", SysCookieValue);
                    }
                }
                else
                    ZhiFang.LabStar.Common.LogHelp.Info("检验单中无任何项目信息，不能复检！检验单ID：" + testFormID);
            }
            else if (testForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：检验单信息不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }


        public BaseResultDataValue EditLisTestFormReCheckCancel(string testFormIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrWhiteSpace(testFormIDList))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验单ID参数不能为空！";
            }
            else
            {
                string[] tempArray = testFormIDList.Split(',');
                foreach (string testFormID in tempArray)
                {
                    baseResultDataValue = EditLisTestFormReCheckCancel(long.Parse(testFormID), isClearRedoDesc, isClearRedoValues, memoInfo);
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestFormReCheckCancel(long testFormID, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                LisTestForm testForm = this.Get(testFormID);
                if (testForm != null && testForm.RedoStatus == 1)
                {
                    IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID);
                    if (listTestItem != null && listTestItem.Count > 0)
                    {
                        IList<LisTestItem> tempList = listTestItem.Where(p => p.RedoStatus == 1).ToList();
                        if (tempList != null && tempList.Count > 0)
                        {
                            foreach (LisTestItem entity in tempList)
                            {
                                entity.RedoStatus = 0;
                                if (isClearRedoDesc)
                                    entity.RedoDesc = "";
                                if (isClearRedoValues)
                                    entity.RedoValues = "";
                                IBLisTestItem.Entity = entity;
                                if (IBLisTestItem.Edit())
                                    IBLisOperate.AddLisOperate(entity, TestItemOperateType.取消复检.Value, "检验单项目取消复检", SysCookieValue);
                            }
                        }
                    }
                    testForm.RedoStatus = 0;
                    this.Entity = testForm;
                    baseResultDataValue.success = this.Edit();
                    IBLisOperate.AddLisOperate(testForm, TestFormOperateType.取消复检.Value, "取消复检", SysCookieValue);
                }
                else if (testForm == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：检验单信息不能为空！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "提示信息：检验单不是复检状态！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditLisTestItemReCheckCancel(long testFormID, string testItemIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (string.IsNullOrWhiteSpace(testItemIDList))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "检验单项目ID参数不能为空！";
                return baseResultDataValue;
            }

            LisTestForm testForm = this.Get(testFormID);
            if (testForm != null && testForm.RedoStatus == 1)
            {
                IList<LisTestItem> listTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID);
                if (listTestItem != null && listTestItem.Count > 0)
                {
                    string[] tempArray = testItemIDList.Split(',');
                    foreach (string testItemID in tempArray)
                    {
                        IList<LisTestItem> tempList = listTestItem.Where(p => p.Id == long.Parse(testItemID)).ToList();
                        if (tempList != null && tempList.Count > 0)
                        {
                            LisTestItem entity = tempList[0];
                            if (entity.RedoStatus == 1)
                            {
                                entity.RedoStatus = 0;
                                if (isClearRedoDesc)
                                    entity.RedoDesc = "";
                                if (isClearRedoValues)
                                    entity.RedoValues = "";
                                IBLisTestItem.Entity = entity;
                                if (IBLisTestItem.Edit())
                                {
                                    ZhiFang.LabStar.Common.LogHelp.Info("检验单项目【" + entity.LBItem.CName + "】取消复检！");
                                    IBLisOperate.AddLisOperate(entity, TestItemOperateType.取消复检.Value, "检验单项目取消复检", SysCookieValue);
                                }
                            }
                            else
                                ZhiFang.LabStar.Common.LogHelp.Info("检验单项目【" + entity.LBItem.CName + "】不是复检项目，不能取消复检！");
                        }
                        else
                            ZhiFang.LabStar.Common.LogHelp.Info("未找到检验单项目信息，不能取消复检！检验单项目ID：" + testItemID);
                    }
                    IList<LisTestItem> tempRedoList = listTestItem.Where(p => p.RedoStatus == 1).ToList();
                    if (tempRedoList == null || tempRedoList.Count == 0)
                    {
                        testForm.RedoStatus = 0;
                        this.Entity = testForm;
                        baseResultDataValue.success = this.Edit();
                        if (baseResultDataValue.success)
                            IBLisOperate.AddLisOperate(testForm, TestFormOperateType.取消复检.Value, "取消复检", SysCookieValue);
                    }
                }
                else
                    ZhiFang.LabStar.Common.LogHelp.Info("检验单中无任何项目信息，不能取消复检！检验单ID：" + testFormID);
            }
            else if (testForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：检验单信息不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "提示信息：检验单不是复检状态！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }


        #endregion

        #region 历史对比

        /// <summary>
        /// 检验单项目结果历史对比
        /// 对已保存检验单中的项目结果进行结果历史对比并保存结果对比值
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestItemResultHistoryCompareByTestFormID(long testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("检验单历史对比获取检验单及其项目信息！");
            LisTestForm testForm = this.Get(testFormID);
            if (testForm != null)
            {
                ItemHistoryComparePara ihcp = IBBParaItem.GetItemHistoryComparePara(testForm.LBSection.Id.ToString(), "", "");
                if (ihcp.IsHistoryCompare)
                {
                    IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestform.Id=" + testFormID + TestItemCommoWhereHQL);
                    if (listTestItem != null && listTestItem.Count > 0)
                    {
                        baseResultDataValue = EditLisTestItemResultHistoryCompare(testForm, ref listTestItem, ihcp);
                        foreach (LisTestItem testItem in listTestItem)
                        {
                            IBLisTestItem.Entity = testItem;
                            IBLisTestItem.Edit();
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "无法获取检验单项目信息！";
                        ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                    }
                }
                else 
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验小组：" + testForm.LBSection.CName + ",历史对比参数未打开，请设置此参数！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = TestFormHintVar.Hint_TestFormNoExistByID;
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 对指定的检验单项目结果历史对比
        /// 对未保存的检验单项目结果进行结果历史对比并返回结果对比值
        /// </summary>
        /// <param name="testForm">检验单实体</param>
        /// <param name="listTestItem">检验单项目列表</param>
        /// <returns></returns>
        public EntityList<LisTestItem> EditLisTestItemResultHistoryCompareByTestItem(LisTestForm testForm, ref IList<LisTestItem> listTestItem)
        {
            EntityList<LisTestItem> entityList = new EntityList<LisTestItem>();
            ZhiFang.LabStar.Common.LogHelp.Info("检验单历史对比获取检验单及其项目信息！");
            if (testForm != null)
            {
                ItemHistoryComparePara ihcp = IBBParaItem.GetItemHistoryComparePara(testForm.LBSection.Id.ToString(), "", "");
                if (ihcp.IsHistoryCompare)
                {
                    if (listTestItem != null && listTestItem.Count > 0)
                    {                      
                        EditLisTestItemResultHistoryCompare(testForm, ref listTestItem, ihcp);
                        entityList.count = listTestItem.Count;
                        entityList.list = listTestItem;
                    }
                    else
                        ZhiFang.LabStar.Common.LogHelp.Info("无法获取检验单项目信息！");
                }
                else
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("检验小组：" + testForm.LBSection.CName + ",历史对比参数未打开，请设置此参数！");
                }
            }
            else
                ZhiFang.LabStar.Common.LogHelp.Info("无法获取检验单信息！");
            return entityList;
        }

        /// <summary>
        /// 检验单历史对比
        /// </summary>
        /// <param name="testForm">检验单实体</param>
        /// <param name="listTestItem">检验单项目列表</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestItemResultHistoryCompare(LisTestForm testForm, ref IList<LisTestItem> listTestItem, ItemHistoryComparePara ihcp)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            if (testForm == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取检验单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            if (listTestItem == null || listTestItem.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取检验单项目信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            ZhiFang.LabStar.Common.LogHelp.Info("检验单历史对比开始！条码号：" + testForm.BarCode);
            //根据参数组合获取历史检验单的HQL条件
            DateTime curTestDate = ((DateTime)testForm.GTestDate);//当前样本检验日期
            string beginTestDate = curTestDate.AddDays(-ihcp.HistoryCompareDays).ToString("yyyy-MM-dd");
            string endTestDate = curTestDate.ToString("yyyy-MM-dd");
            string strTestFormHQL = " listestform.GTestDate>=\'" + beginTestDate + "\' and listestform.GTestDate<=\'" + endTestDate + "\'" + TestFormCommoWhereHQL;
            string strTempHQL = getItemHistoryCompareHQL(testForm, ihcp);
            if (strTempHQL.Length == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法获取历史检验单的HQL条件！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            strTestFormHQL += strTempHQL;
            //获取历史检验单信息
            IList<LisTestForm> listHistoryTestForm = this.QueryLisTestForm(strTestFormHQL, "").Where(p => p.Id != testForm.Id).ToList();
            if (listHistoryTestForm == null || listHistoryTestForm.Count == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无历史检验单信息！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            else
            {
                testForm.IExamine = listHistoryTestForm.Count;
                this.Entity = testForm;
                this.Add();
            }

            foreach (LisTestItem testItem in listTestItem)
            {
                //获取历史检验单项目信息
                IList<LisTestItem> listHistoryTestItem = IBLisTestItem.QueryLisTestItem(strTestFormHQL + " and lbitem.Id=" + testItem.LBItem.Id + TestItemCommoWhereHQL);
                listHistoryTestItem = listHistoryTestItem.Where(p => p.LisTestForm.Id != testForm.Id && (p.ReportValue != null && p.ReportValue.Trim() != "")).ToList();
                if (listHistoryTestItem == null || listHistoryTestItem.Count == 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("项目【" + testItem.LBItem.CName + "】无历史检验项目信息！");
                    continue;
                }

                testItem.HisResultCount = ihcp.CompareRecordCount;
                listHistoryTestItem = listHistoryTestItem.OrderByDescending(p => p.LisTestForm.GTestDate).OrderByDescending(p=>p.LisTestForm.DataAddTime).ToList();
                LisTestItem tempTestItem = listHistoryTestItem[0];
                if (ihcp.CompareRecordCount == 1)
                {
                    string preCompStatus = "";
                    testItem.PreResultID = tempTestItem.Id;
                    testItem.PreGTestDate = tempTestItem.GTestDate;
                    testItem.PreValue = tempTestItem.ReportValue;
                    if (testItem.QuanValue != null && tempTestItem.QuanValue != null)
                        testItem.PreValueComp = _GetItemHistoryCompareResult(testItem.QuanValue, tempTestItem.QuanValue, ihcp, ref preCompStatus);
                    else
                        testItem.PreValueComp = _GetItemHistoryCompareResult(testItem.ReportValue, tempTestItem.ReportValue, ihcp, ref preCompStatus);
                    //testItem.PreCompStatus = tempTestItem.ResultStatus;
                    testItem.PreCompStatus = preCompStatus;

                    testItem.PreTestItemID2 = null;
                    testItem.PreGTestDate2 = null;
                    testItem.PreValue2 = null;
                    testItem.PreTestItemID3 = null;
                    testItem.PreGTestDate3 = null;
                    testItem.PreValue3 = null;
                }
                else
                {
                    int count = 1;
                    foreach (LisTestItem tempEntity in listHistoryTestItem)
                    {
                        tempTestItem = tempEntity;
                        if (count > ihcp.CompareRecordCount)
                            break;
                        if (count == 1)
                        {
                            string preCompStatus = "";
                            testItem.PreResultID = tempTestItem.Id;
                            testItem.PreGTestDate = tempTestItem.GTestDate;
                            testItem.PreValue = tempTestItem.ReportValue;
                            if (testItem.QuanValue != null && tempTestItem.QuanValue != null)
                                testItem.PreValueComp = _GetItemHistoryCompareResult(testItem.QuanValue, tempEntity.QuanValue, ihcp, ref preCompStatus);
                            else
                                testItem.PreValueComp = _GetItemHistoryCompareResult(testItem.ReportValue, tempEntity.ReportValue, ihcp, ref preCompStatus);
                            //testItem.PreCompStatus = tempTestItem.ResultStatus;
                            testItem.PreCompStatus = preCompStatus;

                            testItem.PreTestItemID2 = null;
                            testItem.PreGTestDate2 = null;
                            testItem.PreValue2 = null;
                            testItem.PreTestItemID3 = null;
                            testItem.PreGTestDate3 = null;
                            testItem.PreValue3 = null;
                        }
                        else if (count == 2)
                        {
                            testItem.PreTestItemID2 = tempTestItem.Id;
                            testItem.PreGTestDate2 = tempTestItem.GTestDate;
                            testItem.PreValue2 = tempTestItem.ReportValue;
                            testItem.PreTestItemID3 = null;
                            testItem.PreGTestDate3 = null;
                            testItem.PreValue3 = null;
                        }
                        else if (count == 3)
                        {
                            testItem.PreTestItemID3 = tempTestItem.Id;
                            testItem.PreGTestDate3 = tempTestItem.GTestDate;
                            testItem.PreValue3 = tempTestItem.ReportValue;
                        }
                        count++;
                    }
                }
                //IBLisTestItem.Entity = testItem;
                //IBLisTestItem.Edit();
            }
            ZhiFang.LabStar.Common.LogHelp.Info("检验单历史对比结束！条码号：" + testForm.BarCode);
            return baseResultDataValue;
        }

        private string getItemHistoryCompareHQL(LisTestForm testForm, ItemHistoryComparePara ihcp)
        {
            string resultHQL = "";
            if (ihcp.IsCompareSampleType && testForm.GSampleTypeID != null && testForm.GSampleTypeID > 0)
                resultHQL += " and listestform.GSampleTypeID=" + testForm.GSampleTypeID;

            if (ihcp.HistoryCompareSection.Trim().Length > 0)
                resultHQL += " and listestform.LBSection.Id in (" + ihcp.HistoryCompareSection + ")";

            if (ihcp.HistoryCompareField.Trim().Length > 0)
            {
                string[] patientField = ihcp.HistoryCompareField.Split('|');
                if (patientField.Contains("PatNo", StringComparer.OrdinalIgnoreCase) && testForm.PatNo != null && testForm.PatNo.Trim().Length > 0)
                    resultHQL += " and listestform.PatNo=\'" + testForm.PatNo + "\'";
                if (patientField.Contains("CName", StringComparer.OrdinalIgnoreCase) && testForm.CName != null && testForm.CName.Trim().Length > 0)
                    resultHQL += " and listestform.CName=\'" + testForm.CName + "\'";
            }
            return resultHQL;
        }


        /// <summary>
        /// 定性结果历史对比
        /// </summary>
        /// <param name="curItemResult">当前结果</param>
        /// <param name="oldItemResult">历史结果</param>
        /// <returns></returns>
        private string _GetItemHistoryCompareResult(double? curItemResult, double? oldItemResult, ItemHistoryComparePara ihcp, ref string preCompStatus)
        {
            string resultValue = "";

            if (curItemResult != null && oldItemResult != null)
            {

                double? diffValue = curItemResult - oldItemResult;
                if (oldItemResult != 0)
                {
                    double? percentValue = diffValue * 100 / oldItemResult;
                    resultValue = string.Format("{0:N" + ihcp.DecimalBit.ToString() + "}", percentValue);
                    if (percentValue >= ihcp.DiffValueH)
                        preCompStatus = ihcp.DiffValueHFalg;
                    if (percentValue <= 0 - ihcp.DiffValueH)
                        preCompStatus = ihcp.DiffValueLFalg;
                    if (percentValue >= ihcp.DiffValueHH)
                        preCompStatus = ihcp.DiffValueHHFalg;
                    if (percentValue <= 0 - ihcp.DiffValueHH)
                        preCompStatus = ihcp.DiffValueLLFalg;
                }
                else if (curItemResult != 0)
                    resultValue = "0";
                //注意：当curItemResult和oldItemResult都为0的时候，resultValue="";
            }
            resultValue = resultValue + "%";
            return resultValue;
        }

        /// <summary>
        /// 定性结果历史对比
        /// </summary>
        /// <param name="curItemResult">当前结果</param>
        /// <param name="oldItemResult">历史结果</param>
        /// <returns></returns>
        private string _GetItemHistoryCompareResult(string curItemResult, string oldItemResult, ItemHistoryComparePara ihcp, ref string preCompStatus)
        {
            string resultValue = "";
            if (curItemResult != null && curItemResult.Trim().Length > 0 && oldItemResult != null && oldItemResult.Trim().Length > 0)
            {
                int dxFlag = 0;//定性对比临时标志
                if (curItemResult.IndexOf("+") >= 0 || curItemResult.ToUpper().IndexOf("POS") >= 0 || curItemResult.IndexOf("阳") >= 0)
                {
                    dxFlag = 1;
                }
                else if (curItemResult.IndexOf("-") >= 0 || curItemResult.ToUpper().IndexOf("NEG") >= 0 || curItemResult.IndexOf("阴") >= 0)
                {
                    dxFlag = 2;
                }

                if (dxFlag == 2 && (oldItemResult.IndexOf("+") >= 0 || oldItemResult.ToUpper().IndexOf("POS") >= 0 || oldItemResult.IndexOf("阳") >= 0))
                {
                    resultValue = "阳转阴";
                }
                else if (dxFlag == 1 && (oldItemResult.IndexOf("-") >= 0 || oldItemResult.ToUpper().IndexOf("NEG") >= 0 || oldItemResult.IndexOf("阴") >= 0))
                {
                    resultValue = "阴转阳";
                }
            }
            return resultValue;
        }

        #endregion

        #region 智能审核判定和标志更新

        /// <summary>
        /// 更新检验单智能审核状态ZFSysCheckStatus
        /// 此服务只更新智能审核标志和原因，不做任何状态判断
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="checkFlag">智能审核标志：1为智能审核成功，0未进行智能审核，-1审核失败</param>
        /// <param name="checkInfo">智能审核原因说明</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestFormZFSysCheckStatus(long testFormID, int checkFlag, string checkInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LisTestForm testForm = this.Get(testFormID);
            if (testForm != null)
            {
                if (testForm.MainStatusID.ToString() != TestFormMainStatus.检验中.Key)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "检验单为【" + TestFormMainStatus.GetStatusDic()[testForm.MainStatusID.ToString()].Name + "】状态，不能确认！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo + "条码号：" + testForm.BarCode);
                    return baseResultDataValue;
                }
                testForm.ZFSysCheckStatus = checkFlag;
                testForm.ZFSysCheckInfo = checkInfo;
                this.Entity = testForm;
                baseResultDataValue.success = this.Edit();
                if (!baseResultDataValue.success)
                {
                    baseResultDataValue.ErrorInfo = "更新检验单智能审核状态失败！";
                    ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取不到指定的检验单！";
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue EditJudgeLisTestFormZFSysCheckStatus(LisTestForm testForm, IList<LisTestItem> listLisTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<BPara> listPara = IBBParaItem.QueryParaValueByParaTypeCode("NTestType_SysJudge_TestItem_Para," +
                "NTestType_SysJudge_OrderInfo_Para,NTestType_SysJudge_TestDate_Para,NTestType_SysJudge_TestResult_Para,NTestType_SysJudge_OtherInfo_Para", testForm.LBSection.Id.ToString(), "", "");
            baseResultDataValue = QueryAutoCheckLisTestForm(testForm, listLisTestItem, listPara, "");
            if (baseResultDataValue.success)
                testForm.ZFSysCheckStatus = 1;
            else
            {
                if (baseResultDataValue.Code == -1)
                    testForm.ZFSysCheckStatus = 0;
                else
                    testForm.ZFSysCheckStatus = -1;
            }
            testForm.ZFSysCheckInfo = baseResultDataValue.ResultDataValue;
            this.Entity = testForm;
            baseResultDataValue.success = this.Edit();
            if (!baseResultDataValue.success)
            {
                baseResultDataValue.ErrorInfo = "更新检验单系统自动审核状态失败！";
                ZhiFang.LabStar.Common.LogHelp.Info(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        private BaseResultDataValue QueryAutoCheckLisTestForm(LisTestForm testForm, IList<LisTestItem> listTestItem, IList<BPara> listPara, string checkerID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            bool isAutoCheckFlag = true;
            ZhiFang.LabStar.Common.LogHelp.Info("检验单自动审核判定开始！");
            //判定失败信息
            StringBuilder checkInfo = new StringBuilder();
            //临时存储变量
            StringBuilder tempInfo = new StringBuilder();
            if (testForm == null)
            {
                isAutoCheckFlag = false;
                checkInfo.Append(",\"检验单为空！\"");
            }
            if (listTestItem == null || listTestItem.Count == 0)
            {
                isAutoCheckFlag = (isAutoCheckFlag && false);
                checkInfo.Append(",\"检验单项目为空！\"");
            }
            if (testForm.MainStatusID.ToString() != TestFormMainStatus.检验中.Key)
            {
                isAutoCheckFlag = (isAutoCheckFlag && false);
                checkInfo.Append(",\"检验单为【" + TestFormMainStatus.GetStatusDic()[testForm.MainStatusID.ToString()].Name + "】状态！\"");
            }
            ZhiFang.LabStar.Common.LogHelp.Info("检验日期---" + testForm.GTestDate.ToString() + ", 样本号---" + testForm.GSampleNo + ", ID-- - " + testForm.Id);
            if (testForm.FormInfoStatus != 1)
            {
                isAutoCheckFlag = (isAutoCheckFlag && false);
                checkInfo.Append(",\"检验单必要信息不完整（姓名，性别，样本类型）！\"");
            }
            if (testForm.TestAllStatus != 1)
            {
                isAutoCheckFlag = (isAutoCheckFlag && false);
                checkInfo.Append(",\"项目检测未完成！\"");
            }
            if (!isAutoCheckFlag)
            {
                brdv.success = false;
                brdv.Code = -1;
                brdv.ResultDataValue = checkInfo.ToString().Remove(0, 1);
                ZhiFang.LabStar.Common.LogHelp.Info("检验单基本信息不完整，不做智能审核判定！提示信息：" + brdv.ResultDataValue);
                return brdv;
            }
            #region 申请信息校验--基本检查，不需要参数控制

            //判定检验单的医嘱单是否为空

            if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_OrderInfo_0001"))
            {
                string testFormSource = testForm.ISource.ToString();
                //if ((testFormSource == TestFormSource.人工核收.Key || testFormSource == TestFormSource.分发核收.Key || testFormSource == TestFormSource.通讯核收.Key) && testForm.LisOrderForm == null)
                if (testForm.LisOrderForm == null)
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"" + TestFormHintVar.Hint_NoExistOrderForm + "\"");
                }
            }

            //患者的姓名、性别、就诊类型、出生日期、年龄、年龄单位这几个属性值是否为空，如有一项为空即为患者信息不完整
            if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_OrderInfo_0002"))
            {
                if (string.IsNullOrWhiteSpace(testForm.CName))
                {
                    isAutoCheckFlag = false;
                    //checkInfo.Append(",\"姓名不能为空！\"");
                    tempInfo.Append(",姓名");
                }
                if (testForm.GenderID == null)
                {
                    isAutoCheckFlag = false;
                    //checkInfo.Append(",\"性别不能为空！\"");
                    tempInfo.Append(",性别");
                }
                if (testForm.LisPatient == null || testForm.LisPatient.Birthday == null)
                {
                    isAutoCheckFlag = false;
                    //checkInfo.Append(",\"出生日期不能为空！\"");
                    tempInfo.Append(",出生日期");
                }
                if (testForm.Age == null)
                {
                    isAutoCheckFlag = false;
                    //checkInfo.Append(",\"年龄不能为空！\"");
                    tempInfo.Append(",年龄");
                }

                if (testForm.AgeUnitID == null)
                {
                    isAutoCheckFlag = false;
                    //checkInfo.Append(",\"年龄单位不能为空！\"");
                    tempInfo.Append(",年龄单位");
                }
                if (testForm.SickTypeID == null)
                {
                    isAutoCheckFlag = false;
                    //checkInfo.Append(",\"就诊类型不能为空！\"");
                    tempInfo.Append(",就诊类型");
                }
                if (tempInfo.Length > 0)
                {
                    checkInfo.Append(",\"" + string.Format(PatientInfoHintVar.Hint_ParaIsEmpty, tempInfo.ToString().Remove(0, 1)) + "\"");
                    tempInfo.Clear();
                }
            }
            //样本特殊性状检查,样本单SampleSpecialDesc字段的值是否为空，如不为空，则样本属于特殊性状
            if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_OrderInfo_0003"))
            {
                if (!string.IsNullOrWhiteSpace(testForm.SampleSpecialDesc))
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"样本处于特殊性状检查！\"");
                }
            }
            //检验项目与医嘱项目对比检查
            if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_OrderInfo_0004"))
            {
                IList<LisTestItem> tempList = listTestItem.Where(p => p.OrdersItemID == null || p.OrdersItemID == 0).ToList();
                if (tempList != null && tempList.Count > 0)
                {
                    isAutoCheckFlag = false;
                    foreach (LisTestItem testItem in tempList)
                    {
                        tempInfo.Append("," + testItem.LBItem.CName);
                    }
                    if (tempInfo.Length > 0)
                    {
                        checkInfo.Append(",\"" + string.Format(TestItemHintVar.Hint_ParaNoOrderItem, tempList.Count, tempInfo.ToString().Remove(0, 1)) + "\"");
                        tempInfo.Clear();
                    }
                }
            }
            #endregion

            #region 检验时间校验

            #region 各时间节点是否为空校验
            if ((IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestDate_0001")) && testForm.CollectTime == null)
            {
                isAutoCheckFlag = false;
                checkInfo.Append(",\"" + EachTimePointCheckHintVar.Hint_CollectTimeIsEmpty + "\"");
            }

            if ((IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestDate_0003")) && testForm.InceptTime == null)
            {
                isAutoCheckFlag = false;
                checkInfo.Append(",\"" + EachTimePointCheckHintVar.Hint_InceptTimeIsEmpty + "\"");
            }

            if ((IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestDate_0004")) && testForm.OnLineTime == null)
            {
                isAutoCheckFlag = false;
                checkInfo.Append(",\"" + EachTimePointCheckHintVar.Hint_OnLineTimeeIsEmpty + "\"");
            }

            if ((IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestDate_0002")) && testForm.TestTime == null)
            {
                isAutoCheckFlag = false;
                checkInfo.Append(",\"" + EachTimePointCheckHintVar.Hint_TestTimeIsEmpty + "\"");
            }

            //if (testForm.TestEndTime == null)
            //{
            //    isAutoCheckFlag = false;
            //    checkInfo.Append(",\"检验完成时间不能为空！\"");
            //}
            #endregion 

            #region 各时间节点正确性检查（采样时间 < 收样时间 < 上机时间 < 检验时间）

            if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestDate_0006"))
            {
                if (testForm.CollectTime != null && testForm.InceptTime != null && testForm.CollectTime >= testForm.InceptTime)
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"采样时间 > 收样时间！\"");
                }

                if (testForm.InceptTime != null && testForm.OnLineTime != null && testForm.InceptTime >= testForm.OnLineTime)
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"收样时间 > 上机时间！\"");
                }

                if (testForm.TestTime != null && testForm.OnLineTime != null && testForm.OnLineTime >= testForm.TestTime)
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"上机时间 > 检验时间！\"");
                }

                if (testForm.CollectTime != null && testForm.InceptTime == null && testForm.OnLineTime != null && testForm.CollectTime >= testForm.OnLineTime)
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"采样时间 > 上机时间！\"");
                }

                if (testForm.CollectTime != null && testForm.InceptTime == null && testForm.OnLineTime == null && testForm.TestTime == null && testForm.CollectTime >= testForm.TestTime)
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"采样时间 > 检验时间！\"");
                }

                if (testForm.InceptTime != null && testForm.OnLineTime == null && testForm.TestTime == null && testForm.CollectTime >= testForm.TestTime)
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"收样时间 > 检验时间！\"");
                }
            }

            #endregion

            #region 采样到检验超时检查
            if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestDate_0005"))
            {
                if (testForm.CollectTime != null && testForm.TestTime != null)
                {
                    BaseResultDataValue tempResult = IBLBItemTimeW.QueryLisTestItemOverTime(testForm, listTestItem);
                    if (!tempResult.success)
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(tempResult.ErrorInfo);
                    }
                }
            }
            #endregion

            #endregion

            #region 检验结果校验

            foreach (LisTestItem testItem in listTestItem)
            {
                //if (string.IsNullOrWhiteSpace(testItem.ReportValue))
                //{
                //    isAutoCheckFlag = false;
                //    checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果为空！\"");
                //    continue;
                //}

                //结果值非负检查
                if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0003") && (testItem.QuanValue < 0))
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果为负值！\"");
                }

                //仪器结果报警
                if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0009") && (!string.IsNullOrEmpty(testItem.EResultAlarm)))
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】仪器结果报警！\"");
                }

                //历史对比差异大
                if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0008") && (testItem.PreCompStatus == "▲" || testItem.PreCompStatus == "▼"))
                {
                    isAutoCheckFlag = false;
                    checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果异常 " + testItem.PreCompStatus + "\"");
                }

                if (!string.IsNullOrWhiteSpace(testItem.ResultStatusCode))
                {
                    string resultStatusCode = testItem.ResultStatusCode.ToUpper();
                    //结果有效检查
                    if (resultStatusCode == "U")
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果无效！\"");
                        continue;
                    }

                    //结果异常高/异常低状态检查
                    if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0001") && (resultStatusCode == "HH" || resultStatusCode == "LL"))
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果异常 " + resultStatusCode + "\"");
                        continue;
                    }
                    //结果偏高/偏低状态检查
                    if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0002") && (resultStatusCode == "H" || resultStatusCode == "L"))
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果异常 " + resultStatusCode + "\"");
                        continue;
                    }

                    //结果阳性检查（包含阳性 + 等）
                    if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0004") && (resultStatusCode == "POS" || resultStatusCode == "+"))
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果异常 " + resultStatusCode + "\"");
                        continue;
                    }

                    //结果弱阳性检查
                    if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0005") && (resultStatusCode == "LOWPOS" || resultStatusCode == "+-"))
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果异常 " + resultStatusCode + "\"");
                        continue;
                    }

                    //结果异常检查 
                    if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0006") && (resultStatusCode == "ABN" || resultStatusCode == "A" || resultStatusCode == "NEG-A"))
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果异常 " + resultStatusCode + "\"");
                        continue;
                    }

                    //结果警告状态
                    if (IBBPara.JudgeParaBoolValue(listPara, "NTestType_SysJudge_TestResult_0007") && resultStatusCode == "E")
                    {
                        isAutoCheckFlag = false;
                        checkInfo.Append(",\"检验项目【" + testItem.LBItem.CName + "】结果警告状态 " + resultStatusCode + "\"");
                        continue;
                    }
                }
            }

            #endregion

            #region 其它校验

            #endregion

            if (isAutoCheckFlag)
                brdv.ResultDataValue = "系统自动审核成功";
            else
                brdv.ResultDataValue = "[" + checkInfo.ToString().Remove(0, 1) + "]";
            ZhiFang.LabStar.Common.LogHelp.Info("检验单自动审核判定结束！判定结果：" + isAutoCheckFlag.ToString() + "---" + brdv.ResultDataValue);
            brdv.success = isAutoCheckFlag;
            return brdv;
        }

        #endregion

        #region 危急值生成与发送

        /// <summary>
        /// 判断检验单是否有危急值，有则新增危急值信息
        /// </summary>
        /// <param name="testFormID"></param>
        /// <returns></returns>
        public BaseResultDataValue AddLisTestFormPanicValueMsg(long testFormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            LisTestForm lisTestForm = this.Get(testFormID);
            if (lisTestForm != null)
            {
                IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + lisTestForm.Id + " and listestitem.MainStatusID>=0 ");
                brdv = AddLisTestFormPanicValueMsg(lisTestForm, listTestItem);
            }
            else
            {
                brdv.success = false;
                brdv.Code = -1;
                brdv.ErrorInfo = TestFormHintVar.Hint_TestFormIsEmpty;
                ZhiFang.LabStar.Common.LogHelp.Info("危急值判定：" + brdv.ErrorInfo);
            }
            return brdv;
        }

        /// <summary>
        /// 判断检验单是否有危急值，有则新增危急值信息
        /// </summary>
        /// <param name="lisTestForm"></param>
        /// <param name="listTestItem"></param>
        /// <returns></returns>
        public BaseResultDataValue AddLisTestFormPanicValueMsg(LisTestForm lisTestForm, IList<LisTestItem> listTestItem)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (lisTestForm != null)
            {
                if (listTestItem != null && listTestItem.Count > 0)
                {
                    //本次检验单产生的危急值项目记录
                    IList<LisTestItem> panicValueList = listTestItem.Where(p => p.ResultStatusCode != null && (p.AlarmLevel == int.Parse(TestFormReportValueAlarmLevel.危急.Key))).ToList();
                    if (panicValueList != null && panicValueList.Count > 0)
                    {
                        LisTestFormMsg testFormMsg = null;
                        //查找检验单是否产生过危急值记录
                        IList<LisTestFormMsg> listLisTestFormMsg = IBLisTestFormMsg.SearchListByHQL(" listestformmsg.LisTestForm.Id=" + lisTestForm.Id );                       
                        if (listLisTestFormMsg != null && listLisTestFormMsg.Count > 0)
                        {
                            //查找检验单是否有未发送的危急值记录
                            listLisTestFormMsg = listLisTestFormMsg.Where(p => p.ReportStatus == 0).ToList();
                            if (listLisTestFormMsg != null && listLisTestFormMsg.Count > 0)
                                testFormMsg = listLisTestFormMsg[0];
                            else
                                testFormMsg = new LisTestFormMsg();
                            //查询检验单产生的所有危急值值项目记录
                            IList<LisTestFormMsgItem> listLisTestMsgItem = IBLisTestFormMsgItem.SearchListByHQL(" listestformmsgitem.TestFormID=" + lisTestForm.Id);
                            if (listLisTestMsgItem != null && listLisTestMsgItem.Count > 0)
                            {
                                //循环本次检验单产生的危急值项目记录，并判断本次危急值项目是否存在于旧危急值项目记录中，如果存在则删除
                                for (int i = panicValueList.Count - 1; i >= 0; i--)
                                {
                                    LisTestItem testItem = panicValueList[i];
                                    IList<LisTestFormMsgItem> tempList = listLisTestMsgItem.Where(p => p.ItemID == testItem.LBItem.Id && (p.LisTestFormMsg.ReportStatus == 0 || p.LisTestFormMsg.ReportStatus == 1) &&
                                                                                                  p.ReportValue == testItem.ReportValue && p.ResultStatusCode == testItem.ResultStatusCode).ToList();
                                    if (tempList != null && tempList.Count > 0)
                                    {
                                        panicValueList.Remove(panicValueList[i]);
                                    }
                                }
                            }
                        }
                        else
                            testFormMsg = new LisTestFormMsg();

                        if (panicValueList != null && panicValueList.Count > 0)
                        {

                            testFormMsg.MsgType = 1;
                            testFormMsg.PatNo = lisTestForm.PatNo;
                            testFormMsg.BarCode = lisTestForm.BarCode;
                            testFormMsg.GTestDate = lisTestForm.GTestDate;
                            testFormMsg.LisTestForm = lisTestForm;

                            IBLisTestFormMsg.Entity = testFormMsg;
                            if (listLisTestFormMsg != null && listLisTestFormMsg.Count > 0)
                                IBLisTestFormMsg.Edit();
                            else
                                IBLisTestFormMsg.Add();

                            string testFormAlarmInfo = "";
                            string detailDesc = "";
                            foreach (LisTestItem panicValue in panicValueList)
                            {
                                LisTestFormMsgItem testItemMsg = new LisTestFormMsgItem();
                                testItemMsg.LisTestFormMsg = testFormMsg;
                                testItemMsg.TestFormID = testFormMsg.LisTestForm.Id;
                                testItemMsg.TestItemID = panicValue.Id;
                                testItemMsg.ItemID = panicValue.LBItem.Id;
                                testItemMsg.ItemName = panicValue.LBItem.CName;
                                testItemMsg.ReportValue = panicValue.ReportValue;
                                testItemMsg.ResultStatus = panicValue.ResultStatus;
                                testItemMsg.ResultStatusCode = panicValue.ResultStatusCode;
                                testItemMsg.AlarmLevel = panicValue.AlarmLevel;
                                testItemMsg.AlarmInfo = panicValue.AlarmInfo;
                                IBLisTestFormMsgItem.Entity = testItemMsg;
                                IBLisTestFormMsgItem.Add();

                                panicValue.AlarmInfo = panicValue.LBItem.CName + panicValue.ResultStatus;
                                IBLisTestItem.Entity = panicValue;
                                IBLisTestItem.Edit();

                                testFormAlarmInfo = testFormAlarmInfo == "" ? panicValue.AlarmInfo : testFormAlarmInfo + "," + panicValue.AlarmInfo;
                                string tempPanicValue = panicValue.LBItem.CName + "=" + panicValue.ReportValue + "(" + panicValue.ResultStatusCode + ")";
                                detailDesc = detailDesc == "" ? tempPanicValue : detailDesc + "," + tempPanicValue;
                            }
                            testFormMsg.MasterDesc = testFormAlarmInfo;
                            testFormMsg.DetailDesc = detailDesc;
                            IBLisTestFormMsg.Entity = testFormMsg;
                            IBLisTestFormMsg.Edit();
                            if (panicValueList != null && panicValueList.Count > 0)
                            {
                                IList<string> listEmpID = IBLBRight.QueryEmpIDBySectionID(lisTestForm.LBSection.Id);
                                if (listEmpID != null && listEmpID.Count > 0)
                                    listEmpID = listEmpID.Distinct().ToList();

                                SysDelegateVar.SendSysMsgDelegateVar("", SysMessageType.危急值消息.Value.Code, lisTestForm.LBSection.Id.ToString(), listEmpID);
                            }

                            lisTestForm.AlarmLevel = int.Parse(TestFormReportValueAlarmLevel.危急.Key);
                            lisTestForm.AlarmInfo = testFormAlarmInfo;
                            this.Entity = lisTestForm;
                            this.Edit();
                        }
                    }
                    else //如果此次判定检验单没有危急值，查找是否存在未发送的危急值，如果存在则更新危急值标记并删除未发送的危急值
                    {
                        if (lisTestForm.AlarmLevel.ToString() == TestFormReportValueAlarmLevel.危急.Key)
                        {
                            lisTestForm.AlarmLevel = 0;
                            lisTestForm.AlarmInfo = "";
                            this.Entity = lisTestForm;
                            this.Edit();
                            IList<LisTestItem> tempList = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + lisTestForm.Id + " and listestitem.MainStatusID>=0 and listestitem.AlarmLevel=" + TestFormReportValueAlarmLevel.危急.Key);
                            if (tempList != null && tempList.Count > 0)
                            {
                                foreach (LisTestItem testItem in tempList)
                                {
                                    testItem.AlarmLevel = 0;
                                    testItem.AlarmInfo = "";
                                    IBLisTestItem.Entity = testItem;
                                    IBLisTestItem.Edit();
                                }
                            }
                        }

                        IList<LisTestFormMsg> listLisTestFormMsg = IBLisTestFormMsg.SearchListByHQL(" listestformmsg.LisTestForm.Id=" + lisTestForm.Id + " and listestformmsg.ReportStatus != 1");
                        if (listLisTestFormMsg != null && listLisTestFormMsg.Count > 0)
                        {
                            IBLisTestFormMsgItem.DeleteByHql(" from LisTestFormMsgItem listestformmsgitem where listestformmsgitem.LisTestFormMsg.Id=" + listLisTestFormMsg[0].Id);
                            IBLisTestFormMsg.DeleteByHql(" from LisTestFormMsg listestformmsg where listestformmsg.Id=" + listLisTestFormMsg[0].Id);
                        }
                    }
                }
                else
                {
                    brdv.success = false;
                    brdv.Code = -1;
                    brdv.ErrorInfo = TestItemHintVar.Hint_TestItemIsEmpty;
                    ZhiFang.LabStar.Common.LogHelp.Info("危急值判定：" + brdv.ErrorInfo);
                }
            }
            else
            {
                brdv.success = false;
                brdv.Code = -1;
                brdv.ErrorInfo = TestFormHintVar.Hint_TestFormIsEmpty;
                ZhiFang.LabStar.Common.LogHelp.Info("危急值判定：" + brdv.ErrorInfo);
            }
            return brdv;
        }

        public BaseResultDataValue EditDisposeLisTestItemPanicValue(string testFormMsgIDList, int msgSendFlag, string msgSendInfo, SysCookieValue sysCookieValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<LisTestFormMsg> listTestFormMsg = IBLisTestFormMsg.SearchListByHQL(" listestformmsg.Id in(" + testFormMsgIDList + ")");
            if (listTestFormMsg != null && listTestFormMsg.Count > 0)
            {
                foreach (LisTestFormMsg testFormMsg in listTestFormMsg)
                {
                    if (msgSendFlag == 1)
                    {
                        LisTestForm testForm = this.Get(testFormMsg.LisTestForm.Id);
                        if (testForm != null)
                        {
                            IList<LisTestItem> listTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + testForm.Id + TestItemCommoWhereHQL);
                            baseResultDataValue = DisposeLisTestItemPanicValue(testForm, listTestItem);
                            if (baseResultDataValue.success)
                            {
                                testFormMsg.ReportStatus = msgSendFlag;
                                testFormMsg.ReporterID = sysCookieValue.EmpID;
                                testFormMsg.Reporter = sysCookieValue.EmpName;
                                testFormMsg.ReportTime = DateTime.Now;
                                testFormMsg.ReportInfo = msgSendInfo;
                                IBLisTestFormMsg.Entity = testFormMsg;
                                IBLisTestFormMsg.Edit();
                            }
                        }
                    }
                    else if (msgSendFlag == 2)
                    {
                        testFormMsg.ReportStatus = msgSendFlag;
                        testFormMsg.ReporterID = sysCookieValue.EmpID;
                        testFormMsg.Reporter = sysCookieValue.EmpName;
                        testFormMsg.ReportTime = DateTime.Now;
                        testFormMsg.ReportInfo = msgSendInfo;
                        IBLisTestFormMsg.Entity = testFormMsg;
                        IBLisTestFormMsg.Edit();
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditPanicValuePhoneCallInfo(string testFormMsgIDList, int phoneCallFlag, string phoneNumber, string phoneReceiver, string phoneCallInfo, SysCookieValue sysCookieValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrWhiteSpace(testFormMsgIDList))
            {
                IList<LisTestFormMsg> listTestFormMsg = IBLisTestFormMsg.SearchListByHQL(" listestformmsg.Id in(" + testFormMsgIDList + ")");
                if (listTestFormMsg != null && listTestFormMsg.Count > 0)
                {
                    foreach (LisTestFormMsg testFormMsg in listTestFormMsg)
                    {

                        testFormMsg.PhoneStatus = phoneCallFlag;
                        testFormMsg.PhoneCallerID = sysCookieValue.EmpID;
                        testFormMsg.PhoneCaller = sysCookieValue.EmpName;
                        testFormMsg.PhoneTime = DateTime.Now;
                        testFormMsg.PhoneNum = phoneNumber;
                        testFormMsg.PhoneDesc = phoneCallInfo;
                        testFormMsg.PhoneReceiver = phoneReceiver;
                        IBLisTestFormMsg.Entity = testFormMsg;
                        IBLisTestFormMsg.Edit();
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditPanicValueReadInfo(string testFormMsgIDList, SysCookieValue sysCookieValue)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrWhiteSpace(testFormMsgIDList))
            {
                IList<LisTestFormMsg> listTestFormMsg = IBLisTestFormMsg.SearchListByHQL(" listestformmsg.Id in(" + testFormMsgIDList + ")");
                if (listTestFormMsg != null && listTestFormMsg.Count > 0)
                {
                    foreach (LisTestFormMsg testFormMsg in listTestFormMsg)
                    {
                        testFormMsg.ReaderID = sysCookieValue.EmpID;
                        testFormMsg.Reader = sysCookieValue.EmpName;
                        testFormMsg.ReadTime = DateTime.Now;
                        IBLisTestFormMsg.Entity = testFormMsg;
                        IBLisTestFormMsg.Edit();
                    }
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DisposeLisTestItemPanicValue(LisTestForm testForm, IList<LisTestItem> listTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<LisTestItem> tempList = listTestItem.Where(p => p.ResultStatusCode != null && (p.ResultStatusCode == "HH" || p.ResultStatusCode == "LL")).ToList();
            if (tempList != null && tempList.Count > 0)
            {
                SCMsg scMsg = GetMsgInfoByLisTestForm(testForm);
                PanicValueMsgVO panicValueMsgVO = new PanicValueMsgVO()
                {
                    MSGCONTENT = new MSGCONTENT()
                    {
                        MSGBODY = new MSGBODY()
                        {
                            //MSG = new MSG(),
                            MSG = new List<MSG>(),
                        },
                        MSGKEY = GetMSGKEYByLisTestForm(testForm),
                    },
                };
                StringBuilder stringBuilder = new StringBuilder();

                string itemPanicValue = "";
                foreach (LisTestItem testItem in tempList)
                {
                    panicValueMsgVO.MSGCONTENT.MSGBODY.MSG.Add(GetMSGByLisTestItem(testForm, testItem));
                    if (itemPanicValue == "")
                        itemPanicValue = "发送检验项目【" + testItem.LBItem.CName + "】危急值:" + testItem.ReportValue + "" + testItem.ResultStatusCode;
                    else
                        itemPanicValue += ",发送检验项目【" + testItem.LBItem.CName + "】危急值:" + testItem.ReportValue + "" + testItem.ResultStatusCode;
                }
                scMsg.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                stringBuilder.Clear();
                stringBuilder.Append("{\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"utf-8\"},\"MSG\":");
                stringBuilder.Append(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(panicValueMsgVO));
                stringBuilder.Append("}");
                scMsg.MsgContent = stringBuilder.ToString();
                baseResultDataValue = SendTestItemPanicValueMsgToPlatform(scMsg);
                if (baseResultDataValue.success)
                {
                    //IBLisOperate.AddLisOperate(testForm, TestFormOperateType.危急值发送.Value, itemPanicValue, null);
                }
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SendTestItemPanicValueMsgToPlatform(SCMsg scMsg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("MsgPlatformServiceUrl");
            if (string.IsNullOrEmpty(url))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "集成平台服务地址不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            else
                url += "/ServerWCF/IMService.svc/ST_UDTO_AddSCMsg";

            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;//日期序列化为“/Data()/”格式，否则无法反序列化
                string StrJson = Newtonsoft.Json.JsonConvert.SerializeObject(scMsg, Newtonsoft.Json.Formatting.None, settings);
                string para = "{\"entity\":" + StrJson + "}";
                baseResultDataValue = ZhiFang.LabStar.Common.HTTPRequest.WebRequestHttpPost(url, para, "application/json", "");
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "服务请求异常：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        private MSGKEY GetMSGKEYByLisTestForm(LisTestForm testForm)
        {
            MSGKEY MSGKEY = new MSGKEY();
            if (testForm.LisPatient != null)
            {
                MSGKEY.AGEUNITNAME = testForm.LisPatient.AgeUnitName;
                MSGKEY.GENDERNAME = testForm.LisPatient.GenderName;
                MSGKEY.DEPTNAME = testForm.LisPatient.DeptName;
                MSGKEY.DISTRICTNAME = testForm.LisPatient.DistrictName;
                MSGKEY.WARDNAME = testForm.LisPatient.DistrictName;
                MSGKEY.BED = testForm.LisPatient.Bed;
                MSGKEY.DOCTOR = testForm.LisPatient.DoctorName;
                MSGKEY.SICKTYPENO = testForm.LisPatient.SickTypeID.ToString();
                MSGKEY.SICKTYPENAME = testForm.LisPatient.SickType;
            }
            MSGKEY.RECEIVEDATE = testForm.GTestDate != null ? testForm.GTestDate.ToString("yyyy-MM-dd") : null;
            MSGKEY.TESTTYPENO = testForm.TestType != null ? testForm.TestType.Value.ToString() : "";
            MSGKEY.SAMPLENO = testForm.GSampleNo;
            if (testForm.LBSection != null)
            {
                MSGKEY.SECTIONNO = testForm.LBSection.Id.ToString();
                MSGKEY.SECTIONNAME = testForm.LBSection.CName;
            }
            MSGKEY.PATNO = testForm.PatNo;
            MSGKEY.CNAME = testForm.CName;
            MSGKEY.AGE = testForm.Age != null ? testForm.Age.Value.ToString() : "";
            MSGKEY.CHECKER = testForm.Checker;
            MSGKEY.CHECKDATE = testForm.CheckTime != null ? testForm.CheckTime.Value.ToString("yyyy-MM-dd") : null;
            MSGKEY.CHECKTIME = testForm.CheckTime != null ? testForm.CheckTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            MSGKEY.SERIALNO = testForm.BarCode;
            MSGKEY.REPORTFORMID = testForm.Id.ToString();
            MSGKEY.LISDOCTORNO = "";
            MSGKEY.HISDOCTORID = "";
            MSGKEY.HISDOCTORPHONECODE = "";
            MSGKEY.TECHNICIAN = testForm.MainTester;
            MSGKEY.COLLECTDATE = testForm.CollectTime != null ? testForm.CollectTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            MSGKEY.INCEPTTIME = testForm.InceptTime != null ? testForm.InceptTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            return MSGKEY;
        }

        private MSG GetMSGByLisTestItem(LisTestForm testForm, LisTestItem testItem)
        {
            MSG MSG = new MSG();

            MSG.RECEIVEDATE = testForm.GTestDate != null ? testForm.GTestDate.ToString("yyyy-MM-dd") : null;
            MSG.TESTTYPENO = testForm.TestType != null ? testForm.TestType.Value.ToString() : "";
            MSG.SAMPLENO = testForm.GSampleNo;
            if (testForm.LBSection != null)
            {
                MSG.SECTIONNO = testForm.LBSection.Id.ToString();
            }
            if (testItem.PLBItem != null)
            {
                MSG.PARITEMNO = testItem.PLBItem.Id.ToString();
                MSG.PARITEMNAME = testItem.PLBItem.EName;
                MSG.PARITEMSNAME = testItem.PLBItem.SName;
            }
            if (testItem.LBItem != null)
            {
                MSG.ITEMNO = testItem.LBItem.Id.ToString();
                MSG.TESTITEMNAME = testItem.LBItem.CName;
                MSG.TESTITEMSNAME = testItem.LBItem.SName;
                MSG.UNIT = testItem.LBItem.Unit;
            }
            MSG.REFRANGE = testItem.RefRange;
            MSG.TESTITEMDATETIME = testItem.TestTime != null ? testItem.TestTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            MSG.REPORTVALUEALL = testItem.ReportValue;
            MSG.REPORTFORMID = testForm.Id.ToString();
            MSG.RESULTSTATUS = testItem.ResultStatus;
            MSG.MASTERDESC = "";
            MSG.DETAILDESC = "";
            MSG.ITEMKEY = "";
            return MSG;
        }


        private SCMsg GetMsgInfoByLisTestForm(LisTestForm testForm)
        {
            SCMsg scMsg = new SCMsg();
            scMsg.LabID = testForm.LabID;
            scMsg.IsUse = true;
            scMsg.MsgTypeID = 1;
            scMsg.MsgTypeCode = "ZF_LAB_START_CV";
            //scMsg.MsgContent = XMLStrToJson(msg);
            scMsg.SystemID = 1001;
            scMsg.SystemCName = "智方_检验之星";
            scMsg.SystemCode = "ZF_LAB_START";
            scMsg.DataUpdateTime = DateTime.Now;
            //scMsg.RequireConfirmTime = DateTime.Now.AddMinutes(TimeSpan);
            scMsg.SendIPAddress = ZhiFang.LabStar.Common.IPHelper.GetClientIP();
            //发送者信息
            scMsg.SenderName = testForm.Confirmer;
            scMsg.SenderID = testForm.ConfirmerId != null ? testForm.ConfirmerId.Value : 0;
            scMsg.SenderAccount = "";

            //发送小组信息
            if (testForm.LBSection != null)
            {
                scMsg.SendSectionName = testForm.LBSection.CName;
                scMsg.SendSectionID = testForm.LBSection.Id;
            }

            if (testForm.LisPatient != null)
            {
                //就诊类型信息
                scMsg.RecSickTypeName = testForm.LisPatient.SickType;
                scMsg.RecSickTypeID = testForm.LisPatient.SickTypeID != null ? testForm.LisPatient.SickTypeID.Value : 0;

                //接收科室信息                                                 
                scMsg.RecDeptName = testForm.LisPatient.DeptName;
                scMsg.RecDeptID = testForm.LisPatient.DeptID != null ? testForm.LisPatient.DeptID.Value : 0;
                //scMsg.RecDeptCode = "";
                //scMsg.RecDeptCodeHIS = "";
                //scMsg.RecDeptPhoneCode = "";

                //接收病区信息
                scMsg.RecDistrictName = testForm.LisPatient.DistrictName;
                scMsg.RecDistrictID = testForm.LisPatient.DistrictID != null ? testForm.LisPatient.DistrictID.Value : 0;

                //接收医生信息
                scMsg.RecDoctorName = testForm.LisPatient.DoctorName;
                scMsg.RecDoctorID = testForm.LisPatient.DoctorID != null ? testForm.LisPatient.DoctorID.Value : 0;
                scMsg.RecDoctorCode = "";
                scMsg.RecDoctorCodeHIS = "";
            }

            //确认科室信息默认是接收科室
            scMsg.ConfirmDeptName = scMsg.RecDeptName;
            scMsg.ConfirmDeptCode = scMsg.RecDeptCode;
            scMsg.ConfirmDeptCodeHIS = scMsg.RecDeptCodeHIS;
            return scMsg;
        }
        #endregion

        #region 条码快捷核收


        /// <summary>
        /// 快捷核收样本单
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddTestFormByQuickReceive(string barCode, long sectionID, string strReceiveDate, string sampleNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("快捷核收开始，条码号：" + barCode + ",样本号：" + sampleNo + ",检验小组ID：" + sectionID.ToString() + ",检验日期：" + strReceiveDate);
            ZhiFang.LabStar.Common.LogHelp.Info("根据小组ID查询检验小组信息！");
            LBSection section = IBLBSection.Get(sectionID);
            if (section == null)
            {
                brdv.success = false;
                brdv.Code = -1;
                brdv.ErrorInfo = TestFormHintVar.Hint_SectionIsEmpty;
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            ZhiFang.LabStar.Common.LogHelp.Info("查询到检验小组：" + section.CName);

            DateTime receiveDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrWhiteSpace(strReceiveDate))
                receiveDate = DateTime.Parse(strReceiveDate);

            ZhiFang.LabStar.Common.LogHelp.Info("检验日期取值：" + receiveDate.ToString("yyyy-MM-dd"));
            LisBarCodeForm barCodeForm = null;
            IList<LisBarCodeItem> listBarCodeItem = null;
            int barCodeItemCount = 0;
            brdv = QueryLisBarCodeFormByBarCode(barCode, section, ref barCodeForm, ref listBarCodeItem, ref barCodeItemCount);
            if (brdv.success && barCodeForm != null && listBarCodeItem != null && listBarCodeItem.Count > 0)
            {
                #region 检验单判断
                LisTestForm testForm = null;
                IList<LisTestForm> listTestForm = null;
                string strWhere = "";
                if (!string.IsNullOrWhiteSpace(sampleNo))
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("样本号不为空，按样本号查找检验单！");
                    strWhere = " listestform.LBSection.Id=" + section.Id + " and listestform.GTestDate=\'" + receiveDate.ToString("yyyy-MM-dd") + 
                        "\' and listestform.GSampleNo=\'" + sampleNo + "\'" + TestFormCommoWhereHQL;
                    listTestForm = this.SearchListByHQL(strWhere);
                    if (listTestForm != null && listTestForm.Count == 1)
                    {
                        
                        if (listTestForm[0].MainStatusID.ToString() != TestFormMainStatus.检验中.Key)
                        {
                            brdv.success = false;
                            brdv.Code = -1;//已存在指定样本号的检验单且在该检验单状态下不能核收
                            brdv.ErrorInfo = TestFormHintVar.Hint_TestFormExist + "状态为：" + TestFormMainStatus.GetStatusDic()[testForm.MainStatusID.ToString()].Name + "，不能核收到此检验单！";
                            ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                            return brdv;
                        }
                        ZhiFang.LabStar.Common.LogHelp.Info("按样本号查找到检验单，状态为检验中！");
                        testForm = listTestForm[0];
                    }
                    else if (listTestForm != null && listTestForm.Count > 1)
                    {
                        brdv.success = false;
                        brdv.Code = -2;
                        brdv.ErrorInfo = "存在" + listTestForm.Count + "条样本号为【" + sampleNo + "】的检验单，不能核收!";
                        ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                        return brdv;
                    }
                }

                strWhere = " listestform.LBSection.Id=" + section.Id + " and listestform.GTestDate=\'" + receiveDate.ToString("yyyy-MM-dd") + 
                    "\' and listestform.BarCode=\'" + barCode + "\'" + TestFormCommoWhereHQL;
                IList<LisTestForm> listTestFormByBarCode = this.SearchListByHQL(strWhere);
                if (listTestFormByBarCode != null && listTestFormByBarCode.Count > 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info(TestFormHintVar.Hint_TestFormExist + "，条码号：" + barCode);
                    if (listTestFormByBarCode[0].MainStatusID.ToString() != TestFormMainStatus.检验中.Key)
                    {
                        brdv.success = false;
                        brdv.Code = -3;
                        brdv.ErrorInfo = TestFormHintVar.Hint_TestFormExist + "状态：" + TestFormMainStatus.GetStatusDic()[testForm.MainStatusID.ToString()].Name;
                        ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo + "，条码号：" + barCode);
                        return brdv;
                    }
                    //未能按样本号找到检验单
                    if (testForm == null)
                        testForm = listTestFormByBarCode[0];
                }
                #endregion

                if (testForm != null && (!string.IsNullOrWhiteSpace(testForm.BarCode)))
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("查找到检验单后，如果该检验单条码号不为空，判断是否与核收条码号不同！");
                    if (testForm.BarCode.Trim() != barCode)
                    {
                        brdv.success = false;
                        brdv.Code = -3;
                        brdv.ErrorInfo = "检验单已经有条码：" + testForm.BarCode.Trim() + "，不能再核收条码：" + barCode;
                        ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                        return brdv;
                    }             
                }

                brdv = AddTestFormByBarCodeFrom(ref testForm, sampleNo, section, receiveDate, ref barCodeForm, ref listBarCodeItem);
                if (brdv.success)
                {
                    IList<LisBarCodeItem> tempList = listBarCodeItem.Where(p => p.ReceiveFlag != 1).ToList();
                    if ((tempList == null || tempList.Count == 0) && (barCodeItemCount == listBarCodeItem.Count))
                    {
                        barCodeForm.ReceiveFlag = 1;
                        barCodeForm.ReceiveTime = DateTime.Now;
                        IBLisBarCodeForm.Entity = barCodeForm;
                        IBLisBarCodeForm.Edit();
                    }
                    foreach (LisBarCodeItem barCodeItem in listBarCodeItem)
                    {
                        barCodeItem.ReceiveFlag = 1;
                        IBLisBarCodeItem.Entity = barCodeItem;
                        IBLisBarCodeItem.Edit();
                    }
                }
            }
            ZhiFang.LabStar.Common.LogHelp.Info("快捷核收结束！");
            return brdv;
        }

        /// <summary>
        /// 快捷核收根据条码查询样本单
        /// </summary>
        /// <param name="barCode">条码号</param>
        /// <param name="section">检验小组</param>
        /// <param name="barCodeForm">返回的样本单</param>
        /// <param name="listBarCodeItem">返回的样本单项目</param>
        /// <returns></returns>
        public BaseResultDataValue QueryLisBarCodeFormByBarCode(string barCode, LBSection section, ref LisBarCodeForm barCodeForm, ref IList<LisBarCodeItem> listBarCodeItem, ref int barCodeItemCount)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("根据条码号查询样本单！");
            IList<LisBarCodeForm> listBarCodeForm = IBLisBarCodeForm.SearchListByHQL(" lisbarcodeform.BarCode=\'" + barCode + "\'");
            if (listBarCodeForm == null || listBarCodeForm.Count == 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Info("未找到对应的样本单信息，条码号：" + barCode);
                //if (调用接口核收)
                brdv.success = false;
                brdv.Code = 1;
                brdv.ErrorInfo = BarCodeFormHintVar.Hint_BarCodeFormIsEmpty;
                return brdv;
            }
            else if (listBarCodeForm.Count > 1)
            {
                listBarCodeForm = listBarCodeForm.OrderByDescending(p => p.DataAddTime).ToList();
                ZhiFang.LabStar.Common.LogHelp.Info("找到【" + listBarCodeForm.Count + "】条对应的样本单信息，取创建时间最近的样本单！");
            }
            else
                ZhiFang.LabStar.Common.LogHelp.Info("查询到" + listBarCodeForm.Count + "条样本单信息！");

            barCodeForm = listBarCodeForm[0];
            
            if (barCodeForm.BarCodeFlag == -1)
            {
                ZhiFang.LabStar.Common.LogHelp.Info("该样本单已经作废！");
                brdv.success = false;
                brdv.Code = -1;
                brdv.ErrorInfo = BarCodeFormHintVar.Hint_BarCodeFormIsEmpty;
                return brdv;
            }

            if (barCodeForm.ReceiveFlag == 1)
            {
                ZhiFang.LabStar.Common.LogHelp.Info("该样本单已经核收！" );
                brdv.success = false;
                brdv.Code = -1;
                brdv.ErrorInfo = BarCodeFormHintVar.Hint_BarCodeFormIsReceive;
                return brdv;
            }

            //条码单状态判断，例如未签收的单子不能核收
            //应该有样本参数控制是否判断样本采集和签收状态
            if (barCodeForm.BarCodeStatusID == int.Parse(BarCodeStatus.样本采集.Value.Code))
            { 
            
            }
            if (barCodeForm.BarCodeStatusID == int.Parse(BarCodeStatus.样本签收.Value.Code))
            {

            }

            ZhiFang.LabStar.Common.LogHelp.Info("开始查询样本单未核收项目信息！" );
           
            listBarCodeItem = IBLisBarCodeItem.SearchListByHQL(" lisbarcodeitem.LisBarCodeForm.Id=" + barCodeForm.Id +
                " and (lisbarcodeitem.BarCodeItemFlag is null or lisbarcodeitem.BarCodeItemFlag=0) and (lisbarcodeitem.ReceiveFlag is null or lisbarcodeitem.ReceiveFlag=0)");
            if (listBarCodeItem == null || listBarCodeItem.Count == 0)
            {
                brdv.success = false;
                brdv.Code = 2;
                brdv.ErrorInfo = BarCodeFormHintVar.Hint_NoExistReceiveItem;
                ZhiFang.LabStar.Common.LogHelp.Info("样本单中未查询到要核收的项目！");
                return brdv;
            }
            barCodeItemCount = listBarCodeItem.Count;
            ZhiFang.LabStar.Common.LogHelp.Info("查询到" + listBarCodeItem.Count + "条未核收的项目！");
            ZhiFang.LabStar.Common.LogHelp.Info("开始检查未核收采样项目是否属于检验小组【"+ section.CName + "】项目");
            IList<LBSectionItem> listSectionItem = IBLBSectionItem.SearchListByHQL(" lbsectionitem.LBSection.Id=" + section.Id);
            if (listSectionItem == null || listSectionItem.Count == 0)
            {
                brdv.success = false;
                brdv.Code = 3;
                brdv.ErrorInfo = "检验小组【" + section.CName + "】未设置任何检验项目！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }

            for (int i = listBarCodeItem.Count - 1; i >= 0; i--)
            {
                LisBarCodeItem barCodeItem = listBarCodeItem[i];
                IList<LBSectionItem> tempList = listSectionItem.Where(p => p.LBItem.Id == barCodeItem.BarCodesItemID).ToList();
                if (tempList == null || tempList.Count == 0)
                {
                    listBarCodeItem.Remove(barCodeItem);
                    ZhiFang.LabStar.Common.LogHelp.Info("移除非本小组项目，项目ID：" + barCodeItem.BarCodesItemID);
                }
            }
            if (listBarCodeItem == null || listBarCodeItem.Count == 0)
            {
                brdv.success = false;
                brdv.Code = 4;
                brdv.ErrorInfo = BarCodeFormHintVar.Hint_NoExistReceiveItem;
                ZhiFang.LabStar.Common.LogHelp.Info("样本单中可核收项目都不属该检验小组项目！" );
                return brdv;
            }
            ZhiFang.LabStar.Common.LogHelp.Info("查询到该小组有" + listBarCodeItem.Count + "条未核收的项目！" );

            return brdv;
        }

        public BaseResultDataValue AddTestFormByBarCodeFrom(ref LisTestForm testForm, string sampleNo, LBSection section, DateTime receiveDate, ref LisBarCodeForm barCodeForm, ref IList<LisBarCodeItem> listBarCodeItem)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (testForm == null)
            {
                testForm = new LisTestForm();
                testForm.IsAddTestForm = true;
            }
            if (testForm.IsAddTestForm)
            {
                testForm.LBSection = section;
                testForm.GTestDate = receiveDate;
                testForm.TestType = int.Parse(TestType.常规.Key);                
                testForm.GSampleNo = sampleNo;
                if (string.IsNullOrWhiteSpace(testForm.GSampleNo))
                    testForm.GSampleNo = CreateNewSampleNoByOldSampleNo(section.Id, receiveDate.ToString("yyyy-MM-dd"), "").ResultDataValue;
                testForm.GSampleNoForOrder = LisCommonMethod.DisposeSampleNo(testForm.GSampleNo);
                testForm.MainStatusID = int.Parse(TestFormMainStatus.检验中.Key);
            }
            testForm.BarCode = barCodeForm.BarCode;
            testForm.StatusID = int.Parse(TestFormStatusID.样本核收.Key);
            testForm.ISource = int.Parse(TestFormSource.人工核收.Key);
            testForm.SampleForm = barCodeForm;
            testForm.CollectTime = barCodeForm.CollectTime;
            testForm.LisOrderForm = barCodeForm.LisOrderForm;
            testForm.LisPatient = barCodeForm.LisPatient;
            testForm.GSampleTypeID = barCodeForm.SampleTypeID;
            if (testForm.GSampleTypeID != null)
            {
                LBSampleType sampleType = IBLBSampleType.Get(testForm.GSampleTypeID.Value);
                testForm.GSampleType = sampleType != null ? sampleType.CName : "";
            }
            testForm.InceptTime = barCodeForm.InceptTime;
            testForm.UrgentState = barCodeForm.IsUrgent.ToString();//加急标识
            testForm.ReceiveTime = DateTime.Now;
            if (testForm.LisPatient == null && barCodeForm.LisOrderForm != null && barCodeForm.LisOrderForm.PatID != null)
            {
                testForm.LisPatient = IBLisPatient.Get(barCodeForm.LisOrderForm.PatID.Value);
            }
            if (testForm.LisPatient != null)
            {
                testForm.PatNo = testForm.LisPatient.PatNo;
                testForm.CName = testForm.LisPatient.CName;                
                testForm.GenderID = testForm.LisPatient.GenderID;
                testForm.Age = testForm.LisPatient.Age;               
                testForm.AgeDesc = testForm.LisPatient.AgeDesc;
                testForm.AgeUnitID = testForm.LisPatient.AgeUnitID;
                testForm.PatWeight = testForm.LisPatient.PatWeight;
                testForm.DeptID = testForm.LisPatient.DeptID;
                testForm.DistrictID = testForm.LisPatient.DistrictID;            
                testForm.SickTypeID = testForm.LisPatient.SickTypeID; 
            }
            if (barCodeForm.LisOrderForm != null)
            {
                testForm.Charge = barCodeForm.LisOrderForm.Charge;
            }
            testForm.FormInfoStatus = JudgeLisTestFormInfoStatus(testForm);
            this.Entity = testForm;

            bool isSaveFlag = true;
            if (testForm.IsAddTestForm)
                isSaveFlag = this.Add();
            else
                isSaveFlag = this.Edit();
            if (isSaveFlag)
            {
                brdv.success = isSaveFlag;
                brdv.ResultDataValue = testForm.Id.ToString();
                IBLisOperate.AddLisOperate(testForm, TestFormOperateType.检验单生成.Value, "检验单生成", SysCookieValue);
                IList<LisTestItem> listNewTestItem = AddTestItemByBarCodeItem(testForm, ref listBarCodeItem);
                foreach (LisTestItem newTestItem in listNewTestItem)
                {
                    IBLisTestItem.Entity = newTestItem;
                    if (!IBLisTestItem.Add())
                        brdv.success = false;
                }
            }
            return brdv;
        }

        public IList<LisTestItem> AddTestItemByBarCodeItem(LisTestForm testForm, ref IList<LisBarCodeItem> listBarCodeItem)
        {
            IList<LisTestItem> listOldTestItem = null;
            IList<LisTestItem> listNewTestItem = new List<LisTestItem>();
            if (!testForm.IsAddTestForm)
                listOldTestItem = IBLisTestItem.QueryLisTestItem(" listestitem.LisTestForm.Id=" + testForm.Id + TestItemCommoWhereHQL);

            LBItem parItem = null;
            foreach (LisBarCodeItem barCodeItem in listBarCodeItem)
            {
                parItem = null;
                IList<LBItem> listAddTestItem = new List<LBItem>();
                if (barCodeItem.BarCodesItemID != null)
                {
                    LBItem item = IBLBItem.Get(barCodeItem.BarCodesItemID.Value);
                    if (item.GroupType == 0)
                    {
                        listAddTestItem.Add(item);
                    }
                    else if (item.GroupType == 1)
                    {
                        parItem = item;
                        IList<LBItemGroup> listLBItemGroup = IBLBItemGroup.QueryLBItemGroup(" lbgroup.Id=" + item.Id, "LBItemGroup_LBItem_Id,LBItemGroup_LBGroup_Id");
                        IList <LBItem> listGroupItem = listLBItemGroup.Select(p => p.LBItem).ToList();
                        listAddTestItem = listAddTestItem.Union(listGroupItem).ToList();
                    }
                    else if (item.GroupType == 2)
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("样本单项目【" + item.CName + "---" + item.Id + "】属于组套项目，不能核收！");
                    }
                    else
                    {
                        ZhiFang.LabStar.Common.LogHelp.Info("样本单项目【" + item.CName + "---" + item.Id + "】未知项目类型，不能核收！");
                    }
                }

                foreach (LBItem tempItem in listAddTestItem)
                {
                    IList<LisTestItem> tempOldList = null;
                    if (listOldTestItem != null && listOldTestItem.Count > 0)
                        tempOldList = listOldTestItem.Where(p => p.LBItem != null && p.LBItem.Id == tempItem.Id).ToList();

                    IList<LisTestItem> tempNewList = listNewTestItem.Where(p => p.LBItem != null && p.LBItem.Id == tempItem.Id).ToList();
                    if ((tempOldList == null || tempOldList.Count == 0) && (tempNewList == null || tempNewList.Count == 0))
                    {
                        LisTestItem newTestItem = new LisTestItem
                        {
                            LisOrderItem = barCodeItem.LisOrderItem,
                            LisBarCodeItem = barCodeItem,
                            OrdersItemID = barCodeItem.OrdersItemID,
                            BarCodesItemID = barCodeItem.BarCodesItemID,
                            LBItem = tempItem,
                            PLBItem = parItem,
                            LisTestForm = testForm,
                            GTestDate = testForm.GTestDate,
                            MainStatusID = testForm.MainStatusID,
                            StatusID = int.Parse(TestItemStatusID.样本核收.Key),
                            ISource = int.Parse(TestItemSource.分发核收.Key)
                        };
                        barCodeItem.ReceiveFlag = 1;
                        listNewTestItem.Add(newTestItem);
                    }
                }            
            }
            return listNewTestItem;
        }


        #endregion

        /// <summary>
        /// 更新检验单打印次数
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditLisTestFormPrintCount(string testFormID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (!string.IsNullOrWhiteSpace(testFormID))
            {
                long[] arrayFormID = Array.ConvertAll<string, long>(testFormID.Split(','), p => long.Parse(p));
                string strWhere = " listestform.Id in (" + String.Join(",", arrayFormID) + ")";
                IList<LisTestForm> listTestForm = this.SearchListByHQL(strWhere);
                if (listTestForm != null && listTestForm.Count > 0)
                {
                    foreach (LisTestForm testForm in listTestForm)
                    {
                        testForm.PrintCount += 1;
                        this.Entity = testForm;
                        brdv.success = this.Edit();
                    }
                }
            }
            return brdv;
        }

        //定制查询检验项目--排序
        public EntityList<LisTestItemVO> DZQueryLisTestItem(long TestFormId)
        {
            LisTestForm lisTestForm = this.Get(TestFormId);
            IList<LisTestItem> listLisTestItem = null;

            List<LisTestItemVO> lisTestItemVO = new List<LisTestItemVO>();

            if (lisTestForm != null) listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + TestFormId + TestItemCommoWhereHQL);


            IList<LBSectionItem> listSectionItem = IBLBSectionItem.SearchListByHQL(" lbsectionitem.LBSection.Id=" + lisTestForm.LBSection.Id);

            if (listLisTestItem != null && listLisTestItem.Count() > 0)
            {

                foreach (var listestitem in listLisTestItem)
                {
                    LisTestItemVO listtestitemvo = new LisTestItemVO();
                    listtestitemvo = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisTestItemVO, LisTestItem>(listestitem);
                    //组合
                    LBSectionItem lbsection1 = (listtestitemvo.PLBItem != null && listSectionItem.Count(a => a.LBItem.Id == listtestitemvo.PLBItem.Id) > 0) ? listSectionItem.First(a => a.LBItem.Id == listtestitemvo.PLBItem.Id) : null;
                    if (lbsection1 == null)
                        listtestitemvo.ZDYDisOrder1 = 9999;
                    else
                        listtestitemvo.ZDYDisOrder1 = lbsection1.DispOrder;
                    //单项
                    LBSectionItem lbsection2 = (listtestitemvo.LBItem != null && listSectionItem.Count(a => a.LBItem.Id == listtestitemvo.LBItem.Id) > 0) ? listSectionItem.First(a => a.LBItem.Id == listtestitemvo.LBItem.Id) : null;
                    if (lbsection2 == null)
                        listtestitemvo.ZDYDisOrder2 = 9999;
                    else
                        listtestitemvo.ZDYDisOrder2 = lbsection2.DispOrder;

                    lisTestItemVO.Add(listtestitemvo);
                }
            }

            lisTestItemVO.OrderBy(a => new
            {
                a.ZDYDisOrder1,
                d = a.PLBItem.DispOrder,
                a.ZDYDisOrder2,
                f = a.LBItem.DispOrder,
            });

            EntityList<LisTestItemVO> entitylistTestItemVO = new EntityList<LisTestItemVO>();
            entitylistTestItemVO.list = lisTestItemVO;
            entitylistTestItemVO.count = lisTestItemVO.Count();
            return entitylistTestItemVO;
        }
    }

}