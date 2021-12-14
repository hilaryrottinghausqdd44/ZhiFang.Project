using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Common
{
    public enum QCValueTypeEnum
    {
        定量靶值标准差 = 1,
        定量区间 = 2,
        定性 = 3
    }

    public enum QCValueIsControlEnum
    {
        在控 = 1,
        警告 = 2,
        失控 = 3,
    }

    public enum ItemAllItemValueTypeEnum
    {
        定量 = 1,
        定性 = 2,
        定性定量 = 3,
        描述 = 4,
        图形 = 5
    }

    public enum EPBEquipDoubleDir
    {
        单向仪器 = 1,
        双向仪器 = 2
    }

    //public enum EPBEquipType
    //{
    //    生免类仪器 = 1,
    //    酶免类仪器 = 2
    //}

    public class EnumDictionary
    {
        public static Dictionary<int, string> EnumToDictionary(string EnumTypeName)
        {
            string NameSpace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            Type enumType = Type.GetType(NameSpace + "." + EnumTypeName);
            //string keyName = enumType.FullName;
            //List<string> EnumList = new List<string>();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            //if (EnumList.Contains(keyName))
            if (enumType != null)
            {
                foreach (int enumValue in Enum.GetValues(enumType))
                {
                    string enumName = Enum.GetName(enumType, enumValue);
                    dictionary.Add(enumValue, enumName);
                }
            }
            return dictionary;
        }

        public static List<int> EnumToKeyList(string EnumTypeName)
        {
            return EnumToDictionary(EnumTypeName).Keys.ToList<int>();
        }
        public static List<string> EnumToNameList(string EnumTypeName)
        {
            return EnumToDictionary(EnumTypeName).Values.ToList<string>();
        }
    }    
}
