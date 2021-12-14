using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Entity.Common
{
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
    }
    public class BaseClassDicEntity<T>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
        public T Extend { get; set; }
    }
    public enum BParameterParaNo
    {
        IsSyncWebConfig,//系统参数表是否同步过web.config的相关配置项
        UploadFilesPath,//附件上传保存路径
        UpLoadPicturePath,
        uploadConfigIson,//在线编辑器的上传配置文件的路径
        ExcelExportSavePath,//execl导出的临时保存路径
        UploadEmpSignPath,//电子签名保存路径
        SaveHelpHtmlAndJson,//帮助系统生成html保存路径
        QMSFFReviseNoRule//QMS文档修订号规则:修订号规则:固定前缀|变化前缀(默认取年份)|当前序号
    }
    /// <summary>
    /// 文档状态
    /// </summary>
    public static class FFileStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 暂存 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "暂存", Code = "TmpApply", FontColor = "#ffffff", BGColor = "#2c2c2c" });
        public static KeyValuePair<string, BaseClassDicEntity> 已提交 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已提交", Code = "Submit", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static KeyValuePair<string, BaseClassDicEntity> 已审核 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "已审核", Code = "Checked", FontColor = "#ffffff", BGColor = "#aad08f" });
        public static KeyValuePair<string, BaseClassDicEntity> 已批准 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "已批准", Code = "Approval", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static KeyValuePair<string, BaseClassDicEntity> 发布 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "发布", Code = "Release", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static KeyValuePair<string, BaseClassDicEntity> 作废 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "作废", Code = "Invalid", FontColor = "#ffffff", BGColor = "#FF000F" });

        public static KeyValuePair<string, BaseClassDicEntity> 撤消提交 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "撤消提交", Code = "UndoSubmit", FontColor = "#ffffff", BGColor = "#e98f36" });

        public static KeyValuePair<string, BaseClassDicEntity> 审核退回 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "审核退回", Code = "AuditBack", FontColor = "#ffffff", BGColor = "#7dc5eb" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批退回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "审批退回", Code = "TwoAuditBack", FontColor = "#ffffff", BGColor = "#1195db" });
        // public static KeyValuePair<string, BaseClassDicEntity> 撤消发布 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "撤消发布", Code = "PublishBack", FontColor = "#ffffff", BGColor = "#88147f" });

        //public static KeyValuePair<string, BaseClassDicEntity> 禁用 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "禁用", Code = "Disabled", FontColor = "#ffffff", BGColor = "#e8989a" });
        //public static KeyValuePair<string, BaseClassDicEntity> 撤消禁用 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "撤消禁用", Code = "UndoDisabled", FontColor = "#ffffff", BGColor = "#0D0D0D" });
        public static KeyValuePair<string, BaseClassDicEntity> 打回起草人 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "打回起草人", Code = "DraftedBack", FontColor = "#ffffff", BGColor = "#2c2c2c" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(FFileStatus.暂存.Key, FFileStatus.暂存.Value);
            dic.Add(FFileStatus.已提交.Key, FFileStatus.已提交.Value);
            dic.Add(FFileStatus.已审核.Key, FFileStatus.已审核.Value);
            dic.Add(FFileStatus.已批准.Key, FFileStatus.已批准.Value);
            dic.Add(FFileStatus.发布.Key, FFileStatus.发布.Value);
            dic.Add(FFileStatus.作废.Key, FFileStatus.作废.Value);
            dic.Add(FFileStatus.撤消提交.Key, FFileStatus.撤消提交.Value);
            dic.Add(FFileStatus.审核退回.Key, FFileStatus.审核退回.Value);
            dic.Add(FFileStatus.审批退回.Key, FFileStatus.审批退回.Value);
            //dic.Add(FFileStatus.撤消发布.Key, FFileStatus.撤消发布.Value);
            dic.Add(FFileStatus.打回起草人.Key, FFileStatus.打回起草人.Value);
            return dic;
        }
    }
    /// <summary>
    /// 文档操作记录的操作类型
    /// </summary>
    public enum FFileOperationType
    {
        起草 = 1,
        修订 = 2,
        审核 = 3,
        批准 = 4,
        发布 = 5,
        浏览 = 6,
        作废 = 7,
        撤消提交 = 8,
        审核退回 = 9,
        批准退回 = 10,
        撤消发布 = 11,
        修订文档 = 12,
        禁用 = 13,
        撤消禁用 = 14,
        打回起草人 = 15,
        置顶 = 16,
        撤消置顶 = 17,
        更新文档类型 = 18,
        编辑更新 = 19//新闻管理/文档管理时的文档内容(不更新文档状态/阅读对象信息)的编辑更新操作
    }
    /// <summary>
    /// 文件物理存储时，做一个处理：在文件名后+（.zf）,用来防止病毒文件挂在服务器直接执行
    /// </summary>
    public enum FileExt
    {
        zf
    }

    /// <summary>
    /// 文档抄送对象/文档阅读对象的对象类型
    /// </summary>
    public enum FFileObjectType
    {
        无 = -1,
        全部人员 = 1,
        科室 = 2,
        角色 = 3,
        人员 = 4,
        区域 = 5
    }

    /// <summary>
    /// 文档参数
    /// </summary>
    public static class FFileParameter
    {
        public static string UploadFilesPath = "UploadFiles";
        public static string UpLoadPicturePath = "Images";
        public static string uploadConfigIson = "upload.json";
        public static string SaveHelpHtmlAndJson = "Helper";
    }
}
