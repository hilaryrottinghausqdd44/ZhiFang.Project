using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ZhiFang.Model.WeiXinDic
{
    /// <summary>
    /// 用户订单状态
    /// </summary>
    public static class UserOrderFormStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 待缴费 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "待缴费", Code = "UnPay", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 已交费 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "已交费", Code = "Payed", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 使用中 = new KeyValuePair<string, BaseClassDicEntity>("31", new BaseClassDicEntity() { Id = "31", Name = "使用中", Code = "Useing", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 部分使用 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "部分使用", Code = "PartialUse", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 完全使用 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "完全使用", Code = "Useed", FontColor = "#ffffff", BGColor = "#2aa515" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消订单 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "取消订单", Code = "CancelApply", FontColor = "#ffffff", BGColor = "#12abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消处理中 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "取消处理中", Code = "Canceling", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 取消成功 = new KeyValuePair<string, BaseClassDicEntity>("7", new BaseClassDicEntity() { Id = "7", Name = "取消成功", Code = "Canceled", FontColor = "#ffffff", BGColor = "#17abe3" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请 = new KeyValuePair<string, BaseClassDicEntity>("8", new BaseClassDicEntity() { Id = "8", Name = "退款申请", Code = "RefundApply", FontColor = "#ffffff", BGColor = "#1195db" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请处理中 = new KeyValuePair<string, BaseClassDicEntity>("9", new BaseClassDicEntity() { Id = "9", Name = "退款申请处理中", Code = "RefundApplying", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款申请被打回 = new KeyValuePair<string, BaseClassDicEntity>("10", new BaseClassDicEntity() { Id = "10", Name = "退款申请被打回", Code = "RefundApplyBack", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款中 = new KeyValuePair<string, BaseClassDicEntity>("11", new BaseClassDicEntity() { Id = "11", Name = "退款中", Code = "Refunding", FontColor = "#ffffff", BGColor = "#dd6572" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 退款完成 = new KeyValuePair<string, BaseClassDicEntity>("12", new BaseClassDicEntity() { Id = "12", Name = "退款完成", Code = "Refunded", FontColor = "#ffffff", BGColor = "#e97f36" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(UserOrderFormStatus.待缴费.Key, UserOrderFormStatus.待缴费.Value);
            dic.Add(UserOrderFormStatus.已交费.Key, UserOrderFormStatus.已交费.Value);
            dic.Add(UserOrderFormStatus.使用中.Key, UserOrderFormStatus.使用中.Value);
            dic.Add(UserOrderFormStatus.部分使用.Key, UserOrderFormStatus.部分使用.Value);
            dic.Add(UserOrderFormStatus.完全使用.Key, UserOrderFormStatus.完全使用.Value);
            dic.Add(UserOrderFormStatus.取消订单.Key, UserOrderFormStatus.取消订单.Value);
            dic.Add(UserOrderFormStatus.取消处理中.Key, UserOrderFormStatus.取消处理中.Value);
            dic.Add(UserOrderFormStatus.取消成功.Key, UserOrderFormStatus.取消成功.Value);
            dic.Add(UserOrderFormStatus.退款申请.Key, UserOrderFormStatus.退款申请.Value);
            dic.Add(UserOrderFormStatus.退款申请处理中.Key, UserOrderFormStatus.退款申请处理中.Value);
            dic.Add(UserOrderFormStatus.退款申请被打回.Key, UserOrderFormStatus.退款申请被打回.Value);
            dic.Add(UserOrderFormStatus.退款中.Key, UserOrderFormStatus.退款中.Value);
            dic.Add(UserOrderFormStatus.退款完成.Key, UserOrderFormStatus.退款完成.Value);
            return dic;
        }
    }
    public class BaseClassDicEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string FontColor { get; set; }
        public string BGColor { get; set; }
    }

    [DataContract]
    public class ConsumerUserOrderFormVO
    {
        public ConsumerUserOrderFormVO() { }

        /// <summary>
        /// 消费码
        /// </summary>
        [DataMember]
        public string PayCode { get; set; }

        /// <summary>
        /// 采样单位ID
        /// </summary>
        [DataMember]
        public string WeblisSourceOrgID { get; set; }

        /// <summary>
        /// 采样单位名称
        /// </summary>
        [DataMember]
        public string WeblisSourceOrgName { get; set; }

        /// <summary>
        /// 采样单位所属区域ID
        /// </summary>
        [DataMember]
        public string ConsumerAreaID { get; set; }
    }
}
