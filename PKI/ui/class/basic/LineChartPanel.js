/**
 * 柱状图面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.basic.LineChartPanel',{
	extend:'Shell.class.basic.ResizePanel',
	layout:'fit',
	
	/**服务地址*/
	selectUrl:'',
	/**线名称字段*/
	lineField:'LineName',
	/**默认线名称*/
	defaultLineName:'数据',
	/**X轴字段*/
    xField:'X',
    /**Y轴字段*/
    yField:'Y',
    /**X轴类型*/
    xAxisType:'value',
    /**Y轴类型*/
   	yAxisType:'value',
    /**Y轴Label样式*/
    yAxisLabelFormatter:'',
    /**空白策略*/
    boundaryGap:true,
    
    /**平均值线*/
    hasAverageLine:true,
    /**最大值线*/
    hasMaxLine:true,
    /**最小值线*/
    hasMinLine:true,
    
    /**是否等距*/
    isEquidistant:false,
    /**排序字段*/
	sortField:null,
    /**默认倒序*/
	isDesc:false,
	
	/**开启辅助线*/
	markShow:false,
	/**开启区域缩放*/
	dataZoomShow:false,
	/**开启数据视图*/
    dataViewShow:false,
    /**数据视图只读*/
    dataViewReadOnly:true,
    /**开启图形*/
    magicTypeShow:false,
    /**默认图标类型*/
    defaultChartType:'line',
    
    
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
    	me.chart =  Ext.create('Shell.ux.chart.LineChart');
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
		var me = this;
		var config = me.createOption(data);//X轴等距/不等距
		var con = Ext.apply(me.chart.chartConfig,config);
		
		con.toolbox.feature.mark.show = me.markShow;//辅助线
		con.toolbox.feature.dataZoom.show = me.dataZoomShow;//区域缩放
		con.toolbox.feature.dataView.show = me.dataViewShow;//数据视图
		con.toolbox.feature.dataView.readOnly = me.dataViewReadOnly;//数据视图只读
		con.toolbox.feature.magicType.show = me.magicTypeShow;//图形
		
		me.chart.changeChart(con);
	},
	/**创建参数*/
	createOption:function(list){
		var me = this,
			isEquidistant = me.isEquidistant,
			list = list || [],
			len = list.length,
			legendData = [],
			xAxisDataMap = {},
			xAxisData = [],
			map = {},
			series = [],
			option = {};
			
		//折线归置
		for(var i=0;i<len;i++){
			var lineName = list[i][me.lineField] || me.defaultLineName;
			if(!map[lineName]) map[lineName] = [];
			map[lineName].push(list[i]);
			
			if(isEquidistant  || me.xAxisType == 'category'){//X轴等距
				var xValue = list[i][me.xField];
				if(!xAxisDataMap[xValue]) xAxisDataMap[xValue] = true;
			}
		}
		//X轴等距
		if(isEquidistant  || me.xAxisType == 'category'){
			for(var i in xAxisDataMap){
				xAxisData.push(i);//X轴数据
			}
		}
		
		//整合数据
		for(var i in map){
			legendData.push(i);
			if(me.sortField){//需要排序
				map[i] = JShell.Array.reorder(map[i],me.sortField,me.isDesc);//重新排列
			}
			
			var data = [];
			for(var j in map[i]){
				data.push(map[i][j][me.yField]);
			}
			
			var serie = {type:me.defaultChartType,smooth:true,symbolSize:3,name:i,data:data};
			serie.markPoint = {itemStyle:{normal:{color:'red',borderWidth:5}}};
			
			var markPoint = {data:[]},markLine = {data:[]};
			//最大值
			if(me.hasMaxLine){
				markPoint.data.push({
					type:'max',name:'最大值',symbol:'emptyCircle',symbolSize:10,
					itemStyle:{normal:{color:'#1e90ff',label:{position:'top'}}}
				});
				markLine.data.push({
					type:'max',name:'最大值',itemStyle:{normal:{color:'#dc143c'}}
				});
			}
			//最小值
			if(me.hasMinLine){
				markPoint.data.push({
					type:'min',name:'最小值',symbol:'emptyCircle',symbolSize:10,
					itemStyle:{normal:{color:'#1e90ff',label:{position:'top'}}}
				});
				markLine.data.push({
					type:'min',name:'最小值',itemStyle:{normal:{color:'#dc143c'}}
				});
			}
			//平均值
			if(me.hasAverageLine){
				markLine.data.push({
					type:'average',name:'平均值',itemStyle:{normal:{color:'#dc143c'}}
				});
			}
			
			if(markPoint.data.length > 0) serie.markPoint = markPoint;
			if(markLine.data.length > 0) serie.markLine = markLine;
			
			series.push(serie);
		}
		
		//设置参数
		option.legend = {data:legendData};//图例
		option.xAxis = [{type:me.xAxisType}];//X轴
		option.yAxis = [{type:me.yAxisType,axisLine:{lineStyle:{color:'#dc143c'}}}];//Y轴
		if(me.yAxisLabelFormatter) option.yAxis[0].axisLabel = {formatter:me.yAxisLabelFormatter};
		//X轴等距
		if(isEquidistant || me.xAxisType == 'category'){
			option.xAxis[0].boundaryGap = false;
			option.xAxis[0].data = xAxisData
		}
		if(me.boundaryGap) option.xAxis[0].boundaryGap = true;
		option.series = series;
		
		return option;
	},
	/**更新数据 @public*/
	loadData:function(data){
   		var me = this,
   			list = data || [],
   			len = list.length,
   			arr = [];
   		
   		for(var i=0;i<len;i++){
   			var obj = {};
   			obj[me.lineField] = list[i][me.lineField];
   			obj[me.xField] = list[i][me.xField];
   			obj[me.yField] = list[i][me.yField];
   			arr.push(obj);
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