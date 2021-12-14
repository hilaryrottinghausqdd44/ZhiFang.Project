/**
 * 堆叠条形图:按货品一级分类(堆叠为二级分类)
 * @author longfc
 * @version 2019-02-25
 */
Ext.define('Shell.class.rea.client.statistics.echart.stock.goodsclass.stack.App', {
	extend: 'Shell.ux.panel.AppPanel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '库存统计-按货品分类',
	width: 1200,
	height: 800,
	/**是否需要刷新分类列表*/
	hasRefresh: true,
	layout: {
		type: 'border',
		regionWeights: {
			south: 1,
			west: 2
		}
	},
	/**统计类型:入库按货品一级分类(堆叠为二级分类):1;库存按货品一级分类(堆叠为二级分类):11;出库按货品一级分类(堆叠为二级分类):21;*/
	statisticType: 11,
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchStackEChartsVOListByHql?isPlanish=true',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemdblclick: function() {},
			sortchange: function(ct, column, direction, eOpts) {
				JShell.Action.delay(function() {
					//me.afterDataLoad();
				}, null, 200);
			}
		});
		me.getComponent("buttonsToolbar1").getComponent("cboYAxis").setValue("goodsclass");
		//me.onLoadData();
	},
	initComponent: function() {
		var me = this;

		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
		me.initDateArea(-60);
	},
	//创建功能按钮栏
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'top',
			itemId: 'buttonsToolbar1',
			items: ['refresh', '-', {
				xtype: 'uxSimpleComboBox',
				itemId: 'cboYAxis',
				emptyText: '纵坐标选择',
				fieldLabel: '纵坐标',
				labelWidth: 50,
				width: 135,
				//value: "goodsclass",
				//disabled: true,
				data: [
					["goodsclass", "货品分类"],
					["sumtotal", "总金额"]
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						me.onLoadData();
					}
				}
			},  {
				xtype: 'uxdatearea',
				itemId: 'date',
				labelAlign: 'right',
				width: 190,
				labelWidth: 0,
				fieldLabel: '',
				listeners: {
					enter: function() {
						me.onLoadData();
					}
				}
			}, '-', {
				xtype: 'button',
				iconCls: 'button-search',
				text: '查询',
				tooltip: '查询操作',
				handler: function() {
					me.onLoadData();
				}
			}, {
				text: '清空',
				iconCls: 'button-cancel',
				tooltip: '<b>清除原先的选择</b>',
				handler: function() {
					me.onClearSearch();
				}
			}, '->', {
				xtype: 'label',
				margin: '0 0 0 5',
				itemId: 'CountMoney'
			}]
		};
		return dockedItems;
	},
	//创建内部组件
	createItems: function() {
		var me = this;
		me.Grid = Ext.create('Shell.class.rea.client.statistics.echart.basic.goodsclass.Grid', {
			region: 'west',
			header: false,
			width: 360,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});

		me.BarChart = Ext.create('Shell.class.rea.client.statistics.echart.stock.goodsclass.stack.BarChart', {
			region: 'center',
			header: false
		});
		return [me.Grid, me.BarChart];
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
		var dateareaToolbar = me.getComponent('buttonsToolbar1'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateArea) date.setValue(dateArea);
	},
	/**清空处理*/
	onClearSearch: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		buttonsToolbar.getComponent('CountMoney').setText("", false);
		me.Grid.store.each(function(record) {
			record.set("SumTotal", 0);
			record.set("SumTotalPercent", 0);
			record.commit();
		});
		me.BarChart.clearData();
	},
	//刷新按钮处理
	onRefreshClick: function() {
		var me = this;
		me.onLoadData();
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		var me = this;
		return "";
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var dateArea = buttonsToolbar.getComponent('date');
		var docHql = [];
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		if(labID) {
			docHql.push('reabmsqtydtl.LabID=' + labID);
		}

		if(dateArea && dateArea.getValue()) {
			var value = dateArea.getValue();
			if(value.start) {
				var start = Ext.Date.format(value.start, "Y-m-d");
				if(start) {
					docHql.push("reabmsqtydtl.DataAddTime>='" + start + " 00:00:00'");
				}
			}
			if(value.end) {
				var end = Ext.Date.format(value.end, "Y-m-d");
				if(end) {
					docHql.push("reabmsqtydtl.DataAddTime<='" + end + " 23:59:59'");
				}
			}
		}

		if(docHql && docHql.length > 0) {
			docHql = docHql.join(" and ");
		} else {
			docHql = "";
		}
		return docHql;
	},
	/**获取部门货品条件*/
	getDeptGoodsHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var dtlHql = [];
		if(dtlHql && dtlHql.length > 0) {
			dtlHql = dtlHql.join(" and ");
		} else {
			dtlHql = "";
		}
		return dtlHql;
	},
	/**获取机构货品条件*/
	getReaGoodsHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var dtlHql = [];
		if(dtlHql && dtlHql.length > 0) {
			dtlHql = dtlHql.join(" and ");
		} else {
			dtlHql = "";
		}
		return dtlHql;
	},
	//加载数据
	onLoadData: function() {
		var me = this;
		if(me.hasRefresh == true) {
			me.Grid.onSearch();
			me.hasRefresh = false;
		}

		var url = JShell.System.Path.ROOT + me.selectUrl; // + '&t=' + new Date().getTime();
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var deptGoodsHql = me.getDeptGoodsHql();
		var reaGoodsHql = me.getReaGoodsHql();

		var params = [];
		params.push("statisticType=" + me.statisticType);
		if(docHql) params.push("docHql=" + JShell.String.encode(docHql));
		if(dtlHql) params.push("dtlHql=" + JShell.String.encode(dtlHql));
		if(deptGoodsHql) params.push("deptGoodsHql=" + JShell.String.encode(deptGoodsHql));
		if(reaGoodsHql) params.push("reaGoodsHql=" + JShell.String.encode(reaGoodsHql));
		if(params && params.length > 0) {
			params = params.join("&");
		} else {
			params = "";
		}
		if(params) url = url + "&" + params; // (url.indexOf('?') == -1 ? '?' : '&')
		JShell.Server.get(url, function(data) {
			//console.log("data:" + JcallShell.JSON.encode(data));
			if(data.success) {
				var data2 = data.value;
				me.afterDataLoad(data2);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	//数据加载完毕处理
	afterDataLoad: function(data) {
		var me = this;
		if(!data || data.length <= 0) {
			me.onClearSearch();
			return;
		}
		var countMoney = 0;
		var list = data.goodsClassList;
		if(!list) list = [];
		if(list && list.length > 0) {
			for(var i = list.length - 1; i >= 0; i--) {
				var sumTotal = list[i]["SumTotal"];
				if(!sumTotal) sumTotal = 0;
				var sumTotalPercent = list[i]["SumTotalPercent"];
				if(!sumTotalPercent) sumTotalPercent = 0;
				sumTotal=parseFloat(sumTotal);
				sumTotalPercent=parseFloat(sumTotalPercent);
				countMoney += parseFloat(sumTotal);
				var record = me.Grid.store.findRecord("ReaGoodsClassVO_CName", list[i]["GoodsClass"]);
				if(record) {
					record.set("SumTotal", parseFloat(sumTotal.toFixed(2)));
					record.set("SumTotalPercent", parseFloat(sumTotalPercent.toFixed(2)));
					record.commit();
				}
			}
			me.Grid.store.sort([{
				property: 'SumTotal',
				direction: 'DESC'
			}]);
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var yAxis = buttonsToolbar.getComponent("cboYAxis").getValue();
		me.BarChart.loadData(yAxis, data);
		me.setCountMoney(countMoney);
	},
	//总金额设置
	setCountMoney: function(value) {
		var me = this;
		var text = '<b>总金额：' + value.toFixed(2) + '元</b>';
		me.getComponent('buttonsToolbar1').getComponent('CountMoney').setText(text, false);
	}
});