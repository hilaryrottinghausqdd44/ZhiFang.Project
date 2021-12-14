/**
 * 理论消耗量 -按项目试剂图表
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.statistics.consume.theory.TestItemChart', {
	extend: 'Shell.ux.chart.EChartsPanel4',

	layout: 'fit',
	title: '按项目试剂图表',
	/**纵坐标选择值*/
	yAxisValue: "sumtotal",
	/**查询服务*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearChconsumeTheoryEChartsVOByHql?isPlanish=true',
	/**当前查询日期范围*/
	dateArea: null,
	/**统计类型:按试剂项目图表:1;按项目试剂图表:2;*/
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
		params.push("isMergeOfItem=" +false);
		if(me.equipIDStr)
			params.push("equipIDStr=" + me.equipIDStr);
		if(me.goodsIdStr)
			params.push("goodsIdStr=" + me.goodsIdStr);
		if(me.dateArea) {
			if(me.dateArea.start) {
				params.push("startDate=" + JShell.Date.toString(me.dateArea.start, true));
			}
			if(me.dateArea.end) {
				params.push("endDate=" + JShell.Date.toString(me.dateArea.end, true) + "");
			}
		}
		if(params && params.length > 0) {
			params = params.join("&");
		} else {
			params = "";
		}
		if(params) url = url + "&" + params; // (url.indexOf('?') == -1 ? '?' : '&')
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.afterDataLoad(data.value);
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
	changeData: function(data) {
		var me = this;
		if(!data) {
			me.clearData();
			return;
		}
		var dataset = data.dataset || [];
		if(!dataset || dataset.source <= 0) {
			me.clearData();
			//me.showErrror("获取统计数据信息为空!");
			return;
		}
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
			xAxis: {
				type: 'category',
				axisLabel: {
					interval: 0,
					rotate: 20
				}
			},
			yAxis: [{
				type: 'value',
				min: 0,
				name: '消耗量'
			}, {
				type: 'value',
				name: '金额',
				min: 0,
				axisLabel: {
					formatter: '{value}元'
				}
			}],
			grid: {
				top: '8%',
				left: '1%',
				right: '2%',
				bottom: '6%',
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
					end: 50
				}
			],
			series: [{
					name: '常规消耗',
					barMaxWidth:me.barMaxWidth,
					barMinHeight: me.barMinHeight,
					type: 'bar'
				},
				{
					name: '复检消耗',
					barMaxWidth:me.barMaxWidth,
					barMinHeight: me.barMinHeight,
					type: 'bar'
				},
				{
					name: '质控消耗',
					barMaxWidth:me.barMaxWidth,
					barMinHeight: me.barMinHeight,
					type: 'bar'
				}, {
					name: '常规金额',
					yAxisIndex: 1,
					barMaxWidth:me.barMaxWidth,
					barMinHeight: me.barMinHeight,
					type: 'bar'
				},
				{
					name: '复检金额',
					yAxisIndex: 1,
					barMaxWidth:me.barMaxWidth,
					barMinHeight: me.barMinHeight,
					type: 'bar'
				},
				{
					name: '质控金额',
					yAxisIndex: 1,
					barMaxWidth:me.barMaxWidth,
					barMinHeight: me.barMinHeight,
					type: 'bar'
				}
			]
		};
		config.dataset = dataset;
		config.legend = {
			data: ["常规消耗", "复检消耗", "质控消耗", "常规金额", "复检金额", "质控金额"]
		};
		me.changeChart(config, true);
	}
});