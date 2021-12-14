using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.NHibernate.Generic.Support;
using ZhiFang.IDAO.Base;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;
using Spring.Data.NHibernate.Generic;
using ZhiFang.Entity.RBAC;
using Spring.Context;
using Spring.Context.Support;
using System.Reflection;
using ZhiFang.Entity.Base;
using NHibernate.Transform;

namespace ZhiFang.DAO.NHB.Base
{
    public abstract class BaseDaoNHB<T, T1> : HibernateDaoSupport, IDBaseDao<T, T1> where T : BaseEntity
    {
        #region IBaseDao<T,T1> 成员
        public string DataRowRoleHQLString = "";
        public BaseDaoNHB()
        {

        }
        public EntityList<T> GetListBySQLAndHQL(string strHQL, string strHqlWhere, int start, int count)
        {
            GetDataRowRoleHQLString();
            //Dictionary<string, BTDMacroCommand> dicmc = BTDMacroCommandDao.DicBTDMacroCommand;

            EntityList<T> entityList = new EntityList<T>();

            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += "and " + strHqlWhere;
            }
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            int? start1 = null;
            int? count1 = null;
            if (start > 0)
            {
                start1 = start;
            }
            if (count > 0)
            {
                count1 = count;
            }
            try
            {
                DaoNHBSearchByHqlAction<List<T>, T> action = new DaoNHBSearchByHqlAction<List<T>, T>(strHQL, start1, count1);
                //ISQLQuery query = this.Session.CreateSQLQuery(strHQL);
                //IList<T> list = query.List<T>();
                entityList.list = this.HibernateTemplate.Execute<List<T>>(action);
                entityList.count = (entityList.list != null ? entityList.list.Count : 0);
            }
            catch (Exception ee)
            {
                ZhiFang.Common.Log.Log.Error(ee.Message);
                throw ee;
            }
            return entityList;
        }
        public void GetDataRowRoleHQLString()
        {
            IApplicationContext context = ContextRegistry.GetContext();
            object bslog = context.GetObject("DataRowRoleHQL");
            DataRowRoleHQLString = ((DataRowRoleHQL)bslog).Hql;
            if (DataRowRoleHQLString == null || DataRowRoleHQLString.Trim().Length == 0)
            {
                DataRowRoleHQLString = " 1=1 ";
            }
            DataRowRoleHQLString = string.Format("({0})", FilterMacroCommand(DataRowRoleHQLString));
            string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
            string IsLabFlag = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag);
            if (IsLabFlag != null && IsLabFlag.Trim() == "1")
            {
                if (labid != null && labid.Trim() != "")
                {
                    DataRowRoleHQLString += " and LabID=" + labid + " ";
                }
            }
            //ZhiFang.Common.Log.Log.Info("根据权限获取的where=" + DataRowRoleHQLString);
        }
        public virtual int GetTotalCount()
        {
            GetDataRowRoleHQLString();
            int count = this.GetListCountByHQL(DataRowRoleHQLString);
            return count;
            //Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            //dic.Add(typeof(T).Name, null);
            //DaoNHBCriteriaCountAction<T> action = new DaoNHBCriteriaCountAction<T>(dic);
            //int count = this.HibernateTemplate.Execute<int>(action);
            //return count;
        }

        public virtual IList<T> GetObjects(T entity)
        {
            GetDataRowRoleHQLString();
            string tmphql = " 1=1 ";
            tmphql += " and " + DataRowRoleHQLString;
            Type t = typeof(T);
            var c = (T)entity;
            foreach (var tt in t.GetProperties())
            {
                if (tt != null)
                {
                    object o = tt.GetValue(c, null);
                    if (o != null && o.ToString().IndexOf("List") < 0)
                    {
                        long v = 0;
                        try
                        {
                            v = Convert.ToInt64(o);
                        }
                        catch
                        {
                            v = -1;
                        }
                        if (v != 0)
                        {
                            tmphql += " and " + t.Name.ToLower() + "." + tt.Name + " = " + o.ToString();
                        }
                    }
                }
            }
            return GetListByHQL(tmphql, -1, -1).list;
        }

        public virtual T Get(T1 id)
        {
            GetDataRowRoleHQLString();
            IList<T> ilist = this.HibernateTemplate.Find<T>(" select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where " + DataRowRoleHQLString + " and " + typeof(T).Name.ToLower() + ".id=" + id);
            if (ilist != null && ilist.Count > 0)
            {
                return ilist[0];
            }
            else
            {
                return null;
            }
        }

        public virtual IList<T> Find<T>(string hql)
        {
            GetDataRowRoleHQLString();
            return this.HibernateTemplate.Find<T>(hql + " and " + DataRowRoleHQLString);
        }

        public virtual bool Save(T entity)
        {
            ((BaseEntity)entity).DataAddTime = DateTime.Now;
            this.HibernateTemplate.Save(entity);
            return true;
        }

        public virtual object SaveByEntity(T entity)
        {
            ((BaseEntity)entity).DataAddTime = DateTime.Now;
            return this.HibernateTemplate.Save(entity);
        }

        public virtual bool BatchSaveVO(T voList)
        {
            this.HibernateTemplate.SaveOrUpdateCopy(voList);
            return true;
        }

        public virtual bool Update(T entity)
        {
            this.HibernateTemplate.Update(entity);
            return true;
        }

        public virtual bool Update(string[] strParas)
        {
            string strEntityName = typeof(T).Name;
            string strEntityNameLower = typeof(T).Name.ToLower();
            string strWhere = "";
            StringBuilder sb = new StringBuilder();

            sb.Append("update " + strEntityName + " " + strEntityNameLower + " set ");
            foreach (string s in strParas)
            {
                if (s.Trim().IndexOf("Id=") >= 0 && s.Trim().IndexOf(".Id=") < 0)
                {
                    if (s.Trim().Split('=')[0].Trim() == "Id")
                        strWhere = s;
                    else
                        sb.Append(strEntityNameLower + "." + s + ", ");
                }
                else
                {
                    if (s.Trim().IndexOf("DataTimeStamp=") >= 0 || s.Trim().IndexOf(".DataTimeStamp=") >= 0)
                    {

                    }
                    else
                    {
                        sb.Append(strEntityNameLower + "." + s + ", ");
                    }
                }
            }
            int n = sb.ToString().LastIndexOf(",");
            sb.Remove(n, 1);
            sb.Append("where " + strEntityNameLower + "." + strWhere);
            ZhiFang.Common.Log.Log.Debug(sb.ToString());
            int row = this.UpdateByHql(sb.ToString());
            if (row > 0)
                return true;
            else
                return false;
        }

        public virtual bool SaveOrUpdate(T entity)
        {
            this.HibernateTemplate.SaveOrUpdate(entity);
            return true;
        }

        public virtual bool Delete(T entity)
        {
            this.HibernateTemplate.Delete(entity);
            return true;
        }

        public virtual bool Delete(T1 id)
        {
            try
            {
                this.HibernateTemplate.Delete(Get(id));
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.ToUpper().IndexOf("REFERENCE") >= 0)
                {
                    throw new Exception("该对象被引用，不能删除！");
                }
                else
                    throw ex;
            }
        }

        public virtual bool DeleteByHQL(T1 id)
        {
            try
            {
                this.HibernateTemplate.Delete(" from " + typeof(T).Name + " t where t.Id=" + id.ToString());
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException != null && ex.InnerException.InnerException.Message.ToUpper().IndexOf("REFERENCE") >= 0)
                {
                    throw new Exception("该对象被引用，不能删除！");
                }
                else
                    throw ex;
            }
        }

        public virtual void Flush()
        {
            this.HibernateTemplate.Flush();
        }

        public virtual void Evict(T entity)
        {
            this.HibernateTemplate.Evict(entity);
        }

        public virtual IList<T> LoadAll()
        {
            GetDataRowRoleHQLString();
            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            IList<T> ilist = this.HibernateTemplate.Find<T>(FilterMacroCommand(strHQL + " and " + DataRowRoleHQLString));
            return ilist;
        }

        public virtual int UpdateByHql(string hql)
        {
            var result = this.HibernateTemplate.Execute<int>(new DaoNHBHqlAction(FilterMacroCommand(hql)));
            return result;
        }

        public virtual int DeleteByHql(string hql)
        {
            var result = this.HibernateTemplate.Delete(FilterMacroCommand(hql));
            return result;
        }

        public EntityList<T> GetListByHQL(string strHqlWhere, int start, int count)
        {
            GetDataRowRoleHQLString();
            //Dictionary<string, BTDMacroCommand> dicmc = BTDMacroCommandDao.DicBTDMacroCommand;
            // IList<BTDMacroCommand> listmc1 = (List<BTDMacroCommand>)this.HibernateTemplate.Find<BTDMacroCommand>(" from BTDMacroCommand ");
            EntityList<T> entityList = new EntityList<T>();

            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                //for (int i = 0; i < dicmc.Count; i++)
                //{
                //    strHqlWhere = strHqlWhere.Replace(dicmc.ElementAt(i).Key, dicmc.ElementAt(i).Value.ClassCode);
                //}
                strHQL += "and " + strHqlWhere;
            }
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            //ZhiFang.Common.Log.Log.Debug("strHQL="+ strHQL);
            int? start1 = null;
            int? count1 = null;
            if (start > 0)
            {
                start1 = start;
            }
            if (count > 0)
            {
                count1 = count;
            }
            DaoNHBSearchByHqlAction<List<T>, T> action = new DaoNHBSearchByHqlAction<List<T>, T>(strHQL, start1, count1);

            entityList.list = this.HibernateTemplate.Execute<List<T>>(action);
            entityList.count = this.GetListCountByHQL(strHqlWhere);
            //entityList.count = (entityList.list != null ? entityList.list.Count : 0);
            return entityList;
        }
        public EntityList<T> GetListByHQL(string strHqlWhere, string Order, int start, int count)
        {
            GetDataRowRoleHQLString();

            EntityList<T> entityList = new EntityList<T>();

            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += "and " + strHqlWhere;
            }
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            if (Order != null && Order.Trim().Length > 0)
            {
                strHQL += " order by " + Order;
            }
            int? start1 = null;
            int? count1 = null;
            if (start > 0)
            {
                start1 = start;
            }
            if (count > 0)
            {
                count1 = count;
            }
            DaoNHBSearchByHqlAction<List<T>, T> action = new DaoNHBSearchByHqlAction<List<T>, T>(strHQL, start1, count1);

            entityList.list = this.HibernateTemplate.Execute<List<T>>(action);
            entityList.count = this.GetListCountByHQL(strHqlWhere);
            //entityList.count = (entityList.list != null ? entityList.list.Count : 0);
            return entityList;
        }

        public EntityList<T> GetListByHQL(string strHqlWhere, int start, int count, string strHQL)
        {
            GetDataRowRoleHQLString();
            //Dictionary<string, BTDMacroCommand> dicmc = BTDMacroCommandDao.DicBTDMacroCommand;
            EntityList<T> entityList = new EntityList<T>();
            if (!String.IsNullOrEmpty(strHQL))
            {
                strHQL = strHQL + " where 1=1 ";
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                {
                    strHQL += "and " + strHqlWhere;
                }
                strHQL += " and " + DataRowRoleHQLString;
                strHQL = FilterMacroCommand(strHQL);//宏命令过滤
                int? start1 = null;
                int? count1 = null;
                if (start > 0)
                {
                    start1 = start;
                }
                if (count > 0)
                {
                    count1 = count;
                }
                DaoNHBSearchByHqlAction<List<T>, T> action = new DaoNHBSearchByHqlAction<List<T>, T>(strHQL, start1, count1);
                entityList.list = this.HibernateTemplate.Execute<List<T>>(action);
                entityList.count = this.GetListCountByHQL(strHqlWhere);
            }

            return entityList;
        }
        public EntityList<T> GetListByHQL(string strHqlWhere, string Order, int start, int count, string strHQL)
        {
            GetDataRowRoleHQLString();
            EntityList<T> entityList = new EntityList<T>();
            if (!String.IsNullOrEmpty(strHQL))
            {
                strHQL = strHQL + " where 1=1 ";
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                {
                    strHQL += "and " + strHqlWhere;
                }
                strHQL += " and " + DataRowRoleHQLString;
                strHQL = FilterMacroCommand(strHQL);//宏命令过滤
                if (Order != null && Order.Trim().Length > 0)
                {
                    strHQL += " order by " + Order;
                }
                int? start1 = null;
                int? count1 = null;
                if (start > 0)
                {
                    start1 = start;
                }
                if (count > 0)
                {
                    count1 = count;
                }
                DaoNHBSearchByHqlAction<List<T>, T> action = new DaoNHBSearchByHqlAction<List<T>, T>(strHQL, start1, count1);

                entityList.list = this.HibernateTemplate.Execute<List<T>>(action);
                entityList.count = this.GetListCountByHQL(strHqlWhere);
            }

            return entityList;
        }

        public int GetListCountByHQL(string strHqlWhere)
        {
            GetDataRowRoleHQLString();
            string strHQL = "select count(*) from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            strHQL += " and " + DataRowRoleHQLString;
            if (strHqlWhere != null && strHqlWhere.Length > 0 && strHqlWhere.IndexOf(" order by") >= 0)
            {
                strHqlWhere = strHqlWhere.Substring(0, strHqlWhere.IndexOf(" order by"));
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHQL += "and " + strHqlWhere;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<T> action = new DaoNHBGetCountByHqlAction<T>(strHQL);
            return this.HibernateTemplate.Execute<int>(action);
        }

        public object GetTotalByHQL(string strHqlWhere,string fields)
        {
            GetDataRowRoleHQLString();
            if (fields != null && fields.Trim() != "")
            {
                string tmp = "";
                foreach (var f in fields.Split(','))
                {
                    if(f!=null)
                    tmp += "sum(" + f.Trim() + "),";
                }
                if (tmp.Length > 0)
                {
                    tmp = tmp.Remove(tmp.LastIndexOf(','));
                }
                string strHQL = "select "+ tmp + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
                strHQL += " and " + DataRowRoleHQLString;
                if (strHqlWhere != null && strHqlWhere.Length > 0 && strHqlWhere.IndexOf(" order by") >= 0)
                {
                    strHqlWhere = strHqlWhere.Substring(0, strHqlWhere.IndexOf(" order by"));
                }
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                    strHQL += "and " + strHqlWhere;
                strHQL = FilterMacroCommand(strHQL);//宏命令过滤
                DaoNHBGetTotalByHqlAction<T> action = new DaoNHBGetTotalByHqlAction<T>(strHQL);
                return this.HibernateTemplate.Execute(action);
            }
            return null;
        }
        public string FilterMacroCommand(string hql)//宏命令过滤
        {
            //BTDMacroCommandDao btdmc = new BTDMacroCommandDao();
            //string tmpFhql = "";
            //string tmpLhql = "";
            //string tmphql = "";
            //string tmpp = "";
            //foreach (var a in BTDMacroCommandDao.DicBTDMacroCommand)
            //{
            //    if (a.Value.TypeCode == "DateTimeDynamic")
            //    {
            //        while (hql.IndexOf(a.Key) >= 0)
            //        {
            //            tmpFhql = "";
            //            tmpLhql = "";
            //            tmpp = "";
            //            tmpFhql = hql.Substring(0, hql.IndexOf(a.Key));
            //            tmphql = hql.Substring(hql.IndexOf(a.Key) + a.Key.Length, hql.Length - hql.IndexOf(a.Key) - a.Key.Length);
            //            tmpp = tmphql.Substring(0, tmphql.IndexOf("$"));
            //            tmpLhql = tmphql.Substring(tmphql.IndexOf("$") + 1, tmphql.Length - tmphql.IndexOf("$") - 1);
            //            string[] p = tmpp.Split(',');
            //            ArrayList al = new ArrayList();
            //            foreach (var p1 in p)
            //            {
            //                al.Add(p1);
            //            }
            //            a.Value.Parameter = al;
            //            hql = hql.Replace(a.Key + tmpp + "$", a.Value.ClassCode);
            //        }
            //    }
            //    if (a.Value.TypeCode == "DateTime")
            //    {
            //        hql = hql.Replace(a.Key, "'" + a.Value.ClassCode + "'");
            //    }
            //    if (a.Value.TypeCode == "StrUserInfo")
            //    {
            //        hql = hql.Replace(a.Key, "'" + a.Value.ClassCode + "'");
            //    }
            //    if (a.Value.TypeCode == "IntUserInfo")
            //    {
            //        hql = hql.Replace(a.Key, a.Value.ClassCode);
            //    }
            //    if (a.Value.TypeCode == "IntDynamic")
            //    {
            //        while (hql.IndexOf(a.Key) >= 0)
            //        {
            //            tmpFhql = "";
            //            tmpLhql = "";
            //            tmpp = "";
            //            tmpFhql = hql.Substring(0, hql.IndexOf(a.Key));
            //            tmphql = hql.Substring(hql.IndexOf(a.Key) + a.Key.Length, hql.Length - hql.IndexOf(a.Key) - a.Key.Length);
            //            tmpp = tmphql.Substring(0, tmphql.IndexOf("$"));
            //            tmpLhql = tmphql.Substring(tmphql.IndexOf("$") + 1, tmphql.Length - tmphql.IndexOf("$") - 1);
            //            string[] p = tmpp.Split(',');
            //            ArrayList al = new ArrayList();
            //            foreach (var p1 in p)
            //            {
            //                al.Add(p1);
            //            }
            //            a.Value.Parameter = al;
            //            hql = hql.Replace(a.Key + tmpp + "$", a.Value.ClassCode);
            //        }
            //    }
            //}
            return hql;
        }
        #endregion

        #region
        //public virtual int GetTotalCount(T t)
        //{
        //    Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
        //    dic.Add(typeof(T).Name, new List<ICriterion>() { Example.Create(t).ExcludeZeroes() });
        //    DaoNHBCriteriaCountAction<T> action = new DaoNHBCriteriaCountAction<T>(dic);
        //    int count = this.HibernateTemplate.Execute<int>(action);
        //    return count;
        //}
        //public virtual T Load(T1 id)
        //{
        //    return this.HibernateTemplate.Load<T>(id);
        //}
        #endregion

        #region IDBaseDao<T,T1> 成员


        public IList<T> GetListByHQL(string strHqlWhere)
        {
            GetDataRowRoleHQLString();
            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += "and " + strHqlWhere;
            }
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);
            return HibernateTemplate.Find<T>(strHQL);
        }

        #endregion
    }

    public class DaoNHBSearchByHqlAction<T, T1> : IHibernateCallback<T>
    {
        private string hql;
        private int? start;
        private int? count;
        public string HQL
        {
            get { return hql; }
            set { hql = value; }
        }
        public int? Start
        {
            get { return start; }
        }
        public int? Count
        {
            get { return count; }
        }
        public DaoNHBSearchByHqlAction(string hql, int? start, int? count)
        {
            this.hql = hql;
            this.start = start;
            this.count = count;
        }
        public T DoInHibernate(ISession session)
        {
            IQuery query = null;
            if (hql != null && hql.Length > 0)
            {
                query = session.CreateQuery(hql);
            }
            if (query != null && this.start != null && this.start.Value > 0)
            {
                query.SetFirstResult((this.start.Value - 1) * this.count.Value);
                if (this.count != null)
                {
                    query.SetMaxResults(this.count.Value);
                }
            }
            return (T)query.List<T1>();

        }
    }

    public class DaoNHBSearchByHqlActionTSQLHash<T> : IHibernateCallback<T>
    {
        private string hql;
        public DaoNHBSearchByHqlActionTSQLHash(string hql)
        {
            this.hql = hql;
        }
        public T DoInHibernate(ISession session)
        {
            IQuery query = null;
            if (hql != null && hql.Length > 0)
            {
                query = session.CreateSQLQuery(hql);
            }
            ZhiFang.Common.Log.Log.Debug("DAO.NHB.Base.DaoNHBSearchByHqlActionTSQLHash: sql = " + hql);
            return (T) query.SetResultTransformer(Transformers.AliasToEntityMap).List();
            
        }
    }
    public class DaoNHBGetCountByHqlAction<T> : IHibernateCallback<int>
    {
        private string hql;
        public string HQL
        {
            get { return hql; }
            set { hql = value; }
        }
        public DaoNHBGetCountByHqlAction(string hql)
        {
            this.hql = hql;
        }
        public int DoInHibernate(ISession session)
        {
            IQuery query = null;
            if (hql != null && hql.Length > 0)
            {
                query = session.CreateQuery(hql);
            }
            return Convert.ToInt32(query.UniqueResult());

        }
    }
    public class DaoNHBGetTotalByHqlAction<T> : IHibernateCallback<object>
    {
        private string hql;
        public string HQL
        {
            get { return hql; }
            set { hql = value; }
        }
        public DaoNHBGetTotalByHqlAction(string hql)
        {
            this.hql = hql;
        }
        public object DoInHibernate(ISession session)
        {
            IQuery query = null;
            if (hql != null && hql.Length > 0)
            {
                query = session.CreateQuery(hql);
            }
            return query.UniqueResult() ;

        }
    }
    public class DaoNHBGetMaxByHqlAction<T> : IHibernateCallback<long>
    {
        private string hql;
        public string HQL
        {
            get { return hql; }
            set { hql = value; }
        }
        public DaoNHBGetMaxByHqlAction(string hql)
        {
            this.hql = hql;
        }
        public long DoInHibernate(ISession session)
        {
            IQuery query = null;
            if (hql != null && hql.Length > 0)
            {
                query = session.CreateQuery(hql);
            }
            long maxNo = 0;
            if (query.UniqueResult() != null)
                maxNo = Convert.ToInt64(query.UniqueResult());
            return maxNo;

        }
    }
    public class DaoNHBHqlAction : IHibernateCallback<int>
    {
        private string hql;
        public string HQL
        {
            get { return hql; }
            set { hql = value; }
        }
        public DaoNHBHqlAction(string hql)
        {
            this.hql = hql;
        }
        public int DoInHibernate(ISession session)
        {
            return (int)session.CreateQuery(hql).ExecuteUpdate();

        }
    }
    public class DaoNHBCriteriaAction<T, T1> : IHibernateCallback<T>
    {
        private Dictionary<string, List<ICriterion>> conditionlist;
        private int? start;
        private int? count;
        private Order order;
        public Dictionary<string, List<ICriterion>> ConditionList
        {
            get { return conditionlist; }
        }
        public int? Start
        {
            get { return start; }
        }
        public int? Count
        {
            get { return count; }
        }
        public Order Order
        {
            get { return order; }
        }
        public DaoNHBCriteriaAction(Dictionary<string, List<ICriterion>> conditionlist, int start, int count, Order order)
        {
            this.conditionlist = conditionlist;
            this.start = start;
            this.count = count;
            this.order = order;
        }
        public DaoNHBCriteriaAction(Dictionary<string, List<ICriterion>> conditionlist, Order order)
        {
            this.conditionlist = conditionlist;
            this.order = order;
        }
        public DaoNHBCriteriaAction(Dictionary<string, List<ICriterion>> conditionlist)
        {
            this.conditionlist = conditionlist;
        }
        public T DoInHibernate(ISession session)
        {
            ICriteria ic = null;
            foreach (var a in conditionlist)
            {
                if (ic == null)
                {
                    ic = session.CreateCriteria(typeof(T1));
                }
                else
                {
                    ic = ic.CreateCriteria(a.Key);
                }
                if (ic != null && a.Value != null)
                {
                    for (int i = 0; i < a.Value.Count(); i++)
                    {
                        ic.Add(a.Value[i]);
                    }
                }
            }
            if (this.start != null && this.start.Value > 0)
            {
                ic.SetFirstResult((this.start.Value - 1) * this.count.Value);

                if (this.count != null)
                {
                    ic.SetMaxResults(this.count.Value);
                }
            }
            if (this.order != null)
            {
                ic.AddOrder(order);
            }
            return (T)ic.List<T1>();

        }
    }
    public class DaoNHBCriteriaCountAction<T> : IHibernateCallback<int>
    {
        private Dictionary<string, List<ICriterion>> conditionlist;
        public Dictionary<string, List<ICriterion>> ConditionList
        {
            get { return conditionlist; }
        }
        public DaoNHBCriteriaCountAction(Dictionary<string, List<ICriterion>> conditionlist)
        {
            this.conditionlist = conditionlist;
        }
        public int DoInHibernate(ISession session)
        {
            ICriteria ic = null;
            foreach (var a in conditionlist)
            {
                if (ic == null)
                {
                    ic = session.CreateCriteria(typeof(T)).SetProjection(Projections.RowCount());
                }
                else
                {
                    ic = ic.CreateCriteria(a.Key);
                }
                if (ic != null && a.Value != null)
                {
                    for (int i = 0; i < a.Value.Count(); i++)
                    {
                        ic.Add(a.Value[i]);
                    }
                    ic = ic.SetProjection(Projections.RowCount());
                }
            }
            return (int)ic.UniqueResult();
        }
    }
}
