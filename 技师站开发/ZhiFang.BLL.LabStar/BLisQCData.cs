using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisQCData : BaseBLL<LisQCData>, ZhiFang.IBLL.LabStar.IBLisQCData
    {
        IDLBQCItemTimeDao IDLBQCItemTimeDao { get; set; }
        IDLBQCItemDao IDLBQCItemDao { get; set; }
        IDLBQCItemRuleDao IDLBQCItemRuleDao { get; set; }
        IDLBQCRuleDao IDLBQCRuleDao { get; set; }
        IDLBQCRulesConDao IDLBQCRulesConDao { get; set; }
        IDLBQCRuleBaseDao IDLBQCRuleBaseDao { get; set; }
        IDLBQCMaterialDao IDLBQCMaterialDao { get; set; }
        IDLBEquipDao IDLBEquipDao { get; set; }
        public BaseResultDataValue GetCalcTargetByQCData(long qcItemID, string beginDate, string endDate)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("开始计算靶值！" + "QCItemID：" + qcItemID + "，日期范围：" + beginDate + "  " + endDate);
            if (string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
            {
                brdv.success = false;
                brdv.ErrorInfo = "开始日期和结束日期不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo + "QCItemID：" + qcItemID + "，日期范围：" + beginDate + "  " + endDate);
                return brdv;
            }
            endDate = (DateTime.Parse(endDate).AddDays(1)).ToString("yyyy-MM-dd");
            IList<LisQCData> listLisQCData = this.SearchListByHQL(" lisqcdata.LBQCItem.Id=" + qcItemID.ToString() +
                " and lisqcdata.BUse=1 and lisqcdata.ReceiveTime>=\'" + beginDate + "\'" +
                " and lisqcdata.ReceiveTime<\'" + endDate + "\'");
            if (listLisQCData == null || listLisQCData.Count == 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "质控数据为空，无法计算靶值！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo + "QCItemID：" + qcItemID + "，日期范围：" + beginDate + "  " + endDate);
                return brdv;
            }
            brdv = GetCalcTarget(listLisQCData);
            ZhiFang.LabStar.Common.LogHelp.Info("结束计算靶值！");
            return brdv;
        }

        public BaseResultDataValue GetCalcSDByQCData(long qcItemID, string beginDate, string endDate)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("开始计算标准差！" + "QCItemID：" + qcItemID + "，日期范围：" + beginDate + "  " + endDate);
            if (string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
            {
                brdv.success = false;
                brdv.ErrorInfo = "开始日期和结束日期不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo + "QCItemID：" + qcItemID + "，日期范围：" + beginDate + "  " + endDate);
                return brdv;
            }
            endDate = (DateTime.Parse(endDate).AddDays(1)).ToString("yyyy-MM-dd");
            IList<LisQCData> listLisQCData = this.SearchListByHQL(" lisqcdata.LBQCItem.Id=" + qcItemID.ToString() +
                " and lisqcdata.BUse=1 and lisqcdata.ReceiveTime>=\'" + beginDate + "\'" +
                " and lisqcdata.ReceiveTime<\'" + endDate + "\'");
            //质控数据至少为三个
            if (listLisQCData == null || listLisQCData.Count == 0 || listLisQCData.Count < 3)
            {
                brdv.success = false;
                brdv.ErrorInfo = "质控数据个数少于3个，无法计算标准差！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo + "QCItemID：" + qcItemID + "，日期范围：" + beginDate + "  " + endDate);
                return brdv;
            }
            brdv = GetCalcSD(listLisQCData);
            ZhiFang.LabStar.Common.LogHelp.Info("结束计算标准差！");
            return brdv;
        }

        public BaseResultDataValue GetCalcTargetSDByQCData(string listQCItemID, string beginDate, string endDate)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ZhiFang.LabStar.Common.LogHelp.Info("开始计算靶值标准差！" + "QCItemID：" + listQCItemID + "，日期范围：" + beginDate + "  " + endDate);
            if (string.IsNullOrEmpty(listQCItemID) || string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
            {
                brdv.success = false;
                brdv.ErrorInfo = "质控项目ID、开始日期和结束日期不能为空！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo + "QCItemID：" + listQCItemID + "，日期范围：" + beginDate + "  " + endDate);
                return brdv;
            }
            endDate = (DateTime.Parse(endDate).AddDays(1)).ToString("yyyy-MM-dd");
            string dateHQL = " and lisqcdata.ReceiveTime>=\'" + beginDate + "\'" + " and lisqcdata.ReceiveTime<\'" + endDate + "\'";
            string[] listID = listQCItemID.Split(',');
            int calcCount = 0;
            StringBuilder sbResult = new StringBuilder();
            foreach (string qcItemID in listID)
            {
                IList<LisQCData> listLisQCData = this.SearchListByHQL(" lisqcdata.LBQCItem.Id=" + qcItemID + " and lisqcdata.BUse=1 " + dateHQL);
                //质控数据至少为三个
                if (listLisQCData == null || listLisQCData.Count == 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Info("质控数据为空，无法计算靶值和标准差！QCItemID：" + qcItemID + "，日期范围：" + beginDate + "  " + endDate);
                    continue;
                }
                BaseResultDataValue tempBaseResult = GetCalcTargetSD(listLisQCData);
                if (tempBaseResult.success)
                {
                    sbResult.Append(",{\"QCItemID\":\"" + qcItemID + "\"," + tempBaseResult.ResultDataValue + "}");
                    calcCount++;
                }
            }
            brdv.ResultDataValue = "{\"count\":\"" + calcCount + "\",\"list\":[" + sbResult.ToString().Remove(0, 1) + "]}";
            ZhiFang.LabStar.Common.LogHelp.Info("结束计算靶值标准差！");
            return brdv;
        }

        #region 靶值计算等函数

        /// <summary>
        /// 根据质控数据列表，计算靶值
        /// </summary>
        /// <param name="listLisQCData">质控数据列表</param>
        /// <returns></returns>
        private BaseResultDataValue GetCalcTarget(IList<LisQCData> listLisQCData)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            double target = listLisQCData.Average(p => p.QuanValue);
            brdv.ResultDataValue = target.ToString();
            return brdv;
        }

        /// <summary>
        /// 根据质控数据列表，计算标准差
        /// </summary>
        /// <param name="qcdValueList">质控数据列表</param>
        /// <returns></returns>
        private BaseResultDataValue GetCalcSD(IList<LisQCData> listLisQCData)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //数据个数
            int dataCount = listLisQCData.Count;
            if (dataCount < 3)
            {
                brdv.success = false;
                brdv.ErrorInfo = "质控数据个数少于3个，无法计算标准差！";
                ZhiFang.LabStar.Common.LogHelp.Info(brdv.ErrorInfo);
                return brdv;
            }
            //平均值
            double valueAvg = listLisQCData.Average(p => p.QuanValue);
            //求和
            double valueSum = listLisQCData.Sum(p => (p.QuanValue - valueAvg) * (p.QuanValue - valueAvg));
            //取平方根
            double SD = Math.Sqrt(valueSum / (dataCount - 1));
            brdv.ResultDataValue = SD.ToString();
            return brdv;
        }

        /// <summary>
        /// 根据质控数据列表，计算靶值标准差
        /// </summary>
        /// <param name="qcdValueList">质控数据列表</param>
        /// <returns></returns>
        private BaseResultDataValue GetCalcTargetSD(IList<LisQCData> listLisQCData)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //数据个数
            int dataCount = listLisQCData.Count;
            //平均值
            double valueAvg = listLisQCData.Average(p => p.QuanValue);
            string SD = "";
            if (listLisQCData.Count >= 3)
            {
                //求和
                double valueSum = listLisQCData.Sum(p => (p.QuanValue - valueAvg) * (p.QuanValue - valueAvg));
                //取平方根
                SD = Math.Sqrt(valueSum / (dataCount - 1)).ToString();
            }
            else
                ZhiFang.LabStar.Common.LogHelp.Info("质控数据个数少于3个，无法计算标准差！QCItemID：" + listLisQCData[0].LBQCItem.Id);
            brdv.ResultDataValue = "\"Target\":\"" + valueAvg.ToString() + "\",\"SD\":\"" + SD + "\"";
            return brdv;
        }
        /// <summary>
        /// 月质控数据查询
        /// </summary>
        /// <param name="QCItemID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public List<LisQCDataMonthVO> getQCMothsData(long QCItemID, bool buse, DateTime startDate, DateTime endDate)
        {
            //查询质控项目数据
            string where = "LBQCItem.Id=" + QCItemID + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' ";
            if (!buse)
            {
                where += " and BUse=1";
            }
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(where);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.getQCMothsData 质控项目数据:where = " + where + " count= " + lisQCDatas.Count);
            //查询 质控项目时效
            string qcItemTimeWhere = "LBQCItem.Id=" + QCItemID + " and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qcItemTimeWhere);
            ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.getQCMothsData 质控项目时效:where = " + qcItemTimeWhere + " count = " + lBQCItemTimes.Count);
            //设置传入时间段内的时间
            List<LisQCDataMonthVO> lisQCDataVOs = new List<LisQCDataMonthVO>();
            for (; startDate < endDate.AddDays(1); startDate = startDate.AddDays(1))
            {
                LisQCDataMonthVO lisQCDataVO = new LisQCDataMonthVO() { ReceiveTime = startDate };
                //如果质控项目时效有匹配的数据加入
                var lBQCItemTime = lBQCItemTimes.Where(a => a.StartDate <= startDate && (a.EndDate >= startDate || !a.EndDate.HasValue));
                if (lBQCItemTime.Count() <= 0)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug("getQCMothsData 没有项目时效 时间：" + startDate.ToString("yyyy-MM-dd"));
                }
                //如果质控项目数据有匹配的数据加入
                var lisQCData = lisQCDatas.Where(a => a.ReceiveTime >= startDate && a.ReceiveTime <= DateTime.Parse(startDate.ToString("yyyy-MM-dd 23:59:59")));
                if (lisQCData.Count() > 0)
                {
                    foreach (var item in lisQCData.OrderBy(a => a.ReceiveTime))
                    {
                        LisQCDataMonthVO lisQCDataV1O = new LisQCDataMonthVO() { ReceiveTime = startDate };
                        lisQCDataV1O = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCDataMonthVO, LisQCData>(item);
                        lisQCDataV1O.Prec = IDLBQCItemDao.Get(QCItemID).LBItem.Prec;
                        if (lBQCItemTime.Count() > 0)
                        {
                            lisQCDataV1O.lBQCItemTime = lBQCItemTime.First();
                        }
                        lisQCDataVOs.Add(lisQCDataV1O);
                    }

                }
                else
                {
                    lisQCDataVO = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCDataMonthVO, LisQCData>(new LisQCData() { LBQCItem = new LBQCItem() { Id = QCItemID } });
                    lisQCDataVO.Id = 0;
                    lisQCDataVO.Prec = IDLBQCItemDao.Get(QCItemID).LBItem.Prec;
                    lisQCDataVO.ReceiveTime = startDate;
                    if (lBQCItemTime.Count() > 0)
                    {
                        lisQCDataVO.lBQCItemTime = lBQCItemTime.First();
                    }
                    lisQCDataVOs.Add(lisQCDataVO);
                }

            }
            return lisQCDataVOs;
        }
        /// <summary>
        /// 保存质控项目数据
        /// </summary>
        /// <param name="lisQCDataVO"></param>
        /// <returns></returns>
        public bool AddLisQCData(LisQCData LisQCData)
        {
            #region 组织数据
            var QCItemID = LisQCData.LBQCItem.Id;
            var ReceiveTime = LisQCData.ReceiveTime;
            //根据质控物获得项目id 查找同一个批次的数据
            LBQCItem lBQCItem = IDLBQCItemDao.Get(QCItemID);
            LisQCData.LBQCItem = lBQCItem;
            //同一个仪器 模块，组 同一个项目的数据
            string LBQCItemwhere = "LBItem.Id = " + lBQCItem.LBItem.Id + " and LBQCMaterial.LBEquip.Id=" + lBQCItem.LBQCMaterial.LBEquip.Id + " and LBQCMaterial.EquipModule='" + lBQCItem.LBQCMaterial.EquipModule + "' and LBQCMaterial.QCGroup='" + lBQCItem.LBQCMaterial.QCGroup + "'";
            IList<LBQCItem> lBQCItems = IDLBQCItemDao.GetListByHQL(LBQCItemwhere);
            List<long> lbqcitemids = new List<long>();
            lBQCItems.ToList().ForEach(aa => lbqcitemids.Add(aa.Id));
            //查找质控数据
            string lisdatawhere = "LBQCItem.Id in (" + string.Join(",", lbqcitemids) + ") and ReceiveTime>='" + ReceiveTime.AddDays(-15).ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + ReceiveTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            var lisQCDatas = DBDao.GetListByHQL(lisdatawhere).OrderBy(a => a.ReceiveTime).ToList();
            if (lisQCDatas.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.AddLisQCData 未找质控项目数据! where=" + lisdatawhere);
            }
            //查找时效  
            string lbQCItemTimeWhere = "LBQCItem.Id in (" + string.Join(",", lbqcitemids) + ")  and StartDate<='" + ReceiveTime.ToString("yyyy-MM-dd") + "' and (EndDate>='" + ReceiveTime.AddDays(-15).ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(lbQCItemTimeWhere);
            if (lBQCItemTimes.Count <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.AddLisQCData 未找到时效信息! where=" + lbQCItemTimeWhere);
            }
            //查找默认质控规则
            var lBQCRules = IDLBQCRuleDao.GetListByHQL("bDefault=1").ToList();
            //查找特殊质控规则
            IList<LBQCItemRule> lBQCItemRules = IDLBQCItemRuleDao.GetListByHQL("LBQCItem.Id in(" + string.Join(",", lbqcitemids) + ")");
            if (lBQCItemRules.Count(a => a.LBQCItem.Id == QCItemID) > 0)
            {
                lBQCRules.Clear();
                lBQCItemRules.Where(a => a.LBQCItem.Id == QCItemID).ToList().ForEach(a => lBQCRules.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCRule, LBQCRule>(a.LBQCRule)));
            }
            #endregion
            double quanvalue = 0;
            if (double.TryParse(LisCommonMethod.DisposeReportValue(LisQCData.ReportValue), out quanvalue))
            {
                LisQCData.QuanValue = quanvalue;

            }
            //当前项目如果没有质控时效 不进行质控规则判断
            if (lBQCItemTimes.Count(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)) <= 0)
            {
                LisQCData.LoseType = "无法判断";
            }
            else
            {
                //去除没有质控时效的数据
                for (int i = 0; i < lisQCDatas.Count; i++)
                {
                    var item = lisQCDatas[i];
                    if (lBQCItemTimes.Count(a => a.LBQCItem.Id == item.LBQCItem.Id && a.StartDate <= DateTime.Parse(item.ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= item.ReceiveTime || !a.EndDate.HasValue)) <= 0)
                    {
                        lisQCDatas.Remove(item);
                        i--;
                    }
                }
                //质控类型 0：靶值标准差， 1：定性 2：值范围
                int qcmType = lBQCItem.QCDataType;
                switch (qcmType)
                {
                    case 0:
                        //同批次的质控数据
                        var otherLisQCDatas = lisQCDatas.Where(a => a.ReceiveTime >= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && a.ReceiveTime <= ReceiveTime).ToList();
                        //同批次的质控时效
                        List<long> ostheritemids = new List<long>();
                        otherLisQCDatas.ForEach(a => ostheritemids.Add(a.LBQCItem.Id));
                        List<LBQCItemTime> otherlBQCItemTimes = new List<LBQCItemTime>();
                        lBQCItemTimes.ToList().ForEach(a =>
                        {
                            if (ostheritemids.Count(b => a.LBQCItem.Id == b && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)) > 0)
                            {
                                otherlBQCItemTimes.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItemTime, LBQCItemTime>(a));
                            }
                        });
                        //当前项目的质控数据
                        var itemqcdata = lisQCDatas.Where(a => a.LBQCItem.Id == QCItemID).ToList();
                        //当前项目的时效
                        var lbqcitemtiem = lBQCItemTimes.Where(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)).First();
                        //调用规则判断方法
                        SortedList<string, string> keyValuePairs = TargetSD(itemqcdata,
                            lbqcitemtiem,
                            lBQCRules,
                            otherLisQCDatas,
                            otherlBQCItemTimes,
                            LisQCData.QuanValue, lisQCDatas.ToList(), lBQCItemTimes.ToList(),
                            ReceiveTime,
                            lbqcitemids.Count);
                        LisQCData.Loserule = keyValuePairs["loserule"];
                        LisQCData.LoseType = keyValuePairs["loseType"];
                        break;
                    case 1:
                        //当前项目的时效
                        var QualitativeQCMitemtiem = lBQCItemTimes.Where(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)).First();
                        SortedList<string, string> QualitativeQCMPairs = QualitativeQCM(LisQCData.ReportValue, QualitativeQCMitemtiem);
                        LisQCData.Loserule = QualitativeQCMPairs["loserule"];
                        LisQCData.LoseType = QualitativeQCMPairs["loseType"];
                        break;
                    case 2:
                        var RangesQCMitemtiem = lBQCItemTimes.Where(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)).First();
                        SortedList<string, string> RangesQCMPairs = RangesQCM(LisQCData.QuanValue, RangesQCMitemtiem);
                        LisQCData.Loserule = RangesQCMPairs["loserule"];
                        LisQCData.LoseType = RangesQCMPairs["loseType"];
                        break;
                }
            }
            bool flag = DBDao.Save(LisQCData);
            return flag;
        }

        private SortedList<string, string> RangesQCM(double Value, LBQCItemTime rangesQCMitemtiem)
        {
            SortedList<string, string> keyValuePairs = new SortedList<string, string>();
            keyValuePairs["loserule"] = "";
            keyValuePairs["loseType"] = "在控";
            if (Value < rangesQCMitemtiem.LLValue || Value > rangesQCMitemtiem.HHValue)
            {
                keyValuePairs["loseType"] = "失控";
            }
            if (Value < rangesQCMitemtiem.LValue || Value > rangesQCMitemtiem.HValue)
            {
                keyValuePairs["loseType"] = "警告";
            }
            return keyValuePairs;
        }
        /// <summary>
        /// 定性规则判断
        /// </summary>
        /// <param name="reprotValue"></param>
        /// <param name="lBQCItemTime"></param>
        /// <returns></returns>
        private SortedList<string, string> QualitativeQCM(string reprotValue, LBQCItemTime lBQCItemTime)
        {
            SortedList<string, string> keyValuePairs = new SortedList<string, string>();
            keyValuePairs["loserule"] = "";
            keyValuePairs["loseType"] = "失控";
            if (lBQCItemTime.DescAll == null || lBQCItemTime.DescAll == "")
            {
                throw new Exception("定性时效没有设置时效值!");
            }
            var Qualitative = lBQCItemTime.DescAll.Split('|');
            foreach (var item in Qualitative)
            {
                if (reprotValue == item.Split('：')[1].Trim())
                {
                    keyValuePairs["loseType"] = item.Split('：')[0];
                    break;
                }
            }

            return keyValuePairs;
        }

        /// <summary>
        /// 规则判断
        /// </summary>
        /// <param name="lisQCDatas">当前质控项目数据</param>
        /// <param name="lBQCItemTime">当前质控项目时效</param>
        /// <param name="lBQCRules">当前质控项目规则</param>
        /// <param name="lisQCDataR_xS">同批次其他质控数据</param>
        /// <param name="othertiems">同批次其他质控时效</param>
        /// <param name="QuanValue">当前质控项目数据值</param>
        /// <returns></returns>
        SortedList<string, string> TargetSD(IEnumerable<LisQCData> lisQCDatas, LBQCItemTime lBQCItemTime, List<LBQCRule> lBQCRules,
            List<LisQCData> lisQCDataR_xS, List<LBQCItemTime> othertiems,
            double QuanValue, List<LisQCData> lisQCdataAll, List<LBQCItemTime> lBQCItemTimeALL, DateTime ReceiveTime, int QCMCount)
        {
            //记录需要更新的其他质控数据
            Dictionary<long, LisQCData> otherupdatedata = new Dictionary<long, LisQCData>();
            //记录质控状态
            StringBuilder stringBuilder = new StringBuilder();
            //记录失控类型
            string loseType = "";
            //用来表示 N_xs 和R_xs 在规则计算完毕的情况下还需要继续计算规则 其他浓度的计算
            bool iscontrol = false;
            //循环质控规则
            foreach (var a in lBQCRules.OrderBy(a => a.DispOrder))
            {
                //之前计算如果出现了规则
                if (a.LoseType == "失控" && a.BWarnState && loseType != "警告")
                {
                    iscontrol = true;
                }
                //判断是否失控 
                bool flag = false;
                foreach (var b in a.LBQCRulesConList.OrderBy(ord => ord.DispOrder))
                {
                    int N = b.LBQCRuleBase.CountN;
                    int M = b.LBQCRuleBase.CountM;
                    double X = b.LBQCRuleBase.MultX;
                    if (b.LBQCRuleBase.RuleType.ToString() == QCRule.N_xS.Key)
                    {
                        //高浓度还是低浓度 高=1
                        int HLValue = 0;
                        #region 同质控物计算 如果失控规则依赖警告规则那么只有出发了警告才会计算
                        if (!iscontrol)
                        {
                            //获得需要计算的项目值
                            List<double> QuanValues = new List<double>();
                            QuanValues.Add(QuanValue); //定量结果
                                                       //获得同浓度的 都是高浓度，或者都是低浓度的
                            lisQCDatas.Reverse().Take(N - 1).ToList().ForEach(d =>
                            {
                                if (QuanValue > lBQCItemTime.Target && d.QuanValue > lBQCItemTime.Target)
                                {
                                    HLValue = 1;
                                    QuanValues.Add(d.QuanValue);
                                }
                                if (QuanValue < lBQCItemTime.Target && d.QuanValue < lBQCItemTime.Target)
                                {
                                    HLValue = 2;
                                    QuanValues.Add(d.QuanValue);
                                }
                            });
                            //同浓度规则判定
                            bool HValue = false;
                            bool LValue = false;
                            if (QuanValues.Count == N)
                            {
                                if (flag)
                                {
                                    string type = "";
                                    StringBuilder rule = new StringBuilder();
                                    N_xSJudge(a, b, lBQCItemTime, ref rule, ref type, X, QuanValues, ref HValue, ref LValue);
                                }
                                else
                                {
                                    flag = N_xSJudge(a, b, lBQCItemTime, ref stringBuilder, ref loseType, X, QuanValues, ref HValue, ref LValue);
                                }
                            }
                        }
                        #endregion

                        //同批次计算n最少等于2  当前数据必须满足1-2s
                        if (stringBuilder.ToString().IndexOf("1-2S") >= 0 && N > 1) //stringBuilder.ToString().IndexOf("1-2S") >= 0 && 
                        {
                            //判断是第几批
                            int count = lisQCDataR_xS.Count(xs => xs.LBQCItem.Id == lBQCItemTime.LBQCItem.Id);

                            var otherData = lisQCDataR_xS;
                            //是不是需要找前一天的数据
                            int itemcount = N - QCMCount - 1;
                            if (itemcount > 0)
                            {
                                //计算取多少天的数据 每天的数据没有时间概念
                                int daycount = itemcount / QCMCount;
                                if (itemcount % QCMCount > 0)
                                {
                                    daycount++;
                                }
                                otherData = lisQCdataAll.Where(all => all.ReceiveTime >= DateTime.Parse(ReceiveTime.AddDays(-daycount).ToString("yyyy-MM-dd")) && all.ReceiveTime <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd"))).ToList();
                            }

                            //存储计算数据
                            List<LisQCData> lisQCDatass = new List<LisQCData>();
                            //按照质控物项目id分组 找出需要计算的批次数据  找同浓度  同时匹配数据的时效
                            List<IGrouping<long, LisQCData>> datagroup = new List<IGrouping<long, LisQCData>>();
                            if (HLValue == 1)
                            {
                                datagroup = otherData.Where(ts => ts.QuanValue > lBQCItemTimeALL.Where(tims => tims.StartDate <= ts.ReceiveTime && (tims.EndDate >= ts.ReceiveTime || !tims.EndDate.HasValue) && tims.LBQCItem.Id == ts.LBQCItem.Id).First().Target).GroupBy(xs => xs.LBQCItem.Id).ToList();
                            }
                            if (HLValue == 2)
                            {
                                datagroup = otherData.Where(ts => ts.QuanValue < lBQCItemTimeALL.Where(tims => tims.StartDate <= ts.ReceiveTime && (tims.EndDate >= ts.ReceiveTime || !tims.EndDate.HasValue) && tims.LBQCItem.Id == ts.LBQCItem.Id).First().Target).GroupBy(xs => xs.LBQCItem.Id).ToList();
                            }
                            datagroup.ForEach(gp =>
                            {
                                var gpord = gp.OrderBy(od => od.ReceiveTime);
                                if (gpord.Count() - 1 >= count)
                                {
                                    lisQCDatass.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(gpord.ElementAt(count)));
                                }
                            });
                            //如果其他浓度满足1-2s的个数等于或者大于N 表示满足规则
                            List<LisQCData> lisQCs = new List<LisQCData>();
                            foreach (var item in lisQCDatass)
                            {
                                bool hVaule = false, lValue = false;
                                var tiems = lBQCItemTimeALL.Where(tims => tims.StartDate <= item.ReceiveTime && (tims.EndDate >= item.ReceiveTime || !tims.EndDate.HasValue) && tims.LBQCItem.Id == item.LBQCItem.Id);
                                if (flag)
                                {
                                    string type = "";
                                    StringBuilder rule = new StringBuilder();
                                    N_xSJudge(a, b, tiems.First(), ref rule, ref type, X, new List<double>() { item.QuanValue }, ref hVaule, ref lValue);
                                }
                                else
                                {
                                    N_xSJudge(a, b, tiems.First(), ref stringBuilder, ref loseType, X, new List<double>() { item.QuanValue }, ref hVaule, ref lValue);
                                }
                                //如果是高浓度同时计算出来的也是高浓度
                                if (HLValue == 1 && hVaule)
                                {
                                    lisQCs.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(item));
                                }
                                //如果是低浓度同时计算出来的也是低浓度
                                if (HLValue == 2 && lValue)
                                {
                                    lisQCs.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(item));
                                }
                            }
                            if (lisQCs.Count >= N - 1)
                            {

                                if (!flag)
                                {
                                    if (stringBuilder.ToString().IndexOf(b.LBQCRuleBase.QCRuleBaseName) < 0)
                                    {
                                        stringBuilder.AppendFormat("/{0}", b.LBQCRuleBase.QCRuleBaseName);
                                        loseType = a.LoseType;
                                    }
                                    if (a.LoseType == "警告")
                                    {
                                        stringBuilder.AppendFormat("/{0}", b.LBQCRuleBase.QCRuleBaseName);
                                        loseType = a.LoseType;
                                    }
                                    if (a.LoseType == "失控")
                                    {
                                        stringBuilder.AppendFormat("/{0}", b.LBQCRuleBase.QCRuleBaseName);
                                        loseType = a.LoseType;
                                        flag = true;
                                    }
                                }

                                //其他浓度更新状态
                                foreach (var item in lisQCs)
                                {

                                    if (item.LoseType != "失控" && item.LoseType == "警告")
                                    {
                                        if (otherupdatedata.ContainsKey(item.Id))
                                        {
                                            if (otherupdatedata[item.Id].LoseType != "失控" && otherupdatedata[item.Id].LoseType == "警告")
                                            {
                                                item.LoseType = a.LoseType;
                                                item.Loserule += b.LBQCRuleBase.QCRuleBaseName;
                                                otherupdatedata[item.Id] = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(item); ;
                                                string uphql = "update LisQCData set LoseType='" + a.LoseType + "'" + ",Loserule=Loserule+'/" + b.LBQCRuleBase.QCRuleBaseName + "' where Id=" + item.Id;
                                                DBDao.UpdateByHql(uphql);
                                            }
                                        }
                                        else
                                        {
                                            item.LoseType = a.LoseType;
                                            item.Loserule += b.LBQCRuleBase.QCRuleBaseName;
                                            otherupdatedata[item.Id] = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(item); ;
                                            string uphql = "update LisQCData set LoseType='" + a.LoseType + "'" + ",Loserule=Loserule+'/" + b.LBQCRuleBase.QCRuleBaseName + "' where Id=" + item.Id;
                                            DBDao.UpdateByHql(uphql);
                                        }

                                    }
                                }
                            }
                        }

                        if (flag)
                        {
                            ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.TargetSD N_xS失控");
                        }
                    }
                    if (b.LBQCRuleBase.RuleType.ToString() == QCRule.N_X.Key && !iscontrol)
                    {
                        //获得需要计算的项目值
                        List<double> QuanValues = new List<double>();
                        QuanValues.Add(QuanValue); //定量结果
                        lisQCDatas.Reverse().Take(N - 1).ToList().ForEach(d => QuanValues.Add(d.QuanValue));
                        if (QuanValues.Count == N)
                        {
                            if (flag)
                            {
                                string type = "";
                                StringBuilder rule = new StringBuilder();
                                N_XJudge(a, b, lBQCItemTime, ref rule, ref type, QuanValues);
                            }
                            else
                            {
                                flag = N_XJudge(a, b, lBQCItemTime, ref stringBuilder, ref loseType, QuanValues);
                                if (flag)
                                {
                                    ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.TargetSD N_X失控");
                                }
                            }
                        }

                    }
                    if (b.LBQCRuleBase.RuleType.ToString() == QCRule.N_T.Key && !iscontrol)
                    {
                        //获得需要计算的项目值
                        List<double> QuanValues = new List<double>();
                        QuanValues.Add(QuanValue); //定量结果
                        lisQCDatas.Reverse().Take(N - 1).ToList().ForEach(d => QuanValues.Add(d.QuanValue));
                        if (QuanValues.Count == N)
                        {

                            if (flag)
                            {
                                string type = "";
                                StringBuilder rule = new StringBuilder();
                                N_TJudge(QuanValue, a, b, ref rule, ref type, QuanValues);
                            }
                            else
                            {
                                flag = N_TJudge(QuanValue, a, b, ref stringBuilder, ref loseType, QuanValues);
                                if (flag)
                                {
                                    ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.TargetSD N_T失控");
                                }
                            }
                        }
                    }
                    if (b.LBQCRuleBase.RuleType.ToString() == QCRule.R_xS.Key)
                    {
                        //查找当前质控今天做了几次 判断是第几批
                        int count = lisQCDataR_xS.Count(xs => xs.LBQCItem.Id == lBQCItemTime.LBQCItem.Id);
                        //存储计算数据
                        List<LisQCData> lisQCDatass = new List<LisQCData>();
                        //按照质控物项目id分组 找出需要计算的批次数据
                        var datagroup = lisQCDataR_xS.GroupBy(xs => xs.LBQCItem.Id);
                        foreach (var gp in datagroup)
                        {
                            var gpord = gp.OrderBy(od => od.ReceiveTime);
                            if (gpord.Count() - 1 >= count)
                            {
                                lisQCDatass.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(gpord.ElementAt(count)));
                            }
                        }

                        if (stringBuilder.ToString().IndexOf("1-2S") >= 0 && lisQCDatass.Count > 0)
                        {
                            List<long> timeids = new List<long>();
                            lisQCDatass.ForEach(f => timeids.Add(f.LBQCItem.Id));

                            if (flag)
                            {
                                string type = "";
                                StringBuilder rule = new StringBuilder();
                                R_xSJudge(QuanValue, a, b, lBQCItemTime, ref rule, ref type, X, lisQCDatass, othertiems, iscontrol, otherupdatedata);
                            }
                            else
                            {
                                flag = R_xSJudge(QuanValue, a, b, lBQCItemTime, ref stringBuilder, ref loseType, X, lisQCDatass, othertiems, iscontrol, otherupdatedata);
                                if (flag)
                                {
                                    ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.TargetSD R_xS失控");
                                }
                            }
                        }
                    }
                    if (b.LBQCRuleBase.RuleType.ToString() == QCRule.M_Of_NxS.Key && !iscontrol)
                    {
                        //获得需要计算的项目值
                        List<double> QuanValues = new List<double>();
                        QuanValues.Add(QuanValue); //定量结果
                        lisQCDatas.Reverse().Take(N - 1).ToList().ForEach(d => QuanValues.Add(d.QuanValue));
                        //如果都超过规则
                        if (QuanValues.Count == N)
                        {
                            if (flag)
                            {
                                string type = "";
                                StringBuilder rule = new StringBuilder();
                                M_Of_NxSJudge(a, b, lBQCItemTime, ref rule, ref type, M, X, QuanValues);
                            }
                            else
                            {
                                if (flag)
                                {
                                    flag = M_Of_NxSJudge(a, b, lBQCItemTime, ref stringBuilder, ref loseType, M, X, QuanValues);
                                    ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.TargetSD M_Of_NxS失控");
                                }
                            }

                        }
                    }
                };
                if (loseType == "失控")
                {
                    iscontrol = true;
                }
            };

            //返回需要保存的状态
            SortedList<string, string> keyValuePairs = new SortedList<string, string>();
            string resultloserule = "";
            if (stringBuilder.ToString().Length > 0)
            {
                resultloserule = string.Join("/", stringBuilder.ToString().Substring(1).Split('/').Distinct());
            }
            keyValuePairs["loserule"] = resultloserule;
            if (loseType == "")
            {
                loseType = "在控";
            }
            keyValuePairs["loseType"] = loseType;
            return keyValuePairs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="QuanValue"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="lBQCItemTime"></param>
        /// <param name="stringBuilder"></param>
        /// <param name="loseType"></param>
        /// <param name="X"></param>
        /// <param name="lisQCDatass"></param>
        /// <param name="lisQCDatassTimes"></param>
        /// <param name="iscontrol">规则是否已经不需要执行了</param>
        /// <returns></returns>
        private bool R_xSJudge(double QuanValue, LBQCRule a, LBQCRulesCon b, LBQCItemTime lBQCItemTime, ref StringBuilder stringBuilder, ref string loseType, double X, List<LisQCData> lisQCDatass, IList<LBQCItemTime> lisQCDatassTimes, bool iscontrol,
            Dictionary<long, LisQCData> otherupdatedata)
        {
            bool flag = false;

            //计算
            double firstvalue = 0;
            if (QuanValue > lBQCItemTime.Target)
            {
                firstvalue = (QuanValue - lBQCItemTime.Target) / lBQCItemTime.SD;
            }
            if (QuanValue < lBQCItemTime.Target)
            {
                firstvalue = (lBQCItemTime.Target - QuanValue) / lBQCItemTime.SD;
            }

            bool isControl = false;
            StringBuilder qcname = new StringBuilder();
            foreach (var js in lisQCDatass)
            {
                double result = 0;
                var times = lisQCDatassTimes.Where(tim => tim.LBQCItem.Id == js.LBQCItem.Id).First();
                if (js.QuanValue > times.Target)
                {
                    result = (js.QuanValue - times.Target) / times.SD;
                }
                if (js.QuanValue < times.Target)
                {
                    result = (times.Target - js.QuanValue) / times.SD;
                }

                //2个值都要大于规则的一半才可以计算规则
                if (result > X / 2 && firstvalue > X / 2 && result + firstvalue > X) //结果归一处理  大于4倍sd 直接为4即可
                {
                    if (a.LoseType == "警告" && !iscontrol)
                    {
                        stringBuilder.AppendFormat("/{0}", b.LBQCRuleBase.QCRuleBaseName);
                        loseType = a.LoseType;
                    }
                    if (a.LoseType == "失控")
                    {
                        if (!isControl && !iscontrol)
                        {
                            stringBuilder.AppendFormat("/{0}", b.LBQCRuleBase.QCRuleBaseName);
                            loseType = a.LoseType;
                            isControl = true;
                        }

                        if (js.LoseType != "失控" && js.LoseType == "警告")
                        {

                            if (otherupdatedata.ContainsKey(js.Id))
                            {
                                if (otherupdatedata[js.Id].LoseType != "失控" && otherupdatedata[js.Id].LoseType == "警告")
                                {
                                    js.LoseType = a.LoseType;
                                    js.Loserule += b.LBQCRuleBase.QCRuleBaseName;
                                    otherupdatedata[js.Id] = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(js);
                                    string uphql = "update LisQCData set LoseType='" + a.LoseType + "'" + ",Loserule=Loserule+'/" + b.LBQCRuleBase.QCRuleBaseName + "' where Id=" + js.Id;
                                    DBDao.UpdateByHql(uphql);
                                }
                            }
                            else
                            {
                                js.LoseType = a.LoseType;
                                js.Loserule += b.LBQCRuleBase.QCRuleBaseName;
                                otherupdatedata[js.Id] = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LisQCData, LisQCData>(js);
                                string uphql = "update LisQCData set LoseType='" + a.LoseType + "'" + ",Loserule=Loserule+'/" + b.LBQCRuleBase.QCRuleBaseName + "' where Id=" + js.Id;
                                DBDao.UpdateByHql(uphql);
                            }


                        }
                    }
                }
            };
            if (isControl)
            {
                flag = true;
            }
            return flag;
        }

        private bool M_Of_NxSJudge(LBQCRule LBQCRule, LBQCRulesCon LBQCRulesCon, LBQCItemTime lBQCItemTime, ref StringBuilder stringBuilder, ref string loseType, int M, double X, List<double> QuanValues)
        {
            bool flag = false;
            if (QuanValues.Count(c => c > lBQCItemTime.Target + (lBQCItemTime.SD * X) || c > lBQCItemTime.Target - (lBQCItemTime.SD * X)) >= M)
            {
                if (LBQCRule.LoseType == "警告")
                {
                    stringBuilder.AppendFormat("/{0}", LBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = LBQCRule.LoseType;
                }
                if (LBQCRule.LoseType == "失控")
                {
                    stringBuilder.AppendFormat("/{0}", LBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = LBQCRule.LoseType;
                    flag = true;
                }
            }
            return flag;
        }

        private bool N_TJudge(double QuanValue, LBQCRule LBQCRule, LBQCRulesCon LBQCRulesCon, ref StringBuilder stringBuilder, ref string loseType, List<double> QuanValues)
        {
            bool flag = false;
            var MixValue = QuanValue;
            var MinValue = QuanValue;
            bool MixFlag = true;
            bool MinFlag = true;
            for (int i = 1; i < QuanValues.Count; i++)
            {
                if (MixValue < QuanValues[i])
                {
                    MixFlag = false;
                }
                if (MinValue > QuanValues[i])
                {
                    MinFlag = false;
                }
                MixValue = QuanValues[i];
                MinValue = QuanValues[i];

            }

            if (MinFlag || MixFlag)
            {
                if (LBQCRule.LoseType == "警告")
                {
                    stringBuilder.AppendFormat("/{0}", LBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = LBQCRule.LoseType;
                }
                if (LBQCRule.LoseType == "失控")
                {
                    stringBuilder.AppendFormat("/{0}", LBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = LBQCRule.LoseType;
                    flag = true;
                }
            }
            return flag;
        }

        private bool N_xSJudge(LBQCRule lBQCRule, LBQCRulesCon lBQCRulesCon, LBQCItemTime lBQCItemTime, ref StringBuilder stringBuilder, ref string loseType, double X, List<double> QuanValues, ref bool HValue, ref bool LValue)
        {
            bool flag = false;
            if (QuanValues.Count(c => c > lBQCItemTime.Target + (lBQCItemTime.SD * X) || c < lBQCItemTime.Target - (lBQCItemTime.SD * X)) == QuanValues.Count)
            {
                HValue = QuanValues.Count(c => c > lBQCItemTime.Target + (lBQCItemTime.SD * X)) == QuanValues.Count;
                LValue = QuanValues.Count(c => c < lBQCItemTime.Target - (lBQCItemTime.SD * X)) == QuanValues.Count;
                if (lBQCRule.LoseType == "警告")
                {
                    stringBuilder.AppendFormat("/{0}", lBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = lBQCRule.LoseType;
                }
                if (lBQCRule.LoseType == "失控")
                {
                    stringBuilder.AppendFormat("/{0}", lBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = lBQCRule.LoseType;
                    flag = true;
                }
            }
            return flag;
        }

        private bool N_XJudge(LBQCRule lBQCRule, LBQCRulesCon lBQCRulesCon, LBQCItemTime lBQCItemTime, ref StringBuilder stringBuilder, ref string loseType, List<double> QuanValues)
        {
            bool flag = false;

            //浓度测定值连续10次在均值一侧
            if (QuanValues.Count(c => c > lBQCItemTime.Target) == QuanValues.Count || QuanValues.Count(c => c < lBQCItemTime.Target) == QuanValues.Count)
            {
                if (lBQCRule.LoseType == "警告")
                {
                    stringBuilder.AppendFormat("/{0}", lBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = lBQCRule.LoseType;
                }
                if (lBQCRule.LoseType == "失控")
                {
                    stringBuilder.AppendFormat("/{0}", lBQCRulesCon.LBQCRuleBase.QCRuleBaseName);
                    loseType = lBQCRule.LoseType;
                    flag = true;
                }
            }
            return flag;
        }
        /// <summary>
        /// 更新质控项目数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <returns></returns>
        public void EditLisQCData(LisQCData LisQCData)
        {
            #region 组织数据
            var QCItemID = LisQCData.LBQCItem.Id;
            var ReceiveTime = LisQCData.ReceiveTime;
            //根据质控物获得项目id 查找同一个批次的数据
            LBQCItem lBQCItem = IDLBQCItemDao.Get(QCItemID);
            LisQCData.LBQCItem = lBQCItem;
            //同一个仪器 模块，组 同一个项目的数据
            string LBQCItemwhere = "LBItem.Id = " + lBQCItem.LBItem.Id + " and LBQCMaterial.LBEquip.Id=" + lBQCItem.LBQCMaterial.LBEquip.Id + " and LBQCMaterial.EquipModule='" + lBQCItem.LBQCMaterial.EquipModule + "' and LBQCMaterial.QCGroup='" + lBQCItem.LBQCMaterial.QCGroup + "'";
            IList<LBQCItem> lBQCItems = IDLBQCItemDao.GetListByHQL(LBQCItemwhere);
            List<long> lbqcitemids = new List<long>();
            lBQCItems.ToList().ForEach(aa => lbqcitemids.Add(aa.Id));
            //查找质控数据  不查找当前修改的数据
            string lisdatawhere = "Id<>" + LisQCData.Id + " and LBQCItem.Id in (" + string.Join(",", lbqcitemids) + ") and ReceiveTime>='" + ReceiveTime.AddDays(-15).ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + ReceiveTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            var lisQCDatas = DBDao.GetListByHQL(lisdatawhere).OrderBy(a => a.ReceiveTime).ToList();
            if (lisQCDatas.Count() <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.UpdateLisQCData 未找质控项目数据! where=" + lisdatawhere);
            }
            //查找时效  
            string lbQCItemTimeWhere = "LBQCItem.Id in (" + string.Join(",", lbqcitemids) + ")  and StartDate<='" + ReceiveTime.ToString("yyyy-MM-dd") + "' and (EndDate>='" + ReceiveTime.AddDays(-15).ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(lbQCItemTimeWhere);
            if (lBQCItemTimes.Count <= 0)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.UpdateLisQCData 未找到时效信息! where=" + lbQCItemTimeWhere);
            }
            //查找默认质控规则
            var lBQCRules = IDLBQCRuleDao.GetListByHQL("bDefault=1").ToList();
            //查找特殊质控规则
            IList<LBQCItemRule> lBQCItemRules = IDLBQCItemRuleDao.GetListByHQL("LBQCItem.Id in(" + string.Join(",", lbqcitemids) + ")");
            if (lBQCItemRules.Count(a => a.LBQCItem.Id == QCItemID) > 0)
            {
                lBQCRules.Clear();
                lBQCItemRules.Where(a => a.LBQCItem.Id == QCItemID).ToList().ForEach(a => lBQCRules.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCRule, LBQCRule>(a.LBQCRule)));
            }
            #endregion

            double quanvalue = 0;
            if (double.TryParse(LisCommonMethod.DisposeReportValue(LisQCData.ReportValue), out quanvalue))
            {
                LisQCData.QuanValue = quanvalue;
            }
            //当前项目如果没有质控时效 不进行质控规则判断
            if (lBQCItemTimes.Count(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)) <= 0)
            {
                LisQCData.LoseType = "无法判断";
            }
            else
            {
                //去除没有质控时效的数据
                for (int i = 0; i < lisQCDatas.Count; i++)
                {
                    var item = lisQCDatas[i];
                    if (lBQCItemTimes.Count(a => a.LBQCItem.Id == item.LBQCItem.Id && a.StartDate <= DateTime.Parse(item.ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= item.ReceiveTime || !a.EndDate.HasValue)) <= 0)
                    {
                        lisQCDatas.Remove(item);
                        i--;
                    }
                }
                //质控类型 0：靶值标准差， 1：定性 2：值范围
                int qcmType = lBQCItem.QCDataType;
                switch (qcmType)
                {
                    case 0:
                        //同批次的质控数据
                        var otherLisQCDatas = lisQCDatas.Where(a => a.ReceiveTime >= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && a.ReceiveTime <= ReceiveTime).ToList();
                        //同批次的质控时效
                        List<long> ostheritemids = new List<long>();
                        otherLisQCDatas.ForEach(a => ostheritemids.Add(a.LBQCItem.Id));
                        List<LBQCItemTime> otherlBQCItemTimes = new List<LBQCItemTime>();
                        lBQCItemTimes.ToList().ForEach(a =>
                        {
                            if (ostheritemids.Count(b => a.LBQCItem.Id == b && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && a.EndDate >= ReceiveTime) > 0)
                            {
                                otherlBQCItemTimes.Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBQCItemTime, LBQCItemTime>(a));
                            }
                        });
                        //当前项目的质控数据
                        var itemqcdata = lisQCDatas.Where(a => a.LBQCItem.Id == QCItemID).ToList();
                        //当前项目的时效
                        var lbqcitemtiem = lBQCItemTimes.Where(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)).First();
                        //调用规则判断方法
                        SortedList<string, string> keyValuePairs = TargetSD(itemqcdata,
                            lbqcitemtiem,
                            lBQCRules,
                            otherLisQCDatas,
                            otherlBQCItemTimes,
                            LisQCData.QuanValue, lisQCDatas.ToList(), lBQCItemTimes.ToList(),
                            ReceiveTime,
                            lbqcitemids.Count);
                        LisQCData.Loserule = keyValuePairs["loserule"];
                        LisQCData.LoseType = keyValuePairs["loseType"];
                        break;
                    case 1:
                        //当前项目的时效
                        var QualitativeQCMitemtiem = lBQCItemTimes.Where(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)).First();
                        SortedList<string, string> QualitativeQCMPairs = QualitativeQCM(LisQCData.ReportValue, QualitativeQCMitemtiem);
                        LisQCData.Loserule = QualitativeQCMPairs["loserule"];
                        LisQCData.LoseType = QualitativeQCMPairs["loseType"];
                        break;
                    case 2:
                        var RangesQCMitemtiem = lBQCItemTimes.Where(a => a.LBQCItem.Id == QCItemID && a.StartDate <= DateTime.Parse(ReceiveTime.ToString("yyyy-MM-dd")) && (a.EndDate >= ReceiveTime || !a.EndDate.HasValue)).First();
                        SortedList<string, string> RangesQCMPairs = RangesQCM(LisQCData.QuanValue, RangesQCMitemtiem);
                        LisQCData.Loserule = RangesQCMPairs["loserule"];
                        LisQCData.LoseType = RangesQCMPairs["loseType"];
                        break;
                }
            }
        }
        /// <summary>
        /// LJ图
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureLJ QCMFigureLJ(long qCItemId, DateTime startDate, DateTime endDate)
        {
            LBQCItem lBQCItem = IDLBQCItemDao.Get(qCItemId);
            LBEquip lBEquip = IDLBEquipDao.Get(lBQCItem.LBQCMaterial.LBEquip.Id);

            string lisdatawhere = "LBQCItem.Id=" + qCItemId + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            string qctimewhere = "LBQCItem.Id=" + qCItemId + "  and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qctimewhere).OrderBy(a => a.StartDate).ToList();
            QCMFigureLJ qCMFigureLJ = new QCMFigureLJ();
            //Y轴
            if (lBQCItemTimes.Count > 0)
            {
                for (int i = 0; i < lBQCItemTimes.Count; i++)
                {
                    var qcitemtime = lBQCItemTimes[i];
                    List<LJY_Data> lJY_Datas = new List<LJY_Data>();
                    //计算 N倍标准差
                    for (int j = 1; j < 4; j++)
                    {
                        LJY_Data lJY_Data = new LJY_Data();
                        lJY_Data.Name = "+" + j + "SD";
                        lJY_Data.SDValue = (qcitemtime.Target + j * qcitemtime.SD).ToString();
                        lJY_Data.Value = j.ToString();
                        lJY_Datas.Add(lJY_Data);
                        LJY_Data lJY_Data1 = new LJY_Data();
                        lJY_Data1.Name = "-" + j + "SD";
                        lJY_Data1.Value = (-j).ToString();
                        lJY_Data1.SDValue = (qcitemtime.Target - j * qcitemtime.SD).ToString();
                        lJY_Datas.Add(lJY_Data1);
                    }
                    LJY lJY = new LJY();
                    lJY.Max = 4;
                    lJY.Min = -4;
                    lJY.DateTime = qcitemtime.StartDate;
                    lJY_Datas.Add(new LJY_Data() { Name = "Target", Value = "0", SDValue = qcitemtime.Target.ToString() });
                    lJY.Data = lJY_Datas;
                    if (i == 0)
                    {
                        qCMFigureLJ.Y = lJY;
                    }
                    else
                    {
                        if (qCMFigureLJ.ItemTimes == null)
                        {
                            qCMFigureLJ.ItemTimes = new List<LJY>();
                        }
                        qCMFigureLJ.ItemTimes.Add(lJY);
                    }
                }

            }
            //数据整理
            int index = 0;
            List<int> batch = new List<int>(); //批次
            List<DateTime> dateTimes = new List<DateTime>(); //时间
            SortedList<DateTime, int> itemtimesindex = new SortedList<DateTime, int>(); // 存储时效所在位置
            foreach (var item in lisQCDatas.OrderBy(a => a.ReceiveTime))
            {
                LJ_Data lJ_Data = new LJ_Data();
                //判断是否使用数据
                if (item.BUse)
                {
                    index++;
                }
                lJ_Data.BUse = item.BUse;
                lJ_Data.ReportValue = item.ReportValue;
                lJ_Data.QcDataId = item.Id.ToString();
                lJ_Data.DateTime = item.ReceiveTime;
                lJ_Data.Batch = index;
                lJ_Data.rolseType = item.LoseType;
                lJ_Data.loseRule = item.Loserule;
                lJ_Data.People = item.Operator;
                var times = lBQCItemTimes.Where(a => a.StartDate <= item.ReceiveTime && (a.EndDate >= item.ReceiveTime || !a.EndDate.HasValue));
                lJ_Data.YValue = item.QuanValue;
                if (times.Count() > 0)
                {
                    if (!itemtimesindex.ContainsKey(times.First().StartDate) && qCMFigureLJ.Y.DateTime != times.First().StartDate)
                    {
                        itemtimesindex.Add(times.First().StartDate, index);
                    }
                    //值计算
                    lJ_Data.YValue = (item.QuanValue - times.First().Target) / times.First().SD;
                    //如果归一值超过图的边界
                    if (lJ_Data.YValue >= qCMFigureLJ.Y.Max)
                    {
                        lJ_Data.YValue = qCMFigureLJ.Y.Max - 0.2;
                    }
                    if (lJ_Data.YValue <= qCMFigureLJ.Y.Min)
                    {
                        lJ_Data.YValue = qCMFigureLJ.Y.Min + 0.2;
                    }
                    lJ_Data.SD = times.First().SD.ToString();
                    lJ_Data.Target = times.First().Target.ToString();
                }
                lJ_Data.ItemName = item.LBQCItem.LBItem.CName;
                lJ_Data.QCMaterial = item.LBQCItem.LBQCMaterial.CName;
                lJ_Data.Equip = lBEquip.CName;
                lJ_Data.EValue = item.EValue;

                if (qCMFigureLJ.Data == null)
                {
                    qCMFigureLJ.Data = new List<LJ_Data>();
                }
                qCMFigureLJ.Data.Add(lJ_Data);
                batch.Add(index);
                dateTimes.Add(item.ReceiveTime);
            }
            qCMFigureLJ.X = new LJX();
            batch.Insert(0, 0);
            batch.Add(++index);
            qCMFigureLJ.X.Batch = batch.Distinct().ToList();
            dateTimes.Insert(0, startDate);
            dateTimes.Add(endDate);
            qCMFigureLJ.X.Date = dateTimes.Distinct().ToList();
            //设置时效所在位置
            if (qCMFigureLJ.ItemTimes != null)
            {
                if (itemtimesindex.Count <= 0)
                {


                    if (lBQCItemTimes.Count(a => a.StartDate > qCMFigureLJ.Data.Last().DateTime) > 0)
                    {
                        qCMFigureLJ.ItemTimes.ForEach(a => a.Batch = index);
                    }
                    else
                    {
                        qCMFigureLJ.ItemTimes.ForEach(a => a.Batch = 1);
                    }
                }
                else
                {
                    qCMFigureLJ.ItemTimes.ForEach(a => a.Batch = index);
                    foreach (var item in itemtimesindex)
                    {
                        foreach (var ItemTime in qCMFigureLJ.ItemTimes)
                        {
                            if (ItemTime.Batch == index)
                            {
                                ItemTime.Batch = item.Value;
                            }
                            if (ItemTime.DateTime == item.Key)
                            {
                                break;
                            }
                        }
                    }
                }
            }


            return qCMFigureLJ;
        }
        /// <summary>
        /// Z分图
        /// </summary>
        /// <param name="equipId"></param>
        /// <param name="QCMId"></param>
        /// <param name="itemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureZ QCMFigureZ(long equipId, long QCMId, long itemId, DateTime startDate, DateTime endDate)
        {
            Dictionary<long, List<Z_Data>> QCItemDatasAll = new Dictionary<long, List<Z_Data>>(); //存储所有不同质控物的数据
            LBQCMaterial lBQCMaterial = IDLBQCMaterialDao.Get(QCMId);
            string eqwhere = "LBEquip.Id=" + equipId + " and QCGroup='" + lBQCMaterial.QCGroup + "' and EquipModule='" + lBQCMaterial.EquipModule + "'";
            IList<LBQCMaterial> lBQCMaterials = IDLBQCMaterialDao.GetListByHQL(eqwhere);
            List<long> qcmids = new List<long>();
            lBQCMaterials.ToList().ForEach(a => qcmids.Add(a.Id));
            string qcitemwhere = " QCDataType=0 and  LBQCMaterial.Id in (" + string.Join(",", qcmids) + ") and LBItem.Id=" + itemId;
            IList<LBQCItem> lBQCItems = IDLBQCItemDao.GetListByHQL(qcitemwhere);
            List<long> lbqcitemids = new List<long>();
            List<long> eqids = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                //QCItemDatas.Add(a.Id, new List<Z_Data>());
                lbqcitemids.Add(a.Id);
                eqids.Add(a.LBQCMaterial.LBEquip.Id);
            });
            IList<LBEquip> lBEquips = IDLBEquipDao.GetListByHQL("Id in (" + string.Join(",", eqids) + ")");
            string lisdatawhere = "LBQCItem.Id in (" + string.Join(",", lbqcitemids) + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            string qctimewhere = "LBQCItem.Id in (" + string.Join(",", lbqcitemids) + ")  and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qctimewhere).OrderBy(a => a.StartDate).ToList();
            QCMFigureZ qCMFigureZ = new QCMFigureZ();
            //Y轴
            if (lBQCItemTimes.Count > 0)
            {
                for (int i = 0; i < lBQCItemTimes.Count; i++)
                {
                    var qcitemtime = lBQCItemTimes[i];


                    List<ZY_Data> ZY_Datas = new List<ZY_Data>();
                    //计算 N倍标准差
                    for (int j = 1; j < 4; j++)
                    {
                        ZY_Data ZY_Data = new ZY_Data();
                        ZY_Data.Name = "+" + j + "SD";
                        ZY_Data.SDValue = (qcitemtime.Target + j * qcitemtime.SD).ToString();
                        ZY_Data.Value = j.ToString();
                        ZY_Datas.Add(ZY_Data);
                        ZY_Data ZY_Data1 = new ZY_Data();
                        ZY_Data1.Name = "-" + j + "SD";
                        ZY_Data1.Value = (-j).ToString();
                        ZY_Data1.SDValue = (qcitemtime.Target - j * qcitemtime.SD).ToString();
                        ZY_Datas.Add(ZY_Data1);
                    }
                    ZY ZY = new ZY();
                    ZY.Max = 4;
                    ZY.Min = -4;
                    var qcmid = lBQCItems.Where(a => a.Id == qcitemtime.LBQCItem.Id).First().LBQCMaterial.Id;
                    ZY.LBQCMaterialCName = lBQCMaterials.Where(a => a.Id == qcmid).First().CName;
                    ZY.DateTime = qcitemtime.StartDate;
                    ZY_Datas.Add(new ZY_Data() { Name = "Target", Value = "0", SDValue = qcitemtime.Target.ToString() });
                    ZY.Data = ZY_Datas;
                    if (i == 0)
                    {
                        qCMFigureZ.Y = ZY;
                    }
                    else
                    {
                        if (qCMFigureZ.ItemTimes == null)
                        {
                            qCMFigureZ.ItemTimes = new List<ZY>();
                        }
                        qCMFigureZ.ItemTimes.Add(ZY);
                    }
                }
            }
            //数据
            int index = 1;
            //记录最大的质控数据时间
            DateTime dateTimeMax = DateTime.Parse("1999-01-01");
            SortedList<DateTime, int> itemtimesindex = new SortedList<DateTime, int>(); // 存储时效所在位置
            //根据每一天区分不同浓度的批次
            for (; startDate < endDate.AddDays(1); startDate = startDate.AddDays(1))
            {
                List<Z_Data> z_Datas = new List<Z_Data>();
                var qcdatadays = lisQCDatas.Where(a => a.ReceiveTime >= startDate && a.ReceiveTime < startDate.AddDays(1));
                //如果当天存在数据
                if (qcdatadays.Count() > 0)
                {

                    Dictionary<long, List<Z_Data>> QCItemDatas = new Dictionary<long, List<Z_Data>>(); //存储不同质控物的数据
                    //将数据放到对应的质控项目数组中
                    foreach (var item in qcdatadays.OrderBy(a => a.ReceiveTime))
                    {
                        //组织返回数据
                        Z_Data Z_Data = new Z_Data();
                        Z_Data.ReportValue = item.ReportValue;
                        Z_Data.QcDataId = item.Id.ToString();
                        Z_Data.DateTime = item.ReceiveTime;
                        Z_Data.BUse = item.BUse;
                        //Z_Data.Batch = index;
                        Z_Data.rolseType = item.LoseType;
                        Z_Data.loseRule = item.Loserule;
                        Z_Data.People = item.Operator;
                        Z_Data.EValue = item.EValue;
                        var times = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.LBQCItem.Id && a.StartDate <= item.ReceiveTime && (a.EndDate >= item.ReceiveTime || !a.EndDate.HasValue));
                        Z_Data.YValue = item.QuanValue;
                        if (times.Count() > 0)
                        {

                            Z_Data.YValue = (item.QuanValue - times.First().Target) / times.First().SD;

                            if (Z_Data.YValue >= qCMFigureZ.Y.Max)
                            {
                                Z_Data.YValue = qCMFigureZ.Y.Max - 0.2;
                            }
                            if (Z_Data.YValue <= qCMFigureZ.Y.Min)
                            {
                                Z_Data.YValue = qCMFigureZ.Y.Min + 0.2;
                            }
                            Z_Data.SD = times.First().SD.ToString();
                            Z_Data.Target = times.First().Target.ToString();
                        }
                        Z_Data.ItemName = item.LBQCItem.LBItem.CName;
                        Z_Data.QCMaterial = item.LBQCItem.LBQCMaterial.CName;
                        Z_Data.Equip = lBEquips.Where(eq => eq.Id == item.LBQCItem.LBQCMaterial.LBEquip.Id).First().CName;
                        if (QCItemDatas.ContainsKey(item.LBQCItem.Id))
                        {
                            QCItemDatas[item.LBQCItem.Id].Add(Z_Data);
                        }
                        else
                        {
                            QCItemDatas.Add(item.LBQCItem.Id, new List<Z_Data>() { Z_Data });
                        }
                    }

                    //获得最大的质控项目数据长度
                    int Maxdata = 0;
                    foreach (var item in QCItemDatas)
                    {
                        Maxdata = item.Value.Count > Maxdata ? item.Value.Count : Maxdata;
                    }

                    int maxidex = 0;
                    //循环不同的质控项目数据
                    for (int i = 0; i < Maxdata; i++)
                    {
                        foreach (var item in QCItemDatas)
                        {
                            //如果这个质控项目没有数据了则不加index
                            if (item.Value.Count > i)
                            {
                                //如果是不使用数据 批次不要增加
                                if (item.Value[i].BUse)
                                {
                                    item.Value[i].Batch = i > 0 ? item.Value[i - 1].Batch + 1 : index;
                                    //var itemdatas = QCItemDatasAll.Where(a => a.Key == item.Key);
                                    //if (itemdatas.Count(a=>a.Value.Count>0)>0)
                                    //{
                                    //    item.Value[i].Batch = i > 0? itemdatas.First().Value.Last().Batch + 1 : index;
                                    //}
                                }
                                else
                                {
                                    item.Value[i].Batch = i > 0 ? item.Value[i - 1].Batch : index;
                                    //var itemdatas = QCItemDatasAll.Where(a => a.Key == item.Key);
                                    //if (itemdatas.Count(a => a.Value.Count > 0) > 0)
                                    //{
                                    //    item.Value[i].Batch = i > 0 ? itemdatas.First().Value.Last().Batch : index;
                                    //}
                                }
                                maxidex = item.Value[i].Batch; //获得当前最大的批次数


                                //符合同一个质控项目和时间的 时效
                                var times = lBQCItemTimes.Where(a => a.LBQCItem.Id == item.Key && a.StartDate <= item.Value[i].DateTime && (a.EndDate >= item.Value[i].DateTime || !a.EndDate.HasValue));
                                if (times.Count() > 0)
                                {
                                    if (!itemtimesindex.ContainsKey(times.First().StartDate) && qCMFigureZ.Y.DateTime != times.First().StartDate)
                                    {
                                        itemtimesindex.Add(times.First().StartDate, maxidex);
                                    }
                                    if (dateTimeMax == DateTime.Parse("1999-01-01"))
                                    {
                                        dateTimeMax = item.Value[i].DateTime;
                                    }
                                    else
                                    {
                                        dateTimeMax = dateTimeMax > item.Value[i].DateTime ? dateTimeMax : item.Value[i].DateTime;
                                    }
                                }

                                if (QCItemDatasAll.ContainsKey(item.Key))
                                {
                                    QCItemDatasAll[item.Key].Add(ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<Z_Data, Z_Data>(item.Value[i]));
                                }
                                else
                                {
                                    QCItemDatasAll.Add(item.Key, new List<Z_Data>() { ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<Z_Data, Z_Data>(item.Value[i]) });
                                }
                            }
                        }
                    }
                    index = maxidex > 0 ? maxidex + 1 : index;
                }
            }
            //加入到总数据中
            foreach (var item in QCItemDatasAll)
            {
                if (qCMFigureZ.Data == null)
                {
                    qCMFigureZ.Data = new List<List<Z_Data>>();
                }
                qCMFigureZ.Data.Add(item.Value);
            }
            //格式化总数据 区分使用和不使用的数据
            //foreach (var item in qCMFigureZ.Data)
            //{
            //    if (qCMFigureZ.UnData == null)
            //    {
            //        qCMFigureZ.UnData = new List<List<Z_Data>>();
            //    }
            //    qCMFigureZ.UnData.Add(item.Where(a => a.BUse == false).ToList());
            //    item.RemoveAll(a=>a.BUse==false);// (item.Where(a => a.BUse == false).ToList());
            //}
            //设置批次位置
            List<int> batchs = new List<int>();
            for (int i = 0; i <= index + 1; i++)
            {
                batchs.Add(i);
            }
            qCMFigureZ.X = new ZX() { Batch = batchs };

            //设置时效所在位置
            if (qCMFigureZ.ItemTimes != null)
            {
                qCMFigureZ.ItemTimes.ForEach(a => a.Batch = 999);
                if (itemtimesindex.Count <= 0)
                {
                    //if (lBQCItemTimes.Count(a => a.StartDate > dateTimeMax) > 0)
                    //{
                    //    qCMFigureZ.ItemTimes.ForEach(a => a.Batch = batchs.Last());
                    //}
                    //else
                    //{
                    //    qCMFigureZ.ItemTimes.ForEach(a => a.Batch = 1);
                    //}
                }
                else
                {
                    //qCMFigureZ.ItemTimes.ForEach(a => a.Batch = batchs.Last());
                    foreach (var item in itemtimesindex)
                    {
                        foreach (var ItemTime in qCMFigureZ.ItemTimes)
                        {
                            //if (ItemTime.Batch == batchs.Last())
                            //{
                            //    ItemTime.Batch = item.Value;
                            //}
                            if (ItemTime.DateTime == item.Key)
                            {
                                ItemTime.Batch = item.Value;
                                //break;
                            }
                        }
                    }
                }
            }

            return qCMFigureZ;
        }
        /// <summary>
        /// 值范围图
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureValueRange QCMFigureValueRange(long qCItemId, DateTime startDate, DateTime endDate)
        {
            LBQCItem lBQCItem = IDLBQCItemDao.Get(qCItemId);
            LBEquip lBEquip = IDLBEquipDao.Get(lBQCItem.LBQCMaterial.LBEquip.Id);
            string lisdatawhere = "LBQCItem.Id=" + qCItemId + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            string qctimewhere = "LBQCItem.Id=" + qCItemId + "  and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qctimewhere).OrderBy(a => a.StartDate).ToList();
            QCMFigureValueRange QCMFigureValueRange = new QCMFigureValueRange();
            List<long> tiemsids = new List<long>();//存储时效id
            List<double> qcDataValues = new List<double>();//存储数据结果

            //Y轴
            double? MagMax = null;//Y轴最大值
            double? MagMin = null; //Y轴最小值
            double MagMaxSpacing = 0;// 每个时效的间距
            double MagMinSpacing = 0;// 每个时效的间距
            foreach (var item in lisQCDatas)
            {
                qcDataValues.Add(item.QuanValue);
            }
            if (lBQCItemTimes.Count > 0)
            {
                //计算图形Y
                foreach (var item in lBQCItemTimes)
                {
                    List<double> Maxvalue = new List<double>();
                    List<double> Minvalue = new List<double>();
                    if (item.HHValue != 999999)
                    {
                        Maxvalue.Add(item.HHValue);
                    }
                    if (item.HValue != 999999)
                    {
                        Maxvalue.Add(item.HValue);
                    }
                    if (item.Target != 999999)
                    {
                        Maxvalue.Add(item.Target);
                    }
                    if (item.LLValue != -999999)
                    {
                        Minvalue.Add(item.LLValue);
                    }
                    if (item.LValue != -999999)
                    {
                        Minvalue.Add(item.LValue);
                    }
                    if (item.Target != -999999)
                    {
                        Minvalue.Add(item.Target);
                    }
                    var maxv = Maxvalue.Max();
                    var minv = Minvalue.Min();
                    //如果最大值等于最小值 间距为1
                    double spacing = 0;
                    if (maxv == minv)
                    {
                        //如果相等 最大值等于结果数据的最大值 最小值等于结果数据的最小值
                        maxv = qcDataValues.Count > 0 ? qcDataValues.Max() : 0;
                        minv = qcDataValues.Count > 0 ? qcDataValues.Min() : 0;
                        spacing = 1;
                    }
                    else
                    {
                        spacing = (maxv - minv) / 6;
                    }
                    //图形范围
                    if (!MagMax.HasValue || MagMax.Value < maxv + spacing)
                    {
                        MagMax = maxv + spacing;
                        MagMaxSpacing = spacing;
                    }
                    if (!MagMin.HasValue || MagMin.Value > minv + spacing)
                    {
                        MagMin = minv - spacing;
                        MagMinSpacing = spacing;
                    }
                }

                for (int i = 0; i < lBQCItemTimes.Count; i++)
                {
                    var qcitemtime = lBQCItemTimes[i];
                    List<QCMFigureValueRangeY_Data> QCMFigureValueRangeY_Datas = new List<QCMFigureValueRangeY_Data>();
                    //靶值线
                    QCMFigureValueRangeY_Datas.Add(new QCMFigureValueRangeY_Data() { Name = "Target", Value = qcitemtime.Target.ToString(), SDValue = "" });
                    //警告线
                    if (qcitemtime.HValue != qcitemtime.HHValue && qcitemtime.HValue != 999999)
                    {
                        QCMFigureValueRangeY_Datas.Add(new QCMFigureValueRangeY_Data() { Name = "警告", Value = qcitemtime.HValue.ToString(), SDValue = "" });//(qcitemtime.Target + spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    if (qcitemtime.LValue != qcitemtime.LLValue && qcitemtime.LValue != -999999)
                    {
                        QCMFigureValueRangeY_Datas.Add(new QCMFigureValueRangeY_Data() { Name = "警告", Value = qcitemtime.LValue.ToString(), SDValue = "" });//(qcitemtime.Target - spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    //失控线
                    if (qcitemtime.HHValue != 999999)
                    {
                        QCMFigureValueRangeY_Datas.Add(new QCMFigureValueRangeY_Data() { Name = "失控", Value = qcitemtime.HHValue.ToString(), SDValue = "" });//(qcitemtime.Target + 2 * spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    if (qcitemtime.LLValue != -999999)
                    {
                        QCMFigureValueRangeY_Datas.Add(new QCMFigureValueRangeY_Data() { Name = "失控", Value = qcitemtime.LLValue.ToString(), SDValue = "" });//(qcitemtime.Target - 2 * spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    //Y
                    QCMFigureValueRangeY QCMFigureValueRangeY = new QCMFigureValueRangeY();
                    QCMFigureValueRangeY.Max = MagMax.Value;
                    QCMFigureValueRangeY.Min = MagMin.Value;
                    QCMFigureValueRangeY.DateTime = qcitemtime.StartDate;
                    QCMFigureValueRangeY.Data = QCMFigureValueRangeY_Datas;
                    if (i == 0)
                    {
                        QCMFigureValueRange.Y = QCMFigureValueRangeY;
                    }
                    else
                    {
                        if (QCMFigureValueRange.ItemTimes == null)
                        {
                            QCMFigureValueRange.ItemTimes = new List<QCMFigureValueRangeY>();
                        }
                        QCMFigureValueRange.ItemTimes.Add(QCMFigureValueRangeY);
                    }
                }
            }

            //数据整理
            int index = 0;
            List<int> batch = new List<int>(); //批次
            List<DateTime> dateTimes = new List<DateTime>(); //时间
            SortedList<DateTime, int> itemtimesindex = new SortedList<DateTime, int>();//存储时效位置
            foreach (var item in lisQCDatas.OrderBy(a => a.ReceiveTime))
            {
                if (item.BUse)
                {
                    index++;
                }
                QCMFigureValueRange_Data QCMFigureValueRange_Data = new QCMFigureValueRange_Data();
                QCMFigureValueRange_Data.ReportValue = item.ReportValue;
                QCMFigureValueRange_Data.BUse = item.BUse;
                QCMFigureValueRange_Data.QcDataId = item.Id.ToString();
                QCMFigureValueRange_Data.DateTime = item.ReceiveTime;
                QCMFigureValueRange_Data.Batch = index;
                QCMFigureValueRange_Data.rolseType = item.LoseType;
                QCMFigureValueRange_Data.loseRule = item.Loserule;
                QCMFigureValueRange_Data.People = item.Operator;
                var times = lBQCItemTimes.Where(a => a.StartDate <= item.ReceiveTime && (a.EndDate >= item.ReceiveTime || !a.EndDate.HasValue));
                QCMFigureValueRange_Data.YValue = item.QuanValue;
                if (times.Count() > 0)
                {
                    if (!itemtimesindex.ContainsKey(times.First().StartDate) && QCMFigureValueRange.Y.DateTime != times.First().StartDate)
                    {
                        itemtimesindex.Add(times.First().StartDate, index);
                    }
                    //如果值超过图的边界
                    if (QCMFigureValueRange_Data.YValue > MagMax.Value - MagMaxSpacing)
                    {
                        if (MagMax < 0)
                        {
                            QCMFigureValueRange_Data.YValue = MagMax.Value + Math.Abs(MagMaxSpacing * 0.2);
                        }
                        else
                        {
                            QCMFigureValueRange_Data.YValue = MagMax.Value - Math.Abs(MagMaxSpacing * 0.2);
                        }
                    }
                    if (QCMFigureValueRange_Data.YValue <= MagMin.Value + MagMinSpacing)
                    {
                        if (MagMin.Value < 0)
                        {
                            QCMFigureValueRange_Data.YValue = MagMin.Value + Math.Abs(MagMaxSpacing * 0.2);
                        }
                        else
                        {
                            QCMFigureValueRange_Data.YValue = MagMin.Value + Math.Abs(MagMaxSpacing * 0.2);
                        }
                    }
                    QCMFigureValueRange_Data.SD = times.First().SD.ToString();
                    QCMFigureValueRange_Data.Target = times.First().Target.ToString();
                }
                QCMFigureValueRange_Data.ItemName = item.LBQCItem.LBItem.CName;
                QCMFigureValueRange_Data.QCMaterial = item.LBQCItem.LBQCMaterial.CName;
                QCMFigureValueRange_Data.Equip = lBEquip.CName;
                QCMFigureValueRange_Data.EValue = item.EValue;
                if (QCMFigureValueRange.Data == null)
                {
                    QCMFigureValueRange.Data = new List<QCMFigureValueRange_Data>();
                }
                QCMFigureValueRange.Data.Add(QCMFigureValueRange_Data);
                batch.Add(index);
                dateTimes.Add(item.ReceiveTime);
            }


            QCMFigureValueRange.X = new QCMFigureValueRangeX();
            batch.Insert(0, 0);
            batch.Add(++index);
            QCMFigureValueRange.X.Batch = batch.Distinct().ToList();
            dateTimes.Insert(0, startDate);
            dateTimes.Add(endDate);
            QCMFigureValueRange.X.Date = dateTimes.Distinct().ToList();

            //设置时效所在位置
            if (QCMFigureValueRange.ItemTimes != null)
            {
                if (itemtimesindex.Count <= 0)
                {
                    QCMFigureValueRange.ItemTimes.ForEach(a => a.Batch = 1);
                }
                else
                {
                    foreach (var item in itemtimesindex)
                    {
                        foreach (var ItemTime in QCMFigureValueRange.ItemTimes)
                        {
                            if (ItemTime.Batch == 0)
                            {
                                ItemTime.Batch = item.Value;
                            }
                            if (ItemTime.DateTime == item.Key)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return QCMFigureValueRange;
        }
        /// <summary>
        /// Monica图
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureValueMonica QCMFigureValueMonica(long qCItemId, DateTime startDate, DateTime endDate)
        {
            LBQCItem lBQCItem = IDLBQCItemDao.Get(qCItemId);
            LBEquip lBEquip = IDLBEquipDao.Get(lBQCItem.LBQCMaterial.LBEquip.Id);
            string lisdatawhere = "LBQCItem.Id=" + qCItemId + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            string qctimewhere = "LBQCItem.Id=" + qCItemId + "  and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qctimewhere).OrderBy(a => a.StartDate).ToList();
            QCMFigureValueMonica QCMFigureValueMonica = new QCMFigureValueMonica();
            List<long> tiemsids = new List<long>();//存储时效id
            List<double> qcDataValues = new List<double>();//存储数据结果

            //Y轴
            double? MagMax = null;//Y轴最大值
            double? MagMin = null; //Y轴最小值
            double MagMaxSpacing = 0;// 每个时效的间距
            double MagMinSpacing = 0;// 每个时效的间距
            foreach (var item in lisQCDatas)
            {
                qcDataValues.Add(item.QuanValue);
            }
            if (lBQCItemTimes.Count > 0)
            {
                //计算图形Y
                foreach (var item in lBQCItemTimes)
                {
                    List<double> Maxvalue = new List<double>();
                    List<double> Minvalue = new List<double>();
                    if (item.HHValue != 999999)
                    {
                        Maxvalue.Add(item.HHValue);
                    }
                    if (item.HValue != 999999)
                    {
                        Maxvalue.Add(item.HValue);
                    }
                    if (item.Target != 999999)
                    {
                        Maxvalue.Add(item.Target);
                    }
                    if (item.LLValue != -999999)
                    {
                        Minvalue.Add(item.LLValue);
                    }
                    if (item.LValue != -999999)
                    {
                        Minvalue.Add(item.LValue);
                    }
                    if (item.Target != -999999)
                    {
                        Minvalue.Add(item.Target);
                    }
                    var maxv = Maxvalue.Max();
                    var minv = Minvalue.Min();
                    //如果最大值等于最小值 间距为1
                    double spacing = 0;
                    if (maxv == minv)
                    {
                        //如果相等 最大值等于结果数据的最大值 最小值等于结果数据的最小值
                        maxv = qcDataValues.Max();
                        minv = qcDataValues.Min();
                        spacing = 1;
                    }
                    else
                    {
                        spacing = (maxv - minv) / 6;
                    }
                    //图形范围
                    if (!MagMax.HasValue || MagMax.Value < maxv + spacing)
                    {
                        MagMax = maxv + spacing;
                        MagMaxSpacing = spacing;
                    }
                    if (!MagMin.HasValue || MagMin.Value > minv + spacing)
                    {
                        MagMin = minv - spacing;
                        MagMinSpacing = spacing;
                    }
                }

                for (int i = 0; i < lBQCItemTimes.Count; i++)
                {
                    var qcitemtime = lBQCItemTimes[i];
                    List<QCMFigureValueMonicaY_Data> QCMFigureValueMonicaY_Datas = new List<QCMFigureValueMonicaY_Data>();
                    //靶值线
                    QCMFigureValueMonicaY_Datas.Add(new QCMFigureValueMonicaY_Data() { Name = "Target", Value = qcitemtime.Target.ToString(), SDValue = "" });
                    //警告线
                    if (qcitemtime.HValue != qcitemtime.HHValue && qcitemtime.HValue != 999999)
                    {
                        QCMFigureValueMonicaY_Datas.Add(new QCMFigureValueMonicaY_Data() { Name = "警告", Value = qcitemtime.HValue.ToString(), SDValue = "" });//(qcitemtime.Target + spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    if (qcitemtime.LValue != qcitemtime.LLValue && qcitemtime.LValue != -999999)
                    {
                        QCMFigureValueMonicaY_Datas.Add(new QCMFigureValueMonicaY_Data() { Name = "警告", Value = qcitemtime.LValue.ToString(), SDValue = "" });//(qcitemtime.Target - spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    //失控线
                    if (qcitemtime.HHValue != 999999)
                    {
                        QCMFigureValueMonicaY_Datas.Add(new QCMFigureValueMonicaY_Data() { Name = "失控", Value = qcitemtime.HHValue.ToString(), SDValue = "" });//(qcitemtime.Target + 2 * spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    if (qcitemtime.LLValue != -999999)
                    {
                        QCMFigureValueMonicaY_Datas.Add(new QCMFigureValueMonicaY_Data() { Name = "失控", Value = qcitemtime.LLValue.ToString(), SDValue = "" });//(qcitemtime.Target - 2 * spacings.Where(a => a.Key == qcitemtime.Id).First().Value).ToString()
                    }
                    //Y
                    QCMFigureValueMonicaY QCMFigureValueMonicaY = new QCMFigureValueMonicaY();
                    QCMFigureValueMonicaY.Max = MagMax.Value;
                    QCMFigureValueMonicaY.Min = MagMin.Value;
                    QCMFigureValueMonicaY.DateTime = qcitemtime.StartDate;
                    QCMFigureValueMonicaY.Data = QCMFigureValueMonicaY_Datas;
                    if (i == 0)
                    {
                        QCMFigureValueMonica.Y = QCMFigureValueMonicaY;
                    }
                    else
                    {
                        if (QCMFigureValueMonica.ItemTimes == null)
                        {
                            QCMFigureValueMonica.ItemTimes = new List<QCMFigureValueMonicaY>();
                        }
                        QCMFigureValueMonica.ItemTimes.Add(QCMFigureValueMonicaY);
                    }
                }
            }

            //数据整理
            int index = 0;
            List<int> batch = new List<int>(); //批次
            List<DateTime> dateTimes = new List<DateTime>(); //时间
            SortedList<DateTime, int> itemtimesindex = new SortedList<DateTime, int>();//存储时效位置
            var lisQCDatasOrder = lisQCDatas.OrderBy(a => a.ReceiveTime);
            foreach (var lisQCDataOrder in lisQCDatasOrder.GroupBy(gp => gp.ReceiveTime.ToString("yyyy-MM-dd"))) //根据一天分为一个批次
            {
                index++;
                List<double> ToDayDatas = new List<double>();//存储今天数据
                List<QCMFigureValueMonica_Data> qCMFigureValueMonica_Datas = new List<QCMFigureValueMonica_Data>();
                //当天的时效
                var times = lBQCItemTimes.Where(a => a.StartDate <= lisQCDataOrder.First().ReceiveTime && (a.EndDate >= lisQCDataOrder.First().ReceiveTime || !a.EndDate.HasValue));
                foreach (var item in lisQCDataOrder)
                {
                    if (item.BUse)
                    {
                        ToDayDatas.Add(item.QuanValue);
                    }
                    QCMFigureValueMonica_Data QCMFigureValueMonica_Data = new QCMFigureValueMonica_Data();
                    QCMFigureValueMonica_Data.ReportValue = item.ReportValue;
                    QCMFigureValueMonica_Data.BUse = item.BUse;
                    QCMFigureValueMonica_Data.QcDataId = item.Id.ToString();
                    QCMFigureValueMonica_Data.DateTime = item.ReceiveTime;
                    QCMFigureValueMonica_Data.Batch = index;
                    QCMFigureValueMonica_Data.rolseType = item.LoseType;
                    QCMFigureValueMonica_Data.loseRule = item.Loserule;
                    QCMFigureValueMonica_Data.People = item.Operator;
                    QCMFigureValueMonica_Data.YValue = item.QuanValue;
                    if (times.Count() > 0)
                    {
                        if (!itemtimesindex.ContainsKey(times.First().StartDate) && QCMFigureValueMonica.Y.DateTime != times.First().StartDate)
                        {
                            itemtimesindex.Add(times.First().StartDate, index);
                        }
                        //如果值超过图的边界
                        if (QCMFigureValueMonica_Data.YValue > MagMax.Value - MagMaxSpacing)
                        {
                            if (MagMax < 0)
                            {
                                QCMFigureValueMonica_Data.YValue = MagMax.Value + Math.Abs(MagMaxSpacing * 0.2);
                            }
                            else
                            {
                                QCMFigureValueMonica_Data.YValue = MagMax.Value - Math.Abs(MagMaxSpacing * 0.2);
                            }
                        }
                        if (QCMFigureValueMonica_Data.YValue <= MagMin.Value + MagMinSpacing)
                        {
                            if (MagMin.Value < 0)
                            {
                                QCMFigureValueMonica_Data.YValue = MagMin.Value + Math.Abs(MagMaxSpacing * 0.2);
                            }
                            else
                            {
                                QCMFigureValueMonica_Data.YValue = MagMin.Value + Math.Abs(MagMaxSpacing * 0.2);
                            }
                        }
                        QCMFigureValueMonica_Data.SD = times.First().SD.ToString();
                        QCMFigureValueMonica_Data.Target = times.First().Target.ToString();
                    }
                    QCMFigureValueMonica_Data.ItemName = item.LBQCItem.LBItem.CName;
                    QCMFigureValueMonica_Data.QCMaterial = item.LBQCItem.LBQCMaterial.CName;
                    QCMFigureValueMonica_Data.Equip = lBEquip.CName;
                    QCMFigureValueMonica_Data.EValue = item.EValue;
                    qCMFigureValueMonica_Datas.Add(QCMFigureValueMonica_Data);
                }
                //如果数据只有一条不加入均值
                if (QCMFigureValueMonica.Data == null)
                {
                    QCMFigureValueMonica.Data = new List<QCMFigureValueMonica_Data>();
                }
                if (ToDayDatas.Count() > 1)
                {
                    //当前的均值
                    QCMFigureValueMonica.Data.Add(new QCMFigureValueMonica_Data()
                    {
                        ReportValue = ToDayDatas.Average().ToString(),
                        YValue = ToDayDatas.Average(),
                        ToDayTarget = ToDayDatas.Average().ToString(),
                        DateTime = qCMFigureValueMonica_Datas.First().DateTime,
                        Batch = index,
                        SD = qCMFigureValueMonica_Datas.First().SD,
                        Target = qCMFigureValueMonica_Datas.First().Target,
                        ItemName = qCMFigureValueMonica_Datas.First().ItemName,
                        QCMaterial = qCMFigureValueMonica_Datas.First().QCMaterial,
                        Equip = lBEquip.CName,
                        EValue = qCMFigureValueMonica_Datas.First().EValue
                    });
                    QCMFigureValueMonica.Data.AddRange(qCMFigureValueMonica_Datas);
                    //第二次加入当前的均值  --前台要求
                    QCMFigureValueMonica.Data.Add(new QCMFigureValueMonica_Data()
                    {
                        ReportValue = ToDayDatas.Average().ToString(),
                        YValue = ToDayDatas.Average(),
                        ToDayTarget = ToDayDatas.Average().ToString(),
                        DateTime = qCMFigureValueMonica_Datas.First().DateTime,
                        Batch = index,
                        SD = qCMFigureValueMonica_Datas.First().SD,
                        Target = qCMFigureValueMonica_Datas.First().Target,
                        ItemName = qCMFigureValueMonica_Datas.First().ItemName,
                        QCMaterial = qCMFigureValueMonica_Datas.First().QCMaterial,
                        Equip = lBEquip.CName,
                        EValue = qCMFigureValueMonica_Datas.First().EValue
                    });
                }
                else
                {
                    QCMFigureValueMonica.Data.AddRange(qCMFigureValueMonica_Datas);
                }

                batch.Add(index);
            }

            QCMFigureValueMonica.X = new QCMFigureValueMonicaX();
            batch.Insert(0, 0);
            batch.Add(++index);
            QCMFigureValueMonica.X.Batch = batch.Distinct().ToList();
            dateTimes.Insert(0, startDate);
            dateTimes.Add(endDate);
            QCMFigureValueMonica.X.Date = dateTimes.Distinct().ToList();

            //设置时效所在位置
            if (QCMFigureValueMonica.ItemTimes != null)
            {
                if (itemtimesindex.Count <= 0)
                {
                    QCMFigureValueMonica.ItemTimes.ForEach(a => a.Batch = 1);
                }
                else
                {
                    foreach (var item in itemtimesindex)
                    {
                        foreach (var ItemTime in QCMFigureValueMonica.ItemTimes)
                        {
                            if (ItemTime.Batch == 0)
                            {
                                ItemTime.Batch = item.Value;
                            }
                            if (ItemTime.DateTime == item.Key)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return QCMFigureValueMonica;
        }
        /// <summary>
        /// 定性图
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureQualitative QCMFigureQualitative(long qCItemId, DateTime startDate, DateTime endDate)
        {
            LBQCItem lBQCItem = IDLBQCItemDao.Get(qCItemId);
            LBEquip lBEquip = IDLBEquipDao.Get(lBQCItem.LBQCMaterial.LBEquip.Id);
            string lisdatawhere = "LBQCItem.Id=" + qCItemId + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            string qctimewhere = "LBQCItem.Id=" + qCItemId + "  and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qctimewhere).OrderBy(a => a.StartDate).ToList();
            QCMFigureQualitative qCMFigureQualitative = new QCMFigureQualitative();

            //Y轴
            //记录y轴的坐标
            Dictionary<long, SortedList<int, string>> YData = new Dictionary<long, SortedList<int, string>>();
            if (lBQCItemTimes.Count > 0)
            {
                List<int> YMax = new List<int>();
                List<int> YMin = new List<int>();
                for (int i = 0; i < lBQCItemTimes.Count; i++)
                {
                    var qcitemtime = lBQCItemTimes[i];
                    List<QCMFigureQualitativeY_Data> QCMFigureQualitativeY_Datas = new List<QCMFigureQualitativeY_Data>();
                    var qctimes = qcitemtime.DescAll.Split('|');
                    var Tindex = qctimes.ToList().FindIndex(a => a.IndexOf("靶值") >= 0);  //靶值所在位置
                    var index = Tindex + 1;
                    foreach (var item in qctimes)
                    {
                        index--;
                        var cm = item.Split('：');
                        QCMFigureQualitativeY_Datas.Add(new QCMFigureQualitativeY_Data() { Name = cm[0].Trim(), Value = index.ToString().Trim(), SDValue = cm[1].Trim() });
                        if (!YData.ContainsKey(qcitemtime.Id))
                        {
                            YData.Add(qcitemtime.Id, new SortedList<int, string>() { { index, cm[1].Trim() } });
                        }
                        else
                        {
                            YData[qcitemtime.Id].Add(index, cm[1].Trim());
                        }
                    }
                    //Y
                    --index;
                    YMax.Add(Tindex + 1);
                    YMin.Add(index);
                    QCMFigureQualitativeY QCMFigureQualitativeY = new QCMFigureQualitativeY();
                    QCMFigureQualitativeY.Min = index;
                    QCMFigureQualitativeY.DateTime = qcitemtime.StartDate;
                    QCMFigureQualitativeY.Data = QCMFigureQualitativeY_Datas;
                    if (i == 0)
                    {
                        qCMFigureQualitative.Y = QCMFigureQualitativeY;
                    }
                    else
                    {
                        if (qCMFigureQualitative.ItemTimes == null)
                        {
                            qCMFigureQualitative.ItemTimes = new List<QCMFigureQualitativeY>();
                        }
                        qCMFigureQualitative.ItemTimes.Add(QCMFigureQualitativeY);
                    }
                }
                //设置所有时效中最高的Y值
                qCMFigureQualitative.Y.Max = YMax.Max();
                if (qCMFigureQualitative.ItemTimes != null)
                {
                    foreach (var item in qCMFigureQualitative.ItemTimes)
                    {
                        item.Max = YMax.Max();
                        item.Min = YMin.Min();
                    }
                }
            }
            else
            {
                ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.QCMFigureQualitative 未找到质控时效信息!");
            }
            int QCindex = 0;
            //数据处理
            List<int> batch = new List<int>();
            SortedList<DateTime, int> itemtimesindex = new SortedList<DateTime, int>();//存储时效位置
            foreach (var item in lisQCDatas.OrderBy(a => a.ReceiveTime))
            {
                if (item.BUse)
                {
                    QCindex++;
                }
                QCMFigureQualitative_Data QCMFigureQualitative_Data = new QCMFigureQualitative_Data();
                QCMFigureQualitative_Data.ReportValue = item.ReportValue;
                QCMFigureQualitative_Data.BUse = item.BUse;
                QCMFigureQualitative_Data.QcDataId = item.Id.ToString();
                QCMFigureQualitative_Data.DateTime = item.ReceiveTime;
                QCMFigureQualitative_Data.Batch = QCindex;
                QCMFigureQualitative_Data.rolseType = item.LoseType;
                QCMFigureQualitative_Data.loseRule = item.Loserule;
                QCMFigureQualitative_Data.People = item.Operator;
                //当天的时效
                var times = lBQCItemTimes.Where(a => a.StartDate <= item.ReceiveTime && (a.EndDate >= item.ReceiveTime || !a.EndDate.HasValue) && a.LBQCItem.Id == item.LBQCItem.Id);
                if (times.Count() > 0)
                {
                    if (!itemtimesindex.ContainsKey(times.First().StartDate) && qCMFigureQualitative.Y.DateTime != times.First().StartDate)
                    {
                        itemtimesindex.Add(times.First().StartDate, QCindex);
                    }
                    //如果数据值匹配到y轴的坐标  把坐标值放到YValue
                    var ydatas = YData.Where(a => a.Key == times.First().Id).First().Value;
                    KeyValuePair<int, string> ydata = new KeyValuePair<int, string>();
                    foreach (var a in ydatas)
                    {
                        double rvalue = 0;
                        bool isNum = double.TryParse(item.ReportValue, out rvalue);
                        double yvalue = 0;
                        bool YDaataIsNum = double.TryParse(a.Value, out yvalue);
                        if (isNum && YDaataIsNum)
                        {
                            if (yvalue == rvalue)
                            {
                                ydata = a;
                                break;
                            }
                        }
                        else
                        {
                            if (a.Value == item.ReportValue)
                            {
                                ydata = a;
                                break;
                            }
                        }
                    }
                    if (ydata.Value != null)
                    {
                        QCMFigureQualitative_Data.YValue = ydata.Key;
                    }
                    else
                    {
                        var othervalue = ydatas.Where(a => a.Value == "其它").First();
                        QCMFigureQualitative_Data.YValue = othervalue.Key;
                    }
                }

                QCMFigureQualitative_Data.ItemName = item.LBQCItem.LBItem.CName;
                QCMFigureQualitative_Data.QCMaterial = item.LBQCItem.LBQCMaterial.CName;
                QCMFigureQualitative_Data.Equip = lBEquip.CName;
                QCMFigureQualitative_Data.EValue = item.EValue;
                if (qCMFigureQualitative.Data == null)
                {
                    qCMFigureQualitative.Data = new List<QCMFigureQualitative_Data>();
                }
                qCMFigureQualitative.Data.Add(QCMFigureQualitative_Data);
                batch.Add(QCindex);
            }

            qCMFigureQualitative.X = new QCMFigureQualitativeX();
            batch.Insert(0, 0);
            batch.Add(++QCindex);
            qCMFigureQualitative.X.Batch = batch.Distinct().ToList();

            //设置时效所在位置
            if (qCMFigureQualitative.ItemTimes != null)
            {
                if (itemtimesindex.Count <= 0)
                {
                    if (lBQCItemTimes.Count(a => a.StartDate > qCMFigureQualitative.Data.Last().DateTime) > 0)
                    {
                        qCMFigureQualitative.ItemTimes.ForEach(a => a.Batch = QCindex);
                    }
                    else
                    {
                        qCMFigureQualitative.ItemTimes.ForEach(a => a.Batch = 1);
                    }
                }
                else
                {
                    qCMFigureQualitative.ItemTimes.ForEach(a => a.Batch = QCindex);
                    foreach (var item in itemtimesindex)
                    {
                        foreach (var ItemTime in qCMFigureQualitative.ItemTimes)
                        {
                            if (ItemTime.Batch == QCindex)
                            {
                                ItemTime.Batch = item.Value;
                            }
                            if (ItemTime.DateTime == item.Key)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return qCMFigureQualitative;
        }
        /// <summary>
        /// Youden 图
        /// </summary>
        /// <param name="qCItemIds">2个值，第一个时效为x轴，第二个时效为y轴</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureYouden QCMFigureYouden(List<long> qCItemIds, DateTime startDate, DateTime endDate)
        {
            Dictionary<long, List<Z_Data>> QCItemDatas = new Dictionary<long, List<Z_Data>>(); //存储不同质控物的数据
            IList<LBQCItem> lBQCItems = IDLBQCItemDao.GetListByHQL("Id in (" + string.Join(",", qCItemIds) + ")");
            List<long> qcmdis = new List<long>();
            lBQCItems.ToList().ForEach(a =>
            {
                qcmdis.Add(a.LBQCMaterial.Id);
            });
            LBEquip lBEquip = IDLBEquipDao.Get(lBQCItems.First().LBQCMaterial.LBEquip.Id);//2个都是同一个仪器
            IList<LBQCMaterial> lBQCMaterials = IDLBQCMaterialDao.GetListByHQL("Id in (" + string.Join(",", qcmdis) + ")");
            string lisdatawhere = "LBQCItem.Id in (" + string.Join(",", qCItemIds) + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            string qctimewhere = "LBQCItem.Id in (" + string.Join(",", qCItemIds) + ") and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qctimewhere).OrderBy(a => a.StartDate).ToList();
            QCMFigureYouden QCMFigureYouden = new QCMFigureYouden();
            //Y轴
            if (lBQCItemTimes.Count > 0 && lBQCItemTimes.Count(a => a.LBQCItem.Id == qCItemIds.Last()) > 0)
            {
                var qcitemtime = lBQCItemTimes.First();
                List<QCMFigureYoudenY_Data> ZY_Datas = new List<QCMFigureYoudenY_Data>();
                //计算 N倍标准差
                for (int j = 1; j < 4; j++)
                {
                    QCMFigureYoudenY_Data ZY_Data = new QCMFigureYoudenY_Data();
                    ZY_Data.Name = "+" + j + "SD";
                    ZY_Data.SDValue = (qcitemtime.Target + j * qcitemtime.SD).ToString();
                    ZY_Data.Value = j.ToString();
                    ZY_Datas.Add(ZY_Data);
                    QCMFigureYoudenY_Data ZY_Data1 = new QCMFigureYoudenY_Data();
                    ZY_Data1.Name = "-" + j + "SD";
                    ZY_Data1.Value = (-j).ToString();
                    ZY_Data1.SDValue = (qcitemtime.Target - j * qcitemtime.SD).ToString();
                    ZY_Datas.Add(ZY_Data1);
                }
                QCMFigureYoudenY ZY = new QCMFigureYoudenY();
                ZY.Max = 4;
                ZY.Min = -4;
                ZY.QCMName = lBQCMaterials.Where(a => a.Id == lBQCItems.Where(b => b.Id == qCItemIds.Last()).First().LBQCMaterial.Id).First().CName;
                ZY_Datas.Add(new QCMFigureYoudenY_Data() { Name = "Target", Value = "0", SDValue = qcitemtime.Target.ToString() });
                ZY.Data = ZY_Datas;
                QCMFigureYouden.Y = ZY;
            }
            //X轴
            if (lBQCItemTimes.Count > 0 && lBQCItemTimes.Count(a => a.LBQCItem.Id == qCItemIds.First()) > 0)
            {
                var qcitemtime = lBQCItemTimes.Where(a => a.LBQCItem.Id == qCItemIds.First()).First();
                List<QCMFigureYoudenY_Data> ZY_Datas = new List<QCMFigureYoudenY_Data>();
                //计算 N倍标准差
                for (int j = 1; j < 4; j++)
                {
                    QCMFigureYoudenY_Data ZY_Data = new QCMFigureYoudenY_Data();
                    ZY_Data.Name = "+" + j + "SD";
                    ZY_Data.SDValue = (qcitemtime.Target + j * qcitemtime.SD).ToString();
                    ZY_Data.Value = j.ToString();
                    ZY_Datas.Add(ZY_Data);
                    QCMFigureYoudenY_Data ZY_Data1 = new QCMFigureYoudenY_Data();
                    ZY_Data1.Name = "-" + j + "SD";
                    ZY_Data1.Value = (-j).ToString();
                    ZY_Data1.SDValue = (qcitemtime.Target - j * qcitemtime.SD).ToString();
                    ZY_Datas.Add(ZY_Data1);
                }
                QCMFigureYoudenY ZY = new QCMFigureYoudenY();
                ZY.Max = 4;
                ZY.Min = -4;
                ZY.QCMName = lBQCMaterials.Where(a => a.Id == lBQCItems.Where(b => b.Id == qCItemIds.First()).First().LBQCMaterial.Id).First().CName;
                ZY_Datas.Add(new QCMFigureYoudenY_Data() { Name = "Target", Value = "0", SDValue = qcitemtime.Target.ToString() });
                ZY.Data = ZY_Datas;
                QCMFigureYouden.X = ZY;
            }
            //数据
            int index = 0;
            //根据每一天区分不同浓度的批次
            for (; startDate < endDate.AddDays(1); startDate = startDate.AddDays(1))
            {
                List<QCMFigureYouden_Data> z_Datas = new List<QCMFigureYouden_Data>();
                //当天2个浓度的数据
                var qcdatadays = lisQCDatas.Where(a => a.ReceiveTime >= startDate && a.ReceiveTime < startDate.AddDays(1));
                var Xqcdatadays = qcdatadays.Where(a => a.LBQCItem.Id == qCItemIds.First()); //x轴坐标数据
                var Yqcdatadays = qcdatadays.Where(a => a.LBQCItem.Id == qCItemIds.Last()); //y轴坐标数据
                //如果当天 2个浓度都存在数据 
                if (Xqcdatadays.Count() > 0 && Yqcdatadays.Count() > 0)
                {
                    //时效
                    var Xtimes = lBQCItemTimes.Where(a => a.StartDate <= startDate && (a.EndDate >= startDate || !a.EndDate.HasValue) && a.LBQCItem.Id == qCItemIds.First());
                    var Ytimes = lBQCItemTimes.Where(a => a.StartDate <= startDate && (a.EndDate >= startDate || !a.EndDate.HasValue) && a.LBQCItem.Id == qCItemIds.Last());
                    //匹配当天同批次数据个数如果有多余的则忽略
                    int NumberOfMatchingData = Xqcdatadays.Count() > Yqcdatadays.Count() ? Yqcdatadays.Count() : Xqcdatadays.Count();
                    for (int i = 0; i < NumberOfMatchingData; i++)
                    {
                        index++;
                        QCMFigureYouden_Data cMFigureYouden_Data = new QCMFigureYouden_Data();
                        cMFigureYouden_Data.XReportValue = Xqcdatadays.ElementAt(i).ReportValue;
                        cMFigureYouden_Data.YReportValue = Yqcdatadays.ElementAt(i).ReportValue;
                        //质控判定
                        List<string> rolseTypes = new List<string>();
                        rolseTypes.Add(Xqcdatadays.ElementAt(i).LoseType);
                        rolseTypes.Add(Yqcdatadays.ElementAt(i).LoseType);
                        cMFigureYouden_Data.rolseType = "在控";
                        if (rolseTypes.Contains("警告"))
                        {
                            cMFigureYouden_Data.rolseType = "警告";
                        }
                        if (rolseTypes.Contains("失控"))
                        {
                            cMFigureYouden_Data.rolseType = "失控";
                        }
                        //X
                        cMFigureYouden_Data.XValue = Xqcdatadays.ElementAt(i).QuanValue;
                        if (Xtimes.Count() > 0)
                        {
                            cMFigureYouden_Data.XValue = (Xqcdatadays.ElementAt(i).QuanValue - Xtimes.First().Target) / Xtimes.First().SD;

                            if (cMFigureYouden_Data.XValue >= QCMFigureYouden.X.Max)
                            {
                                cMFigureYouden_Data.XValue = QCMFigureYouden.X.Max - 0.2;
                            }
                            if (cMFigureYouden_Data.XValue <= QCMFigureYouden.X.Min)
                            {
                                cMFigureYouden_Data.XValue = QCMFigureYouden.X.Min + 0.2;
                            }
                        }
                        //Y
                        cMFigureYouden_Data.YValue = Yqcdatadays.ElementAt(i).QuanValue;
                        if (Ytimes.Count() > 0)
                        {
                            cMFigureYouden_Data.YValue = (Yqcdatadays.ElementAt(i).QuanValue - Ytimes.First().Target) / Ytimes.First().SD;

                            if (cMFigureYouden_Data.YValue >= QCMFigureYouden.Y.Max)
                            {
                                cMFigureYouden_Data.YValue = QCMFigureYouden.Y.Max - 0.2;
                            }
                            if (cMFigureYouden_Data.YValue <= QCMFigureYouden.Y.Min)
                            {
                                cMFigureYouden_Data.YValue = QCMFigureYouden.Y.Min + 0.2;
                            }
                        }
                        cMFigureYouden_Data.Batch = index;
                        cMFigureYouden_Data.DateTime = startDate;
                        //加入数据
                        if (QCMFigureYouden.Data == null)
                        {
                            QCMFigureYouden.Data = new List<QCMFigureYouden_Data>();
                        }
                        QCMFigureYouden.Data.Add(cMFigureYouden_Data);
                    }
                }
            }

            return QCMFigureYouden;
        }
        /// <summary>
        /// 正太分布图
        /// </summary>
        /// <param name="qCItemIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureNormalDistribution QCMFigureNormalDistribution(List<long> qCItemIds, DateTime startDate, DateTime endDate)
        {
            LBQCItem lBQCItem = IDLBQCItemDao.Get(qCItemIds.First()); //2个都是同一个仪器
            string lisdatawhere = "LBQCItem.Id in (" + string.Join(",", qCItemIds) + ") and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            lisQCDatas = lisQCDatas.Where(a => a.LoseType != "失控").ToList();
            string qctimewhere = "LBQCItem.Id in (" + string.Join(",", qCItemIds) + ") and StartDate<='" + endDate.ToString("yyyy-MM-dd") + "' and (EndDate>='" + startDate.ToString("yyyy-MM-dd") + "' or EndDate is null)";
            IList<LBQCItemTime> lBQCItemTimes = IDLBQCItemTimeDao.GetListByHQL(qctimewhere).OrderBy(a => a.StartDate).ToList();
            QCMFigureNormalDistribution QCMFigureNormalDistribution = new QCMFigureNormalDistribution();
            //X轴
            List<QCMFigureNormalDistributionY_Data> ZY_Datas = new List<QCMFigureNormalDistributionY_Data>();
            //计算 N倍标准差
            for (int j = 1; j < 4; j++)
            {
                QCMFigureNormalDistributionY_Data ZY_Data = new QCMFigureNormalDistributionY_Data();
                ZY_Data.Name = "+" + j + "SD";
                ZY_Data.Value = j.ToString();
                ZY_Datas.Add(ZY_Data);
                QCMFigureNormalDistributionY_Data ZY_Data1 = new QCMFigureNormalDistributionY_Data();
                ZY_Data1.Name = "-" + j + "SD";
                ZY_Data1.Value = (-j).ToString();
                ZY_Datas.Add(ZY_Data1);
            }
            QCMFigureNormalDistributionY ZY = new QCMFigureNormalDistributionY();
            ZY.Max = 4;
            ZY.Min = -4;
            ZY_Datas.Add(new QCMFigureNormalDistributionY_Data() { Name = "Target", Value = "0" });//, SDValue = qcitemtime.Target.ToString()
            ZY.Data = ZY_Datas;
            QCMFigureNormalDistribution.X = ZY;
            // }
            //参考线 y轴刻度
            List<double> YScale = new List<double>();
            //实际数据的Y轴刻度
            List<double> dataYScale = new List<double>();
            //参考线
            double e = 2.71828;
            int pointCount = 200;
            //ZhiFang.LabStar.Common.LogHelp.Debug("参考曲线");
            for (int i = 0; i < pointCount; i++)
            {
                QCMFigureNormalDistribution_Data qCMFigureNormalDistribution_Data = new QCMFigureNormalDistribution_Data();
                qCMFigureNormalDistribution_Data.XValue = -4 + (8.0 / pointCount) * i;
                qCMFigureNormalDistribution_Data.YValue = Math.Pow(e, -Math.Pow(qCMFigureNormalDistribution_Data.XValue / 2, 2));
                YScale.Add(qCMFigureNormalDistribution_Data.YValue); //记录y轴刻度
                if (QCMFigureNormalDistribution.ReferenceLine == null)
                {
                    QCMFigureNormalDistribution.ReferenceLine = new List<QCMFigureNormalDistribution_Data>();
                }
                //ZhiFang.LabStar.Common.LogHelp.Debug("i="+i+"    X= "+ qCMFigureNormalDistribution_Data.XValue+"  Y="+ qCMFigureNormalDistribution_Data.YValue);
                QCMFigureNormalDistribution.ReferenceLine.Add(qCMFigureNormalDistribution_Data);
            }

            //数据
            if (lisQCDatas.Count > 0)
            {
                SortedList<long, List<double>> iTargetSD = new SortedList<long, List<double>>(); //存储浓度的计算靶值和sd
                foreach (var item in lisQCDatas.GroupBy(a => a.LBQCItem.Id))
                {
                    foreach (var iteme in lBQCItemTimes)
                    {
                        List<LisQCData> qcdata = new List<LisQCData>(); ;
                        if (iteme.EndDate.HasValue)
                        {
                            qcdata = item.Where(i => i.LBQCItem.Id == iteme.LBQCItem.Id && i.ReceiveTime > iteme.StartDate && i.ReceiveTime < iteme.EndDate).ToList();
                        }
                        else
                        {
                            qcdata = item.Where(i => i.LBQCItem.Id == iteme.LBQCItem.Id && i.ReceiveTime > iteme.StartDate).ToList();
                        }
                        //如果这个时效段有数据，加入数组中
                        if (qcdata.Count > 0)
                        {
                            //计算靶值
                            double iTarget = qcdata.Average(avg => avg.QuanValue);
                            //计算sd
                            double iSD = 0;
                            if (qcdata.Count() >= 3)
                            {
                                //求和
                                double valueSum = qcdata.Sum(p => (p.QuanValue - iTarget) * (p.QuanValue - iTarget));
                                //取平方根 
                                iSD = Math.Sqrt(valueSum / (qcdata.Count() - 1));
                            }
                            else
                            {
                                ZhiFang.LabStar.Common.LogHelp.Debug("BLisQCData.QCMFigureNormalDistribution 质控项目数据小于3不计算标准差");
                            }
                            //如果计算sd为0不用画图
                            if (iSD != 0)
                            {
                                if (!iTargetSD.ContainsKey(iteme.Id)) //时效id
                                {
                                    iTargetSD.Add(iteme.Id, new List<double>() { iTarget, iSD });
                                }
                            }
                        }
                    }

                }

                //画点
                foreach (var item in iTargetSD)
                {
                    var iTargetSd = iTargetSD.Where(a => a.Key == item.Key).First().Value; //不同时效对应的计算靶值和标准差
                    var qctime = lBQCItemTimes.Where(t => t.Id == item.Key).First();
                    QCMFigureNormalDistribution_QCItemData data = new QCMFigureNormalDistribution_QCItemData();
                    //循环画点
                    //ZhiFang.LabStar.Common.LogHelp.Debug("iTarget="+ iTargetSd[0]+ " iSD="+ iTargetSd[1]);
                    // ZhiFang.LabStar.Common.LogHelp.Debug("iBaseTarget=" + qctime.Target + " iBaseSD=" + qctime.SD);
                    for (int i = 0; i < pointCount; i++)
                    {
                        QCMFigureNormalDistribution_Data qCMFigureNormalDistribution_Data = new QCMFigureNormalDistribution_Data();

                        var iTarget = iTargetSd[0]; //计算靶值
                        var iSD = iTargetSd[1]; //计算标准差
                        var x = iTarget - 3 * iSD + (iSD * 6.0 / pointCount) * i;
                        var y = Math.Pow(e, -Math.Pow((x - iTarget) / iSD, 2) / 2);

                        qCMFigureNormalDistribution_Data.XValue = (x - qctime.Target) / qctime.SD;  //x值
                        qCMFigureNormalDistribution_Data.YValue = y * (qctime.SD / iSD);//Y值

                        dataYScale.Add(qCMFigureNormalDistribution_Data.YValue); // 记录y刻度
                        data.QCMName = lisQCDatas.Where(a => a.LBQCItem.Id == qctime.LBQCItem.Id).First().LBQCItem.LBQCMaterial.CName;
                        if (data.Data == null)
                        {
                            data.Data = new List<QCMFigureNormalDistribution_Data>();
                        }
                        //  ZhiFang.LabStar.Common.LogHelp.Debug("i=" + i + "    X= " + qCMFigureNormalDistribution_Data.XValue + "  Y=" + qCMFigureNormalDistribution_Data.YValue);
                        data.Data.Add(qCMFigureNormalDistribution_Data);
                    }
                    if (QCMFigureNormalDistribution.Data == null)
                    {
                        QCMFigureNormalDistribution.Data = new List<QCMFigureNormalDistribution_QCItemData>();
                    }
                    QCMFigureNormalDistribution.Data.Add(data);
                }
            }

            //Y轴
            var yMax = YScale.Max();
            if (dataYScale.Count > 0)
            {
                yMax = YScale.Max() + dataYScale.Average();
            }
            QCMFigureNormalDistribution.Y = new QCMFigureNormalDistributionY() { Min = YScale.Min(), Max = yMax };
            return QCMFigureNormalDistribution;
        }
        /// <summary>
        /// 累积和图
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <param name="target"></param>
        /// <param name="sD"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureCumulativeSumGraph QCMFigureCumulativeSumGraph(long qCItemId, double target, double sD, DateTime startDate, DateTime endDate)
        {
            QCMFigureCumulativeSumGraph qCMFigureCumulativeSumGraph = new QCMFigureCumulativeSumGraph();
            string lisdatawhere = "LBQCItem.Id=" + qCItemId + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            //Y
            QCMFigureCumulativeSumGraphY qCMFigureCumulativeSumGraphY = new QCMFigureCumulativeSumGraphY();
            var sd5 = target + 5 * sD;
            var sd_5 = target - 5 * sD;
            qCMFigureCumulativeSumGraphY.Max = sd5 + 1;
            qCMFigureCumulativeSumGraphY.Min = sd_5 - 1;
            qCMFigureCumulativeSumGraphY.Data = new List<QCMFigureCumulativeSumGraphY_Data>() {
                new QCMFigureCumulativeSumGraphY_Data(){Name = "Target",SDValue = target.ToString(),Value = target.ToString()},
                new QCMFigureCumulativeSumGraphY_Data(){Name = "+5SD",SDValue = sd5.ToString(),Value = sd5.ToString()},
                new QCMFigureCumulativeSumGraphY_Data(){Name = "-5SD",SDValue = sd_5.ToString(),Value = sd_5.ToString()}
            };
            qCMFigureCumulativeSumGraph.Y = qCMFigureCumulativeSumGraphY;
            //数据
            int index = 0;
            List<int> batch = new List<int>(); //批次
            foreach (var item in lisQCDatas)
            {
                index++;
                batch.Add(index);
                QCMFigureCumulativeSumGraph_Data qCMFigureCumulativeSumGraph_Data = new QCMFigureCumulativeSumGraph_Data();
                qCMFigureCumulativeSumGraph_Data.Batch = index;
                if (index == 1)
                {
                    qCMFigureCumulativeSumGraph_Data.YValue = item.QuanValue;
                    if (item.QuanValue > target + sD * 3 || item.QuanValue < target - sD * 3)
                    {
                        qCMFigureCumulativeSumGraph_Data.YValue = target;
                    }
                }
                else
                {
                    qCMFigureCumulativeSumGraph_Data.YValue = item.QuanValue - target + qCMFigureCumulativeSumGraph.Data.Last().YValue;
                    if (qCMFigureCumulativeSumGraph_Data.YValue > target + sD * 3 || qCMFigureCumulativeSumGraph_Data.YValue < target - sD * 3)
                    {
                        qCMFigureCumulativeSumGraph_Data.YValue = target;
                    }
                }
                if (qCMFigureCumulativeSumGraph.Data == null)
                {
                    qCMFigureCumulativeSumGraph.Data = new List<QCMFigureCumulativeSumGraph_Data>();
                }
                qCMFigureCumulativeSumGraph.Data.Add(qCMFigureCumulativeSumGraph_Data);
            }
            batch.Insert(0, 0);
            batch.Add(++index);
            qCMFigureCumulativeSumGraph.X = new QCMFigureCumulativeSumGraphX();
            qCMFigureCumulativeSumGraph.X.Batch = batch;

            return qCMFigureCumulativeSumGraph;
        }

        /// <summary>
        /// 频数分布图
        /// </summary>
        /// <param name="qCItemId"></param>
        /// <param name="target"></param>
        /// <param name="sD"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public QCMFigureFrequencyDistribution QCMFigureFrequencyDistribution(long qCItemId, double target, double sD, DateTime startDate, DateTime endDate)
        {
            QCMFigureFrequencyDistribution qCMFigureCumulativeSumGraph = new QCMFigureFrequencyDistribution();
            string lisdatawhere = "LBQCItem.Id=" + qCItemId + " and ReceiveTime>='" + startDate.ToString("yyyy-MM-dd") + "' and ReceiveTime<='" + endDate.ToString("yyyy-MM-dd 23:59:59") + "' and LoseType is not null and LoseType<>'无法判断'";
            IList<LisQCData> lisQCDatas = DBDao.GetListByHQL(lisdatawhere);
            //Y
            QCMFigureFrequencyDistributionY qCMFigureCumulativeSumGraphY = new QCMFigureFrequencyDistributionY();
            var sd3 = target + 3 * sD;
            var sd_3 = target - 3 * sD;
            qCMFigureCumulativeSumGraphY.Max = lisQCDatas.Count;
            qCMFigureCumulativeSumGraphY.Min = 0;
            //计算 N倍标准差
            qCMFigureCumulativeSumGraphY.Data = new List<QCMFigureFrequencyDistributionY_Data>() {
                new QCMFigureFrequencyDistributionY_Data(){Name = "Target",SDValue = target.ToString(),Value = target.ToString()},
                new QCMFigureFrequencyDistributionY_Data(){Name = "+3SD",SDValue = sd3.ToString(),Value = sd3.ToString()},
                new QCMFigureFrequencyDistributionY_Data(){Name = "-3SD",SDValue = sd_3.ToString(),Value = sd_3.ToString()} };

            qCMFigureCumulativeSumGraph.Y = qCMFigureCumulativeSumGraphY;
            //数据
            double groupSpacing = 6 * sD / 11;//组距
            List<double> batch = new List<double>(); //批次
            int i = 0;
            int lisqccount = 0;
            for (; i < 11; i++)
            {
                //if (i==0)
                //{
                //    lisqccount = lisQCDatas.Where(a =>a.QuanValue<target-3*sD).Count();
                //        QCMFigureFrequencyDistribution_Data qCMFigureCumulativeSumGraph_Data = new QCMFigureFrequencyDistribution_Data();
                //        qCMFigureCumulativeSumGraph_Data.Batch = target - sD * 3;
                //        qCMFigureCumulativeSumGraph_Data.YValue = lisqccount > 0 ? lisqccount : 0;
                //        qCMFigureCumulativeSumGraph.Data = new List<QCMFigureFrequencyDistribution_Data>();
                //        qCMFigureCumulativeSumGraph.Data.Add(qCMFigureCumulativeSumGraph_Data);
                //    continue;
                //}
                batch.Add(target - 3 * sD + groupSpacing * i);
                lisqccount = lisQCDatas.Where(a => target - sD * 3 + (groupSpacing * i) <= a.QuanValue && a.QuanValue < target - sD * 3 + (groupSpacing * (i + 1))).Count();
                QCMFigureFrequencyDistribution_Data qCMFigureCumulativeSumGraph_Data1 = new QCMFigureFrequencyDistribution_Data();
                qCMFigureCumulativeSumGraph_Data1.Batch = target - sD * 3 + (groupSpacing * i);
                qCMFigureCumulativeSumGraph_Data1.YValue = lisqccount > 0 ? lisqccount : 0;
                if (qCMFigureCumulativeSumGraph.Data == null)
                {
                    qCMFigureCumulativeSumGraph.Data = new List<QCMFigureFrequencyDistribution_Data>();
                }
                qCMFigureCumulativeSumGraph.Data.Add(qCMFigureCumulativeSumGraph_Data1);
            }
            //lisqccount = lisQCDatas.Where(a => target - 3 * sD + (groupSpacing * i - 1) <= a.QuanValue).Count();
            //QCMFigureFrequencyDistribution_Data qCMFigureCumulativeSumGraph_Data2 = new QCMFigureFrequencyDistribution_Data();
            //qCMFigureCumulativeSumGraph_Data2.Batch = target - sD * 3 + (groupSpacing * i - 1);
            //qCMFigureCumulativeSumGraph_Data2.YValue = lisqccount > 0 ? lisqccount : 0;
            //qCMFigureCumulativeSumGraph.Data.Add(qCMFigureCumulativeSumGraph_Data2);
            batch.Insert(0, target - 3 * sD - groupSpacing);
            batch.Add(target - 3 * sD + groupSpacing * (i + 1));
            qCMFigureCumulativeSumGraph.X = new QCMFigureFrequencyDistributionX();
            qCMFigureCumulativeSumGraph.X.Batch = batch;

            return qCMFigureCumulativeSumGraph;
        }
        #endregion

        #region 质控通讯数据导入
        public BaseResultDataValue AddLisEquipItemResult_QC(long labID, IList<EquipQCResult> listEquipQCResult)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listEquipQCResult != null && listEquipQCResult.Count > 0)
            {
                Dictionary<long, LBQCItem> dicLBQCItem = new Dictionary<long, LBQCItem>();
                foreach (EquipQCResult equipQCResult in listEquipQCResult)
                {
                    LBQCItem lbQCItem = null;
                    if (dicLBQCItem.ContainsKey(equipQCResult.QCItemNo))
                        lbQCItem = dicLBQCItem[equipQCResult.QCItemNo];
                    else
                    {
                        IList<LBQCItem> listLBQCItem = IDLBQCItemDao.GetListByHQL(" lbqcitem.ItemNo=" + equipQCResult.QCItemNo);
                        if (listLBQCItem == null || listLBQCItem.Count == 0)
                            lbQCItem = null;
                        else
                            lbQCItem = listLBQCItem[0];
                        dicLBQCItem.Add(equipQCResult.QCItemNo, lbQCItem);
                    }
                    if (lbQCItem == null)
                        continue;

                    IList<LisQCData> listLisQCData = this.SearchListByHQL(" lisqcdata.LisEquip.Id=" + "" +
                        " and lisqcdata.LBItem.Id=" + "");


                }
            }
            return baseResultDataValue;
        }

        #endregion
    }
}