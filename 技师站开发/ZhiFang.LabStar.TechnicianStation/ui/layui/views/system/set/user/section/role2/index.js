/**
	@name：检验小组权限
	@author：liangyl
	@version 2019-11-14
 */
layui.extend({
	uxutil:'/ux/util',
    uxtable:'/ux/table',
    SingleIndex:'views/system/set/user/section/role2/single/index',
    BatchIndex:'views/system/set/user/section/role2/batch/index'
}).use(['uxutil','element','SingleIndex','BatchIndex'],function(){
	var $ = layui.$,
		uxutil=layui.uxutil,
		element = layui.element,
		BatchIndex = layui.BatchIndex,
		SingleIndex = layui.SingleIndex;
	  
	//单选模式
	var SingleIndexInstance = null;
	//多选模式
	var BatchIndexInstance = null;
	//多选模式是否已初始化
	var isBataLoad = false;
	
	element.on('tab(roletab)', function(data){
        if(data.index==1 && !isBataLoad){ //多选模式
        	//初始化多选模式
        	initBataHtml();
        	isBataLoad = true;
        }
	});
	//初始化列表
	function initHtml(){
//		$('#single-index').html('');
		//单选实例化
		SingleIndexInstance = SingleIndex.render({
			domId:'single-index'
		});
//		initBataHtml();
	};
	//初始化列表
	function initBataHtml(){
//		$('#bata-index').html('');
		//单选实例化
		BatchIndexInstance = BatchIndex.render({
			domId:'bata-index'
		});
	};
	function init(){
		//初始化
		initHtml();
	};
	 // 窗体大小改变时，调整高度显示
	$(window).resize(function() {
		var win = $(window),
		    maxHeight = win.height()-20;
		
	});
	//初始化
	init();
});