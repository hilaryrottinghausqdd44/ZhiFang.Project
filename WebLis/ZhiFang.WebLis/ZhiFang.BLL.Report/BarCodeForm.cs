using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Report;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using ZhiFang.Model.UiModel;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
namespace ZhiFang.BLL.Report
{
    //BarCodeForm		
    public partial class BarCodeForm : IBBarCodeForm
    {
        IDAL.IDBarCodeForm dal = DALFactory.DalFactory<IDAL.IDBarCodeForm>.GetDalByClassName("BarCodeForm");
        private readonly IDTestItemControl idtic = DalFactory<IDTestItemControl>.GetDalByClassName("B_TestItemControl");
        private readonly IDSampleTypeControl idstc = DalFactory<IDSampleTypeControl>.GetDalByClassName("B_SampleTypeControl");
        private readonly IDGenderTypeControl idgtc = DalFactory<IDGenderTypeControl>.GetDalByClassName("B_GenderTypeControl");
        public BarCodeForm()
        {

        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long BarCodeFormNo)
        {
            return dal.Exists(BarCodeFormNo);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.BarCodeForm model)
        {
            return dal.Add(model);
        }

        public int Add_TaiHe(ZhiFang.Model.BarCodeForm model)
        {
            return dal.Add_TaiHe(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ZhiFang.Model.BarCodeForm model)
        {
            return dal.Update(model);
        }

        public int Update_TaiHe(ZhiFang.Model.BarCodeForm model)
        {
            return dal.Update_TaiHe(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(long BarCodeFormNo)
        {
            return dal.Delete(BarCodeFormNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.BarCodeForm GetModel(long BarCodeFormNo)
        {
            return dal.GetModel(BarCodeFormNo);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public ZhiFang.Model.BarCodeForm GetModelByCache(int BarCodeFormNo)
        {

            string CacheKey = "BarCodeFormModel-" + BarCodeFormNo;
            object objModel = ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(BarCodeFormNo);
                    if (objModel != null)
                    {
                        int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
                        ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (ZhiFang.Model.BarCodeForm)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.BarCodeForm> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.BarCodeForm> modelList = new List<ZhiFang.Model.BarCodeForm>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.BarCodeForm model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.BarCodeForm();
                    if (dt.Rows[n]["BarCodeFormNo"].ToString() != "")
                    {
                        model.BarCodeFormNo = long.Parse(dt.Rows[n]["BarCodeFormNo"].ToString());
                    }
                    model.Collecter = dt.Rows[n]["Collecter"].ToString();
                    if (dt.Rows[n]["CollecterID"].ToString() != "")
                    {
                        model.CollecterID = int.Parse(dt.Rows[n]["CollecterID"].ToString());
                    }
                    if (dt.Rows[n]["CollectDate"].ToString() != "")
                    {
                        model.CollectDate = DateTime.Parse(dt.Rows[n]["CollectDate"].ToString());
                    }
                    if (dt.Rows[n]["CollectTime"].ToString() != "")
                    {
                        model.CollectTime = DateTime.Parse(dt.Rows[n]["CollectTime"].ToString());
                    }
                    model.refuseUser = dt.Rows[n]["refuseUser"].ToString();
                    model.refuseopinion = dt.Rows[n]["refuseopinion"].ToString();
                    model.refusereason = dt.Rows[n]["refusereason"].ToString();
                    if (dt.Rows[n]["refuseTime"].ToString() != "")
                    {
                        model.refuseTime = DateTime.Parse(dt.Rows[n]["refuseTime"].ToString());
                    }
                    if (dt.Rows[n]["signflag"].ToString() != "")
                    {
                        model.signflag = int.Parse(dt.Rows[n]["signflag"].ToString());
                    }
                    model.incepter = dt.Rows[n]["incepter"].ToString();
                    model.BarCode = dt.Rows[n]["BarCode"].ToString();
                    if (dt.Rows[n]["inceptTime"].ToString() != "")
                    {
                        model.inceptTime = DateTime.Parse(dt.Rows[n]["inceptTime"].ToString());
                    }
                    if (dt.Rows[n]["inceptDate"].ToString() != "")
                    {
                        model.inceptDate = DateTime.Parse(dt.Rows[n]["inceptDate"].ToString());
                    }
                    model.ReceiveMan = dt.Rows[n]["ReceiveMan"].ToString();
                    if (dt.Rows[n]["ReceiveDate"].ToString() != "")
                    {
                        model.ReceiveDate = DateTime.Parse(dt.Rows[n]["ReceiveDate"].ToString());
                    }
                    if (dt.Rows[n]["ReceiveTime"].ToString() != "")
                    {
                        model.ReceiveTime = DateTime.Parse(dt.Rows[n]["ReceiveTime"].ToString());
                    }
                    model.PrintInfo = dt.Rows[n]["PrintInfo"].ToString();
                    if (dt.Rows[n]["PrintCount"].ToString() != "")
                    {
                        model.PrintCount = int.Parse(dt.Rows[n]["PrintCount"].ToString());
                    }
                    if (dt.Rows[n]["Dr2Flag"].ToString() != "")
                    {
                        model.Dr2Flag = int.Parse(dt.Rows[n]["Dr2Flag"].ToString());
                    }
                    if (dt.Rows[n]["FlagDateDelete"].ToString() != "")
                    {
                        model.FlagDateDelete = DateTime.Parse(dt.Rows[n]["FlagDateDelete"].ToString());
                    }
                    if (dt.Rows[n]["DispenseFlag"].ToString() != "")
                    {
                        model.DispenseFlag = int.Parse(dt.Rows[n]["DispenseFlag"].ToString());
                    }
                    if (dt.Rows[n]["SamplingGroupNo"].ToString() != "")
                    {
                        model.SamplingGroupNo = int.Parse(dt.Rows[n]["SamplingGroupNo"].ToString());
                    }
                    model.SerialScanTime = dt.Rows[n]["SerialScanTime"].ToString();
                    if (dt.Rows[n]["BarCodeSource"].ToString() != "")
                    {
                        model.BarCodeSource = int.Parse(dt.Rows[n]["BarCodeSource"].ToString());
                    }
                    if (dt.Rows[n]["DeleteFlag"].ToString() != "")
                    {
                        model.DeleteFlag = int.Parse(dt.Rows[n]["DeleteFlag"].ToString());
                    }
                    if (dt.Rows[n]["SendOffFlag"].ToString() != "")
                    {
                        model.SendOffFlag = int.Parse(dt.Rows[n]["SendOffFlag"].ToString());
                    }
                    model.SendOffMan = dt.Rows[n]["SendOffMan"].ToString();
                    model.EMSMan = dt.Rows[n]["EMSMan"].ToString();
                    if (dt.Rows[n]["SendOffDate"].ToString() != "")
                    {
                        model.SendOffDate = DateTime.Parse(dt.Rows[n]["SendOffDate"].ToString());
                    }
                    model.ReportSignMan = dt.Rows[n]["ReportSignMan"].ToString();
                    if (dt.Rows[n]["ReportSignDate"].ToString() != "")
                    {
                        model.ReportSignDate = DateTime.Parse(dt.Rows[n]["ReportSignDate"].ToString());
                    }
                    model.RefuseIncepter = dt.Rows[n]["RefuseIncepter"].ToString();
                    if (dt.Rows[n]["IsPrep"].ToString() != "")
                    {
                        model.IsPrep = int.Parse(dt.Rows[n]["IsPrep"].ToString());
                    }
                    model.RefuseIncepterMemo = dt.Rows[n]["RefuseIncepterMemo"].ToString();
                    if (dt.Rows[n]["ReportFlag"].ToString() != "")
                    {
                        model.ReportFlag = int.Parse(dt.Rows[n]["ReportFlag"].ToString());
                    }
                    model.SendOffMemo = dt.Rows[n]["SendOffMemo"].ToString();
                    if (dt.Rows[n]["SampleTypeNo"].ToString() != "")
                    {
                        model.SampleTypeNo = int.Parse(dt.Rows[n]["SampleTypeNo"].ToString());
                    }
                    model.SampleSendNo = dt.Rows[n]["SampleSendNo"].ToString();
                    if (dt.Rows[n]["WebLisFlag"].ToString() != "")
                    {
                        model.WebLisFlag = int.Parse(dt.Rows[n]["WebLisFlag"].ToString());
                    }
                    if (dt.Rows[n]["WebLisOpTime"].ToString() != "")
                    {
                        model.WebLisOpTime = DateTime.Parse(dt.Rows[n]["WebLisOpTime"].ToString());
                    }
                    model.WebLiser = dt.Rows[n]["WebLiser"].ToString();
                    model.WebLisDescript = dt.Rows[n]["WebLisDescript"].ToString();
                    model.WebLisOrgID = dt.Rows[n]["WebLisOrgID"].ToString();
                    if (dt.Rows[n]["isSpiltItem"].ToString() != "")
                    {
                        model.isSpiltItem = int.Parse(dt.Rows[n]["isSpiltItem"].ToString());
                    }
                    if (dt.Rows[n]["WebLisIsReply"].ToString() != "")
                    {
                        model.WebLisIsReply = int.Parse(dt.Rows[n]["WebLisIsReply"].ToString());
                    }
                    model.WebLisReplyDate = dt.Rows[n]["WebLisReplyDate"].ToString();
                    model.WebLisSourceOrgId = dt.Rows[n]["WebLisSourceOrgId"].ToString();
                    if (dt.Rows[n]["WebLisUploadTime"].ToString() != "")
                    {
                        model.WebLisUploadTime = DateTime.Parse(dt.Rows[n]["WebLisUploadTime"].ToString());
                    }
                    if (dt.Rows[n]["WebLisUploadStatus"].ToString() != "")
                    {
                        model.WebLisUploadStatus = int.Parse(dt.Rows[n]["WebLisUploadStatus"].ToString());
                    }
                    if (dt.Rows[n]["WebLisUploadTestStatus"].ToString() != "")
                    {
                        model.WebLisUploadTestStatus = int.Parse(dt.Rows[n]["WebLisUploadTestStatus"].ToString());
                    }
                    model.WebLisUploader = dt.Rows[n]["WebLisUploader"].ToString();
                    model.WebLisUploadDes = dt.Rows[n]["WebLisUploadDes"].ToString();
                    model.WebLisSourceOrgName = dt.Rows[n]["WebLisSourceOrgName"].ToString();
                    model.ClientNo = dt.Rows[n]["ClientNo"].ToString();
                    if (dt.Rows[n]["IsAffirm"].ToString() != "")
                    {
                        model.IsAffirm = int.Parse(dt.Rows[n]["IsAffirm"].ToString());
                    }
                    model.ClientName = dt.Rows[n]["ClientName"].ToString();
                    if (dt.Rows[n]["ReceiveFlag"].ToString() != "")
                    {
                        model.ReceiveFlag = int.Parse(dt.Rows[n]["ReceiveFlag"].ToString());
                    }
                    if (dt.Rows[n]["SampleCap"].ToString() != "")
                    {
                        model.SampleCap = decimal.Parse(dt.Rows[n]["SampleCap"].ToString());
                    }
                    model.ClientHost = dt.Rows[n]["ClientHost"].ToString();

                    if (dt.Columns.Contains("SampleTypeName"))
                    {
                        model.SampleTypeName = dt.Rows[n]["SampleTypeName"].ToString();
                    }
                    if (dt.Columns.Contains("ItemNo"))
                    {
                        model.ItemNo = dt.Rows[n]["ItemNo"].ToString();
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
        /// 根据实体获取DataSet
        /// </summary>
        public DataSet GetList(ZhiFang.Model.BarCodeForm model)
        {
            return dal.GetList(model);
        }
        public int GetTotalCount()
        {
            return dal.GetTotalCount();
        }
        public int GetTotalCount(ZhiFang.Model.BarCodeForm model)
        {
            return dal.GetTotalCount(model);
        }
        #endregion

        #region IBBarCodeForm 成员


        public string GetNewBarCodeFormNo(int clientno)
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString();
            #region
            //string a = DateTime.Now.Year.ToString().Substring(2,2);

            //#region Month
            //if (DateTime.Now.Month > 9)
            //{
            //    a += DateTime.Now.Month.ToString();
            //}
            //else
            //{
            //    a += "0" + DateTime.Now.Month.ToString();
            //}
            //#endregion
            //#region day
            //if (DateTime.Now.Day > 9)
            //{
            //    a += DateTime.Now.Day.ToString();
            //}
            //else
            //{
            //    a += "0" + DateTime.Now.Day.ToString();
            //}
            //#endregion
            //#region Hour
            //if (DateTime.Now.Hour > 9)
            //{
            //    a += DateTime.Now.Hour.ToString();
            //}
            //else
            //{
            //    a += "0" + DateTime.Now.Hour.ToString();
            //}
            //#endregion
            //#region Minute
            //if (DateTime.Now.Minute > 9)
            //{
            //    a += DateTime.Now.Minute.ToString();
            //}
            //else
            //{
            //    a += "0" + DateTime.Now.Minute.ToString();
            //}
            //#endregion
            //#region Second
            //if (DateTime.Now.Second > 9)
            //{
            //    a += DateTime.Now.Second.ToString();
            //}
            //else
            //{
            //    a += "0" + DateTime.Now.Second.ToString();
            //}
            //#endregion
            //#region Millisecond
            //if (DateTime.Now.Millisecond < 10)
            //{
            //    //a += "00" + DateTime.Now.Millisecond.ToString();
            //    a += "0";
            //}
            //else
            //{
            //    if (DateTime.Now.Millisecond < 100)
            //    {
            //        //a += "0" + DateTime.Now.Millisecond.ToString();
            //        a += "0";
            //    }
            //    else
            //    {
            //        //a += DateTime.Now.Millisecond.ToString();
            //        a += DateTime.Now.Millisecond.ToString().Substring(0,1);
            //    }
            //}
            //#endregion


            ////return DateTime.Now.GetHashCode().ToString() + clientno.ToString();
            //return a + clientno.ToString();
            #endregion
        }
        public bool DeleteList(string BarCodeFormNolist)
        {
            throw new NotImplementedException();
        }

        public string GetNewBarCode(string ClientNo)
        {
            return dal.GetNewBarCode(ClientNo);
        }

        public int UpdateByBarCode(Model.BarCodeForm barCodeForm)
        {
            return dal.UpdateByBarCode(barCodeForm);
        }
        public int UpdateWebLisFlagByBarCode(string WebLisFlag, string BarCode, string WebLisOrgID)
        {
            return dal.UpdateWebLisFlagByBarCode(WebLisFlag, BarCode, WebLisOrgID);
        }
        public int UpdatePrintFlag(Model.BarCodeForm model)
        {
            return dal.UpdatePrintFlag(model);
        }
        public bool CheckBarCodeCenter(DataSet dsBarCodeForm, string DestiOrgID, out string ReturnDescription)
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
                        if (dsBarCodeForm.Tables[0].Columns.Contains("SampleTypeNo"))
                        {
                            for (int i = 0; i < dsBarCodeForm.Tables[0].Rows.Count; i++)
                            {
                                if (dsBarCodeForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsBarCodeForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                                {
                                    stringList.Add(dsBarCodeForm.Tables[0].Rows[i]["SampleTypeNo"].ToString());
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
                                            ReturnDescription += String.Format("中心端的SampleTypeNo={0}的编号未和实验室的对照", SamplleTypeControl.SampleTypeNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("实验室内样本类型为空，无法进行对照");
                                return false;
                            }
                        }
                        break;
                    case "ITEMNO":
                        if (dsBarCodeForm.Tables[0].Columns.Contains("ITEMNO"))
                        {
                            for (int count = 0; count < dsBarCodeForm.Tables[0].Rows.Count; count++)
                            {
                                if (dsBarCodeForm.Tables[0].Rows[count]["ITEMNO"].ToString() != null && dsBarCodeForm.Tables[0].Rows[count]["ITEMNO"].ToString() != "")
                                {
                                   var tmp= dsBarCodeForm.Tables[0].Rows[count]["ITEMNO"].ToString().Split(',');
                                    foreach (var a in tmp)
                                    {
                                        if(a!=null && a.Trim()!="")
                                        l.Add(a);
                                    }
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
                                            ReturnDescription += String.Format("中心端的ITEMNO={0}的编号和实验室的未对照", TestItemControl.ItemNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            //else
                            //{
                            //    ReturnDescription += String.Format("CheckBarCodeCenter实验室内项目编码为空，无法进行对照");
                            //    return false;
                            //}
                        }
                        break;
                    case "GenderNo":
                        if (dsBarCodeForm.Tables[0].Columns.Contains("GenderNo"))
                        {
                            for (int count = 0; count < dsBarCodeForm.Tables[0].Rows.Count; count++)
                            {
                                if (dsBarCodeForm.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsBarCodeForm.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                                {
                                    ListStr.Add(dsBarCodeForm.Tables[0].Rows[count]["GenderNo"].ToString());
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
                                            ReturnDescription += String.Format("中心端的GenderNo={0}的编号和实验室的未对照", GenderType.GenderNo);
                                        }
                                    }
                                    return false;
                                }
                            }
                            else
                            {
                                ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                                return false;
                            }
                        }
                        break;

                }
            }

            return true;
        }

        public bool CheckBarCodeLab(DataSet dsBarCodeForm, string DestiOrgID, out string ReturnDescription)
        {
            ReturnDescription = "";
            try
            {
                List<string> stringList = new List<string>();
                List<string> l = new List<string>();
                List<string> ListStr = new List<string>();
                Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
                Model.TestItemControl TestItemControl = new Model.TestItemControl();
                Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
                bool result = false;
                if (ConfigHelper.GetConfigString("TransCodField") != "" && ConfigHelper.GetConfigString("TransCodField") != null)
                {
                    string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
                    ZhiFang.Common.Log.Log.Info("TransCodField:" + string.Join(";",strArray));
                    foreach (string str in strArray)
                    {
                        switch (str)
                        {
                            case "SAMPLETYPENO":
                                if (dsBarCodeForm.Tables[0].Columns.Contains("SampleTypeNo"))
                                {
                                    for (int i = 0; i < dsBarCodeForm.Tables[0].Rows.Count; i++)
                                    {
                                        if (dsBarCodeForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != null && dsBarCodeForm.Tables[0].Rows[i]["SampleTypeNo"].ToString() != "")
                                        {
                                            stringList.Add(dsBarCodeForm.Tables[0].Rows[i]["SampleTypeNo"].ToString());
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
                                                    ReturnDescription += String.Format("实验室内SampleTypeNo={0}的编号未和中心端的对照", SamplleTypeControl.SampleTypeControlNo);
                                                    ZhiFang.Common.Log.Log.Info(ReturnDescription);
                                                }
                                            }
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        ReturnDescription += String.Format("实验室内样本类型为空，无法进行对照");
                                        return false;
                                    }
                                }
                                break;
                            case "ITEMNO":
                                if (dsBarCodeForm.Tables[0].Columns.Contains("ITEMNO"))
                                {
                                    for (int count = 0; count < dsBarCodeForm.Tables[0].Rows.Count; count++)
                                    {
                                        if (dsBarCodeForm.Tables[0].Rows[count]["ITEMNO"].ToString() != null && dsBarCodeForm.Tables[0].Rows[count]["ITEMNO"].ToString() != "")
                                        {
                                            l.Add(dsBarCodeForm.Tables[0].Rows[count]["ITEMNO"].ToString());
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
                                                ZhiFang.Common.Log.Log.Info("@@@@@@@@@@@@ControlItemNo:"+ TestItemControl.ControlItemNo + ",ControlLabNo:"+ TestItemControl.ControlLabNo + ",count:"+ count);
                                                if (count <= 0)
                                                {
                                                    ReturnDescription += String.Format("实验室内ITEMNO={0}的编号和中心的未对照", TestItemControl.ControlItemNo);
                                                    ZhiFang.Common.Log.Log.Info(ReturnDescription);
                                                }
                                            }
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        ReturnDescription += String.Format("CheckBarCodeLab实验室内项目编码为空，无法进行对照");
                                        return false;
                                    }
                                }
                                break;
                            case "GenderNo":
                                if (dsBarCodeForm.Tables[0].Columns.Contains("GenderNo"))
                                {
                                    for (int count = 0; count < dsBarCodeForm.Tables[0].Rows.Count; count++)
                                    {
                                        if (dsBarCodeForm.Tables[0].Rows[count]["GenderNo"].ToString() != null && dsBarCodeForm.Tables[0].Rows[count]["GenderNo"].ToString() != "")
                                        {
                                            ListStr.Add(dsBarCodeForm.Tables[0].Rows[count]["GenderNo"].ToString());
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
                                                    ReturnDescription += String.Format("实验室内GenderNo={0}的编号和中心的未对照", GenderType.GenderControlNo);
                                                    ZhiFang.Common.Log.Log.Info(ReturnDescription);
                                                }
                                            }
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        ReturnDescription += String.Format("实验室内性别编号为空，无法进行对照");
                                        return false;
                                    }
                                }

                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("CheckBarCodeLab错误信息：" + ex.Message.ToString());
                return false;
            }
            return true;
        }
        #endregion

        #region IBLLBase<BarCodeForm> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        public List<Model.BarCodeForm> GetModelList(Model.BarCodeForm t)
        {
            DataSet ds = this.GetList(t);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return this.DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取送检单位
        /// </summary>
        /// <returns></returns>
        public DataSet GetWeblisOrgName(ZhiFang.Model.BarCodeForm model)
        {
            return dal.GetWeblisOrgName(model);
        }


        public DataSet GetAllList(Model.BarCodeForm model)
        {
            return dal.GetAllList(model);
        }


        public DataSet GetBarCodeView(string BarCode)
        {
            return dal.GetBarCodeView(BarCode);
        }




        public int UpdateByList(List<string> lisStrColumn, List<string> lisStrData)
        {
            return dal.UpdateByList(lisStrColumn, lisStrData);
        }

        public int AddByList(List<string> lisStrColumn, List<string> lisStrData)
        {
            return dal.AddByList(lisStrColumn, lisStrData);
        }


        public bool DeleteBarCodeByNRequestFormNo(string NRequestFormNo)
        {
            DataSet ds = dal.GetListByNRequestFormNo(long.Parse(NRequestFormNo));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Select(" weblisflag=1").Length > 0)
                {
                    return false;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //if (Delete(long.Parse(ds.Tables[0].Rows[i]["BarCode"].ToString())) <= 0)
                    //{
                    //    return false;
                    //}
                    Delete(long.Parse(ds.Tables[0].Rows[i]["BarCodeFormNo"].ToString()));
                }
                return true;
            }
            return true;
        }

        #endregion


        //public bool AddBarCodeForm(List<Model.BarCodeForm> bcf_List)
        //{
        //    bool result = false;
        //    ZhiFang.Common.Log.Log.Debug("bcf_List.Count:" + bcf_List.Count);
        //    foreach (Model.BarCodeForm bcf in bcf_List)
        //    {
        //        int i = this.Add(bcf);
        //        if (i > 0)
        //            result = true;
        //        else
        //            result = false;
        //    }
        //    return result;
        //}

        /// <summary>
        /// 转换成UiBarCode给前台
        /// </summary>
        /// <param name="bcf_List"></param>
        /// <param name="nrf_m"></param>
        /// <returns></returns>
        //public List<UiBarCode> ConvertBarCodeFormToUiBarCode(List<Model.BarCodeForm> bcf_List, Model.NRequestForm nrf_m)
        //{

        //}

        //public bool UpdateBarCodeForm(List<Model.BarCodeForm> bcf_List)
        //{
        //    bool result = false;
        //    foreach (Model.BarCodeForm bcf in bcf_List)
        //    {
        //        int i;
        //        if (this.Exists((long)bcf.BarCodeFormNo))
        //        {
        //            i = this.Update(bcf);
        //        }
        //        else
        //            i = this.Add(bcf);

        //        if (i > 0)
        //            result = true;
        //        else
        //            result = false;
        //    }
        //    return result;
        //}

        //public List<UiBarCode> GetUiBarCodeListByNrequestFormNo(long nrequestFormNo)
        //{

        //}
        /// <summary>
        /// 根据申请单号,获取批量打印的条码信息
        /// </summary>
        /// <param name="nrequestFormNo"></param>
        /// <returns></returns>
        //public List<UiBarCode> GetBatchUiBarCodeListByNrequestFormNo(long nrequestFormNo)
        //{

        //}

        //int IBBarCodeForm.Add(Model.BarCodeForm model)
        //{
        //    throw new NotImplementedException();
        //}

        //int IBBarCodeForm.Delete(long BarCodeFormNo)
        //{
        //    throw new NotImplementedException();
        //}

        //DataSet IBBarCodeForm.GetWeblisOrgName(Model.BarCodeForm model)
        //{
        //    throw new NotImplementedException();
        //}

        //DataSet IBBarCodeForm.GetAllList(Model.BarCodeForm model)
        //{
        //    throw new NotImplementedException();
        //}

        //bool IBBarCodeForm.DeleteList(string BarCodeFormNolist)
        //{
        //    throw new NotImplementedException();
        //}

        //Model.BarCodeForm IBBarCodeForm.GetModel(long BarCodeFormNo)
        //{
        //    throw new NotImplementedException();
        //}

        //List<Model.BarCodeForm> IBBarCodeForm.DataTableToList(DataTable dt)
        //{
        //    throw new NotImplementedException();
        //}

        //DataSet IBBarCodeForm.GetAllList()
        //{
        //    throw new NotImplementedException();
        //}

        //string IBBarCodeForm.GetNewBarCode(string ClientNo)
        //{
        //    throw new NotImplementedException();
        //}

        //int IBBarCodeForm.GetTotalCount()
        //{
        //    throw new NotImplementedException();
        //}

        //int IBBarCodeForm.GetTotalCount(Model.BarCodeForm model)
        //{
        //    throw new NotImplementedException();
        //}

        //int IBBarCodeForm.UpdateByBarCode(Model.BarCodeForm barCodeForm)
        //{
        //    throw new NotImplementedException();
        //}

        //int IBBarCodeForm.UpdatePrintFlag(Model.BarCodeForm model)
        //{
        //    throw new NotImplementedException();
        //}

        //string IBBarCodeForm.GetNewBarCodeFormNo(int client)
        //{
        //    throw new NotImplementedException();
        //}

        //DataSet IBBarCodeForm.GetBarCodeView(string BarCode)
        //{
        //    throw new NotImplementedException();
        //}

        //bool IBBarCodeForm.CheckBarCodeCenter(DataSet dsBarCodeForm, string DestiOrgID, out string ReturnDescription)
        //{
        //    throw new NotImplementedException();
        //}

        //bool IBBarCodeForm.CheckBarCodeLab(DataSet dsBarCodeForm, string DestiOrgID, out string ReturnDescription)
        //{
        //    throw new NotImplementedException();
        //}

        //int IBBarCodeForm.UpdateByList(List<string> lisStrColumn, List<string> lisStrData)
        //{
        //    throw new NotImplementedException();
        //}

        //int IBBarCodeForm.AddByList(List<string> lisStrColumn, List<string> lisStrData)
        //{
        //    throw new NotImplementedException();
        //}

        //bool IBBarCodeForm.DeleteBarCodeByNRequestFormNo(string NRequestFormNo)
        //{
        //    throw new NotImplementedException();
        //}

        bool IBBarCodeForm.AddBarCodeForm(List<Model.BarCodeForm> bcf_List)
        {
            bool result = false;
            ZhiFang.Common.Log.Log.Debug("bcf_List.Count:" + bcf_List.Count);
            foreach (Model.BarCodeForm bcf in bcf_List)
            {
                int i = this.Add(bcf);
                if (i > 0)
                    result = true;
                else
                    result = false;
            }
            return result;
        }

        bool IBBarCodeForm.AddBarCodeForm_TaiHe(List<Model.BarCodeForm> bcf_List)
        {
            bool result = false;
            ZhiFang.Common.Log.Log.Debug("bcf_List.Count:" + bcf_List.Count);
            foreach (Model.BarCodeForm bcf in bcf_List)
            {
                int i = this.Add_TaiHe(bcf);
                if (i > 0)
                    result = true;
                else
                    result = false;
            }
            return result;
        }

        /// <summary>
        /// 转换成UiBarCode给前台
        /// </summary>
        /// <param name="bcf_List"></param>
        /// <param name="nrf_m"></param>
        /// <returns></returns>
        List<UiBarCode> IBBarCodeForm.ConvertBarCodeFormToUiBarCode(List<Model.BarCodeForm> bcf_List, Model.NRequestForm nrf_m)
        {
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            IBTestItem centerTestItem = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL();
            IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();
            List<UiBarCode> uiBarCodeList = new List<UiBarCode>();
            UiBarCode uiBarCode = new UiBarCode();
            foreach (Model.BarCodeForm barCodeForm in bcf_List)
            {
                uiBarCode = new UiBarCode();
                uiBarCode.BarCode = barCodeForm.BarCode;
                uiBarCode.ColorName = barCodeForm.Color;
                if (barCodeForm.Color != null && barCodeForm.Color != "")
                {
                    //uiBarCode.ColorValue = ZhiFang.BLL.Report.Lib.ItemColor()[barCodeForm.Color].ColorValue;
                    uiBarCode.ColorValue = icd.GetModelByColorName(barCodeForm.Color).ColorValue;
                    foreach (var itemSample in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(barCodeForm.Color)) //ZhiFang.BLL.Report.Lib.ItemColor()[barCodeForm.Color].SampleType)
                    {
                        if (itemSample.SampleTypeID == (int)barCodeForm.SampleTypeNo)
                        {
                            uiBarCode.SampleType = itemSample.CName;
                        }
                    }
                }
                string[] itemNames = barCodeForm.ItemName.Split(',');
                string[] itemNos = barCodeForm.ItemNo.Split(',');

                List<string> tempItemNameList = new List<string>();
                foreach (var itemno in itemNos)
                {
                    if (itemno == "" || itemno == null)
                        continue;
                    DataSet dsItem = ltic.GetLabTestItemByItemNo(nrf_m.ClientNo, itemno);
                    if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                        tempItemNameList.Add(dsItem.Tables[0].Rows[0]["CName"].ToString());
                }
                uiBarCode.ItemList = tempItemNameList;
                uiBarCodeList.Add(uiBarCode);
            }
            return uiBarCodeList;
        }

        /// <summary>
        /// 十堰太和 申请单录入 组套项目不需要对照 
        /// </summary>
        /// <param name="bcf_List"></param>
        /// <param name="nrf_m"></param>
        /// <returns></returns>
        List<UiBarCode> IBBarCodeForm.ConvertBarCodeFormToUiBarCode_TaiHe(List<Model.BarCodeForm> bcf_List, Model.NRequestForm nrf_m)
        {
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            IBTestItem centerTestItem = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL();
            IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();
            List<UiBarCode> uiBarCodeList = new List<UiBarCode>();
            UiBarCode uiBarCode = new UiBarCode();
            foreach (Model.BarCodeForm barCodeForm in bcf_List)
            {
                uiBarCode = new UiBarCode();
                uiBarCode.BarCode = barCodeForm.BarCode;
                uiBarCode.ColorName = barCodeForm.Color;
                if (barCodeForm.Color != null && barCodeForm.Color != "")
                {
                    //uiBarCode.ColorValue = ZhiFang.BLL.Report.Lib.ItemColor()[barCodeForm.Color].ColorValue;
                    uiBarCode.ColorValue = icd.GetModelByColorName(barCodeForm.Color).ColorValue;
                    foreach (var itemSample in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(barCodeForm.Color)) //ZhiFang.BLL.Report.Lib.ItemColor()[barCodeForm.Color].SampleType)
                    {
                        if (itemSample.SampleTypeID == (int)barCodeForm.SampleTypeNo)
                        {
                            uiBarCode.SampleType = itemSample.CName;
                        }
                    }
                }
                string[] itemNames = barCodeForm.LabItemName.Split(',');
                string[] itemNos = barCodeForm.LabItemNo.Split(',');

                List<string> tempItemNameList = new List<string>();
                foreach (var itemName in itemNames)
                {
                    if (itemName == "" || itemName == null)
                        continue;
                    tempItemNameList.Add(itemName);
                }
                //uiBarCode.ItemList = tempItemNameList;
                uiBarCode.ItemList = new List<string>() { barCodeForm.ItemName };
                uiBarCodeList.Add(uiBarCode);
            }
            return uiBarCodeList;
        }

        bool IBBarCodeForm.UpdateBarCodeForm(List<Model.BarCodeForm> bcf_List)
        {
            bool result = false;
            foreach (Model.BarCodeForm bcf in bcf_List)
            {
                int i;
                if (this.Exists((long)bcf.BarCodeFormNo))
                {
                    i = this.Update(bcf);
                }
                else
                    i = this.Add(bcf);

                if (i > 0)
                    result = true;
                else
                    result = false;
            }
            return result;
        }

        bool IBBarCodeForm.UpdateBarCodeForm_TaiHe(List<Model.BarCodeForm> bcf_List)
        {
            bool result = false;
            foreach (Model.BarCodeForm bcf in bcf_List)
            {
                int i;
                if (this.Exists((long)bcf.BarCodeFormNo))
                {
                    i = this.Update_TaiHe(bcf);
                }
                else
                    i = this.Add_TaiHe(bcf);

                if (i > 0)
                    result = true;
                else
                    result = false;
            }
            return result;
        }

        List<UiBarCode> IBBarCodeForm.GetUiBarCodeListByNrequestFormNo(long nrequestFormNo)
        {
            List<UiBarCode> uibarCodeList = new List<UiBarCode>();
            UiBarCode uiBarCode = new UiBarCode();
            IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
            Model.NRequestForm nrf_m = rfb.GetModel(nrequestFormNo);
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();
            IBSampleType samptype = BLLFactory<IBSampleType>.GetBLL();
            string[] barCodes = nrf_m.BarCode.Split(',');

            foreach (string barCode in barCodes)
            {
                if (barCode == null || barCode == "")
                    continue;

                DataSet BarCodeFormds = this.GetList(new Model.BarCodeForm() { BarCode = barCode });

                if (BarCodeFormds != null && BarCodeFormds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = BarCodeFormds.Tables[0].Rows[0];
                    long barCodeFormNo = long.Parse(dr["BarCodeFormNo"].ToString());

                    //string barCode_m = dr["BarCode"].ToString();

                    string sampleTypeNo = dr["SampleTypeNo"].ToString();

                    string colorName = this.GetModel(barCodeFormNo).Color;
                    string colorValue = "";
                    List<SampleTypeDetail> sampleTypeDetailList = new List<SampleTypeDetail>();
                    if (colorName != "" && colorName != null)
                    {
                        colorValue = icd.GetModelByColorName(colorName).ColorValue; //ZhiFang.BLL.Report.Lib.ItemColor()[colorName].ColorValue;
                        SampleTypeDetail sampletypedetail = new SampleTypeDetail();

                        foreach (var sampletype in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(colorName))//zhifang.bll.report.lib.itemcolor()[colorname].sampletype)
                        {
                            sampletypedetail = new SampleTypeDetail();
                            sampletypedetail.CName = sampletype.CName;
                            sampletypedetail.SampleTypeID = sampletype.SampleTypeID.ToString();
                            sampleTypeDetailList.Add(sampletypedetail);
                        }
                    }
                    List<Model.NRequestItem> nrequestItemList = rib.GetModelList(new Model.NRequestItem() { NRequestFormNo = nrequestFormNo, BarCodeFormNo = barCodeFormNo });
                    List<string> itemList = new List<string>();
                    foreach (var item in nrequestItemList)
                    {
                        //返回客户端的项目号
                        DataSet dsItem = ltic.GetLabTestItemByItemNo(nrf_m.ClientNo, item.ParItemNo);
                        if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                        {
                            itemList.Add(dsItem.Tables[0].Rows[0]["ItemNo"].ToString());
                        }

                        dsItem = ltic.GetLabTestItemByItemNo(nrf_m.ClientNo, item.CombiItemNo);
                        if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                        {
                            itemList.Add(dsItem.Tables[0].Rows[0]["ItemNo"].ToString());
                        }

                        //itemList.Add(item.ParItemNo);
                    }

                    //给对象赋值
                    uiBarCode = new UiBarCode();
                    uiBarCode.BarCode = barCode;
                    uiBarCode.ColorName = colorName;
                    uiBarCode.ColorValue = colorValue;
                    uiBarCode.ItemList = itemList;
                    uiBarCode.SampleType = sampleTypeNo;
                    uiBarCode.SampleTypeDetailList = sampleTypeDetailList;

                    uibarCodeList.Add(uiBarCode);

                }
            }
            return uibarCodeList;
        }

        /// <summary>
        /// 十堰太和 组套项目不需要对照
        /// </summary>
        /// <param name="nrequestFormNo"></param>
        /// <returns></returns>
        List<UiBarCode> IBBarCodeForm.GetUiBarCodeListByNrequestFormNo_TaiHe(long nrequestFormNo)
        {
            List<UiBarCode> uibarCodeList = new List<UiBarCode>();
            UiBarCode uiBarCode = new UiBarCode();
            IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
            Model.NRequestForm nrf_m = rfb.GetModel(nrequestFormNo);
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            IBItemColorDict icd = BLLFactory<IBItemColorDict>.GetBLL();
            IBSampleType samptype = BLLFactory<IBSampleType>.GetBLL();
            string[] barCodes = nrf_m.BarCode.Split(',');

            foreach (string barCode in barCodes)
            {
                if (barCode == null || barCode == "")
                    continue;

                DataSet BarCodeFormds = this.GetList(new Model.BarCodeForm() { BarCode = barCode });

                if (BarCodeFormds != null && BarCodeFormds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = BarCodeFormds.Tables[0].Rows[0];
                    long barCodeFormNo = long.Parse(dr["BarCodeFormNo"].ToString());

                    //string barCode_m = dr["BarCode"].ToString();

                    string sampleTypeNo = dr["SampleTypeNo"].ToString();

                    string colorName = this.GetModel(barCodeFormNo).Color;
                    string colorValue = "";
                    List<SampleTypeDetail> sampleTypeDetailList = new List<SampleTypeDetail>();
                    if (colorName != "" && colorName != null)
                    {
                        colorValue = icd.GetModelByColorName(colorName).ColorValue; //ZhiFang.BLL.Report.Lib.ItemColor()[colorName].ColorValue;
                        SampleTypeDetail sampletypedetail = new SampleTypeDetail();

                        foreach (var sampletype in ZhiFang.BLL.Common.Lib.GetSampleTypeByColorName(colorName))//zhifang.bll.report.lib.itemcolor()[colorname].sampletype)
                        {
                            sampletypedetail = new SampleTypeDetail();
                            sampletypedetail.CName = sampletype.CName;
                            sampletypedetail.SampleTypeID = sampletype.SampleTypeID.ToString();
                            sampleTypeDetailList.Add(sampletypedetail);
                        }
                    }
                    DataSet dsNrequestItem = rib.GetList_TaiHe(new Model.NRequestItem() { NRequestFormNo = nrequestFormNo, BarCodeFormNo = barCodeFormNo });
                    List<string> itemList = new List<string>();
                    if (dsNrequestItem != null && dsNrequestItem.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsNrequestItem.Tables[0].Rows.Count; i++)
                        {
                            itemList.Add(dsNrequestItem.Tables[0].Rows[i]["LabParItemNo"].ToString());
                        }

                    }

                    //给对象赋值
                    uiBarCode = new UiBarCode();
                    uiBarCode.BarCode = barCode;
                    uiBarCode.ColorName = colorName;
                    uiBarCode.ColorValue = colorValue;
                    uiBarCode.ItemList = itemList;
                    uiBarCode.SampleType = sampleTypeNo;
                    uiBarCode.SampleTypeDetailList = sampleTypeDetailList;

                    uibarCodeList.Add(uiBarCode);

                }
            }
            return uibarCodeList;
        }

        /// <summary>
        /// 根据申请单号,获取批量打印的条码信息
        /// </summary>
        /// <param name="nrequestFormNo"></param>
        /// <returns></returns>
        List<UiBarCode> IBBarCodeForm.GetBatchUiBarCodeListByNrequestFormNo(long nrequestFormNo)
        {
            List<UiBarCode> uibarCodeList = new List<UiBarCode>();
            UiBarCode uiBarCode = new UiBarCode();
            IBNRequestForm rfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
            //IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
            IBLab_TestItem ltic = ZhiFang.BLLFactory.BLLFactory<IBLab_TestItem>.GetBLL();
            Model.NRequestForm nrf_m = rfb.GetModel(nrequestFormNo);
            string[] barCodes = nrf_m.BarCode.Split(',');

            foreach (string barCode in barCodes)
            {
                if (barCode == null || barCode == "")
                    continue;

                DataSet BarCodeFormds = this.GetList(new Model.BarCodeForm() { BarCode = barCode });

                if (BarCodeFormds != null && BarCodeFormds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = BarCodeFormds.Tables[0].Rows[0];
                    long barCodeFormNo = long.Parse(dr["BarCodeFormNo"].ToString());

                    //string barCode_m = dr["BarCode"].ToString();

                    //string sampleTypeNo = dr["SampleTypeNo"].ToString();

                    string colorName = this.GetModel(barCodeFormNo).Color;
                    List<SampleType> sampleTypeList = new List<SampleType>();
                    string sampleTypeName = "";
                    string colorValue = "";
                    //if (colorName != "" && colorName != null)
                    //{
                    //    colorValue = ZhiFang.BLL.Report.Lib.ItemColor()[colorName].ColorValue;                       
                    //    sampleTypeList = ZhiFang.BLL.Report.Lib.ItemColor()[colorName].SampleType;

                    //    sampleTypeName = sampleTypeList.Find(p => p.SampleTypeID == int.Parse(dr["SampleTypeNo"].ToString())) == null ? "" : sampleTypeList.Find(p => p.SampleTypeID == int.Parse(dr["SampleTypeNo"].ToString())).CName;
                    //}
                    sampleTypeName = dr["SampleTypeName"].ToString();

                    List<string> itemNameList = new List<string>();
                    string[] itemNos = dr["ItemNo"].ToString().Split(',');
                    foreach (var itemNo in itemNos)
                    {
                        if (itemNo == null || itemNo == "")
                            continue;
                        DataSet dsItem = ltic.GetLabTestItemByItemNo(nrf_m.ClientNo, itemNo);
                        if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                        {
                            itemNameList.Add(dsItem.Tables[0].Rows[0]["CName"].ToString());
                        }
                    }

                    //给对象赋值
                    uiBarCode = new UiBarCode();
                    uiBarCode.BarCode = barCode;
                    uiBarCode.ColorName = colorName;
                    uiBarCode.ColorValue = colorValue;
                    uiBarCode.ItemList = itemNameList;
                    uiBarCode.SampleType = sampleTypeName;
                    //uiBarCode.SampleTypeDetailList = sampleTypeDetailList;

                    uibarCodeList.Add(uiBarCode);

                }
            }
            return uibarCodeList;
        }

        public DataSet GetBarCodeByOrderNo(string OrderNo)
        {
            return dal.GetBarCodeByOrderNo(OrderNo);
        }
        public int UpdateByOrderNo(Model.BarCodeForm barCode)
        {
            return dal.UpdateByOrderNo(barCode);
        }
        public bool OtherOrderUser(string BarCode)
        {
            return dal.OtherOrderUser(BarCode);
        }
        public int UpdateOrderNoByBarCodeFormNo(string BarCodeFormNo, string OrderNo)
        {
            return dal.UpdateOrderNoByBarCodeFormNo(BarCodeFormNo, OrderNo);
        }
        public int Add(string strSql)
        {
            return dal.Add(strSql);
        }
        public DataSet GetRefuseList(string strSql)
        {
            return dal.GetRefuseList(strSql);
        }

        /// <summary>
        /// 条码是否存在
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="barcodelist"></param>
        /// <returns></returns>
        public bool IsExistBarCode(string flag, List<UiBarCode> barcodelist, out string repeatbarcodestr)
        {
            bool result = false;
            //判断数据库中是否存在
            int dsRowCount = 0;
            repeatbarcodestr = "";
            foreach (var uiBarCode in barcodelist)
            {
                DataSet dsTemp = this.GetList(new ZhiFang.Model.BarCodeForm() { BarCode = uiBarCode.BarCode });
                if (dsTemp != null)
                {
                    dsRowCount += dsTemp.Tables[0].Rows.Count;
                    repeatbarcodestr += uiBarCode.BarCode + ",";
                }
            }

            if (flag == "1")
            {
                if (dsRowCount > 0)
                    result = true;
                else
                    result = false;
            }
            else
                result = false;

            return result;
        }
    }
}
