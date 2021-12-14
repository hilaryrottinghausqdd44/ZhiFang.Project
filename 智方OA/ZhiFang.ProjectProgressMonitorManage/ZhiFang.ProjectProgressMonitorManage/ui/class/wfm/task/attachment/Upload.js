/**
 * 任务附件列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.attachment.Upload',{
	extend:'Shell.ux.form.uploadPanel.UploadContainer',
	title:'任务附件',
	
	width:600,
	height:400,
	
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachmentByHQL?isPlanish=true',
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectAttachmentByField',
	//新增附件文件时保存服务
	addUrl: '/ProjectProgressMonitorManageService.svc/WM_UploadNewFiles',
	/**下载文件服务*/
	downLoadUrl: "/ProjectProgressMonitorManageService.svc/WM_UDTO_PProjectAttachmentDownLoadFiles",
	
	//新增文件所保存的数据对象名称
	objectEName: "PProjectAttachment",
	//外键字段(如:任务表--'PTask',工作任务日志表:'PWorkTaskLog',抄送关系表:'PTaskCopyFor')
	fkObjectName: 'PTask',
	
	/**默认加载数据*/
	defaultLoad: true,
	/**隐藏查看附件列*/
	hideShowColumn: false,
	
	/**任务ID*/
	TaskId:null,
	
	initComponent:function(){
		var me = this;
		me.fkObjectId = me.TaskId;
		me.callParent(arguments);
	}
});