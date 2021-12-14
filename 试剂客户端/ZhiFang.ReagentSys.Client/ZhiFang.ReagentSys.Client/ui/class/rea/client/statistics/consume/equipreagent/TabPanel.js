/**
 * 仪器试剂使用量
 * @author longfc	
 * @version 2019-02-22
 */
Ext.define('Shell.class.rea.client.statistics.consume.equipreagent.TabPanel', {
	extend: 'Ext.tab.Panel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '仪器试剂使用量',
	header: false,
	/**试剂选择框的宽度*/
	goodsNameWidth: "94%",
	/**选择仪器的IDStr*/
	equipIDStr: null,
	/**Lis仪器编码*/
	lisEquipCodeStr: "",
	/**当前选择的仪器名称*/
	equipCName: "",
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var me = this;
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
		me.Grid = Ext.create("Shell.class.rea.client.statistics.consume.equipreagent.Grid", {
			title: "项目检测量",
			header: true,
			border: false,
			itemId: 'Grid'
		});
		me.BarChart = Ext.create("Shell.class.rea.client.statistics.consume.equipreagent.BarChart", {
			title: '图表信息',
			header: true,
			border: false,
			itemId: 'BarChart'
		});
		var appInfos = [me.Grid, me.BarChart];
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
		if(comtab.itemId == "BarChart") {
			var idArr = me.equipIDStr.split(",");
			if(!me.equipIDStr || idArr.length != 1) {
				me.BarChart.setTitle("图表信息");
				me.BarChart.clearData();
				me.BarChart.showErrror("图表只按当前选择的仪器进行统计!请选择仪器后再统计!");
				return;
			}
		}

		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		if(!docHql) docHql = "";
		if(!dtlHql) dtlHql = "";
		if(!reaGoodsHql) reaGoodsHql = "";

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

		me.Grid.dateArea = dateArea;
		me.Grid.equipIDStr = me.equipIDStr;
		me.Grid.lisEquipCodeStr = me.lisEquipCodeStr;

		me.BarChart.dateArea = dateArea;
		me.BarChart.equipIDStr = me.equipIDStr;
		me.BarChart.lisEquipCodeStr = me.lisEquipCodeStr;

		switch(comtab.itemId) {
			case 'Grid':
				me.Grid.docHql = docHql;
				me.Grid.dtlHql = dtlHql;
				me.Grid.reaGoodsHql = reaGoodsHql;
				me.Grid.onSearch();
				break;
			case 'BarChart':
				me.BarChart.setTitle(me.equipCName + "图表信息");
				me.BarChart.docHql = docHql;
				me.BarChart.dtlHql = dtlHql;
				me.BarChart.reaGoodsHql = reaGoodsHql;
				me.BarChart.onSearch();
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

			case 'BarChart':

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
		me.BarChart.dateArea = dateArea;
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var date = buttonsToolbar.getComponent('date');
		var where = [];
		where.push("reabmsoutdoc.OutType=1");
		if(date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					where.push("reabmsoutdoc.DataAddTime>='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push("reabmsoutdoc.DataAddTime<='" + JShell.Date.toString(dateValue.end, true) + " 23:59:59'");
					//where.push("reabmsoutdoc.DataAddTime<='" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if(where && where.length > 0) {
			where = where.join(" and ");
		} else {
			where = "";
		}
		return where;
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var where = [];
		where.push("reabmsoutdtl.TestEquipID is not null ");
		if(me.equipIDStr) {
			where.push("reabmsoutdtl.TestEquipID in (" + me.equipIDStr + ")");
		}
		if(where && where.length > 0) {
			where = where.join(" and ");
		} else {
			where = "";
		}
		return where;
	},
	/**获取机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var goodsIDStr = buttonsToolbar.getComponent('GoodsIDStr');
		var where = [];
		if(goodsIDStr) {
			var idStr = goodsIDStr.getValue();
			if(idStr) {
				where.push("reagoods.Id in (" + idStr + ")");
			}
		}
		if(where && where.length > 0) {
			where = where.join(" and ");
		} else {
			where = "";
		}
		return where;
	}
});