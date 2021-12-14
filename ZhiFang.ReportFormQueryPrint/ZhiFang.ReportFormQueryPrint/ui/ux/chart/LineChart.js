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
	hasAverageLine:true,
	/**是否有最大值和最小值标识*/
	hasMaxAndMin:true,
	
	chartConfig:{
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
	    yAxis:[{type:'value'}]
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
   		}
   		
		me.callParent([config,bo]);
	}
});