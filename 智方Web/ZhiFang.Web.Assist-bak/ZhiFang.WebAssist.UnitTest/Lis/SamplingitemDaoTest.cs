
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.WebAssist.UnitTest
{	
	[TestClass]
	public class SamplingitemDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSamplingitemDao SamplingitemDao;
        public SamplingitemDaoTest()
        {
            SamplingitemDao = context.GetObject("SamplingitemDao") as IDSamplingitemDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	Samplingitem entity = new Samplingitem();
        	entity.Id = longGUID; 
entity.Id = longGUID; 
entity.DispOrder = 1; 
entity.IsDefault = 1; 
entity.MinItemCount = 1; 
entity.MustItem = 1; 
entity.VirtualItemNo = 1; 
			bool b = SamplingitemDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	Samplingitem entity = SamplingitemDao.Get(longGUID);
        	
        	bool b = SamplingitemDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	Samplingitem entity = SamplingitemDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = SamplingitemDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}