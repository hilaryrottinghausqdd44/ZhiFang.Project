/**
	@name：equipments.leftTable 检验小组选择左列表
	@author：longfc
	@version 2019-04-29
 */
layui.define(['table', 'uxutil', 'dataadapter'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;

	var leftTable = {
		searchInfo: {
			isLike: true,
			fields: ['pgroup.CName', 'pgroup.ShortCode']
		},
		config: {
			elem: '',
			toolbar: "",
			skin: 'line',
			even: true,
			height: '680',
			page: true,
			limit: 14,
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
			url: uxutil.path.ROOT +
				"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPGroupByHQL?isPlanish=true&fields=PGroup_Id,PGroup_CName",
			response: dataadapter.toResponse(),
			parseData: function(res) {
				var result = dataadapter.toList(res);
				return result;
			}
		},
		set: function(options) {
			var me = this;
			me.config = $.extend({}, me.config, options);
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, leftTable.config, options);
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
	leftTable.render = function(options) {
		var table = new Class(options);
		return table;
	};
	//暴露接口
	exports('leftTable', leftTable);
});
