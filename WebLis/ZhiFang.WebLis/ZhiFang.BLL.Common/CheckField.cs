using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.BLL.Common
{
    public class CheckField
    {
        // <summary>
        /// 判断DataTale中判断某个字段中包含某个数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <param name="fieldData"></param>
        /// <returns></returns>
        public static Boolean IsColumnIncludeData(DataTable dt, String columnName, string fieldData)
        {
            if (dt == null)
            {
                return false;
            }
            else
            {
                DataRow[] dataRows = dt.Select(columnName + "='" + fieldData + "'");
                if (dataRows.Length.Equals(1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        /// <summary>
        /// 判断DataTale中是否包含字段
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="Field">检测字段(字符串形式,不区分大小写,用逗号分隔(批量))</param>
        /// <param name="messages">返回信息</param>
        /// <returns>不存在或失败</returns>
        public static Boolean IsColumnField(DataTable dt, string Field, out string messages)
        {
            bool Flag = true;
            messages = "";
            if (dt == null)
            {
                messages = "数据不存在！";
                return false;
            }
            else
            {
                string[] A_Field = Field.Trim().ToUpper().Split(',');
                if (Field.Trim()==""||A_Field.Length <= 0)
                {
                    messages = "没有需要检测的列！";
                    return true;
                }
                else
                {
                    for (int i = 0; i < A_Field.Length; i++)
                    {
                        if (!dt.Columns.Contains(A_Field[i].Trim()))
                        {
                            messages += A_Field[i].Trim() + ",";
                            Flag = false;
                        }
                    }
                    messages = messages.TrimEnd(',');
                    if (Flag)
                        return true;
                    else
                    {
                        messages = "检测到缺失的字段如下:" + messages;
                        return false;
                    }
                }
            }

        }

        
    }
}
