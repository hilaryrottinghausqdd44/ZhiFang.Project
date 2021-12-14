layui.use(['element'],function(){
	var $ = layui.$,
		element = layui.element;
		
	element.render();
	element.on('tab(tab)',function(obj){
		var iframe = $("#" + $(this).attr("lay-id"))[0];
		if(iframe){
			var iframeHeight = $(window).height() - 44;
			$(iframe).height(iframeHeight);
		}
	});
	
	//初始化
	function init(){
		$("iframe").each(function(index,el){
			$(this).attr("src",$(this).attr("url") + "?t=" + new Date().getTime());
		});
		var showIframe = $(".layui-show").find("iframe");
		showIframe.load(function(){
			setTimeout(function(){
				$('.layui-this').click();
				$('.iframe').css("visibility","visible");
			},500);
		});
	};
	init();
});