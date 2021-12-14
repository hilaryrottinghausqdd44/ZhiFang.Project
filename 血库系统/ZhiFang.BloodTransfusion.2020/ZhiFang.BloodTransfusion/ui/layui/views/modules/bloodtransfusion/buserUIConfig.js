/**
	@name：layui列表列设置及还原处理
	@author：longfc
	@version 2019-12-30
 */
layui.extend({
	//uxutil: 'ux/util',
	//uxdata: 'ux/data'
}).define(['jquery'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	
	var buserUIConfig = {
		
		config:{
			
		},
		/**
		 * 保存列表列设置信息
		 * @param {Object} table
		 * 
		 */
		saveCols:function(table){
			
		},
		/**
		 * 还原列表列设置信息
		 * @param {Object} table
		 * 
		 */
		decreaseCols:function(table){
			
		}
		
	};
	
	//暴露接口
	exports('buserUIConfig', buserUIConfig);
});