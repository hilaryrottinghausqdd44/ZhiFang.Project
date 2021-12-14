layui.extend({
	uxutil:'ux/util',
	print:'ux/print'
}).use(['jquery','uxutil','print','layer'],function(){
	var $ = layui.jquery,
		uxutil = layui.uxutil,
		print = layui.print;
		
	//测试PDF文件地址
	var url = uxutil.path.LAYUI + "/test/print/test.pdf";
	
	//直接打印
	$("#print").on("click",function(){
		for(var i=0;i<3;i++){
			print.instance.pdf.print(url,'PDF打印任务' + (i+1));
		}
	});
	//预览打印
	$("#preview").on("click",function(){
		print.instance.pdf.preview([url,url,url,url]);
	});
});