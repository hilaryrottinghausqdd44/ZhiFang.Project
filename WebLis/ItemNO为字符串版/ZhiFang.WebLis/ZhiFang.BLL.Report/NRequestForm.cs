using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
namespace ZhiFang.BLL.Report
{
    /// <summary>
    /// NRequestForm
    /// </summary>
    public partial class NRequestForm : ZhiFang.IBLL.Report.IBNRequestForm
    {
        private readonly IDNRequestForm dal = DalFactory<IDNRequestForm>.GetDalByClassName("NRequestForm");
        private readonly IDTestItemControl idtic = DalFactory<IDTestItemControl>.GetDalByClassName("B_TestItemControl.");
        private readonly IDSampleTypeControl idstc = DalFactory<IDSampleTypeControl>.GetDalByClassName("B_SampleTypeControl");
        private readonly IDGenderTypeControl idgtc = DalFactory<IDGenderTypeControl>.GetDalByClassName("B_GenderTypeControl");
        private readonly IDNRequestItem DNRequestItem = DalFactory<IDNRequestItem>.GetDalByClassName("NRequestItem");
        private readonly IDBarCodeForm DBarCodeForm = DalFactory<IDBarCodeForm>.GetDalByClassName("BarCodeForm");
        public NRequestForm()
        {

        }
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.NRequestForm model)
        {
            return dal.Add(model);
        }
        public int Add_PKI(Model.NRequestForm model)
        {
            return dal.Add_PKI(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Model.NRequestForm model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(string SerialNo)
        {
            return dal.Delete(SerialNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.NRequestForm GetModel(long NRequestFormNo)
        {

            return dal.GetModel(NRequestFormNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.NRequestForm GetModelByCache(long NRequestFormNo)
        {

            string CacheKey = "NRequestFormModel-" + NRequestFormNo.ToString();
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(NRequestFormNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.NRequestForm)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.NRequestForm> DataTableToList(DataTable dt)
        {
            List<Model.NRequestForm> modelList = new List<Model.NRequestForm>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.NRequestForm model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.NRequestForm();
                    if (dt.Rows[n]["SerialNo"] != null && dt.Rows[n]["SerialNo"].ToString() != "")
                    {
                        model.SerialNo = dt.Rows[n]["SerialNo"].ToString();
                    }
                    if (dt.Rows[n]["ReceiveFlag"] != null && dt.Rows[n]["ReceiveFlag"].ToString() != "")
                    {
                        model.ReceiveFlag = int.Parse(dt.Rows[n]["ReceiveFlag"].ToString());
                    }
                    if (dt.Rows[n]["StatusNo"] != null && dt.Rows[n]["StatusNo"].ToString() != "")
                    {
                        model.StatusNo = int.Parse(dt.Rows[n]["StatusNo"].ToString());
                    }
                    if (dt.Rows[n]["SampleTypeNo"] != null && dt.Rows[n]["SampleTypeNo"].ToString() != "")
                    {
                        model.SampleTypeNo = int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
                    }
                    if (dt.Rows[n]["PatNo"] != null && dt.Rows[n]["PatNo"].ToString() != "")
                    {
                        model.PatNo = dt.Rows[n]["PatNo"].ToString();
                    }
                    if (dt.Rows[n]["CName"] != null && dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    if (dt.Rows[n]["GenderNo"] != null && dt.Rows[n]["GenderNo"].ToString() != "")
                    {
                        model.GenderNo = int.Parse(dt.Rows[n]["GenderNo"].ToString());
                    }
                    if (dt.Rows[n]["Birthday"] != null && dt.Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
                    }
                    if (dt.Rows[n]["Age"] != null && dt.Rows[n]["Age"].ToString() != "")
                    {
                        model.Age = decimal.Parse(dt.Rows[n]["Age"].ToString());
                    }
                    if (dt.Rows[n]["AgeUnitNo"] != null && dt.Rows[n]["AgeUnitNo"].ToString() != "")
                    {
                        model.AgeUnitNo = int.Parse(dt.Rows[n]["AgeUnitNo"].ToString());
                    }
                    if (dt.Rows[n]["FolkNo"] != null && dt.Rows[n]["FolkNo"].ToString() != "")
                    {
                        model.FolkNo = int.Parse(dt.Rows[n]["FolkNo"].ToString());
                    }
                    if (dt.Rows[n]["DistrictNo"] != null && dt.Rows[n]["DistrictNo"].ToString() != "")
                    {
                        model.DistrictNo = int.Parse(dt.Rows[n]["DistrictNo"].ToString());
                    }
                    if (dt.Rows[n]["WardNo"] != null && dt.Rows[n]["WardNo"].ToString() != "")
                    {
                        model.WardNo = int.Parse(dt.Rows[n]["WardNo"].ToString());
                    }
                    if (dt.Rows[n]["Bed"] != null && dt.Rows[n]["Bed"].ToString() != "")
                    {
                        model.Bed = dt.Rows[n]["Bed"].ToString();
                    }
                    if (dt.Rows[n]["DeptNo"] != null && dt.Rows[n]["DeptNo"].ToString() != "")
                    {
                        model.DeptNo = int.Parse(dt.Rows[n]["DeptNo"].ToString());
                    }
                    if (dt.Rows[n]["Doctor"] != null && dt.Rows[n]["Doctor"].ToString() != "")
                    {
                        model.Doctor = int.Parse(dt.Rows[n]["Doctor"].ToString());
                    }
                    if (dt.Rows[n]["DiagNo"] != null && dt.Rows[n]["DiagNo"].ToString() != "")
                    {
                        model.DiagNo = int.Parse(dt.Rows[n]["DiagNo"].ToString());
                    }
                    if (dt.Rows[n]["ChargeNo"] != null && dt.Rows[n]["ChargeNo"].ToString() != "")
                    {
                        model.ChargeNo = int.Parse(dt.Rows[n]["ChargeNo"].ToString());
                    }
                    if (dt.Rows[n]["Charge"] != null && dt.Rows[n]["Charge"].ToString() != "")
                    {
                        model.Charge = decimal.Parse(dt.Rows[n]["Charge"].ToString());
                    }
                    if (dt.Rows[n]["CollecterID"] != null && dt.Rows[n]["CollecterID"].ToString() != "")
                    {
                        model.CollecterID = dt.Rows[n]["CollecterID"].ToString();
                    }
                    if (dt.Rows[n]["Collecter"] != null && dt.Rows[n]["Collecter"].ToString() != "")
                    {
                        model.Collecter = dt.Rows[n]["Collecter"].ToString();
                    }
                    if (dt.Rows[n]["CollectDate"] != null && dt.Rows[n]["CollectDate"].ToString() != "")
                    {
                        model.CollectDate = DateTime.Parse(dt.Rows[n]["CollectDate"].ToString());
                    }
                    if (dt.Rows[n]["CollectTime"] != null && dt.Rows[n]["CollectTime"].ToString() != "")
                    {
                        model.CollectTime = DateTime.Parse(dt.Rows[n]["CollectTime"].ToString());
                    }
                    if (dt.Rows[n]["Operator"] != null && dt.Rows[n]["Operator"].ToString() != "")
                    {
                        model.Operator = dt.Rows[n]["Operator"].ToString();
                    }
                    if (dt.Rows[n]["OperDate"] != null && dt.Rows[n]["OperDate"].ToString() != "")
                    {
                        model.OperDate = DateTime.Parse(dt.Rows[n]["OperDate"].ToString());
                    }
                    if (dt.Rows[n]["OperTime"] != null && dt.Rows[n]["OperTime"].ToString() != "")
                    {
                        model.OperTime = DateTime.Parse(dt.Rows[n]["OperTime"].ToString());
                    }
                    if (dt.Rows[n]["FormMemo"] != null && dt.Rows[n]["FormMemo"].ToString() != "")
                    {
                        model.FormMemo = dt.Rows[n]["FormMemo"].ToString();
                    }
                    if (dt.Rows[n]["RequestSource"] != null && dt.Rows[n]["RequestSource"].ToString() != "")
                    {
                        model.RequestSource = dt.Rows[n]["RequestSource"].ToString();
                    }
                    if (dt.Rows[n]["Artificerorder"] != null && dt.Rows[n]["Artificerorder"].ToString() != "")
                    {
                        model.Artificerorder = dt.Rows[n]["Artificerorder"].ToString();
                    }
                    if (dt.Rows[n]["sickorder"] != null && dt.Rows[n]["sickorder"].ToString() != "")
                    {
                        model.sickorder = dt.Rows[n]["sickorder"].ToString();
                    }
                    if (dt.Rows[n]["chargeflag"] != null && dt.Rows[n]["chargeflag"].ToString() != "")
                    {
                        model.chargeflag = dt.Rows[n]["chargeflag"].ToString();
                    }
                    if (dt.Rows[n]["jztype"] != null && dt.Rows[n]["jztype"].ToString() != "")
                    {
                        model.jztype = int.Parse(dt.Rows[n]["jztype"].ToString());
                    }
                    if (dt.Rows[n]["zdy1"] != null && dt.Rows[n]["zdy1"].ToString() != "")
                    {
                        model.zdy1 = dt.Rows[n]["zdy1"].ToString();
                    }
                    if (dt.Rows[n]["zdy2"] != null && dt.Rows[n]["zdy2"].ToString() != "")
                    {
                        model.zdy2 = dt.Rows[n]["zdy2"].ToString();
                    }
                    if (dt.Rows[n]["zdy3"] != null && dt.Rows[n]["zdy3"].ToString() != "")
                    {
                        model.zdy3 = dt.Rows[n]["zdy3"].ToString();
                    }
                    if (dt.Rows[n]["zdy4"] != null && dt.Rows[n]["zdy4"].ToString() != "")
                    {
                        model.zdy4 = dt.Rows[n]["zdy4"].ToString();
                    }
                    if (dt.Rows[n]["zdy5"] != null && dt.Rows[n]["zdy5"].ToString() != "")
                    {
                        model.zdy5 = dt.Rows[n]["zdy5"].ToString();
                    }
                    if (dt.Rows[n]["FlagDateDelete"] != null && dt.Rows[n]["FlagDateDelete"].ToString() != "")
                    {
                        model.FlagDateDelete = DateTime.Parse(dt.Rows[n]["FlagDateDelete"].ToString());
                    }
                    if (dt.Rows[n]["FormComment"] != null && dt.Rows[n]["FormComment"].ToString() != "")
                    {
                        model.FormComment = dt.Rows[n]["FormComment"].ToString();
                    }
                    if (dt.Rows[n]["nurseflag"] != null && dt.Rows[n]["nurseflag"].ToString() != "")
                    {
                        model.nurseflag = dt.Rows[n]["nurseflag"].ToString();
                    }
                    if (dt.Rows[n]["diag"] != null && dt.Rows[n]["diag"].ToString() != "")
                    {
                        model.diag = dt.Rows[n]["diag"].ToString();
                    }
                    if (dt.Rows[n]["CaseNo"] != null && dt.Rows[n]["CaseNo"].ToString() != "")
                    {
                        model.CaseNo = dt.Rows[n]["CaseNo"].ToString();
                    }
                    if (dt.Rows[n]["refuseopinion"] != null && dt.Rows[n]["refuseopinion"].ToString() != "")
                    {
                        model.refuseopinion = dt.Rows[n]["refuseopinion"].ToString();
                    }
                    if (dt.Rows[n]["refusereason"] != null && dt.Rows[n]["refusereason"].ToString() != "")
                    {
                        model.refusereason = dt.Rows[n]["refusereason"].ToString();
                    }
                    if (dt.Rows[n]["signintime"] != null && dt.Rows[n]["signintime"].ToString() != "")
                    {
                        model.signintime = DateTime.Parse(dt.Rows[n]["signintime"].ToString());
                    }
                    if (dt.Rows[n]["signer"] != null && dt.Rows[n]["signer"].ToString() != "")
                    {
                        model.signer = dt.Rows[n]["signer"].ToString();
                    }
                    if (dt.Rows[n]["signflag"] != null && dt.Rows[n]["signflag"].ToString() != "")
                    {
                        model.signflag = int.Parse(dt.Rows[n]["signflag"].ToString());
                    }
                    if (dt.Rows[n]["SamplingGroupNo"] != null && dt.Rows[n]["SamplingGroupNo"].ToString() != "")
                    {
                        model.SamplingGroupNo = int.Parse(dt.Rows[n]["SamplingGroupNo"].ToString());
                    }
                    if (dt.Rows[n]["PrintCount"] != null && dt.Rows[n]["PrintCount"].ToString() != "")
                    {
                        model.PrintCount = int.Parse(dt.Rows[n]["PrintCount"].ToString());
                    }
                    if (dt.Rows[n]["PrintInfo"] != null && dt.Rows[n]["PrintInfo"].ToString() != "")
                    {
                        model.PrintInfo = dt.Rows[n]["PrintInfo"].ToString();
                    }
                    if (dt.Rows[n]["SampleCap"] != null && dt.Rows[n]["SampleCap"].ToString() != "")
                    {
                        model.SampleCap = decimal.Parse(dt.Rows[n]["SampleCap"].ToString());
                    }
                    if (dt.Rows[n]["IsPrep"] != null && dt.Rows[n]["IsPrep"].ToString() != "")
                    {
                        model.IsPrep = int.Parse(dt.Rows[n]["IsPrep"].ToString());
                    }
                    if (dt.Rows[n]["IsAffirm"] != null && dt.Rows[n]["IsAffirm"].ToString() != "")
                    {
                        model.IsAffirm = int.Parse(dt.Rows[n]["IsAffirm"].ToString());
                    }
                    if (dt.Rows[n]["IsSampling"] != null && dt.Rows[n]["IsSampling"].ToString() != "")
                    {
                        model.IsSampling = int.Parse(dt.Rows[n]["IsSampling"].ToString());
                    }
                    if (dt.Rows[n]["IsSend"] != null && dt.Rows[n]["IsSend"].ToString() != "")
                    {
                        model.IsSend = int.Parse(dt.Rows[n]["IsSend"].ToString());
                    }
                    if (dt.Rows[n]["incepter"] != null && dt.Rows[n]["incepter"].ToString() != "")
                    {
                        model.incepter = dt.Rows[n]["incepter"].ToString();
                    }
                    if (dt.Rows[n]["inceptTime"] != null && dt.Rows[n]["inceptTime"].ToString() != "")
                    {
                        model.inceptTime = DateTime.Parse(dt.Rows[n]["inceptTime"].ToString());
                    }
                    if (dt.Rows[n]["inceptDate"] != null && dt.Rows[n]["inceptDate"].ToString() != "")
                    {
                        model.inceptDate = DateTime.Parse(dt.Rows[n]["inceptDate"].ToString());
                    }
                    if (dt.Rows[n]["isByHand"] != null && dt.Rows[n]["isByHand"].ToString() != "")
                    {
                        if ((dt.Rows[n]["isByHand"].ToString() == "1") || (dt.Rows[n]["isByHand"].ToString().ToLower() == "true"))
                        {
                            model.isByHand = true;
                        }
                        else
                        {
                            model.isByHand = false;
                        }
                    }
                    if (dt.Rows[n]["AssignFlag"] != null && dt.Rows[n]["AssignFlag"].ToString() != "")
                    {
                        model.AssignFlag = int.Parse(dt.Rows[n]["AssignFlag"].ToString());
                    }
                    if (dt.Rows[n]["OldSerialNo"] != null && dt.Rows[n]["OldSerialNo"].ToString() != "")
                    {
                        model.OldSerialNo = dt.Rows[n]["OldSerialNo"].ToString();
                    }
                    if (dt.Rows[n]["TestTypeNo"] != null && dt.Rows[n]["TestTypeNo"].ToString() != "")
                    {
                        model.TestTypeNo = int.Parse(dt.Rows[n]["TestTypeNo"].ToString());
                    }
                    if (dt.Rows[n]["DispenseFlag"] != null && dt.Rows[n]["DispenseFlag"].ToString() != "")
                    {
                        model.DispenseFlag = int.Parse(dt.Rows[n]["DispenseFlag"].ToString());
                    }
                    if (dt.Rows[n]["refuseUser"] != null && dt.Rows[n]["refuseUser"].ToString() != "")
                    {
                        model.refuseUser = dt.Rows[n]["refuseUser"].ToString();
                    }
                    if (dt.Rows[n]["refuseTime"] != null && dt.Rows[n]["refuseTime"].ToString() != "")
                    {
                        model.refuseTime = DateTime.Parse(dt.Rows[n]["refuseTime"].ToString());
                    }
                    if (dt.Rows[n]["jytype"] != null && dt.Rows[n]["jytype"].ToString() != "")
                    {
                        model.jytype = dt.Rows[n]["jytype"].ToString();
                    }
                    if (dt.Rows[n]["SerialScanTime_old"] != null && dt.Rows[n]["SerialScanTime_old"].ToString() != "")
                    {
                        model.SerialScanTime_old = dt.Rows[n]["SerialScanTime_old"].ToString();
                    }
                    if (dt.Rows[n]["IsCheckFee"] != null && dt.Rows[n]["IsCheckFee"].ToString() != "")
                    {
                        model.IsCheckFee = int.Parse(dt.Rows[n]["IsCheckFee"].ToString());
                    }
                    if (dt.Rows[n]["Dr2Flag"] != null && dt.Rows[n]["Dr2Flag"].ToString() != "")
                    {
                        model.Dr2Flag = int.Parse(dt.Rows[n]["Dr2Flag"].ToString());
                    }
                    if (dt.Rows[n]["ExecDeptNo"] != null && dt.Rows[n]["ExecDeptNo"].ToString() != "")
                    {
                        model.ExecDeptNo = int.Parse(dt.Rows[n]["ExecDeptNo"].ToString());
                    }
                    if (dt.Rows[n]["ClientHost"] != null && dt.Rows[n]["ClientHost"].ToString() != "")
                    {
                        model.ClientHost = dt.Rows[n]["ClientHost"].ToString();
                    }
                    if (dt.Rows[n]["PreNumber"] != null && dt.Rows[n]["PreNumber"].ToString() != "")
                    {
                        model.PreNumber = int.Parse(dt.Rows[n]["PreNumber"].ToString());
                    }
                    if (dt.Rows[n]["UrgentState"] != null && dt.Rows[n]["UrgentState"].ToString() != "")
                    {
                        model.UrgentState = dt.Rows[n]["UrgentState"].ToString();
                    }
                    if (dt.Rows[n]["ZDY6"] != null && dt.Rows[n]["ZDY6"].ToString() != "")
                    {
                        model.ZDY6 = dt.Rows[n]["ZDY6"].ToString();
                    }
                    if (dt.Rows[n]["ZDY7"] != null && dt.Rows[n]["ZDY7"].ToString() != "")
                    {
                        model.ZDY7 = dt.Rows[n]["ZDY7"].ToString();
                    }
                    if (dt.Rows[n]["ZDY8"] != null && dt.Rows[n]["ZDY8"].ToString() != "")
                    {
                        model.ZDY8 = dt.Rows[n]["ZDY8"].ToString();
                    }
                    if (dt.Rows[n]["ZDY9"] != null && dt.Rows[n]["ZDY9"].ToString() != "")
                    {
                        model.ZDY9 = dt.Rows[n]["ZDY9"].ToString();
                    }
                    if (dt.Rows[n]["ZDY10"] != null && dt.Rows[n]["ZDY10"].ToString() != "")
                    {
                        model.ZDY10 = dt.Rows[n]["ZDY10"].ToString();
                    }
                    if (dt.Rows[n]["phoneCode"] != null && dt.Rows[n]["phoneCode"].ToString() != "")
                    {
                        model.phoneCode = dt.Rows[n]["phoneCode"].ToString();
                    }
                    if (dt.Rows[n]["IsNode"] != null && dt.Rows[n]["IsNode"].ToString() != "")
                    {
                        model.IsNode = int.Parse(dt.Rows[n]["IsNode"].ToString());
                    }
                    if (dt.Rows[n]["PhoneNodeCount"] != null && dt.Rows[n]["PhoneNodeCount"].ToString() != "")
                    {
                        model.PhoneNodeCount = int.Parse(dt.Rows[n]["PhoneNodeCount"].ToString());
                    }
                    if (dt.Rows[n]["AutoNodeCount"] != null && dt.Rows[n]["AutoNodeCount"].ToString() != "")
                    {
                        model.AutoNodeCount = int.Parse(dt.Rows[n]["AutoNodeCount"].ToString());
                    }
                    if (dt.Rows[n]["clientno"] != null && dt.Rows[n]["clientno"].ToString() != "")
                    {
                        model.ClientNo = dt.Rows[n]["clientno"].ToString();
                    }
                    if (dt.Rows[n]["SerialScanTime"] != null && dt.Rows[n]["SerialScanTime"].ToString() != "")
                    {
                        model.SerialScanTime = DateTime.Parse(dt.Rows[n]["SerialScanTime"].ToString());
                    }
                    if (dt.Rows[n]["CountNodesFormSource"] != null && dt.Rows[n]["CountNodesFormSource"].ToString() != "")
                    {
                        model.CountNodesFormSource = dt.Rows[n]["CountNodesFormSource"].ToString();
                    }
                    if (dt.Rows[n]["StateFlag"] != null && dt.Rows[n]["StateFlag"].ToString() != "")
                    {
                        model.StateFlag = int.Parse(dt.Rows[n]["StateFlag"].ToString());
                    }
                    if (dt.Rows[n]["AffirmTime"] != null && dt.Rows[n]["AffirmTime"].ToString() != "")
                    {
                        model.AffirmTime = DateTime.Parse(dt.Rows[n]["AffirmTime"].ToString());
                    }
                    if (dt.Rows[n]["IsNurseDo"] != null && dt.Rows[n]["IsNurseDo"].ToString() != "")
                    {
                        model.IsNurseDo = int.Parse(dt.Rows[n]["IsNurseDo"].ToString());
                    }
                    if (dt.Rows[n]["NurseSender"] != null && dt.Rows[n]["NurseSender"].ToString() != "")
                    {
                        model.NurseSender = dt.Rows[n]["NurseSender"].ToString();
                    }
                    if (dt.Rows[n]["NurseSendTime"] != null && dt.Rows[n]["NurseSendTime"].ToString() != "")
                    {
                        model.NurseSendTime = DateTime.Parse(dt.Rows[n]["NurseSendTime"].ToString());
                    }
                    if (dt.Rows[n]["NurseSendCarrier"] != null && dt.Rows[n]["NurseSendCarrier"].ToString() != "")
                    {
                        model.NurseSendCarrier = dt.Rows[n]["NurseSendCarrier"].ToString();
                    }
                    if (dt.Rows[n]["CollectCount"] != null && dt.Rows[n]["CollectCount"].ToString() != "")
                    {
                        model.CollectCount = int.Parse(dt.Rows[n]["CollectCount"].ToString());
                    }
                    if (dt.Rows[n]["ForeignSendFlag"] != null && dt.Rows[n]["ForeignSendFlag"].ToString() != "")
                    {
                        model.ForeignSendFlag = int.Parse(dt.Rows[n]["ForeignSendFlag"].ToString());
                    }
                    if (dt.Rows[n]["HisAffirm"] != null && dt.Rows[n]["HisAffirm"].ToString() != "")
                    {
                        model.HisAffirm = int.Parse(dt.Rows[n]["HisAffirm"].ToString());
                    }
                    if (dt.Rows[n]["PatPhoto"] != null && dt.Rows[n]["PatPhoto"].ToString() != "")
                    {
                        model.PatPhoto = (byte[])dt.Rows[n]["PatPhoto"];
                    }
                    if (dt.Rows[n]["ChargeOrderNo"] != null && dt.Rows[n]["ChargeOrderNo"].ToString() != "")
                    {
                        model.ChargeOrderNo = dt.Rows[n]["ChargeOrderNo"].ToString();
                    }
                    if (dt.Rows[n]["ReportFlag"] != null && dt.Rows[n]["ReportFlag"].ToString() != "")
                    {
                        model.ReportFlag = int.Parse(dt.Rows[n]["ReportFlag"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return dal.GetAllList();
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>


        #endregion  Method

        #region IBLLBase<NRequestForm> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public DataSet GetList(Model.NRequestForm model)
        {
            return dal.GetList(model);
        }

        public List<Model.NRequestForm> GetModelList(Model.NRequestForm model)
        {
            DataSet ds = dal.GetList(model);
            return DataTableToList(ds.Tables[0]);
        }

        #endregion

        #region IBNRequestForm 成员


        public string GetNCode(int p)
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
            //Random rand = new Random();
            //int intRan = rand.Next(10);
            //return DateTime.Now.ToString("yyMMddHHmmssmmm") + intRan.ToString().PadLeft(2, '0');
            //return DateTime.Now.ToString("ddHHmmssmm") + intRan.ToString().PadLeft(2, '0');
        }

        public DataSet GetListByBarCodeNo(string BarCodeNo)
        {
            return dal.GetListByBarCodeNo(BarCodeNo);
        }

        public int Delete(long NRequestFormNo)
        {
            DBarCodeForm.DeleteList_ByNRequestFormNo(NRequestFormNo);
            DNRequestItem.DeleteList_ByNRequestFormNo(NRequestFormNo);
            return dal.Delete(NRequestFormNo);
        }

        public DataTable GetAllData(Model.NRequestForm model, int StartPage, int PageSize, out int intPageCount, out int iCount)
        {
            iCount = dal.GetTotalCount(model);
            intPageCount = int.Parse((iCount / PageSize).ToString()) + 1;
            return dal.GetListByPage(model, StartPage, PageSize).Tables[0];
        }
        public DataTable GetNRequstFormList(Model.NRequestForm model, int StartPage, int PageSize, out int intPageCount, out int iCount)
        {
            iCount = dal.GetNRequstFormListTotalCount(model);
            intPageCount = int.Parse((iCount / PageSize).ToString()) + 1;
            return dal.GetNRequstFormListByPage(model, StartPage, PageSize).Tables[0];
        }
        public DataTable GetNRequstFormList2(Model.NRequestForm model, int StartPage, int PageSize, out int intPageCount, out int iCount)
        {
            iCount = dal.GetNRequstFormListTotalCount2(model);
            intPageCount = int.Parse((iCount / PageSize).ToString()) + 1;
            return dal.GetNRequstFormListByPage2(model, StartPage, PageSize).Tables[0];
        }
        public DataTable GetAll(Model.NRequestForm Model)
        {
            return dal.GetListBy(Model).Tables[0];
        }

        public bool CheckNReportFormCenter(DataSet dsForm, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                switch (str)
                {
                    case "SAMPLETYPENO":
                        if (dsForm.Tables[0].Columns.Contains("SampleTypeNo"))
                        {
                            for (int i = 0; i < dsForm.Tables[0].Rows.Count; i++)
                            {
                                if (dsForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                                {
                                    stringList.Add(dsForm.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                                }
                            }
                            if (stringList.Count > 0)
                            {
                                result = idstc.CheckIncludeCenterCode(stringList, DestiOrgID);
                                if (!result)
                                {
                                    for (int j = 0; j < stringList.Count; j++)
                                    {
                                        SamplleTypeControl.SampleTypeNo = Convert.ToInt32(stringList[j].Trim());
                                        SamplleTypeControl.LabCode = DestiOrgID;
                                        int count = idstc.GetTotalCount(SamplleTypeControl);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的SampleTypeNo={0}的编号未和实验室={1}的对照", SamplleTypeControl.SampleTypeNo, DestiOrgID);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("实验室={0}内样本类型为空，无法进行对照", DestiOrgID);
                                return false;
                            }
                        }
                        break;
                    case "ITEMNO":
                        if (dsForm.Tables[0].Columns.Contains("ParItemNo"))
                        {
                            for (int count = 0; count < dsForm.Tables[0].Rows.Count; count++)
                            {
                                if (dsForm.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsForm.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                                {
                                    l.Add(dsForm.Tables[0].Rows[count]["ParItemNo"].ToString());
                                }
                            }
                            if (l.Count > 0)
                            {
                                result = idtic.CheckIncludeCenterCode(l, DestiOrgID);
                                if (!result)
                                {
                                    for (int n = 0; n < l.Count; n++)
                                    {
                                        TestItemControl.ItemNo = l[n].Trim();
                                        TestItemControl.ControlLabNo = DestiOrgID;
                                        int count = idtic.GetTotalCount(TestItemControl);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的ParItemNo={0}的编号和实验室={1}的未对照", TestItemControl.ItemNo, DestiOrgID);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("CheckNReportFormCenter实验室={0}内项目编码为空，无法进行对照", DestiOrgID);
                                return false;
                            }
                        }
                        break;
                    case "GenderNo":
                        if (dsForm.Tables[0].Columns.Contains("GenderNo"))
                        {
                            for (int count = 0; count < dsForm.Tables[0].Rows.Count; count++)
                            {
                                if (dsForm.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsForm.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                                {
                                    ListStr.Add(dsForm.Tables[0].Rows[count]["GenderNo"].ToString());
                                }
                            }
                            if (ListStr.Count > 0)
                            {
                                result = idgtc.CheckIncludeCenterCode(ListStr, DestiOrgID);
                                if (!result)
                                {
                                    for (int n = 0; n < ListStr.Count; n++)
                                    {
                                        GenderType.GenderNo = Convert.ToInt32(ListStr[n].Trim());
                                        GenderType.LabCode = DestiOrgID;
                                        int count = idgtc.GetTotalCount(GenderType);
                                        if (count <= 0)
                                        {
                                            ReturnDescription += String.Format("中心端的GenderNo={0}的编号和实验室={1}的未对照", GenderType.GenderNo, DestiOrgID);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("实验室={0}内性别编号为空，无法进行对照", DestiOrgID);
                                return false;
                            }
                        }
                        break;
                }
            }
            return true;
        }

        public bool CheckNReportFormLab(DataSet dsForm, string DestiOrgID, out string ReturnDescription)
        {
            List<string> stringList = new List<string>();
            ReturnDescription = "";
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            bool result = false;
            if (dsForm.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                for (int i = 0; i < dsForm.Tables[0].Rows.Count; i++)
                {
                    if (dsForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                    {
                        stringList.Add(dsForm.Tables[0].Rows[i]["SampleTypeNo"].ToString());
                    }
                }
                if (stringList.Count > 0)
                {
                    result = idstc.CheckIncludeLabCode(stringList, DestiOrgID);
                    if (!result)
                    {
                        for (int j = 0; j < stringList.Count; j++)
                        {
                            SamplleTypeControl.SampleTypeControlNo = stringList[j].Trim();
                            SamplleTypeControl.LabCode = DestiOrgID;
                            int count = idstc.GetTotalCount(SamplleTypeControl);
                            if (count <= 0)
                            {
                                ReturnDescription = String.Format("实验室={1}内SampleTypeNo={0}的编号未和中心端的对照", SamplleTypeControl.SampleTypeControlNo, DestiOrgID);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室={0}内样本类型为空，无法进行对照",DestiOrgID);
                    return false;
                }
            }
            if (dsForm.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < dsForm.Tables[0].Rows.Count; count++)
                {
                    if (dsForm.Tables[0].Rows[count]["ParItemNo"].ToString() != null && dsForm.Tables[0].Rows[count]["ParItemNo"].ToString() != "")
                    {
                        l.Add(dsForm.Tables[0].Rows[count]["ParItemNo"].ToString());
                    }
                }
                if (l.Count > 0)
                {
                    result = idtic.CheckIncludeLabCode(l, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < l.Count; n++)
                        {
                            TestItemControl.ControlItemNo = l[n].Trim();
                            TestItemControl.ControlLabNo = DestiOrgID;
                            int count = idtic.GetTotalCount(TestItemControl);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室={1}内ParItemNo={0}的编号和中心的未对照", TestItemControl.ControlItemNo, DestiOrgID);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription = String.Format("CheckNReportFormLab实验室={0}内项目编码为空，无法进行对照", DestiOrgID);
                    return false;
                }
            }
            if (dsForm.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < dsForm.Tables[0].Rows.Count; count++)
                {
                    if (dsForm.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsForm.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                    {
                        ListStr.Add(dsForm.Tables[0].Rows[count]["GenderNo"].ToString());
                    }
                }
                if (ListStr.Count > 0)
                {
                    result = idgtc.CheckIncludeLabCode(ListStr, DestiOrgID);
                    if (!result)
                    {
                        for (int n = 0; n < ListStr.Count; n++)
                        {
                            GenderType.GenderControlNo = ListStr[n].Trim();
                            GenderType.LabCode = DestiOrgID;
                            int count = idgtc.GetTotalCount(GenderType);
                            if (count <= 0)
                            {
                                ReturnDescription += String.Format("实验室={1}内GenderNo={0}的编号和中心的未对照", GenderType.GenderControlNo, DestiOrgID);
                                ZhiFang.Common.Log.Log.Info(ReturnDescription);
                            }
                        }
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += String.Format("实验室={0}内性别编号为空，无法进行对照", DestiOrgID);
                    return false;
                }
            }
            return true;
        }
        #endregion

        public DataSet GetListByModel(Model.NRequestForm NRequestForm, Model.BarCodeForm BarCodeForm)
        {
            return dal.GetListByModel(NRequestForm, BarCodeForm);
        }

        public DataSet GetListBy(Model.NRequestForm model)
        {
            return dal.GetListBy(model);
        }

        public int UpdateByList(List<string> listStrColumnNf, List<string> listStrDataNf)
        {
            throw new NotImplementedException();
        }

        public int AddByList(List<string> listStrColumnNf, List<string> listStrDataNf)
        {
            return dal.AddByList(listStrColumnNf, listStrDataNf);
        }

        public Model.NRequestForm GetModelBySerialNo(string SerialNo)
        {
            return dal.GetModelBySerialNo(SerialNo);
        }

        public string GetBarCodeByNRequestFormNo(string p)
        {
            string barcode = "";
            DataTable dt = dal.GetBarCodeByNRequestFormNo(p);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barcode += dt.Rows[i]["BarCode"].ToString().Trim() + ",";
                }
                return barcode.Substring(0, barcode.Length - 1);
            }
            return "";
        }

        public string GetBarCodeByNRequestFormNo(string nrequestformno, string barCode, out string barcodeformno, out string colorname, out string itemname, out string itemno, out string samplytypename)
        {
            string barcode = "";
            barcodeformno = "";
            colorname = "";
            itemname = "";
            itemno = "";
            samplytypename = "";
            DataTable dt = dal.GetBarCodeByNRequestFormNo(nrequestformno, barCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barcode += dt.Rows[i]["BarCode"].ToString().Trim() + ",";
                    barcodeformno += dt.Rows[i]["BarCodeFormNo"].ToString().Trim() + ",";
                    colorname += dt.Rows[i]["color"].ToString().Trim() + ",";
                    itemname += dt.Rows[i]["ItemName"].ToString().Trim() + ";";
                    itemno += dt.Rows[i]["ItemNo"].ToString().Trim() + ";";
                    samplytypename += dt.Rows[i]["SampleTypeName"].ToString().Trim() + ";";
                }
                barcodeformno = barcodeformno.Substring(0, barcodeformno.Length - 1);
                colorname = colorname.Substring(0, colorname.Length - 1);
                itemname = itemname.Substring(0, itemname.Length - 1);
                itemno = itemno.Substring(0, itemno.Length - 1);
                samplytypename = samplytypename.Substring(0, samplytypename.Length - 1);
                return barcode.Substring(0, barcode.Length - 1);
            }
            return "";
        }
        public string GetBarCodeByNRequestFormNo(string nrequestformno, out string barcodeformno, out string colorname, out string itemname, out string itemno, out string samplytypename)
        {
            string barcode = "";
            barcodeformno = "";
            colorname = "";
            itemname = "";
            itemno = "";
            samplytypename = "";
            DataTable dt = dal.GetBarCodeByNRequestFormNo(nrequestformno);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barcode += dt.Rows[i]["BarCode"].ToString().Trim() + ",";
                    barcodeformno += dt.Rows[i]["BarCodeFormNo"].ToString().Trim() + ",";
                    colorname += dt.Rows[i]["color"].ToString().Trim() + ",";

                    if (dt.Rows[i]["ItemName"] != null )
                    {
                        string[] tmpitemnamea = dt.Rows[i]["ItemName"].ToString().Trim().Split(',');
                        foreach (string itn in tmpitemnamea)
                        {
                            if (!itemname.Contains(itn.Trim()))
                            {
                                itemname += itn.Trim() + ";";
                            }
                        }
                    }
                    if (dt.Rows[i]["ItemNo"] != null )
                    {
                        string[] tmpitemnoa = dt.Rows[i]["ItemNo"].ToString().Trim().Split(',');
                        foreach (string itn in tmpitemnoa)
                        {
                            if (!itemno.Contains(itn.Trim()))
                            {
                                itemno += itn.Trim() + ";";
                            }
                        }
                    }
                }
                barcodeformno = barcodeformno.Substring(0, barcodeformno.Length - 1);
                colorname = colorname.Substring(0, colorname.Length - 1);
                itemname = (itemname.Length>0)?itemname.Substring(0, itemname.Length - 1):"";
                itemno = (itemno.Length>0)?itemno.Substring(0, itemno.Length - 1):"";
                return barcode.Substring(0, barcode.Length - 1);
            }
            return "";
        }
        public List<Model.UiModel.SampleTypeBarCodeInfo> GetBarCodeAndCNameByNReuqestFormNo(DataTable NrequestFformDs)
        {
            List<Model.UiModel.SampleTypeBarCodeInfo> SampleTypeBarCodeInfoList = new List<Model.UiModel.SampleTypeBarCodeInfo>();
            Model.UiModel.SampleTypeBarCodeInfo SampleTypeBarCodeInfo = null;
            foreach (DataRow dr in NrequestFformDs.Rows)
            {

                DataTable dt = dal.GetBarCodeAndCNameByNRequestFormNo(dr["NRequestFormNo"].ToString().Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //过滤其它订单使用的条码
                        if (!DBarCodeForm.OtherOrderUser(dt.Rows[i]["BarCode"].ToString().Trim()))
                        {
                            SampleTypeBarCodeInfo = new Model.UiModel.SampleTypeBarCodeInfo();

                            SampleTypeBarCodeInfo.CName = dt.Rows[i]["CName"].ToString().Trim();
                            SampleTypeBarCodeInfo.BarCode = dt.Rows[i]["BarCode"].ToString().Trim();
                            SampleTypeBarCodeInfo.BarCodeFormNo = dt.Rows[i]["BarCodeFormNo"].ToString().Trim();
                            if (dt.Rows[i]["CollectDate"].ToString() != "")
                                SampleTypeBarCodeInfo.CollectDate = DateTime.Parse(dt.Rows[i]["CollectDate"].ToString().Trim());
                            if (dt.Rows[i]["ReceiveDate"].ToString() != "")
                                SampleTypeBarCodeInfo.ReceiveDate = DateTime.Parse(dt.Rows[i]["ReceiveDate"].ToString().Trim());

                            if (dt.Rows[i]["OperDate"].ToString() != "")
                                SampleTypeBarCodeInfo.OperDate = DateTime.Parse(dt.Rows[i]["OperDate"].ToString().Trim());
                            SampleTypeBarCodeInfoList.Add(SampleTypeBarCodeInfo);
                        }
                    }

                }
            }
            return SampleTypeBarCodeInfoList;
        }
        public bool CheckNReportFormStatus(long NRequestFromNo)
        {
            if (dal.CheckNReportFormWeblisFlag(NRequestFromNo) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool AddNrequest(Model.NRequestForm nrf_m, List<Model.BarCodeForm> bcf_List)
        {
            bool result = false;
            //IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            //对CombiItemName，barcode赋值
            foreach (Model.BarCodeForm barCodeform in bcf_List)
            {
                nrf_m.BarCode += barCodeform.BarCode + ",";
                nrf_m.CombiItemName = barCodeform.ItemName;
            }
            if (this.Add(nrf_m) > 0)
            {
                result = true;
            }
            return result;

        }

        public bool UpdateNrequest(Model.NRequestForm nrf_m, List<Model.BarCodeForm> bcf_List)
        {
            bool result = false;
            //IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            //对CombiItemName，barcode赋值
            foreach (Model.BarCodeForm barCodeform in bcf_List)
            {
                nrf_m.BarCode += barCodeform.BarCode + ",";
                nrf_m.CombiItemName += barCodeform.ItemName + ",";
            }

            if (this.Update(nrf_m) > 0)
            {
                result = true;
            }

            return result;
        }

        public bool UpdatePrintTimesByNrequestNo(long nrequestFormNo)
        {
            bool result = false;
            //IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            Model.NRequestForm model = this.GetModel(nrequestFormNo); ;
            if (model != null)
            {
                model.PrintTimes = model.PrintTimes + 1;
                if (this.Update(model) > 0)
                {
                    result = true;
                }
            }
            else
            {
                model.PrintTimes = 1;
                model.NRequestFormNo = nrequestFormNo;
                if (this.Add(model) > 0)
                {
                    result = true;
                }
            }

            return result;
        }



        #region 个人检验情况统计
        public DataSet GetStaticPersonTestItemPriceList(int rows, int page, Model.StaticPersonTestItemPrice model)
        {
            return dal.GetStaticPersonTestItemPriceList(rows, page, model);
        }

        public DataSet GetStaticPersonTestItemPriceList(Model.StaticPersonTestItemPrice model)
        {
            return dal.GetStaticPersonTestItemPriceList(model);
        }

        List<Model.StaticPersonTestItemPrice> IBNRequestForm.GetStaticPersonDataTableToList(DataTable dt)
        {

            List<ZhiFang.Model.StaticPersonTestItemPrice> modelList = new List<ZhiFang.Model.StaticPersonTestItemPrice>();
            int rows = dt.Rows.Count;
            if (rows > 0)
            {
                ZhiFang.Model.StaticPersonTestItemPrice model;
                for (int n = 0; n < rows; n++)
                {
                    model = new ZhiFang.Model.StaticPersonTestItemPrice();
                    if (dt.Rows[n]["ClientName"].ToString() != "")
                    {
                        model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    }
                    if (dt.Rows[n]["Age"].ToString() != "")
                    {
                        model.Age = dt.Rows[n]["Age"].ToString();
                    }
                    if (dt.Rows[n]["GenderName"].ToString() != "")
                    {
                        model.GenderName = dt.Rows[n]["GenderName"].ToString();
                    }
                    if (dt.Rows[n]["OperDate"].ToString() != "")
                    {
                        model.OperDate = Convert.ToDateTime(dt.Rows[n]["OperDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (dt.Rows[n]["BCName"].ToString() != "")
                    {
                        model.BCName = dt.Rows[n]["BCName"].ToString();
                    }
                    if (dt.Rows[n]["PatNo"].ToString() != "")
                    {
                        model.PatNo = dt.Rows[n]["PatNo"].ToString();
                    }
                    if (dt.Rows[n]["BarCode"].ToString() != "")
                    {
                        model.BarCode = dt.Rows[n]["BarCode"].ToString();
                    }
                    if (dt.Rows[n]["ParItemNo"].ToString() != "")
                    {
                        model.ParItemNo = dt.Rows[n]["ParItemNo"].ToString();
                    }
                    if (dt.Rows[n]["DCName"].ToString() != "")
                    {
                        model.DCName = dt.Rows[n]["DCName"].ToString();
                    }
                    if (dt.Rows[n]["Price"].ToString() != "")
                    {
                        model.Price = Convert.ToDecimal(dt.Rows[n]["Price"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion

        #region
        public DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            return dal.GetStaticRecOrgSamplePrice(model, rows, page);
        }

        public DataSet GetOpertorWorkCount(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            return dal.GetOpertorWorkCount(model, rows, page);
        }

        public DataSet GetStaticRecOrgSamplePrice(Model.StaticRecOrgSamplePrice model)
        {
            return dal.GetStaticRecOrgSamplePrice(model);
        }

        // 工作量条码条码查询。liwk 2015-10-29
        public DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model, int rows, int page)
        {
            return dal.GetBarcodePrice(model, rows, page);
        }
        public DataSet GetBarcodePrice(Model.StaticRecOrgSamplePrice model)
        {
            return dal.GetBarcodePrice(model);
        }


        List<Model.StaticRecOrgSamplePrice> IBNRequestForm.BarcodeDataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.StaticRecOrgSamplePrice> modelList = new List<ZhiFang.Model.StaticRecOrgSamplePrice>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.StaticRecOrgSamplePrice model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.StaticRecOrgSamplePrice();
                    if (dt.Rows[n]["ClientName"].ToString() != "")
                    {
                        model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    }
                    if (dt.Rows[n]["Barcode"].ToString() != "")
                    {
                        model.Barcode = dt.Rows[n]["Barcode"].ToString();
                    }
                    if (dt.Rows[n]["OperDate"].ToString() != "")
                    {
                        model.OperDate = Convert.ToDateTime(dt.Rows[n]["OperDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (dt.Rows[n]["Price"].ToString() != "")
                    {
                        model.Price = dt.Rows[n]["Price"].ToString();
                    }



                    modelList.Add(model);
                }
            }
            return modelList;
        }




        List<Model.StaticRecOrgSamplePrice> IBNRequestForm.DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.StaticRecOrgSamplePrice> modelList = new List<ZhiFang.Model.StaticRecOrgSamplePrice>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.StaticRecOrgSamplePrice model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.StaticRecOrgSamplePrice();
                    if (dt.Rows[n]["ClientName"].ToString() != "")
                    {
                        model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    }
                    if (dt.Rows[n]["CName"].ToString() != "")
                    {
                        model.CName = dt.Rows[n]["CName"].ToString();
                    }
                    //if (dt.Rows[n]["OperDate"].ToString() != "")
                    //{
                    //    model.OperDate = Convert.ToDateTime(dt.Rows[n]["OperDate"].ToString()).ToString("yyyy-MM-dd");
                    //}
                    //if (dt.Rows[n]["OperDateBegin"].ToString() != "")
                    //{
                    //    model.OperDateBegin = dt.Rows[n]["OperDateBegin"].ToString();
                    //}
                    //if (dt.Rows[n]["OperDateEnd"].ToString() != "")
                    //{
                    //    model.OperDateEnd = dt.Rows[n]["OperDateEnd"].ToString();
                    //}
                    if (dt.Rows[n]["ParItemNo"].ToString() != "")
                    {
                        model.ParItemNo = dt.Rows[n]["ParItemNo"].ToString();
                    }
                    if (dt.Rows[n]["SampleNum"].ToString() != "")
                    {
                        model.SampleNum = dt.Rows[n]["SampleNum"].ToString();
                    }
                    if (dt.Rows[n]["Price"].ToString() != "")
                    {
                        model.Price = dt.Rows[n]["Price"].ToString();
                    }
                    if (dt.Rows[n]["ItemTotalPrice"].ToString() != "")
                    {
                        model.ItemTotalPrice = dt.Rows[n]["ItemTotalPrice"].ToString();
                    }
                    if (dt.Rows[n]["ItemTotalPrice"].ToString() != "")
                    {
                        model.ItemTotalPrice = dt.Rows[n]["ItemTotalPrice"].ToString();
                    }


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        List<Model.StaticRecOrgSamplePrice> IBNRequestForm.OperatorWorkDataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.StaticRecOrgSamplePrice> modelList = new List<ZhiFang.Model.StaticRecOrgSamplePrice>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.StaticRecOrgSamplePrice model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.StaticRecOrgSamplePrice();
                    if (dt.Columns.Contains("ClientNo") && dt.Rows[n]["ClientNo"].ToString() != "")
                    {
                        model.ClientNo = dt.Rows[n]["ClientNo"].ToString();
                    }
                    if (dt.Columns.Contains("ClientName") && dt.Rows[n]["ClientName"].ToString() != "")
                    {
                        model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    }
                    if (dt.Columns.Contains("OperDate") && dt.Rows[n]["OperDate"].ToString() != "")
                    {
                        model.OperDate = dt.Rows[n]["OperDate"].ToString();
                    }

                    if (dt.Columns.Contains("Operator") && dt.Rows[n]["Operator"].ToString() != "")
                    {
                        model.Operator = dt.Rows[n]["Operator"].ToString();
                    }
                    if (dt.Columns.Contains("BarcodeNum") && dt.Rows[n]["BarcodeNum"].ToString() != "")
                    {
                        model.BarcodeNum = int.Parse(dt.Rows[n]["BarcodeNum"].ToString());
                    }
                    if (dt.Columns.Contains("SumMoney") && dt.Rows[n]["SumMoney"].ToString() != "")
                    {
                        model.SumMoney = float.Parse(dt.Rows[n]["SumMoney"].ToString());
                    }
                   
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion



        public int Add(string strSql)
        {
            return dal.Add(strSql);
        }
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public DataSet GetRefuseList(string strSql)
        {
            return dal.GetRefuseList(strSql);
        }

        public DataTable GetNRequstFormListByDetailsAndPage(Model.NRequestForm model, int StartPage, int PageSize, out int intPageCount, out int iCount)
        {
            iCount = dal.GetNRequstFormListByDetailsTotalCount(model);
            intPageCount = int.Parse((iCount / PageSize).ToString()) + 1;
            return dal.GetNRequstFormListByDetailsAndPage(model, StartPage, PageSize).Tables[0];
        }
    }
}

