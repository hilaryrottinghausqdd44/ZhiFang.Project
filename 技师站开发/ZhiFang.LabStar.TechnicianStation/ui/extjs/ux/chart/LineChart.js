/**
 * 折线图类
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.chart.LineChart',{
	extend:'Shell.ux.chart.ECharts',
	
	/**图信息*/
	chartConfig:{
		calculable:true,//拖拽重计算特性
		grid:{x:60,y:70,x2:60,y2:50},
		tooltip:{trigger:'axis'},
	    toolbox:{
	        show:true,
	        feature:{
	            mark:{show:true},
	            dataZoom:{show:true},//区域缩放
	            dataView:{show:true,readOnly:false},
	            magicType:{show:true,type:['line','bar']},
	            restore:{show:true},
	            saveAsImage:{show:true}
	        }
	    }
	}
});