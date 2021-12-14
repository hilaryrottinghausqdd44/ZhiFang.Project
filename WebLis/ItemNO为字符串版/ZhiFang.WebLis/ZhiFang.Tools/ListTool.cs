using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace ZhiFang.Tools
{
    public class ListTool
    {       
        /// <summary>
        /// ListPaging
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="page">home page</param>
        /// <param name="limit">条数</param>
        /// <param name="ls">List</param>
        /// <returns>List</returns>
        public List<T> Pagination<T>(int page,int limit,List<T> ls)
        {
            //调用范例
            //List<T> ceshi = Pagination<T>(page,limit, ls);
            //int page = 2;
            //int row = 3;
            if (ls == null || ls.Count == 0)
            { }
            Common.Log.Log.Info("第:" + page + "页，显示:" + limit + "条");
            List<T> Result = new List<T>();
            int rowCount = page * limit;
            if (ls.Count >= rowCount)
            {
                for (int i = rowCount - limit; i < rowCount; i++)
                {
                    Common.Log.Log.Info("1：" + ls[i].ToString());
                    Result.Add(ls[i]);
                }
            }
            else
            {
                if (ls.Count > rowCount - limit)
                {
                    for (int i = rowCount - limit; i < ls.Count; i++)
                    {
                        Common.Log.Log.Info("2：" + ls[i].ToString());
                        Result.Add(ls[i]);
                    }
                }
            }
            return Result;
        }

        /// <summary>
        /// DataSet转List
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List</returns>
        public List<T> GetListColumns<T>(DataTable table)
        {
            //调用范例
            //List<T> ceshi = GetListColumns<T>(table);

            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (value.GetType() == typeof(System.DBNull))
                        {
                            value = null;
                        }
                        pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// DataSet转List
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List</returns>
        public static List<T> GetListColumnsStatic<T>(DataTable table)
        {
            //调用范例
            //List<T> ceshi = GetListColumns<T>(table);

            List<T> list = new List<T>();
            T t = default(T);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<T>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (value.GetType() == typeof(System.DBNull))
                        {
                            value = null;
                        }
                        if (pro.PropertyType == typeof(System.Int32))
                        {
                            value = Convert.ToInt32(value);
                            pro.SetValue(t, Convert.ToInt32(value), null);
                            continue;
                        }
                        if (pro.PropertyType == typeof(System.Int32?))
                        {
                            if (value!= null&& value.ToString().Trim()!="")                            
                            {
                                value = Convert.ToInt32(value);
                                pro.SetValue(t, Convert.ToInt32(value), null);
                            }
                            continue;
                        }
                        pro.SetValue(t, value, null);
                    }
                }
                list.Add(t);
            }
            return list;
        }

        /// <summary>
        /// 无标题DataSet转List
        /// </summary>
        /// <typeparam name="T">entity class</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List</returns>
        //public List<string> GetList<T>(DataTable table)
        //{
        //    DataTable dt = table;
        //    List<string[]> list = new List<string[]>();
        //    foreach (DataRow r in dt.Rows)
        //    {
        //        int colCount = r.ItemArray.Count();
        //        string[] items = new string[colCount];
        //        for (int i = 0; i < colCount; i++)
        //        {
        //            items[i] = Convert.ToString(r.ItemArray[i]);
        //        }
        //        list.Add(items);
        //    }
        //    return list;
        //}


        public class CustomObject
        {
           public int count { get; set; }
           public List<string> Rows { get; set; }
        }
    }
}
