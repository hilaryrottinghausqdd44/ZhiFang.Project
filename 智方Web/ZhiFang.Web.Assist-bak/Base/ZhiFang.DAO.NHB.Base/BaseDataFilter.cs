using System;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Metadata;
using NHibernate.Persister.Entity;
using System.Collections;
using ZhiFang.Entity.RBAC;
using Spring.Context;
using Spring.Context.Support;

namespace ZhiFang.DAO.NHB.Base
{
    public class BaseDataFilter
    {
        public static string GetDataRowRoleHQLString()
        {
            string DataRowRoleHQLString = "";
            IApplicationContext context = ContextRegistry.GetContext();
            if (context.ContainsObject("DataRowRoleHQL"))
            {
                object bslog = context.GetObject("DataRowRoleHQL");
                DataRowRoleHQLString = ((DataRowRoleHQL)bslog).Hql;
            }
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
            return DataRowRoleHQLString;
        }

        public static string FilterMacroCommand(string hql)//宏命令过滤
        {
            //ZhiFang.Common.Log.Log.Info("BTDMacroCommandDao");
            string tmpFhql = "";
            string tmpLhql = "";
            string tmphql = "";
            string tmpp = "";
            foreach (var a in BTDMacroCommandDao.DicBTDMacroCommand)
            {
                if (a.Value.TypeCode == "DateTimeDynamic")
                {
                    while (hql.IndexOf(a.Key) >= 0)
                    {
                        tmpFhql = "";
                        tmpLhql = "";
                        tmpp = "";
                        tmpFhql = hql.Substring(0, hql.IndexOf(a.Key));
                        tmphql = hql.Substring(hql.IndexOf(a.Key) + a.Key.Length, hql.Length - hql.IndexOf(a.Key) - a.Key.Length);
                        tmpp = tmphql.Substring(0, tmphql.IndexOf("$"));
                        tmpLhql = tmphql.Substring(tmphql.IndexOf("$") + 1, tmphql.Length - tmphql.IndexOf("$") - 1);
                        string[] p = tmpp.Split(',');
                        ArrayList al = new ArrayList();
                        foreach (var p1 in p)
                        {
                            al.Add(p1);
                        }
                        a.Value.Parameter = al;
                        hql = hql.Replace(a.Key + tmpp + "$", a.Value.ClassCode);
                    }
                }
                if (a.Value.TypeCode == "DateTime")
                {
                    hql = hql.Replace(a.Key, "'" + a.Value.ClassCode + "'");
                }
                if (a.Value.TypeCode == "StrUserInfo")
                {
                    hql = hql.Replace(a.Key, "'" + a.Value.ClassCode + "'");
                }
                if (a.Value.TypeCode == "IntUserInfo")
                {
                    hql = hql.Replace(a.Key, a.Value.ClassCode);
                }
                if (a.Value.TypeCode == "IntDynamic")
                {
                    while (hql.IndexOf(a.Key) >= 0)
                    {
                        tmpFhql = "";
                        tmpLhql = "";
                        tmpp = "";
                        tmpFhql = hql.Substring(0, hql.IndexOf(a.Key));
                        tmphql = hql.Substring(hql.IndexOf(a.Key) + a.Key.Length, hql.Length - hql.IndexOf(a.Key) - a.Key.Length);
                        tmpp = tmphql.Substring(0, tmphql.IndexOf("$"));
                        tmpLhql = tmphql.Substring(tmphql.IndexOf("$") + 1, tmphql.Length - tmphql.IndexOf("$") - 1);
                        string[] p = tmpp.Split(',');
                        ArrayList al = new ArrayList();
                        foreach (var p1 in p)
                        {
                            al.Add(p1);
                        }
                        a.Value.Parameter = al;
                        hql = hql.Replace(a.Key + tmpp + "$", a.Value.ClassCode);
                    }
                }
            }
            return hql;
        }
    }

    public class NHibernateHelper
    {
        public static void GetTableAndMainFieldName<T>(ISessionFactory sessionFactory, ref string tableName, ref string mainFieldName)
        {
            IClassMetadata classMetadata = sessionFactory.GetClassMetadata(typeof(T));
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
        }

        public static string GetSqlByHql<T>(ISession session, ISessionFactory sessionFactory, string strHqlWhere)
        {
            string strHQL = "select " + typeof(T).Name.ToLower() + " from " + typeof(T).Name + " " + typeof(T).Name.ToLower() + " where 1=1 ";
            string strWhere = "";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
                strWhere += " and " + strHqlWhere;
            strWhere += " and " + BaseDataFilter.GetDataRowRoleHQLString();
            strWhere = BaseDataFilter.FilterMacroCommand(strWhere);
            strHQL += strWhere;
            var tempSession = (NHibernate.Engine.ISessionImplementor)session;
            var sf = (NHibernate.Engine.ISessionFactoryImplementor)sessionFactory;
            //HQLStringQueryPlan参数shallow为true只返回主键id，否则返回全部字段
            var sql = new NHibernate.Engine.Query.HQLStringQueryPlan(strHQL, true, tempSession.EnabledFilters, sf);
            string strSQL = "";
            if (sql.SqlStrings != null && sql.SqlStrings.Length > 0)
            {
                strSQL = sql.SqlStrings[0].ToLower();
                int selectIndex = strSQL.IndexOf("select ") + 7;
                int fromIndex = strSQL.IndexOf(" from ");
                if (fromIndex > selectIndex)
                {
                    strSQL = strSQL.Remove(selectIndex, fromIndex - selectIndex);
                    strSQL = strSQL.Insert(selectIndex, "*");
                }
                else
                    strSQL = "";
            }
            return strSQL;
        }
    }

}
