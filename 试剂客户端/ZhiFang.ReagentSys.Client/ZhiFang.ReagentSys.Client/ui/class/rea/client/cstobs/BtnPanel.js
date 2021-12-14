/**
 * CS试剂客户端升级BS
 * @author longfc
 * @version 2018-10-17
 */
Ext.define('Shell.class.rea.client.cstobs.BtnPanel', {
	extend: 'Ext.tab.Panel',
	title: 'CS试剂客户端升级BS向导',
	width: 700,
	height: 480,
	autoScroll:true,
	layout: 'fit',
	/**内容周围距离*/
	bodyPadding: '1px',
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddCSUpdateToBSByStep',
	hasLoadMask: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.BtnForm.on({
			saveClick: function(cenorg) {
				me.fireEvent('saveClick', cenorg);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('saveClick');
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.BtnForm = Ext.create('Shell.class.rea.client.cstobs.BtnForm', {
			header: false,
			itemId: 'BtnForm',
			region: 'center',
		});
		return [me.BtnForm];
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	}
});