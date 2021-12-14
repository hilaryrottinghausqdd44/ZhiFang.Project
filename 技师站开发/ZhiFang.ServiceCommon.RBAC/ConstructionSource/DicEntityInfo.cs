using System.Collections.Generic;

namespace ZhiFang.ServiceCommon.RBAC
{
    public class DicEntityInfo
    {
        public string CreatServiceAddress { get; set; }
        public string RetrieveServiceAddress { get; set; }
        public string UpdateServiceAddress { get; set; }
        public string DeleteServiceAddress { get; set; }
        public List<EntityFrameTree> EntityFrameTree { get; set; }

    }
}