using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PWorkMonthLog

	/// <summary>
	/// PWorkMonthLog object for NHibernate mapped table 'P_WorkMonthLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "工作月总结表", ClassCName = "PWorkMonthLog", ShortCode = "PWorkMonthLog", Desc = "工作月总结表")]
	public class PWorkMonthLog : PWorkLogBase
    {

    }
    #endregion
}