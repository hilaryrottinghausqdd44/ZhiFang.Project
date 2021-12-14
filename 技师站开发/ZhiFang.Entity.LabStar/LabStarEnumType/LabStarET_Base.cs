using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar
{
    //此文件定义系统基础枚举类
    public class BaseClassDicEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        ////类别编码，用于枚举细项分类
        //public string TypeCode { get; set; }

        public string FontColor { get; set; }

        //背景色
        public string BGColor { get; set; }

        public string SName { get; set; }

        public string DefaultValue { get; set; }

        public string Memo { get; set; }
    }


    /// <summary>
    /// 小组专业功能类型
    /// </summary>
    public static class SectionFunType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 通用 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "通用", Code = "TY", FontColor = "#ffffff", BGColor = "", Memo = "通用（生免等）" });
        public static KeyValuePair<string, BaseClassDicEntity> 微生物 = new KeyValuePair<string, BaseClassDicEntity>("20", new BaseClassDicEntity() { Id = "20", Name = "微生物", Code = "WSW", FontColor = "#ffffff", BGColor = "", Memo = "微生物" });
        public static KeyValuePair<string, BaseClassDicEntity> 细胞学 = new KeyValuePair<string, BaseClassDicEntity>("30", new BaseClassDicEntity() { Id = "30", Name = "细胞学", Code = "XBX", FontColor = "#ffffff", BGColor = "", Memo = "细胞学" });
        public static KeyValuePair<string, BaseClassDicEntity> 病理 = new KeyValuePair<string, BaseClassDicEntity>("40", new BaseClassDicEntity() { Id = "40", Name = "病理", Code = "BL", FontColor = "#ffffff", BGColor = "", Memo = "病理" });
        public static KeyValuePair<string, BaseClassDicEntity> 酶免 = new KeyValuePair<string, BaseClassDicEntity>("50", new BaseClassDicEntity() { Id = "50", Name = "酶免", Code = "MM", FontColor = "#ffffff", BGColor = "", Memo = "酶免" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 通用.Key, 通用.Value },
                { 微生物.Key, 微生物.Value },
                { 细胞学.Key, 细胞学.Value },
                { 病理.Key, 病理.Value },
                { 酶免.Key, 酶免.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 细胞学专业子类型
    /// </summary>
    public static class CytologyChildType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 骨髓细胞形态学 = new KeyValuePair<string, BaseClassDicEntity>("31", new BaseClassDicEntity() { Id = "31", Name = "骨髓细胞形态学", Code = "GSXBXTX", FontColor = "#ffffff", BGColor = "", Memo = "骨髓细胞形态学" });
        public static KeyValuePair<string, BaseClassDicEntity> 干细胞形态学 = new KeyValuePair<string, BaseClassDicEntity>("32", new BaseClassDicEntity() { Id = "32", Name = "干细胞形态学", Code = "GXBXT", FontColor = "#ffffff", BGColor = "", Memo = "干细胞形态学" });
        public static KeyValuePair<string, BaseClassDicEntity> 外周血细胞形态 = new KeyValuePair<string, BaseClassDicEntity>("33", new BaseClassDicEntity() { Id = "33", Name = "外周血细胞形态", Code = "WZXXBXT", FontColor = "#ffffff", BGColor = "", Memo = "外周血细胞形态" });
        public static KeyValuePair<string, BaseClassDicEntity> 腹水形态学 = new KeyValuePair<string, BaseClassDicEntity>("35", new BaseClassDicEntity() { Id = "34", Name = "腹水形态学", Code = "FSXTX", FontColor = "#ffffff", BGColor = "", Memo = "腹水形态学" });
        public static KeyValuePair<string, BaseClassDicEntity> 脑脊液形态学 = new KeyValuePair<string, BaseClassDicEntity>("35", new BaseClassDicEntity() { Id = "35", Name = "脑脊液形态学", Code = "NJYXTX", FontColor = "#ffffff", BGColor = "", Memo = "脑脊液形态学" });
        public static KeyValuePair<string, BaseClassDicEntity> 外周血红细胞形态 = new KeyValuePair<string, BaseClassDicEntity>("36", new BaseClassDicEntity() { Id = "36", Name = "外周血红细胞形态", Code = "WZXHXBXT", FontColor = "#ffffff", BGColor = "", Memo = "外周血红细胞形态" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 骨髓细胞形态学.Key, 骨髓细胞形态学.Value },
                { 干细胞形态学.Key, 干细胞形态学.Value },
                { 外周血细胞形态.Key, 外周血细胞形态.Value },
                { 腹水形态学.Key, 腹水形态学.Value },
                { 脑脊液形态学.Key, 脑脊液形态学.Value },
                { 外周血红细胞形态.Key, 外周血红细胞形态.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 小组类型
    /// </summary>
    public static class SectionType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 通用 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "通用", Code = "Common", FontColor = "#ffffff", BGColor = "", Memo = "通用" });
        public static KeyValuePair<string, BaseClassDicEntity> 生化 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "生化", Code = "Normal", FontColor = "#ffffff", BGColor = "", Memo = "生化" });
        public static KeyValuePair<string, BaseClassDicEntity> 微生物 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "微生物", Code = "Micro", FontColor = "#ffffff", BGColor = "", Memo = "微生物" });
        public static KeyValuePair<string, BaseClassDicEntity> 生化类_图 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "生化类(图)", Code = "NormalIncImage", FontColor = "#ffffff", BGColor = "", Memo = "生化类(图)" });
        public static KeyValuePair<string, BaseClassDicEntity> 微生物_图 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "微生物(图)", Code = "MicroIncImage", FontColor = "#ffffff", BGColor = "", Memo = "微生物(图)" });
        public static KeyValuePair<string, BaseClassDicEntity> 细胞形态学 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "细胞形态学", Code = "CellMorphology", FontColor = "#ffffff", BGColor = "", Memo = "细胞形态学" });
        public static KeyValuePair<string, BaseClassDicEntity> Fish检测 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "Fish检测", Code = "FishCheck", FontColor = "#ffffff", BGColor = "", Memo = "Fish检测" });
        public static KeyValuePair<string, BaseClassDicEntity> 院感检测 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "院感检测", Code = "SensorCheck", FontColor = "#ffffff", BGColor = "", Memo = "院感检测" });
        public static KeyValuePair<string, BaseClassDicEntity> 染色体检测 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "染色体检测", Code = "ChromosomeCheck", FontColor = "#ffffff", BGColor = "", Memo = "染色体检测" });
        public static KeyValuePair<string, BaseClassDicEntity> 病理检测 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "病理检测", Code = "PathologyCheck", FontColor = "#ffffff", BGColor = "", Memo = "病理检测" });
        public static KeyValuePair<string, BaseClassDicEntity> 临检 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "临检", Code = "Clinical", FontColor = "#ffffff", BGColor = "", Memo = "临检" });
        public static KeyValuePair<string, BaseClassDicEntity> 发光 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "发光", Code = "Luminous", FontColor = "#ffffff", BGColor = "", Memo = "发光" });
        public static KeyValuePair<string, BaseClassDicEntity> 免疫定量 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "免疫定量", Code = "IQuantitative", FontColor = "#ffffff", BGColor = "", Memo = "免疫定量" });
        public static KeyValuePair<string, BaseClassDicEntity> 免疫定性 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "免疫定性", Code = "IQualitative", FontColor = "#ffffff", BGColor = "", Memo = "免疫定性" });
        public static KeyValuePair<string, BaseClassDicEntity> 酶标仪 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "酶标仪", Code = "Eliasa", FontColor = "#ffffff", BGColor = "", Memo = "酶标仪" });
        public static KeyValuePair<string, BaseClassDicEntity> 涂片 = new KeyValuePair<string, BaseClassDicEntity>("21", new BaseClassDicEntity() { Id = "21", Name = "涂片", Code = "Smear", FontColor = "#ffffff", BGColor = "", Memo = "涂片" });
        public static KeyValuePair<string, BaseClassDicEntity> 微生态 = new KeyValuePair<string, BaseClassDicEntity>("22", new BaseClassDicEntity() { Id = "22", Name = "微生态", Code = "Microecology", FontColor = "#ffffff", BGColor = "", Memo = "微生态" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 通用.Key, 通用.Value },
                { 生化.Key, 生化.Value },
                { 微生物.Key, 微生物.Value },
                { 生化类_图.Key, 生化类_图.Value },
                { 微生物_图.Key, 微生物_图.Value },
                { 细胞形态学.Key, 细胞形态学.Value },
                { Fish检测.Key, Fish检测.Value },
                { 院感检测.Key, 院感检测.Value },
                { 染色体检测.Key, 染色体检测.Value },
                { 病理检测.Key, 病理检测.Value },
                { 临检.Key, 临检.Value },
                { 发光.Key, 发光.Value },
                { 免疫定量.Key, 免疫定量.Value },
                { 免疫定性.Key, 免疫定性.Value },
                { 酶标仪.Key, 酶标仪.Value },
                { 涂片.Key, 涂片.Value },
                { 微生态.Key, 微生态.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 小组专业编辑
    /// </summary>
    public static class SectionProDll
    {
        public static KeyValuePair<string, BaseClassDicEntity> 常规 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "常规", Code = "CG", FontColor = "#ffffff", BGColor = "", Memo = "常规）" });
        public static KeyValuePair<string, BaseClassDicEntity> 常规大文本 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "常规+大文本", Code = "CGDWB", FontColor = "#ffffff", BGColor = "", Memo = "常规+大文本" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 常规.Key, 常规.Value },
                { 常规大文本.Key, 常规大文本.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验类型：常规、急诊、质控
    /// </summary>
    public static class TestType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 常规 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "常规", Code = "CG", FontColor = "#ffffff", BGColor = "", Memo = "常规" });
        public static KeyValuePair<string, BaseClassDicEntity> 急诊 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "急诊", Code = "JZ", FontColor = "#ffffff", BGColor = "", Memo = "急诊" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "质控", Code = "ZK", FontColor = "#ffffff", BGColor = "", Memo = "质控" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 常规.Key, 常规.Value },
                { 急诊.Key, 急诊.Value },
                { 质控.Key, 质控.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 短语类别：SamplePhrase, ItemPhrase
    /// </summary>
    public static class PhraseType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 样本短语类别 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "样本短语类别", Code = "SamplePhrase", FontColor = "#ffffff", BGColor = "", Memo = "样本短语类别" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目短语类别 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "项目短语类别", Code = "ItemPhrase", FontColor = "#ffffff", BGColor = "", Memo = "项目短语类别" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 样本短语类别.Key, 样本短语类别.Value },
                { 项目短语类别.Key, 项目短语类别.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 项目短语：项目结果短语，项目结果说明，项目复检原因
    /// </summary>
    public static class ItemPhrase
    {
        public static KeyValuePair<string, BaseClassDicEntity> 项目结果短语 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "项目结果短语", Code = "XMJGDY", FontColor = "#ffffff", BGColor = "", Memo = "项目结果短语" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目结果说明 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "项目结果说明", Code = "XMJGSM", FontColor = "#ffffff", BGColor = "", Memo = "项目结果说明" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目复检原因 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "项目复检原因", Code = "XMFJYY", FontColor = "#ffffff", BGColor = "", Memo = "项目复检原因" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 项目结果短语.Key, 项目结果短语.Value },
                { 项目结果说明.Key, 项目结果说明.Value },
                { 项目复检原因.Key, 项目复检原因.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 组合类型：单项，组合，组套
    /// </summary>
    public static class ItemGroupType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 单项 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "单项", Code = "DX", FontColor = "#ffffff", BGColor = "", Memo = "单项" });
        public static KeyValuePair<string, BaseClassDicEntity> 组合 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "组合", Code = "ZH", FontColor = "#ffffff", BGColor = "", Memo = "组合" });
        public static KeyValuePair<string, BaseClassDicEntity> 组套 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "组套", Code = "ZT", FontColor = "#ffffff", BGColor = "", Memo = "组套" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 单项.Key, 单项.Value },
                { 组合.Key, 组合.Value },
                { 组套.Key, 组套.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 组合特殊属性
    /// </summary>
    public static class ItemSpecProperty
    {
        public static KeyValuePair<string, BaseClassDicEntity> 常规项目 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "常规项目", Code = "CGXM", FontColor = "#ffffff", BGColor = "", Memo = "常规项目" });
        public static KeyValuePair<string, BaseClassDicEntity> 大文本 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "大文本", Code = "DWB", FontColor = "#ffffff", BGColor = "", Memo = "大文本" });
        public static KeyValuePair<string, BaseClassDicEntity> 仅大文本 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "仅大文本", Code = "JDWB", FontColor = "#ffffff", BGColor = "", Memo = "仅大文本" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 常规项目.Key, 常规项目.Value },
                { 大文本.Key, 大文本.Value },
                { 仅大文本.Key, 仅大文本.Value }
            };
            return dic;
        }
    }


    /// <summary>
    /// 样本短语：检验备注，检验评语，警告提示，初审说明，终审说明，特殊性状描述
    /// </summary>
    public static class SamplePhrase
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验备注 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "检验备注", Code = "JYBZ", FontColor = "#ffffff", BGColor = "", Memo = "检验备注" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验评语 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "检验评语", Code = "JYPY", FontColor = "#ffffff", BGColor = "", Memo = "检验评语" });
        public static KeyValuePair<string, BaseClassDicEntity> 警告提示 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "警告提示", Code = "JGTS", FontColor = "#ffffff", BGColor = "", Memo = "警告提示" });
        public static KeyValuePair<string, BaseClassDicEntity> 初审说明 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "初审说明", Code = "CSSM", FontColor = "#ffffff", BGColor = "", Memo = "初审说明" });
        public static KeyValuePair<string, BaseClassDicEntity> 终审说明 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "终审说明", Code = "ZSSM", FontColor = "#ffffff", BGColor = "", Memo = "终审说明" });
        public static KeyValuePair<string, BaseClassDicEntity> 特殊性状描述 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "特殊性状描述", Code = "TSXZMS", FontColor = "#ffffff", BGColor = "", Memo = "特殊性状描述" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本备注 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "7", Name = "样本备注", Code = "YBBZ", FontColor = "#ffffff", BGColor = "", Memo = "样本备注" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本描述 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "8", Name = "样本描述", Code = "YBMS", FontColor = "#ffffff", BGColor = "", Memo = "样本描述" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验备注.Key, 检验备注.Value },
                { 检验评语.Key, 检验评语.Value },
                { 警告提示.Key, 警告提示.Value },
                { 初审说明.Key, 初审说明.Value },
                { 终审说明.Key, 终审说明.Value },
                { 特殊性状描述.Key, 特殊性状描述.Value },
                { 样本备注.Key, 样本备注.Value },
                { 样本描述.Key, 样本描述.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 结果类型：定性，定量，描述，图形
    /// </summary>
    public static class ResultValueType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 定性 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "定性", Code = "DX", FontColor = "#ffffff", BGColor = "", Memo = "定性" });
        public static KeyValuePair<string, BaseClassDicEntity> 定量 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "定量", Code = "DL", FontColor = "#ffffff", BGColor = "", Memo = "定量" });
        public static KeyValuePair<string, BaseClassDicEntity> 描述 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "描述", Code = "MS", FontColor = "#ffffff", BGColor = "", Memo = "描述" });
        public static KeyValuePair<string, BaseClassDicEntity> 图形 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "图形", Code = "TX", FontColor = "#ffffff", BGColor = "", Memo = "图形" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 定性.Key, 定性.Value },
                { 定量.Key, 定量.Value },
                { 描述.Key, 描述.Value },
                { 图形.Key, 图形.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 人员身份
    /// </summary>
    public static class EmpSystemType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验技师 = new KeyValuePair<string, BaseClassDicEntity>("1001001", new BaseClassDicEntity() { Id = "1001001", Name = "检验技师", Code = "1001001", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 护士 = new KeyValuePair<string, BaseClassDicEntity>("1001002", new BaseClassDicEntity() { Id = "1001002", Name = "护士", Code = "1001002", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 医生 = new KeyValuePair<string, BaseClassDicEntity>("1001003", new BaseClassDicEntity() { Id = "1001003", Name = "医生", Code = "1001003", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 护工 = new KeyValuePair<string, BaseClassDicEntity>("1001004", new BaseClassDicEntity() { Id = "1001004", Name = "护工", Code = "1001004", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 医务科人员 = new KeyValuePair<string, BaseClassDicEntity>("1001005", new BaseClassDicEntity() { Id = "1001005", Name = "医务科人员", Code = "1001005", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 财务人员 = new KeyValuePair<string, BaseClassDicEntity>("1001006", new BaseClassDicEntity() { Id = "1001006", Name = "财务人员", Code = "1001006", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验技师.Key, 检验技师.Value },
                { 护士.Key, 护士.Value },
                { 医生.Key, 医生.Value },
                { 护工.Key, 护工.Value },
                { 医务科人员.Key, 医务科人员.Value },
                { 财务人员.Key, 财务人员.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 部门身份
    /// </summary>
    public static class DeptSystemType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 送检科室 = new KeyValuePair<string, BaseClassDicEntity>("1001101", new BaseClassDicEntity() { Id = "1001101", Name = "送检科室", Code = "1001101", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 病区 = new KeyValuePair<string, BaseClassDicEntity>("1001102", new BaseClassDicEntity() { Id = "1001102", Name = "病区", Code = "1001102", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 病房 = new KeyValuePair<string, BaseClassDicEntity>("1001103", new BaseClassDicEntity() { Id = "1001103", Name = "病房", Code = "1001103", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 执行科室 = new KeyValuePair<string, BaseClassDicEntity>("1001104", new BaseClassDicEntity() { Id = "1001104", Name = "执行科室", Code = "1001104", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验科室 = new KeyValuePair<string, BaseClassDicEntity>("1001105", new BaseClassDicEntity() { Id = "1001105", Name = "检验科室", Code = "1001105", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 送检客户 = new KeyValuePair<string, BaseClassDicEntity>("1001106", new BaseClassDicEntity() { Id = "1001106", Name = "送检客户", Code = "1001106", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验专业大组 = new KeyValuePair<string, BaseClassDicEntity>("1001107", new BaseClassDicEntity() { Id = "1001107", Name = "检验专业大组", Code = "1001107", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 院区 = new KeyValuePair<string, BaseClassDicEntity>("1001108", new BaseClassDicEntity() { Id = "1001108", Name = "院区", Code = "1001108", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 送检科室.Key, 送检科室.Value },
                { 病区.Key, 病区.Value },
                { 病房.Key, 病房.Value },
                { 执行科室.Key, 执行科室.Value },
                { 检验科室.Key, 检验科室.Value },
                { 送检客户.Key, 送检客户.Value },
                { 检验专业大组.Key, 检验专业大组.Value },
                { 院区.Key, 院区.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 性别
    /// </summary>
    public static class GenderType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 男 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "男", Code = "M", FontColor = "#ffffff", BGColor = "", Memo = "男性" });
        public static KeyValuePair<string, BaseClassDicEntity> 女 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "女", Code = "F", FontColor = "#ffffff", BGColor = "", Memo = "女性" });
        public static KeyValuePair<string, BaseClassDicEntity> 女转男 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "女转男", Code = "FTM", FontColor = "#ffffff", BGColor = "", Memo = "女性改（变）为男性" });
        public static KeyValuePair<string, BaseClassDicEntity> 男转女 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "男转女", Code = "MTF", FontColor = "#ffffff", BGColor = "", Memo = "男性改（变）为女性" });
        public static KeyValuePair<string, BaseClassDicEntity> 未知 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "未知", Code = "U", FontColor = "#ffffff", BGColor = "", Memo = "未知的性别" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {

                { 男.Key, 男.Value },
                { 女.Key, 女.Value },
                { 女转男.Key, 女转男.Value },
                { 男转女.Key, 男转女.Value },
                { 未知.Key, 未知.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 年龄单位
    /// </summary>
    public static class AgeUnitType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 岁 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "岁", Code = "Y", FontColor = "#ffffff", BGColor = "", Memo = "岁" });
        public static KeyValuePair<string, BaseClassDicEntity> 月 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "月", Code = "M", FontColor = "#ffffff", BGColor = "", Memo = "月" });
        public static KeyValuePair<string, BaseClassDicEntity> 天 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "天", Code = "D", FontColor = "#ffffff", BGColor = "", Memo = "日" });
        public static KeyValuePair<string, BaseClassDicEntity> 小时 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "小时", Code = "H", FontColor = "#ffffff", BGColor = "", Memo = "小时" });
        public static KeyValuePair<string, BaseClassDicEntity> 分钟 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "分钟", Code = "N", FontColor = "#ffffff", BGColor = "", Memo = "分钟" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 岁.Key, 岁.Value },
                { 月.Key, 月.Value },
                { 天.Key, 天.Value },
                { 小时.Key, 小时.Value },
                { 分钟.Key, 分钟.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 检验项目超时类型
    /// </summary>
    public static class ItemOverTimeType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 采样签收 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "采样签收", Code = "CYQS", FontColor = "#ffffff", BGColor = "", Memo = "采样签收" });
        public static KeyValuePair<string, BaseClassDicEntity> 签收审核 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "签收审核", Code = "QSSH", FontColor = "#ffffff", BGColor = "", Memo = "签收审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 采样审核 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "采样审核", Code = "CYSH", FontColor = "#ffffff", BGColor = "", Memo = "采样审核" });
        public static KeyValuePair<string, BaseClassDicEntity> 采样检验 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "采样检验", Code = "CYJY", FontColor = "#ffffff", BGColor = "", Memo = "采样检验" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 采样签收.Key, 采样签收.Value },
                { 签收审核.Key, 签收审核.Value },
                { 采样审核.Key, 采样审核.Value },
                { 采样检验.Key, 采样检验.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 基础数据类型
    /// </summary>
    public static class BaseDataType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 结果状态 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "结果状态", Code = "ResultStatus", FontColor = "#ffffff", BGColor = "", Memo = "结果状态" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 结果状态.Key, 结果状态.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 对照字典类型
    /// </summary>
    public static class ContrastDicType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 人员 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "人员", Code = "HR_Employee", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 部门 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部门", Code = "HR_Dept", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本类型 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "样本类型", Code = "LB_SampleType", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 民族 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "民族", Code = "LB_Folk", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 性别 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "性别", Code = "GenderType", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 年龄单位 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "年龄单位", Code = "AgeUnitType", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 诊断类型 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "诊断类型", Code = "LB_Diag", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 人员.Key, 人员.Value },
                { 部门.Key, 部门.Value },
                { 样本类型.Key, 样本类型.Value },
                { 民族.Key, 民族.Value },
                { 性别.Key, 性别.Value },
                { 年龄单位.Key, 年龄单位.Value },
                { 诊断类型.Key, 诊断类型.Value },
            };
            return dic;
        }
    }

    /// <summary>
    /// 打印模板_业务类型
    /// </summary>
    public static class PrintModelBusinessType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 常规检验 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "常规检验", Code = "TestNormal", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 质控 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "质控", Code = "QC", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 前处理 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "前处理", Code = "PRE", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 常规检验.Key, 常规检验.Value },
                { 质控.Key, 质控.Value },
                { 前处理.Key, 前处理.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 打印模板_模板类型
    /// </summary>
    public static class PrintModelModelType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验清单 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "检验清单", Code = "SampleList", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 单一失控报告 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "单一失控报告", Code = "OnlyLoseReport", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 失控统计报告 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "失控统计报告", Code = "LoseStatisticsReport", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 补打条码 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "补打条码", Code = "RepairPrintBarcode", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目结果打印导出 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "项目结果打印导出", Code = "ItemResultPrintExport", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本条码_样本清单 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "样本条码_样本清单", Code = "BarCodeSampleList", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本条码_取单凭证 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "样本条码_取单凭证", Code = "BarCodeSampleGatherVoucher", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本签收_样本清单 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "样本签收_样本清单", Code = "BarCodeSampleSignForSampleList", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本签收_取单凭证 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "样本签收_取单凭证", Code = "BarCodeSampleSignForGatherVoucher", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本采集_样本清单 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "样本采集_样本清单", Code = "BarCodeSampleGetherSampleList", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本送检_样本清单 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "样本送检_样本清单", Code = "BarCodeSampleExchangeInspectSampleList", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本送检_附加清单 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "样本送检_附加清单", Code = "BarCodeSampleExchangeInspectAppendSampleList", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发_样本清单 = new KeyValuePair<string, BaseClassDicEntity>("13", new BaseClassDicEntity() { Id = "13", Name = "样本分发_样本清单", Code = "BarCodeSampleDisepenseSampleList", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发_分发标签 = new KeyValuePair<string, BaseClassDicEntity>("14", new BaseClassDicEntity() { Id = "14", Name = "样本分发_分发标签", Code = "BarCodeSampleDisepenseTag", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 样本分发_流转单 = new KeyValuePair<string, BaseClassDicEntity>("15", new BaseClassDicEntity() { Id = "15", Name = "样本分发_流转单", Code = "BarCodeSampleDisepenseFlowSheet", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 检验清单.Key, 检验清单.Value },
                { 单一失控报告.Key, 单一失控报告.Value },
                { 失控统计报告.Key, 失控统计报告.Value },
                { 补打条码.Key, 补打条码.Value },
                { 项目结果打印导出.Key, 项目结果打印导出.Value },
                { 样本条码_样本清单.Key, 样本条码_样本清单.Value },
                { 样本条码_取单凭证.Key,样本条码_取单凭证.Value},
                { 样本签收_样本清单.Key,样本签收_样本清单.Value},
                { 样本签收_取单凭证.Key,样本签收_取单凭证.Value},
                { 样本采集_样本清单.Key,样本采集_样本清单.Value},
                { 样本送检_样本清单.Key,样本送检_样本清单.Value},
                { 样本送检_附加清单.Key,样本送检_附加清单.Value},
                { 样本分发_样本清单.Key,样本分发_样本清单.Value},
                { 样本分发_分发标签.Key,样本分发_分发标签.Value},
                { 样本分发_流转单.Key,样本分发_流转单.Value}
            };
            return dic;
        }
    }

    /// <summary>
    /// 系统消息类型
    /// </summary>
    public static class SysMessageType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 通讯结果消息 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "通讯结果消息", Code = "TXJGMSG", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 危急值消息 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "危急值消息", Code = "WJZMSG", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 通讯结果消息.Key, 通讯结果消息.Value },
                { 危急值消息.Key, 危急值消息.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 仪器结果替换判断比较符号
    /// </summary>
    public static class EquipResultReplaceCompSymbol
    {
        public static KeyValuePair<string, BaseClassDicEntity> 完全等于 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "完全等于", Code = "WQDY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分等于部分替换 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "部分等于,部分替换", Code = "BFDYBFTH", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分等于全部替换 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "部分等于,全部替换", Code = "BFDYQBTH", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 等于 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "=", Code = "DY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于等于 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = ">=", Code = "DYDY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 小于等于 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "<=", Code = "XYDF", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = ">", Code = "DY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 小于 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "<", Code = "XY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于等于小于等于 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = ">=,<=", Code = "DYDYXYDY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于小于 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = ">,<", Code = "DYXY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于等于小于 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = ">=,<", Code = "DYDYXY", FontColor = "#ffffff", BGColor = "", Memo = "" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于小于等于 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = ">,<=", Code = "DYXYDY", FontColor = "#ffffff", BGColor = "", Memo = "" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 完全等于.Key, 完全等于.Value },
                { 部分等于部分替换.Key, 部分等于部分替换.Value },
                { 部分等于全部替换.Key, 部分等于全部替换.Value },
                { 等于.Key, 等于.Value },
                { 大于等于.Key, 大于等于.Value },
                { 小于等于.Key, 小于等于.Value },
                { 大于.Key, 大于.Value },
                { 大于等于小于等于.Key, 大于等于小于等于.Value },
                { 大于等于小于.Key, 大于等于小于.Value },
                { 大于等于小于.Key, 大于等于小于.Value },
                { 大于小于等于.Key, 大于小于等于.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 对比内容字段
    /// </summary>
    public static class CompValueField
    {
        public static KeyValuePair<string, BaseClassDicEntity> 定量结果 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "定量结果", Code = "DLJG", FontColor = "#ffffff", BGColor = "", Memo = "定量结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 定性结果 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "定性结果", Code = "DXJG", FontColor = "#ffffff", BGColor = "", Memo = "定性结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 结果状态 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "结果状态", Code = "JGZT'", FontColor = "#ffffff", BGColor = "", Memo = "结果状态" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目存在 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "项目存在", Code = "DLJG", FontColor = "#ffffff", BGColor = "", Memo = "项目存在" });
        public static KeyValuePair<string, BaseClassDicEntity> 项目计算结果 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "项目计算结果", Code = "DXJG", FontColor = "#ffffff", BGColor = "", Memo = "项目计算结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 历史定量结果 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "历史定量结果", Code = "JGZT'", FontColor = "#ffffff", BGColor = "", Memo = "历史定量结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 历史报告结果 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "历史报告结果", Code = "DLJG", FontColor = "#ffffff", BGColor = "", Memo = "历史报告结果" });
        public static KeyValuePair<string, BaseClassDicEntity> 历史结果状态 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "历史结果状态", Code = "DXJG", FontColor = "#ffffff", BGColor = "", Memo = "历史结果状态" });
        public static KeyValuePair<string, BaseClassDicEntity> 定量结果与历史值之差的绝对值 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "定量结果与历史值之差的绝对值", Code = "JGZT'", FontColor = "#ffffff", BGColor = "", Memo = "定量结果与历史值之差的绝对值" });
        public static KeyValuePair<string, BaseClassDicEntity> 定量结果与历史值计算 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "定量结果与历史值计算", Code = "DLJG", FontColor = "#ffffff", BGColor = "", Memo = "定量结果与历史值计算" });
        public static KeyValuePair<string, BaseClassDicEntity> 报告结果与历史报告值对比 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "报告结果与历史报告值对比", Code = "DXJG", FontColor = "#ffffff", BGColor = "", Memo = "报告结果与历史报告值对比" });
        public static KeyValuePair<string, BaseClassDicEntity> 结果状态与历史结果状态对比 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "结果状态与历史结果状态对比", Code = "JGZT'", FontColor = "#ffffff", BGColor = "", Memo = "结果状态与历史结果状态对比" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 定量结果.Key, 定量结果.Value },
                { 定性结果.Key, 定性结果.Value },
                { 结果状态.Key, 结果状态.Value },
                { 项目存在.Key, 项目存在.Value },
                { 项目计算结果.Key, 项目计算结果.Value },
                { 历史定量结果.Key, 历史定量结果.Value },
                { 历史报告结果.Key, 历史报告结果.Value },
                { 历史结果状态.Key, 历史结果状态.Value },
                { 定量结果与历史值之差的绝对值.Key, 定量结果与历史值之差的绝对值.Value },
                { 定量结果与历史值计算.Key, 定量结果与历史值计算.Value },
                { 报告结果与历史报告值对比.Key, 报告结果与历史报告值对比.Value },
                { 结果状态与历史结果状态对比.Key, 结果状态与历史结果状态对比.Value }
            };
            return dic;            
        }
    }

    /// <summary>
    /// 对比值类型
    /// </summary>
    public static class CompValueType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 数值 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "数值", Code = "SZ", FontColor = "#ffffff", BGColor = "", Memo = "数值" });
        public static KeyValuePair<string, BaseClassDicEntity> 文本 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "文本", Code = "WB", FontColor = "#ffffff", BGColor = "", Memo = "文本" });
        public static KeyValuePair<string, BaseClassDicEntity> 存在与否 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "存在与否", Code = "CZYF", FontColor = "#ffffff", BGColor = "", Memo = "存在与否" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 数值.Key, 数值.Value },
                { 文本.Key, 文本.Value },
                { 存在与否.Key, 存在与否.Value }
            };
            return dic;
        }
    }

    /// <summary>
    /// 对比类型
    /// </summary>
    public static class CompType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 大于 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "＞", Code = "DY", FontColor = "#ffffff", BGColor = "", Memo = "大于" });
        public static KeyValuePair<string, BaseClassDicEntity> 小于 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "＜", Code = "XY", FontColor = "#ffffff", BGColor = "", Memo = "小于" });
        public static KeyValuePair<string, BaseClassDicEntity> 等于 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "＝", Code = "DY", FontColor = "#ffffff", BGColor = "", Memo = "等于" });
        public static KeyValuePair<string, BaseClassDicEntity> 不等于 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "≠", Code = "BDY", FontColor = "#ffffff", BGColor = "", Memo = "不等于" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于等于 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "≥", Code = "DYDY", FontColor = "#ffffff", BGColor = "", Memo = "大于等于" });
        public static KeyValuePair<string, BaseClassDicEntity> 小于等于 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "≤", Code = "XYDY", FontColor = "#ffffff", BGColor = "", Memo = "小于等于" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于小于 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "＞,＜", Code = "DYXY", FontColor = "#ffffff", BGColor = "", Memo = "大于小于" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于等于小于 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "≥,＜", Code = "WB", FontColor = "#ffffff", BGColor = "", Memo = "大于等于小于" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于小于等于 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "＞,≤", Code = "DYXYDY", FontColor = "#ffffff", BGColor = "", Memo = "大于小于等于" });
        public static KeyValuePair<string, BaseClassDicEntity> 大于等于小于等于 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "≥,≤", Code = "DYDYXYDY", FontColor = "#ffffff", BGColor = "", Memo = "大于等于小于等于" });
        public static KeyValuePair<string, BaseClassDicEntity> 包含 = new KeyValuePair<string, BaseClassDicEntity>("20", new BaseClassDicEntity() { Id = "20", Name = "包含", Code = "BH", FontColor = "#ffffff", BGColor = "", Memo = "包含" });
        public static KeyValuePair<string, BaseClassDicEntity> 不包含 = new KeyValuePair<string, BaseClassDicEntity>("21", new BaseClassDicEntity() { Id = "21", Name = "不包含", Code = "BBH", FontColor = "#ffffff", BGColor = "", Memo = "不包含" });
        public static KeyValuePair<string, BaseClassDicEntity> 存在 = new KeyValuePair<string, BaseClassDicEntity>("30", new BaseClassDicEntity() { Id = "30", Name = "存在", Code = "CZ", FontColor = "#ffffff", BGColor = "", Memo = "存在" });
        public static KeyValuePair<string, BaseClassDicEntity> 不存在 = new KeyValuePair<string, BaseClassDicEntity>("31", new BaseClassDicEntity() { Id = "31", Name = "不存在", Code = "BCZ", FontColor = "#ffffff", BGColor = "", Memo = "不存在" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>
            {
                { 大于.Key, 大于.Value },
                { 小于.Key, 小于.Value },
                { 等于.Key, 等于.Value },
                { 不等于.Key, 不等于.Value },
                { 大于等于.Key, 大于等于.Value },
                { 小于等于.Key, 小于等于.Value },
                { 大于小于.Key, 大于小于.Value },
                { 大于等于小于.Key, 大于等于小于.Value },
                { 大于小于等于.Key, 大于小于等于.Value },
                { 大于等于小于等于.Key, 大于等于小于等于.Value },
                { 包含.Key, 包含.Value },
                { 不包含.Key, 不包含.Value },
                { 存在.Key, 存在.Value },
                { 不存在.Key, 不存在.Value }
            };
            return dic;
        }
    }
}
