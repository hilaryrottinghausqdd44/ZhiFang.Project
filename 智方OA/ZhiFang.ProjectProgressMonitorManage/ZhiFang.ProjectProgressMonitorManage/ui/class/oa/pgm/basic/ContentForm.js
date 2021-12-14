/**
 * 程序发布详细说明内容
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.oa.pgm.basic.ContentForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '程序详细说明内容',

	/**获取数据服务路径*/
	selectUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramById?isPlanish=true&isUpdateCounts=false',
	/**新增服务地址*/
	addUrl: '/PDProgramManageService.svc/PGM_UDTO_AddPGMProgramByFormData',
	/**修改服务地址*/
	editUrl: '/PDProgramManageService.svc/PGM_UDTO_UpdatePGMProgramByFieldAndFormData',

	width: 1366,
	height: 400,
	formtype: "add",
	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**开启加载数据遮罩层*/
	hasLoadMask: false,
	autoScroll: false,
	layout: 'fit',
	bodyPadding: '0px 0px 0px 0px',
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		var width = document.body.clientWidth;
		var height = document.body.clientHeight * 0.725;
		height = (height > 395 ? height : 395);

		items = [{
			name: 'PGMProgram_Content',
			itemId: 'PGMProgram_Content',
			margin: '0px 0px 0px 0px',
			xtype: 'ueditor',
			width: '100%',
			height: height,
			autoScroll: true,
			border: false
		}];
		return items;
	}
});