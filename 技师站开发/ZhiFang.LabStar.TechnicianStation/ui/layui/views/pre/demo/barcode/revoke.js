/**
 @Name：撤销确认
 @Author：
 @version 2016-12-27
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['form'],function(){
	var $ = layui.$,
		form = layui.form;
	form.render();
	
	/**获取表单数据*/
	function getValues (formId) {
	    var obj = {};
		var t = $('#'+ formId+'[name]').serializeArray();
		console.log(t);
		$.each(t, function() {
			obj[this.name] = this.value;
		});
		return  obj;
	}			           
    getValues("LAY-revoke-form");
//	
//	 form.val("LAY-revoke-form", {
//	    BarCode: "贤心"
//	   ,BarCodeOrName: "女"
//	   ,ZDY2: "3"
// });
//					   
	//关闭页面
	function CloseWin(){
//		   parent.location.reload(); // 父页面刷新
	   var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	   parent.layer.close(index); //再执行关闭
	} 	
	$('#btnClose').on('click', function (event) {
		CloseWin();
	});
});
