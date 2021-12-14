using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BSiteOperationRecords
    {
        private readonly IDSiteOperationRecords dal = DalFactory<IDSiteOperationRecords>.GetDal("SiteOperationRecords");
        public int Add(Model.SiteOperationRecords t)
        {
            return dal.Add(t);
        }
        public int addSiteOperationRecord(string serviceName,string ip,string hostName)
        {
            if (string.IsNullOrWhiteSpace(ip) )
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                ip = endpoint.Address;
                
            }
            if (string.IsNullOrWhiteSpace(hostName))
            {
                hostName = Dns.GetHostEntry(ip).HostName;
            }
            
            Model.SiteOperationRecords siteOperationRecords = new Model.SiteOperationRecords(ip,hostName,serviceName);
            return dal.Add(siteOperationRecords);
        }

        public bool isExist(string SiteHostName,string SiteIP)
        {
            if (string.IsNullOrWhiteSpace(SiteIP))
            {
                OperationContext context = OperationContext.Current;
                MessageProperties properties = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                SiteIP = endpoint.Address;

            }
            if (string.IsNullOrWhiteSpace(SiteHostName))
            {
                SiteHostName = Dns.GetHostEntry(SiteIP).HostName;
            }
            return dal.Exists(SiteHostName, SiteIP);
        }
    }
}
