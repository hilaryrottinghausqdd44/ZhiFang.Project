/**
 @Name：修改样本类型
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
//		   parent.location.reload(); // 父页面刷新
	   var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	   parent.layer.close(index); //再执行关闭
	} 	
	$('#btnClose').on('click', function (event) {
		CloseWin();
	});

	//扫码,回车事件
    $('#txtBarCode').on('keydown', function (event) {
        if (event.keyCode == 13) {
        	var barCode =$('#txtBarCode');
        	if(barCode.val()){
//				    $("input[name='barcode']").
//                  barCode.val('');
        	}else{
        		layer.alert('条码号不能为空,请扫码!', {
		           skin: 'layui-layer-molv'
		          ,closeBtn: 0
		        });
        	}
            return false;
       }
    });
	
});
