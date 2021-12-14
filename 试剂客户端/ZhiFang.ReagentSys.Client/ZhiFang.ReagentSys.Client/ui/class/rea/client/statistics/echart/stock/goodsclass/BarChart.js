/**
 * 库存统计-按货品分类
 * @author longfc
 * @version 2019-02-25
 */
Ext.define('Shell.class.rea.client.statistics.echart.stock.goodsclass.BarChart', {
	extend: 'Shell.ux.chart.EChartsPanel4',

	layout: 'fit',
	title: '库存统计-按货品分类',
	/**纵坐标选择值*/
	yAxisValue: "sumtotal",
	/**货品分类*/
	ClassType: "goodsclass",
	/**金额占比的图表类型*/
	percentType: "bar",
	
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**
	 * 更改图表
	 * @param {Object} list 格式[{'ReaCompanyName':'aaa','SumTotal':10,'SumTotalPercent':'10%'}]
	 */
	changeData: function(list) {
		var me = this;
		var len = (list || []).length;
		if(len<=0){
			me.clearData();
			return;
		}
		var axisData = [],
			sumTotalList = [],
			sumTotalPercentList = [];

		for(var i = 0; i < len; i++) {
			var sumTotal = list[i].EChartsVO_SumTotal;
			if(!sumTotal) sumTotal = 0;
			sumTotalList.push(parseFloat(sumTotal));
			var sumTotalPercent = list[i].EChartsVO_SumTotalPercent;
			if(!sumTotalPercent) sumTotalPercent = 0;
			sumTotalPercentList.push(parseFloat(sumTotalPercent).toFixed(2));
			if(me.ClassType == "goodsclass") {
				axisData.push(list[i].EChartsVO_GoodsClass);
			} else {
				axisData.push(list[i].EChartsVO_GoodsClassType);
			}
		}
		var colors = ['#5793f3', '#d14a61', '#675bba'];
		var config = {
			title: {
				'text': me.title,
				'subtext': '数据来自LRMP'
			},
			tooltip: { //提示框组件
				trigger: 'axis',
				axisPointer: { // 坐标轴指示器，坐标轴触发有效
					type: 'shadow' // 默认为直线，可选为：'line' | 'shadow'
				}
			},
			legend: { //图例组件
				data: ['采购金额', '采购金额占比']
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
						type: ['line', 'bar']//, 'stack', 'tiled'
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
			series: [{ //系列列表
				name: '采购金额',
				type: 'bar',
				barMaxWidth: me.barMaxWidth,
				barMinHeight: me.barMinHeight,
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
			}]
		};
		var series2 = {
			name: '采购金额占比',
			type: me.percentType, //'bar',
			barMaxWidth: me.barMaxWidth,
			barMinHeight: me.barMinHeight,
			itemStyle: {
				normal: {
					label: {
						show: true,
						position: 'insideRight',
						formatter: '{c}%'
					}
				}
			},
			data: sumTotalPercentList
		};
		var axis2 = [{
			type: 'value',
			name: '金额(元)',
			min: 0.00001,
			position: 'left',
			axisLine: {
				lineStyle: {
					color: colors[0]
				}
			}
		}, {
			type: 'value',
			name: '金额占比(%)',
			position: 'right',
			min: 0.00001,
			//max: 100,
			position: 'right',
			//offset: 80,
			axisLine: {
				lineStyle: {
					color: colors[1]
				}
			}
		}];
		if(me.yAxisValue == "goodsclass") {
			series2.xAxisIndex = 1;
			config.xAxis = axis2;
			config.yAxis = [{
				type: 'category',
				data: axisData
			}];
		} else {
			series2.yAxisIndex = 1;
			config.xAxis = [{
				type: 'category',
				data: axisData
			}];
			config.yAxis = axis2;
		}
		config.series.push(series2);
		me.changeChart(config, true);
	},
	//加载数据
	loadData: function(yAxis, data) {
		var me = this;
		me.yAxisValue = yAxis;
		me.changeData(data);
	}
});