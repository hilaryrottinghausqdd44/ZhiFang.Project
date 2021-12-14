using NHibernate.Metadata;
using NHibernate.Persister.Entity;
using Spring.Data.NHibernate.Generic;
using Spring.Data.NHibernate.Generic.Support;
using System;
using System.Collections.Generic;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;

namespace ZhiFang.DAO.NHB.Base
{
    public abstract class BaseDaoNHBService<T, T1> : HibernateDaoSupport, IDBaseDao<T, T1> where T : BaseEntityService
    {
        #region IBaseDao<T,T1> 成员

        public BaseDaoNHBService()
        {

        }
        public EntityList<T> GetListBySQLAndHQL(string strHQL, string strHqlWhere, int start, int count)
        {
            Dictionary<string, BTDMacroCommand> dicmc = BTDMacroCommandDao.DicBTDMacroCommand;

            EntityList<T> entityList = new EntityList<T>();

            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += " and " + strHqlWhere;
            }
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
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

        public virtual int GetTotalCount()
        {
            int count = this.GetListCountByHQL(BaseDataFilter.GetDataRowRoleHQLString<T>());
            return count;
            //Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            //dic.Add(typeof(T).Name, null);
            //DaoNHBCriteriaCountAction<T> action = new DaoNHBCriteriaCountAction<T>(dic);
            //int count = this.HibernateTemplate.Execute<int>(action);
            //return count;
        }

        public virtual IList<T> GetObjects(T entity)
        {
            string tmphql = " 1=1 ";
            tmphql += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
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
            IList<T> ilist = this.HibernateTemplate.Find<T>(" select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where " + BaseDataFilter.GetDataRowRoleHQLString<T>() + " and " + typeof(T).Name.ToLower() + ".id=" + id);
            if (ilist != null && ilist.Count > 0)
            {
                return ilist[0];
            }
            else
            {
                return null;
            }
        }

        public virtual IList<T> Find(string hql)
        {
            return this.HibernateTemplate.Find<T>(hql + " and " + BaseDataFilter.GetDataRowRoleHQLString<T>());
        }

        public virtual bool Save(T entity)
        {
            ((BaseEntityService)entity).DataAddTime = DateTime.Now;
            this.HibernateTemplate.Save(entity);
            return true;
        }

        public virtual object SaveByEntity(T entity)
        {
            ((BaseEntityService)entity).DataAddTime = DateTime.Now;
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
            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            IList<T> ilist = this.HibernateTemplate.Find<T>(BaseDataFilter.FilterMacroCommand(strHQL + " and " + BaseDataFilter.GetDataRowRoleHQLString<T>()));
            return ilist;
        }

        public virtual int UpdateByHql(string hql)
        {
            var result = this.HibernateTemplate.Execute<int>(new DaoNHBHqlAction(BaseDataFilter.FilterMacroCommand(hql)));
            return result;
        }

        public virtual int DeleteByHql(string hql)
        {
            var result = this.HibernateTemplate.Delete(BaseDataFilter.FilterMacroCommand(hql));
            return result;
        }

        public EntityList<T> GetListByHQL(string strHqlWhere, int start, int count)
        {
            Dictionary<string, BTDMacroCommand> dicmc = BTDMacroCommandDao.DicBTDMacroCommand;
            // IList<BTDMacroCommand> listmc1 = (List<BTDMacroCommand>)this.HibernateTemplate.Find<BTDMacroCommand>(" from BTDMacroCommand ");
            EntityList<T> entityList = new EntityList<T>();

            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                //for (int i = 0; i < dicmc.Count; i++)
                //{
                //    strHqlWhere = strHqlWhere.Replace(dicmc.ElementAt(i).Key, dicmc.ElementAt(i).Value.ClassCode);
                //}
                strHQL += " and " + strHqlWhere;
            }
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
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
            EntityList<T> entityList = new EntityList<T>();

            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += " and " + strHqlWhere;
            }
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
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
            Dictionary<string, BTDMacroCommand> dicmc = BTDMacroCommandDao.DicBTDMacroCommand;
            EntityList<T> entityList = new EntityList<T>();
            if (!String.IsNullOrEmpty(strHQL))
            {
                strHQL = strHQL + " where 1=1 ";
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                {
                    strHQL += " and " + strHqlWhere;
                }
                strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
                strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
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
            EntityList<T> entityList = new EntityList<T>();
            if (!String.IsNullOrEmpty(strHQL))
            {
                strHQL = strHQL + " where 1=1 ";
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                {
                    strHQL += " and " + strHqlWhere;
                }
                strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
                strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
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

        public EntityList<T> GetListByHQL(string strHqlWhere, string Order, int start, int count, string strHQL, string strHQLCount)
        {
            EntityList<T> entityList = new EntityList<T>();
            if (!String.IsNullOrEmpty(strHQL))
            {
                strHQL = strHQL + " where 1=1 ";
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                {
                    strHQL += " and " + strHqlWhere;
                }
                strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
                strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
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
                entityList.count = this.GetListCountByHQL(strHqlWhere, strHQLCount);
            }

            return entityList;
        }

        public int GetListCountByHQL(string strHqlWhere)
        {
            string strHQL = "select count(*) from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            if (strHqlWhere != null && strHqlWhere.Length > 0 && strHqlWhere.IndexOf(" order by") >= 0)
            {
                strHqlWhere = strHqlWhere.Substring(0, strHqlWhere.IndexOf(" order by"));
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHQL += " and " + strHqlWhere;
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<T> action = new DaoNHBGetCountByHqlAction<T>(strHQL);
            return this.HibernateTemplate.Execute<int>(action);
        }

        public int GetListCountByHQL(string strHqlWhere, string strHQLCount)
        {
            string strHQL = strHQLCount + " where 1=1 ";
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            if (strHqlWhere != null && strHqlWhere.Length > 0 && strHqlWhere.IndexOf(" order by") >= 0)
            {
                strHqlWhere = strHqlWhere.Substring(0, strHqlWhere.IndexOf(" order by"));
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHQL += " and " + strHqlWhere;
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<T> action = new DaoNHBGetCountByHqlAction<T>(strHQL);
            return this.HibernateTemplate.Execute<int>(action);
        }

        public int GetCurPageByHQL(long id, string strHqlWhere, string Order, int count)
        {
            int resultPage = -1;
            string tableName = "";
            string mainFieldName = "";
            if (count <= 0)
            {
                ZhiFang.Common.Log.Log.Info("分页定位limit<=0");
                return -1;
            }
            IClassMetadata classMetadata = this.SessionFactory.GetClassMetadata(typeof(T));
            if (classMetadata != null)
            {
                tableName = ((SingleTableEntityPersister)classMetadata).TableName;
                NHibernate.Type.IType IdentifierType = classMetadata.IdentifierType;
                //-----取类的主键信息---//
                Type componentType = typeof(NHibernate.Type.ComponentType);//复合主键标识
                Type realType = IdentifierType.GetType();

                if (componentType.ToString() == realType.ToString())//为复合主键
                {
                    // 取每个主键属性。
                    NHibernate.Type.ComponentType currentType = ((NHibernate.Type.ComponentType)IdentifierType);
                    string[] columnNames = new string[currentType.PropertyNames.Length];
                    for (int index = 0; index < currentType.PropertyNames.Length; index++)
                    {
                        columnNames[index] = ((SingleTableEntityPersister)classMetadata).IdentifierColumnNames[index];
                    }
                    foreach (string str in columnNames)
                    {
                        if (mainFieldName != null && mainFieldName.Length > 0)
                            mainFieldName = str;
                        else
                            mainFieldName += "," + str;
                    }
                }
                else  //为单一主键
                {
                    //string[] ColumnNames = new string[1];
                    //ColumnNames[0] = ((SingleTableEntityPersister)classMetadata).IdentifierColumnNames[0];
                    mainFieldName = ((SingleTableEntityPersister)classMetadata).IdentifierColumnNames[0];
                }
            }
            if (tableName != "" && mainFieldName != "")
            {
                string strSort = "";
                string strWhere = "";
                if (Order != null && Order.Trim().Length > 0)
                {
                    strSort += " ROW_NUMBER() OVER(ORDER BY " + Order + ") as SortNumber, ";
                }
                string strHQL = "select " + strSort + " " + mainFieldName + " from " + tableName + " " + typeof(T).Name.ToLower() + " where 1=1 ";
                string strHQLCount = "select count(*) from " + tableName + " " + typeof(T).Name.ToLower() + " where 1=1 ";
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                    strWhere += " and " + strHqlWhere;
                strWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
                strWhere = BaseDataFilter.FilterMacroCommand(strWhere);
                strHQLCount += strWhere;
                strHQL = "Select SortNumber from (" + strHQL + strWhere + ") MM where " + mainFieldName + "=" + id + " order by SortNumber  ";
                ZhiFang.Common.Log.Log.Info("分页定位SQLCount：" + strHQLCount);
                ZhiFang.Common.Log.Log.Info("分页定位SQL：" + strHQL);
                IList<int> tempCountList = this.Session.CreateSQLQuery(strHQLCount).List<int>();
                if (tempCountList != null && tempCountList.Count > 0)
                {
                    int allCount = tempCountList[0];
                    if (allCount > count)
                    {
                        IList<object> tempList = this.Session.CreateSQLQuery(strHQL).List<object>();
                        if (tempList != null && tempList.Count > 0)
                        {
                            int index = Convert.ToInt32(tempList[0]);
                            int pageCount = allCount % count == 0 ? allCount / count : allCount / count + 1;
                            for (int i = 1; i <= pageCount; i++)
                            {
                                if (index >= ((i - 1) * count + 1) && index <= (i * count))
                                {
                                    resultPage = i;
                                    break;
                                }
                                else
                                    resultPage = 1;
                            }
                        }
                        else
                        {
                            resultPage = 1;
                            ZhiFang.Common.Log.Log.Info("分页定位根据Id【" + id + "】获取不到数据");
                        }
                    }
                    else
                        resultPage = 1;
                }
                else
                    ZhiFang.Common.Log.Log.Info("分页定位总记录数等于0");
            }
            else
                ZhiFang.Common.Log.Log.Info("分页定位无法获取表名或字段名");
            return resultPage;
        }

        public EntityPageList<T> GetListByHQL(long id, string strHqlWhere, string Order, int start, int count)
        {
            EntityPageList<T> entityList = new EntityPageList<T>();
            start = GetCurPageByHQL(id, strHqlWhere, Order, count);
            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += " and " + strHqlWhere;
            }
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
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
            entityList.page = start;
            return entityList;
        }

        public EntityPageList<T> GetListByHQL(long id, string strHqlWhere, string Order, int start, int count, string strHQL, string strHQLCount)
        {
            EntityPageList<T> entityList = new EntityPageList<T>();
            int curPage = GetCurPageByHQL(id, strHqlWhere, Order, count);
            if (curPage > 0)
                start = curPage;
            EntityList<T> tempList = GetListByHQL(strHqlWhere, Order, start, count, strHQL, strHQLCount);
            entityList.count = tempList.count;
            entityList.list = tempList.list;
            entityList.page = start;
            return entityList;
        }

        public IList<T> GetListByHQL(string strHqlWhere, string[] listEntityName)
        {
            string strFetchHQL = "";
            string entityTypeName = typeof(T).Name;
            string entityTypeNameLower = entityTypeName.ToLower();
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
            }
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strHqlWhere = " and " + strHqlWhere;
            strHqlWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            strHqlWhere = BaseDataFilter.FilterMacroCommand(strHqlWhere);
            string strHQL = "select " + entityTypeNameLower + " from " + entityTypeName + " " + entityTypeNameLower + " " + strFetchHQL + " where 1=1 " + strHqlWhere;
            IList<T> listLisTestForm = this.Session.CreateQuery(strHQL).List<T>();
            return listLisTestForm;
        }

        public EntityList<T> GetListByHQL(string strHqlWhere, string order, int start, int count, string[] listEntityName)
        {
            string strFetchHQL = "";
            string strNotFetchHQL = "";
            string entityTypeName = typeof(T).Name;
            string entityTypeNameLower = entityTypeName.ToLower();
            foreach (string entityName in listEntityName)
            {
                strFetchHQL += " left join fetch " + entityName;
                strNotFetchHQL += " left join " + entityName;
            }
            string strHQL = "select " + entityTypeNameLower + " from " + entityTypeName + " " + entityTypeNameLower + " " + strFetchHQL;
            string strHQLCount = "select count(*) from " + entityTypeName + " " + entityTypeNameLower + " " + strNotFetchHQL;
            EntityList<T> entityList = this.GetListByHQL(strHqlWhere, order, start, count, strHQL, strHQLCount);
            return entityList;
        }

        #endregion

        #region IDBaseDao<T,T1> 成员


        public IList<T> GetListByHQL(string strHqlWhere)
        {
            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += " and " + strHqlWhere;
            }
            strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);
            return HibernateTemplate.Find<T>(strHQL);
        }

        public object GetTotalByHQL(string strHqlWhere, string fields)
        {
            if (fields != null && fields.Trim() != "")
            {
                string tmp = "";
                foreach (var f in fields.Split(','))
                {
                    if (f != null)
                        tmp += "sum(" + f.Trim() + "),";
                }
                if (tmp.Length > 0)
                {
                    tmp = tmp.Remove(tmp.LastIndexOf(','));
                }
                string strHQL = "select " + tmp + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
                strHQL += " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
                if (strHqlWhere != null && strHqlWhere.Length > 0 && strHqlWhere.IndexOf(" order by") >= 0)
                {
                    strHqlWhere = strHqlWhere.Substring(0, strHqlWhere.IndexOf(" order by"));
                }
                if (strHqlWhere != null && strHqlWhere.Length > 0)
                    strHQL += " and " + strHqlWhere;
                strHQL = BaseDataFilter.FilterMacroCommand(strHQL);//宏命令过滤
                DaoNHBGetTotalByHqlAction<T> action = new DaoNHBGetTotalByHqlAction<T>(strHQL);
                return this.HibernateTemplate.Execute(action);
            }
            return null;
        }

        #endregion

        public string GetBaseDataFilter()
        {
            string strHQL = " and " + BaseDataFilter.GetDataRowRoleHQLString<T>();
            strHQL = BaseDataFilter.FilterMacroCommand(strHQL);

            return strHQL;
        }
    }

}
