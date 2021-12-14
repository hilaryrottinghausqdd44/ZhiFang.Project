
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
	public class SCRecordPhraseDaoTest
	{
		static long longGUID = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
		Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
        IDSCRecordPhraseDao SCRecordPhraseDao;
        public SCRecordPhraseDaoTest()
        {
            SCRecordPhraseDao = context.GetObject("SCRecordPhraseDao") as IDSCRecordPhraseDao;
        }
        
        [TestMethod]
        public void TestSave()
        {
        	SCRecordPhrase entity = new SCRecordPhrase();
        	entity.Id = longGUID; 
entity.CName = "测试"; 
entity.SName = "测试"; 
entity.ShortCode = "测试"; 
entity.PinYinZiTou = "测试"; 
entity.IsUse = true; 
entity.Memo = "测试"; 
entity.DispOrder = 1; 
			bool b = SCRecordPhraseDao.Save(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestUpdate()
        {
        	SCRecordPhrase entity = SCRecordPhraseDao.Get(longGUID);
        	
        	bool b = SCRecordPhraseDao.Update(entity);
            Assert.AreEqual(true, b);
        }
        
        [TestMethod]
        public void TestGet()
        {
        	SCRecordPhrase entity = SCRecordPhraseDao.Get(longGUID);
            Assert.AreEqual(entity.Id, longGUID);
        }
        
        [TestMethod]
        public void TestDel()
        {
        	bool b = SCRecordPhraseDao.Delete(longGUID);
            Assert.AreEqual(true, b);
        }
        
	} 
}