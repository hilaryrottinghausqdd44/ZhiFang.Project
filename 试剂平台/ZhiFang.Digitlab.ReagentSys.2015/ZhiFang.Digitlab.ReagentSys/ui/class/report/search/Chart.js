/**
 * 历史对比图
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.report.search.Chart',{
	extend:'Shell.ux.chart.EChartsPanel',
	
	/**语言包内容*/
	Shell_class_report_search_Chart:{
		title:{
			TEXT:'历史对比'
		},
		legendData:['结果']
	},
	
	width:800,
	height:400,
	
	bodyPadding:5,
	
	/**X轴字段*/
	xField:'ReceiveDate',
	/**Y轴字段*/
	yField:'ReportValue',
	
	/**数据条件对象*/
	serverParams:null,
	
	/**开启加载数据遮罩层*/
	hasLoadMask:true,
	/**加载数据提示*/
	loadingText:JShell.Server.LOADING_TEXT,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.chartData){
			me.changeData(me.chartData);
		}
	},
	
	initComponent:function(){
		var me = this;
		
		//替换语言包
		me.changeLangage('Shell.class.report.search.Chart');
		
		//标题
		me.title = me.Shell_class_report_search_Chart.title.TEXT;
		me.legendData = me.Shell_class_report_search_Chart.legendData;
		
		//图表按需加载类型
		me.requireId = [
			'echarts',
			'echarts/chart/line',
			'echarts/chart/bar'
		];
		//charts文件名称
		var fileLocation = JShell.System.Path.UI + '/echarts/echarts';
		//图表按需加载路径
		me.requirePaths = {
	        'echarts':fileLocation,
	        'echarts/chart/line':fileLocation,
	        'echarts/chart/bar':fileLocation
	   	};
		
		me.callParent(arguments);
	},
	
	/**更改图表*/
	changeData:function(data){
		var me = this;
		var obj = data || {};
		
		var config = me.createOption(obj);
		
		if(config.xAxis[0].data.length == 0){
			me.clearData();
			me.showErrror('没有数据!');
		}else{
			me.changeChart(config);
		}
	},
	/**创建图表参数*/
	createOption:function(data){
		var me = this;
		var option = {};
		
		//标题
//		option.title = {
//			text:me.title,
//			subtext: '数据来自试剂平台',
//			x: 'center'
//		};
		//悬浮框
		option.tooltip = {
			trigger: 'axis',
			axisPointer : {
	            type : 'shadow'
	       	}
		};
		//图例
//		option.legend = {
//			show:false,
//			//selectedMode:false,
//			data:me.legendData
//		};
		
		//网格属性
		option.grid = {
			x:40,
			y:30,
			x2:60
		};
		//启用拖拽重计算特性
		option.calculable = true;
		//工具箱
		option.toolbox = {
			orient:'vertical',
	        show : true,
	        feature : {
	            mark : {show: true},
	            dataZoom : {show: true},
	            //dataView : {show: true, readOnly: false},
	            magicType: {show: true, type: ['line', 'bar']},
	            restore : {show: true},
	            saveAsImage : {show: true}
	        }
	    };
		//数据区域缩放
		option.dataZoom = {
			show:true,
			realtime:true,
			start:50,
			end:100
		};
				
        var xAxisData = me.getXAxisData(data);
        var lineData = me.getLineData(data);
        
	    option.xAxis = [{
            type : 'category',
            boundaryGap : true,
            //axisTick: {onGap:false},
            //splitLine: {show:false},
            data : xAxisData,
            axisLabel:{
            	formatter:function(value){
            		return value.slice(5,10).replace(/\//g,'-');
            	}
            }
        }];
	    option.yAxis = [{
	    	name:me.legendData[0],
            type : 'value',
            scale:true,
            boundaryGap: [0.01, 0.01]
        }];
        
	    option.series = [{
            name:me.legendData[0],
            type:'bar',
            //yAxisIndex:1,
            //barWidth:20,//柱状宽度
            barMinHeight:10,//柱状图最低高度
            itemStyle: {
                normal: {
                    color: 'green',
                    barBorderColor: 'black',
                    barBorderWidth: 1,
                    barBorderRadius:0
                }
            },
            data:lineData[0]
        }];
		
		return option;
	},
	getXAxisData:function(data){
		var me = this,
			list = data.list || [],
			len = list.length,
			xAxisData = [];
		
		for(var i=0;i<len;i++){
			xAxisData.push(list[i][me.xField]);
		}
		return xAxisData;
	},
	getLineData:function(data){
		var me = this,
			lineData = [[]];
		
		var list = data.list || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
			var value = list[i][me.yField];
			lineData[0].push(value);
		}
		return lineData;
	},
	getTooltipRow:function(list){
		var me = this;
		var value = [];
		
		return value;
	}
});