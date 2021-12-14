layui.extend({
	uxutil:'ux/util',
	liip_common:'modules/liip/common'
}).use(['uxutil','liip_common','element','layer'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		liip_common = layui.liip_common,
		element = layui.element;
		
	//检验之星系统编码
	var ZF_LAB_START_CODE = 'ZF_LAB_START';
	
	element.render();
	element.on('tab(tab)',function(obj){
		//var iframe = obj.elem[0].getElementsByTagName('iframe')[0];
		var iframe = $("#" + $(this).attr("lay-id"))[0];
		if(iframe){
			var iframeHeight = iframe.contentWindow.document.documentElement.scrollHeight;
			$(iframe).height(iframeHeight);
		}
	});
	
	//初始化
	function init(){
		liip_common.system.getMap(function(data){
			var labStarUrl = uxutil.path.LOCAL + '/' + data[ZF_LAB_START_CODE].SystemHost,
				reg_doctor_url = $("#reg_doctor").attr("url").replace(/{ZF_LAB_START}/g,labStarUrl) + '?t=' + new Date().getTime(),
				reg_tech_url = $("#reg_tech").attr("url").replace(/{ZF_LAB_START}/g,labStarUrl) + '?t=' + new Date().getTime();
			$("#reg_doctor").attr("src",reg_doctor_url);
			$("#reg_tech").attr("src",reg_tech_url);
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