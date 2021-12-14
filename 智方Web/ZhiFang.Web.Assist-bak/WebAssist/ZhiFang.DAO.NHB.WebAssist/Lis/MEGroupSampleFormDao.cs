using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.DAO.NHB.WebAssist
{
    public class MEGroupSampleFormDao : BaseDaoNHB<MEGroupSampleForm, long>, IDMEGroupSampleFormDao
    {
        public string GetNextGSampleNo(int sectionNo, string testTime)
        {
            string gsampleNo = "";

            if (sectionNo < 0 && string.IsNullOrEmpty(testTime))
            {
                return gsampleNo;
            }

            string strHQL = string.Format(" select Max(GSampleNo) from MEGroupSampleForm megroupsampleform where megroupsampleform.SectionNo={0} and megroupsampleform.TestTime>='{1} 00:00:00.000' and megroupsampleform.TestTime<='{2} 23:59:59'", sectionNo, testTime, testTime);
            //IList<string> tempList = this.HibernateTemplate.Find<string>(strHQL);
            DaoNHBGetStringMaxByHqlAction<string> action = new DaoNHBGetStringMaxByHqlAction<string>(strHQL);
            gsampleNo = this.HibernateTemplate.Execute<string>(action);
            int gsampleNo2 = -1;
            if (!string.IsNullOrEmpty(gsampleNo))
            {
                int.TryParse(gsampleNo, out gsampleNo2);
                if (gsampleNo2 > 0) gsampleNo = (gsampleNo2 + 1).ToString();
            }
            return gsampleNo;
        }
    }
}