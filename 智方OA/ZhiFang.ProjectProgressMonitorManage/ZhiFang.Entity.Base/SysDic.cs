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
    public static class SysPublicSet
    {
        public static class SysDicCookieSession
        {
            public static string LabID = "000100";//平台服务客户ID
            public static string LabName = "000101";//平台服务客户名称
            public static string IsLabFlag = "900000";//是否是平台服务客户,0不是，1是。

        }
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
        public static string 新任务提醒 = "NewTaskInfo";
        public static string 新任务通知 = "NewTaskMessage";
        public static string 验收通过提醒 = "TaskChecked";
        public static string 任务即将到期通知 = "TaskSoonExpired";
        public static string 公告通知 = "Announcement";
        public static string 科研动态通知 = "TechnologyInfo";
        public static string 绑定状态通知 = "BindStatus";
        public static string 用户绑定成功通知 = "BindSuccess";
        public static string 系统提醒 = "SysInfo";
        public static string 集成系统新消息提醒 = "SysNewMessageInfo";
        public static string 考勤提醒 = "ATMessageInfo";
        public static string 考勤状态通知 = "ATStatusMessageInfo";
        public static string 请假审批提醒 = "ATLeaveMessageInfo";
        public static string 请假条已被处理提醒 = "ATApprovaMessageInfo";
        public static string 出差审批通知 = "ATTripMessageInfo";
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

    public static class ATTypeId
    {
        public const long P签到 = 10101;
        public const long 迟到 = 10102;
        public const long P签退 = 10201;
        public const long 早退 = 10202;
        public const long P请假 = 10301;
        public const long 病假 = 10302;
        public const long 补签卡 = 10303;
        public const long 产假 = 10304;
        public const long 调休 = 10305;
        public const long 护理假 = 10306;
        public const long 婚假 = 10307;
        public const long 年假 = 10308;
        public const long 丧假 = 10309;
        public const long 事假 = 10310;
        public const long P外出 = 10401;
        public const long P出差 = 10501;
        public const long P加班 = 10601;
        public const long P上报位置 = 10701;
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
    
}
