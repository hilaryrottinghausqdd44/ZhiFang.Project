{
	init:function(config,callback){
		var me = this;
		JcallShell.Msg.log(config.id + "-JS.init成功");
		$("#" + config.id + "-tab-1").on("click",function(){
			$("#" + config.id + "-title").html(config.title + "-点击TAB1");
		});
		$("#" + config.id + "-tab-2").on("click",function(){
			$("#" + config.id + "-title").html(config.title + "-点击TAB2");
		});
		$("#" + config.id + "-tab-3").on("click",function(){
			$("#" + config.id + "-title").html(config.title + "-点击TAB3");
		});
	}
}