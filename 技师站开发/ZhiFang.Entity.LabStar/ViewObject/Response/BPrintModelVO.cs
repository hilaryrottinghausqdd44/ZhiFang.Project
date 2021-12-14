using System.Runtime.Serialization;

namespace ZhiFang.Entity.LabStar.ViewObject.Response
{
    [DataContract]
    public class BPrintModelVO : BPrintModel
    {
        protected bool _IsFile = false;
        [DataMember]
        public bool IsFile { get { return _IsFile; } set { _IsFile = value; } } //记录物理文件是否存在
    }
}
