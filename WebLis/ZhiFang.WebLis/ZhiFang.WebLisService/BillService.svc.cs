using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;
using System.ServiceModel.Description;

namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“BillService”。
    public class BillService : IBillService
    {
        IBTB_CheckClientAccount ibtcca = BLLFactory<IBTB_CheckClientAccount>.GetBLL();
        #region 上传对账单
        //internal static ServiceHost myServiceHost = null;
        public bool UpLoadBll(DataSet ds, List<string> filesname, List<byte[]> filebyte, List<string> fileitemsname, List<byte[]> fileitemsbyte, out string errorinfo, out string strfailidforlist)
        {
            //BasicHttpBinding myBinding = new BasicHttpBinding();
            //myBinding.Security.Mode = BasicHttpSecurityMode.None;
            errorinfo = "";
            strfailidforlist = "";
            bool flag = false;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        #region 写入文件
                        flag = UpLoadExcel(filesname, filebyte, fileitemsname, fileitemsbyte, out errorinfo);
                        errorinfo += errorinfo;
                        if (flag == false)
                            return false;
                        #endregion

                        #region 赋值
                        TB_CheckClientAccount tbcc_m = new TB_CheckClientAccount();
                        if (ds.Tables[0].Rows[i]["cid"] != "" && ds.Tables[0].Rows[i]["cid"] != null)
                            tbcc_m.id = Convert.ToInt32(ds.Tables[0].Rows[i]["cid"]);

                        if (ds.Tables[0].Rows[i]["yid"] != "" && ds.Tables[0].Rows[i]["yid"] != null)
                            tbcc_m.monthid = Convert.ToInt32(ds.Tables[0].Rows[i]["yid"]);

                        if (ds.Tables[0].Rows[i]["yname"] != "" && ds.Tables[0].Rows[i]["yname"] != null)
                            tbcc_m.monthname = ds.Tables[0].Rows[i]["yname"].ToString();

                        if (ds.Tables[0].Rows[i]["cclientname"] != "" && ds.Tables[0].Rows[i]["cclientname"] != null)
                            tbcc_m.clientname = ds.Tables[0].Rows[i]["cclientname"].ToString();

                        if (ds.Tables[0].Rows[i]["cstatus"] != "" && ds.Tables[0].Rows[i]["cstatus"] != null)
                            tbcc_m.status = ds.Tables[0].Rows[i]["cstatus"].ToString();

                        if (ds.Tables[0].Rows[i]["cremark"] != "" && ds.Tables[0].Rows[i]["cremark"] != null)
                            tbcc_m.remark = ds.Tables[0].Rows[i]["cremark"].ToString();

                        if (ds.Tables[0].Rows[i]["ccheckdate"] != "" && ds.Tables[0].Rows[i]["ccheckdate"] != null)
                            tbcc_m.checkdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ccheckdate"].ToString());

                        if (ds.Tables[0].Rows[i]["cfilepath"] != "" && ds.Tables[0].Rows[i]["cfilepath"] != null)
                            tbcc_m.filepath = ds.Tables[0].Rows[i]["cfilepath"].ToString();

                        if (ds.Tables[0].Rows[i]["ccreatedate"] != "" && ds.Tables[0].Rows[i]["ccreatedate"] != null)
                            tbcc_m.createdate = Convert.ToDateTime(ds.Tables[0].Rows[i]["ccreatedate"].ToString());

                        if (ds.Tables[0].Rows[i]["creply"] != "" && ds.Tables[0].Rows[i]["creply"] != null)
                            tbcc_m.reply = ds.Tables[0].Rows[i]["creply"].ToString();

                        if (ds.Tables[0].Rows[i]["cclientno"] != "" && ds.Tables[0].Rows[i]["cclientno"] != null)
                            tbcc_m.clientno = ds.Tables[0].Rows[i]["cclientno"].ToString();

                        if (ds.Tables[0].Rows[i]["cauditstatus"] != "" && ds.Tables[0].Rows[i]["cauditstatus"] != null)
                            tbcc_m.auditstatus = ds.Tables[0].Rows[i]["cauditstatus"].ToString();

                        if (ds.Tables[0].Rows[i]["downloadfile"] != "" && ds.Tables[0].Rows[i]["downloadfile"] != null)
                            tbcc_m.downloadfile = ds.Tables[0].Rows[i]["downloadfile"].ToString();

                        if (ds.Tables[0].Rows[i]["ccount"] != "" && ds.Tables[0].Rows[i]["ccount"] != null)
                            tbcc_m.count = ds.Tables[0].Rows[i]["ccount"].ToString();

                        if (ds.Tables[0].Rows[i]["csumprice"] != "" && ds.Tables[0].Rows[i]["csumprice"] != null)
                            tbcc_m.sumprice = ds.Tables[0].Rows[i]["csumprice"].ToString();

                        if (ds.Tables[0].Rows[i]["cfilepathitem"] != "" && ds.Tables[0].Rows[i]["cfilepathitem"] != null)
                            tbcc_m.filepathitem = ds.Tables[0].Rows[i]["cfilepathitem"].ToString();

                        if (ds.Tables[0].Rows[i]["downloadfileitem"] != "" && ds.Tables[0].Rows[i]["downloadfileitem"] != null)
                            tbcc_m.downloadfileitem = ds.Tables[0].Rows[i]["downloadfileitem"].ToString();

                        #endregion

                        #region 删除对账单
                        flag = DeleteBll(tbcc_m.id, out errorinfo);
                        errorinfo += errorinfo;
                        #endregion

                        #region 写入表
                        if (tbcc_m.id > 0)
                            if (ibtcca.Add(tbcc_m) != 0)
                                strfailidforlist += tbcc_m.id + ",";
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        errorinfo += ex.ToString();
                        return false;
                    }
                }
                strfailidforlist = strfailidforlist.TrimEnd(',');
                return true;
            }
            else
                errorinfo = "无对账单信息";

            return true;
        }

        #endregion

        #region 删除对账单
        public bool DeleteBll(int id, out string errorinfo)
        {
            errorinfo = "";
            Common.Log.Log.Info("删除对账单:" + id);
            try
            {
                TB_CheckClientAccount tbcc_m = new TB_CheckClientAccount();
                if (id > 0)
                    tbcc_m.id = id;
                return ibtcca.Delete(tbcc_m.id);
            }
            catch (Exception ex)
            {
                errorinfo = ex.ToString();
                return false;
            }

        }
        #endregion

        #region 对账单项目对账上传(Excel)
        public bool UpLoadExcel(List<string> filesname, List<byte[]> filebyte, List<string> fileitemsname, List<byte[]> fileitemsbyte, out string errorinfo)
        {
            errorinfo = "";
            try
            {
                string[] path = filesname[0].Split('@');
                string[] pathimtem = fileitemsname[0].Split('@');
                string ExcelPath = Common.Public.ConfigHelper.GetConfigString("ExcelPath");
                #region 对账单
                Common.Log.Log.Error("开始！");
                if (filesname != null && filebyte != null)
                {
                    if (filesname.Count > 0 && filebyte.Count > 0 && filesname.Count == filebyte.Count)
                    {
                        Common.Log.Log.Error("附件名称个数：" + filesname.Count + "附件个数：" + filebyte.Count);
                        for (int i = 0; i < filesname.Count; i++)
                        {
                            Common.Log.Log.Info("保存：" + Common.Public.FilesHelper.CreatDirFile(ExcelPath + path[0], filesname[i], filebyte[i]));
                        }
                    }
                }
                #endregion

                #region 项目对账
                if (fileitemsname != null && fileitemsbyte != null)
                {
                    if (fileitemsname.Count > 0 && fileitemsbyte.Count > 0 && fileitemsname.Count == fileitemsbyte.Count)
                    {
                        Common.Log.Log.Error("附件名称个数：" + fileitemsname.Count + "附件个数：" + fileitemsbyte.Count);
                        for (int i = 0; i < fileitemsname.Count; i++)
                        {
                            Common.Log.Log.Info("保存：" + Common.Public.FilesHelper.CreatDirFile(ExcelPath + pathimtem[0], fileitemsname[i], fileitemsbyte[i]));
                        }
                    }
                }
                Common.Log.Log.Error("结束！");
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                errorinfo = ex.ToString();
                return false;
            }

        }
        #endregion
    }
}
