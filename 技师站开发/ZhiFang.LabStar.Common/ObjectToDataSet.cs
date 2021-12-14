using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ZhiFang.LabStar.Common
{
    public class ObjectToDataSet
    {
        /// <summary>
        /// 对象数组转化为DataSet
        /// </summary>
        /// <param name="objArr">对象数组</param>
        /// <param name="propertyList">对象属性列表</param>
        /// <returns>DataSet</returns>
        public static DataSet ObjectArrayConvertDataSet<T>(IList<T> objList, string propertyList, string DataTableName)
        {
            if ((objList == null || objList.Count == 0) || string.IsNullOrEmpty(propertyList))
                return null;
            DataSet tempDataSet = CreateDataSet(objList[0], propertyList, DataTableName);
            tempDataSet = FillDataSet(tempDataSet, objList);
            return tempDataSet;
        }
        /// <summary>
        /// 根据类型创建数据集
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns>DataSet</returns>
        public static DataSet CreateDataSet(object obj, string propertyList, string DataTableName)
        {
            Type objType = obj.GetType(); ;
            DataSet tempDataSet = new DataSet();
            DataTable tempDataTable = new DataTable(DataTableName);
            tempDataSet.Tables.Add(tempDataTable);
            string[] tempPropertyArray = propertyList.Split(',');
            foreach (string tempPropertyName in tempPropertyArray)
            {
                int tempInt = 0;
                Type tempType = null;
                string[] tempArray = tempPropertyName.Split('_');
                for (int i = 1; i < tempArray.Length; i++)
                {
                    tempInt++;
                    if (tempInt == 1)
                        tempType = objType;
                    PropertyInfo tempPropertyInfo = tempType.GetProperty(tempArray[i].Trim());
                    if (tempPropertyInfo != null)
                    {
                        if (tempInt == tempArray.Length - 1)
                        {
                            //DataColumn tempDataColumn = new DataColumn(tempPropertyInfo.Name, tempPropertyInfo.PropertyType);
                            DataColumn tempDataColumn;
                            if (tempPropertyInfo.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, typeof(DateTime));
                            }
                            else if (tempPropertyInfo.PropertyType.ToString() == "System.Nullable`1[System.Int32]")
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, typeof(Int32));
                            }
                            else if (tempPropertyInfo.PropertyType.ToString() == "System.Nullable`1[System.Int64]")
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, typeof(Int64));
                            }
                            else
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, tempPropertyInfo.PropertyType);
                            }
                            tempDataTable.Columns.Add(tempDataColumn);
                        }
                        else if (tempInt < tempArray.Length - 1)
                        {
                            tempType = tempPropertyInfo.PropertyType;
                        }
                    }
                }
            }
            return tempDataSet;
        }

        /// <summary>
        /// 根据类型创建数据集
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns>DataSet</returns>
        public static DataTable CreateDataTable(object obj, string propertyList, string DataTableName)
        {
            Type objType = obj.GetType(); ;
            DataTable tempDataTable = new DataTable(DataTableName);
            string[] tempPropertyArray = propertyList.Split(',');
            foreach (string tempPropertyName in tempPropertyArray)
            {
                int tempInt = 0;
                Type tempType = null;
                string[] tempArray = tempPropertyName.Split('_');
                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempInt++;
                    if (tempInt == 1)
                        tempType = objType;
                    PropertyInfo tempPropertyInfo = tempType.GetProperty(tempArray[i].Trim());
                    if (tempPropertyInfo != null)
                    {
                        if (tempInt == tempArray.Length)
                        {
                            //DataColumn tempDataColumn = new DataColumn(tempPropertyInfo.Name, tempPropertyInfo.PropertyType);
                            DataColumn tempDataColumn;
                            if (tempPropertyInfo.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, typeof(DateTime));
                            }
                            else if (tempPropertyInfo.PropertyType.ToString() == "System.Nullable`1[System.Int32]")
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, typeof(Int32));
                            }
                            else if (tempPropertyInfo.PropertyType.ToString() == "System.Nullable`1[System.Int64]")
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, typeof(Int64));
                            }
                            else
                            {
                                tempDataColumn = new DataColumn(tempPropertyName, tempPropertyInfo.PropertyType);
                            }
                            tempDataTable.Columns.Add(tempDataColumn);
                        }
                        else if (tempInt < tempArray.Length)
                        {
                            tempType = tempPropertyInfo.PropertyType;
                        }
                    }
                }
            }
            return tempDataTable;
        }
        /// <summary>
        /// 根据对象数组填充数据集
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="objArr"></param>
        /// <returns></returns>
        public static DataSet FillDataSet<T>(DataSet dataSet, IList<T> objList)
        {
            DataColumnCollection dcs = dataSet.Tables[0].Columns;
            foreach (object obj in objList)
            {
                DataRow dr = dataSet.Tables[0].NewRow();
                for (int i = 0; i < dcs.Count; i++)
                {
                    int tempInt = 0;
                    object tempObject = obj;
                    string[] tempArray = dcs[i].ColumnName.Split('_');
                    for (int k = 1; k < tempArray.Length; k++)
                    {
                        tempInt++;
                        PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(tempArray[k].Trim());
                        if (tempPropertyInfo != null)
                        {
                            tempObject = tempPropertyInfo.GetValue(tempObject, null);
                            if (tempObject != null)
                            {
                                if (tempInt == tempArray.Length - 1)
                                    dr[i] = tempObject;
                            }
                            else
                            {
                                dr[i] = DBNull.Value;
                                break;
                            }
                        }
                    }
                }
                dataSet.Tables[0].Rows.Add(dr);
            }
            return dataSet;
        }

        /// <summary>
        /// 根据对象数组填充数据集
        /// </summary>
        /// <param name="DataTable"></param>
        /// <param name="objArr"></param>
        /// <returns></returns>
        public static DataTable FillDataTable<T>(DataTable DataTable, IList<T> objList)
        {
            DataColumnCollection dcs = DataTable.Columns;
            foreach (object obj in objList)
            {
                DataRow dr = DataTable.NewRow();
                for (int i = 0; i < dcs.Count; i++)
                {
                    int tempInt = 0;
                    object tempObject = obj;
                    string[] tempArray = dcs[i].ColumnName.Split('_');
                    for (int k = 0; k < tempArray.Length; k++)
                    {
                        tempInt++;
                        PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(tempArray[k].Trim());
                        if (tempPropertyInfo != null)
                        {
                            tempObject = tempPropertyInfo.GetValue(tempObject, null);
                            if (tempObject != null)
                            {
                                if (tempInt == tempArray.Length)
                                    dr[i] = tempObject;
                            }
                            else
                            {
                                dr[i] = DBNull.Value;
                                break;
                            }
                        }
                    }
                }
                DataTable.Rows.Add(dr);
            }
            return DataTable;
        }

        /// <summary>
        /// 根据对象数组填充数据集
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="objArr"></param>
        /// <returns></returns>
        public static DataSet FillDataSet<T>(DataSet dataSet, IList<T> objList, Dictionary<string, string> dicFormat)
        {
            DataColumnCollection dcs = dataSet.Tables[0].Columns;
            foreach (object obj in objList)
            {
                DataRow dr = dataSet.Tables[0].NewRow();
                for (int i = 0; i < dcs.Count; i++)
                {
                    int tempInt = 0;
                    object tempObject = obj;
                    string[] tempArray = dcs[i].ColumnName.Split('_');
                    for (int k = 1; k < tempArray.Length; k++)
                    {
                        tempInt++;
                        PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(tempArray[k].Trim());
                        if (tempPropertyInfo != null)
                        {
                            tempObject = tempPropertyInfo.GetValue(tempObject, null);
                            if (tempObject != null)
                            {
                                if (tempInt == tempArray.Length - 1)
                                {
                                    string format = "";
                                    if (dicFormat.ContainsKey(dcs[i].ColumnName))
                                        format = dicFormat[dcs[i].ColumnName];
                                    if ((!string.IsNullOrEmpty(format)) && (tempObject.GetType() == typeof(DateTime) || tempObject.GetType() == typeof(DateTime?)))
                                    {
                                        dr[i] = ((DateTime)tempObject).ToString(format);
                                    }
                                    else
                                        dr[i] = tempObject;
                                }
                            }
                            else
                            {
                                dr[i] = DBNull.Value;
                                break;
                            }
                        }
                    }
                }
                dataSet.Tables[0].Rows.Add(dr);
            }
            return dataSet;
        }


        /// <summary>
        /// List实体类 转换为datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable EntityConvetDataTable<T>(IList<T> entity) where T : class
        {
            DataTable table = new DataTable();
            if (entity.Count <= 0)
            {
                return table;
            }
            System.Reflection.PropertyInfo[] info = entity[0].GetType().GetProperties();
            for (int i = 0; i < info.Length; i++)
            {
                Type colType = info[i].PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                table.Columns.Add(info[i].Name, colType);
            }
            foreach (T item in entity)
            {
                DataRow dataRow = table.NewRow();
                for (int i = 0; i < info.Length; i++)
                {
                    if (info[i].GetValue(item, null) == null)
                    {
                        dataRow[info[i].Name] = DBNull.Value;
                    }
                    else
                    {
                        dataRow[info[i].Name] = info[i].GetValue(item, null);
                    }
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }
    }
}
