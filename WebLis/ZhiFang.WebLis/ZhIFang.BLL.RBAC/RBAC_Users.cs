using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Model;

namespace ZhiFang.BLL.RBAC
{
    //RBAC_Users
    public partial class RBAC_Users
    {

        private readonly ZhiFang.DAL.RBAC.RBAC_Users dal = new DAL.RBAC.RBAC_Users();
        public RBAC_Users()
        { }       

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ZhiFang.Model.RBAC.Entity.RBAC_Users model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.RBAC.Entity.RBAC_Users model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.RBAC.Entity.RBAC_Users GetModel(int ID)
        {

            return dal.GetModel(ID);
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
        public List<Model.RBAC.Entity.RBAC_Users> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.RBAC.Entity.RBAC_Users> DataTableToList(DataTable dt)
        {
            List<Model.RBAC.Entity.RBAC_Users> modelList = new List<Model.RBAC.Entity.RBAC_Users>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.RBAC.Entity.RBAC_Users model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.RBAC.Entity.RBAC_Users();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.Account = dt.Rows[n]["Account"].ToString();
                    model.Password = dt.Rows[n]["Password"].ToString();
                    if (dt.Rows[n]["EmpID"].ToString() != "")
                    {
                        model.EmpID = int.Parse(dt.Rows[n]["EmpID"].ToString());
                    }
                    model.UserDesc = dt.Rows[n]["UserDesc"].ToString();
                    if (dt.Rows[n]["EnMPwd"].ToString() != "")
                    {
                        if ((dt.Rows[n]["EnMPwd"].ToString() == "1") || (dt.Rows[n]["EnMPwd"].ToString().ToLower() == "true"))
                        {
                            model.EnMPwd = true;
                        }
                        else
                        {
                            model.EnMPwd = false;
                        }
                    }
                    if (dt.Rows[n]["PwdExprd"].ToString() != "")
                    {
                        if ((dt.Rows[n]["PwdExprd"].ToString() == "1") || (dt.Rows[n]["PwdExprd"].ToString().ToLower() == "true"))
                        {
                            model.PwdExprd = true;
                        }
                        else
                        {
                            model.PwdExprd = false;
                        }
                    }
                    if (dt.Rows[n]["AccExprd"].ToString() != "")
                    {
                        if ((dt.Rows[n]["AccExprd"].ToString() == "1") || (dt.Rows[n]["AccExprd"].ToString().ToLower() == "true"))
                        {
                            model.AccExprd = true;
                        }
                        else
                        {
                            model.AccExprd = false;
                        }
                    }
                    if (dt.Rows[n]["AccLock"].ToString() != "")
                    {
                        if ((dt.Rows[n]["AccLock"].ToString() == "1") || (dt.Rows[n]["AccLock"].ToString().ToLower() == "true"))
                        {
                            model.AccLock = true;
                        }
                        else
                        {
                            model.AccLock = false;
                        }
                    }
                    if (dt.Rows[n]["LockedPeriod"].ToString() != "")
                    {
                        model.LockedPeriod = int.Parse(dt.Rows[n]["LockedPeriod"].ToString());
                    }
                    if (dt.Rows[n]["AuUnlock"].ToString() != "")
                    {
                        model.AuUnlock = int.Parse(dt.Rows[n]["AuUnlock"].ToString());
                    }
                    if (dt.Rows[n]["AccLockDt"].ToString() != "")
                    {
                        model.AccLockDt = DateTime.Parse(dt.Rows[n]["AccLockDt"].ToString());
                    }
                    if (dt.Rows[n]["LoginTm"].ToString() != "")
                    {
                        model.LoginTm = DateTime.Parse(dt.Rows[n]["LoginTm"].ToString());
                    }
                    if (dt.Rows[n]["AccExpTm"].ToString() != "")
                    {
                        model.AccExpTm = DateTime.Parse(dt.Rows[n]["AccExpTm"].ToString());
                    }
                    if (dt.Rows[n]["AccCreateTime"].ToString() != "")
                    {
                        model.AccCreateTime = DateTime.Parse(dt.Rows[n]["AccCreateTime"].ToString());
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

        public BaseResultDataValue SetEmpPWD(string empId, string pwd)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                Model.RBAC.Entity.RBAC_Users users = new Model.RBAC.Entity.RBAC_Users();
                users.EmpID = int.Parse(empId);
                users.Password = ZhiFang.Tools.MD5Helper.StringToMD5Hash(pwd.Trim());
                if (dal.SetEmpPWDByEmpID(users.EmpID, users.Password) <=0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "设置密码失败！";
                    return brdv;
                }
                brdv.success = true;
                brdv.ErrorInfo = "设置密码成功！";
                return brdv;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
    }
}