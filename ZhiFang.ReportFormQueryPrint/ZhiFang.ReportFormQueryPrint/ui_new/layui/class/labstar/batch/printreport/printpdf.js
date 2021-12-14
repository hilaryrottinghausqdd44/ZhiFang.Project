/**
 * @name：打印pdf功能
 * @author wangyz
 * @version 2021-05-18
 */
 layui.extend({
	print:'ux/print'
}).define(['uxutil','print'],function(exports){
	"use strict";
	
	var layer = layui.layer,
		$ = layui.$,
		uxutil = layui.uxutil,
		print= layui.print;
	var printpdf={};
	//打印列表printList
	printpdf.printList=[];
	//打印消息列表
	printpdf.message="";
	//生成报告服务
	var createReportUrl=uxutil.path.ROOT +'/ServiceWCF/ReportFormService.svc/GetReportFormPDFByReportFormID';	
	/**增加打印次数服务路径*/
	var addPrintTimesUrl= uxutil.path.ROOT + '/ServiceWCF/ReportFormService.svc/ReportFormAddUpdatePrintTimes';
    /**增加打印次数服务路径*/
	var updateTestFormPrintCountUrl= uxutil.path.LABSTARPATH+'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestFormPrintCount';
	//测试
	printpdf.test = function(options){
		console.log(uxutil.path.ROOT);
		layer.msg("ceshi");
	};

	printpdf.print = function(reportList){
		//ZFPrint.init();
		var me=this;
		//先将打印列表置空
		this.printList=[];
		//将消息置空
		me.message="";
		$("#messageDiv").html(me.message);
		//测试PDF文件地址
		//var url = uxutil.path.ROOT + "/ReportFormFiles/aa.pdf";
		
		//先生成,每生成一份就向me.printList里存储一份
		me.onServerCreateReport(reportList,function(){
			//提示消息内容填充
			$("#messageDiv").html(me.message);
			//回调打印功能
			me.isPrinting(me.printList,function(){
				

			});
		});
		
	};
	//生成报告功能
	printpdf.onServerCreateReport=function(reportList,callback){
		var me=this;
		for(var i=0;i<reportList.length;i++){
			me.onServerCreateReportOne(reportList[i]);
		}
		callback();
	}
	//生成单份报告功能
	printpdf.onServerCreateReportOne=function(report,callback){
		var me=this;
		var url=createReportUrl+'?ReportFormID=' + report.ReportFormID + '&SectionNo=' + report.SectionNo + '&SectionType=' + report.SectionType;
		uxutil.server.ajax({
			url:url,
			async: false
		}, function (data) {
			if (data) {
				if(!data.success){
					me.message+="<div>报告单："+report.ReportFormID+"生成错误！---"+data.ErrorInfo+"</div>"
				}
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if (!value) return;
				
				me.printList.push({ReportFormId:value.ReportFormID,url:value.PDFPath,TestFormId:report.RFID});
			} else {
				layer.msg(data.msg);
			}
		});
	}
	printpdf.isPrinting=function(printList,callback){
		var me=this;
		var reportformIds=[];
		var testformIds=[];
		for(var i=0;i<printList.length;i++){
			
			//处理url为clodop可用的url
			var url = printList[i].url;
			var pdfurl= uxutil.path.ROOT + "/" + url;
			var finalUrl = encodeURI(pdfurl);//地址中有中文，必须转义clodop才能获取到pdf
			
			//打印
			print.instance.pdf.print(finalUrl,'PDF打印任务' + printList[i].ReportFormId,function(){
				//打印成功更新打印次数
				reportformIds.push(printList[i].ReportFormId);
				testformIds.push(printList[i].TestFormId);

			});
			//clodop预览打印
			//print.instance.pdf.preview([url,url,url,url]);
		}
		//更新打印次数
		me.addPrintTimes(reportformIds.join(','));
		me.addTestFormPrintCount(testformIds.join(','));
		callback();
	}
	//增加打印次数
	printpdf.addPrintTimes=function(ids){
		var me=this;
		var url=addPrintTimesUrl+"?reportformidstr=" + ids;
		uxutil.server.ajax({
			url:url,
			async: false
		}, function (data) {
			if (data) {
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if (!value) return;
			} else {
				layer.msg(data.msg);
			}
		});
	}
	//增加打印次数
	printpdf.addTestFormPrintCount=function(ids){
		var me=this;
		var url=updateTestFormPrintCountUrl;
		uxutil.server.ajax({
			url:url,
			async: false,
            type: 'post',
            data: JSON.stringify({ testFormID: ids })
		}, function (data) {
			if (data) {
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if (!value) return;
			} else {
				layer.msg(data.msg);
			}
		});
	}
	//暴露接口
	exports('printpdf',printpdf);
});