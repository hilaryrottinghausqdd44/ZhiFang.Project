/**
 * 供货管理
 * @author longfc
 * @version 2018-04-26
 */
Ext.define('Shell.class.rea.client.reasale.comp.add.DtlGrid', {
	extend: 'Shell.class.rea.client.reasale.basic.add.DtlGrid',
	title: '供货明细列表',

	/**后台排序*/
	remoteSort: false,
	/**用户UI配置Key*/
	userUIKey: 'reasale.comp.add.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "供货明细列表",
	/**供货单的订货方信息*/
	reaLabInfo: {
		"ReaLabID": "",
		"ReaLabCName": "",
		"PlatformOrgNo": ""
	},
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
		if(me.reaLabInfo && me.reaLabInfo.ReaLabID) {
			me.showDtGridCheck(me.reaLabInfo.ReaLabID);
		} else {
			JShell.Msg.error("请选择订货方后再操作!");
		}
	},
	/**表单的订货方选择后联动供货明细列表*/
	setReaLabInfo: function(reaLab) {
		var me = this;
		me.reaLabInfo = reaLab;
	}
});