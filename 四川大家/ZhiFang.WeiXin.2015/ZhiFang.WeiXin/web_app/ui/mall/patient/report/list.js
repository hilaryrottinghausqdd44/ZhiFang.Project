$(function() {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	
	//获取报告列表服务地址
	var SEARCH_REPORT_LIST_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_SearchReportFormByPC";
	
	//获取报告列表数据
	function getReportData(callback) {
		var url = SEARCH_REPORT_LIST_URL + '?PayCode=' + params.PAYCODE;
		
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		//获取数据
		JcallShell.Server.ajax({
			showError:true,
			url: url
		}, function(data) {
			setTimeout(function(){$("#loading-div").modal("hide");},500);
			callback(data);
		});
	}
	
	//更改报告列表内容
	function changeReportListHtml(list) {
		var templet = getRowTemplet(),
			len = list.length,
			html = [];
		
		for(var i=0;i<len;i++) {
			var row = templet;
			row = row.replace(/{Name}/g, list[i].info.HospitalName);
			row = row.replace(/{VisitTime}/g, list[i].info.VisitTime);
			row = row.replace(/{ReportTime}/g, list[i].info.ReportTime);
			row = row.replace(/{Id}/g, list[i].info.ReportId);
			html.push(row);
		}
		
		$("#list").html(html.join(""));
	}
	//获取列表行模板
	function getRowTemplet() {
		var templet =
			'<div class="list-div">' +
				'<div style="float:left;padding-left:5px;">' +
					'<div style="color:#169ada;"><b>{Name}</b></div>' +
					'<div>就诊时间：{VisitTime}</div>' +
					'<div>报告时间：{ReportTime}</div>' +
				'</div>' +
				'<div style="float:left;width:100%;">' +
					'<div class="list-div-button" onclick="showInfo(\'{Id}\')">详情</div>' +
				'</div>' +
			'</div>';
		return templet;
	}
	//显示错误信息
	function showError(msg) {
		$("#list").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}

	function showInfo(id) {
		//跳转到信息页面
		location.href = "info.html?id=" + id;
	}
	window.showInfo = showInfo;
	
	//查询数据
	function onSearch(){
		//获取套餐列表数据
		getReportData(function(data) {
			if(data.success == true) {
				var list = data.value || [];
				if(list.length > 0){
					changeReportListHtml(list);//更改报告列表内容
					document.documentElement.scrollTop = document.body.scrollTop =0;
				}else{
					showError("没有找到数据！");
				}
			} else {
				showError(data.msg);
			}
		});
	}
	
	//初始化页面
	function initHtml() {
		//查询数据
		onSearch();
	}
	
	//初始化页面
	initHtml();
});