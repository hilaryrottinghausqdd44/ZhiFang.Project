/**
	@name：医嘱申请检验结果列表
	@author：longfc
	@version 2019-07-17
 */
layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	dateutil: "views/modules/common/dateutil",
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig'
}).define(['layer', 'table', "util", 'uxutil', 'dataadapter', 'dateutil', 'cachedata', 'bloodsconfig'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var util = layui.util;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var dateutil = layui.dateutil;
	var cachedata = layui.cachedata;
	var bloodsconfig = layui.bloodsconfig;

	var breqFormResultEditTable = {
		searchInfo: null,
		config: {
			elem: '',
			id: "",
			toolbar: "",
			/**申请单号*/
			ReqFormNo: "",
			/**默认传入参数*/
			defaultParams: {
				HisDoctorId: "", //医嘱申请医生Id,
				HisPatId: "", //医嘱申请患者Id 门诊号
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
				"property": 'BloodBReqFormResult_IsPreTrransfusionEvaluationItem',
				"direction": 'ASC'
			}, {
				"property": 'BloodBReqFormResult_DispOrder',
				"direction": 'ASC'
			}, {
				"property": 'BloodBReqFormResult_BTestItemNo',
				"direction": 'ASC'
			}, {
				"property": 'BloodBReqFormResult_BTestTime',
				"direction": 'DESC'
			}],
			selectUrl: uxutil.path.ROOT +
				"/BloodTransfusionManageService.svc/BT_UDTO_SelectBloodBReqFormResultListByVLisResultHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqFormResult",
			/**table查询服务URL*/
			url: "",
			where: "",
			page: false,
			limit: 1000,
			//limits: [10, 15, 20, 30],
			defaultOrderBy: [{
				"property": 'BloodBReqFormResult_IsPreTrransfusionEvaluationItem',
				"direction": 'ASC'
			}, {
				"property": 'BloodBReqFormResult_DispOrder',
				"direction": 'ASC'
			}, {
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
					title: '检验结果ID'
				}, {
					field: 'BloodBReqFormResult_BReqFormID',
					hide: true,
					width: 140,
					title: '申请单号'
				}, {
					field: 'BloodBReqFormResult_Barcode',
					hide: true,
					width: 155,
					sort: true,
					title: '条形号'
				}, {
					field: 'BloodBReqFormResult_BTestItemNo',
					hide: true,
					sort: true,
					title: '检验项目代码'
				}, {
					field: 'BloodBReqFormResult_BTestItemCName',
					sort: true,
					//hide: true,
					width: 155,
					title: '检验项目'
				}, {
					field: 'BloodBReqFormResult_BTestItemEName',
					sort: true,
					hide: true,
					width: 225,
					title: 'LIS项目'
				}, {
					field: 'BloodBReqFormResult_ItemResult',
					width: 145,
					title: 'LIS结果'
					//,templet: '#selectItemResult'
					//,edit: 'text'
				}, {
					field: 'BloodBReqFormResult_ItemUnit',
					width: 75,
					//hide: true,
					sort: true,
					title: '单位'
				}, {
					field: 'BloodBReqFormResult_BTestTime',
					width: 155,
					//hide: true,
					sort: true,
					title: '审核时间'
				}, {
					field: 'BloodBReqFormResult_ItemLisResult',
					width: 105,
					hide: true,
					title: 'LIS原始结果'
				}, {
					field: 'BloodBReqFormResult_IsPreTrransfusionEvaluationItem',
					width: 85,
					hide: true,
					title: '是否是输血前评估项'
				}, {
					field: 'BloodBReqFormResult_DispOrder',
					width: 85,
					hide: true,
					title: '显示次序'
				}]
			],
			response: dataadapter.toResponse(),
			parseData: function(res) {
				var result = dataadapter.toList(res);
				result = breqFormResultEditTable.changeResult(result);
				//数据转换完成后对外公开的处理接口(parseDataAfter)
				if(typeof this.parseDataAfter == 'function') {
					this.parseDataAfter(result);
				}
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
		me.config = $.extend({}, breqFormResultEditTable.config, me.config, options);
		var inst = $.extend(true, {}, breqFormResultEditTable, me); //table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};

	//LIS结果为空时默认值
	breqFormResultEditTable.getLisDefaulltItemsResult = function(data) {
		var me = this;
		var itemResult = "";
		itemResult = cachedata.getCache("LisDefaulltItemsResult");
		if(!itemResult) {
			itemResult = bloodsconfig.Common.LIS_DEFAULT_ITEMSRESULT;
		} else {
			bloodsconfig.Common.LIS_DEFAULT_ITEMSRESULT = itemResult;
		}
		return itemResult;
	};
	//Class.pt = Class.prototype;
	//重新处理获取的数据
	breqFormResultEditTable.changeResult = function(data) {
		var me = this;
		if(!data || !data.data || data.data.length <= 0) return data;

		for(var i = 0; i < data.data.length; i++) {
			var itemResult = data.data[i]["BloodBReqFormResult_ItemResult"];
			if(!itemResult) itemResult = me.getLisDefaulltItemsResult();
			data.data[i]["BloodBReqFormResult_ItemResult"] = itemResult;
		}
		return data;
	};
	//调车列表排序
	breqFormResultEditTable.onSearch = function(sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/**获取查询框内容*/
	breqFormResultEditTable.getLikeWhere = function(value) {
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
	breqFormResultEditTable.getSearchWhere = function() {
		var me = this;
		var arr = [];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	breqFormResultEditTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	breqFormResultEditTable.getFields = function(isString) {
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
	breqFormResultEditTable.setReqFormNo = function(reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置默认的查询where
	breqFormResultEditTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqFormResultEditTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqFormResultEditTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取医嘱申请单的LIS检验结果条件
	breqFormResultEditTable.getVLisresultHql = function() {
		var me = this;
		var arr = [];
		var vlisresultHql = "",
			cname = me.config.defaultParams.CName || "";
		var patNo = me.config.defaultParams.PatNo || ""; //住院号
		var patId = me.config.defaultParams.HisPatId || ""; //门诊号
		if(!patNo) patNo = "";
		if(!patId) patId = "";
		//获取LIS检验结果时,是否同按门诊号及住院号获取
		if(bloodsconfig.HisInterface.ISGETLISRESULTOFPATIDORPATNO == true) {
			if(patId.length > 0 && patNo.length > 0) {
				arr.push("(vbloodlisresult.PatNo='" + patNo + "' or vbloodlisresult.PatNo='" + patId + "')");
			} else if(patNo.length > 0) {
				arr.push("vbloodlisresult.PatNo='" + patNo + "'");
			}
		} else {
			if(patNo.length > 0) {
				arr.push("vbloodlisresult.PatNo='" + patNo + "'");
			}
		}

		if(cname.length > 0) {
			arr.push("vbloodlisresult.PatName='" + cname + "'");
		}
		//只获取患者的30天内LIS检验结果
		if(arr.length > 0) {
			var sdate = "";
			var edate = util.toDateString("", "yyyy-MM-dd");
			//测试开始日期范围
			//if(edate) sdate = '2019-05-10';
			var day3=me.gtLisResultDays();
			sdate = util.toDateString(dateutil.getNextDate(edate, -day3), "yyyy-MM-dd");
			if(sdate.length > 0)
				arr.push("vbloodlisresult.CheckDateTime>='" + sdate + " 00:00:00'");
			if(edate.length > 0)
				arr.push("vbloodlisresult.CheckDateTime<='" + edate + " 23:59:59'");
		}
		if(arr.length > 0) vlisresultHql = "(" + arr.join(" and ") + ")";
		return vlisresultHql;
	};
	//获取几天内的LIS检验结果
	breqFormResultEditTable.gtLisResultDays=function(){
		var me = this;
		var days=bloodsconfig.Common.GET_LISRESULT_DAYS;
		var days2 = cachedata.getCache("GetLisResultDays");
		if(days2){
			days2=parseInt(days2);
		}
		if(days2&&days2>0)days=days2;
		if(days){
			days=parseInt(days);
		}
		if(!days)days=7;
		bloodsconfig.Common.GET_LISRESULT_DAYS=days;
		return days;
	};
	//获取查询where
	breqFormResultEditTable.getWhere = function() {
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
			arr.push("bloodbreqformresult.BReqFormID='" + me.config.ReqFormNo + "'");
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
		//if(!vlisresultHql) layer.msg('获取LIS结果条件为空!');
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	/**删除一条数据*/
	breqFormResultEditTable.delOneById = function(index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BloodBReqFormResult_Id"];
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
	breqFormResultEditTable.result = function(that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	//获取申请血制品的提交信息
	breqFormResultEditTable.getBreqItemSubmit = function(breqFormResultEditTable1) {
		var me = breqFormResultEditTable1 || this;
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
				var id = "" + item["BloodBReqFormResult_Id"];
				var entity = JSON.stringify(item).replace(/BloodBReqFormResult_/g, "");
				if(entity) entity = JSON.parse(entity);

				entity.DispOrder = entity.LAY_TABLE_INDEX + 1;
				delete entity.LAY_TABLE_INDEX;
				if(entity.BTestTime) {
					entity.BTestTime = uxutil.date.toServerDate(entity.BTestTime);
				} else {
					entity.BTestTime = null;
				}
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
	breqFormResultEditTable.render = function(options) {
		var me = this;
		var inst = new Class(options);

		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqFormResultEditTable', breqFormResultEditTable);
});