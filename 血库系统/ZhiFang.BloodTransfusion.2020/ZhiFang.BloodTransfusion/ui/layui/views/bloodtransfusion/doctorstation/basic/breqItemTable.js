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

	var breqItemTable = {
		searchInfo: {
			isLike: true,
			fields: ['bloodbreqitem.BloodNo']
		},
		config: {
			elem: '',
			id: "",
			toolbar: "",
			/**申请单号*/
			ReqFormNo: "",
			/**默认传入参数*/
			defaultParams: {
				HisDoctorId: "", //医嘱申请医生Id,
				HisPatId: "", //医嘱申请患者Id
				PatNo: "", //医嘱申请患者住院号
				HisDeptId: ""//医嘱申请科室Id
			},
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**列表当前排序*/
			sort: [{
				"property": 'BloodBreqItem_BloodOrder',
				"direction": 'ASC'
			}],
			/**基本查询服务URL*/
			selectUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemEntityListByJoinHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBreqItem",
			/**table查询服务URL*/
			url: "",
			where: "",
			page: false,
			limit: 1000,
			defaultOrderBy: [{
				"property": 'BloodBreqItem_BloodOrder',
				"direction": 'ASC'
			}],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				}, {
					field: 'BloodBreqItem_Id',
					hide: true,
					width: 140,
					title: '申请明细ID'
				}, {
					field: 'BloodBreqItem_BReqFormID',
					hide: true,
					width: 140,
					title: '申请单号'
				}, {
					field: 'BloodBreqItem_BloodOrder',
					width: 75,
					title: '明细号'
				}, {
					field: 'BloodBreqItem_BCNo',
					sort: true,
					hide: true,
					title: '血制品类型编码'
				}, {
					field: 'BloodBreqItem_BloodclassCName',
					hide: true,
					title: '血制品类型'
				}, {
					field: 'BloodBreqItem_BloodNo',
					sort: true,
					hide: true,
					width: 95,
					title: '血制品编码'
				}, {
					field: 'BloodBreqItem_BloodCName',
					sort: true,
					width: 145,
					title: '血制品'
				}, {
					field: 'BloodBreqItem_BReqCount',
					width: 85,
					title: '申请量'
				}, {
					field: 'BloodBreqItem_BloodUnitNo',
					hide: true,
					title: '血制品单位号'
				}, {
					field: 'BloodBreqItem_BloodUnitCName',
					width: 85,
					title: '单位'
				}, {
					field: 'BloodBreqItem_ZX1',
					hide: true,
					title: '备注1'
				}, {
					field: 'BloodBreqItem_ZX2',
					hide: true,
					title: '备注2'
				}, {
					field: 'BloodBreqItem_ZX3',
					hide: true,
					title: '备注3'
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
		me.config = $.extend({}, breqItemTable.config, me.config, options);		
		var inst = $.extend(true, {}, breqItemTable, me);// table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//调车列表排序
	breqItemTable.onSearch = function (sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/**获取查询框内容*/
	breqItemTable.getLikeWhere = function (value) {
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
	breqItemTable.getSearchWhere = function () {
		var me = this;
		var arr=[];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	breqItemTable.getLoadUrl = function () {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	breqItemTable.getFields = function (isString) {
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
	breqItemTable.setDefaultWhere = function (where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的申请单号
	breqItemTable.setReqFormNo = function (reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqItemTable.setExternalWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqItemTable.setWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询where
	breqItemTable.getWhere = function () {
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
			arr.push("bloodbreqitem.BReqFormID='" + me.config.ReqFormNo + "'");
		}
		var where = "";
		if (arr.length > 0) where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		where = {
			"where": where,
			'fields': me.getFields(true).join(',')
		};
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t=new Date().getTime();
		
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if (defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		return where;
	};
	/**删除一条数据*/
	breqItemTable.delOneById = function (index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BloodBreqItem_Id"];
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
	breqItemTable.result = function (that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	//核心入口
	breqItemTable.render = function (options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqItemTable', breqItemTable);
});
