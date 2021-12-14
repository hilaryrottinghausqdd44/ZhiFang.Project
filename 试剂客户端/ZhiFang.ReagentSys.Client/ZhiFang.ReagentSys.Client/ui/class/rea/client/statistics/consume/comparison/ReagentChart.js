/**
 * 消耗比对分析
 * @author longfc	
 * @version 2019-02-25
 */
Ext.define('Shell.class.rea.client.statistics.consume.comparison.ReagentChart', {
	extend: 'Shell.ux.chart.EChartsPanel4',
	
	title: '消耗比对分析',
	layout: 'fit',
	
	/**查询服务*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearChconsumeComparisonEChartsVOByHql?isPlanish=true',
	
	/**纵坐标选择值*/
	yAxisValue: "sumtotal",

	/**当前查询日期范围*/
	dateArea: null,
	/**统计类型:1为理论消耗量;2为消耗比对分析;*/
	statisticType: 2,
	/**选择仪器的IDStr*/
	equipIDStr: "",
	/**选择试剂的IDStr*/
	goodsIdStr: "",
	/**Lis仪器编码*/
	lisEquipCodeStr: "",

	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	onSearch: function() {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl; // + '&t=' + new Date().getTime();
		me.clearData();
		var params = [];
		var cbsShowZero = false;
		params.push("statisticType=" + me.statisticType);
		params.push("showZero=" + cbsShowZero);
		params.push("isMergeOfItem=" + true);
		if (me.equipIDStr)
			params.push("equipIDStr=" + me.equipIDStr);
		if (me.goodsIdStr)
			params.push("goodsIdStr=" + me.goodsIdStr);
		if (me.dateArea) {
			if (me.dateArea.start) {
				params.push("startDate=" + JShell.Date.toString(me.dateArea.start, true));
			}
			if (me.dateArea.end) {
				params.push("endDate=" + JShell.Date.toString(me.dateArea.end, true) + "");
			}
		}
		if (params && params.length > 0) {
			params = params.join("&");
		} else {
			params = "";
		}
		if (params) url = url + "&" + params; // (url.indexOf('?') == -1 ? '?' : '&')
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.afterDataLoad(data.value);
			} else {
				me.showErrror("提示信息:" + data.msg);
			}
		});
	},
	//数据加载完毕处理
	afterDataLoad: function(data) {
		var me = this;
		var countMoney = 0;
		me.loadData(data);
	},
	//加载数据
	loadData: function(data) {
		var me = this;
		me.changeData(data);
	},
	changeData: function(data) {
		var me = this;
		if (!data) {
			me.clearData();
			return;
		}
		var legendData = data.legend.data || [];
		if (!legendData || legendData.length <= 0) {
			me.clearData();
			return;
		}
		var xAxisData = data.xAxis.data || [];
		if (!xAxisData || xAxisData.length <= 0) {
			me.clearData();
			return;
		}
		var seriesList = data.series || [];
		if (!seriesList || seriesList.length <= 0) {
			me.clearData();
			return;
		}
		var config = {
			title: {
				'text': "", // me.title,
				'subtext': ''
			},
			tooltip: { //提示框组件
				trigger: 'axis',
				axisPointer: { // 坐标轴指示器，坐标轴触发有效
					type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
				}
			},
			toolbox: { //工具栏
				show: true,
				feature: {
					mark: {
						show: false
					},
					dataView: {
						show: false,
						readOnly: false
					},
					restore: {
						show: false
					},
					saveAsImage: {
						show: true
					}
				}
			},
			grid: {
				top: '16%',
				left: '1%',
				right: '2%',
				bottom: '2%',
				containLabel: true
			},
			dataZoom: [{
					type: 'slider', //图表下方的伸缩条
					show: true,
					start: 1,
					end: 50
				},
				{
					type: 'inside',
					start: 1,
					end: 100
				}
			],
			legend: { //图例组件
				type: 'scroll',
				//orient: 'vertical',
				right: 40,
				//top: 2,
				//bottom: 20,
				data: legendData,
				selected: {
					"实际使用量": true,
					"试剂成本": true,
					"常规理论消耗": true,
					"质控理论消耗": true,
					"复检理论消耗": true,
					"消耗比": true,
					"项目收入": true,
					"成本利润率": true,
					"成本占比": false,
					"毛利率": false,
					"额外消耗比": false
				}
			},
			calculable: true,
			xAxis: [{
				type: 'category',
				data: xAxisData,
				axisLabel: {
					interval: 0,
					rotate: 20
				}
			}],
			yAxis: [{
					type: 'value',
					name: '消耗量',
					min: 0
				},
				{
					type: 'value',
					name: '消耗比',
					position: 'right',
					min: 0,
					axisLabel: {
						formatter: '{value}%'
					}
				},
				{
					type: 'value',
					name: '试剂成本',
					position: 'right',
					offset: 45,
					//min: 0,
					axisLabel: {
						formatter: '{value}元'
					}
				},
				{
					type: 'value',
					name: '成本利润率',
					position: 'right',
					offset: 120,
					//min: 0,
					axisLabel: {
						formatter: '{value}%'
					}
				}
			]
		};

		//series
		var series = [];
		if (!seriesList) {
			seriesList = [];
			var jseries = {
				"name": "未知",
				"type": "bar"
			};
			seriesList.push(jseries);
		}
		var itemStyle = {
			normal: {
				label: {
					show: true,
					position: 'insideRight',
					formatter: '{c}'
				}
			}
		};
		for (var i = 0; i < seriesList.length; i++) {
			var jseries = seriesList[i];
			var isFormatter = false;
			switch (jseries.name) {
				case "消耗比":
					isFormatter = true;
					break;
				case "成本占比":
					isFormatter = true;
					break;
				case "成本利润率":
					isFormatter = true;
					break;
				case "毛利率":
					isFormatter = true;
					break;
				case "额外消耗比":
					isFormatter = true;
					break;
				default:
					break;
			}
			if (isFormatter == true) {
				jseries.itemStyle = {
					normal: {
						label: {
							show: true,
							position: 'insideRight',
							formatter: '{c}%'
						}
					}
				};
				jseries.barMaxWidth = me.barMaxWidth;
				jseries.barMinHeight = me.barMinHeight;
			} else {
				jseries.barMaxWidth = me.barMaxWidth;
				jseries.itemStyle = itemStyle;
				jseries.barMinHeight = me.barMinHeight;
			}
			series.push(jseries);
		}
		config.series = series;
		me.changeChart(config, true);
	}
});
