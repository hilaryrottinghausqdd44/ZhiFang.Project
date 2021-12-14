
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.BloodTransfusion.Common;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodTransItem : BaseBLL<BloodTransItem>, ZhiFang.IBLL.BloodTransfusion.IBBloodTransItem
    {
        IDBloodTransRecordTypeItemDao IDBloodTransRecordTypeItemDao { get; set; }
        IDBloodTransRecordTypeDao IDBloodTransRecordTypeDao { get; set; }
        IBSCOperation IBSCOperation { get; set; }

        #region 定制查询
        public EntityList<BloodTransItem> SearchBloodTransItemListByTransFormId(long transFormId)
        {
            EntityList<BloodTransItem> tempEntityList = new EntityList<BloodTransItem>();

            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" bloodtransitem.BloodTransForm.Id=" + transFormId);
            strbHql.Append(" and bloodtransitem.ContentTypeID=" + ClassificationOfTransfusionContent.输血记录项.Key);
            string sort = "bloodtransitem.BloodTransRecordTypeItem.BloodTransRecordType.DispOrder ASC,bloodtransitem.BloodTransRecordTypeItem.DispOrder ASC";
            tempEntityList = ((IDBloodTransItemDao)base.DBDao).GetListByHQL(strbHql.ToString(), sort, -1, -1);

            return tempEntityList;
        }
        private BloodTransRecordTypeItem GetBloodTransRecordTypeItem(long contentTypeId)
        {
            BloodTransRecordTypeItem entity = new BloodTransRecordTypeItem();
            StringBuilder strbHql = new StringBuilder();
            //按输血过程内容分类ID获取
            strbHql.Append(" bloodtransrecordtypeitem.IsVisible=1 ");
            strbHql.Append(" and bloodtransrecordtypeitem.BloodTransRecordType.IsVisible=1 ");
            strbHql.Append(" and bloodtransrecordtypeitem.BloodTransRecordType.ContentTypeID=" + contentTypeId);
            IList<BloodTransRecordTypeItem> tempEntityList = IDBloodTransRecordTypeItemDao.GetListByHQL(strbHql.ToString());
            if (tempEntityList != null && tempEntityList.Count > 0)
            {
                entity = tempEntityList[0];
            }
            return entity;
        }
        public BloodTransItem GetBloodTransItemByContentTypeID(long contentTypeId, string transFormId)
        {
            BloodTransItem entity = new BloodTransItem();
            StringBuilder strbHql = new StringBuilder();

            if (transFormId == "-1") transFormId = "";
            //输血过程记录主单ID为空时,表示新增输血过程记录
            if (string.IsNullOrEmpty(transFormId))
            {
                //按输血过程内容分类ID获取
                strbHql.Append(" bloodtransrecordtypeitem.IsVisible=1 ");
                strbHql.Append(" and bloodtransrecordtypeitem.BloodTransRecordType.IsVisible=1 ");
                strbHql.Append(" and bloodtransrecordtypeitem.BloodTransRecordType.ContentTypeID=" + contentTypeId);
                IList<BloodTransRecordTypeItem> tempEntityList = IDBloodTransRecordTypeItemDao.GetListByHQL(strbHql.ToString());
                entity.Visible = true;
                if (tempEntityList != null && tempEntityList.Count > 0)
                {
                    entity.DispOrder = tempEntityList[0].DispOrder;
                    entity.BloodTransRecordTypeItem = tempEntityList[0];
                }
            }
            else
            {
                strbHql.Append(" bloodtransitem.BloodTransForm.Id=" + transFormId);
                strbHql.Append(" and bloodtransitem.ContentTypeID=" + contentTypeId);
                IList<BloodTransItem> tempEntityList = ((IDBloodTransItemDao)base.DBDao).GetListByHQL(strbHql.ToString());
                if (tempEntityList != null && tempEntityList.Count > 0)
                    entity = tempEntityList[0];
            }
            return entity;
        }

        #endregion

        #region 新增输血过程记录
        /// <summary>
        /// 新增发血血袋输血记录项(病人体征信息)
        /// </summary>
        /// <returns></returns>
        public void AddTransfusionAntriesList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, long empID, string empName)
        {
            for (int i = 0; i < transfusionAntriesList.Count; i++)
            {
                BloodTransItem entity = transfusionAntriesList[i];
                entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.输血记录项.Key);
                bool result = AddBloodTransItem(transForm, transfusionAntriesList[i], empID, empName);
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},保存输血过程记录(病人体征信息)失败!", transForm.BBagCode);
                    throw new Exception(brdv.ErrorInfo);
                }
            }
        }
        /// <summary>
        /// 新增发血血袋不良反应选择项集合信息
        /// </summary>
        /// <returns></returns>
        public void AddAdverseReactionList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> adverseReactionList, long empID, string empName)
        {
            for (int i = 0; i < adverseReactionList.Count; i++)
            {
                BloodTransItem entity = adverseReactionList[i];
                entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.不良反应选择项.Key);
                entity.NumberItemResult = null;
                bool result = AddBloodTransItem(transForm, entity, empID, empName);
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},保存血袋不良反应失败失败!", transForm.BBagCode);
                    throw new Exception(brdv.ErrorInfo);
                }
            }
        }
        /// <summary>
        /// 新增发血血袋临床处理措施集合信息
        /// </summary>
        /// <returns></returns>
        public void AddClinicalMeasuresList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> clinicalMeasuresList, long empID, string empName)
        {
            for (int i = 0; i < clinicalMeasuresList.Count; i++)
            {
                BloodTransItem entity = clinicalMeasuresList[i];
                entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.临床处理措施.Key);
                entity.NumberItemResult = null;
                bool result = AddBloodTransItem(transForm, entity, empID, empName);
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},保存血袋临床处理措施失败!", transForm.BBagCode);
                    throw new Exception(brdv.ErrorInfo);
                }
            }
        }
        public void AddClinicalResults(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResults, long empID, string empName)
        {
            BloodTransItem entity = clinicalResults;
            entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.临床处理结果.Key);
            entity.NumberItemResult = null;
            bool result = AddBloodTransItem(transForm, entity, empID, empName);
            ZhiFang.Common.Log.Log.Debug("发血单号为：" + transForm.BloodBOutItem.Id + ",的临床处理结果保存返回值为：" + result);
            if (result == false)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("血袋号为:{0},保存血袋临床处理结果信息失败!", transForm.BBagCode);
                throw new Exception(brdv.ErrorInfo);
            }
        }
        public void AddClinicalResultsDesc(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResultsDesc, long empID, string empName)
        {
            BloodTransItem entity = clinicalResultsDesc;
            entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.临床处理结果描述.Key);
            entity.NumberItemResult = null;
            bool result = AddBloodTransItem(transForm, entity, empID, empName);
            ZhiFang.Common.Log.Log.Debug("发血单号为：" + transForm.BloodBOutItem.Id + ",的临床处理结果保存返回值为：" + result + ",编辑值：" + entity.TransItemResult);
            if (result == false)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("血袋号为:{0},保存血袋临床处理结果描述信息失败!", transForm.BBagCode);
                throw new Exception(brdv.ErrorInfo);
            }

        }
        public bool AddBloodTransItem(BloodTransForm transForm, BloodTransItem transfusionAntries, long empID, string empName)
        {
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (transForm.DataTimeStamp == null)
                transForm.DataTimeStamp = dataTimeStamp;

            BloodTransItem entity = ClassMapperHelp.GetMapper<BloodTransItem, BloodTransItem>(transfusionAntries);
            entity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            entity.BloodTransForm = transForm;

            if (entity.ContentTypeID == int.Parse(ClassificationOfTransfusionContent.临床处理结果描述.Key) && entity.BloodTransRecordTypeItem == null)
            {
                entity.BloodTransRecordTypeItem = GetBloodTransRecordTypeItem(entity.ContentTypeID);
            }

            if (transfusionAntries.BloodTransRecordTypeItem != null)
                entity.BloodTransRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(transfusionAntries.BloodTransRecordTypeItem.Id);
            if (entity.BloodTransRecordTypeItem != null)
                entity.DispOrder = entity.BloodTransRecordTypeItem.DispOrder;
            if (entity.BloodTransRecordType == null && transfusionAntries.BloodTransRecordType != null)
                entity.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(transfusionAntries.BloodTransRecordType.Id);
            if (entity.ContentTypeID != int.Parse(ClassificationOfTransfusionContent.输血记录项.Key) && entity.ContentTypeID != int.Parse(ClassificationOfTransfusionContent.临床处理结果描述.Key))
            {
                if (entity.BloodTransRecordTypeItem != null)
                {
                    entity.TransItemResult = entity.BloodTransRecordTypeItem.CName;
                }
            }
            if (entity.ContentTypeID == int.Parse(ClassificationOfTransfusionContent.临床处理结果描述.Key) && entity.BloodTransRecordType == null)
            {
                if (entity.BloodTransRecordTypeItem != null && entity.BloodTransRecordTypeItem.BloodTransRecordType != null)
                    entity.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(entity.BloodTransRecordTypeItem.BloodTransRecordType.Id);
            }
            //病人体征的数字结果处理
            if (entity.ContentTypeID == int.Parse(ClassificationOfTransfusionContent.输血记录项.Key) && !entity.NumberItemResult.HasValue && !string.IsNullOrEmpty(entity.TransItemResult))
            {
                double numberItemResult = int.MinValue;
                bool tryResult = double.TryParse(entity.TransItemResult, out numberItemResult);
                if (tryResult == true && numberItemResult != int.MinValue)
                    entity.NumberItemResult = numberItemResult;
            }
            else if (entity.ContentTypeID != int.Parse(ClassificationOfTransfusionContent.输血记录项.Key))
            {
                entity.NumberItemResult = null;
            }

            entity.DataAddTime = DateTime.Now;
            entity.Visible = true;
            this.Entity = entity;
            bool result = this.Add();
            return result;
        }
        #endregion

        #region 更新输血过程记录明细信息
        public void EditTransfusionAntriesList(ref BaseResultDataValue brdv, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, long empID, string empName)
        {
            StringBuilder editMemo = new StringBuilder();
            for (int i = 0; i < transfusionAntriesList.Count; i++)
            {
                BloodTransItem entity = this.Get(transfusionAntriesList[i].Id);
                string serverValue = entity.TransItemResult;

                if (entity == null) continue;
                //描述结果值处理
                string editValue = transfusionAntriesList[i].TransItemResult;
                if (string.IsNullOrEmpty(editValue)) editValue = "";
                entity.TransItemResult = editValue;
                //数字结果值处理
                entity.NumberItemResult = transfusionAntriesList[i].NumberItemResult;
                if (!entity.NumberItemResult.HasValue && !string.IsNullOrEmpty(entity.TransItemResult))
                {
                    double numberItemResult = int.MinValue;
                    bool tryResult = double.TryParse(entity.TransItemResult, out numberItemResult);
                    if (tryResult == true && numberItemResult != int.MinValue)
                        entity.NumberItemResult = numberItemResult;
                }
                if (!editValue.Equals(serverValue))
                {
                    this.Entity = entity;
                    bool result = this.Edit();
                    string editInfo = "【" + entity.BloodTransRecordType.CName + "-" + entity.BloodTransRecordTypeItem.CName + "】由原来:" + serverValue + ",修改为:" + editValue + ";";// + System.Environment.NewLine;
                    ZhiFang.Common.Log.Log.Info("发血明细单号:" + transForm.BloodBOutItem.Id + "," + editInfo);
                    editMemo.Append(editInfo);
                    if (result == false)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("血袋号为:{0},更新发血血袋临床处理结果描述信息失败!", transForm.BBagCode);
                        throw new Exception(brdv.ErrorInfo);
                    }
                }
            }
            if (editMemo.Length > 0)
                AddSCOperation(transForm, "EditTransfusionAntries", long.Parse(UpdateOperationType.病人体征记录项.Key), editMemo.ToString(), empID, empName);
        }
        public void EditClinicalResults(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResults, long empID, string empName)
        {
            BloodTransRecordTypeItem recordTypeItem = null;
            if (clinicalResults.BloodTransRecordTypeItem != null)
            {
                recordTypeItem = IDBloodTransRecordTypeItemDao.Get(clinicalResults.BloodTransRecordTypeItem.Id);
            }
            BloodTransItem entity = this.Get(clinicalResults.Id);
            if (entity == null)
            {
                brdv.success = true;
                return;
            }
            //原来的临床处理结果
            BloodTransRecordTypeItem serverRecordTypeItem = null;
            if (entity.BloodTransRecordTypeItem != null)
                serverRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(entity.BloodTransRecordTypeItem.Id);
            string serverValue = "";
            if (serverRecordTypeItem != null)
                serverValue = serverRecordTypeItem.CName;

            entity.BloodTransRecordTypeItem = recordTypeItem;
            BloodTransRecordType transRecordType = null;
            if (recordTypeItem != null)
                transRecordType = recordTypeItem.BloodTransRecordType;
            entity.BloodTransRecordType = transRecordType;
            if (recordTypeItem != null)
            {
                entity.TransItemResult = recordTypeItem.CName;
                entity.DispOrder = recordTypeItem.DispOrder;
            }
            else
            {
                entity.TransItemResult = "";
            }
            if (!serverValue.Equals(entity.TransItemResult))
            {
                entity.NumberItemResult = null;
                this.Entity = entity;
                bool result = this.Edit();
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},更新发血血袋临床处理结果信息失败!", transForm.BBagCode);
                    throw new Exception(brdv.ErrorInfo);
                }
                string editInfo = "【临床处理结果记录项】由原来:" + serverValue.ToString() + ",修改为:" + entity.TransItemResult + ";";// + System.Environment.NewLine;
                AddSCOperation(transForm, "EditClinicalResults", long.Parse(UpdateOperationType.临床处理结果记录项.Key), editInfo, empID, empName);
            }
        }
        public void EditClinicalResultsDesc(ref BaseResultDataValue brdv, BloodTransForm transForm, BloodTransItem clinicalResultsDesc, long empID, string empName)
        {
            BloodTransItem entity = this.Get(clinicalResultsDesc.Id);
            if (entity == null)
            {
                brdv.success = true;
                return;
            }
            string serverValue = entity.TransItemResult;
            if (serverValue != clinicalResultsDesc.TransItemResult)
            {
                if (entity.ContentTypeID == int.Parse(ClassificationOfTransfusionContent.临床处理结果描述.Key) && entity.BloodTransRecordTypeItem == null)
                {
                    entity.BloodTransRecordTypeItem = GetBloodTransRecordTypeItem(entity.ContentTypeID);
                }
                entity.TransItemResult = clinicalResultsDesc.TransItemResult;
                if (entity.BloodTransRecordType == null)
                {
                    entity.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(entity.BloodTransRecordTypeItem.BloodTransRecordType.Id);
                }
                this.Entity = entity;
                bool result = this.Edit();
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},更新发血血袋临床处理结果描述信息失败!", transForm.BBagCode);
                    throw new Exception(brdv.ErrorInfo);
                }
                string editInfo = "【输血过程临床处理结果描述】由原来:" + serverValue + ",修改为:" + entity.TransItemResult + ";";//+ System.Environment.NewLine;
                ZhiFang.Common.Log.Log.Debug("发血单号为：" + transForm.BloodBOutItem.Id + ",的临床处理结果描述保存返回值为：" + result + ",编辑信息：" + editInfo);
                AddSCOperation(transForm, "EditClinicalResultsDesc", long.Parse(UpdateOperationType.临床处理结果描述记录项.Key), editInfo, empID, empName);
            }
        }

        #endregion

        #region 批量修改录入
        /// <summary>
        /// 批量修改录入获取病人体征录入信息
        /// 后续可考虑按病人体征字典及记录项结果分组进行比较（但速度会比较慢）
        /// </summary>
        /// <param name="outDtlIdStr"></param>
        /// <returns></returns>
        public EntityList<BloodTransItem> SearchPatientSignsByOutDtlIdStr(string outDtlIdStr)
        {
            EntityList<BloodTransItem> entityList = new EntityList<BloodTransItem>();
            entityList.list = new List<BloodTransItem>();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                return entityList;
            }

            string[] outDtlArr = outDtlIdStr.Split(',');
            string outDtlHql = _getOutDtlIdStrHql(outDtlArr);
            //获取当前选择的发血血袋的所有输血过程记录项信息
            StringBuilder strb = new StringBuilder();
            strb.Append(" 1=1 ");
            strb.Append(" and bloodtransitem.ContentTypeID =" + ClassificationOfTransfusionContent.输血记录项.Key);
            if (outDtlHql.Length > 0) strb.Append(" and " + outDtlHql);
            ZhiFang.Common.Log.Log.Debug("SearchPatientSignsByOutDtlIdStr.HQL:" + strb.ToString());
            IList<BloodTransItem> transItemList = this.SearchListByHQL(strb.ToString());
            //获取所有在用的(病人体征)输血记录项字典信息
            IList<BloodTransRecordTypeItem> recordTypeItemList = _getRecordTypeItem(ClassificationOfTransfusionContent.输血记录项.Key);
            //按发血血袋明细分组
            var groupByList = transItemList.GroupBy(p => new
            {
                p.BloodTransForm.BloodBOutItem
            });
            if (transItemList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取病人体征记录项--已登记的发血血袋明细的分组数:{0},不等于当前选择的发血血袋明细数:{1},属于完全未作过输血过程记录登记情况,结果值全部为空!", groupByList.Count(), outDtlArr.Length));
                entityList.list = _getBloodTransItemList(recordTypeItemList, 1);
            }
            else if (transItemList.Count > 0 && groupByList.Count() != outDtlArr.Length)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取病人体征记录项--已登记的发血血袋明细的分组数:{0},不等于当前选择的发血血袋明细数:{1},属于部分已登记但部分完全未登记情况,结果值全部为空!", groupByList.Count(), outDtlArr.Length));
                entityList.list = GetPatientSigns2(outDtlArr, transItemList);
            }
            else if (transItemList.Count > 0 && groupByList.Count() == outDtlArr.Length)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取病人体征记录项--已登记的发血血袋明细的分组数:{0},等于当前选择的发血血袋明细数:{1}!", groupByList.Count(), outDtlArr.Length));
                entityList.list = GetPatientSigns(outDtlArr, transItemList);
            }
            entityList.count = entityList.list.Count();
            return entityList;
        }
        /// <summary>
        /// 发血血袋的病人体征信息：部分已登记但部分完全未登记情况
        /// </summary>
        /// <param name="outDtlArr"></param>
        /// <param name="transItemList"></param>
        /// <returns></returns>
        private IList<BloodTransItem> GetPatientSigns2(string[] outDtlArr, IList<BloodTransItem> transItemList)
        {
            IList<BloodTransItem> entityList = new List<BloodTransItem>();
            //先按发血血袋明细及输血过程记录分类及输血过程记录项分组
            var groupByList2 = transItemList.GroupBy(p => new
            {
                //p.BloodTransForm.BloodBOutItem,
                p.BloodTransRecordType,
                p.BloodTransRecordTypeItem
            });
            ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取病人体征记录项--属于部分已登记但部分完全未登记情况!"));
            foreach (var groupBy2 in groupByList2)
            {
                //再按某一记录项的结果值分组
                var groupByList3 = groupBy2.GroupBy(p => p.TransItemResult);
                BloodTransItem transItem = new BloodTransItem();
                transItem.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.输血记录项.Key);
                transItem.Visible = true;
                transItem.BloodTransRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(groupBy2.Key.BloodTransRecordTypeItem.Id);
                transItem.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(groupBy2.Key.BloodTransRecordType.Id);
                transItem.DispOrder = transItem.BloodTransRecordTypeItem.DispOrder;
                //某一记录项结果值仅有一个时
                if (groupByList3.Count() == 1)
                {
                    var transItemResult = groupBy2.ElementAt(0).TransItemResult;
                    if (string.IsNullOrEmpty(transItemResult))
                    {
                        //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且为空时
                        transItem.BatchSign = 1;
                        transItem.TransItemResult = "";
                        transItem.NumberItemResult = null;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值全部为空!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                    }
                    else if (!string.IsNullOrEmpty(transItemResult))
                    {
                        //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且不为空时
                        transItem.BatchSign = 2;
                        transItem.TransItemResult = "";
                        transItem.NumberItemResult = null;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值全部相同!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                    }
                }
                else if (groupByList3.Count() > 1)
                {
                    //当前选择的发血血袋明细的某一记录项的结果值不完成一致
                    transItem.BatchSign = 2;
                    transItem.TransItemResult = "";
                    transItem.NumberItemResult = null;
                    ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值不完成一致!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值分组数为空!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                }
                entityList.Add(transItem);
            }
            return entityList;
        }
        private IList<BloodTransItem> GetPatientSigns(string[] outDtlArr, IList<BloodTransItem> transItemList)
        {
            IList<BloodTransItem> entityList = new List<BloodTransItem>();
            //先按发血血袋明细及输血过程记录分类及输血过程记录项分组
            var groupByList2 = transItemList.GroupBy(p => new
            {
                //p.BloodTransForm.BloodBOutItem,
                p.BloodTransRecordType,
                p.BloodTransRecordTypeItem
            });
            foreach (var groupBy2 in groupByList2)
            {
                //再按某一记录项的结果值分组
                var groupByList3 = groupBy2.GroupBy(p => p.TransItemResult);
                BloodTransItem transItem = new BloodTransItem();
                transItem.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.输血记录项.Key);
                transItem.Visible = true;
                transItem.BloodTransRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(groupBy2.Key.BloodTransRecordTypeItem.Id);
                transItem.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(groupBy2.Key.BloodTransRecordType.Id);
                transItem.DispOrder = transItem.BloodTransRecordTypeItem.DispOrder;
                //某一记录项结果值仅有一个时
                if (groupByList3.Count() == 1)
                {
                    var transItemResult = groupBy2.ElementAt(0).TransItemResult;
                    if (string.IsNullOrEmpty(transItemResult))
                    {
                        //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且为空时
                        transItem.BatchSign = 1;
                        transItem.TransItemResult = "";
                        transItem.NumberItemResult = null;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值全部为空!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                    }
                    else if (!string.IsNullOrEmpty(transItemResult))
                    {
                        //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且不为空时
                        transItem.BatchSign = 3;
                        transItem.TransItemResult = groupBy2.ElementAt(0).TransItemResult;
                        transItem.NumberItemResult = groupBy2.ElementAt(0).NumberItemResult;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值全部相同!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                    }
                }
                else if (groupByList3.Count() > 1)
                {
                    //当前选择的发血血袋明细的某一记录项的结果值不完成一致
                    transItem.BatchSign = 2;
                    transItem.TransItemResult = "";
                    transItem.NumberItemResult = null;
                    ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值不完成一致!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取病人体征记录项:{0}--{1},结果值分组数为空!", groupBy2.ElementAt(0).BloodTransRecordType.CName, groupBy2.ElementAt(0).BloodTransRecordTypeItem.CName));
                }
                entityList.Add(transItem);
            }
            return entityList;
        }
        public EntityList<BloodTransItem> SearchAdverseReactionOptionsByOutDtlIdStr(string outDtlIdStr, long recordTypeId, string where, ref int batchSign)
        {
            EntityList<BloodTransItem> entityList = new EntityList<BloodTransItem>();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                return entityList;
            }

            string[] outDtlArr = outDtlIdStr.Split(',');
            string outDtlHql = _getOutDtlIdStrHql(outDtlArr);
            StringBuilder strb = new StringBuilder();
            strb.Append(" 1=1 ");
            strb.Append(" and bloodtransitem.ContentTypeID =" + ClassificationOfTransfusionContent.不良反应选择项.Key);
            strb.Append(" and bloodtransitem.BloodTransRecordType.Id =" + recordTypeId + " "); ;
            if (outDtlHql.Length > 0) strb.Append(" and " + outDtlHql);
            if (!string.IsNullOrEmpty(where))
                strb.Append(" and (" + where + ") ");
            ZhiFang.Common.Log.Log.Debug("SearchAdverseReactionOptionsByOutDtlIdStr.HQL:" + strb.ToString());
            IList<BloodTransItem> transItemList = this.SearchListByHQL(strb.ToString());
            ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的不良反应症状记录为:{1}!", outDtlIdStr, transItemList.Count));

            if (transItemList.Count == 0)
            {
                batchSign = 1;
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的不良反应症状记录为空!", outDtlIdStr));
                return entityList;
            }
            var groupByList = transItemList.GroupBy(p => new
            {
                //p.BloodTransForm.BloodBOutItem,
                p.BloodTransRecordTypeItem
            });
            //各发血血袋明细都存在的不良反应症状集合信息
            IList<BloodTransItem> entityList2 = new List<BloodTransItem>();

            foreach (var groupBy in groupByList)
            {
                var transItemList2 = transItemList.Where(p => p.BloodTransRecordTypeItem.Id == groupBy.Key.BloodTransRecordTypeItem.Id);
                //当前选择的发血血袋明细的某一不良反应时间点的某不良反应症状记录都存在
                if (transItemList2.Count() == outDtlArr.Length)
                {
                    if (batchSign != 2) batchSign = 3;
                    ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的某一不良反应时间点的某不良反应症状记录:{1},已登记的不良反应症状的分组数:{2},等于当前选择的发血血袋明细数:{3},该记录项都存在于各发血血袋明细里!", outDtlIdStr, groupBy.ElementAt(0).BloodTransRecordTypeItem.CName, transItemList2.Count(), outDtlArr.Length));
                    entityList2.Add(_getBloodTransItem(groupBy.ElementAt(0), 3));
                }
                else if (transItemList2.Count() != outDtlArr.Length)
                {
                    batchSign = 2;
                    ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的某一不良反应时间点的某不良反应症状记录:{1},已登记的不良反应症状的分组数:{2},不等于当前选择的发血血袋明细数:{3},该记录项部分存在于各发血血袋明细里!", outDtlIdStr, groupBy.ElementAt(0).BloodTransRecordTypeItem.CName, transItemList2.Count(), outDtlArr.Length));
                }
            }
            if (entityList2.Count == 0)
            {
                batchSign = 1;
            }
            entityList.list = entityList2;
            entityList.count = entityList.list.Count();
            return entityList;
        }
        public EntityList<BloodTransItem> SearchClinicalMeasuresByOutDtlIdStr(string outDtlIdStr, string where, ref int batchSign)
        {
            EntityList<BloodTransItem> entityList = new EntityList<BloodTransItem>();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                return entityList;
            }

            string[] outDtlArr = outDtlIdStr.Split(',');
            string outDtlHql = _getOutDtlIdStrHql(outDtlArr);
            StringBuilder strb = new StringBuilder();
            strb.Append(" 1=1 ");
            strb.Append(" and bloodtransitem.ContentTypeID =" + ClassificationOfTransfusionContent.临床处理措施.Key);
            if (outDtlHql.Length > 0) strb.Append(" and " + outDtlHql);
            if (!string.IsNullOrEmpty(where))
                strb.Append(" and (" + where + ") ");
            ZhiFang.Common.Log.Log.Debug("SearchClinicalMeasuresByOutDtlIdStr.HQL:" + strb.ToString());
            IList<BloodTransItem> transItemList = this.SearchListByHQL(strb.ToString());
            ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的临床处理措施记录为:{1}!", outDtlIdStr, transItemList.Count));

            if (transItemList.Count == 0)
            {
                batchSign = 1;
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的临床处理措施记录为空!", outDtlIdStr));
                return entityList;
            }
            var groupByList = transItemList.GroupBy(p => new
            {
                //p.BloodTransForm.BloodBOutItem,
                p.BloodTransRecordTypeItem
            });
            //各发血血袋明细都存在的临床处理措施集合信息
            IList<BloodTransItem> entityList2 = new List<BloodTransItem>();
            foreach (var groupBy in groupByList)
            {
                var transItemList2 = transItemList.Where(p => p.BloodTransRecordTypeItem.Id == groupBy.Key.BloodTransRecordTypeItem.Id);
                //当前选择的发血血袋明细的某一临床处理措施记录都存在
                if (transItemList2.Count() == outDtlArr.Length)
                {
                    if (batchSign != 2) batchSign = 3;
                    ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的某一临床处理措施记录:{1},已登记的临床处理措施的分组数:{2},等于当前选择的发血血袋明细数:{3},该记录项都存在于各发血血袋明细里!", outDtlIdStr, groupBy.ElementAt(0).BloodTransRecordTypeItem.CName, transItemList2.Count(), outDtlArr.Length));
                    entityList2.Add(_getBloodTransItem(groupBy.ElementAt(0), 3));
                }
                else if (transItemList2.Count() != outDtlArr.Length)
                {
                    batchSign = 2;
                    ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},的某一临床处理措施记录:{1},已登记的临床处理措施的分组数:{2},不等于当前选择的发血血袋明细数:{3},该记录项部分存在于各发血血袋明细里!", outDtlIdStr, groupBy.ElementAt(0).BloodTransRecordTypeItem.CName, transItemList2.Count(), outDtlArr.Length));
                }
            }
            if (entityList2.Count == 0)
            {
                batchSign = 1;
            }

            entityList.list = entityList2;
            entityList.count = entityList.list.Count();
            return entityList;
        }
        public BloodTransItem SearchClinicalResultsByOutDtlIdStr(string outDtlIdStr, string where, ref int batchSign)
        {
            BloodTransItem entity = new BloodTransItem();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                return entity;
            }
            string[] outDtlArr = outDtlIdStr.Split(',');
            string outDtlHql = _getOutDtlIdStrHql(outDtlArr);
            StringBuilder strb = new StringBuilder();
            strb.Append(" 1=1 ");
            strb.Append(" and bloodtransitem.ContentTypeID =" + ClassificationOfTransfusionContent.临床处理结果.Key);
            if (outDtlHql.Length > 0) strb.Append(" and " + outDtlHql);
            if (!string.IsNullOrEmpty(where))
                strb.Append(" and (" + where + ") ");
            ZhiFang.Common.Log.Log.Debug("SearchClinicalResultsByOutDtlIdStr.HQL:" + strb.ToString());
            IList<BloodTransItem> transItemList = this.SearchListByHQL(strb.ToString());
            ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取临床处理结果--当前选择的发血血袋明细:{0},的临床处理结果记录为:{1}!", outDtlIdStr, transItemList.Count));

            if (transItemList.Count == 0)
            {
                batchSign = 1;
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取临床处理结果--当前选择的发血血袋明细:{0},的临床处理结果记录为空!", outDtlIdStr));
                return entity;
            }

            //按发血血袋明细分组
            var groupByList2 = transItemList.GroupBy(p => new
            {
                p.BloodTransForm.BloodBOutItem
            });
            if (transItemList.Count > 0 && groupByList2.Count() != outDtlArr.Length)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取临床处理结果--已登记的发血血袋明细的分组数:{0},当前选择的发血血袋明细数:{1},属于部分已登记但部分完全未登记情况,结果值全部为空!", groupByList2.Count(), outDtlArr.Length));
                batchSign = 2;
                entity = _getBloodTransItem(groupByList2.ElementAt(0).ElementAt(0), batchSign);
                entity.TransItemResult = "";
                entity.NumberItemResult = null;
                entity.BloodTransRecordTypeItem = null;
                entity.BloodTransRecordType = null;
            }
            else
            {
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取临床处理结果--已登记的发血血袋明细的分组数:{0},当前选择的发血血袋明细数:{1}!", groupByList2.Count(), outDtlArr.Length));
                var groupByList3 = transItemList.GroupBy(p => new
                {
                    p.TransItemResult
                });
                entity = _getBloodTransItem(groupByList3.ElementAt(0).ElementAt(0), batchSign);
                if (groupByList3.Count() == 1)
                {
                    var transItemResult = groupByList3.ElementAt(0).ElementAt(0).TransItemResult;
                    if (string.IsNullOrEmpty(transItemResult))
                    {
                        //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且为空时
                        entity.BatchSign = 1;
                        batchSign = 1;
                        entity.TransItemResult = "";
                        entity.NumberItemResult = null;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)临床处理结果:{0},结果值全部为空!", outDtlIdStr));
                    }
                    else if (!string.IsNullOrEmpty(transItemResult))
                    {
                        //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且不为空时
                        entity.BatchSign = 3;
                        batchSign = 3;
                        entity.TransItemResult = transItemResult;
                        //entity.NumberItemResult = groupBy2.ElementAt(0).NumberItemResult;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)临床处理结果:{0},结果值全部相同!", outDtlIdStr));
                    }
                }
                else if (groupByList3.Count() > 1)
                {
                    //当前选择的发血血袋明细的某一记录项的结果值不完成一致
                    entity.BatchSign = 2;
                    batchSign = 2;
                    entity.TransItemResult = "";
                    entity.NumberItemResult = null;
                    ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取临床处理结果:{0},结果值不完成一致!", outDtlIdStr));
                }
                else
                {
                    entity.BatchSign = 2;
                    batchSign = 2;
                    entity.TransItemResult = "";
                    entity.NumberItemResult = null;
                    ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取临床处理结果:{0},结果值分组数为空!", outDtlIdStr));
                }
            }
            return entity;
        }
        public BloodTransItem SearchClinicalResultsDescByOutDtlIdStr(string outDtlIdStr, string where, ref int batchSign)
        {
            BloodTransItem entity = new BloodTransItem();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                return entity;
            }

            string[] outDtlArr = outDtlIdStr.Split(',');
            string outDtlHql = _getOutDtlIdStrHql(outDtlArr);
            StringBuilder strb = new StringBuilder();
            strb.Append(" 1=1 ");
            strb.Append(" and bloodtransitem.ContentTypeID =" + ClassificationOfTransfusionContent.临床处理结果描述.Key);
            if (outDtlHql.Length > 0) strb.Append(" and " + outDtlHql);
            if (!string.IsNullOrEmpty(where))
                strb.Append(" and (" + where + ") ");
            ZhiFang.Common.Log.Log.Debug("SearchClinicalResultsDescByOutDtlIdStr.HQL:" + strb.ToString());
            IList<BloodTransItem> transItemList = this.SearchListByHQL(strb.ToString());

            if (transItemList.Count == 0)
            {
                batchSign = 1;
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取临床处理结果描述--当前选择的发血血袋明细:{0},的临床处理结果描述记录为空!", outDtlIdStr));
                return entity;
            }

            //按发血血袋明细分组
            var groupByList2 = transItemList.GroupBy(p => new
            {
                p.BloodTransForm.BloodBOutItem
            });
            if (transItemList.Count > 0 && groupByList2.Count() != outDtlArr.Length)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取临床处理结果描述--已登记的发血血袋明细的分组数:{0},当前选择的发血血袋明细数:{1},属于部分已登记但部分完全未登记情况,结果值全部为空!", groupByList2.Count(), outDtlArr.Length));
                batchSign = 2;
                entity = _getBloodTransItem(groupByList2.ElementAt(0).ElementAt(0), batchSign);
                entity.TransItemResult = "";
                entity.NumberItemResult = null;
            }
            else
            {
                ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)获取临床处理结果描述--已登记的发血血袋明细的分组数:{0},当前选择的发血血袋明细数:{1}!", groupByList2.Count(), outDtlArr.Length));
                var groupByList = transItemList.GroupBy(p => new
                {
                    p.BloodTransRecordTypeItem
                });
                //继续判断结果是否完全相同
                foreach (var groupBy2 in groupByList)
                {
                    var groupByList3 = groupBy2.GroupBy(p => p.TransItemResult);
                    entity = _getBloodTransItem(groupByList.ElementAt(0).ElementAt(0), batchSign);
                    if (groupByList3.Count() == 1)
                    {
                        var transItemResult = groupBy2.ElementAt(0).TransItemResult;
                        if (string.IsNullOrEmpty(transItemResult))
                        {
                            //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且为空时
                            entity.BatchSign = 1;
                            batchSign = 1;
                            entity.TransItemResult = "";
                            entity.NumberItemResult = null;
                            ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取临床处理结果描述:{0},结果值全部为空!", outDtlIdStr));
                        }
                        else if (!string.IsNullOrEmpty(transItemResult))
                        {
                            //当前选择的发血血袋明细的某一记录项的结果值存在且结果值相同且不为空时
                            entity.BatchSign = 3;
                            batchSign = 3;
                            entity.TransItemResult = groupBy2.ElementAt(0).TransItemResult;
                            //entity.NumberItemResult = groupBy2.ElementAt(0).NumberItemResult;
                            ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取临床处理结果描述:{0},结果值全部相同!", outDtlIdStr));
                        }
                    }
                    else if (groupByList3.Count() > 1)
                    {
                        //当前选择的发血血袋明细的某一记录项的结果值不完成一致
                        entity.BatchSign = 2;
                        batchSign = 2;
                        entity.TransItemResult = "";
                        entity.NumberItemResult = null;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取临床处理结果描述:{0},结果值不完成一致!", outDtlIdStr));
                    }
                    else
                    {
                        entity.BatchSign = 2;
                        batchSign = 2;
                        entity.TransItemResult = "";
                        entity.NumberItemResult = null;
                        ZhiFang.Common.Log.Log.Debug(string.Format("(批量修改录入)获取临床处理结果描述:{0},结果值分组数为空!", outDtlIdStr));
                    }
                }

            }
            return entity;
        }
        private IList<BloodTransRecordTypeItem> _getRecordTypeItem(string contentTypeID)
        {
            StringBuilder recordTypeItemHql = new StringBuilder();
            recordTypeItemHql.Append(" bloodtransrecordtypeitem.IsVisible=1 ");
            recordTypeItemHql.Append(" and bloodtransrecordtypeitem.BloodTransRecordType.IsVisible=1 ");
            recordTypeItemHql.Append(" and bloodtransrecordtypeitem.BloodTransRecordType.ContentTypeID=" + contentTypeID);
            IList<BloodTransRecordTypeItem> recordTypeItemList = IDBloodTransRecordTypeItemDao.GetListByHQL(recordTypeItemHql.ToString());
            return recordTypeItemList;
        }
        private string _getOutDtlIdStrHql(string[] arrStr)
        {
            if (arrStr.Length <= 0) return "";

            StringBuilder strb = new StringBuilder();
            strb.Append(" (");
            for (int i = 0; i < arrStr.Length; i++)
            {
                strb.Append(" bloodtransitem.BloodTransForm.BloodBOutItem.Id ='" + arrStr[i] + "' ");
                if (i < arrStr.Length - 1) strb.Append(" or ");
            }
            strb.Append(") ");
            // ZhiFang.Common.Log.Log.Debug(strb.ToString());
            return strb.ToString();
        }
        private IList<BloodTransItem> _getBloodTransItemList(IList<BloodTransRecordTypeItem> recordTypeItemList, int batchSign)
        {
            IList<BloodTransItem> entityList = new List<BloodTransItem>();
            foreach (var bloodTransRecordTypeItem in recordTypeItemList)
            {
                BloodTransItem transItem = new BloodTransItem();
                transItem.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.输血记录项.Key);
                transItem.DispOrder = bloodTransRecordTypeItem.DispOrder;
                transItem.Visible = true;
                transItem.BatchSign = batchSign;
                transItem.TransItemResult = "";
                transItem.NumberItemResult = null;
                transItem.BloodTransRecordTypeItem = bloodTransRecordTypeItem;
                transItem.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(bloodTransRecordTypeItem.BloodTransRecordType.Id);
                entityList.Add(transItem);
            }
            return entityList;
        }
        private BloodTransItem _getBloodTransItem(BloodTransItem entity, int batchSign)
        {
            BloodTransItem transItem = new BloodTransItem();
            transItem.BatchSign = batchSign;
            transItem.Visible = entity.Visible;
            transItem.DispOrder = entity.DispOrder;
            transItem.ContentTypeID = entity.ContentTypeID;
            transItem.TransItemResult = entity.TransItemResult;
            transItem.NumberItemResult = entity.NumberItemResult;
            transItem.BloodTransRecordTypeItem = entity.BloodTransRecordTypeItem;
            transItem.BloodTransRecordType = entity.BloodTransRecordType;
            return transItem;
        }

        public BaseResultBool DelBatchTransItemByAdverseReactions(string outDtlIdStr, long recordTypeId, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入参数(outDtlIdStr)为空";
                return tempBaseResultBool;
            }
            string[] arrStr = outDtlIdStr.Split(',');
            int result = 0;
            StringBuilder strb = new StringBuilder();
            foreach (var outDtlId in arrStr)
            {
                strb.Clear();
                strb.Append(" From BloodTransItem bloodtransitem where bloodtransitem.BloodTransForm.BloodBOutItem.Id = '" + outDtlId + "'");
                strb.Append(" and bloodtransitem.ContentTypeID =" + ClassificationOfTransfusionContent.不良反应选择项.Key);
                strb.Append(" and bloodtransitem.BloodTransRecordType.Id =" + recordTypeId + "");
                result = this.DeleteByHql(strb.ToString());
            }

            return tempBaseResultBool;
        }
        public BaseResultBool DelBatchTransItemByClinicalMeasures(string outDtlIdStr, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入参数(outDtlIdStr)为空";
                return tempBaseResultBool;
            }
            string[] arrStr = outDtlIdStr.Split(',');
            int result = 0;
            StringBuilder strb = new StringBuilder();
            foreach (var outDtlId in arrStr)
            {
                strb.Clear();
                strb.Append(" From BloodTransItem bloodtransitem where bloodtransitem.BloodTransForm.BloodBOutItem.Id = '" + outDtlId + "'");
                strb.Append(" and bloodtransitem.ContentTypeID =" + ClassificationOfTransfusionContent.临床处理措施.Key);
                result = this.DeleteByHql(strb.ToString());
            }

            return tempBaseResultBool;
        }

        public BaseResultDataValue EditBatchTransfusionAntriesByOutDtlList(BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (transfusionAntriesList == null || transfusionAntriesList.Count <= 0)
            {
                brdv.success = true;
                brdv.ErrorInfo = "传入参数(transfusionAntriesList)为空";
                return brdv;
            }
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" bloodtransitem.BloodTransForm.BloodBOutItem.Id='" + transForm.BloodBOutItem.Id + "'");
            strbHql.Append(" and bloodtransitem.ContentTypeID=" + ClassificationOfTransfusionContent.输血记录项.Key);
            IList<BloodTransItem> serverTransItemList = ((IDBloodTransItemDao)base.DBDao).GetListByHQL(strbHql.ToString());
            StringBuilder editMemo = new StringBuilder();
            for (int i = 0; i < transfusionAntriesList.Count; i++)
            {
                //判断发血血袋明细是否存在该记录项
                var tempList = serverTransItemList.Where(p => p.BloodTransRecordTypeItem.Id == transfusionAntriesList[i].BloodTransRecordTypeItem.Id);
                //如果已经存在,则不处理
                if (tempList != null && tempList.Count() > 0)
                {
                    string editValue = transfusionAntriesList[i].TransItemResult;
                    BloodTransItem entity = tempList.ElementAt(0);
                    string serverValue = entity.TransItemResult;
                    if (!string.IsNullOrEmpty(editValue) && editValue != serverValue)
                    {
                        entity.TransItemResult = editValue;
                        entity.BloodTransRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(entity.BloodTransRecordTypeItem.Id);
                        if (entity.BloodTransRecordType == null)
                        {
                            entity.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(entity.BloodTransRecordTypeItem.BloodTransRecordType.Id);
                        }
                        if (!string.IsNullOrEmpty(editValue))
                        {
                            double editValue2 = int.MinValue;
                            bool tryResult = double.TryParse(editValue, out editValue2);
                            if (tryResult == true && editValue2 != int.MinValue)
                            {
                                entity.NumberItemResult = editValue2;
                            }
                            else
                            {
                                entity.NumberItemResult = null;
                            }
                        }
                        if (!editValue.Equals(serverValue))
                        {
                            this.Entity = entity;
                            bool result = this.Edit();
                            if (result == false)
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = string.Format("血袋号为:{0},更新发血血袋的输血过程病人体征记录项信息失败!", transForm.BBagCode);
                                //throw new Exception(brdv.ErrorInfo);
                            }
                            string editInfo = "【" + entity.BloodTransRecordType.CName + "-" + entity.BloodTransRecordTypeItem.CName + "】由原来:" + serverValue + ",修改为:" + editValue + ";";// + System.Environment.NewLine;
                            ZhiFang.Common.Log.Log.Info("发血明细单号:" + transForm.BloodBOutItem.Id + "," + editInfo);
                            editMemo.Append(editInfo);
                        }
                    }
                    else
                    {
                        //ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)病人体征记录项:{0},原结果值为:{1},当前结果值为:{2},本次不作更新处理!", entity.BloodTransRecordTypeItem.CName, serverValue, editValue));
                    }
                }
                else
                {
                    BloodTransItem entity = transfusionAntriesList[i];
                    entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.输血记录项.Key);
                    bool result = AddBloodTransItem(transForm, entity, empID, empName);
                    if (result == false)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("血袋号为:{0},保存输血过程记录(病人体征信息)失败!", transForm.BBagCode);
                        //throw new Exception(brdv.ErrorInfo);
                    }
                }
            }

            if (editMemo.Length > 0)
                AddSCOperation(transForm, "EditTransfusionAntries", long.Parse(UpdateOperationType.病人体征记录项.Key), editMemo.ToString(), empID, empName);
            return brdv;
        }
        public BaseResultDataValue EditBatchAdverseReactionByOutDtlList(BloodTransForm transForm, IList<BloodTransItem> adverseReactionList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (adverseReactionList == null || adverseReactionList.Count <= 0)
            {
                brdv.success = true;
                brdv.ErrorInfo = "传入参数(adverseReactionList)为空";
                return brdv;
            }
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" bloodtransitem.BloodTransForm.BloodBOutItem.Id='" + transForm.BloodBOutItem.Id + "'");
            strbHql.Append(" and bloodtransitem.ContentTypeID=" + ClassificationOfTransfusionContent.不良反应选择项.Key);
            IList<BloodTransItem> serverTransItemList = ((IDBloodTransItemDao)base.DBDao).GetListByHQL(strbHql.ToString());

            for (int i = 0; i < adverseReactionList.Count; i++)
            {
                BloodTransItem entity = adverseReactionList[i];
                //判断发血血袋明细是否存在该记录项
                var tempList = serverTransItemList.Where(p => p.BloodTransRecordTypeItem.Id == entity.BloodTransRecordTypeItem.Id && p.BloodTransRecordType.Id == entity.BloodTransRecordType.Id);
                //如果已经存在,则不处理
                if (tempList != null && tempList.Count() > 0) continue;

                entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.不良反应选择项.Key);
                entity.NumberItemResult = null;
                bool result = AddBloodTransItem(transForm, entity, empID, empName);
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},保存血袋不良反应失败失败!", transForm.BBagCode);
                    //throw new Exception(brdv.ErrorInfo);
                }
            }
            return brdv;
        }
        public BaseResultDataValue EditBatchClinicalMeasuresByOutDtlList(BloodTransForm transForm, IList<BloodTransItem> clinicalMeasuresList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (clinicalMeasuresList == null || clinicalMeasuresList.Count <= 0)
            {
                brdv.success = true;
                brdv.ErrorInfo = "传入参数(outDtlList)为空";
                return brdv;
            }
            //先获取该发血血袋已登记的临床处理措施记录
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" bloodtransitem.BloodTransForm.BloodBOutItem.Id='" + transForm.BloodBOutItem.Id + "'");
            strbHql.Append(" and bloodtransitem.ContentTypeID=" + ClassificationOfTransfusionContent.临床处理措施.Key);
            IList<BloodTransItem> serverTransItemList = ((IDBloodTransItemDao)base.DBDao).GetListByHQL(strbHql.ToString());

            for (int i = 0; i < clinicalMeasuresList.Count; i++)
            {
                //判断发血血袋明细是否存在该记录项
                var tempList = serverTransItemList.Where(p => p.BloodTransRecordTypeItem.Id == clinicalMeasuresList[i].BloodTransRecordTypeItem.Id);
                //如果已经存在,则不处理
                if (tempList != null && tempList.Count() > 0) continue;

                BloodTransItem entity = clinicalMeasuresList[i];
                entity.ContentTypeID = int.Parse(ClassificationOfTransfusionContent.临床处理措施.Key);
                entity.NumberItemResult = null;
                bool result = AddBloodTransItem(transForm, entity, empID, empName);
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},保存血袋临床处理措施失败!", transForm.BBagCode);
                    //throw new Exception(brdv.ErrorInfo);
                }
            }
            return brdv;
        }
        public BaseResultDataValue EditBatchClinicalResultsByOutDtlListr(BloodTransForm transForm, BloodTransItem clinicalResults, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (clinicalResults == null)
            {
                brdv.success = true;
                brdv.ErrorInfo = "传入参数(clinicalResults)为空";
                return brdv;
            }

            //先获取该发血血袋已登记的临床处理结果记录
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" bloodtransitem.BloodTransForm.BloodBOutItem.Id='" + transForm.BloodBOutItem.Id + "'");
            strbHql.Append(" and bloodtransitem.ContentTypeID=" + ClassificationOfTransfusionContent.临床处理结果.Key);
            IList<BloodTransItem> serverTransItemList = ((IDBloodTransItemDao)base.DBDao).GetListByHQL(strbHql.ToString());
            //如果已经存在,则作更新处理
            if (serverTransItemList != null && serverTransItemList.Count() > 0)
            {
                BloodTransItem entity = serverTransItemList[0];
                //原来的临床处理结果
                BloodTransRecordTypeItem serverRecordTypeItem = null;
                if (entity.BloodTransRecordTypeItem != null)
                    serverRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(entity.BloodTransRecordTypeItem.Id);
                string serverValue = "";
                if (serverRecordTypeItem != null)
                    serverValue = serverRecordTypeItem.CName;

                BloodTransRecordTypeItem editRecordTypeItem = null;
                if (clinicalResults.BloodTransRecordTypeItem != null)
                {
                    editRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(clinicalResults.BloodTransRecordTypeItem.Id);
                }
                entity.BloodTransRecordTypeItem = editRecordTypeItem;
                BloodTransRecordType transRecordType = null;
                if (editRecordTypeItem != null)
                {
                    transRecordType = editRecordTypeItem.BloodTransRecordType;
                    entity.BloodTransRecordType = transRecordType;
                    entity.DispOrder = editRecordTypeItem.DispOrder;
                }
                if (editRecordTypeItem != null)
                {
                    entity.TransItemResult = editRecordTypeItem.CName;
                }
                else
                {
                    entity.TransItemResult = "";
                }
                if (!serverValue.Equals(entity.TransItemResult))
                {
                    this.Entity = entity;
                    bool result = this.Edit();
                    string editInfo = "【临床处理结果项】由原来:" + serverValue + ",修改为:" + entity.TransItemResult + ";";// + System.Environment.NewLine;
                    ZhiFang.Common.Log.Log.Info("发血明细单号:" + transForm.BloodBOutItem.Id + editInfo);
                    if (result == false)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("血袋号为:{0},更新发血血袋临床处理结果信息失败!", transForm.BBagCode);
                        //throw new Exception(brdv.ErrorInfo);
                    }
                    AddSCOperation(transForm, "EditClinicalResults", long.Parse(UpdateOperationType.临床处理结果记录项.Key), editInfo, empID, empName);
                }
            }
            else
            {
                clinicalResults.NumberItemResult = null;
                AddClinicalResults(ref brdv, transForm, clinicalResults, empID, empName);
            }

            return brdv;
        }
        public BaseResultDataValue EditBatchClinicalResultsDescByOutDtlList(BloodTransForm transForm, BloodTransItem clinicalResultsDesc, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (clinicalResultsDesc == null)
            {
                brdv.success = true;
                brdv.ErrorInfo = "传入参数(clinicalResultsDesc)为空";
                return brdv;
            }
            //先获取该发血血袋已登记的临床处理结果描述记录
            StringBuilder strbHql = new StringBuilder();
            strbHql.Append(" bloodtransitem.BloodTransForm.BloodBOutItem.Id='" + transForm.BloodBOutItem.Id + "'");
            strbHql.Append(" and bloodtransitem.ContentTypeID=" + ClassificationOfTransfusionContent.临床处理结果描述.Key);
            IList<BloodTransItem> serverTransItemList = ((IDBloodTransItemDao)base.DBDao).GetListByHQL(strbHql.ToString());

            //如果已经存在,则作更新处理
            if (serverTransItemList != null && serverTransItemList.Count() > 0)
            {
                BloodTransItem entity = serverTransItemList[0];
                string serverValue = entity.TransItemResult;
                if (!serverValue.Equals(clinicalResultsDesc.TransItemResult))
                {
                    if (entity.BloodTransRecordTypeItem == null)
                    {
                        entity.BloodTransRecordTypeItem = GetBloodTransRecordTypeItem(entity.ContentTypeID);
                    }
                    else
                    {
                        entity.BloodTransRecordTypeItem = IDBloodTransRecordTypeItemDao.Get(entity.BloodTransRecordTypeItem.Id);
                    }
                    entity.TransItemResult = clinicalResultsDesc.TransItemResult;

                    if (entity.BloodTransRecordType == null && entity.BloodTransRecordTypeItem.BloodTransRecordType != null)
                    {
                        entity.BloodTransRecordType = IDBloodTransRecordTypeDao.Get(entity.BloodTransRecordTypeItem.BloodTransRecordType.Id);
                    }
                    this.Entity = entity;
                    bool result = this.Edit();
                    string editInfo = "【" + entity.BloodTransRecordTypeItem.CName + "】由原来:" + serverValue + ",修改为:" + entity.TransItemResult + ";";
                    ZhiFang.Common.Log.Log.Info("发血明细单号:" + transForm.BloodBOutItem.Id + editInfo);
                    if (result == false)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("血袋号为:{0},更新发血血袋的临床处理结果描述信息失败!", transForm.BBagCode);
                        //throw new Exception(brdv.ErrorInfo);
                    }
                    AddSCOperation(transForm, "EditClinicalResultsDesc", long.Parse(UpdateOperationType.临床处理结果描述记录项.Key), editInfo, empID, empName);
                }
            }
            else
            {
                clinicalResultsDesc.NumberItemResult = null;
                AddClinicalResultsDesc(ref brdv, transForm, clinicalResultsDesc, empID, empName);
            }

            return brdv;
        }
        #endregion

        #region 修改信息记录
        public void AddSCOperation(BloodTransForm transForm, string businessModuleCode, long type, string editInfo, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            strbMemo.Append(editInfo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = transForm.LabID;
                sco.BobjectID = transForm.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = businessModuleCode;
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = type;
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                sco.DataUpdateTime = DateTime.Now;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        #endregion

    }
}