using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.ServiceModel.Web;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.ServiceCommon
{
    public class PublicServiceCommon
    {
        public virtual ZhiFang.Digitlab.IBLL.Business.IBBSoftWare IBBSoftWare { get; set; }
        public virtual ZhiFang.Digitlab.IBLL.Business.IBBSoftWareUpdateHistory IBBSoftWareUpdateHistory { get; set; }

        public bool UpLoadFile(string fileName, byte[] pReadByte)
        {
            bool tempResult = false;
            FileStream pFileStream = null;
            try
            {
                if (File.Exists(fileName))
                {
                    pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                    pFileStream.Write(pReadByte, 0, pReadByte.Length);
                    tempResult = true;
                }
            }
            catch
            {
            }
            finally
            {
                if (pFileStream != null)

                    pFileStream.Close();
            }
            return tempResult;
        }

        public byte[] DownLoadFile(string FileDir)
        {
            return FilesHelper.GetFileByte(FileDir);
        }

        public byte[] DownLoadUpdateFile(string FileDir)
        {
            string tempFileName = Path.Combine(ZhiFang.Common.Public.ConfigHelper.GetConfigString("SoftWarePublishPath"), FileDir);
            return FilesHelper.GetFileByte(tempFileName);
        }

        public byte[] DownLoadReportFile(string FileURL)
        {
            //FileURL = @"C:\TestFile\TestReort.pdf";
            string tempFileName = System.AppDomain.CurrentDomain.BaseDirectory + FileURL;
            return FilesHelper.GetFileByte(tempFileName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        //public BaseResultDataValue Public_File_UpLoadByFrom(string relativePath, string fileType)
        public BaseResultDataValue Public_File_UpLoadByFrom()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    tempBaseResultDataValue.ErrorInfo = "未检测到文件！";
                    tempBaseResultDataValue.success = false;
                }
                else
                {
                    string tempPath = "";
                    string temprelativePath = HttpContext.Current.Request.Form["relativePath"];
                    string tempGUIDFileName = HttpContext.Current.Request.Form["isGUIDFileName"];
                    if (string.IsNullOrEmpty(temprelativePath))
                    {
                        temprelativePath = "TempUpLoadFile";
                    }

                    tempPath = System.AppDomain.CurrentDomain.BaseDirectory + temprelativePath;
                    if (!Directory.Exists(tempPath))
                        Directory.CreateDirectory(tempPath);
                    for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        HttpPostedFile tempFile = HttpContext.Current.Request.Files[i];
                        int len = tempFile.ContentLength;
                        if (len > 0 && !string.IsNullOrEmpty(tempFile.FileName))
                        {
                            string tempFileName = "";
                            if (!string.IsNullOrEmpty(tempGUIDFileName) && tempGUIDFileName.ToUpper() == "TRUE")
                            {
                                string filetype = Path.GetExtension(tempFile.FileName).ToUpper();
                                tempFileName = GUIDHelp.GetGUIDString() + filetype;
                            }
                            else
                                tempFileName = tempFile.FileName;
                            tempFile.SaveAs(tempPath + "\\" + tempFileName);
                            //tempBaseResultDataValue.ResultDataValue = "{ FilePath:" + temprelativePath + "/" + tempFile.FileName + "}";
                            tempBaseResultDataValue.ResultDataValue = "{ FilePath:'" + tempFileName+ "'}";
                        }
                        else
                        {
                            tempBaseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                            tempBaseResultDataValue.success = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                tempBaseResultDataValue.ErrorInfo = e.Message;
                tempBaseResultDataValue.success = false;
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 下载和打开文件
        /// </summary>
        /// <param name="filePath">文件路径（加密之后的文件路径）</param>
        /// <param name="type">获取文件之后的操作方式：0下载文件；1打开文件</param>
        /// <param name="fileName">要保存的文件名</param>
        /// <returns>文件流</returns>
        public Stream Public_UDTO_DownLoadFile(string filePath, int type, string fileName)
        {
            FileStream tempFileStream = null;
            string tempFileName = "";
            try
            {
                string tempFilePath = SecurityHelp.DecryptString(filePath, "7?hm???");
                //string tempFilePath = id;
                if (!File.Exists(tempFilePath))
                    throw new FileNotFoundException("要获取的文件不存在或已删除！");
                //取文件路径中的文件名
                if (string.IsNullOrEmpty(fileName))
                    //tempFileName = System.IO.Path.GetFileName(tempFilePath);
                    tempFileName = System.IO.Path.GetFileNameWithoutExtension(tempFilePath);
                else
                    //tempFileName = HttpUtility.UrlEncode(name);
                    tempFileName = fileName;
                //取文件路径中的文件扩展名
                string tempExtension = System.IO.Path.GetExtension(tempFilePath);
                tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
                //System.Threading.Thread.Sleep(1000);
                tempFileName = tempFileName + tempExtension;
                Encoding code = Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                if (type == 0) //下载文件
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + tempFileName);
                    //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                    //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                }
                else if (type == 1)//直接打开PDF文件
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + tempFileName);
                    //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                }
            }
            catch (Exception ex)
            {
                tempFileStream = null;
                throw new Exception(ex.Message);
            }
            return tempFileStream;
        }

        public BaseResultDataValue Public_UDTO_GetEnumNameList(string EnumTypeName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (!string.IsNullOrEmpty(EnumTypeName))
                {
                    List<string> EnumNameList = EnumDictionary.EnumToNameList(EnumTypeName);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(EnumNameList);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
            }
            return tempBaseResultDataValue;      
        }

        public BaseResultDataValue Public_UDTO_CheckIsUpdateSoftWare(string softWareCode, string softWareCurVersion)
        {
            //ZhiFang.Common.Log.Log.Debug("1softWareCode:" + softWareCode + ";softWareCurVersion:" + softWareCurVersion);
            bool isPlanish = true;
            string fields = "BSoftWareVersionManager_Name," + "BSoftWareVersionManager_Comment," +
                            "BSoftWareVersionManager_SoftWareVersion," + "BSoftWareVersionManager_SoftWareName," +
                            "BSoftWareVersionManager_SoftWareComment," + "BSoftWareVersionManager_Id," +
                            "BSoftWareVersionManager_PublishName," + "BSoftWareVersionManager_PublishTime," +
                            "BSoftWareVersionManager_Code," + "BSoftWareVersionManager_DataAddTime";
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BSoftWareVersionManager> tempEntityList = new EntityList<BSoftWareVersionManager>();
            //ZhiFang.Common.Log.Log.Debug("2softWareCode:" + softWareCode + ";softWareCurVersion:" + softWareCurVersion);
            try
            {
                if (IBBSoftWare == null)
                {
                    //ZhiFang.Common.Log.Log.Debug("3softWareCode:" + softWareCode + ";softWareCurVersion:" + softWareCurVersion);
                }
                tempEntityList = IBBSoftWare.SearchCheckIsUpdateSoftWare(softWareCode, softWareCurVersion);
                //ZhiFang.Common.Log.Log.Debug("4softWareCode:" + softWareCode + ";softWareCurVersion:" + softWareCurVersion);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                //ZhiFang.Common.Log.Log.Debug("tempParseObjectProperty:" + tempParseObjectProperty + ";softWareCurVersion:" + softWareCurVersion);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BSoftWareVersionManager>(tempEntityList.list);
                    //ZhiFang.Common.Log.Log.Debug("tempEntityList.list:" + tempEntityList.list.Count);
                }
                else
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.ToString();
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Debug("Public_UDTO_CheckIsUpdateSoftWare异常:" + ex.ToString());
            }
            return tempBaseResultDataValue;         
        }

        public BaseResultDataValue Public_UDTO_AddSoftWareUpdateHistory(BSoftWareUpdateHistory entity)
        {
            IBBSoftWareUpdateHistory.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBSoftWareUpdateHistory.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBSoftWareUpdateHistory.Get(IBBSoftWareUpdateHistory.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBSoftWareUpdateHistory.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;       
        }

    }
}
