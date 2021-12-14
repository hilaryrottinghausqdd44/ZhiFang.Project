/**
 * 折线图-柱状图类
 * @author wyz
 * @version 2021-06-25
 */
Ext.define('Shell.ux.chart.LineChartNew',{
	extend:'Shell.ux.chart.EChartsNew',
	alias:'widget.uxlinechart',
	
	/**默认的显示类型 bar,line*/
	type:'line',
	/**是否有平均值线*/
	hasAverageLine:false,
	/**是否有最大值和最小值标识*/
	hasMaxAndMin:false,
	chartConfig:{
		//color:['rgba(68, 136, 187)','#2f4554', '#61a0a8', '#d48265', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3'],
		tooltip:{trigger:'axis'},
		toolbox:{
	        show:true,
	        feature:{
	            mark:{show:true},
	            //dataView:{show:true,readOnly:false},
	            magicType:{show:true,type:['line','bar','stack','tiled']},
	            restore:{show:true},
	            saveAsImage:{show:true}
	        }
	    },
	    //calculable:true,
	    yAxis:[{type:'value',max: function(value) {return value.max;}}],
	},
	/**更改图表*/
	changeChart: function (config, bo) {
		var me = this,
			markPoint = { data: [{ type: 'max', name: '最大值' }, { type: 'min', name: '最小值' }] },
			//markLine = { data: [{ type: 'average', name: '平均值' }] },
			series = config.series || [],
			length = series.length;
		for (var i = 0; i < length; i++) {
			config.series[i].type = me.type;
			if (me.hasMaxAndMin) config.series[i].markPoint = markPoint;
			if (me.hasAverageLine) config.series[i].markLine = markLine;
			//参考值的两条横线
			if (config.series[i].RefRange.length > 0) {
				var range = config.series[i].RefRange[i];
				//判断参考范围是某个区间，再做相应处理
				if (range.indexOf("-") != -1) {
					var rangeList = range.split("-");
					var min = Number(rangeList[0]);
					var max = Number(rangeList[1]);
					config.yAxis[0].max = function (value) {
						var maxNum = value.max;
						maxNum = (Number(maxNum) - max) > 0 ? maxNum : max;
						return maxNum;
					}
					config.series[i].markLine = {
						data: [
							{
								yAxis: min, name: '参考限值1',
								lineStyle: {
									type: 'dashed',
									color: '#102b6a'
								},
								label: {
									//将警示值放在哪个位置，三个值“start”,"middle","end"  开始  中点 结束
									position: "end",
									formatter: "参考限值" + min//基准线的显示文字
								}
							},
							{
								yAxis: max, name: '参考限值2',
								lineStyle: {
									type: 'dashed',
									color: '#d71345'
								},
								label: {
									//将警示值放在哪个位置，三个值“start”,"middle","end"  开始  中点 结束
									position: "end",
									formatter: "参考限值" + max//基准线的显示文字
								}
							}
						]
					};
				}
				//判断参考范围是大于某个值或小于某个值，再做相应处理
				if (range.indexOf(">") != -1 || range.indexOf("<") != -1) {
					range = range.replace(">", "").replace("<", "");
					config.yAxis[0].max = function (value) {
						var maxNum = value.max;
						maxNum = (Number(maxNum) - Number(range)) > 0 ? maxNum : range;
						return maxNum;
					};
					config.series[i].markLine = {
						data: [
							{
								yAxis: range, name: '参考限值' + range,
								lineStyle: {
									type: 'dashed',
									color: '#d71345'
								},
								label: {
									//将警示值放在哪个位置，三个值“start”,"middle","end"  开始  中点 结束
									position: "end",
									formatter: "参考限值" + range//基准线的显示文字
								}
							}
						]
					};
				}

			}
			   config.series[i].itemStyle={ normal: {color:'#ff9277',lineStyle:{color: 'rgba(68, 136, 187)'},label : {show: true}}};
			   
   		}
		me.callParent([config,bo]);
	}
});