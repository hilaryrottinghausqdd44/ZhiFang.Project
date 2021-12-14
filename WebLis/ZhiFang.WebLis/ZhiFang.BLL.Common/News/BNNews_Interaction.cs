using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model;

namespace ZhiFang.BLL.Common.News
{
    //文档交流表（不附带附件）
    public class BNNews_Interaction
    {

        private readonly ZhiFang.IDAL.IDNNews_Interaction dal = DalFactory<IDNNews_Interaction>.GetDalByClassName("DNNews_Interaction");
        public BNNews_Interaction()
        { }

        #region  Method
        

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.N_News_Interaction model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.N_News_Interaction model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long InteractionID)
        {

            return dal.Delete(InteractionID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.N_News_Interaction GetModel(long InteractionID)
        {

            return dal.GetModel(InteractionID);
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
        public List<Model.N_News_Interaction> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.N_News_Interaction> DataTableToList(DataTable dt)
        {
            List<Model.N_News_Interaction> modelList = new List<Model.N_News_Interaction>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.N_News_Interaction model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.N_News_Interaction();
                    if (dt.Rows[n]["LabID"].ToString() != "")
                    {
                        model.LabID = long.Parse(dt.Rows[n]["LabID"].ToString());
                    }
                    if (dt.Rows[n]["InteractionID"].ToString() != "")
                    {
                        model.InteractionID = long.Parse(dt.Rows[n]["InteractionID"].ToString());
                    }
                    if (dt.Rows[n]["FileID"].ToString() != "")
                    {
                        model.FileID = long.Parse(dt.Rows[n]["FileID"].ToString());
                    }
                    model.Contents = dt.Rows[n]["Contents"].ToString();
                    if (dt.Rows[n]["SenderID"].ToString() != "")
                    {
                        model.SenderID = long.Parse(dt.Rows[n]["SenderID"].ToString());
                    }
                    model.SenderName = dt.Rows[n]["SenderName"].ToString();
                    if (dt.Rows[n]["ReceiverID"].ToString() != "")
                    {
                        model.ReceiverID = long.Parse(dt.Rows[n]["ReceiverID"].ToString());
                    }
                    model.ReceiverName = dt.Rows[n]["ReceiverName"].ToString();
                    if (dt.Rows[n]["HasAttachment"].ToString() != "")
                    {
                        if ((dt.Rows[n]["HasAttachment"].ToString() == "1") || (dt.Rows[n]["HasAttachment"].ToString().ToLower() == "true"))
                        {
                            model.HasAttachment = true;
                        }
                        else
                        {
                            model.HasAttachment = false;
                        }
                    }
                    model.Memo = dt.Rows[n]["Memo"].ToString();
                    if (dt.Rows[n]["IsUse"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsUse"].ToString() == "1") || (dt.Rows[n]["IsUse"].ToString().ToLower() == "true"))
                        {
                            model.IsUse = true;
                        }
                        else
                        {
                            model.IsUse = false;
                        }
                    }
                    if (dt.Rows[n]["DataAddTime"].ToString() != "")
                    {
                        model.DataAddTime = DateTime.Parse(dt.Rows[n]["DataAddTime"].ToString());
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

        public EntityListEasyUI<N_News_Interaction> GetModelList(string where, string sort, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public EntityListEasyUI<N_News_Interaction> GetModelList(string where, int page, int limit)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}