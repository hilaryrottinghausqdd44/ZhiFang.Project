
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.Base;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BSCRecordPhrase : BaseBLL<SCRecordPhrase>, ZhiFang.IBLL.WebAssist.IBSCRecordPhrase
    {
        IBSCOperation IBSCOperation { get; set; }

        #region 修改信息记录
        public void AddSCOperation(SCRecordPhrase serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<SCRecordPhrase>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "SCRecordPhrase";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.公共记录项短语.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        #endregion

        public BaseResultDataValue SearchSCRecordPhraseOfGKByHQL(int page, int limit, string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            tempBaseResultDataValue.success = true;

            IList<SCRecordPhrase> entityList = this.SearchIListByHQL(where, sort, page, limit);

            JObject jresult = new JObject();
            JArray jdepList = new JArray();
            //按科室进行分组
            var groupByDeptList = entityList.GroupBy(p => p.TypeObjectId);
            foreach (var groupByDept in groupByDeptList)
            {
                //某一科室记录项集合
                JObject jdeptItem = new JObject();
                //某一科室的所有记录项结果短语集合
                JArray jdeptItemList = new JArray();
                //按记录项进行分组
                var groupByItemList = groupByDept.GroupBy(p => p.BObjectId);
                foreach (var groupByItem in groupByItemList)
                {
                    JObject phraseItem = new JObject();
                    //某一记录项的结果短语集合
                    JArray phraseList = new JArray();
                    foreach (var item in groupByItem)
                    {
                        JObject phrase = new JObject();
                        phrase.Add("value", item.CName);
                        phrase.Add("text", item.CName);
                        phraseList.Add(phrase);
                    }
                    phraseItem.Add("BObjectId", groupByItem.Key);
                    phraseItem.Add("Data", phraseList);
                    jdeptItemList.Add(phraseItem);
                }
                jdeptItem.Add("DeptId", groupByDept.Key);
                jdeptItem.Add("Data", jdeptItemList);
                jdepList.Add(jdeptItem);
            }
            jresult.Add("list", jdepList);
            string listStr = jresult.ToString().Replace(Environment.NewLine, "").Replace(" ", "");
            //ZhiFang.Common.Log.Log.Debug("listStr:" + listStr);
            tempBaseResultDataValue.ResultDataValue = listStr;

            return tempBaseResultDataValue;
        }
    }
}