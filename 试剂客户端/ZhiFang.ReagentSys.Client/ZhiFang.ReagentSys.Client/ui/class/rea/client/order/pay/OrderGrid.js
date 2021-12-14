/**
 * @description 订单付款
 * @author longfc
 * @version 2019-01-03
 */
Ext.define('Shell.class.rea.client.order.pay.OrderGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderGrid',

	title: '订单付款',
	width: 800,
	height: 500,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**录入:entry/审核:check*/
	OTYPE: "pay",
	/**下拉状态默认值*/
	defaultStatusValue: "3",
	/**订单总单付款状态Key*/
	PayStausKey: "ReaBmsOrderDocPayStaus",
	/**用户UI配置Key*/
	userUIKey: 'order.pay.OrderGrid',
	/**用户UI配置Name*/
	userUIName: "订单付款列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initSearchDate(-10);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.PayStausKey, false, true, null);
		//订单状态不能为:暂存,审核退回,审批退回
		me.defaultWhere = 'reabmscenorderdoc.Status!=0 and reabmscenorderdoc.Status!=1 and reabmscenorderdoc.Status!=2 and reabmscenorderdoc.Status!=11';

		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItemsZdy: function() {
		var me = this;
		me.callParent(arguments);
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [];
		me.dockedItems.splice(1, 0, me.createQuickPayStausButtonToolbar());
	},
	/**创建快捷查询栏*/
	createQuickPayStausButtonToolbar: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'button',
			itemId: "btnAllPayStaus",
			text: '全部',
			tooltip: '按全部查',
			handler: function(button, e) {
				me.onPayStausSearch(0, button);
			}
		}, {
			xtype: 'button',
			itemId: "btnPayStaus1",
			text: '未付款',
			tooltip: '未付款',
			handler: function(button, e) {
				me.onPayStausSearch(1, button);
			}
		}, {
			xtype: 'button',
			itemId: "btnPayStaus2",
			text: '已付款 ',
			tooltip: '已付款',
			handler: function(button, e) {
				me.onPayStausSearch(2, button);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'payStausButtonToolbar',
			items: items
		});
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		var itemArr = [];
		//暂存
		if(tempList[1]) itemArr.push(tempList[1]);
		//申请
		if(tempList[2]) itemArr.push(tempList[2]);
		//审核退回
		if(tempList[3]) itemArr.push(tempList[3]);
		//审批退回
		if(tempList[12]) itemArr.push(tempList[12]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = me.callParent(arguments);
		columns.splice(3, 0, {
			dataIndex: 'ReaBmsCenOrderDoc_PayStaus',
			text: '付款状态',
			width: 95,
			renderer: function(value, meta) {
				var v = value;
				//未付款
				if(!v)v="1";
				
				if(JShell.REA.StatusList.Status[me.PayStausKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.PayStausKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.PayStausKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.PayStausKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.PayStausKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.PayStausKey].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerCompCode',
			text: '供货商平台编码',
			width: 95,
			hidden:true,
			defaultRenderer: true
		});
		return columns;
	},
	/**创建数据列*/
	onPayStausSearch: function(payStaus, button) {
		var me = this;
		me.setButtonDayToggle(button);
		me.externalWhere = "";
		if(payStaus != 0) {
			me.externalWhere = "reabmscenorderdoc.PayStaus=" + payStaus;
		}
		me.onSearch();
	},
	/**按日期按钮点击后样式设置*/
	setButtonDayToggle: function(button) {
		var me = this;
		var buttonsToolbar = me.getComponent('payStausButtonToolbar');

		var items = buttonsToolbar.items.items;
		Ext.Array.forEach(items, function(item, index) {
			if(item && item.xtype == "button") item.toggle(false);
		});
		button.toggle(true);
	}
});