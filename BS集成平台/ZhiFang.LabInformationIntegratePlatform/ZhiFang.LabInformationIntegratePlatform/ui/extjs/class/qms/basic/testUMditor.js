/**
 * 文档内容
 * @author 
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.basic.testUMditor', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '文档内容',

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/QMS_UDTO_SearchFFileById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/QMS_UDTO_AddFFileAndFFileCopyUser',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField',
	layout: 'anchor',
	width: 1366,
	height: 480,
	formtype: "add",
	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	autoScroll: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//		me.bodyPadding = 0;
		me.selectUrl = me.selectUrl + "&isAddFFileReadingLog=0&isAddFFileOperation=0";
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		//创建可见组件
		me.createShowItems();
		//获取列表布局组件内容
		items = items.concat(me.getTableLayoutItems());
		return items;
	},
	/**创建可见组件*/
	createShowItems: function() {
		var me = this;
		me.FFile_Content = {
			name: 'FFile_Content',
			itemId: 'FFile_Content',
			fieldLabel: '文档内容',
			width: me.width-100,
			height: me.height,
			border: false,
			//id: "testueditor",
			xtype: 'ueditor',
			allowBlank: true
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this,
			items = [];
		//修订号
		me.FFile_Content.colspan = 1;
		items.push(me.FFile_Content);

		return items;

	}
});