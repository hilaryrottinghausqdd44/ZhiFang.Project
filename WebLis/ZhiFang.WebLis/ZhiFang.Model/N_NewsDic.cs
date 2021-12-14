using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model.WeiXinDic;

namespace ZhiFang.Model
{
    public static class NNewsStatus
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 起草 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "起草", Code = "Drafter", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 审批通过 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "审批通过", Code = "Approval", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 审批退回 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "审批退回", Code = "UnApproval", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 发布 = new KeyValuePair<string, BaseClassDicEntity>("4", new BaseClassDicEntity() { Id = "4", Name = "发布", Code = "Publisher", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 发布撤回 = new KeyValuePair<string, BaseClassDicEntity>("5", new BaseClassDicEntity() { Id = "5", Name = "发布撤回", Code = "Publisher", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 禁用 = new KeyValuePair<string, BaseClassDicEntity>("6", new BaseClassDicEntity() { Id = "6", Name = "禁用", Code = "UnIsUse", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(起草.Key, 起草.Value);
            dic.Add(审批通过.Key, 审批通过.Value);
            dic.Add(审批退回.Key, 审批退回.Value);
            dic.Add(发布.Key, 发布.Value);
            dic.Add(发布撤回.Key, 发布撤回.Value);
            dic.Add(禁用.Key, 禁用.Value);
            return dic;
        }
    }
}
