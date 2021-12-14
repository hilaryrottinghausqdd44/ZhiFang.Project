using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.OA;
using ZhiFang.Entity.OA;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.OA;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using System.Web;
using System.IO;
using ZhiFang.Entity.OA.ViewObject.Response;
using System.Text.RegularExpressions;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;

namespace ZhiFang.BLL.OA
{
    /// <summary>
    ///
    /// </summary>
    public class BAHServerLicence : BaseBLL<AHServerLicence>, ZhiFang.IBLL.OA.IBAHServerLicence
    {
        public IBAHOperation IBAHOperation { get; set; }
        public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDRBACEmpRolesDao IDRBACEmpRolesDao { get; set; }
        public IBBParameter IBBParameter { get; set; }
        public IDBEquipDao IDBEquipDao { get; set; }
        public IDPGMProgramDao IDPGMProgramDao { get; set; }
        public IBAHServerEquipLicence IBAHServerEquipLicence { get; set; }
        public IBAHServerProgramLicence IBAHServerProgramLicence { get; set; }
        public IDPClientDao IDPClientDao { get; set; }

        /// <summary>
        /// 服务器申请授权文件上传并返回处理好的申请信息
        /// </summary>
        /// <param name="file"></param>
        /// <param name="licenceCode">客户表的授权编码</param>
        /// <returns></returns>
        public BaseResultDataValue UploadAHServerLicenceFile(HttpPostedFile file, long pclientID, string licenceCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            ApplyAHServerLicence applyAHServerLicence = new ApplyAHServerLicence();
            List<AHServerProgramLicence> programLists = new List<AHServerProgramLicence>();
            List<AHServerEquipLicence> equipLists = new List<AHServerEquipLicence>();
            List<LicenceProgramType> programTypeLists = new List<LicenceProgramType>();
            AHServerLicence ahserverLicence = new AHServerLicence();

            applyAHServerLicence.LicenceProgramTypeList = programTypeLists;
            applyAHServerLicence.AHServerLicence = ahserverLicence;
            applyAHServerLicence.ApplyProgramInfoList = new List<ApplyProgramInfo>();
            applyAHServerLicence.AHServerEquipLicenceList = equipLists;
            applyAHServerLicence.AHServerProgramLicenceList = programLists;

            if (String.IsNullOrEmpty(licenceCode))
            {
                brdv.success = false;
                brdv.ErrorInfo = "客户信息的授权编码的为空!";
            }
            if (brdv.success && file != null && file.FileName != null && file.ContentLength > 0)
            {
                #region 上传保存处理
                int startIndex = file.FileName.LastIndexOf(@"\");
                startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                string tempName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                string newFileName = tempName.Substring(0, tempName.LastIndexOf("."));
                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                string fileName = "";
                string applyLicenceCode = "";
                fileName = newFileName + fileExt;

                int len = file.ContentLength;
                string contentType = file.ContentType;
                //上传附件路径
                string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
                if (len < 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "上传服务器授权文件的内容为空!";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                }
                else if (String.IsNullOrEmpty(parentPath))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "系统参数的上传附件路径未设置,不能保存上传服务器授权文件!";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                }

                string tempPath = "\\AHServerLicence\\" + "服务器授权文件导入\\" + newFileName + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                parentPath = parentPath + tempPath;
                if (!Directory.Exists(parentPath))
                    Directory.CreateDirectory(parentPath);

                string filepath = Path.Combine(parentPath, fileName);
                if (brdv.success)
                {
                    try
                    {
                        file.SaveAs(filepath);
                    }
                    catch (Exception ee)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "保存服务器授权文件出错!" + ee.Message;
                        ZhiFang.Common.Log.Log.Error("授权文件为:" + filepath + ";" + ee.ToString());
                    }
                }
                #endregion

                #region 授权申请主要信息
                //读取出的授权文件里的所有信息
                string info = "";
                //授权申请主要信息
                string[] tempApplyStr = new string[] { };
                //程序授权申请明细信息
                string[] tempProgrampStr = new string[] { };
                //仪器通讯授权申请明细信息
                string[] tempEquipStr = new string[] { };
                //判断是主服务器授权还是备份服务器授权
                bool isLRNo1 = true;
                var licenceTypelist = LicenceType.GetStatusDic();
                var licenceDateStatuslist = LicenceDateStatus.GetStatusDic();
                if (brdv.success)
                {
                    try
                    {
                        StreamReader sr = new StreamReader(filepath, Encoding.Default);
                        info = sr.ReadToEnd();
                        sr.Close();
                        if (String.IsNullOrEmpty(info))
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "读取服务器授权文件内容为空!";
                            ZhiFang.Common.Log.Log.Error("授权文件为:" + filepath + ";" + brdv.ErrorInfo);
                        }
                    }
                    catch (Exception ee)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "读取服务器授权文件内容出错!" + ee.Message;
                        ZhiFang.Common.Log.Log.Error("授权文件为:" + filepath + ";" + ee.ToString());
                    }
                }

                if (brdv.success)
                {
                    //授权申请主要信息
                    string applyInfo = "";
                    //程序授权申请明细信息
                    string programInfo = "";
                    //仪器通讯授权申请明细信息
                    string equipInfo = "";
                    int programStartIndex = info.IndexOf("****主程序授权****");
                    int equipStartIndex = info.IndexOf("****通讯程序授权****");
                    int tryStartIndex = info.IndexOf("****试用授权****");

                    if (programStartIndex > 0)
                        applyInfo = info.Substring(0, programStartIndex);

                    if (programStartIndex > 0 && equipStartIndex > 0 && equipStartIndex > programStartIndex)
                        programInfo = info.Substring(programStartIndex + 1, equipStartIndex);

                    if (equipStartIndex > 0 && tryStartIndex > 0 && tryStartIndex > equipStartIndex)
                        equipInfo = info.Substring(equipStartIndex + 1);

                    if (!String.IsNullOrEmpty(applyInfo))
                    {
                        tempApplyStr = applyInfo.Split(Environment.NewLine.ToCharArray());
                        if (tempApplyStr.Length > 0)
                        {
                            //过滤掉空数据行
                            tempApplyStr = tempApplyStr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                        }
                    }
                    if (!String.IsNullOrEmpty(programInfo))
                    {
                        tempProgrampStr = programInfo.Split(Environment.NewLine.ToCharArray());
                        if (tempProgrampStr.Length > 0)
                        {
                            //过滤掉空数据行
                            tempProgrampStr = tempProgrampStr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                        }
                    }

                    if (!String.IsNullOrEmpty(equipInfo))
                    {
                        tempEquipStr = equipInfo.Split(Environment.NewLine.ToCharArray());
                        if (tempEquipStr.Length > 0)
                        {
                            tempEquipStr = tempEquipStr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                        }
                    }
                }
                #endregion

                #region 处理授权文件的申请信息
                if (brdv.success)
                {
                    string[] tempArr = new string[] { };
                    ahserverLicence.ISNewApply = true;
                    ahserverLicence.Status = long.Parse(LicenceStatus.申请.Key.ToString());
                    ahserverLicence.PClientID = pclientID;
                    #region 授权申请主要信息处理
                    if (tempApplyStr.Length > 0)
                    {
                        var c = IDPClientDao.Get(pclientID);
                        foreach (string nextLine in tempApplyStr)
                        {
                            if (brdv.success == false)
                                break;
                            tempArr = nextLine.Split(':');
                            tempArr = tempArr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                            if (nextLine.Contains("当前授权申请号"))
                            {
                                if (tempArr.Length == 2)
                                {
                                    ahserverLicence.LRNo = tempArr[1].Trim();
                                    //ZhiFang.Common.Log.Log.Info("当前授权申请号:" + tempArr[1].Trim());
                                }
                            }
                            else if (nextLine.Contains("主服务器授权申请号"))
                            {
                                if (tempArr.Length == 2)
                                {
                                    ahserverLicence.LRNo1 = tempArr[1].Trim();
                                }
                            }
                            else if (nextLine.Contains("备份服务器授权申请号"))
                            {
                                if (tempArr.Length == 2)
                                {
                                    ahserverLicence.LRNo2 = tempArr[1].Trim();
                                    ZhiFang.Common.Log.Log.Info("备份服务器授权申请号:" + tempArr[1].Trim());
                                }
                            }
                            else if (nextLine.Contains("客户服务编号"))
                            {
                                if (tempArr.Length == 2)
                                {
                                    if (!String.IsNullOrEmpty(tempArr[1].Trim()))
                                    {
                                        applyLicenceCode = tempArr[1].Trim();
                                    }
                                    if (applyLicenceCode != licenceCode)
                                    {
                                        brdv.success = false;
                                        brdv.ErrorInfo = "申请的客户服务编号" + applyLicenceCode + "与客户信息授权编码为" + licenceCode + " 不一致!";
                                    }
                                }
                            }
                            else if (nextLine.Contains("客户显示名称"))
                            {
                                if (tempArr.Length == 2)
                                {
                                    ahserverLicence.PClientName = tempArr[1].Trim();
                                    //ZhiFang.Common.Log.Log.Info("客户显示名称:" + tempArr[1].Trim());
                                    if (c.LicenceClientName != tempArr[1].Trim())
                                    {
                                        brdv.success = false;
                                        brdv.ErrorInfo = "申请的客户显示名称" + tempArr[1].Trim() + "与客户信息客户显示名称为" + c.LicenceClientName + " 不一致!";
                                        return brdv;
                                    }
                                }
                            }
                            else if (nextLine.Contains("PTechSQH"))
                            {
                                if (tempArr.Length == 2)
                                {
                                    applyAHServerLicence.SQH = tempArr[1].Trim();
                                }
                            }
                        }

                        //if (c != null)
                        //{
                        //    if (c.LRNo1 != null && c.LRNo1.Trim() != "")
                        //    {
                        //        if (ahserverLicence.LRNo1 == null || ahserverLicence.LRNo1.Trim() == "" || ahserverLicence.LRNo1.Trim() != c.LRNo1.Trim())
                        //        {
                        //            brdv.success = false;
                        //            brdv.ErrorInfo = "授权文件内主服务器授权号和OA系统内该客户的主服务器授权号不一致！授权文件内主服务器授权号为空！";
                        //            return brdv;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (ahserverLicence.LRNo1 != null && ahserverLicence.LRNo1.Trim() != "" )
                        //        {
                        //            brdv.success = false;
                        //            brdv.ErrorInfo = "授权文件内主服务器授权号和OA系统内该客户的主服务器授权号不一致！OA系统内主服务器授权号为空！";
                        //            return brdv;
                        //        }
                        //    }

                        //    if (c.LRNo2 != null && c.LRNo2.Trim() != "")
                        //    {
                        //        if (ahserverLicence.LRNo2 == null || ahserverLicence.LRNo2.Trim() == "" || ahserverLicence.LRNo2.Trim() != c.LRNo2.Trim())
                        //        {
                        //            brdv.success = false;
                        //            brdv.ErrorInfo = "授权文件内备份服务器授权号和OA系统内该客户的备份服务器授权号不一致！授权文件内备份服务器授权号为空！";
                        //            return brdv;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (ahserverLicence.LRNo2 != null && ahserverLicence.LRNo2.Trim() != "")
                        //        {
                        //            brdv.success = false;
                        //            brdv.ErrorInfo = "授权文件内备份服务器授权号和OA系统内该客户的备份服务器授权号不一致！OA系统内备份服务器授权号为空！";
                        //            return brdv;
                        //        }
                        //    }
                        //}
                        #region LRNo
                        if (!String.IsNullOrEmpty(ahserverLicence.LRNo))
                        {
                            if (!String.IsNullOrEmpty(ahserverLicence.LRNo1) && ahserverLicence.LRNo == ahserverLicence.LRNo1)
                            {
                                isLRNo1 = true;
                            }
                            else if (!String.IsNullOrEmpty(ahserverLicence.LRNo2) && ahserverLicence.LRNo == ahserverLicence.LRNo2)
                            {
                                isLRNo1 = false;
                            }
                            else
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = "当前授权申请号与主服务器授权申请号及备份服务器授权申请号不一致!";
                            }
                        }
                        else
                        {
                            brdv.success = false;
                            brdv.ErrorInfo = "获取当前授权申请号信息为空!";
                        }
                        #endregion

                        //更新主备服务器授权申请号
                        string lrno1, lrno2;
                        lrno1 = (ahserverLicence.LRNo1 != null) ? ahserverLicence.LRNo1 : "";
                        lrno2 = (ahserverLicence.LRNo2 != null) ? ahserverLicence.LRNo2 : "";
                        ZhiFang.Common.Log.Log.Debug("UploadAHServerLicenceFile.授权申请时，更新客户信息中的当前、主、备服务器授权申请号。update PClient  set LRNo1='" + lrno1 + "',LRNo2='" + lrno2 + "' where Id=" + pclientID);
                        IDPClientDao.UpdateByHql("update PClient  set LRNo1='" + lrno1 + "',LRNo2='" + lrno2 + "' where Id=" + pclientID);

                        if (brdv.success)
                            applyAHServerLicence.AHServerLicence = ahserverLicence;
                    }
                    else
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "获取授权文件的申请号信息为空!";
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo + "授权文件为:" + filepath);
                    }
                    #endregion

                    #region 授权申请的程序类型
                    //(授权文件中行开头包含有四个星号的,如****主程序授权****)
                    string[] allRows = info.Split(Environment.NewLine.ToCharArray());
                    if (brdv.success && allRows.Length > 0)
                    {
                        allRows = allRows.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                        for (int i = 0; i < allRows.Length; i++)
                        {
                            string curLine = allRows[i];
                            curLine = curLine.Trim();
                            if (curLine.Contains("****") && curLine != "****试用授权****")
                            {
                                curLine = curLine.Replace("*", "");
                                LicenceProgramType entity = new LicenceProgramType();
                                entity.Id = i + 1;
                                entity.CName = curLine;
                                switch (entity.CName)
                                {
                                    case "主程序授权":
                                        entity.CName = "检验之星";
                                        entity.Code = "PTech";
                                        break;
                                    case "通讯程序授权":
                                        entity.CName = "仪器通讯程序";
                                        entity.Code = "BEquip";
                                        break;
                                    default:
                                        entity.Code = "Others";
                                        break;
                                }
                                programTypeLists.Add(entity);
                            }
                            applyAHServerLicence.LicenceProgramTypeList = programTypeLists;
                        }
                    }
                    #endregion

                    #region 程序授权申请明细信息处理
                    if (brdv.success && tempProgrampStr.Length > 0)
                    {
                        /***
                         * 取****主程序授权****的标题列信息:
                         * 序号 站点名称                授权类型 截止日期    主服务器授权号            备份服务器授权号                
                         ***/
                        string programpHeadStr = tempProgrampStr[2];
                        ZhiFang.Common.Log.Log.Debug("programpHeadStr:" + programpHeadStr);
                        long SNo = 0;
                        int sNoIndex = programpHeadStr.IndexOf("序号");//获取序号的起始位置
                        int sNoHanNum = GetHanNumFromString(programpHeadStr.Substring(0, sNoIndex));//汉字补位
                        sNoIndex = sNoIndex + sNoHanNum;

                        int nodeNameIndex = programpHeadStr.IndexOf("站点名称");//站点名称开始的索引位置 5
                        int nodeNameHanNum = GetHanNumFromString(programpHeadStr.Substring(0, nodeNameIndex));//汉字补位
                        nodeNameIndex = nodeNameIndex + nodeNameHanNum;

                        int licenceTypeIndex = programpHeadStr.IndexOf("授权类型");//授权类型开始索引位置 29;
                        int licenceTypeHanNum = GetHanNumFromString(programpHeadStr.Substring(0, licenceTypeIndex));//汉字补位
                        licenceTypeIndex = licenceTypeIndex + licenceTypeHanNum;

                        int licenceDateIndex = programpHeadStr.IndexOf("截止日期");//截止日期开始的索引位置 36
                        int licenceDateHanNum = GetHanNumFromString(programpHeadStr.Substring(0, licenceDateIndex));//汉字补位
                        licenceDateIndex = licenceDateIndex + licenceDateHanNum;

                        int licenceKey1Index = programpHeadStr.IndexOf("主服务器授权号");//主服务器授权号开始的索引位置 48
                        int licenceKey1HanNum = GetHanNumFromString(programpHeadStr.Substring(0, licenceKey1Index));//汉字补位
                        licenceKey1Index = licenceKey1Index + licenceKey1HanNum;

                        int licenceKey2Index = programpHeadStr.IndexOf("备份服务器授权号");//备份服务器授权号开始的索引位置 72
                        int licenceKey2HanNum = GetHanNumFromString(programpHeadStr.Substring(0, licenceKey2Index));//汉字补位
                        licenceKey2Index = licenceKey2Index + licenceKey2HanNum;

                        ZhiFang.Common.Log.Log.Debug(string.Format("sNoIndex:{0},nodeNameIndex:{1},licenceTypeIndex:{2},licenceDateIndex:{3},licenceKey1Index:{4},licenceKey2Index:{5}", sNoIndex, nodeNameIndex, licenceTypeIndex, licenceDateIndex, licenceKey1Index, licenceKey2Index));

                        //获取除通讯节点及通讯子节点之外的所有程序信息
                        List<PGMProgram> pgmprogramList = (List<PGMProgram>)IDPGMProgramDao.GetListByHQL("IsUse=1 and PBDictTree.Id!=5684872576807158459 and SubBDictTree.Id!=5684872576807158459");

                        for (int i = 0; i < tempProgrampStr.Length; i++)
                        {
                            string curInfo = tempProgrampStr[i];
                            tempArr = curInfo.Split(' ');
                            tempArr = tempArr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                            //该行的第一列是否是数字
                            if (tempArr.Length > 0 && long.TryParse(tempArr[0], out SNo))
                            {
                                AHServerProgramLicence entity = _getAHServerProgramLicence(isLRNo1, applyAHServerLicence, pgmprogramList, SNo, sNoIndex, nodeNameIndex, licenceTypeIndex, licenceDateIndex, licenceKey1Index, licenceKey2Index, programpHeadStr, curInfo);
                                programLists.Add(entity);
                            }
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("获取程序授权申请明细信息为空!授权文件为:" + filepath);
                    }
                    #endregion

                    #region 程序明细二次处理
                    //按程序SQH将上传程序明细信息及上一次申请主程序明细信息分组处理为商业及临时
                    if (brdv.success)
                        applyAHServerLicence.ApplyProgramInfoList = GetApplyProgramInfoList(programLists, ahserverLicence);
                    #endregion

                    #region 仪器通讯授权申请明细信息处理
                    if (brdv.success && tempEquipStr.Length > 0)
                    {
                        IList<BEquip> bequipList = IDBEquipDao.GetListByHQL("IsUse=1");
                        //过滤掉空数据行
                        tempEquipStr = tempEquipStr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                        /***
                         * 取****通讯程序授权****的标题列信息:
                         * 序号 仪器编号    程序名         SQH   授权类型  截止日期     授权号                    站点名               仪器名称        
                         ***/
                        string equipHeadStr = tempEquipStr[2];
                        ZhiFang.Common.Log.Log.Debug("equipHeadStr:" + equipHeadStr);
                        int sNoIndex = equipHeadStr.IndexOf("序号");//获取序号的起始位置
                        int sNoHanNum = GetHanNumFromString(equipHeadStr.Substring(0, sNoIndex));//汉字补位
                        sNoIndex = sNoIndex + sNoHanNum;

                        int equipNoIndex = equipHeadStr.IndexOf("仪器编号");//获取仪器编号的起始位置
                        int equipNoHanNum = GetHanNumFromString(equipHeadStr.Substring(0, equipNoIndex));//汉字补位
                        equipNoIndex = equipNoIndex + equipNoHanNum;

                        int programNameIndex = equipHeadStr.IndexOf("程序名");//获取程序名的起始位置
                        int programNameHanNum = GetHanNumFromString(equipHeadStr.Substring(0, programNameIndex));//汉字补位
                        programNameIndex = programNameIndex + programNameHanNum;

                        int sqhIndex = equipHeadStr.IndexOf("SQH");//获取SQH的起始位置
                        int sqhHanNum = GetHanNumFromString(equipHeadStr.Substring(0, sqhIndex));//汉字补位
                        sqhIndex = sqhIndex + sqhHanNum;

                        int licenceTypeIndex = equipHeadStr.IndexOf("授权类型");//获取授权类型的起始位置
                        int licenceTypeHanNum = GetHanNumFromString(equipHeadStr.Substring(0, licenceTypeIndex));//汉字补位
                        licenceTypeIndex = licenceTypeIndex + licenceTypeHanNum;

                        int licenceDateIndex = equipHeadStr.IndexOf("截止日期");//获取截止日期的起始位置
                        int licenceDateHanNum = GetHanNumFromString(equipHeadStr.Substring(0, licenceDateIndex));//汉字补位
                        licenceDateIndex = licenceDateIndex + licenceDateHanNum;

                        int licenceKeyIndex = equipHeadStr.IndexOf("授权号");//获取授权号的起始位置
                        int licenceKeyHanNum = GetHanNumFromString(equipHeadStr.Substring(0, licenceKeyIndex));//汉字补位
                        licenceKeyIndex = licenceKeyIndex + licenceKeyHanNum;

                        int nodeNameIndex = equipHeadStr.IndexOf("站点名");//获取站点名的起始位置
                        int nodeNameHanNum = GetHanNumFromString(equipHeadStr.Substring(0, nodeNameIndex));//汉字补位
                        nodeNameIndex = nodeNameIndex + nodeNameHanNum;

                        int equipNameIndex = equipHeadStr.IndexOf("仪器名称");//获取仪器名称的起始位置
                        int equipNameHanNum = GetHanNumFromString(equipHeadStr.Substring(0, equipNameIndex));//汉字补位
                        equipNameIndex = equipNameIndex + equipNameHanNum;

                        ZhiFang.Common.Log.Log.Debug(string.Format("sNoIndex:{0},equipNoIndex:{1},programNameIndex:{2},sqhIndex:{3},licenceTypeIndex:{4},licenceDateIndex:{5},licenceKeyIndex:{6},nodeNameIndex:{7},equipNameIndex:{8}", sNoIndex, equipNoIndex, programNameIndex, sqhIndex, licenceTypeIndex, licenceDateIndex, licenceKeyIndex, nodeNameIndex, equipNameIndex));

                        var tmpahserverlicence = DBDao.GetListByHQL(" PClientID =" + pclientID + " and Status in (" + LicenceStatus.商务授权通过.Key + "," + LicenceStatus.特批授权通过.Key + ") order by GenDateTime desc ");
                        IList<AHServerEquipLicence> tmpahequiplist = null;
                        if (tmpahserverlicence != null && tmpahserverlicence.Count > 0)
                            tmpahequiplist = IBAHServerEquipLicence.SearchListByHQL(" ServerLicenceID=" + tmpahserverlicence.ElementAt(0).Id + " ");
                        for (int i = 0; i < tempEquipStr.Length; i++)
                        {
                            string curInfo = tempEquipStr[i];
                            tempArr = curInfo.Split(' ');
                            tempArr = tempArr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                            //该行的第一列是否是数字
                            long SNo = 0;
                            if (tempArr.Length > 0 && long.TryParse(tempArr[0].Trim(), out SNo))
                            {
                                AHServerEquipLicence entity = _getAHServerEquipLicence(bequipList, tmpahequiplist, SNo, sNoIndex, equipNoIndex, programNameIndex, sqhIndex, licenceTypeIndex, licenceDateIndex, licenceKeyIndex, nodeNameIndex, equipNameIndex, equipHeadStr, curInfo);
                                equipLists.Add(entity);
                            }
                        }
                        if (equipLists != null && equipLists.Count > 0)
                            equipLists = equipLists.OrderBy(p => p.SNo).ToList();
                        applyAHServerLicence.AHServerEquipLicenceList = equipLists;
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Error("获取仪器通讯授权申请明细信息为空!授权文件为:" + filepath);
                    }
                    #endregion

                }
                #endregion

            }
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(applyAHServerLicence);
            return brdv;
        }
        private AHServerProgramLicence _getAHServerProgramLicence(bool isLRNo1, ApplyAHServerLicence applyAHServerLicence, List<PGMProgram> pgmprogramList, long SNo, int sNoIndex, int nodeNameIndex, int licenceTypeIndex, int licenceDateIndex, int licenceKey1Index, int licenceKey2Index, string programpHeadStr, string curInfo)
        {
            AHServerProgramLicence entity = new AHServerProgramLicence();
            entity.ISNewApply = true;
            entity.SNo = SNo;
            entity.SQH = applyAHServerLicence.SQH;
            var tempList = pgmprogramList.Where(s => s.SQH == entity.SQH).OrderByDescending(a => a.DataAddTime).ToList();
            if (tempList != null && tempList.Count() > 0)
            {
                entity.ProgramID = tempList[0].Id;
                entity.ProgramName = tempList[0].Title;
            }

            #region 站点名称及索引处理(站点名称可能包含中文名称或为空)
            string nodeTableName = "";
            int nodeNameHanNum = GetHanNumFromString(curInfo.Substring(0, nodeNameIndex));//计算nodeNameIndex之前共有几个汉字
            int nodeNameLen = licenceTypeIndex - nodeNameIndex;
            nodeTableName = curInfo.Substring(nodeNameIndex - nodeNameHanNum, nodeNameLen).Trim();//24
            string[] tempArr2 = nodeTableName.Split(' ');
            tempArr2 = tempArr2.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            //取站点名称时,站点名称包含中文名称,24的长度可能将授权类型也取到了nodeTableName中
            if (tempArr2.Length == 2)
            {
                nodeTableName = tempArr2[0].Trim();
            }
            entity.NodeTableName = nodeTableName;

            #endregion

            #region 授权类型
            int licenceTypeHanNum = GetHanNumFromString(curInfo.Substring(0, licenceTypeIndex));//汉字处理
            int licenceTypeLen = licenceDateIndex - licenceTypeIndex;
            string licenceTypeName = curInfo.Substring(licenceTypeIndex - licenceTypeHanNum, licenceTypeLen).Trim();//
            entity.LicenceTypeName = licenceTypeName;
            //ZhiFang.Common.Log.Log.Info("授权类型为:" + licenceTypeName);
            switch (licenceTypeName)
            {
                case "商业":
                    entity.LicenceTypeId = int.Parse(LicenceType.商业.Key.ToString());
                    break;
                case "评估":
                    entity.LicenceTypeId = int.Parse(LicenceType.评估.Key.ToString());
                    break;
                case "测试":
                    entity.LicenceTypeId = int.Parse(LicenceType.测试.Key.ToString());
                    break;
                case "临时":
                    entity.LicenceTypeId = int.Parse(LicenceType.临时.Key.ToString());
                    break;
                default:
                    break;
            }
            #endregion

            int licenceDateHanNum = GetHanNumFromString(curInfo.Substring(0, licenceDateIndex));//汉字处理
            int licenceDateLen = licenceKey1Index - licenceDateIndex;
            string licenceDate = curInfo.Substring(licenceDateIndex - licenceDateHanNum, licenceDateLen).Trim();//12
            if (!String.IsNullOrEmpty(licenceDate))
            {
                DateTime tempDate = DateTime.MinValue;
                if (DateTime.TryParse(licenceDate, out tempDate))
                    entity.LicenceDate = tempDate;
            }
            //ZhiFang.Common.Log.Log.Info("截止日期为:" + licenceDate);
            var licenceTypelist = LicenceType.GetStatusDic();
            entity = SetAHServerProgramLicenceStatus(licenceTypelist, entity);

            #region 主服务器授权还是备份服务器授权
            int licenceKey1HanNum = GetHanNumFromString(curInfo.Substring(0, licenceKey1Index));//汉字处理
            int licenceKey1Len = licenceKey2Index - licenceKey1Index;
            string licenceKey1 = curInfo.Substring(licenceKey1Index - licenceKey1HanNum, licenceKey1Len).Trim();//24
            //ZhiFang.Common.Log.Log.Info("主服务器授权号:" + licenceKey1);

            int licenceKey2HanNum = GetHanNumFromString(curInfo.Substring(0, licenceKey2Index));//汉字处理
            string licenceKey2 = curInfo.Substring(licenceKey2Index - licenceKey2HanNum).Trim();//24
            //ZhiFang.Common.Log.Log.Info("备服务器授权号为:" + licenceKey2);
            if (isLRNo1)
            {
                entity.LicenceKey1 = "";//licenceKey1
            }
            else
            {
                entity.LicenceKey2 = "";//licenceKey2
            }
            #endregion

            return entity;
        }
        private AHServerEquipLicence _getAHServerEquipLicence(IList<BEquip> bequipList, IList<AHServerEquipLicence> tmpahequiplist, long SNo, int sNoIndex, int equipNoIndex, int programNameIndex, int sqhIndex, int licenceTypeIndex, int licenceDateIndex, int licenceKeyIndex, int nodeNameIndex, int equipNameIndex, string equipHeadStr, string curInfo)
        {
            //string curLine = curInfo;
            string[] tempArr = curInfo.Split(' ');
            tempArr = tempArr.Where(s => !string.IsNullOrEmpty(s)).ToArray();

            AHServerEquipLicence entity = new AHServerEquipLicence();
            entity.SNo = SNo;
            entity.ISNewApply = true;
            //仪器编号为第二列
            string equipID = tempArr[1].Trim();
            if (!string.IsNullOrEmpty(equipID))
            {
                entity.EquipID = long.Parse(equipID);
            }
            //ZhiFang.Common.Log.Log.Info("仪器编号为:" + equipID);
            #region 程序名处理
            //程序名为第三列
            string programName = tempArr[2].Trim();
            entity.ProgramName = programName;
            //ZhiFang.Common.Log.Log.Info("用户程序名为:" + programName);
            #endregion

            #region 系统程序名,取仪器的型号
            int sqhLen = licenceTypeIndex - sqhIndex;
            string sqlStr = curInfo.Substring(0, sqhIndex);
            int sqhHanNum = GetHanNumFromString(sqlStr);//计算sqhIndex之前共有几个汉字
            string sqh = curInfo.Substring(sqhIndex - sqhHanNum, sqhLen).Trim();//4
            entity.SQH = sqh;
            //ZhiFang.Common.Log.Log.Debug("licenceTypeIndex:" + licenceTypeIndex + ",sqhIndex:" + sqhIndex + ",sqhHanNum:" + sqhHanNum + ",SQH:" + entity.SQH);
            string equipversion = "";
            if (!String.IsNullOrEmpty(sqh))
            {
                var tempList = bequipList.Where(s => s.Shortcode == sqh).ToList();
                if (tempList != null && tempList.Count() > 0)
                {
                    equipversion = tempList[0].Equipversion;
                    entity.SYSSQH = tempList[0].Shortcode;
                }
            }
            entity.Equipversion = equipversion;
            //ZhiFang.Common.Log.Log.Info("系统程序名为:" + equipversion);
            #endregion

            #region 授权类型
            int licenceTypeHanNum = GetHanNumFromString(curInfo.Substring(0, licenceTypeIndex));//计算licenceTypeIndex之前共有几个汉字
            int licenceTypeLen = licenceDateIndex - licenceTypeIndex;
            string licenceTypeName = curInfo.Substring(licenceTypeIndex - licenceTypeHanNum, licenceTypeLen).Trim();//4
            entity.LicenceTypeName = licenceTypeName;
            switch (licenceTypeName)
            {
                case "商业":
                    entity.LicenceTypeId = int.Parse(LicenceType.商业.Key.ToString());
                    break;
                case "评估":
                    entity.LicenceTypeId = int.Parse(LicenceType.评估.Key.ToString());
                    break;
                case "测试":
                    entity.LicenceTypeId = int.Parse(LicenceType.测试.Key.ToString());
                    break;
                case "临时":
                    entity.LicenceTypeId = int.Parse(LicenceType.临时.Key.ToString());
                    break;
                default:
                    break;
            }
            //ZhiFang.Common.Log.Log.Info("授权类型为:" + licenceTypeName);
            var licenceTypelist = LicenceType.GetStatusDic();
            entity = SetAHServerEquipLicenceStatus(licenceTypelist, entity);
            #endregion

            int licenceDateHanNum = GetHanNumFromString(curInfo.Substring(0, licenceDateIndex));//计算licenceDateIndex之前共有几个汉字
            int licenceDateLen = licenceKeyIndex - licenceDateIndex;
            string licenceDate = curInfo.Substring(licenceDateIndex - licenceDateHanNum, licenceDateLen).Trim();//12
            if (!String.IsNullOrEmpty(licenceDate))
            {
                DateTime tempDate = DateTime.MinValue;
                if (DateTime.TryParse(licenceDate, out tempDate))
                    entity.LicenceDate = tempDate;
            }

            int licenceKeyHanNum = GetHanNumFromString(curInfo.Substring(0, licenceKeyIndex));//计算licenceKeyIndex之前共有几个汉字
            int licenceKeyLen = nodeNameIndex - licenceKeyIndex;
            string licenceKey = curInfo.Substring(licenceKeyIndex - licenceKeyHanNum, licenceKeyLen).Trim();//24
            entity.LicenceKey = "";//licenceKey

            #region 站点名称
            int nodeNameyHanNum = GetHanNumFromString(curInfo.Substring(0, nodeNameIndex));//计算nodeNameIndex之前共有几个汉字
            int nodeNameLen = equipNameIndex - nodeNameIndex;
            string nodeTableName = curInfo.Substring(nodeNameIndex - nodeNameyHanNum, nodeNameLen).Trim();//16
            entity.NodeTableName = nodeTableName;
            //ZhiFang.Common.Log.Log.Info("站点名为:" + nodeTableName);
            #endregion

            if (tmpahequiplist != null && tmpahequiplist.Count > 0)
            {
                var ahequip = tmpahequiplist.Where(a => a.SNo == entity.SNo);
                if (ahequip != null && ahequip.Count() > 0)
                {
                    entity.AHBeforeSQH = ahequip.ElementAt(0).SQH;
                    if (ahequip.ElementAt(0).SQH == entity.SQH)
                    {
                        //ZhiFang.Common.Log.Log.Info("LicenceDate:" + ahequip.ElementAt(0).LicenceDate);
                        if (ahequip.ElementAt(0).LicenceDate.HasValue)
                            entity.AHBeforeDateTime = ahequip.ElementAt(0).LicenceDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            entity.AHBeforeDateTime = "";
                        if (ahequip.ElementAt(0).LicenceDate.HasValue)
                            entity.CalcDays = ahequip.ElementAt(0).LicenceDate.Value.Subtract(DateTime.Now).Days;
                        else
                            entity.CalcDays = 0;
                        entity.AHBeforeLicenceTypeId = ahequip.ElementAt(0).LicenceTypeId;
                        entity.AHBeforeLicenceTypeName = ahequip.ElementAt(0).LicenceTypeId.HasValue ? LicenceType.GetStatusDic()[ahequip.ElementAt(0).LicenceTypeId.ToString()].Name : "";
                    }
                }
            }
            //仪器名称
            int equipNameHanNum = GetHanNumFromString(curInfo.Substring(0, equipNameIndex));//计算equipNameIndex之前共有几个汉字
            string equipName = curInfo.Substring(equipNameIndex - equipNameHanNum).Trim();
            //ZhiFang.Common.Log.Log.Info("仪器名称为:" + equipName);
            entity.EquipName = equipName;
            return entity;
        }
        /// <summary>
        /// 程序明细二次处理
        /// </summary>
        /// <param name="programLicenceList"></param>
        /// <param name="ahserverLicence"></param>
        /// <returns></returns>
        private List<ApplyProgramInfo> GetApplyProgramInfoList(List<AHServerProgramLicence> programLicenceList, AHServerLicence ahserverLicence)
        {
            var licenceDateStatuslist = LicenceDateStatus.GetStatusDic();
            var licenceTypelist = LicenceType.GetStatusDic();
            List<ApplyProgramInfo> applyProgramInfoList = new List<ApplyProgramInfo>();
            //上一次主程序申请的授权信息
            List<AHServerProgramLicence> preBusinessList = null;
            List<AHServerProgramLicence> preTempList = null;
            //是否是空站点数
            bool nodeTableCountsIsNull = false;
            #region programLists为空时的处理
            if (programLicenceList == null || programLicenceList.Count < 1)
            {
                nodeTableCountsIsNull = true;
                //获取除通讯节点及通讯子节点之外的所有程序信息
                List<PGMProgram> pgmprogramList = (List<PGMProgram>)IDPGMProgramDao.GetListByHQL("IsUse=1 and PBDictTree.Id!=5684872576807158459 and SubBDictTree.Id!=5684872576807158459 and SQH='2000'");

                AHServerProgramLicence businessEntity = new AHServerProgramLicence();
                businessEntity.SNo = 1;
                businessEntity.IsUse = true;
                businessEntity.SQH = "2000";
                businessEntity.ServerLicenceID = ahserverLicence.Id;
                businessEntity.LicenceTypeId = int.Parse(LicenceType.商业.Key.ToString());

                AHServerProgramLicence tempEntity = new AHServerProgramLicence();
                tempEntity.SNo = 2;
                tempEntity.IsUse = true;
                tempEntity.SQH = "2000";
                tempEntity.ServerLicenceID = ahserverLicence.Id;
                tempEntity.LicenceTypeId = int.Parse(LicenceType.临时.Key.ToString());
                tempEntity.LicenceTypeName = licenceTypelist[LicenceType.临时.Key.ToString()].Name;
                if (pgmprogramList.Count > 0)
                {
                    var tempList = pgmprogramList.OrderByDescending(a => a.DataAddTime).ToList();
                    businessEntity.ProgramID = tempList[0].Id;
                    businessEntity.ProgramName = tempList[0].Title;

                    tempEntity.ProgramID = tempList[0].Id;
                    tempEntity.ProgramName = tempList[0].Title;
                }
                programLicenceList.Add(businessEntity);
                programLicenceList.Add(tempEntity);
            }
            #endregion

            #region 上一次申请的主程序
            long serverLicenceID = 0;
            string sql = "Id!=" + ahserverLicence.Id + " and PClientID=" + ahserverLicence.PClientID + " and ((IsSpecially=0 and Status=" + LicenceStatus.商务授权通过.Key + ") or (IsSpecially=1 and Status=" + LicenceStatus.特批授权通过.Key + "))";
            string order = " GenDateTime DESC";
            EntityList<AHServerLicence> ahserverLicenceList = this.SearchListByHQL(sql, order, 1, 1);
            if (ahserverLicenceList != null && ahserverLicenceList.count > 0)
                serverLicenceID = ahserverLicenceList.list[0].Id;
            IList<AHServerProgramLicence> preProgramList = null;

            if (serverLicenceID != 0)
                preProgramList = IBAHServerProgramLicence.SearchListByHQL("ServerLicenceID=" + serverLicenceID);
            #endregion

            var sqhList = programLicenceList.GroupBy(m => m.SQH.Trim()).Select(m => new
            {
                SQH = m.FirstOrDefault().SQH,
                LicenceTypeId = m.FirstOrDefault().LicenceTypeId,
                ProgramID = m.FirstOrDefault().ProgramID,
                ProgramName = m.FirstOrDefault().ProgramName
            });
            //var q = programLists.GroupBy(p => new { p.ServerLicenceID, p.SQH }).Where(x => x.Count() > 1).ToList();
            foreach (var sqhItem in sqhList)
            {
                if (preProgramList != null && preProgramList.Count > 0)
                {
                    //上一次主程序授权类型为商业的站点数
                    preBusinessList = preProgramList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.商业.Key.ToString())).ToList();
                    //上一次主程序授权类型为临时的站点数
                    preTempList = preProgramList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString())).ToList();
                }
                //当前主程序的SQH的授权类型为商业的站点数
                var businessList = programLicenceList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.商业.Key.ToString())).ToList();
                //当前主程序的SQH的授权类型为临时的站点数
                var tempList2 = programLicenceList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString())).ToList();

                //主程序授权类型为商业的点申请信息
                ApplyProgramInfo businessEntity = new ApplyProgramInfo();
                businessEntity.LicenceTypeId = int.Parse(LicenceType.商业.Key.ToString());
                businessEntity.LicenceTypeName = licenceTypelist[LicenceType.商业.Key.ToString()].Name;
                businessEntity.LicenceStatusId = int.Parse(LicenceDateStatus.有效.Key.ToString());
                businessEntity.LicenceStatusName = licenceDateStatuslist[businessEntity.LicenceStatusId.ToString()].Name;
                businessEntity.SQH = sqhItem.SQH;
                businessEntity.ProgramID = sqhItem.ProgramID;
                businessEntity.ProgramName = sqhItem.ProgramName;
                if (businessList != null)
                    businessEntity.NodeTableCounts = (nodeTableCountsIsNull == true ? 0 : businessList.Count);
                if (preBusinessList != null)
                    businessEntity.PreNodeTableCounts = preBusinessList.Count;

                //主程序授权类型为临时的点申请信息
                ApplyProgramInfo tempEntity = new ApplyProgramInfo();
                tempEntity.LicenceTypeId = int.Parse(LicenceType.临时.Key.ToString());
                tempEntity.SQH = sqhItem.SQH;
                tempEntity.ProgramID = sqhItem.ProgramID;
                tempEntity.ProgramName = sqhItem.ProgramName;
                if (tempList2 != null && tempList2.Count > 0)
                {
                    tempEntity.LicenceDate = tempList2[0].LicenceDate;
                    tempEntity.LicenceTypeName = licenceTypelist[LicenceType.临时.Key.ToString()].Name;
                    if (tempEntity.LicenceDate.HasValue)
                    {
                        tempEntity.LicenceStatusId = GetLicenceStatus(tempEntity.LicenceDate.Value.ToString("yyyy-MM-dd"));
                    }
                    if (tempEntity.LicenceStatusId > 0)
                        tempEntity.LicenceStatusName = licenceDateStatuslist[tempEntity.LicenceStatusId.ToString()].Name;
                }
                if (tempList2 != null)
                    tempEntity.NodeTableCounts = (nodeTableCountsIsNull == true ? 0 : tempList2.Count);
                if (preTempList != null && preTempList.Count > 0)
                {
                    tempEntity.PreNodeTableCounts = preTempList.Count;
                    tempEntity.PreLicenceDate = preTempList[0].LicenceDate;
                    tempEntity.PreLicenceTypeId = preTempList[0].LicenceTypeId;
                    tempEntity.PreLicenceTypeName = preTempList[0].LicenceTypeId.HasValue ? LicenceType.GetStatusDic()[preTempList[0].LicenceTypeId.Value.ToString()].Name : "";
                    TimeSpan d1 = preTempList[0].LicenceDate.Value.Subtract(DateTime.Now);
                    tempEntity.CalcDays = d1.Days;
                }
                applyProgramInfoList.Add(businessEntity);
                applyProgramInfoList.Add(tempEntity);
            }
            return applyProgramInfoList;
        }

        private AHServerProgramLicence SetAHServerProgramLicenceStatus(Dictionary<string, BaseClassDicEntity> licenceTypelist, AHServerProgramLicence entity)
        {
            var licenceDateStatuslist = LicenceDateStatus.GetStatusDic();
            if (entity.LicenceTypeId.HasValue)
            {
                switch (entity.LicenceTypeId.ToString())
                {
                    case "1":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.商业.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.商业.Key.ToString()].Name;
                        entity.LicenceStatusId = int.Parse(LicenceDateStatus.有效.Key.ToString());

                        break;
                    case "2":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.临时.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.临时.Key.ToString()].Name;
                        if (entity.LicenceDate.HasValue)
                        {
                            entity.LicenceStatusId = GetLicenceStatus(entity.LicenceDate.Value.ToString("yyyy-MM-dd"));
                            //TimeSpan d1 = entity.LicenceDate.Value.Subtract(DateTime.Now);
                            //entity.CalcDays = d1.Days;
                        }
                        break;
                    case "3":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.评估.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.评估.Key.ToString()].Name;
                        if (entity.LicenceDate.HasValue)
                        {
                            entity.LicenceStatusId = GetLicenceStatus(entity.LicenceDate.Value.ToString("yyyy-MM-dd"));
                            //TimeSpan d1 = entity.LicenceDate.Value.Subtract(DateTime.Now);
                            //entity.CalcDays = d1.Days;
                        }
                        break;
                    case "4":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.测试.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.测试.Key.ToString()].Name;
                        if (entity.LicenceDate.HasValue)
                        {
                            entity.LicenceStatusId = GetLicenceStatus(entity.LicenceDate.Value.ToString("yyyy-MM-dd"));
                            //TimeSpan d1 = entity.LicenceDate.Value.Subtract(DateTime.Now);
                            //entity.CalcDays = d1.Days;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (entity.LicenceStatusId > 0)
                entity.LicenceStatusName = licenceDateStatuslist[entity.LicenceStatusId.ToString()].Name;
            return entity;
        }

        private AHServerEquipLicence SetAHServerEquipLicenceStatus(Dictionary<string, BaseClassDicEntity> licenceTypelist, AHServerEquipLicence entity)
        {
            var licenceDateStatuslist = LicenceDateStatus.GetStatusDic();
            if (entity.LicenceTypeId.HasValue)
            {
                switch (entity.LicenceTypeId.ToString())
                {
                    case "1":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.商业.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.商业.Key.ToString()].Name;
                        entity.LicenceStatusId = int.Parse(LicenceDateStatus.有效.Key.ToString());
                        break;
                    case "2":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.临时.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.临时.Key.ToString()].Name;
                        if (entity.LicenceDate.HasValue)
                        {
                            entity.LicenceStatusId = GetLicenceStatus(entity.LicenceDate.Value.ToString("yyyy-MM-dd"));
                            TimeSpan d1 = entity.LicenceDate.Value.Subtract(DateTime.Now);
                            entity.CalcDays = d1.Days;
                        }
                        break;
                    case "3":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.评估.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.评估.Key.ToString()].Name;
                        if (entity.LicenceDate.HasValue)
                        {
                            entity.LicenceStatusId = GetLicenceStatus(entity.LicenceDate.Value.ToString("yyyy-MM-dd"));
                            TimeSpan d1 = entity.LicenceDate.Value.Subtract(DateTime.Now);
                            entity.CalcDays = d1.Days;
                        }
                        break;
                    case "4":
                        entity.LicenceTypeId = long.Parse(licenceTypelist[LicenceType.测试.Key.ToString()].Id);
                        entity.LicenceTypeName = licenceTypelist[LicenceType.测试.Key.ToString()].Name;
                        if (entity.LicenceDate.HasValue)
                        {
                            entity.LicenceStatusId = GetLicenceStatus(entity.LicenceDate.Value.ToString("yyyy-MM-dd"));
                            TimeSpan d1 = entity.LicenceDate.Value.Subtract(DateTime.Now);
                            entity.CalcDays = d1.Days;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (entity.LicenceStatusId > 0)
                entity.LicenceStatusName = licenceDateStatuslist[entity.LicenceStatusId.ToString()].Name;
            return entity;
        }
        public BaseResultDataValue AddAHAHServerLicenceAndDetails(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ApplyAHServerLicence applyEntity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            AHServerLicence entity = applyEntity.AHServerLicence;
            entity.Status = long.Parse(LicenceStatus.申请.Key.ToString());
            entity.ApplyDataTime = DateTime.Now;
            entity.IsUse = true;
            this.Entity = entity;
            if (base.Add())
            {
                SaveAHOperation(entity);
                StringBuilder errorInfo = new StringBuilder();

                #region 添加程序明细
                if (applyEntity.ApplyProgramInfoList != null && applyEntity.ApplyProgramInfoList.Count > 0)
                {
                    var applyProgramInfoList = applyEntity.ApplyProgramInfoList.OrderBy(a => a.SQH).ThenBy(a => a.LicenceTypeId);
                    //SQH滤重
                    var sqhLists = applyProgramInfoList.GroupBy(p => new { p.SQH }).Where(x => x.Count() > 1).ToList();

                    #region 程序明细的不同SQH处理
                    int sno = 0;
                    foreach (var sqhItem in sqhLists)
                    {
                        sno = 0;
                        var tempLists = applyProgramInfoList.Where(p => p.SQH == sqhItem.Key.SQH);
                        //每一个SQH的授权类型都可能有商业及临时的站点
                        foreach (ApplyProgramInfo item in tempLists)
                        {
                            for (int i = 0; i < item.NodeTableCounts; i++)
                            {
                                sno = sno + 1;
                                AHServerProgramLicence tempEntity = new AHServerProgramLicence();
                                tempEntity.ServerLicenceID = entity.Id;
                                tempEntity.SNo = sno;
                                tempEntity.LicenceTypeId = item.LicenceTypeId;
                                tempEntity.LicenceDate = item.LicenceDate;
                                tempEntity.SQH = item.SQH;
                                tempEntity.ProgramID = item.ProgramID;
                                tempEntity.ProgramName = item.ProgramName;
                                tempEntity.IsUse = true;
                                IBAHServerProgramLicence.Entity = tempEntity;
                                bool result = IBAHServerProgramLicence.Add();
                                if (result == false)
                                {
                                    errorInfo.Append("添加申请程序名称为" + item.ProgramName + ",SQH为" + item.SQH + "出错!" + Environment.NewLine);
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                #region 添加仪器明细
                if (applyEntity.AHServerEquipLicenceList != null && applyEntity.AHServerEquipLicenceList.Count > 0)
                {
                    var applyEquipLicenceList = applyEntity.AHServerEquipLicenceList.OrderBy(a => a.SNo).ThenBy(a => a.LicenceTypeId);

                    foreach (AHServerEquipLicence item in applyEquipLicenceList)
                    {
                        item.ServerLicenceID = entity.Id;
                        item.IsUse = true;
                        IBAHServerEquipLicence.Entity = item;
                        bool result = IBAHServerEquipLicence.Add();
                        if (result == false)
                        {
                            errorInfo.Append("添加申请仪器通讯名称为" + item.EquipName + ",SQH为" + item.SQH + "出错!" + Environment.NewLine);
                        }
                    }
                }
                #endregion

                AHServerLicenceStatusMessagePush(pushWeiXinMessageAction, this.Entity.Id, this.Entity.Status.ToString(), this.Entity);
                if (errorInfo.ToString().Length > 0)
                {
                    brdv.ErrorInfo = "服务器授权申请保存错误:" + Environment.NewLine + errorInfo.ToString();
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    brdv.success = false;
                }
                else
                {
                    brdv.success = true;
                }
            }
            else
            {
                brdv.ErrorInfo = "AddAHAHServerLicenceAndDetails.Add错误！";
                brdv.success = false;
            }

            return brdv;
        }
        /// <summary>
        /// 修改服务器授权信息及明细信息(包括手工追加的程序明细信息)
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="updateEntity"></param>
        /// <param name="tempArray"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        public BaseResultBool UpdateAHServerLicenceAndDetails(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, ApplyAHServerLicence updateEntity, string[] tempArray, long EmpID, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            //前台传回的待更新保存的实体信息
            AHServerLicence editAHServer = updateEntity.AHServerLicence;
            //当前编辑待保存的服务器授权申请保存在数据库的信息
            AHServerLicence serverEntity = new AHServerLicence();
            serverEntity = DBDao.Get(editAHServer.Id);
            if (serverEntity != null)
            {
                editAHServer.PClientID = serverEntity.PClientID;
                editAHServer.PClientName = serverEntity.PClientName;
                editAHServer.LRNo = serverEntity.LRNo;
                editAHServer.LRNo1 = serverEntity.LRNo1;
                editAHServer.LRNo2 = serverEntity.LRNo2;
                editAHServer.ApplyDataTime = serverEntity.ApplyDataTime;
            }
            //生成及返回授权文件的实体信息
            ApplyAHServerLicence applyEntity = new ApplyAHServerLicence();
            applyEntity.AHServerLicence = editAHServer;
            applyEntity.AHServerEquipLicenceList = updateEntity.AHServerEquipLicenceList;
            //生成及返回授权文件的程序明细信息
            IList<AHServerProgramLicence> applyAllProgramList = new List<AHServerProgramLicence>();

            var tmpa = tempArray.ToList();
            if (serverEntity == null)
            {
                brb.ErrorInfo = "服务器授权ID：" + editAHServer.Id + "为空！";
                ZhiFang.Common.Log.Log.Error(brb.ErrorInfo);
                brb.success = false;
                return brb;
            }
            if (!AHServerLicenceStatusUpdateCheck(editAHServer, serverEntity, EmpID, EmpName, tmpa))
            {
                brb.ErrorInfo = "服务器授权ID：" + editAHServer.Id + "的状态为：" + LicenceStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                ZhiFang.Common.Log.Log.Error(brb.ErrorInfo);
                brb.success = false;
                return brb;
            }
            bool result = true;
            StringBuilder errorInfo = new StringBuilder();

            #region 申请程序明细信息处理
            //需要新增站点的程序明细信息.string为
            IList<AHServerProgramLicence> tempAddProgramList = new List<AHServerProgramLicence>();
            StringBuilder delProgramList = new StringBuilder();

            if (brb.success)
            {
                #region 程序明细第一次处理
                //当前编辑待保存的程序明细保存在数据库的信息
                IList<AHServerProgramLicence> serverAllProgramList = new List<AHServerProgramLicence>();
                serverAllProgramList = IBAHServerProgramLicence.SearchListByHQL("ServerLicenceID=" + editAHServer.Id);
                serverAllProgramList = serverAllProgramList.OrderBy(a => a.SNo).ToList();
                //授权申请明细先与服务器的程序明细同步
                applyAllProgramList = serverAllProgramList;

                //当前需要编辑保存的程序授权明细信息
                IList<ApplyProgramInfo> curProgramList = updateEntity.ApplyProgramInfoList;

                if (curProgramList == null)
                {
                    curProgramList = new List<ApplyProgramInfo>();
                }
                //找出程序明细的不同SHQ
                var sqhList = curProgramList.GroupBy(m => m.SQH.Trim()).Select(m => new
                {
                    SQH = m.FirstOrDefault().SQH,
                    LicenceTypeId = m.FirstOrDefault().LicenceTypeId,
                    ProgramID = m.FirstOrDefault().ProgramID,
                    ProgramName = m.FirstOrDefault().ProgramName
                });
                //var q = programLists.GroupBy(p => new { p.ServerLicenceID, p.SQH }).Where(x => x.Count() > 1).ToList();
                foreach (var sqhItem in sqhList)
                {
                    #region 商业授权站点添加或者删除的处理
                    // 当前主程序的SQH为sqhItem.SQH及授权类型为商业的站点数
                    var businessList = curProgramList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.商业.Key.ToString())).ToList();
                    //服务器上的SQH为sqhItem.SQH及授权类型为商业的站点数
                    var businessList2 = serverAllProgramList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.商业.Key.ToString())).ToList();

                    if (businessList.Count > 0)
                    {
                        //获取当前程序明细申请的授权类型为商业的总站点数
                        int counts = 0, addCounts = 0, delCounts = 0;
                        foreach (var infoItem in businessList)
                        {
                            counts = counts + infoItem.NodeTableCounts;
                        }
                        //需要删除的商业授权站点数=服务器的商业站点数-当前申请的商业授权站点数
                        if (businessList2.Count > counts)
                            delCounts = businessList2.Count - counts;
                        if (delCounts > 0)
                        {
                            if (counts >= 1)
                                counts = counts - 1;
                            else
                                counts = 0;
                            for (int i = counts; i < businessList2.Count; i++)
                            {
                                var delEntoty = businessList2[i];
                                delProgramList.Append(delEntoty.Id + ",");
                                //需要移除的商业授权程序明细
                                applyAllProgramList.Remove(delEntoty);
                            }
                        }

                        //需要添加的商业授权站点数=当前申请的商业授权站点数-服务器的商业站点数
                        if (counts > businessList2.Count)
                            addCounts = counts - businessList2.Count;
                        if (addCounts > 0)
                        {
                            var addEntoty = businessList[0];
                            for (int i = 0; i < addCounts; i++)
                            {
                                AHServerProgramLicence addEntity = new AHServerProgramLicence();
                                addEntity.ServerLicenceID = editAHServer.Id;
                                addEntity.LicenceTypeId = addEntoty.LicenceTypeId;
                                addEntity.LicenceDate = addEntoty.LicenceDate;
                                addEntity.SQH = addEntoty.SQH;
                                addEntity.ProgramID = addEntoty.ProgramID;
                                addEntity.ProgramName = addEntoty.ProgramName;
                                addEntity.IsUse = true;
                                tempAddProgramList.Add(addEntity);
                                //需要添加的商业授权程序明细
                                applyAllProgramList.Add(addEntity);
                            }
                        }
                    }

                    #endregion

                    #region 临时授权站点添加/删除/更新保存的处理
                    //当前主程序的SQH的授权类型为临时的站点数
                    var curTempList = curProgramList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString())).ToList();
                    //服务器上的SQH为sqhItem.SQH及授权类型为临时的站点数
                    var curTempServerList = serverAllProgramList.Where(s => s.SQH == sqhItem.SQH && s.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString())).ToList();

                    if (curTempList.Count > 0)
                    {
                        //获取当前SQH为sqhItem.SQH的程序明细申请的授权类型为临时的总站点数
                        int curCounts = 0, addCounts = 0, delCounts = 0;
                        var curTempEntity = curTempList[0];
                        foreach (var infoItem in curTempList)
                        {
                            curCounts = curCounts + infoItem.NodeTableCounts;
                        }
                        #region 需要删除商业临时站点数
                        //需要删除的商业临时站点数=服务器的临时站点数-当前申请的临时授权站点数
                        if (curTempServerList.Count > curCounts)
                            delCounts = curTempServerList.Count - curCounts;
                        if (delCounts > 0)
                        {
                            //if (curCounts >= 1)
                            //    curCounts = curCounts - 1;
                            //else
                            //    curCounts = 0;
                            for (int i = curCounts; i < curTempServerList.Count; i++)
                            {
                                var delEntoty = curTempServerList[i];
                                delProgramList.Append(delEntoty.Id + ",");
                                //需要移除的临时授权程序明细
                                applyAllProgramList.Remove(delEntoty);
                            }
                        }
                        #endregion

                        #region 需要添加的临时授权站点数
                        //需要添加的临时授权站点数=当前申请的临时授权站点数-服务器的临时站点数
                        if (curCounts > curTempServerList.Count)
                            addCounts = curCounts - curTempServerList.Count;
                        if (addCounts > 0)
                        {
                            //var addEntoty = curTempList[0];
                            for (int i = 0; i < addCounts; i++)
                            {
                                AHServerProgramLicence addEntity = new AHServerProgramLicence();
                                addEntity.ServerLicenceID = editAHServer.Id;
                                addEntity.LicenceTypeId = curTempEntity.LicenceTypeId;
                                addEntity.LicenceDate = curTempEntity.LicenceDate;
                                addEntity.SQH = curTempEntity.SQH;
                                addEntity.ProgramID = curTempEntity.ProgramID;
                                addEntity.ProgramName = curTempEntity.ProgramName;
                                addEntity.IsUse = true;
                                tempAddProgramList.Add(addEntity);
                                //需要添加的临时授权程序明细
                                applyAllProgramList.Add(addEntity);
                            }
                        }
                        #endregion

                        #region 需要更新当前SQH为sqhItem.SQH的程序明细的授权日期
                        if (applyAllProgramList.Count > 0 && curTempServerList.Count > 0)
                        {
                            for (int i = 0; i < curTempServerList.Count; i++)
                            {
                                var editEntity = curTempServerList[i];
                                var tempList2 = applyAllProgramList.Where(p => p.Id == editEntity.Id);
                                if (tempList2.Count() > 0)
                                {
                                    int indexOf = applyAllProgramList.IndexOf(tempList2.ElementAt(0));
                                    editEntity.LicenceTypeId = curTempEntity.LicenceTypeId;
                                    editEntity.LicenceDate = curTempEntity.LicenceDate;
                                    editEntity.SQH = curTempEntity.SQH;
                                    editEntity.ProgramID = curTempEntity.ProgramID;
                                    editEntity.ProgramName = curTempEntity.ProgramName;
                                    editEntity.IsUse = true;
                                    applyAllProgramList[indexOf] = editEntity;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if (curTempServerList.Count > 0)
                        {
                            for (int i = 0; i < curTempServerList.Count; i++)
                            {
                                var delEntoty = curTempServerList[i];
                                delProgramList.Append(delEntoty.Id + ",");
                                //需要移除的临时授权程序明细
                                applyAllProgramList.Remove(delEntoty);
                            }
                        }
                    }
                    #endregion

                }
                //需要重新按SQH及授权类型排序和重新对SNo赋值
                applyAllProgramList = SetAHServerProgramLicenceSNo(applyAllProgramList);
                applyEntity.AHServerProgramLicenceList = applyAllProgramList;

                #endregion
            }
            #endregion

            #region 授权号处理
            if (brb.success)
            {
                bool isGetLicenceKey = false;
                if (editAHServer.Status.ToString() == LicenceStatus.商务授权通过.Key && editAHServer.IsSpecially == false)
                {
                    if (serverEntity.Status.ToString() == LicenceStatus.申请.Key || serverEntity.Status.ToString() == LicenceStatus.特批授权退回.Key)
                    {
                        isGetLicenceKey = true;
                    }
                }
                else if (editAHServer.Status.ToString() == LicenceStatus.特批授权通过.Key)
                {
                    if (serverEntity.Status.ToString() == LicenceStatus.商务授权通过.Key)
                    {
                        isGetLicenceKey = true;
                    }
                }
                if (isGetLicenceKey == true)
                {
                    BaseResultBool baseResultBool = new BaseResultBool();
                    //生成授权申请返回文件
                    baseResultBool = CreateAndSaveLicenceKeyFile(ref applyEntity);
                }
            }
            #endregion

            #region 新增程序明细信息或者修改程序明细信息的处理
            if (delProgramList.ToString().Length > 0)
            {
                var delArr = delProgramList.ToString().TrimEnd(',');
                int deleteCpunt = IBAHServerProgramLicence.DeleteByStrId(delArr);
                if (deleteCpunt < 1)
                {
                    brb.success = false;
                    errorInfo.Append("删除程序明细ID为(" + delArr + ")出错!" + Environment.NewLine);
                }
            }
            if (brb.success && applyEntity.AHServerProgramLicenceList != null && applyEntity.AHServerProgramLicenceList.Count > 0)
            {
                result = true;
                int sno = 0;
                foreach (AHServerProgramLicence item in applyEntity.AHServerProgramLicenceList)
                {
                    sno = sno + 1;
                    if (!item.SNo.HasValue)
                    {
                        item.SNo = sno;
                    }
                    //判断是否是否为新增的数据
                    var tempList2 = tempAddProgramList.Where(p => p.Id == item.Id);
                    IBAHServerProgramLicence.Entity = item;
                    if (tempList2.Count() > 0)
                    {
                        result = IBAHServerProgramLicence.Add();
                        if (result == false)
                        {
                            errorInfo.Append("添加程序明细为" + item.ProgramName + ",SQH为" + item.SQH + "保存出错!" + Environment.NewLine);
                        }
                    }
                    else
                    {
                        //编辑保存
                        List<string> tempList = new List<string>();
                        tempList.Add("Id=" + item.Id);
                        tempList.Add("SNo=" + item.SNo);

                        if (item.LicenceTypeId.HasValue)
                            tempList.Add("LicenceTypeId=" + item.LicenceTypeId);
                        else
                            tempList.Add("LicenceTypeId=null");

                        if (!String.IsNullOrEmpty(item.SQH))
                            tempList.Add("SQH='" + item.SQH + "'");

                        if (item.LicenceDate.HasValue)
                            tempList.Add("LicenceDate='" + item.LicenceDate + "'");
                        else
                            tempList.Add("LicenceDate=null");
                        if (!String.IsNullOrEmpty(item.LicenceKey1))
                            tempList.Add("LicenceKey1='" + item.LicenceKey1 + "'");
                        else
                            tempList.Add("LicenceKey1=null");
                        if (!String.IsNullOrEmpty(item.LicenceKey2))
                            tempList.Add("LicenceKey2='" + item.LicenceKey2 + "'");
                        else
                            tempList.Add("LicenceKey2=null");
                        var tempArray2 = tempList.ToArray();
                        IBAHServerProgramLicence.Entity = item;
                        result = IBAHServerProgramLicence.Update(tempArray2);
                        if (result == false)
                        {
                            errorInfo.Append("更新申请程序名称为" + item.ProgramName + ",SQH为" + item.SQH + "保存出错!" + Environment.NewLine);
                        }
                    }
                }
            }
            #endregion

            #region 修改仪器明细信息
            if (brb.success && applyEntity.AHServerEquipLicenceList != null && applyEntity.AHServerEquipLicenceList.Count > 0)
            {
                result = true;
                foreach (AHServerEquipLicence item in applyEntity.AHServerEquipLicenceList)
                {
                    if (result == false)
                    {
                        break;
                    }
                    item.ServerLicenceID = editAHServer.Id;
                    item.IsUse = true;
                    IBAHServerEquipLicence.Entity = item;
                    switch (item.ISNewApply)
                    {
                        case true:
                            result = IBAHServerEquipLicence.Add();
                            if (result == false)
                            {
                                errorInfo.Append("添加申请仪器名称为" + item.EquipName + ",SQH为" + item.SQH + "保存出错!" + Environment.NewLine);
                            }
                            break;
                        default:
                            List<string> tempList = new List<string>();
                            tempList.Add("Id=" + item.Id);
                            //tempList.Add("DispOrder=" + item.DispOrder);
                            if (item.SNo.HasValue)
                                tempList.Add("SNo=" + item.SNo);

                            if (item.LicenceTypeId.HasValue)
                                tempList.Add("LicenceTypeId=" + item.LicenceTypeId);
                            else
                                tempList.Add("LicenceTypeId=null");

                            if (!String.IsNullOrEmpty(item.SQH))
                                tempList.Add("SQH='" + item.SQH + "'");

                            if (item.LicenceDate.HasValue)
                                tempList.Add("LicenceDate='" + item.LicenceDate + "'");
                            else
                                tempList.Add("LicenceDate=null");

                            if (!String.IsNullOrEmpty(item.LicenceKey))
                                tempList.Add("LicenceKey='" + item.LicenceKey + "'");
                            else
                                tempList.Add("LicenceKey=null");
                            var tempArray2 = tempList.ToArray();
                            result = IBAHServerEquipLicence.Update(tempArray2);
                            if (result == false)
                            {
                                errorInfo.Append("更新申请仪器名称为" + item.EquipName + ",SQH为" + item.SQH + "保存出错!" + Environment.NewLine);
                            }
                            break;
                    }
                    brb.success = result;
                }
            }
            #endregion

            if (brb.success)
            {
                tempArray = tmpa.ToArray();
                brb.success = this.Update(tempArray);
                if (brb.success)
                {
                    brb.success = true;
                    SaveAHOperation(editAHServer);
                    AHServerLicenceStatusMessagePush(pushWeiXinMessageAction, serverEntity.Id, editAHServer.Status.ToString(), null);
                }
                else
                {
                    brb.ErrorInfo = "UpdateAHServerLicenceAndDetails.Update错误！";
                    brb.success = false;
                }
            }
            else
            {
                brb.ErrorInfo = "服务器授权ID：" + editAHServer.Id + "的状态为：" + LicenceStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "操作出错:" + errorInfo.ToString();
                ZhiFang.Common.Log.Log.Error(brb.ErrorInfo);
                brb.success = false;
            }
            return brb;
        }

        /// <summary>
        /// 修改服务器授权信息
        /// </summary>
        /// <param name="pushWeiXinMessageAction"></param>
        /// <param name="tempArray"></param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <returns></returns>
        public BaseResultBool AHServerLicenceStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long EmpID, string EmpName)
        {
            BaseResultBool brb = new BaseResultBool();
            AHServerLicence entity = this.Entity;
            var tmpa = tempArray.ToList();
            AHServerLicence tmpEntity = new AHServerLicence();
            tmpEntity = DBDao.Get(entity.Id);
            if (tmpEntity == null)
            {
                brb.ErrorInfo = "服务器授权ID：" + entity.Id + "为空！";
                brb.success = false;
                return brb;
            }

            if (!AHServerLicenceStatusUpdateCheck(entity, tmpEntity, EmpID, EmpName, tmpa))
            {
                return new BaseResultBool() { ErrorInfo = "服务器授权ID：" + entity.Id + "的状态为：" + LicenceStatus.GetStatusDic()[tmpEntity.Status.ToString()].Name + "！", success = false };
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                SaveAHOperation(entity);

                AHServerLicenceStatusMessagePush(pushWeiXinMessageAction, entity.Id, entity.Status.ToString(), null);
                brb.success = true;
            }
            else
            {
                brb.ErrorInfo = "AHServerLicenceStatusUpdate.Update错误！";
                brb.success = false;
            }
            return brb;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">前台传入的实体信息</param>
        /// <param name="tmpEntity">从服务器获取到的实体信息</param>
        /// <param name="EmpID"></param>
        /// <param name="EmpName"></param>
        /// <param name="tmpa">待更新保存的信息</param>
        /// <returns></returns>
        bool AHServerLicenceStatusUpdateCheck(AHServerLicence entity, AHServerLicence tmpEntity, long EmpID, string EmpName, List<string> tmpa)
        {
            #region 暂存
            if (entity.Status.ToString() == LicenceStatus.暂存.Key)
            {
                if (tmpEntity.Status.ToString() != LicenceStatus.暂存.Key && tmpEntity.Status.ToString() != LicenceStatus.申请.Key && tmpEntity.Status.ToString() != LicenceStatus.商务授权退回.Key)
                {
                    return false;
                }
            }
            #endregion

            #region 申请
            if (entity.Status.ToString() == LicenceStatus.申请.Key)
            {
                if (tmpEntity.Status.ToString() != LicenceStatus.暂存.Key && tmpEntity.Status.ToString() != LicenceStatus.商务授权退回.Key)
                {
                    return false;
                }

                tmpa.Add("ApplyID=" + EmpID + " ");
                tmpa.Add("ApplyName='" + EmpName + "'");
                tmpa.Add("ApplyDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OneAuditID=null");
                tmpa.Add("OneAuditName=null");
                tmpa.Add("OneAuditDataTime=null");
                tmpa.Add("OneAuditInfo=null");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");
                tmpa.Add("TwoAuditInfo=null");
                tmpa.Add("GenDateTime=null");
            }
            #endregion

            #region 商务授权通过
            if (entity.Status.ToString() == LicenceStatus.商务授权通过.Key)
            {
                if (tmpEntity.Status.ToString() != LicenceStatus.申请.Key && tmpEntity.Status.ToString() != LicenceStatus.特批授权退回.Key)
                {
                    return false;
                }
                tmpa.Add("OneAuditID=" + EmpID + " ");
                tmpa.Add("OneAuditName='" + EmpName + "'");
                tmpa.Add("OneAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OneAuditInfo='" + entity.OneAuditInfo + "'");

                tmpa.Add("TwoAuditID=null");
                tmpa.Add("TwoAuditName=null");
                tmpa.Add("TwoAuditDataTime=null");
                tmpa.Add("TwoAuditInfo=null");

                //判断是否需要特批,如果不需要特批,直接生成服务器授权码
                bool IsSpecially = entity.IsSpecially;
                if (IsSpecially == false)
                {
                    tmpa.Add("IsSpecially=0");
                    tmpa.Add("GenDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
                else
                {
                    tmpa.Add("IsSpecially=1");
                    tmpa.Add("GenDateTime=null");
                }
            }
            #endregion

            #region 商务授权退回
            if (entity.Status.ToString() == LicenceStatus.商务授权退回.Key)
            {
                if (tmpEntity.Status.ToString() != LicenceStatus.申请.Key && tmpEntity.Status.ToString() != LicenceStatus.特批授权退回.Key)
                {
                    return false;
                }
                tmpa.Add("OneAuditID=" + EmpID + " ");
                tmpa.Add("OneAuditName='" + EmpName + "'");
                tmpa.Add("OneAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OneAuditInfo='" + entity.OneAuditInfo + "'");
                tmpa.Add("IsSpecially=null");
                tmpa.Add("GenDateTime=null");

            }
            #endregion

            #region 特批授权通过
            if (entity.Status.ToString() == LicenceStatus.特批授权通过.Key)
            {
                if (tmpEntity.Status.ToString() != LicenceStatus.商务授权通过.Key)
                {
                    return false;
                }
                tmpa.Add("TwoAuditID=" + EmpID + " ");
                tmpa.Add("TwoAuditName='" + EmpName + "'");
                tmpa.Add("TwoAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoAuditInfo='" + entity.TwoAuditInfo + "'");
                tmpa.Add("GenDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            }
            #endregion

            #region 特批授权退回
            if (entity.Status.ToString() == LicenceStatus.特批授权退回.Key)
            {
                if (tmpEntity.Status.ToString() != LicenceStatus.商务授权通过.Key)
                {
                    return false;
                }
                tmpa.Add("TwoAuditID=" + EmpID + " ");
                tmpa.Add("TwoAuditName='" + EmpName + "'");
                tmpa.Add("TwoAuditDataTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("TwoAuditInfo='" + entity.TwoAuditInfo + "'");
                tmpa.Add("GenDateTime=null");
                tmpa.Add("IsSpecially=null");
            }
            #endregion

            return true;
        }
        private void AHServerLicenceStatusMessagePush(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id, string StatusId, AHServerLicence entity)
        {
            List<long> receiveidlist = new List<long>();
            string message = "";
            string sendmen = "";
            AHServerLicence ahserverlicence = entity;
            if (ahserverlicence == null)
                ahserverlicence = DBDao.Get(id);
            //string url = "WeiXin/WeiXinMainRouter.aspx?operate=TASKINFO&id=" + PTaskId + "&IsSingle=1" + "&name=" + ptask.CName;
            //string tmpstr = "";
            //if (ahserverlicence. != null && ahserverlicence.ProgramName.Trim() != "")
            //{
            //    tmpstr = ahserverlicence.ProgramName;
            //}
            //else
            //{
            //    tmpstr = ahserverlicence.EquipName;
            ////}
            //ahserverlicence.LicenceTypeName = LicenceType.GetStatusDic()[ahserverlicence.LicenceTypeId.Value.ToString()].Name;

            #region 申请
            if (StatusId.Trim() == LicenceStatus.暂存.Key)
            {

            }
            if (StatusId.Trim() == LicenceStatus.申请.Key)
            {
                IList<RBACEmpRoles> rbacerlist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id in( " + RoleList.授权审核.Key + ") ");
                if (rbacerlist != null && rbacerlist.Count() > 0)
                {
                    foreach (RBACEmpRoles rbacer in rbacerlist)
                    {
                        receiveidlist.Add(rbacer.HREmployee.Id);
                    }
                }
                message = "您收到待审核的授权申请（客户名称：" + ahserverlicence.PClientName + "，授权类型：文件授权），申请人：" + ahserverlicence.ApplyName + "。";
            }
            #endregion

            #region 商务授权通过
            if (StatusId.Trim() == LicenceStatus.商务授权通过.Key)
            {
                receiveidlist.Add(ahserverlicence.ApplyID.Value);
                message = "您的授权申请（客户名称：" + ahserverlicence.PClientName + "，授权类型：文件授权）,已被" + ahserverlicence.OneAuditName + "定为'商务授权通过'状态。";
            }
            #endregion

            #region 商务授权退回
            if (StatusId.Trim() == LicenceStatus.商务授权退回.Key)
            {
                receiveidlist.Add(ahserverlicence.ApplyID.Value);
                message = "您的授权申请（客户名称：" + ahserverlicence.PClientName + "，授权类型：文件授权）,已被" + ahserverlicence.OneAuditName + "定为'商务授权退回'状态。";
            }
            #endregion

            #region 特批授权通过
            if (StatusId.Trim() == LicenceStatus.特批授权通过.Key)
            {
                receiveidlist.Add(ahserverlicence.ApplyID.Value);
                message = "您的授权申请（客户名称：" + ahserverlicence.PClientName + "，授权类型：文件授权）,已被" + ahserverlicence.TwoAuditName + "定为'商务授权通过'状态。";
            }
            #endregion

            #region 特批授权退回
            if (StatusId.Trim() == LicenceStatus.特批授权退回.Key)
            {
                receiveidlist.Add(ahserverlicence.OneAuditID.Value);
                message = "您的授权申请（客户名称：" + ahserverlicence.PClientName + "，授权类型：文件授权）,已被" + ahserverlicence.TwoAuditName + "定为'特批授权退回'状态。";
            }
            #endregion

            #region 授权完成
            if (StatusId.Trim() == LicenceStatus.授权完成.Key)
            {
                receiveidlist.Add(ahserverlicence.ApplyID.Value);
                message = "您的授权申请（客户名称：" + ahserverlicence.PClientName + "，授权类型：文件授权）,已被" + ahserverlicence.OneAuditName + "定为'商务授权通过'状态。";
            }
            #endregion

            if (receiveidlist.Count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:" + receiveidlist.Count);
                Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                string urgencycolor = "#11cd6e";
                data.Add("first", new TemplateDataObject() { color = "#1d73cd", value = "你收到授权申请信息" });
                data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = message });
                data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = "OA系统：授权申请" });
                data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = ahserverlicence.ApplyName });
                string tmpdatetime = (ahserverlicence.DataAddTime.HasValue) ? ahserverlicence.DataAddTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:@" + receiveidlist.Count);
                string url = "";
                if ((ahserverlicence.IsSpecially && ahserverlicence.Status.ToString() == LicenceStatus.特批授权通过.Key) || (!ahserverlicence.IsSpecially && ahserverlicence.Status.ToString() == LicenceStatus.商务授权通过.Key))
                {
                    url = "WeiXin/WeiXinMainRouter.aspx?operate=AHSERVERLICENCEINFO&id=" + ahserverlicence.Id;
                }
                IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "licence", url);
                //ZhiFang.Common.Log.Log.Debug("PContractStatusMessagePush.receiveidlist.Count:@@" + receiveidlist.Count);
            }
        }
        /// <summary>
        /// 获取服务器授权信息及明细信息数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchAHServerLicenceAndAndDetailsById(long id)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ApplyAHServerLicence applyAHServerLicence = new ApplyAHServerLicence();
            applyAHServerLicence = GetAHServerLicenceAndAndDetailsById(id);
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            brdv.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(applyAHServerLicence);
            return brdv;
        }

        public ApplyAHServerLicence GetAHServerLicenceAndAndDetailsById(long id)
        {
            ApplyAHServerLicence applyAHServerLicence = new ApplyAHServerLicence();
            List<AHServerProgramLicence> programLists = new List<AHServerProgramLicence>();
            IList<AHServerEquipLicence> equipLists = new List<AHServerEquipLicence>();
            IList<LicenceProgramType> programTypeLists = new List<LicenceProgramType>();
            applyAHServerLicence.ApplyProgramInfoList = new List<ApplyProgramInfo>();
            AHServerLicence ahserverLicence = new AHServerLicence();
            ahserverLicence = this.Get(id);
            var licenceTypelist = LicenceType.GetStatusDic();

            #region 程序明细
            programLists = (List<AHServerProgramLicence>)IBAHServerProgramLicence.SearchListByHQL("ServerLicenceID=" + id);
            if (programLists.Count > 0)
            {
                programLists = programLists.OrderBy(a => a.SNo).ToList();
                for (int i = 0; i < programLists.Count; i++)
                {
                    programLists[i] = SetAHServerProgramLicenceStatus(licenceTypelist, programLists[i]);
                }

                //SQH滤重
                var q = programLists.GroupBy(p => new { p.ServerLicenceID, p.SQH }).Where(x => x.Count() > 1).ToList();

                //ZhiFang.Common.Log.Log.Debug("程序类型有:" + q.Count);
                foreach (var item in q)
                {
                    LicenceProgramType tempEntity = new LicenceProgramType();
                    tempEntity.Id = long.Parse(item.Key.ServerLicenceID.ToString());
                    tempEntity.SQH = item.Key.SQH;
                    switch (item.Key.SQH)
                    {
                        case "2000":
                            tempEntity.CName = "检验之星";
                            tempEntity.Code = "PTech";
                            break;
                        default:
                            tempEntity.CName = "其他程序";
                            tempEntity.Code = "OThers";
                            break;
                    }
                    programTypeLists.Add(tempEntity);
                }
            }
            else
            {
                LicenceProgramType tempEntity = new LicenceProgramType();
                tempEntity.Id = id;
                tempEntity.CName = "检验之星";
                tempEntity.Code = "PTech";
                tempEntity.SQH = "2000";
                programTypeLists.Add(tempEntity);
            }
            #endregion

            #region 仪器通讯程序
            var tmpahserverlicence = DBDao.GetListByHQL(" PClientID =" + ahserverLicence.PClientID + " and Status in (" + LicenceStatus.商务授权通过.Key + "," + LicenceStatus.特批授权通过.Key + ") order by GenDateTime desc ");
            IList<AHServerEquipLicence> tmpahequiplist = null;
            if (tmpahserverlicence != null && tmpahserverlicence.Count > 0)
                tmpahequiplist = IBAHServerEquipLicence.SearchListByHQL(" ServerLicenceID=" + tmpahserverlicence.ElementAt(0).Id + " ");

            equipLists = IBAHServerEquipLicence.SearchListByHQL("ServerLicenceID=" + id);
            if (equipLists.Count > 0)
            {
                equipLists = equipLists.OrderBy(a => a.SNo).ToList();
                for (int i = 0; i < equipLists.Count; i++)
                {
                    equipLists[i] = SetAHServerEquipLicenceStatus(licenceTypelist, equipLists[i]);
                    if (tmpahequiplist != null && tmpahequiplist.Count > 0)
                    {
                        var ahequip = tmpahequiplist.Where(a => a.SNo == equipLists[i].SNo);
                        if (ahequip != null && ahequip.Count() > 0)
                        {
                            equipLists[i].AHBeforeSQH = ahequip.ElementAt(0).SQH;
                            if (ahequip.ElementAt(0).SQH == equipLists[i].SQH)
                            {
                                //equipLists[i].AHBeforeDateTime = tmpahserverlicence.ElementAt(0).GenDateTime.HasValue ? tmpahserverlicence.ElementAt(0).GenDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                                equipLists[i].AHBeforeDateTime = ahequip.ElementAt(0).LicenceDate.HasValue ? ahequip.ElementAt(0).LicenceDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                                equipLists[i].CalcDays = ahequip.ElementAt(0).LicenceDate.HasValue ? ahequip.ElementAt(0).LicenceDate.Value.Subtract(DateTime.Now).Days : 0;
                                equipLists[i].AHBeforeLicenceTypeId = ahequip.ElementAt(0).LicenceTypeId;
                                equipLists[i].AHBeforeLicenceTypeName = ahequip.ElementAt(0).LicenceTypeId.HasValue ? LicenceType.GetStatusDic()[ahequip.ElementAt(0).LicenceTypeId.ToString()].Name : "";
                            }
                        }







                        //var ahequip = tmpahequiplist.Where(a => a.SQH == equipLists[i].SQH && a.SNo == equipLists[i].SNo);
                        //if (ahequip != null && ahequip.Count() > 0)
                        //{
                        //    //equipLists[i].AHBeforeDateTime = tmpahserverlicence.ElementAt(0).GenDateTime.HasValue ? tmpahserverlicence.ElementAt(0).GenDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                        //    equipLists[i].AHBeforeDateTime = ahequip.ElementAt(0).LicenceDate.HasValue ? ahequip.ElementAt(0).LicenceDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";
                        //    equipLists[i].CalcDays = ahequip.ElementAt(0).LicenceDate.HasValue ? ahequip.ElementAt(0).LicenceDate.Value.Subtract(DateTime.Now).Days : 0;
                        //    equipLists[i].AHBeforeLicenceTypeId = ahequip.ElementAt(0).LicenceTypeId;
                        //    equipLists[i].AHBeforeLicenceTypeName = ahequip.ElementAt(0).LicenceTypeId.HasValue ? LicenceType.GetStatusDic()[ahequip.ElementAt(0).LicenceTypeId.ToString()].Name : "";
                        //}
                    }
                }

                LicenceProgramType tempEntity = new LicenceProgramType();
                tempEntity.Id = ahserverLicence.Id;
                tempEntity.Code = "BEquip";
                tempEntity.CName = "仪器通讯程序";
                programTypeLists.Add(tempEntity);
            }
            else
            {
                LicenceProgramType tempEntity = new LicenceProgramType();
                tempEntity.Id = id;
                tempEntity.Code = "BEquip";
                tempEntity.CName = "仪器通讯程序";
                programTypeLists.Add(tempEntity);
            }
            #endregion

            applyAHServerLicence.AHServerLicence = ahserverLicence;
            applyAHServerLicence.LicenceProgramTypeList = programTypeLists;
            applyAHServerLicence.AHServerEquipLicenceList = equipLists;

            #region 程序明细二次处理
            //按程序SQH将上传程序明细信息及上一次申请主程序明细信息分组处理为商业及临时
            applyAHServerLicence.ApplyProgramInfoList = GetApplyProgramInfoList(programLists, ahserverLicence);
            #endregion

            applyAHServerLicence.LRNoIsIdentical = true;
            if (applyAHServerLicence.AHServerLicence.PClientID.HasValue)
            {
                PClient pclient = IDPClientDao.Get(long.Parse(applyAHServerLicence.AHServerLicence.PClientID.ToString()));
                if (pclient != null)
                {
                    applyAHServerLicence.PClientLRNo1 = pclient.LRNo1;
                    applyAHServerLicence.PClientLRNo2 = pclient.LRNo2;
                    if (applyAHServerLicence.AHServerLicence.LRNo1 != pclient.LRNo1) applyAHServerLicence.LRNoIsIdentical = false;
                    if (applyAHServerLicence.AHServerLicence.LRNo2 != pclient.LRNo2) applyAHServerLicence.LRNoIsIdentical = false;
                }
            }
            return applyAHServerLicence;
        }

        /// <summary>
        /// 获取服务器授权需要特批的数据
        /// </summary>
        /// <param name="where"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<AHServerLicence> SearchSpecialApprovalAHServerLicenceByHQL(string where, int page, int limit, string sort)
        {
            EntityList<AHServerLicence> tempEntityList = new EntityList<AHServerLicence>();
            //需要特批的默认条件处理
            string hqlWhere = " IsSpecially=1 ";

            if (String.IsNullOrEmpty(hqlWhere))
            {
                hqlWhere = where;
            }
            else if (!String.IsNullOrEmpty(where) && !String.IsNullOrEmpty(hqlWhere))
            {
                hqlWhere = hqlWhere + " and " + where;
            }
            tempEntityList = ((IDAHServerLicenceDao)base.DBDao).SearchSpecialApprovalAHServerLicenceByHQL(hqlWhere, page, limit, sort);
            return tempEntityList;
        }
        /// <summary>
        /// 生成并保存服务器授权申请返回文件
        /// </summary>
        /// <param name="entity"></param>
        public BaseResultBool CreateAndSaveLicenceKeyFile(ref ApplyAHServerLicence applyEntity)
        {
            BaseResultBool brb = new BaseResultBool();
            brb.success = true;
            try
            {
                IList<AHServerProgramLicence> programLists = new List<AHServerProgramLicence>();
                programLists = applyEntity.AHServerProgramLicenceList;

                IList<AHServerEquipLicence> equipLists = new List<AHServerEquipLicence>();
                equipLists = applyEntity.AHServerEquipLicenceList;
                if (equipLists != null)
                    equipLists = equipLists.OrderBy(a => a.SNo).ThenBy(a => a.LicenceTypeId).ToList();
                //客户服务号
                string licenceCode = "";
                if (applyEntity.AHServerLicence.PClientID.HasValue)
                {
                    PClient pclient = IDPClientDao.Get(long.Parse(applyEntity.AHServerLicence.PClientID.ToString()));
                    if (pclient != null)
                    {
                        licenceCode = pclient.LicenceCode;
                    }
                }

                #region 生成授权文件保存
                string newFilePath = GetAHServerLicenceKeyFilePath(applyEntity.AHServerLicence);
                StringBuilder info = new StringBuilder();

                #region 第一部分信息
                info.Append(applyEntity.AHServerLicence.PClientName + "授权返回" + Environment.NewLine);
                info.Append(Environment.NewLine);

                info.Append("主服务器授权申请号:  " + applyEntity.AHServerLicence.LRNo1 + Environment.NewLine);
                info.Append("备份服务器授权申请号:" + applyEntity.AHServerLicence.LRNo2 + Environment.NewLine);
                info.Append("客户服务编号:        " + licenceCode + Environment.NewLine);
                info.Append("客户显示名称:        " + applyEntity.AHServerLicence.PClientName + Environment.NewLine);
                info.Append("PTechSQH:            " + "2000" + Environment.NewLine);
                info.Append(Environment.NewLine);
                #endregion

                string licenceTypeId = "", pLicenceType = "", LRNo1 = "", LRNo2 = "", clientName = "", pSQH = "", pSNo = "";
                DateTime pENDDate = DateTime.Now;
                var licenceTypelist = LicenceType.GetStatusDic();

                #region 程序明细授权信息
                //SQH滤重
                if (programLists != null)
                {
                    var sqhLists = programLists.GroupBy(p => new { p.ServerLicenceID, p.SQH }).Where(x => x.Count() > 1).ToList();
                    foreach (var sqhItem in sqhLists)
                    {
                        var tempLists = programLists.Where(p => p.SQH == sqhItem.Key.SQH);
                        if (tempLists != null)
                            tempLists = tempLists.OrderBy(a => a.SNo).ToList();
                        switch (sqhItem.Key.SQH)
                        {
                            case "2000":
                                //主程序授权
                                info.Append("****主程序授权****" + Environment.NewLine);
                                info.Append(Environment.NewLine);

                                break;
                            default:
                                //其他程序授权
                                info.Append("****其他程序授权****" + Environment.NewLine);
                                info.Append(Environment.NewLine);
                                break;
                        }
                        #region 行明细处理
                        info.Append("---------------------------------------------------------------------------------" + Environment.NewLine);
                        info.Append("序号  授权类型 截止日期    主服务器授权号             备份服务器授权号           " + Environment.NewLine);
                        info.Append("---------------------------------------------------------------------------------" + Environment.NewLine);

                        foreach (var item in tempLists)
                        {
                            #region 授权号生成处理
                            pENDDate = DateTime.Now;

                            clientName = applyEntity.AHServerLicence.PClientName;
                            if (item.LicenceTypeId.HasValue)
                            {
                                licenceTypeId = item.LicenceTypeId.ToString();// 
                                pLicenceType = licenceTypelist[item.LicenceTypeId.ToString()].Name; ;
                            }
                            else
                            {
                                licenceTypeId = "";
                                pLicenceType = "";
                            }
                            pSQH = item.SQH;
                            pSNo = item.SNo.Value.ToString();
                            if (item.LicenceTypeId.HasValue && item.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString()) && item.LicenceDate.HasValue)
                                pENDDate = DateTime.Parse(item.LicenceDate.Value.ToString("yyyy-MM-dd"));
                            //调用生成授权号的接口获取授权号信息

                            #region 主服务器授权号
                            LRNo1 = applyEntity.AHServerLicence.LRNo1;
                            string licenceKey1 = "";
                            //licenceKey1 = "6443-6209-4180-0161-53242";

                            if (!String.IsNullOrEmpty(LRNo1))
                                licenceKey1 = CreatServerLicence(licenceTypeId, licenceCode, LRNo1, clientName, pSQH, pSNo, pENDDate);

                            programLists[programLists.IndexOf(item)].LicenceKey1 = licenceKey1;
                            item.LicenceKey1 = licenceKey1;

                            if (String.IsNullOrEmpty(licenceKey1))
                                licenceKey1 = "                        ";
                            #endregion
                            #region 备份服务器授权号
                            LRNo2 = applyEntity.AHServerLicence.LRNo2;
                            string licenceKey2 = "";
                            //licenceKey2 = "6443-6209-4180-0161-5324";

                            if (!String.IsNullOrEmpty(LRNo2))
                                licenceKey2 = CreatServerLicence(licenceTypeId, licenceCode, LRNo2, clientName, pSQH, pSNo, pENDDate);

                            programLists[programLists.IndexOf(item)].LicenceKey2 = licenceKey2;
                            item.LicenceKey2 = licenceKey2;
                            if (String.IsNullOrEmpty(licenceKey2))
                                licenceKey2 = "                        ";
                            #endregion
                            #endregion

                            #region 每一行的列信息处理
                            //序号
                            //每一列的空格补位
                            string spaces = "";
                            int snoLength = 0;
                            if (item.SNo.HasValue)
                            {
                                snoLength = item.SNo.Value.ToString().Length;
                            }
                            switch (snoLength)
                            {
                                case 1:
                                    spaces = "     ";
                                    break;
                                case 2:
                                    spaces = "    ";
                                    break;
                                case 3:
                                    spaces = "   ";
                                    break;
                                case 4:
                                    spaces = "  ";
                                    break;
                                default:
                                    break;
                            }
                            info.Append(item.SNo.Value.ToString());
                            info.Append(spaces);

                            //授权类型
                            spaces = "     ";
                            info.Append(pLicenceType);
                            info.Append(spaces);

                            //截止日期
                            spaces = "  ";
                            string licenceDate = "";
                            if (item.LicenceTypeId.HasValue && item.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString()) && item.LicenceDate.HasValue)
                                licenceDate = item.LicenceDate.Value.ToString("yyyy-MM-dd");
                            else
                            {
                                licenceDate = "          ";
                            }
                            info.Append(licenceDate);
                            info.Append(spaces);

                            //主服务器授权号
                            spaces = "   ";
                            if (String.IsNullOrEmpty(licenceKey1))
                                licenceKey1 = "                        ";
                            info.Append(licenceKey1);
                            info.Append(spaces);

                            //备份服务器授权号
                            spaces = "   ";
                            if (String.IsNullOrEmpty(licenceKey2))
                                licenceKey2 = "                        ";
                            info.Append(licenceKey2);
                            info.Append(spaces);

                            info.Append(Environment.NewLine);
                            #endregion
                        }
                        #endregion

                        info.Append("---------------------------------------------------------------------------------" + Environment.NewLine);
                    }
                }
                else
                {
                    //主程序授权
                    info.Append("****主程序授权****" + Environment.NewLine);
                    info.Append(Environment.NewLine);
                    info.Append("---------------------------------------------------------------------------------" + Environment.NewLine);
                    info.Append("序号  授权类型 截止日期    主服务器授权号             备份服务器授权号           " + Environment.NewLine);
                    info.Append("---------------------------------------------------------------------------------" + Environment.NewLine);

                    info.Append("---------------------------------------------------------------------------------" + Environment.NewLine);
                }
                #endregion

                #region 仪器明细授权信息
                info.Append(Environment.NewLine);
                info.Append("****通讯程序授权****" + Environment.NewLine);
                info.Append(Environment.NewLine);
                info.Append("------------------------------------------------------------------------------------------" + Environment.NewLine);
                info.Append("序号 用户程序名     系统程序名     SQH    授权类型 截止日期    授权号                     " + Environment.NewLine);
                info.Append("------------------------------------------------------------------------------------------" + Environment.NewLine);
                #region 每一行明细处理
                if (equipLists != null)
                {
                    //equipLists = equipLists.OrderBy(p => p.SNo).ToList();
                    foreach (var item in equipLists)
                    {
                        #region 授权号生成处理
                        pENDDate = DateTime.Now;
                        string LRNo = applyEntity.AHServerLicence.LRNo;
                        clientName = applyEntity.AHServerLicence.PClientName;
                        if (item.LicenceTypeId.HasValue)
                        {
                            licenceTypeId = item.LicenceTypeId.ToString();// 
                            pLicenceType = licenceTypelist[item.LicenceTypeId.ToString()].Name; ;
                        }
                        else
                        {
                            licenceTypeId = "";
                            pLicenceType = "";
                        }
                        pSQH = item.SQH;
                        pSNo = item.SNo.Value.ToString();
                        if (item.LicenceTypeId.HasValue && item.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString()) && item.LicenceDate.HasValue)
                            pENDDate = DateTime.Parse(item.LicenceDate.Value.ToString("yyyy-MM-dd"));
                        //调用生成授权号的接口获取授权号信息 "";// 
                        string licenceKey = "";
                        //licenceKey = "6443-6209-4180-0161-5324";

                        if (!String.IsNullOrEmpty(LRNo))
                            licenceKey = CreatServerLicence(licenceTypeId, licenceCode, "", clientName, pSQH, pSNo, pENDDate);

                        equipLists[equipLists.IndexOf(item)].LicenceKey = licenceKey;
                        item.LicenceKey = licenceKey;
                        if (String.IsNullOrEmpty(licenceKey))
                            licenceKey = "                        ";
                        #endregion

                        #region 每一行的列信息处理
                        //序号

                        //每一列的空格补位
                        string spaces = "";
                        int snoLength = 0;
                        if (item.SNo.HasValue)
                        {
                            snoLength = item.SNo.Value.ToString().Length;
                        }
                        switch (snoLength)
                        {
                            case 1:
                                spaces = "    ";
                                break;
                            case 2:
                                spaces = "   ";
                                break;
                            case 3:
                                spaces = "  ";
                                break;
                            case 4:
                                spaces = " ";
                                break;
                            default:
                                break;
                        }
                        if (item.SNo.HasValue)
                        {
                            info.Append(item.SNo.Value.ToString());
                        }
                        info.Append(spaces);

                        //用户程序名
                        spaces = " ";
                        string programName = item.ProgramName;
                        if (String.IsNullOrEmpty(programName))
                        {
                            spaces = "";
                            programName = "              ";
                        }
                        else
                        {
                            GetChangeStr(ref spaces, ref programName);
                        }
                        info.Append(programName);
                        info.Append(spaces);

                        //系统程序名
                        string spaces2 = " ";
                        string equipversion = item.Equipversion;
                        if (String.IsNullOrEmpty(equipversion))
                        {
                            spaces = "";
                            equipversion = "              ";
                        }
                        else
                        {
                            GetChangeStr(ref spaces2, ref equipversion);
                        }
                        info.Append(equipversion);
                        info.Append(spaces2);

                        //SQH
                        spaces = "   ";
                        string sqh = item.SQH;
                        if (String.IsNullOrEmpty(sqh))
                        {
                            sqh = "    ";
                        }
                        info.Append(sqh);
                        info.Append(spaces);

                        //授权类型
                        spaces = "     ";
                        if (String.IsNullOrEmpty(pLicenceType))
                        {
                            pLicenceType = "    ";
                        }
                        info.Append(pLicenceType);
                        info.Append(spaces);

                        //截止日期
                        spaces = "  ";
                        string licenceDate = "";
                        if (item.LicenceTypeId.HasValue && item.LicenceTypeId == int.Parse(LicenceType.临时.Key.ToString()) && item.LicenceDate.HasValue)
                            licenceDate = item.LicenceDate.Value.ToString("yyyy-MM-dd");
                        else
                        {
                            licenceDate = "          ";
                        }
                        info.Append(licenceDate);
                        info.Append(spaces);

                        //授权号
                        spaces = "   ";
                        if (String.IsNullOrEmpty(licenceKey))
                        {
                            licenceKey = "                        ";
                        }
                        info.Append(licenceKey);
                        info.Append(spaces);

                        info.Append(Environment.NewLine);
                        #endregion
                    }
                }
                #endregion

                info.Append("------------------------------------------------------------------------------------------" + Environment.NewLine);
                #endregion

                //byte[] infoByte = System.Text.Encoding.UTF8.GetBytes(info.ToString());

                byte[] infoByte = Encoding.Default.GetBytes(info.ToString());
                using (FileStream fsWrite = new FileStream(newFilePath, FileMode.Create, FileAccess.Write))
                {
                    fsWrite.Write(infoByte, 0, infoByte.Length);
                    fsWrite.Dispose();
                    fsWrite.Close();
                    info.Clear();
                };
                #endregion

                applyEntity.AHServerProgramLicenceList = programLists;
                applyEntity.AHServerEquipLicenceList = equipLists;
            }
            catch (Exception ee)
            {
                brb.success = false;
                brb.ErrorInfo = ee.ToString();

            }

            if (brb.success)
            {
                //UpdatePClientLRNo(applyEntity.AHServerLicence);
            }
            return brb;
        }
        /// <summary>
        /// 获得处理后的用户程序名或系统程序名
        /// equipversion长度为15,长度包括空格补位的spaces值
        /// </summary>
        /// <param name="spaces">空格补位</param>
        /// <param name="equipversion"></param>
        private void GetChangeStr(ref string spaces, ref string equipversion)
        {
            int strLength = 14;
            //统计站点名称有几个汉字,长度需要动态调整
            int hanNum = GetHanNumFromString(equipversion);
            if (String.IsNullOrEmpty(equipversion))
            {
                spaces = " ";
                equipversion = "             ";
            }
            else if (hanNum < 1)//没有汉字的处理
            {
                int lengthStr = equipversion.Length;
                spaces = "  ";
                if (lengthStr > strLength)
                {
                    lengthStr = strLength;
                }
                else if (lengthStr < strLength)
                {
                    for (int i = lengthStr; i < strLength; i++)
                    {
                        spaces = spaces + " ";
                    }
                }
                equipversion = equipversion.Substring(0, lengthStr);// - 1
                string tempStr = equipversion + spaces;
                if (tempStr.Length > strLength)
                {
                    spaces = " ";
                    equipversion = tempStr.Substring(0, 14);
                }
                else if (tempStr.Length < strLength)
                {
                    equipversion = equipversion + " ";
                    GetChangeStr(ref spaces, ref equipversion);
                }
            }
            else if (hanNum > 0)//有汉字的处理
            {
                spaces = " ";
                int lengthStr2 = GetStrOfhanLength(equipversion);
                if (lengthStr2 > strLength)
                {
                    spaces = " ";
                    equipversion = equipversion.Substring(0, equipversion.Length - 1);
                    GetChangeStr(ref spaces, ref equipversion);
                }
                else if (lengthStr2 < strLength)
                {
                    equipversion = equipversion + " ";
                    GetChangeStr(ref spaces, ref equipversion);
                }
            }
        }
        /// <summary>
        /// 获取字符串的长度(一个汉字的长度为2)
        /// </summary>
        /// <param name="equipversion"></param>
        /// <returns></returns>
        private int GetStrOfhanLength(string equipversion)
        {
            int hanNum = GetHanNumFromString(equipversion);
            int lengthStr = equipversion.Length;
            //double hanNum2 = Math.Round((double.Parse(hanNum.ToString()) / 2), 2);
            ////小数点处理
            //double decimalNum = hanNum2 - Math.Truncate(hanNum2);
            //if (decimalNum > 0)
            //{
            //    hanNum2 = Math.Truncate(hanNum2) + 1;
            //}
            int lengthStr2 = (int)(lengthStr + hanNum);
            return lengthStr2;
        }

        /// <summary>
        /// 获取服务器授权文件导出的路径
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        private string GetAHServerLicenceKeyFilePath(AHServerLicence applyEntity)
        {
            string filePath = "", basePath = "", fileName = applyEntity.Id + ".txt";
            //一级保存路径
            basePath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
            basePath = basePath + "\\" + "AHServerLicence\\";
            filePath = basePath + "服务器授权文件导出\\";
            if (!String.IsNullOrEmpty(applyEntity.PClientName))
            {
                filePath = filePath + applyEntity.PClientName + "\\";
            }
            if (applyEntity.ApplyDataTime.HasValue)
            {
                filePath = filePath + applyEntity.ApplyDataTime.Value.ToString("yyyy年MM月dd") + "\\";
            }
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            string newFilePath = Path.Combine(filePath, fileName);
            return newFilePath;
        }

        /// <summary>
        /// 需要重新按SQH及授权类型排序和重新对SNo赋值更新
        /// </summary>
        /// <param name="programLicenceList"></param>
        /// <returns></returns>
        private IList<AHServerProgramLicence> SetAHServerProgramLicenceSNo(IList<AHServerProgramLicence> programLicenceList)
        {
            if (programLicenceList != null)
            {
                programLicenceList = programLicenceList.OrderBy(a => a.SQH).ThenBy(a => a.LicenceTypeId).ToList();
                int SNo = 0;
                //SQH滤重
                var sqhLists = programLicenceList.GroupBy(p => new { p.ServerLicenceID, p.SQH }).Where(x => x.Count() > 1).ToList();
                foreach (var sqhItem in sqhLists)
                {
                    SNo = 0;
                    var tempLists = programLicenceList.Where(p => p.SQH == sqhItem.Key.SQH);
                    if (tempLists != null)
                    {
                        tempLists = tempLists.OrderBy(a => a.LicenceTypeId).ToList();
                        foreach (var item in tempLists)
                        {
                            SNo = SNo + 1;
                            programLicenceList[programLicenceList.IndexOf(item)].SNo = SNo;
                        }
                    }
                }
            }
            return programLicenceList;
        }

        /// <summary>
        /// 获取单个服务器明细的授权码
        /// </summary>
        /// <param name="pLicenceType">商业、临时</param>
        /// <param name="serverNo">服务号</param>
        /// <param name="Mac">服务器授权申请号(硬件) </param>
        /// <param name="clientName">客户名称</param>
        /// <param name="pSQH">SQH号</param>
        /// <param name="pSNo">序号</param>
        /// <param name="pENDDate">结束时间</param>
        /// <returns></returns>
        private string CreatServerLicence(string pLicenceType, string serverNo, string Mac, string clientName, string pSQH, string pSNo, DateTime pENDDate)
        {
            ZhiFang.Common.Log.Log.Debug("CreatServerLicenceKey参数：pLicenceType=" + pLicenceType.ToString() + ";serverNo=" + serverNo + ";Mac=" + Mac + ";clientName=" + clientName + ";pSQH=" + pSQH + ";pSNo=" + pSNo + ";pENDDate=" + pENDDate.ToString("yyyy-MM-dd") + ";");
            if (pLicenceType != null && pLicenceType.Trim() != "")
            {
                OALSR.OALClient tmp = new OALSR.OALClient();
                string licenceKey = tmp.GetNumLicence(pLicenceType, serverNo, Mac, clientName, pSQH, pSNo, pENDDate.ToString("yyyy-MM-dd"));
                switch (licenceKey.Trim())
                {
                    case "-1": ZhiFang.Common.Log.Log.Debug("CreatServerLicence错误:" + licenceKey); return "";
                    case "-2": ZhiFang.Common.Log.Log.Debug("CreatServerLicence错误:" + licenceKey); return "";
                    case "-3": ZhiFang.Common.Log.Log.Debug("CreatServerLicence错误:" + licenceKey); return "";
                }
                ZhiFang.Common.Log.Log.Debug("CreatServerLicenceKey参数：pLicenceType=" + pLicenceType.ToString() + ";serverNo=" + serverNo + ";Mac=" + Mac + ";clientName=" + clientName + ";pSQH=" + pSQH + ";pSNo=" + pSNo + ";pENDDate=" + pENDDate.ToString("yyyy-MM-dd") + ";licenceKey:" + licenceKey);
                return licenceKey;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetCreateLicenceKey错误,LicenceTypeId为空！");
                return "";
            }
        }
        /// <summary>
        /// 下载服务器授权码文件
        /// </summary>
        /// <param name="entity"></param>
        public FileStream DownLoadAHServerLicenceFile(long id, ref string fileName)
        {
            AHServerLicence applyEntity = this.Get(id);
            string newFilePath = "";
            if (applyEntity == null)
            {
                ZhiFang.Common.Log.Log.Error("服务器授权的ID为:" + id + "不存在");
                return null;
            }
            else
            {
                //重新生成授权申请返回文件(临时用的重新生成)
                //BaseResultBool brb =RegenerateAHServerLicenceById(id);
                newFilePath = GetAHServerLicenceKeyFilePath(applyEntity);
            }
            fileName = applyEntity.PClientName + "授权返回.txt";
            if (String.IsNullOrEmpty(fileName))
            {
                fileName = id + ".txt";
            }
            FileStream fileStream = null;
            if (File.Exists(newFilePath))
            {
                fileStream = new FileStream(newFilePath, FileMode.Open, FileAccess.Read);
                //fileStream.Close();
            }
            else
            {
                fileStream = new FileStream(newFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                ZhiFang.Common.Log.Log.Error("服务器授权文件不存在!" + newFilePath);
            }
            return fileStream;
        }
        /// <summary>
        /// 授权操作记录登记
        /// </summary>
        /// <param name="entityLicence"></param>
        private void SaveAHOperation(AHServerLicence entityLicence)
        {
            AHServerLicence entity = this.Entity;
            if (entityLicence != null)
            {
                entity = entityLicence;
            }
            if (entity.Status.ToString() != LicenceStatus.暂存.Key)
            {
                AHOperation sco = new AHOperation();
                sco.BobjectID = entity.Id;
                string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (empid != null && empid.Trim() != "")
                    sco.CreatorID = long.Parse(empid);
                if (empname != null && empname.Trim() != "")
                    sco.CreatorName = empname;
                sco.BusinessModuleCode = "AHServerLicence";
                sco.Memo = entity.OperationMemo;

                sco.Type = entity.Status;
                sco.TypeName = LicenceStatus.GetStatusDic()[entity.Status.ToString()].Name;
                IBAHOperation.Entity = sco;
                IBAHOperation.Add();
            }
        }
        /// <summary>
        /// 截止日期(有效期)状态
        /// </summary>
        /// <param name="licenceDate"></param>
        /// <returns></returns>
        private int GetLicenceStatus(string licenceDate)
        {
            int licenceStatusId = 0;

            string startDate = DateTime.Now.ToString("yyyy-MM-dd");
            TimeSpan d1 = DateTime.Parse(licenceDate).Subtract(DateTime.Parse(startDate));
            if (d1.Days >= 10)
            {
                licenceStatusId = int.Parse(LicenceDateStatus.有效.Key.ToString());
            }
            else if (d1.Days > 0 && d1.Days < 10)
            {
                licenceStatusId = int.Parse(LicenceDateStatus.十天内到期.Key.ToString());
            }
            else
            {
                licenceStatusId = int.Parse(LicenceDateStatus.失效.Key.ToString());
            }
            return licenceStatusId;
        }
        /// <summary>
        /// 返回字符串中的汉字个数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetHanNumFromString(string str)
        {
            int count = 0;
            if (!String.IsNullOrEmpty(str))
            {
                //中文标点符号
                char[] arrStr = new char[] { '－', '–', '—', '‘', '’', '“', '”', '…', '、', '。', '〈', '〉', '《', '》', '「', '」', '『', '』', '【', '】', '〔', '〕', '！', '（', '）', '，', '．', '：', '；', '？' };
                Regex regex = new Regex(@"^[\u4E00-\u9FA5]{0,}$");//匹配中文字符
                //Regex regexPlace = new Regex(@"([\p{P}*])");//匹配标点符号
                for (int i = 0; i < str.Length; i++)
                {
                    if (regex.IsMatch(str[i].ToString()))
                    {
                        count++;
                    }
                    else if (arrStr.Contains(str[i]))
                    {
                        ZhiFang.Common.Log.Log.Debug("中文标点符号:" + str[i]);
                        count++;
                    }
                    //else if (char.IsPunctuation(str[i]) && regexPlace.IsMatch(str[i].ToString()))
                    //{
                    //    ZhiFang.Common.Log.Log.Debug("PlaceStr:" + str[i]);
                    //    count++;
                    //}
                }
            }
            return count;
        }
        /// <summary>
        /// 重新生成服务器授权返回文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseResultBool RegenerateAHServerLicenceById(long id)
        {
            BaseResultBool brb = new BaseResultBool();
            ApplyAHServerLicence applyEntity = new ApplyAHServerLicence();
            applyEntity.AHServerLicence = this.Get(id);
            //生成授权申请返回文件
            if (applyEntity.AHServerLicence != null)
            {
                IList<AHServerProgramLicence> programLists = (List<AHServerProgramLicence>)IBAHServerProgramLicence.SearchListByHQL("ServerLicenceID=" + id);
                applyEntity.AHServerProgramLicenceList = programLists;
                IList<AHServerEquipLicence> equipLists = IBAHServerEquipLicence.SearchListByHQL("ServerLicenceID=" + id);
                applyEntity.AHServerEquipLicenceList = equipLists;
                brb = CreateAndSaveLicenceKeyFile(ref applyEntity);
            }

            return brb;
        }
        /// <summary>
        /// 服务器授权审核生成授权码后,如果当前申请的主或备服务器授权申请号与其客户信息的不致,
        /// 客户信息的需要为当前授权申请的号
        /// </summary>
        /// <param name="applyEntity"></param>
        /// <returns></returns>
        private bool UpdatePClientLRNo(AHServerLicence applyEntity)
        {
            bool result = true;
            PClient pclient = IDPClientDao.Get(long.Parse(applyEntity.PClientID.ToString()));
            bool isUpdate = false;
            if (pclient != null)
            {
                if (!pclient.LRNo1.Equals(applyEntity.LRNo1))
                    isUpdate = true;
                if (isUpdate == false && !pclient.LRNo2.Equals(applyEntity.LRNo2))
                    isUpdate = true;
            }
            if (isUpdate)
            {
                pclient.LRNo1 = applyEntity.LRNo1;
                pclient.LRNo2 = applyEntity.LRNo2;
                result = IDPClientDao.Update(pclient);
            }
            return result;
        }

        public EntityList<AHServerLicence> SearchListByHQL_LicenceInfo(string where, string returnSortStr, int page, int limit)
        {
            EntityList<AHServerLicence> list = base.SearchListByHQL(where, returnSortStr, page, limit);
            if (list != null && list.list != null && list.list.Count() > 0)
            {
                SetLicenceInfo(list);
            }
            return list;
        }

        public EntityList<AHServerLicence> SearchListByHQL_LicenceInfo(string where, int page, int limit)
        {
            EntityList<AHServerLicence> list = base.SearchListByHQL(where, page, limit);
            if (list != null && list.list != null && list.list.Count() > 0)
            {
                SetLicenceInfo(list);
            }
            return list;
        }
        private EntityList<AHServerLicence> SetLicenceInfo(EntityList<AHServerLicence> list)
        {
            List<string> clientno = new List<string>();
            foreach (var c in list.list)
            {
                if (c.PClientID.HasValue && !clientno.Contains(c.PClientID.ToString())) clientno.Add(c.PClientID.ToString());

                string equipWhere = " ServerLicenceID=" + c.Id + " and LicenceDate is not null ";
                var equiplicencelist = IBAHServerEquipLicence.SearchListByHQL(equipWhere);
                if (equiplicencelist != null && equiplicencelist.Count() > 0)
                {
                    c.LicenceDate = equiplicencelist.OrderBy(a => a.LicenceDate).First().LicenceDate;
                }
            }
            var clist = IDPClientDao.GetListByHQL(" Id in (" + string.Join(",", clientno) + ") ");
            if (clist != null && clist.Count > 0)
            {
                foreach (var c in list.list)
                {
                    if (clist.Count(a => a.Id == c.PClientID) > 0)
                    {
                        c.LicenceClientName = clist.Where(a => a.Id == c.PClientID).ElementAt(0).LicenceClientName;
                        c.LicenceCode = clist.Where(a => a.Id == c.PClientID).ElementAt(0).LicenceCode;
                    }
                }
            }
            return list;
        }

        #region 2020-08-25 longc 添加按仪器明细授权截止日期过滤
        public EntityList<AHServerLicence> SearchListByDocAndDtlHQL_LicenceInfo(string where, string dtlWhere, string returnSortStr, int page, int limit)
        {
            EntityList<AHServerLicence> entityList = new EntityList<AHServerLicence>();
            if (string.IsNullOrEmpty(dtlWhere))
            {
                entityList = base.SearchListByHQL(where, returnSortStr, page, limit);
                if (entityList != null && entityList.list != null && entityList.list.Count() > 0)
                {
                    SetLicenceInfo(entityList);
                }
            }
            else
            {
                #region 分步查询方式
                //EntityList<AHServerLicence> list = base.SearchListByHQL(where, returnSortStr, -1, -1);
                //if (list != null && list.list != null && list.count > 0)
                //{
                //    entityList = SetLicenceInfoByDocAndDtl(list, dtlWhere, page, limit);
                //}
                #endregion

                //联合按仪器明细查询方式
                entityList = ((IDAHServerLicenceDao)base.DBDao).SearchListByDocAndDtlHQL_LicenceInfo(where, dtlWhere, page, limit, returnSortStr);
                if (entityList != null && entityList.list != null && entityList.list.Count() > 0)
                {
                    SetLicenceInfo(entityList);
                }
            }

            return entityList;
        }
        public EntityList<AHServerLicence> SearchListByDocAndDtlHQL_LicenceInfo(string where, string dtlWhere, int page, int limit)
        {
            EntityList<AHServerLicence> entityList = new EntityList<AHServerLicence>();
            if (string.IsNullOrEmpty(dtlWhere))
            {
                entityList = base.SearchListByHQL(where, page, limit);
                if (entityList != null && entityList.list != null && entityList.list.Count() > 0)
                {
                    SetLicenceInfo(entityList);
                }
            }
            else
            {
                #region 分步查询方式
                //EntityList<AHServerLicence> list = base.SearchListByHQL(where, -1, -1);
                //if (list != null && list.list != null && list.count > 0)
                //{
                //    entityList = SetLicenceInfoByDocAndDtl(list, dtlWhere, page, limit);
                //} 
                #endregion

                //联合按仪器明细查询方式
                entityList = ((IDAHServerLicenceDao)base.DBDao).SearchListByDocAndDtlHQL_LicenceInfo(where, dtlWhere, page, limit, "");
                if (entityList != null && entityList.list != null && entityList.list.Count() > 0)
                {
                    SetLicenceInfo(entityList);
                }
            }

            return entityList;
        }
        private EntityList<AHServerLicence> SetLicenceInfoByDocAndDtl(EntityList<AHServerLicence> entityList1, string dtlWhere, int page, int limit)
        {
            EntityList<AHServerLicence> entityList = new EntityList<AHServerLicence>();
            entityList.list = new List<AHServerLicence>();

            IList<AHServerLicence> tempList = new List<AHServerLicence>();
            //按授权截止日期过滤
            if (!string.IsNullOrEmpty(dtlWhere))
            {
                foreach (var entity in entityList1.list)
                {
                    string equipWhere = " ServerLicenceID=" + entity.Id + " and LicenceDate is not null ";
                    equipWhere += " and LicenceTypeId != " + LicenceType.商业.Key + " and (" + dtlWhere + ") ";
                    var equiplicencelist = IBAHServerEquipLicence.SearchListByHQL(equipWhere);
                    if (equiplicencelist != null && equiplicencelist.Count() > 0)
                    {
                        entity.LicenceDate = equiplicencelist.Where(p => p.LicenceDate.HasValue == true).OrderBy(a => a.LicenceDate).First().LicenceDate;
                    }
                    if (entity.LicenceDate.HasValue)
                    {
                        tempList.Add(entity);
                    }
                }
                entityList.count = tempList.Count;
            }
            else
            {
                tempList = entityList.list;
                entityList.count = tempList.Count;
            }

            #region 分页处理
            if (tempList.Count > 0)
            {
                if (limit > 0 && limit < tempList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list2 = tempList.Skip(startIndex).Take(endIndex);
                    if (list2 != null) tempList = list2.ToList();
                }
                entityList.list = tempList;
            }
            #endregion

            #region 获取客户信息
            IList<string> clientno = new List<string>();
            foreach (var entity in entityList.list)
            {
                if (entity.PClientID.HasValue && !clientno.Contains(entity.PClientID.ToString())) clientno.Add(entity.PClientID.ToString());
            }
            if (clientno.Count > 0)
            {
                var clist = IDPClientDao.GetListByHQL(" Id in (" + string.Join(",", clientno) + ") ");
                if (clist != null && clist.Count > 0)
                {
                    foreach (var entity in entityList.list)
                    {
                        if (clist.Count(a => a.Id == entity.PClientID) > 0)
                        {
                            entity.LicenceClientName = clist.Where(a => a.Id == entity.PClientID).ElementAt(0).LicenceClientName;
                            entity.LicenceCode = clist.Where(a => a.Id == entity.PClientID).ElementAt(0).LicenceCode;
                        }
                    }
                }
            }
            #endregion

            return entityList;
        }

        #endregion
    }
}