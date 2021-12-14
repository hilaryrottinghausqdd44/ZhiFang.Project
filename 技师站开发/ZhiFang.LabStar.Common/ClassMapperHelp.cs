using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZhiFang.Common.Public;

namespace ZhiFang.LabStar.Common
{
    /// <summary>
    /// 将实体对象进行复制拷贝
    /// </summary>
    public class ClassMapperHelp
    {
        /// <summary>
        /// 通过反射将对象进行复制拷贝
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static D GetMapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>();
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name)//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }

        /// <summary>
        /// 通过反射将对象进行复制拷贝
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static D GetEntityMapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>();
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    if (sp.Name == "Id" || sp.Name == "DataAddTime" || sp.Name == "DataTimeStamp" || sp.Name == "DataUpdateTime")
                        continue;
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name)//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }

        public static T EntityCopy<T>(T entity, string[] noCopyProperty)
        {
            T newEntity = Activator.CreateInstance<T>();
            try
            {
                foreach (System.Reflection.PropertyInfo pi in entity.GetType().GetProperties())
                {
                    if (noCopyProperty == null || (!noCopyProperty.Contains(pi.Name)))
                    {
                        System.Reflection.PropertyInfo newpi = newEntity.GetType().GetProperty(pi.Name);
                        newpi.SetValue(newEntity, pi.GetValue(entity, null), null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newEntity;
        }

        /// <summary>
        /// 复制实体的指定属性
        /// </summary>
        /// <typeparam name="T">实体类名</typeparam>
        /// <param name="formEntity">源实体</param>
        /// <param name="toEntity">目标实体</param>
        /// <param name="strProperty">属性列表字符串，英文逗号分隔</param>
        /// <returns></returns>
        public static T EntityCopyByProperty<T>(T formEntity, T toEntity, string strProperty)
        {
            if (string.IsNullOrWhiteSpace(strProperty))
                return toEntity;
            string[] arrayProperty = strProperty.Split(',');
            return EntityCopyByProperty<T>(formEntity, toEntity, arrayProperty);
        }

        /// <summary>
        /// 复制实体的指定属性
        /// </summary>
        /// <typeparam name="T">实体类名</typeparam>
        /// <param name="formEntity">源实体</param>
        /// <param name="toEntity">目标实体</param>
        /// <param name="arrayProperty">属性数组</param>
        /// <returns></returns>
        public static T EntityCopyByProperty<T>(T formEntity, T toEntity, string[] arrayProperty)
        {
            if (toEntity == null)
                toEntity = Activator.CreateInstance<T>();
            if (arrayProperty == null || arrayProperty.Length == 0)
                return toEntity;
            try
            {
                if (formEntity != null)
                {
                    foreach (System.Reflection.PropertyInfo pi in formEntity.GetType().GetProperties())
                    {
                        if (arrayProperty == null || (arrayProperty.Contains(pi.Name)))
                        {
                            System.Reflection.PropertyInfo newpi = toEntity.GetType().GetProperty(pi.Name);
                            newpi.SetValue(toEntity, pi.GetValue(formEntity, null), null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return toEntity;
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
            //string tempEnumType = "QCValueTypeEnum,QCValueIsControlEnum,ItemAllItemValueTypeEnum,WorkLogExportLevel";
            string[] fieldArray = fields.Split(',');
            IList<string> fieldValueList = new List<string>();
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
                                fieldValueList.Add(FieldName + "=" + "'" + StringPlus.SQLConvertSpecialCharacter(Convert.ToDateTime(tempFieldValue).ToString("yyyy-MM-dd HH:mm:ss.fff")) + "'");
                            }
                            else
                            {
                                fieldValueList.Add(FieldName + "=" + "'" + StringPlus.SQLConvertSpecialCharacter(tempFieldValue.ToString()) + "'");
                            }
                        }
                        //else if (tempEnumType.Contains(tempType)) //如果为枚举类型，则转换为Int类型存储
                        //{
                        //    fieldValueArray[tempIndex] = FieldName + "=" + ((int)tempFieldValue).ToString();
                        //}
                        else if (tempType.Trim().ToLower() == "boolean")
                        {
                            fieldValueList.Add(FieldName + "=" + (((bool)tempFieldValue) ? "1" : "0"));
                        }
                        else
                        {
                            fieldValueList.Add(FieldName + "=" + tempFieldValue.ToString());
                        }
                    }
                    else
                    {
                        fieldValueList.Add(FieldName + "=null");
                    }
                }
                else
                {
                    if (tempPropertyInfo != null)
                        fieldValueList.Add(FieldName + "=null");
                }
            }
            return fieldValueList.ToArray();
        }
        /// <summary>
        /// 将一个实体类复制到另一个实体类
        /// </summary>
        /// <param name="objectsrc">源实体类</param>
        /// <param name="objectdest">复制到的实体类</param>
        public static void EntityToEntity(object objectsrc, object objectdest)
        {
            var sourceType = objectsrc.GetType();
            var destType = objectdest.GetType();
            foreach (var source in sourceType.GetProperties())
            {
                foreach (var dest in destType.GetProperties())
                {
                    if (dest.Name == source.Name)
                    {
                        dest.SetValue(objectdest, source.GetValue(objectsrc));
                    }
                }
            }
        }
    }
}
