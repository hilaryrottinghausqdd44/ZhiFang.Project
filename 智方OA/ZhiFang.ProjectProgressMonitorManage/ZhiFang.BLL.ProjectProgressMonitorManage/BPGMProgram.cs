
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using System.Web;
using System.IO;
using Spring.Context;
using Spring.Context.Support;
using System.Collections;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BPGMProgram : BaseBLL<PGMProgram>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBPGMProgram
    {
        public IBBParameter IBBParameter { get; set; }
        public IBBEquip IBBEquip { get; set; }
        public IDBEquipDao IDBEquipDao { get; set; }
        public IBSCOperation IBSCOperation { get; set; }
        public IBBDictTree IBBDictTree { get; set; }
        public Dictionary<string, string> dicHQL = new Dictionary<string, string>();
        public BaseResultDataValue AddPGMProgramByFormData(PGMProgram entity, HttpPostedFile file, int fFileOperationType, string ffileOperationMemo, string programType)
        {
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                entity.CreatorID = long.Parse(employeeID);
            }
            if (String.IsNullOrEmpty(entity.CreatorName))
            {
                entity.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            }
            if (entity.PBDictTree != null)
            {
                if (entity.PBDictTree.DataTimeStamp == null)
                    entity.PBDictTree.DataTimeStamp = arrDataTimeStamp;
            }
            if (entity.SubBDictTree != null)
            {
                if (entity.SubBDictTree.DataTimeStamp == null)
                    entity.SubBDictTree.DataTimeStamp = arrDataTimeStamp;
            }
            if (entity.BEquip != null)
            {
                if (entity.BEquip.DataTimeStamp == null)
                    entity.BEquip.DataTimeStamp = arrDataTimeStamp;
            }
            if (entity.OriginalPGMProgram != null)
            {
                if (entity.OriginalPGMProgram.DataTimeStamp == null)
                    entity.OriginalPGMProgram.DataTimeStamp = arrDataTimeStamp;
            }
            this.Entity = entity;
            //ZhiFang.Common.Log.Log.Debug("新增程序处理附件信息开始:");
            tempBaseResultDataValue = UploadAttachment(file, programType);
            //ZhiFang.Common.Log.Log.Debug("新增程序处理附件信息结束:");
            if (tempBaseResultDataValue.success)
            {
                tempBaseResultDataValue.success = this.Add();
                //保存操作记录
                switch (fFileOperationType)
                {
                    case 1:
                        fFileOperationType = (int)SCOperationType.程序暂存;
                        ffileOperationMemo = "程序暂存";
                        break;
                    case 2:
                        fFileOperationType = (int)SCOperationType.程序直接发布;
                        ffileOperationMemo = "程序直接发布";
                        break;
                    default:
                        break;
                }
                AddOperation(this.Entity, fFileOperationType, ffileOperationMemo);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool UpdatePGMProgramByFieldAndFormData(string[] tempArray, PGMProgram entity, HttpPostedFile file, int fFileOperationType, string ffileOperationMemo)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.success = this.Update(tempArray);
            //保存操作记录
            switch (fFileOperationType)
            {
                case 1:
                    fFileOperationType = (int)SCOperationType.程序修改暂存;
                    ffileOperationMemo = "程序修改暂存";
                    break;
                case 2:
                    fFileOperationType = (int)SCOperationType.程序修改发布;
                    ffileOperationMemo = "程序修改发布";
                    break;
                default:
                    break;
            }
            //保存操作记录
            AddOperation(this.Entity, fFileOperationType, ffileOperationMemo);
            return baseResultBool;
        }
        /// <summary>
        /// 更新程序的附件信息及附件上传
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="programType"></param>
        public BaseResultDataValue UploadAttachment(HttpPostedFile file, string programType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string objectName = "PGMProgram", fileName = "", newFileName = "", contentType = "";
            int len = 0;
            if (this.Entity.Status == (int)PGMProgramStatus.发布)
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
                {
                    this.Entity.PublisherID = long.Parse(employeeID);
                }
                if (String.IsNullOrEmpty(this.Entity.PublisherName))
                {
                    this.Entity.PublisherName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                }
                if (this.Entity.PublisherDateTime == null)
                {
                    this.Entity.PublisherDateTime = DateTime.Now;
                }
            }
            this.Entity.DataUpdateTime = DateTime.Now;

            if (file != null && file.FileName != null && file.ContentLength > 0)
            {
                //file.FileName处理
                //如果是IE传回来的是"H:\常用.txt"格式,需要处理为常用.txt;火狐传回的是常用.txt
                int startIndex = file.FileName.LastIndexOf(@"\");
                startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                string tempName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                newFileName = tempName.Substring(0, tempName.LastIndexOf("."));

                len = file.ContentLength;
                contentType = file.ContentType;

                //上传附件路径
                //string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("UploadFilesPath");
                string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
                if (String.IsNullOrEmpty(parentPath))
                {
                    parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("UploadFilesPath");
                }
                if (!String.IsNullOrEmpty(parentPath))
                {
                    string tempPath = "\\" + objectName + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                    parentPath = parentPath + tempPath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));

                    fileName = newFileName + "_" + this.Entity.Id + fileExt + "." + FileExt.zf.ToString();

                    tempPath = tempPath + fileName;
                    this.Entity.FileName = newFileName + fileExt;
                    this.Entity.Size = len;// / 1024;
                    this.Entity.FilePath = tempPath;
                    this.Entity.FileExt = fileExt;
                    this.Entity.FileType = contentType;

                    string filepath = Path.Combine(parentPath, fileName);
                    try
                    {
                        file.SaveAs(filepath);

                    }
                    catch (Exception ex)
                    {
                        ZhiFang.Common.Log.Log.Error("附件保存错误信息:" + ex.Message);
                        brdv.ErrorInfo = ex.Message;
                        brdv.ResultDataValue = "{id:''}";
                        brdv.success = false;
                    };
                }
                else
                {
                    brdv.ErrorInfo = "附件上传保存路径为空!";
                    brdv.ResultDataValue = "{id:''}";
                    brdv.success = false;
                }
            }
            return brdv;
        }

        public bool UpdateCountsById(long id)
        {
            return ((IDPGMProgramDao)base.DBDao).UpdateCountsById(id);
        }
        /// <summary>
        /// 程序的禁用或启用操作
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public bool UpdateIsUseByStrIds(string strIds, bool isUse)
        {
            bool result = ((IDPGMProgramDao)base.DBDao).UpdateIsUseByStrIds(strIds, isUse);
            if (!String.IsNullOrEmpty(strIds))
            {
                string[] arrIds = strIds.Split(',');
                foreach (var id in arrIds)
                {
                    PGMProgram pgMProgram = new PGMProgram();
                    pgMProgram.Id = long.Parse(id);

                    AddOperation(pgMProgram, (isUse == false ? (int)SCOperationType.程序禁用 : (int)SCOperationType.程序启用), isUse == false ? "程序禁用" : "程序启用");
                }
            }
            return result;
        }
        /// <summary>
        /// 程序的发布操作
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public bool UpdateStatusByStrIds(string strIds, string status)
        {
            bool result = ((IDPGMProgramDao)base.DBDao).UpdateStatusByStrIds(strIds, status);
            if (!String.IsNullOrEmpty(strIds) && status == ((int)PGMProgramStatus.发布).ToString())
            {
                string[] arrIds = strIds.Split(',');
                foreach (var id in arrIds)
                {
                    PGMProgram pgMProgram = new PGMProgram();
                    pgMProgram.Id = long.Parse(id);

                    AddOperation(pgMProgram, (int)SCOperationType.程序修改发布, "程序发布");
                }
            }
            return result;
        }

        /// <summary>
        /// 操作记录登记
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="type"></param>
        private void AddOperation(PGMProgram entity, int operationType, string operationMemo)
        {
            SCOperation sco = new SCOperation();
            sco.BobjectID = entity.Id;
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                sco.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                sco.CreatorName = empname;
            sco.BusinessModuleCode = "PGMProgram";
            sco.Memo = entity.OperationMemo;

            sco.Type = entity.Status;
            sco.TypeName = operationMemo;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }

        public EntityList<PGMProgram> SearchPGMProgramByBDictTreeId(string where, bool isSearchChildNode, int page, int limit, string sort, string maxLevelStr)
        {
            EntityList<PGMProgram> tempList = new EntityList<PGMProgram>();

            if (where != null && where.Length > 0)
            {
                string tempProgramHQL = "";
                string tempTreeSQL = "";//
                string lastWhereSQL = "";

                string[] tempArrHQL = where.Split('^');
                if (tempArrHQL.Length > 0)
                {
                    //字典树id(123,1233,1244)
                    string[] tempTreeIDStr = tempArrHQL[0].Split('=');
                    if (tempArrHQL.Length > 1 && (!string.IsNullOrEmpty(tempArrHQL[1])))
                        tempProgramHQL = tempArrHQL[1];
                    if (!string.IsNullOrEmpty(tempTreeIDStr[1].ToString()))
                    {
                        if (isSearchChildNode == true)
                        {
                            BaseResultTree tempBaseResultTree = IBBDictTree.SearchBDictTree(tempTreeIDStr[1].ToString(), maxLevelStr);
                            if (tempBaseResultTree != null && tempBaseResultTree.Tree != null)
                            {
                                tempTreeSQL = tempTreeSQL + GetPropertySQLByTree(tempBaseResultTree.Tree);
                            }
                        }
                        else
                        {
                            tempTreeSQL = " or pgmprogram.SubBDictTree.Id in(" + tempTreeIDStr[1].ToString() + ")";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(tempTreeSQL))
                {
                    lastWhereSQL = "(" + tempTreeSQL.Trim().Remove(0, 3) + ")";

                    //当where里带有指定传入的树节点ID时,指定传入的树节点ID优先于DataRowRoleHQL的树节点ID
                    IApplicationContext context = ContextRegistry.GetContext();
                    object bslog = context.GetObject("DataRowRoleHQL");
                    DataRowRoleHQL DRowRoleHQL = (DataRowRoleHQL)bslog;
                    if (DRowRoleHQL != null && DRowRoleHQL.DicPreconditionsHQL != null && DRowRoleHQL.DicPreconditionsHQL.ContainsKey("BDictTree"))
                        DRowRoleHQL.DicPreconditionsHQL.Remove("BDictTree");

                }
                if (!string.IsNullOrEmpty(tempProgramHQL))
                {
                    lastWhereSQL = lastWhereSQL + " and " + tempProgramHQL;
                }
                //ZhiFang.Common.Log.Log.Debug("strWhereSQL:"+ strWhereSQL);
                if (!string.IsNullOrEmpty(lastWhereSQL))
                {
                    #region 行数据权限处理
                    string rowRoleHQL = "";
                    rowRoleHQL = this.GetDataRowRoleHQL(isSearchChildNode);
                    if (!string.IsNullOrEmpty(rowRoleHQL))
                    {
                        lastWhereSQL += (" and " + rowRoleHQL);
                    }
                    //ZhiFang.Common.Log.Log.Debug("SearchPGMProgramByBDictTreeId--RowRoleHQL:" + rowRoleHQL);
                    #endregion

                    tempList = this.SearchListByHQL(lastWhereSQL, sort, page, limit);
                }
            }
            return tempList;
        }
        string GetPropertySQLByTree(List<tree> treeList)
        {
            string strWhereSQL = "";
            foreach (tree tempTree in treeList)
            {
                strWhereSQL = strWhereSQL + " or pgmprogram.SubBDictTree.Id=" + tempTree.tid.ToString();
                if (tempTree.Tree.Count > 0)
                    strWhereSQL = strWhereSQL + GetPropertySQLByTree(tempTree.Tree);
            }
            return strWhereSQL;
        }
        #region 获取行数据权限的查询条件
        /// <summary>
        /// 获取行数据权限的查询条件
        /// </summary>
        /// <returns></returns>
        public string GetDataRowRoleHQL(bool isSearchChildNode)
        {
            string strHQL = "";
            IApplicationContext context = ContextRegistry.GetContext();
            object bslog = context.GetObject("DataRowRoleHQL");
            DataRowRoleHQL D = (DataRowRoleHQL)bslog;
            Dictionary<string, string> dicHQL = D.DicPreconditionsHQL;
            if (dicHQL == null) return strHQL;

            StringBuilder tempHqlStr = new StringBuilder();
            String[] keyStr = dicHQL.Keys.ToArray<String>();
            for (int i = 0; i < keyStr.Count(); i++)
            {
                var hqlValue = dicHQL[keyStr[i]];
                if (!string.IsNullOrEmpty(hqlValue))
                {
                    switch (keyStr[i])
                    {
                        case "BDictTree":
                            hqlValue = GetDataRowRoleHQLOfBDictTree(hqlValue, isSearchChildNode);
                            if (!string.IsNullOrEmpty(hqlValue))
                            {
                                hqlValue = hqlValue + (i < keyStr.Count() - 1 ? " or " : "");
                                tempHqlStr.Append(hqlValue);
                            }
                            break;
                        case "PGMProgram":
                            if (hqlValue.Trim().IndexOf('(') != 0 && hqlValue.Trim().LastIndexOf(')') != hqlValue.Length - 1)
                                hqlValue = "(" + hqlValue.Trim() + ")";
                            hqlValue = hqlValue + (i < keyStr.Count() - 1 ? " or " : "");
                            tempHqlStr.Append(hqlValue);
                            break;
                        default:
                            break;
                    }
                }
            }
            strHQL = tempHqlStr.ToString();
            if (!string.IsNullOrEmpty(strHQL)) strHQL = "(" + strHQL.Trim() + ")";
            ZhiFang.Common.Log.Log.Debug("BPGMProgram--GetDataRowRoleHQL:" + strHQL);
            return strHQL;
        }
        public string GetDataRowRoleHQLOfBDictTree(string hqlValue, bool isSearchChildNode)
        {
            string strHQL = "";
            StringBuilder tempIdStr = new StringBuilder();
            IList<BDictTree> tempList = new List<BDictTree>();
            if (isSearchChildNode)
            {
                EntityList<BDictTree> entityList = IBBDictTree.SearchBDictTreeAndChildTreeByHQL(1, 5000, hqlValue, "", true);
                if (entityList != null && entityList.list.Count() > 0) tempList = entityList.list;
            }
            else
            {
                tempList = IBBDictTree.SearchListByHQL(hqlValue);
            }

            foreach (var model in tempList)
            {
                tempIdStr.Append(model.Id + ",");
            }
            if (!string.IsNullOrEmpty(tempIdStr.ToString()))
            {
                strHQL = "(pgmprogram.SubBDictTree.Id in (" + tempIdStr.ToString().TrimEnd(',') + "))";
            }
            return strHQL;
        }
        #endregion
    }
}