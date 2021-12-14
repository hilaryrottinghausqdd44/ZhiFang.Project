/**
	@ mept.qualityindicator.qiTypeEChart 质量指标类型图表
	@author：longfc
	@version 2019-05-24
 */
layui.extend({
	//uxutil: 'ux/util',
	//echarts: 'src/echarts/echarts'
	//echarts: 'admin/layuiadmin/lib/extend/echarts'
}).define(["uxutil", "layer", "echarts"], function (exports) {
	"use strict";

	var $ = layui.$;
	var uxutil = layui.uxutil;
	var layer = layui.layer;
	var echarts = layui.echarts;

	var qiTypeEChart = {
		config: {
			/**图表所在的div*/
			elem: null,
			/**统计结果分类:LStatTotalClassificatione的Id值;质量指标类型值为1*/
			classificationId: "1",
			/**指标类型所属分类:QualityIndicatorType的Id值*/
			qitype: "",
			/**指标类型字典(PhrasesWatchClass)Id值*/
			pwclassid: "",
			/**统计日期类型:LStatTotalStatDateType的Id值*/
			statDateType: "",
			/**统计维度:各质量指标分类类型对应的统计维度的Id值*/
			sadimension: "",
			/**开始日期*/
			startDate: "",
			/**结束日期 */
			endDate: "",
			/**查询结果*/
			result: null,
			/**对应的echart的数据*/
			echartData: null,
			/**对应的echart的Option*/
			echartOptions: null,
			/**对应的echart对象*/
			echart: null,
			/**load处理后的回调*/
			afterLoad: function (data) {

			}
		}
	};
	//质量指标-质量指标类型-统计日期类型
	var LStatTotalStatDateTypeStrategy = function () {
		//内部算法集合封装
		var strategy = {
			day: function () {
				return 1;
			},
			month: function () {
				return 2;
			},
			year: function () {
				return 3;
			},
			quarter: function () {
				return 4;
			}
		};
		//调用接口
		return function (algorithm) {
			return strategy[algorithm] && strategy[algorithm]();
		}
	}();
	//质量指标-质量指标类型-查询类型
	var QualityIndicatorSearchTypeStrategy = function () {
		//内部算法集合封装
		var strategy = {
			month: function () {
				return 1;
			},
			quarter: function () {
				return 2;
			},
			year: function () {
				return 3;
			},
			lastMonth: function () {
				return 4;
			},
			curMonth: function () {
				return 5;
			},
			dateRange: function () {
				return 6;
			}
		};
		//调用接口
		return function (algorithm) {
			return strategy[algorithm] && strategy[algorithm]();
		}
	}();
	//质量指标-质量指标分类类型
	var QualityIndicatorTypeStrategy = function () {
		//内部算法集合封装
		var strategy = {
			//标本类型错误率
			sampleTypeSADimension: function () {

			},
			//标本容器错误率
			sTContainerErrorSADimension: function () {

			},
			//标本采集量错误率
			sTCollectionErrorSADimension: function () {

			},
			//血培养污染统计
			bloodCulturePollutionSADimension: function () {

			}
		};
		//调用接口
		return function (algorithm) {
			return strategy[algorithm] && strategy[algorithm]();
		}
	}();
	//EChartData处理
	var EChartDataStrategy = function () {
		//内部算法集合封装
		var strategy = {
			legendData: function (inst, result) {
				var legendData = result.legendData || ["不合格占比", "错误数", "总数"];
				return legendData;
			},
			xAxisData: function (inst, result) {
				var xAxisData = result.axisData;
				//测试数据
				if (xAxisData.length <= 0)
					xAxisData = ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"];
				return xAxisData;
			},
			seriesData: function (inst, result) {
				var data = {
					"failedAmount": [],
					"failedTotal": [],
					"specimenTotal": [],
					"failedRate": []
				};
				if (result.seriesData) data = result.seriesData;
				if(!data.failedAmount)data.failedAmount=[];
				if(!data.failedTotal)data.failedTotal=[];
				if(!data.specimenTotal)data.specimenTotal=[];
				if(!data.failedRate)data.failedRate=[];
				var seriesData = {
					//不合格标本数(错误数)
					failedAmount: data.failedAmount.length>0?data.failedAmount:[9, 12, 15, 6, 13, 50, 45, 11, 15, 17, 25, 10],
					//不合格标本总数()
					failedTotal: data.failedTotal.length>0?data.failedTotal: [90, 120, 150, 60, 10, 50, 90, 110, 110, 170, 20, 100],
					//标本总量()
					specimenTotal: data.specimenTotal.length>0?data.specimenTotal: [900, 1200, 1500, 600, 130, 500, 930, 110, 150, 170, 250, 100],
					//不合格占比
					failedRate: data.failedRate.length>0?data.failedRate:[9, 12, 15, 6, 13, 50, 45, 11, 15, 17, 25, 10]
				};

				return seriesData;
			}
		};
		//调用接口
		return function (algorithm, inst, result) {
			return strategy[algorithm] && strategy[algorithm](inst, result);
		}
	}();
	//构造器
	var Class = function (options) {
		var me = this;
		me.config = $.extend({}, me.config, qiTypeEChart.config, options);
		return me.render();
	};
	Class.pt = Class.prototype;
	//默认配置
	Class.pt.config = {
		fields: "",
		lastData: "",
		selectUrl: uxutil.path.ROOT +
			"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSPSAQualityIndicatorTypeOfEChart"
	};
	//获取查询Fields
	Class.pt.getFields = function (isString) {
		var me = this;
		return me.config.fields;
	};
	//获取查询参数
	Class.pt.geSearhParam = function (inst) {
		var me = this;
		var options = inst || me.config;
		var arrHql = [];

		if (options.classificationId) {
			arrHql.push('classificationId=' + options.classificationId);
		}
		if (options.qitype) {
			arrHql.push('qitype=' + options.qitype);
		}
		if (options.statDateType) {
			arrHql.push('statDateType=' + options.statDateType);
		}
		if (options.sadimension) {
			arrHql.push('sadimension=' + options.sadimension);
		}
		if (options.startDate) {
			arrHql.push('startDate=' + options.startDate);
		}
		if (options.endDate) {
			arrHql.push('endDate=' + options.endDate);
		}
		var fields = me.getFields(true);
		if (fields) {
			arrHql.push('fields=' + fields);
		}
		return arrHql;
	};
	Class.pt.load = function (inst) {
		var me = this;
		inst = inst || me.config;
		var qitype = inst.qitype;
		if (!qitype) return inst;

		//先清空数据及图表
		inst.result = {};
		inst.echart = null;
		inst.echartData = null;

		var afterLoad = me.config.afterLoad;
		var url = me.config.selectUrl;
		var arrHql = me.geSearhParam(inst);
		if (arrHql && arrHql.length > 0) {
			arrHql = arrHql.join("&");
		} else {
			arrHql = "";
		}

		url += (url.indexOf('?') == -1 ? '?' : '&') + arrHql;
		uxutil.server.ajax({
			url: url
		}, function (data) {
			var result = "";
			if (data.success) {
				//console.log(data);
				inst = me.changeData(inst, data.value || {});
				inst = me.initEChart(inst);
				return inst;
			} else {
				//清空图表
				inst = me.clear(inst);
				return inst;
			}
			if (afterLoad) {
				afterLoad(result);
				return inst;
			}
		});

	};
	//转换获取的数据
	Class.pt.changeData = function (inst, result) {
		var me = this;
		inst.echartData = {
			legendData: [],
			xAxisData: [],
			seriesData: []
		};
		//Data处理
		var legendData = [], xAxisData = [], seriesData = [];
		legendData = EChartDataStrategy("legendData", inst, result);
		xAxisData = EChartDataStrategy("xAxisData", inst, result);
		seriesData = EChartDataStrategy("seriesData", inst, result);
		inst.echartData = {
			legendData: legendData,
			xAxisData: xAxisData,
			seriesData: seriesData
		};

		inst.result = result;
		return inst;
	};
	//清空
	Class.pt.clear = function (inst) {
		var me = this;
		//先清空数据及图表
		inst.result = {};
		inst.echart = null;
		inst.echartData = null;

		return inst;
	};
	//获取EChart的Data
	Class.pt.getECData = function (inst, result) {
		var me = this;

		var colors = ['#d14a61', '#FF5722', '#675bba'];
		var echartOptions = [{
			tooltip: {
				trigger: "axis"
			},
			barMaxWidth: 20,
			grid: {
				top: '25',//"12%"
				left: '1%',
				right: '2%',
				bottom: '2%',
				containLabel: true
			},
			legend: {
				//top:"top",
				data: inst.echartData.legendData || []
			},
			xAxis: [{
				type: "category",
				data: inst.echartData.xAxisData || []
			}],
			yAxis: [{
				type: "value",
				name: "",
				axisLabel: {
					formatter: "{value}"
				}
			}, {
				type: "value",
				name: "",
				position: 'right',
				axisLabel: {
					formatter: "{value}%"
				}
			}],
			series: [{
				name: "不合格占比",
				type: "line",
				yAxisIndex: 1,
				// min: 0,
				// max: 100,
				data: inst.echartData.seriesData["failedRate"] || []
			}, {
				name: '错误数',
				type: 'bar',
				stack: '总量',
				data: inst.echartData.seriesData["failedAmount"] || [],
				axisLine: {
					lineStyle: {
						color: colors[0]
					}
				}
			}, {
				name: '总数',
				type: 'bar',
				stack: '总量',
				data: inst.echartData.seriesData["specimenTotal"] || [],
				axisLine: {
					lineStyle: {
						color: colors[2]
					}
				}
			}]
		}];
		//如果纵坐标数组长度大于8,需要缩放功能组件
		var len = inst.echartData.xAxisData.length || 0;
		//console.log(len);
		if (len > 12) {
			echartOptions[0].grid.bottom = "15%";
			echartOptions[0].dataZoom =
				[{
					type: 'slider', //图表下方的伸缩条
					show: true,
					//height: '20px',
					start: 1,
					end: 10
				},
				{
					type: 'inside',
					//height: '6px',
					start: 1,
					end: 10
				}
				];
		}
		inst.echartOptions = echartOptions;
		return inst;
	};
	//初始化某一EChart
	Class.pt.initEChart = function (inst) {
		var me = this;
		var myEchartDiv = $("#" + inst.elem).children("div");
		inst = me.getECData(inst);
		var myEchart = layui.echarts.init(myEchartDiv[0]);//, layui.echartsTheme

		myEchart.setOption(inst.echartOptions[0]);
		window.onresize = myEchart.resize;
		inst.echart = myEchart;
		return inst;
	};
	//初始化
	Class.pt.init = function (inst) {
		var me = this;
		inst = me.load(inst);
		//inst = me.initEChart(inst);
		return inst;
	};
	//初始化
	Class.pt.render = function (options) {
		var me = this;
		if (options) me.config = $.extend({}, me.config, options);
		var inst = new $.extend({}, Class.pt, me.config);
		inst = me.init(inst);
		//后续是否考虑缓存inst
		return inst;
	};
	//核心入口
	qiTypeEChart.render = function (options) {
		var me = this;
		var inst = new Class(options);
		return inst;
	};

	//暴露接口
	exports('qiTypeEChart', qiTypeEChart);
});