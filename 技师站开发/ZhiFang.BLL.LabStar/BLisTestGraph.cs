using System;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLisTestGraph : BaseBLL<LisTestGraph>, ZhiFang.IBLL.LabStar.IBLisTestGraph
    {
        public BaseResultDataValue AppendLisTestGraphToDataBase(long? graphDataID, long labID, string graphBase64, string graphThumb)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string strSQL = "";
            string strDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (graphDataID == null)
            {
                graphDataID = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                strSQL = "insert into Lis_GraphData(LabID, GraphDataID, GraphData, ThumbData, DataAddTime, DataUpdateTime) values(" +
                    labID + "," +
                    graphDataID + "," +
                    "\'" + graphBase64 + "\'," +
                    "\'" + graphThumb + "\'," +
                    "\'" + strDate + "\'," +
                    "\'" + strDate + "\')";
            }
            else
            {
                strSQL = "update Lis_GraphData set LabID=" + labID +
                         ",GraphData=\'" + graphBase64 + "\'" +
                         ",ThumbData=\'" + graphThumb + "\'" +
                         ",DataUpdateTime=\'" + strDate + "\'" +
                         " where GraphDataID=" + graphDataID;
            }
            ZhiFang.LabStar.DAO.ADO.BaseResult baseresult = ZhiFang.LabStar.DAO.ADO.SqlServerHelper.ExecuteSql(strSQL, ZhiFang.LabStar.DAO.ADO.SqlServerHelper.LabStarGraphConnectStr);
            brdv.success = baseresult.success;
            brdv.ErrorInfo = baseresult.ErrorInfo;
            brdv.ResultDataValue = graphDataID.ToString();
            return brdv;
        }

    }
}