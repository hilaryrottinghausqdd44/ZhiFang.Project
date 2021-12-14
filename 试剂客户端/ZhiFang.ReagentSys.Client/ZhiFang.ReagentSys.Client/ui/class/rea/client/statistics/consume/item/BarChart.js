/**
 * 项目（实际）检测量
 * 从Lis系统获取数据,统计每台仪器每个项目实际检测量
 * @author longfc	
 * @version 2019-02-22
 */
Ext.define('Shell.class.rea.client.statistics.consume.item.BarChart', {
	extend: 'Shell.ux.chart.EChartsPanel4',

	layout: 'fit',
	title: '实际检测量',
	/**查询服务*/
	selectUrl: '/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchLisResultsEChartsVOByHql?isPlanish=true',
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
	/**统计类型:仪器试剂使用量:1;*/
	statisticType: 1,
	/**选择仪器的IDStr*/
	equipIDStr: null,
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
		var cbsShowZero = false; //me.getComponent('buttonsToolbar1').getComponent('cbsShowZero').getValue();
		params.push("statisticType=" + me.statisticType);
		params.push("showZero=" + cbsShowZero);
		//params.push("fields=EChartsVO_ReaCompanyID,EChartsVO_ReaCompCode,EChartsVO_ReaCompanyName,EChartsVO_AllSumTotal,EChartsVO_SumTotal,EChartsVO_SumTotalPercent");
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
	/**
	 * 更改图表
	 * @param {Object} list 格式[{'ReaCompanyName':'aaa','SumTotal':10,'SumTotalPercent':'10%'}]
	 */
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
			legend: {
				data: ["常规检测量", "复检检测量", "质控检测量"],
				itemGap: 5
			},
			xAxis: {
				type: 'category',
				axisLabel: {
					interval: 0,
					rotate: 20
				}
			},
			yAxis: {},
			grid: {
				top: '6%',
				left: '1%',
				right: '2%',
				bottom: '8%',
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
			series: [{
					name: '常规检测量',
					stack: '总量',
					barMaxWidth:me.barMaxWidth,
					type: 'bar'
				},
				{
					name: '复检检测量',
					stack: '总量',
					barMaxWidth:me.barMaxWidth,
					type: 'bar'
				},
				{
					name: '质控检测量',
					stack: '总量',
					barMaxWidth:me.barMaxWidth,
					type: 'bar'
				}
			]
		};
		config.dataset = dataset;
		me.changeChart(config, true);
	}
});