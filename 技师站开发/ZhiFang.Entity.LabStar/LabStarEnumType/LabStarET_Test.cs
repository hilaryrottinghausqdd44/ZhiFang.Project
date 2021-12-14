using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar
{
    //此文件定义常规检验枚举类

    /// <summary>
    /// 0:分发 1:lis人工申请 2:仪器增加 3：通讯核收 4：人工核收检验申请单
    /// </summary>
    public static class TestFormSource
    {
        public static KeyValuePair<string, BaseClassDicEntity> 分发核收 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "分发核收", Code = "FFHS", FontColor = "#ffffff", BGColor = "", Memo = "分发核收" });
        public static KeyValuePair<string, BaseClassDicEntity> LIS = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "LIS", Code = "LIS", FontColor = "#ffffff", BGColor = "", Memo = "LIS" });
        public static KeyValuePair<string, BaseClassDicEntity> 仪器 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "仪器", Code = "YQ", FontColor = "#ffffff", BGColor = "", Memo = "仪器" });
        public static KeyValuePair<string, BaseClassDicEntity> 通讯核收 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "通讯核收", Code = "TXHS", FontColor = "#ffffff", BGColor = "", Memo = "通讯核收" });
        public static KeyValuePair<string, BaseClassDicEntity> 人工核收 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "人工核收", Code = "RGHS", FontColor = "#ffffff", BGColor = "", Memo = "人工核收" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 分发核收.Key, 分发核收.Value },
                { LIS.Key, LIS.Value },
                { 仪器.Key, 仪器.Value },
                { 通讯核收.Key, 通讯核收.Value },
                { 人工核收.Key, 人工核收.Value }
            };
            return dic;
        }
    }


    /// <summary>
    /// 检验单主状态
    /// </summary>
    public static class TestFormMainStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 医生审核 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "医生审核", Code = "YSSH", FontColor = "#ffffff", BGColor = "", Memo = "医生审核-预留" });
        public static KeyValuePair<string, BaseClassDicEntity> 终审 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "终审", Code = "ZS", FontColor = "#ffffff", BGColor = "", Memo = "终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 初审 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "初审", Code = "CS", FontColor = "#ffffff", BGColor = "", Memo = "初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 中间报告 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "中间报告", Code = "ZJBG", FontColor = "#ffffff", BGColor = "", Memo = "中间报告" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验中 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "检验中", Code = "JYZ", FontColor = "#ffffff", BGColor = "", Memo = "检验中" });
        public static KeyValuePair<string, BaseClassDicEntity> 复检备份 = new KeyValuePair<string, BaseClassDicEntity>("-1", new BaseClassDicEntity() { Id = "-1", Name = "复检备份", Code = "FJBF", FontColor = "#ffffff", BGColor = "", Memo = "复检备份" });
        public static KeyValuePair<string, BaseClassDicEntity> 删除作废 = new KeyValuePair<string, BaseClassDicEntity>("-2", new BaseClassDicEntity() { Id = "-2", Name = "删除作废", Code = "SCBF", FontColor = "#ffffff", BGColor = "", Memo = "删除作废" });
        public static KeyValuePair<string, BaseClassDicEntity> 反审备份 = new KeyValuePair<string, BaseClassDicEntity>("-3", new BaseClassDicEntity() { Id = "-3", Name = "反审备份", Code = "FSBF", FontColor = "#ffffff", BGColor = "", Memo = "反审备份" });
        public static KeyValuePair<string, BaseClassDicEntity> 冻结 = new KeyValuePair<string, BaseClassDicEntity>("-4", new BaseClassDicEntity() { Id = "-4", Name = "冻结", Code = "DJ", FontColor = "#ffffff", BGColor = "", Memo = "冻结，作废的一种，检验单存疑" });
        public static KeyValuePair<string, BaseClassDicEntity> 合并 = new KeyValuePair<string, BaseClassDicEntity>("-5", new BaseClassDicEntity() { Id = "-5", Name = "合并", Code = "HB", FontColor = "#ffffff", BGColor = "", Memo = "合并检验单，删除检验单备份" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 医生审核.Key, 医生审核.Value },
                { 终审.Key, 终审.Value },
                { 初审.Key, 初审.Value },
                { 中间报告.Key, 中间报告.Value },
                { 检验中.Key, 检验中.Value },
                { 复检备份.Key, 复检备份.Value },
                { 删除作废.Key, 删除作废.Value },
                { 反审备份.Key, 反审备份.Value },
                { 冻结.Key, 冻结.Value },
                { 合并.Key, 合并.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 0:手工增加 1:仪器增加 2:分发核收
    /// </summary>
    public static class TestItemSource
    {
        public static KeyValuePair<string, BaseClassDicEntity> 手工增加 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "0", Name = "手工增加", Code = "SGZJ", FontColor = "#ffffff", BGColor = "", Memo = "手工增加" });
        public static KeyValuePair<string, BaseClassDicEntity> 仪器增加 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "1", Name = "仪器增加", Code = "YQZJ", FontColor = "#ffffff", BGColor = "", Memo = "仪器增加" });
        public static KeyValuePair<string, BaseClassDicEntity> 分发核收 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "2", Name = "分发核收", Code = "FFHS", FontColor = "#ffffff", BGColor = "", Memo = "分发核收" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 分发核收.Key, 分发核收.Value },
                { 手工增加.Key, 手工增加.Value },
                { 仪器增加.Key, 仪器增加.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验单项目主状态
    /// </summary>
    public static class TestItemMainStatus
    {
        public static KeyValuePair<string, BaseClassDicEntity> 正常 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "正常", Code = "ZC", FontColor = "#ffffff", BGColor = "", Memo = "正常" });
        public static KeyValuePair<string, BaseClassDicEntity> 复检 = new KeyValuePair<string, BaseClassDicEntity>("-1", new BaseClassDicEntity() { Id = "-1", Name = "复检", Code = "FJ", FontColor = "#ffffff", BGColor = "", Memo = "复检" });
        public static KeyValuePair<string, BaseClassDicEntity> 删除作废 = new KeyValuePair<string, BaseClassDicEntity>("-2", new BaseClassDicEntity() { Id = "-2", Name = "删除作废", Code = "SCZF", FontColor = "#ffffff", BGColor = "", Memo = "删除作废" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 正常.Key, 正常.Value },
                { 复检.Key, 复检.Value },
                { 删除作废.Key, 删除作废.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验单过程状态
    /// </summary>
    public static class TestFormStatusID
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验单生成 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "检验单生成", Code = "JYDSC", FontColor = "#ffffff", BGColor = "", Memo = "检验单生成" });
        public static KeyValuePair<string, BaseClassDicEntity> 双向发送 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "双向发送", Code = "SXFS", FontColor = "#ffffff", BGColor = "", Memo = "双向发送" });
        public static KeyValuePair<string, BaseClassDicEntity> 上机检验 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "上机检验", Code = "SJJY", FontColor = "#ffffff", BGColor = "", Memo = "上机检验" });
        public static KeyValuePair<string, BaseClassDicEntity> 仪器检验出结果 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "仪器检验出结果", Code = "YQJYCJG", FontColor = "#ffffff", BGColor = "", Memo = "仪器检验出结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验完成 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "检验完成", Code = "JYWC", FontColor = "#ffffff", BGColor = "", Memo = "检验完成" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统判定失败 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "系统判定失败", Code = "XTPDSB", FontColor = "#ffffff", BGColor = "", Memo = "系统判定失败" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统判定成功 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "系统判定成功", Code = "XTPDCG", FontColor = "#ffffff", BGColor = "", Memo = "系统判定成功" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本组内签收 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "样本组内签收", Code = "YBZNQS", FontColor = "#ffffff", BGColor = "", Memo = "样本组内签收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本拒收 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "样本拒收", Code = "YBJS", FontColor = "#ffffff", BGColor = "", Memo = "样本拒收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本让步 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "样本让步", Code = "YBRB", FontColor = "#ffffff", BGColor = "", Memo = "样本让步" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本核收 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "样本核收", Code = "YBHS", FontColor = "#ffffff", BGColor = "", Memo = "样本核收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "样本分发", Code = "YBFF", FontColor = "#ffffff", BGColor = "", Memo = "样本分发" });
        public static KeyValuePair<string, BaseClassDicEntity> 初审 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "初审", Code = "CS", FontColor = "#ffffff", BGColor = "", Memo = "初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 终审 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "终审", Code = "ZS", FontColor = "#ffffff", BGColor = "", Memo = "终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 反审 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "反审", Code = "FS", FontColor = "#ffffff", BGColor = "", Memo = "反审" });
        public static KeyValuePair<string, BaseClassDicEntity> 复检 = new KeyValuePair<string, BaseClassDicEntity>("16", new BaseClassDicEntity() { Id = "16", Name = "复检", Code = "FJ", FontColor = "#ffffff", BGColor = "", Memo = "复检" });
        public static KeyValuePair<string, BaseClassDicEntity> 医生审核 = new KeyValuePair<string, BaseClassDicEntity>("17", new BaseClassDicEntity() { Id = "17", Name = "医生审核", Code = "YSSH", FontColor = "#ffffff", BGColor = "", Memo = "医生审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验超时 = new KeyValuePair<string, BaseClassDicEntity>("18", new BaseClassDicEntity() { Id = "18", Name = "检验超时", Code = "JYCS", FontColor = "#ffffff", BGColor = "", Memo = "检验超时" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核超时 = new KeyValuePair<string, BaseClassDicEntity>("19", new BaseClassDicEntity() { Id = "19", Name = "审核超时", Code = "SHCS", FontColor = "#ffffff", BGColor = "", Memo = "审核超时" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统自动初审 = new KeyValuePair<string, BaseClassDicEntity>("20", new BaseClassDicEntity() { Id = "20", Name = "系统自动初审", Code = "XTZDCS", FontColor = "#ffffff", BGColor = "", Memo = "系统自动初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统自动终审 = new KeyValuePair<string, BaseClassDicEntity>("21", new BaseClassDicEntity() { Id = "21", Name = "系统自动终审", Code = "XTZDZS", FontColor = "#ffffff", BGColor = "", Memo = "系统自动终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告初审后处理标记 = new KeyValuePair<string, BaseClassDicEntity>("22", new BaseClassDicEntity() { Id = "22", Name = "报告初审后处理标记", Code = "BGCSHCLBJ", FontColor = "#ffffff", BGColor = "", Memo = "报告初审后处理标记" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告终审后处理标记 = new KeyValuePair<string, BaseClassDicEntity>("23", new BaseClassDicEntity() { Id = "23", Name = "报告终审后处理标记", Code = "BGZSHCLBJ", FontColor = "#ffffff", BGColor = "", Memo = "报告终审后处理标记" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告反审后处理标记 = new KeyValuePair<string, BaseClassDicEntity>("24", new BaseClassDicEntity() { Id = "24", Name = "报告反审后处理标记", Code = "BGFSHCLBJ", FontColor = "#ffffff", BGColor = "", Memo = "报告反审后处理标记" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本合并 = new KeyValuePair<string, BaseClassDicEntity>("25", new BaseClassDicEntity() { Id = "25", Name = "样本合并", Code = "YBHB", FontColor = "#ffffff", BGColor = "", Memo = "样本合并" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验单生成.Key, 检验单生成.Value },
                { 双向发送.Key, 双向发送.Value },
                { 上机检验.Key, 上机检验.Value },
                { 仪器检验出结果.Key, 仪器检验出结果.Value },
                { 检验完成.Key, 检验完成.Value },
                { 系统判定失败.Key, 系统判定失败.Value },
                { 系统判定成功.Key, 系统判定成功.Value },
                { 样本组内签收.Key, 样本组内签收.Value },
                { 样本拒收.Key, 样本拒收.Value },
                { 样本让步.Key, 样本让步.Value },
                { 样本核收.Key, 样本核收.Value },
                { 样本分发.Key, 样本分发.Value },
                { 初审.Key, 初审.Value },
                { 终审.Key, 终审.Value },
                { 反审.Key, 反审.Value },
                { 复检.Key, 复检.Value },
                { 医生审核.Key, 医生审核.Value },
                { 检验超时.Key, 检验超时.Value },
                { 审核超时.Key, 审核超时.Value },
                { 系统自动初审.Key, 系统自动初审.Value },
                { 系统自动终审.Key, 系统自动终审.Value },
                { 报告初审后处理标记.Key, 报告初审后处理标记.Value },
                { 报告终审后处理标记.Key, 报告终审后处理标记.Value },
                { 报告反审后处理标记.Key, 报告反审后处理标记.Value },
                { 样本合并.Key, 样本合并.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验单项目过程状态
    /// </summary>
    public static class TestItemStatusID
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验单生成 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "检验单生成", Code = "JYDSC", FontColor = "#ffffff", BGColor = "", Memo = "检验单生成" });
        public static KeyValuePair<string, BaseClassDicEntity> 双向发送 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "双向发送", Code = "SXFS", FontColor = "#ffffff", BGColor = "", Memo = "双向发送" });
        public static KeyValuePair<string, BaseClassDicEntity> 上机检验 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "上机检验", Code = "SJJY", FontColor = "#ffffff", BGColor = "", Memo = "上机检验" });
        public static KeyValuePair<string, BaseClassDicEntity> 仪器检验出结果 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "仪器检验出结果", Code = "YQJYCJG", FontColor = "#ffffff", BGColor = "", Memo = "仪器检验出结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 结果人工编辑 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "结果人工编辑", Code = "JYWC", FontColor = "#ffffff", BGColor = "", Memo = "结果人工编辑" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目信息调整 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "项目信息调整", Code = "XTPDSB", FontColor = "#ffffff", BGColor = "", Memo = "项目信息调整" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本核收 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "样本核收", Code = "YBHS", FontColor = "#ffffff", BGColor = "", Memo = "样本核收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "样本分发", Code = "YBFF", FontColor = "#ffffff", BGColor = "", Memo = "样本分发" });
        public static KeyValuePair<string, BaseClassDicEntity> 初审 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "初审", Code = "CS", FontColor = "#ffffff", BGColor = "", Memo = "初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 终审 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "终审", Code = "ZS", FontColor = "#ffffff", BGColor = "", Memo = "终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 反审 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "反审", Code = "FS", FontColor = "#ffffff", BGColor = "", Memo = "反审" });
        public static KeyValuePair<string, BaseClassDicEntity> 复检 = new KeyValuePair<string, BaseClassDicEntity>("16", new BaseClassDicEntity() { Id = "16", Name = "复检", Code = "FJ", FontColor = "#ffffff", BGColor = "", Memo = "复检" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验单生成.Key, 检验单生成.Value },
                { 双向发送.Key, 双向发送.Value },
                { 上机检验.Key, 上机检验.Value },
                { 仪器检验出结果.Key, 仪器检验出结果.Value },
                { 结果人工编辑.Key, 结果人工编辑.Value },
                { 项目信息调整.Key, 项目信息调整.Value },
                { 样本核收.Key, 样本核收.Value },
                { 样本分发.Key, 样本分发.Value },
                { 初审.Key, 初审.Value },
                { 终审.Key, 终审.Value },
                { 反审.Key, 反审.Value },
                { 复检.Key, 复检.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 结果状态与报告状态
    /// </summary>
    public static class TestFormReportStatusID
    {
        public static KeyValuePair<string, BaseClassDicEntity> 阳性标本 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "阳性标本", Code = "YXBB", FontColor = "#ffffff", BGColor = "", Memo = "阳性标本" });
        public static KeyValuePair<string, BaseClassDicEntity> 危急值标本 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "危急值标本", Code = "WJZBB", FontColor = "#ffffff", BGColor = "", Memo = "危急值标本" });
        public static KeyValuePair<string, BaseClassDicEntity> 结果异常 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "结果异常", Code = "JGYC", FontColor = "#ffffff", BGColor = "", Memo = "结果异常" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告发布 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "报告发布", Code = "BGFB", FontColor = "#ffffff", BGColor = "", Memo = "报告发布" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告迁移 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "报告迁移", Code = "BGQY", FontColor = "#ffffff", BGColor = "", Memo = "报告迁移" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告上传 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "报告上传", Code = "BGSC", FontColor = "#ffffff", BGColor = "", Memo = "报告上传" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告打印_技师站 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "报告打印_技师站", Code = "BGDYJSZ", FontColor = "#ffffff", BGColor = "", Memo = "报告打印_技师站" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告打印_自助打印站 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "报告打印_自助打印站", Code = "BGDYZZDYZ", FontColor = "#ffffff", BGColor = "", Memo = "报告打印_自助打印站" });
        public static KeyValuePair<string, BaseClassDicEntity> 中间报告 = new KeyValuePair<string, BaseClassDicEntity>("17", new BaseClassDicEntity() { Id = "17", Name = "中间报告", Code = "ZJBG", FontColor = "#ffffff", BGColor = "", Memo = "中间报告" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 阳性标本.Key, 阳性标本.Value },
                { 危急值标本.Key, 危急值标本.Value },
                { 结果异常.Key, 结果异常.Value },
                { 报告发布.Key, 报告发布.Value },
                { 报告迁移.Key, 报告迁移.Value },
                { 报告上传.Key, 报告上传.Value },
                { 报告打印_技师站.Key, 报告打印_技师站.Value },
                { 报告打印_自助打印站.Key, 报告打印_自助打印站.Value },
                { 中间报告.Key, 中间报告.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验单操作类型
    /// </summary>
    public static class TestFormOperateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验单生成 = new KeyValuePair<string, BaseClassDicEntity>("10001", new BaseClassDicEntity() { Id = "10001", Name = "检验单生成", Code = "JYDSC", FontColor = "#ffffff", BGColor = "", Memo = "检验单-生成" });
        public static KeyValuePair<string, BaseClassDicEntity> 双向发送 = new KeyValuePair<string, BaseClassDicEntity>("10002", new BaseClassDicEntity() { Id = "10002", Name = "双向发送", Code = "SXFS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-双向发送" });
        public static KeyValuePair<string, BaseClassDicEntity> 上机检验 = new KeyValuePair<string, BaseClassDicEntity>("10003", new BaseClassDicEntity() { Id = "10003", Name = "上机检验", Code = "SJJY", FontColor = "#ffffff", BGColor = "", Memo = "检验单-上机检验" });
        public static KeyValuePair<string, BaseClassDicEntity> 仪器检验出结果 = new KeyValuePair<string, BaseClassDicEntity>("10004", new BaseClassDicEntity() { Id = "10004", Name = "仪器检验出结果", Code = "YQJYCJG", FontColor = "#ffffff", BGColor = "", Memo = "检验单-仪器检验出结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验完成 = new KeyValuePair<string, BaseClassDicEntity>("10005", new BaseClassDicEntity() { Id = "10005", Name = "检验完成", Code = "JYWC", FontColor = "#ffffff", BGColor = "", Memo = "检验单-检验完成" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统判定失败 = new KeyValuePair<string, BaseClassDicEntity>("10006", new BaseClassDicEntity() { Id = "10006", Name = "系统判定失败", Code = "XTPDSB", FontColor = "#ffffff", BGColor = "", Memo = "检验单-系统判定失败" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统判定成功 = new KeyValuePair<string, BaseClassDicEntity>("10007", new BaseClassDicEntity() { Id = "10007", Name = "系统判定成功", Code = "XTPDCG", FontColor = "#ffffff", BGColor = "", Memo = "检验单-系统判定成功" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本组内签收 = new KeyValuePair<string, BaseClassDicEntity>("10008", new BaseClassDicEntity() { Id = "10008", Name = "样本组内签收", Code = "YBZNQS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-样本组内签收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本拒收 = new KeyValuePair<string, BaseClassDicEntity>("10009", new BaseClassDicEntity() { Id = "10009", Name = "样本拒收", Code = "YBJS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-样本拒收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本让步 = new KeyValuePair<string, BaseClassDicEntity>("10010", new BaseClassDicEntity() { Id = "10010", Name = "样本让步", Code = "YBRB", FontColor = "#ffffff", BGColor = "", Memo = "检验单-样本让步" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本核收 = new KeyValuePair<string, BaseClassDicEntity>("10011", new BaseClassDicEntity() { Id = "10011", Name = "样本核收", Code = "YBHS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-样本核收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发 = new KeyValuePair<string, BaseClassDicEntity>("10012", new BaseClassDicEntity() { Id = "10012", Name = "样本分发", Code = "YBFF", FontColor = "#ffffff", BGColor = "", Memo = "检验单-样本分发" });
        public static KeyValuePair<string, BaseClassDicEntity> 初审 = new KeyValuePair<string, BaseClassDicEntity>("10013", new BaseClassDicEntity() { Id = "10013", Name = "初审", Code = "CS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 终审 = new KeyValuePair<string, BaseClassDicEntity>("10014", new BaseClassDicEntity() { Id = "10014", Name = "终审", Code = "ZS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 反审 = new KeyValuePair<string, BaseClassDicEntity>("10015", new BaseClassDicEntity() { Id = "10015", Name = "反审", Code = "FS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-反审" });
        public static KeyValuePair<string, BaseClassDicEntity> 复检 = new KeyValuePair<string, BaseClassDicEntity>("10016", new BaseClassDicEntity() { Id = "10016", Name = "复检", Code = "FJ", FontColor = "#ffffff", BGColor = "", Memo = "检验单-复检" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消复检 = new KeyValuePair<string, BaseClassDicEntity>("10100", new BaseClassDicEntity() { Id = "10100", Name = "取消复检", Code = "QXFJ", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-取消复检" });

        public static KeyValuePair<string, BaseClassDicEntity> 医生审核 = new KeyValuePair<string, BaseClassDicEntity>("10017", new BaseClassDicEntity() { Id = "10017", Name = "医生审核", Code = "YSSH", FontColor = "#ffffff", BGColor = "", Memo = "检验单-医生审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验超时 = new KeyValuePair<string, BaseClassDicEntity>("10018", new BaseClassDicEntity() { Id = "10018", Name = "检验超时", Code = "JYCS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-检验超时" });
        public static KeyValuePair<string, BaseClassDicEntity> 审核超时 = new KeyValuePair<string, BaseClassDicEntity>("10019", new BaseClassDicEntity() { Id = "10019", Name = "审核超时", Code = "SHCS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-审核超时" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统自动初审 = new KeyValuePair<string, BaseClassDicEntity>("10020", new BaseClassDicEntity() { Id = "10020", Name = "系统自动初审", Code = "XTZDCS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-系统自动初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 系统自动终审 = new KeyValuePair<string, BaseClassDicEntity>("10021", new BaseClassDicEntity() { Id = "10021", Name = "系统自动终审", Code = "XTZDZS", FontColor = "#ffffff", BGColor = "", Memo = "检验单-系统自动终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告初审后处理标记 = new KeyValuePair<string, BaseClassDicEntity>("10022", new BaseClassDicEntity() { Id = "10022", Name = "报告初审后处理标记", Code = "BGCSHCLBJ", FontColor = "#ffffff", BGColor = "", Memo = "检验单-报告初审后处理标记" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告终审后处理标记 = new KeyValuePair<string, BaseClassDicEntity>("10023", new BaseClassDicEntity() { Id = "10023", Name = "报告终审后处理标记", Code = "BGZSHCLBJ", FontColor = "#ffffff", BGColor = "", Memo = "检验单-报告终审后处理标记" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告反审后处理标记 = new KeyValuePair<string, BaseClassDicEntity>("10024", new BaseClassDicEntity() { Id = "10024", Name = "报告反审后处理标记", Code = "BGFSHCLBJ", FontColor = "#ffffff", BGColor = "", Memo = "检验单-报告反审后处理标记" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本合并 = new KeyValuePair<string, BaseClassDicEntity>("10025", new BaseClassDicEntity() { Id = "10025", Name = "样本合并", Code = "YBHB", FontColor = "#ffffff", BGColor = "", Memo = "检验单-样本合并" });
        public static KeyValuePair<string, BaseClassDicEntity> 删除 = new KeyValuePair<string, BaseClassDicEntity>("10200", new BaseClassDicEntity() { Id = "10200", Name = "删除", Code = "SC", FontColor = "#ffffff", BGColor = "", Memo = "检验单-检验中删除" });
        public static KeyValuePair<string, BaseClassDicEntity> 删除恢复 = new KeyValuePair<string, BaseClassDicEntity>("10201", new BaseClassDicEntity() { Id = "10201", Name = "删除恢复", Code = "SCHF", FontColor = "#ffffff", BGColor = "", Memo = "检验单-检验中删除恢复" });
        public static KeyValuePair<string, BaseClassDicEntity> 危急值发送 = new KeyValuePair<string, BaseClassDicEntity>("11990", new BaseClassDicEntity() { Id = "11990", Name = "危急值发送", Code = "WJZFS", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-危急值发送" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验单生成.Key, 检验单生成.Value },
                { 双向发送.Key, 双向发送.Value },
                { 上机检验.Key, 上机检验.Value },
                { 仪器检验出结果.Key, 仪器检验出结果.Value },
                { 检验完成.Key, 检验完成.Value },
                { 系统判定失败.Key, 系统判定失败.Value },
                { 系统判定成功.Key, 系统判定成功.Value },
                { 样本组内签收.Key, 样本组内签收.Value },
                { 样本拒收.Key, 样本拒收.Value },
                { 样本让步.Key, 样本让步.Value },
                { 样本核收.Key, 样本核收.Value },
                { 样本分发.Key, 样本分发.Value },
                { 初审.Key, 初审.Value },
                { 终审.Key, 终审.Value },
                { 反审.Key, 反审.Value },
                { 复检.Key, 复检.Value },
                { 医生审核.Key, 医生审核.Value },
                { 检验超时.Key, 检验超时.Value },
                { 审核超时.Key, 审核超时.Value },
                { 系统自动初审.Key, 系统自动初审.Value },
                { 系统自动终审.Key, 系统自动终审.Value },
                { 报告初审后处理标记.Key, 报告初审后处理标记.Value },
                { 报告终审后处理标记.Key, 报告终审后处理标记.Value },
                { 报告反审后处理标记.Key, 报告反审后处理标记.Value },
                { 样本合并.Key, 样本合并.Value },
                { 删除.Key, 删除.Value },
                { 删除恢复.Key, 删除恢复.Value },
                { 危急值发送.Key, 危急值发送.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验单项目操作类型
    /// </summary>
    public static class TestItemOperateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验项目生成 = new KeyValuePair<string, BaseClassDicEntity>("11001", new BaseClassDicEntity() { Id = "11001", Name = "检验项目生成", Code = "JYXMSC", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-生成" });
        public static KeyValuePair<string, BaseClassDicEntity> 双向发送 = new KeyValuePair<string, BaseClassDicEntity>("11002", new BaseClassDicEntity() { Id = "11002", Name = "双向发送", Code = "SXFS", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-双向发送" });
        public static KeyValuePair<string, BaseClassDicEntity> 上机检验 = new KeyValuePair<string, BaseClassDicEntity>("11003", new BaseClassDicEntity() { Id = "11003", Name = "上机检验", Code = "SJJY", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-上机检验" });
        public static KeyValuePair<string, BaseClassDicEntity> 仪器检验出结果 = new KeyValuePair<string, BaseClassDicEntity>("11004", new BaseClassDicEntity() { Id = "11004", Name = "仪器检验出结果", Code = "YQJYCJG", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-仪器检验出结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 结果人工编辑 = new KeyValuePair<string, BaseClassDicEntity>("11005", new BaseClassDicEntity() { Id = "11005", Name = "结果人工编辑", Code = "JYWC", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-结果人工编辑" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目信息调整 = new KeyValuePair<string, BaseClassDicEntity>("11006", new BaseClassDicEntity() { Id = "11006", Name = "项目信息调整", Code = "XTPDSB", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-信息调整" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本核收 = new KeyValuePair<string, BaseClassDicEntity>("11011", new BaseClassDicEntity() { Id = "11011", Name = "样本核收", Code = "YBHS", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-样本核收" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发 = new KeyValuePair<string, BaseClassDicEntity>("11012", new BaseClassDicEntity() { Id = "11012", Name = "样本分发", Code = "YBFF", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-样本分发" });
        public static KeyValuePair<string, BaseClassDicEntity> 初审 = new KeyValuePair<string, BaseClassDicEntity>("11013", new BaseClassDicEntity() { Id = "11013", Name = "初审", Code = "CS", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-初审" });
        public static KeyValuePair<string, BaseClassDicEntity> 终审 = new KeyValuePair<string, BaseClassDicEntity>("11014", new BaseClassDicEntity() { Id = "11014", Name = "终审", Code = "ZS", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-终审" });
        public static KeyValuePair<string, BaseClassDicEntity> 反审 = new KeyValuePair<string, BaseClassDicEntity>("11015", new BaseClassDicEntity() { Id = "11015", Name = "反审", Code = "FS", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-反审" });
        public static KeyValuePair<string, BaseClassDicEntity> 复检 = new KeyValuePair<string, BaseClassDicEntity>("11016", new BaseClassDicEntity() { Id = "11016", Name = "复检", Code = "FJ", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-复检" });
        public static KeyValuePair<string, BaseClassDicEntity> 取消复检 = new KeyValuePair<string, BaseClassDicEntity>("11017", new BaseClassDicEntity() { Id = "11017", Name = "取消复检", Code = "QXFJ", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-取消复检" });

        public static KeyValuePair<string, BaseClassDicEntity> 删除 = new KeyValuePair<string, BaseClassDicEntity>("11200", new BaseClassDicEntity() { Id = "11200", Name = "删除", Code = "SC", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-检验中删除" });
        public static KeyValuePair<string, BaseClassDicEntity> 删除恢复 = new KeyValuePair<string, BaseClassDicEntity>("11201", new BaseClassDicEntity() { Id = "11201", Name = "删除恢复", Code = "SCHF", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-检验中删除恢复" });

        public static KeyValuePair<string, BaseClassDicEntity> 危急值发送 = new KeyValuePair<string, BaseClassDicEntity>("11990", new BaseClassDicEntity() { Id = "11990", Name = "危急值发送", Code = "WJZFS", FontColor = "#ffffff", BGColor = "", Memo = "检验项目-危急值发送" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验项目生成.Key, 检验项目生成.Value },
                { 双向发送.Key, 双向发送.Value },
                { 上机检验.Key, 上机检验.Value },
                { 仪器检验出结果.Key, 仪器检验出结果.Value },
                { 结果人工编辑.Key, 结果人工编辑.Value },
                { 项目信息调整.Key, 项目信息调整.Value },
                { 样本核收.Key, 样本核收.Value },
                { 样本分发.Key, 样本分发.Value },
                { 初审.Key, 初审.Value },
                { 终审.Key, 终审.Value },
                { 反审.Key, 反审.Value },
                { 复检.Key, 复检.Value },
                { 删除.Key, 删除.Value },
                { 删除恢复.Key, 删除恢复.Value },
                { 危急值发送.Key, 危急值发送.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 结果状态与报告状态操作记录
    /// </summary>
    public static class TestFormReportStatusOperateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 阳性标本 = new KeyValuePair<string, BaseClassDicEntity>("10101", new BaseClassDicEntity() { Id = "10101", Name = "阳性标本", Code = "YXBB", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-阳性标本" });
        public static KeyValuePair<string, BaseClassDicEntity> 危急值标本 = new KeyValuePair<string, BaseClassDicEntity>("10102", new BaseClassDicEntity() { Id = "10102", Name = "危急值标本", Code = "WJZBB", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-危急值标本" });
        public static KeyValuePair<string, BaseClassDicEntity> 结果异常 = new KeyValuePair<string, BaseClassDicEntity>("10103", new BaseClassDicEntity() { Id = "10103", Name = "结果异常", Code = "JGYC", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-结果异常" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告发布 = new KeyValuePair<string, BaseClassDicEntity>("10110", new BaseClassDicEntity() { Id = "10110", Name = "报告发布", Code = "BGFB", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-报告发布" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告迁移 = new KeyValuePair<string, BaseClassDicEntity>("10111", new BaseClassDicEntity() { Id = "10111", Name = "报告迁移", Code = "BGQY", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-报告迁移" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告上传 = new KeyValuePair<string, BaseClassDicEntity>("10112", new BaseClassDicEntity() { Id = "10112", Name = "报告上传", Code = "BGSC", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-报告上传" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告打印_技师站 = new KeyValuePair<string, BaseClassDicEntity>("10113", new BaseClassDicEntity() { Id = "10113", Name = "报告打印_技师站", Code = "BGDYJSZ", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-报告打印-技师站" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告打印_自助打印站 = new KeyValuePair<string, BaseClassDicEntity>("10114", new BaseClassDicEntity() { Id = "10114", Name = "报告打印_自助打印站", Code = "BGDYZZDYZ", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-报告打印-自助打印站" });
        public static KeyValuePair<string, BaseClassDicEntity> 中间报告 = new KeyValuePair<string, BaseClassDicEntity>("10117", new BaseClassDicEntity() { Id = "10117", Name = "中间报告", Code = "ZJBG", FontColor = "#ffffff", BGColor = "", Memo = "结果报告状态-中间报告" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 阳性标本.Key, 阳性标本.Value },
                { 危急值标本.Key, 危急值标本.Value },
                { 结果异常.Key, 结果异常.Value },
                { 报告发布.Key, 报告发布.Value },
                { 报告迁移.Key, 报告迁移.Value },
                { 报告上传.Key, 报告上传.Value },
                { 报告打印_技师站.Key, 报告打印_技师站.Value },
                { 报告打印_自助打印站.Key, 报告打印_自助打印站.Value },
                { 中间报告.Key, 中间报告.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验单结果警示级别
    /// </summary>
    public static class TestFormReportValueAlarmLevel
    {
        public static KeyValuePair<string, BaseClassDicEntity> 正常 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "正常", Code = "ZC", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 警示 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "警示", Code = "JS", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 警告 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "警告", Code = "JG", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 严重警告 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "严重警告", Code = "YZJG", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 危急 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "危急", Code = "WJ", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 错误 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "错误", Code = "CW", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 正常.Key, 正常.Value },
                { 警示.Key, 警示.Value },
                { 警告.Key, 警告.Value },
                { 严重警告.Key, 严重警告.Value },
                { 危急.Key, 危急.Value },
                { 错误.Key, 错误.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 授权类型
    /// </summary>
    public static class AuthorizeType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 临时 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "临时", Code = "LS", FontColor = "#ffffff", BGColor = "", Memo = "临时(一次性)" });
        public static KeyValuePair<string, BaseClassDicEntity> 周期 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "周期", Code = "LS", FontColor = "#ffffff", BGColor = "", Memo = "周期(多次)" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 临时.Key, 临时.Value },
                { 周期.Key, 周期.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 授权操作类型
    /// </summary>
    public static class AuthorizeOperateType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 审核 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "审核", Code = "SQSH", FontColor = "#ffffff", BGColor = "", Memo = "审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验确认 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "检验确认", Code = "SQJYQR", FontColor = "#ffffff", BGColor = "", Memo = "检验确认" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 审核.Key, 审核.Value },
                { 检验确认.Key, 检验确认.Value }
            };
            return dic;
        }
    }

}
