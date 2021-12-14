$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);

	//获取报告列表服务地址
	//var SEARCH_REPORT_LIST_URL = JcallShell.System.Path.ROOT +"/ServiceWCF/ReportFromService.svc/SelectReportListByPerson_Barcode_Name_DataRoundType";
	var SEARCH_REPORT_LIST_URL = JcallShell.System.Path.ROOT +"/ServiceWCF/ReportFromService.svc/SelectReportListByPerson_Patno_Name_DataRoundType";
	//alert('Barcode = ' + params.STRBARCODE + ' & Name=' + params.STRUSERACCOUNT);
	//选中的报告类型,1=一个月；2=半年内；3=半年前
	var CHECKED_DATE_TYPE = params.TYPE;

	//当前页码
	var PAGE = 1;
	//每页数量
	var LIMIT = 20;

	//获取报告列表数据
	function getReportData(callback) {
		if (!CHECKED_DATE_TYPE) {
			showError("必须选一个时间类型！");
			return;
		}

		//var url = SEARCH_REPORT_LIST_URL + '?DataRoundType=' + CHECKED_DATE_TYPE + '&page=' + PAGE + '&rows=' + LIMIT + '&Barcode=' + params.STRBARCODE + '&Name=' + params.STRUSERACCOUNT;

		var url = SEARCH_REPORT_LIST_URL + '?DataRoundType=' + CHECKED_DATE_TYPE + '&page=' + PAGE + '&rows=' + LIMIT + '&Patno=' + params.STRBARCODE + '&Name=' + params.STRUSERACCOUNT;

		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		//获取数据
		JcallShell.Server.ajax({
			showError: false,
			url: url
		}, function (data) {
			setTimeout(function () { $("#loading-div").modal("hide"); }, 500);
			callback(data);
		});
	}

	//更改报告列表内容
	function changeListHtml(list) {
		var templet = getRowTemplet(),
			len = list.length,
			html = [];

		for (var i = 0; i < len; i++) {
			var row = templet;
			row = row.replace(/{Name}/g, list[i].CLIENTNAME);
			row = row.replace(/{VisitTime}/g, list[i].COLLECTDATE.substr(0,10));
			row = row.replace(/{ReportTime}/g, list[i].CHECKDATE.substr(0, 10));
			row = row.replace(/{Id}/g, list[i].ReportFormID);
			html.push(row);
		}

		$("#list").append(html.join(""));
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
		$("#list").html('<div style="float:left;width:100%;padding:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}

	function showInfo(id) {
		//跳转到信息页面
		location.href = "info.html?id=" + id;
	}
	window.showInfo = showInfo;

	//查询数据
	function onSearch() {
		if (PAGE == 1) {
			$("#list").html("");
		}
		//获取报告列表数据
		getReportData(function (data) {
			if (data.success == true) {
				//更改列表内容
				var list = data.ResultDataValue ? (JSON.parse(data.ResultDataValue).rows || []) : [];
				if (list.length == 0) {
					if (PAGE == 1) {
						showError("没有找到报告！");
					}
				} else {
					if (list.length < LIMIT) {
						$("#button-loadmore").hide();
					} else {
						$("#button-loadmore").show();
					}
					changeListHtml(list);
				}
			} else {
				if (PAGE == 1) {
					$("#button-loadmore").hide();
					showError(data.msg);
				}
			}
		});
	}

	//初始化页面
	function initHtml() {
			//查询数据
			onSearch();
	}

	//加载更多按钮处理
	$("#button-loadmore").on("click", function () {
		PAGE++;
		initHtml();
	});

	//不同状态的查询条件切换
	var CHECKED_DIV = null
	$(".div_uncheck").on("click", function () {
		if (CHECKED_DIV) {
			CHECKED_DIV.removeClass("div_checked");
		}
		CHECKED_DIV = $(this);
		CHECKED_DIV.addClass("div_checked");
		CHECKED_DATE_TYPE = CHECKED_DIV.attr("data");
		initHtml();
	});
	if (CHECKED_DATE_TYPE) {
		$(".div_uncheck").each(function () {
			if ($(this).attr("data") == CHECKED_DATE_TYPE) {
				$(this).click();
			}
		});
	} else {
		$(".div_uncheck")[0].click();
	}

});