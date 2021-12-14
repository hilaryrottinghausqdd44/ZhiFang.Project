using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    public class JsonSerializer
    {
        public static string JsonDateTimeFormat = "yyyy/MM/dd HH:mm:ss";
        public static string JsonDotNetSerializer(object o)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(o, Formatting.Indented, settings);
            return result;
        }
        public static string JsonDotNetSerializerNoEnterSpace(object o)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string result = JsonConvert.SerializeObject(o, Formatting.None, settings);
            return result;
        }
        public static string ByteTypeToString(byte[] ByteField)
        {
            string tempByteStr = "";
            if (ByteField != null)
            {
                foreach (int tempByte in ByteField)
                {
                    tempByteStr = tempByteStr + "," + tempByte.ToString();
                }
                if (tempByteStr.Length > 0)
                {
                    tempByteStr = tempByteStr.Remove(0, 1);
                }
            }
            else
            {
                tempByteStr = "null";
            }
            return tempByteStr;
        }
    }
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] props = null;
        Type type = null;

        public LimitPropsContractResolver(string[] props, Type t)
        {
            this.props = props;
            this.type = t;
        }
        protected override IList<JsonProperty> CreateProperties(Type type,MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            return list.Where(p => props.Contains(p.PropertyName)).ToList();
        }
    }
    public class JsonConvertClass : JsonConverter
    {
        /// <summary>
        /// 是否允许转换
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            bool canConvert = false;
            switch (objectType.FullName)
            {
                case "System.Int64":
                    canConvert = true;
                    break;
                case "System.Single":
                    canConvert = true;
                    break;
                case "System.Double":
                    canConvert = true;
                    break;
                case "System.Decimal":
                    canConvert = true;
                    break;
                case "System.Byte[]":
                    canConvert = true;
                    break;
                case "System.DateTime":
                    canConvert = true;
                    break;
            }
            return canConvert;
        }

        

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 是否允许转换JSON字符串时调用
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value.GetType().FullName == "System.Int64" || value.GetType().FullName == "System.Single" || value.GetType().FullName == "System.Double" || value.GetType().FullName == "System.Decimal")
            {
                writer.WriteValue(value.ToString());
            }
            if (value.GetType().FullName == "System.Byte[]")
            {
                writer.WriteValue(ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.ByteTypeToString((byte[])value));
            }
            if (value.GetType().FullName == "System.DateTime")
            {

                //writer.WriteValue(((System.DateTime)value).Ticks.ToString());
                writer.WriteValue(((System.DateTime)value).ToString(ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDateTimeFormat, DateTimeFormatInfo.InvariantInfo));
            }
        }
    }
    #region 序列化类 JsonSerializer Class
    /// <summary>
    /// 序列化类。通过反射和Json.net两种方式把类序列为Json字符串；分为压平和不压平两种格式。
    /// </summary>
    public class JsonSerializerClass
    {
        public const string ValueSeparator = ":";//属性和属性值分隔符
        public const string PropertySeparator = ",";//各属性分隔符
        public const string TierSeparator = "_"; //对象层次结构分隔符
        public const string CommonSeparator = "\""; //转义字符
        private List<string> paraList;
        private string paraStr;
        private bool jsonFieldIsAddSeparator = true;
        #region 属性ParaStr
        /// <summary>
        ///需要返回信息的参数列表 
        /// </summary>
        public string ParaStr
        {
            get
            {
                return paraStr;
            }
            set
            {
                paraStr = value;
                paraList.Clear();
                if ((paraStr != null) && (paraStr.Length > 0))
                {
                    string[] tempArray = paraStr.Split(',');
                    foreach (string tempStr in tempArray)
                    {
                        paraList.Add(tempStr);
                    }
                }
            }
        }
        #endregion
        //返回的json字符串是否压平
        public bool IsPlanish { get; set; }
        //需要返回子对象的层次级别
        public int Layer { get; set; }
        //Json中字段名(属性名)是否加CommonSeparator
        public bool JsonFieldIsAddSeparator
        {
            get { return jsonFieldIsAddSeparator; }
            set { jsonFieldIsAddSeparator = value; }
        }

        public List<string> ParaList
        {
            get
            {
                return paraList;
            }
            set
            {
                paraList = value;
            }
        }

        public JsonSerializerClass()
        {
            paraList = new List<string>();
        }

         public JsonSerializerClass(string parastr)
        {
            paraList = new List<string>();
            ParaStr = parastr;
        }

        private bool GetPropertyListByFieldsIsNull(object tempObject)
        {
            Type tempObjectType = tempObject.GetType();
            foreach (PropertyInfo tempProperty in tempObjectType.GetProperties())
            {
                string tempType = tempProperty.PropertyType.Name;
                //属性为列表类型不解析    
                if (tempType.IndexOf("IList") < 0)
                {
                    if (tempProperty.PropertyType.FullName.IndexOf("ZhiFang.Digitlab") < 0)
                    {
                        paraList.Add(tempObjectType.Name + TierSeparator + tempProperty.Name);
                    }
                }
            }
            return true;
        }

        #region CombinationJsonStr函数 将属性名称和属性值序列化为Json字符串
        //propertyObject参数 属性对象
        //classTypeName参数  属性所属类名或根类名_属性名...
        //propertyName参数   属性名
        private string CombinationJsonStr(object propertyObject, string classTypeName, PropertyInfo tempProperty)
        {
            string ResultStr = "";
            string propertyName = tempProperty.Name;
            string tempType = tempProperty.PropertyType.Name;
            string propertySeparator = CommonSeparator;
            if (!jsonFieldIsAddSeparator)
                propertySeparator = "";
            //防止对象为null时，无法转为字符串（无法ToString()）。
            if (propertyObject == null)
            {
                ResultStr = propertySeparator + classTypeName +
                            TierSeparator + propertyName + propertySeparator + ValueSeparator + CommonSeparator +
                            "" + CommonSeparator;

            }
            else
            {
                string tempPropertyValue = "";
                if (tempType == "Byte[]")
                {
                    tempPropertyValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.ByteTypeToString((byte[])propertyObject);
                }
                else if ((tempType == "Nullable`1") && ((tempProperty.PropertyType.FullName.IndexOf("DateTime") > 0)))
                {
                    //tempPropertyValue = DateTime.Parse(propertyObject.ToString()).Ticks.ToString();
                    tempPropertyValue = DateTime.Parse(propertyObject.ToString()).ToString(ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDateTimeFormat, DateTimeFormatInfo.InvariantInfo);
                }
                else
                {
                    tempPropertyValue = propertyObject.ToString();
                }
                ResultStr = propertySeparator + classTypeName +
                            TierSeparator + propertyName + propertySeparator + ValueSeparator + CommonSeparator +
                            tempPropertyValue + CommonSeparator;
            }
            return ResultStr;
        }
        #endregion

        #region CombinationJsonStrEx函数 将属性名称和属性值序列化为Json字符串
        //propertyObject参数 属性对象
        //classTypeName参数  属性所属类名或根类名_属性名...
        //propertyName参数   属性名
        private string CombinationJsonStrEx(object propertyObject, string classTypeName, PropertyInfo tempProperty)
        {
            string ResultStr = "";
            string tempType = tempProperty.PropertyType.Name;
            string propertySeparator = CommonSeparator;
            if (!jsonFieldIsAddSeparator)
                propertySeparator = "";
            //防止对象为null时，无法转为字符串（无法ToString()）。
            if (propertyObject == null)
            {
                ResultStr = propertySeparator + classTypeName +
                            propertySeparator + ValueSeparator + CommonSeparator +
                            "" + CommonSeparator;

            }
            else
            {
                string tempPropertyValue = "";
                if (tempType == "Byte[]")
                {
                    tempPropertyValue = ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.ByteTypeToString((byte[])propertyObject);
                }
                else if ((tempType == "Nullable`1" && tempProperty.PropertyType.FullName.IndexOf("DateTime") > 0) ||
                         (tempType.ToUpper() == "DATETIME" && tempProperty.PropertyType.FullName.IndexOf("DateTime") > 0))
                {
                    //tempPropertyValue = DateTime.Parse(propertyObject.ToString()).Ticks.ToString();
                    tempPropertyValue = DateTime.Parse(propertyObject.ToString()).ToString(ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDateTimeFormat, DateTimeFormatInfo.InvariantInfo);
                }
                else if (tempType == "Boolean")
                {
                    tempPropertyValue = propertyObject.ToString().ToLower();
                }
                else
                {
                    tempPropertyValue = propertyObject.ToString();
                }
                ResultStr = propertySeparator + classTypeName +
                            propertySeparator + ValueSeparator + CommonSeparator +
                            tempPropertyValue + CommonSeparator;
            }
            return ResultStr;
        }
        #endregion

        #region 对象列表压平IList<T> GetObjectListPlanish<T>
        public string GetObjectListPlanish<T>(IList<T> ObjectList)
        {
            string ResultStr = "";
            if (ObjectList != null)
            {
                StringBuilder tempStringBuilder = new StringBuilder();
                //存储对象列表中每个自定义属性对象序列化后的JSON串
                foreach (T tempObject in ObjectList)
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
                    ResultStr = "{count:" + ObjectList.Count.ToString() + ",list:[" + tempStringBuilder.ToString().Remove(0, 1) + "]" + "}";
                }
            }
            return ResultStr;
        }
        #endregion

        #region 单一对象压平(服务层使用) GetObjectListPlanish<T>
        public string GetSingleObjectPlanish<T>(T SingleObject)
        {
            //自定义属性对象序列化后的JSON串
            string tempResultStr = "";
            if (SingleObject != null)
            {
                GetObjectPropertyPlanishEx(SingleObject, SingleObject.GetType().Name, ref tempResultStr);
                if (tempResultStr.Length > 0)
                {
                    //组合每个自定义属性对象序列化后的JSON串
                    tempResultStr = "{" + tempResultStr.Remove(0, 1) + "}";
                }
            }
            return tempResultStr;
        }
        #endregion

        #region 单一对象压平 GetObjectPropertyPlanishEx

        protected void GetObjectPropertyPlanishEx(Object WillPlanishObject, string TypeName, ref string ResultStr)
        {
            if (ParaList.Count <= 0)
            {
                GetPropertyListByFieldsIsNull(WillPlanishObject);
                //GetObjectPropertyPlanish(WillPlanishObject, TypeName, ref ResultStr);
            }
            StringBuilder tempStringBuilder = new StringBuilder();
            foreach (string tempPropertyNameStr in ParaList)
            {
                object tempObject = WillPlanishObject;
                int tempInt = 0;
                string[] tempPropertyArray = tempPropertyNameStr.Split('_');
                for (int i = 1; i < tempPropertyArray.Length; i++)
                {
                    tempInt++;
                    PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(tempPropertyArray[i].Trim());
                    if (tempPropertyInfo != null)
                    {
                        tempObject = tempPropertyInfo.GetValue(tempObject, null);
                        if (tempObject != null)
                        {
                            if (tempInt == tempPropertyArray.Length - 1)
                            {
                                tempStringBuilder.Append(PropertySeparator + CombinationJsonStrEx(tempObject, tempPropertyNameStr, tempPropertyInfo));
                            }
                        }
                        else
                        {
                            tempStringBuilder.Append(PropertySeparator + CombinationJsonStrEx(tempObject, tempPropertyNameStr, tempPropertyInfo));
                            break;
                        }
                    }
                }
            }
            if (tempStringBuilder.Length > 0)
            {
                ResultStr = tempStringBuilder.ToString();
            }
        }

        #endregion

        //#region 单一对象不压平 GetObjectPropertyNoPlanish
        //public string GetObjectPropertyNoPlanish(Object WillPlanishObject)
        //{
        //    //把对象序列化为Json字符串
        //    if (WillPlanishObject != null)
        //    {
        //        JsonSerializerSettings settings = new JsonSerializerSettings();
        //        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //        return JsonConvert.SerializeObject(WillPlanishObject, Formatting.Indented, settings);
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        //#endregion

        #region 单一对象不压平 GetObjectPropertyNoPlanish
        public string GetObjectPropertyNoPlanish<T>(T WillPlanishObject, string fields)
        {
            if (WillPlanishObject != null)
            {
                if (string.IsNullOrEmpty(fields))
                    return "";
                ExcludePropertiesContractResolver<T> tempObject = new ExcludePropertiesContractResolver<T>(WillPlanishObject, fields);
                JsonSerializerSettings Settings = new JsonSerializerSettings();
                Settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                Settings.ContractResolver = tempObject;
                return JsonConvert.SerializeObject(WillPlanishObject, Formatting.Indented, Settings);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 单一对象不压平 GetObjectPropertyNoPlanish
        public string GetObjectPropertyNoPlanish<T>(T WillPlanishObject)
        {
            if (WillPlanishObject != null)
            {
                JsonSerializerSettings Settings = new JsonSerializerSettings();
                if (string.IsNullOrEmpty(paraStr))
                {
                    //return "";
                    Settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                }
                else
                {
                    ExcludePropertiesContractResolver<T> tempObject = new ExcludePropertiesContractResolver<T>(WillPlanishObject, paraStr);
                    Settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    Settings.ContractResolver = tempObject;
                }
                return JsonConvert.SerializeObject(WillPlanishObject, Formatting.Indented, Settings);
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
    #endregion

    #region ExcludePropertiesContractResolver Class 此类可以使Json.net实现按指定的字段序列化
    /// <summary>
    /// 此类可以使Json.net实现按指定的字段序列化.
    /// </summary>
    public class ExcludePropertiesContractResolver<T> : DefaultContractResolver
    {
        private Dictionary<string, IEnumerable<string>> PropertyDictionary { get; set; }
        private IEnumerable<string> lstExclude;
        private void DictionaryAddProperty(Dictionary<string, IEnumerable<string>> tempDictionary, string TypeName, string PropertyName)
        {
            if (tempDictionary.ContainsKey(TypeName))
            {
                List<string> tempList = tempDictionary[TypeName].ToList();
                if (tempList.IndexOf(PropertyName) < 0)
                {
                    tempList.Add(PropertyName);
                }
                tempDictionary[TypeName] = tempList;
            }
            else
            {
                tempDictionary.Add(TypeName, new List<string> { PropertyName });
            }
        }
        private void ParseArray(object tempObject, Dictionary<string, IEnumerable<string>> tempDictionary, List<string> tempList, Dictionary<string, object> tempPropertyValue)
        {
            try
            {
                int tempInt = 0;
                for (int i = 1; i < tempList.Count; i++)
                {
                    tempInt++;
                    string tempType = tempList[i - 1];
                    //序列化的字段为第一层,例如:HRDept_CName,HRDept_Id
                    if (tempInt == tempList.Count - 1)
                    {
                        DictionaryAddProperty(tempDictionary, tempType.Trim(), tempList[tempList.Count - 1].Trim());
                    }
                    //序列化的字段为第二层,第三层.......
                    //例如:HRDept_HREmployeeList_Id,HRDept_HREmployeeList_CName,HRDept_HREmployeeList_RBACUserList_Id,HRDept_HREmployeeList_RBACUserList_CName
                    else
                    {
                        DictionaryAddProperty(tempDictionary, tempType.Trim(), tempList[i].Trim());
                        //如果实体是List数组，取数组的第一个实体对象
                        if ((tempObject.GetType().IsGenericType) && (tempObject.GetType().Name == "List`1" || tempObject.GetType().Name == "IList`1" || tempObject.GetType().Name == "PersistentGenericBag`1"))
                        {
                            System.Collections.IList Ilist = tempObject as System.Collections.IList;
                            if (Ilist != null && Ilist.Count > 0)
                                tempObject = Ilist[0];
                            else
                                break;
                        }
                        PropertyInfo tempPropertyInfo = tempObject.GetType().GetProperty(tempList[i].Trim());
                        if (tempPropertyInfo != null)
                        {
                            if ((tempPropertyInfo.PropertyType.Name == "List`1") || (tempPropertyInfo.PropertyType.Name == "IList`1"))
                            {
                                tempType = tempPropertyInfo.PropertyType.GetGenericArguments()[0].Name;
                                int tempIndex = tempType.IndexOf("`");
                                if (tempIndex >= 0)
                                {
                                    tempType = tempType.Remove(tempIndex);
                                }
                            }
                            //else if ((tempPropertyInfo.PropertyType.BaseType.Name == "Array"))
                            //{

                            //}
                            else
                            {
                                tempType = tempPropertyInfo.PropertyType.Name;
                            }
                            tempList[1] = tempType;
                            tempList.RemoveAt(0);
                            if (tempList.Count > 2)
                            {
                                if (!tempPropertyValue.ContainsKey(tempList[i].Trim()))
                                {
                                    tempPropertyValue[tempList[i].Trim()] = tempPropertyInfo.GetValue(tempObject, null);
                                }
                                ParseArray(tempPropertyValue[tempList[i].Trim()], tempDictionary, tempList, tempPropertyValue);
                            }
                            else
                            {
                                ParseArray(null, tempDictionary, tempList, null);
                            }
                        }
                        //如果根据属性得不到实体，则删除父节点直接组合子节点
                        if (tempList.Count > 0)
                        {
                            tempList.RemoveAt(0);
                            ParseArray(null, tempDictionary, tempList, null);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
            
            }
        }

        public ExcludePropertiesContractResolver(T EntityObject, string PropertyStrList)
        {
            Dictionary<string, IEnumerable<string>> tempDictionary = new Dictionary<string, IEnumerable<string>>();
            //存储反射出来的属性实体变量
            Dictionary<string, object> tempPropertyValue = new Dictionary<string, object>();
            if ((EntityObject != null) && (PropertyStrList != ""))
            {
                string[] tempPropertyArray = PropertyStrList.Split(',');
                foreach (string tempProperty in tempPropertyArray)
                {
                    List<string> tempList = tempProperty.Split('_').ToList();
                    ParseArray(EntityObject, tempDictionary, tempList, tempPropertyValue);
                }
            }
            PropertyDictionary = tempDictionary;
        }

        public ExcludePropertiesContractResolver(IList<T> EntityObjectList, string PropertyStrList)
        {
            Dictionary<string, IEnumerable<string>> tempDictionary = new Dictionary<string, IEnumerable<string>>();
            Dictionary<string, object> tempPropertyValue = new Dictionary<string, object>();
            if ((EntityObjectList.Count > 0) && (PropertyStrList != ""))
            {
                T tempobject = EntityObjectList[0];
                string[] tempPropertyArray = PropertyStrList.Split(',');
                foreach (string tempProperty in tempPropertyArray)
                {
                    List<string> tempList = tempProperty.Split('_').ToList();
                    ParseArray(tempobject, tempDictionary, tempList, tempPropertyValue);
                }
            }
            PropertyDictionary = tempDictionary;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            List<JsonProperty> tempList = base.CreateProperties(type, memberSerialization).ToList();
            if ((PropertyDictionary.Count > 0) && (PropertyDictionary.ContainsKey(type.Name)))
            {
                PropertyDictionary.TryGetValue(type.Name, out lstExclude);
                return tempList.FindAll(p => lstExclude.Contains(p.PropertyName));
            }
            else
            {
                return tempList;
            }
        }
    }
    #endregion
}
