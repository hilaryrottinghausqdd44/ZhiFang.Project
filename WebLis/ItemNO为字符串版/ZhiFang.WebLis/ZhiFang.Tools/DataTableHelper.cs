using System;
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
    }
}

