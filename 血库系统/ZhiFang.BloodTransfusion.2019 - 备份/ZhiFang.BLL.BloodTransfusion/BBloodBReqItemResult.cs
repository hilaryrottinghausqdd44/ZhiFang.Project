
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
    public class BBloodBReqItemResult : BaseBLL<BloodBReqItemResult>, ZhiFang.IBLL.BloodTransfusion.IBBloodBReqItemResult
    {
        IDBloodBTestItemDao IDBloodBTestItemDao { get; set; }
        IDVBloodLisResultDao IDVBloodLisResultDao { get; set; }

        public IList<BloodBReqItemResult> SearchBloodBReqItemResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            IList<BloodBReqItemResult> entityList = new List<BloodBReqItemResult>();
            entityList = ((IDBloodBReqItemResultDao)base.DBDao).SearchBloodBReqItemResultListByJoinHql(where, bloodbtestitemHql, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBReqItemResult> SearchBloodBReqItemResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit)
        {
            EntityList<BloodBReqItemResult> entityList = new EntityList<BloodBReqItemResult>();
            entityList = ((IDBloodBReqItemResultDao)base.DBDao).SearchBloodBReqItemResultEntityListByJoinHql(where, bloodbtestitemHql, sort, page, limit);
            return entityList;
        }
        public EntityList<BloodBReqItemResult> GetBloodBReqItemResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, string sort, int page, int limit)
        {
            EntityList<BloodBReqItemResult> entityList = new EntityList<BloodBReqItemResult>();
            entityList.list = new List<BloodBReqItemResult>();

            //从医嘱申请的检验结果表里获取LIS结果信息
            IList<BloodBReqItemResult> lisResulList = new List<BloodBReqItemResult>();
            if (!string.IsNullOrEmpty(reqFormId))
            {
                lisResulList = this.SearchListByHQL(reqresulthql);
                entityList.list = lisResulList;
            }

            //从LIS检验结果视图里获取LIS结果信息
            if (!string.IsNullOrEmpty(vlisresultHql))
            {
                //获取在用并且为医嘱结果录入项的血库检验项目集合
                string reqEditItemHql = "bloodbtestitem.Visible=1 and bloodbtestitem.IsResultItem=1";
                IList<BloodBTestItem> tempList2 = IDBloodBTestItemDao.GetListByHQL(reqEditItemHql);
                if (tempList2 == null || tempList2.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("获取医嘱申请的检验结果对应的血库检验项目信息为空!请维护好血库系统的检验项目信息后再获取!");
                    return entityList;
                }
                StringBuilder testItemIdStr = new StringBuilder();
                foreach (BloodBTestItem item in tempList2)
                {
                    testItemIdStr.Append(item.Id + ",");
                }
                vlisresultHql = vlisresultHql + " and vbloodlisresult.ItemNo in (" + testItemIdStr.ToString().TrimEnd(',') + ")";
                //LIS同库
                //IList<VBloodLisResult> vlisResulList = IDVBloodLisResultDao.GetListByHQL(vlisresultHql);
                //LIS不同库
                IList<VBloodLisResult> vlisResulList = DataAccess_SQL.CreateVBloodLisResultDao_SQL().SelectListByHQL(vlisresultHql);
                foreach (VBloodLisResult ventity in vlisResulList)
                {
                    //需要判断检验结果是否已经存在医嘱申请的检验结果里(条码+检验项目编码)
                    if (lisResulList != null && lisResulList.Count > 0)
                    {
                        var tempList3 = lisResulList.Where(p => p.Barcode == ventity.BarCode && p.BTestItemNo == ventity.ItemNo);
                        if (tempList3 != null && tempList3.Count() > 0)
                        {
                            //entityList.list.Add(tempList3.ElementAt(0));
                            continue;
                        }
                    }
                    BloodBReqItemResult entity = GetBloodBReqItemResultOfVLis(reqFormId, ventity);
                    entityList.list.Add(entity);
                }
            }
            entityList.count = entityList.list.Count;
            return entityList;
        }
        private BloodBReqItemResult GetBloodBReqItemResultOfVLis(string reqFormId, VBloodLisResult ventity)
        {
            BloodBReqItemResult entity = new BloodBReqItemResult();

            entity.BReqFormID = reqFormId;
            entity.Barcode = ventity.BarCode;
            entity.BTestItemNo = ventity.ItemNo;
            entity.BTestItemCName = ventity.ItemName;
            entity.PatNo = ventity.PatNo;
            entity.ItemResult = ventity.ItemResult;
            entity.ItemUnit = ventity.ItemUnit;

            if (!string.IsNullOrEmpty(ventity.CheckDateTime))
                entity.BTestTime = DateTime.Parse(ventity.CheckDateTime);
            entity.PatID = "";
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
            return entity;
        }
        public BaseResultDataValue AddBReqItemResultOfReqForm(BloodBReqForm reqForm)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;
            string reqresulthql = "bloodbreqitemresult.BReqFormID='" + reqForm.Id + "'";
            //先删除该医嘱申请对应的LIS检验结果信息,再重新获取
            this.DeleteByHql("From BloodBReqItemResult bloodbreqitemresult where bloodbreqitemresult.BReqFormID='" + reqForm.Id + "'");

            //默认取7天范围的检验结果,可以考虑用运行参数替换
            string sdate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            //测试日期
            //sdate = "2019 - 05-10 00:00:00";
            string edate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string vlisresultHql = string.Format("(vbloodlisresult.PatNo = '{0}' and vbloodlisresult.PatName = '{1}' and vbloodlisresult.Itemtesttime >= '{2}' and vbloodlisresult.Itemtesttime <= '{3}')", reqForm.PatNo, reqForm.CName, sdate, edate);
            EntityList<BloodBReqItemResult> tempEntityList = GetBloodBReqItemResultListByVLisResultHql("", vlisresultHql, "", "", -1, -1);
            if (tempEntityList == null || tempEntityList.count <= 0) return brdv;

            brdv = AddBReqItemResultList(reqForm, tempEntityList.list);

            return brdv;
        }
        public BaseResultDataValue AddBReqItemResultList(BloodBReqForm reqForm, IList<BloodBReqItemResult> addResultList)
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
            var groupByList = addResultList.GroupBy(p => p.BTestItemNo);//.OrderByDescending(p=>p.);
            foreach (var groupBy in groupByList)
            {
                BloodBReqItemResult entity = groupBy.OrderByDescending(p => p.BTestTime).ElementAt(0);
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "新增医嘱申请检验结果失败!";
                    break;
                }

                entity.BReqFormID = reqForm.Id;
                entity.DispOrder = bloodOrder;
                entity.PatNo = reqForm.PatNo;
                entity.PatID = reqForm.PatID;
                entity.Visible = true;
                if (reqForm.ReqTime.HasValue)
                    entity.BReqTime = reqForm.ReqTime;
                this.Entity = entity;
                brdv.success = this.Add();
                bloodOrder += 1;
            }

            return brdv;
        }
    }
}