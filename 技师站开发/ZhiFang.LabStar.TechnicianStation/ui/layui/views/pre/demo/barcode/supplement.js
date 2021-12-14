/**
 @Name：信息补录
 @Author：
 @version 2016-12-27
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['form'],function(){
	var $ = layui.$,
		form = layui.form;
	form.render();
	//关闭页面
	function CloseWin(){
    //parent.location.reload(); // 父页面刷新
	   var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	   parent.layer.close(index); //再执行关闭
	} 	
	$('#btnClose').on('click', function (event) {
		CloseWin();
	});
});
