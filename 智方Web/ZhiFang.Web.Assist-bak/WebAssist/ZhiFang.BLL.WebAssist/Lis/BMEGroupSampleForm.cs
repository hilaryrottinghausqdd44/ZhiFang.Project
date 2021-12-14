
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.WebAssist;
using ZhiFang.IDAO.RBAC;
using ZhiFang.WebAssist.Common;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BMEGroupSampleForm : BaseBLL<MEGroupSampleForm>, ZhiFang.IBLL.WebAssist.IBMEGroupSampleForm
    {
        IBMEGroupSampleItem IBMEGroupSampleItem { get; set; }

        IDDepartmentDao IDDepartmentDao { get; set; }
        IBPUser IBPUser { get; set; }
        IBPGroup IBPGroup { get; set; }
        IDSectionItemDao IDSectionItemDao { get; set; }
        IDTestItemDao IDTestItemDao { get; set; }
        IDSickTypeDao IDSickTypeDao { get; set; }
        IBSampleType IBSampleType { get; set; }
        IDSCRecordItemLinkDao IDSCRecordItemLinkDao { get; set; }

        #region 院感申请单按科室自动核收关系进行自动写入
        public BaseResultDataValue SaveByDeptAutoCheck(ref GKSampleRequestForm gkEntity, IList<SCRecordDtl> dtlEntityList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            SickType sickType = _getSickType();
            PGroup pgroup = _selectPGroup();
            PUser mainTester = _selectPUser();
            //SampleType sampleType = _FSampleType(gkEntity, "");

            int sampleTypeNo = -1;
            int.TryParse(gkEntity.SCRecordType.SampleTypeCode, out sampleTypeNo);

            MEGroupSampleForm entity = new MEGroupSampleForm();
            if (entity.LabID < 0)
                entity.LabID = 0;

            int sectionNo = _selectPSectionNo();
            if (sickType != null)
                entity.Jztype = int.Parse(sickType.Id.ToString());

            if (sectionNo != -200)
                entity.SectionNo = sectionNo;// int.Parse(pgroup.Id.ToString());

            if (sampleTypeNo > 0)
                entity.SampleTypeNo = sampleTypeNo; //int.Parse(sampleType.Id.ToString());

            if (mainTester != null)
            {
                entity.MainTesterId = int.Parse(mainTester.Id.ToString());
                entity.MainTester = mainTester.CName;
            }

            entity.ZDY1 = gkEntity.Id.ToString();

            entity.TestTypeNo = 1;
            entity.MainState = 1;//-2：删除；-1：冻结（未使用）；1：检验中；2：初审；3:终审
            entity.BAllResultTest = 0;//1:检验完成，0：检验未完成
            entity.ReportType = 1;//0：中间报告；1：正式报告
            entity.DataUpdateTime = DateTime.Now;

            entity.PatNo = gkEntity.BarCode;
            entity.CName = gkEntity.CName;
            entity.OldSerialNo = gkEntity.BarCode;
            entity.SerialNo = gkEntity.BarCode;
            entity.DataAddTime = DateTime.Now;

            entity.CollecterID = gkEntity.SamplerId.ToString();
            entity.GTestDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
            entity.TestTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            entity.ReceiveTime = DateTime.Now;
            entity.ReceiveMan = empName;
            entity.GSampleNo = GetGSampleNo(entity);
            entity.GSampleNoForOrder = entity.GSampleNo.PadLeft(20, '0');//00000000000000000199

            gkEntity.SampleNo = entity.GSampleNo;

            IList<MEGroupSampleItem> itemsList = GroupSampleItemList(gkEntity, entity, dtlEntityList);

            if (itemsList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "转换申请记录项的检验项目信息失败！";
                return brdv;
            }
            //this.Entity = entity;
            //bool result = this.Add();

            bool result = IDAO.NHB.WebAssist.DataAccess_SQL.CreateMEGroupSampleFormDao_SQL().Insert(entity);

            if (result == false)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("监测科室为:{0},监测类型为:{1},新增院感登记,按科室自动核收保存失败!", gkEntity.DeptCName, gkEntity.SCRecordType.CName);
                return brdv;
            }

            if (entity.DataTimeStamp == null)
            {
                entity.DataTimeStamp = dataTimeStamp;
            }

            brdv = IBMEGroupSampleItem.SaveMEGroupSampleItemOfGK(entity, ref itemsList, empID, empName);
            //if (brdv.success == false)
            //throw new Exception(brdv.ErrorInfo);

            return brdv;
        }
        /// <summary>
        /// 获取Lis对照的就诊类型信息
        /// </summary>
        /// <returns></returns>
        private SickType _getSickType()
        {
            SickType entity = null;

            if (GKSysHelp.SickType != null)
            {
                entity = GKSysHelp.SickType;
                return entity;
            }

            string code = JSONConfigHelp.GetString(SysConfig.GKSYS.Key, "LISSickTypeCode");
            if (string.IsNullOrEmpty(code))
            {
                ZhiFang.Common.Log.Log.Warn("未配置GKConfig.json的院感申请的就诊类型对照码信息！");
                return entity;
            }
            IList<SickType> tempList = IDSickTypeDao.LoadAll();
            foreach (var item in tempList)
            {
                if (item.ContractCode.ToLower() == code.ToLower())
                {
                    entity = item;
                    GKSysHelp.ContractCode = item.ContractCode.Replace("_", "");
                    GKSysHelp.SickType = entity;
                    break;
                }
            }
            return entity;
        }
        /// <summary>
        /// 获取LIS的默认核收小组信息
        /// </summary>
        /// <returns></returns>
        private PGroup _selectPGroup()
        {
            PGroup entity = new PGroup();
            if (GKSysHelp.PGroup != null)
            {
                entity = GKSysHelp.PGroup;
                return entity;
            }

            string code = JSONConfigHelp.GetString(SysConfig.GKSYS.Key, "LISSectionNo");
            if (string.IsNullOrEmpty(code))
            {
                ZhiFang.Common.Log.Log.Warn("未配置GKConfig.json的LIS的默认核收小组编号信息！");
                return entity;
            }
            int id2 = -100;
            int.TryParse(code, out id2);
            entity = IBPGroup.Get(id2);// ClassMapperHelp.GetMapper<PGroup, PGroup>(IBPGroup.Get(id2));//IDPGroupDao.FindById(id2);
            GKSysHelp.PGroup = entity;

            return entity;
        }
        /// <summary>
        /// 获取LIS的默认核收小组信息
        /// </summary>
        /// <returns></returns>
        private int _selectPSectionNo()
        {
            int sectionNo = -200;
            if (GKSysHelp.PGroup != null)
            {
                sectionNo = GKSysHelp.PGroup.Id;
                return sectionNo;
            }

            string code = JSONConfigHelp.GetString(SysConfig.GKSYS.Key, "LISSectionNo");
            if (string.IsNullOrEmpty(code))
            {
                ZhiFang.Common.Log.Log.Warn("未配置GKConfig.json的LIS的默认核收小组编号信息！");
                return sectionNo;
            }
            int.TryParse(code, out sectionNo);

            return sectionNo;
        }
        /// <summary>
        /// 获取Lis的默认检验者信息
        /// </summary>
        /// <returns></returns>
        private PUser _selectPUser()
        {
            PUser entity = null;
            if (GKSysHelp.MainTester != null)
            {
                entity = GKSysHelp.MainTester;
                return entity;
            }

            string code = JSONConfigHelp.GetString(SysConfig.GKSYS.Key, "LISMainTesterNo");
            if (string.IsNullOrEmpty(code))
            {
                ZhiFang.Common.Log.Log.Warn("未配置GKConfig.json的LIS的默认核收检验者编号信息！");
                return entity;
            }
            int id2 = -100;
            int.TryParse(code, out id2);
            entity = IBPUser.Get(id2);// ClassMapperHelp.GetMapper<PUser, PUser>(IBPUser.Get(id2));
            GKSysHelp.MainTester = entity;

            return entity;
        }
        /// <summary>
        /// 获取Lis对照样本类型信息
        /// 样本类型对照院感的监测类型编码
        /// </summary>
        /// <param name="codeKey"></param>
        /// <returns></returns>
        private SampleType _FSampleType(GKSampleRequestForm gkEntity, string codeKey)
        {
            SampleType entity = null;
            if (string.IsNullOrEmpty(codeKey)) codeKey = GKSysHelp.ContractCode;
            if (string.IsNullOrEmpty(codeKey)) return entity;

            // IList<SampleType> tempList = IBSampleType.SearchListByHQL("sampletype." + codeKey + "='" + gkEntity.SCRecordType.Id + "'");
            IList<SampleType> tempList = IBSampleType.LoadAll().Where(p => p.Code4 == gkEntity.SCRecordType.Id.ToString()).ToList();
            if (tempList == null || tempList.Count <= 0)
            {
                return entity;
            }

            if (tempList.Count >= 1)
            {
                ZhiFang.Common.Log.Log.Warn("样本类型对照码为：" + gkEntity.SCRecordType.Id + ",存在多个样本类型对照，系统取第一个样本类型！");
                tempList = tempList.OrderBy(p => p.DispOrder).ToList();
            }
            entity = tempList[0];// ClassMapperHelp.GetMapper<SampleType, SampleType>(tempList[0]);// tempList[0];

            return entity;
        }
        /// <summary>
        /// 获取检验单的姓名
        /// 取监测类型的样品信息记录项集合的第一项的结果值
        /// </summary>
        /// <returns></returns>
        private string _getCName(IList<SCRecordDtl> dtlEntityList)
        {
            string cname = "";

            return cname;
        }
        /// <summary>
        /// 获取小组样本号信息
        /// </summary>
        /// <param name="code_4"></param>
        /// <returns></returns>
        private string GetGSampleNo(MEGroupSampleForm entity)
        {
            string gsampleNo = "";
            gsampleNo = ((IDMEGroupSampleFormDao)base.DBDao).GetNextGSampleNo(entity.SectionNo, entity.GTestDate.Value.ToString("yyyy-MM-dd"));
            if (string.IsNullOrEmpty(gsampleNo)) gsampleNo = "1";

            return gsampleNo;
        }
        /// <summary>
        /// 获取检验单项目明细集合信息
        /// 按类型与记录项关系的对照码进行处理
        /// </summary>
        /// <param name="docEntity"></param>
        /// <param name="dtlEntityList"></param>
        /// <param name=""></param>
        /// <returns></returns>
        private IList<MEGroupSampleItem> GroupSampleItemList(GKSampleRequestForm gkEntity, MEGroupSampleForm docEntity, IList<SCRecordDtl> dtlEntityList)
        {
            IList<MEGroupSampleItem> itemsList = new List<MEGroupSampleItem>();

            TestItem pTestItem = null;
            int pTestItemId = -1;

            IList<SCRecordItemLink> itemLinkList = IDSCRecordItemLinkDao.GetListByHQL("screcorditemlink.SCRecordType.Id=" + gkEntity.SCRecordType.Id);

            if (itemLinkList.Count > 0) itemLinkList = itemLinkList.OrderBy(p => p.DispOrder).ToList();

            if (!string.IsNullOrEmpty(gkEntity.SCRecordType.TypeCode))
                int.TryParse(gkEntity.SCRecordType.TypeCode, out pTestItemId);
            else
                ZhiFang.Common.Log.Log.Warn("监测类型对照码为：" + gkEntity.SCRecordType.Id + ",对照码为空！");

            if (pTestItemId > 0)
                pTestItem = ClassMapperHelp.GetMapper<TestItem, TestItem>(IDTestItemDao.FindById(pTestItemId));

            if (pTestItem == null)
                ZhiFang.Common.Log.Log.Warn("监测类型对照码为：" + gkEntity.SCRecordType.Id + ",对照码为空！");
            string testItemCode = "";
            foreach (var item in dtlEntityList)
            {
                testItemCode = "";
                var itemLinkList2 = itemLinkList.Where(p => p.SCRecordTypeItem.Id == item.SCRecordTypeItem.Id);
                if (itemLinkList2 == null || itemLinkList2.Count() <= 0) continue;
                SCRecordItemLink recordItemLink = itemLinkList2.ElementAt(0);

                //透析液及透析用水的检验项目取开单申请选择的检验项目
                if (gkEntity.SCRecordType.Id == 15)
                {
                    testItemCode = item.TestItemCode;
                }
                else
                {
                    //没对照码不写入到LIS
                    if (string.IsNullOrEmpty(itemLinkList2.ElementAt(0).TestItemCode)) continue;
                    testItemCode = recordItemLink.TestItemCode;
                }

                if (string.IsNullOrEmpty(testItemCode))
                {
                    ZhiFang.Common.Log.Log.Warn("监测类型记录项编码为：" + item.SCRecordTypeItem.Id + ",对照码为空！");
                    continue;
                }

                string testItemHql = "testitem.Id=" + testItemCode;
                IList<TestItem> testItemList2 = IDTestItemDao.SelectListByHQL(testItemHql);
                //检验子项目转换
                foreach (var testItem in testItemList2)
                {
                    MEGroupSampleItem entity = new MEGroupSampleItem();
                    entity.GroupSampleFormID = docEntity.Id;
                    if (pTestItemId > 0)
                        entity.PItemNo = pTestItemId;
                    entity.ItemNo = testItem.Id;
                    entity.Units = testItem.Unit;
                    entity.DispOrder = testItem.DispOrder;
                    entity.GTestDate = docEntity.GTestDate;

                    entity.ZDY1 = docEntity.Id.ToString();
                    entity.ZDY2 = item.ItemResult;
                    entity.ZDY4 = docEntity.GBarCode;
                    entity.ZDY5 = item.Id.ToString();
                    string itemResult = item.ItemResult;

                    //透析液及透析用水的检验项目结果值处理
                    if (gkEntity.SCRecordType.Id == 15 && item.SCRecordTypeItem.Id == 120010)
                    {
                        itemResult = "0";
                    }
                    entity.ReportValue = itemResult;
                    //结果默认为空
                    if (string.IsNullOrEmpty(entity.ReportValue))
                    {
                        entity.ReportValue = "0";
                        entity.QuanValue = 0;
                    }
                    itemsList.Add(entity);
                }

            }

            return itemsList;
        }

        /// <summary>
        /// 获取检验单项目明细集合信息
        /// </summary>
        /// <param name="docEntity"></param>
        /// <param name="dtlEntityList"></param>
        /// <param name=""></param>
        /// <returns></returns>
        private IList<MEGroupSampleItem> GroupSampleItemList2(GKSampleRequestForm gkEntity, MEGroupSampleForm docEntity, IList<SCRecordDtl> dtlEntityList)
        {
            IList<MEGroupSampleItem> itemsList = new List<MEGroupSampleItem>();

            //TestItem pTestItem = null;
            //int pTestItemId = -1;

            //if (!string.IsNullOrEmpty(gkEntity.SCRecordType.TypeCode))
            //    int.TryParse(gkEntity.SCRecordType.TypeCode, out pTestItemId);
            //else
            //    ZhiFang.Common.Log.Log.Warn("监测类型对照码为：" + gkEntity.SCRecordType.Id + ",对照码为空！");

            //if (pTestItemId > 0)
            //    pTestItem = ClassMapperHelp.GetMapper<TestItem, TestItem>(IDTestItemDao.FindById(pTestItemId));

            //if (pTestItem == null)
            //    ZhiFang.Common.Log.Log.Warn("监测类型对照码为：" + gkEntity.SCRecordType.Id + ",对照码为空！");

            //foreach (var item in dtlEntityList)
            //{
            //    string itemCode = item.SCRecordTypeItem.ItemCode;
            //    if (string.IsNullOrEmpty(itemCode))
            //    {
            //        ZhiFang.Common.Log.Log.Warn("监测类型记录项编码为：" + item.SCRecordTypeItem.Id + ",对照码为空！");
            //        continue;
            //    }

            //    string testItemHql = "testitem.Id=" + itemCode;
            //    IList<string> testItemList = new List<string>();
            //    //是否存在多个LIS检验子项目
            //    if (itemCode.IndexOf('^') >= 0)
            //    {
            //        var list = itemCode.Split('^');
            //        for (int i = 0; i < list.Length; i++)
            //        {
            //            testItemList.Add(list[i]);
            //        }
            //        itemCode = itemCode.Replace('^', ',');
            //        testItemHql = "testitem.Id in (" + itemCode + ")";
            //    }
            //    else
            //    {
            //        testItemList.Add(itemCode);
            //    }

            //    IList<TestItem> testItemList2 = IDTestItemDao.SelectListByHQL(testItemHql);
            //    //检验子项目转换
            //    foreach (var testItem in testItemList2)
            //    {
            //        MEGroupSampleItem entity = new MEGroupSampleItem();
            //        entity.GroupSampleFormID = docEntity.Id;
            //        if (pTestItemId > 0)
            //            entity.PItemNo = pTestItemId;
            //        entity.ItemNo = testItem.Id;
            //        entity.Units = testItem.Unit;
            //        entity.ZDY2 = item.ItemResult;
            //        entity.ReportValue = item.ItemResult;
            //        entity.DispOrder = testItem.DispOrder;
            //        entity.GTestDate = docEntity.GTestDate;

            //        entity.ZDY1 = docEntity.Id.ToString();
            //        entity.ZDY5 = item.Id.ToString();
            //        //entity.SerialNo = docEntity.GBarCode;

            //        itemsList.Add(entity);
            //    }

            //}

            return itemsList;
        }

        #endregion

    }
}