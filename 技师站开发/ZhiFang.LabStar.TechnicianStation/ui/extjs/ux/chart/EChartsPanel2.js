/**
 * echart图类(版本基于echart 4.2.1)
 * @author longfc
 * @version 2019-02-27
 */
Ext.define('Shell.ux.chart.EChartsPanel2', {
	extend: 'Ext.panel.Panel',
	alias: 'widget.uxechartspanel',
	mixins: ['Shell.ux.Langage'],

	/**
	 * 柱状图的最小高度
	 * 柱条最小高度，可用于防止某数据项的值过小而影响交互。
	 * */
	barMinHeight: 15,
	/**
	 * 柱状图的最大宽度
	 * 在同一坐标系上，此属性会被多个 'bar' 系列共享。此属性应设置于此坐标系中最后一个 'bar' 系列上才会生效，并且是对此坐标系中所有 'bar' 系列生效。
	 * */
	barMaxWidth:35,
	/**是否带边框*/
	hasDivBoder: false,
	/**默认创建图表*/
	defaultCreateChart: true,
	/**图信息*/
	chartConfig: {},
	chart: null,
	/**错误信息样式*/
	errorFormat: '<div style="color:red;text-align:center;margin:15px;font-weight:bold;">{msg}</div>',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			boxready: function() {
				me.createHtml();
				if(me.defaultCreateChart) {
					me.createChart();
				}
			},
			resize: function() {
				if(me.chart) {
					me.chart.resize();
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		//me.createHtml();
		me.callParent();
	},
	/**创建HTML*/
	createHtml: function() {
		var me = this,
			id = me.getChartId(),
			html =
			"<div id='" + id + "' style='width:100%;height:100%;border:" +
			(me.hasDivBoder ? "1px" : "0") + " solid #ccc;'></div>";

		me.update(html);
	},
	/**创建图表*/
	createChart: function(config) {
		var me = this,
			id = me.getChartId(),
			con = Ext.clone(config) || {},
			chartConfig = me.chartConfig;
		var divChart = document.getElementById(id);
		if(divChart) {
			// 基于准备好的dom，初始化echarts图表
			me.chart = echarts.init(divChart);
			// 为echarts对象加载数据
			me.chart.setOption(Ext.apply(chartConfig, con));
		} else {
			me.createHtml();
			divChart = document.getElementById(id);
			if(!me.chart && divChart) {
				me.chart = echarts.init(divChart);
				// 为echarts对象加载数据
				me.chart.setOption(Ext.apply(chartConfig, con));
			}
		}
	},
	/**获取图的ID*/
	getChartId: function() {
		return this.getId() + '-chart';
	},
	/**获取徒错误信息的ID*/
	getChartErrorId: function() {
		return this.getChartId() + '-error';
	},
	/**改变DIV大小*/
	changeDivSize: function() {
		var me = this,
			id = me.getChartId(),
			width = me.getWidth(),
			height = me.getHeight(),
			divChart = document.getElementById(id);
		if(!divChart) {
			me.createHtml();
			divChart = document.getElementById(id);
		}
		divChart.setSize(width, height);
	},
	/**更改图表*/
	changeChart: function(config, bo) {
		var me = this;
		if(me.chart) {
			var divChart = document.getElementById(me.getChartId());
			if(!divChart) me.createHtml();
			me.chart.setOption(config, bo);
		} else {
			me.createChart(config);
		}
	},
	/**清空数据*/
	clearData: function() {
		var me = this;
		if(me.chart) me.chart.clear();
	},
	/**显示错误信息*/
	showErrror: function(msg) {
		var me = this,
			divChart = document.getElementById(me.getChartId());
		if(me.chart) me.chart.clear();
		var error = me.errorFormat.replace(/{msg}/, msg);
		if(divChart) {
			//divChart.innerHTML = error;
		}
	}
});