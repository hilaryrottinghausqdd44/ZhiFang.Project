/**
 * 公共附件列表
 * @author longfc
 * @version 2016-10-21
 */
Ext.define('Shell.class.sysbase.scattachment.SCAttachment', {
	extend: 'Shell.ux.form.uploadPanel.UploadContainer',
	title: '公共附件列表',

	width: 600,
	height: 400,
	selectUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCAttachmentByHQL?isPlanish=true',
	delUrl: '/SystemCommonService.svc/SC_UDTO_UpdateSCAttachmentByField',
	//新增附件文件时保存服务
	addUrl: '/SystemCommonService.svc/SC_UploadAddSCAttachment',
	/**必须传--下载文件服务*/
	downLoadUrl: "/SystemCommonService.svc/SC_UDTO_DownLoadSCAttachment",
	/**必须传--更新附件文件时保存服务*/
	editUrl: '/SystemCommonService.svc/SC_UDTO_UpdateSCAttachmentByField',
	//新增文件所保存的数据对象名称
	objectEName: "SCAttachment",
	//外键字段
	fkObjectName: 'BobjectID',
	/*附件列表类型(ObjId:按附件实体的业务对象名称处理;ObjName:按附件外键属性名称处理*/
	SearchType:"ObjId",
	/**默认加载数据*/
	defaultLoad: true,
	/**附件ID*/
	PK: null,
	/*附件操作类型:0:下载;1:是直接打开*/
	operateType: 0,
	/*公共附件分类保存文件夹名称*/
	SaveCategory:"",
	BusinessModuleCode:"",
	initComponent: function() {
		var me = this;
		me.formtype = me.formtype || "add";
		me.PK = me.PK || "";
		me.BusinessModuleCode=me.BusinessModuleCode||"";
		me.fkObjectId = me.PK;
		me.fkObjectName = me.fkObjectName || "BobjectID";
		me.objectEName = me.objectEName || "SCAttachment";
		me.SearchType||"ObjId";
		me.SaveCategory=me.SaveCategory||"";
		me.callParent(arguments);
	}
});