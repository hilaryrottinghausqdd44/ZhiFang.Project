/**
 * 柱状图面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.statistics.ReagentStockLineChart',{
	extend:'Shell.ux.chart.EChartsPanel',
	
	title:'试剂库存分析',
	width:800,
	height:400,
	
	bodyPadding:5,
	
	legendData:['低库存值','高库存值','库存数量','总计金额'],
	
	/**X轴字段_试剂名称*/
	xField:'CenQtyDtl_GoodsName',
	/**Y轴字段1_低库存报警数量*/
	yField1:'CenQtyDtl_LowQty',
	/**Y轴字段2_高库存报警数量*/
	yField2:'CenQtyDtl_HeightQty',
	/**Y轴字段3_库存数量*/
	yField3:'CenQtyDtl_GoodsQty',
	/**Y轴字段4_总计金额*/
	yField4:'CenQtyDtl_SumTotal',
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.chartData){
			me.changeData(me.chartData);
		}
	},
	
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
	       	},
	       	formatter:function(params){
	       		var v = [];
	       		v.push(params[0][1]);
	       		//低限
	       		v.push(params[0][0] + ':' + params[0][2]);
	       		//高限
	       		var max = parseInt(params[1][2]) + parseInt(params[0][2]);
	       		v.push(params[1][0] + ':' + max);
	       		
	       		if(params[2]){
	       			v.push(params[2][0] + ':' + params[2][2]);
	       		}
	       		if(params[3]){
	       			v.push(params[3][0] + ':' + params[3][2]);
	       		}
	       		
	       		return v.join('</br>');
	       	}
		};
		//图例
		option.legend = {
			//selectedMode:false,
			//data:me.legendData,
			data:['库存数量','总计金额'],
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
//      },{
//          type : 'category',
//          boundaryGap : true,
//          axisLine: {show:false},
//          axisTick: {show:false},
//          axisLabel: {show:false},
//          splitArea: {show:false},
//          splitLine: {show:false},
//          data : xAxisData
        }];
	    option.yAxis = [{
	    	name:me.legendData[2],
            type : 'value',
            scale:true,
            boundaryGap: [0.01, 0.01]
        },{
        	name:me.legendData[3],
            type : 'value',
            scale:true,
            boundaryGap: [0.01, 0.01]
        }];
        
	    option.series = [{
            name:me.legendData[0],
            type:'bar',
            stack: '总量',
            barWidth:5,//柱状宽度
            barMinHeight:10,//柱状图最低高度
            itemStyle: {
                normal: {
                    color: 'orange',
                    barBorderColor: 'black',
                    barBorderWidth: 1,
                    barBorderRadius:0
                }
            },
            data:lineData[0]
        },{
            name:me.legendData[1],
            type:'bar',
            stack: '总量',
            barWidth:5,//柱状宽度
            barMinHeight:10,//柱状图最低高度
            //xAxisIndex:1,
            itemStyle: {
                normal: {
                    color: 'blue',
                    barBorderColor: 'black',
                    barBorderWidth: 1,
                    barBorderRadius:0
                }
            },
            data:lineData[1]
        },{
            name:me.legendData[2],
            type:'bar',
            //barWidth:20,//柱状宽度
            barMinHeight:10,//柱状图最低高度
            itemStyle: {
                normal: {
                    //color: 'blue',
                    barBorderColor: 'black',
                    barBorderWidth: 1,
                    barBorderRadius:0,
//                  color:function(seriesIndex,x,y,a,b){
//                  	JShell.Msg.log(seriesIndex + ',' + x + ',' + y);
//						
//            			var color = 'blue';
//            			if(x >= 0){
//            				color = option.series[2].data[x].color;
//            			}
//            			
//	              		return color;
//                  },
                  	label:{
                  		show:true,
                  		textStyle:{color:'red'},
                  		formatter:function(name,x,y){
                  			var value = '';
                  			var xAxisData = option.xAxis[0].data;
                  			var index = null;
                  			for(var i in xAxisData){
                  				if(xAxisData[i] == x){
                  					index = i;
                  					break;
                  				}
                  			}
                  			var min = option.series[0].data[index];
                  			var max = option.series[1].data[index];
                  			
                  			var v = parseInt(y);
                  			min = parseInt(min);
                  			max = parseInt(max);
                  			if(v < min){
                  				value = '低';
                  			}else if(v > max){
                  				value = '高';
                  			}
                  			
	                      	return value;
	                    }
                  	}
                }
            },
            data:lineData[2]
        },{
            name:me.legendData[3],
            type:'bar',
            yAxisIndex:1,
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
            data:lineData[3]
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
			lineData = [[],[],[],[]];
		
		var list = data.list || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
			var min = list[i][me.yField1];
			var max = list[i][me.yField2];
			var value = list[i][me.yField3];
//			if(value <= min){
//				lineData[2].push({value:value,color:'red'});
//			}else if(value >= max){
//				lineData[2].push({value:value,color:'orange'});
//			}else{
//				lineData[2].push(value);
//			}

			lineData[0].push(min);
			lineData[1].push(max - min);
			lineData[2].push(value);
			lineData[3].push(list[i][me.yField4]);
		}
		return lineData;
	},
	getTooltipRow:function(list){
		var me = this;
		var value = [];
		
		return value;
	}
});