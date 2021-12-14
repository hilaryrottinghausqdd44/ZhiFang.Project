/**
 * 项目（实际）检测量
 * 从Lis系统获取数据,统计每台仪器每个项目实际检测量
 * @author longfc	
 * @version 2019-02-22
 */
Ext.define('Shell.class.rea.client.statistics.consume.item.TabPanel', {
	extend: 'Ext.tab.Panel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '项目实际检测量',
	header: false,
	/**检验项目选择框的宽度*/
	reaTestItemWidth: "94%",
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
			/**页签切换事件处理*/
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
		me.Grid = Ext.create("Shell.class.rea.client.statistics.consume.item.Grid", {
			title: "项目检测量",
			header: true,
			border: false,
			itemId: 'Grid'
		});
		me.BarChart = Ext.create("Shell.class.rea.client.statistics.consume.item.BarChart", {
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
		if (me.hasLoadMask) {
			me.body.mask(text);
		}
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
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
		}, '-', {
			xtype: 'splitbutton',
			textAlign: 'left',
			iconCls: 'button-add',
			text: '同步LIS基础信息',
			tooltip: '在获取LIS项目检测量前,LRMP先从LIS同步检验仪器,检验项目,仪器项目关系信息',
			handler: function(btn, e) {
				btn.overMenuTrigger = true;
				btn.onClick(e);
			},
			menu: [{
				text: '获取LIS检验仪器',
				iconCls: 'button-add',
				tooltip: '获取LIS检验仪器',
				listeners: {
					click: function(but) {
						me.onSyncBasicInfo("ReaTestEquipLab");
					}
				}
			}, {
				text: '获取LIS检验项目',
				iconCls: 'button-add',
				tooltip: '获取LIS仪器信息',
				listeners: {
					click: function(but) {
						me.onSyncBasicInfo("ReaTestItem");
					}
				}
			}, {
				text: '获取LIS仪器项目关系',
				iconCls: 'button-add',
				tooltip: '获取LIS仪器项目关系',
				listeners: {
					click: function(but) {
						me.onSyncBasicInfo("ReaTestEquipItem");
					}
				}
			}]
		}, '-', {
			boxLabel: '是否覆盖已提取结果',
			name: 'cboIsCover',
			itemId: 'cboIsCover',
			xtype: 'checkboxfield',
			inputValue: false,
			checked: false
		}, {
			xtype: 'button',
			iconCls: 'button-import',
			text: '获取LIS项目检测量',
			tooltip: '为了保证项目检测量能正确导入:(1)请确保检验项目信息已与LIS同步完成;(2)请确认仪器项目关系已与LIS同步完成;',
			handler: function() {
				me.onImport();
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
		items = me.createReaTestItemItems(items);
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
	/**创建检验项目选择*/
	createReaTestItemItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;
		items.push({
			fieldLabel: '',
			emptyText: '检验项目选择',
			name: 'ReaTestItemName',
			itemId: 'ReaTestItemName',
			labelWidth: 0,
			width: me.reaTestItemWidth, //"95%",
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.testitem.choose.App', {
					title: '检验项目选择',
					width: maxWidth,
					height: height,
					listeners: {
						accept: function(p, records) {
							me.onReaTestItemCheck(p, records);
						}
					}
				}).show();
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaTestItemId',
			name: 'ReaTestItemId',
			fieldLabel: '检验项目ID',
			hidden: true
		});
		return items;
	},
	/**检验项目选择*/
	onReaTestItemCheck: function(p, records) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');

		var value1 = [],
			value2 = [];
		if (records && records.length > 0) {
			for (var i in records) {
				value1.push(records[i].get('ReaTestItem_Id'));
				value2.push(records[i].get('ReaTestItem_CName'));
			}
		}
		if (value1 && value1.length > 0) {
			value1 = value1.join(",");
		} else {
			value1 = "";
		}
		if (value2 && value2.length > 0) {
			value2 = value2.join(";");
		} else {
			value2 = "";
		}
		buttonsToolbar.getComponent('ReaTestItemId').setValue(value1);
		buttonsToolbar.getComponent('ReaTestItemName').setValue(value2);
		p.close();
	},
	onSearch: function() {
		var me = this;
		var comtab = me.getActiveTab(me.items.items[0]);
		//图表只按当前选择的仪器进行图表统计
		if (comtab.itemId == "BarChart") {
			var idArr = me.equipIDStr.split(",");
			if (!me.equipIDStr || idArr.length != 1) {
				me.BarChart.setTitle("图表信息");
				me.BarChart.clearData();
				me.BarChart.showErrror("图表只按当前选择的仪器进行统计!请选择仪器后再统计!");
				return;
			}
		}

		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		if (!docHql) docHql = "";
		if (!dtlHql) dtlHql = "";
		if (!reaGoodsHql) reaGoodsHql = "";

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var date = buttonsToolbar.getComponent('date');
		var dateArea = "";
		if (date) {
			dateArea = date.getValue();
		}
		if (!dateArea) {
			JShell.Msg.alert('日期范围不能为空！');
			return;
		}
		if (me.Grid.isMultiple) {
			if (me.Grid.store.getCount() <= 0) {
				JShell.Msg.alert('请选择仪器后再操作！');
				return;
			}
			var idArr = [],
				lisCodeArr = [];
			me.Grid.store.each(function(record) {
				idArr.push(record.get("ReaTestEquipLab_Id"));
				var lisCode = record.get("ReaTestEquipLab_LisCode");
				if (lisCode)
					lisCodeArr.push(lisCode);
			});
			if (idArr.length > 0) {
				me.equipIDStr = idArr.join(",");
			}
			if (lisCodeArr.length > 0) {
				me.lisEquipCodeStr = lisCodeArr.join(",");
			}
		}
		me.Grid.dateArea = dateArea;
		me.Grid.equipIDStr = me.equipIDStr;
		me.Grid.lisEquipCodeStr = me.lisEquipCodeStr;

		me.BarChart.dateArea = dateArea;
		me.BarChart.equipIDStr = me.equipIDStr;
		me.BarChart.lisEquipCodeStr = me.lisEquipCodeStr;

		switch (comtab.itemId) {
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
		buttonsToolbar.getComponent('ReaTestItemId').setValue("");
		buttonsToolbar.getComponent('ReaTestItemName').setValue("");

		var comtab = me.getActiveTab(me.items.items[0]);
		switch (comtab.itemId) {
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
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('buttonsToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);
		me.Grid.dateArea = dateArea;
		me.BarChart.dateArea = dateArea;
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		return "";
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var date = buttonsToolbar.getComponent('date');
		var searchToolbar = me.getComponent('searchToolbar');
		var testItemIdStr = me.getComponent('buttonsToolbar1').getComponent('ReaTestItemId').getValue();

		var where = [];
		where.push("realisteststatisticalresults.TestEquipID is not null ");
		if (date) {
			var dateValue = date.getValue();
			if (dateValue) {
				if (dateValue.start) {
					where.push("realisteststatisticalresults.TestDate>='" + JShell.Date.toString(dateValue.start, true) +
						" 00:00:00'");
				}
				if (dateValue.end) {
					where.push("realisteststatisticalresults.TestDate<='" + JShell.Date.toString(dateValue.end, true) +
						" 23:59:59'");
					//where.push("realisteststatisticalresults.TestDate<='" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if (me.equipIDStr) {
			where.push("realisteststatisticalresults.TestEquipID in (" + me.equipIDStr + ")");
		}
		if (testItemIdStr) {
			where.push("realisteststatisticalresults.TestItemID in (" + testItemIdStr + ")");
		}
		if (where && where.length > 0) {
			where = where.join(" and ");
		} else {
			where = "";
		}
		return where;
	},
	/**获取机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this;

		return "";
	},
	/**从LIS同步检验仪器,检验项目,仪器项目关系*/
	onSyncBasicInfo: function(syncType) {
		var me = this;
		var params = {
			"syncType": syncType
		};
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_AddSyncLISBasicInfo";
		me.showMask("同步LIS基础信息中...");
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			me.hideMask();
			if (data.success) {
				JShell.Msg.alert("同步LIS基础信息完成!", null, 2000);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**从LIS导入检测结果*/
	onImport: function() {
		var me = this;
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_AddReaLisTestStatisticalResults";
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var date = buttonsToolbar.getComponent('date');
		var dateArea = null,
			beginDate = "",
			endDate = "";
		if (date) {
			dateArea = date.getValue();
			if (dateArea) {
				if (dateArea.start) beginDate = JShell.Date.toString(dateArea.start, true);
				if (dateArea.end) endDate = JShell.Date.toString(dateArea.end, true);
			}
		}
		if (!dateArea) {
			JShell.Msg.alert('日期范围不能为空！');
			return;
		}
		if (me.Grid.isMultiple) {
			if (me.Grid.store.getCount() <= 0) {
				JShell.Msg.alert('请选择仪器后再操作！');
				return;
			}
			var idArr = [],
				lisCodeArr = [];
			me.Grid.store.each(function(record) {
				idArr.push(record.get("ReaTestEquipLab_Id"));
				var lisCode = record.get("ReaTestEquipLab_LisCode");
				if (lisCode)
					lisCodeArr.push(lisCode);
			});
			if (idArr.length > 0) {
				me.equipIDStr = idArr.join(",");
			}
			if (lisCodeArr.length > 0) {
				me.lisEquipCodeStr = lisCodeArr.join(",");
			}
		}
		var isCover = false;
		var cboIsCover = buttonsToolbar.getComponent('cboIsCover');
		if (cboIsCover) {
			isCover = cboIsCover.getValue();
		}
		var params = {
			"isCover": isCover, //当次提取的结果是否覆盖原已提取的检测结果
			"testType": "", //检测类型,全部时为空
			"beginDate": beginDate,
			"endDate": endDate,
			"equipIDStr": me.equipIDStr,
			"lisEquipCodeStr": me.lisEquipCodeStr,
			"where": "",
			"order": ""
		};
		me.showMask("获取LIS检测结果中...");
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			me.hideMask();
			if (data.success) {
				me.Grid.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
	}
});
