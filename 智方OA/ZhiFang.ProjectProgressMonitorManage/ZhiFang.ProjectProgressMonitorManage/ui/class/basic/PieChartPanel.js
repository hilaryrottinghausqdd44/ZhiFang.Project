/**
 * 饼图面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.basic.PieChartPanel',{
	extend:'Shell.class.basic.ResizePanel',
	layout:'fit',
	
	/**服务地址*/
	selectUrl:'',
	/**名称字段*/
    nameField:'',
    /**值字段*/
    valueField:'',
    /**弹出框标题*/
    TooltipTitle:'',
    
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    },
    initComponent:function(){
    	var me = this;
    	me.items = me.createItems();
    	me.callParent(arguments);
    },
    /**创建内部组件*/
    createItems:function(){
    	var me = this;
    	me.chart =  Ext.create('Shell.ux.chart.PieChart');
    	return me.chart;
    },
    /**执行加载*/
    onRefresh:function(){
    	var me = this;
    	var url = JShell.System.Path.ROOT + me.selectUrl;
    	url = me.changeUrl(url);
    	me.chart.clearData();
    	JShell.Server.get(url,function(data){
    		if(data.success){
    			var value = me.changeResult(data.value);
    			me.loadData(value.list);
    		}else{
    			me.chart.showErrror(data.msg);
    		}
    	});
    },
    /**图表赋值*/
	setData:function(data,option){
		var me = this,
			list = data || [],
			len = list.length;
			
		var config = {
		    title:{x:'center'},
		    legend:{orient:'vertical',x:'left',data:[]},
		    series:[{
	            name:me.TooltipTitle,type:'pie',radius :'55%',
	            center:['50%','50%'],data:[]
	        }]
		};
		for(var i in option){
			config[i] = option[i];
		}
		
		var legendData = [];
		for(var i=0;i<len;i++){
			legendData.push(list[i].name);
		}
		config.legend.data = legendData;
		config.series[0].data = list;
		
		me.chart.changeChart(Ext.apply(me.chart.chartConfig,config));
	},
	/**更新数据 @public*/
	loadData:function(data){
   		var me = this,
   			list = data || [],
   			len = list.length,
   			arr = [];
   		
   		for(var i=0;i<len;i++){
   			arr.push({
   				name:list[i][me.nameField],
   				value:list[i][me.valueField]
   			});
   		}
   		me.setData(arr);
  	},
  	/**更改返回值 @可以根据需要重写*/
    changeResult:function(data){
    	return data;
   	},
   	/**更改URL @可以根据需要重写*/
   	changeUrl:function(url){
   		return url;
   	}
});