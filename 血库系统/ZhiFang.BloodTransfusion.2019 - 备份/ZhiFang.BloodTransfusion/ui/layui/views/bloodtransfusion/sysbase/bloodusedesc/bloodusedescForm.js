/**
	@name：就诊类型表单
	@author：longfc
	@version 2019-07-14
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter',
}).define(['uxutil', 'dataadapter', 'form', "layer","layedit"], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var form = layui.form;
	var layedit=layui.layedit;
	var layer = layui.layer;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;

	var bloodusedescForm = {
		config: {
			//主键
			PK: "100",
			//表单类型
			formtype: "edit",
			elem: '#LAY-app-form-BloodUseDesc',
			id: "LAY-app-form-BloodUseDesc",
			formfilter: "LAY-app-form-BloodUseDesc",
			layeditIndex:0,
			/**查询表单数据项 */
			selectFields: "",
			lastData: "",
			selectUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDescById?isPlanish=true",
			/**新增服务地址 BT_UDTO_AddBloodUseDescAndDtl */
			addUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_AddBloodUseDesc",
			/**修改服务地址*/
			editUrl: uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseDescByField"
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, bloodusedescForm.config, options);
		var inst = $.extend(true, {}, bloodusedescForm, me);
		return inst;
	};
	//Class.pt = Class.prototype;
	bloodusedescForm.load = function(id, callback) {
		var me = this;
		if(!id) id = me.config.PK;
		if(!id) return;

		var afterLoad = me.config.afterLoad;
		me.config.PK = id;
		var url = me.config.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getSelectFields();
		url += '&id=' + id;

		uxutil.server.ajax({
			url: url
		}, function(data) {
			var result = "";
			if(data.success) {
				result = me.changeData(data.value || {});
			} else {
				//清空表单信息
				result = {};
			}
			me.setValues(result);
			if(afterLoad) {
				afterLoad(result);
			}
			if(callback) callback(result);
		});
	};
	//对表单赋值处理
	bloodusedescForm.setValues = function(result) {
		var me = this;
		var isRender = true;
//		var filter = me.config.formfilter;
//		var itemFrom = layui.jquery('.layui-form[lay-filter="' + filter + '"] div.layui-form-item');
//
//		layui.each(result, function(name, value) {
//			name = "" + name;
//			var itemElem = layui.jquery("input[name=" + name + "]");
//			var type = "";
//
//			//如果对应的表单不存在，则不执行
//			if(!itemElem || !itemElem[0]) return;
//
//			type = itemElem[0].type;
//			//如果为复选框
//			if(type === 'checkbox') {
//				itemElem[0].checked = value;
//			} else if(type === 'radio') { //如果为单选框
//				itemElem.each(function() {
//					if(this.value == value) {
//						this.checked = true
//					}
//				});
//			} else { //其它类型的表单
//				itemElem.val(value);
//			}
//			isRender = true;
//		});
		$("#BloodUseDesc_Id").val(result["BloodUseDesc_Id"]);
		$("#BloodUseDesc_Visible").val(result["BloodUseDesc_Visible"]);
		$("#BloodUseDesc_Contents").val(result["BloodUseDesc_Contents"]);
		//layedit.sync(me.config.layeditIndex);
		layui.layedit.setContent(me.config.layeditIndex,result["BloodUseDesc_Contents"]);
	};
	//重新处理表单获取的数据
	bloodusedescForm.setNullValues = function(result) {
		var me = this;
		return result;
	};
	//重新处理表单获取的数据
	bloodusedescForm.changeData = function(result) {
		var me = this;
		return result;
	};
	//表单新增初始化
	bloodusedescForm.isAdd = function() {
		var me = this;
	};
	//表单编辑初始化
	bloodusedescForm.isEdit = function(id) {
		var me = this;
	};
	//表单查看初始化
	bloodusedescForm.isShow = function(id) {
		var me = this;
	};
	//表单项是否只读处理
	bloodusedescForm.setReadOnly = function(id) {
		var me = this;
	};
	/**初始化信息数据*/
	bloodusedescForm.initInfo = function() {
		var me = this,
			type = me.config.formtype,
			id = me.config.PK;

		if(type == 'add') {
			me.isAdd();
		} else if(type == 'edit') {
			if(id) {
				me.isEdit(id);
			}
		} else if(type == 'show') {
			if(id) {
				me.isShow(id);
			} else {
				me.setReadOnly(true);
			}
		}
	};

	/**@overwrite 获取新增的数据*/
	bloodusedescForm.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/BloodUseDesc_/g, "");
		if(entity) entity = JSON.parse(entity);
		if(entity.file) delete entity.file;

		if(entity.Visible == "") entity.Visible = 1;
		return {
			"entity": entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	bloodusedescForm.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		entity.fields = me.getFields(entity);
		if(data["BloodUseDesc_Id"])
			entity.entity.Id = data["BloodUseDesc_Id"];
		return entity;
	};
	//获提交的Fields
	bloodusedescForm.getFields = function(entity, isString) {
		var me = this;
		//		var fields = [];
		//		layui.each(entity, function (key, value) {
		//			fields.push(key);
		//		});
		//		return fields.join(',');
		return "Id,Contents,Visible";
	};
	//表单保存处理
	bloodusedescForm.onSave =  function(editForm, submitData, callback) {
		var me = editForm || this;
		var addUrl = me.config.addUrl;
		var editUrl = me.config.editUrl;
		var formtype = editForm.config.formtype;
		var id = editForm.config.PK;
		var formData = submitData || {};

		var url = formtype == 'add' ? addUrl : editUrl;
		var params = formtype == 'add' ? me.getAddParams(formData.field) : me.getEditParams(formData.field);
		if(!params) {
			layer.msg("封装保存参数信息失败!");
			return;
		}
		if(id && !params.entity.Id) params.entity.Id = id;
		//新增
		if(!params.entity.Id) params.entity.Id = "-1";
		params = JSON.stringify(params);

		var config = {
			type: "POST",
			url: url,
			data: params
		};
		//显示遮罩层
		layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.closeAll('loading');
			//隐藏遮罩层
			if(data.success) {
				id = formtype == 'add' ? data.value.id : id;
				id += '';
				if(formtype == 'add') {
					editForm.PK = id;
					me.config.PK=id;
				}
				layer.msg("保存成功");
			} else {
				layer.msg(data.msg);
			}
			if(callback) callback(data);
		});
	};
	/**初始化表单信息*/
	bloodusedescForm.initForm = function() {
		var me = this;
	};
	bloodusedescForm.getSelectFields = function() {
		var me = this;
		var field = [];
		var formElem = $(me.config.elem);
		if(!formElem) return "";
		var fieldElem = formElem.find('input,select,textarea')
		layui.each(fieldElem, function(_, item) {
			item.name = (item.name || '').replace(/^\s*|\s*&/, '');
			if(!item.name) return;

			//用于支持数组 name
			if(/^.*\[\]$/.test(item.name)) {
				var key = item.name.match(/^(.*)\[\]$/g)[0];
				nameIndex[key] = nameIndex[key] | 0;
				item.name = item.name.replace(/^(.*)\[\]$/, '$1[' + (nameIndex[key]++) + ']');
			}

			field.push(item.name);
		});
		//me.config.selectFields = field.join(",");
		me.config.selectFields="BloodUseDesc_Id,BloodUseDesc_Contents,BloodUseDesc_Visible";
		return me.config.selectFields;
	};
	//核心入口
	bloodusedescForm.render = function(options) {
		var me = this;
		var inst = new Class(options);
		me.initForm();
		return inst;
	};

	//基础类公共方法合并
	//bloodusedescForm = $.extend(true, Class.prototype, bloodusedescForm);

	//暴露接口
	exports('bloodusedescForm', bloodusedescForm);
});