$(function() {
	//外部参数
	//var params = JcallShell.getRequestParams(true);
	var params = JcallShell.getEncryptionRequestParams(true);//解析加密后的url，再获取参数
	
	//获取报告服务地址
	var GET_REPORT_LIST_URL = JcallShell.System.Path.ROOT + 
		"/ServiceWCF/ReportFormService.svc/SelectReport";
	
	//加载第几页
	var PAGE = 1;
	//每页加载多少条数据
	var LIMIT = 10;
	//查询条件
	var WHERE = "";
	
	//报告时间字段
	var DATE_FIELD = "RECEIVEDATE";
	//套餐组合名称字段
	var ITEM_NAME_FIELD = "ItemName";
	//医院微平台传参数：病人就诊卡号、健康卡号，报告检验日期等查询报告
	//支持接收的参数,病历号、时间字段、开始日期、结束日期
	var PARAMS_ARRAY = ["CNAME","PATNO","DATEFIELD","START","END"];
	

	//获取列表数据
	function getListData(callback) {
		var url = GET_REPORT_LIST_URL;
		var fields = [
			'ReportFormID','SECTIONNO','CNAME','SectionType',
			'RECEIVEDATE','CHECKDATE','ItemName'
	    ];
	    fields.push(DATE_FIELD,ITEM_NAME_FIELD);
	    
	    url += "?fields=" + fields.join(",");
	    url += "&page=" + PAGE + "&limit=" + LIMIT;
	    url += "&where=" + WHERE;
	    
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
	function quchong(repeatString){
		var arr=repeatString.split(',')
		var len = arr.length;
		arr.sort();
		for(var i=len-1;i>0;i--){
			if(arr[i]==arr[i-1]){
			arr.splice(i,1);
			}
		}
		return arr;
	}
	  
	//更改列表内容
	function changeListHtml(list) {
		var templet = getRowTemplet(),
			html = [];
		for(var i in list) {
			var row = templet;
			row = row.replace(/{ReportFormID}/g, list[i].ReportFormID);
			row = row.replace(/{SectionNo}/g, list[i].SECTIONNO);
			row = row.replace(/{SectionType}/g, list[i].SectionType);
			row = row.replace(/{DATE}/g, JcallShell.Date.toString(list[i][DATE_FIELD],true));
			//row = row.replace(/{CNAME}/g, list[i].CNAME || "");	
			//row = row.replace(/{ItemName}/g, list[i][ITEM_NAME_FIELD] || "");		
	  		var itemname = quchong(list[i][ITEM_NAME_FIELD]);	  
			row = row.replace(/{ItemName}/g, itemname || "");	
			html.push(row);
		}
		
		$("#list").append(html.join(""));
	}
	//获取列表行模板
	function getRowTemplet() {
		var templet =
			'<div class="list-div" style="padding:10px 0;border-bottom:1px dashed #e0e0e0;" onclick="showInfo(\'{ReportFormID}\',\'{SectionNo}\',\'{SectionType}\');">' +
				'<div style="float:left;">' +
					'<div><b>{DATE}</b><span style="color:#169ada;margin-left:5px;">{ItemName}</span></div>' +
	            '</div>' +
	            '<div style="float:right;color:#e0e0e0;"><b>〉</b></div>' +
			'</div>';
		return templet;
	}
	//显示错误信息
	function showError(msg) {
		$("#list").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}

	function showInfo(ReportFormID,SectionNo,SectionType) {
		//跳转到信息页面
		var originalWhere = "ReportFormID=" + ReportFormID + "&SectionNo=" + SectionNo +"&SectionType=" + SectionType;	
		var encryptionWhere =  window.btoa(window.encodeURIComponent(originalWhere));//加密url
		
		location.href = "info.html?"+encryptionWhere;
	}
	window.showInfo = showInfo;
	//初始化页面
	function initHtml() {
		//获取列表数据
		getListData(function(data) {
			if(data.success == true) {
				//更改列表内容
				var list = data.value ? (data.value.rows || []) : [];
				if(list.length == 0){
					if(PAGE == 1) {
						showError("没有找到报告！");
					}
				}else{
					if(list.length < LIMIT) {
						$("#button-loadmore").hide();
					} else {
						$("#button-loadmore").show();
					}
					/*$("#total").html("共" + data.value.total + "条");
					$("#total").show();*/
					changeListHtml(list);
				}
			} else {
				if(PAGE == 1){
					$("#button-loadmore").hide();
					showError(data.msg);
				}
			}
		});
	}
	
	//获取条件
	function getWhere(){
		var where = [];
		
		for(var i in params){
			if($.inArray(i,PARAMS_ARRAY)!=-1){
				//时间字段
				if(i== "DATEFIELD" && params.DATEFIELD){
					var dateWhere = [];
					if(params.START){
						dateWhere.push(params.DATEFIELD + ">='" + JcallShell.Date.toString(params.START,true) + "'");
					}
					if(params.END){
						dateWhere.push(params.DATEFIELD + "<'" + 
							JcallShell.Date.toString(JcallShell.Date.getNextDate(params.END),true) + "'");
					}
					where.push(dateWhere.join(" and "));
				}else if(i == "START" || i == "END"){
					//不处理
				}else{
					where.push(i + "='" + params[i] + "'");
				}
			}
		}
		
		where = where.join(" and ");
		
		return where;
	}
	
	$("#button-refresh").on("click",function(){
		PAGE = 1;
		$("#list").html("");
		initHtml();
	});
	$("#button-loadmore").on("click",function(){
		PAGE++;
		initHtml();
	});
	$("#list-gotologin").on("click",function(){
		window.location.href = JcallShell.System.Path.UI + '/report/login/userLogin.html'
	});
	
	//查询条件
	WHERE = getWhere();
	//初始化页面
	initHtml();
});