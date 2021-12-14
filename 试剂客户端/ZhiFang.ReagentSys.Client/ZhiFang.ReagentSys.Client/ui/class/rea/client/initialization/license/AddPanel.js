/**
 * 初始化新增
 * @author liangyl
 * @version 2018-03-12
 */
Ext.define('Shell.class.rea.client.initialization.license.AddPanel', {
	extend: 'Ext.tab.Panel',
	
	title: '初始化新增',
	width: 700,
	height: 480,
	autoScroll: false,
	layout: 'fit',
	/**内容周围距离*/
	bodyPadding: '1px',
	
	/**新增出库单并更新库存*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddGoodsReaBmsTransferDoc',
	
	hasLoadMask: true,
	OrgObject: {},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Form.on({
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
		me.Form = Ext.create('Shell.class.rea.client.initialization.license.BtnForm', {
			header: false,
			itemId: 'Form',
			region: 'center',
		});
		me.GuidePanel = Ext.create('Shell.class.rea.client.initialization.license.GuidePanel', {
			region: 'center',
			header: false,
			formtype: "add",
			disabled: true,
			defaultLoad: false,
			itemId: 'DtlGrid',
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		return [me.Form, me.GuidePanel];
	},
	loadData: function(cenorg) {
		var me = this;

		me.OrgObject = cenorg;
		me.Form.OrgObject = cenorg;
		me.GuidePanel.OrgObject = cenorg;
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