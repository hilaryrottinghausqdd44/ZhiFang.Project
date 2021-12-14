using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ZhiFang.LabInformationIntegratePlatform.MessageHub
{
    public class TestHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}