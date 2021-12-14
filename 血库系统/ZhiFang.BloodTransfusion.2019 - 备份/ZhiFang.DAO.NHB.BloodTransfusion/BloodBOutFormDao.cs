using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class BloodBOutFormDao : BaseDaoNHBServiceByString<BloodBOutForm, string>, IDBloodBOutFormDao
    {
        #region IList
        private IList<BloodBOutForm> SearchBloodBOutFormList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<BloodBOutForm> entityList = new List<BloodBOutForm>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BloodBOutForm>, BloodBOutForm> action = new DaoNHBSearchByHqlAction<List<BloodBOutForm>, BloodBOutForm>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<BloodBOutForm>>(action);
            return entityList;
        }
        private int SearchBloodBOutFormCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<BloodBOutForm> actionCount = new DaoNHBGetCountByHqlAction<BloodBOutForm>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        public EntityList<BloodBOutForm> SearchBloodBOutFormOfLeftJoinByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            StringBuilder sqlHql = new StringBuilder();//new BloodBOutForm(bloodboutform,bloodbreqform)
            sqlHql.Append(" select bloodboutform from BloodBOutForm bloodboutform ");
            sqlHql.Append(" left join fetch bloodboutform.BloodBReqForm bloodbreqform ");

            string strHQL = sqlHql.ToString();
            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(*) from  BloodBOutForm bloodboutform ");
            countHql.Append(" left join bloodboutform.BloodBReqForm bloodbreqform ");

            string strHQLCount = countHql.ToString();
            return this.GetListByHQL(strHqlWhere, sort, page, limit, strHQL, strHQLCount);

            //return entityList;
        }
        #endregion

        #region 获取已发血但是未交接完成的病人清单
        private IList<BloodBReqFormVO> SearchBloodBReqFormVOList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<BloodBReqFormVO> entityList = new List<BloodBReqFormVO>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BloodBReqFormVO>, BloodBReqFormVO> action = new DaoNHBSearchByHqlAction<List<BloodBReqFormVO>, BloodBReqFormVO>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<BloodBReqFormVO>>(action);
            return entityList;
        }
        private int SearchBloodBReqFormVOCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<BloodBReqFormVO> actionCount = new DaoNHBGetCountByHqlAction<BloodBReqFormVO>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        public EntityList<BloodBReqFormVO> SearchBloodBReqFormVOOfHandoverByHQL(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<BloodBReqFormVO> entityList = new EntityList<BloodBReqFormVO>();
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select new BloodBReqFormVO(bloodbreqform.AdmID,bloodbreqform.PatNo,bloodbreqform.CName,bloodbreqform.Sex,bloodbreqform.Birthday,bloodbreqform.AgeALL,bloodbreqform.DeptNo,bloodbreqform.Bed) from BloodBOutForm bloodboutform  ");
            sqlHql.Append(" left join bloodboutform.BloodBReqForm bloodbreqform ");//fetch

            sqlHql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                sqlHql.Append(" and " + strHqlWhere);
            sqlHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString());
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);

            sqlHql.Append(" group by bloodbreqform.AdmID,bloodbreqform.PatNo,bloodbreqform.CName,bloodbreqform.Sex,bloodbreqform.Birthday,bloodbreqform.AgeALL,bloodbreqform.DeptNo,bloodbreqform.Bed ");

            string strHQL = sqlHql.ToString();
            entityList.list = SearchBloodBReqFormVOList(strHQL, page, limit);

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select bloodbreqform.AdmID from BloodBOutForm bloodboutform ");
            countHql.Append(" left join bloodboutform.BloodBReqForm bloodbreqform ");
            countHql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                countHql.Append(" and " + strHqlWhere);
            countHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString());
            countHql.Append(" group by bloodbreqform.AdmID,bloodbreqform.PatNo,bloodbreqform.CName,bloodbreqform.Sex,bloodbreqform.Birthday,bloodbreqform.AgeALL,bloodbreqform.DeptNo,bloodbreqform.Bed ");
        
            string strCountHQL = countHql.ToString();
            DaoNHBSearchByHqlAction<List<string>, string> action = new DaoNHBSearchByHqlAction<List<string>, string>(strCountHQL, 0, 0);
            IList<string> list2 = this.HibernateTemplate.Execute<List<string>>(action);
            entityList.count = list2.Count;

            //entityList.count = SearchBloodBReqFormVOCount(strCountHQL);
            return entityList;
        }
        #endregion


    }
}