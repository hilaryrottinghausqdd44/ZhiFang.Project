/**
 * 仪器试剂使用量
 * @author longfc	
 * @version 2019-02-22
 */
Ext.define('Shell.class.rea.client.statistics.consume.equipreagent.BarChart', {
	extend: 'Shell.ux.chart.EChartsPanel4',
	
	title: '仪器试剂使用量',
	layout: 'fit',
	/**纵坐标选择值*/
	yAxisValue: "sumtotal",
	/**业务主单查询条件*/
	docHql: "",
	/**业务明细查询条件*/
	dtlHql: "",
	/**机构货品查询条件*/
	reaGoodsHql: "",
	/**当前查询日期范围*/
	dateArea: null,
	/**统计类型:1为理论消耗量;2为消耗比对分析;*/
	statisticType: 1,
	/**查询服务*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipReagUsageEChartsVOByHql?isPlanish=true',

	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	onSearch: function() {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl; // + '&t=' + new Date().getTime();
		me.clearData();
		var params = [];
		var cbsShowZero = false; //me.getComponent('buttonsToolbar1').getComponent('cbsShowZero').getValue();
		params.push("statisticType=" + me.statisticType);
		params.push("showZero=" + cbsShowZero);
		params.push("fields=EChartsVO_TestEquipID,EChartsVO_TestEquipName,EChartsVO_AllSumTotal,EChartsVO_SumTotal,EChartsVO_SumTotalPercent,EChartsVO_AllGoodsQty,EChartsVO_GoodsQty,EChartsVO_GoodsQtyPercent,EChartsVO_GoodsCName");
		if(me.docHql) params.push("docHql=" + JShell.String.encode(me.docHql));
		if(me.dtlHql) params.push("dtlHql=" + JShell.String.encode(me.dtlHql));
		if(me.deptGoodsHql) params.push("deptGoodsHql=" + JShell.String.encode(me.deptGoodsHql));
		if(me.reaGoodsHql) params.push("reaGoodsHql=" + JShell.String.encode(me.reaGoodsHql));
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
				me.showErrror("提示信息:"+data.msg);
			}
		});
	},
	//数据加载完毕处理
	afterDataLoad: function(list) {
		var me = this;
		var countMoney = 0;
		me.loadData(list);
	},
	//加载数据
	loadData: function(data) {
		var me = this;
		me.changeData(data);
	},
	/**
	 * 更改图表
	 * @param {Object} list 格式[{'ReaCompanyName':'aaa','SumTotal':10,'SumTotalPercent':'10%'}]
	 */
	changeData: function(list) {
		var me = this;
		var len = (list || []).length;
		if(len <= 0) {
			me.clearData();
			return;
		}

		var axisData1 = [],
			axisData2 = [],
			sumTotalList = [],
			sumTotalPercentList = [],
			goodsQtyList = [],
			goodsQtyPercentList = [];

		for(var i = 0; i < len; i++) {
			var sumTotal = list[i].EChartsVO_SumTotal;
			if(!sumTotal) sumTotal = 0;
			sumTotalList.push(parseFloat(sumTotal));
			var sumTotalPercent = list[i].EChartsVO_SumTotalPercent;
			if(!sumTotalPercent) sumTotalPercent = 0;
			sumTotalPercentList.push(parseFloat(sumTotalPercent).toFixed(2));

			var goodsQty = list[i].EChartsVO_GoodsQty;
			if(!goodsQty) goodsQty = 0;
			goodsQtyList.push(parseFloat(goodsQty));
			var goodsQtyPercent = list[i].EChartsVO_GoodsQtyPercent;
			if(!goodsQtyPercent) goodsQtyPercent = 0;
			goodsQtyPercentList.push(parseFloat(goodsQtyPercent).toFixed(2));
			axisData1.push(list[i].EChartsVO_TestEquipName);
			axisData2.push(list[i].EChartsVO_GoodsCName);
		}
		var colors = ['#5793f3', '#d14a61', '#675bba'];
		var config = {
			title: {
				'text': "",//me.title,
				'subtext': '数据来自LRMP'
			},
			tooltip: { //提示框组件
				trigger: 'axis',
				axisPointer: { // 坐标轴指示器，坐标轴触发有效
					type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
				}
			},
			legend: { //图例组件
				data: ['实际使用量', '实际金额(元)']
			},
			toolbox: { //工具栏
				show: true,
				feature: {
					mark: {
						show: false
					},
					dataView: {
						show: true,
						readOnly: false
					},
					magicType: {
						show: true,
						type: ['line', 'bar'] //, 'stack', 'tiled'
					},
					restore: {
						show: true
					},
					saveAsImage: {
						show: true
					}
				}
			},
			calculable: true,
			
			grid: {
				top: '6%',
				left: '1%',
				right: '2%',
				bottom: '6%',
				containLabel: true
			},
			dataZoom: [{
					type: 'slider', //图表下方的伸缩条
					show: true,
					start: 1,
					end: 100
				},
				{
					type: 'inside',
					start: 1,
					end: 100
				}
			],
			series: [{ //系列列表
				name: '实际使用量',
				type: 'bar',
				barMaxWidth:me.barMaxWidth,
				barMinHeight: 50,
				itemStyle: {
					normal: {
						label: {
							show: true,
							position: 'insideRight',
							formatter: '{c}'
						}
					}
				},
				data: goodsQtyList
			}]
		};
		var series2 = { //系列列表
			name: '实际金额(元)',
			type: 'bar',
			barMaxWidth:me.barMaxWidth,
			barMinHeight: 50,
			itemStyle: {
				normal: {
					label: {
						show: true,
						position: 'insideRight',
						formatter: '{c}元'
					}
				}
			},
			data: sumTotalList
		};

		var axis2 = [{
			type: 'value',
			name: '实际出库量',
			min: 0,
			position: 'left',
			axisLine: {
				lineStyle: {
					color: colors[0]
				}
			}
		}, {
			type: 'value',
			name: '实际金额(元)',
			min: 0,
			position: 'right',
			axisLine: {
				lineStyle: {
					color: colors[1]
				}
			}
		}];

		config.xAxis = [{
			type: 'category',
			axisLabel: {
				interval: 0,
				rotate: 15
			},
			data: axisData2
		}];

		series2.yAxisIndex = 1;
		config.series.push(series2);
		config.yAxis = axis2;
		me.changeChart(config, true);
	}
});