using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using ZhiFang.ServiceCommon.RBAC;
using Newtonsoft.Json.Linq;
using System.ServiceModel.Channels;
using Newtonsoft.Json;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaCenBarCodeFormat : BaseBLL<ReaCenBarCodeFormat>, IBReaCenBarCodeFormat
    {
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }

        #region 条码规则导入导出
        public FileStream DownLoadReaCenBarCodeFormatOfPlatformOrgNo(string platformOrgNo, long labID)
        {
            //获取供应商条码规则及公共条码规则信息 
            string hqlWhere = string.Format("reacenbarcodeformat.IsUse=1 and (reacenbarcodeformat.PlatformOrgNo={0} or reacenbarcodeformat.Type=1) and reacenbarcodeformat.LabID={1}", platformOrgNo, labID);
            IList<ReaCenBarCodeFormat> tempList = this.SearchListByHQL(hqlWhere);

            string basePath = HttpRuntime.AppDomainAppPath.ToString() + "\\tempFiles\\ReaCenBarCodeFormat\\DownLoad";

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            string filePathAll = Path.Combine(basePath, platformOrgNo + ".json" + "." + FileExt.zf);
            if (File.Exists(filePathAll))
            {
                File.Delete(filePathAll);
            }
            ParseObjectProperty ffileAttachmentProperty = new ParseObjectProperty();

            JObject jresult = new JObject();
            jresult.Add("platformOrgNo", platformOrgNo);
            jresult.Add("list", JArray.FromObject(tempList));

            FileStream fsjson = new FileStream(filePathAll, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter swjson = new StreamWriter(fsjson, Encoding.UTF8);

            swjson.WriteLine(jresult.ToString());
            swjson.Close();
            fsjson.Close();

            return new FileStream(filePathAll, FileMode.Open, FileAccess.Read);
        }
        public BaseResultBool UploadReaCenBarCodeFormatOfAttachment(HttpPostedFile file, long labID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //解析上传的附件内容

            int startIndex = file.FileName.LastIndexOf(@"\");
            startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
            string fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
            string fileExt = fileName.Substring(fileName.LastIndexOf("."));
            if (!fileExt.Equals("." + FileExt.zf))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "导入的条码规则附件文件后缀名必须是.zf";
                return tempBaseResultBool;
            }

            string basePath = HttpRuntime.AppDomainAppPath.ToString() + "\\tempFiles\\ReaCenBarCodeFormat\\Upload";
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string filePathAll = Path.Combine(basePath, file.FileName + "");
            if (File.Exists(filePathAll))
            {
                File.Delete(filePathAll);
            }
            try
            {
                file.SaveAs(filePathAll);
            }
            catch (Exception ee)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "保存条码规则附件文件出错!" + ee.Message;
                ZhiFang.Common.Log.Log.Error("条码规则附件文件为:" + filePathAll + ";" + tempBaseResultBool.ErrorInfo);
            }

            StreamReader sr = new StreamReader(filePathAll, Encoding.Default);
            string jsonStr = sr.ReadToEnd();
            sr.Close();
            if (string.IsNullOrEmpty(jsonStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "附件文件内容为空!";
                return tempBaseResultBool;
            }

            JObject jresult = JObject.Parse(jsonStr);
            JToken orgNoToken = jresult["platformOrgNo"];
            if (string.IsNullOrEmpty(orgNoToken.ToString()))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "附件文件的平台编码信息为空,不能导入!";
                return tempBaseResultBool;
            }
            var uploadList = jresult.SelectToken("list").ToList();
            //IList<ReaCenBarCodeFormat> codeFormatList = jresult["list"].ToObject<IList<ReaCenBarCodeFormat>>();
            if (uploadList == null || uploadList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "附件文件的条码规则信息为空,不能导入!";
                return tempBaseResultBool;
            }
            string platformOrgNo = orgNoToken.ToString();
            //获取该机构原所有在用的供应商条码规则及公共条码规则信息
            string hqlWhere = string.Format("reacenbarcodeformat.IsUse=1 and (reacenbarcodeformat.PlatformOrgNo={0} or reacenbarcodeformat.Type=1) and reacenbarcodeformat.LabID={1}", platformOrgNo, labID);
            IList<ReaCenBarCodeFormat> tempList = this.SearchListByHQL(hqlWhere);
            foreach (JToken item in uploadList)
            {
                ReaCenBarCodeFormat model = new ReaCenBarCodeFormat();
                //var codeFormat = item.ToObject<ReaCenBarCodeFormat>();
                model.LabID = labID;
                model.IsUse = bool.Parse(item["IsUse"].ToString());
                model.Type = long.Parse(item["Type"].ToString());
                model.SplitCount = int.Parse(item["SplitCount"].ToString());
                model.CName = item["CName"].ToString();
                model.SName = item["SName"].ToString();
                model.ShortCode = item["ShortCode"].ToString();
                model.Pinyinzitou = item["Pinyinzitou"].ToString();
                model.BarCodeFormatExample = item["BarCodeFormatExample"].ToString();
                model.RegularExpression = item["RegularExpression"].ToString();
                model.Memo = item["Memo"].ToString();
                if (model.Type == 2)
                    model.PlatformOrgNo = int.Parse(platformOrgNo);
                this.Entity = model;
                this.Add();
            }
            if (uploadList.Count > 0 && tempList.Count > 0)
            {
                string memo = DateTime.Now.ToString("yyyy年MM月dd日HH") + "导入新的条码规则,该条码规则被停用;";
                foreach (ReaCenBarCodeFormat entity in tempList)
                {
                    entity.Memo = memo + entity.Memo;
                    entity.IsUse = false;
                    this.Entity = entity;
                    this.Edit();
                }
            }
            return tempBaseResultBool;
        }
        #endregion

        private BaseResultDataValue DecodingBarCode(string barCode, ReaCenBarCodeFormat regularRBCF, ref Dictionary<string, string> dicBarCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string[] strSplit = new string[] { regularRBCF.ShortCode };//分隔符    
            //arrayBarCode为分隔之后的数组
            string[] arrayBarCode = barCode.Split(strSplit, StringSplitOptions.None);
            JObject jsonObject = JObject.Parse(regularRBCF.RegularExpression);
            foreach (JProperty keyObject in jsonObject.Properties())
            {
                if (keyObject == null && keyObject.Value != null)
                    continue;
                int index = int.Parse(keyObject.Value["Index"].ToString());
                int start = int.Parse(keyObject.Value["StartIndex"].ToString());
                int count = int.Parse(keyObject.Value["Length"].ToString());
                if (index < 0)//忽略此项，不做解析
                    continue;
                string keyValue = "";
                if (index < arrayBarCode.Length)
                    keyValue = arrayBarCode[index];
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：获取" + keyObject.Name + "信息的规则设置错误！";
                    return brdv;
                }
                if (start >= 0 && count > 0 && start < keyValue.Length)
                {
                    if (count + start >= keyValue.Length)
                        count = keyValue.Length - start;
                    keyValue = keyValue.Substring(start, count);
                }
                dicBarCode.Add(keyObject.Name, keyValue);
            }
            return brdv;
        }
        public BaseResultDataValue DecodingSanBarCode(string barcode, ref Dictionary<string, Dictionary<string, string>> dicMultiBarCode, ref int barCodeType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string strBarCode = barcode;
            if (string.IsNullOrEmpty(barcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("条码信息不能为空！");
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }
            string strHQL = "(reacenbarcodeformat.IsUse=1)";
            IList<ReaCenBarCodeFormat> listRBCF = this.SearchListByHQL(strHQL).OrderBy(p => p.Type).ThenBy(p => p.DispOrder).ToList();
            if (listRBCF == null || listRBCF.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("不存在该条码的规则信息，请先设置条码规则！");
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }
            ReaCenBarCodeFormat regularRBCF = null;
            for (int i = 0; i <= listRBCF.Count - 1; i++)
            {
                ReaCenBarCodeFormat RBCF = listRBCF[i];

                bool isPrefix = true;//是否包含前缀标识
                if (!string.IsNullOrWhiteSpace(RBCF.SName))
                    isPrefix = (barcode.IndexOf(RBCF.SName) >= 0);
                if (!isPrefix)
                    continue;
                string[] splitChar = new string[] { RBCF.ShortCode };//分隔符  
                string[] array = strBarCode.Split(splitChar, StringSplitOptions.None);
                if (array.Length == RBCF.SplitCount + 1)
                {
                    regularRBCF = RBCF;
                    Dictionary<string, string> dicKey = new Dictionary<string, string>();
                    BaseResultDataValue tempBRDV = DecodingBarCode(strBarCode, regularRBCF, ref dicKey);
                    if (regularRBCF.BarCodeType.HasValue)
                        barCodeType = regularRBCF.BarCodeType.Value;
                    else
                        barCodeType = -1;
                    if (tempBRDV.success && dicKey.Count > 0 && dicKey.ContainsKey("ProdGoodsNo"))
                    {
                        string prodGoodsNo = dicKey["ProdGoodsNo"];
                        if ((!string.IsNullOrEmpty(prodGoodsNo)) && (!dicMultiBarCode.ContainsKey(prodGoodsNo)))
                            dicMultiBarCode.Add(prodGoodsNo, dicKey);
                    }
                }
            }//for
            return brdv;
        }
        private BaseResultDataValue DecodingSanBarCode(long reaCompID, string barcode, ref Dictionary<string, string> dicBarCode, ref int barCodeType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string strBarCode = barcode;
            if (string.IsNullOrEmpty(barcode))
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("条码信息不能为空！");
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }
            ReaCenOrg reaCenOrg = IBReaCenOrg.Get(reaCompID);
            if (reaCenOrg == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("获取不到供应商信息!供应商Id为：{0},", reaCompID);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }

            string strHQL = "(reacenbarcodeformat.Type=1 and reacenbarcodeformat.IsUse=1)";
            string platformOrgNo = reaCenOrg.PlatformOrgNo == null ? "" : reaCenOrg.PlatformOrgNo.ToString();
            if (!string.IsNullOrWhiteSpace(platformOrgNo))
                strHQL += " or (reacenbarcodeformat.Type=2 and reacenbarcodeformat.PlatformOrgNo=\'" + platformOrgNo + "\'" +
                          " and reacenbarcodeformat.IsUse=1)";

            IList<ReaCenBarCodeFormat> listRBCF = this.SearchListByHQL(strHQL);
            if (listRBCF == null || listRBCF.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("供应商【{0}】的条码格式信息不能为空！", reaCenOrg.CName);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }
            IList<ReaCenBarCodeFormat> listRBCFComp = listRBCF.OrderByDescending(p => p.Type).ThenBy(p => p.DispOrder).ToList();
            ReaCenBarCodeFormat regularRBCF = null;
            for (int i = 0; i <= listRBCFComp.Count - 1; i++)
            {
                ReaCenBarCodeFormat RBCF = listRBCFComp[i];

                bool isPrefix = true;//是否包含前缀标识
                if (!string.IsNullOrWhiteSpace(RBCF.SName))
                    isPrefix = (barcode.IndexOf(RBCF.SName) >= 0);
                if (!isPrefix)
                    continue;
                string[] splitChar = new string[] { RBCF.ShortCode };//分隔符  
                string[] array = strBarCode.Split(splitChar, StringSplitOptions.None);
                if (array.Length == RBCF.SplitCount + 1)
                {
                    regularRBCF = RBCF;
                    break;
                }
            }//for

            if (regularRBCF == null || string.IsNullOrWhiteSpace(regularRBCF.RegularExpression))
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("供应商【{0}】没有设置条码格式信息！", reaCenOrg.CName);
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                return brdv;
            }
            if (regularRBCF.BarCodeType.HasValue)
                barCodeType = regularRBCF.BarCodeType.Value;
            else
                barCodeType = -1;
            brdv = DecodingBarCode(strBarCode, regularRBCF, ref dicBarCode);
            return brdv;
        }
        public ReaGoodsScanCodeVO GetReaGoodsScanCodeVOBySanBarCode(long reaCompID, string barcode)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();

            //dicKey存储解析之后的信息，Key为属性名，Value为属性值。
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            int barCodeType = -1;//1:一维条码;2:二维条码
            BaseResultDataValue brdv = DecodingSanBarCode(reaCompID, barcode, ref dicKey, ref barCodeType);
            vo.BoolFlag = brdv.success;
            vo.ErrorInfo = brdv.ErrorInfo;
            if (brdv.success == false)
            {
                return vo;
            }
            switch (barCodeType)
            {
                case 1:
                    /***
                     * 如果解析条码规则类型为一维条码
                     * 批条码信息:从本地的供货明细查找
                     * 盒条码信息:从货品条码操作记录里查找
                     **/
                    break;
                case 2:
                    vo = IBReaGoodsOrgLink.SearchReaGoodsScanCodeVOByScanBarCode(reaCompID, barcode, dicKey);
                    break;
                default:
                    break;
            }
            return vo;
        }
    }
}