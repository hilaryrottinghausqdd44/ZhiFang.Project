/**
 * 打印应用
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintApp',{
	extend:'Shell.ux.panel.AppPanel',
	
	title:'打印查询',
	layout:{type:'border',regionWeights:{north:3,west:2,south:1}},
	
    /**默认数据条件,过滤没有文件的结果*/
	defaultWhere:'resultsend=1',
	/**外部数据条件*/
	externalWhere:'',
	/**错误信息*/
	errorInfo: [],

    /**A4纸张类型，1(A4) 2(16开)*/
	A4Type: 1,
    /**默认打印类型*/
    printType:'A4',
    /**默认勾选过滤框*/
	checkFilter: false,
    /**默认勾选未打印框*/
	checkUnprint: false,
    /**默认程序天数*/
	defaultDates: 7,
    /**默认每页数量*/
	defaultPageSize: 50,
    /**默认顺序*/
	defaultOrderBy: [],
	
    /**收缩*/
	headCollapsed: false,
	/**是否开启打印功能*/
	hasPrint: true,
    /**定义的接收参数*/
	requestParamsArr: ['PATNO', 'ZDY3', 'CNAME', 'RECEIVEDATE'],
    /**默认勾选*/
    autoSelect:false,
	
	/**获取定义参数*/
	getParamsArray:function(){
		//定义标准的参数列表,自动过滤掉非标准的参数
		//住院：病历号、姓名、核收日期
		//门诊：卡号(自定义3)、姓名、核收日期
	    return this.requestParamsArr;
	},
	
	/**渲染完后处理*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var PrintSearch = me.getComponent('PrintSearch'),
			PrintList = me.getComponent('PrintList'),
			PrintContent = me.getComponent('PrintContent'),
            PrintChart = me.getComponent('PrintChart');
			
		//列表行选中监听
		PrintList && PrintList.on({
			select:function(view,record){
			    Shell.util.Action.delay(function () {
			        var SectionType = record.get("SectionType");

			        if (PrintChart) PrintChart.clearData();

			        if (SectionType == 1 || SectionType == 3) {
			            if (PrintChart) PrintChart.show();
			        } else {
			            if (PrintChart) PrintChart.hide();
			        }

					PrintContent.changeContent({
						ReportFormID:record.get("ReportFormID"),
						SectionNo:record.get("SECTIONNO"),
						SectionType: record.get("SectionType"),
						RECEIVEDATE: record.get("RECEIVEDATE")
					});
				},null,500);
			}
		});
		
		//查询栏监听
		if(PrintSearch){
			PrintSearch.on({
				search:function(com,where){
					me.onSearch(where);
				}
			});
			
			//初始值
			var params = me.params,
				hasDate = false;
				
			for(var i in params){
				//组装成默认条件
				if(i == "RECEIVEDATE"){//核收时间
					var field = PrintSearch.getFieldsByName('RECEIVEDATE'),
						arr = params[i].split(";");
						
					if(arr.length == 1){
						field.setValue({start:Shell.util.Date.getDate(arr[0])});
					}else if(arr.length == 2){
						var date = {};
						if(arr[0]){
							date.start = Shell.util.Date.getDate(arr[0]);
						}
						if(arr[1]){
							date.end = Shell.util.Date.getDate(arr[1]);
						}
						field.setValue(date);
					}
					
					hasDate = true;
				}else{
					var field = PrintSearch.getFieldsByName(i);
					
					field.setValue(params[i]);
					//field.disable();
				}
			}
			
			if(!hasDate){
				var field = PrintSearch.getFieldsByName('RECEIVEDATE'),
					da = new Date();
					
				var date = {
				    start: Shell.util.Date.getNextDate(da, 1 - me.defaultDates),
					end:da
				};
				field.setValue(date);
			}
		}
	},
	
	initComponent:function(){
		var me = this;
		
		me.initListWhere();//初始化列表的条件内容
		
		me.apps = me.createApps();
		
		me.callParent(arguments);
	},
	
	/**创建内部组件*/
	createApps:function(){
		var me = this;
		var apps = [{
			className:'Shell.ReportPrint.class.PrintSearch',
			itemId:'PrintSearch',region:'north',
			split: true, collapsible: true, headCollapsed: me.headCollapsed
		},{
			className:'Shell.ReportPrint.class.PrintList',
			itemId:'PrintList',header:false,
			width:520,region:'west',
			split:true,collapsible:true,
			hasPrint: me.hasPrint,
			defaultPageSize: me.defaultPageSize,
			defaultOrderBy:me.defaultOrderBy,
			defaultWhere:me.defaultWhere,
			externalWhere:me.externalWhere,
			//defaultLoad: me.externalWhere ? true : false,
			A4Type: me.A4Type,
			printType:me.printType,
			checkFilter: me.checkFilter,
			checkUnprint: me.checkUnprint,
			autoSelect: me.autoSelect
		},{
			className:'Shell.ReportPrint.class.PrintContent',autoScroll:true,
			itemId:'PrintContent',header:false,region:'center'
		},{
			className:'Shell.ReportPrint.class.PrintChart',
			itemId:'PrintChart',header:false,
			height:250,region:'south',collapsed:true,
			split:true,collapsible:true
		}];
		
		return apps;
	},
	
	/**查询*/
	onSearch:function(where){
		var me = this,
			PrintList = me.getComponent('PrintList');
			
		PrintList.internalWhere = PrintList.getInternalWhere();
		PrintList.load(where);
	},
	
	/**初始化列表的条件内容*/
	initListWhere:function(){
		var me = this,
			array = me.getParamsArray(),
			params = Shell.util.Path.getRequestParams(true),
			pars = {},
			where = [];

	    //参数转换处理
		params = me.changeParams(params);
			
		if(params["HASPRINT"]){
			if(params["HASPRINT"].toLocaleLowerCase() === "false") me.hasPrint = false;
		}
		
		for(var i in params){
			var bo = Shell.util.String.inArray(i,array);
			if(bo) pars[i] = params[i];
		}
		
		me.params = pars;
		
		var hasDate = false;
		
		for(var i in pars){
			//组装成默认条件
			if(i == "RECEIVEDATE"){//核收时间
				var arr = pars[i].split(";");
				if(arr.length == 1){
					where.push(i + ">='" + arr[0] + "'");
				}else if(arr.length == 2){
					var da = [];
					if(arr[0]){da.push(i + ">='" + arr[0] + "'");}
					if(arr[1]){da.push(i + "<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(arr[1]),true) + "'");}
					where.push(da.join(" and "));
				}
				hasDate = true;
			}else{
				where.push(i + "='" + pars[i] + "'");
			}
		}
		
		if(!hasDate){
			var date = new Date(),
				start = Shell.util.Date.toString(Shell.util.Date.getNextDate(date, 1 - me.defaultDates), true),
				end = Shell.util.Date.toString(Shell.util.Date.getNextDate(date),true);
				da = [];
				
			da.push("RECEIVEDATE>='" + start + "'");
			da.push("RECEIVEDATE<'" + end + "'");
			
			where.push(da.join(" and "));
		}
		
		me.externalWhere = where.join(" and ");
	},
	/**清空所有数据*/
	clearData:function(){
		var PrintList = me.getComponent("PrintList"),
			PrintContent = me.getComponent("PrintContent"),
			PrintChart = me.getComponent("PrintChart");
			
		PrintList.clearData();
		PrintContent.update("");
		if (PrintChart) PrintChart.clearData();
	},
    //参数转换
	changeParams: function (params) {
	    return params;
	}
});