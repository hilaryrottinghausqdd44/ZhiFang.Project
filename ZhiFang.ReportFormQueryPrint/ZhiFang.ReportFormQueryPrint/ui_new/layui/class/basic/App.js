/**
 * 站点页面基础类
 * @author 王耀宗
 * @version 2021-4-26
 */

layui.extend({
	uxutil: 'ux/util',
	formExtend: 'ux/form/app'
}).use(['uxutil', 'table', 'form','element','laydate','formExtend'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		table = layui.table,
		$ = layui.jquery,
		form = layui.form,
		element = layui.element,
		formExtend = layui.formExtend,
		laydate = layui.laydate;
	var app = {};
	//服务地址
	app.url = {
		//获得左侧列表数据   
		getLeftTableDataUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/SelectReport',
		getRightReportUrl: uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/GetReportFormPDFByReportFormID",
		getRightResultUrl: uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/PreviewReport",
		getAllPublicSettingUrl: uxutil.path.ROOT +'/ServiceWCF/DictionaryService.svc/GetAllPublicSetting',
		/**表单部分**/
		getFormSettingUrl: uxutil.path.ROOT + "/ServiceWCF/DictionaryService.svc/GetBModuleFormControlSetByFormCode?isPlanish=true",
		getGridSettingUrl: uxutil.path.ROOT + "/ServiceWCF/DictionaryService.svc/GetBModuleGridControlSetByGridCode?isPlanish=true",
		
	};
	//表格的表头类型
	app.HTML_Table_Config ={
		"1001": {
			"name": "常规列",
			"code": "normal"
		},
		"1002": {
			"name": "复选列",
			"code": "checkbox"
		},
		"1003": {
			"name": "单选列",
			"code": "radio"
		},
		"1004": {
			"name": "序号列",
			"code": "numbers"
		},
		"1005": {
			"name": "空列",
			"code": "space"
		}
	}
	//table列
	app.cols = {
		left: [
			[
				{
					type: 'numbers',
					title: '序号'
				},
				
				{
					field: 'CNAME',
					width: 60,
					title: '姓名',
					sort: false,
					//hide: true
				},
				
				{
					field: 'ReportFormID',
					title: '报告id',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'CHECKTIME',
					title: '审核时间',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'RECEIVEDATE',
					title: '核收日期',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'SectionType',
					title: '小组类型',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'SECTIONNO',
					title: '小组编号',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'PageName',
					title: '报告类型',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'PageCount',
					title: '报告页数',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'PatNo',
					title: '病历号',
					width: 100,
					hide: true,
					sort: false
				}
			]
		]
	};
	//全局设置参数
	app.config={
		isviewportHeader:true,//是否显示标题栏
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
		MaxDownLoadNum:100,
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
		
		//是否开启部分审核报告
		IsbTempReport:false,
		
		//列表宽度
		listWidth:'',
		//是否查询request表
		IsQueryRequest:false,
		//是否查询样本状态
		IsSampleState:false,
		//列表格式
		ListClassName: 'Shell.class.basic.List',
		//历史对比的查询日期类型,  审核（报告）时间==>CHECKDATE  核收日期==>RECEIVEDATE
		HistoryCompareDateField:'CHECKDATE',
		//历史对比的默认查询天数
		HistoryCompareDefaultDates: 90,
		//历史对比框默认状态，
		HistoryDefaultCollapsed: 'true', 
		//报告的结果功能，查询数据时排序的字段
		sortFields: '',
		//时间查询条件的限制天数
		queryDateRange:180,
		//是否新窗口加载iframe预览打印
		NewWindowLoadIframeToPrint:'false',
		//是否使用c_lodop打印
		IsUseClodopPrint:'false'
	}
	//get参数
	app.paramsObj = {
		patno: '0000029053'//病例号	
	};
	app.appType='';
	
	
	/**右侧页面内容*/
	app.pageContent = { report: null, result: null };
	/**数据条件对象*/
	app.serverParams = null;
	//初始化  
	app.init = function () {
		var me = this;
		me.appType=$("#appType").html();//获取是哪个站点
		me.getAllPublicSetting();//获取全局配置
		$("#rightContentBody").css("height", ($(window).height() - 79) + "px"); //设置中间容器高度
		$("#contentDiv").css("height", ($(window).height() - 106) + "px");
		//设置默认选择的页签	
		if (me.config.hasReportPage) {
			$("#pageTypeDiv").append('<input id="yulan" type="radio" name="PageType" value="yulan" title="预览" lay-filter="switchTest">');
			if (me.config.defaultCheckedPage == 1) {
				$("#yulan").attr("checked", "");
			}
			form.render('radio', 'checkedPageType')//更新单选框	
		}
		if (me.config.hasResultPage) {
			$("#pageTypeDiv").append('<input id="jieguo" type="radio" name="PageType" value="jieguo" title="结果" lay-filter="switchTest">');
			if (me.config.defaultCheckedPage == 2) {
				$("#jieguo").attr("checked", "");
			}
			form.render('radio', 'checkedPageType')//更新单选框
		}
		//是否显示标题栏
		if(!me.config.isviewportHeader){
			$("#title").css("display","none")
		}
		//是否收起查询框
		if(me.config.headCollapsed){
			$("#layuiCollaContent").removeClass("layui-show");
		}
		//me.getParams();
		me.getFormSetting();
		me.initLeftTable();
		me.listeners();	
	};
	
	//获得url参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.PATNO) {
			me.paramsObj.patno = params.PATNO;
		}		
	};
	/***********************************************全局******************************************************* */
	//监听事件
	app.listeners = function () {
		var me = this;
		//日期范围
		laydate.render({
			elem: '#selectdateValue'
			, range: '~'
		});
		//表格头工具栏事件
		table.on('toolbar(toolbarDemo)', function(obj){
			switch(obj.event){
				case 'refresh':
					//刷新
					
				break;
				case 'previewprint':
					//预览打印
					
				break;
				case 'mergeprint':
					//汇总打印
					
				break;
				case 'ProjectContrast':
					//项目对比
					
				break;
				case 'download':
					//下载
					
				break;
			};
		});
		//监听左侧列表行单击事件
		table.on('row(leftTable)', function (obj) {
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			//获取单机行的信息供右侧查询报告		 
			var reportData = obj.data;
			
			//获取页签类型：返回值yulan或jieguo
			var ResultTypeValue = me.getResultTypeValue();
			//根据页签类型获取pdf还是文字报告
			//右侧内容填充
			me.serverParams = reportData;
			me.changeContent({
				ReportFormID: me.serverParams.ReportFormID,
				SectionNo: me.serverParams.SECTIONNO,
				SectionType: me.serverParams.SectionType,
				RECEIVEDATE: me.serverParams.RECEIVEDATE
			}, false, 1);
		});
		//监听页签切换
		form.on('radio(switchTest)', function (data) {
			
			me.changeContent(null, true);
		});  
		//未打印复选框点击事件
		form.on('checkbox(unPrintCheckbox)', function(data){
			
			// console.log(data.elem.checked); //是否被选中，true或者false
			// console.log(data.value); //复选框value值，也可以通过data.elem.value得到
			
		}); 
		//过滤复选框点击事件
		form.on('checkbox(filterCheckbox)', function(data){
			
			// console.log(data.elem.checked); //是否被选中，true或者false
			// console.log(data.value); //复选框value值，也可以通过data.elem.value得到
			
		}); 
	};

	/**开启功能栏*/
	app.enableControl = function () {
		this.disableControl(true);
	};
	//获得全局设置
	app.getAllPublicSetting=function(){
		var me = this;
		var url = me.url.getAllPublicSettingUrl+'?pageType=' + me.appType;
		uxutil.server.ajax({
			url: url,
			async: false
		}, function (data) {
			if (data) {
				if (!data.success) {
					//layer.msg(data.ErrorInfo);
					return;
				}
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
						//me[dataListName] = value.list;
					} else {
						value = value + "";
					}
				}
				if (!value) return;
				//对全局设置数据格式做处理
				var items = value.list;
                for (var i = 0; i < items.length; i++) {
                    var item = items[i];
                    if (item.ParaDesc == 'bool') {
                        item.ParaValue = item.ParaValue === 'true' ? true : false;
                    }
                    if (item.ParaDesc == 'int') {
                        item.ParaValue = parseInt(item.ParaValue);
                    }
                    if (item.ParaDesc == 'stringArry') {
                        if (item.ParaValue == '') {
                            item.ParaValue = [];
                        } else {
                            item.ParaValue = item.ParaValue.split(',');
                        }
                    }
                    if (item.ParaNo == 'ForcedPagingField') {
                        if (item.ParaValue == '') {
                            item.ParaValue = '';
                        } else {
                            item.ParaValue = { dataIndex: item.ParaValue, text: '' };
                        }
                    }
                    if(item.ParaNo == 'isviewportHeader'){
                    	isviewportHeader = item.ParaValue;
                    }
					if(item.ParaValue){
						me.config[item.ParaNo] = item.ParaValue;
					}
                    
                }
				//处理结束
				
						
			} else {
				//layer.msg(data.msg);
			}
		});
	}

	
	/**禁用功能栏*/
	app.disableControl= function (bo) {
		//var me = this,
		//	type = me.getComponent('toptoolbar').getComponent('type'),
		//	items = type.items.items,
		//	len = items.length;

		//for (var i = 0; i < len; i++) {
		//	items[i][bo ? "enable" : "disable"]();
		//}
		
		var input=$("input[name='PageType']");
	}
	/***********************************************查询条件******************************************************* */
	//获得动态表单配置
	app.getFormSetting = function() {
		var me = this,
			indexs = layer.load(),
			url = me.url.getFormSettingUrl + "&where=IsUse=1 and FormCode='LabBetweenComparisonRateForm'&sort=[{'property': 'DispOrder','direction': 'ASC'}]&page=1&limit=100";
		uxutil.server.ajax({
			url: url
		}, function (res) {
			layer.close(indexs);
			if(res.success) {
				if(res.ResultDataValue) {
					var data = JSON.parse(res.ResultDataValue).list;
					if(data.length > 0) {
						var html = "";
						$.each(data, function(i, itemI) {
							var objI = JSON.parse(JSON.stringify(itemI).replace(/BModuleFormControlList_/g, ""));
							html += formExtend.render(objI, "CreateHtml");
						});
						$("#conditionsForm").append(html);
						$.each(data, function(j, itemJ) {
							var objJ = JSON.parse(JSON.stringify(itemJ).replace(/BModuleFormControlList_/g, ""));
							formExtend.render(objJ, "initData");
						});
						form.render();
					}
				}
			} else {
				layer.msg(res.ErrorInfo, {
					icon: 5,
					anim: 6
				});
			}
		});
	};
	
	/***********************************************List******************************************************* */
	//获得动态表格配置
    app.getGridSetting = function () {
        var me = this,
            url = me.url.getGridSettingUrl + "&where=IsUse=1 and GridCode='" + me.GridCode + "' and (LabID='' or LabID is null) &sort=[{'property': 'DispOrder','direction': 'ASC'}]";
        uxutil.server.ajax({
			url: url
		}, function (res) {
            if (res.success) {
                
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        var cols = [];
                        $.each(data, function (i, item) {
                            //拼接列头
                            var obj = {};
                            if (item.BModuleGridControlList_TypeID) obj["type"] = me.HTML_Table_Config[item.BModuleGridControlList_TypeID]["code"];
                            if (item.BModuleGridControlList_MapField) obj["field"] = item.BModuleGridControlList_MapField;
                            if (item.BModuleGridControlList_ColName) obj["title"] = item.BModuleGridControlList_ColName;
                            if (item.BModuleGridControlList_Width) obj["width"] = item.BModuleGridControlList_Width;
                            if (item.BModuleGridControlList_MinWidth) obj["minWidth"] = item.BModuleGridControlList_MinWidth;
                            if (item.BModuleGridControlList_Fixed) obj["fixed"] = item.BModuleGridControlList_Fixed;
                            if (item.BModuleGridControlList_IsHide) obj["hide"] = String(item.BModuleGridControlList_IsHide) == "true" ? true : false;
                            if (item.BModuleGridControlList_IsOrder) obj["sort"] = String(item.BModuleGridControlList_IsOrder) == "true" ? true : false;
                            if (item.BModuleGridControlList_Edit) obj["edit"] = item.BModuleGridControlList_Edit;
                            if (item.BModuleGridControlList_StyleContent) obj["style"] = item.BModuleGridControlList_StyleContent;
                            if (item.BModuleGridControlList_Align) obj["align"] = item.BModuleGridControlList_Align;
                            if (item.BModuleGridControlList_Toolbar) obj["toolbar"] = String(item.BModuleGridControlList_Toolbar) == "false" ? false : item.BModuleGridControlList_Toolbar;
                            if (item.BModuleGridControlList_ColData) {
                                var str = "{" + item.BModuleGridControlList_ColData + "}";
                                var colDataObj = eval("(" + str + ")");
                                obj = $.extend({}, colDataObj, obj);
                            }
                            cols.push(obj);
                            //获得初始排序
                            if (String(item.BModuleGridControlList_IsOrder) == "true" && (item.BModuleGridControlList_OrderType.toUpperCase() == "ASC" || item.BModuleGridControlList_OrderType.toUpperCase() == "DESC")) {
                                me.tableInitialSort.push({ "property": (item.BModuleGridControlList_MapField.indexOf("_") != -1 ? item.BModuleGridControlList_MapField.split("_")[1] : item.BModuleGridControlList_MapField), "direction": item.BModuleGridControlList_OrderType });
                            }
                        });						
                        me.initTable('', cols);
                    }
                } else {
                    me.initTable('', []);
                    layer.msg("未配置表格列头！");
                }
            } else {
                layer.msg(res.ErrorInfo, { icon: 5, anim: 6 });
            }
        });
    };
	//初始化左侧列表  
	app.initLeftTable = function () {
		var me = this;			
		var where = "PATNO='" + me.paramsObj.patno+"'";
		//var where = "PATNO='18000887Ab'";
		//page和limit默认会传
		var url = me.url.getLeftTableDataUrl + "?fields=" + me.GetLeftTableFields(me.cols.left[0], true) + "&Where=" + where  + "&t=" + new Date().getTime();		
		table.render({
			elem: '#leftTable',
			height: 'full-80',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: url,
			cols: me.cols.left,
			limit: 50,
			limits:[10,20,50,100,200],
			autoSort: true, //禁用前端自动排序
			toolbar: '#toolbarDemo',	
			defaultToolbar: ['filter'],
			text: {        
				none: '暂无相关数据'
			},
			response: function () {
				return {
					statusCode: true, //成功状态码	
					statusName: 'code', //code key	 
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态		
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.total || 0, //解析数据长度	
					"data": data.rows || []
				};
			},
			done: function (res, curr, count) {
				//table渲染完毕后，右侧预览第一条报告内容
				if (res) {
					var data = res.data ? res.data : [];					
					if (data.length>0) {
						var reportformid = data[0].ReportFormID;
						me.serverParams = data[0];
						//右侧内容填充
						me.changeContent({
							ReportFormID: me.serverParams.ReportFormID,
							SectionNo: me.serverParams.SECTIONNO,
							SectionType: me.serverParams.SectionType,
							RECEIVEDATE: me.serverParams.RECEIVEDATE
						},false,1);
                    }
                }
				
			}
		});
	};
	//获取查询Fields	
	app.GetLeftTableFields = function (col, isString) {
		var me = this,
			columns = col || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};

		
	
	
	/***********************************************Content******************************************************* */
	/**获取返回数据类型*/
	app.getResultTypeValue= function () {
		var me = this;
		//获取选择的页签类型	
		var pageTypeData = form.val('checkedPageType');//获得单选框的value
		return pageTypeData.PageType;
	}
	/**@public 更改内容，isPrivate为false相当于点击左侧列表，为true相当于页签切换*/
	app.changeContent=function (params, isPrivate, isenableControl) {
		var me = this,
			type = me.getResultTypeValue(),
			params = isPrivate ? me.serverParams : params,
			isenableControl = isenableControl || 2;
		if (!isPrivate) me.pageContent = { report: null, result: null };
        /**
		 * 打印必须先安装adobe组件，如果没有安装，弹出安装文件下载
		 */
		//报告页签开启了才判断
		if (me.config.hasReportPage && type == "yulan") {
			var isAcrobatPluginInstall = uxutil.Adobe.isInstall();
			if (!isAcrobatPluginInstall) {
				me.enableControl();
				me.pageContent.report = uxutil.Adobe.getDownLoadHtml();
			}
		}
		var innerHTML = type == "yulan" ? me.pageContent.report : me.pageContent.result;
		if (innerHTML) {//已存在,不需要重新加载
			me.updateRightContent(innerHTML);
			return;
		}
		if (!isPrivate && (!params || !params.ReportFormID || !params.SectionNo || !params.SectionType)) {
			var errorInfo = "";
			if (!params) {
				
				layer.open({ title: false, content: 'layui.class.labstar.indexjs的changeContent方法没有接收到参数对象!' });
				return;
			}
			errorInfo +=("layui.class.labstar.indexjs的changeContent方法接收的参数对象有错!</br>");
			if (!params.ReportFormID) { errorInfo +="<b style='color:red'>ReportFormID</b>参数错误!</br>"; }
			if (!params.SectionNo) { errorInfo +="<b style='color:red'>SectionNo</b>参数错误!</br>"; }
			if (!params.SectionType) { errorInfo +="<b style='color:red'>SectionType</b>参数错误!</br>"; }
			layer.open({ title: false, content: errorInfo })
			return;
		}
		me.serverParams = params;
		me.getContent(me.serverParams, function (text) {
			var result = text,
				html = "";
			if (result.success) {
				html = result.ResultDataValue;
			} else {
				html =
					'<div style="margin:20px 10px;color:red;text-align:center;">' +
					'<div><b style="font-size:16px;">错误信息</b></div>' +
					'<div>' + result.ErrorInfo + '</div>' +
					'</div>';
			}
			if (!html) html = '<div style="margin:20px 10px;text-align:center;"><b>没有数据</b></div>';
			me.updateRightContent(html);

			me.pageContent[type == "yulan" ? "report" : "result"] = html;//储存页面信息

			if (isenableControl == 1) {

			} else {
				me.enableControl();
			}
		});
	}
	/*获取报告内容*/
	app.getContent = function (params, callback) {
		var me = this,
			type = me.getResultTypeValue(),
			ModelType = (type == "yulan" ? "report" : "result");
		if (params.ReportFormID == null || params.SectionNo == null || params.SectionType == null) {
			$("#contentDiv").text("layui.class.labstar.indexjs.getContent方法参数params的内容错误！");
			
			return;
		}
		if (ModelType == "report") {
			//生成报告
			var url = me.url.getRightReportUrl + "?ReportFormID=" + params.ReportFormID + '&t=' + new Date().getTime();
			me.getToServer(url, function (v) {
				var data = eval(v);
				var entity = {
					success: true,
					ResultDataValue: '<div style="text-align:center;padding:20px;"><b>报告文件不存在！<b></div>'
				};
				if (data.success) {
					var rfid = params.ReportFormID;
					var path = eval('(' + data.ResultDataValue + ')').PDFPath + '?t=' + new Date().getTime();
					entity.ResultDataValue =
						'<iframe src="' + uxutil.path.ROOT + '/' + path +
						'"  frameborder="0" ' +
						' style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:98%;width:100%;' +
						'top:0px;left:0px;right:0px;bottom:0px;" ' +
						'></iframe>';
				}
				callback(entity);
			});
			return;
		}
		me.updateRightContent("");
		if (me.hasLoadMask) { me.body.mask(me.loadingText); }//显示遮罩层
		if (me.IsQueryRequest + "" == "true") {
			me.getImgeSrcUrl = "/ServiceWCF/ReportFormService.svc/PreviewRequest";
		} else {
			me.getImgeSrcUrl = "/ServiceWCF/ReportFormService.svc/PreviewReport";
		}
		var url = me.url.getRightResultUrl + "?ReportFormID=" + params.ReportFormID + "&SectionNo=" + params.SectionNo +
			"&SectionType=" + params.SectionType + "&ModelType=" + ModelType + "&sortFields=" + me.config.sortFields;
		me.disableControl();
		me.getToServer(url, callback);
	};
	//更新右侧内容区域
	app.updateRightContent = function (html) {
		$("#contentDiv").html(html);
	};
	
	app.getToServer = function (url, callback, async) {
		var bo = async === false ? false : true;
		$.ajax({
			type: "GET",
			url: url,
			async:bo,
			success: function (data) {
				if (callback instanceof Function) {
					callback(data);
                }
			},
			error: function (request, errorType, errorMessage) {
				var errorInfo = "[" + errorType + "] " + errorMessage;
				layer.open({ title: "请求错误", content: errorInfo })
			}
		});
    }
	
	//初始化调用入口
	app.init();
});