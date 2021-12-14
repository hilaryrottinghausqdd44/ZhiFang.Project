using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.ReportFormQueryPrint.Model
{
    /// <summary>
    /// 文件物理存储时，做一个处理：在文件名后+（.zf）,用来防止病毒文件挂在服务器直接执行
    /// </summary>
    public enum FileExt
    {
        zf
    }
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string Describe { get; set; }
    }

    #region 数据库字典

    /// <summary>
    /// 数据库类型
    /// </summary>
    public static class DatabaseType
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 报告库 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Describe = "报告库"});
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 检验之星库 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Describe = "检验之星库" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(报告库.Key, 报告库.Value);
            dic.Add(检验之星库.Key, 检验之星库.Value);
            
            return dic;
        }
    }
    /// <summary>
    /// 报告库升级脚本版本
    /// </summary>
    public static class ReportDB
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1001 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.1", new BaseClassDicEntity() { Id = "1.0.0.1", Describe = "BS报告库所用的试图和存储过程"});
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1002 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.2", new BaseClassDicEntity() { Id = "1.0.0.2", Describe = "创建视图ReportFormAllQueryDataSource" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1003 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.3", new BaseClassDicEntity() { Id = "1.0.0.3", Describe = "创建表B_ColumnsSetting,B_ColumnsUnit,B_Parameter,B_SearchSetting,B_SearchUnit并增加数据" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1004 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.4", new BaseClassDicEntity() { Id = "1.0.0.4", Describe = "新建病区表，增加查询项" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1005 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.5", new BaseClassDicEntity() { Id = "1.0.0.5", Describe = "修改存储过程GetReportValue" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1006 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.6", new BaseClassDicEntity() { Id = "1.0.0.6", Describe = "修改表B_ColumnsUnit和B_ColumnsSetting解决显示列异常" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1007 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.7", new BaseClassDicEntity() { Id = "1.0.0.7", Describe = "修改存储过程GetReportMicroGroupFullList" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1008 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.8", new BaseClassDicEntity() { Id = "1.0.0.8", Describe = "删除B_SearchSetting中SearchType字段,删除B_ColumnsSetting中Render字段" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1009 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.9", new BaseClassDicEntity() { Id = "1.0.0.9", Describe = "增加SickType表,修改B_SearchUnit表中就诊类型数据" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本10010 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.10", new BaseClassDicEntity() { Id = "1.0.0.10", Describe = "增加SampleType表增加显示列和查询项" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1011 = new KeyValuePair<string, BaseClassDicEntity>("1.0.1.1", new BaseClassDicEntity() { Id = "1.0.1.1", Describe = "增加查询项增加wardType表修改打印次数显示列" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1012 = new KeyValuePair<string, BaseClassDicEntity>("1.0.1.2", new BaseClassDicEntity() { Id = "1.0.1.2", Describe = "创建用户科室对应表" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1013 = new KeyValuePair<string, BaseClassDicEntity>("1.0.1.3", new BaseClassDicEntity() { Id = "1.0.1.3", Describe = "解决查询项出生日期和年龄无法使用的问题" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(脚本1001.Key, 脚本1001.Value);
            dic.Add(脚本1002.Key, 脚本1002.Value);
            dic.Add(脚本1003.Key, 脚本1003.Value);
            dic.Add(脚本1004.Key, 脚本1004.Value);
            dic.Add(脚本1005.Key, 脚本1005.Value);
            dic.Add(脚本1006.Key, 脚本1006.Value);
            dic.Add(脚本1007.Key, 脚本1007.Value);
            dic.Add(脚本1008.Key, 脚本1008.Value);
            dic.Add(脚本1009.Key, 脚本1009.Value);
            dic.Add(脚本10010.Key, 脚本10010.Value);
            dic.Add(脚本1011.Key, 脚本1011.Value);
            dic.Add(脚本1012.Key, 脚本1012.Value);
            dic.Add(脚本1013.Key, 脚本1013.Value);

            return dic;
        }
    }
    /// <summary>
    /// 报告库升级脚本版本
    /// </summary>
    public static class DigitlabDB
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1001 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.1", new BaseClassDicEntity() { Id = "1.0.0.1", Describe = "BS报告库所用的试图和存储过程" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1002 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.2", new BaseClassDicEntity() { Id = "1.0.0.2", Describe = "创建视图ReportFormAllQueryDataSource" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1003 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.3", new BaseClassDicEntity() { Id = "1.0.0.3", Describe = "创建表B_ColumnsSetting,B_ColumnsUnit,B_Parameter,B_SearchSetting,B_SearchUnit并增加数据" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1004 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.4", new BaseClassDicEntity() { Id = "1.0.0.4", Describe = "删除B_SearchSetting中JsCode字段,增加查询项" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1005 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.5", new BaseClassDicEntity() { Id = "1.0.0.5", Describe = "修改存储过程GetReportValue" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1006 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.6", new BaseClassDicEntity() { Id = "1.0.0.6", Describe = "修改表B_ColumnsUnit和B_ColumnsSetting解决显示列异常" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1007 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.7", new BaseClassDicEntity() { Id = "1.0.0.7", Describe = "删除B_SearchSetting中SearchType字段,删除B_ColumnsSetting中Render字段" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1008 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.8", new BaseClassDicEntity() { Id = "1.0.0.8", Describe = "修改B_SearchUnit中就诊类型查询项" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1009 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.9", new BaseClassDicEntity() { Id = "1.0.0.9", Describe = "增加查询项和显示列" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本10010 = new KeyValuePair<string, BaseClassDicEntity>("1.0.0.10", new BaseClassDicEntity() { Id = "1.0.0.10", Describe = "增加request表四个试图和存储过程" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1011 = new KeyValuePair<string, BaseClassDicEntity>("1.0.1.1", new BaseClassDicEntity() { Id = "1.0.1.1", Describe = "增加查询项和全局配置修改打印次数" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1012 = new KeyValuePair<string, BaseClassDicEntity>("1.0.1.2", new BaseClassDicEntity() { Id = "1.0.1.2", Describe = "创建用户科室对应表" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1013 = new KeyValuePair<string, BaseClassDicEntity>("1.0.1.3", new BaseClassDicEntity() { Id = "1.0.1.3", Describe = "试图新增字段" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 脚本1014 = new KeyValuePair<string, BaseClassDicEntity>("1.0.1.4", new BaseClassDicEntity() { Id = "1.0.1.4", Describe = "解决查询项出生日期和年龄无法使用的问题" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(脚本1001.Key, 脚本1001.Value);
            dic.Add(脚本1002.Key, 脚本1002.Value);
            dic.Add(脚本1003.Key, 脚本1003.Value);
            dic.Add(脚本1004.Key, 脚本1004.Value);
            dic.Add(脚本1005.Key, 脚本1005.Value);
            dic.Add(脚本1006.Key, 脚本1006.Value);
            dic.Add(脚本1007.Key, 脚本1007.Value);
            dic.Add(脚本1008.Key, 脚本1008.Value);
            dic.Add(脚本1009.Key, 脚本1009.Value);
            dic.Add(脚本10010.Key, 脚本10010.Value);
            dic.Add(脚本1011.Key, 脚本1011.Value);
            dic.Add(脚本1012.Key, 脚本1012.Value);
            dic.Add(脚本1013.Key, 脚本1013.Value);
            dic.Add(脚本1014.Key, 脚本1014.Value);

            return dic;
        }
    }

    #endregion
}
