/**
 * 汇总-折线/柱状图类
 * @author Jcall
 * @version 2014-08-27
 */
Ext.define('Shell.ux.chart.SumLineChart',{
	extend:'Shell.ux.chart.ECharts',
	alias:'widget.uxsumlinechart',
	
	mixins:[
		'Shell.ux.server.Ajax',
		'Shell.ux.PanelController'
	],
	
	/**默认的显示类型 bar,line*/
	type:'line',
	
	width:400,
	height:200,
	
	/**标题*/
	title:{
		text:'统计汇总图',
		subtext:''
	},
	
	/**开启加载数据遮罩层*/
	hasLoadMask:true,
	
	/**默认数据条件*/
	defaultWhere:'',
	/**内部数据条件*/
	internalWhere:'',
	/**外部数据条件*/
	externalWhere:'',
	/**获取数据服务地址*/
	selectUrl:'',
	/**分组字段*/
	groupByFields:'',
	/**统计字段*/
	statFields:'',
	
	/**默认属性*/
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
	    calculable:true,
	    yAxis:[{type:'value'}]
	},
	
	/**初始化组件属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('load');
		me.chartConfig.title = me.title;//标题
		me.callParent(arguments);
	},
	
	/**@public 根据where条件加载数据*/
	load:function(where,groupByFields,statFields,isPrivate){
		var me = this;
		me.externalWhere = isPrivate ? me.externalWhere : where;
		me.groupByFields = isPrivate ? me.groupByFields : (groupByFields || '');
		me.statFields = isPrivate ? me.statFields : (statFields || '');
		
		//获取数据
		me.getDataFromServer();
	},
	/**从后台获取数据*/
	getDataFromServer:function(){
		var me = this,
			url = me.getLoadUrl();
			
		if(me.hasLoadMask){me.showMask();}
    	
    	me.getToServer(url,function(text){
    		var result=me.responseTextToList(text);
    		if(result.success){
    			me.loadData(result.list,me.groupByFields.split(',').length);
    		}else{
    			me.showError(result.ErrorInfo);
    		}
    		if(me.hasLoadMask){me.hideMask();}//隐藏遮罩层
    		me.fireEvent('load',me);
    	},false);
	},
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			where = '';
		
		//默认条件
		if(me.defaultWhere && me.defaultWhere != ''){
			where += '(' + me.defaultWhere +') and ';
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != ''){
			where += '(' + me.internalWhere + ') and ';
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != ''){
			where += '(' + me.externalWhere + ') and ';
		}
		
		where = where.slice(-5) == ' and ' ? where.slice(0,-5) : where;
		
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'where=' + Shell.util.String.encode(where) + 
			'&groupByFields=' + me.groupByFields + '&statFields=' + me.statFields;
		
		return url;
	},
	/**更改数据*/
	changeData:function(data){
		//例如条件为fields='HITACHIClientTypeName,IsFiling'
		//HITACHIBusinessProject_HITACHIClientTypeName 医院等级
		//HITACHIBusinessProject_IsFiling 是否归档
		//fields只有一个字段时,也就是只有一条线:统计汇总
		//第一个字段就是X轴数据字段,后面的字段合成多条线,如上面的就是说X轴是医院等级,有两条线：已归档和未归档
		
		//需要返回的数据格式
//		{
//			lines:['已归档','未归档'],//线的名称
//			xValues:['三甲','三乙','二丙','二乙'],//X轴的数据
//			yValues：{//数据
//				'已归档':[15,18,0,24],//没有值的赋0
//				'未归档':[11,12,14,10]
//			}
//		}
		var me = this,
			data = data || {},
			lines = data.lines || [],
			xValues = data.xValues || [],
			yValues = data.yValues || {},
			series = [];
			
		for(var i in yValues){
			series.push({name:i,type:me.type,data:yValues[i],
				itemStyle:{
					normal:{label:{show:true,position:'top'}}
				}
			});
		}
			
		var option = {
            legend:{data:lines},
            xAxis:[{type:'category',
            
            axisLabel:{rotate:45,formatter:function(v){
            	var value = (v && Shell.util.String.lenASCII(v) > 10) ? Shell.util.String.substrASCII(v,0,10) : v;
            	return value;
            }},data:xValues}],
            series:series
        };
        
        // 为echarts对象加载数据 
        me.changeChart(option);
	},
	/**
	 * 替换数据-单一统计值
	 * @param {} data
	 * @param {} num 分组字段数量
	 */
	loadData:function(data,num){
		var me = this,
			num = num ? num : 1,
			list = data || [],
			length = list.length,
			separator = '-',
			lines = [],
			yValues = {},
			xValues = [],
			
			xValuesObj = {};
			yValuesObj = {};
			
		for(var i=0;i<length;i++){
			var lis = list[i],
				len = lis.length;
			for(var j=0;j<len;j++){
				if(lis[j] === true){
					list[i][j] = '是';
				}else if(lis[j] === false){
					list[i][j] = '否';
				}
			}
		}
			
		for(var i=0;i<length;i++){
			var lis = list[i],
				gArr = [],
				dataArr = [],
				xV = '';
				
			for(var j=0;j<lis.length;j++){
				var li = lis[j];
				if(j == 0){
					xV = li;
					if(!xValuesObj[li]){xValuesObj[li] = true;}
				}else if(j > 0 && j < num){
					gArr.push(li);
				}else{
					dataArr.push(li);
				}
			}
			var lineName = gArr.length == 0 ? '统计汇总' : gArr.join(separator);
			if(!yValuesObj[lineName]){
				yValuesObj[lineName] = {};
			}
			yValuesObj[lineName][xV] = dataArr[0];//
		}
		
		for(var i in xValuesObj){xValues.push(i);}
		
		for(var i in yValuesObj){
			var line = yValuesObj[i];
			yValues[i] = [];
			for(var j in xValues){
				var xV = xValues[j];
				var value = line[xV] == null ? 0 : line[xV];
				yValues[i].push(value);
			}
		}
		
		for(var i in yValues){lines.push(i);}
		
		var d = {
			lines:lines,
			xValues:xValues,
			yValues:yValues
		};
		
		me.changeData(d);
	},
	/**
	 * 替换数据_原始
	 * @param {} data
	 * @param {} num 分组字段数量
	 */
	loadData_s:function(data,num){
		var me = this,
			num = num ? num : 1,
			list = data || [],
			length = list.length,
			separator = '-',
			lines = [],
			yValues = {},
			xValues = [],
			
			xValuesObj = {};
			
		for(var i=0;i<length;i++){
			var lis = list[i],
				gArr = [],
				dataArr = [];
				
			for(var j=0;j<lis.length;j++){
				var li = lis[j];
				if(j == 0){
					if(!xValuesObj[li]){xValuesObj[li] = true;}
				}else if(j > 0 && j < num){
					gArr.push(li);
				}else{
					dataArr.push(li);
				}
			}
			var lineName = gArr.length == 0 ? '统计汇总' : gArr.join(separator);
			if(!yValues[lineName]){
				yValues[lineName] = [];
			}
			yValues[lineName].push(dataArr[0]);//
		}
		
		for(var i in yValues){lines.push(i);}
		
		for(var i in xValuesObj){xValues.push(i);}
		
		var d = {
			lines:lines,
			xValues:xValues,
			yValues:yValues
		};
		
		me.changeData(d);
	},
	/**更改标题*/
	changeTitle:function(title){
		var me = this;
		me.title = title;
	},
	/**显示遮罩层*/
	showMask:function(){
		var me = this;
		me.mk = me.mk || new Ext.LoadMask(me.getEl(),{msg:'数据加载中...',removeMask:true});
		me.mk.show();//显示遮罩层
	},
	/**隐藏遮罩层*/
	hideMask:function(){
		var me = this;
		if(me.mk){me.mk.hide();}
	},
	
	/**更改图表*/
	changeChart:function(config){
		var me = this,
			markPoint = {data:[{type:'max',name:'最大值'},{type:'min',name:'最小值'}]},
            markLine = {data:[{type:'average',name:'平均值'}]},
            series = config.series || [],
            length = series.length;
			
   		for(var i=0;i<length;i++){
   			config.series[i].type = me.type;
   			config.series[i].markPoint = markPoint;
   			config.series[i].markLine = markLine;
   		}
   		
   		config = Ext.applyIf(config,me.chartConfig);
   		config.title = me.title;
   		
		me.callParent([config,true]);
	}
});