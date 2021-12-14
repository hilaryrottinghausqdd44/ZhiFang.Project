using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.Entity.ReagentSys
{  
    #region BmsCenSaleDtlBarCode

	/// <summary>
	/// BmsCenSaleDtlBarCode object for NHibernate mapped table 'BmsCenSaleDtlBarCode'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "平台供货明细条码", ClassCName = "BmsCenSaleDtlBarCode", ShortCode = "BmsCenSaleDtlBarCode", Desc = "")]
	public class BmsCenSaleDtlBarCode : BaseEntity
	{
		#region Member Variables
		
        protected string _goodsSerial;
        protected string _lotSerial;
        protected string _packSerial;
        protected string _mixSerial;
		protected BmsCenSaleDtl _bmsCenSaleDtl;

		#endregion

		#region Constructors

		public BmsCenSaleDtlBarCode() { }

		public BmsCenSaleDtlBarCode( string goodsSerial, string lotSerial, string packSerial, string mixSerial, BmsCenSaleDtl bmsCenSaleDtl )
		{
			this._goodsSerial = goodsSerial;
			this._lotSerial = lotSerial;
			this._packSerial = packSerial;
			this._mixSerial = mixSerial;
			this._bmsCenSaleDtl = bmsCenSaleDtl;
		}

		#endregion

		#region Public Properties


        [DataMember]
        [DataDesc(CName = "产品条码", ShortCode = "GoodsSerial", Desc = "产品条码", ContextType = SysDic.All, Length = 100)]
        public virtual string GoodsSerial
		{
			get { return _goodsSerial; }
			set
			{
				_goodsSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "批号条码", ShortCode = "LotSerial", Desc = "批号条码", ContextType = SysDic.All, Length = 100)]
        public virtual string LotSerial
		{
			get { return _lotSerial; }
			set
			{
				_lotSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "包装单位条码", ShortCode = "PackSerial", Desc = "包装单位条码", ContextType = SysDic.All, Length = 100)]
        public virtual string PackSerial
		{
			get { return _packSerial; }
			set
			{
				_packSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = " 混合条码", ShortCode = "MixSerial", Desc = " 混合条码", ContextType = SysDic.All, Length = 100)]
        public virtual string MixSerial
		{
			get { return _mixSerial; }
			set
			{
				_mixSerial = value;
			}
		}

        [DataMember]
        [DataDesc(CName = "BmsCenSaleDtl", ShortCode = "BmsCenSaleDtl", Desc = "BmsCenSaleDtl")]
		public virtual BmsCenSaleDtl BmsCenSaleDtl
		{
			get { return _bmsCenSaleDtl; }
			set { _bmsCenSaleDtl = value; }
		}

        
		#endregion
	}
	#endregion
}