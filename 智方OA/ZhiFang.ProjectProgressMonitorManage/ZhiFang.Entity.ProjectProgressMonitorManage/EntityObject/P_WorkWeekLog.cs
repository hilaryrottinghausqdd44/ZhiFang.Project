using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZhiFang.Entity.Base;

namespace ZhiFang.Entity.ProjectProgressMonitorManage
{
	#region PWorkWeekLog

	/// <summary>
	/// PWorkWeekLog object for NHibernate mapped table 'P_WorkWeekLog'.
	/// </summary>
    [DataContract]
    [DataDesc(CName = "工作周计划表", ClassCName = "PWorkWeekLog", ShortCode = "PWorkWeekLog", Desc = "工作周计划表")]
	public class PWorkWeekLog : PWorkLogBase
    {
		
    }
    #endregion
}