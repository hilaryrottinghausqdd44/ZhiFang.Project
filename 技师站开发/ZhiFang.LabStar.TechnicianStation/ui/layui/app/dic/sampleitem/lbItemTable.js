/**
	@name：项目样本类型维护-按检验项目设置
	@author：longfc
	@version 2019-09-21
 */
layui.extend({
	uxtable: 'ux/table'
}).define(['uxutil', 'uxtable', 'table'], function(exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		uxtable = layui.uxtable;
	//按传入的检验小组Id从小组项目里获取检验项目信息(传入的检验小组Id值为空时从检验项目获取信息)
	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchByLBSectionItemHQL';

	var lbItemTable = {
		//参数配置
		config: {
			/**
			 * 当前选择的检验小组Id值
			 */
			lbsectionId: "",
			page: true,
			limit: 50,
			loading: true,
			PKField: "LBItem_Id",
			/**默认加载数据*/
			defaultLoad: false,
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
				"property": 'LBItem_DispOrder',
				"direction": 'ASC'
			}],
			/**列表当前排序*/
			sort: [{
				"property": 'LBItem_DispOrder',
				"direction": 'ASC'
			}],
			url: "", //SELECT_URL,
			where: "",
			cols: [
				[{
						type: 'numbers',
						title: '行号',
						fixed: 'left'
					},
					{
						field: 'LBItem_Id',
						title: 'ID',
						width: 150,
						sort: true,
						hide: true
					},
					{
						field: 'LBItem_CName',
						title: '项目名称',
						minWidth: 160,
						sort: true
					}, {
						field: 'LBItem_EName',
						title: '英文名称',
						hide: true,
						width: 100,
						sort: true
					},
					{
						field: 'LBItem_DispOrder',
						title: '显示次序',
						width: 100,
						sort: true
					}
				]
			],
			text: {
				none: '暂无相关数据'
			},
			done: function(res, curr, response) {
				if (res && res.data && res.data.length > 0 && this.autoSelect == true) {
					lbItemTable.doAutoSelect(this, 0);
				}
			}
		}
	};

	var Class = function(setings) {
		var me = this;
		var config = $.extend(true, {}, uxtable.config, lbItemTable.config, setings);
		var tableIns = $.extend(true, {}, lbItemTable);
		tableIns.config = config;
		return tableIns;
	};

	Class.pt = Class.prototype;
	/**获取查询框内容*/
	lbItemTable.getLikeWhere = function(value) {
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
	 * @param that:当前操作实例对象
	 * @param rowIndex: 指定选中的行
	 * */
	lbItemTable.doAutoSelect = function(that, rowIndex) {
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
			//thatrow.click();
		}, 300);
	};
	//获取查询Url
	lbItemTable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	lbItemTable.getFields = function(isString) {
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
	lbItemTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	lbItemTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	lbItemTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	lbItemTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	lbItemTable.getTableWhere = function() {
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
			var search = $('#table_lbItem_like_search');
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
		//当前选择的检验小组Id
		if (me.config.lbsectionId) {
			where.lbsectionId = me.config.lbsectionId;
		}
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	//列表查询项处理
	lbItemTable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	lbItemTable.onSearch = function(where) {
		var me = this;
		if (where) me.setExternalWhere(where);
		me.config.url = me.getLoadUrl();
		me.config.where = me.getWhere();
		/* if (me.instance) {
			var filter = me.config.id || "table_testitem";
			me.instance.reload(filter, {
				url: me.config.url,
				where: me.config.where
			});
		} else {
			
		} */
		var uxtable1 = uxtable.render(me.config);
		me.instance = uxtable1.instance;
		return me;
	};
	//清空列表数据
	lbItemTable.clearData = function() {
		var me = this;
		var filter = me.config.id || "table_testitem";
		table.reload(filter, {
			url: "",
			where: "",
			data: []
		});
	};
	//主入口
	lbItemTable.render = function(options) {
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
			return tableIns;
		}
	};

	//暴露接口
	exports('lbItemTable', lbItemTable);
});
