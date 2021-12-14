using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.Base
{
    public class EditGetUpdateMemoHelp
    {
        public static void EditGetUpdateMemo<T>(T serverEntity, T updateEntity, Type type, string[] arrFields, ref StringBuilder strbMemo)
        {
            //为空判断
            if (serverEntity == null && updateEntity == null)
                return;
            else if (serverEntity == null || updateEntity == null)
                return;

            Type t = type;
            System.Reflection.PropertyInfo[] props = t.GetProperties();
            foreach (var po in props)
            {
                if (po.Name == "Id" || po.Name == "LabID" || po.Name == "DataAddTime" || po.Name == "DataUpdateTime" || po.Name == "DataTimeStamp")
                    continue;

                if (arrFields.Contains(po.Name) == true && IsCanCompare(po.PropertyType))
                {
                    object serverValue = po.GetValue(serverEntity, null);
                    object updateValue = po.GetValue(updateEntity, null);
                    if (serverValue == null)
                        serverValue = "";
                    if (updateValue == null)
                        updateValue = "";
                    if (!serverValue.ToString().Equals(updateValue.ToString()))
                    {
                        string cname = po.Name;
                        foreach (var pattribute in po.GetCustomAttributes(false))
                        {
                            if (pattribute.ToString() == "ZhiFang.Entity.Base.DataDescAttribute")
                            {
                                cname = ((DataDescAttribute)pattribute).CName;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(serverValue.ToString()))
                            serverValue = "空";
                        if (string.IsNullOrEmpty(updateValue.ToString()))
                            updateValue = "空";
                        strbMemo.Append("【" + cname + "】由原来:" + serverValue.ToString() + ",修改为:" + updateValue.ToString() + ";" + System.Environment.NewLine);
                    }
                }
            }
        }
        /// <summary>
        /// 该类型是否可直接进行值的比较
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsCanCompare(Type t)
        {
            if (t.IsValueType)
            {
                return true;
            }
            else
            {
                //String是特殊的引用类型，它可以直接进行值的比较
                if (t.FullName == typeof(String).FullName)
                {
                    return true;
                }
                return false;
            }
        }

    }
}
