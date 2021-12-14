using System.Collections.Generic;

namespace ZhiFang.Entity.Common
{

    /// <summary>
    /// 文件物理存储时，做一个处理：在文件名后+（.zf）,用来防止病毒文件挂在服务器直接执行
    /// </summary>
    public enum FileExt
    {
        zf
    }
    public class BaseClassDicModelConfigEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string cols { get; set; }
        public string URL { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
    }
    #region 配置参数字典

    public static class FormControl_Config
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 输入框 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1001", new BaseClassDicModelConfigEntity() { Id = "1001", Name = "输入框", Code = "Input", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期框年类型 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1002", new BaseClassDicModelConfigEntity() { Id = "1002", Name = "日期框年类型", Code = "DateYear", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期框范围年类型 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1003", new BaseClassDicModelConfigEntity() { Id = "1003", Name = "日期框范围年类型", Code = "DateYearRange", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期框年月类型 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1004", new BaseClassDicModelConfigEntity() { Id = "1004", Name = "日期框年月类型", Code = "DateYearMonth", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期框范围年月类型 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1005", new BaseClassDicModelConfigEntity() { Id = "1005", Name = "日期框范围年月类型", Code = "DateYearMonthRange", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期选择器 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1006", new BaseClassDicModelConfigEntity() { Id = "1006", Name = "日期选择器", Code = "Date", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期选择器范围 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1007", new BaseClassDicModelConfigEntity() { Id = "1007", Name = "日期选择器范围", Code = "DateRange", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 时间选择器 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1008", new BaseClassDicModelConfigEntity() { Id = "1008", Name = "时间选择器", Code = "Time", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 时间选择器范围 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1009", new BaseClassDicModelConfigEntity() { Id = "1009", Name = "时间选择器范围", Code = "TimeRange", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期时间选择器 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1010", new BaseClassDicModelConfigEntity() { Id = "1010", Name = "日期时间选择器", Code = "DateTime", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 日期时间选择器范围 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1011", new BaseClassDicModelConfigEntity() { Id = "1011", Name = "日期时间选择器范围", Code = "DateTimeRange", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 文本域 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1012", new BaseClassDicModelConfigEntity() { Id = "1012", Name = "文本域", Code = "TextArea", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 单选下拉框 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1013", new BaseClassDicModelConfigEntity() { Id = "1013", Name = "单选下拉框", Code = "RadioSelect", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 复选下拉框 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1014", new BaseClassDicModelConfigEntity() { Id = "1014", Name = "复选下拉框", Code = "CheckSelect", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 单选框 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1015", new BaseClassDicModelConfigEntity() { Id = "1015", Name = "单选框", Code = "Radio", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 复选框 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1016", new BaseClassDicModelConfigEntity() { Id = "1016", Name = "复选框", Code = "CheckBox", FontColor = "#ffffff", BGColor = "#f4c600" });

    }

    public static class TableControl_Config
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 常规列 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1001", new BaseClassDicModelConfigEntity() { Id = "1001", Name = "常规列", Code = "normal", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 复选列 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1002", new BaseClassDicModelConfigEntity() { Id = "1002", Name = "复选列", Code = "checkbox", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 单选列 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1003", new BaseClassDicModelConfigEntity() { Id = "1003", Name = "单选列", Code = "radio", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 序号列 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1004", new BaseClassDicModelConfigEntity() { Id = "1004", Name = "序号列", Code = "numbers", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicModelConfigEntity> 空列 = new KeyValuePair<string, BaseClassDicModelConfigEntity>("1005", new BaseClassDicModelConfigEntity() { Id = "1005", Name = "空列", Code = "space", FontColor = "#ffffff", BGColor = "#f4c600" });
    }
   
    #endregion

}
