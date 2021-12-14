/**
 * 相关医嘱报告
 * @author 王耀宗
 * @version 2021-4-26
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form', 'laydate'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		table = layui.table,
		$ = layui.jquery,
		laydate = layui.laydate,
		form = layui.form;
	var app = {};
	//服务地址
	app.url = {
		//获得左侧列表数据   
		getLeftTableDataUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/SelectReport',
		getRightReportUrl: uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/GetReportFormPDFByReportFormID",
		getRightResultUrl: uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/PreviewReport"
	};
	app.cols = {
		left: [
			[
				{
					type: 'numbers',
					title: '序号'
				},
				{
					field: 'RECEIVEDATE',
					width: 80,
					title: '日期',
					sort: false,
					templet: function(d){
						return uxutil.date.toString(d.RECEIVEDATE, true);
					  }
					//hide: true
				},
				{
					field: 'SectionName',
					width: 100,
					title: '小组',
					sort: false,
					//hide: true
				},
				{
					field: 'SAMPLENO',
					width: 60,
					title: '样本号',
					sort: false,
					//hide: true
				},
				{
					field: 'ParItemName',
					width: 120,
					title: '医嘱项目',
					sort: false,
					//hide: true
				},
				{
					field: 'CNAME',
					width: 60,
					title: '姓名',
					sort: false,
					//hide: true
				},
				{

					field: 'PatNo',
					title: '病例号',
					width: 100,
					//hide: true,
					sort: false
				},
				{
					field: 'ReportFormID',
					title: '报告id',
					width: 100,
					hide: true,
					sort: false
				},
				{
					field: 'SectionNo',
					title: '小组编号',
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
				} ,
				{ title: '操作', width: 100, align: 'center', toolbar: '#tableOperation', fixed: 'right' }
			]
		]
	};
	//get参数
	app.paramsObj = {
		patno: '123'//病例号	
	};
	app.dateWhere="";
	/**报告页签*/
	app.hasReportPage = true;
		/**结果页签*/
	app.hasResultPage = true;
	/**右侧默认勾选的页签*/
	app.defaultCheckedPage = 1;
	/**右侧页面内容*/
	app.pageContent = { report: null, result: null };
	/**数据条件对象*/
	app.serverParams = null;
	//初始化  
	app.init = function () {
		var me = this;
		$("#rightContentBody").css("height", ($(window).height() - 30) + "px"); //设置中间容器高度
		//设置默认选择的页签	
		if (me.hasReportPage) {
			$("#pageTypeDiv").append('<input id="yulan" type="radio" name="PageType" value="yulan" title="预览" lay-filter="switchTest">');
			if (me.defaultCheckedPage == 1) {
				$("#yulan").attr("checked", "");
			}
			form.render('radio', 'checkedPageType')//更新单选框	
		}
		if (me.hasResultPage) {
			$("#pageTypeDiv").append('<input id="jieguo" type="radio" name="PageType" value="jieguo" title="结果" lay-filter="switchTest">');
			if (me.defaultCheckedPage == 2) {
				$("#jieguo").attr("checked", "");
			}
			form.render('radio', 'checkedPageType')//更新单选框
		}
		me.getParams();
		me.initDateListeners();
		me.initLeftTable();
		me.listeners();	
	};
	
	//获得参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.PATNO) {
			me.paramsObj.patno = params.PATNO;
		}		
	};

	//初始化左侧列表  
	app.initLeftTable = function () {
		
		var me = this;			
		var where = "PATNO='" + me.paramsObj.patno+"'";
		//var where = "PATNO='18000887Ab'";
		//日期条件
		if(me.dateWhere){
			where +=me.dateWhere;
		}else{
			var today = new Date(),
					startDay=uxutil.date.toString(uxutil.date.getNextDate(today, -60), true),
					endDay=uxutil.date.toString(uxutil.date.getNextDate(today, 1), true);
					where+= " and ReceiveDate >= '" + startDay + "' and ReceiveDate <'" +endDay + "'";
		}
		
		//page和limit默认会传
		var url = me.url.getLeftTableDataUrl + "?fields=" + me.GetLeftTableFields(me.cols.left[0], true) + "&Where=" + where  + "&t=" + new Date().getTime();		
		table.render({
			elem: '#leftTable',
			height: 'full-55',//table高度
			size: 'sm',
			page: true,//分页开启		
			url: url,
			cols: me.cols.left,
			limit: 50,
			limits:[10,20,50,100,200],
			autoSort: true, //禁用前端自动排序		
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

	//监听事件	
	app.listeners = function () {
		var me = this;
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
		//监听表格工具条
		table.on('tool(leftTable)', function (obj) {
			var data = obj.data;
			if (obj.event === 'showPDF') {
				var win = $(parent.window),
					maxWidth = win.width()-60,
					maxHeight = win.height()-60;
					var serverParams=encodeURI(JSON.stringify(me.serverParams));
				parent.layer.open({
					title:'预览报告',
					type:2,
					content:uxutil.path.LOCAL + '/ZhiFang.ReportFormQueryPrint/ui_new/layui/class/labstar/report/previewcontent.html?serverParams='+serverParams,//$('#rightContentBodycard'),
					shade: 0,
					maxmin:true,
					toolbar:true,
					resize:true,
					area:[maxWidth+'px',maxHeight+'px'],
					success:function(layero,index){
					
					}
				});
			} else if (obj.event === 'showResult') {
				layer.open({
					title: '删除确认'
					, content: '确定要删除吗！'
					, btn: ['确认', '取消']
					, skin: 'layui-layer-molv' //样式类名
					, yes: function (index, layero) {
						//确认按钮
						var entity = { formno: [data.FormNo] };
						uxutil.server.ajax({
							url: me.url.deletePrintTimesUrl,
							async: false,
							type: "post",
							data: JSON.stringify(entity)
						}, function (data) {
							if (data) {
								if (data.success) {
									layer.msg("删除成功");

								} else {
									layer.msg("删除失败");
								}

							} else {
								layer.msg("删除失败");
							}
						});
						app.searchData();
						layer.close(index); //如果设定了yes回调，需进行手工关闭
					}
					, btn2: function (index, layero) {

					}
				});
			}
		});
		//监听页签切换
		form.on('radio(switchTest)', function (data) {
			//console.log(data.elem); //得到radio原始DOM对象
			//console.log(data.value); //被点击的radio的value值
			me.changeContent(null, true);
		});  

	};
	//填充右侧内容		
	app.fillRightContent = function (reportformid) {
		//alert(reportformid);
	};
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
		 * @JcallShell
		 * @version 2018-07-04
		 */
		//报告页签开启了才判断
		if (me.hasReportPage && type == "yulan") {
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
						'"frameborder="0" ' +
						'style="overflow:hidden;overflow-x:hidden;overflow-y:hidden;height:540px;width:100%;' +
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
			"&SectionType=" + params.SectionType + "&ModelType=" + ModelType + "&sortFields=" + me.sortFields;
		me.disableControl();
		me.getToServer(url, callback);
	};
	//更新右侧内容区域
	app.updateRightContent = function (html) {
		$("#contentDiv").html(html);
	};
	/**开启功能栏*/
	app.enableControl = function () {
		this.disableControl(true);
	};
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
    };
	
	//监听新日期控件
	app.initDateListeners = function () {
		var me = this,
			today = new Date();
		//赋值日期框
		$("#TestDate").val(uxutil.date.toString(uxutil.date.getNextDate(today, -60), true) + " ~ " + uxutil.date.toString(today, true));
		//监听检验日期图标
		$("#TestDate+i.layui-icon").on("click", function () {
			
			var elemID = $(this).prev().attr("id");
			var key = $("#" + elemID).attr("lay-key");
			if ($('#layui-laydate' + key).length > 0) {
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
			}
			var datatype = $("#" + elemID).attr("data-type");
			if (datatype == "text") {
				me.initDate();
				$("#" + elemID).attr("data-type", "date");
			} else {
				$("#" + elemID).attr("data-type", "text");
				var key = $("#" + elemID).attr("lay-key");
				$('#layui-laydate' + key).remove();
			}
		});
		//监听日期input -- 不弹出日期框
        $("#DateType").on('focus', '.DateValue', function () {
            me.preventDefault();
            layui.stope(window.event);
            return false;
        });
	};
	//阻止默认事件
    app.preventDefault = function () {
        var me = this,
            device = layui.device();
        if (device.ie) {
            window.event.returnValue = false;
        } else {
            window.event.preventDefault();
        }
	};
	//初始化新日期控件
	app.initDate = function () {
		var me = this,
			today = new Date();
		//查询日期
		laydate.render({//存在默认值
			elem: '#TestDate',
			eventElem: '#TestDate+i.layui-icon',
			type: 'date',
			range: '~',
			show: true,
			//value: uxutil.date.toString(uxutil.date.getNextDate(today, me.config.searchDays), true) + " - " + uxutil.date.toString(today, true),
			done: function (value, date, endDate) { 
				// console.log(value);
				if (value) {
					var info=me.isDateRangeValid(value);
					if(info.msg!=""){
						layer.msg(info.msg);
						return;
					}
					me.dateWhere = " and ReceiveDate >= '" + info.start + "' and ReceiveDate <'" +info.end + "'";
				}else{
					var today = new Date(),
					startDay=uxutil.date.toString(uxutil.date.getNextDate(today, -60), true),
					endDay=uxutil.date.toString(uxutil.date.getNextDate(today, 1), true);
					me.dateWhere= " and ReceiveDate >= '" + startDay + "' and ReceiveDate <'" +endDay + "'";
				}
				me.initLeftTable();
			}
		});
		
				//选完时间后直接查询列表
				//me.initLeftTable();
	};
	//判断日期范围格式是否正确：2021-01-01 ~ 2021-02-01
	app.isDateRangeValid=function(DateRange){
		var info={};
		var msg="";
		var start="";
		var end="";
		if (DateRange) {
			//验证日期是否正确
			if (DateRange.indexOf(" ~ ") == -1) {
				msg = "日期格式不正确!";
			}else{
				//验证是否都是日期
				var daterange=DateRange.split(" ~ ");
				if(daterange.length==2){
					start = daterange[0];
					end = daterange[1];
					var DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式
					if (!uxutil.date.isValid(start) || !DATE_FORMAT.test(start) || !uxutil.date.isValid(end) || !DATE_FORMAT.test(end)) {
						msg = "日期格式不正确!";
					}
					//验证开始日期是否大于结束日期
					//uxutil.date.difference()
					if (new Date(start).getTime() > new Date(end).getTime()) {
						msg = "开始日期不能大于结束日期!";
					}
					info.start=start;
					info.end=uxutil.date.toString(uxutil.date.getNextDate(end, 1), true)
				}else{
					msg = "日期格式不正确!";
				}
			}
		}
		
		info.msg=msg;
		return info;
	};
	//初始化调用入口
	app.init();
});