/**
	@name：字典类型列表
	@author：longfc
	@version 2019-08-29
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter'
}).define(['layer', 'table', "form", 'uxutil', 'dataadapter'], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var form = layui.form;

	var bdictTable = {
		searchInfo: {
			isLike: true,
			fields: ['bdict.SName', 'bdict.CName']
		},
		config: {
			elem: '',
			id: "",
			toolbar: "",
			/**字典类型ID:默认-1,表示获取不存在的字典类型信息*/
			bdictTypeId: "-1",
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
				"property": 'BDict_DispOrder',
				"direction": 'ASC'
			}],
			/**基本查询服务URL*/
			selectUrl: uxutil.path.ROOT + "/SingleTableService.svc/ST_UDTO_SearchBDictByHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/SingleTableService.svc/ST_UDTO_DelBDict",
			/**table查询服务URL*/
			url: "",
			where: "",
			//height: 'full-20',
			page: true,
			limits: [10, 15, 20, 30, 40, 50],
			defaultOrderBy: [{
				"property": 'BDict_BloodOrder',
				"direction": 'ASC'
			}],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				}, {
					field: 'BDict_Id',
					width: 140,
					hide: true,
					title: 'ID'
				},{
					field: 'BDict_CName',
					width: 160,
					sort: true,
					title: '名称'
				}, {
					field: 'BDict_SName',
					width: 190,
					sort: true,
					title: '简称'
				},{
					field: 'BDict_Shortcode',
					width: 190,
					sort: true,
					title: '快捷码'
				},  {
					field: 'BDict_DispOrder',
					sort: true,
					width: 95,
					title: '显示次序'
				}, {
					field: 'BDict_IsUse',
					sort: true,
					//type:'checkbox',
					title: '启用',
					templet: function(data) {
						var value = "" + data["BDict_IsUse"];
						if(value == "1" || value == "true") value = "1";
						if(value == "1") {
							value = "是";
						} else {
							value = "否";
						}
						return value;
					}
				}]
			],
			response: dataadapter.toResponse(),
			parseData: function(res) {
				var result = dataadapter.toList(res);
				return result;
			},
			done: function(res, curr, response) {
				if(this.autoSelect == true) {
					bdictTable.doAutoSelect(this.table, 0);
				}
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
		me.config = $.extend({}, bdictTable.config,me.config, options);
		me.config.url = bdictTable.getLoadUrl();
		me.config.where = bdictTable.getWhere();
		me = $.extend(true, {}, bdictTable,me);
		return me;
	};
	//Class.pt = Class.prototype;
	//调车列表排序
	bdictTable.onSearch = function(sort) {
		var me = this;
		me.config.sort = sort || [];
	};
	/***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	bdictTable.doAutoSelect = function(curTable, rowIndex) {
		var data = table.cache[curTable.key] || [];
		if(!data || data.length <= 0) return;

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
	bdictTable.setRadioCheck = function(obj) {
		var me = this;
		var index = $(obj.tr).data('index');
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //选中行样式
		obj.tr.find('input[lay-type="layTableRadio"]').prop("checked", true);
		var thisData = table.cache[me.config.id];
		//重置数据单选属性
		layui.each(thisData, function(i, item) {
			if(index === i) {
				item.LAY_CHECKED = true;
			} else {
				delete item.LAY_CHECKED;
			}
		});
		form.render('radio');
	};
	/**获取查询框内容*/
	bdictTable.getLikeWhere = function(value) {
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
	bdictTable.getSearchWhere = function() {
		var me = this;
		var arr = [];
		var where = "";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	bdictTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	bdictTable.getFields = function(isString) {
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
	//设置默认的查询where
	bdictTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的申请单号
	bdictTable.setReqFormNo = function(reqFormNo) {
		var me = this;
		me.config.ReqFormNo = reqFormNo;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	bdictTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	bdictTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询where
	bdictTable.getWhere = function() {
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
		//外部传入的当前字典类型Id
		if(me.config.bdictTypeId) {
			arr.push("bdict.BDictType.Id="+me.config.bdictTypeId);
		}
		
		var whereAll = "";
		if(arr.length > 0) whereAll = arr.join(") and (");
		if(whereAll) whereAll = "(" + whereAll + ")";

		var where = {
			"where": whereAll,
			'fields': me.getFields(true).join(',')
		};
		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if(defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		where.t = new Date().getTime();
		return where;
	};
	/**删除一条数据*/
	bdictTable.delOneById = function(index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BDict_Id"];
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
	//核心入口
	bdictTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('bdictTable', bdictTable);
});