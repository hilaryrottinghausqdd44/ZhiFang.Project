using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.Base
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
    public enum ReportFormType
    {
        all,
        //1.生化类
        Normal,
        //2.微生物
        Micro,
        //3.生化类（图）
        NormalIncImage,
        //4.微生物（图）
        MicroIncImage,
        //5.细胞形态学
        CellMorphology,
        //6.Fish检测（图）
        FishCheck,
        //7.院感检测（图）
        SensorCheck,
        //8.染色体检测（图）
        ChromosomeCheck,
        //9.病理检测（图）
        PathologyCheck,
    }

    public static class SysDicCookieSession
    {
        public static string LabID = "000100";//平台服务客户ID
        public static string LabName = "000101";//平台服务客户名称
        public static string IsLabFlag = "900000";//是否是平台服务客户,0不是，1是。        
    }

    public static class SysWeiXinTemplate//有点复杂了，看看以后能不能存到配置文件里，而不是数据库。
    {
        //public delegate void PushWeiXinMessageTest(string openid, string templateid, string color, string url);
        public delegate void PushWeiXinMessage(string openid, string templateid, string color, string url, Dictionary<string, TemplateDataObject> data);
        public static string 退费通知 = "Refunform";
        public static string 结算成功提醒 = "DoctorBonusFinish";
        public static string 医嘱提醒 = "OrderFormPush";
        public static string 检验报告通知 = "ReportFormPush";
        public static string 退费通知医生 = "RefunformDoct";
        public static string 退费通知银行 = "RefunformBank";
        public static string 退费通知精简 = "RefunformSingle";
        public static string t8 = "BindSuccess";
        public static string t9 = "SysInfo";
        public static string t10 = "SysNewMessageInfo";
        public static string t11 = "ATMessageInfo";
        public static string t12 = "ATStatusMessageInfo";
        public static string t13 = "ATLeaveMessageInfo";
        public static string t14 = "ATApprovaMessageInfo";
        public static string t15 = "ATTripMessageInfo";
        public static string t16 = "NewTaskInfo";
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

    public static class SysWeiXinPayBack
    {
        public delegate SortedDictionary<string, object> PayBack(string transaction_id, string out_trade_no, string total_fee, string refund_fee, string out_refund_no);

    }
    public static class SysWeiXinPayToUser
    {
        public delegate SortedDictionary<string, object> PayToUser(string partner_trade_no, string OpenID, string re_user_name, float amount, string desc, string check_name = "FORCE_CHECK");

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

}
