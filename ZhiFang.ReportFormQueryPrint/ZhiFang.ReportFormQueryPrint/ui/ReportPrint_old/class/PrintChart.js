/**
 * 历史比对图表
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintChart',{
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
	/**X轴字段*/
	xField:'ReceiveDate',
	/**Y轴字段*/
	yField:'ReportValue',
	
	/**数据条件对象*/
	serverParams:null,
	
	/**开启加载数据遮罩层*/
	hasLoadMask:true,
	/**加载数据提示*/
	loadingText:'数据加载中...',
	/**是否存在收缩按钮*/
	hasCollapseButton:true,
	
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
		}];
		
		me.toolbars = [{dock:'top',itemId:'toptoolbar',buttons:['refresh',
			{xtype:'uxdatearea',itemId:'date',fieldLabel:'核收日期'},
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
			
			where = "datediff(month,rf.RECEIVEDATE,'" + receiveDate +"')<=3";
		}else{
			var start = "",
				end = "",
				arr = [];
				
			if(value.start){
				start = Shell.util.Date.toString(value.start,true);
				arr.push("rf.RECEIVEDATE>='" + start + "'");
			}
			if(value.end){
				end = Shell.util.Date.toString(value.end,true);
				arr.push("rf.RECEIVEDATE<='" + end + "'");
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
                    option = me.doDataChange(value);
				}else{
                    me.showError(result.ErrorInfo);
				}
				callback(option);
			});
		},null,100);
	},
	/**数据处理*/
	doDataChange:function(list){
		var me = this,
			list = list || [],
			len = list.length,
			lineName = "结果",
			option = {
				tooltip:{trigger:'axis'},
				legend:{data:[lineName]},
				xAxis:[{type:'category',axisLabel:{rotate:45},data:[]}],
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
		var me = this;
		
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
	    this.getComponent('chart').clearData();
	}
});