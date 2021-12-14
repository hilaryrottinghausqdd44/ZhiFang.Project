
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.Business;

namespace ZhiFang.Digitlab.BLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  class BBSoftWare : BaseBLL<BSoftWare>, ZhiFang.Digitlab.IBLL.Business.IBBSoftWare
	{
        IBBSoftWareVersionManager IBBSoftWareVersionManager { get; set; }

        public EntityList<BSoftWareVersionManager> SearchCheckIsUpdateSoftWare(string softWareCode, string softWareCurVersion)
        {
            EntityList<BSoftWareVersionManager> entityList = new EntityList<BSoftWareVersionManager>();
            IList<BSoftWareVersionManager> listSoftWareVersionManager = _searchCheckIsUpdateSoftWare(softWareCode, softWareCurVersion);
            entityList.count = listSoftWareVersionManager.Count;
            entityList.list = listSoftWareVersionManager;
            return entityList;
        }
        //public BaseResultDataValue SearchCheckIsUpdateSoftWare(string softWareCode, string softWareCurVersion)
        //{
        //    BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
        //    IList<BSoftWareVersionManager> listSoftWareVersionManager = _searchCheckIsUpdateSoftWare(softWareCode, softWareCurVersion);
        //    foreach (BSoftWareVersionManager softWareVersionManager in listSoftWareVersionManager)
        //    {
        //        string tempstr = softWareVersionManager.Id.ToString() + "|" + 
        //                         softWareVersionManager.Code + "|" + 
        //                         softWareVersionManager.Name + "|" + 
        //                         softWareVersionManager.SoftWareVersion + "|" + 
        //                         softWareVersionManager.SoftWareComment + "|" + 
        //                         softWareVersionManager.SoftWareName + "|" +
        //                         ZhiFang.Common.Public.ConfigHelper.GetConfigString("SoftWarePublishPath") + "\\" +
        //                         softWareVersionManager.SoftWareName + "\\" + 
        //                         softWareVersionManager.SoftWareVersion + "\\" +  
        //                         Path.GetFileName(softWareVersionManager.Name) + ".zip";
        //       if ( string.IsNullOrEmpty(tempBaseResultDataValue.ResultDataValue))
        //           tempBaseResultDataValue.ResultDataValue = tempstr;
        //        else
        //           tempBaseResultDataValue.ResultDataValue += ","+tempstr;
        //    }
        //    return tempBaseResultDataValue;  
        //}

        public IList<BSoftWareVersionManager> _searchCheckIsUpdateSoftWare(string softWareCode, string softWareCurVersion)
        {
            IList<BSoftWareVersionManager> listSoftWareVersionManager = new List<BSoftWareVersionManager>();
            IList<BSoftWare> listBSoftWare = this.SearchListByHQL("bsoftware.IsUse=true and bsoftware.Code='" + softWareCode + "'");
            if (listBSoftWare != null && listBSoftWare.Count == 1)
            {
                //IList<BSoftWareVersionManager> listVersion = listBSoftWare[0].BSoftWareVersionManagerList;
                IList<BSoftWareVersionManager> listVersion = IBBSoftWareVersionManager.SearchListByHQL("bsoftwareversionmanager.IsUse=true and bsoftwareversionmanager.Code='" + softWareCode + "'");
                if (listVersion != null && listVersion.Count > 0)
                {
                    //listVersion = listVersion.Where(p => p.IsUse==true).OrderBy( p => p.SoftWareVersion).ToList();
                    listVersion = listVersion.OrderBy(p => p.SoftWareVersion).ToList();
                    foreach (BSoftWareVersionManager bsoftWareVersion in listVersion)
                    {
                        //string strPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("SoftWarePublishPath") + "\\" + bsoftWareVersion.SoftWareName + "\\" + bsoftWareVersion.SoftWareVersion;
                        if (_judgeSoftwareVersion(softWareCurVersion, bsoftWareVersion.SoftWareVersion))
                        {
                            listSoftWareVersionManager.Add(bsoftWareVersion);
                        }
                    }
                }
            }
            return listSoftWareVersionManager;
        }
        /// <summary>
        /// 根据新旧版本号判断是否需要升级
        /// </summary>
        /// <param name="oldVersion">旧版本号</param>
        /// <param name="newVersion">新版本号</param>
        /// <returns></returns>
        public bool _judgeSoftwareVersion(string oldVersion, string newVersion)
        {
            bool flag = false;
            string[] arrayOldVersion = oldVersion.Split('.');
            string[] arrayNewVersion = newVersion.Split('.');
            for (int i = 0; i < arrayOldVersion.Length; i++)
            {
                if (int.Parse(arrayNewVersion[i]) > int.Parse(arrayOldVersion[i]))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public bool RemoveByCode(string BSoftWareCode)
        {
            IBBSoftWareVersionManager.DeleteByHql(" From BSoftWareVersionManager bsoftwareversionmanager where bsoftwareversionmanager.Code='" + BSoftWareCode + "'");

            return this.DeleteByHql(" From BSoftWare bsoftware where bsoftware.Code='" + BSoftWareCode + "'") > 0;
        }
	}
}