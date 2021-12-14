/**
	@name：layui.ux.modules.dbtable 数据适配器
	@author：longfc
	@version 2019-04-29
 */
layui.define(['jquery', 'table'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;

	var dbtable = {
		config: {
			leftTableId: "LAY-app-table-leftTable",
			rightTableId: "LAY-app-table-rightTable",
		},
		set: function(options) {
			var me = this;
			me.config = $.extend({}, me.config, options);
		}
	};
	//操作当前实例
	var thisDbtable = function() {
		var that = this,
			options = that.config;
		return {
			config: options
		}
	};
	//构造器
	var Class = function(options) {
		var that = this;
		that.config = $.extend({}, that.config, dbtable.config, options);
		that.render();
	};
	//初始渲染
	Class.prototype.render = function(options){
	  var that = this;
	};
	/**
	 * @method clear
	 * @param {Type} tableId 待清空的列表ID
	 * @desc 清空所有元素
	 */
	Class.prototype.clear = function(tableId) {
		var that = this;
		var cache = table.cache[tableId];
	};
	/**
	 * @method remove
	 * @param {Type} tableId 待清空的列表ID
	 * @param {Type} row 列表的指定行
	 * @desc 删除列表的指定行
	 */
	Class.prototype.remove = function(tableId, row) {
		var that = this;
		var cache = table.cache[tableId];
	};
	/**
	 * @method addFirst
	 * @param {Type} tableId 待添加行列表ID
	 * @param {Type} row 添加的行
	 * @desc 向指定列表第一行插入指定行
	 */
	Class.prototype.addFirst = function(tableId, row) {
		var that = this;
		var cache = table.cache[tableId];
	};
	/**
	 * @method addLast
	 * @param {Type} tableId 待添加行列表ID
	 * @param {Type} row 添加的行
	 * @desc 向指定列表末尾添加指定行
	 */
	Class.prototype.addLast = function(tableId, row) {
		var that = this;
		var cache = table.cache[tableId];
	};
	//核心入口
	dbtable.render = function(options) {
		var inst = new Class(options);
		return thisDbtable.call(inst);
	};
	//暴露接口
	exports('dbtable', dbtable);
});
