/**
 * @description 按快捷查询按钮查询列表基础类
 * @author longfc
 * @version 2017-12-13
 */
Ext.define('Shell.class.rea.client.SearchGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建快捷查询栏*/
	createQuickSearchButtonToolbar: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			text: '今天',
			tooltip: '按今天查',
			handler: function() {
				me.onQuickSearch(0);
			}
		}, {
			xtype: 'button',
			text: '10天内',
			tooltip: '按10天查',
			handler: function() {
				me.onQuickSearch(-10);
			}
		}, {
			xtype: 'button',
			text: '20天内 ',
			tooltip: '按20天查',
			handler: function() {
				me.onQuickSearch(-20);
			}
		}, {
			xtype: 'button',
			text: '30天内',
			tooltip: '按30天查',
			handler: function() {
				me.onQuickSearch(-30);
			}
		}, {
			xtype: 'button',
			text: '60天内',
			tooltip: '按60天查',
			handler: function() {
				me.onQuickSearch(-60);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'quickSearchButtonToolbar',
			items: items
		});
	},
	/**按日期快捷查询*/
	onQuickSearch: function(day) {
		var me = this;
		if(!me.validDateType()) return;
		me.onSetDateArea(day);
		me.onSearch();
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
		//console.log(dateArea);
		return dateArea;
	}
});