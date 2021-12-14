/**
 * 库存货品选择
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.add.choose.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '库存货品选择',
	/*左列表机构货品条件*/
	leftReaGoodHql: '',
	/*右列表默认条件*/
	rightDefaultWhere: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.LeftGrid.on({
			onBeforeSearch: function(grid) {
				me.onBeforeSearch();
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
	initComponent: function() {
		var me = this;
		me.addEvents('accept', 'onSearch', 'onIsLocked');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.LeftGrid = Ext.create('Shell.class.rea.client.inventory.add.choose.LeftGrid', {
			header: false,
			itemId: 'LeftGrid',
			region: 'west',
			width: "50%",
			split: false,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false
		});
		me.RightGrid = Ext.create('Shell.class.rea.client.inventory.add.choose.RightGrid', {
			header: true,
			itemId: 'RightGrid',
			region: 'center',
			split: false,
			collapsible: false,
			collapsed: false
		});
		var appInfos = [me.LeftGrid, me.RightGrid];
		return appInfos;
	},
	/**获取左列表的外部条件*/
	onBeforeSearch: function() {
		var me = this;
		var externalWhere = '';
		var arrIdStr = [];
		me.RightGrid.store.each(function(rec) {
			arrIdStr.push(rec.get(me.RightGrid.PKField));
		});
		if(arrIdStr && arrIdStr.length > 0) {
			me.leftReaGoodHql = "reagoods.Id not in(" + arrIdStr.join(",") + ")";
		}
		me.fireEvent('onSearch', me, me.LeftGrid);
	},
	clearData: function() {
		var me = this;
		
	},
	/**加载左列表数据*/
	loadData: function(params) {
		var me = this;
		me.LeftGrid.docEntity = params.docEntity;
		me.LeftGrid.reaGoodHql = params.reaGoodHql;
		me.LeftGrid.loadData(params);
	},
	/**从左列表选择行记录加入到右列表处理*/
	onAddRight: function(records) {
		var me = this;
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		Ext.Array.each(records, function(record, index, allItems) {
			var isAdd = true;
			me.RightGrid.store.each(function(rec) {
				if(rec.get(me.RightGrid.PKField) == record.get(me.LeftGrid.PKField)) {
					isAdd = false;
					return false;
				}
			});
			if(isAdd == true) {
				me.RightGrid.store.add(record.data);
			}
			var index = me.LeftGrid.store.indexOf(record);
			if(index >= 0) me.LeftGrid.store.removeAt(index);
		});
		me.onIsLocked();
	},
	/**从右列表移除选择的行记录后处理*/
	onRemoveRight: function(records) {
		var me = this;
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		Ext.Array.each(records, function(record, index, allItems) {
			var isAdd = true;
			me.LeftGrid.store.each(function(rec) {
				if(rec.get(me.RightGrid.PKField) == record.get(me.LeftGrid.PKField)) {
					isAdd = false;
					return false;
				}
			});
			if(isAdd == true) {
				me.LeftGrid.store.add(record.data);
			}
			var index = me.RightGrid.store.indexOf(record);
			if(index >= 0) me.RightGrid.store.removeAt(index);
		});
		me.onIsLocked();
	},
	onIsLocked: function() {
		var me = this;
		var isLocked = false;
		if(me.RightGrid.store.getCount() > 0) {
			isLocked = true;
		}
		me.fireEvent('onIsLocked', me, isLocked);
	}
});