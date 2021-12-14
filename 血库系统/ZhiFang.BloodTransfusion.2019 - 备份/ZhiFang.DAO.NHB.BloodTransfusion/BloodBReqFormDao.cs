using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.DAO.NHB.BloodTransfusion
{
    public class BloodBReqFormDao : BaseDaoNHBServiceByString<BloodBReqForm, string>, IDBloodBReqFormDao
    {
        IDBloodBPreItemDao IDBloodBPreItemDao { get; set; }

        #region 公共
        private IList<BloodBReqForm> SearchBloodBReqFormList(string strHQL, int page, int limit)
        {
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            IList<BloodBReqForm> entityList = new List<BloodBReqForm>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<BloodBReqForm>, BloodBReqForm> action = new DaoNHBSearchByHqlAction<List<BloodBReqForm>, BloodBReqForm>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<BloodBReqForm>>(action);
            return entityList;
        }
        private int SearchBloodBReqFormCount(string strCountHQL)
        {
            strCountHQL = BaseDataFilter.FilterMacroCommand(strCountHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<BloodBReqForm> actionCount = new DaoNHBGetCountByHqlAction<BloodBReqForm>(strCountHQL);
            int count = this.HibernateTemplate.Execute<int>(actionCount);
            return count;
        }
        #endregion

        #region 医生站
        /// <summary>
        /// 医嘱申请不能按就诊类型,申请类型,科室,医生进行联合查询,因为相关数据项都是空的多
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public IList<BloodBReqForm> SearchBloodBReqFormListByJoinHql(string where, string sort, int page, int limit)
        {
            IList<BloodBReqForm> entityList = new List<BloodBReqForm>();
            StringBuilder sqlHql = new StringBuilder();

            sqlHql.Append(" select new BloodBReqForm(bloodbreqform,bloodbreqtype,bloodusetype,department,doctor) from BloodBReqForm bloodbreqform,BloodBReqType bloodbreqtype,BloodUseType bloodusetype,Department department,Doctor doctor where bloodbreqform.BReqTypeID=bloodbreqtype.Id and bloodbreqform.UseTypeID=bloodusetype.Id and bloodbreqform.DeptNo=department.Id and bloodbreqform.DoctorNo=doctor.Id ");
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);

            entityList = SearchBloodBReqFormList(sqlHql.ToString(), page, limit);

            return entityList;
        }
        public EntityList<BloodBReqForm> SearchBloodBReqFormEntityListByJoinHql(string where, string sort, int page, int limit)
        {
            EntityList<BloodBReqForm> entityList = new EntityList<BloodBReqForm>();
            entityList.list = new List<BloodBReqForm>();

            var list = SearchBloodBReqFormListByJoinHql(where, sort, page, limit);
            if (list == null || list.Count <= 0)
            {
                return entityList;
            }
            entityList.list = list;
            StringBuilder strCountHQL = new StringBuilder();
            strCountHQL.Append(" select count(DISTINCT bloodbreqform.Id) from BloodBReqForm bloodbreqform,BloodBReqType bloodbreqtype,BloodUseType bloodusetype,Department department,Doctor doctor where bloodbreqform.BReqTypeID=bloodbreqtype.Id and bloodbreqform.UseTypeID=bloodusetype.Id and bloodbreqform.DeptNo=department.Id and bloodbreqform.DoctorNo=doctor.Id ");

            if (!string.IsNullOrEmpty(where))
            {
                strCountHQL.Append(" and " + where);
            }
            int counts = SearchBloodBReqFormCount(strCountHQL.ToString());
            entityList.count = counts;
            return entityList;
        }
        /// <summary>
        /// 更新用血申请的打印总数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool UpdateBloodBReqFormPrintTotalById(string id)
        {
            bool result = true;
            if (!String.IsNullOrEmpty(id.ToString()))
            {
                string hql = "update BloodBReqForm bloodbreqform set bloodbreqform.PrintTotal=(bloodbreqform.PrintTotal+1) where bloodbreqform.Id='" + id + "'";
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;

        }
        #endregion

        #region 护士站
        public EntityList<BloodBReqForm> SearchBloodBReqFormListByBBagCodeAndHql(string strHqlWhere, string scanCodeField, string bbagCode, string sort, int page, int limit)
        {
            EntityList<BloodBReqForm> entityList = new EntityList<BloodBReqForm>();
            return IDBloodBPreItemDao.SearchBloodBReqFormListByBBagCodeAndHql(strHqlWhere, scanCodeField, bbagCode, sort, page, limit);
        }
        #endregion

        public IList<WarpAndDeptVO> GetWarpAndDeptList(string strHqlWhere, string sort, int page, int limit)
        {
            IList<WarpAndDeptVO> entityList = new List<WarpAndDeptVO>();
            StringBuilder sqlHql = new StringBuilder();//DISTINCT
            sqlHql.Append(" select new WarpAndDeptVO(bloodbreqform.DeptNo,bloodbreqform.WardNo,bloodbreqform.HisWardNo) from BloodBReqForm bloodbreqform,Department department where bloodbreqform.DeptNo=department.Id and (department.ParentID is null or department.ParentID=0) ");

            sqlHql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(strHqlWhere))
                sqlHql.Append(" and " + strHqlWhere);
            sqlHql.Append(" and " + BaseDataFilter.GetDataRowRoleHQLString());
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);

            string strHQL = sqlHql.ToString();
            int? start1 = null;
            int? count1 = null;
            if (page > 0)
                start1 = page;
            if (limit > 0)
                count1 = limit;
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBSearchByHqlAction<List<WarpAndDeptVO>, WarpAndDeptVO> action = new DaoNHBSearchByHqlAction<List<WarpAndDeptVO>, WarpAndDeptVO>(strHQL, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<WarpAndDeptVO>>(action);
            if (entityList != null) entityList = entityList.Distinct().ToList();
            return entityList;
        }
    }
}