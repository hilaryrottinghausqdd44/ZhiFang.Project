/**
	@name：项目样本类型维护-按样本类型选择项目
	@author：longfc
	@version 2019-09-27
 */
layui.extend({}).define(['uxutil', 'uxtable', 'table'], function(exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		uxtable = layui.uxtable;

	var MOD_NAME = "lefttable";
	//获取列表数据
	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleItemByHQL';

	var lefttable = {
		//参数配置
		config: {
			page: false,
			limit: 1000,
			loading: true,
			defaultLoad: true,
			loading: true,
			PKField: "LBSampleItem_Id",
			/**后台排序*/
			remoteSort: true,
			/**是否自动选中行*/
			autoSelect: true,
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**列表当前排序*/
			defaultOrderBy: [{
				"property": 'LBSampleItem_LBItem_Id',
				"direction": 'ASC'
			}],
			/**列表当前排序*/
			sort: [{
				"property": 'LBSampleItem_LBItem_Id',
				"direction": 'ASC'
			}],
			url: "", //SELECT_URL,
			where: "",
			//调用服务获取到的原始数据
			defaultLoadData: [],
			cols: [
				[{
						type: 'checkbox',
						fixed: 'left'
					},
					{
						type: 'numbers',
						title: '行号',
						fixed: 'left'
					},
					{
						field: 'LBSampleItem_Id',
						title: '关系ID',
						width: 150,
						sort: true,
						hide: true
					},
					{
						field: 'LBSampleItem_LBItem_Id',
						title: '项目编号',
						width: 100,
						hide: true,
						sort: true
					},
					{
						field: 'LBSampleItem_LBItem_CName',
						title: '项目名称',
						width: 100,
						sort: true
					},
					{
						field: 'LBSampleItem_LBItem_DispOrder',
						title: '次序',
						width: 100,
						sort: true,
						hide: true
					}, {
						field: 'LBSampleItem_LBSampleType_Id',
						title: '样本类型ID',
						width: 150,
						sort: true,
						hide: true
					},
					{
						field: 'LBSampleItem_LBSampleType_CName',
						title: '样本类型',
						minWidth: 130,
						sort: true
					},
					{
						field: 'LBSampleItem_LBSampleType_SName',
						title: '样本类型简称',
						width: 130,
						sort: true,
						hide: true
					}
				]
			],
			text: {
				none: '暂无相关数据'
			},
			parseData: function(res, curr, response) { //res即为原始返回的数据
				if (!res) return;
				var type = typeof res.ResultDataValue,
					data = res.ResultDataValue;

				if (type == 'string') {
	                data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
				}
				var list = [];
				for (var i in data.list) {
					list.push(this.changeData(data.list[i]));
				}
				if (typeof this.onAfterLoad == 'function') {
					typeof this.onAfterLoad === 'function' && this.onAfterLoad(list);
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
				};
			},
			changeData: function(data) {
				return data;
			},
			done: function(res, curr, response) {
				if (res&&res.data&&res.data.length>0&&this.autoSelect == true) {
					lefttable.doAutoSelect(this, 0);
				}
			}
		}
	};
	var Class = function(setings) {
		var me = this;
		var config = $.extend(true, {}, uxtable.config, lefttable.config, setings);
		var tableIns = $.extend(true, {}, lefttable);
		tableIns.config = config;
		return tableIns;
	};
	Class.pt = Class.prototype;
	//表单事件监听
	lefttable.on = function(events, callback) {
		return layui.onevent.call(this, MOD_NAME, events, callback);
	};
	//
	// lefttable.onAfterLoad = function(res, curr, response) {
	// 	if (callback) {
	// 		return callback(records);
	// 	} else {
	// 		return records;
	// 	}
	// };
	/**获取查询框内容*/
	lefttable.getLikeWhere = function(value) {
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
	/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	lefttable.doAutoSelect = function(that, rowIndex) {
		var me = this;
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = that.elem.attr('lay-filter');
		var thatrow = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.instance.key][index] = [];
				tr.remove();
				that.instance.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 300);
	};
	//获取查询Url
	lefttable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	lefttable.getFields = function(isString) {
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
	lefttable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	lefttable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	lefttable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	lefttable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	lefttable.getTableWhere = function() {
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
		var whereStr = "";
		if (arr.length > 0) whereStr = arr.join(") and (");
		if (whereStr) whereStr = "(" + whereStr + ")";
		var sort = me.config.sort || me.config.defaultOrderBy;
		if (sort && sort.length > 0) sort = JSON.stringify(sort);
		var where = {
			"isPlanish": true,
			"where": whereStr,
			'fields': me.getFields(true).join(','),
			"sort": sort
		};
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	//列表查询项处理
	lefttable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	lefttable.onSearch = function(where) {
		var me = this;
		if (where) me.setExternalWhere(where);
		me.config.url = me.getLoadUrl();
		me.config.where = me.getWhere();
		var uxtable1 = uxtable.render(me.config);
		me.instance = uxtable1.instance;
		var res = table.cache[me.instance.id];
		//if(res.data&&me.config.defaultLoadData)me.config.defaultLoadData=res;
		typeof me.config.donerender === 'function' && me.config.donerender(res, null, null);
		return me;
	};
	/**
	 * 获取保存的提交实体集合信息
	 * 包括已经存在的关系及需要新增保存的关系
	 */
	lefttable.getEntityList = function(defaultParams) {
		var me = this;
		var entityList = [];
		var leftData = table.cache['lefttable'];
		var strDataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		for (var i = 0; i < leftData.length; i++) {
			var id = leftData[i]["LBSampleItem_Id"];
			if (!id) id = -1;
			var entity2 = {
				Id: id,
				LBSampleType: {
					Id: leftData[i]["LBSampleItem_LBSampleType_Id"],
					CName: leftData[i]["LBSampleItem_LBSampleType_CName"],
					DataTimeStamp: strDataTimeStamp
				},
				LBItem: {
					Id: leftData[i]["LBSampleItem_LBItem_Id"] || defaultParams.id,
					CName: leftData[i]["LBSampleItem_LBItem_CName"] || defaultParams.cname,
					DataTimeStamp: strDataTimeStamp
				}
			}
			entityList.push(entity2);
		}
		return entityList;
	};
	//清空列表数据
	lefttable.clearData = function() {
		var me = this;
		var filter = me.config.id || "lefttable";
		table.reload(filter, {
			url: "",
			where: "",
			data: []
		});
	};
	//主入口
	lefttable.render = function(options) {
		var me = this;
		var tableIns = new Class(options);
		//加载数据
		if (tableIns.config.defaultLoad == true) {
			me = tableIns;
			me.onSearch();
			return me;
		} else {
			tableIns.config.url="";
			tableIns.config.data=[];
			var uxtable1 = uxtable.render(tableIns.config);
			tableIns.instance = uxtable1.instance;
			var res = table.cache[me.instance.id];
			typeof tableIns.config.donerender === 'function' && tableIns.config.donerender(res, null, null);
			return tableIns;
		}
	};
	//暴露接口
	exports('lefttable', lefttable);
});
