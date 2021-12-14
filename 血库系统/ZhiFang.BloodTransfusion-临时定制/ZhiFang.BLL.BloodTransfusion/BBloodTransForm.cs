
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
    public class BBloodTransForm : BaseBLL<BloodTransForm>, ZhiFang.IBLL.BloodTransfusion.IBBloodTransForm
    {
        IDBloodBOutItemDao IDBloodBOutItemDao { get; set; }
        IDBloodBReqFormDao IDBloodBReqFormDao { get; set; }
        IDBloodstyleDao IDBloodstyleDao { get; set; }
        IBBloodBOutForm IBBloodBOutForm { get; set; }
        IBBloodBOutItem IBBloodBOutItem { get; set; }
        IBBloodTransItem IBBloodTransItem { get; set; }
        IDBloodTransItemDao IDBloodTransItemDao { get; set; }
        IBBloodBagOperation IBBloodBagOperation { get; set; }
        IBSCOperation IBSCOperation { get; set; }

        #region 新增输血过程记录
        public BaseResultDataValue AddBloodTransFormAndDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int i = 0; i < outDtlList.Count; i++)
            {
                BloodBReqForm reqForm = outDtlList[i].BloodBReqForm;
                BloodBOutForm outForm = outDtlList[i].BloodBOutForm;
                Bloodstyle bloodstyle = outDtlList[i].Bloodstyle;

                BloodBOutItem outItem = IDBloodBOutItemDao.Get(outDtlList[i].Id);
                if (outItem == null)
                {
                    ZhiFang.Common.Log.Log.Error(string.Format("血袋号为:{0},发血明细单号为:{1},获取发血血袋信息为空!", outDtlList[i].BBagCode, outDtlList[i].Id));
                    continue;
                }

                #region 保存输血过程记录主单
                BloodTransForm editEntity = new BloodTransForm();// ClassMapperHelp.GetMapper<BloodTransForm, BloodTransForm>(transForm);

                if (outItem.BloodBReqForm == null)
                    outItem.BloodBReqForm = IDBloodBReqFormDao.Get(reqForm.Id);
                editEntity.BloodBReqForm = outItem.BloodBReqForm;

                if (editEntity.BloodBOutForm == null)
                    outItem.BloodBOutForm = IBBloodBOutForm.Get(outForm.Id);
                editEntity.BloodBOutForm = outItem.BloodBOutForm;

                if (outItem.Bloodstyle == null)
                    outItem.Bloodstyle = IDBloodstyleDao.Get(bloodstyle.Id);
                editEntity.Bloodstyle = outItem.Bloodstyle;

                outDtlList[i] = outItem;

                GetEditEntity(ref editEntity, transForm);
                editEntity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                editEntity.TransFormNo = GetTransFormNo();
                editEntity.BloodBOutItem = outDtlList[i];

                editEntity.BBagCode = outDtlList[i].BBagCode;
                editEntity.PCode = outDtlList[i].Pcode;
                editEntity.DataAddTime = DateTime.Now;
                editEntity.DataUpdateTime = DateTime.Now;
                editEntity.DispOrder = i;
                editEntity.Visible = true;
                editEntity.DataTimeStamp = editEntity.Bloodstyle.DataTimeStamp;
                if (string.IsNullOrEmpty(editEntity.BBagCode) && editEntity.BloodBOutItem != null)
                {
                    editEntity.BBagCode = editEntity.BloodBOutItem.BBagCode;
                }
                if (string.IsNullOrEmpty(editEntity.PCode) && editEntity.BloodBOutItem != null)
                {
                    editEntity.PCode = editEntity.BloodBOutItem.Pcode;
                }
                this.Entity = editEntity;
                bool result = this.Add();
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},新增输血过程记录保存失败!", editEntity.BBagCode);
                    return brdv;
                    //throw new Exception(brdv.ErrorInfo);
                    //break;
                }
                #endregion

                #region 新增输血过程记录项
                if (editEntity.DataTimeStamp == null)
                {
                    editEntity.DataTimeStamp = editEntity.Bloodstyle.DataTimeStamp;
                }
                //新增发血血袋输血记录项(病人体征信息)
                if (transfusionAntriesList != null && transfusionAntriesList.Count > 0)
                {
                    IBBloodTransItem.AddTransfusionAntriesList(ref brdv, editEntity, transfusionAntriesList, empID, empName);
                    if (brdv.success == false)
                        throw new Exception(brdv.ErrorInfo);
                }
                //新增发血血袋不良反应选择项集合信息
                if (adverseReactionList != null && adverseReactionList.Count > 0)
                {
                    IBBloodTransItem.AddAdverseReactionList(ref brdv, editEntity, adverseReactionList, empID, empName);
                    if (brdv.success == false)
                        throw new Exception(brdv.ErrorInfo);
                }
                //新增发血血袋临床处理措施集合信息
                if (clinicalMeasuresList != null && clinicalMeasuresList.Count > 0)
                {
                    IBBloodTransItem.AddClinicalMeasuresList(ref brdv, editEntity, clinicalMeasuresList, empID, empName);
                    if (brdv.success == false)
                        throw new Exception(brdv.ErrorInfo);
                }
                //新增发血血袋临床处理结果信息
                if (clinicalResults != null)
                {
                    IBBloodTransItem.AddClinicalResults(ref brdv, editEntity, clinicalResults, empID, empName);
                    if (brdv.success == false)
                        throw new Exception(brdv.ErrorInfo);
                }
                //新增发血血袋临床处理结果描述信息
                if (clinicalResultsDesc != null)
                {
                    IBBloodTransItem.AddClinicalResultsDesc(ref brdv, editEntity, clinicalResultsDesc, empID, empName);
                    if (brdv.success == false)
                        throw new Exception(brdv.ErrorInfo);
                }
                #endregion

                //更新发血血袋的输血过程登记完成度
                EditBloodBOutItem(ref brdv, ref editEntity, transfusionAntriesList);
                if (brdv.success == false)
                    throw new Exception(brdv.ErrorInfo);
            }
            //发血血袋对应的发血总单的输血过程登记完成度处理
            EditBloodBOutForm(ref brdv, outDtlList);

            return brdv;
        }

        #endregion

        #region 公共部分
        /// <summary>
        /// 获取输血过程主单号
        /// </summary>
        /// <returns></returns>
        private string GetTransFormNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        private void GetEditEntity(ref BloodTransForm editEntity, BloodTransForm transForm)
        {
            editEntity.BBagCode = transForm.BBagCode;
            editEntity.PCode = transForm.PCode;
            editEntity.BeforeCheckID1 = transForm.BeforeCheckID1;
            editEntity.BeforeCheck1 = transForm.BeforeCheck1;
            editEntity.BeforeCheckID2 = transForm.BeforeCheckID2;

            editEntity.BeforeCheck2 = transForm.BeforeCheck2;
            editEntity.TransBeginTime = transForm.TransBeginTime;
            editEntity.TransCheckID1 = transForm.TransCheckID1;
            editEntity.TransCheck1 = transForm.TransCheck1;
            editEntity.TransCheckID2 = transForm.TransCheckID2;

            editEntity.TransCheck2 = transForm.TransCheck2;
            editEntity.TransEndTime = transForm.TransEndTime;
            editEntity.AdverseReactionsTime = transForm.AdverseReactionsTime;
            editEntity.HasAdverseReactions = transForm.HasAdverseReactions;
            editEntity.AdverseReactionsHP = transForm.AdverseReactionsHP;
            editEntity.DataUpdateTime = DateTime.Now;
        }
        private void EditBloodBOutItem(ref BaseResultDataValue brdv, ref BloodTransForm entity, IList<BloodTransItem> transfusionAntriesList)
        {
            if (entity.BloodBOutItem.DataTimeStamp == null)
            {
                entity.BloodBOutItem = IBBloodBOutItem.Get(entity.BloodBOutItem.Id);
            }
            int counts = 0;//发血血袋的全部病人体征记录项的结果判断处理
            if (transfusionAntriesList != null && transfusionAntriesList.Count > 0)
            {
                counts = transfusionAntriesList.Where(p => p.TransItemResult == null || p.TransItemResult == "").Count();
            }
            else
            {
                string hqlWhere = string.Format(" (bloodtransitem.TransItemResult is null or bloodtransitem.TransItemResult='') and bloodtransitem.ContentTypeID={0} and bloodtransitem.BloodTransForm.BloodBOutItem.Id='{1}'", ClassificationOfTransfusionContent.输血记录项.Key, entity.BloodBOutItem.Id);
                counts = IDBloodTransItemDao.GetListCountByHQL(hqlWhere);
            }

            if (counts > 0)
            {
                ZhiFang.Common.Log.Log.Info(string.Format("发血血袋明细单号为:{0},的病人体征记录项存在为空的结果,总数为:{1}!", entity.BloodBOutItem.Id, counts));
                entity.BloodBOutItem.CourseCompletion = int.Parse(CourseCompletion.已登记.Key);
            }
            else
            {
                entity.BloodBOutItem.CourseCompletion = int.Parse(CourseCompletion.登记完成.Key);
            }

            IBBloodBOutItem.Entity = entity.BloodBOutItem;
            bool result = IBBloodBOutItem.Edit();
            if (!result)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("血袋号为:{0},的输血登记完成度失败!", entity.BBagCode);
                throw new Exception(brdv.ErrorInfo);
            }
        }
        private void EditBloodBOutForm(ref BaseResultDataValue brdv, IList<BloodBOutItem> outDtlList)
        {
            //对血袋输血过程登记完成度按发血主单分组
            var groupByList = outDtlList.GroupBy(p => p.BloodBOutForm);
            foreach (var groupBy in groupByList)
            {
                BloodBOutForm outForm = groupBy.Key;
                //发血血袋的全部病人体征记录项的结果判断处理
                int counts = 0;
                //先判断发血单当前选择的发血血袋明细
                counts = outDtlList.Where(p => p.BloodBOutForm.Id == outForm.Id && p.CourseCompletion != int.Parse(CourseCompletion.登记完成.Key)).Count();
                //发血单当前未选择的发血血袋明细
                if (counts <= 0)
                {
                    StringBuilder outDtlHql = new StringBuilder();
                    outDtlHql.Append(" bloodboutitem.BloodBOutForm.Id='" + outForm.Id + "'");
                    outDtlHql.Append(" and (bloodboutitem.CourseCompletion is null or bloodboutitem.CourseCompletion!=" + CourseCompletion.登记完成.Key + ") ");
                    foreach (BloodBOutItem item in groupBy)
                    {
                        outDtlHql.Append(" and bloodboutitem.Id!='" + item.Id + "'");
                    }
                    counts = IDBloodBOutItemDao.GetListCountByHQL(outDtlHql.ToString());
                }

                if (counts > 0)
                {
                    //ZhiFang.Common.Log.Log.Info(string.Format("发血血袋单号为:{0},存在未完成输血过程记录登记的发血血袋,总数为:{1}!", outForm.Id ,counts));
                    outForm.CourseCompletion = int.Parse(CourseCompletion.已登记.Key);
                }
                else
                {
                    outForm.CourseCompletion = int.Parse(CourseCompletion.登记完成.Key);
                }

                IBBloodBOutForm.Entity = outForm;
                bool result = IBBloodBOutForm.Edit();
                if (!result)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "更新发血单号为:" + outForm.Id + "的输血登记完成度失败!";
                    throw new Exception(brdv.ErrorInfo);
                }
            }
        }
        private string[] GetEditFields()
        {
            string[] arrFields = { "TransBeginTime", "TransEndTime", "BeforeCheckID1", "BeforeCheck1", "BeforeCheckID2", "BeforeCheck2", "TransCheckID1", "TransCheck1", "TransCheck1", "TransCheckID2", "TransCheck2", "HasAdverseReactions", "AdverseReactionsTime", "AdverseReactionsHP" };
            return arrFields;
        }
        #endregion

        #region 更新输血过程
        public BaseResultDataValue EditBloodTransFormAndDtlList(BloodTransForm editEntity, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            BloodTransForm serverEntity = this.Get(editEntity.Id);
            BloodTransForm serverEntity2 = ClassMapperHelp.GetMapper<BloodTransForm, BloodTransForm>(serverEntity);
            IList<BloodBOutItem> outDtlList = new List<BloodBOutItem>();

            //更新输血过程记录主单信息
            EditBloodTransForm(ref brdv, ref serverEntity, editEntity, empID, empName);
            if (brdv.success == false)
                throw new Exception(brdv.ErrorInfo);

            //输血过程记录主单信息修改项记录
            AddSCOperation(serverEntity2, editEntity, empID, empName);

            //更新发血血袋输血记录项(病人体征信息)
            if (transfusionAntriesList != null && transfusionAntriesList.Count > 0)
            {
                IBBloodTransItem.EditTransfusionAntriesList(ref brdv, serverEntity, transfusionAntriesList, empID, empName);
                if (brdv.success == false)
                    throw new Exception(brdv.ErrorInfo);
            }
            //新增发血血袋不良反应选择项集合信息
            if (adverseReactionList != null && adverseReactionList.Count > 0)
            {
                IBBloodTransItem.AddAdverseReactionList(ref brdv, serverEntity, adverseReactionList, empID, empName);
                if (brdv.success == false)
                    throw new Exception(brdv.ErrorInfo);
            }
            //新增发血血袋临床处理措施集合信息
            if (clinicalMeasuresList != null && clinicalMeasuresList.Count > 0)
            {
                IBBloodTransItem.AddClinicalMeasuresList(ref brdv, serverEntity, clinicalMeasuresList, empID, empName);
                if (brdv.success == false)
                    throw new Exception(brdv.ErrorInfo);
            }
            //更新发血血袋临床处理结果信息
            if (clinicalResults != null)
            {
                IBBloodTransItem.EditClinicalResults(ref brdv, serverEntity, clinicalResults, empID, empName);
                if (brdv.success == false)
                    throw new Exception(brdv.ErrorInfo);
            }
            //更新发血血袋临床处理结果描述信息
            if (clinicalResultsDesc != null)
            {
                IBBloodTransItem.EditClinicalResultsDesc(ref brdv, serverEntity, clinicalResultsDesc, empID, empName);
                if (brdv.success == false)
                    throw new Exception(brdv.ErrorInfo);
            }

            //更新发血血袋的输血过程登记完成度
            EditBloodBOutItem(ref brdv, ref serverEntity, transfusionAntriesList);
            if (brdv.success == false)
                throw new Exception(brdv.ErrorInfo);

            //发血血袋对应的发血总单的输血过程登记完成度处理
            outDtlList.Add(serverEntity.BloodBOutItem);
            EditBloodBOutForm(ref brdv, outDtlList);

            return brdv;
        }
        private void EditBloodTransForm(ref BaseResultDataValue brdv, ref BloodTransForm serverEntity, BloodTransForm editEntity, long empID, string empName)
        {
            GetEditEntity(ref serverEntity, editEntity);
            this.Entity = serverEntity;
            bool result = this.Edit();
            if (result == false)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("血袋号为:{0},更新输血过程记录保存失败!", serverEntity.BBagCode);
                throw new Exception(brdv.ErrorInfo);
            }
        }
        #endregion

        #region 批量修改录入
        public BloodTransForm SearchBloodTransFormByOutDtlIdStr(string outDtlIdStr, ref int batchSign)
        {
            BloodTransForm entity = new BloodTransForm();
            if (string.IsNullOrEmpty(outDtlIdStr))
            {
                return entity;
            }

            string[] outDtlArr = outDtlIdStr.Split(',');
            StringBuilder strb = new StringBuilder();
            strb.Append(" 1=1 ");
            strb.Append(" and " + _getOutDtlIdStrHql(outDtlArr));
            ZhiFang.Common.Log.Log.Debug("SearchBloodTransFormByOutDtlIdStr.HQL:" + strb.ToString());
            IList<BloodTransForm> transFormList = this.SearchListByHQL(strb.ToString());
            if (transFormList != null && transFormList.Count > 0)
            {
                entity = transFormList.OrderBy(p => p.DataAddTime).ElementAt(0);
                //ZhiFang.Common.Log.Log.Info(string.Format("(批量修改录入)当前选择的发血血袋明细:{0},获取最早登记的输血过程记录基本信息的记录ID为:{1}!", outDtlIdStr, entity.Id));
            }
            return entity;
        }
        private string _getOutDtlIdStrHql(string[] arrStr)
        {
            if (arrStr.Length <= 0) return "";

            StringBuilder strb = new StringBuilder();
            strb.Append(" (");
            for (int i = 0; i < arrStr.Length; i++)
            {
                strb.Append(" bloodtransform.BloodBOutItem.Id ='" + arrStr[i] + "' ");
                if (i < arrStr.Length - 1) strb.Append(" or ");
            }
            strb.Append(") ");
            //ZhiFang.Common.Log.Log.Debug(strb.ToString());
            return strb.ToString();
        }
        public BaseResultDataValue EditBatchTransFormAndDtlListByOutDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm editTransForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };

            //获取待新增的发血血袋明细的输血过程记录基本信息
            IList<BloodTransForm> addTransFormList = new List<BloodTransForm>();
            //获取待更新保存的发血血袋明细的输血过程记录基本信息
            IList<BloodTransForm> editTransFormList = new List<BloodTransForm>();
            for (int i = 0; i < outDtlList.Count; i++)
            {
                string reqFormId = outDtlList[i].BloodBReqForm.Id;
                BloodBOutItem outItem = IBBloodBOutItem.Get(outDtlList[i].Id);
                if (outItem == null) continue;

                outItem.BloodBOutForm = IBBloodBOutForm.Get(outItem.BloodBOutForm.Id);
                if (string.IsNullOrEmpty(reqFormId) && !string.IsNullOrEmpty(outItem.BloodBReqForm.Id))
                    reqFormId = outItem.BloodBReqForm.Id;
                if (string.IsNullOrEmpty(reqFormId) && !string.IsNullOrEmpty(outItem.BloodBOutForm.BloodBReqForm.Id))
                    reqFormId = outItem.BloodBOutForm.BloodBReqForm.Id;
                if (!string.IsNullOrEmpty(reqFormId))
                    outItem.BloodBReqForm = IDBloodBReqFormDao.Get(reqFormId);
                outItem.Bloodstyle = IDBloodstyleDao.Get(outItem.Bloodstyle.Id);

                string tranFormHql = " bloodtransform.Visible=1 and bloodtransform.BloodBOutItem.Id ='" + outItem.Id + "' ";
                IList<BloodTransForm> transFormList = this.SearchListByHQL(tranFormHql.ToString());
                if (transFormList != null && transFormList.Count > 0)
                {
                    BloodTransForm editEntity = transFormList[0];
                    GetEditBatchEntity(ref editEntity, editTransForm, outItem, "edit");
                    editTransFormList.Add(editEntity);
                }
                else
                {
                    BloodTransForm addEntity = new BloodTransForm();
                    GetEditBatchEntity(ref addEntity, editTransForm, outItem, "add");
                    addTransFormList.Add(addEntity);
                }
            }
            //更新各发血血袋的输血过程记录基本信息
            brdv = EditBatchTransFormByOutDtlList(editTransForm, addTransFormList, editTransFormList, transfusionAntriesList, adverseReactionList, clinicalMeasuresList, clinicalResults, clinicalResultsDesc, empID, empName);
            return brdv;
        }
        public BaseResultDataValue EditBatchTransFormByOutDtlList(BloodTransForm editTransForm, IList<BloodTransForm> addTransFormList, IList<BloodTransForm> editTransFormList, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //当前选择的发血血袋的全部输血过程记录基本信息
            IList<BloodTransForm> allTransFormList = new List<BloodTransForm>();
            //获取发血血袋明细记录集合信息
            IList<BloodBOutItem> outDtlServerList = new List<BloodBOutItem>();

            #region 输血过程记录基本信息处理
            //发血血袋明细的新增输血过程记录信息
            for (int i = 0; i < addTransFormList.Count; i++)
            {
                BloodTransForm editEntity = addTransFormList[i];
                outDtlServerList.Add(editEntity.BloodBOutItem);
                this.Entity = editEntity;
                bool result = this.Add();
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},新增输血过程记录保存失败!", editEntity.BBagCode);
                    return brdv;
                }
                if (editEntity.DataTimeStamp == null)
                {
                    editEntity.DataTimeStamp = editEntity.Bloodstyle.DataTimeStamp;
                }
                allTransFormList.Add(editEntity);
            }
            //发血血袋明细的编辑更新输血过程记录信息
            for (int i = 0; i < editTransFormList.Count; i++)
            {
                BloodTransForm serverEntity = editTransFormList[i];
                outDtlServerList.Add(serverEntity.BloodBOutItem);
                this.Entity = serverEntity;
                bool result = this.Edit();
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("血袋号为:{0},更新输血过程记录保存失败!", serverEntity.BBagCode);
                    return brdv;
                }
                //输血过程记录主单信息修改项记录
                AddSCOperation(serverEntity, editTransForm, empID, empName);
                allTransFormList.Add(serverEntity);
            }
            #endregion

            #region 各输血过程记录明细处理
            for (int i = 0; i < allTransFormList.Count; i++)
            {
                BloodTransForm editEntity = allTransFormList[i];
                //更新各发血血袋的输血过程记录项的病人体征信息
                brdv = IBBloodTransItem.EditBatchTransfusionAntriesByOutDtlList(editEntity, transfusionAntriesList, empID, empName);

                //更新各发血血袋的输血过程记录项的不良反应症状信息
                brdv = IBBloodTransItem.EditBatchAdverseReactionByOutDtlList(editEntity, adverseReactionList, empID, empName);

                //更新各发血血袋的输血过程记录项的临床处理措施信息
                brdv = IBBloodTransItem.EditBatchClinicalMeasuresByOutDtlList(editEntity, clinicalMeasuresList, empID, empName);

                //更新各发血血袋的输血过程记录项的临床处理结果信息
                brdv = IBBloodTransItem.EditBatchClinicalResultsByOutDtlListr(editEntity, clinicalResults, empID, empName);

                //更新各发血血袋的输血过程记录项的临床处理结果描述信息
                brdv = IBBloodTransItem.EditBatchClinicalResultsDescByOutDtlList(editEntity, clinicalResultsDesc, empID, empName);

                //更新发血血袋的输血过程登记完成度
                EditBloodBOutItem(ref brdv, ref editEntity, transfusionAntriesList);
                if (brdv.success == false)
                    throw new Exception(brdv.ErrorInfo);

                ////发血血袋对应的发血总单的输血过程登记完成度处理
                EditBloodBOutForm(ref brdv, outDtlServerList);
            }
            #endregion

            return brdv;
        }
        private void GetEditBatchEntity(ref BloodTransForm editEntity, BloodTransForm curTransForm, BloodBOutItem outItem, string editType)
        {
            editEntity.BloodBOutItem = outItem;
            editEntity.BloodBOutForm = outItem.BloodBOutForm;
            editEntity.BloodBReqForm = outItem.BloodBReqForm;
            editEntity.Bloodstyle = outItem.Bloodstyle;
            if (string.IsNullOrEmpty(editEntity.BBagCode))
                editEntity.BBagCode = outItem.BBagCode;
            if (string.IsNullOrEmpty(editEntity.PCode))
                editEntity.PCode = outItem.Pcode;
            if (editType == "add")
            {
                editEntity.Visible = true;
                editEntity.DispOrder = outItem.DispOrder;
            }
            if (editType == "edit" && editEntity.HasAdverseReactions != curTransForm.HasAdverseReactions)
            {
                editEntity.HasAdverseReactions = curTransForm.HasAdverseReactions;
            }
            if (string.IsNullOrEmpty(editEntity.TransFormNo))
                editEntity.TransFormNo = GetTransFormNo();
            editEntity.DataUpdateTime = DateTime.Now;
            if (string.IsNullOrEmpty(editEntity.BBagCode) && !string.IsNullOrEmpty(curTransForm.BBagCode))
                editEntity.BBagCode = outItem.BBagCode;
            if (string.IsNullOrEmpty(editEntity.PCode) && !string.IsNullOrEmpty(curTransForm.PCode))
                editEntity.PCode = outItem.Pcode;

            if (!editEntity.BeforeCheckID1.HasValue && curTransForm.BeforeCheckID1.HasValue)
            {
                editEntity.BeforeCheckID1 = curTransForm.BeforeCheckID1;
                editEntity.BeforeCheck1 = curTransForm.BeforeCheck1;
            }
            if (!editEntity.BeforeCheckID2.HasValue && curTransForm.BeforeCheckID2.HasValue)
            {
                editEntity.BeforeCheckID2 = curTransForm.BeforeCheckID2;
                editEntity.BeforeCheck2 = curTransForm.BeforeCheck2;
            }

            if (!editEntity.TransCheckID1.HasValue && curTransForm.TransCheckID1.HasValue)
            {
                editEntity.TransCheckID1 = curTransForm.TransCheckID1;
                editEntity.TransCheck1 = curTransForm.TransCheck1;
            }
            if (!editEntity.TransCheckID2.HasValue && curTransForm.TransCheckID2.HasValue)
            {
                editEntity.TransCheckID2 = curTransForm.TransCheckID2;
                editEntity.TransCheck2 = curTransForm.TransCheck2;
            }
            if (!editEntity.TransBeginTime.HasValue && curTransForm.TransBeginTime.HasValue)
                editEntity.TransBeginTime = curTransForm.TransBeginTime;
            if (!editEntity.TransEndTime.HasValue && curTransForm.TransEndTime.HasValue)
                editEntity.TransEndTime = curTransForm.TransEndTime;
            if (!editEntity.AdverseReactionsTime.HasValue && curTransForm.AdverseReactionsTime.HasValue)
                editEntity.AdverseReactionsTime = curTransForm.AdverseReactionsTime;
            if (!editEntity.AdverseReactionsHP.HasValue && curTransForm.AdverseReactionsHP.HasValue)
                editEntity.AdverseReactionsHP = curTransForm.AdverseReactionsHP;

        }
        #endregion

        #region 修改信息记录
        public void AddSCOperation(BloodTransForm serverEntity, BloodTransForm editEntity, long empID, string empName)
        {
            string[] arrFields = GetEditFields();
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemo<BloodTransForm>(serverEntity, editEntity, editEntity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = serverEntity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "EditBloodTransForm";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.输血过程记录基本信息.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                sco.DataUpdateTime = DateTime.Now;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        private void EditGetUpdateMemo<T>(T serverEntity, T updateEntity, Type type, string[] arrFields, ref StringBuilder strbMemo)
        {
            //为空判断
            if (serverEntity == null && updateEntity == null)
                return;
            else if (serverEntity == null || updateEntity == null)
                return;

            Type t = type;
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            foreach (var po in props)
            {
                if (po.Name == "Id" || po.Name == "LabID" || po.Name == "DataAddTime" || po.Name == "DataUpdateTime" || po.Name == "DataTimeStamp")
                    continue;

                if (arrFields.Contains(po.Name) == true && IsCanCompare(po.PropertyType))
                {
                    object serverValue = po.GetValue(serverEntity, null);
                    object updateValue = po.GetValue(updateEntity, null);
                    if (serverValue == null)
                        serverValue = "";
                    if (updateValue == null)
                        updateValue = "";
                    if (!serverValue.Equals(updateValue))
                    {
                        string cname = po.Name;
                        foreach (var pattribute in po.GetCustomAttributes(false))
                        {
                            if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                            {
                                cname = ((DataDescAttribute)pattribute).CName;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(serverValue.ToString()))
                            serverValue = "空";
                        if (string.IsNullOrEmpty(updateValue.ToString()))
                            updateValue = "空";
                        //if (!updateValue.Equals(serverValue))
                        strbMemo.Append("【" + cname + "】由原来:" + serverValue.ToString() + ",修改为:" + updateValue.ToString() + ";" + System.Environment.NewLine);
                    }
                }
            }
        }
        /// <summary>
        /// 该类型是否可直接进行值的比较
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsCanCompare(Type t)
        {
            if (t.IsValueType)
            {
                return true;
            }
            else
            {
                //String是特殊的引用类型，它可以直接进行值的比较
                if (t.FullName == typeof(String).FullName)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

    }
}