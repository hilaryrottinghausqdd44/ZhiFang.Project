/**
	@name：equipment.rightTable 小组选择右列表
	@author：longfc
	@version 2019-04-29
 */
layui.define(['table', 'uxutil', 'dataadapter'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;

	var rightTable = {
		config: {
			elem: '',
			toolbar: "",
			skin: 'line',
			even: true,
			height: '680',
			page: false,
			//limit: 14,
			cols: [
				[{
					type: 'checkbox'
				}, {
					field: 'PGroup_Id',
					sort: true,
					width: 120,
					title: '小组编号'
				}, {
					field: 'PGroup_CName',
					sort: true,
					title: '小组名称'
				}]
			],
			data:[]
		},
		set: function(options) {
			var me = this;
			me.config = $.extend({}, me.config, options);
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, rightTable.config, options);
		me.render(me.config);
	};
	//获取查询Url
	Class.prototype.getLoadUrl = function() {
		var me = this;
		var url = this.url;
	};
	//初始渲染
	Class.prototype.render = function(options) {
		var me = this;
		return table.render(me.config);
	};
	//核心入口
	rightTable.render = function(options) {
		var inst = new Class(options);		
		return inst;
	};
	//暴露接口
	exports('rightTable', rightTable);
});
