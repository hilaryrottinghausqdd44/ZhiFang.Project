using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;
using ZhiFang.Common.Public;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Linq;
using System.Web;
using System.IO;
using System;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaGoodsRegister : BaseBLL<ReaGoodsRegister>, ZhiFang.IBLL.ReagentSys.Client.IBReaGoodsRegister
    {

        //注册文件路径(由web.config的配置项+实体对象名称+所属机构编号一起组成完全路径)
        //public readonly string FilePath = "\\ReaGoodsRegister";
        //IBCenOrg IBCenOrg { get; set; }

        public BaseResultDataValue AddReaGoodsRegisterAndUploadRegisterFile(HttpPostedFile file)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (file != null && file.ContentLength > 0)
            {
                BaseResultBool brdv2 = UploadRegisterFile(file);
            }
            brdv.success = this.Add();
            return brdv;
        }

        public BaseResultBool UpdateReaGoodsRegisterAndUploadRegisterFileByField(string[] tempArray, HttpPostedFile file)
        {
            BaseResultBool brdv = new BaseResultBool();

            if (file != null && file.ContentLength > 0)
            {
                BaseResultBool brdv2 = UploadRegisterFile(file);
                if (!string.IsNullOrEmpty(this.Entity.RegisterFilePath))
                {
                    var tmpa = tempArray.ToList();
                    tmpa.Add("RegisterFilePath='" + this.Entity.RegisterFilePath + "'");
                    tempArray = tmpa.ToArray();
                }
            }
            if (tempArray != null)
            {
                brdv.success = this.Update(tempArray);
            }
            return brdv;
        }

        public EntityList<ReaGoodsRegister> SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere, int page, int count)
        {
            IList<ReaGoodsRegister> list = this.SearchListByHQL(strHqlWhere);
            EntityList<ReaGoodsRegister> el = GetFilterRepeatRegisterNoList(list, page, count);
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
        public EntityList<ReaGoodsRegister> SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(string strHqlWhere, string Order, int page, int count)
        {
            IList<ReaGoodsRegister> list = ((IDReaGoodsRegisterDao)base.DBDao).GetListByHQL(strHqlWhere);
            EntityList<ReaGoodsRegister> el = GetFilterRepeatRegisterNoList(list, page, count);
            return el;
        }
        /// <summary>
        /// 查询过滤掉重复的注册证编号的产品注册证表
        /// </summary>
        /// <param name="retultList"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private EntityList<ReaGoodsRegister> GetFilterRepeatRegisterNoList(IList<ReaGoodsRegister> retultList, int page, int limit)
        {
            EntityList<ReaGoodsRegister> el = new EntityList<ReaGoodsRegister>();
            var registerNoLists = new List<ReaGoodsRegister>();
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
        private BaseResultBool UploadRegisterFile(HttpPostedFile file)
        {
            BaseResultBool brdv = new BaseResultBool();
            string registerFilePath = "";

            if (file != null)
            {
                registerFilePath = this.Entity.CompanyName + "\\";

                string parentPath = this.GetFileParentPath(this.Entity.LabID);
                string allFilePath = parentPath + registerFilePath;
                if (!Directory.Exists(allFilePath))
                {
                    Directory.CreateDirectory(allFilePath);
                }
                string fileName = filterFileName(file.FileName);

                try
                {
                    registerFilePath = registerFilePath + this.Entity.Id + ".pdf";
                    this.Entity.RegisterFilePath = registerFilePath;
                    string filepath = Path.Combine(parentPath, registerFilePath);

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
        private string filterFileName(string fileName)
        {
            List<char> charArr = new List<char>() { '\\', '/', '*', '?', '"', '<', '>', '|', ':' };
            return charArr.Aggregate(fileName, (current, c) => current.Replace(c, '#'));
        }
        /// <summary>
        /// 获取上传保存的文件目录
        /// </summary>
        /// <returns></returns>
        public string GetFileParentPath(long labID)
        {
            string path = "~/UploadFiles/ReaGoodsRegister/";
            if (labID >= 0)
                path = path + labID + "/";
            string parentPath = HttpContext.Current.Server.MapPath(path);
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
        public FileStream GetReaGoodsRegisterFileStream(long id, ref string fileName)
        {
            FileStream fileStream = null;
            string filePath = "";

            ReaGoodsRegister entity = this.Get(id);
            string parentPath = this.GetFileParentPath(entity.LabID);
            if (entity == null || string.IsNullOrEmpty(entity.RegisterFilePath))
            {
                return fileStream;
            }
            fileName = entity.CName + ".pdf";
            filePath = entity.RegisterFilePath;
            filePath = parentPath + filePath;
            fileStream = new FileStream(@filePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }
    }
}