using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using System.Collections.Generic;
using System;
using System.Linq;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ZFReaRestful.BmsSaleExtract;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///1.同一供应商的同一货品(货品编号+单位),启用的只能是一个;
    ///2.同一供应商的不同货品,其供应商货品编码不能相同;
    /// </summary>
    public class BReaGoodsOrgLink : BaseBLL<ReaGoodsOrgLink>, ZhiFang.IBLL.ReagentSys.Client.IBReaGoodsOrgLink
    {
        IBSCOperation IBSCOperation { get; set; }
        IBReaCenOrg IBReaCenOrg { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }

        public BaseResultDataValue AddReaGoodsOrgLink(long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BaseResultBool baseResultBool = EditReaGoodsOrgLinkVerify();
            baseResultDataValue.success = baseResultBool.success;
            baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
            if (baseResultBool.success == false)
                return baseResultDataValue;

            baseResultDataValue.success = base.Add();
            this.AddSCOperation(this.Entity, empID, empName, int.Parse(ReaGoodsOrgLinkStatus.新增货品价格.Key));

            return baseResultDataValue;
        }
        public BaseResultBool EditReaGoodsOrgLink(long empID, string empName)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //编辑更新时,如果状态为启用的,需要先进行验证判断
            if (this.Entity.Visible == 1)
            {
                //编辑时,如前台不传CenOrg值,先获取CenOrg
                if (this.Entity.CenOrg == null)
                {
                    ReaGoodsOrgLink tempEntity = this.Get(this.Entity.Id);
                    this.Entity.CenOrg = tempEntity.CenOrg;
                }
                baseResultBool = EditReaGoodsOrgLinkVerify();
                if (baseResultBool.success == false) return baseResultBool;
            }

            baseResultBool = EditReaGoodsOrgLinkVerify();
            if (baseResultBool.success == false) return baseResultBool;

            baseResultBool.success = base.Edit();
            this.AddSCOperation(this.Entity, empID, empName, int.Parse(ReaGoodsOrgLinkStatus.编辑货品价格.Key));
            return baseResultBool;
        }
        public BaseResultBool UpdateReaGoodsOrgLink(string[] tempArray, long empID, string empName)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //编辑更新时,如果状态为启用的,需要先进行验证判断
            if (this.Entity.Visible == 1)
            {
                //编辑时,如前台不传CenOrg值,先获取CenOrg
                if (this.Entity.CenOrg == null)
                {
                    ReaGoodsOrgLink tempEntity = this.Get(this.Entity.Id);
                    this.Entity.CenOrg = tempEntity.CenOrg;
                }
                baseResultBool = EditReaGoodsOrgLinkVerify();
                if (baseResultBool.success == false) return baseResultBool;
            }

            baseResultBool.success = base.Update(tempArray);
            this.AddSCOperation(this.Entity, empID, empName, int.Parse(ReaGoodsOrgLinkStatus.编辑货品价格.Key));
            return baseResultBool;
        }
        public BaseResultBool EditReaGoodsOrgLinkVerify()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (this.Entity == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "Entity为空!";
                return baseResultBool;
            }
            if (this.Entity.CenOrg == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "供货或订货机构(CenOrg)为空!";
                return baseResultBool;
            }
            //if (string.IsNullOrEmpty(this.Entity.ProdGoodsNo))
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "厂商货品编号(ProdGoodsNo)为空!";
            //    return baseResultBool;
            //}
            //if (string.IsNullOrEmpty(this.Entity.CenOrgGoodsNo))
            //{
            //    baseResultBool.success = false;
            //    baseResultBool.ErrorInfo = "供货商货品编号(CenOrgGoodsNo)为空!";
            //    return baseResultBool;
            //}
            ReaGoods reaGoods = null;
            if (this.Entity.ReaGoods != null && string.IsNullOrEmpty(this.Entity.ReaGoods.UnitName))
                reaGoods = IDReaGoodsDao.Get(this.Entity.ReaGoods.Id);

            if (reaGoods == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取货品信息为空!";
                return baseResultBool;
            }

            //区分是否为同一货品:产品编号+货品单位 ReaGoodsNo
            string hqlWhere = string.Format("reagoodsorglink.Visible=1 and reagoodsorglink.Id!={0} and reagoodsorglink.CenOrg.Id={1} and reagoodsorglink.ReaGoods.ReaGoodsNo='{2}' and reagoodsorglink.ReaGoods.UnitName='{3}'", this.Entity.Id, this.Entity.CenOrg.Id, reaGoods.ReaGoodsNo, reaGoods.UnitName);

            IList<ReaGoodsOrgLink> tempList = this.SearchListByHQL(hqlWhere);
            if (tempList.Count > 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = string.Format("产品编号为:{0},单位为:{1},在当前机构已维护并处于有效启用状态,请不要重复维护!", reaGoods.ReaGoodsNo, reaGoods.UnitName);
                return baseResultBool;
            }
            return baseResultBool;
        }

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
            strWhere = string.Format(" reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.OrgType={0}  and reagoodsorglink.ReaGoods.Id in ({1})", ReaCenOrgType.供货方.Key, goodIdStr);
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
                    orgvo.DispOrder = tempCenOrgList.ElementAt(i).CenOrg.DispOrder;

                    orgvo.ReaGoodsNo = tempCenOrgList.ElementAt(i).ReaGoods.ReaGoodsNo;
                    orgvo.ProdGoodsNo = tempCenOrgList.ElementAt(i).ProdGoodsNo;
                    orgvo.CenOrgGoodsNo = tempCenOrgList.ElementAt(i).CenOrgGoodsNo;
                    orgvo.GoodsNo = tempCenOrgList.ElementAt(i).ReaGoods.GoodsNo;
                    orgvo.Price = tempCenOrgList.ElementAt(i).Price;
                    if (!vo.ReaCenOrgVOList.Contains(orgvo)) vo.ReaCenOrgVOList.Add(orgvo);
                }
                if (!tempListVO.Contains(vo)) tempListVO.Add(vo);
            }
            return tempListVO;

        }
        public EntityList<ReaGoodsOrgLink> SearchReaGoodsOrgLinkAndChildListByHQL(long orgId, string where, string sort, int page, int limit, bool isSearchChild, int orgType)
        {
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            string strWhere = "";
            string idStr = orgId.ToString();
            if (isSearchChild == true)
                idStr = IBReaCenOrg.SearchOrgIDStrListByOrgID(orgId, orgType);

            if (!string.IsNullOrWhiteSpace(where))
                strWhere = where;
            if (!string.IsNullOrWhiteSpace(idStr))
            {
                if (!string.IsNullOrWhiteSpace(strWhere)) strWhere += " and";
                strWhere = " reagoodsorglink.CenOrg.Id in (" + idStr + ")";
            }
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = this.SearchListByHQL(strWhere, sort, page, limit);
                }
                else
                {
                    entityList = this.SearchListByHQL(strWhere, page, limit);
                }
            }
            return entityList;
        }
        public IList<ReaGoodsVO> SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr(long reaCompID, string goodsNoStr)
        {
            IList<ReaGoodsVO> resultList = new List<ReaGoodsVO>();

            string where = string.Format("reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.OrgType={0} and reagoodsorglink.ReaGoods.GoodsNo in({1}) and reagoodsorglink.CenOrg.Id={2}", ReaCenOrgType.供货方.Key, goodsNoStr, reaCompID);
            IList<ReaGoodsOrgLink> tempList = this.SearchListByHQL(where);
            if (tempList.Count > 0)
            {
                foreach (var item in tempList)
                {
                    ReaGoodsVO vo = new ReaGoodsVO();
                    vo.BarCodeType = item.BarCodeType;
                    vo.ReaGoodsID = item.ReaGoods.Id;
                    vo.ReaGoodsName = item.ReaGoods.CName;
                    vo.SName = item.ReaGoods.SName;
                    vo.EName = item.ReaGoods.EName;

                    vo.CompGoodsLinkID = item.Id;
                    vo.ContractPrice = item.Price;
                    vo.ReaGoodsNo = item.ReaGoods.ReaGoodsNo;
                    vo.ProdGoodsNo = item.ReaGoods.ProdGoodsNo;
                    vo.CenOrgGoodsNo = item.CenOrgGoodsNo;
                    vo.GoodsNo = item.ReaGoods.GoodsNo;
                    resultList.Add(vo);
                }
            }
            return resultList;
        }
        public void AddSCOperation(ReaGoodsOrgLink entity, long empID, string empName, int status)
        {
            SCOperation sco = new SCOperation();
            sco.LabID = entity.LabID;
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaGoodsOrgLink";
            sco.Memo = "维护价格为:" + entity.Price;
            sco.IsUse = true;
            sco.Type = status;
            sco.TypeName = ReaGoodsOrgLinkStatus.GetStatusDic()[status.ToString()].Name;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }
        public ReaGoodsScanCodeVO SearchReaGoodsScanCodeVOByScanBarCode(long reaCompID, string barCode, Dictionary<string, string> dicKey)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();

            if (dicKey.ContainsKey("ProdGoodsNo"))
            {
                string strWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id=" + reaCompID.ToString() +
                " and (reagoodsorglink.CenOrgGoodsNo='" + dicKey["ProdGoodsNo"] + "' or reagoodsorglink.ReaGoods.ProdGoodsNo='" + dicKey["ProdGoodsNo"] + "')";

                //如果存在货品单位信息
                if (dicKey.ContainsKey("Unit"))
                    strWhere = strWhere + " and reagoodsorglink.ReaGoods.UnitName='" + dicKey["Unit"] + "'";
                IList<ReaGoodsOrgLink> listReaGoodsOrgLink = this.SearchListByHQL(strWhere);

                //如果条码存在单位,但在按单位获取货品信息为空时,按供应商+(货品厂商编号 或供应商货品编码)获取
                if (dicKey.ContainsKey("Unit") && listReaGoodsOrgLink.Count <= 0)
                {
                    strWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id=" + reaCompID.ToString() +
                " and (reagoodsorglink.CenOrgGoodsNo=\'" + dicKey["ProdGoodsNo"] + "\' or reagoodsorglink.ReaGoods.ProdGoodsNo=\'" + dicKey["ProdGoodsNo"] + "\')";
                    listReaGoodsOrgLink = this.SearchListByHQL(strWhere);
                }
                if (listReaGoodsOrgLink != null && listReaGoodsOrgLink.Count > 0)
                {
                    foreach (var RGOL in listReaGoodsOrgLink)
                    {
                        ReaBarCodeVO reaBarCodeVO = new ReaBarCodeVO();
                        reaBarCodeVO.ReaGoodsID = RGOL.ReaGoods.Id;
                        reaBarCodeVO.CName = RGOL.ReaGoods.CName;
                        reaBarCodeVO.SName = RGOL.ReaGoods.SName;
                        reaBarCodeVO.EName = RGOL.ReaGoods.EName;
                        reaBarCodeVO.UnitName = RGOL.ReaGoods.UnitName;

                        reaBarCodeVO.UnitMemo = RGOL.ReaGoods.UnitMemo;
                        reaBarCodeVO.ApproveDocNo = RGOL.ReaGoods.ApproveDocNo;
                        reaBarCodeVO.RegistNo = RGOL.ReaGoods.RegistNo;
                        reaBarCodeVO.RegistDate = RGOL.ReaGoods.RegistDate;
                        reaBarCodeVO.RegistNoInvalidDate = RGOL.ReaGoods.RegistNoInvalidDate;

                        reaBarCodeVO.BarCodeType = RGOL.BarCodeType;
                        reaBarCodeVO.CompGoodsLinkID = RGOL.Id;
                        reaBarCodeVO.Price = RGOL.Price;
                        reaBarCodeVO.BiddingNo = RGOL.BiddingNo;

                        reaBarCodeVO.OtherPackSerial = barCode;
                        reaBarCodeVO.UsePackSerial = barCode;
                        reaBarCodeVO.UsePackQRCode = barCode;
                        reaBarCodeVO.SysPackSerial = barCode;

                        reaBarCodeVO.ReaGoodsNo = RGOL.ReaGoods.ReaGoodsNo;
                        reaBarCodeVO.ProdGoodsNo = RGOL.ProdGoodsNo;
                        reaBarCodeVO.CenOrgGoodsNo = RGOL.CenOrgGoodsNo;
                        reaBarCodeVO.GoodsNo = RGOL.ReaGoods.GoodsNo;

                        if (dicKey.ContainsKey("LotNo"))
                            reaBarCodeVO.LotNo = dicKey["LotNo"];
                        if (dicKey.ContainsKey("InvalidDate"))
                        {
                            DateTime invalidDate = DateTime.MinValue;
                            DateTime.TryParse(dicKey["InvalidDate"], out invalidDate);
                            if (invalidDate == DateTime.MinValue)
                                invalidDate = DateTime.ParseExact(dicKey["InvalidDate"], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                            if (invalidDate != DateTime.MinValue)
                                reaBarCodeVO.InvalidDate = invalidDate.ToString("yyyy-MM-dd");
                        }
                        if (dicKey.ContainsKey("GoodsQty"))
                            reaBarCodeVO.GoodsQty = double.Parse(dicKey["GoodsQty"]);
                        if (dicKey.ContainsKey("CurDispOrder"))
                            reaBarCodeVO.CurDispOrder = int.Parse(dicKey["CurDispOrder"]);
                        vo.ReaBarCodeVOList.Add(reaBarCodeVO);
                    }
                }
            }
            if (vo.ReaBarCodeVOList.Count > 0)
                vo.BoolFlag = true;
            else
                vo.ErrorInfo = "供应商ID为:" + reaCompID.ToString() + ",货品编号为" + dicKey["ProdGoodsNo"] + "获取货品信息为空!";
            return vo;
        }

        public EntityList<ReaGoods> SearchReaGoodsByScanBarCode(string barCode, Dictionary<string, Dictionary<string, string>> dicMultiKey)
        {
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            IList<ReaGoods> listReaGoods = new List<ReaGoods>();
            IList<long> listGoodsID = new List<long>();
            foreach (KeyValuePair<string, Dictionary<string, string>> kv in dicMultiKey)
            {
                Dictionary<string, string> dicKey = kv.Value;
                if (dicKey.ContainsKey("ProdGoodsNo") && dicKey["ProdGoodsNo"] != null)
                {
                    //reagoodsorglink.ReaGoods.ProdGoodsNo
                    string strWhere = " reagoodsorglink.Visible=1 " + " and (reagoodsorglink.CenOrgGoodsNo=\'" + dicKey["ProdGoodsNo"] + "\' or reagoodsorglink.ReaGoods.ProdGoodsNo=\'" + dicKey["ProdGoodsNo"] + "\')";
                    ;
                    IList<ReaGoodsOrgLink> listReaGoodsOrgLink = this.SearchListByHQL(strWhere);
                    if (listReaGoodsOrgLink != null && listReaGoodsOrgLink.Count > 0)
                    {
                        foreach (ReaGoodsOrgLink entity in listReaGoodsOrgLink)
                        {
                            if (entity.ReaGoods != null && listGoodsID.IndexOf(entity.ReaGoods.Id) < 0)
                            {
                                listGoodsID.Add(entity.ReaGoods.Id);
                                listReaGoods.Add(entity.ReaGoods);
                            }
                        }//foreach
                    }
                }//dicKey
            }//foreach
            entityList.count = listReaGoods.Count;
            entityList.list = listReaGoods;
            return entityList;
        }
        public EntityList<ReaGoodsOrgLink> SearchReaGoodsOrgLinkByScanBarCode(string barCode, Dictionary<string, Dictionary<string, string>> dicMultiKey)
        {
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            IList<ReaGoodsOrgLink> returnList = new List<ReaGoodsOrgLink>();
            IList<long> listReaGoodsOrgLinkID = new List<long>();
            foreach (KeyValuePair<string, Dictionary<string, string>> kv in dicMultiKey)
            {
                Dictionary<string, string> dicKey = kv.Value;
                if (dicKey.ContainsKey("ProdGoodsNo") && dicKey["ProdGoodsNo"] != null)
                {
                    //reagoodsorglink.ReaGoods.ProdGoodsNo
                    string strWhere = " reagoodsorglink.Visible=1 " + " and (reagoodsorglink.CenOrgGoodsNo=\'" + dicKey["ProdGoodsNo"] + "\' or reagoodsorglink.ReaGoods.ProdGoodsNo=\'" + dicKey["ProdGoodsNo"] + "\')";
                    IList<ReaGoodsOrgLink> listReaGoodsOrgLink = this.SearchListByHQL(strWhere);
                    if (listReaGoodsOrgLink != null && listReaGoodsOrgLink.Count > 0)
                    {
                        foreach (ReaGoodsOrgLink entity in listReaGoodsOrgLink)
                        {
                            if (entity != null && listReaGoodsOrgLinkID.IndexOf(entity.Id) < 0)
                            {
                                listReaGoodsOrgLinkID.Add(entity.Id);
                                returnList.Add(entity);
                            }
                        }//foreach
                    }
                }//dicKey
            }//foreach

            entityList.count = returnList.Count;
            entityList.list = returnList;
            return entityList;
        }
        #region 客户端字典与平台供应商字典同步
        public Stream GetLabDictionaryExportToComp(string reaServerCompCode, string reaServerLabcCode, ref string fileName)
        {
            Stream fileStream = null;
            // reagoodsorglink.Visible=1 and 
            string strWhere = "reagoodsorglink.CenOrg.PlatformOrgNo='" + reaServerCompCode + "' and reagoodsorglink.CenOrgGoodsNo is not null";
            IList<ReaGoodsOrgLink> linkList = this.SearchListByHQL(strWhere);

            JObject jresult = new JObject();
            //实验室所属LabID
            jresult.Add("LabID", linkList[0].LabID);
            //实验室所属机构平台编码
            jresult.Add("ReaServerLabcCode", reaServerLabcCode);
            //供应商所属机构平台编码
            jresult.Add("ReaServerCompCode", reaServerCompCode);
            //供应商货品关系信息
            jresult.Add("ReaGoodsOrgLinkList", JArray.FromObject(linkList));

            fileName = "供应商货品关系信息导出.json." + FileExt.zf.ToString();
            string basePath = GetFilePath(linkList[0].LabID, reaServerCompCode);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            string filePathAll = Path.Combine(basePath, fileName);

            FileStream fsWriter = new FileStream(filePathAll, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            byte[] buffer = Encoding.UTF8.GetBytes(Common.Public.SecurityHelp.MD5Encrypt(jresult.ToString(), Common.Public.SecurityHelp.PWDMD5Key));
            fsWriter.Write(buffer, 0, buffer.Length);
            fsWriter.Close();

            fileStream = new FileStream(filePathAll, FileMode.Open);
            StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
            long fileSize = fileStream.Length;

            return fileStream;
        }
        public BaseResultDataValue AddUploadLabDictionaryOfCompSync(HttpPostedFile file, long compLabID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            //解析上传文件内容
            Stream sf = file.InputStream;
            StreamReader sr = new StreamReader(sf, Encoding.UTF8);
            //解密文件内容
            string tempStr = Common.Public.SecurityHelp.MD5Decrypt(sr.ReadToEnd(), Common.Public.SecurityHelp.PWDMD5Key);
            //ZhiFang.Common.Log.Log.Debug("jresult:" + tempStr);
            JObject jresult = JObject.Parse(tempStr);
            sr.Close();
            sf.Close();

            if (jresult == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "解析上传文件内容失败!";
                return brdv;
            }
            //上传实验室所属LabID
            long labId = long.Parse(jresult["LabID"].ToString());
            //供应商的机构平台编码
            string reaServerCompCode = jresult["ReaServerCompCode"].ToString();
            if (string.IsNullOrEmpty(reaServerCompCode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "供应商的机构平台编码为空!";
                return brdv;
            }
            //上传实验室所属机构平台编码
            string reaServerLabcCode = jresult["ReaServerLabcCode"].ToString();
            if (string.IsNullOrEmpty(reaServerLabcCode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "上传实验室所属机构平台编码为空!";
                return brdv;
            }
            //上传实验室的某一供应商货品关系字典信息
            IList<ReaGoodsOrgLink> labLinkList = jresult["ReaGoodsOrgLinkList"].ToObject<IList<ReaGoodsOrgLink>>();
            if (labLinkList == null || labLinkList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "上传的供应商货品信息为空!";
                return brdv;
            }

            //通过上传实验室所属机构平台编码找出平台供应商的某一订货方机构信息
            IList<ReaCenOrg> reaCenOrgList = IBReaCenOrg.SearchListByHQL("reaCenOrg.PlatformOrgNo='" + reaServerLabcCode + "'");
            if (reaCenOrgList.Count != 1)
            {
                brdv.success = false;
                brdv.ErrorInfo = "获取平台供应商的订货方机构信息记录为:" + reaCenOrgList.Count;
                return brdv;
            }
            //平台供应商的某一订货方机构信息
            ReaCenOrg labReaCenOrg = reaCenOrgList[0];
            brdv = AddCompDictionaryOfLabSync(labLinkList, labReaCenOrg);
            return brdv;
        }
        private BaseResultDataValue AddCompDictionaryOfLabSync(IList<ReaGoodsOrgLink> labLinkList, ReaCenOrg labReaCenOrg)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            //获取平台供应商的某一订货方的货品关系信息  reagoodsorglink.Visible=1 and 
            string strWhere = "reagoodsorglink.CenOrg.PlatformOrgNo='" + labReaCenOrg.PlatformOrgNo + "'";
            IList<ReaGoodsOrgLink> compLinkList = this.SearchListByHQL(strWhere);
            //获取平台供应商的所有在用的货品信息
            IList<ReaGoods> compReaGoodsList = IDReaGoodsDao.GetListByHQL("reaGoods.Visible=1");

            foreach (ReaGoodsOrgLink labLink in labLinkList)
            {
                var tempCompLink = compLinkList.Where(p => p.ReaGoods.ReaGoodsNo == labLink.CenOrgGoodsNo);
                if (tempCompLink != null && tempCompLink.Count() == 1)
                {
                    //客户端上传的货品在平台供应商的订货方货品关系里已经存在(关系已对照)
                    continue;
                }

                //订货方货品关系不存在,继续判断货品信息是否存在平台供应商里
                var tempCompReaGoods = compReaGoodsList.Where(p => p.ReaGoodsNo == labLink.CenOrgGoodsNo && p.UnitName == labLink.ReaGoods.UnitName);
                if (tempCompReaGoods == null || tempCompReaGoods.Count() >= 0)
                {
                    //在平台供应商添加机构货品信息及订货方货品关系信息
                    ReaGoods compGoods = labLink.ReaGoods;
                    compGoods.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                    brdv.success = IDReaGoodsDao.Save(compGoods);

                    brdv = AddReaGoodsOrgLinkOfLabSync(compGoods, labLink, labReaCenOrg);
                }
                else
                {
                    //只添加订货方机构货品关系信息
                    ReaGoods compGoods = tempCompReaGoods.ElementAt(0);
                    brdv = AddReaGoodsOrgLinkOfLabSync(compGoods, labLink, labReaCenOrg);
                }
            }

            return brdv;
        }
        private BaseResultDataValue AddReaGoodsOrgLinkOfLabSync(ReaGoods compGoods, ReaGoodsOrgLink labLink, ReaCenOrg labReaCenOrg)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            ReaGoodsOrgLink link = new ReaGoodsOrgLink();
            link.ReaGoods = compGoods;
            if (link.ReaGoods.DataTimeStamp == null)
                link.ReaGoods.DataTimeStamp = dataTimeStamp;
            link.ReaCenOrg = labReaCenOrg;
            link.Visible = 1;
            link.Price = labLink.Price;

            link.BiddingNo = labLink.BiddingNo;
            link.DispOrder = labLink.DispOrder;
            link.BeginTime = labLink.BeginTime;
            link.EndTime = labLink.EndTime;
            link.BarCodeType = labLink.BarCodeType;

            link.IsPrintBarCode = labLink.IsPrintBarCode;
            link.LabcGoodsLinkID = labLink.Id;
            link.ProdGoodsNo = labLink.ProdGoodsNo;

            this.Entity = link;
            brdv.success = this.Add();
            return brdv;
        }
        private string GetFilePath(long labId, string subDir)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "tempFiles\\";
            if (!string.IsNullOrEmpty(subDir))
            {
                filePath = filePath + subDir;
            }
            if (labId > 0)
                filePath = filePath + "\\" + labId;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            return filePath;
        }
        #endregion

        public BaseResultData SaveReaGoodsOrgLinkByMatchInterface(IList<ReaGoods> listReaGoods, long empID, string empName)
        {
            ReaGoodsOrgLink reaGoodsOrgLink = null;
            return SaveReaGoodsOrgLinkByMatchInterface(listReaGoods, null, empID, empName, ref reaGoodsOrgLink);
        }

        public BaseResultData SaveReaGoodsOrgLinkByMatchInterface(IList<ReaGoods> listReaGoods, ReaCenOrg reaOrg, long empID, string empName, ref ReaGoodsOrgLink reaGoodsOrgLink)
        {
            BaseResultData baseResultData = new BaseResultData();
            string strWhere = string.Format("reagoodsorglink.CenOrg.OrgType={0})", ReaCenOrgType.供货方.Key);
            IList<ReaGoodsOrgLink> reaGoodsOrgLinkList = new List<ReaGoodsOrgLink>();
            EntityList<ReaGoodsOrgLink> entityList = (this.DBDao as IDReaGoodsOrgLinkDao).QueryReaGoodsOrgLinkDao(strWhere, "", 0, 0);
            if (entityList != null && entityList.list != null)
                reaGoodsOrgLinkList = entityList.list;
            ReaGoodsOrgLink serverEntity = null;
            bool isEdit = false;
            //IList<ReaGoods> reaGoodsAllList = IDReaGoodsDao.LoadAll();
            IList<ReaCenOrg> reaCenOrgAllList = new List<ReaCenOrg>();
            if (reaOrg == null)
                reaCenOrgAllList = IBReaCenOrg.LoadAll();
            else
                reaCenOrgAllList.Add(reaOrg);

            foreach (ReaGoods reaGoods in listReaGoods)
            {
                isEdit = false;
                serverEntity = null;

                ReaCenOrg reaCenOrg = new ReaCenOrg();
                reaCenOrg.MatchCode = reaGoods.ReaCompCode;

                ReaGoodsOrgLink editEntity = new ReaGoodsOrgLink();
                editEntity.LabID = reaGoods.LabID;
                editEntity.ReaGoods = reaGoods;
                editEntity.CenOrg = reaCenOrg;
                editEntity.CenOrgGoodsNo = reaGoods.MatchCode;
                editEntity.Price = reaGoods.Price;
                editEntity.ProdGoodsNo = reaGoods.ProdGoodsNo;
                editEntity.Memo = reaGoods.ZDYMemo;

                #region 非空值的验证
                if (editEntity.CenOrg == null)
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的供货商信息(CenOrg)为空");
                    continue;
                }
                if (string.IsNullOrEmpty(editEntity.CenOrg.MatchCode))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的供货商信息(MatchCode)为空");
                    continue;
                }

                if (editEntity.ReaGoods == null)
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的供货商货品关系信息的货品信息(ReaGoods)为空");
                    continue;
                }
                if (string.IsNullOrEmpty(editEntity.ReaGoods.MatchCode))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的机构货品名称为：" + editEntity.ReaGoods.CName + ",物资对照码(MatchCode)值为空");
                    continue;
                }
                if (string.IsNullOrEmpty(editEntity.ReaGoods.UnitName))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的机构货品名称为：" + editEntity.ReaGoods.CName + ",包装单位(UnitName)值为空");
                    continue;
                }
                if (string.IsNullOrEmpty(editEntity.ReaGoods.ReaGoodsNo))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的机构货品名称为：" + editEntity.ReaGoods.CName + ",机构货品编码(ReaGoodsNo)值为空");
                    continue;
                }
                if (string.IsNullOrEmpty(editEntity.CenOrgGoodsNo))
                {
                    ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的机构货品名称为：" + editEntity.ReaGoods.CName + ",机构货品编码(ReaGoodsNo)值为空");
                    continue;
                }
                #endregion

                ////找出供货商货品关系对应的机构货品信息
                //var tempGoodsList = reaGoodsAllList.Where(p => p.Id == editEntity.ReaGoods.Id);
                //if (tempGoodsList != null && tempGoodsList.Count() > 0)
                //{
                //    editEntity.ReaGoods = tempGoodsList.ElementAt(0);
                //}
                //else
                //{
                //    tempGoodsList = reaGoodsAllList.Where(p => p.ReaGoodsNo == editEntity.ReaGoods.ReaGoodsNo && p.UnitName == editEntity.ReaGoods.UnitName);
                //    if (tempGoodsList != null && tempGoodsList.Count() > 0)
                //    {
                //        editEntity.ReaGoods = tempGoodsList.ElementAt(0);
                //    }
                //    else
                //    {
                //        ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的机构货品信息(ReaGoods),不存在机构货品信息表里,请先同步机构货品信息后再操作!");
                //        continue;
                //    }
                //}

                //找出供货商货品关系对应的供货商信息
                IList<ReaCenOrg> tempReaCenOrgList = reaCenOrgAllList.Where(p => p.Id == editEntity.CenOrg.Id).ToList();
                if (tempReaCenOrgList != null && tempReaCenOrgList.Count > 0)
                {
                    editEntity.CenOrg = tempReaCenOrgList[0];
                }
                else
                {
                    tempReaCenOrgList = reaCenOrgAllList.Where(p => p.MatchCode == editEntity.CenOrg.MatchCode).ToList();
                    if (tempReaCenOrgList != null && tempReaCenOrgList.Count > 0)
                    {
                        editEntity.CenOrg = tempReaCenOrgList[0];
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("同步物资接口的供货商货品关系的供货商信息(CenOrg),不存在供货商字典表里,请先同步供货商字典后再操作!");
                        continue;
                    }
                }

                IList<ReaGoodsOrgLink> tempList2 = reaGoodsOrgLinkList.Where(p => p.CenOrgGoodsNo == editEntity.CenOrgGoodsNo && p.ReaGoods.UnitName == editEntity.ReaGoods.UnitName).ToList();
                if (tempList2.Count > 0)
                {
                    serverEntity = tempList2[0];
                    isEdit = true;
                }

                if (isEdit)
                {
                    double oldPrice = serverEntity.Price;
                    serverEntity.DataUpdateTime = DateTime.Now;
                    //serverEntity.BarCodeType = editEntity.BarCodeType;
                    serverEntity.ProdGoodsNo = editEntity.ProdGoodsNo;
                    serverEntity.Price = editEntity.Price;
                    //serverEntity = ClassMapperHelp.GetMapper<ReaGoodsOrgLink, ReaGoodsOrgLink>(editEntity);
                    this.Entity = serverEntity;
                    reaGoodsOrgLink = serverEntity;
                    baseResultData.success = this.Edit();
                    //供货商货品价格变动更新记录
                    if (oldPrice != editEntity.Price)
                        this.AddSCOperation(this.Entity, empID, empName, int.Parse(ReaGoodsOrgLinkStatus.编辑货品价格.Key));
                }
                else
                {
                    editEntity.DispOrder = 1;
                    editEntity.BarCodeType = editEntity.ReaGoods.BarCodeMgr;
                    editEntity.ProdGoodsNo = editEntity.ReaGoods.ProdGoodsNo;
                    editEntity.BeginTime = DateTime.Now;
                    editEntity.DataAddTime = DateTime.Now;
                    editEntity.Visible = 1;
                    this.Entity = editEntity;
                    reaGoodsOrgLink = editEntity;
                    baseResultData.success = this.Add();
                    this.AddSCOperation(this.Entity, empID, empName, int.Parse(ReaGoodsOrgLinkStatus.新增货品价格.Key));
                }
            }
            return baseResultData;
        }

        public string QueryReaGoodsXML(string goodsNo, string lastModifyTime, string resultFieldList, string resultType)
        {
            string result = "";
            string hqlWhere = " 1=1 ";
            if (!string.IsNullOrWhiteSpace(goodsNo))
                hqlWhere = hqlWhere + " and reagoods.ReaGoodsNo=\'" + goodsNo + "\'";

            if (!string.IsNullOrWhiteSpace(lastModifyTime))
                hqlWhere = hqlWhere + " and reagoods.DataUpdateTime>=\'" + lastModifyTime + "\'";

            EntityList<ReaGoods> listReaGoods = IDReaGoodsDao.GetListByHQL(hqlWhere, 0, 0);
            if (listReaGoods != null && listReaGoods.count > 0)
            {
                if (string.IsNullOrWhiteSpace(resultFieldList))
                    resultFieldList = "Id,CName,ReaGoodsNo,Price,UnitName,UnitMemo,GoodsDesc,ProdGoodsNo,ProdOrgName,GoodsClass,GoodsClassType,StorageType,"+
                        "RegistNo,RegistDate,RegistNoInvalidDate,MatchCode,DataUpdateTime";
                string[] listField = resultFieldList.Split(',');
                StringBuilder strXml = new StringBuilder();
                foreach (ReaGoods entity in listReaGoods.list)
                {
                    strXml.Append("<Row>");
                    foreach (string field in listField)
                    {
                        if (string.IsNullOrWhiteSpace(field))
                            continue;
                        System.Reflection.PropertyInfo propertyInfo = entity.GetType().GetProperty(field);
                        if (propertyInfo != null)
                        {
                            object value = propertyInfo.GetValue(entity, null);
                            string strValue = (value == null ? "" : value.ToString());
                            strXml.Append("<" + field + ">" + strValue + "</" + field + ">");
                        }
                    }
                    strXml.Append("</Row>");
                }
                result = strXml.ToString();
            }
            return result;
        }

        public string QueryReaOrgGoodsXML(string goodsNo, string orgNo, string lastModifyTime, string resultFieldList, string resultType)
        {
            string result = "";
            string hqlWhere = " 1=1 ";
            if (!string.IsNullOrWhiteSpace(goodsNo))
                hqlWhere = hqlWhere + " and reagoodsorglink.ReaGoods.ReaGoodsNo=\'" + goodsNo + "\'";
            if (!string.IsNullOrWhiteSpace(orgNo))
                hqlWhere = " and reagoodsorglink.ReaCenOrg.OrgNo=" + orgNo;
            if (!string.IsNullOrWhiteSpace(lastModifyTime))
                hqlWhere = hqlWhere + " and reagoodsorglink.DataUpdateTime>\'" + lastModifyTime + "\'";

            EntityList<ReaGoodsOrgLink> listReaGoodsOrgLink = this.SearchListByHQL(hqlWhere, 0, 0);
            if (listReaGoodsOrgLink != null && listReaGoodsOrgLink.count > 0)
            {
                if (string.IsNullOrWhiteSpace(resultFieldList))
                    resultFieldList = "Id,OrgID,GoodsID,OrgName,CName,ReaGoodsNo,Price,UnitName,UnitMemo,GoodsDesc,ProdGoodsNo,ProdOrgName,GoodsClass,GoodsClassType,StorageType," +
                        "RegistNo,RegistDate,RegistNoInvalidDate,MatchCode,DataUpdateTime";
                string[] listField = resultFieldList.Split(',');
                StringBuilder strXml = new StringBuilder();
                foreach (ReaGoodsOrgLink entity in listReaGoodsOrgLink.list)
                {
                    strXml.Append("<Row>");
                    foreach (string field in listField)
                    {
                        object tempObject = entity;
                        if (string.IsNullOrWhiteSpace(field))
                            continue;
                        System.Reflection.PropertyInfo propertyInfo = null;
                        if (field == "Id" || field == "OrgID" || field == "GoodsID")
                            propertyInfo = entity.GetType().GetProperty(field);
                        else if (field == "OrgName")
                        {
                            if (entity.CenOrg != null)
                            {
                                propertyInfo = entity.CenOrg.GetType().GetProperty("CName");
                                tempObject = entity.CenOrg;
                            }
                        }
                        else if (entity.ReaGoods != null)
                        {
                            propertyInfo = entity.ReaGoods.GetType().GetProperty(field);
                            tempObject = entity.ReaGoods;
                        }
                        if (propertyInfo != null)
                        {
                            object value = propertyInfo.GetValue(tempObject, null);
                            string strValue = (value == null ? "" : value.ToString());
                            strXml.Append("<" + field + ">" + strValue + "</" + field + ">");
                        }
                    }
                    strXml.Append("</Row>");
                }
                result = strXml.ToString();
            }
            return result;
        }

    }
}