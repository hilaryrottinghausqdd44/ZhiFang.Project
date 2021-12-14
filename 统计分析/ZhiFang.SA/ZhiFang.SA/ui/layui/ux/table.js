/**
	@name：layui.ux.table 列表基类  
	@author：Jcall
	@version 2019-03-25
 */
layui.extend({
	uxutil:'ux/util'
}).define(['jquery','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		table = layui.table;
		
	var config = {
		
	};
	
	var uxtable = {
		//核心入口
		render:function(options){
			var config = {
				parseData:function(res){//res即为原始返回的数据
					if(!res) return;
					var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
					return {
						"code": res.success ? 0 : 1, //解析接口状态
						"msg": res.ErrorInfo, //解析提示文本
						"count": data.count || 0, //解析数据长度
						"data": data.list || []
					};
				}
			};
			//深度拷贝
			config = $.extend(true,config,options);
			
			return table.render(config);
		},
		changeData:function(){}
	};
	
	//暴露接口
	exports('uxtable',uxtable);
});