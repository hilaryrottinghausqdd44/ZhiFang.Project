/**
	@name：当前库存信息列表
	@author：longfc
	@version 2019-10-28
 */
layui.extend({
	uxtable: 'ux/table',
	bloodsconfig: '/config/bloodsconfig'
}).define(['uxutil', 'form', 'table', 'uxtable','bloodsconfig'], function(exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		//form = layui.form,
		table = layui.table,
		bloodsconfig=layui.bloodsconfig,
		uxtable = layui.uxtable;

	//获取列表数据
	var SELECT_URL =bloodsconfig.CSServer.CS_GEBLOODBILLINFO_URL;// uxutil.path.ROOT +"/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleEntityListByJoinHql1?isPlanish=true";

	var previewpdfTable = {
		//参数配置
		config: {
			/**医嘱申请单号*/
			reqFormNo: "",
			pdfType:"",//1:配血记录;2:发血记录;
			page: false,
			loading: true,
			PKField: "Id",
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
						field: 'Date',
						title: '操作日期',
						width: 175,
						sort: false
					},
					{
						field: 'Id',
						title: '记录单号',
						minWidth: 155,
						sort: false
					},
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
					previewpdfTable.doAutoSelect(this.table, 0);
				}
			}
		}
	};

	var Class = function(setings) {
		var me = this;
		var config = $.extend(true, {}, uxtable.config, previewpdfTable.config, setings);
		var tableIns = $.extend(true, {}, previewpdfTable);
		tableIns.config = config;
		return tableIns;
	};

	Class.pt = Class.prototype;
	/**获取查询框内容*/
	previewpdfTable.getLikeWhere = function(value) {
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
	previewpdfTable.doAutoSelect = function(that, rowIndex) {
		var me = this;
		var data = table.cache[that.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		//var filter = that.elem.attr('lay-filter');
		var filter = that.config.elem.attr('lay-filter');
		var tr = $(that.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: tr,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[that.key][index] = [];
				tr.remove();
				that.scrollPatch();
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
	previewpdfTable.updateRow = function(fields, rowIndex) {
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
	previewpdfTable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	previewpdfTable.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length;
		var fields = [];
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
	previewpdfTable.setReqFormNo = function(reqFormNo) {
		var me = this;
		me.config.reqFormNo = reqFormNo;
	};
	//设置默认的查询where
	previewpdfTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	previewpdfTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	previewpdfTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	previewpdfTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	previewpdfTable.getTableWhere = function() {
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
			var search = $('#table_previewpdf_like_search');
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
			where.sReqFormNo = me.config.reqFormNo;
		}
		if (me.config.pdfType) {
			where.sType = me.config.pdfType;
		}
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	//列表查询项处理
	previewpdfTable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	previewpdfTable.onSearch = function(where) {
		var me = this;
		if (where) me.setExternalWhere(where);
		me.config.url = me.getLoadUrl();
		me.config.where = me.getWhere();
		me.instance = table.render(me.config);

		return me;
	};
	//清空列表数据
	previewpdfTable.clearData = function() {
		var me = this;
		var filter = me.config.id || "table_previewpdf";
		table.reload(filter, {
			url: "",
			where: "",
			data: []
		});
	};
	//主入口
	previewpdfTable.render = function(options) {
		var me = this;
		var tableIns = new Class(options);
		//加载数据
		if (tableIns.config.defaultLoad == true) {
			tableIns=tableIns.onSearch();
			return tableIns;
		} else {
			tableIns.config.url = "";
			tableIns.config.data = [];
			tableIns.instance = table.render(tableIns.config);			
			return tableIns;
		}
	};

	//暴露接口
	exports('previewpdfTable', previewpdfTable);
});
