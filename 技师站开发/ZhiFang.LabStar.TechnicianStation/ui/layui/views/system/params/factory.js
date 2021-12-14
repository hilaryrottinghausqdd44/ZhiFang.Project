/**
	@name：出厂设置
	@author：liangyl
	@version 2020-02-10
 */
layui.extend({

}).define(['form','paraform','uxutil'],function(exports){
	"use strict";
	
	var $ = layui.$,
	    form = layui.form,
		uxutil = layui.uxutil,
		paraform = layui.paraform;
		
	  //获取出厂设置参数服务
    var SELECT_FACTORY_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryFactorySettingPara?isPlanish=true"; 
			
	 
	var factoryform={
		//全局项
		config:{
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,factoryform.config,setings);
	};
	Class.pt = Class.prototype;
   
    //核心入口
	factoryform.render = function(options){
		var me = new Class(options);
		me.loadData = me.loadDatas;
		me.clearData = me.clearData;
		return me;
	};
    //加载表单数据	
	Class.pt.loadDatas = function(paraTypeCode){
		var me = this;
		var URL = SELECT_FACTORY_URL;
		var para = paraform.render();
		var index = layer.load();
		para.load(URL,paraTypeCode,'',function(data){
			layer.close(index);
			var list = data.value.list || [];
			
			if(list.length==0){//数据不存在时
				me.clearData('#ContentDiv');
				return;
			}
			para.createControl(list,'#ContentDiv','Factory');
			
				//高度
	        $("#Factory").css("height", ($(window).height() - 125) + "px");//设置表单容器高度
	        $("#Factory").css("width", $('.cardHeight').width()+ "px");
	
	        // 窗体大小改变时，调整高度显示
	    	$(window).resize(function() {
				 //表单高度
			    $("#Factory").css("height", ($(window).height() - 125) + "px");//设置表单容器高度
			    $("#Factory").css("width", $('.cardHeight').width()+ "px");
	    	});
		});
	};
	
    //清空表单数据	
	Class.pt.clearData = function(divid){
		var me = this;
		var HTML ='<div class="layui-none">暂无数据</div>';
		$(divid).html(HTML);
	};
	
     	//暴露接口
	exports('factoryform',factoryform);
});		