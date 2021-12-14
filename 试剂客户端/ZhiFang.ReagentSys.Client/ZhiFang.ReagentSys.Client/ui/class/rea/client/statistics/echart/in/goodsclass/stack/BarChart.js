/**
 * 堆叠条形图:按货品一级分类(堆叠为二级分类)
 * @author longfc
 * @version 2019-02-21
 */
Ext.define('Shell.class.rea.client.statistics.echart.in.goodsclass.stack.BarChart', {
	extend: 'Shell.ux.chart.EChartsPanel4',

	layout: 'fit',
	title: '入库统计-按货品分类',
	/**纵坐标选择值*/
	yAxisValue: "goodsclass",

	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**
	 * 更改图表
	 * @param {Object} list 格式[{'ReaCompanyName':'aaa','SumTotal':10,'SumTotalPercent':'10%'}]
	 */
	changeData: function(data) {
		var me = this;
		if(!data){
			me.clearData();
			return;
		}
		var legendData = data.legend.data|| [];
		if(!legendData){
			me.clearData();
			return;
		}
		var config = {
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
						show: true,
						readOnly: false
					},
					restore: {
						show: true
					},
					saveAsImage: {
						show: true
					}
				}
			},
			grid: {
				left: '3%',
				right: '4%',
				bottom: '3%',
				containLabel: true
			},
			legend: { //图例组件
				data: legendData
			},
			calculable: true
		};
		//yAxis
		var axisData = data.axis.data;
		if(!axisData) axisData = ["未知"];

		if(me.yAxisValue == "goodsclass") {
			config.xAxis = {
				type: 'category',
				data: axisData
			};
			config.yAxis = {
				type: 'value',
				min: 0.00001
			};
		} else {
			config.yAxis = {
				type: 'category',
				min: 0.00001,
				data: axisData
			};
			config.xAxis = {
				//min: 0.00001,
				type: 'value'
			};
		}

		//series
		var series = [];
		var list = data.series;
		if(!list) {
			list = [];
			var jseries={
				"name": "未知",
				"type":"bar",
				"stack":"总量"
			};
			list.push(jseries);
		}
		var itemStyle = {
			normal: {
				label: {
					show: true,
					position: 'insideRight',
					formatter: '{c}元'
				}
			}
		};
		for(var i = 0; i < list.length; i++) {
			var jseries = list[i];
			jseries.itemStyle = itemStyle;
			jseries.type = 'bar';
			jseries.stack = '总量';
			//jseries.min=0.00001;
			//jseries.barMinHeight=me.barMinHeight;
			jseries.barMaxWidth=me.barMaxWidth;
			series.push(jseries);
		}
		config.series = series;
		me.changeChart(config, true);
	},
	//加载数据
	loadData: function(yAxis, data) {
		var me = this;
		me.yAxisValue = yAxis;
		me.changeData(data);
	}
});