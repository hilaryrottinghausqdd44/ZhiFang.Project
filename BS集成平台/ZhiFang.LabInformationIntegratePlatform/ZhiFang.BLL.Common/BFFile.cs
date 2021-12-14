using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ZhiFang.BLL.Base;
using ZhiFang.IDAO.Common;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Common;
using System.Web;
using ZhiFang.Entity.RBAC;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.Common
{
    /// <summary>
    ///
    /// </summary>
    public class BFFile : BaseBLL<FFile>, ZhiFang.IBLL.Common.IBFFile
    {
        public IDFFileOperationDao IDFFileOperationDao { get; set; }
        public IDFFileCopyUserDao IDFFileCopyUserDao { get; set; }
        public IDFFileReadingUserDao IDFFileReadingUserDao { get; set; }
        public IDFFileReadingLogDao IDFFileReadingLogDao { get; set; }
        IDBParameterDao IDBParameterDao { get; set; }
        public IBBDictTree IBBDictTree { get; set; }
        //public IBBWeiXinAccount IBBWeiXinAccount { set; get; }
        public IDAO.RBAC.IDHREmployeeDao IDHREmployeeDao { set; get; }
        public IDAO.RBAC.IDHRDeptDao IDHRDeptDao { set; get; }
        public IDAO.RBAC.IDRBACEmpRolesDao IDRBACEmpRolesDao { set; get; }
        IBBParameter IBBParameter { get; set; }

        public override bool Add()
        {
            if (this.Entity.WeiXinAuthor == null || this.Entity.WeiXinAuthor.Trim() == "")
            {
                this.Entity.WeiXinAuthor = this.Entity.CreatorName;
            }
            if (this.Entity.WeiXinDigest == null || this.Entity.WeiXinDigest.Trim() == "")
            {
                this.Entity.WeiXinDigest = this.Entity.Summary;
            }
            return base.Add();
        }

        /// <summary>
        /// 根据ID获取实体
        /// 在文档浏览时需要添加文件浏览的操作记录
        /// </summary>
        /// <param name="longID"></param>
        /// <param name="isAddFFileReadingLog">是否需要添加文档阅读记录信息:1需要,0:不需要</param>
        /// <param name="isAddFFileOperation">是否需要添加文档操作记录信息:1需要,0:不需要</param>
        /// <returns></returns>
        public FFile _GetFFileAndAddFFileCopyUser(long longID, int isAddFFileReadingLog, int isAddFFileOperation)
        {
            FFile ffile = null;
            try
            {
                ffile = ((IDFFileDao)base.DBDao).Get(longID);
                if (ffile != null && isAddFFileReadingLog == 1)
                {
                    this.AddFFileReadingLog(ffile, (int)FFileOperationType.浏览);
                }
            }
            catch (Exception ee)
            {
                ZhiFang.Common.Log.Log.Error(ee.Source);
                //throw ee;
            }
            return ffile;
        }
        /// <summary>
        /// 新增文档信息及文档抄送对象信息表
        /// </summary>
        /// <param name="type">操作记录的操作类型值</param>
        /// <param name="ffileCopyUserType">搅抄送对象类型,(=-1默认没有选择)</param>
        /// <param name="isRevise">是否修订的新增:是:1,否:0或null</param>
        /// <returns></returns>
        //public BaseResultDataValue AddFFileAndFFileCopyUser(FFile entity, IList<FFileCopyUser> fFileCopyUserList, int fFileOperationType, int ffileCopyUserType, string ffileOperationMemo, IList<FFileReadingUser> fFileReadingUserList, int ffileReadingUserType, HttpPostedFile newThumbnails)
        //{
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
        //    string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
        //    if (entity == null)
        //    {
        //        tempBaseResultDataValue.ErrorInfo = "entity信息为空!";
        //        tempBaseResultDataValue.success = false;
        //        return tempBaseResultDataValue;
        //    }
        //    if (entity.Creator == null && employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
        //    {
        //        HREmployee creator = new HREmployee();
        //        creator.Id = long.Parse(employeeID);
        //        creator.DataTimeStamp = arrDataTimeStamp;
        //        entity.Creator = creator;
        //    }
        //    if (String.IsNullOrEmpty(entity.CreatorName))
        //    {
        //        entity.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
        //    }
        //    string fields = "";
        //    entity = EditFFileOperationInfo(entity, "", ref fields);
        //    if (entity.Revisor != null)
        //    {
        //        entity.Revisor.DataTimeStamp = arrDataTimeStamp;
        //    }
        //    this.Entity = entity;

        //    if (!this.Entity.IsSyncWeiXin.HasValue || !this.Entity.IsSyncWeiXin.Value)
        //        UploadNewsThumbnails(newThumbnails, "add");
        //    tempBaseResultDataValue.success = this.Add();
        //    if (tempBaseResultDataValue.success)
        //    {
        //        if (entity.Status != int.Parse(FFileStatus.暂存.Key))
        //        {
        //            //保存文档操作记录
        //            AddFFileOperation(entity, fFileOperationType, ffileOperationMemo);
        //        }
        //        //保存文档抄送对象信息
        //        if (ffileCopyUserType != -1 || (fFileCopyUserList != null && fFileCopyUserList.Count > 0))
        //        {
        //            AddFFileCopyUser(entity, fFileCopyUserList, "Add", ffileCopyUserType);
        //        }
        //        //保存文档抄送对象信息
        //        if (ffileReadingUserType != -1 || (fFileReadingUserList != null && fFileReadingUserList.Count > 0))
        //        {
        //            tempBaseResultDataValue.success = AddFFileReadingUser(entity, fFileReadingUserList, "Edit", ffileReadingUserType);
        //            if (tempBaseResultDataValue.success == false)
        //            {
        //                tempBaseResultDataValue.ErrorInfo = "保存文档抄送对象信息错误!";
        //            }
        //        }
        //        EditInvalidOfRelease(entity,"Add");
        //    }
        //    return tempBaseResultDataValue;
        //}
        /// <summary>
        /// 更新文档基本信息时,更新文档抄送对象或更新文档阅读对象信息
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="fFileCopyUserList"></param>
        /// <param name="fFileReadingUserList"></param>
        /// <param name="fields"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        public BaseResultBool SaveFFileAndFFileCopyUserAndFFileReadingUser(string[] tempArray, IList<FFileCopyUser> fFileCopyUserList, IList<FFileReadingUser> fFileReadingUserList, int fFileOperationType, int ffileCopyUserType, int ffileReadingUserType, string ffileOperationMemo, FFile entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (tempArray != null)
            {
                tempBaseResultBool.success = this.Update(tempArray);

            }
            if (tempBaseResultBool.success)
            {
                EditInvalidOfRelease(entity, "Edit");

                //新增文档操作记录
                if (fFileOperationType != (int)FFileOperationType.起草)
                {
                    try
                    {
                        AddFFileOperation(entity, fFileOperationType, ffileOperationMemo);

                    }
                    catch (Exception ex)
                    {
                        ZhiFang.Common.Log.Log.Error("新增文档操作记录错误!" + ex.Message);
                        tempBaseResultBool.ErrorInfo = "新增文档操作记录错误!";
                        //throw;
                    }
                }
                //更新文档抄送对象信息
                if (ffileCopyUserType != -1 || (fFileCopyUserList != null && fFileCopyUserList.Count > 0))
                {
                    tempBaseResultBool.success = AddFFileCopyUser(entity, fFileCopyUserList, "Edit", ffileCopyUserType);
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "更新文档抄送对象信息错误!";
                    }
                }
                //保存文档阅读对象信息
                if (ffileReadingUserType != -1 || (fFileReadingUserList != null && fFileReadingUserList.Count > 0))
                {
                    tempBaseResultBool.success = AddFFileReadingUser(entity, fFileReadingUserList, "Edit", ffileReadingUserType);
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "保存文档阅读对象信息错误!";
                    }
                }
            }
            return tempBaseResultBool;

        }

        /// <summary>
        /// 删除文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        public BaseResultBool UpdateFFileIsUseByIds(string strIds, bool isUse, int fFileOperationType)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string[] tempArray = null;
            if (!String.IsNullOrEmpty(strIds))
            {
                tempArray = strIds.Split(',');
                tempBaseResultBool.success = ((IDFFileDao)base.DBDao).UpdateFFileIsUseByIds(strIds, isUse);
            }
            if (tempBaseResultBool.success)
            {
                //新增文档操作记录
                FFile entity = new FFile();
                byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                entity.DataTimeStamp = arrDataTimeStamp;
                fFileOperationType = (isUse == false ? (int)FFileOperationType.禁用 : (int)FFileOperationType.撤消禁用);
                string meno = (isUse == true ? "禁用" : "撤消禁用");
                foreach (string id in tempArray)
                {
                    entity.Id = long.Parse(id);
                    AddFFileOperation(entity, fFileOperationType, "");
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 发布后处理
        /// </summary>
        /// <param name="entity">当前保存的文档</param>
        private void EditInvalidOfRelease(FFile entity, string type)
        {
            if (entity.Status == int.Parse(FFileStatus.发布.Key))
            {
                //当前文档的原文档
                try
                {
                    FFile ffile = null;
                    //第一种情况,前台已经传回原文档ID
                    if (entity.OriginalFileID.HasValue)
                    {
                        ffile = this.Get(entity.OriginalFileID.Value);
                    }
                    else
                    {
                        if (type == "Edit")
                        {
                            //第二种情况,前台没传回原文档ID
                            FFile ffile2 = this.Get(entity.Id);
                            if (ffile2 != null && ffile2.OriginalFileID.HasValue)
                            {
                                ffile = this.Get(ffile2.OriginalFileID.Value);
                                entity.OriginalFileID = ffile.OriginalFileID;
                            }
                        }
                    }

                    if (ffile != null && entity.OriginalFileID.HasValue)
                    {
                        //发布的修订文档的原文档
                        ffile.Status = int.Parse(FFileStatus.作废.Key);
                        //string[] tempArray2 = new string[2];
                        //tempArray2[0] = "Id=" + ffile.Id.ToString();
                        //tempArray2[1] = "Status=" + ffile.Status;
                        this.Entity = ffile;
                        this.Edit();
                        AddFFileOperation(ffile, (int)FFileOperationType.作废, "发布文档后并作废原文档");
                        //指向回当前的实体对象
                        this.Entity = entity;
                    }
                }
                catch (Exception ee)
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// 文档操作记录登记
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="type"></param>
        private void AddFFileOperation(FFile ffile, int type, string ffileOperationMemo)
        {
            FFileOperation ffileOperation = new FFileOperation();
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (ffile.DataTimeStamp == null)
            {
                ffile.DataTimeStamp = arrDataTimeStamp;
            }
            FFile file = new FFile();
            file.Id = ffile.Id;
            file.DataTimeStamp = arrDataTimeStamp;// ffile.DataTimeStamp;

            ffileOperation.FFile = file;
            ffileOperation.LabID = ffile.LabID;
            ffileOperation.IsUse = true;
            ffileOperation.DataAddTime = DateTime.Now;
            ffileOperation.Type = type;
            ffileOperation.Memo = ffileOperationMemo;
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);

            if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                HREmployee creator = new HREmployee();
                creator.Id = long.Parse(employeeID);
                creator.DataTimeStamp = arrDataTimeStamp;
                ffileOperation.Creator = creator;
            }
            ffileOperation.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            IDFFileOperationDao.Save(ffileOperation);
        }
        /// <summary>
        /// 文档阅读记录登记(并更新计算阅读次数)
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="type"></param>
        public void AddFFileReadingLog(FFile ffile, int type)
        {
            FFileReadingLog model = new FFileReadingLog();
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            FFile tempFFile = new FFile();
            if (ffile.DataTimeStamp == null)
            {
                ffile.DataTimeStamp = arrDataTimeStamp;
            }
            tempFFile.Id = ffile.Id;
            tempFFile.DataTimeStamp = ffile.DataTimeStamp;
            //tempFFile.LabID = ffile.LabID;
            model.FFile = tempFFile;
            // model.LabID = tempFFile.LabID;
            model.IsUse = true;
            model.DataAddTime = DateTime.Now;
            //阅读时长
            //model.ReadTimes
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);

            if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                HREmployee creator = new HREmployee();
                creator.Id = long.Parse(employeeID);
                creator.DataTimeStamp = arrDataTimeStamp;
                model.Creator = creator;
                model.Reader = creator;
            }
            model.ReaderName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            model.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            try
            {
                IDFFileReadingLogDao.Save(model);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            ((IDFFileDao)base.DBDao).UpdateFFileCountsById(tempFFile.Id);
        }

        #region 新增或修改时的各时间及操作人信息处理
        /// <summary>
        /// 新增或修改时的各时间及操作人信息处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="serverEntity"></param>
        /// <returns></returns>
        public FFile EditFFileOperationInfo(FFile entity, string typeStr, ref string fields)
        {
            FFile serverEntity = null;
            if (typeStr == "edit")
                serverEntity = this.Get(entity.Id);
            else
                serverEntity = entity;
            if (serverEntity != null)
            {
                if (!entity.OriginalFileID.HasValue)
                    entity.OriginalFileID = serverEntity.OriginalFileID;
            }
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            if (entity.Status == int.Parse(FFileStatus.已提交.Key))
            {
                entity.DrafterDateTime = DateTime.Now;
                entity.DrafterId = long.Parse(employeeID);
                entity.DrafterCName = employeeName;
                GetDrafterFields(ref fields);
                //下一审核人
                if (!entity.CheckerId.HasValue)
                {
                    entity.CheckerId = serverEntity.CheckerId;
                    entity.CheckerName = serverEntity.CheckerName;
                }
                //清空提交之后的信息
                GetCheckerFields(ref fields);
                GetApprovalFields(ref fields);
                GetPublisherFields(ref fields);

            }
            else if (entity.Status == int.Parse(FFileStatus.已审核.Key))
            {
                if (!serverEntity.DrafterDateTime.HasValue)
                {
                    entity.DrafterDateTime = DateTime.Now;
                    entity.DrafterId = long.Parse(employeeID);
                    entity.DrafterCName = employeeName;
                    GetDrafterFields(ref fields);
                }
                entity.CheckerDateTime = DateTime.Now;
                entity.CheckerId = long.Parse(employeeID);
                entity.CheckerName = employeeName;
                GetCheckerFields(ref fields);
                //下一审批人
                if (!entity.ApprovalId.HasValue)
                {
                    entity.ApprovalId = serverEntity.ApprovalId;
                    entity.ApprovalName = serverEntity.ApprovalName;
                }
                //清空提交之后的信息
                GetApprovalFields(ref fields);
                GetPublisherFields(ref fields);
            }
            else if (entity.Status == int.Parse(FFileStatus.已批准.Key))
            {
                entity = GetApprovalInfo(entity, serverEntity, ref fields);
            }
            else if (entity.Status == int.Parse(FFileStatus.发布.Key))
            {
                entity = GetPublisherInfo(entity, serverEntity, ref fields);
            }
            return entity;
        }

        private void GetDrafterFields(ref string fields)
        {
            if (!fields.Contains("DrafterDateTime"))
                fields += ",DrafterDateTime";
            if (!fields.Contains("DrafterId"))
                fields += ",DrafterId";
            if (!fields.Contains("DrafterCName"))
                fields += ",DrafterCName";
        }
        private void GetCheckerFields(ref string fields)
        {
            if (!fields.Contains("CheckerDateTime"))
                fields += ",CheckerDateTime";
            if (!fields.Contains("CheckerId"))
                fields += ",CheckerId";
            if (!fields.Contains("CheckerName"))
                fields += ",CheckerName";
        }
        private void GetApprovalFields(ref string fields)
        {
            if (!fields.Contains("ApprovalDateTime"))
                fields += ",ApprovalDateTime";
            if (!fields.Contains("ApprovalId"))
                fields += ",ApprovalId";
            if (!fields.Contains("ApprovalName"))
                fields += ",ApprovalName";
        }
        private void GetPublisherFields(ref string fields)
        {
            if (!fields.Contains("PublisherDateTime"))
                fields += ",PublisherDateTime";
            if (!fields.Contains("PublisherId"))
                fields += ",PublisherId";
            if (!fields.Contains("PublisherName"))
                fields += ",PublisherName";
        }
        private FFile GetApprovalInfo(FFile entity, FFile serverEntity, ref string fields)
        {
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!serverEntity.DrafterDateTime.HasValue)
            {
                entity.DrafterDateTime = DateTime.Now;
                entity.DrafterId = long.Parse(employeeID);
                entity.DrafterCName = employeeName;
                GetDrafterFields(ref fields);
            }

            if (!serverEntity.CheckerDateTime.HasValue)
            {
                entity.CheckerDateTime = DateTime.Now;
                entity.CheckerId = long.Parse(employeeID);
                entity.CheckerName = employeeName;
                GetCheckerFields(ref fields);
            }

            entity.ApprovalDateTime = DateTime.Now;
            entity.ApprovalId = long.Parse(employeeID);
            entity.ApprovalName = employeeName;
            GetApprovalFields(ref fields);
            //下一发布人
            if (!entity.PublisherId.HasValue)
            {
                entity.PublisherId = serverEntity.PublisherId;
                entity.PublisherName = serverEntity.PublisherName;
            }
            //清空提交之后的信息
            GetPublisherFields(ref fields);
            return entity;
        }
        private FFile GetPublisherInfo(FFile entity, FFile serverEntity, ref string fields)
        {
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!serverEntity.DrafterDateTime.HasValue)
            {
                entity.DrafterDateTime = DateTime.Now;
                entity.DrafterId = long.Parse(employeeID);
                entity.DrafterCName = employeeName;
                GetDrafterFields(ref fields);
            }

            if (!serverEntity.CheckerDateTime.HasValue)
            {
                entity.CheckerDateTime = DateTime.Now;
                entity.CheckerId = long.Parse(employeeID);
                entity.CheckerName = employeeName;
                GetCheckerFields(ref fields);
            }
            if (!serverEntity.ApprovalDateTime.HasValue)
            {
                entity.ApprovalDateTime = DateTime.Now;
                entity.ApprovalId = long.Parse(employeeID);
                entity.ApprovalName = employeeName;
                GetApprovalFields(ref fields);
            }
            entity.PublisherDateTime = DateTime.Now;
            entity.PublisherId = long.Parse(employeeID);
            entity.PublisherName = employeeName;
            GetPublisherFields(ref fields);

            if (entity.OriginalFileID.HasValue)
            {
                entity.ReviseTime = DateTime.Now;
                if (!fields.Contains("ReviseTime"))
                    fields += ",ReviseTime";
            }
            ZhiFang.Common.Log.Log.Debug("fields:" + fields);
            return entity;
        }
        #endregion
        /// <summary>
        /// 置顶/撤消置顶文档信息
        /// </summary>
        /// <param name="strIds"></param>
        /// <param name="IsTop"></param>
        /// <param name="fFileOperationType"></param>
        /// <returns></returns>
        public BaseResultBool UpdateFFileIsTopByIds(string strIds, bool IsTop, int fFileOperationType)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string[] tempArray = null;
            if (!String.IsNullOrEmpty(strIds))
            {
                tempArray = strIds.Split(',');
                tempBaseResultBool.success = ((IDFFileDao)base.DBDao).UpdateFFileIsTopByIds(strIds, IsTop);
            }
            if (tempBaseResultBool.success)
            {
                //新增文档操作记录
                FFile entity = new FFile();
                byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                entity.DataTimeStamp = arrDataTimeStamp;

                fFileOperationType = (IsTop == true ? (int)FFileOperationType.置顶 : (int)FFileOperationType.撤消置顶);
                string meno = (IsTop == true ? "置顶" : "撤消置顶");
                foreach (string id in tempArray)
                {
                    entity.Id = long.Parse(id);
                    AddFFileOperation(entity, fFileOperationType, meno);
                }
            }
            return tempBaseResultBool;

        }

        /// <summary>
        /// 保存文档抄送对象信息
        /// </summary>
        /// <param name="entity"></param>
        private bool AddFFileCopyUser(FFile entity, IList<FFileCopyUser> fFileCopyUserList, string type, int ffileCopyUserType)
        {
            bool isExec = true, result = true;

            //如果是修改文档时,先删除该文档下原有的抄送对象信息,再新增保存
            if (type == "Edit")
            {
                result = IDFFileCopyUserDao.DeleteByFFileId(entity.Id);
            }
            FFileCopyUser model = new FFileCopyUser();
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (ffileCopyUserType == (int)FFileObjectType.全部人员)
            {
                model.Type = ffileCopyUserType;
                model.IsUse = true;
                FFile file = new FFile();
                file.Id = entity.Id;
                file.DataTimeStamp = arrDataTimeStamp;
                model.FFile = file;
                model.DataAddTime = DateTime.Now;
                model.LabID = entity.LabID;

                if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
                {
                    HREmployee creator = new HREmployee();
                    creator.Id = long.Parse(employeeID);
                    creator.DataTimeStamp = arrDataTimeStamp;
                    model.Creator = creator;
                }

                model.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                result = IDFFileCopyUserDao.Save(model);
                isExec = false;
            }
            else
            {
                foreach (FFileCopyUser obj in fFileCopyUserList)
                {
                    if (isExec == false)
                    {
                        break;
                    }
                    model = new FFileCopyUser();
                    model.Type = ffileCopyUserType;
                    model.IsUse = true;
                    FFile file = new FFile();
                    file.Id = entity.Id;
                    file.DataTimeStamp = arrDataTimeStamp;
                    model.FFile = file;
                    model.DataAddTime = DateTime.Now;
                    model.LabID = entity.LabID;
                    if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
                    {
                        HREmployee creator = new HREmployee();
                        creator.Id = long.Parse(employeeID);
                        creator.DataTimeStamp = arrDataTimeStamp;
                        model.Creator = creator;
                    }
                    model.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                    switch (ffileCopyUserType)
                    {
                        case (int)FFileObjectType.科室:
                            obj.HRDept.DataTimeStamp = arrDataTimeStamp;
                            model.HRDept = obj.HRDept;
                            result = IDFFileCopyUserDao.Save(model);
                            isExec = true;
                            break;
                        case (int)FFileObjectType.角色:
                            obj.RBACRole.DataTimeStamp = arrDataTimeStamp;
                            model.RBACRole = obj.RBACRole;
                            result = IDFFileCopyUserDao.Save(model);
                            isExec = true;
                            break;
                        case (int)FFileObjectType.人员:
                            obj.User.DataTimeStamp = arrDataTimeStamp;
                            model.User = obj.User;
                            result = IDFFileCopyUserDao.Save(model);
                            isExec = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 保存文档阅读对象信息
        /// </summary>
        /// <param name="entity"></param>
        private bool AddFFileReadingUser(FFile entity, IList<FFileReadingUser> fFileReadingUserList, string type, int ffileReadingUserType)
        {
            bool isExec = true, result = true;
            //如果是修改文档时,先删除该文档下原有的抄送对象信息,再新增保存
            if (type == "Edit")
            {
                result = IDFFileReadingUserDao.DeleteByFFileId(entity.Id);
            }
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            FFileReadingUser model = new FFileReadingUser();
            if (ffileReadingUserType == (int)FFileObjectType.全部人员)
            {
                model.Type = ffileReadingUserType;
                model.IsUse = true;
                FFile file = new FFile();
                file.Id = entity.Id;
                file.DataTimeStamp = arrDataTimeStamp;
                model.FFile = file;
                model.DataAddTime = DateTime.Now;
                model.LabID = entity.LabID;

                if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
                {
                    HREmployee creator = new HREmployee();
                    creator.Id = long.Parse(employeeID);
                    creator.DataTimeStamp = arrDataTimeStamp;
                    model.Creator = creator;
                }
                model.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                result = IDFFileReadingUserDao.Save(model);
                isExec = false;
            }
            else
            {
                foreach (FFileReadingUser obj in fFileReadingUserList)
                {
                    if (isExec == false)
                    {
                        break;
                    }
                    model = new FFileReadingUser();
                    model.Type = ffileReadingUserType;
                    FFile file = new FFile();
                    file.Id = entity.Id;
                    file.DataTimeStamp = arrDataTimeStamp;
                    model.FFile = file;
                    model.DataAddTime = DateTime.Now;
                    model.LabID = entity.LabID;
                    if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
                    {
                        HREmployee creator = new HREmployee();
                        creator.Id = long.Parse(employeeID);
                        creator.DataTimeStamp = arrDataTimeStamp;
                        model.Creator = creator;
                    }
                    model.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    switch (ffileReadingUserType)
                    {
                        case (int)FFileObjectType.科室:
                            obj.HRDept.DataTimeStamp = arrDataTimeStamp;
                            model.HRDept = obj.HRDept;
                            result = IDFFileReadingUserDao.Save(model);
                            isExec = true;
                            break;
                        case (int)FFileObjectType.角色:
                            obj.RBACRole.DataTimeStamp = arrDataTimeStamp;
                            model.RBACRole = obj.RBACRole;
                            result = IDFFileReadingUserDao.Save(model);
                            isExec = true;
                            break;
                        case (int)FFileObjectType.人员:
                            obj.User.DataTimeStamp = arrDataTimeStamp;
                            model.User = obj.User;
                            result = IDFFileReadingUserDao.Save(model);
                            isExec = true;
                            break;
                        case (int)FFileObjectType.区域:
                            model.AreaId = obj.AreaId;
                            result = IDFFileReadingUserDao.Save(model);
                            isExec = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 依当前登录者的员工Id信息查询当前登录者的抄送文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeID(string where, bool isSearchChildNode, int page, int count)
        {
            EntityList<FFile> tmpEntityList = new EntityList<FFile>();
            string strHqlWhere = GetHQLByWhere(where, isSearchChildNode);
            tmpEntityList = ((IDFFileDao)base.DBDao).SearchFFileCopyUserListByHQLAndEmployeeIDCookie(strHqlWhere, page, count);
            return tmpEntityList;
        }
        /// <summary>
        /// 依当前登录者的员工Id信息查询当前登录者的抄送文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileCopyUserListByHQLAndEmployeeID(string where, bool isSearchChildNode, string Order, int page, int count)
        {
            string strHqlWhere = GetHQLByWhere(where, isSearchChildNode);
            return ((IDFFileDao)base.DBDao).SearchFFileCopyUserListByHQLAndEmployeeIDCookie(strHqlWhere, Order, page, count);
        }
        /// <summary>
        /// 依当前登录者的员工Id信息查询当前登录者的阅读文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string where, bool isSearchChildNode, int page, int count)
        {
            string strHqlWhere = GetHQLByWhere(where, isSearchChildNode);
            var efFiles = ((IDFFileDao)base.DBDao).SearchFFileReadingUserListByHQLAndEmployeeIDCookie(strHqlWhere, page, count);
            //2020 增加已阅读未阅读状态
            if (efFiles.count>0)
            {
                List<long> f_File = new List<long>();
                efFiles.list.ToList().ForEach(a=>f_File.Add(a.Id));
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                var redlogs = IDFFileReadingLogDao.GetListByHQL("IsUse=1 and FFile.Id in (" + string.Join(",",f_File)+ ") and Reader.Id=" + employeeID);
                foreach (var item in efFiles.list)
                {
                    //如果当前人有阅读信息则为阅读过
                    if (redlogs.Count(a => a.FFile.Id == item.Id) > 0) {
                        item.IsHREmployeeReader = true;
                    }
                }
            }
            return efFiles;
        }
        /// <summary>
        /// 判断当前登录人有多少个未读消息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int UnHREmployeeReaderCount(string where)
        {
            var ff = SearchFFileReadingUserListByHQLAndEmployeeID(where, true, -1, -1);
            int count = 0;
            if (ff.count>0)
            {
                count = ff.list.Count(a => a.IsHREmployeeReader == false);
            }
            return count;
        }

        /// <summary>
        /// 依员工Id查询阅读文档信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="employeeID"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string where, bool isSearchChildNode, int page, int count, string employeeID, string employeeName)
        {
            string strHqlWhere = GetHQLByWhere(where, isSearchChildNode);
            return ((IDFFileDao)base.DBDao).SearchFFileReadingUserListByHQLAndEmployeeID(strHqlWhere, null, page, count, employeeID, employeeName);
        }
        /// <summary>
        /// 依员工Id目录树ID查询阅读文档信息
        /// </summary>
        /// <param name="dictreeids"></param>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="employeeID"></param>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string dictreeids, string where, bool isSearchChildNode, int page, int count, string employeeID, string employeeName)
        {
            string strHqlWhere = GetHQLWhereByStr(dictreeids, isSearchChildNode);
            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                strHqlWhere = !String.IsNullOrEmpty(where) ? (strHqlWhere + " and " + where) : strHqlWhere;
            }
            else
            {
                strHqlWhere = where;
            }
            return ((IDFFileDao)base.DBDao).SearchFFileReadingUserListByHQLAndEmployeeID(strHqlWhere, null, page, count, employeeID, employeeName);
        }

        /// <summary>
        /// 依当前登录者的员工Id信息查询当前登录者的阅读文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string where, bool isSearchChildNode, string Order, int page, int count)
        {
            string strHqlWhere = GetHQLByWhere(where, isSearchChildNode);
            //2020 增加已阅读未阅读状态
            var efFiles= ((IDFFileDao)base.DBDao).SearchFFileReadingUserListByHQLAndEmployeeIDCookie(strHqlWhere, Order, page, count);

            if (efFiles.count > 0)
            {
                List<long> f_File = new List<long>();
                efFiles.list.ToList().ForEach(a => f_File.Add(a.Id));
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                var redlogs = IDFFileReadingLogDao.GetListByHQL("IsUse=1 and FFile.Id in (" + string.Join(",", f_File) + ") and Reader.Id=" + employeeID);
                foreach (var item in efFiles.list)
                {
                    //如果当前人有阅读信息则为阅读过
                    if (redlogs.Count(a => a.FFile.Id == item.Id) > 0)
                    {
                        item.IsHREmployeeReader = true;
                    }
                }
            }
            return efFiles;
        }
        /// <summary>
        /// 依当前登录者的员工Id信息查询当前登录者的阅读文档信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileReadingUserListByHQLAndEmployeeID(string dictreeids, string where, bool isSearchChildNode, string Order, int page, int count)
        {
            string strHqlWhere = GetHQLWhereByStr(dictreeids, isSearchChildNode);
            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                strHqlWhere = !String.IsNullOrEmpty(where) ? (strHqlWhere + " and " + where) : strHqlWhere;
            }
            else
            {
                strHqlWhere = where;
            }
            return ((IDFFileDao)base.DBDao).SearchFFileReadingUserListByHQLAndEmployeeIDCookie(strHqlWhere, Order, page, count);
        }

        /// <summary>
        /// 处理文档抄送查询及文档阅读查询的where串
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode">查询传入节点的所有子孙节点</param>
        /// <returns></returns>
        private string GetHQLByWhere(string where, bool isSearchChildNode)
        {
            List<string> tmpa = new List<string>();
            string tmpdictreeids = "";
            string hqlwhere = "";
            if (!String.IsNullOrEmpty(where) && where.IndexOf('^') != -1 )
                tmpa = where.ToString().Trim().Split('^').ToList();

            hqlwhere = where;
            if (tmpa.Count > 0)
            {
                string[] tempIDHQL = tmpa[0].Split('=');
                if (tempIDHQL.Length > 1 && (!string.IsNullOrEmpty(tempIDHQL[1])))
                    tmpdictreeids = tempIDHQL[1];
            }
            if (tmpa.Count > 1)
            {
                hqlwhere = tmpa[1];
            }
            string dictreeHQL =  GetHQLWhereByStr(tmpdictreeids, isSearchChildNode);

            if (!string.IsNullOrEmpty(dictreeHQL))
            {
                hqlwhere = !String.IsNullOrEmpty(hqlwhere) ? (dictreeHQL + " and " + hqlwhere) : dictreeHQL;
            }
            return hqlwhere;




            //string tempFFileHQL = "";//文档的原查询条件
            //string strBDictTreeSQL = "";//文档所属树类型信息的查询条件
            //string strHqlWhere = "";
            //tempFFileHQL = "";
            //string[] tempHQLList = new string[0];

            //if (!String.IsNullOrEmpty(where))
            //    tempHQLList = where.ToString().Trim().Split('^');

            ////查询传入节点的所有子孙节点
            //if (tempHQLList.Length > 0)
            //{
            //    string[] tempIDHQL = tempHQLList[0].Split('=');

            //    if (tempHQLList.Length > 1 && (!string.IsNullOrEmpty(tempHQLList[1])))
            //        tempFFileHQL = tempHQLList[1];

            //    if (isSearchChildNode == true)
            //    {
            //        BaseResultTree tempBaseResultTree = IBBDictTree.SearchBDictTree(tempIDHQL[1].ToString(), "0");
            //        if (tempBaseResultTree != null && tempBaseResultTree.Tree != null)
            //        {
            //            strBDictTreeSQL = GetPropertySQLByTree(tempBaseResultTree.Tree);
            //        }

            //        if (!string.IsNullOrEmpty(strBDictTreeSQL))
            //        {
            //            strHqlWhere = "(" + strBDictTreeSQL.Trim().Remove(0, 3) + ")";
            //        }
            //    }
            //    else if (!string.IsNullOrEmpty(tempIDHQL[1]))
            //    {
            //        strHqlWhere = "(ffile.BDictTree.Id in(" + tempIDHQL[1].ToString() + "))";
            //    }
            //}
            //if (!string.IsNullOrEmpty(tempFFileHQL))
            //{
            //    strHqlWhere = !String.IsNullOrEmpty(strHqlWhere) ? (strHqlWhere + " and " + tempFFileHQL) : tempFFileHQL;
            //}
            //return strHqlWhere;

        }
        /// <summary>
        /// 处理文档抄送查询及文档阅读查询的where串
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode">查询传入节点的所有子孙节点</param>
        /// <returns></returns>
        private string GetHQLWhereByStr(string DictTreeIds, bool isSearchChildNode)
        {
            string strdictids = DictTreeIds;
            string strBDictTreeSQL = "";//文档所属树类型信息的查询条件
            string strHqlWhere = "";

            //查询传入节点的所有子孙节点
            if (DictTreeIds.Length > 0)
            {
                if (isSearchChildNode == true)
                {
                    BaseResultTree tempBaseResultTree = IBBDictTree.SearchBDictTree(strdictids.ToString(), "0");
                    if (tempBaseResultTree != null && tempBaseResultTree.Tree != null)
                    {
                        strBDictTreeSQL = GetPropertySQLByTree(tempBaseResultTree.Tree);
                    }

                    if (!string.IsNullOrEmpty(strBDictTreeSQL))
                    {
                        strHqlWhere = "(" + strBDictTreeSQL.Trim().Remove(0, 3) + ")";
                    }
                }
                else
                {
                    strHqlWhere = "(ffile.BDictTree.Id in(" + strdictids + "))";
                }
            }
            return strHqlWhere;
        }
        /// <summary>
        /// 处理文档抄送查询及文档阅读查询的where串
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isSearchChildNode">查询传入节点的所有子孙节点</param>
        /// <returns></returns>
        private string GetHQLWhereByLong(long[] DictTreeIds, bool isSearchChildNode)
        {
            return GetHQLWhereByStr(string.Join(",", DictTreeIds), isSearchChildNode);
            //if (!string.IsNullOrEmpty(tempFFileHQL))
            //{
            //    strHqlWhere = !String.IsNullOrEmpty(strHqlWhere) ? (strHqlWhere + " and " + tempFFileHQL) : tempFFileHQL;
            //}           
        }

        /// <summary>
        /// 查询某一类型树的直属文档列表(包含某一类型树的所有子类型树)
        /// </summary>
        /// <param name="where">id =4685257987940586259,123132313^(hremployee.CName like '%c1%')</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public EntityList<FFile> SearchFFileByBDictTreeId(string where, bool isSearchChildNode, int page, int limit, string sort, string maxLevelStr)
        {
            EntityList<FFile> tempList = new EntityList<FFile>();
            if (where != null && where.Length > 0)
            {
                string tempFFileHQL = "";//文档的查询条件
                string strBDictTreeSQL = "";//文档所属树类型信息的查询条件
                string strWhereSQL = "";

                string[] tempHQLList = where.Split('^');
                if (tempHQLList.Length > 0)
                {
                    //文档字典树id
                    string[] tempIDHQL = tempHQLList[0].Split('=');

                    if (tempHQLList.Length > 1 && (!string.IsNullOrEmpty(tempHQLList[1])))
                        tempFFileHQL = tempHQLList[1];
                    if (!string.IsNullOrEmpty(tempIDHQL[1].ToString()))
                    {
                        if (isSearchChildNode == true)
                        {
                            BaseResultTree tempBaseResultTree = IBBDictTree.SearchBDictTree(tempIDHQL[1].ToString(), maxLevelStr);
                            if (tempBaseResultTree != null && tempBaseResultTree.Tree != null)
                            {
                                strBDictTreeSQL = strBDictTreeSQL + GetPropertySQLByTree(tempBaseResultTree.Tree);
                            }
                        }
                        else
                        {
                            strBDictTreeSQL = " or ffile.BDictTree.Id in(" + tempIDHQL[1].ToString() + ")";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(strBDictTreeSQL))
                {
                    strWhereSQL = "(" + strBDictTreeSQL.Trim().Remove(0, 3) + ")";
                }
                if (!string.IsNullOrEmpty(tempFFileHQL))
                {
                    strWhereSQL = strWhereSQL + " and " + tempFFileHQL;
                }
                //ZhiFang.Common.Log.Log.Debug("查询某一类型树的直属文档列表HQL:" + strWhereSQL);
                if (!string.IsNullOrEmpty(strWhereSQL))
                {
                    tempList = ((IDFFileDao)base.DBDao).GetListByHQL(strWhereSQL, sort, page, limit);
                }
            }
            return tempList;
        }

        string GetPropertySQLByTree(List<tree> treeList)
        {
            string strWhereSQL = "";
            foreach (tree tempTree in treeList)
            {
                strWhereSQL = strWhereSQL + " or ffile.BDictTree.Id=" + tempTree.tid.ToString();
                if (tempTree.Tree.Count > 0)
                    strWhereSQL = strWhereSQL + GetPropertySQLByTree(tempTree.Tree);
            }
            return strWhereSQL;
        }

        public bool FFileWeiXinMessagePushById(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, long id)
        {
            FFile ffile = DBDao.Get(id);
            if (ffile != null)
            {
                IList<FFileReadingUser> ffilereadinguserlist = IDFFileReadingUserDao.GetListByHQL(" FFile.Id=" + id);
                if (ffilereadinguserlist.Count > 0)
                {
                    List<long> receiveidlist = new List<long>();
                    List<HREmployee> emplist = new List<HREmployee>();
                    switch (ffilereadinguserlist[0].Type)
                    {
                        case 1:
                            emplist = IDHREmployeeDao.GetListByHQL("1=1").ToList();
                            if (emplist.Count() > 0)
                            {
                                foreach (HREmployee emp in emplist)
                                {
                                    receiveidlist.Add(emp.Id);
                                }
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.Common.BFFile.FFileWeiXinMessagePushById：没有员工！");
                            }
                            break;
                        case 2:
                            List<long> deptidlist = new List<long>();
                            foreach (FFileReadingUser ffilereadinguser in ffilereadinguserlist)
                            {
                                if (ffilereadinguser.HRDept != null)
                                    deptidlist.Add(ffilereadinguser.HRDept.Id);
                            }
                            if (deptidlist.Count > 0)
                            {
                                emplist = IDHREmployeeDao.GetListByHQL(" HRDept.Id in (" + string.Join(",", deptidlist.ToArray()) + ")").ToList();
                                if (deptidlist.Count > 0)
                                {
                                    foreach (HREmployee emp in emplist)
                                    {
                                        receiveidlist.Add(emp.Id);
                                    }
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.Common.BFFile.FFileWeiXinMessagePushById：部门：" + string.Join(",", deptidlist.ToArray()) + "内没有员工！");
                                }
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.Common.BFFile.FFileWeiXinMessagePushById：抄送表内没有部门！");
                            }
                            break;
                        case 3:
                            List<long> Roleidlist = new List<long>();
                            foreach (FFileReadingUser ffilereadinguser in ffilereadinguserlist)
                            {
                                if (ffilereadinguser.RBACRole != null)
                                    Roleidlist.Add(ffilereadinguser.RBACRole.Id);
                            }
                            if (Roleidlist.Count > 0)
                            {
                                IList<RBACEmpRoles> emproleslist = IDRBACEmpRolesDao.GetListByHQL(" RBACRole.Id in (" + string.Join(",", Roleidlist.ToArray()) + ")").ToList();
                                if (emproleslist.Count > 0)
                                {
                                    foreach (RBACEmpRoles emproles in emproleslist)
                                    {
                                        receiveidlist.Add(emproles.HREmployee.Id);
                                    }
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.Common.BFFile.FFileWeiXinMessagePushById：角色：" + string.Join(",", Roleidlist.ToArray()) + "内没有员工！");
                                }
                            }
                            else
                            {
                                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.Common.BFFile.FFileWeiXinMessagePushById：抄送表内没有角色！");
                            }
                            break;
                        case 4:
                            foreach (FFileReadingUser ffilereadinguser in ffilereadinguserlist)
                            {
                                if (ffilereadinguser.User != null)
                                    receiveidlist.Add(ffilereadinguser.User.Id);
                            }
                            break;
                    }

                    if (receiveidlist.Count > 0)
                    {
                        string tmpdatetime = "";
                        if (ffile.BeginTime.HasValue)
                        {
                            tmpdatetime += ffile.BeginTime.Value.ToString("yyyy-MM-dd");
                            if (ffile.EndTime.HasValue)
                            {
                                tmpdatetime += "——" + ffile.EndTime.Value.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                tmpdatetime += "——永久";
                            }
                        }
                        else
                        {
                            tmpdatetime = "";
                            if (ffile.EndTime.HasValue)
                            {
                                tmpdatetime += ffile.EndTime.Value.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                tmpdatetime = "永久";
                            }
                        }


                        Dictionary<string, TemplateDataObject> data = new Dictionary<string, TemplateDataObject>();
                        string urgencycolor = "#11cd6e";
                        string fileno = (ffile.No != null && ffile.No.Trim() != "") ? "(" + ffile.No + ")" : "";
                        data.Add("first", new TemplateDataObject() { color = urgencycolor, value = "名称：" + ffile.Title + fileno });
                        data.Add("keyword1", new TemplateDataObject() { color = "#000000", value = ffile.Summary });
                        data.Add("keyword2", new TemplateDataObject() { color = "#000000", value = ffile.Source });
                        data.Add("keyword3", new TemplateDataObject() { color = "#000000", value = ffile.CreatorName });
                        data.Add("keyword4", new TemplateDataObject() { color = "#000000", value = tmpdatetime });
                        data.Add("remark", new TemplateDataObject() { color = urgencycolor, value = "请登录OA查看" });
                        //IBBWeiXinAccount.PushWeiXinMessage(pushWeiXinMessageAction, receiveidlist, data, "file", "WeiXin/WeiXinMainRouter.aspx?operate=NEWS&ffileid=" + id);
                    }
                    else
                    {
                        //抄送人没有关注微信号
                    }
                }
                else
                {
                    //没有抄送人
                }
                return true;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.BLL.Common.BFFile.FFileWeiXinMessagePushById：参数id为空！");
                return false;
            }
        }
        /// <summary>
        /// 依文档Id及查询类型获取文档的抄送对象信息或阅读对象信息
        /// </summary>
        /// <param name="ffileId">文档Id</param>
        /// <param name="searchType">查询类型</param>
        /// <returns></returns>
        public BaseResultDataValue SearchFFileCopyUserOrReadingUserByFFileId(long ffileId, string searchType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string hqlWhere = "", type = "";
            //string resultStr = "{type:null,list:null}";
            JObject jresult = new JObject();

            StringBuilder idStr = new StringBuilder();
            StringBuilder cnameStr = new StringBuilder();
            switch (searchType)
            {
                case "1":
                    hqlWhere = "ffilecopyuser.FFile.Id=" + ffileId;
                    IList<FFileCopyUser> listCopyUser = IDFFileCopyUserDao.GetListByHQL(hqlWhere);
                    foreach (var model in listCopyUser)
                    {
                        //1全部人员、2按部门、3按角色、4按人员
                        string id = "", cname = "";
                        type = model.Type.ToString();
                        switch (model.Type)
                        {
                            case 1:
                                id = "";
                                cname = "";
                                break;
                            case 2:
                                id = model.HRDept.Id.ToString();
                                cname = model.HRDept.CName;
                                break;
                            case 3:
                                id = model.RBACRole.Id.ToString();
                                cname = model.RBACRole.CName;
                                break;
                            case 4:
                                id = model.User.Id.ToString();
                                cname = model.User.CName;
                                break;
                            default:
                                break;
                        }
                        if (model.Type != 1)
                        {
                            idStr.Append(id);
                            idStr.Append(",");

                            cnameStr.Append(cname);
                            cnameStr.Append(",");
                        }
                    }
                    break;
                case "2"://阅读对象
                    hqlWhere = "ffilereadinguser.FFile.Id=" + ffileId;
                    IList<FFileReadingUser> listReadingUser = IDFFileReadingUserDao.GetListByHQL(hqlWhere);
                    foreach (var model in listReadingUser)
                    {
                        //1全部人员、2按部门、3按角色、4按人员
                        string id = "", cname = "";
                        type = model.Type.ToString();
                        switch (model.Type)
                        {
                            case 1:
                                id = "";
                                cname = "";
                                break;
                            case 2:
                                id = model.HRDept.Id.ToString();
                                cname = model.HRDept.CName;
                                break;
                            case 3:
                                id = model.RBACRole.Id.ToString();
                                cname = model.RBACRole.CName;
                                break;
                            case 4:
                                id = model.User.Id.ToString();
                                cname = model.User.CName;
                                break;
                            default:
                                break;
                        }
                        if (model.Type != 1)
                        {
                            idStr.Append(id);
                            idStr.Append(",");

                            cnameStr.Append(cname);
                            cnameStr.Append(",");
                        }
                    }
                    break;
                default:
                    break;
            }
            string tempIds = idStr.ToString().TrimEnd(',');
            string tempCNames = cnameStr.ToString().TrimEnd(',');
            JObject jlist = new JObject();
            jlist.Add("idStr", tempIds);
            jlist.Add("cnameStr", tempCNames);

            jresult.Add("type", type);
            jresult.Add("list", jlist);
            //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
            baseResultDataValue.ResultDataValue = jresult.ToString();

            return baseResultDataValue;
        }
        #region 新闻缩略图上传及下载
        /// <summary>
        /// 上传新闻缩略图
        /// </summary>
        /// <param name="newThumbnails"></param>
        /// <param name="type">add:为新增新闻时上传;update为修改新闻时上传</param>
        /// <returns></returns>
        //public BaseResultDataValue UploadNewsThumbnails(HttpPostedFile newThumbnails, string type)
        //{
        //    BaseResultDataValue brdv = new BaseResultDataValue();
        //    if (this.Entity == null)
        //    {
        //        brdv.success = false;
        //        brdv.ErrorInfo = "新闻缩略图Entity信息为空！";
        //        brdv.ResultDataValue = "";
        //        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
        //    }
        //    else if (newThumbnails == null)
        //    {
        //        brdv.success = false;
        //        brdv.ErrorInfo = "未检测到文件！";
        //        brdv.ResultDataValue = "";
        //    }
        //    if (brdv.success)
        //    {

        //        string savePath = SysPublicSet.FFileNews.ThumbServerPath;
        //        if (!Directory.Exists(savePath))
        //            Directory.CreateDirectory(savePath);
        //        string fileName = this.Entity.Id + ".png";
        //        string filepath = Path.Combine(savePath, fileName);
        //        //ZhiFang.Common.Log.Log.Error(filepath);
        //        if (newThumbnails != null)
        //        {
        //            newThumbnails.SaveAs(filepath);
        //            //string savePath = tempPath + fileName;
        //            //ZhiFang.Common.Log.Log.Debug("UploadNewsThumbnails.SaveAs=" +  savePath );
        //            this.Entity.ThumbnailsPath = "" + SysPublicSet.FFileNews.ThumbPath + fileName + "";

        //            brdv.ResultDataValue = SysPublicSet.FFileNews.ThumbPath + fileName.Replace("\\\\", "/");
        //            if (type.ToLower() == "update")
        //            {
        //                List<string> tmpa = new List<string>();
        //                tmpa.Add("Id=" + this.Entity.Id);
        //                tmpa.Add("ThumbnailsPath='" + SysPublicSet.FFileNews.ThumbPath + fileName + "'");
        //                //ZhiFang.Common.Log.Log.Debug("UploadNewsThumbnails.ThumbnailsPath=" + "ThumbnailsPath='" + savePath + "'" + "");
        //                var tempArray = tmpa.ToArray();
        //                this.Update(tempArray);
        //            }
        //        }

        //    }
        //    return brdv;
        //}
        /// <summary>
        /// 新闻缩略图下载
        /// </summary>
        /// <param name="id"></param>
        public FileStream DownLoadNewsThumbnailsById(long id)
        {
            FileStream fileStream = null;
            //BaseResultDataValue brdv = new BaseResultDataValue();
            //string parentPath = SysPublicSet.FFileNews.ThumbPath;
            FFile tmpf = DBDao.Get(id);
            if (tmpf == null)
            {
                ZhiFang.Common.Log.Log.Error("新闻不存在,不能访问新闻缩略图！");
            }
            else
            {
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + tmpf.ThumbnailsPath))
                {
                    fileStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + tmpf.ThumbnailsPath, FileMode.Open, FileAccess.Read);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("新闻缩略图不存在,访问路径为" + tmpf.ThumbnailsPath);
                }
            }
            return fileStream;
        }

        public BaseResultDataValue AddFFileAndFFileCopyUser(FFile entity, IList<FFileCopyUser> fFileCopyUserList, int fFileOperationType, int ffileCopyUserType, string ffileOperationMemo, IList<FFileReadingUser> fFileReadingUserList, int ffileReadingUserType, object newThumbnails)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            byte[] arrDataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (entity == null)
            {
                tempBaseResultDataValue.ErrorInfo = "entity信息为空!";
                tempBaseResultDataValue.success = false;
                return tempBaseResultDataValue;
            }
            if (entity.Creator == null && employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                HREmployee creator = new HREmployee();
                creator.Id = long.Parse(employeeID);
                creator.DataTimeStamp = arrDataTimeStamp;
                entity.Creator = creator;
            }
            if (String.IsNullOrEmpty(entity.CreatorName))
            {
                entity.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            }
            string fields = "";
            entity = EditFFileOperationInfo(entity, "", ref fields);
            if (entity.Revisor != null)
            {
                entity.Revisor.DataTimeStamp = arrDataTimeStamp;
            }
            //文档修订号处理
            if (entity.Type == 1 && entity.Revisor != null && string.IsNullOrEmpty(entity.ReviseNo))
            {
                EditReviseFileCreateReviseNo(ref entity);
            }
            this.Entity = entity;

            if (!this.Entity.IsSyncWeiXin.HasValue || !this.Entity.IsSyncWeiXin.Value)
                UploadNewsThumbnails(newThumbnails, "add");
            tempBaseResultDataValue.success = this.Add();
            if (tempBaseResultDataValue.success)
            {
                if (entity.Status != int.Parse(FFileStatus.暂存.Key))
                {
                    //保存文档操作记录
                    AddFFileOperation(entity, fFileOperationType, ffileOperationMemo);
                }
                //保存文档抄送对象信息
                if (ffileCopyUserType != -1 || (fFileCopyUserList != null && fFileCopyUserList.Count > 0))
                {
                    AddFFileCopyUser(entity, fFileCopyUserList, "Add", ffileCopyUserType);
                }
                //保存文档抄送对象信息
                if (ffileReadingUserType != -1 || (fFileReadingUserList != null && fFileReadingUserList.Count > 0))
                {
                    tempBaseResultDataValue.success = AddFFileReadingUser(entity, fFileReadingUserList, "Edit", ffileReadingUserType);
                    if (tempBaseResultDataValue.success == false)
                    {
                        tempBaseResultDataValue.ErrorInfo = "保存文档抄送对象信息错误!";
                    }
                }
                EditInvalidOfRelease(entity, "Add");
            }
            return tempBaseResultDataValue;
        }
        private void EditReviseFileCreateReviseNo(ref FFile entity)
        {
            //QMS文档修订号规则:修订号规则:固定前缀|变化前缀(默认取年份)|当前序号
            IList<BParameter> tempList = IDBParameterDao.GetListByHQL("bparameter.ParaNo='" + BParameterParaNo.QMSFFReviseNoRule + "'");
            if (tempList == null || tempList.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Warn("获取系统参数[QMS文档修订号规则]信息为空,系统不处理修订文档的文件修订号信息!");
                return;
            }
            BParameter bparameter = tempList[0];
            string paraValue = bparameter.ParaValue.Trim();
            if (string.IsNullOrEmpty(paraValue))
            {
                ZhiFang.Common.Log.Log.Warn("获取系统参数[QMS文档修订号规则],参数值为空,系统不处理修订文档的文件修订号信息!");
                return;
            }
            if (paraValue.IndexOf('|') < 0)
            {
                ZhiFang.Common.Log.Log.Warn("获取系统参数[QMS文档修订号规则],参数值配置不正确,系统不处理修订文档的文件修订号信息!");
                return;
            }
            string[] arrStr = bparameter.ParaValue.Trim().Split('|');
            if (arrStr.Length != 3)
            {
                ZhiFang.Common.Log.Log.Warn("获取系统参数[QMS文档修订号规则],参数值配置不正确,系统不处理修订文档的文件修订号信息!");
                return;
            }
            string prefixStr1 = arrStr[0].Trim();
            string prefixStr2 = arrStr[1].Trim();
            string curNoStr = arrStr[2].Trim();
            //变化前缀等于默认值"0000",默认取当前年份
            if (!string.IsNullOrEmpty(prefixStr2) && prefixStr2 == "0000")
            {
                prefixStr2 = DateTime.Now.ToString("yyyy");
            }

            int serialNoLen = curNoStr.Length;//当前序号的长度
            if (string.IsNullOrEmpty(curNoStr))
            {
                curNoStr = "0000";
                serialNoLen = 4;
            }
            long curNo = 0;
            long.TryParse(curNoStr, out curNo);
            curNo = curNo + 1;
            if (curNo.ToString().Length > serialNoLen)
                serialNoLen = curNo.ToString().Length;
            curNoStr = curNo.ToString().PadLeft(serialNoLen, '0');//左补零
            StringBuilder reviseNo = new StringBuilder();
            reviseNo.Append(prefixStr1);
            reviseNo.Append(prefixStr2);
            reviseNo.Append(curNoStr);
            entity.ReviseNo = reviseNo.ToString();
            ZhiFang.Common.Log.Log.Warn(string.Format("文件名称为:{0},文档修订号为:{1},修订日期为:{2}!", entity.Title, entity.ReviseNo, DateTime.Now.ToString()));
            paraValue = prefixStr1 + "|" + prefixStr2 + "|" + curNoStr;
            bparameter.ParaValue = paraValue;
            IBBParameter.Entity = bparameter;
            bool result = IBBParameter.Edit();
            if (result == false)
            {
                ZhiFang.Common.Log.Log.Error(string.Format("文件名称为:{0},文档修订号为:{1},修订日期为:{2}!,更新系统参数[QMS文档修订号规则]的参数值:{3},保存失败!", entity.Title, entity.ReviseNo, DateTime.Now.ToString(), bparameter.ParaValue));
            }
        }
        public BaseResultDataValue UploadNewsThumbnails(object newThumbnails, string type)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (this.Entity == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "新闻缩略图Entity信息为空！";
                brdv.ResultDataValue = "";
                ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
            }
            else if (newThumbnails == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "未检测到文件！";
                brdv.ResultDataValue = "";
            }
            if (brdv.success)
            {

                string savePath = SysPublicSet.FFileNews.ThumbServerPath;
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                string fileName = this.Entity.Id + ".png";
                string filepath = Path.Combine(savePath, fileName);
                //ZhiFang.Common.Log.Log.Error(filepath);
                if (newThumbnails != null)
                {
                    var d = (HttpPostedFile)newThumbnails;
                    d.SaveAs(filepath);
                    //string savePath = tempPath + fileName;
                    //ZhiFang.Common.Log.Log.Debug("UploadNewsThumbnails.SaveAs=" +  savePath );
                    this.Entity.ThumbnailsPath = "" + SysPublicSet.FFileNews.ThumbPath + fileName + "";

                    brdv.ResultDataValue = SysPublicSet.FFileNews.ThumbPath + fileName.Replace("\\\\", "/");
                    if (type.ToLower() == "update")
                    {
                        List<string> tmpa = new List<string>();
                        tmpa.Add("Id=" + this.Entity.Id);
                        tmpa.Add("ThumbnailsPath='" + SysPublicSet.FFileNews.ThumbPath + fileName + "'");
                        //ZhiFang.Common.Log.Log.Debug("UploadNewsThumbnails.ThumbnailsPath=" + "ThumbnailsPath='" + savePath + "'" + "");
                        var tempArray = tmpa.ToArray();
                        this.Update(tempArray);
                    }
                }

            }
            return brdv;
        }


        #endregion

    }
}