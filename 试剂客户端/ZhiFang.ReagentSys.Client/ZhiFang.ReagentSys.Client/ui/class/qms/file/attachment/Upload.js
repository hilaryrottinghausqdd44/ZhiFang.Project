/**
 * QMS的文档附件列表
 * @author longfc
 * @version 2016-06-22
 */
Ext.define('Shell.class.qms.file.attachment.Upload', {
	extend: 'Shell.ux.form.uploadPanel.UploadContainer',
	title: '文档附件',

	width: 600,
	height: 400,
	selectUrl: '/CommonService.svc/QMS_UDTO_SearchFFileAttachmentByHQL?isPlanish=true',
	delUrl: '/CommonService.svc/QMS_UDTO_UpdateFFileAttachmentByField',
	//新增附件文件时保存服务
	addUrl: '/SingleTableService.svc/WM_UploadNewFiles',
	/**必须传--下载文件服务*/
	downLoadUrl: "/CommonService.svc/QMS_UDTO_FFileAttachmentDownLoadFiles",
	/**必须传--更新附件文件时保存服务*/
	editUrl: '/CommonService.svc/QMS_UDTO_UpdateFFileAttachmentByField',
	//新增文件所保存的数据对象名称
	objectEName: "FFileAttachment",
	//外键字段
	fkObjectName: 'FFile',
	/**默认加载数据*/
	defaultLoad: true,
	/**文档ID*/
	FFileId: null,
	operateType: 1,
	BusinessModuleCode: "FFile",
	/**
	 * 排序字段
	 * @exception 
	 * [{property:'FFileAttachment_DispOrder',direction:'ASC'}]
	 */
	defaultOrderBy: [{
		property: 'FFileAttachment_DispOrder',
		direction: 'ASC'
	}],
	initComponent: function() {
		var me = this;
		me.formtype = me.formtype || "add";
		me.FFileId = me.FFileId || "";
		me.fkObjectId = me.FFileId;
		me.fkObjectName = me.fkObjectName || "FFile";
		me.objectEName = me.objectEName || "FFileAttachment";
		me.defaultLoad = me.defaultLoad || true;
		me.callParent(arguments);
	}
});