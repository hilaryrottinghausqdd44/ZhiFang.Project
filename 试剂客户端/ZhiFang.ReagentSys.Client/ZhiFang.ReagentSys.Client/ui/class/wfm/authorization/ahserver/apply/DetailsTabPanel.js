/**
 * 服务器授权申请明细
 * @author longfc	
 * @version 2016-12-20
 */
Ext.define('Shell.class.wfm.authorization.ahserver.apply.DetailsTabPanel', {
	extend: 'Ext.tab.Panel',
	title: '服务器授权申请明细',
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**上传的授权申请文件的主要信息*/
	ApplyAHServerLicence: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.ProgramLicenceGrid = Ext.create('Shell.class.wfm.authorization.ahserver.apply.ProgramLicenceGrid', {
			title: '主程序授权明细',
			PK: me.PK,
			header: false,
			border: false,
			/**带功能按钮栏*/
			hasButtontoolbar: false,
			itemId: 'ProgramLicenceGrid'
		});
		me.EquipLicenceGrid = Ext.create('Shell.class.wfm.authorization.ahserver.apply.EquipLicenceGrid', {
			title: '仪器授权明细',
			PK: me.PK,
			header: false,
			border: false,
			itemId: 'EquipLicenceGrid'
		});
		return [me.ProgramLicenceGrid, me.EquipLicenceGrid];
	},
	/**
	 * 隐藏 tab
	 * @param tabPanel
	 * @param tab
	 * @returns {boolean}
	 */
	hideTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.hide();
		tab.tab.hide();
	},
	/**
	 * 显示 tab
	 * @param tabPanel
	 * @param tab
	 */
	showTab: function(index) {
		var me = this;
		var tab = me.items.getAt(index);
		tab.show();
		tab.tab.show();
		me.setActiveTab(tab);
	}
});