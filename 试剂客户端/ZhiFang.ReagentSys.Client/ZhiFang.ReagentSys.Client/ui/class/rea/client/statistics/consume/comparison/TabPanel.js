/**
 * 消耗比对分析
 * @author longfc	
 * @version 2019-02-25
 */
Ext.define('Shell.class.rea.client.statistics.consume.comparison.TabPanel', {
	extend: 'Ext.tab.Panel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '消耗比对分析',
	header: false,
	/**试剂选择框的宽度*/
	goodsNameWidth: "94%",
	/**选择仪器的IDStr*/
	equipIDStr: null,
	/**Lis仪器编码*/
	lisEquipCodeStr: "",
	/**当前选择的仪器名称*/
	equipCName: "",
	/**同一仪器相同试剂不同项目的结果按项目合并*/
	isMergeOfItem: true,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				me.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
		me.initDateArea(-30);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create("Shell.class.rea.client.statistics.consume.comparison.Grid", {
			title: "消耗比对分析",
			header: true,
			border: false,
			itemId: 'Grid'
		});
		me.ReagentChart = Ext.create("Shell.class.rea.client.statistics.consume.comparison.ReagentChart", {
			title: '图表信息',
			header: true,
			border: false,
			itemId: 'ReagentChart'
		});
		var appInfos = [me.Grid, me.ReagentChart];
		return appInfos;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = [];
		items.push(me.createButtonToolbar1Items());
		items.push(me.createButtonToolbarItems());
		return items;
	},
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			xtype: 'uxdatearea',
			itemId: 'date',
			labelAlign: 'right',
			labelWidth: 60,
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		}, {
			boxLabel: '同一仪器相同试剂不同项目的结果按项目合并',
			name: 'cbsShowZero',
			itemId: 'cbsmergeOfItem',
			xtype: 'checkboxfield',
			hidden: true,
			inputValue: true,
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.isMergeOfItem = newValue;
					me.onSearch();
				}
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		}, {
			text: '清空',
			iconCls: 'button-cancel',
			tooltip: '<b>清除原先的选择</b>',
			handler: function() {
				me.onClearData();
			}
		}];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var items = [];
		items = me.createGoodsNameItems(items);
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar1',
			items: items
		});
	},
	/**创建试剂选择*/
	createGoodsNameItems: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		items.push({
			fieldLabel: '',
			emptyText: '货品选择',
			name: 'GoodsName',
			itemId: 'GoodsName',
			labelWidth: 0,
			width: me.goodsNameWidth, //"95%",
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.goods2.choose.App', {
					title: '货品选择',
					width: maxWidth,
					height: height,
					listeners: {
						accept: function(p, records) {
							me.onGoodsCheck(p, records);
						}
					}
				}).show();
			}
		}, {
			xtype: 'textfield',
			itemId: 'GoodsIDStr',
			name: 'GoodsIDStr',
			fieldLabel: '货品ID',
			hidden: true
		});
		return items;
	},
	/**试剂选择*/
	onGoodsCheck: function(p, records) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var goodsIDStr = buttonsToolbar.getComponent('GoodsIDStr');
		if(!goodsIDStr) {
			p.close();
			JShell.Msg.overwrite('onGoodsAccept');
			return;
		}
		var value1 = [],
			value2 = [];
		if(records && records.length > 0) {
			for(var i in records) {
				value1.push(records[i].get('ReaGoods_Id'));
				value2.push(records[i].get('ReaGoods_CName'));
			}
		}
		if(value1 && value1.length > 0) {
			value1 = value1.join(",");
		} else {
			value1 = "";
		}
		if(value2 && value2.length > 0) {
			value2 = value2.join(";");
		} else {
			value2 = "";
		}
		var goodsName = buttonsToolbar.getComponent('GoodsName');
		goodsIDStr.setValue(value1);
		goodsName.setValue(value2);
		p.close();
	},
	onSearch: function() {
		var me = this;
		var comtab = me.getActiveTab(me.items.items[0]);
		//图表只按当前选择的仪器进行图表统计
		if(comtab.itemId == "ReagentChart") {
			var idArr = me.equipIDStr.split(",");
			if(!me.equipIDStr || idArr.length != 1) {
				me.ReagentChart.setTitle("图表信息");
				me.ReagentChart.clearData();
				me.ReagentChart.showErrror("图表只按当前选择的仪器进行统计!请选择仪器后再统计!");
				return;
			}
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var date = buttonsToolbar.getComponent('date');
		var dateArea = "";
		if(date) {
			dateArea = date.getValue();
		}
		if(!dateArea) {
			JShell.Msg.alert('日期范围不能为空！');
			return;
		}
		if(me.Grid.isMultiple) {
			if(me.Grid.store.getCount() <= 0) {
				JShell.Msg.alert('请选择仪器后再操作！');
				return;
			}
			var idArr = [],
				lisCodeArr = [];
			me.Grid.store.each(function(record) {
				idArr.push(record.get("ReaTestEquipLab_Id"));
				var lisCode = record.get("ReaTestEquipLab_LisCode");
				if(lisCode)
					lisCodeArr.push(lisCode);
			});
			if(idArr.length > 0) {
				me.equipIDStr = idArr.join(",");
			}
			if(lisCodeArr.length > 0) {
				me.lisEquipCodeStr = lisCodeArr.join(",");
			}
		}
		me.Grid.isMergeOfItem = me.isMergeOfItem;
		var goodsIdStr = me.getComponent('buttonsToolbar1').getComponent('GoodsIDStr').getValue();
		me.Grid.dateArea = dateArea;
		me.Grid.equipIDStr = me.equipIDStr;
		me.Grid.goodsIdStr = goodsIdStr;
		me.Grid.lisEquipCodeStr = me.lisEquipCodeStr;
		me.ReagentChart.dateArea = dateArea;
		me.ReagentChart.equipIDStr = me.equipIDStr;
		me.ReagentChart.goodsIdStr = goodsIdStr;
		me.ReagentChart.lisEquipCodeStr = me.lisEquipCodeStr;
		switch(comtab.itemId) {
			case 'Grid':
				me.Grid.onSearch();
				break;
			case 'ReagentChart':
				me.ReagentChart.setTitle(me.equipCName + "图表信息");
				me.ReagentChart.onSearch();
				break;
			default:
				break
		}
	},
	onClearData: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var goodsIDStr = buttonsToolbar.getComponent('GoodsIDStr');
		var goodsName = buttonsToolbar.getComponent('GoodsName');
		if(goodsIDStr)
			goodsIDStr.setValue("");
		if(goodsName)
			goodsName.setValue("");
		var comtab = me.getActiveTab(me.items.items[0]);
		switch(comtab.itemId) {
			case 'Grid':
				break;
			case 'ReagentChart':

				break;
			default:
				break
		}
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('buttonsToolbar'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateArea) date.setValue(dateArea);
		me.Grid.dateArea = dateArea;
		me.ReagentChart.dateArea = dateArea;
	}
});