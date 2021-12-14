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
	public class QCItemTimeDao : BaseDaoNHB<QCItemTime, long>, IDQCItemTimeDao
	{
        #region IDQCItemTimeDao 成员

        public IList<QCItemTime> SearchQCItemTimeByQCItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate)
        {
            ////根据起止日期获取所有交叉部分的质控时效
            //string strHQL = "from QCItemTime qcitemtime where qcitemtime.QCItem.Id=" + longQCItemID + " and ( " +
            //                "(qcitemtime.BeginDate<='" + strStartDate + "' and qcitemtime.EndDate>='" + strStartDate + "') or " +
            //                "(qcitemtime.BeginDate<='" + strEndDate + "' and qcitemtime.EndDate>='" + strEndDate + "') or " +
            //                "(qcitemtime.BeginDate<='" + strEndDate + "' and (qcitemtime.EndDate is null or qcitemtime.EndDate='')) or" +
            //                "(qcitemtime.BeginDate>='" + strStartDate + "' and qcitemtime.EndDate<='" + strEndDate + "')) ";

            string strHQL = "from QCItemTime qcitemtime where qcitemtime.QCItem.Id=" + longQCItemID + " and qcitemtime.BeginDate>'" + strStartDate + "' and qcitemtime.BeginDate<'" + strEndDate + "' ";
            return base.HibernateTemplate.Find<QCItemTime>(strHQL);
        }

        public IList<QCItemTime> SearchQCItemTimeByQCItemIDAndDate(long longQCItemID, string strDate)
        {
            string strHQL = "from QCItemTime qcitemtime where qcitemtime.QCItem.Id=" + longQCItemID + " and qcitemtime.BeginDate<='" + strDate + "' and (qcitemtime.EndDate>='" + strDate + "' or qcitemtime.EndDate is null or qcitemtime.EndDate='')";
            return base.HibernateTemplate.Find<QCItemTime>(strHQL);
        }

        public IList<QCItemTime> SearchQCItemTimeCustomColByQCItemIDAndDate(long longQCItemID, string strDate)
        {
            IList<QCItemTime> tempQCItemTimeList = new List<QCItemTime>();
            string strHQL = "select qcitemtime.Target,qcitemtime.SD,qcitemtime.CV from QCItemTime qcitemtime where qcitemtime.QCItem.Id=" + longQCItemID + " and qcitemtime.BeginDate<='" + strDate + "' and (qcitemtime.EndDate>='" + strDate + "' or qcitemtime.EndDate is null or qcitemtime.EndDate='')";
            var l = base.HibernateTemplate.Find<object>(strHQL);
            if (l != null && l.Count > 0)
            {
                QCItemTime entity = new QCItemTime();
                if (((object[])(l[0]))[0] != null)
                    entity.Target = double.Parse((((object[])(l[0]))[0]).ToString());
                if (((object[])(l[0]))[1] != null)
                    entity.SD = double.Parse((((object[])(l[0]))[1]).ToString());
                if (((object[])(l[0]))[2] != null)
                    entity.CV = double.Parse((((object[])(l[0]))[2]).ToString());
                tempQCItemTimeList.Add(entity);
            }

            return tempQCItemTimeList;
        }

        #endregion
    } 
}