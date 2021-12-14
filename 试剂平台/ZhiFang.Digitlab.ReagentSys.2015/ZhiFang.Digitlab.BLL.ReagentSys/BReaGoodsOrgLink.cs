using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using System.Web;
using System.IO;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaGoodsOrgLink : BaseBLL<ReaGoodsOrgLink>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaGoodsOrgLink
    {
        IBSCOperation IBSCOperation { get; set; }

        /// <summary>
        /// 获取供应商货品信息(按货品进行分组,找出每组货品下的对应的供应商信息)
        /// </summary>
        /// <param name="goodIdStr"></param>
        /// <returns></returns>
        public IList<ReaGoodsCenOrgVO> SearchReaCenOrgGoodsListByGoodIdStr(string goodIdStr)
        {
            IList<ReaGoodsCenOrgVO> tempListVO = new List<ReaGoodsCenOrgVO>();
            if (string.IsNullOrEmpty(goodIdStr)) return tempListVO;
            string strWhere = "";
            //过滤机构类型为供应商与货品信息的数据
            string curDate = DateTime.Now.ToString("yy-MM-dd");
            strWhere = string.Format(" reagoodsorglink.Visible=1 and (reagoodsorglink.BeginTime<='{0}' and (reagoodsorglink.EndTime is null or reagoodsorglink.EndTime>='{1}')) and reagoodsorglink.CenOrg.OrgType={2}  and reagoodsorglink.ReaGoods.Id in ({3})", curDate, curDate, ReaCenOrgType.供货方.Key, goodIdStr);
            IList<ReaGoodsOrgLink> tempReaOrderList = this.SearchListByHQL(strWhere);
            if (tempReaOrderList == null || tempReaOrderList.Count == 0) return tempListVO;

            tempReaOrderList = tempReaOrderList.OrderBy(p => p.CenOrg.DispOrder).OrderByDescending(p => p.ReaGoods.DataAddTime).ToList();

            Dictionary<string, ReaGoodsOrgLink> tempDictionary = new Dictionary<string, ReaGoodsOrgLink>();
            //过滤重复的供应商+货品信息
            foreach (var reaOrder in tempReaOrderList)
            {
                string key = reaOrder.CenOrg.Id + ";" + reaOrder.ReaGoods.Id;
                if (!tempDictionary.Keys.Contains(key))
                {
                    tempDictionary.Add(key, reaOrder);
                }
            }

            //按货品进行分组,找出每组货品下的对应的供应商信息
            var tempGroupBy = tempDictionary.Values.ToList().GroupBy(p => p.ReaGoods).ToList();
            foreach (var listReaGoods in tempGroupBy)
            {
                ReaGoodsCenOrgVO vo = new ReaGoodsCenOrgVO();
                vo.GoodsId = listReaGoods.ElementAt(0).ReaGoods.Id.ToString();
                vo.GoodsCName = listReaGoods.ElementAt(0).ReaGoods.CName;

                vo.ReaCenOrgVOList = new List<ReaCenOrgVO>();
                //供应商按优先级排序,以处理默认供应商赋值
                var tempCenOrgList = listReaGoods.OrderBy(p => p.DispOrder);
                for (int i = 0; i < tempCenOrgList.Count(); i++)
                {
                    ReaCenOrgVO orgvo = new ReaCenOrgVO();
                    orgvo.Id = tempCenOrgList.ElementAt(i).Id.ToString();
                    orgvo.CenOrgId = tempCenOrgList.ElementAt(i).CenOrg.Id.ToString();
                    orgvo.CenOrgCName = tempCenOrgList.ElementAt(i).CenOrg.CName.ToString();
                    orgvo.CenOrgGoodsNo = tempCenOrgList.ElementAt(i).CenOrgGoodsNo;
                    orgvo.DispOrder = tempCenOrgList.ElementAt(i).CenOrg.DispOrder;
                    if (!vo.ReaCenOrgVOList.Contains(orgvo)) vo.ReaCenOrgVOList.Add(orgvo);
                }
                if (!tempListVO.Contains(vo)) tempListVO.Add(vo);
            }
            return tempListVO;

        }

        public void AddReaReqOperation(ReaGoodsOrgLink entity, long empID, string empName, int status)
        {
            SCOperation sco = new SCOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaGoodsOrgLink";
            sco.Memo = "";
            sco.IsUse = true;
            sco.Type = status;
            sco.TypeName = ReaGoodsOrgLinkStatus.GetStatusDic()[status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
    }
}