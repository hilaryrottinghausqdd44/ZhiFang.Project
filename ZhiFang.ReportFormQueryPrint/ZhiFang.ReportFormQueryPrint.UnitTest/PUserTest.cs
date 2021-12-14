using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.ReportFormQueryPrint.UnitTest
{
   [TestClass]
    public class PUserTest
    {
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BPUser bpu = new BLL.BPUser();
        
        [TestMethod]
        public void CovertPassWordTest()
        {
            string result = bpu.CovertPassWord("");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void UnCovertPassWord()
        {
            string result = bpu.UnCovertPassWord("==qImDGS");
            Assert.AreEqual("", result);
        }
    }
}
