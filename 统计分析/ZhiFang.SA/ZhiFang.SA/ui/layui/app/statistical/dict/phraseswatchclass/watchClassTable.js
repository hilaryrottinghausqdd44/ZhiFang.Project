/**
	@name：phraseswatchclass.watchClassTable 质量指标类型列表
	@author：longfc
	@version 2019-05-09
 */
layui.extend({
	//uxutil: 'ux/util'
	dataadapter: 'ux/dataadapter'
}).define(['layer', 'table', 'uxutil', 'dataadapter'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;

	var watchClassTable = {
		searchInfo: {
			isLike: true,
			fields: ['phraseswatchclass.CName', 'phraseswatchclass.SName', 'phraseswatchclass.ShortCode']
		},
		config: {
			elem: '',
			toolbar: "",
			//skin: 'line',
			//even: true,
			height: '680',
			page: true,
			limit: 10,
			//limits: [5, 10, 20, 30, 40, 50, 60, 70, 80, 90],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				},{
					type: 'checkbox'
				}, {
					field: 'PhrasesWatchClass_QIndicatorTypeCName',
					sort: true,
					width: 140,
					title: '质量指标类型'
				}, {
					field: 'PhrasesWatchClass_Id',
					sort: false,
					hide: true,
					width: 140,
					title: '类型编号'
				}, {
					field: 'PhrasesWatchClass_CName',
					sort: true,
					title: '类型名称'
				}, {
					field: 'PhrasesWatchClass_ShortCode',
					sort: true,
					width: 95,
					title: '快捷码'
				}, {
					field: 'PhrasesWatchClass_DispOrder',
					sort: true,
					width: 95,
					title: '显示次序'
				}, {
					title: '操作列',
					fixed: 'right',
					width: 150,
					align: 'center',
					toolbar: '#LAY-app-table-WatchClass-ActionColumn'
				}]
			],
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			url: uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassByHQL?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_DelPhrasesWatchClass",
			where: "",
			defaultOrderBy: [{
				"property": 'PhrasesWatchClass_ParentID',
				"direction": 'ASC'
			}, {
				"property": 'PhrasesWatchClass_DispOrder',
				"direction": 'ASC'
			}],
			response: dataadapter.toResponse(),
			parseData: function(res) {
				var result = dataadapter.toList(res);
				return result;
			}
		},
		set: function(options) {
			var me = this;
			if (options) me.config = $.extend({}, me.config, options);
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, watchClassTable.config, options);
		return me.render();
	};
	Class.pt=Class.prototype;	
	//获取查询Url
	Class.pt.getLoadUrl = function() {
		var me = this;
		var defaultOrderBy = me.config.defaultOrderBy || [];
		var url = uxutil.path.ROOT +"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassByHQL?isPlanish=true";
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getFields(true).join(',');

		var where = me.getWhere();
		if (where) url += '&where=' + where;
		if (defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);
		return url;
	};
	//获取查询Fields
	Class.pt.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	//获取查询where
	Class.pt.getWhere = function() {
		var me = this,
			arr = [];
		//默认条件
		if (me.config.defaultWhere && me.config.defaultWhere != '') {
			arr.push(me.config.defaultWhere);
		}
		//内部条件
		if (me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.config.internalWhere);
		}
		//外部条件
		if (me.config.externalWhere && me.config.externalWhere != '') {
			arr.push(me.config.externalWhere);
		}
		var where = "";
		if (arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		return where;
	};
	/**删除一条数据*/
	Class.pt.delOneById = function(index, obj,callback) {
		var me = this;
		var url = me.config.delUrl;
		console.log(obj.data);
		var id=obj.data["PhrasesWatchClass_Id"];
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			uxutil.server.ajax({
				url: url
			}, function(data) {
				if (data.success) {
					obj.del();
					if (callback) callback();
				} else {
					layer.msg('删除失败:'+data.msg);
				}
			});
		}, 100 * index);
	};
	//初始渲染
	Class.pt.render = function(options) {
		var me = this;
		if (options) me.config = $.extend({}, me.config, options);
		me.config.url = me.getLoadUrl();
		var inst = table.render(me.config);
		//返回对应暂时用这种方式继承Class
		inst = $.extend({}, Class.pt, inst);
		//后续是否考虑缓存inst
		return inst;
	};
	//对外公开返回对象
	Class.pt.result = function(that){
		that=that||new Class();
		return {
			//config:that.config,
			delOneById:that.delOneById
		}
	};
	//核心入口
	watchClassTable.render = function(options) {
		var inst = new Class(options);
		return inst;
	};
	//暴露接口
	exports('watchClassTable', watchClassTable);
});
