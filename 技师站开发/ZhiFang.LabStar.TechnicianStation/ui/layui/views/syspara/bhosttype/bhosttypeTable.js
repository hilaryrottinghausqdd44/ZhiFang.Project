/**
	@name：站点类型列表
	@author：longfc
	@version 2019-10-11
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
	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeByHQL';
	//物理删除列表数据
	var DELETE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelBHostType';

	var bhosttypeTable = {
		searchInfo: {
			isLike: true,
			fields: ['bhosttype.CName', 'bhosttype.SName']
		},
		//参数配置
		config: {
			page: true,
			limit: 50,
			loading: true,
			PKField: "BHostType_Id",
			/**默认加载数据*/
			defaultLoad: true,
			/**后台排序*/
			remoteSort: true,
			/**是否自动选中行*/
			autoSelect: true,
			/**选择行的行索引*/
			selectIndex: 0,
			/**选择行的行的主键值*/
			selectPK: "",
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**列表当前排序*/
			defaultOrderBy: [{
				"property": 'BHostType_DispOrder',
				"direction": 'ASC'
			}],
			/**列表当前排序*/
			sort: [{
				"property": 'BHostType_DispOrder',
				"direction": 'ASC'
			}],
			url: "", //SELECT_URL,
			where: "",
			cols: [
				[{
						type: 'checkbox',
						fixed: 'left'
					}, {
						type: 'numbers',
						title: '行号',
						fixed: 'left'
					},
					{
						field: 'BHostType_Id',
						title: 'ID',
						width: 150,
						sort: true,
						hide: true
					},
					{
						field: 'BHostType_CName',
						title: '名称',
						width: 160,
						sort: true
					},
					{
						field: 'BHostType_SName',
						title: '简称',
						width: 120,
						sort: true
					},
					{
						field: 'BHostType_Shortcode',
						title: '快捷码',
						width: 120,
						sort: true
					},
					{
						field: 'BHostType_PinYinZiTou',
						title: '拼音字头',
						width: 120,
						sort: true
					},
					{
						field: 'BHostType_DispOrder',
						title: '次序',
						width: 80,
						sort: true
					}
				]
			],
			text: {
				none: '暂无相关数据'
			},
			done: function(res, curr, response) {
				if (res && res.data && res.data.length > 0 && this.autoSelect == true) {
					if (this.selectPK) {
						for (var i = 0; i < res.data.length; i++) {
							if (res.data[i][this.PKField] == this.selectPK) {
								this.selectIndex = res.data[i]["LAY_TABLE_INDEX"];
								break;
							}
						}
					}
					bhosttypeTable.doAutoSelect(this, this.selectIndex);
				}
			}
		}
	};

	var Class = function(setings) {
		var me = this;
		var config = $.extend(true, {}, uxtable.config, bhosttypeTable.config, setings);
		var tableIns = $.extend(true, {}, bhosttypeTable);
		tableIns.config = config;
		return tableIns;
	};

	Class.pt = Class.prototype;
	/**g更新选择行的行的主键值*/
	bhosttypeTable.setSelectPK= function(value) {
		var me = this;
		me.config.selectPK = value;
	};
	/**g更新选择行的行索引*/
	bhosttypeTable.setSelectIndex = function(value) {
		var me = this;
		me.config.selectIndex = value;
	};
	/**获取查询框内容*/
	bhosttypeTable.getLikeWhere = function(value) {
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
	bhosttypeTable.doAutoSelect = function(that, rowIndex) {
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
	//物理删除处理
	bhosttypeTable.onDelete = function(data, callback) {
		var me = this;
		if (!data || data.length <= 0) {
			var checkStatus = table.checkStatus(me.config.id);
			data = checkStatus.data; //得到选中的数据
			if (data.length === 0) {
				return layer.msg('请选择数据后再操作');
			}
		}
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = data.length;
		//显示遮罩层
		//显示遮罩层
		layer.load();
		for (var i in data) {
			var id = data[i][me.config.PKField];
			me.delOneById(i, id, callback);
		}
	};
	/**删除一条数据*/
	bhosttypeTable.delOneById = function(index, id, callback) {
		var me = this;
		var url = DELETE_URL + '?id=' + id;
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		url = url + "&t=" + new Date().getTime();
		var config = {
			type: "GET",
			url: url
		};
		setTimeout(function() {
			uxutil.server.ajax(config, function(result) {
				if (result.success) {
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					//隐藏遮罩层
					layer.closeAll('loading');
					if (callback) callback(result);
				}
			}, true);
		}, 100 * index);
	};
	//获取查询Url
	bhosttypeTable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	bhosttypeTable.getFields = function(isString) {
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
	bhosttypeTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	bhosttypeTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	bhosttypeTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	bhosttypeTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	bhosttypeTable.getTableWhere = function() {
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
			var search = $('#table_bhosttype_like_search');
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
	bhosttypeTable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	bhosttypeTable.onSearch = function(where) {
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
	bhosttypeTable.render = function(options) {
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
	exports('bhosttypeTable', bhosttypeTable);
});
