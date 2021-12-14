/**
 * 仪器详细内容说明
 * @author longfc 
 * @version 2016-10-09
 */
Ext.define('Shell.class.sysbase.bequip.ContentForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],
	title: '仪器详细内容说明',

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBEquipById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddBEquip',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateBEquipByField',
	
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
		//me.buttonToolbarItems = ['->', 'save', 'reset'];
		var width = document.body.clientWidth;
		var height = document.body.clientHeight*0.725;
		height=(height>395?height:395);
		
		items = [{
			name: 'BEquip_Content',
			itemId: 'BEquip_Content',
			margin: '0px 0px 0px 0px',
			xtype: 'ueditor',
			width:'100%',
			height:height,
			autoScroll: true,
			border: false
		}];
		return items;
	}
});