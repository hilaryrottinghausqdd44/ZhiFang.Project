/**
 * 折线图-柱状图类
 * @author Jcall
 * @version 2014-08-27
 */
Ext.define('Shell.ux.chart.LineChart',{
	extend:'Shell.ux.chart.ECharts',
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
	changeChart:function(config,bo){
		var me = this,
			markPoint = {data:[{type:'max',name:'最大值'},{type:'min',name:'最小值'}]},
           	markLine = {data:[{type:'average',name:'平均值'}]},
			series = config.series || [],
			length = series.length;
   		for(var i=0;i<length;i++){
			   config.series[i].type = me.type;
   			if(me.hasMaxAndMin) config.series[i].markPoint = markPoint;
			   if(me.hasAverageLine) config.series[i].markLine = markLine;
			   config.series[i].itemStyle={ normal: {color:'#ff9277',lineStyle:{color: 'rgba(68, 136, 187)'},label : {show: true}}};
			   
   		}
		me.callParent([config,bo]);
	}
});