/**
 * 入库统计-按货品分类
 * @author longfc
 * @version 2019-02-20
 */
Ext.define('Shell.class.rea.client.statistics.echart.in.goodsclass.App', {
	extend: 'Shell.ux.panel.AppPanel',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '入库统计-按货品分类',
	width: 1200,
	height: 800,
	/**统计类型:按库房:1;按供货商:2;按品牌:3;按货品一级分类:4;按货品二级分类:5;*/
	statisticType: 4,
	/**是否需要刷新分类列表*/
	hasRefresh: true,

	layout: {
		type: 'border',
		regionWeights: {
			south: 1,
			west: 2
		}
	},
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchInEChartsVOListByHql?isPlanish=true',

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
		me.onLoadData();
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
				itemId: 'cboGoodsClass',
				emptyText: '分类选择',
				fieldLabel: '分类选择',
				labelWidth: 60,
				width: 145,
				value: "goodsclass",
				data: [
					["goodsclass", "一级分类"],
					["goodsclasstype", "二级分类"]
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						me.hasRefresh = true;
						if(newValue == "goodsclass") {
							me.statisticType = 4;
						} else {
							me.statisticType = 5;
						}
						me.onLoadData();
					}
				}
			}, {
				xtype: 'uxSimpleComboBox',
				itemId: 'cboYAxis',
				emptyText: '纵坐标选择',
				fieldLabel: '纵坐标',
				labelWidth: 50,
				width: 125,
				value: "sumtotal",
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
			}, {
				xtype: 'uxSimpleComboBox',
				itemId: 'cboSumTotalPercent',
				emptyText: '金额占比显示为',
				fieldLabel: '金额占比显示为',
				labelWidth: 95,
				width: 175,
				value: "bar",
				//disabled: true,
				data: [
					["line", "拆线图"],
					["bar", "柱状图"]
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						me.onLoadData();
					}
				}
			}, {
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

		me.BarChart = Ext.create('Shell.class.rea.client.statistics.echart.in.goodsclass.BarChart', {
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
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var dateArea = buttonsToolbar.getComponent('date');
		var docHql = [];
		var labID = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.LABID) || "";
		docHql.push('reabmsindoc.Visible=1');
		if(labID) {
			docHql.push('reabmsindoc.LabID=' + labID);
		}

		if(dateArea && dateArea.getValue()) {
			var value = dateArea.getValue();
			if(value.start) {
				var start = Ext.Date.format(value.start, "Y-m-d");
				if(start) {
					docHql.push("reabmsindoc.DataAddTime>='" + start + " 00:00:00'");
				}
			}
			if(value.end) {
				var end = Ext.Date.format(value.end, "Y-m-d");
				if(end) {
					docHql.push("reabmsindoc.DataAddTime<='" + end + " 23:59:59'");
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
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
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
		var classType = me.getComponent('buttonsToolbar1').getComponent("cboGoodsClass").getValue();
		me.Grid.ClassType = classType;
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
		params.push("fields=EChartsVO_GoodsClass,EChartsVO_GoodsClassType,EChartsVO_AllSumTotal,EChartsVO_SumTotal,EChartsVO_SumTotalPercent");
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
			if(data.success) {
				var data2 = data.value;
				var list = [];
				if(data2 && data2.list) list = data2.list;
				me.afterDataLoad(list);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	//数据加载完毕处理
	afterDataLoad: function(list) {
		var me = this;
		var curClassType = me.getComponent('buttonsToolbar1').getComponent("cboGoodsClass").getValue();
		var countMoney = 0;
		if(list && list.length > 0) {
			for(var i = list.length - 1; i >= 0; i--) {
				var sumTotal = list[i]["EChartsVO_SumTotal"];
				if(!sumTotal) sumTotal = 0;
				var sumTotalPercent = list[i]["EChartsVO_SumTotalPercent"];
				if(!sumTotalPercent) sumTotalPercent = 0;
				sumTotal=parseFloat(sumTotal);
				sumTotalPercent=parseFloat(sumTotalPercent);
				countMoney += parseFloat(sumTotal);
				var classType = list[i]["EChartsVO_GoodsClass"];
				if(curClassType == "goodsclasstype") classType = list[i]["EChartsVO_GoodsClassType"];
				var record = me.Grid.store.findRecord("ReaGoodsClassVO_CName", classType);
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
		var cboSumTotalPercent= buttonsToolbar.getComponent("cboSumTotalPercent").getValue();
		me.BarChart.percentType=cboSumTotalPercent;
		me.BarChart.ClassType = curClassType;
		me.BarChart.loadData(yAxis, list);
		me.setCountMoney(countMoney);
	},
	//总金额设置
	setCountMoney: function(value) {
		var me = this;
		var text = '<b>总金额：' + value.toFixed(2) + '元</b>';
		me.getComponent('buttonsToolbar1').getComponent('CountMoney').setText(text, false);
	}
});