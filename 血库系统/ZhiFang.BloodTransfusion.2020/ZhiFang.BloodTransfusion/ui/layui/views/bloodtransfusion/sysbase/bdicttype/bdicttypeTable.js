/**
	@name：字典类型列表
	@author：longfc
	@version 2019-08-29
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter'
	"soulTable": "ux/other/soultable/soulTable"
}).define(['layer', 'table', "form", 'uxutil', 'dataadapter', 'soulTable'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var soulTable = layui.soulTable;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var form = layui.form;

	var bdicttypeTable = {
		searchInfo: {
			isLike: true,
			fields: ['bdicttype.DictTypeCode', 'bdicttype.CName']
		},
		config: {
			elem: '',
			id: "",
			toolbar: "",
			//defaultToolbar: ['filter'],
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**是否自动选中行*/
			autoSelect: true,
			/**列表当前排序*/
			sort: [{
				"property": 'BDictType_DispOrder',
				"direction": 'ASC'
			}],
			/**基本查询服务URL*/
			selectUrl: uxutil.path.ROOT + "/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictTypeByHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/ServerWCF/SingleTableService.svc/ST_UDTO_DelBDictType",
			/**table查询服务URL*/
			url: "",
			where: "",
			//height: 'full-20',
			page: true,
			limits: [10, 15, 20, 30, 40, 50],
			defaultOrderBy: [{
				"property": 'BDictType_BloodOrder',
				"direction": 'ASC'
			}],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				}, {
					field: 'BDictType_Id',
					width: 140,
					hide: true,
					title: 'ID',
					filter: true
				}, {
					field: 'BDictType_DictTypeCode',
					width: 190,
					sort: true,
					title: '编码',
					filter: true
				}, {
					field: 'BDictType_CName',
					width: 160,
					sort: true,
					title: '名称',
					filter: true
				}, {
					field: 'BDictType_DispOrder',
					sort: true,
					width: 115,
					title: '显示次序',
					filter: true
				}, {
					field: 'BDictType_IsUse',
					sort: true,
					//type:'checkbox',
					title: '启用',
					templet: function(data) {
						var value = "" + data["BDictType_IsUse"];
						if (value == "1" || value == "true") value = "1";
						if (value == "1") {
							value = "是";
						} else {
							value = "否";
						}
						return value;
					}
				}]
			],
			drag: {
				toolbar: true
			}, 
			//soulTable开启拖拽工具栏
			soulSort: false, //soulTable
			filter: {
				bottom: false, //soulTable隐藏底部筛选区域，默认为 true
				//soulTable用于控制表头下拉显示，可以控制顺序、显示, 依次是：表格列、筛选数据、筛选条件、编辑筛选条件、导出excel
				items: ['column', 'excel'] //'column', 'data', 'condition', 'editCondition', 'excel'
			},
			response: dataadapter.toResponse(),
			parseData: function(res) {
				var result = dataadapter.toList(res);
				return result;
			},
			done: function(res, curr, response) {
				soulTable.render(this); //soulTable初始化
				if (this.autoSelect == true) {
					bdicttypeTable.doAutoSelect(this.table, 0);
				}
			}
		},
		set: function(options) {
			var me = this;
			if (options) me.config = $.extend({}, me.config, options);
		}
	};

	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, bdicttypeTable.config, options);
		me.config.url = bdicttypeTable.getLoadUrl();
		me.config.where = bdicttypeTable.getWhere();
		var obj = $.extend(true, {}, bdicttypeTable, me, options); // table,
		return obj;
	};
	//设置外部传入的排序信息range
	bdicttypeTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//Class.pt = Class.prototype;
	//调车列表排序
	bdicttypeTable.onSearch = function(sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	bdicttypeTable.doAutoSelect = function(curTable, rowIndex) {
		var data = table.cache[curTable.key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = curTable.config.elem.attr('lay-filter');
		var thatrow = $(curTable.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[curTable.key][index] = [];
				tr.remove();
				curTable.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 300);
	};
	/***
	 * @description 行单击后设置当前行为单选选中状态 
	 * @param obj:当前操作行
	 * */
	bdicttypeTable.setRadioCheck = function(obj) {
		var me = this;
		var index = $(obj.tr).data('index');
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //选中行样式
		obj.tr.find('input[lay-type="layTableRadio"]').prop("checked", true);
		var thisData = table.cache[me.config.id];
		//重置数据单选属性
		layui.each(thisData, function(i, item) {
			if (index === i) {
				item.LAY_CHECKED = true;
			} else {
				delete item.LAY_CHECKED;
			}
		});
		form.render('radio');
	};
	/**获取查询框内容*/
	bdicttypeTable.getLikeWhere = function(value) {
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
	//列表查询项处理
	bdicttypeTable.getSearchWhere = function() {
		var me = this;
		var arr = [];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	bdicttypeTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	bdicttypeTable.getFields = function(isString) {
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
	bdicttypeTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的申请单号
	bdicttypeTable.setReqFormNo = function(reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	bdicttypeTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	bdicttypeTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询where
	bdicttypeTable.getWhere = function() {
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
		var whereAll = "";
		if (arr.length > 0) whereAll = arr.join(") and (");
		if (whereAll) whereAll = "(" + whereAll + ")";

		var where = {
			"where": whereAll,
			'fields': me.getFields(true).join(',')
		};
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if (defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		where.t = new Date().getTime();
		return where;
	};
	/**删除一条数据*/
	bdicttypeTable.delOneById = function(index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BDictType_Id"];
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			uxutil.server.ajax({
				url: url
			}, function(data) {
				if (data.success) {
					obj.del();
					if (callback) callback();
				} else {
					layer.msg('删除失败:' + data.msg);
				}
			});
		}, 100 * index);
	};
	//核心入口
	bdicttypeTable.render = function(options) {
		var me = this;

		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('bdicttypeTable', bdicttypeTable);
});
