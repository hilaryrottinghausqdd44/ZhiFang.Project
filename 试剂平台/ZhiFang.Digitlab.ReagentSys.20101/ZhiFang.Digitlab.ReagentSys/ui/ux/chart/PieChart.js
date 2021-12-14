/**
 * 饼图类
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.chart.PieChart',{
	extend:'Shell.ux.chart.ECharts',
	
	/**图信息*/
	chartConfig:{
		calculable:true,//拖拽重计算特性
		tooltip:{
	        trigger:'item',
	        formatter:'<center>{a}</center>{b} : {c} ({d}%)'
		},
	    toolbox:{
	        show:true,
	        feature:{
	            //mark:{show: true},
	            //dataView:{show: true, readOnly: false},
	            //magicType:{show:true,type:[]},
	            restore:{show:true},
	            saveAsImage:{show:true}
	        }
	    }
	}
});