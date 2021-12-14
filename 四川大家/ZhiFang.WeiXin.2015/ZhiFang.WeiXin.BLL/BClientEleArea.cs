
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.Common.Log;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BClientEleArea : ZhiFang.BLL.Base.BaseBLL<ClientEleArea>, ZhiFang.WeiXin.IBLL.IBClientEleArea
    {
        IDCLIENTELEDao CLIENTELEDao { get;set;}
        public EntityList<ClientEleAreaVO> SearchClientEleAreaAndCLIENTELE(int id,string where, int page, int limit)
        {
            EntityList<ClientEleAreaVO> clientEleAreaVO = new EntityList<ClientEleAreaVO>();
            if(id > 0){
                where+=" and Id=" + id;
            }
            EntityList<ClientEleArea> clientEleAreas = DBDao.GetListByHQL(where, page, limit);
            List<ClientEleAreaVO> list = new List<ClientEleAreaVO>();
            if (clientEleAreas != null && clientEleAreas.count > 0)
            {
                foreach (var clientEleArea in clientEleAreas.list)
                {
                    ClientEleAreaVO vo = new ClientEleAreaVO();
                    vo = ClientEleAreaTOVO(clientEleArea);
                    IList<CLIENTELE> clientEle=  CLIENTELEDao.GetListByHQL("ClIENTNO =" + clientEleArea.ClientNo);
                    if(clientEle !=null && clientEle.Count > 0)
                    {
                        vo.clienteleName = clientEle[0].CNAME;
                        vo.clienteleId = clientEle[0].Id;
                    }
                    list.Add(vo);
                }
            }
            clientEleAreaVO.list = list;
            clientEleAreaVO.count = clientEleAreas.count;
            return clientEleAreaVO;
        }

        public EntityList<ClientEleAreaVO> SearchClientEleAreaAndCLIENTELE(string where, string order, int page, int limit)
        {
            EntityList<ClientEleAreaVO> clientEleAreaVO = new EntityList<ClientEleAreaVO>();
            EntityList<ClientEleArea> clientEleAreas = DBDao.GetListByHQL(where,order, page, limit);
            List<ClientEleAreaVO> list = new List<ClientEleAreaVO>();
            if (clientEleAreas != null && clientEleAreas.count > 0)
            {
                foreach (var clientEleArea in clientEleAreas.list)
                {
                    ClientEleAreaVO vo = new ClientEleAreaVO();
                    vo = ClientEleAreaTOVO(clientEleArea);
                    IList<CLIENTELE> clientEle = CLIENTELEDao.GetListByHQL("ClIENTNO =" + clientEleArea.ClientNo);
                    if (clientEle != null && clientEle.Count > 0)
                    {
                        vo.clienteleName = clientEle[0].CNAME;
                        vo.clienteleId = clientEle[0].Id;
                    }
                    list.Add(vo);
                }
            }
            clientEleAreaVO.list = list;
            return clientEleAreaVO;
        }

        ClientEleAreaVO ClientEleAreaTOVO(ClientEleArea clientEleArea)
        {
            ClientEleAreaVO vo = new ClientEleAreaVO();
            vo.Id = clientEleArea.Id;
            vo.AreaCName = clientEleArea.AreaCName;
            vo.AreaShortName = clientEleArea.AreaShortName;

            return vo;
        }
    }
}