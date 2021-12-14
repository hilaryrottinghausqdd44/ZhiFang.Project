/**
	@name：医嘱申请血制品列表
	@author：longfc
	@version 2019-06-27
 */
layui.extend({
	//uxutil: 'ux/util'
	bloodsconfig: '/config/bloodsconfig'
}).define(['layer', 'table', 'uxutil', 'dataadapter', 'bloodsconfig'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var bloodsconfig = layui.bloodsconfig;
	var chooseTableName = "chooseBloodstyle";

	var breqItemEditTable = {
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
				"property": 'BloodBreqItem_BloodOrder',
				"direction": 'ASC'
			}],
			selectUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemEntityListByJoinHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBreqItem",
			/**table查询服务URL*/
			url: "",
			where: "",
			page: false,
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
					width: 95,
					hide: true,
					title: '明细序号'
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
					width: 95,
					title: '申请量',
					edit: 'text'
				}, {
					field: 'BloodBreqItem_BloodUnitNo',
					hide: true,
					title: '单位编码'
				}, {
					field: 'BloodBreqItem_BloodUnitCName',
					width: 125,
					title: '血制品单位'
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
				}, {
					title: '删除',
					fixed: 'right',
					width: 65,
					align: 'center',
					toolbar: '#LAY-app-table-BloodBreqItem-ActionColumn'
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
		me.config = $.extend({}, breqItemEditTable.config, me.config, options);
		var inst = $.extend(true, {}, breqItemEditTable, me); // table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//调车列表排序
	breqItemEditTable.onSearch = function(sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/**获取查询框内容*/
	breqItemEditTable.getLikeWhere = function(value) {
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
	breqItemEditTable.getSearchWhere = function() {
		var me = this;
		var arr = [];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	breqItemEditTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	breqItemEditTable.getFields = function(isString) {
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
	breqItemEditTable.setReqFormNo = function(reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置默认的查询where
	breqItemEditTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqItemEditTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqItemEditTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询where
	breqItemEditTable.getWhere = function() {
		var me = this,
			arr = [];
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
			arr.push("bloodbreqitem.BReqFormID='" + me.config.ReqFormNo + "'");
		}
		var where = "";
		if(arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		where = {
			"where": where,
			'fields': me.getFields(true).join(',')
		};
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if(defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	/**删除一条数据*/
	breqItemEditTable.delete = function(obj) {
		var me = this;
		var data = obj.data;
		var id = obj.data["BloodBreqItem_Id"];
		if(!id || id == "-1") {
			obj.del();

		} else {
			me.delOneById(1, id);
		}
	};
	/**删除一条数据*/
	breqItemEditTable.delOneById = function(index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BloodBreqItem_Id"];
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
	breqItemEditTable.result = function(that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	//清空血制品选择信息
	breqItemEditTable.removeSessionData = function() {
		layui.sessionData(chooseTableName, {
			key: "dataList",
			remove: true
		});
	}
	//打开血制品选择
	breqItemEditTable.openChooseWin = function(idStr, callback) {
		var me = this;
		var params = [];
		params.push("idStr=" + idStr);
		me.removeSessionData();
		layer.open({
			type: 2,
			title: "血制品选择",
			area: ['540px', '92%'],
			content: 'chooseBloodstyle.html?' + params.join("&"),
			id: "LAY-app-form-open-choose-Bloodstyle",
			btn: ['选择后继续', '确认并关闭', '关闭'],
			yes: function(index, layero) { //确定按钮回调方法				
				me.onChooseBloodstyle(callback);
			},
			btn2: function(index, layero) { //选择并关闭
				me.onChooseBloodstyle(function() {
					layer.close(index);
				});
				if(callback) callback();
			},
			end: function() { //层销毁后触发的回调
				me.onChooseBloodstyle(callback);
			},
			cancel: function() { //右上角关闭按钮触发的回调
				me.onChooseBloodstyle(callback);
			}
		});
	};
	//血制品选择后处理
	breqItemEditTable.onChooseBloodstyle = function(callback) {
		var me = this;
		var chooseData = layui.sessionData(chooseTableName);
		var dataList = chooseData.dataList || [];
		var dataTable = table.cache["LAY-app-table-BloodBreqItem"] || [];
		//先删除已经选择的血制品
		var addList = [];
		if(dataList && dataList.length > 0) {
			var isAdd = true;
			$.each(dataList, function(index1, data1) {
				isAdd = true;
				$.each(dataTable, function(index2, data2) {
					if(data1["Bloodstyle_Id"] == data2["BloodBreqItem_BloodNo"]) {
						isAdd = false;
						return false;
					}
				});
				if(isAdd == true && data1) addList.push(data1);
			});
		}
		//新增血制品行
		if(addList && addList.length > 0) {
			var reqFormID = "";
			var bloodOrder = 0;
			$.each(addList, function(index1, data1) {
				bloodOrder = dataTable.length + 1;
				var addObj = {
					"BloodBreqItem_Id": -1,
					"BloodBreqItem_BReqFormID": reqFormID, //申请单号
					"BloodBreqItem_BloodOrder": bloodOrder, //明细序号
					"BloodBreqItem_BCNo": data1["Bloodstyle_BloodClass_Id"], //血制品分类编码
					"BloodBreqItem_BloodclassCName": data1["Bloodstyle_BloodClassCName"],
					"BloodBreqItem_BloodNo": data1["Bloodstyle_Id"],
					"BloodBreqItem_BloodCName": data1["Bloodstyle_CName"],
					"BloodBreqItem_BReqCount": data1["Bloodstyle_BReqCount"],
					"BloodBreqItem_BloodUnitNo": data1["Bloodstyle_BloodUnit_Id"],
					"BloodBreqItem_BloodUnitCName": data1["Bloodstyle_BloodUnitCName"]
				};
				dataTable.push(addObj);
			});
		}
		if(callback) callback(dataTable);
	};
	//保存前验证
	breqItemEditTable.onSaveVerification = function(breqItemEditTable1) {
		var me = breqItemEditTable1 || this;
		var result = {
			success: true,
			msg: ""
		};
		var data = table.cache[me.tableIns.config.id] || [];
		layui.each(data, function(i, item) {
			if(item.constructor === Array) {
				//invalidNum++; //无效数据，或已删除的
				return;
			}
			if(item) {
				var reqCount = "" + item["BloodBreqItem_BReqCount"];
				var bloodCName = "" + item["BloodBreqItem_BloodCName"];
				if(!reqCount) {
					result.success = false;
					result.msg = "血制品名称为【" + bloodCName + "】的申请数为空!";
					return false;
				}
				reqCount = parseFloat(reqCount);
				if(reqCount < 0) {
					result.success = false;
					result.msg = "血制品名称为【" + bloodCName + "】的申请数非法!";
					return false;
				}
			}
		});
		return result;
	};
	//获取申请血制品的提交信息
	breqItemEditTable.getBreqItemSubmit = function(breqItemEditTable1) {
		var me = breqItemEditTable1 || this;
		var submitData = {
			"addBreqItemList": [],
			"editBreqItemList": []
		};
		var data = table.cache[me.tableIns.config.id] || [];
		layui.each(data, function(i, item) {
			if(item.constructor === Array) {
				//invalidNum++; //无效数据，或已删除的
				return;
			}
			if(item) {
				var id = "" + item["BloodBreqItem_Id"];
				var entity = JSON.stringify(item).replace(/BloodBreqItem_/g, "");
				if(entity) entity = JSON.parse(entity);
				entity.BloodOrder = entity.LAY_TABLE_INDEX + 1;
				entity.DispOrder = entity.LAY_TABLE_INDEX + 1;
				delete entity.LAY_TABLE_INDEX;
				entity.Bloodstyle={
					Id:entity.BloodNo
				};
				if(!id || id == "0" || id == "-1") {
					entity.Id = -1;
					submitData.addBreqItemList.push(entity);
				} else {
					submitData.editBreqItemList.push(entity);
				}
			}
		});
		return submitData;
	};
	//核心入口
	breqItemEditTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqItemEditTable', breqItemEditTable);
});