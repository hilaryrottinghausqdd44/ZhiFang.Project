/**
 * 任务内容
 * @author 
 * @version 2016-07-08
 */
Ext.define('Shell.class.wfm.task.basic.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '任务内容',
	width: 600,
	height: 600,
	
	autoScroll:true,
	
	/**获取数据服务路径*/
	selectTaskUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskById',
	/**获取附件服务路径*/
	selectAttachmentUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachmentByHQL',
	/**任务操作记录服务路径*/
	selectOperLogUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskOperLogByHQL',
	/**文件下载服务路径*/
    downloadUrl:'/ProjectProgressMonitorManageService.svc/WM_UDTO_PProjectAttachmentDownLoadFiles',
    /**获取任务类型人员服务*/
    selectPTaskTypeEmpLinkUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkByHQL',
	
	/**任务数据*/
	TaskData: '',
	/**任务类型人员数据*/
	TaskTypeEmpLinkData:'',
	/**附件数据*/
	AttachmentData: '',
	/**操作记录数据*/
	OperLogData:'',
	
	/**是否显示操作记录*/
	showOperLog:true,
	
	/**任务ID*/
	TaskId:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化Html页面
		me.initHtml();
	},
	
	initComponent: function() {
		var me = this;
		
		me.html = 
		'<div class="loading-div">' +
			'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
			'<div style="padding-top:10px;">页面加载中</div>' +
		'</div>';
		
		me.callParent(arguments);
	},
	
	initHtml:function(){
		var me = this;
		
		me.loadTaskData(function(data){
			me.TaskData = data;//任务数据
			me.loadTaskTypeEmpLinkData(function(data){
				me.TaskTypeEmpLinkData = data;//任务类型人员数据
				me.loadAttachmentData(function(data){
					me.AttachmentData = data;//附件数据
					
					if(me.showOperLog){
						me.loadOperLogData(function(data){
							me.OperLogData = data;//操作记录数据
							me.initHtmlContent();//初始化HTML页面
						});
					}else{
						me.initHtmlContent();//初始化HTML页面
					}
				});
			});
		});
	},
	/**初始化HTML页面*/
	initHtmlContent: function() {
		var me = this,
			html = [];
			
		//获取任务HTML
		html.push(me.getTaskHtml());
		//获取附件HTML
		html.push(me.getAttachmentHtml());
		
		if(me.showOperLog){
			//获取操作记录HTML
			html.push(me.getOperLogHtml());
		}
		
		//更新HTML内容
		me.update(html.join(''));
		//初始化下载监听
		me.initDownloadListeners();
	},
	
	/**加载任务信息*/
	loadTaskData:function(callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectTaskUrl);
			
		//主键、名称、内容、执行方式、执行地点、任务分类
		//任务主类别、任务父类别、任务类别、
		//紧急程度、任务状态、任务进度、客户、
		//申请人、一审人、二审人、指派人、执行人、检查人、
		//要求完成时间、计划开始时间、计划完成时间、预计工作量、实际开始时间、实际完成时间、实际工作量、
		
		var fields = [
			'Id','CName','Contents','ExecutTypeName','ExecutAddr','PClassName',
			'MTypeName','PTypeName','TypeName','PTypeID',
			'Urgency_Id','UrgencyName','Status_Id','StatusName','PaceName','PClient_Name',
			'ApplyName','OneAuditName','TwoAuditName','PublisherName','ExecutorName','CheckerName',
			'ReqEndTime','EstiStartTime','EstiEndTime','EstiWorkload','StartTime','EndTime','Workload'
		];
		fields = "PTask_" + fields.join(",PTask_");
		
		url += "?isPlanish=true&fields=" + fields + "&id=" + me.TaskId;
		
		//url += "&fields=PTask_Id,PTask_Contents&id=" + me.TaskId;
		
		JShell.Server.get(url, function(data) {
			callback(data);
		});
	},
	/**获取附件信息*/
	loadAttachmentData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);
			
		var fields = [
			'PProjectAttachment_Id', 'PProjectAttachment_FileName', 'PProjectAttachment_FileSize',
			'PProjectAttachment_CreatorName', 'PProjectAttachment_DataAddTime',
			'PProjectAttachment_NewFileName','PProjectAttachment_FileExt'
		];
		
		url += "?isPlanish=true&fields=" + fields.join(",");
		url += '&where=pprojectattachment.IsUse=1 and pprojectattachment.PTask.Id=' + me.TaskId;
		
		JShell.Server.get(url, function(data) {
			callback(data);
		});
	},
	/**获取操作记录信息*/
	loadOperLogData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectOperLogUrl);
			
		var fields = ['Id','PTaskOperTypeID','OperaterID','OperaterName','DataAddTime','OperateMemo'];
		url += '?isPlanish=true&fields=PTaskOperLog_' + fields.join(',PTaskOperLog_');
		url += '&where=ptaskoperlog.PTaskID=' + me.TaskId;
		url += '&sort=[{"property":"PTaskOperLog_DataAddTime","direction":"ASC"}]';
		
		JShell.Server.get(url, function(data) {
			callback(data);
		});
	},
	/**获取任务类型人员信息*/
	loadTaskTypeEmpLinkData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectPTaskTypeEmpLinkUrl);
			
		var fields = ['Id','TwoAudit','Publish','EmpName'];
		url += '?isPlanish=true&fields=PTaskTypeEmpLink_' + fields.join(',PTaskTypeEmpLink_');
		url += '&where=ptasktypeemplink.TaskTypeID=' + me.TaskData.value.PTask_PTypeID;
		
		JShell.Server.get(url, function(data) {
			callback(data);
		});
	},
	
	/**获取任务HTML*/
	getTaskHtml:function(){
		var me = this,
			data = me.TaskData,
			html = '';
			
		if(data.success){
			html = me.getTaskTemplet();
			if(data.value){
				html = html.replace(/{CName}/g,data.value.PTask_CName || '');
				html = html.replace(/{Content}/g,data.value.PTask_Contents || '');
				html = html.replace(/{ExecutTypeName}/g,data.value.PTask_ExecutTypeName || '');
				html = html.replace(/{ExecutAddr}/g,data.value.PTask_ExecutAddr || '');
				html = html.replace(/{PClassName}/g,data.value.PTask_PClassName || '');
				
				html = html.replace(/{MTypeName}/g,data.value.PTask_MTypeName || '');
				html = html.replace(/{PTypeName}/g,data.value.PTask_PTypeName || '');
				html = html.replace(/{TypeName}/g,data.value.PTask_TypeName || '');
				
				//任务紧急程度
				var UrgencyInfo = JShell.WFM.GUID.getInfoByGUID('TaskUrgency',data.value.PTask_Urgency_Id);
				var TaskUrgencyText = '';
				if(UrgencyInfo){
					var UrgencyStyle = [];
					if(UrgencyInfo.color){
						UrgencyStyle.push('color:' + UrgencyInfo.color);
					}
					if(UrgencyInfo.bgcolor){
						UrgencyStyle.push('background-color:' + UrgencyInfo.bgcolor);
						UrgencyStyle.push('border:1px solid ' + UrgencyInfo.bgcolor);
					}
					TaskUrgencyText = 
						'<div style="float:left;padding:5px;' + 
							UrgencyStyle.join(';') + '"><b>' + UrgencyInfo.text + '</b>' +
						'</div>';
				}
				html = html.replace(/{UrgencyName}/g,TaskUrgencyText || '');
				
				//任务状态
				var StatusInfo = JShell.WFM.GUID.getInfoByGUID('TaskStatus',data.value.PTask_Status_Id);
				var StatusText = '';
				if(StatusInfo){
					var StatusStyle = [];
					StatusStyle.push('color:' + (StatusInfo.bgcolor || '#000000'));
					StatusStyle.push('border:1px solid ' + (StatusInfo.bgcolor || '#000000'));
					StatusText = 
						'<div style="float:left;padding:5px;' + 
							StatusStyle.join(';') + '"><b>' + StatusInfo.text + '</b>' +
						'</div>';
				}
				html = html.replace(/{StatusName}/g,StatusText || '');
				
				html = html.replace(/{PaceName}/g,data.value.PTask_PaceName || '');
				html = html.replace(/{PClient_Name}/g,data.value.PTask_PClient_Name || '');
				
				html = html.replace(/{ApplyName}/g,data.value.PTask_ApplyName || '');
				html = html.replace(/{OneAuditName}/g,data.value.PTask_OneAuditName || '');
				html = html.replace(/{TwoAuditName}/g,data.value.PTask_TwoAuditName || '');
				html = html.replace(/{PublisherName}/g,data.value.PTask_PublisherName || '');
				html = html.replace(/{ExecutorName}/g,data.value.PTask_ExecutorName || '');
				html = html.replace(/{CheckerName}/g,data.value.PTask_CheckerName || '');
				
				html = html.replace(/{ReqEndTime}/g,(JShell.Date.toString(data.value.PTask_ReqEndTime,true) || ''));
				
				html = html.replace(/{EstiStartTime}/g,JShell.Date.toString(data.value.PTask_EstiStartTime,true) || '');
				html = html.replace(/{EstiEndTime}/g,JShell.Date.toString(data.value.PTask_EstiEndTime,true) || '');
				html = html.replace(/{EstiWorkload}/g,data.value.PTask_EstiWorkload || '');
				html = html.replace(/{StartTime}/g,JShell.Date.toString(data.value.PTask_StartTime,true) || '');
				html = html.replace(/{EndTime}/g,JShell.Date.toString(data.value.PTask_EndTime,true) || '');
				html = html.replace(/{Workload}/g,data.value.PTask_Workload || '');
				
				//可二审人员和可分配人员
				var TaskTypeEmpLinkTwoAuditName = [],
					TaskTypeEmpLinkPublisherName = [];
				
				if(me.TaskTypeEmpLinkData){
					var list = (me.TaskTypeEmpLinkData.value || {}).list || [];
					for(var i in list){
						if(list[i].PTaskTypeEmpLink_TwoAudit + "" == "true"){
							TaskTypeEmpLinkTwoAuditName.push(list[i].PTaskTypeEmpLink_EmpName);
						}
						if(list[i].PTaskTypeEmpLink_Publish + "" == "true"){
							TaskTypeEmpLinkPublisherName.push(list[i].PTaskTypeEmpLink_EmpName);
						}
					}
				}else{
					TaskTypeEmpLinkTwoAuditName = ['无'];
					TaskTypeEmpLinkPublisherName = ['无'];
				}
				html = html.replace(/{TaskTypeEmpLinkTwoAuditName}/g,TaskTypeEmpLinkTwoAuditName.join(" , "));
				html = html.replace(/{TaskTypeEmpLinkPublisherName}/g,TaskTypeEmpLinkPublisherName.join(" , "));
			}
		}else{
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'任务内容');
			html = html.replace(/{Error}/g,data.msg);
		}
		
		return html;
	},
	/**获取任务HTML模板*/
	getTaskTemplet: function() {
		//return this.getContentTemplet('任务内容','{Content}');
		
		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		//var sDivStyle = 'padding:5px;border:1px solid #000000;float:left;margin:0 10px 10px 0;';
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';
		
		var templet =
		//任务标题
		'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{CName}</h4>' +
		'</div>' +
		//紧急程度、状态、要求完成时间、类别
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '"><b>{MTypeName} - {PTypeName} - {TypeName}</b></div>' +
				'{UrgencyName}' +
				'<div style="' + sDivStyle + 'color:#d6204b;"><b>要求完成时间：{ReqEndTime}</b></div>' +
				'{StatusName}' +
			'</div>' +
		'</div>' +
		//客户、执行方式+地点
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '"><b>客户：{PClient_Name}</b></div>' +
				'<div style="' + sDivStyle + '">执行方式：{ExecutTypeName}</div>' +
				'<div style="' + sDivStyle + '">执行地点：{ExecutAddr}</div>' +
				 // 添加任务分类 @author liangyl @version 2017-07-13
				'<div style="' + sDivStyle + '">任务分类：{PClassName}</div>' +
				
			'</div>' +
		'</div>' +
		//人员
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">申请人：{ApplyName}</div>' +
				'<div style="' + sDivStyle + '">一审人：{OneAuditName}</div>' +
				'<div style="' + sDivStyle + '">二审人：{TwoAuditName}</div>' +
				'<div style="' + sDivStyle + '">分配人：{PublisherName}</div>' +
				'<div style="' + sDivStyle + '">执行人：{ExecutorName}</div>' +
				'<div style="' + sDivStyle + '">验收人：{CheckerName}</div>' +
				'<div style="' + sDivStyle + 'border:1px solid #7dc5eb;color:#7dc5eb;margin-bottom:2px;border-radius:2px;">可二审人员：{TaskTypeEmpLinkTwoAuditName}</div>' +
				'<div style="' + sDivStyle + 'border:1px solid #be8dbd;color:#be8dbd;margin-bottom:2px;border-radius:2px;">可分配人员：{TaskTypeEmpLinkPublisherName}</div>' +
			'</div>' +
		'</div>' +
		//计划
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + 'border-color:#e0620d;color:#e0620d;">计划开始时间：{EstiStartTime}</div>' +
				'<div style="' + sDivStyle + 'border-color:#e0620d;color:#e0620d;">计划完成时间：{EstiEndTime}</div>' +
				'<div style="' + sDivStyle + 'border-color:#e0620d;color:#e0620d;">预计工作量：{EstiWorkload}</div>' +
	//		'</div>' +
	//		//实际
	//		'<div class="col-sm-12">' +
				'<div style="' + sDivStyle + 'border-color:#2aa515;color:#2aa515;">实际开始时间：{StartTime}</div>' +
				'<div style="' + sDivStyle + 'border-color:#2aa515;color:#2aa515;">实际完成时间：{EndTime}</div>' +
				'<div style="' + sDivStyle + 'border-color:#2aa515;color:#2aa515;">实际工作量：{Workload}</div>' +
			'</div>' +
		'</div>' +
		//进度
		'<div class="col-sm-12" style="text-align:center;margin-top:10px;">' +
			'<div class="progress progress-striped">' +
			    '<div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" ' +
			        'aria-valuemin="0" aria-valuemax="100" style="width:{PaceName};">' +
			        '<span>{PaceName} 完成</span>' +
			    '</div>' +
			'</div>' +
		'</div>' +
		//任务内容
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">任务内容</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{Content}</p>' +
		'</div>';
		return templet;
	},
	
	/**获取附件HTML*/
	getAttachmentHtml: function() {
		var me = this,
			data = me.AttachmentData,
			html = '';
			
		if(data.success) {
			var list = (data.value || {}).list || [],
				len = list.length,
				attArr = [],
				html= '';
				
			for(var i = 0; i < len; i++) {
				attArr.push(me.getOneAttachmentHtml(list[i]));
			}
			html = me.getAttachmentTemplet();
			html = html.replace(/{AttachmentList}/g, (attArr.join("") || '无'));
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'附件');
			html = html.replace(/{Error}/g,data.msg);
		}
		return html;
	},
	/**获取一条附件信息HTML*/
	getOneAttachmentHtml:function(data){
		var me = this,
			temp = me.getOneAttachmentTemplet(),
			html = '';
		
		var Title = data.PProjectAttachment_CreatorName + ' 创建于 ' +
			JShell.Date.toString(data.PProjectAttachment_DataAddTime);
		var Name = data.PProjectAttachment_NewFileName + data.PProjectAttachment_FileExt;
			
		temp = temp.replace(/{Id}/g, data.PProjectAttachment_Id);
		temp = temp.replace(/{Title}/g, Title);
		temp = temp.replace(/{FileName}/g, Name);
		temp = temp.replace(/{FileSize}/g, JShell.Bytes.toSize(data.PProjectAttachment_FileSize));
		
		html = temp;
		
		return html;
	},
	
	/**获取附件模板*/
	getAttachmentTemplet: function() {
		return this.getContentTemplet('附件信息','{AttachmentList}');
	},
	/**获取一个附件模板*/
	getOneAttachmentTemplet: function() {
		var templet =
		'<div style="padding:5px;">' +
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" title="{Title}">{FileName}</a> ' +
			'<span style="color:green;">({FileSize})</span>' +
		'</div>';
		return templet;
	},
	
	/**获取操作记录HTML*/
	getOperLogHtml: function() {
		var me = this,
			data = me.OperLogData,
			html = '';
			
		if(data.success) {
			var list = (data.value || {}).list || [],
				len = list.length,
				attArr = [],
				html= '';
				
			for(var i = 0; i < len; i++) {
				attArr.push(me.getOneOperLogHtml(list[i]));
			}
			html = me.getOperLogTemplet();
			html = html.replace(/{OperLogList}/g, (attArr.join("") || '无'));
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'操作记录');
			html = html.replace(/{Error}/g,data.msg);
		}
		return html;
	},
	/**获取一条操作记录信息HTML*/
	getOneOperLogHtml:function(data){
		var me = this,
			temp = me.getOneOperLogTemplet(),
			html = '';
		
		var OperTypeInfo = JShell.WFM.GUID.getInfoByGUID('TaskStatus',data.PTaskOperLog_PTaskOperTypeID);
		var OperTypeName = OperTypeInfo ? OperTypeInfo.text : '';
		
		var style = [];
		if(OperTypeInfo.bgcolor){style.push('color:' + OperTypeInfo.bgcolor);}
		OperTypeName = '<b style="' + style.join(';') + '">' + OperTypeName + '</b> ';
		
		if(data.PTaskOperLog_OperateMemo){
			OperTypeName += '处理意见：<b>' + data.PTaskOperLog_OperateMemo + '</b>';
		}
			
		temp = temp.replace(/{DataAddTime}/g, JShell.Date.toString(data.PTaskOperLog_DataAddTime));
		temp = temp.replace(/{OperaterName}/g, data.PTaskOperLog_OperaterName);
		temp = temp.replace(/{OperTypeName}/g, OperTypeName);
		
		html = temp;
		
		return html;
	},
	
	/**获取操作记录模板*/
	getOperLogTemplet: function() {
		return this.getContentTemplet('操作记录','{OperLogList}');
	},
	/**获取一个操作记录模板*/
	getOneOperLogTemplet: function() {
		var templet =
		'<div style="padding:5px;">' +
			'{DataAddTime} {OperaterName} {OperTypeName}' +
		'</div>';
		return templet;
	},
	
	/**获取内容模板*/
	getContentTemplet:function(title,name){
		var templet =
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">' + title + '</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">' + name + '</p>' +
		'</div>';
		return templet;
	},
	/**获取错误信息HTML模板*/
	getErrorTemplet:function(){
		var templet = 
		'<div class="col-sm-12">' +
			'<h4>{Title}-错误信息</h4>' +
			'<p style="color:red;padding:5px;word-break:break-all;word-wrap:break-word;">{Error}</p>' +
		'</div>';
		return templet;
	},
	
	/**初始化下载监听*/
	initDownloadListeners:function(){
		var me = this,
			DomArray = Ext.query("[filedownload]"),
			len = DomArray.length;
			
		for(var i=0;i<len;i++){
			DomArray[i].onclick = function() {
				var id = this.getAttribute("data");
				me.onDwonload(id);
			};
		}
	},
	/**点击下载文件*/
	onDwonload:function(id){
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=1&id=' + id;
		window.open(url);
	}
});