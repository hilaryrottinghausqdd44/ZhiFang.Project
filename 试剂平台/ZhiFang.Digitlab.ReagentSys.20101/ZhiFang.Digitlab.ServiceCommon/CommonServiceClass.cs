using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Globalization;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.ServiceCommon
{
    /// <summary>
    /// 此类定义服务层所需要的公共属性
    /// </summary>
    public class CommonServiceClass
    {

    }

    #region CommonServiceMethod Class 此类定义服务层所需要的公共方法
    /// <summary>
    /// 此类定义服务层所需要的公共方法
    /// </summary>
    public class CommonServiceMethod
    {
        /// <summary>
        ///  解析排序Json字符串，获取排序字符串
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static string GetSortHQL(string strJson)
        {
            string returnStr = "";
            if (!string.IsNullOrEmpty(strJson))
            {
                JArray tempJArray = JArray.Parse(strJson);
                List<string> SortList = new List<string>();
                string SortStr = "";
                string FirstStr = "";
                foreach (var tempObject in tempJArray)
                {
                    SortStr = tempObject["property"].ToString().Replace("_", ".") + " " + tempObject["direction"].ToString().ToUpper();
                    int tempIndex = SortStr.IndexOf(".");
                    FirstStr = SortStr.Substring(0, tempIndex);
                    StringBuilder tempStringBuilder = new StringBuilder(SortStr);
                    tempStringBuilder.Replace(FirstStr, FirstStr.ToLower(), 0, tempIndex);
                    SortList.Add(tempStringBuilder.ToString());
                }
                if (SortList.Count > 0)
                {
                    returnStr = string.Join(",", SortList.ToArray());
                }
            }
            return returnStr;
        }
        /// <summary>
        /// 获取对象指定属性和值字符串。['属性名=属性值','属性名1=属性值1']
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fields">属性列表字符串</param>
        /// <returns>返回属性和值的字符串数组</returns>
        public static string[] GetUpdateFieldValueStr(object entity, string fields)
        {
            int tempIndex = -1;
            PropertyInfo tempPropertyInfo = null;
            string tempKeyStr = "String,DateTime";
            string tempEnumType = "QCValueTypeEnum,QCValueIsControlEnum,ItemAllItemValueTypeEnum";
            string[] fieldArray = fields.Split(',');
            string[] fieldValueArray = new string[fieldArray.Length];
            string FieldName = "";
            object tempEntity = null;
            foreach (string tempFieldName in fieldArray)
            {
                tempEntity = entity;
                tempIndex++;
                FieldName = tempFieldName;
                if (tempFieldName.IndexOf("_") > 0)
                {
                    List<string> tempList = tempFieldName.Split('_').ToList();
                    string childFieldName = tempList[tempList.Count - 1];
                    for (int i = 0; i < tempList.Count - 1; i++)
                    {
                        tempPropertyInfo = tempEntity.GetType().GetProperty(tempList[i].Trim());
                        if (tempPropertyInfo != null)
                        {
                            tempEntity = tempPropertyInfo.GetValue(tempEntity, null);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (tempEntity != null)
                    {
                        tempPropertyInfo = tempEntity.GetType().GetProperty(childFieldName.Trim());
                    }
                    FieldName = string.Join(".", tempList.ToArray());
                }
                else
                {
                    tempPropertyInfo = tempEntity.GetType().GetProperty(FieldName.Trim());
                }
                if (tempEntity != null && tempPropertyInfo != null)
                {
                    object tempFieldValue = tempPropertyInfo.GetValue(tempEntity, null);
                    if (tempFieldValue != null)
                    {

                        string tempType = tempFieldValue.GetType().Name;
                        if (tempKeyStr.Contains(tempType))       //如果为字符串和时间类型，需要加引号
                        {
                            if (tempType.Trim().ToLower() == "datetime")
                            {
                                fieldValueArray[tempIndex] = FieldName + "=" + "'" + StringPlus.SQLConvertSpecialCharacter(Convert.ToDateTime(tempFieldValue).ToString("yyyy-MM-dd HH:mm:ss.fff")) + "'";
                            }
                            else
                            {
                                fieldValueArray[tempIndex] = FieldName + "=" + "'" + StringPlus.SQLConvertSpecialCharacter(tempFieldValue.ToString()) + "'";
                            }
                        }
                        else if (tempEnumType.Contains(tempType)) //如果为枚举类型，则转换为Int类型存储
                        {
                            fieldValueArray[tempIndex] = FieldName + "=" + ((int)tempFieldValue).ToString();
                        }
                        else if (tempType.Trim().ToLower() == "boolean")
                        {
                            fieldValueArray[tempIndex] = FieldName + "=" + (((bool)tempFieldValue) ? "1" : "0");
                        }
                        else
                        {
                            fieldValueArray[tempIndex] = FieldName + "=" + tempFieldValue.ToString();
                        }
                    }
                    else
                    {
                        fieldValueArray[tempIndex] = FieldName + "=null";
                    }
                }
                else
                {
                    fieldValueArray[tempIndex] = FieldName + "=null";
                }
            }
            return fieldValueArray;
        }
        /// <summary>
        /// 获取ADD方法需要返回的信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetAddMethodResultStr(BaseEntity entity)
        {
            return "{id:" + "\"" + entity.Id.ToString() + "\"" + "}";
        }

        public static string GetAddMethodResultStr(string resultStr)
        {
            return "{" + resultStr + "}";
        }
    }
    #endregion

    #region 序列化类 ParseObjectProperty Class
    /// <summary>
    /// 序列化类。通过反射和Json.net两种方式把类序列为Json字符串；分为压平和不压平两种格式。
    /// </summary>
    public class ParseObjectProperty: ZhiFang.Common.Public.JsonSerializerClass
    {
        public ParseObjectProperty(): base()
        {

        }
        public ParseObjectProperty(string parastr): base(parastr)
        {
           
        }
        #region 对象列表压平EntityList<T> GetObjectListPlanish<T>
        public string GetObjectListPlanish<T>(EntityList<T> ObjectList)
        {
            string ResultStr = "";
            if (ObjectList != null && ObjectList.count > 0)                                         
            {
                StringBuilder tempStringBuilder = new StringBuilder();
                //存储对象列表中每个自定义属性对象序列化后的JSON串
                foreach (T tempObject in ObjectList.list)
                {
                    string tempResultStr = "";
                    GetObjectPropertyPlanishEx(tempObject, tempObject.GetType().Name, ref tempResultStr);
                    if (tempResultStr.Length > 0)
                    {
                        //组合每个自定义属性对象序列化后的JSON串
                        tempStringBuilder.Append("," + "{" + tempResultStr.Remove(0, 1) + "}");
                    }
                }
                if (tempStringBuilder.Length > 0)
                {
                    ResultStr = "{count:" + ObjectList.count.ToString() + ",list:[" + tempStringBuilder.ToString().Remove(0, 1) + "]" + "}";
                }
            }
            return ResultStr;
        }
        #endregion
    }
    #endregion
}
