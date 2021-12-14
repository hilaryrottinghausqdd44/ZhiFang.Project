using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaCenBarCodeFormat : BaseBLL<ReaCenBarCodeFormat>, IBLL.ReagentSys.IBReaCenBarCodeFormat
    {
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }
        IBReaGoods IBReaGoods { get; set; }
        public ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfCompIDAndSerialNo(long reaCompID, string serialNo)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();
            if (string.IsNullOrEmpty(serialNo))
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码为空!");
                return vo;
            }
            ReaCenOrg reaCenOrg = IBReaCenOrg.Get(reaCompID);
            if (reaCenOrg == null)
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("供应商Id为:{0},获取供应商信息为空!", reaCompID);
                return vo;
            }
            if (!reaCenOrg.PlatformOrgNo.HasValue)
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("供应商Id为:{0},获取供应商的平台机构编码信息为空!", reaCompID);
                return vo;
            }

            //获取供应商条码格式信息
            IList<ReaCenBarCodeFormat> tempList = this.SearchListByHQL(string.Format("reacenbarcodeformat.PlatformOrgNo={0}", reaCenOrg.PlatformOrgNo.Value));
            if (tempList == null || tempList.Count <= 0)
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("供应商Id为:{0},获取供应商的条码格式信息为空!", reaCompID);
                ZhiFang.Common.Log.Log.Error(vo.ErrorInfo);
                return vo;
            };
            ReaCenBarCodeFormat barCodeFormat = null;
            #region 匹配供应商条码格式
            foreach (ReaCenBarCodeFormat model in tempList)
            {
                //条码规则前缀
                string prefixStr = model.SName;
                //条码规则前缀不为空的处理
                if (!string.IsNullOrEmpty(prefixStr))
                {
                    string perfixStr2 = serialNo.Substring(0, prefixStr.Length).ToLower();
                    if (perfixStr2 != prefixStr.ToLower())
                    {
                        continue;
                    }
                }
                //条码规则的分割符
                string dividerStr = model.ShortCode;
                //条码规则的分割符为空
                if (string.IsNullOrEmpty(dividerStr)) continue;
                //条码规则的分割符不为空,但传入的条码号值没包含有分割符
                if (!string.IsNullOrEmpty(dividerStr) && !serialNo.Contains(@"" + dividerStr)) continue;

                //传入条码的分隔符数量
                string[] arrSplitStr = serialNo.Split(dividerStr.ToCharArray());
                int splitCount = arrSplitStr.Length - 1;
                //传入条码的分隔符数量与当前条码规则的分隔符数量不相等
                if (splitCount != model.SplitCount) continue;

                //当前的条码规则的前缀,分割符及分隔符数量与传入条码一致时
                barCodeFormat = model;
                break;
            }
            #endregion

            if (barCodeFormat != null)
                vo = DecodingReaGoodsScanCodeVOByBarCodeFormat(barCodeFormat, reaCenOrg, reaCompID, serialNo);
            else
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("供应商Id为:{0},匹配供应商条码格式失败!", reaCompID);
                ZhiFang.Common.Log.Log.Error(vo.ErrorInfo);
            }
            return vo;
        }
        public ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOByBarCodeFormat(ReaCenBarCodeFormat barCodeFormat, ReaCenOrg reaCenOrg, long reaCompID, string serialNo)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();
            //条码规则前缀
            string prefixStr = barCodeFormat.SName;
            switch (prefixStr)
            {
                case "ZFRP":
                    vo = DecodingReaGoodsScanCodeVOByFormatPrefixOfZFRP(barCodeFormat, reaCenOrg, reaCompID, serialNo);
                    break;
                case "https://m.roche-diag.cn/barcode?num=":
                    vo.BoolFlag = false;
                    vo.ErrorInfo = string.Format("供应商Id为:{0},条码号为:{1},条码规则前缀为{2}未实现条码规则解码!", reaCompID, serialNo, prefixStr);
                    break;
                default:
                    vo.BoolFlag = false;
                    vo.ErrorInfo = string.Format("供应商Id为:{0},条码号为:{1},条码规则前缀为{2}未实现条码规则解码!", reaCompID, serialNo, prefixStr);
                    ZhiFang.Common.Log.Log.Error(vo.ErrorInfo);
                    break;
            }
            return vo;
        }
        /// <summary>
        /// 条码规则前缀为ZFRP
        /// 智方编码|一号码|厂商机构码|厂商产品编码|批号|效期|供应商机构码|货品明细ID|当前序号|明细数量
        /// </summary>
        /// <param name="barCodeFormat"></param>
        /// <param name="reaCompID"></param>
        /// <param name="serialNo"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOByFormatPrefixOfZFRP(ReaCenBarCodeFormat barCodeFormat, ReaCenOrg reaCenOrg, long reaCompID, string serialNo)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();

            //条码规则前缀
            string prefixStr = barCodeFormat.SName;
            //条码规则的分割符
            string dividerStr = barCodeFormat.ShortCode;
            //传入条码的分隔后
            string[] arrSplitStr = serialNo.Split(dividerStr.ToCharArray());
            //厂商机构码
            string prodOrgNo = arrSplitStr[2];
            //厂商产品编码
            string prodGoodsNo = arrSplitStr[3];
            //货品批号
            string lotNo = arrSplitStr[4];
            //货品有效期
            string invalidDate = arrSplitStr[5];
            if (string.IsNullOrEmpty(prodOrgNo))
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码号为:{0},厂商机构码为空,不能扫码!", serialNo, reaCenOrg.PlatformOrgNo.Value, prodOrgNo);
                return vo;
            }
            if (reaCenOrg.PlatformOrgNo.Value != int.Parse(prodOrgNo))
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码号为:{0},供应商平台机构码为:{1}与厂商机构码:{2}不一致!", serialNo, reaCenOrg.PlatformOrgNo.Value, prodOrgNo);
                return vo;
            }

            //过滤机构类型为供应商与货品信息的数据
            string curDate = DateTime.Now.ToString("yy-MM-dd");
            //(reagoodsorglink.BeginTime<='{0}' and (reagoodsorglink.EndTime is null or reagoodsorglink.EndTime>='{1}')) and
            string strWhere = string.Format(" reagoodsorglink.Visible=1 and  reagoodsorglink.CenOrg.OrgType={0} and reagoodsorglink.CenOrg.Id={1} and reagoodsorglink.ReaGoods.ProdGoodsNo='{2}' and reagoodsorglink.ReaGoods.BarCodeMgr={3}", ReaCenOrgType.供货方.Key, reaCompID, prodGoodsNo, int.Parse(ReaGoodsBarCodeMgr.盒条码.Key));
            IList<ReaGoodsOrgLink> reaGoodsOrgLinkList = IBReaGoodsOrgLink.SearchListByHQL(strWhere);
            if (reaGoodsOrgLinkList.Count <= 0)
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("供应商Id为:{0},条码号为:{1},获取供应商与货品信息为空!", reaCompID, serialNo);
                ZhiFang.Common.Log.Log.Debug(string.Format("厂商产品编码为{0},货品批号为{1},货品有效期为{2},{3}", prodGoodsNo, lotNo, invalidDate, vo.ErrorInfo));
                return vo;
            }
            foreach (var item in reaGoodsOrgLinkList)
            {
                ReaBarCodeVO barCode = new ReaBarCodeVO();
                barCode.LotNo = lotNo;
                barCode.ProdGoodsNo = prodGoodsNo;
                barCode.InvalidDate = invalidDate;

                barCode.ReaGoodsID = item.ReaGoods.Id;
                barCode.CName = item.ReaGoods.CName;
                barCode.EName = item.ReaGoods.EName;
                barCode.SName = item.ReaGoods.SName;
                barCode.GoodsNo = item.ReaGoods.GoodsNo;
                barCode.UnitName = item.ReaGoods.UnitName;
                barCode.UnitMemo = item.ReaGoods.UnitMemo;
                barCode.ApproveDocNo = item.ReaGoods.ApproveDocNo;
                barCode.RegistNo = item.ReaGoods.RegistNo;
                barCode.RegistDate = item.ReaGoods.RegistDate;
                barCode.RegistNoInvalidDate = item.ReaGoods.RegistNoInvalidDate;
                barCode.BarCodeMgr = item.ReaGoods.BarCodeMgr;

                barCode.ReaGoodsOrgLinkID = item.Id;
                barCode.Price = item.Price;
                barCode.BiddingNo = item.BiddingNo;
                barCode.OtherPackSerial = serialNo;
                barCode.UsePackSerial = serialNo;
                vo.ReaBarCodeVOList.Add(barCode);
            }
            vo.BoolFlag = true;
            return vo;
        }

    }

}