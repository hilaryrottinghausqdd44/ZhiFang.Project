/**
 * 历史比对图表
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.tiaoshi.class.PrintChart',{
	extend:'Shell.ux.panel.Panel',
	
	requires:[
		'Shell.ux.form.field.DateArea',
		'Shell.ux.chart.LineChart'
	],
	
	title:'历史比对图表',
	width:500,
	height:300,
	layout:'fit',
	
	/**获取数据服务路径*/
	selectUrl:'/ServiceWCF/ReportFormService.svc/ResultHistory',
	//时间条件字段
	whereDateField:'CheckDate',
	/**X轴字段*/
	xField:'CheckDateTime',
	/**Y轴结果字段*/
	yField:'ReportValue',
	//Y轴结果描述字段
	yFieldDesc:'ReportDesc',
	//X轴文字
	xFieldText:'审核时间',
	//Y轴结果文字
	yFieldText:'结果',
	//Y轴描述文字
	yFieldDescText:'结果描述',
	/**数据条件对象*/
	serverParams:null,
	
	/**开启加载数据遮罩层*/
	hasLoadMask:true,
	/**加载数据提示*/
	loadingText:'数据加载中...',
	/**是否存在收缩按钮*/
	hasCollapseButton:true,
	//最后一次数据
	_lastData:null,
	
	/**渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.disableControl();
		me.callParent(arguments);
		//视图准备完毕
		me.on({
			expand:function(p,d){
				if(me.isCollapsed && me.selectUrl){me.load(null,true);}
				me.isCollapsed = false;
			}
		});
	},
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.items = [{
			xtype:'uxlinechart',
			itemId:'chart'
		},{
			xtype:'panel',
			itemId:'info',
			border:false,
			hidden:true
		}];
		
		me.toolbars = [{dock:'top',itemId:'toptoolbar',buttons:['refresh',
			{xtype:'uxdatearea',itemId:'date',fieldLabel:me.xFieldText},
			{xtype:'uxbutton',itemId:'search',iconCls:'button-search',tooltip:'<b>查询</b>'}
		]}];
		
		if(me.hasCollapseButton){
			me.toolbars[0].buttons.push('->',{xtype:'uxbutton',itemId:'collapse',text:'',iconCls:'button-down',tooltip:'<b>收缩面板</b>'});
		}
		
		me.callParent(arguments);
	},
	/**收缩*/
	onCollapseClick:function(but){
		this.collapse();
	},
	/**刷新处理*/
	onRefreshClick:function(){
		this.onSearchClick();
	},
	/**查询处理*/
	onSearchClick:function(){
		this.load(null,true);
	},
	
	enableControl:function(){
		this.disableControl(true);
	},
	disableControl:function(bo){
		var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			items = toptoolbar.items.items,
			len = items.length;
		
		for(var i=0;i<len;i++){
			if(items[i].itemId == "collapse") continue;
			items[i][bo ? "enable" : "disable"]();
		}
	},
	
	/**获取查询条件和图表标题*/
	getWhereAndTitle:function(value){
		var me = this,
			receiveDate = me.serverParams.ReceiveDate,
			title = {text:"历史对比数据",subtext:""},
			where = "";
			
		if(!value){
			title.subtext = "最近三个月";
			
			where = "datediff(month,rf." + me.whereDateField + ",'" + receiveDate +"')<=3";
		}else{
			var start = "",
				end = "",
				arr = [];
				
			if(value.start){
				start = Shell.util.Date.toString(value.start,true);
				arr.push("rf." + me.whereDateField + ">='" + start + "'");
			}
			if(value.end){
				end = Shell.util.Date.toString(value.end,true);
				arr.push("rf." + me.whereDateField + "<='" + end + "'");
			}
			title.subtext = (start ? start : "开始") + " ~ " + (end ? end : "至今");
			where = arr.join(" and ")
			if(arr.length == 2){
				where = "(" + where + ")";
			}
		}
		
		return {where:where,title:title};
	},
	/**加载数据*/
	getDataFromServer:function(params,where,callback){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl + "?PatNo=" + params.PatNo + 
				"&ItemNo=" + params.ItemNo + "&Table=" + params.Table;
		if(where){url += "&where=" + where;}
			
		Shell.util.Action.delay(function(){
		    me.getToServer(url, function (text) {
                var result = Ext.JSON.decode(text),
				    option = null;

                if (result.success) {
                    var value = Ext.JSON.decode(result.ResultDataValue);
                    var list = me.onDataChange(value);
                    option = me.getOptionData(list);
				}else{
                    me.showError(result.ErrorInfo);
				}
				callback(option);
			});
		},null,100);
	},
	/**数据处理*/
	getOptionData:function(list){
		var me = this,
			list = list || [],
			len = list.length,
			lineName = "结果",
			option = {
				tooltip:{
					trigger:'axis',
                    padding: 5,  
                    formatter: function(params){
                    	var info = me.getInfoByX(params[0][1]);
                        return me.xFieldText + '：' + info[me.xField] + 
                        	'</br>' + me.yFieldText + '：' + info[me.yField] +
                        	'</br>' + me.yFieldDescText + '：' + (info[me.yFieldDesc] || '');
					}
				},
				legend:{data:[lineName]},
				xAxis:[{
					type:'category',axisLabel:{rotate:45,formatter:function(value){
						return value.substring(5,10);
					}},data:[]
				}],
				yAxis:[{type:'value'}],
				series:[{type:'value',name:lineName,data:[]}],
				toolbox:{
			        show:true,
			        feature:{
			            mark:{show:true},
			            magicType:{show:true,type:['line','bar']},
			            restore:{show:true},
			            saveAsImage:{show:true}
			        }
			    }
			};
		
		for(var i=0;i<len;i++){
			option.xAxis[0].data.push(list[i][me.xField]);
			option.series[0].data.push(list[i][me.yField]);
		}
			
		return option;
	},
	
	/**更改图表内容*/
	load:function(params,isPrivate){
		var me = this,
			info = me.getComponent('info'),
			chart = me.getComponent('chart');
		
		if(!isPrivate && (!params || !params.PatNo || !params.ItemNo || !params.Table || !params.ReceiveDate)){
			var errorInfo = [];
			if(!params){
				me.showError("Shell.print.class.PrintChart的load方法没有接收到参数对象!");
				return;
			}
			errorInfo.push("Shell.print.class.PrintChart的load方法接收的参数对象有错!");
			if(!params.PatNo){errorInfo.push("<b style='color:red'>PATNO</b>参数错误!");}
			if(!params.ItemNo){errorInfo.push("<b style='color:red'>ITEMNO</b>参数错误!");}
			if(!params.Table){errorInfo.push("<b style='color:red'>SECTIONTYPE</b>参数错误!");}
			if(!params.ReceiveDate){errorInfo.push("<b style='color:red'>RECEIVEDATE</b>参数错误!");}
			
			me.showError(errorInfo.join("</br>"));
			return;
		}
		
		//清空错误信息
		info.update("");
		info.hide();
		//图表显示
		chart.show();
		
		me.serverParams = isPrivate ? me.serverParams : params;
		
		var collapsed = me.getCollapsed(),
			chart = me.getComponent('chart'),
			date = me.getComponent('toptoolbar').getComponent('date'),
			value = date.getValue(true),
			info = me.getWhereAndTitle(value);
			
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		if(me.hasLoadMask){me.body.mask(me.loadingText);}//显示遮罩层
		me.disableControl();
		
		me.getDataFromServer(me.serverParams,info.where,function(option){
			if(!option){
				chart.clearData();
			}else{
				option.title = info.title;
				chart.changeChart(option,true);
			}
			
			me.enableControl();
			if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
		});
	},
    /**清理数据*/
	clearData: function () {
		var me = this,
			info = me.getComponent('info'),
			chart = me.getComponent('chart');
			
		//清空错误信息
		info.update("");
		info.hide();
		//清理图表数据
		chart.clearData();
		chart.show();
	},
	/**提示错误*/
	showError:function(value){
		var me = this,
			info = me.getComponent('info'),
			chart = me.getComponent('chart');
			
		//清理数据
		chart.clearData();
		chart.hide();
		//错误信息
		value = '<div style="text-align:center;padding:20px;">' + value + '</div>';
		info.update(value);
		info.show();
	},
	//根据X轴数据获取对应的对象
	getInfoByX:function(value){
		var me = this,
			list = me._lastData,
			len = list.length,
			info = null;
		
		for(var i=0;i<len;i++){
			if(list[i][me.xField] == value){
				info = list[i];
				break;
			}
		}
		
		return info;
	},
	//数据更改
	onDataChange:function(list){
		var me = this,
			arr = list || [],
			len = arr.length;
			
		for(var i=0;i<len;i++){
			arr[i][me.xField] = arr[i]['CheckDate'] + ' ' + arr[i]['CheckTime'];
		}
			
        me._lastData = arr;
        
        return arr;
	}
});