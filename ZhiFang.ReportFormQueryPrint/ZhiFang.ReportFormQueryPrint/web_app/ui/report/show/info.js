$(function() {
	//外部参数
	//var params = JcallShell.getRequestParams(true);
	 var params = JcallShell.getEncryptionRequestParams(true);//解析加密后的url，再获取参数
	
	//获取报告结果
	var GET_REPORT_INFO_URL = JcallShell.System.Path.ROOT + 
		"/ServiceWCF/ReportFormService.svc/PreviewReportExtPageName?PageName=Mobile";
		
	var MODELTYPE = "result";
	
	function getInfoData(callback){
		var url = GET_REPORT_INFO_URL + "&ReportFormID=" + params.REPORTFORMID + "&SectionNo=" + params.SECTIONNO +
		"&SectionType=" + params.SECTIONTYPE + "&ModelType=" + MODELTYPE;
		
		ShellComponent.mask.loading();
		//获取数据
		JcallShell.Server.ajax({
			url:url,
			showError:true
		}, function(data) {
			setTimeout(function() {
				ShellComponent.mask.hide();
				callback(data);
			}, 100);
		});
	}
	//初始化页面
	function initHtml() {
		//获取列表数据
		getInfoData(function(data) {
			if(data.success == true) {
				$("#info").html(data.value);
			} else {
				showError(data.msg);
			}
		});
	}
	
	$("#button-back").on("click",function(){
		history.go(-1);
	});
	$("#button-refresh").on("click",function(){
		initHtml();
	});
	
	initHtml();
});