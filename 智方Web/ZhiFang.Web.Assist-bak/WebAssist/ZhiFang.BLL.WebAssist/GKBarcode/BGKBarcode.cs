using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.WebAssist.GKBarcode;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.BLL.WebAssist
{
    public class BGKBarcode : IBGKBarcode
    {
        IBGKSampleRequestForm IBGKSampleRequestForm { get; set; }
        IBSCRecordTypeItem IBSCRecordTypeItem { get; set; }
        IDGKSampleRequestFormDao IDGKSampleRequestFormDao { get; set; }
        IDSCRecordTypeItemDao IDSCRecordTypeItemDao { get; set; }

        IBSCRecordDtl IBSCRecordDtl { get; set; }
        IBLL.RBAC.IBDepartment IBDepartment { get; set; }
        IBLL.RBAC.IBPUser IBPUser { get; set; }
        IBDepartmentUser IBDepartmentUser { get; set; }
        IBSCRecordPhrase IBSCRecordPhrase { get; set; }
        IBSCRecordItemLink IBSCRecordItemLink { get; set; }
        IBSCRecordType IBSCRecordType { get; set; }

        #region LIS同步原院感科室人员息
        /// <summary>
        /// LIS同步原院感科室人员息
        /// </summary>
        /// <returns></returns>
        public BaseResultBool SaveSyncGKBarcodeInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();

            IList<ZhiFang.Entity.RBAC.Department> lisDeptList = IBDepartment.LoadAll();
            IList<ZhiFang.Entity.RBAC.PUser> lisPUserList = IBPUser.LoadAll();
            IList<ZhiFang.Entity.RBAC.DepartmentUser> lisdeptPUserList = IBDepartmentUser.LoadAll();

            IList<Entity.WebAssist.GKBarcode.Department> gkDeptList = GetDepartmentListByHQL("");
            IList<Entity.WebAssist.GKBarcode.User> gkUserList = GetUserListByHQL("");

            //Dictionary<ZhiFang.Entity.RBAC.Department, ZhiFang.Entity.RBAC.PUser> dictList = new Dictionary<ZhiFang.Entity.RBAC.Department, ZhiFang.Entity.RBAC.PUser>();

            IList<Tuple<Entity.RBAC.Department, Entity.RBAC.PUser>> dictList = new List<Tuple<Entity.RBAC.Department, Entity.RBAC.PUser>>();

            IList<int> addPUserList = new List<int>();

            int multiple = 10000;//用户编码扩大的倍数
            //gkDeptList包含有科室及科室人员关系信息
            foreach (var entity in gkDeptList)
            {
                if (!entity.Dglab_Index.HasValue) continue;

                //先找到LIS的科室信息
                var tempDept = lisDeptList.Where(p => p.Id == entity.Dglab_Index.Value);
                if (tempDept == null || tempDept.Count() <= 0) continue;

                //是否存在科室人员关系信息
                //^133^,^132^,^140^,^141^,^137^,^136^,^142^,^139^,^134^,
                if (entity.Users.Length > 0)
                {
                    string[] userArr = entity.Users.Replace("^", "").Split(',');
                    foreach (var item in userArr)
                    {
                        if (item.Length > 0)
                        {
                            //找出原院感系统的人员信息
                            var tempUser = gkUserList.Where(p => p.UserID == int.Parse(item));
                            if (tempUser == null || tempUser.Count() <= 0) continue;

                            //判断是否已经导入LIS
                            var tempLisUser2 = lisPUserList.Where(p => p.Id == tempUser.ElementAt(0).UserID * multiple);
                            if (tempLisUser2.Count() > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug(string.Format("用户编码为:{0},姓名为:{1},已经导入到LIS", tempUser.ElementAt(0).UserID * multiple, tempUser.ElementAt(0).UserName));
                                continue;
                            }

                            //按人员姓名从Lis进行比较
                            var tempLisUser = lisPUserList.Where(p => p.CName == tempUser.ElementAt(0).UserName);
                            //如果原院感人员信息不存在LIS系统里
                            if (tempLisUser == null || tempLisUser.Count() <= 0)
                            {
                                ZhiFang.Common.Log.Log.Debug(string.Format("用户编码为:{0},姓名为:{1},不存在LIS中", tempUser.ElementAt(0).UserID * multiple, tempUser.ElementAt(0).UserName));

                                if (!addPUserList.Contains(tempUser.ElementAt(0).UserID * multiple))
                                {
                                    //在PUser新增人员信息-暂时不自动建立，由临床自己注册及导入

                                    //ZhiFang.Entity.RBAC.PUser puser = SavePUserOfGKUser(tempUser.ElementAt(0), entity.Dglab_Index.Value, multiple);
                                    //dictList.Add(Tuple.Create(tempDept.ElementAt(0), puser));
                                    //lisPUserList.Add(puser);
                                }
                            }
                            else if (tempLisUser.Count() > 0)
                            {
                                //按姓名找出多个
                                foreach (var item2 in tempLisUser)
                                {
                                    dictList.Add(Tuple.Create(tempDept.ElementAt(0), item2));
                                }
                            }
                        }
                    }
                }
            }
            List<string> tmpa = new List<string>();

            IList<string> deptUserIdList = new List<string>();

            //建立科室人员关系
            foreach (var item in dictList)
            {
                string deptUserId = item.Item1.Id.ToString() + item.Item2.ToString();
                if (deptUserIdList.Contains(deptUserId)) continue;

                deptUserIdList.Add(deptUserId);
                var tempList = lisdeptPUserList.Where(p => p.Department.Id == item.Item1.Id && p.PUser.Id == item.Item2.Id);
                if (tempList.Count() > 0) continue;

                ZhiFang.Entity.RBAC.DepartmentUser deptUser = new Entity.RBAC.DepartmentUser();
                deptUser.Department = item.Item1;
                deptUser.PUser = item.Item2;
                deptUser.IsUse = true;
                deptUser.DispOrder = deptUser.PUser.Id;
                IBDepartmentUser.Entity = deptUser;
                baseResultBool.success = IBDepartmentUser.Add();

                //更新人员所属科室信息
                tmpa.Clear();
                tmpa.Add("Id=" + item.Item2.Id);
                tmpa.Add("Department.Id=" + item.Item1.Id);
                IBPUser.Entity = item.Item2;
                string[] tempArray = tmpa.ToArray();
                IBPUser.Update(tempArray);
            }
            if (baseResultBool.success == true)
            {
                baseResultBool.BoolInfo = "LIS同步原院感科室信息信息成功!";
                baseResultBool.ErrorInfo = baseResultBool.BoolInfo;
            }
            return baseResultBool;
        }
        public Entity.RBAC.PUser SavePUserOfGKUser(Entity.WebAssist.GKBarcode.User user, int deptNo, int multiple)
        {
            Entity.RBAC.PUser puser = new Entity.RBAC.PUser();
            puser.Id = user.UserID * multiple;
            puser.CName = user.UserName;
            puser.Department = IBDepartment.Get(deptNo);
            puser.Visible = 1;
            puser.DispOrder = user.UserID;//存原用户编码
            puser.ShortCode = puser.Id.ToString();//登记帐号 user.QueryCode.ToLower();
            string pwd = "123456";
            if (user.PassWord.Length > 0) pwd = user.PassWord;
            puser.Password = IBPUser.CovertPassWord(pwd);
            puser.Gender = 2;
            puser.Role = "护士角色";
            puser.Usertype = BloodIdentityType.护士.Key;
            puser.DataAddTime = DateTime.Now;

            IBPUser.Entity = puser;
            IBPUser.Add();

            ZhiFang.Common.Log.Log.Debug(string.Format("新增用户编码为:{0},姓名为:{1}", user.UserID, user.UserName));
            return puser;
        }

        public IList<Entity.WebAssist.GKBarcode.Department> GetDepartmentListByHQL(string strSqlWhere)
        {
            IList<Entity.WebAssist.GKBarcode.Department> lisList = DataAccess_SQL.CreateDepartment_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }

        public IList<FeeSetUp> GetFeeSetUpListByHQL(string strSqlWhere)
        {
            IList<FeeSetUp> lisList = DataAccess_SQL.CreateFeeSetUp_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }

        public IList<GKBarRed> GetGKBarRedListByHQL(string strSqlWhere)
        {
            IList<GKBarRed> lisList = DataAccess_SQL.CreateGKBarRed_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }

        public IList<LastBarcodeS> GetLastBarcodeSListByHQL(string strSqlWhere)
        {
            IList<LastBarcodeS> lisList = DataAccess_SQL.CreateLastBarcodeS_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }

        public IList<OperateType> GetOperateTypeListByHQL(string strSqlWhere)
        {
            IList<OperateType> lisList = DataAccess_SQL.CreateOperateType_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }

        public IList<TestTypeInfo> GetTestTypeInfoListByHQL(string strSqlWhere)
        {
            IList<TestTypeInfo> lisList = DataAccess_SQL.CreateTestTypeInfo_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }

        public IList<Entity.WebAssist.GKBarcode.TestType> GetTestTypeListByHQL(string strSqlWhere)
        {
            IList<Entity.WebAssist.GKBarcode.TestType> lisList = DataAccess_SQL.CreateTestType_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }

        public IList<User> GetUserListByHQL(string strSqlWhere)
        {
            IList<User> lisList = DataAccess_SQL.CreateUser_SQL().GetListByHQL(strSqlWhere);
            return lisList;
        }
        #endregion

        #region LIS同步原院感科室记录项的结果短语信息
        public BaseResultBool SaveSyncTestTypeInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.success = true;

            IList<ZhiFang.Entity.RBAC.Department> lisDeptList = IBDepartment.LoadAll();
            IList<SCRecordPhrase> phraseList = IBSCRecordPhrase.LoadAll();
            IList<SCRecordItemLink> itemLinkList = IBSCRecordItemLink.LoadAll();

            IList<Entity.WebAssist.GKBarcode.Department> gkDeptList = GetDepartmentListByHQL("");
            IList<TestTypeInfo> gkTypeInfoList = DataAccess_SQL.CreateTestTypeInfo_SQL().GetListByHQL("");

            //先按科室分组
            var groupByDeptList = gkTypeInfoList.Where(p => p.DepID.HasValue == true).GroupBy(p => p.DepID.Value);

            #region foreach Department
            foreach (var groupByDept in groupByDeptList)
            {
                //从院感科室里找到LIS科室对照码
                var gkDepartmentList = gkDeptList.Where(p => p.DepID == groupByDept.Key);
                if (gkDepartmentList.Count() <= 0) continue;

                Entity.WebAssist.GKBarcode.Department gkDepartment = gkDepartmentList.ElementAt(0);
                var litDepartment = lisDeptList.Where(p => gkDepartment.Dglab_Index.HasValue == true && p.Id == gkDepartment.Dglab_Index);
                if (litDepartment != null && litDepartment.Count() <= 0)
                    continue;

                ZhiFang.Entity.RBAC.Department lisDept = litDepartment.ElementAt(0);

                //再按监测类型分组
                var groupByTestTypeList = groupByDept.GroupBy(p => p.TestTypeID);

                #region foreach SCRecordType
                foreach (var groupByTestType in groupByTestTypeList)
                {
                    //再按监测类型的记录项分组
                    var groupByTestItemList = groupByTestType.GroupBy(p => p.InfoName);

                    //监测类型的记录项关系集合信息
                    var itemLinkList2 = itemLinkList.Where(p => p.SCRecordType.Id == groupByTestType.Key).OrderBy(p => p.DispOrder); ;

                    #region foreach SCRecordTypeItem
                    foreach (var groupByTestItem in groupByTestItemList)
                    {
                        foreach (var item in groupByTestItem)
                        {
                            #region Find SCRecordTypeItem
                            SCRecordTypeItem typeItem = null;
                            if (item.InfoName == "information1" && itemLinkList2.Count() >= 1)
                            {
                                //监测类型的第一项
                                typeItem = itemLinkList2.ElementAt(0).SCRecordTypeItem;
                            }
                            else if (item.InfoName == "information2" && itemLinkList2.Count() >= 2)
                            {
                                //监测类型的第二项
                                typeItem = itemLinkList2.ElementAt(1).SCRecordTypeItem;
                            }
                            else if (item.InfoName == "information3" && itemLinkList2.Count() >= 3)
                            {
                                //监测类型的第三项
                                typeItem = itemLinkList2.ElementAt(2).SCRecordTypeItem;
                            }
                            else if (item.InfoName == "information4" && itemLinkList2.Count() >= 4)
                            {
                                //监测类型的第四项
                                typeItem = itemLinkList2.ElementAt(3).SCRecordTypeItem;
                            }

                            if (typeItem == null)
                                continue;

                            //是否已经在
                            var phraseList2 = phraseList.Where(p => p.TypeObjectId == lisDept.Id && p.BObjectId == typeItem.Id && p.CName == item.InfoText);
                            if (phraseList2.Count() > 0) continue;

                            #endregion

                            #region AddSCRecordPhrase
                            SCRecordPhrase phrase = new SCRecordPhrase();
                            phrase.PhraseType = int.Parse(PhraseType.按科室.Key);
                            phrase.IsUse = true;
                            phrase.TypeObjectId = lisDept.Id;
                            phrase.BObjectId = typeItem.Id;
                            phrase.CName = item.InfoText;

                            phrase.PinYinZiTou = GetPinYin(phrase.CName);
                            phrase.SName = phrase.PinYinZiTou;
                            phrase.ShortCode = phrase.PinYinZiTou;
                            IBSCRecordPhrase.Entity = phrase;
                            IBSCRecordPhrase.Add();
                            ZhiFang.Common.Log.Log.Info(string.Format("新增科室为:{0},记录项名称为:{1},的结果短语:{2}", lisDept.CName, typeItem.CName, phrase.CName));
                            #endregion

                        }
                    }
                    #endregion
                }
                #endregion

            }
            #endregion

            return baseResultBool;
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

        public BaseResultBool SaveSyncDeptPhraseInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.success = true;

            return baseResultBool;
        }
        #endregion

        #region LIS同步原院感申请记录信息
        /// <summary>
        /// LIS同步原院感申请记录信息
        /// </summary>
        /// <returns></returns>
        public BaseResultBool SaveSyncGKBarRedInfo()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            IGKBarRed_SQL IGKBarRed_SQL = DataAccess_SQL.CreateGKBarRed_SQL();
            //只取已经有检验结果的记录
            IList<Entity.WebAssist.GKBarcode.GKBarRed> gkBarRedList = IGKBarRed_SQL.GetListByHQL("(IsSync=0 or IsSync is null) and Apply='1' and Technician is not null ");
            ZhiFang.Common.Log.Log.Debug("gkBarRedList.Count:" + gkBarRedList.Count());
            if (gkBarRedList == null || gkBarRedList.Count <= 0)
            {
                baseResultBool.success = true;
                baseResultBool.ErrorInfo = "没有需要同步导入的数据！";
                return baseResultBool;
            }

            IList<ZhiFang.Entity.RBAC.Department> lisDeptList = IBDepartment.LoadAll();
            IList<ZhiFang.Entity.RBAC.PUser> lisPUserList = IBPUser.LoadAll();
            IList<ZhiFang.Entity.RBAC.DepartmentUser> lisDeptUserList = IBDepartmentUser.LoadAll();

            IList<SCRecordItemLink> itemLinkList = IBSCRecordItemLink.LoadAll();
            IList<SCRecordType> recordTypeList = IBSCRecordType.LoadAll();
            IList<Entity.WebAssist.GKBarcode.Department> gkDeptList = GetDepartmentListByHQL("");
            IList<Entity.WebAssist.GKBarcode.User> gkUserList = GetUserListByHQL("");

            var gkBarRedList2 = gkBarRedList.GroupBy(p => p.DepID);
            IList<GKSampleRequestForm> lisGKDocList = IDGKSampleRequestFormDao.GetListByHQL("length(gksamplerequestform.BarCode)<=10");
            ZhiFang.Common.Log.Log.Debug("lisGKDocList.Count:" + lisGKDocList.Count());

            foreach (var gkGroupBy in gkBarRedList2)
            {
                if (!gkGroupBy.Key.HasValue) continue;

                //找出科室对照信息
                var list2 = gkDeptList.Where(p => p.DepID == gkGroupBy.Key.Value).ToList();
                if (list2 == null || list2.Count() <= 0) continue;
                if (!list2[0].Dglab_Index.HasValue) continue;
                int lisDeptId = list2[0].Dglab_Index.Value;
                var list3 = lisDeptList.Where(p => p.Id == lisDeptId).ToList();
                if (list3 == null || list3.Count() <= 0) continue;

                ZhiFang.Entity.RBAC.Department dept = list3[0];

                foreach (var item in gkGroupBy)
                {
                    GKBarRed barRed = item;
                    var lisGKDocList2 = lisGKDocList.Where(p => p.BarCode == barRed.BarCode);
                    if (lisGKDocList2 == null || lisGKDocList2.Count() <= 0)
                    {
                        //不存在
                        baseResultBool = AddGKBarRedInfo(lisPUserList, lisDeptUserList, gkUserList, recordTypeList, itemLinkList, dept, barRed);
                        barRed.IsSync = true;
                        IGKBarRed_SQL.UpdateIsSync(barRed, true);
                    }
                    else
                    {
                        //已存在

                    }

                }
            }

            return baseResultBool;
        }
        private BaseResultBool AddGKBarRedInfo(IList<ZhiFang.Entity.RBAC.PUser> lisPUserList, IList<ZhiFang.Entity.RBAC.DepartmentUser> lisDeptUserList, IList<Entity.WebAssist.GKBarcode.User> gkUserList, IList<SCRecordType> recordTypeList, IList<SCRecordItemLink> itemLinkList, ZhiFang.Entity.RBAC.Department dept, GKBarRed barRed)
        {
            BaseResultBool brdv = new BaseResultBool();

            GKSampleRequestForm docEntity = new GKSampleRequestForm();

            long id2 = barRed.SerialNo;
            if (id2 > 0)
            {
                id2 = id2 * 1000000;
                docEntity.Id = id2;
            }

            docEntity.SCRecordType = recordTypeList.Where(p => p.Id == long.Parse(barRed.TestTpyeID.Value.ToString())).ElementAt(0);
            
            #region 申请主单封装
            if (string.IsNullOrEmpty(barRed.MonitorType))
            {
                docEntity.MonitorType = 2;
            }
            else
            {
                docEntity.MonitorType = 1;
            }
            docEntity.StatusID = int.Parse(GKSampleFormStatus.已提交.Key);
            docEntity.DeptId = dept.Id;
            docEntity.DeptCName = dept.CName;

            docEntity.Visible = true;
            docEntity.IsAutoReceive = false;
            docEntity.ReqDocNo = barRed.BarCode;
            docEntity.BarCode = barRed.BarCode;
            docEntity.DispOrder = barRed.SerialNo;
            docEntity.CName = barRed.Information1;//
            docEntity.PrintCount = 0;
            docEntity.ReceiveFlag = false;//核收标志
            docEntity.ResultFlag = false;//结果回写标志
            //创建人处理ChroniclerID 是登录人的ID
            //entity.CreatorID = empID;
            //entity.CreatorName = empName;

            docEntity.SampleDate = barRed.RecDate;
            string sampleDate = "", sampleTime = "";
            if (barRed.RecDate.HasValue)
                sampleDate = barRed.RecDate.Value.ToString("yyyy-MM-dd");
            if (barRed.RecTime.HasValue)
                sampleTime = barRed.RecTime.Value.ToString("HH:mm:ss");
            if (!string.IsNullOrEmpty(sampleDate) && !string.IsNullOrEmpty(sampleTime))
                docEntity.SampleTime = DateTime.Parse(sampleDate + " " + sampleTime);

            //采样人处理 CollecterID
            if (barRed.CollecterID.HasValue)
            {
                var gkUserList1 = gkUserList.Where(p => p.UserID == barRed.CollecterID.Value);
                if (gkUserList1 != null && gkUserList1.Count() > 0)
                {
                    docEntity.Sampler = gkUserList1.ElementAt(0).UserName;
                }

                if (!string.IsNullOrEmpty(docEntity.Sampler))
                {
                    var lisDeptUserList2 = lisDeptUserList.Where(p => p.Department.Id == dept.Id && p.PUser.CName == docEntity.Sampler);
                    if (lisDeptUserList2 != null && lisDeptUserList2.Count() > 0)
                    {
                        docEntity.SamplerId = lisDeptUserList2.ElementAt(0).PUser.Id;
                    }
                    else
                    {
                        var lisPUserList2 = lisPUserList.Where(p => p.CName == docEntity.Sampler);
                        if (lisPUserList2 != null && lisPUserList2.Count() > 0)
                            docEntity.SamplerId = lisPUserList2.ElementAt(0).Id;
                    }
                }
            }

            if (!string.IsNullOrEmpty(barRed.RecievedInfor))
            {
                docEntity.ReceiveFlag = true;//核收标志
                docEntity.StatusID = int.Parse(GKSampleFormStatus.已核收.Key);
                //隆绍平2019-07-26 16:27:07
                docEntity.ReceiveId = 26;//隆绍平的员工编码为26
                string recievedInfor = "隆绍平";
                docEntity.ReceiveCName = recievedInfor;
                string receiveDateStr = barRed.RecievedInfor.Replace("隆绍平", "");
                DateTime receiveDate = DateTime.Now;
                bool result2 = DateTime.TryParse(receiveDateStr, out receiveDate);
                if (result2) docEntity.ReceiveDate = receiveDate;

            }

            //审核信息
            docEntity.CheckCName = barRed.Technician;
            docEntity.CheckDate = barRed.TestDate;
            if (!string.IsNullOrEmpty(docEntity.CheckCName))
            {
                docEntity.ReceiveFlag = true;//核收标志
                docEntity.ResultFlag = true;//结果回写标志
                docEntity.StatusID = int.Parse(GKSampleFormStatus.已返结果.Key);

                var lisPUserList2 = lisPUserList.Where(p => p.CName == docEntity.CheckCName);
                if (lisPUserList2 != null && lisPUserList2.Count() > 0)
                {
                    docEntity.CheckId = lisPUserList2.ElementAt(0).Id;
                }
                else
                {
                    var lisPUserList4 = lisPUserList.Where(p => p.CName == docEntity.CheckCName);
                    if (lisPUserList4 != null && lisPUserList4.Count() > 0)
                        docEntity.CheckId = lisPUserList4.ElementAt(0).Id;
                }

            }
            docEntity.TestResult = barRed.TestResult;
            docEntity.BacteriaTotal = barRed.TestResult;//细菌总数
            if (barRed.Judge == GKSampleFormJudgment.合格.Key || barRed.Judge == "1" ||
            barRed.Judge == "true" || barRed.Judge == "合格")
            {
                docEntity.Judgment = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.合格.Key].Id;
            }
            else if (!string.IsNullOrEmpty(barRed.Judge))
            {
                docEntity.Judgment = GKSampleFormJudgment.GetStatusDic()[GKSampleFormJudgment.不合格.Key].Id;
            }
            docEntity.Evaluators = barRed.Judge_Operator;
            docEntity.EvaluationDate = barRed.Judge_Date;
            if (!string.IsNullOrEmpty(docEntity.Evaluators))
            {
                docEntity.ReceiveFlag = true;//核收标志
                docEntity.ResultFlag = true;//结果回写标志
                docEntity.EvaluatorFlag = true;//评估标志
                docEntity.StatusID = int.Parse(GKSampleFormStatus.已评价.Key);

                var lisPUserList2 = lisPUserList.Where(p => p.CName == docEntity.Evaluators);
                if (lisPUserList2 != null && lisPUserList2.Count() > 0)
                {
                    docEntity.EvaluatorId = lisPUserList2.ElementAt(0).Id;
                }
                else
                {
                    var lisPUserList3 = lisPUserList.Where(p => p.CName == docEntity.Evaluators);
                    if (lisPUserList3 != null && lisPUserList2.Count() > 0)
                        docEntity.EvaluatorId = lisPUserList3.ElementAt(0).Id;
                }
            }

            if (!string.IsNullOrEmpty(barRed.Archived))
            {
                docEntity.ReceiveFlag = true;//核收标志
                docEntity.ResultFlag = true;//结果回写标志
                docEntity.Archived = true;//归档标志
                docEntity.StatusID = int.Parse(GKSampleFormStatus.已归档.Key);
            }
            #endregion

            #region 记录明细项处理
            IList<SCRecordDtl> dtlAddList = new List<SCRecordDtl>();
            var itemLinkList2 = itemLinkList.Where(p => p.SCRecordType.Id == docEntity.SCRecordType.Id).OrderBy(p => p.DispOrder).ToList();
            for (int i = 0; i < itemLinkList2.Count(); i++)
            {
                var itemLink = itemLinkList2[i];

                SCRecordDtl dtlEntity = new SCRecordDtl();
                dtlEntity.Id = docEntity.Id + i + 1;

                dtlEntity.RecordDtlNo = docEntity.BarCode + (i + 1).ToString().PadLeft(2, '0');//补两位
                dtlEntity.DispOrder = itemLink.DispOrder;
                dtlEntity.SCRecordType = docEntity.SCRecordType;
                dtlEntity.SCRecordTypeItem = itemLink.SCRecordTypeItem;
                dtlEntity.Visible = true;
                dtlEntity.BObjectID = docEntity.Id;
                dtlEntity.DataAddTime = DateTime.Now;
                dtlEntity.ContentTypeID = int.Parse(SCRecordTypeContentType.院感登记.Key);

                switch (i)
                {
                    case 0:
                        dtlEntity.ItemResult = barRed.Information1;
                        break;
                    case 1:
                        dtlEntity.ItemResult = barRed.Information2;
                        break;
                    case 2:
                        dtlEntity.ItemResult = barRed.Information3;
                        break;
                    case 3:
                        dtlEntity.ItemResult = barRed.Information4;
                        break;
                    default:
                        break;
                }
                dtlAddList.Add(dtlEntity);
            }

            #endregion

            docEntity.DataAddTime = docEntity.SampleTime;
            IBGKSampleRequestForm.Entity = docEntity;
            bool result = IBGKSampleRequestForm.Add();
            if (result == false)
            {
                brdv.success = false;
                brdv.ErrorInfo = string.Format("监测科室为:{0},监测类型为:{1},新增院感登记保存失败!", docEntity.DeptCName, docEntity.SCRecordType.CName);
                return brdv;
            }

            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (docEntity.DataTimeStamp == null)
                docEntity.DataTimeStamp = dataTimeStamp;
            foreach (var item in dtlAddList)
            {
                IBSCRecordDtl.Entity = item;
                bool result2 = IBSCRecordDtl.Add();
                if (!result2)
                {
                    brdv.success = false;
                }
            }
            return brdv;
        }
        #endregion

    }
}
