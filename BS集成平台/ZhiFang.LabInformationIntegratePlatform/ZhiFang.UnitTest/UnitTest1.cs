using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZhiFang.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ZhiFang.DAO.MSSQL.LabStarV6.DoctorDao doctorDao = new DAO.MSSQL.LabStarV6.DoctorDao();
            
            Assert.IsTrue(doctorDao.GetAllList().Tables.Count>0);
        }
    }
}
