using System.Collections.Generic;

namespace ZhiFang.Entity.LabStar
{

    /// <summary>
    /// 医嘱单相关提示信息
    /// </summary>
    public class OrderFormHintVar
    {
        /// <summary>
        /// 医嘱单信息为空
        /// 无法获取对应的医嘱信息
        /// </summary>
        public static string Hint_OrderFormIsEmpty = "医嘱单信息为空！";
    }

    /// <summary>
    ///样本单相关提示信息
    /// </summary>
    public class BarCodeFormHintVar
    {
        /// <summary>
        /// 样本单信息为空
        /// 无法获取对应的样本信息
        /// </summary>
        public static string Hint_BarCodeFormIsEmpty = "样本单信息为空！";

        /// <summary>
        /// 该样本单已经核收
        /// </summary>
        public static string Hint_BarCodeFormIsReceive = "该样本单项目已经全部核收！";

        /// <summary>
        /// 没有要核收的样本项目
        /// </summary>
        public static string Hint_NoExistReceiveItem = "没有该小组要核收的检验项目！";
    }

    /// <summary>
    /// 检验单相关提示信息
    /// </summary>
    public class TestFormHintVar
    {
        /// <summary>
        /// 检验单信息为空！
        /// </summary>
        public static string Hint_TestFormIsEmpty = "检验单信息为空！";

        /// <summary>
        /// 检验单已存在！
        /// </summary>
        public static string Hint_TestFormExist = "检验单已存在！";

        /// <summary>
        /// 提示检验单处于某种状态的情况下，不能进行某种操作
        /// 例如：检验单为【检验中】状态，不能反审！
        /// </summary>
        public static string Hint_TestFormState = "检验单为【{0}】状态，不能{1}！";

        /// <summary>
        /// 根据ID获取不到指定的检验单
        /// </summary>
        public static string Hint_TestFormNoExistByID = "根据ID获取不到指定的检验单！";

        /// <summary>
        /// 检验单没有对应的医嘱信息
        /// </summary>
        public static string Hint_NoExistOrderForm = "没有对应的医嘱信息！";

        /// <summary>
        /// 检验单小组信息为空
        /// </summary>
        public static string Hint_SectionIsEmpty = "检验单小组信息为空！";

        /// <summary>
        /// 检验单样本号信息为空
        /// </summary>
        public static string Hint_SampleNoIsEmpty = "检验单样本号信息为空！";

        /// <summary>
        /// 样本号【{0}】已经存在
        /// </summary>
        public static string Hint_SampleNoIsExist = "样本号【{0}】已经存在！";

        /// <summary>
        /// 检验单检测日期信息为空
        /// </summary>
        public static string Hint_TestDateIsEmpty = "检验单检测日期信息为空！";
    }

    /// <summary>
    /// 检验单项目相关提示信息
    /// </summary>
    public struct TestItemHintVar
    {
        /// <summary>
        /// 检验单项目信息为空！
        /// </summary>
        public static string Hint_TestItemIsEmpty = "检验单项目信息为空！";
        /// <summary>
        /// 检验项目无对应的医嘱项目！
        /// </summary>
        public static string Hint_ParaNoOrderItem = "检验项目({0}个)【{1}】无对应的医嘱项目！";
    }

    /// <summary>
    /// 通讯文件处理提示信息
    /// </summary>
    public class CommFileHintVar
    {
        /// <summary>
        /// 上传的仪器检验结果文件信息为空
        /// </summary>
        public static string Hint_CommFileInfoIsEmpty = "上传的仪器检验结果文件信息为空！";

        /// <summary>
        /// 新增仪器检验结果文件
        /// </summary>
        public static string Hint_AddCommFile = "新增仪器检验结果文件！";
    }

    /// <summary>
    /// 患者信息提示信息
    /// </summary>
    public struct PatientInfoHintVar
    {
        /// <summary>
        /// 患者{0}信息为空，指定参数：姓名、性别、年龄等等
        /// </summary>
        public static string Hint_ParaIsEmpty = "{0}信息为空！";

        /// <summary>
        /// 姓名信息为空
        /// </summary>
        public static string Hint_CNameIsEmpty = "姓名信息为空！";

        /// <summary>
        /// 性别信息为空
        /// </summary>
        public static string Hint_GenderIsEmpty = "性别信息为空！";

        /// <summary>
        /// 出生日期信息为空
        /// </summary>
        public static string Hint_BirthdayIsEmpty = "出生日期信息为空！";

        /// <summary>
        /// 年龄信息为空
        /// </summary>
        public static string Hint_AgeIsEmpty = "年龄信息为空！";

        /// <summary>
        /// 年龄单位信息为空
        /// </summary>
        public static string Hint_AgeUnitIsEmpty = "年龄单位信息为空！";

        /// <summary>
        /// 就诊类型信息为空
        /// </summary>
        public static string Hint_SickTypeIsEmpty = "就诊类型信息为空！";

    }

    /// <summary>
    /// 样本各个时间节点检查提示信息
    /// </summary>
    public struct EachTimePointCheckHintVar
    {
        /// <summary>
        /// 采样时间为空
        /// </summary>
        public static string Hint_CollectTimeIsEmpty = "采样时间为空！";
        /// <summary>
        /// 收样时间为空
        /// </summary>
        public static string Hint_InceptTimeIsEmpty = "收样时间为空！";
        /// <summary>
        /// 上机时间为空
        /// </summary>
        public static string Hint_OnLineTimeeIsEmpty = "上机时间为空！";
        /// <summary>
        /// 检验时间为空
        /// </summary>
        public static string Hint_TestTimeIsEmpty = "检验时间为空！";
    }

}
