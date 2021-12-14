using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using NHibernate;
using NHibernate.Criterion;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class QCDValueDao : BaseDaoNHB<QCDValue, long>, IDQCDValueDao
	{
        #region IDQCDValueDao 成员

        public IList<QCDValue> SearchQCDValueByQCItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate)
        {
            string strHQL = "from QCDValue qcdvalue where qcdvalue.QCItem.Id=" + longQCItemID + " and qcdvalue.ReceiveTime>='" + strStartDate + "' and qcdvalue.ReceiveTime<='" + strEndDate + "' order by qcdvalue.ReceiveTime asc ";
            var l = this.HibernateTemplate.Find<QCDValue>(strHQL);
            return l;
        }

        /// <summary>
        /// 根据质控项目ID和指定日期返回质控数据列表(自定义列)
        /// </summary>
        /// <param name="listQCItem">质控项目列表</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValueCustom&gt;</returns>
        public IList<QCDValueCustom> SearchQCDValueCustomByQCItemIDAndDate(IList<QCItem> listQCItem, string strStartDate, string strEndDate)
        {
            string strQCItemId = "";
            foreach (var tmp in listQCItem)
            {
                strQCItemId += "qcdvalue.QCItem.Id=" + tmp.Id + " or ";
            }
            strQCItemId = " (" + strQCItemId + " 1=2) ";
            string strHQL = "select new QCDValueCustom(qcdvalue.Id,qcdvalue.LabID,qcdvalue.ReceiveTime,qcdvalue.QCDataLotNo, " +
                             "qcdvalue.OriglValue,qcdvalue.ReportValue,qcdvalue.CVValue,qcdvalue.ResultStatus, " +
                             "qcdvalue.IsControl,qcdvalue.QCControlInfo,qcdvalue.IsUse,qcdvalue.IsEquipResult, " +
                             "qcdvalue.QCComment,operator.CName as OperatorName,qcdvalue.DataTimeStamp, " +
                             "qcdvalue.QCItem.Id as QCItemID,qcdvalue.QCItem.ItemAllItem.CName as QCItemName,qcdvalue.QCItem.ItemAllItem.SName as QCItemSName, " +
                             "qcdvalue.QCItem.ValueType as QCItemValueTypeInt,qcdvalue.QCItem.DataTimeStamp as QCItemDataTimeStamp, " +
                             "qcitemtimelist.Target as Target,qcitemtimelist.SD as SD,qcitemtimelist.CV as CV,qcitemtimelist.CaclTarget as CalcTarget,qcitemtimelist.CaclSD as CalcSD, " +
                             "qcdvalue.QCItem.ItemAllItem.Id as ItemID, " +
                             "qcdvalue.QCItem.QCMat.Id as QCMatID,qcdvalue.QCItem.QCMat.CName as QCMatName,qcdvalue.QCItem.QCMat.SName as QCMatSName, " +
                             "qcdvalue.QCItem.QCMat.Manu,qcdvalue.QCItem.QCMat.ConcLevel,qcitemtimelist.QCMatTime.LotNo as QCMatLotNo, " +
                             "qcdvalue.QCItem.QCMat.EPBEquip.Id as EquipID,qcdvalue.QCItem.QCMat.EPBEquip.CName as EquipName,qcdvalue.QCItem.QCMat.EPBEquip.SName as EquipSName, " +
                             "qcdvalue.IsUse as QCDValueIsUse,qcdvalue.QCItem.QCMat.DispOrder as QCMatDispOrder,qcdvalue.QCItem.DispOrder as QCItemDispOrder, " +
                             "qcitemtimelist.QCMatTime.QCMatTimeDesc,qcitemtimelist.QCItemDesc,qcdvalue.QCItem.ItemAllItem.Precision as QCItemPrecision, " +
                             "qcitemtimelist.BeginDate as QCItemTimeBeginDate,qcitemtimelist.EndDate as QCItemTimeEndDate) " +
                             "from QCDValue qcdvalue " +
                             "left join qcdvalue.Operator operator " +
                             "left join qcdvalue.QCItem.QCItemTimeList qcitemtimelist " +
                             "where 1=1 and " + strQCItemId + " and qcdvalue.ReceiveTime>='" + strStartDate + "' and " +
                             "qcdvalue.ReceiveTime<='" + strEndDate + "' " +
                             "and ((date(qcdvalue.ReceiveTime) between qcitemtimelist.BeginDate and qcitemtimelist.EndDate) or (date(qcdvalue.ReceiveTime)>=qcitemtimelist.BeginDate and (date(qcdvalue.ReceiveTime)<=qcitemtimelist.EndDate or qcitemtimelist.EndDate is null))) " +
                             "order by qcdvalue.ReceiveTime asc ";
            var l = this.HibernateTemplate.Find<QCDValueCustom>(strHQL);
            return l;
        }

        public IList<QCDValue> SearchQCDValueByItemIDAndDate(long longItemID, string strStartDate, string strEndDate)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("QCDValue", new List<ICriterion>() { Restrictions.Ge("ReceiveTime", DateTime.Parse(strStartDate)), Restrictions.Le("ReceiveTime", DateTime.Parse(strEndDate)) });
            dic.Add("QCItem", null);
            dic.Add("ItemAllItem", new List<ICriterion>() { Restrictions.Eq("Id", longItemID) });

            DaoNHBCriteriaAction<List<QCDValue>, QCDValue> action = new DaoNHBCriteriaAction<List<QCDValue>, QCDValue>(dic);

            List<QCDValue> l = base.HibernateTemplate.Execute<List<QCDValue>>(action);
            return l;
        }

        /// <summary>
        /// 根据项目ID和指定日期返回质控数据列表
        /// </summary>
        /// <param name="longQCItemID">项目ID</param>
        /// <param name="listQCMat">质控物列表</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValueCustom&gt;</returns>
        public IList<QCDValueCustom> SearchQCDValueByItemIDAndDate(long longItemID, IList<QCMat> listQCMat, string strStartDate, string strEndDate)
        {
            string strQCMatId = "";
            foreach (var tmp in listQCMat)
            {
                strQCMatId += "qcdvalue.QCItem.QCMat.Id=" + tmp.Id + " or ";
            }
            strQCMatId = " (" + strQCMatId + " 1=2) ";
            string strHQL = "select new QCDValueCustom(qcdvalue.Id,qcdvalue.LabID,qcdvalue.ReceiveTime,qcdvalue.QCDataLotNo, " +
                             "qcdvalue.OriglValue,qcdvalue.ReportValue,qcdvalue.CVValue,qcdvalue.ResultStatus, " +
                             "qcdvalue.IsControl,qcdvalue.QCControlInfo,qcdvalue.IsUse,qcdvalue.IsEquipResult, " +
                             "qcdvalue.QCComment,operator.CName as OperatorName,qcdvalue.DataTimeStamp, " +
                             "qcdvalue.QCItem.Id as QCItemID,qcdvalue.QCItem.ItemAllItem.CName as QCItemName,qcdvalue.QCItem.ItemAllItem.SName as QCItemSName, " +
                             "qcdvalue.QCItem.ValueType as QCItemValueTypeInt,qcdvalue.QCItem.DataTimeStamp as QCItemDataTimeStamp, " +
                             "qcitemtimelist.Target as Target,qcitemtimelist.SD as SD,qcitemtimelist.CV as CV,qcitemtimelist.CaclTarget as CalcTarget,qcitemtimelist.CaclSD as CalcSD, " +
                             "qcdvalue.QCItem.ItemAllItem.Id as ItemID, " +
                             "qcdvalue.QCItem.QCMat.Id as QCMatID,qcdvalue.QCItem.QCMat.CName as QCMatName,qcdvalue.QCItem.QCMat.SName as QCMatSName, " +
                             "qcdvalue.QCItem.QCMat.Manu,qcdvalue.QCItem.QCMat.ConcLevel,qcitemtimelist.QCMatTime.LotNo as QCMatLotNo, " +
                             "qcdvalue.QCItem.QCMat.EPBEquip.Id as EquipID,qcdvalue.QCItem.QCMat.EPBEquip.CName as EquipName,qcdvalue.QCItem.QCMat.EPBEquip.SName as EquipSName, " +
                             "qcdvalue.IsUse as QCDValueIsUse,qcdvalue.QCItem.QCMat.DispOrder as QCMatDispOrder,qcdvalue.QCItem.DispOrder as QCItemDispOrder, "+
                             "qcitemtimelist.QCMatTime.QCMatTimeDesc,qcitemtimelist.QCItemDesc,qcdvalue.QCItem.ItemAllItem.Precision as QCItemPrecision, " +
                             "qcitemtimelist.BeginDate as QCItemTimeBeginDate,qcitemtimelist.EndDate as QCItemTimeEndDate) " +
                             "from QCDValue qcdvalue " +
                             "left join qcdvalue.Operator operator " +
                             "left join qcdvalue.QCItem.QCItemTimeList qcitemtimelist " +
                             "where qcdvalue.QCItem.ItemAllItem.Id=" + longItemID + " and qcdvalue.ReceiveTime>='" + strStartDate + "' and " +
                             "qcdvalue.ReceiveTime<='" + strEndDate + "' and " + strQCMatId +
                             "and ((date(qcdvalue.ReceiveTime) between qcitemtimelist.BeginDate and qcitemtimelist.EndDate) or (date(qcdvalue.ReceiveTime)>=qcitemtimelist.BeginDate and (date(qcdvalue.ReceiveTime)<=qcitemtimelist.EndDate or qcitemtimelist.EndDate is null))) " +
                             "order by qcdvalue.ReceiveTime asc ";
            var l = this.HibernateTemplate.Find<QCDValueCustom>(strHQL);
            return l;
        }

        public IList<QCDValue> SearchQCDValueByQueryFilter(string strQueryFilter)
        {
            string strQuery = "select qcdvalue from QCDValue qcdvalue where 1=1 ";
            if (strQueryFilter != null && strQueryFilter.Length > 0)
                strQuery = strQuery + "and " + strQueryFilter;
            return base.HibernateTemplate.Find<QCDValue>(strQuery).ToList<QCDValue>();
        }

        public IList<QCDValue> SearchAllConcentrationQCDValueByQCItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate)
        {
            IList<QCDValue> returnList = new List<QCDValue>();
            string strHQLGetQCItem = "select qcitem from QCItem qcitem,QCMat qcmat where qcitem.QCMat.Id=qcmat.Id and qcitem.Id=:QCItemID";
            IList<QCItem> QCItemList = this.HibernateTemplate.FindByNamedParam<QCItem>(strHQLGetQCItem, "QCItemID", longQCItemID);

            if (QCItemList.Count > 0)
            {
                string strHQLGetQCMat = "select qcmat from QCMat qcmat where qcmat.Id=:QCMatID";
                IList<QCMat> MatList = this.HibernateTemplate.FindByNamedParam<QCMat>(strHQLGetQCMat, "QCMatID", QCItemList[0].QCMat.Id);

                string strHQLGetQTestItem = "from ItemAllItem item where item.Id=:ItemID";
                IList<ItemAllItem> TestItemList = this.HibernateTemplate.FindByNamedParam<ItemAllItem>(strHQLGetQTestItem, "ItemID", QCItemList[0].ItemAllItem.Id);

                string strHQLWhereToMatGroup = "";
                if (MatList[0].MatGroup == null || MatList[0].MatGroup == "")
                    strHQLWhereToMatGroup = "or (qcmat.MatGroup is null or qcmat.MatGroup='')";

                string strHQLWhereToEquipModule = "";
                if (MatList[0].EquipModule == null || MatList[0].EquipModule == "")
                    strHQLWhereToEquipModule = "or (qcmat.EquipModule is null or qcmat.EquipModule='')";

                string strHQL = "";
                strHQL += "select new QCDValue(qcdvalue.Id,qcdvalue.ReceiveTime,qcdvalue.QCDataLotNo, ";
                strHQL += "qcdvalue.ReportValue,qcdvalue.QCComment,qcdvalue.IsControl,qcdvalue.QCControlInfo,qcdvalue.QCItem,qcitemtimelist.Target,qcitemtimelist.SD) ";
                strHQL += "from QCDValue qcdvalue,QCMat qcmat ";
                strHQL += "left join qcdvalue.QCItem.QCItemTimeList qcitemtimelist ";
                strHQL += "where qcdvalue.QCItem.QCMat.Id=qcmat.Id and qcdvalue.QCItem.ItemAllItem.Id=:ItemID and qcdvalue.QCItem.QCMat.EPBEquip.Id=:EquipID and ";
                strHQL += "(qcmat.EquipModule=:EquipModule " + strHQLWhereToEquipModule + ") and ";
                strHQL += "(qcmat.MatGroup=:MatGroup " + strHQLWhereToMatGroup + ") and qcmat.IsUse=true and ";
                strHQL += "qcdvalue.ReceiveTime>='" + strStartDate + "' and qcdvalue.ReceiveTime<='" + strEndDate + "' ";
                strHQL += "and ((date(qcdvalue.ReceiveTime) between qcitemtimelist.BeginDate and qcitemtimelist.EndDate) or (date(qcdvalue.ReceiveTime)>=qcitemtimelist.BeginDate and (date(qcdvalue.ReceiveTime)<=qcitemtimelist.EndDate or qcitemtimelist.EndDate is null))) ";
                strHQL += "order by qcdvalue.QCItem.Id,qcdvalue.ReceiveTime";

                returnList = this.HibernateTemplate.FindByNamedParam<QCDValue>(strHQL, new string[] { "ItemID", "EquipID", "EquipModule", "MatGroup" },
                    new object[] { TestItemList[0].Id, MatList[0].EPBEquip.Id, MatList[0].EquipModule, MatList[0].MatGroup });
            }
            return returnList;
        }

        /// <summary>
        /// 根据项目ID和指定日期返回质控数据列表(自定义查询字段)
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValueCustomToBakUpData&gt;</returns>
        public IList<QCDValue> SearchQCDValueCustomByItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate)
        {
            string strHQL = "select new QCDValue(qcdvalue.Id,qcdvalue.ReceiveTime,qcdvalue.QCDataLotNo, " +
                             "qcdvalue.ReportValue,qcdvalue.QCComment,qcdvalue.IsControl,qcdvalue.QCControlInfo,qcdvalue.QCItem,qcitemtimelist.Target,qcitemtimelist.SD) " +
                             "from QCDValue qcdvalue " +
                             "left join qcdvalue.QCItem.QCItemTimeList qcitemtimelist " +
                             "where qcdvalue.QCItem.Id=" + longQCItemID + " and qcdvalue.ReceiveTime>='" + strStartDate + "' and " +
                             "qcdvalue.ReceiveTime<='" + strEndDate + "' " +
                             "and ((date(qcdvalue.ReceiveTime) between qcitemtimelist.BeginDate and qcitemtimelist.EndDate) or (date(qcdvalue.ReceiveTime)>=qcitemtimelist.BeginDate and (date(qcdvalue.ReceiveTime)<=qcitemtimelist.EndDate or qcitemtimelist.EndDate is null))) ";
            var l = this.HibernateTemplate.Find<QCDValue>(strHQL);
            return l;
        }

        /// <summary>
        /// 根据项目ID和指定日期返回质控数据列表(自定义查询字段，用于计算靶值、计算标准差)
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        public IList<QCDValue> SearchQCDValueCustomByItemIDAndDateToCalcTargetSD(long longQCItemID, string strStartDate, string strEndDate)
        {
            string strHQL = "select new QCDValue(qcdvalue.Id,qcdvalue.ReceiveTime,qcdvalue.QCDataLotNo,qcdvalue.ReportValue) " +
                             "from QCDValue qcdvalue " +
                             "where qcdvalue.QCItem.Id=" + longQCItemID + " and qcdvalue.ReceiveTime>='" + strStartDate + "' and " +
                             "qcdvalue.ReceiveTime<='" + strEndDate + "' ";
            var l = this.HibernateTemplate.Find<QCDValue>(strHQL);
            return l;
        }

        /// <summary>
        /// 质控统计
        /// </summary>
        /// <param name="strStartDate">开始日期</param>
        /// <param name="strEndDate">结束日期</param>
        /// <param name="longSpecialtyID">专业ID</param>
        /// <param name="longEquipID">仪器ID</param>
        /// <param name="longQCMatID">质控物ID</param>
        /// <param name="longItemID">检验项目ID</param>
        /// <returns>IList&lt;QCDataStatistics&gt;</returns>
        public IList<QCDataStatistics> SearchQCDValueToStatistics(string strStartDate, string strEndDate, long longSpecialtyID, long longEquipID, long longQCMatID, long longItemID)
        {
            string strHQL = "select new QCDataStatistics(qcdvalue.QCItem.QCMat.EPBEquip.Id as EquipID,qcdvalue.QCItem.QCMat.EPBEquip.CName as EquipName,qcdvalue.QCItem.QCMat.EPBEquip.SName as EquipSName,qcdvalue.QCItem.QCMat.EPBEquip.DispOrder as EquipDispOrder, " +
                            "qcdvalue.QCItem.QCMat.Id as QCMatID,qcdvalue.QCItem.QCMat.CName as QCMatName,qcdvalue.QCItem.QCMat.SName as QCMatSName,qcdvalue.QCItem.QCMat.Manu,qcdvalue.QCItem.QCMat.ConcLevel,qcitemtimelist.QCMatTime.LotNo as QCMatLotNo,qcdvalue.QCItem.QCMat.DispOrder as QCMatDispOrder, " +
                            "qcdvalue.QCItem.Id as QCItemID,qcdvalue.QCItem.ItemAllItem.CName as QCItemName,qcdvalue.QCItem.ItemAllItem.SName as QCItemSName,qcdvalue.QCItem.DispOrder as QCItemDispOrder,qcdvalue.QCItem.ItemAllItem.Precision as QCItemPrecision,qcdvalue.QCItem.DataTimeStamp as QCItemDataTimeStamp,qcdvalue.QCItem.CCV, " +
                            "qcitemtimelist.BeginDate,qcitemtimelist.EndDate, " +
                            "qcitemtimelist.Target,qcitemtimelist.SD,qcitemtimelist.CV,qcitemtimelist.CaclTarget as CalcTarget,qcitemtimelist.CaclSD as CalcSD, " +
                            "qcdvalue.IsUse as QCDValueIsUse,qcdvalue.IsControl,qcdvalue.ReportValue,qcdvalue.ReceiveTime,qcdvalue.QCDataLotNo, " +
                            "operator.Id as EmpID,operator.CName as EmpName,operator.DataTimeStamp as EmpDataTimeStamp) " +
                            "from QCDValue qcdvalue " +
                            "left join qcdvalue.Operator operator " +
                            "left join qcdvalue.QCItem.QCItemTimeList qcitemtimelist " +
                            "where qcdvalue.ReceiveTime>='" + strStartDate + "' and qcdvalue.ReceiveTime<'" + strEndDate + "' " +
                            "and ((date(qcdvalue.ReceiveTime) between qcitemtimelist.BeginDate and qcitemtimelist.EndDate) or (date(qcdvalue.ReceiveTime)>=qcitemtimelist.BeginDate and (date(qcdvalue.ReceiveTime)<=qcitemtimelist.EndDate or qcitemtimelist.EndDate is null))) ";
            if (longSpecialtyID > 0)
            {
                strHQL += " and qcdvalue.QCItem.ItemAllItem.BSpecialty.Id=" + longSpecialtyID;
            }
            if (longEquipID > 0)
            {
                strHQL += " and qcdvalue.QCItem.QCMat.EPBEquip.Id=" + longEquipID;
            }
            if (longQCMatID > 0)
            {
                strHQL += " and qcdvalue.QCItem.QCMat.Id=" + longQCMatID;
            }
            if (longItemID > 0)
            {
                strHQL += " and qcdvalue.QCItem.ItemAllItem.Id=" + longItemID;
            }
            var l = this.HibernateTemplate.Find<QCDataStatistics>(strHQL);
            return l;
        }

        #endregion
    }

}