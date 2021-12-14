using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace ZhiFang.Common.Dictionary
{
    /// <summary>
    /// 小组类别
    /// </summary>
    public enum SectionType
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

    public enum SectionTypeVisible
    {
        //0
        UnVisible,
        //1
        Visible,
    }
    /// <summary>
    /// 报告title类别
    /// </summary>
    public enum ReportFormTitle
    {
        center,
        client,
        BatchPrint,
        zhuyuan,
        menzhen,
        tijian,

    }
    /// <summary>
    /// 录入界面项目字典分组类别
    /// </summary>
    public enum TestItemSuperGroupClass
    {
        //所有
        ALL,
        //医生组套收费
        DOCTORCOMBICHARGE,
        //普通
        OFTEN,
        //组套
        COMBI,
        //收费
        CHARGE,
        //检验大组
        SUPERGROUP,
        //组套收费

        COMBIITEMPROFILE
    }
    /// <summary>
    /// 检验状态标记
    /// </summary>
    public enum WebLisFlag
    {
        //社区医院BarcodeForm [WebLisFlag]: 0未上传，1上传, 2,修改中, 3删除,4(预留),5签收,6退回, 7核收，8正在检验,9 报告重审中, 10报告已发,11报告修订,12 部分报告
        //交换数据中心BarcodeForm [WebLisFlag]: 0未处理, 1(预留), 2修改中, 3删除,4(预留),5签收, 6退回, 7核收，8正在检验, 9 报告重审中,10报告已发, 11报告修订, 12 部分报告
        UnDo,
        FinishDo,
        Editing,
        Del,
        undefine,
        SignFor,
        ReBack,
        Receive,
        Checking,
        ReportReCheck,
        ReportSended,
        ReportEdit,
        PartReport
    }

    public enum ReportFormFileType
    {
        HTML,
        JPEG,
        JPG,
        GIF,
        BMP,
        PNG,
        TIFF,
        WORD,
        EXECL
    }

}
