/**
 * 供应商货品选择
 * 适合左列表及右列表都为相同的实体对象(ReaGoodsOrgLink),并且右列表不需要默认加载及过滤原已选择的供货商货品
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2018-11-14
 */
Ext.define('Shell.class.rea.client.order.choose.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '供应商货品选择',
	/*左列表默认条件*/
	leftDefaultWhere: '',
	/*右列表默认条件*/
	rightDefaultWhere: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.LeftGrid = Ext.create('Shell.class.rea.client.order.choose.LeftGrid', {
			header: false,
			itemId: 'LeftGrid',
			region: 'west',
			width: "50%",
			split: false,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false,
			defaultWhere: me.leftDefaultWhere
		});
		me.RightGrid = Ext.create('Shell.class.rea.client.order.choose.RightGrid', {
			header: false,
			itemId: 'RightGrid',
			region: 'center',
			split: false,
			collapsible: false,
			collapsed: false,
			defaultWhere: me.rightDefaultWhere
		});
		var appInfos = [me.LeftGrid, me.RightGrid];
		return appInfos;
	},
	/**获取左列表的外部条件*/
	onSetLeftExternalWhere: function() {
		var me = this;
		var externalWhere = '';
		var arrIdStr = [];
		me.RightGrid.store.each(function(rec) {
			arrIdStr.push(rec.get(me.RightGrid.PKField));
		});
		if (arrIdStr && arrIdStr.length > 0) {
			externalWhere = "reagoodsorglink.Id not in(" + arrIdStr.join(",") + ")";
		}
		me.LeftGrid.setExternalWhere(externalWhere);
	},
	onListeners: function() {
		var me = this;
		me.LeftGrid.on({
			onBeforeSearch: function(grid) {
				me.onSetLeftExternalWhere();
			},
			itemdblclick: function(grid, record) {
				me.onAddRight([record]);
			},
			onAccept: function(grid, records) {
				me.onAddRight(records);
			}
		});
		me.RightGrid.on({
			onRemove: function(grid, records) {
				me.onRemoveRight(records);
			},
			onAccept: function(grid, records) {
				me.fireEvent('accept', me, records);
			}
		});
	},
	/**从左列表选择行记录加入到右列表处理*/
	onAddRight: function(records) {
		var me = this;
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		Ext.Array.each(records, function(record, index, allItems) {
			var isAdd = true;
			me.RightGrid.store.each(function(rec) {
				if (rec.get(me.RightGrid.PKField) == record.get(me.LeftGrid.PKField)) {
					isAdd = false;
					return false;
				}
			});
			if (isAdd == true) {
				me.RightGrid.store.add(record.data);
			}
			var index = me.LeftGrid.store.indexOf(record);
			if (index >= 0) me.LeftGrid.store.removeAt(index);
		});
	},
	/**从右列表移除选择的行记录后处理*/
	onRemoveRight: function(records) {
		var me = this;
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		Ext.Array.each(records, function(record, index, allItems) {
			var isAdd = true;
			me.LeftGrid.store.each(function(rec) {
				if (rec.get(me.RightGrid.PKField) == record.get(me.LeftGrid.PKField)) {
					isAdd = false;
					return false;
				}
			});
			if (isAdd == true) {
				me.LeftGrid.store.add(record.data);
			}
			var index = me.RightGrid.store.indexOf(record);
			if (index >= 0) me.RightGrid.store.removeAt(index);
		});

	}
});
