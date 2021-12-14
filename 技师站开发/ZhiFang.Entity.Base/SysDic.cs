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
    }
    public class ClassDicSearchPara
    {
        public string classname { get; set; }
        public string classnamespace { get; set; }
    }
}
