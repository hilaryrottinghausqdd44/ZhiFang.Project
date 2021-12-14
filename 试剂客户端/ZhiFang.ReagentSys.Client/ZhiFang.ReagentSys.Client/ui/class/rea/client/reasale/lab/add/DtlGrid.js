/**
 * 订货方供货管理
 * @author longfc
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.client.reasale.lab.add.DtlGrid', {
	extend: 'Shell.class.rea.client.reasale.basic.add.DtlGrid',
	title: '供货明细列表',

	/**后台排序*/
	remoteSort: false,
	
	/**供货单的供货商信息*/
	reaCompInfo: {
		"CompID": "",
		"CompanyName": "",
		"PlatformOrgNo": ""
	},
	/**用户UI配置Key*/
	userUIKey: 'reasale.lab.add.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "供货明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**@description 新增按钮点击处理方法*/
	onAddDtClick: function() {
		var me = this;
		if(me.reaCompInfo && me.reaCompInfo.CompID) {
			me.showDtGridCheck(me.reaCompInfo.CompID);
		} else {
			JShell.Msg.error("请选择供货商后再操作!");
		}
	},
	/**表单的供货商选择后联动供货明细列表*/
	setReaCompInfo: function(reaComp) {
		var me = this;
		me.reaCompInfo = reaComp;
	}
});