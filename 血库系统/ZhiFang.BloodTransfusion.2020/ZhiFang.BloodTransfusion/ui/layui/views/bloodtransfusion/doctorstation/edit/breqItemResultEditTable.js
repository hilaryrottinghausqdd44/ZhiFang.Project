/**
	@name：医嘱申请检验结果列表
	@author：longfc
	@version 2019-07-01
 */
layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	dateutil: "views/modules/common/dateutil",
	bloodsconfig: '/config/bloodsconfig'
}).define(['layer', 'table', "util", 'uxutil', 'dataadapter', 'dateutil', 'bloodsconfig'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var util = layui.util;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodsconfig = layui.bloodsconfig;
	var dateutil = layui.dateutil;

	var breqItemResultEditTable = {
		searchInfo: {
			isLike: true,
			fields: ['bloodbreqitemresult.BTestItemCName']
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
				HisDeptId: "" //医嘱申请科室Id
			},
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**列表当前排序*/
			sort: [{
				"property": 'BloodBreqItemResult_BTestItemNo',
				"direction": 'ASC'
			}, {
				"property": 'BloodBreqItemResult_BTestTime',
				"direction": 'DESC'
			}],
			selectUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_GetBloodBReqItemResultListByVLisResultHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBreqItemResult",
			/**table查询服务URL*/
			url: "",
			where: "",
			page: false,
			limit: 1000,
			//limits: [10, 15, 20, 30],
			defaultOrderBy: [{
				"property": 'BloodBreqItemResult_BTestItemNo',
				"direction": 'ASC'
			}, {
				"property": 'BloodBreqItemResult_BTestTime',
				"direction": 'DESC'
			}],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				}, {
					field: 'BloodBreqItemResult_Id',
					hide: true,
					width: 140,
					title: '检验结果ID'
				}, {
					field: 'BloodBreqItemResult_BReqFormID',
					hide: true,
					width: 140,
					title: '申请单号'
				}, {
					field: 'BloodBreqItemResult_PatNo',
					hide: true,
					title: '住院号'
				}, {
					field: 'BloodBreqItemResult_PatID',
					hide: true,
					title: '患者ID'
				}, {
					field: 'BloodBreqItemResult_Barcode',
					hide: true,
					width: 155,
					sort: true,
					title: '条形号'
				}, {
					field: 'BloodBreqItemResult_BTestTime',
					width: 165,
					sort: true,
					title: '检验时间'
				}, {
					field: 'BloodBreqItemResult_BTestItemNo',
					hide: true,
					sort: true,
					title: '检验项目代码'
				}, {
					field: 'BloodBreqItemResult_BTestItemCName',
					sort: true,
					width: 185,
					title: '检验项目'
				}, {
					field: 'BloodBreqItemResult_ItemResult',
					width: 105,
					title: '结果'
				}, {
					field: 'BloodBreqItemResult_ItemUnit',
					width: 105,
					title: '单位'
				}]
			],
			response: dataadapter.toResponse(),
			parseData: function(res) {
				var result = dataadapter.toList(res);
				return result;
			}
		},
		set: function(options) {
			var me = this;
			if(options) me.config = $.extend({}, me.config, options);
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, breqItemResultEditTable.config, me.config, options);
		var inst = $.extend(true, {}, breqItemResultEditTable, me); //table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//调车列表排序
	breqItemResultEditTable.onSearch = function(sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/**获取查询框内容*/
	breqItemResultEditTable.getLikeWhere = function(value) {
		var me = this;
		if(me.searchInfo == null) return "";
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for(var i = 0; i < len; i++) {
			if(isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	};
	//列表查询项处理
	breqItemResultEditTable.getSearchWhere = function() {
		var me = this;
		var arr = [];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	breqItemResultEditTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	breqItemResultEditTable.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length,
			fields = [];
		for(var i = 0; i < length; i++) {
			if(columns[i].field) {
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
	breqItemResultEditTable.setReqFormNo = function(reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置默认的查询where
	breqItemResultEditTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqItemResultEditTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqItemResultEditTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取医嘱申请单的LIS检验结果条件
	breqItemResultEditTable.getVLisresultHql = function() {
		var me = this;
		var arr = [];
		var vlisresultHql = "",
			patNo = me.config.defaultParams.PatNo || "",
			cname = me.config.defaultParams.CName || "";
		if(patNo.length > 0) {
			arr.push("vbloodlisresult.PatNo='" + patNo + "'");
		}
		if(cname.length > 0) {
			arr.push("vbloodlisresult.PatName='" + cname + "'");
		}
		//只获取患者的30天内LIS检验结果
		if(arr.length > 0) {
			var sdate = "";
			var edate = util.toDateString("", "yyyy-MM-dd");
			if(edate) sdate = util.toDateString(dateutil.getNextDate(edate, -bloodsconfig.Common.GET_LISRESULT_DAYS), "yyyy-MM-dd");
			if(sdate.length > 0)
				arr.push("vbloodlisresult.Itemtesttime>='" + sdate + " 00:00:00'");
			if(edate.length > 0)
				arr.push("vbloodlisresult.Itemtesttime<='" + edate + " 23:59:59'");
		}
		if(arr.length > 0) vlisresultHql = "(" + arr.join(" and") + ")";
		return vlisresultHql;
	};
	//获取查询where
	breqItemResultEditTable.getWhere = function() {
		var me = this,
			arr = [];
		var where = {
			"reqFormId": "",
			"reqresulthql": "",
			"vlisresultHql": "",
			'fields': "",
			"sort": ""
		};
		//默认条件
		if(me.config.defaultWhere && me.config.defaultWhere != '') {
			arr.push(me.config.defaultWhere);
		}
		//内部条件
		if(me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.config.internalWhere);
		}
		//外部条件
		if(me.config.externalWhere && me.config.externalWhere != '') {
			arr.push(me.config.externalWhere);
		}
		//传入的申请单号处理
		if(me.config.ReqFormNo) {
			arr.push("bloodbreqitemresult.BReqFormID='" + me.config.ReqFormNo + "'");
			where.reqFormId = me.config.ReqFormNo;
		}

		var reqresulthql = "",
			vlisresultHql = "";
		if(arr.length > 0) reqresulthql = arr.join(") and (");
		if(reqresulthql) reqresulthql = "(" + reqresulthql + ")";
		where.reqresulthql = reqresulthql;
		where.fields = me.getFields(true).join(',');
		//排序
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if(defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		//获取LIS结果条件
		var vlisresultHql = me.getVLisresultHql();
		where.vlisresultHql = vlisresultHql;
		if(!vlisresultHql) layer.msg('获取LIS结果条件为空!');
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	/**删除一条数据*/
	breqItemResultEditTable.delOneById = function(index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BloodBreqItemResult_Id"];
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			uxutil.server.ajax({
				url: url
			}, function(data) {
				if(data.success) {
					obj.del();
					if(callback) callback();
				} else {
					layer.msg('删除失败:' + data.msg);
				}
			});
		}, 100 * index);
	};
	//对外公开返回对象
	breqItemResultEditTable.result = function(that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	//获取申请血制品的提交信息
	breqItemResultEditTable.getBreqItemSubmit = function(breqItemResultEditTable1) {
		var me = breqItemResultEditTable1 || this;
		var submitData = {
			"addResultList": [],
			"editResultList": []
		};
		var data = table.cache[me.tableIns.config.id] || [];
		layui.each(data, function(i, item) {
			if(item.constructor === Array) {
				//invalidNum++; //无效数据，或已删除的
				return;
			}
			if(item) {
				var id = "" + item["BloodBreqItemResult_Id"];
				var entity = JSON.stringify(item).replace(/BloodBreqItemResult_/g, "");
				if(entity) entity = JSON.parse(entity);

				entity.DispOrder = entity.LAY_TABLE_INDEX + 1;
				delete entity.LAY_TABLE_INDEX;
				if(entity.BTestTime) entity.BTestTime = uxutil.date.toServerDate(entity.BTestTime);
				if(!id || id == "0" || id == "-1") {
					entity.Id = -1;
					submitData.addResultList.push(entity);
				} else {
					submitData.editResultList.push(entity);
				}
			}
		});
		return submitData;
	};
	//核心入口
	breqItemResultEditTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqItemResultEditTable', breqItemResultEditTable);
});