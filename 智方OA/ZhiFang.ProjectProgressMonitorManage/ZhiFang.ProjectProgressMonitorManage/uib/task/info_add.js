$(function(){
	//初始化编辑框
	$(".summernote").summernote({lang:"zh-CN"});
	//保存
	$("#save").on("click",function(){
		alert("保存");
		var url = JcallShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTask';
		
		var params = {
			CName:$("#CName").val(),
			Contents:$('.summernote').code()
		};
		
		params = JShell.JSON.encode({entity:params});
		
		JcallShell.Server.ajax({
			url:url,
			type:'post',
			data:params
		},function(data){
			
		})
		
		
		
	});
	$(".fa-chevron-up").on("click",function(){
		$(this).toggleClass("fa-chevron-up").toggleClass("fa-chevron-down");
		var data = $(this).attr("data");
		var isUp = $(this).hasClass("fa-chevron-up");
		if(isUp){
			$("#" + data).show();
		}else{
			$("#" + data).hide();
		}
	});
	
});