layui.extend({
	uxutil:'ux/util',
	RevocationForm:'modules/pre/sample/barcode/basic/form'
}).use(['uxutil','element','RevocationForm'],function(){
	var $ = layui.$,
		RevocationForm = layui.RevocationForm,
		uxutil = layui.uxutil;
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//样本单信息实例
	var RevocationFormInstance = null;
	//初始化页面
	function initHtml(){
		//实例化样本单信息
		RevocationFormInstance = RevocationForm.render({
			domId:'RevokeForm',
			height:null
		});
	};
	
	//保存按钮处理
	$("#save_button").on("click",function(){
		RevocationFormInstance.onSave();
	});
	//取消
	$("#clear_button").on("click",function(){
		var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	    parent.layer.close(index); //再执行关闭
	});
	
	//初始化
	function init(){
		//初始化页面
		initHtml();
	};

	//初始化
	init();
});