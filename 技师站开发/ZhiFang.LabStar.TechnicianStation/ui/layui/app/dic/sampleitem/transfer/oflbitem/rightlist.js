/**
	@name：项目样本类型维护-按样本类型选择项目
	@author：longfc
	@version 2019-09-27
 */
layui.extend({}).define(['uxutil', 'uxtable', 'table', 'form'], function(exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form,
		uxtable = layui.uxtable;

	//按传入的检验小组Id从小组项目里获取检验项目信息(传入的检验小组Id值为空时从检验项目获取信息)
	var SELECT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchByLBSectionItemHQL';

	var righttable = {
		searchInfo: {
			isLike: true,
			fields: ['lbitem.CName', 'lbitem.EName', 'lbitem.SName', 'lbitem.UseCode']
		},
		//参数配置
		config: {
			page: true,
			limit: 50,
			loading: true,
			/**检验小组Id*/
			lbsectionId:null,
			/**主键Id*/
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
			oldListData: [], //初始化数据
			SectionID: null, //小组
			cols: [
				[{
						type: 'checkbox',
						fixed: 'left'
					},
					{
						type: 'numbers',
						title: '行号',
						fixed: 'left'
					},{
						field: 'LBSampleItem_Id',
						title: '关系Id',
						width: 110,
						sort: true,
						hide: true
					},
					{
						field: 'LBItem_Id',
						width: 150,
						title: '项目编号',
						hide: true,
						sort: true
					},
					{
						field: 'LBItem_CName',
						minWidth: 150,
						flex: 1,
						title: '项目名称',
						sort: true
					},
					{
						field: 'LBItem_EName',
						width: 150,
						title: '英文名称',
						sort: true
					},
					{
						field: 'LBItem_UseCode',
						width: 150,
						hide:true,
						title: '用户编码',
						sort: true
					}
				]
			],
			text: {
				none: '暂无相关数据'
			},
			parseData: function(res){ //res 即为原始返回的数据
				if(!res) return;
	            var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			changeData:function(data){
				//默认将LBSampleItem_Id值置空
				if(!data["LBSampleItem_Id"]){
					data["LBSampleItem_Id"]="";
				}
				return data;
			},
			done: function(res, curr, response) {
				if (res&&res.data&&res.data.length>0&&this.autoSelect == true) {
					righttable.doAutoSelect(this, 0);
				}
			}
		}
	};

	var Class = function(setings) {
		var me = this;
	
		var config = $.extend(true, {}, uxtable.config, righttable.config, setings);
		var tableIns = $.extend(true, {}, righttable);
		tableIns.config = config;
		return tableIns;
	};
	Class.pt = Class.prototype;

	/**获取查询框内容*/
	righttable.getLikeWhere = function(value) {
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
	righttable.doAutoSelect = function(that, rowIndex) {
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
	righttable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	righttable.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field&&columns[i].field!="LBSampleItem_Id") {
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
	righttable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	righttable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	righttable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	righttable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	righttable.getTableWhere = function() {
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
			var search = $('#table_right_like_search');
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
	righttable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	righttable.onSearch = function(where) {
		var me = this;
		if (where) me.setExternalWhere(where);
		me.config.url = me.getLoadUrl();
		me.config.where = me.getWhere();
		var uxtable1 = uxtable.render(me.config);
		me.instance = uxtable1.instance;
		return me;
	};
	//清空列表数据
	righttable.clearData = function() {
		var me = this;
		var filter = me.config.id || "righttable";
		table.reload(filter, {
			url: "",
			where: "",
			data: []
		});
	};
	//主入口
	righttable.render = function(options) {
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
	exports('righttable', righttable);
});
