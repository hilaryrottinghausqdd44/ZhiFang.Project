using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class QCDStatDao : BaseDaoNHB<QCDStat, long>, IDQCDStatDao
    {
        /// <summary>
        /// 质控统计
        /// </summary>
        /// <param name="qcItemList">质控项目ID列表</param>
        /// <param name="yearID">年份</param>
        /// <param name="monthID">月份</param>
        /// <returns>IList&lt;QCDataStatistics&gt;</returns>
        public IList<QCDataStatistics> SearchQCDValueToStatistics(IList<QCItem> qcItemList, int yearID, int monthID)
        {
            string strQCItemId = "";
            foreach (var tmp in qcItemList)
            {
                strQCItemId += "qcdstat.QCItem.Id=" + tmp.Id + " or ";
            }
            strQCItemId = " (" + strQCItemId + " 1=2) ";

            string strHQL = "select new QCDataStatistics(qcdstat.Id,qcdstat.QCItem.QCMat.EPBEquip.Id as EquipID,qcdstat.QCItem.QCMat.EPBEquip.CName as EquipName,qcdstat.QCItem.QCMat.EPBEquip.SName as EquipSName,qcdstat.QCItem.QCMat.EPBEquip.DispOrder as EquipDispOrder, " +
                            "qcdstat.QCItem.QCMat.Id as QCMatID,qcdstat.QCItem.QCMat.CName as QCMatName,qcdstat.QCItem.QCMat.SName as QCMatSName,qcdstat.QCItem.QCMat.Manu,qcdstat.QCItem.QCMat.ConcLevel,qcdstat.LotNo as QCMatLotNo,qcdstat.QCItem.QCMat.DispOrder as QCMatDispOrder, " +
                            "qcdstat.QCItem.Id as QCItemID,qcdstat.QCItem.ItemAllItem.CName as QCItemName,qcdstat.QCItem.ItemAllItem.SName as QCItemSName,qcdstat.QCItem.DispOrder as QCItemDispOrder,qcdstat.QCItem.ItemAllItem.Precision as QCItemPrecision,qcdstat.QCItem.DataTimeStamp as QCItemDataTimeStamp,qcdstat.QCItem.CCV, " +
                            "qcdstat.BeginDate,qcdstat.EndDate, " +
                            "qcdstat.QCTarget,qcdstat.QCSD,qcdstat.QCCV,qcdstat.TTarget,qcdstat.TSD,qcdstat.TCV, " +
                            "qcdstat.QCCount,qcdstat.SKCount,qcdstat.WarningCount,qcdstat.NoUseCount,qcdstat.ZKCount,qcdstat.QCRValue,qcdstat.QCMValue,qcdstat.QCComment, " +
                            "qcdstat.SD1Count,qcdstat.SD2Count,qcdstat.SD3Count, " +
                            "hremployee.Id as EmpID,hremployee.CName as EmpName,hremployee.DataTimeStamp as EmpDataTimeStamp, " +
                            "qcdstat.YearID,qcdstat.MonthID) " +
                            "from QCDStat qcdstat " +
                            "left outer join qcdstat.HREmployee hremployee " +
                            "where 1=1 and " + strQCItemId;
            if (yearID > 0)
            {
                strHQL += " and qcdstat.YearID=" + yearID;
            }
            if (monthID > 0)
            {
                strHQL += " and qcdstat.MonthID=" + monthID;
            }
            var l = this.HibernateTemplate.Find<QCDataStatistics>(strHQL);
            return l;
        }

        /// <summary>
        /// 根据质控项目和年份、月份查询质控统计数据列表
        /// </summary>
        /// <param name="qcItemList">质控项目ID列表</param>
        /// <param name="yearID">年份</param>
        /// <param name="monthID">月份</param>
        /// <returns>IList&lt;QCDStat&gt;</returns>
        public IList<QCDStat> SearchQCDStatByQCItemAndDate(IList<QCItem> qcItemList, int yearID, int monthID)
        {
            string strQCItemId = "";
            foreach (var tmp in qcItemList)
            {
                strQCItemId += "qcdstat.QCItem.Id=" + tmp.Id + " or ";
            }
            strQCItemId = " (" + strQCItemId + " 1=2) ";

            string strHQL = "from QCDStat qcdstat where 1=1 and " + strQCItemId + " and qcdstat.YearID=" + yearID + " and qcdstat.MonthID=" + monthID;
            var l = this.HibernateTemplate.Find<QCDStat>(strHQL);
            return l;
        }
    }
}