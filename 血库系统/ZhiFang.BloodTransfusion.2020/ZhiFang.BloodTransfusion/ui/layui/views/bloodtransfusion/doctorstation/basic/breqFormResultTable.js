
/**
	@name：医嘱申请检验结果列表
	@author：longfc
	@version 2019-07-17
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter',
	"bloodsconfig": '/config/bloodsconfig',
	"soulTable": "ux/other/soultable/ext/soulTable"
}).define(['layer', 'table', 'uxutil', 'dataadapter',"bloodsconfig","soulTable"], function (exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodsconfig = layui.bloodsconfig;
	var soulTable=layui.soulTable;
	
	var breqFormResultTable = {
		searchInfo: {
			isLike: true,
			fields: ['bloodbreqformresult.BTestItemCName']
		},
		config: {
			/**版本号*/
			version:bloodsconfig.version||"1.0.0.1",
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
				"property": 'BloodBReqFormResult_BTestItemNo',
				"direction": 'ASC'
			}, {
				"property": 'BloodBReqFormResult_BTestTime',
				"direction": 'DESC'
			}],
			selectUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResultEntityListByJoinHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqFormResult",
			/**table查询服务URL*/
			url: "",
			where: "",
			page: false,
			limit: 1000,
			//limits: [10, 15, 20, 30],
			defaultOrderBy: [{
				"property": 'BloodBReqFormResult_BTestItemNo',
				"direction": 'ASC'
			}, {
				"property": 'BloodBReqFormResult_BTestTime',
				"direction": 'DESC'
			}],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				}, {
					field: 'BloodBReqFormResult_Id',
					hide: true,
					width: 140,
					filter: true,
					title: '检验结果ID'
				}, {
					field: 'BloodBReqFormResult_BReqFormID',
					hide: true,
					width: 140,
					filter: true,
					title: '申请单号'
				}, {
					field: 'BloodBReqFormResult_PatNo',
					hide: true,
					title: '住院号'
				}, {
					field: 'BloodBReqFormResult_PatID',
					hide: true,
					filter: true,
					title: '患者ID'
				}, {
					field: 'BloodBReqFormResult_Barcode',
					hide: true,
					width: 145,
					sort: true,
					title: '条形号'
				}, {
					field: 'BloodBReqFormResult_BTestItemNo',
					hide: true,
					sort: true,
					filter: true,
					title: '检验项目代码'
				},{
					field: 'BloodBReqFormResult_BTestItemCName',
					sort: true,
					width: 165,
					filter: true,
					title: '项目名称'
				},  {
					field: 'BloodBReqFormResult_BTestItemEName',
					sort: true,
					hide:true,
					width: 115,
					filter: true,
					title: '检验项目'
				},{
					field: 'BloodBReqFormResult_ItemResult',
					width: 105,
					filter: true,
					title: 'LIS结果'
				}, {
					field: 'BloodBReqFormResult_ItemLisResult',
					width: 105,
					hide:true,
					filter: true,
					title: 'LIS原始结果'
				}, {
					field: 'BloodBReqFormResult_ItemUnit',
					width: 105,
					//hide:true,
					filter: true,
					title: '单位'
				}, {
					field: 'BloodBReqFormResult_BTestTime',
					width: 165,
					sort: true,
					filter: true,
					title: '审核时间'
				}]
			],
			drag: {
				toolbar: true
			}, //soulTable开启拖拽工具栏
			soulSort: false, //soulTable
			filter: {
				bottom: false, //soulTable隐藏底部筛选区域，默认为 true
				//soulTable用于控制表头下拉显示，可以控制顺序、显示, 依次是：表格列、筛选数据、筛选条件、编辑筛选条件、导出excel
				items: ['column', 'excel'] //'column', 'data', 'condition', 'editCondition', 'excel'
			},
			response: dataadapter.toResponse(),
			parseData: function (res) {
				var result = dataadapter.toList(res);
				return result;
			},
			done: function(res, curr, response) {
				soulTable.render(this); //soulTable初始化
				/* if(this.autoSelect == true) {
					breqFormResultTable.doAutoSelect(this.table, 0);
				} */
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
		me.config = $.extend({}, breqFormResultTable.config, me.config, options);
		var inst = $.extend(true, {}, breqFormResultTable, me);//table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//调车列表排序
	breqFormResultTable.onSearch = function (sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/**获取查询框内容*/
	breqFormResultTable.getLikeWhere = function (value) {
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
	breqFormResultTable.getSearchWhere = function () {
		var me = this;
		var arr=[];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	breqFormResultTable.getLoadUrl = function () {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	breqFormResultTable.getFields = function (isString) {
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
	//设置外部传入的申请单号
	breqFormResultTable.setReqFormNo = function (reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置默认的查询where
	breqFormResultTable.setDefaultWhere = function (where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqFormResultTable.setExternalWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqFormResultTable.setWhere = function (where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询where
	breqFormResultTable.getWhere = function () {
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
		//传入的申请单号处理
		if (me.config.ReqFormNo) {
			arr.push("bloodbreqformresult.BReqFormID='" + me.config.ReqFormNo + "'");
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
	breqFormResultTable.delOneById = function (index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BloodBReqFormResult_Id"];
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
	breqFormResultTable.result = function (that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	//核心入口
	breqFormResultTable.render = function (options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqFormResultTable', breqFormResultTable);
});
