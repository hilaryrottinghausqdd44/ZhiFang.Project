/**
	@name：医嘱申请明细基础列表
	@author：longfc
	@version 2019-06-20
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter'
}).define(['layer', 'table', 'uxutil', 'dataadapter'], function (exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;

	var bloodbreqtypeTable = {
		searchInfo: {
			isLike: true,
			fields: ['bloodbreqtype.CName','bloodbreqtype.Shortcode']
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
				"property": 'BloodBReqType_BloodOrder',
				"direction": 'ASC'
			}],
			/**基本查询服务URL*/
			selectUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeByHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqType",
			/**table查询服务URL*/
			url: "",
			where:"",
			page: false,
			limit: 1000,
			defaultOrderBy: [{
				"property": 'BloodBReqType_BloodOrder',
				"direction": 'ASC'
			}],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				}, {
					field: 'BloodBReqType_Id',
					hide: true,
					width: 140,
					title: 'ID'
				}, {
					field: 'BloodBReqType_CName',
					hide: true,
					width: 140,
					title: '名称'
				}, {
					field: 'BloodBReqType_Shortcode',
					width: 75,
					title: '简称'
				}, {
					field: 'BloodBReqType_DispOrder',
					sort: true,
					hide: true,
					title: '显示次序'
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
		me.config = $.extend({}, bloodbreqtypeTable.config, me.config, options);
		me.config.url = me.getLoadUrl();
		me.config.where = me.getWhere();
		me = $.extend(true, {}, bloodbreqtypeTable,Class.pt, me);// table,
		return me;
	};
	Class.pt = Class.prototype;
	//调车列表排序
	Class.pt.onSearch = function (sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	
	/**获取查询框内容*/
	Class.pt.getLikeWhere = function (value) {
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
	Class.pt.getSearchWhere = function () {
		var me = this;
		var arr=[];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	Class.pt.getLoadUrl = function () {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	Class.pt.getFields = function (isString) {
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
	Class.pt.setDefaultWhere = function (where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的申请单号
	Class.pt.setReqFormNo = function (reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	Class.pt.setExternalWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	Class.pt.setWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询where
	Class.pt.getWhere = function () {
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
		//传入的默认参数处理
		if (me.config.defaultParams) {
			//var reqFormNo=me.config.defaultParams.ReqFormNo;

		}
		//传入的申请单号处理
		if (me.config.ReqFormNo) {
			arr.push("BloodBReqType.BReqFormID='" + me.config.ReqFormNo + "'");
		}
		var whereAll = "";
		if (arr.length > 0) whereAll = arr.join(") and (");
		if (whereAll) whereAll = "(" + where + ")";
		
		
		var where = {
			"where": whereAll,
			'fields': me.getFields(true).join(',')
		};
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if (defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		return where;
	};
	/**删除一条数据*/
	Class.pt.delOneById = function (index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BloodBReqType_Id"];
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
	Class.pt.result = function (that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	//核心入口
	bloodbreqtypeTable.render = function (options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('bloodbreqtypeTable', bloodbreqtypeTable);
});
