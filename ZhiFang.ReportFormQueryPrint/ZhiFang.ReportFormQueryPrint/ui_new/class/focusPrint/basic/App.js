/**
 * 打印应用
 * @author Jcall
 * @version 2018-09-03
  * 代码新包迁移
 * @author Jing
 * @version 2018-09-20
 */
Ext.define('Shell.class.focusPrint.basic.App',{
	extend:'Shell.ux.panel.AppPanel',
	
	title:'打印查询',
	layout:{type:'border',regionWeights:{north:3,west:2,south:1}},
	printCountSetting: 100,//批量打印数量配置
    /**默认数据条件,过滤没有文件的结果*/
	defaultWhere: '',//'resultsend=1',
	/**外部数据条件*/
	externalWhere:'',
	/**错误信息*/
	errorInfo: [],
    appType:'',//页面类型
    /**A4纸张类型，1(A4) 2(16开)*/
	A4Type: 1,
    /**默认打印类型*/
    printType:'A4',
    /**默认勾选过滤框*/
	checkFilter: false,
    /**默认勾选未打印框*/
	checkUnprint: false,
    /**默认查询天数*/
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
	hisRequestParamsArr:[],
    /**默认勾选*/
	autoSelect: true,
	/**最大打印数量*/
    maxPrintTimes:2,
	
    /**报告时间字段*/
	DateField: 'RECEIVEDATE',
    /**强制分页字段*/
	ForcedPagingField: '',
    /**点击复选框才选中行*/
	CheckOnly: false,
	/**报告页签*/
    hasReportPage: true,
    /**结果页签*/
    hasResultPage: true,
    /**默认勾选的页签,1=报告页签，2=结果页签*/
    defaultCheckedPage:1,
    /**默认勾选双面打印*/
	checkDoublePrint: false,
	/**是否需要选择打印机*/
	hasPdfPrinter:false,
	/**PDF文件打印机数组*/
	pdfPrinterList: [],
	
	//当报告列表数量<=1时隐藏报告列表，直接显示报告内容
	isListHidden:false,
	
	//查询是否区分大小写
	isCaseSensitive:false,
	
	//列表宽度
	selectUrl: '/ServiceWCF/ReportFormService.svc/SelectReport?fields=RECEIVEDATE,CNAME,PatNo,SectionType,SECTIONNO,ReportFormID,PageName,PageCount&page=1&limit=9999',
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
		
		var QueryPanel = me.getComponent('QueryPanel'),
			List = me.getComponent('List');
		//查询栏监听
		if(QueryPanel){
			QueryPanel.on({
				search:function(com,where){
					var isWhereValid = me.isWhereValid();
					if (isWhereValid) {
						Ext.Ajax.defaultPOSTHander="application/json";
					    Ext.Ajax.request({
					        url: Shell.util.Path.rootPath + me.selectUrl+"&Where="+where,
					        async: false,
					        method: 'get',
					        success: function (response, options) {
					            var reponse = Ext.JSON.decode(response.responseText);
					            if (reponse.success) {
					            	var ResultDataValue =  Ext.JSON.decode(reponse.ResultDataValue);
					            	if(ResultDataValue.total == 0){
					            		Shell.util.Msg.showWarning("未查到报告单");
					            	}else{
					            		Shell.util.Msg.showMsg({
								            title:'打印确认',
								            msg:'确定要打印吗？',
								            icon:Ext.Msg.WARNING,
								            buttons:Ext.Msg.OKCANCEL,
								            callback:function (v) {
					                            if (v == 'ok') {
					                               console.log(ResultDataValue.rows);
					                               List.PrintClick(ResultDataValue.rows);
					                            }
					                        }
										});
					            		
					            	}
				               }else{
				               		Shell.util.Msg.showWarning(reponse.ErrorInfo);
				               }
					        }
					    });
					}else{
						Shell.util.Msg.showWarning('条件不完整（除日期外，需要起码一个其他条件才能查询）！');
					}
				}
			});
			//初始值
			var params = me.params,
				hasDate = false;
			 var field = QueryPanel.getItem("selectdate");
            if (field != "") {
                /**向查询框设置请求路径中的查询参数
                 */
				for(var i in params){
				//组装成默认条件
                    if (i == me.DateField) {//报告时间
                        var fieldText = QueryPanel.getItem("defaultPageSize");
                        fieldText.setValue(me.DateField);
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
			    } else {
                        if (Shell.util.String.inArray(i, me.hisRequestParamsArr)) continue;
                        if (i.indexOf("DATE")>=0) continue;
			        var Normalfield = QueryPanel.getFieldsByName(i);
			        Normalfield.setValue(params[i]);
			        //Normalfield.disable();
				}
			}
			
			if(!hasDate){
			   // var field = QueryPanel.getItem("selectdate");
			   
				da = new Date();
					
				var date = {
				    start: Shell.util.Date.getNextDate(da, 1 - me.defaultDates),
					end:da
				};
				
				if(!me.defaultDates || me.defaultDates <= 0){
					date = {start:null,end:null};
				}
				
				field.setValue(date);
			}
			}
		}
		
		if(me.isListHidden && List){
			List.hide();
			List.on({
				afterload:function(p,records,successful){
					var num = records.length;
					if(num > 1){
						List.show();
					}else{
						List.hide();
					}
				}
			});
		}
		
		//where条件是否符合规范,如果没有参数传入就不查询
		var isWhereValid = me.isWhereValid();
		if (isWhereValid) {
			me.onSearch(null,true);
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
			className:'Shell.class.focusPrint.basic.QueryPanel',
			itemId:'QueryPanel',region:'north',
			split: true, collapsible: true,
			appType: me.appType,
			headCollapsed: me.headCollapsed,
			isCaseSensitive:me.isCaseSensitive
		},{
			className:'Shell.class.focusPrint.basic.List',
			itemId: 'List', hide: true
		}];
		
		return apps;
	},
	
	/**查询*/
	onSearch:function(where,isPrivate){
		var me = this,
			List = me.getComponent('List');
			
		//where条件是否符合规范
		var isWhereValid = me.isWhereValid();
		if (isWhereValid) {
			List.internalWhere = List.getInternalWhere();
			List.load(where,isPrivate);
		}else{
			Shell.util.Msg.showWarning('条件不完整（除日期外，需要起码一个其他条件才能查询）！');
		}
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
			if(!pars[i]) continue;
		    //组装成默认条件

		    if (i.indexOf('DATE') > 0) {//报告时间
				var arr = pars[i].split(";");
				if(arr.length == 1){
					where.push(i + ">='" + arr[0] + "'");
				}else if(arr.length == 2){
					var da = [];
					if(arr[0]){da.push(i + ">='" + arr[0] + "'");}
					if(arr[1]){da.push(i + "<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(arr[1]),true) + "'");}
					where.push(da.join(" and "));
				}
				if(i==me.DateField){hasDate = true;}
		    } else {
                //定制：patno 传递多个参数
		        //if (i == 'PATNO') {
		        //    var str = pars[i].substring(1, pars[i].length - 1);
		        //    var arry = str.split(',');
		        //    var patno = "";
		        //    for (var k = 0; k < arry.length; k++) {
		        //        patno +="'"+ arry[k] + "',";
		        //    }
		        //    patno = patno.substring(0, patno.length - 1);
		        //    where.push(i + " in (" + patno+")");
		        //} else {
		        //    where.push(i + "='" + pars[i] + "'");
		        //}
		        where.push(i + "='" + pars[i] + "'");
			}
		}
		if(!hasDate && me.defaultDates && me.defaultDates > 0){
			var date = new Date(),
				start = Shell.util.Date.toString(Shell.util.Date.getNextDate(date, 1 - me.defaultDates), true),
				end = Shell.util.Date.toString(Shell.util.Date.getNextDate(date),true);
				da = [];
			
			if (me.DateField !=null && me.DateField !='') {
			    da.push(me.DateField + ">='" + start + "'");
			    da.push(me.DateField + "<'" + end + "'");
			    where.push(da.join(" and "));
			}
		}

		var pams = {};
		var whw = [];
		for (var i in params) {
		    var bo = Shell.util.String.inArray(i, me.hisRequestParamsArr);
		    if (bo) pams[i] = params[i];
		}
		for (var j in pams) {
		    whw.push(j + "='" + pams[j] + "'");
		}
		if (whw.length > 0) {
		    whw = "SerialNo " + whw.join(" and ");
		} else {
		    whw = "";
		}
		me.externalWhere = where.join(" and ") + whw;
	},
	/**清空所有数据*/
	clearData:function(){
	    var me = this,
            List = me.getComponent("List");
			
		List.clearData();
	},
    //参数转换
	changeParams: function (params) {
	    return params;
	},
	
	//where条件是否符合,必须存在除时间外的条件才能查询
	isWhereValid:function(){
		var me = this,
			QueryPanel = me.getComponent('QueryPanel'),
			searchObj = QueryPanel.getValues(),
			params = me.getParamsArray(),
			len = params.length,
			bo = false;
		var requestParam = Shell.util.Path.getRequestParams(true);
	
		if(searchObj['DeptNo']){
			bo = true;
		} else {
			for(var i=0;i<len;i++){
			    var field = params[i];
			    if (field.indexOf('DATE') >0) continue;//时间字段除外
				if (requestParam[field]) {
					bo = true;
					break;
				}
			}

			for (var k in searchObj) {
			    if (k.indexOf('DATE')>0) continue;
			    if (searchObj[k] !=null && searchObj[k] !='') {
			        bo = true;
			        break;
			    }
			}
			
			for (var j in requestParam) {
			    if (me.hisRequestParamsArr.length > 0 && Shell.util.String.inArray(j, me.hisRequestParamsArr)) {
			        bo = true;
			        break;
			    }
			}
		}
		
		return bo;
	}
});