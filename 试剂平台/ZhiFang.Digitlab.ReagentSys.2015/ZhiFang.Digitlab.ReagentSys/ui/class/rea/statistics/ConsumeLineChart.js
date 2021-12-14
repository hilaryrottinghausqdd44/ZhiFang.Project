/**
 * 柱状图面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.statistics.ConsumeLineChart',{
	extend:'Shell.ux.chart.EChartsPanel',
	
	title:'试剂消耗分析',
	width:800,
	height:400,
	
	bodyPadding:5,
	
	legendData:['消耗数量','总计金额'],
	
	/**X轴字段_试剂名称*/
	xField:'CenQtyDtlTempHistory_GoodsName',
	/**Y轴字段1_库存数量*/
	yField1:'CenQtyDtlTempHistory_GoodsQty',
	/**Y轴字段2_总计金额*/
	yField2:'CenQtyDtlTempHistory_SumTotal',
	
	initComponent:function(){
		var me = this;
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
		
//		if(!obj.list || obj.list.length == 0){
//			me.showErrror(JShell.Server.NO_DATA);
//			return;
//		}
		
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
		option.title = {
			text:me.title,
			subtext: '数据来自试剂平台',
			x: 'center'
		};
		//悬浮框
		option.tooltip = {
			trigger: 'axis',
			axisPointer : {
	            type : 'shadow'
	        }
		};
		//图例
		option.legend = {
			//selectedMode:false,
			data:me.legendData,
			x: 'left'
		};
		//启用拖拽重计算特性
		option.calculable = true;
		//工具箱
		option.toolbox = {
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
            data : xAxisData
	    }];
	    option.yAxis = [{
	    	name:me.legendData[0],
            type : 'value',
            scale:true,
            boundaryGap: [0.01, 0.01]
        },{
        	name:me.legendData[1],
            type : 'value',
            scale:true,
            boundaryGap: [0.01, 0.01]
        }];
        
	    option.series = [{
            name:me.legendData[0],
            type:'bar',
            //barWidth:20,//柱状宽度
            barMinHeight:10,//柱状图最低高度
            itemStyle: {
                normal: {
                    label: {
                        show: true,
                        position: 'top'
                    }
                }
            },
            data:lineData[0]
        },{
            name:me.legendData[1],
            type:'bar',
            yAxisIndex:1,
            //barWidth:20,//柱状宽度
            barMinHeight:10,//柱状图最低高度
            data:lineData[1]
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
			lineData = [[],[]];
		
		var list = data.list || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
			lineData[0].push(list[i][me.yField1]);
			lineData[1].push(list[i][me.yField2]);
		}
		return lineData;
	}
});