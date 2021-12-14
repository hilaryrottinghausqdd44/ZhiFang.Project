/**
 * 图类
 * @author Jcall
 * @version 2014-08-27
 */
Ext.define('Shell.ux.chart.ECharts',{
	extend:'Ext.Component',
	alias:'widget.uxecharts',
	
	/**是否带边框*/
	hasDivBoder:false,
	
	/**图信息*/
	chartConfig:{},
	chart:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.on({
			boxready:function(){me.createHtml();},
			resize:function(){if(me.chart){me.chart.resize();}}
		});
	},
	/**创建HTML*/
	createHtml:function(){
		var me = this,
			id = me.getChartId(),
			width = me.getWidth(),
			height = me.getHeight(),
			html = "<div id='" + id + "' style='width:100%;height:100%;border:" + 
				(me.hasDivBoder ? "1px" : "0") + " solid #ccc;padding:10px;'></div>";
			
		me.update(html);
	},
	/**创建图表*/
	createChart:function(config){
		var me = this,
			id = me.getChartId(),
			chartConfig = me.chartConfig,
			requireId = ['echarts','echarts/chart/bar'];
			
		require.config({
		    paths:{
		        'echarts':Shell.util.Path.uiPath+'/echarts/echarts',
		        'echarts/chart/bar':Shell.util.Path.uiPath+'/echarts/echarts'
		    }
		});
		require(requireId,function(ec){
	        // 基于准备好的dom，初始化echarts图表
	        me.chart = ec.init(document.getElementById(id)); 
	        // 为echarts对象加载数据 
	        me.chart.setOption(Ext.apply(chartConfig,config)); 
	    });
	},
	/**获取图的ID*/
	getChartId:function(){
		return this.getId() + '-chart';
	},
	/**改变DIV大小*/
	changeDivSize:function(){
		var me = this,
			id = me.getChartId(),
			width = me.getWidth(),
			height = me.getHeight(),
			div = document.getElementById(id);
			
		div.setSize(width,height);
	},
	/**更改图表*/
	changeChart:function(config,bo){
		if(this.chart){
			this.chart.setOption(config,bo);
		}else{
			this.createChart(config);
		}
	},
	
	/**清空数据*/
	clearData:function(){
		this.chart && this.chart.clear();
	}
});