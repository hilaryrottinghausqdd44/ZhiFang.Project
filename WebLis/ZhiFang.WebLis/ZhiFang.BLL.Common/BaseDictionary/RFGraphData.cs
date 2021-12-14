using System; 
using System.Data;
using System.Collections.Generic; 
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
	 	//RFGraphData		
	public partial class RFGraphData:IBSynchData, IBRFGraphData,IBBatchCopy,IBDataDownload
	{
        IDAL.IDRFGraphData dal;
        IDAL.IDBatchCopy dalCopy;
        
		public RFGraphData()
        {
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("2009") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").Trim().IndexOf("66") >= 0 || ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType").ToLower().IndexOf("weblis") >= 0)
            {
                dal = DALFactory.DalFactory<IDAL.IDRFGraphData>.GetDal("RFGraphData", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("RFGraphData", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
            else
            {
                dal = DALFactory.DalFactory<IDAL.IDRFGraphData>.GetDal("B_RFGraphData", ZhiFang.Common.Dictionary.DBSource.LisDB());
                dalCopy = DALFactory.DalFactory<IDAL.IDBatchCopy>.GetDal("B_RFGraphData", ZhiFang.Common.Dictionary.DBSource.LisDB());
            }
        }
		
		#region  Method
		/// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GraphName,int GraphNo,string FormNo)
        {
        	return dal.Exists(GraphName,GraphNo,FormNo);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(ZhiFang.Model.RFGraphData model)
		{
			return dal.Add(model);		
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ZhiFang.Model.RFGraphData model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(string GraphName,int GraphNo,string FormNo)
		{			
			return dal.Delete(GraphName,GraphNo,FormNo);
		}
				
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int DeleteList(string GraphIDlist )
		{
			return dal.DeleteList(GraphIDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZhiFang.Model.RFGraphData GetModel(string GraphName,int GraphNo,string FormNo)
		{			
			return dal.GetModel(GraphName,GraphNo,FormNo);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        public ZhiFang.Model.RFGraphData GetModelByCache(string GraphName, int GraphNo, string FormNo)
		{
			
			string CacheKey = "B_RFGraphDataModel-" + GraphName+GraphNo+FormNo;
			object objModel =ZhiFang.Common.Public.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GraphName,GraphNo,FormNo);
					if (objModel != null)
					{
						int ModelCache = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("ModelCache");
						ZhiFang.Common.Public.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (ZhiFang.Model.RFGraphData)objModel;
		}

		
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<ZhiFang.Model.RFGraphData> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.RFGraphData> modelList = new List<ZhiFang.Model.RFGraphData>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.RFGraphData model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.RFGraphData();
                    if (dt.Rows[n]["GraphID"].ToString() != "")
                    {
                        model.GraphID = int.Parse(dt.Rows[n]["GraphID"].ToString());
                    }
                    model.GraphName = dt.Rows[n]["GraphName"].ToString();
                    if (dt.Rows[n]["GraphNo"].ToString() != "")
                    {
                        model.GraphNo = int.Parse(dt.Rows[n]["GraphNo"].ToString());
                    }
                    if (dt.Rows[n]["EquipNo"].ToString() != "")
                    {
                        model.EquipNo = int.Parse(dt.Rows[n]["EquipNo"].ToString());
                    }
                    if (dt.Rows[n]["PointType"].ToString() != "")
                    {
                        model.PointType = int.Parse(dt.Rows[n]["PointType"].ToString());
                    }
                    if (dt.Rows[n]["ShowPoint"].ToString() != "")
                    {
                        model.ShowPoint = int.Parse(dt.Rows[n]["ShowPoint"].ToString());
                    }
                    if (dt.Rows[n]["MColor"].ToString() != "")
                    {
                        model.MColor = int.Parse(dt.Rows[n]["MColor"].ToString());
                    }
                    model.SColor = dt.Rows[n]["SColor"].ToString();
                    if (dt.Rows[n]["ShowAxis"].ToString() != "")
                    {
                        model.ShowAxis = int.Parse(dt.Rows[n]["ShowAxis"].ToString());
                    }
                    if (dt.Rows[n]["ShowLable"].ToString() != "")
                    {
                        model.ShowLable = int.Parse(dt.Rows[n]["ShowLable"].ToString());
                    }
                    if (dt.Rows[n]["MinX"].ToString() != "")
                    {
                        model.MinX = decimal.Parse(dt.Rows[n]["MinX"].ToString());
                    }
                    if (dt.Rows[n]["MaxX"].ToString() != "")
                    {
                        model.MaxX = decimal.Parse(dt.Rows[n]["MaxX"].ToString());
                    }
                    if (dt.Rows[n]["MinY"].ToString() != "")
                    {
                        model.MinY = decimal.Parse(dt.Rows[n]["MinY"].ToString());
                    }
                    if (dt.Rows[n]["MaxY"].ToString() != "")
                    {
                        model.MaxY = decimal.Parse(dt.Rows[n]["MaxY"].ToString());
                    }
                    if (dt.Rows[n]["ShowTitle"].ToString() != "")
                    {
                        model.ShowTitle = int.Parse(dt.Rows[n]["ShowTitle"].ToString());
                    }
                    model.STitle = dt.Rows[n]["STitle"].ToString();
                    model.GraphValue = dt.Rows[n]["GraphValue"].ToString();
                    model.GraphMemo = dt.Rows[n]["GraphMemo"].ToString();
                    model.GraphF1 = dt.Rows[n]["GraphF1"].ToString();
                    model.GraphF2 = dt.Rows[n]["GraphF2"].ToString();
                    if (dt.Rows[n]["ChartTop"].ToString() != "")
                    {
                        model.ChartTop = int.Parse(dt.Rows[n]["ChartTop"].ToString());
                    }
                    if (dt.Rows[n]["ChartHeight"].ToString() != "")
                    {
                        model.ChartHeight = int.Parse(dt.Rows[n]["ChartHeight"].ToString());
                    }
                    if (dt.Rows[n]["ChartLeft"].ToString() != "")
                    {
                        model.ChartLeft = int.Parse(dt.Rows[n]["ChartLeft"].ToString());
                    }
                    if (dt.Rows[n]["ChartWidth"].ToString() != "")
                    {
                        model.ChartWidth = int.Parse(dt.Rows[n]["ChartWidth"].ToString());
                    }
                    if (dt.Rows[n]["Graphjpg"].ToString() != "")
                    {
                        model.Graphjpg = (byte[])dt.Rows[n]["Graphjpg"];
                    }
                    model.FormNo = dt.Rows[n]["FormNo"].ToString();
                    if (dt.Rows[n]["UnionKey"].ToString() != "")
                    {
                        model.UnionKey = int.Parse(dt.Rows[n]["UnionKey"].ToString());
                    }
                    if (dt.Rows[n]["DTimeStampe"].ToString() != "")
                    {
                        model.DTimeStampe = DateTime.Parse(dt.Rows[n]["DTimeStampe"].ToString());
                    }
                    if (dt.Rows[n]["AddTime"].ToString() != "")
                    {
                        model.AddTime = DateTime.Parse(dt.Rows[n]["AddTime"].ToString());
                    }
                    model.StandCode = dt.Rows[n]["StandCode"].ToString();
                    model.ZFStandCode = dt.Rows[n]["ZFStandCode"].ToString();
                    if (dt.Rows[n]["UseFlag"].ToString() != "")
                    {
                        model.UseFlag = int.Parse(dt.Rows[n]["UseFlag"].ToString());
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
			return GetList(new ZhiFang.Model.RFGraphData());
		}
		/// <summary>
		/// 根据实体获取DataSet
		/// </summary>
		public DataSet GetList(ZhiFang.Model.RFGraphData model)
		{
			return dal.GetList(model);
		}
		public int GetTotalCount()
		{
			return dal.GetTotalCount();
		}
		public int GetTotalCount(ZhiFang.Model.RFGraphData model)
		{
			return dal.GetTotalCount(model);
		}
		public DataSet GetListByPage(ZhiFang.Model.RFGraphData model, int nowPageNum, int nowPageSize)
        {
            if (nowPageNum >= 0 && nowPageSize > 0)
            {
                return dal.GetListByPage(model, nowPageNum, nowPageSize);
            }
            else
                return null;
        }
        
        public bool CopyToLab(List<string> lst)
        {
            return dalCopy.CopyToLab(lst);
        }
		#endregion


        #region IBDataDownload 成员

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            IDAL.IDGetListByTimeStampe dalGetBytStampe = DALFactory.DalFactory<IDAL.IDGetListByTimeStampe>.GetDal("B_RFGraphData", ZhiFang.Common.Dictionary.DBSource.LisDB());
            try
            {
                DataSet dsAll = dalGetBytStampe.GetListByTimeStampe(LabCode.Trim(), time);
                strXMLSchema = dsAll.GetXmlSchema(); //xml结构文件
                strXML = dsAll.GetXml();//数据内容xml文件
                strMsg = "通过服务获取XML成功";
                return 1;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.RFGraphData.GetDictionaryXML---->参数LabCode=" + LabCode + "；time=" + time, ex);
                strXML = "";
                strXMLSchema = "";
                strMsg = "失败，RFGraphData获取最新字典数据出现异常" ;
                return 0;
            }
        }

        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBase<RFGraphData> 成员


        public int AddUpdateByDataSet(DataSet ds)
        {
            return dal.AddUpdateByDataSet(ds);
        }

        #endregion

        #region IBSynchData 成员


        public bool Exists(System.Collections.Hashtable ht)
        {
            throw new NotImplementedException();
        }

        public int AddByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        public int UpdateByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBBatchCopy 成员


        public int DeleteByDataRow(DataRow dr)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool IsExist(string labCodeNo)
        {
            return dalCopy.IsExist(labCodeNo);
        }

        public bool DeleteByLabCode(string LabCodeNo)
        {
            return dalCopy.DeleteByLabCode(LabCodeNo);
        }
    }
}
