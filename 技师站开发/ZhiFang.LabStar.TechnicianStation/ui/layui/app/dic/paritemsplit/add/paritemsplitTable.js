/**
	@name：组合项目拆分--新增组合项目拆分关系列表
	@author：longfc
	@version 2019-10-14
 */
layui.extend({
	uxtable: 'ux/table'
}).define(['uxutil', 'form', 'table', 'uxtable'], function(exports) {
	"use strict";

	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form,
		table = layui.table,
		uxtable = layui.uxtable;

	//获取列表数据
	var SELECT_URL = uxutil.path.ROOT +
		'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchAddLBParItemSplitListByParItemId';

	var paritemsplitTable = {

		//参数配置
		config: {
		 	/**组合项目Id*/
			parItemId: null,
			page: false,
			limit: 500,
			loading: true,
			PKField: "LBParItemSplit_Id",
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
			defaultOrderBy: null,
			/**列表当前排序*/
			sort: [{"property":"LBParItemSplit_NewBarCode","direction":"ASC"}],
			url: "", //SELECT_URL,
			where: "",
			isHideLBSampleType: true,
			cols: [
				[{
						type: 'numbers',
						title: '行号',
						fixed: 'left'
					},
					{
						field: 'LBParItemSplit_Id',
						title: '关系ID',
						width: 150,
						sort: false,
						hide: true
					},
					{
						field: 'LBParItemSplit_LBItem_Id',
						title: '检验项目编号',
						width: 180,
						hide: true,
						sort: false
					},
					{
						field: 'LBParItemSplit_LBItem_CName',
						title: '项目名称',
						width: 200,
						sort: true
					},
					{
						field: 'LBParItemSplit_LBItem_ItemNo',
						title: '编码',
						width: 100,
						sort: true
					},
					{
						field: 'LBParItemSplit_LBItem_SName',
						title: '简称',
						width: 100,
						sort: true
					},
					{
						field: 'LBParItemSplit_LBItem_EName',
						title: '英文名称',
						width: 100,
						sort: true
					},
					{
						field: 'LBParItemSplit_LBSamplingGroup_Id',
						title: '采样组',
						width: 180,
						sort: false,
						templet: function(d) {
							return paritemsplitTable.createSelectStr(d);
						}
					},
					{
						field: 'LBParItemSplit_LBSamplingGroup_CName',
						title: '采样组名称',
						hide: true,
						width: 180,
						sort: false
					},
					{
						field: 'LBParItemSplit_NewBarCode',
						title: '条码序号',
						width: 140,
						edit: "text",
						sort: false
					},
					{
						field: 'LBParItemSplit_IsAutoUnion',
						title: '是否合并检验报告',
						width: 120,
						sort: true,
						align: 'center',
						templet: '#switchTpl_IsAutoUnion'
					},
					{
						field: 'LBParItemSplit_ParItem_Id',
						title: '组合项目编号',
						width: 180,
						hide: true,
						sort: false
					},
					{
						field: 'LBParItemSplit_ParItem_CName',
						title: '组合项目',
						width: 180,
						hide: true,
						sort: false
					},
					{
						field: 'LBParItemSplit_LBSamplingGroupListStr',
						title: '子项目所属采样组Str',
						width: 180,
						hide: true,
						sort: false
					}
				]
			],
			text: {
				none: '暂无相关数据'
			},
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
				var list = data.list || [];
				for(var i in  list){
					list[i].LBParItemSplit_NewBarCode = '-1';
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function(res, curr, response) {
				if (res && res.data && res.data.length > 0 && this.autoSelect == true) {
					paritemsplitTable.doAutoSelect(this, 0);
				}
			}
		}
	};

	var Class = function(setings) {
		var me = this;
		var config = $.extend(true, {}, uxtable.config, paritemsplitTable.config, setings);
		var tableIns = $.extend(true, {}, paritemsplitTable);
		tableIns.config = config;
		return tableIns;
	};

	Class.pt = Class.prototype;
	/**生成采样组下拉选择框信息*/
	paritemsplitTable.createSelectStr = function(d) {
		var me = this;
		var htmlArr = [];
		var curDataValue = d.LBParItemSplit_LBSamplingGroup_Id;
		if (!curDataValue) curDataValue = "";
		htmlArr.push('<select lay-search="" name="select_LBParItemSplit_LBSamplingGroup_Id' + d.LAY_INDEX + '"');
		htmlArr.push('lay-filter="select_LBParItemSplit_LBSamplingGroup_Id" lay-verify="required" data-value="' +
			curDataValue + '" >\n');
		htmlArr.push('<option value="请选择所属采样组"></option>\n');

		var listStr = d["LBParItemSplit_LBSamplingGroupListStr"];
		if (listStr) {
			listStr = JSON.parse(listStr);
			if (listStr.length > 0) {
				for (var i = 0; i < listStr.length; i++) {
					var curId = "" + listStr[i].Id;
					htmlArr.push('<option value="' + curId + '"');
					//当前选中的采样组
					if (curDataValue && curDataValue == curId) {
						htmlArr.push('selected=""');
					}
					htmlArr.push('>' + listStr[i].CName + '</option>\n');
				}
			}
		}
		htmlArr.push('</select>');
		return htmlArr.join("");
	};
	/**获取查询框内容*/
	paritemsplitTable.getLikeWhere = function(value) {
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
	paritemsplitTable.doAutoSelect = function(that, rowIndex) {
		var me = this;
		var data = table.cache[that.instance.key] || [];
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
	paritemsplitTable.updateRow=function(fields,rowIndex){
		var that=this;
		var data = table.cache[that.instance.key] || [];
		if (!data || data.length <= 0) return;		
		var tr = $(that.instance.config.instance.layBody[0]).find('tr:eq(' + rowIndex + ')');
		fields = fields || {};
		layui.each(fields, function(key, value){
		  if(key in data){
		    var templet, td = tr.children('td[data-field="'+ key +'"]');
		    data[key] = value;
		    that.eachCols(function(i, item2){
		      if(item2.field == key && item2.templet){
		        templet = item2.templet;
		      }
		    });
		    td.children(ELEM_CELL).html(function(){
		      return templet ? function(){
		        return typeof templet === 'function' 
		          ? templet(data)
		        : laytpl($(templet).html() || value).render(data)
		      }() : value;
		    }());
		    td.data('content', value);
		  }
		});
	};
	//获取查询Url
	paritemsplitTable.getLoadUrl = function() {
		var me = this;
		var url = SELECT_URL;
		return url;
	};
	//获取查询Fields
	paritemsplitTable.getFields = function(isString) {
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
	//设置组合项目Id
	paritemsplitTable.setParItemId = function(parItemId) {
		var me = this;
		me.config.parItemId = parItemId;
	};
	//设置默认的查询where
	paritemsplitTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	paritemsplitTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的查询where
	paritemsplitTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getTableWhere();
	};
	//设置外部传入的排序信息range
	paritemsplitTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	paritemsplitTable.getTableWhere = function() {
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
		if (me.config.parItemId) {
			arr.push("lbitemgroup.LBGroup.Id=" + me.config.parItemId);
		}
		//模糊查询条件
		if (me.searchInfo) {
			var searchV = "";
			var searchV = $('#search_text_item').val();
			
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
		if (me.config.parItemId) {
			where.parItemId = me.config.parItemId;
		}
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();
		return where;
	};
	//列表查询项处理
	paritemsplitTable.getWhere = function() {
		var me = this;
		me.config.where = me.getTableWhere();
		return me.config.where;
	};
	//对外公开
	paritemsplitTable.onSearch = function(where) {
		var me = this;
		if (where) me.setExternalWhere(where);
		me.config.url = me.getLoadUrl();
		me.config.where = me.getWhere();
		var uxtable1 = uxtable.render(me.config);
		me.instance = uxtable1.instance;
		form.render();
		return me;
	};
	//清空列表数据
	paritemsplitTable.clearData = function() {
		var me = this;
		var filter = me.config.id || "table_paritemsplit";
		table.reload(filter, {
			url: "",
			where: "",
			data: []
		});
	};
	//保存前验证
	paritemsplitTable.onSaveVerify = function() {
		var me = this;
		var cacheData = table.cache[me.config.id];
		var result = {
			success: true,
			msg: ""
		};
		for (var i = 0; i < cacheData.length; i++) {
			var curRow = cacheData[i];
			var num =  i+1;
			if (!curRow["LBParItemSplit_ParItem_Id"]) {
				result.success = false;
				result.msg = "第" + num + "行组合项目信息为空!";
				break;
			}
			if (!curRow["LBParItemSplit_LBItem_Id"]) {
				result.success = false;
				result.msg = "第" + num + "行检验项目信息为空!";
				break;
			}
			if (!curRow["LBParItemSplit_LBSamplingGroup_Id"]) {
				result.success = false;
				result.msg = "第" + num + "行采样组信息为空!";
				break;
			}
			var newBarCode = "" + curRow["LBParItemSplit_NewBarCode"];
			if (!newBarCode) {
				result.success = false;
				result.msg = "第" + num + "行条码序号信息为空!";
				break;
			}
			if (isNaN(newBarCode)) {
				result.success = false;
				result.msg = "第" + num + "行条码序号格式不正确!";
				break;
			}
		}
		return result;
	};
	//获取保存封装提交的实体集合信息
	paritemsplitTable.getEntityList = function() {
		var me = this;
		var entityList = [];
		var cacheData = table.cache[me.config.id];
		var strDataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
		for (var i = 0; i < cacheData.length; i++) {
			var curRow = cacheData[i];
			var isAutoUnion = "" + curRow["LBParItemSplit_IsAutoUnion"];
			if (!isAutoUnion||isAutoUnion == "false") isAutoUnion = 0;
			if (isAutoUnion == "true") isAutoUnion = 1;
			var entity = {
				Id: -1,
				ParItem: {
					Id: curRow["LBParItemSplit_ParItem_Id"],
					CName: curRow["LBParItemSplit_ParItem_CName"],
					DataTimeStamp: strDataTimeStamp
				},
				LBItem: {
					Id: curRow["LBParItemSplit_LBItem_Id"],
					CName: curRow["LBParItemSplit_LBItem_CName"],
					DataTimeStamp: strDataTimeStamp
				},
				LBSamplingGroup: {
					Id: curRow["LBParItemSplit_LBSamplingGroup_Id"],
					CName: curRow["LBParItemSplit_LBSamplingGroup_CName"],
					DataTimeStamp: strDataTimeStamp
				},
				NewBarCode: curRow["LBParItemSplit_NewBarCode"],
				IsAutoUnion: isAutoUnion
			}
			entityList.push(entity);
		}
		return entityList;
	};
	//主入口
	paritemsplitTable.render = function(options) {
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
			form.render();
			return tableIns;
		}
	};

	//暴露接口
	exports('paritemsplitTable', paritemsplitTable);
});
