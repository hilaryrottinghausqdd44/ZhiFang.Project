/**
 * 仪器选择
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.authorization.equip.CheckApp', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '仪器选择',

	width: 880,
	height: 460,
	/*传入的字典类型的仪器厂商品牌ID**/
	ETYPEID: '5724611581318422977',
	/*传入字典类型的仪器分类**/
	EBRADID: '4777630349498328266',
	/**是否单选*/
	checkOne: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.Grid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					var id = record.get('PDict_Id');
					//					me.CheckGrid.EquipTypeID=id;
					me.CheckGrid.loadGridByEquipTypeID(id);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get('PDict_Id');
					me.CheckGrid.loadGridByEquipTypeID(id);
				}, null, 500);
			}
		});

		me.CheckGrid.on({
			accept: function(p, record) {
				me.fireEvent('accept', me, record);
			}
		});
	},

	initComponent: function() {
		var me = this;
		/*仪器厂商品牌ID**/
		me.ETYPEID = me.ETYPEID || '5724611581318422977';
		/*仪器分类**/
		me.EBRADID = me.EBRADID || '4777630349498328266';
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create('Shell.class.sysbase.pdict.PDictGrid', {
			title: '仪器类型',
			header: false,
			hasRefresh: true,
			hasButtontoolbar: true,
			/**是否启用查询框*/
			hasSearch: true,
			hasPagingtoolbar: true,
			/**默认每页数量*/
			defaultPageSize: 500,
			region: 'west',
			split: true,
			collapsible: true,
			itemId: 'Grid',
			defaultWhere: "pdict.PDictType.Id=" + me.ETYPEID
		});
		me.CheckGrid = Ext.create('Shell.class.wfm.authorization.equip.CheckGrid', {
			region: 'center',
			header: false,
			itemId: 'CheckGrid',
			checkOne: me.checkOne,
			/**默认加载*/
			defaultLoad: false
		});

		return [me.Grid, me.CheckGrid];
	}
});