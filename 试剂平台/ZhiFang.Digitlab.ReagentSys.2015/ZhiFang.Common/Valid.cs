using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace ZhiFang.Common.Public
{
   public  class Valid
    {
        /// <summary>
        /// 将<c>object</c>转换成<c>string</c>字符串
        /// </summary>
        /// <param name="value">输入值</param>
        /// <returns>返回转换后的<c>string</c>字符串</returns>
        public static string ToString(object value)
        {
            if (value == null) return String.Empty;
            if (value is bool) return ((bool)value) ? "1" : "0";
            if (value is string) return ((string)value).Trim();
            if (value is ICollection) return ToString((ICollection)value);
            return value.ToString();
        }
        /// <summary>
        /// 将<c>Array</c>数组转换成<c>string</c>字符串
        /// </summary>
        /// <param name="value">输入数组</param>
        /// <returns>返回转换后的<c>string</c>字符串</returns>
        public static string ToString(ICollection value)
        {
            if (value is byte[]) return ToString((byte[])value);
            StringBuilder ret = new StringBuilder();
            foreach (object o in value)
            {
                if (o == null) continue;
                if (ret.Length > 0) ret.Append(',');
                if (o is string) ret.Append("'" + ToSafeString(o) + "'");
                else ret.Append(ToString(o));
            }
            return ret.ToString();
        }

        /// <summary>
        /// 将<c>byte</c>数组转换成<c>string</c>字符串
        /// </summary>
        /// <param name="value">输入<c>byte</c>数组</param>
        /// <returns>返回转换后的<c>string</c>字符串</returns>
        public static string ToString(byte[] value)
        {
            StringBuilder tmp = new StringBuilder("0x");
            for (int j = 0; j < value.Length; j++)
            {
                tmp.Append(value[j].ToString("x"));
            }
            return tmp.ToString();
        }
        /// <summary>
        /// 将<c>object</c>转换成安全的SQL字符串形式（转换单引号）
        /// </summary>
        /// <param name="value">输入值</param>
        /// <returns>返回安全的SQL字符串</returns>
        public static string ToSafeString(object value)
        {
            if (value == null) return String.Empty;
            if (value is string)
            {
                return value.ToString().Trim().Replace("'", "''");
            }
            return ToString(value);
        }

        /// <summary>
        /// 从string字符串转换成Byte数组
        /// </summary>
        /// <param name="value">string字符串</param>
        /// <returns>返回Byte数组</returns>
        public static byte[] ToByteArray(string value)
        {
            value = value.Trim();
            if (value[0] != '0' || value[1] != 'x' && value[1] != 'X') throw new ApplicationException("转换Byte时，字符串必须以0x开始!");
            if (value.Length % 2 == 0) value = value.Substring(2, value.Length - 2);
            else value = "0" + value.Substring(2, value.Length - 2);

            byte[] tmp = new byte[value.Length / 2];
            for (int i = 0; i < value.Length / 2; i++)
            {
                tmp[i] = (byte)(ToByte(value[2 * i]) * 16 + ToByte(value[2 * i + 1]));
            }
            return tmp;
        }
        /// <summary>
        /// 从char字符转换成Byte
        /// </summary>
        /// <param name="c">char字符</param>
        /// <returns>返回Byte</returns>
        public static byte ToByte(char c)
        {
            if (c >= 48 && c <= 57) return (byte)(c - 48);
            if (c >= 97 && c <= 102) return (byte)(c - 87);
            if (c >= 65 && c <= 70) return (byte)(c - 55);
            throw new ApplicationException("字符串转换Byte时含有非法字符");
        }


        public static long ToLong(string value)
        {
            return ToLong(value, 10, 0);
        }
        public static long ToLong(string value, int fromBase)
        {
            return ToLong(value, fromBase, 0);
        }
        public static long ToLong(string value, int fromBase, long init)
        {
            if (value == null) return init;
            try
            {
                return System.Convert.ToInt64(value, fromBase);
            }
            catch (Exception)
            {
                return init;
            }
        }


        public static int ToInt(string value)
        {
            return ToInt(value, 10, 0);
        }
        public static int ToInt(string value, int fromBase)
        {
            return ToInt(value, fromBase, 0);
        }
        public static int ToInt(string value, int fromBase, int init)
        {
            if (value == null) return init;
            try
            {
                return System.Convert.ToInt32(value, fromBase);
            }
            catch (Exception)
            {
                return init;
            }
        }


        /// <summary>
        /// 创建指定基本类型的实例
        /// </summary>
        /// <param name="type">基本类型</param>
        /// <returns>返回指定类型的实例（如果是<see cref="string"/>，返回<see cref="String.Empty"/>）</returns>
        public static object Create(Type type)
        {
            switch (type.Name)
            {
                case "String":
                    return String.Empty;
                case "Int32":
                case "UInt32":
                case "Int16":
                case "UInt16":
                case "Int64":
                case "Boolean":
                case "Byte":
                case "SByte":
                case "Byte[]":
                case "Char":
                case "Decimal":
                case "Double":
                case "Single":
                case "Object":
                    return System.Activator.CreateInstance(type);
                case "DateTime":
                    return DateTime.MinValue;
                default:
                    object tmp = System.Activator.CreateInstance(type);
                    FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                    foreach (FieldInfo fld in fields)
                    {
                        if (fld.Name == "String")
                            fld.SetValue(tmp, String.Empty);
                    }
                    return tmp;
            }
        }



        /// <summary>
        /// 不同类型之间的相互转换
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="destType">转换的类型</param>
        /// <returns>返回转换后的值</returns>
        public static object Convert(object value, Type destType)
        {
            return Convert(value, destType, null);
        }


        /// <summary>
        /// 不同类型之间的相互转换（带默认值）
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="destType">转换的类型</param>
        /// <param name="init">默认值</param>
        /// <returns>返回转换后的值</returns>
        public static object Convert(object value, Type destType, object init)
        {

            //如果目标类型是string，则调用ToString方法
            if (destType.Name == "String") return ToString(value);
            //如果源值为null，则返回init初始值
            //如果init初始值为null，则产生一个新的目标类型实例
            if (value == null)
            {
                if (init == null) return System.Activator.CreateInstance(destType);
                return init;
            }
            //获取源类型
            Type srcType = value.GetType();
            //如果两个类型一样，则直接返回
            if (srcType == destType) return value;

            try
            {
                if (srcType.Name == "String")
                {
                    switch (destType.Name)
                    {
                        case "Int32":
                            return int.Parse((string)value);
                        case "UInt32":
                            return uint.Parse((string)value);
                        case "Int16":
                            return short.Parse((string)value);
                        case "UInt16":
                            return ushort.Parse((string)value);
                        case "Int64":
                            return long.Parse((string)value);
                        case "Boolean":
                            return bool.Parse((string)value);
                        case "Byte":
                            return byte.Parse((string)value);
                        case "SByte":
                            return sbyte.Parse((string)value);
                        case "Byte[]":
                            return ToByteArray((string)value);
                        case "Char":
                            return ((string)value)[0];
                        case "Decimal":
                            return decimal.Parse((string)value);
                        case "Double":
                            return double.Parse((string)value);
                        case "Single":
                            return float.Parse((string)value);
                        case "DateTime":
                            return DateTime.Parse((string)value);
                        case "Object":
                            return ((string)value).Trim();
                    }
                }
                switch (srcType.Name)
                {
                    case "Int32":
                    case "UInt32":
                    case "Int16":
                    case "UInt16":
                    case "Int64":
                    case "Boolean":
                    case "Byte":
                    case "SByte":
                    case "Byte[]":
                    case "Char":
                    case "Decimal":
                    case "Double":
                    case "Single":
                    case "Object":
                        return System.Convert.ChangeType(value, destType);
                }
                FieldInfo[] srcInfo = srcType.GetFields(BindingFlags.Instance | BindingFlags.Public);
                FieldInfo[] destInfo = srcType.GetFields(BindingFlags.Instance | BindingFlags.Public);
                object tmp = System.Activator.CreateInstance(destType);
                for (int i = 0; i < destInfo.Length; i++)
                {
                    if (i < srcInfo.Length)
                        destInfo[i].SetValue(tmp, Convert(srcInfo[i].GetValue(value), destInfo[i].FieldType));
                    else
                        destInfo[i].SetValue(tmp, Create(destInfo[i].FieldType));
                }
                return tmp;
            }
            catch (Exception)
            {
                if (init == null) return System.Activator.CreateInstance(destType);
                return init;
            }

        }

        /// <summary>
        /// 数组类型与结构类型之间的相互转换
        /// </summary>
        /// <param name="value">原始数组类型</param>
        /// <param name="destType">目标结构类型</param>
        /// <returns>返回转换后的实例</returns>
        public static object Convert(Array value, Type destType)
        {
            if (destType.Name == "String") return ToString(value);
            if (value == null) return System.Activator.CreateInstance(destType);

            FieldInfo[] info = destType.GetFields(BindingFlags.Instance | BindingFlags.Public);
            object tmp = System.Activator.CreateInstance(destType);

            for (int i = 0; i < info.Length; i++)
            {
                if (i < value.Length)
                    info[i].SetValue(tmp, Valid.Convert(value.GetValue(i), info[i].FieldType));
                else
                    info[i].SetValue(tmp, Valid.Create(info[i].FieldType));
            }
            return tmp;
        }

        /// <summary>
        /// 数组类型与结构类型之间的相互转换
        /// </summary>
        /// <param name="value">原始数组类型</param>
        /// <param name="destType">目标结构类型</param>
        /// <returns>返回转换后的实例</returns>
        public static object Convert(IList value, Type destType)
        {
            if (destType.Name == "String") return ToString(value);
            if (value == null) return System.Activator.CreateInstance(destType);

            FieldInfo[] info = destType.GetFields(BindingFlags.Instance | BindingFlags.Public);
            object tmp = System.Activator.CreateInstance(destType);

            for (int i = 0; i < info.Length; i++)
            {
                if (i < value.Count)
                    info[i].SetValue(tmp, value[i]);
                else
                    info[i].SetValue(tmp, Valid.Create(info[i].FieldType));
            }
            return tmp;
        }

        /// <summary>
        /// 将值value转换到列表dest中
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="dest">目标列表</param>
        /// <returns>转换后的列表</returns>
        public static object Convert(object value, IList dest)
        {
            if (value == null) return dest;

            FieldInfo[] info = value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);

            for (int i = 0; i < info.Length; i++)
            {
                dest.Add(info[i].GetValue(value));
            }
            return dest;
        }
        /// <summary>
        /// 将值value转换到列表dest中
        /// </summary>
        /// <param name="value">原始值</param>
        /// <param name="dest">目标数组</param>
        /// <returns>转换后的数组</returns>
        public static object Convert(object value, Array dest)
        {
            if (value == null) return dest;

            FieldInfo[] info = value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);

            for (int i = 0; i < dest.Length; i++)
            {
                if (i < info.Length)
                    dest.SetValue(info[i].GetValue(value), i);
                else
                    break;
            }
            return dest;
        }
     

      
       

     

    }
}
