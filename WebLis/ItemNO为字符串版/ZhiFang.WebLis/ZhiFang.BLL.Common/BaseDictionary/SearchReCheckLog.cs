using System;
using System.Data;
using System.Collections.Generic;
using ZhiFang.Common;
using ZhiFang.Model;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public class SearchReCheckLog : IBSearchReCheckLog
    {
        private readonly IDSearchReCheckLog dal = DalFactory<IDSearchReCheckLog>.GetDalByClassName("SearchReCheckLog");

        public SearchReCheckLog()
        { }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ZhiFang.Model.SearchReCheckLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(ZhiFang.Model.SearchReCheckLog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ZhiFang.Model.SearchReCheckLog GetModel(long Id)
        {

            return dal.GetModel(Id);
        }

       
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SearchReCheckLog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZhiFang.Model.SearchReCheckLog> DataTableToList(DataTable dt)
        {
            List<ZhiFang.Model.SearchReCheckLog> modelList = new List<ZhiFang.Model.SearchReCheckLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                ZhiFang.Model.SearchReCheckLog model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new ZhiFang.Model.SearchReCheckLog();
                    if (dt.Rows[n]["Id"] != null && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = long.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["BATCH_NUM"] != null && dt.Rows[n]["BATCH_NUM"].ToString() != "")
                    {
                        model.BATCH_NUM = dt.Rows[n]["BATCH_NUM"].ToString();
                    }
                    if (dt.Rows[n]["UPLOAD_TIME"] != null && dt.Rows[n]["UPLOAD_TIME"].ToString() != "")
                    {
                        model.UPLOAD_TIME = dt.Rows[n]["UPLOAD_TIME"].ToString();
                    }
                    if (dt.Rows[n]["LOCAL_ID"] != null && dt.Rows[n]["LOCAL_ID"].ToString() != "")
                    {
                        model.LOCAL_ID = dt.Rows[n]["LOCAL_ID"].ToString();
                    }
                    if (dt.Rows[n]["RESULTDESC"] != null && dt.Rows[n]["RESULTDESC"].ToString() != "")
                    {
                        model.RESULTDESC = dt.Rows[n]["RESULTDESC"].ToString();
                    }
                    if (dt.Rows[n]["RESULTCODE"] != null && dt.Rows[n]["RESULTCODE"].ToString() != "")
                    {
                        model.RESULTCODE = dt.Rows[n]["RESULTCODE"].ToString();
                    }
                    if (dt.Rows[n]["SERIALNUM_ID"] != null && dt.Rows[n]["SERIALNUM_ID"].ToString() != "")
                    {
                        model.SERIALNUM_ID = dt.Rows[n]["SERIALNUM_ID"].ToString();
                    }
                    if (dt.Rows[n]["UNIQUEID"] != null && dt.Rows[n]["UNIQUEID"].ToString() != "")
                    {
                        model.UNIQUEID = dt.Rows[n]["UNIQUEID"].ToString();
                    }
                    if (dt.Rows[n]["PID"] != null && dt.Rows[n]["PID"].ToString() != "")
                    {
                        model.PID = dt.Rows[n]["PID"].ToString();
                    }
                    if (dt.Rows[n]["BUSINESS_RELATION_ID"] != null && dt.Rows[n]["BUSINESS_RELATION_ID"].ToString() != "")
                    {
                        model.BUSINESS_RELATION_ID = dt.Rows[n]["BUSINESS_RELATION_ID"].ToString();
                    }
                    if (dt.Rows[n]["BUSINESS_ACTIVE_TYPE"] != null && dt.Rows[n]["BUSINESS_ACTIVE_TYPE"].ToString() != "")
                    {
                        model.BUSINESS_ACTIVE_TYPE = dt.Rows[n]["BUSINESS_ACTIVE_TYPE"].ToString();
                    }
                    if (dt.Rows[n]["BUSINESS_ACTIVE_DES"] != null && dt.Rows[n]["BUSINESS_ACTIVE_DES"].ToString() != "")
                    {
                        model.BUSINESS_ACTIVE_DES = dt.Rows[n]["BUSINESS_ACTIVE_DES"].ToString();
                    }
                    if (dt.Rows[n]["BUSINESS_ID"] != null && dt.Rows[n]["BUSINESS_ID"].ToString() != "")
                    {
                        model.BUSINESS_ID = dt.Rows[n]["BUSINESS_ID"].ToString();
                    }
                    if (dt.Rows[n]["BASIC_ACTIVE_TYPE"] != null && dt.Rows[n]["BASIC_ACTIVE_TYPE"].ToString() != "")
                    {
                        model.BASIC_ACTIVE_TYPE = dt.Rows[n]["BASIC_ACTIVE_TYPE"].ToString();
                    }
                    if (dt.Rows[n]["BASIC_ACTIVE_DES"] != null && dt.Rows[n]["BASIC_ACTIVE_DES"].ToString() != "")
                    {
                        model.BASIC_ACTIVE_DES = dt.Rows[n]["BASIC_ACTIVE_DES"].ToString();
                    }
                    if (dt.Rows[n]["BASIC_ACTIVE_ID"] != null && dt.Rows[n]["BASIC_ACTIVE_ID"].ToString() != "")
                    {
                        model.BASIC_ACTIVE_ID = dt.Rows[n]["BASIC_ACTIVE_ID"].ToString();
                    }
                    if (dt.Rows[n]["ORGANIZATION_CODE"] != null && dt.Rows[n]["ORGANIZATION_CODE"].ToString() != "")
                    {
                        model.ORGANIZATION_CODE = dt.Rows[n]["ORGANIZATION_CODE"].ToString();
                    }
                    if (dt.Rows[n]["ORGANIZATION_NAME"] != null && dt.Rows[n]["ORGANIZATION_NAME"].ToString() != "")
                    {
                        model.ORGANIZATION_NAME = dt.Rows[n]["ORGANIZATION_NAME"].ToString();
                    }
                    if (dt.Rows[n]["DOMAIN_CODE"] != null && dt.Rows[n]["DOMAIN_CODE"].ToString() != "")
                    {
                        model.DOMAIN_CODE = dt.Rows[n]["DOMAIN_CODE"].ToString();
                    }
                    if (dt.Rows[n]["DOMAIN_NAME"] != null && dt.Rows[n]["DOMAIN_NAME"].ToString() != "")
                    {
                        model.DOMAIN_NAME = dt.Rows[n]["DOMAIN_NAME"].ToString();
                    }
                    if (dt.Rows[n]["VER"] != null && dt.Rows[n]["VER"].ToString() != "")
                    {
                        model.VER = dt.Rows[n]["VER"].ToString();
                    }
                    if (dt.Rows[n]["VERDES"] != null && dt.Rows[n]["VERDES"].ToString() != "")
                    {
                        model.VERDES = dt.Rows[n]["VERDES"].ToString();
                    }
                    if (dt.Rows[n]["REGION_IDEN"] != null && dt.Rows[n]["REGION_IDEN"].ToString() != "")
                    {
                        model.REGION_IDEN = dt.Rows[n]["REGION_IDEN"].ToString();
                    }
                    if (dt.Rows[n]["DATA_SECURITY"] != null && dt.Rows[n]["DATA_SECURITY"].ToString() != "")
                    {
                        model.DATA_SECURITY = dt.Rows[n]["DATA_SECURITY"].ToString();
                    }
                    if (dt.Rows[n]["RECORD_IDEN"] != null && dt.Rows[n]["RECORD_IDEN"].ToString() != "")
                    {
                        model.RECORD_IDEN = dt.Rows[n]["RECORD_IDEN"].ToString();
                    }
                    if (dt.Rows[n]["CREATE_DATE"] != null && dt.Rows[n]["CREATE_DATE"].ToString() != "")
                    {
                        model.CREATE_DATE = dt.Rows[n]["CREATE_DATE"].ToString();
                    }
                    if (dt.Rows[n]["UPDATE_DATE"] != null && dt.Rows[n]["UPDATE_DATE"].ToString() != "")
                    {
                        model.UPDATE_DATE = dt.Rows[n]["UPDATE_DATE"].ToString();
                    }
                    if (dt.Rows[n]["DATAGENERATE_DATE"] != null && dt.Rows[n]["DATAGENERATE_DATE"].ToString() != "")
                    {
                        model.DATAGENERATE_DATE = dt.Rows[n]["DATAGENERATE_DATE"].ToString();
                    }
                    if (dt.Rows[n]["SYS_CODE"] != null && dt.Rows[n]["SYS_CODE"].ToString() != "")
                    {
                        model.SYS_CODE = dt.Rows[n]["SYS_CODE"].ToString();
                    }
                    if (dt.Rows[n]["SYS_NAME"] != null && dt.Rows[n]["SYS_NAME"].ToString() != "")
                    {
                        model.SYS_NAME = dt.Rows[n]["SYS_NAME"].ToString();
                    }
                    if (dt.Rows[n]["ORG_CODE"] != null && dt.Rows[n]["ORG_CODE"].ToString() != "")
                    {
                        model.ORG_CODE = dt.Rows[n]["ORG_CODE"].ToString();
                    }
                    if (dt.Rows[n]["ORG_NAME"] != null && dt.Rows[n]["ORG_NAME"].ToString() != "")
                    {
                        model.ORG_NAME = dt.Rows[n]["ORG_NAME"].ToString();
                    }
                    if (dt.Rows[n]["TASK_TYPE"] != null && dt.Rows[n]["TASK_TYPE"].ToString() != "")
                    {
                        model.TASK_TYPE = dt.Rows[n]["TASK_TYPE"].ToString();
                    }
                    if (dt.Rows[n]["PERSON_NAME"] != null && dt.Rows[n]["PERSON_NAME"].ToString() != "")
                    {
                        model.PERSON_NAME = dt.Rows[n]["PERSON_NAME"].ToString();
                    }
                    if (dt.Rows[n]["CERT_TYPE"] != null && dt.Rows[n]["CERT_TYPE"].ToString() != "")
                    {
                        model.CERT_TYPE = dt.Rows[n]["CERT_TYPE"].ToString();
                    }
                    if (dt.Rows[n]["CERT_NAME"] != null && dt.Rows[n]["CERT_NAME"].ToString() != "")
                    {
                        model.CERT_NAME = dt.Rows[n]["CERT_NAME"].ToString();
                    }
                    if (dt.Rows[n]["CERT_NUMBER"] != null && dt.Rows[n]["CERT_NUMBER"].ToString() != "")
                    {
                        model.CERT_NUMBER = dt.Rows[n]["CERT_NUMBER"].ToString();
                    }
                    if (dt.Rows[n]["PERSON_TEL"] != null && dt.Rows[n]["PERSON_TEL"].ToString() != "")
                    {
                        model.PERSON_TEL = dt.Rows[n]["PERSON_TEL"].ToString();
                    }
                    if (dt.Rows[n]["TASK_TIME"] != null && dt.Rows[n]["TASK_TIME"].ToString() != "")
                    {
                        model.TASK_TIME = dt.Rows[n]["TASK_TIME"].ToString();
                    }
                    if (dt.Rows[n]["TASK_DESC"] != null && dt.Rows[n]["TASK_DESC"].ToString() != "")
                    {
                        model.TASK_DESC = dt.Rows[n]["TASK_DESC"].ToString();
                    }
                    if (dt.Rows[n]["DOCTOR_ID"] != null && dt.Rows[n]["DOCTOR_ID"].ToString() != "")
                    {
                        model.DOCTOR_ID = dt.Rows[n]["DOCTOR_ID"].ToString();
                    }
                    if (dt.Rows[n]["DOCTOR_NAME"] != null && dt.Rows[n]["DOCTOR_NAME"].ToString() != "")
                    {
                        model.DOCTOR_NAME = dt.Rows[n]["DOCTOR_NAME"].ToString();
                    }
                    if (dt.Rows[n]["BUS_RESULT_CODE"] != null && dt.Rows[n]["BUS_RESULT_CODE"].ToString() != "")
                    {
                        model.BUS_RESULT_CODE = dt.Rows[n]["BUS_RESULT_CODE"].ToString();
                    }
                    if (dt.Rows[n]["BUS_RESULT_DESC"] != null && dt.Rows[n]["BUS_RESULT_DESC"].ToString() != "")
                    {
                        model.BUS_RESULT_DESC = dt.Rows[n]["BUS_RESULT_DESC"].ToString();
                    }
                    if (dt.Rows[n]["UpLoadFlag"] != null && dt.Rows[n]["UpLoadFlag"].ToString() != "")
                    {
                        if ((dt.Rows[n]["UpLoadFlag"].ToString() == "1") || (dt.Rows[n]["UpLoadFlag"].ToString().ToLower() == "true"))
                        {
                            model.UpLoadFlag = true;
                        }
                        else
                        {
                            model.UpLoadFlag = false;
                        }
                    }
                    if (dt.Rows[n]["AddDateTime"] != null && dt.Rows[n]["AddDateTime"].ToString() != "")
                    {
                        model.AddDateTime = DateTime.Parse(dt.Rows[n]["AddDateTime"].ToString());
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
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
    }
}
