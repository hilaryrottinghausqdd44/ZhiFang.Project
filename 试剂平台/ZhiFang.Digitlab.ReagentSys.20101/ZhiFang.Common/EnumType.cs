using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Common.Public
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

    public enum ImmCalcModelEnum
    {
        定性计算 = 0,
        定量计算 = 1,
        仅计算OD值 = 2
    }

    public enum ImmSampleTypeEnum
    {
        无 = 0,
        待 = 1,
        空 = 2,
        阴 = 3,
        阳 = 4,
        质 = 5,
        临 = 6,
        标 = 7,
        测 = 99
    }

    public enum MEGroupSampleFormMainStateEnum
    {
        检验中 = 0,
        已审核 = 1,
        已终审 = 2,
        临时冻结 = -1,
        作废 = -2
    }
    //public class MEGroupSampleFormMainStateEnum
    //{
    //    public static Dictionary<string, int> GetMEGroupSampleFormMainStateEnum()
    //    {
    //        Dictionary<string, int> dic = new Dictionary<string, int>();
    //        dic.Add("检验中", 0);
    //        dic.Add("已审核", 1);
    //        dic.Add("已终审", 2);
    //        dic.Add("临时冻结", -1);
    //        dic.Add("作废", -2);
    //        return dic;
    //    }
    //}
    //public enum EPBEquipType
    //{
    //    生免类仪器 = 1,
    //    酶免类仪器 = 2
    //}
    public enum MEMicroBCBottleManageInfoBottleState
    {
        已入库 = 0,
        已出库 = 1,
        已返还 = 2,
        已过期 = 3,
        已到报警状态 = 4
    }
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
