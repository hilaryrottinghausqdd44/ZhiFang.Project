
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.WebAssist;
using ZhiFang.WebAssist.Common;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BSCRecordDtl : BaseBLL<SCRecordDtl>, ZhiFang.IBLL.WebAssist.IBSCRecordDtl
    {
        IDSCRecordTypeDao IDSCRecordTypeDao { get; set; }
        IDSCRecordTypeItemDao IDSCRecordTypeItemDao { get; set; }
        IDSCRecordPhraseDao IDSCRecordPhraseDao { get; set; }
        IDSCRecordItemLinkDao IDSCRecordItemLinkDao { get; set; }

        #region 院感登记
        public BaseResultDataValue AddSCRecordDtlListOfGK(GKSampleRequestForm docEntity, ref IList<SCRecordDtl> dtlEntityList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<SCRecordPhrase> phraseList = IDSCRecordPhraseDao.GetListByHQL("screcordphrase.PhraseType=2 and screcordphrase.TypeObjectId=" + docEntity.DeptId);
            IList<SCRecordItemLink> linkList = IDSCRecordItemLinkDao.GetListByHQL("screcorditemlink.SCRecordType.Id=" + docEntity.SCRecordType.Id);

            for (int i = 0; i < dtlEntityList.Count; i++)
            {
                SCRecordDtl entity = dtlEntityList[i];
                SCRecordItemLink link = null;
                var list2 = linkList.Where(p => p.SCRecordTypeItem.Id == entity.SCRecordTypeItem.Id);
                if (list2 != null && list2.Count() > 0)
                    link = list2.ElementAt(0);
                entity.ContentTypeID = int.Parse(SCRecordTypeContentType.院感登记.Key);
                entity.RecordDtlNo = docEntity.BarCode + (i + 1).ToString().PadLeft(2, '0');//补两位

                bool result = AddSCRecordDtlOfGK(docEntity, link, ref entity);
                dtlEntityList[i] = entity;
                if (result == false)
                {
                    brdv.success = false;
                    //throw new Exception(brdv.ErrorInfo);
                    break;
                }

                //科室记录项结果短语处理
                var tempList2 = phraseList.Where(p => p.BObjectId == entity.SCRecordTypeItem.Id && entity.ItemResult == p.CName);
                if (tempList2 == null || tempList2.Count() <= 0)
                {
                    AddSCRecordPhrase(docEntity, entity);
                }
            }
            return brdv;
        }
        public BaseResultBool EditSCRecordDtlOfGKList(GKSampleRequestForm docEntity, ref IList<SCRecordDtl> dtlEntityList)
        {
            BaseResultBool brdv = new BaseResultBool();
            foreach (var entity in dtlEntityList)
            {
                var editEntity = this.Get(entity.Id);
                if (editEntity == null)
                {
                    continue;
                }
                editEntity.ItemResult = entity.ItemResult;

                double numberItemResult = int.MinValue;
                bool tryResult = double.TryParse(entity.ItemResult, out numberItemResult);
                if (tryResult == true && numberItemResult != int.MinValue)
                    editEntity.NumberItemResult = numberItemResult;
                this.Entity = editEntity;
                brdv.success = this.Edit();
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = string.Format("更新样品记录项{0}的结果失败!", editEntity.SCRecordTypeItem.CName);
                    break;
                }
            }
            return brdv;
        }
        private bool AddSCRecordDtlOfGK(GKSampleRequestForm docEntity, SCRecordItemLink link, ref SCRecordDtl entity)
        {
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (docEntity.DataTimeStamp == null)
                docEntity.DataTimeStamp = dataTimeStamp;
            int dispOrder = entity.SCRecordTypeItem.DispOrder;
            if (link != null)
            {
                dispOrder = link.DispOrder;
            }
            entity = ClassMapperHelp.GetMapper<SCRecordDtl, SCRecordDtl>(entity);
            entity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
            entity.BObjectID = docEntity.Id;

            entity.SCRecordType = docEntity.SCRecordType;
            entity.SCRecordTypeItem = IDSCRecordTypeItemDao.Get(entity.SCRecordTypeItem.Id);

            entity.DataAddTime = DateTime.Now;
            entity.DispOrder = dispOrder;// entity.SCRecordTypeItem.DispOrder;

            entity.Visible = true;
            double numberItemResult = int.MinValue;
            bool tryResult = double.TryParse(entity.ItemResult, out numberItemResult);
            if (tryResult == true && numberItemResult != int.MinValue)
                entity.NumberItemResult = numberItemResult;

            this.Entity = entity;
            bool result = this.Add();
            return result;
        }
        private void AddSCRecordPhrase(GKSampleRequestForm docEntity, SCRecordDtl entity)
        {
            SCRecordPhrase phrase = new SCRecordPhrase();
            phrase.IsUse = true;
            phrase.DispOrder = entity.SCRecordTypeItem.DispOrder;
            phrase.PhraseType = int.Parse(PhraseType.按科室.Key);
            phrase.TypeObjectId = docEntity.DeptId;
            phrase.BObjectId = entity.SCRecordTypeItem.Id;
            phrase.CName = entity.ItemResult;
            phrase.PinYinZiTou = GetPinYin(phrase.CName);
            phrase.SName = phrase.PinYinZiTou;
            phrase.ShortCode = phrase.PinYinZiTou;
            IDSCRecordPhraseDao.Save(phrase);
        }

        private string GetPinYin(string chinese)
        {
            string pinYin = "";
            try
            {
                if (chinese != null && chinese.Length > 0)
                {
                    char[] tmpstr = chinese.ToCharArray();
                    foreach (char a in tmpstr)
                    {
                        pinYin += ZhiFang.Common.Public.StringPlus.Chinese2Spell.SingleChs2Spell(a.ToString()).Substring(0, 1);
                    }
                }
                else
                {
                    return pinYin;
                }
            }
            catch (Exception e)
            {
                pinYin = "";
            }
            return pinYin;
        }

        #endregion
    }
}