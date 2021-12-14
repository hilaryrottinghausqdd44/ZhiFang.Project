using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Tools
{
    public class DataTableHelper
    {
        public static DataTable ColumnNameToLower(DataTable dt)
        {
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToLower();
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable ColumnNameToUpper(DataTable dt)
        {
            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                }
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable SetTableColumn(DataTable dt, string columnstr)
        {
            string[] c = columnstr.Split(',');
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (!c.Contains(dt.Columns[i].ColumnName))
                {
                    dt.Columns.RemoveAt(i);
                }
            }
            return dt;
        }

        public static DataTable CopyToDataTablea<T>(IEnumerable<T> array)
        {
            var ret = new DataTable();
            foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                ret.Columns.Add(dp.Name, dp.PropertyType);
            foreach (T item in array)
            {
                var Row = ret.NewRow();
                foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                    Row[dp.Name] = dp.GetValue(item);
                ret.Rows.Add(Row);
            }
            return ret;
        }
    }
}

