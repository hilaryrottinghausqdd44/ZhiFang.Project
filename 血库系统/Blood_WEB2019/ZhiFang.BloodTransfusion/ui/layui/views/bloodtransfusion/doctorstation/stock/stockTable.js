/**
	@name：当前库存信息列表
	@author：longfc
	@version 2019-10-28
 */
layui.extend({
	uxtable: 'ux/table',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig'
}).define(['uxutil', 'form', 'table', 'uxtable',"cachedata", 'bloodsconfig'], function(exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		bloodsconfig = layui.bloodsconfig,
		table = layui.table,
		uxtable = layui.uxtable;

	//获取列表数据
	var SELECT_URL = bloodsconfig.getBloodInInfoUrl(); //uxutil.path.ROOT +"/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleEntityListByJoinHql?isPlanish=true";

	var stockTable = {
		//参数配置
		config: {
			/**医嘱申请单号*/
			//reqFormNo: null,
			page: false,
			limit: 5000,
			loading: true,
			PKField: "bloodno",
			/**默认加载数据*/
			defaultLoad: true,
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
			defaultOrderBy: null,
			/**列表当前排序*/
			sort: null,
			url: "", //SELECT_URL,
			where: "",
			cols: [
				[{
						type: 'numbers',
						title: '行号',
						fixed: 'left',
						rowspan: 2
					},
					{
						field: 'BloodName',
						title: '血液名称',
						width: 165,
						rowspan: 2,
						sort: false
					},
					{
						field: 'BloodUnitName',
						title: '单位',
						width: 65,
						rowspan: 2,
						sort: false
					},
					{
						title: 'A型',
						align: 'center',
						colspan: 2
					},
					{
						title: 'B型',
						align: 'center',
						colspan: 2
					},
					{
						title: 'O型',
						align: 'center',
						colspan: 2
					},
					{
						title: 'AB型',
						align: 'center',
						colspan: 2
					},
					{
						title: '合计',
						align: 'center',
						colspan: 2
					}
				],
				[{
						field: 'A_Count',
						title: '血量', //A型
						width: 85,
						sort: false
					},
					{
						field: 'A_Qty',
						title: '袋数', //A型
						width: 85,
						sort: false
					},
					{
						field: 'B_Count',
						title: '血量', //B型
						width: 85,
						sort: false
					},
					{
						field: 'B_Qty',
						title: '袋数', //B型
						width: 85,
						sort: false
					},
					
					{
						field: 'O_Count',
						title: '血量', //O型
						width: 85,
						sort: false
					},
					{
						field: 'O_Qty',
						title: '袋数', //O型
						width: 85,
						sort: false
					},
					{
						field: 'AB_Count',
						title: '血量', //AB型
						width: 85,
						sort: false
					},
					{
						field: 'AB_Qty',
						title: '袋数', //AB型
						width: 85,
						sort: false
					},
					{
						field: 'TotalCount',
						title: '血量', //合计
						width: 105,
						sort: false
					},
					{
						field: 'TotalQty',
						title: '袋数', //合计
						width: 105,
						sort: false
					}
				]
			],
			text: {
				none: '暂无相关数据'
			},
			parseData: function(res) {
				if (!res) return;
			
				var type = typeof res.ResultDataValue,
					data = res.ResultDataValue;
								
				if (type == 'string') {
					data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				}
			
				var count = data.length;
				var list = [];
				for (var i in data) {
					list.push(this.changeData(data[i]));
				}
				var result={
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": count || 0, //解析数据长度
					"data": list || []
				};
				return result;
			},
			changeData:function(data){
				var me = this;
				return data;
			},
			done: function(res, curr, response) {
				if (res && res.data && res.data.length > 0 && this.autoSelect == true) {
					//stockTable.doAutoSelect(this, 0);
				}
			}
		}
	};

	var Class = function(setings) {
		var me = this;
		var config = $.extend(true, {}, uxtable.config, stockTable.config, setings);
		var tableIns = $.extend(true, {}, stockTable);
		tableIns.config = config;
		return tableIns;
	};

	Class.pt = Class.prototype;
	
	/**获取查询框内容*/
	stockTable.getLikeWhere = function(value) {
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
	stockTable.doAutoSelect = function(that, rowIndex) {
		var me = this;
		var data = table.cache[that.id] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = that.elem.attr('lay-filter');
		var tr = $(that.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: tr,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.instance.key][index] = [];
				tr.remove();
				that.instance.scrollPatch();
			},
			update: function(fields) { //修改行数据
				fields = fields || {};
				layui.each(fields, function(key, value) {
					if (key in data) {
						var templet, td = tr.children('td[data-field="' + key + '"]');
						data[key] = value;
						that.eachCols(function(i, item2) {
							if (item2.field == key && item2.templet) {
								templet = item2.templet;
							}
						});
						td.children(ELEM_CELL).html(function() {
							return templet ? function() {
								return typeof templet === 'function' ?
									templet(data) :
									laytpl($(templet).html() || value).render(data)
							}() : value;
						}());
						td.data('content', value);
					}
				});
			}
		};
		setTimeout(function() {
			layui.event.call(tr, 'table', 'row' + '(' + filter + ')', obj);
		}, 300);
	};
	//修改行数据
	stockTable.updateRow = function(fields, rowIndex) {
		var that = this;
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;
		var tr = $(that.instance.config.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		fields = fields || {};
		layui.each(fields, function(key, value) {
			if (key in data) {
				var templet, td = tr.children('td[data-field="' + key + '"]');
				data[key] = value;
				that.eachCols(function(i, item2) {
					if (item2.field == key && item2.templet) {
						templet = item2.templet;
					}
				});
				td.children(ELEM_CELL).html(function() {
					return templet ? function() {
						return typeof templet === 'function' ?
							templet(data) :
							laytpl($(templet).html() || value).render(data)
					}() : value;
				}());
				td.data('content', value);
			}
		});
	};
	//获取查询Url
	stockTable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	stockTable.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[1] || [],
			length = columns.length,
			fields = ["LBParItemSplit_CName"];
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
	//设置申请单号
	stockTable.setReqFormNo = function(reqFormNo) {
		var me = this;
		me.config.reqFormNo = reqFormNo;
	};
	//设置默认的查询where
	stockTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	stockTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	stockTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	stockTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	stockTable.getTableWhere = function() {
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
		//模糊查询条件
		if (me.searchInfo) {
			var searchV = "";
			var search = $('#table_stock_like_search');
			if (search) searchV = search.val();
			if (searchV) {
				searchV = me.getLikeWhere(searchV);
				if (searchV) arr.push(searchV);
			}
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
		if (me.config.reqFormNo) {
			//where.reqFormNo = me.config.reqFormNo;
		}
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	//列表查询项处理
	stockTable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	stockTable.onSearch = function(where) {
		var me = this;
		if (where) me.setExternalWhere(where);
		me.config.url = me.getLoadUrl();
		me.config.where = me.getWhere();
		var uxtable1 = uxtable.render(me.config);
		me.instance = uxtable1.instance;
		//form.render();
		return me;
	};
	//清空列表数据
	stockTable.clearData = function() {
		var me = this;
		var filter = me.config.id || "table_stock";
		table.reload(filter, {
			url: "",
			where: "",
			data: []
		});
	};
	//保存前验证
	stockTable.onSaveVerify = function() {};
	//获取保存封装提交的实体集合信息
	stockTable.getEntityList = function() {};
	//主入口
	stockTable.render = function(options) {
		var me = this;
		var tableIns = new Class(options);
		//加载数据
		if (tableIns.config.defaultLoad == true) {
			me = tableIns;
			me.onSearch();
			return me;
		} else {
			tableIns.config.url = "";
			tableIns.config.data = [];
			var uxtable1 = uxtable.render(tableIns.config);
			tableIns.instance = uxtable1.instance;
			//form.render();
			return tableIns;
		}
	};

	//暴露接口
	exports('stockTable', stockTable);
});
