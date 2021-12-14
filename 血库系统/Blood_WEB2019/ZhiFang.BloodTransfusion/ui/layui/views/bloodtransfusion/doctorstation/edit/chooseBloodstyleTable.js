/**
	@name：血制品选择列表
	@author：longfc
	@version 2019-06-27
 */
layui.extend({
	//uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter'
}).define(['layer', 'table', 'uxutil', 'dataadapter'], function (exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;

	var chooseBloodstyleTable = {
		searchInfo: {
			isLike: true,
			fields: ['bloodstyle.CName']
		},
		config: {
			elem: '',
			id: "",
			toolbar: "",
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**列表当前排序*/
			sort: [{
				"property": 'Bloodstyle_DispOrder',
				"direction": 'ASC'
			}],
			selectUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleEntityListByJoinHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_DelBloodstyle",
			/**table查询服务URL*/
			url: "",
			where: "",
			page: true,
			limit: 30,
			//limits: [10, 15, 20, 30, 40, 50, 60],
			defaultOrderBy: [{
				"property": 'Bloodstyle_DispOrder',
				"direction": 'ASC'
			}],
			//, {type: 'checkbox'} 只能选择一个血制品，所以监听单击或者双击事件就可以
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				},{
					field: 'Bloodstyle_Id',
					hide: true,
					width: 140,
					title: '血制品编码'
				}, {
					field: 'Bloodstyle_BloodClass_Id',
					sort: true,
					hide: true,
					title: '血制品分类编码'
				}, {
					field: 'Bloodstyle_BloodClassCName',
					hide: true,
					width: 95,
					title: '所属分类'
				}, {
					field: 'Bloodstyle_CName',
					sort: true,
					width: 165,
					title: '血制品'
				}, {
					field: 'Bloodstyle_BReqCount',
					width: 85,
					title: '申请量',
					edit: 'text'
				}, {
					type: 'radio'
				}, {
					field: 'Bloodstyle_BloodUnit_Id',
					hide: true,
					title: '单位编码'
				}, {
					field: 'Bloodstyle_BloodUnitCName',
					width: 85,
					title: '单位'
				}]
			],
			response: dataadapter.toResponse(),
			parseData: function (res) {
				var result = dataadapter.toList(res);
				return result;
			}
		},
		set: function (options) {
			var me = this;
			if (options) me.config = $.extend({}, me.config, options);
		}
	};
	//构造器
	var Class = function (options) {
		var me = this;
		me.config = $.extend({}, chooseBloodstyleTable.config, me.config, options);
		var inst = $.extend(true, {}, chooseBloodstyleTable, me);//table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//chooseBloodstyleTable = Class.prototype;
	//调车列表排序
	chooseBloodstyleTable.onSearch = function (sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/**获取查询框内容*/
	chooseBloodstyleTable.getLikeWhere = function (value) {
		var me = this;
		if (me.searchInfo == null) return "";
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	};
	//列表查询项处理
	chooseBloodstyleTable.getSearchWhere = function () {
		var me = this;
		var arr=[];

		var likeSearch = $("#LAY-app-table-choose-Bloodstyle-LikeSearch");
		if (likeSearch) {
			var searchV = likeSearch.val();
			if (searchV) {
				searchV=me.getLikeWhere(searchV);
				if(searchV)arr.push("(" + searchV + ")");
			}
		}

		var where = "";
		if (arr && arr.length > 0) where = arr.join(") and (");
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	chooseBloodstyleTable.getLoadUrl = function () {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	chooseBloodstyleTable.getFields = function (isString) {
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
	//设置默认的查询where
	chooseBloodstyleTable.setDefaultWhere = function (where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	chooseBloodstyleTable.setExternalWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	chooseBloodstyleTable.setWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询where
	chooseBloodstyleTable.getWhere = function () {
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
		where = {
			"where": where,
			'fields': me.getFields(true).join(',')
		};
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if (defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t=new Date().getTime();
		return where;
	};
	/**删除一条数据*/
	chooseBloodstyleTable.delOneById = function (index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["Bloodstyle_Id"];
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function () {
			uxutil.server.ajax({
				url: url
			}, function (data) {
				if (data.success) {
					obj.del();
					if (callback) callback();
				} else {
					layer.msg('删除失败:' + data.msg);
				}
			});
		}, 100 * index);
	};
	//对外公开返回对象
	chooseBloodstyleTable.result = function (that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	//核心入口
	chooseBloodstyleTable.render = function (options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('chooseBloodstyleTable', chooseBloodstyleTable);
});
