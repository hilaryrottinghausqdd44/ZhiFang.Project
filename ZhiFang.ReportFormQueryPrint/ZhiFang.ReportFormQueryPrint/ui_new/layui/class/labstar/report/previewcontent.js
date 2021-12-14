/**
 * 预览内容
 * @author 王耀宗
 * @version 2021-11-3
 */

layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form', 'laydate'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		
		$ = layui.jquery,
		
		form = layui.form;
	var app = {};
	//服务地址
	app.url = {
		//获得左侧列表数据   
		getLeftTableDataUrl: uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/SelectReport',
		getRightReportUrl: uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/GetReportFormPDFByReportFormID",
		getRightResultUrl: uxutil.path.ROOT + "/ServiceWCF/ReportFormService.svc/PreviewReport"
	};
	//get参数
	app.paramsObj = {
		contentValue: '暂无数据'	
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
		//$("#mainContent").css("height", ($(window).height() - 30) + "px"); //设置中间容器高度
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
		me.changeContent(me.serverParams, false, 1);
		
		me.listeners();	
	};
	//获得参数 
	app.getParams = function () {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.SERVERPARAMS) {
			me.paramsObj.serverParams = params.SERVERPARAMS;
			me.serverParams=JSON.parse(params.SERVERPARAMS);
		}	
			
	};
	//监听事件	
	app.listeners = function () {
		var me = this;
		
		//监听页签切换
		form.on('radio(switchTest)', function (data) {
			//console.log(data.elem); //得到radio原始DOM对象
			//console.log(data.value); //被点击的radio的value值
			me.changeContent(null, true);
		});  

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
						'" height="100%" width="100%" frameborder="0" ' +
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
	//初始化调用入口
	app.init();
});