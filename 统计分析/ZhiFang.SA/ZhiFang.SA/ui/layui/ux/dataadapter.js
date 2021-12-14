/**
	@name：layui.ux.modules.dataadapter 数据适配器
	@author：longfc
	@version 2019-04-28
 */
layui.define(function(exports) {
	"use strict";

	var dataadapter = {
		/**
		 * @description 转换返回Response的状态码
		 * */
		toResponse: function() {
			return {
				statusCode: true, //成功状态码
				statusName: 'code', //code key
				msgName: 'msg ', //msg key
				dataName: 'data' //data key
			};
		},
		/**
		 * @description 转换返回list的结果
		 * 严格模式下,resultData的count和list没有双引号,不能进行JSON.parse
		 * */
		toList: function(result) {
			var resultData = result.ResultDataValue;
			//console.log(resultData);
			if (result.success&&resultData) {
				if (resultData.indexOf('"count\"') < 0) {//可能count没有引号
					resultData = resultData.replace(/\"count\"/g, "count");
					resultData = resultData.replace(/count/g, "\"count\"");
				}
				if (resultData.indexOf('"list\"') < 0) {//可能list没有引号
					resultData = resultData.replace(/\"list\"/g, "list");
					resultData = resultData.replace(/list/g, "\"list\"");
				}
				//console.log(resultData);
				resultData = JSON.parse(resultData);
			} else {
				if(!resultData)resultData={};
				resultData["list"] = [];
			}
			result = {
				"code": result.success,
				"msg": result.ErrorInfo,
				"count":resultData.count,
				"data": resultData.list
			}
			return result;
		}
	};

	//暴露接口
	exports('dataadapter', dataadapter);
});
