using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Digitlab.Entity
{
    public enum SysDic
    {
        All,
        Text,
        Number,
        DateTime,
        Image,
        SText,
        NText,
        File,
        List
    }
    public class ItemTypeCodeDic
    {
        public static string GetItemTypeCodeDicJson()
        {
            string tmp = "";
            tmp += "{'Name':'生化类小组','Code':'TestGroupNormal','Id':'1'},";
            tmp += "{'Name':'微生物类小组','Code':'TestGroupMicro','Id':'2'},";
            tmp += "{'Name':'微生物-涂片类小组','Code':'TestGroupMicroSmear','Id':'21'},";
            tmp += "{'Name':'微生物-涂片类小组（扩展）','Code':'TestGroupMicroSmearExt','Id':'211'},";
            tmp += "{'Name':'微生物-培养、鉴定及药敏类小组','Code':'TestGroupMicroCultureAssayAntibioticSusceptibility','Id':'22'},";
            tmp += "{'Name':'微生物-培养、鉴定及药敏类小组（扩展）','Code':'TestGroupMicroCultureAssayAntibioticSusceptibilityExt','Id':'221'},";
            tmp += "{'Name':'微生物-第三方检测（外送）','Code':'TestGroupMicroOtherTest','Id':'23'},";
            tmp += "{'Name':'微生物-病毒免疫','Code':'TestGroupMicroVirusImmunity','Id':'24'},";
            tmp += "{'Name':'生化带图类小组','Code':'TestGroupNormalImage','Id':'3'},";
            tmp += "{'Name':'微生物带图类小组','Code':'TestGroupMicroImage','Id':'4'},";
            tmp += "{'Name':'细胞形态学类小组','Code':'TestGroupCellMorphology','Id':'5'},";
            tmp += "{'Name':'Fish检测（图）类小组','Code':'TestGroupFishCheck','Id':'6'},";
            tmp += "{'Name':'院感检测（图）类小组','Code':'TestGroupSensorCheck','Id':'7'},";
            tmp += "{'Name':'染色体检测（图）类小组','Code':'TestGroupChromosomeCheck','Id':'8'},";
            tmp += "{'Name':'病理检测（图）类小组','Code':'TestGroupPathologyCheck','Id':'9'}";
            tmp = "{'ItemTypeList':[" + tmp + "]}";
            return tmp;

        }
    }

    public class EquipTypeCodeDic
    {
        public static string GetEquipTypeCodeDicJson()
        {
            string tmp = "";
            tmp += "{'Name':'生化类仪器','Code':'TestEquipNormal','Id':'1'},";
            tmp += "{'Name':'微生物类仪器','Code':'TestEquipMicro','Id':'2'},";
            tmp += "{'Name':'生化带图类仪器','Code':'TestEquipNormalImage','Id':'3'},";
            tmp += "{'Name':'微生物带图类仪器','Code':'TestEquipMicroImage','Id':'4'},";
            tmp += "{'Name':'细胞形态学类仪器','Code':'TestEquipCellMorphology','Id':'5'},";
            tmp += "{'Name':'Fish检测（图）类仪器','Code':'TestEquipFishCheck','Id':'6'},";
            tmp += "{'Name':'院感检测（图）类仪器','Code':'TestEquipSensorCheck','Id':'7'},";
            tmp += "{'Name':'染色体检测（图）类仪器','Code':'TestEquipChromosomeCheck','Id':'8'},";
            tmp += "{'Name':'病理检测（图）类仪器','Code':'TestEquipPathologyCheck','Id':'9'}";
            tmp = "{'EquipTypeList':[" + tmp + "]}";
            return tmp;
        }
    }

    public class MicroStepTypeDic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public static List<MicroStepTypeDic> GetMicroStepTypeDicJson()
        {
            List<MicroStepTypeDic> mstdlist = new List<MicroStepTypeDic>();
            //微生物检验步骤
            mstdlist.Add(new MicroStepTypeDic() { Id = 1, Name = "涂片", Code = "Smear" });
            mstdlist.Add(new MicroStepTypeDic() { Id = 2, Name = "培养", Code = "Culture" });
            mstdlist.Add(new MicroStepTypeDic() { Id = 3, Name = "鉴定", Code = "Identification" });
            mstdlist.Add(new MicroStepTypeDic() { Id = 4, Name = "留菌", Code = "RetainedBacteria" });
            mstdlist.Add(new MicroStepTypeDic() { Id = 5, Name = "手工鉴定结果", Code = "MicroAppraisalTest" });
            //三级报告
            mstdlist.Add(new MicroStepTypeDic() { Id = 91, Name = "一级报告", Code = "MicroReportFristLevel" });
            mstdlist.Add(new MicroStepTypeDic() { Id = 92, Name = "二级报告", Code = "MicroReportSecendLevel" });
            mstdlist.Add(new MicroStepTypeDic() { Id = 93, Name = "三级报告", Code = "MicroReportThirdLevel" });

            return mstdlist;
        }
    }
    public static class SysPublicSet
    {
        public static class FFileNews
        {
            public static string ThumbPath = "ServerMedia\\PermanentMedia\\News\\Thumb\\";
            public static string ThumbServerPath = System.AppDomain.CurrentDomain.BaseDirectory + ThumbPath;
        }
    }
    public static class SysWeiXinTemplate//有点复杂了，看看以后能不能存到配置文件里，而不是数据库。
    {
        //public delegate void PushWeiXinMessageTest(string openid, string templateid, string color, string url);
        public delegate void PushWeiXinMessage(string openid, string templateid, string color, string url, Dictionary<string, TemplateDataObject> data);
        public static string 新订单提醒 = "NewOrderInfo";
        public static string 订单状态通知 = "OrderInfoStatusChange";
        public static string 新订单提醒公司 = "NewOrderInfoToComp";
        public static string t3 = "TaskSoonExpired";
        public static string t4 = "Announcement";
        public static string t5 = "TechnologyInfo";
        public static string t6 = "BindStatus";
        public static string t7 = "BindSuccess";
        public static string t8 = "SysInfo";
        public static string t9 = "SysNewMessageInfo";
        public static string t10 = "ATMessageInfo";
        public static string t11 = "ATStatusMessageInfo";
        public static string t12 = "ATLeaveMessageInfo";
        public static string t13 = "ATApprovaMessageInfo";
        public static string t14 = "ATTripMessageInfo";
        public static string t15 = "NewTaskInfo";
        public static string t17 = "NewTaskMessage";
        public static string t18 = "TaskChecked";
        public static string t19 = "NewTaskInfo";
        public static string t20 = "NewTaskMessage";
        public static string t21 = "NewTaskInfo";
        public static string t22 = "NewTaskMessage";
        public static string t23 = "TaskChecked";
        public static string t24 = "NewTaskInfo";
        public static string t25 = "NewTaskMessage";

    }

    public class TemplateIdObject1
    {
        public TemplateDataObject first { get; set; }
        public TemplateDataObject keyword1 { get; set; }
        public TemplateDataObject remark { get; set; }
    }

    public class TemplateIdObject2 : TemplateIdObject1
    {
        public TemplateDataObject keyword2 { get; set; }
    }

    public class TemplateIdObject3 : TemplateIdObject2
    {
        public TemplateDataObject keyword3 { get; set; }
    }

    public class TemplateIdObject4 : TemplateIdObject3
    {
        public TemplateDataObject keyword4 { get; set; }
    }

    public class TemplateIdObject5 : TemplateIdObject4
    {
        public TemplateDataObject keyword5 { get; set; }
    }

    public class TemplateDataObject
    {
        public string value { get; set; }
        public string color { get; set; }
    }

    public class ClassDicSearchPara
    {
        public string classname { get; set; }
        public string classnamespace { get; set; }
    }

    public enum PermanentMediaType
    {
        news,
        image,
        video,
        voice
    }
    /// <summary>
    /// 系统变量（全局变量）
    /// </summary>
    public class SysVar
    {
        //换行符
        static string LineBreak = "<br>";
    }
}
