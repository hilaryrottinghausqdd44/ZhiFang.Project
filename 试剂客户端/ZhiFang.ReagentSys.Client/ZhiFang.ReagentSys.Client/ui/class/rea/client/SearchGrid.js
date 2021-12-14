/**
 * @description 按快捷查询按钮查询列表基础类
 * @author longfc
 * @version 2017-12-13
 */
Ext.define('Shell.class.rea.client.SearchGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**是否有收缩面板按钮*/
	hasCollapse: true,
	//	afterRender: function() {
	//		var me = this;
	//		me.callParent(arguments);
	//	},
	//	initComponent: function() {
	//		var me = this;
	//		me.callParent(arguments);
	//	},
	/**创建快捷查询栏*/
	createQuickSearchButtonToolbar: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			itemId: "CurDay",
			text: '今天',
			tooltip: '按今天查',
			handler: function(button, e) {
				me.onQuickSearch(0, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day10",
			text: '10天内',
			tooltip: '按10天查',
			handler: function(button, e) {
				me.onQuickSearch(-10, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day20",
			text: '20天内 ',
			tooltip: '按20天查',
			handler: function(button, e) {
				me.onQuickSearch(-20, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day30",
			text: '30天内',
			tooltip: '按30天查',
			handler: function(button, e) {
				me.onQuickSearch(-30, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day60",
			text: '60天内',
			tooltip: '按60天查',
			handler: function(button, e) {
				me.onQuickSearch(-60, button);
			}
		});
		if(me.hasCollapse) {
			items.push('->', {
				iconCls: 'button-right',
				tooltip: '<b>收缩面板</b>',
				handler: function() {
					me.collapse();
				}
			});
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'quickSearchButtonToolbar',
			items: items
		});
	},
	/**按日期快捷查询*/
	onQuickSearch: function(day, button) {
		var me = this;
		if(!me.validDateType()) return;
		me.onSetDateArea(day);
		me.setButtonDayToggle(button);
		me.onSearch();
	},
	/**按日期按钮点击后样式设置*/
	setButtonDayToggle: function(button) {
		var me = this;
		var buttonsToolbar = me.getComponent('quickSearchButtonToolbar');

		var items = buttonsToolbar.items.items;
		Ext.Array.forEach(items, function(item, index) {
			if(item && item.xtype == "button") item.toggle(false);
		});

		button.toggle(true);
	},
	/**验证日期类型是否选择*/
	validDateType: function() {
		JShell.Msg.overwrite('请重写validDateType');
		return false;
	},
	/**设置日期范围*/
	onSetDateArea: function(day) {
		JShell.Msg.overwrite('请重写onSetDateArea');
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		//sdate=Ext.Date.format(sdate,"Y-m-d");
		//edate=Ext.Date.format(edate,"Y-m-d");
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**按传入的天数进行默认点击查询*/
	initSearchDate: function(days) {
		var me = this;
		var buttonsToolbar = me.getComponent('quickSearchButtonToolbar');
		var btn = null;
		if(buttonsToolbar) {
			var itemId = "CurDay";
			switch(days) {
				case 0:
					itemId = "CurDay";
					break;
				case -10:
					itemId = "Day10";
					break;
				case -20:
					itemId = "Day20";
					break;
				case -30:
					itemId = "Day30";
					break;
				case -60:
					itemId = "Day60";
					break;
				default:
					break;
			}
			btn = buttonsToolbar.getComponent(itemId);
		}
		if(btn)
			me.onQuickSearch(days, btn);
	}
});