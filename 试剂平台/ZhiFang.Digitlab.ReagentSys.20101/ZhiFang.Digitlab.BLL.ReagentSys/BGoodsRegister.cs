using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using System.Web;
using System.IO;
using ZhiFang.Digitlab.IDAO.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BGoodsRegister : BaseBLL<GoodsRegister>, ZhiFang.Digitlab.IBLL.ReagentSys.IBGoodsRegister
    {
        //注册文件路径(由web.config的配置项+实体对象名称+所属机构编号一起组成完全路径)
        public readonly string FilePath = "\\GoodsRegister";
        ZhiFang.Digitlab.IBLL.ReagentSys.IBCenOrg IBCenOrg { get; set; }

        public BaseResultDataValue AddGoodsRegisterAndUploadRegisterFile(HttpPostedFile file, string hrdeptID, string hrdeptCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            //如果中心机构信息为空,需要获取机构ID及CenOrgNo
            if (!this.Entity.CenOrgID.HasValue || String.IsNullOrEmpty(this.Entity.CenOrgNo))
                brdv = GetCenOrgInfo(hrdeptCode);
            if (brdv.success)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string registerFilePath = "";
                    BaseResultBool brdv2 = UploadRegisterFile(file, ref registerFilePath);
                    this.Entity.RegisterFilePath = registerFilePath;
                }
                //if()
                brdv.success = this.Add();
            }
            return brdv;
        }
        /// <summary>
        /// 获取新增时的机构信息
        /// </summary>
        /// <param name="hrdeptCode"></param>
        /// <returns></returns>
        private BaseResultDataValue GetCenOrgInfo(string hrdeptCode)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (!String.IsNullOrEmpty(hrdeptCode))
            {
                IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + hrdeptCode + "\'");
                if (listCenOrg.Count == 1)
                {
                    this.Entity.CenOrgID = listCenOrg[0].Id;
                    this.Entity.CenOrgNo = listCenOrg[0].OrgNo.ToString();
                }
                else if (listCenOrg.Count > 1)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "机构编号为:" + this.Entity.CenOrgNo + ",存在多个!";
                }
                else
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "机构编号为空!";
                }
            }
            else
            {
                brdv.success = false;
                brdv.ErrorInfo = "机构编号为空!";
            }
            return brdv;
        }

        public BaseResultBool UpdateGoodsRegisterAndUploadRegisterFileByField(string[] tempArray, HttpPostedFile file)
        {
            BaseResultBool brdv = new BaseResultBool();

            if (file != null && file.ContentLength > 0)
            {
                string registerFilePath = "";
                BaseResultBool brdv2 = UploadRegisterFile(file, ref registerFilePath);
                if (!string.IsNullOrEmpty(registerFilePath))
                {
                    this.Entity.RegisterFilePath = registerFilePath;
                    var tmpa = tempArray.ToList();
                    tmpa.Add("RegisterFilePath='" + registerFilePath + "'");
                    tempArray = tmpa.ToArray();
                }
            }
            if (tempArray != null)
            {
                brdv.success = this.Update(tempArray);
            }
            return brdv;
        }

        public EntityList<GoodsRegister> SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere, int page, int count)
        {
            IList<GoodsRegister> list = this.SearchListByHQL(strHqlWhere);
            EntityList<GoodsRegister> el = GetFilterRepeatRegisterNoList(list, page, count);
            return el;
        }
        /// <summary>
        /// 查询过滤掉重复的注册证编号的产品注册证表
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<GoodsRegister> SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere, string Order, int page, int count)
        {
            IList<GoodsRegister> list = ((IDGoodsRegisterDao)base.DBDao).GetListByHQL(strHqlWhere);
            EntityList<GoodsRegister> el = GetFilterRepeatRegisterNoList(list, page, count);
            return el;
        }
        /// <summary>
        /// 查询过滤掉重复的注册证编号的产品注册证表
        /// </summary>
        /// <param name="retultList"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private EntityList<GoodsRegister> GetFilterRepeatRegisterNoList(IList<GoodsRegister> retultList, int page, int limit)
        {
            EntityList<GoodsRegister> el = new EntityList<GoodsRegister>();
            var registerNoLists = new List<GoodsRegister>();
            var tempList = retultList.GroupBy(p => p.RegisterNo).ToList(); ;
            foreach (var list in tempList)
            {
                ZhiFang.Common.Log.Log.Debug("RegisterNo:" + list.Key);
                //注册证编号相同时只显示最后添加的信息
                registerNoLists.Add(list.OrderByDescending(p => p.DataAddTime).ToList().ElementAt(0));
            }
            if (registerNoLists.Count > 0)
                el.count = registerNoLists.Count;
            //分页处理
            if (limit < registerNoLists.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = registerNoLists.Skip(startIndex).Take(endIndex);
                if (list != null)
                {
                    el.list = list.ToList();
                }
            }
            else
            {
                el.list = registerNoLists;
            }
            return el;
        }
        /// <summary>
        /// 上传产品注册证附件
        /// 名称:guid+注册证编号+时间(年月日时分秒)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="registerFilePath"></param>
        /// <returns></returns>
        private BaseResultBool UploadRegisterFile(HttpPostedFile file, ref string registerFilePath)
        {
            BaseResultBool brdv = new BaseResultBool();
            registerFilePath = "";

            if (file != null)
            {
                string cenOrgNo = this.Entity.CenOrgNo;
                if (string.IsNullOrEmpty(cenOrgNo)) cenOrgNo = "Others";
                registerFilePath = cenOrgNo + "\\";

                string parentPath = this.GetFileParentPath();
                string allFilePath = parentPath + registerFilePath;
                if (!Directory.Exists(allFilePath))
                {
                    Directory.CreateDirectory(allFilePath);
                }
                string fileName = filterFileName(file.FileName);

                try
                {
                    registerFilePath = registerFilePath + this.Entity.Id + "_" + this.Entity.RegisterNo + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") +".pdf";
                    this.Entity.RegisterFilePath = registerFilePath;
                    string filepath = Path.Combine(parentPath, registerFilePath);
                    ZhiFang.Common.Log.Log.Debug("filepath:" + filepath);

                    file.SaveAs(filepath);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("产品注册证上传错误信息:" + ex.Message);
                }
            }

            return brdv;
        }
        /// <summary>
        /// 过滤文件名中的非法字符,并返回新的名字. 
        /// </summary>
        private string filterFileName(String fileName)
        {
            List<char> charArr = new List<char>() { '\\', '/', '*', '?', '"', '<', '>', '|', ':' };
            return charArr.Aggregate(fileName, (current, c) => current.Replace(c, '#'));
        }
        /// <summary>
        /// 获取上传保存的文件目录
        /// </summary>
        /// <returns></returns>
        public string GetFileParentPath()
        {
            string parentPath = ConfigHelper.GetConfigString("UploadRegisterFile").Trim();
            if (string.IsNullOrEmpty(parentPath))
            {
                parentPath = HttpContext.Current.Server.MapPath("~/UploadFiles/" + this.FilePath + "/");
            }
            else
            {
                parentPath = parentPath + this.FilePath + "\\";
            }
            if (!Directory.Exists(parentPath))
            {
                Directory.CreateDirectory(parentPath);
            }
            return parentPath;
        }
        /// <summary>
        /// 获取上传保存的证书原件
        /// </summary>
        /// <returns></returns>
        public FileStream GetGoodsRegisterFileStream(long id,ref string fileName)
        {
            FileStream fileStream = null;
            string filePath = "";
            string parentPath = this.GetFileParentPath();
            GoodsRegister entity = this.Get(id);
            if (entity == null || string.IsNullOrEmpty(entity.RegisterFilePath))
            {
                return fileStream;
            }
            fileName=entity.CName + ".pdf";
            filePath = entity.RegisterFilePath;
            filePath = parentPath + filePath;
            fileStream = new FileStream(@filePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }
    }
}