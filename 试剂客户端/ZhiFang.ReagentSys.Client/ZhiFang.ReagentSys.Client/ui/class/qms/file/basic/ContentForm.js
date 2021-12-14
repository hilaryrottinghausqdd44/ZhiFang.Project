/**
 * 文档详细内容
 * @author longfc
 * @version 2016-06-23
 */
Ext.define('Shell.class.qms.file.basic.ContentForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '详细内容',

	/**获取数据服务路径*/
	selectUrl: '/CommonService.svc/QMS_UDTO_SearchFFileById?isPlanish=true&isAddFFileReadingLog=0&isAddFFileOperation=0',
	/**新增服务地址*/
	addUrl: '/CommonService.svc/QMS_UDTO_AddFFileAndFFileCopyUser',
	/**修改服务地址*/
	editUrl: '/CommonService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField',
	
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
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//解决在线编辑器换行出现滚动条后工具栏会被隐藏,需要手工调整高度,工具栏才不会被隐藏
		setTimeout(function() {
			me.setHeight(me.height-1);
		}, 100);
	},
	setHeight: function(height) {
		var me = this;
		//if(height) height = height < 120 ? 120 : height;
		return me.setSize(undefined, height);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//me.buttonToolbarItems = ['->', 'save', 'reset'];
		var width = document.body.clientWidth;
		var height = document.body.clientHeight*0.725;
		height=(height>395?height:395);
		
		items = [{
			name: 'FFile_Content',
			itemId: 'FFile_Content',
			//margin: '5px 0px 2px 0px',//上右下左
			xtype: 'ueditor',
			width:'100%',
			height:height,
			autoScroll: true,
			border: false
		}];
		return items;
	}
});