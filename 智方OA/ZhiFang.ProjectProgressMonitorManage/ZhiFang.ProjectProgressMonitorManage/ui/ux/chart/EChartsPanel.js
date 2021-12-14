/**
 * 图类
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.chart.EChartsPanel',{
	extend:'Ext.panel.Panel',
	alias:'widget.uxechartspanel',
	mixins:['Shell.ux.Langage'],
	
	/**是否带边框*/
	hasDivBoder:false,
	/**默认创建图表*/
	defaultCreateChart:true,
	/**图信息*/
	chartConfig:{},
	chart:null,
	/**错误信息样式*/
	errorFormat:'<div style="color:red;text-align:center;margin:15px;font-weight:bold;">{msg}</div>',
	
	/**图表按需加载类型*/
	requireId:[],
	/**图表按需加载路径*/
	requirePaths:{},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.on({
			boxready:function(){
				me.createHtml();
				
				if(me.defaultCreateChart){
					me.createChart();
				}
			},
			resize:function(){
				if(me.chart){
					me.chart.resize();
				}
			}
		});
	},
	/**创建HTML*/
	createHtml:function(){
		var me = this,
			id = me.getChartId(),
			errorId = me.getChartErrorId(),
			width = me.getWidth(),
			height = me.getHeight(),
			html = 
			"<div id='" + id + "' style='width:100%;height:100%;border:" +
			(me.hasDivBoder ? "1px" : "0") + " solid #ccc;'></div>";
			
		me.update(html);
	},
	/**创建图表*/
	createChart:function(config){
		var me = this,
			id = me.getChartId(),
			con = Ext.clone(config) ||{},
			chartConfig = me.chartConfig;
			
		require.config({paths:me.requirePaths});
		
		require(me.requireId,function(ec,theme){
	        var dom = document.getElementById(id);
	        if(dom){
	        	// 基于准备好的dom，初始化echarts图表
	        	me.chart = ec.init(dom,theme);
		        // 为echarts对象加载数据
		        me.chart.setOption(Ext.apply(chartConfig,con));
	        }
	    });
	},
	/**获取图的ID*/
	getChartId:function(){
		return this.getId() + '-chart';
	},
	/**获取徒错误信息的ID*/
	getChartErrorId:function(){
		return this.getChartId() + '-error';
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
		var me = this;
		
		if(me.chart){
			me.chart.setOption(config,bo);
		}else{
			me.createChart(config);
		}
	},
	
	/**清空数据*/
	clearData:function(){
		if(this.chart){
			this.chart.clear();
		}
	},
	/**显示错误信息*/
	showErrror:function(msg){
		var me = this,
			id = me.getChartId(),
			div = document.getElementById(id);
			
    	me.chart = null;
		var error = me.errorFormat.replace(/{msg}/,msg);
		//div.innerHTML = error;
		if(div){
			div.innerHTML = error;
		}
	}
});