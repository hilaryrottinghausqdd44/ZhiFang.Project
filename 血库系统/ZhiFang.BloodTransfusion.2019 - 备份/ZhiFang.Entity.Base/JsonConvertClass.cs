using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;

namespace ZhiFang.Entity.Base
{
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
                case "System.String":
                    canConvert = true;
                    break;
            }
            return canConvert;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object returnObject = existingValue;
            if (objectType == typeof(Byte[]))
            {
                returnObject = reader.Value;
                if (returnObject != null)
                {
                    string strValue = reader.Value.ToString();
                    strValue = strValue.Replace("[", "");
                    strValue = strValue.Replace("]", "");
                    strValue = strValue.Replace("\'", "");
                    strValue = strValue.Replace("\"", "");
                    string[] list = strValue.Split(',');
                    byte[] byteValue = new byte[list.Length];
                    for (int i = 0; i < list.Length; i++)
                    {
                        byteValue[i] = (byte)int.Parse(list[i]);
                    }
                    returnObject = byteValue;
                }

            }
            else if (objectType == typeof(DateTime?))
            {
                returnObject = reader.Value;
                if (returnObject != null)
                {
                    returnObject = DateTime.Parse(returnObject.ToString());
                }
            }
            else if (objectType == typeof(Int64) || objectType == typeof(Single) || objectType == typeof(Double) || objectType == typeof(Decimal) || objectType == typeof(Int64?))
            {
                returnObject = reader.Value;
                if (returnObject != null)
                {
                    returnObject = long.Parse(returnObject.ToString());
                }
            }
            return returnObject;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(Int64) || value.GetType() == typeof(Single) || value.GetType() == typeof(Double) || value.GetType() == typeof(Decimal))
            {
                writer.WriteValue(value.ToString());
            }
            if (value.GetType() == typeof(Byte[]))
            {
                writer.WriteValue(ZhiFang.Common.Public.JsonSerializer.ByteTypeToString((byte[])value));
            }
            if (value.GetType() == typeof(DateTime))
            {

                //writer.WriteValue(((System.DateTime)value).Ticks.ToString());
                writer.WriteValue(((System.DateTime)value).ToString(ZhiFang.Common.Public.JsonSerializer.JsonDateTimeFormat, DateTimeFormatInfo.InvariantInfo));
            }
        }

        public override bool CanRead
        {
            get
            {
                return true;
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
    }
}
