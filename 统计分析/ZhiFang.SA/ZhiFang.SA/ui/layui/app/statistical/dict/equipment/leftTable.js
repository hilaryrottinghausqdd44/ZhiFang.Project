/**
	@name：equipments.leftTable 仪器选择左列表
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
			fields: ['equipment.CName', 'equipment.ShortCode']
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
					field: 'Equipment_Id',
					sort: true,
					width: 120,
					title: '仪器编号'
				}, {
					field: 'Equipment_CName',
					sort: true,
					title: '仪器名称'
				}]
			],
			url: uxutil.path.ROOT +
				"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipmentByHQL?isPlanish=true&fields=Equipment_CName,Equipment_Id",
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
