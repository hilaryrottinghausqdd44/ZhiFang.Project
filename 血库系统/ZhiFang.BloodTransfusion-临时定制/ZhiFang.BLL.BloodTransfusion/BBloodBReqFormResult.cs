
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodBReqFormResult : BaseBLL<BloodBReqFormResult>, ZhiFang.IBLL.BloodTransfusion.IBBloodBReqFormResult
    {
        IDBloodBTestItemDao IDBloodBTestItemDao { get; set; }
        IDVBloodLisResultDao IDVBloodLisResultDao { get; set; }
        IDBloodBUnitDao IDBloodBUnitDao { get; set; }

        public IList<BloodBReqFormResult> SearchBloodBReqFormResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            IList<BloodBReqFormResult> entityList = new List<BloodBReqFormResult>();
            entityList = ((IDBloodBReqFormResultDao)base.DBDao).SearchBloodBReqFormResultListByJoinHql(where, bloodbtestitemHql, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBReqFormResult> SearchBloodBReqFormResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            EntityList<BloodBReqFormResult> entityList = new EntityList<BloodBReqFormResult>();
            entityList = ((IDBloodBReqFormResultDao)base.DBDao).SearchBloodBReqFormResultEntityListByJoinHql(where, bloodbtestitemHql, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBReqFormResult> GetBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, string sort, int page, int limit)
        {
            EntityList<BloodBReqFormResult> entityList = _getBloodBReqFormResultListByVLisResultHql(reqFormId, vlisresultHql, reqresulthql, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBReqFormResult> SelectBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, string sort, int page, int limit)
        {
            EntityList<BloodBReqFormResult> entityList = _getBloodBReqFormResultListByVLisResultHql(reqFormId, vlisresultHql, reqresulthql, sort, page, limit);
            return entityList;
        }

        private EntityList<BloodBReqFormResult> _getBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, string sort, int page, int limit)
        {
            EntityList<BloodBReqFormResult> entityList = new EntityList<BloodBReqFormResult>();
            entityList.list = new List<BloodBReqFormResult>();

            //从医嘱申请的检验结果表里获取LIS结果信息
            IList<BloodBReqFormResult> lisResulList = new List<BloodBReqFormResult>();
            if (!string.IsNullOrEmpty(reqFormId))
            {
                lisResulList = this.SearchListByHQL(reqresulthql);
                entityList.list = lisResulList;
            }
            IList<BloodBTestItem> tempBTestItemList = new List<BloodBTestItem>();
            //从LIS检验结果视图里获取LIS结果信息
            if (!string.IsNullOrEmpty(vlisresultHql))
            {
                //获取在用并且为医嘱结果录入项的血库检验项目集合
                string reqEditItemHql = "bloodbtestitem.Visible=1 and bloodbtestitem.IsResultItem=1";
                tempBTestItemList = IDBloodBTestItemDao.GetListByHQL(reqEditItemHql);
                if (tempBTestItemList == null || tempBTestItemList.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("获取医嘱申请的检验结果对应的血库检验项目信息为空!请维护好血库系统的检验项目信息后再获取!");
                    return entityList;
                }
                StringBuilder testItemIdStr = new StringBuilder();
                //foreach (BloodBTestItem item in tempBTestItemList)
                //{
                //    testItemIdStr.Append(item.Id + ",");
                //}
                IList<BloodBUnit> bloodBUnitList = IDBloodBUnitDao.LoadAll();
                for (int i = 0; i < tempBTestItemList.Count - 1; i++)
                {
                    var item = tempBTestItemList[i];
                    testItemIdStr.Append(item.Id + ",");
                    //获取项目的单位
                    if (!string.IsNullOrEmpty(item.BUnitNo))
                    {
                        var tempList1 = bloodBUnitList.Where(p => p.Id.ToString() == item.BUnitNo);
                        if (tempList1 != null && tempList1.Count() > 0)
                        {
                            tempBTestItemList[i].BloodBUnit = tempList1.ElementAt(0);
                        }
                    }
                }
                vlisresultHql = vlisresultHql + " and vbloodlisresult.ItemNo in (" + testItemIdStr.ToString().TrimEnd(',') + ")";
                ZhiFang.Common.Log.Log.Info("获取医嘱申请的检验结果条件为:" + vlisresultHql);
                //LIS同库
                //IList<VBloodLisResult> vlisResulList = IDVBloodLisResultDao.GetListByHQL(vlisresultHql);
                //LIS不同库 
                IList<VBloodLisResult> vlisResulList = DataAccess_SQL.CreateVBloodLisResultDao_SQL().SelectListByHQL(vlisresultHql);
                #region 检验结果按项目分组并按检验时间倒序取第一个
                var groupByList = vlisResulList.GroupBy(p => p.ItemNo);//.OrderByDescending(p=>p.);
                foreach (var groupBy in groupByList)
                {
                    VBloodLisResult ventity = groupBy.OrderByDescending(p => p.CheckDateTime).ElementAt(0);
                    //需要判断检验结果是否已经存在医嘱申请的检验结果里(条码+检验项目编码)
                    if (!string.IsNullOrEmpty(reqFormId) && entityList.list.Count > 0)
                    {
                        for (int i = 0; i < entityList.list.Count; i++)
                        {
                            var entity1 = entityList.list[i];
                            if (!entity1.BTestTime.HasValue && entity1.BTestItemNo == ventity.ItemNo)
                            {
                                entity1.Barcode = ventity.BarCode;
                                entity1.BTestItemNo = ventity.ItemNo;
                                entity1.BTestItemCName = ventity.ItemName;
                                entity1.BTestItemEName = ventity.EName;
                                if (string.IsNullOrEmpty(entity1.BTestItemEName))
                                {
                                    entity1.BTestItemEName = ventity.ItemName;
                                }
                                entity1.ItemResult = ventity.ItemResult;
                                entity1.ItemLisResult = ventity.ItemResult;
                                entity1.ItemUnit = ventity.ItemUnit;

                                if (!string.IsNullOrEmpty(ventity.CheckDateTime))
                                    entity1.BTestTime = DateTime.Parse(ventity.CheckDateTime);
                                entityList.list[i] = entity1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        BloodBReqFormResult entity = GetBloodBReqFormResultOfVLis(reqFormId, ventity);
                        var tempList12 = tempBTestItemList.Where(p => p.Id.ToString() == entity.BTestItemNo);
                        if (tempList12 != null && tempList12.Count() > 0)
                        {
                            var tempBTestItem = tempList12.ElementAt(0);
                            entity.BTestItemCName = tempBTestItem.CName;
                            entity.IsPreTrransfusionEvaluationItem = tempBTestItem.IsPreTrransfusionEvaluationItem;
                            //ZhiFang.Common.Log.Log.Debug("1.项目编码为:" + entity.BTestItemNo + "是否为输血前评估项:" + entity.IsPreTrransfusionEvaluationItem);
                            if (tempBTestItem.BloodBUnit != null)
                            {
                                entity.ItemUnit = tempBTestItem.BloodBUnit.BUnitName;
                            }
                        }
                        entityList.list.Add(entity);
                    }

                    //删除tempBTestItemList对应的检验项目
                    var tempList2 = tempBTestItemList.Where(p => p.Id.ToString() == ventity.ItemNo);
                    if (tempList2 != null && tempList2.Count() > 0)
                        tempBTestItemList.Remove(tempList2.ElementAt(0));
                }
                #endregion
            }
            else
            {
                ZhiFang.Common.Log.Log.Error("获取医嘱申请的检验结果条件(vlisresultHql)为空!");
            }

            //新增医嘱申请时,还没有检验结果的医嘱结果录入项处理
            if (string.IsNullOrEmpty(reqFormId) && tempBTestItemList.Count > 0)
            {
                foreach (BloodBTestItem testItem in tempBTestItemList)
                {
                    BloodBReqFormResult entity = GetBloodBReqFormResultOfBTestItem(reqFormId, testItem);
                    //ZhiFang.Common.Log.Log.Debug("2.项目编码为:" + entity.BTestItemNo + "是否为输血前评估项:" + entity.IsPreTrransfusionEvaluationItem);
                    if (testItem.BloodBUnit != null && string.IsNullOrEmpty(entity.ItemUnit))
                    {
                        entity.ItemUnit = testItem.BloodBUnit.BUnitName;
                    }
                    entityList.list.Add(entity);
                }
            }
            else if (entityList.list != null && entityList.list.Count > 0)
            {
                for (int i = 0; i < entityList.list.Count; i++)
                {
                    if (string.IsNullOrEmpty(entityList.list[i].BTestItemCName))
                    {
                        var tempList2 = tempBTestItemList.Where(p => p.Id.ToString() == entityList.list[i].BTestItemNo);
                        if (tempList2 != null && tempList2.Count() > 0)
                        {
                            entityList.list[i].BTestItemCName = tempList2.ElementAt(0).CName;
                            entityList.list[i].IsPreTrransfusionEvaluationItem = tempList2.ElementAt(0).IsPreTrransfusionEvaluationItem;
                        }
                    }
                }
            }
            //按是否为输血前评估项+显示次序+检验项目+检测时间排序
            if (entityList.list.Count > 0) entityList.list = entityList.list.OrderBy(s => s.IsPreTrransfusionEvaluationItem).ThenBy(s => s.DispOrder).ThenBy(s => s.BTestItemNo).ThenBy(s => s.BTestTime).ToList();
            entityList.count = entityList.list.Count;
            return entityList;
        }
        private BloodBReqFormResult GetBloodBReqFormResultOfBTestItem(string reqFormId, BloodBTestItem testItem)
        {
            BloodBReqFormResult entity = new BloodBReqFormResult();
            entity.BReqFormID = reqFormId;
            entity.BTestItemNo = testItem.Id.ToString();
            entity.BTestItemCName = testItem.CName;
            entity.BTestItemEName = testItem.EName;
            entity.IsPreTrransfusionEvaluationItem = testItem.IsPreTrransfusionEvaluationItem;
            if (string.IsNullOrEmpty(entity.BTestItemEName))
            {
                entity.BTestItemEName = testItem.CName;
            }
            if (testItem.BloodBUnit != null)
            {
                entity.ItemUnit = testItem.BloodBUnit.BUnitName;
            }
            entity.Barcode = "";
            entity.ItemResult = "";
            entity.ItemLisResult = "";
            entity.BTestTime = null;
            entity.Visible = true;
            entity.DispOrder = testItem.DispOrder;
            if (string.IsNullOrEmpty(reqFormId)) entity.Id = 0;
            return entity;
        }
        private BloodBReqFormResult GetBloodBReqFormResultOfVLis(string reqFormId, VBloodLisResult ventity)
        {
            BloodBReqFormResult entity = new BloodBReqFormResult();

            entity.BReqFormID = reqFormId;
            entity.Barcode = ventity.BarCode;
            entity.BTestItemNo = ventity.ItemNo;
            entity.BTestItemCName = ventity.ItemName;
            entity.BTestItemEName = ventity.EName;
            if (string.IsNullOrEmpty(entity.BTestItemEName))
            {
                entity.BTestItemEName = ventity.ItemName;
            }
            entity.ItemResult = ventity.ItemResult;
            entity.ItemLisResult = ventity.ItemResult;
            entity.ItemUnit = ventity.ItemUnit;

            if (!string.IsNullOrEmpty(ventity.CheckDateTime))
                entity.BTestTime = DateTime.Parse(ventity.CheckDateTime);
            entity.Visible = true;
            entity.DispOrder = 0;

            //如果医嘱申请信息已存在,Lis检验结果还没保存到医嘱申请对应的LIS检验结果里,直接保存
            if (!string.IsNullOrEmpty(reqFormId))
            {
                this.Entity = entity;
                bool result = this.Add();
                if (result == false)
                {
                    ZhiFang.Common.Log.Log.Error("新增保存医嘱申请单号为:" + reqFormId + ",条码号为:" + ventity.BarCode + ",项目编码为:" + ventity.ItemNo + "的检验结果失败!");
                }
            }
            else
            {
                entity.Id = 0;
            }
            return entity;
        }

        public BaseResultDataValue AddBReqFormResultList(BloodBReqForm reqForm, IList<BloodBReqFormResult> addResultList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //保存验证
            if (addResultList == null || addResultList.Count <= 0)
            {
                brdv.success = true;
                //brdv.ErrorInfo = "传入参数addResultList为空!";
                return brdv;
            }
            int bloodOrder = 1;
            foreach (BloodBReqFormResult entity in addResultList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "新增医嘱申请检验结果失败!";
                    break;
                }
                //先删除旧的LIS检验结果
                this.DeleteByHql("From BloodBReqFormResult bloodbreqformresult where bloodbreqformresult.BReqFormID='" + reqForm.Id + "' and bloodbreqformresult.BTestItemNo='" + entity.BTestItemNo + "'");

                entity.BReqFormID = reqForm.Id;
                if (entity.DispOrder <= 0)
                    entity.DispOrder = bloodOrder;
                entity.Visible = true;

                this.Entity = entity;
                brdv.success = this.Add();
                bloodOrder += 1;
            }
            return brdv;
        }
        public BaseResultDataValue EditBReqFormResultList(BloodBReqForm reqForm, IList<BloodBReqFormResult> editResultList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            foreach (BloodBReqFormResult entity in editResultList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "更新医嘱结果明细失败!";
                    break;
                }
                List<string> tmpa = new List<string>();
                tmpa.Add("Id=" + entity.Id + " ");
                tmpa.Add("BReqFormID='" + reqForm.Id + "' ");
                if (entity.BTestTime.HasValue)
                    tmpa.Add("BTestTime='" + entity.BTestTime.Value + "' ");
                tmpa.Add("ItemResult='" + entity.ItemResult + "' ");
                tmpa.Add("ItemLisResult='" + entity.ItemLisResult + "' ");
                this.Entity = entity;
                brdv.success = this.Update(tmpa.ToArray());
            }

            return brdv;
        }
    }
}