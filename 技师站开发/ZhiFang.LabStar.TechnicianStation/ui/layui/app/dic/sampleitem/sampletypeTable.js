/**
	@name：项目样本类型维护-按样本类型设置
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

	//获取列表数据
	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL';

	var sampletypeTable = {
		searchInfo: {
			isLike: true,
			fields: ['lbsampletype.CName', 'lbsampletype.SName']
		},
		//参数配置
		config: {
			page: true,
			limit: 50,
			loading: true,
			PKField: "LBSampleType_Id",
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
			defaultOrderBy: [{
				"property": 'LBSampleType_DispOrder',
				"direction": 'ASC'
			}],
			/**列表当前排序*/
			sort: [{
				"property": 'LBSampleType_DispOrder',
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
						field: 'LBSampleType_Id',
						title: 'ID',
						width: 150,
						sort: true,
						hide: true
					},
					{
						field: 'LBSampleType_CName',
						title: '样本类型',
						minWidth: 160,
						sort: true
					},
					{
						field: 'LBSampleType_SName',
						title: '简称',
						width: 90,
						hide: true,
						sort: true
					},
					{
						field: 'LBSampleType_DispOrder',
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
				if (res&&res.data&&res.data.length>0&&this.autoSelect == true) {
					sampletypeTable.doAutoSelect(this, 0);
				}
			}
		}
	};

	var Class = function(setings) {
		var me = this;
		var config = $.extend(true, {}, uxtable.config, sampletypeTable.config, setings);
		var tableIns = $.extend(true, {}, sampletypeTable);
		tableIns.config = config;
		return tableIns;
	};

	Class.pt = Class.prototype;
	/**获取查询框内容*/
	sampletypeTable.getLikeWhere = function(value) {
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
	sampletypeTable.doAutoSelect = function(that, rowIndex) {
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
	sampletypeTable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	sampletypeTable.getFields = function(isString) {
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
	sampletypeTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	sampletypeTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	sampletypeTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	sampletypeTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	sampletypeTable.getTableWhere = function() {
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
			var search = $('#table_sampletype_like_search');
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
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	//列表查询项处理
	sampletypeTable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	sampletypeTable.onSearch = function(where) {
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
	//主入口
	sampletypeTable.render = function(options) {
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
			return tableIns;
		}
	};

	//暴露接口
	exports('sampletypeTable', sampletypeTable);
});
