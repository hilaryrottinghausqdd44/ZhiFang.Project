/**
 * 记录项字典选择
 * @author longfc
 * @version 2020-02-25
 */
Ext.define('Shell.class.sysbase.screcordtypeitem.choose.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '内容项选择',
	height: 560,
	/**关系表查询条件*/
	linkWhere:"",
	/*左列表默认条件*/
	leftDefaultWhere: '',
	/*右列表默认条件*/
	rightDefaultWhere: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.LeftGrid = Ext.create('Shell.class.sysbase.screcordtypeitem.choose.LeftGrid', {
			header: false,
			itemId: 'LeftGrid',
			region: 'west',
			width: "50%",
			split: false,
			collapsible: false,
			collapsed: false,
			animCollapse: false,
			animate: false,
			linkWhere: me.linkWhere,
			defaultWhere: me.leftDefaultWhere
		});
		me.RightGrid = Ext.create('Shell.class.sysbase.screcordtypeitem.choose.RightGrid', {
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
		if(arrIdStr && arrIdStr.length > 0) {
			externalWhere = "screcordtypeitem.Id not in(" + arrIdStr.join(",") + ")";
		}
		me.LeftGrid.setExternalWhere(externalWhere);
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

	}
});