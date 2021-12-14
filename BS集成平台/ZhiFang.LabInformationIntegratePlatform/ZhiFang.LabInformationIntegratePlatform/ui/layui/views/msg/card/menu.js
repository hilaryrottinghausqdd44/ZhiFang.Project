layui.extend({
	uxutil:'ux/util',
}).use(['uxutil'],function(){
	var $=layui.$,
		uxutil = layui.uxutil;
		
	var index = parent.layer.getFrameIndex(window.name);
	parent.layer.iframeAuto(index);
		
	//获取系统列表
	var GET_SYSTEM_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL";
	//检验之星系统编码
	var SYSTEM_CODE = 'ZF_LAB_START';
	//系统列表
	var ALL_SYSTEM_LIST = [];
	//获取系统列表
	function loadSystemList(callback){
		var url = GET_SYSTEM_LIST_URL,
			where = [];
		
		url += "?fields=IntergrateSystemSet_Id,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemCode," +
			"IntergrateSystemSet_SystemHost&where=intergratesystemset.SystemCode='" + SYSTEM_CODE + "'";
			
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				ALL_SYSTEM_LIST = data.value.list || [];
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//初始化监听
	function initListeners(){
		var ZF_LAB_START_URL = uxutil.path.LOCAL + '/' + ALL_SYSTEM_LIST[0].SystemHost;
		
		$("li").on("click",function(){
			var id = $(this).attr("id"),
				url = $(this).attr("url");
				
			url = url.replace(/{ZF_LAB_START}/g,ZF_LAB_START_URL);
			url += '?t=' + new Date().getTime();
			
			switch (id){
				case "change_user":window.parent.location.href = url;
					break;
				default:window.open(url,id,'left=0,top=0,scrollbars,resizable=yes,toolbar=no');
					break;
			}
		});
	};
	//初始化
	function init(){
		//获取系统列表
		loadSystemList(function(){
			initListeners();
		});
	};
	//初始化
	init();
});