/**
 * 仪器管理左区域
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.sysbase.bequip.TabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '仪器管理',
	border: false,
	closable: true,

	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',

	PK: '',
	formtype: "",
	initComponent: function() {
		var me = this;
		/*仪器厂商品牌ID**/
		me.ETYPEID = me.ETYPEID || '5724611581318422977';
		/*仪器分类**/
		me.EBRADID = me.EBRADID || '4777630349498328266';
		me.bodyPadding = 1;
		me.setTitles();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
	},

	createItems: function() {
		var me = this;
		me.EquipFactoryBrand = Ext.create('Shell.class.sysbase.pdict.PDictGrid', {
			title: '仪器品牌',
			header: false,
			hasButtontoolbar: true,
			hasRefresh: true,
			hasSearch: true,
			hasPagingtoolbar: true,
			/**默认每页数量*/
			defaultPageSize: 500,
			itemId: 'EquipFactoryBrand',
			border: false,
			defaultWhere: "pdict.PDictType.Id=" + me.EBRADID
		});
		me.EquipType = Ext.create('Shell.class.sysbase.pdict.PDictGrid', {
			title: '仪器类型',
			header: false,
			hasRefresh: true,
			hasButtontoolbar: true,
			/**是否启用查询框*/
			hasSearch: true,
			hasPagingtoolbar: true,
			/**默认每页数量*/
			defaultPageSize: 500,
			itemId: 'EquipType',
			border: false,
			defaultWhere: "pdict.PDictType.Id=" + me.ETYPEID
		});
		return [me.EquipType, me.EquipFactoryBrand];
	},

	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;

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
		} //隐藏遮罩层
	}
});