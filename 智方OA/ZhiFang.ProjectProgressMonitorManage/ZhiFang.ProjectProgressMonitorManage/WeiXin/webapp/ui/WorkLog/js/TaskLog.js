$(function () {
	//当前用户
	var EMPLOYEE_ID = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeID);
	var EMPLOYEE_NAME = Shell.util.Cookie.getCookie(Shell.util.Cookie.mapping.EmployeeName);
	//页面接收的参数
	var PARAMS = Shell.util.getRequestParams(true);
	//任务信息
	var TASK_INFO = null;
	//选中的页签
	CHECKED_TAB = null;
	//当前选中的工作量
	CHECKED_WORKLOAD = null;
	WORKLOAD_VALUE = null;
	//当前显示内容DIV
	var SHOW_CONTENT_DIV = null;
	//获取任务信息服务地址
	var GET_TASK_URL = '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskById?isPlanish=true' +
		'&fields=PTask_Id,PTask_CName,PTask_Contents,PTask_ExecutorID';
	//新增任务日志服务地址
	var ADD_TASKLOG_URL = '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkDayLog';
	
	//更改内容
	function changeContentHtml(type){
		if(!TASK_INFO){
			initTaskInfo(onActiveTab);
		}else{
			onActiveTab();
		}
	}
	//初始化任务信息
	function initTaskInfo(callback){
		Shell.util.Server.ajax({
			url:Shell.util.Path.getRootPath() + GET_TASK_URL + '&id=' + PARAMS.ID
		},function(data){
			initTaskHtml(data);
			callback();
		});
	}
	//初始化任务内容HTML
	function initTaskHtml(data){
		if(data.success){
			TASK_INFO = data.value;
			if(!TASK_INFO){
				showBack('<div class="error-div">没有找到任务信息！</div>');
			}else{
				if(TASK_INFO.PTask_ExecutorID == EMPLOYEE_ID){
					showContent();
					$("#task_title").html(TASK_INFO.PTask_CName);
					$("#page-content-div_2").html(TASK_INFO.PTask_Contents);
				}else{
					showBack('<div class="error-div">当前用户不是任务执行者，无权填写该任务日志！</div>');
				}
			}
		}else{
			showBack('<div class="error-div">没有找到任务信息！</br>' + data.msg + '</div>');
		}
	}
	//切换页面
	function onActiveTab(){
		var type = CHECKED_TAB.attr("data");
		if(SHOW_CONTENT_DIV){
			SHOW_CONTENT_DIV.hide();
		}
		SHOW_CONTENT_DIV = $("#page-content-div_" + type)
		SHOW_CONTENT_DIV.show();
	}
	
	//显示页面内容
	function showContent(){
		$("#back-div").hide();
		$("#info-div").hide();
		$("#content-div").show();
	}
	//显示错误内容
	function showBack(html){
		$("#content-div").hide();
		$("#info-div").hide();
		$("#back-content").html(html);
		$("#back-div").show();
	}
	//显示信息内容
	function showInfo(html){
		$("#content-div").hide();
		$("#back-div").hide();
		$("#info-content").html(html);
		$("#info-div").show();
	}
	//保存数据
	function save(){
		var entity = {
			PTask:{Id:PARAMS.ID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			EmpID:EMPLOYEE_ID,
			EmpName:EMPLOYEE_NAME,
			ToDayContent:$("#ToDayContent").val(),
			NextDayContent:$("#NextDayContent").val(),
			HasRisk:$("#HasRisk").bootstrapSwitch('state'),
			Workload:WORKLOAD_VALUE
		};
		
		if(!entity.ToDayContent){
			showInfo('<div class="error-div">错误信息</br>必须填写"今天总结"</div>');
			return;
		}
		
		Shell.util.Server.ajax({
			type: "POST",
			//data:Shell.util.JSON.encode({entity:entity}),
			data:JSON.stringify({entity:entity}),
			url:Shell.util.Path.getRootPath() + ADD_TASKLOG_URL
		},function(data){
			if(data.success){
				showBack('<div class="no-data-div">保存成功</br>如果不操作,3秒后自动返回</div>');
				setTimeout(function(){
					window.history.back();
				},3000);
			}else{
				showInfo('<div class="error-div">错误信息</br>' + data.msg + '</div>');
			}
		});
	}
	
	//渲染开关按钮
	$("#HasRisk").bootstrapSwitch('state',false);
	//页签点击监听
	$(".page-tab-div-tab").on("click",function(){
		if(CHECKED_TAB){
			CHECKED_TAB.removeClass("page-tab-div-tab-checked");
		}
		CHECKED_TAB = $(this);
		
		CHECKED_TAB.addClass("page-tab-div-tab-checked");
		
		changeContentHtml();
	});
	//返回按钮监听
	$(".page-top-back").on("click",function(){
		window.history.back();
	});
	
	//保存按钮监听
	$("#save").on("click",function(){
		save();
	});
	
	//工作量选择监听
	$(".workload-div").on("click",function(){
		if(CHECKED_WORKLOAD){
			CHECKED_WORKLOAD.removeClass("workload-div-checked");
		}
		CHECKED_WORKLOAD = $(this);
		WORKLOAD_VALUE = CHECKED_WORKLOAD.attr("data");
		CHECKED_WORKLOAD.addClass("workload-div-checked");
	});
	//返回DIV按钮监听
	$("#back-button").on("click",function(){
		window.history.back();
	});
	//确定按钮监听
	$("#ok-button").on("click",function(){
		showContent();
	});
	
	if(PARAMS.ID){
		showContent();
		$(".page-tab-div-tab")[0].click();
		$(".workload-div")[9].click();
	}else{
		showBack('<div class="error-div">请传递任务ID参数！</div>');
	}
});
