using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{

    /// <summary>
    /// 质量记录模板的类型
    /// 1.无论哪种类型的报表，均可每天填写。
    /// 2.如一张报表内部同时需要按日、周、月填写数据，报表类型应定义为月报表，即按最大时间定义类型。
    /// </summary>
    public enum QMSTempletType
    {
        无类型 = 0,
        日报表 = 1,
        周报表 = 2,
        月报表 = 3,
        季报表 = 4,
        半年报 = 5,
        年报表 = 6
    }

    /// <summary>
    /// 质量记录报表可填写的份数
    /// 大多数日报表、月报表等类型的质量数据规定每日和每月只填写一份
    /// 但是也有填写多份的情况，例如：员工的档案、一些特殊的医嘱单记录
    /// </summary>
    public enum QMSReportNumber
    {
        一份 = 0,
        多份 = 1
    }

    /// <summary>
    /// 质量记录审核级别
    /// </summary>
    public enum QMSExamineLevel
    {
        无需审核 = 0,
        一级审核 = 1,
        二级审核 = 2,
        三级审核 = 3
    }

}
