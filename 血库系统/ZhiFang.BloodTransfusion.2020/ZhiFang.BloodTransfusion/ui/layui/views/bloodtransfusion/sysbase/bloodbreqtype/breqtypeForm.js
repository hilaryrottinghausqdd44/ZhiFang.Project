/**
	@name：就诊类型表单
	@author：longfc
	@version 2019-07-14
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter',
	formSelects: '/src/formselects/dist/formSelects-v4.min',
	bloodFormSelects: '/views/modules/bloodtransfusion/bloodFormSelects'
}).define(['uxutil', 'dataadapter', 'laydate', 'form', "layer", 'formSelects', "bloodFormSelects"], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var form = layui.form;
	var layer = layui.layer;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var laydate = layui.laydate;
	var formSelects = layui.formSelects;
	var bloodFormSelects = layui.bloodFormSelects;

	var breqtypeForm = {
		config: {
			//申请单号
			PK: "",
			//表单类型
			formtype: "",
			elem: '#LAY-app-form-BloodBReqType',
			id: "LAY-app-form-BloodBReqType",
			formfilter: "LAY-app-form-BloodBReqType",
			/**查询表单数据项 */
			selectFields: "",
			lastData: "",
			selectUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeById?isPlanish=true",
			/**新增服务地址 BT_UDTO_AddBloodBReqTypeAndDtl */
			addUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqType",
			/**修改服务地址*/
			editUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqTypeByField"
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, breqtypeForm.config, options);
		var inst = $.extend(true, {}, breqtypeForm, me);
		return inst;
	};
	Class.pt = Class.prototype;
	//获提交的Fields
	Class.pt.getFields = function(entity, isString) {
		var me = this;
		var fields = [];
		layui.each(entity, function(key, value) {
			fields.push(key);
		});
		return fields.join(',');
	};
	Class.pt.load = function(id, callback) {
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
	Class.pt.setValues = function(result) {
		var me = this;
		form.val(me.config.formfilter, result);
	};
	//重新处理表单获取的数据
	Class.pt.setNullValues = function(result) {
		var me = this;
		return result;
	};
	//重新处理表单获取的数据
	Class.pt.changeData = function(result) {
		var me = this;
		return result;
	};
	//表单新增初始化
	Class.pt.isAdd = function() {
		var me = this;
	};
	//表单编辑初始化
	Class.pt.isEdit = function(id) {
		var me = this;
	};
	//表单查看初始化
	Class.pt.isShow = function(id) {
		var me = this;
	};
	//表单项是否只读处理
	Class.pt.setReadOnly = function(id) {
		var me = this;
	};
	/**初始化信息数据*/
	Class.pt.initInfo = function() {
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
	Class.pt.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/BloodBReqType_/g, "");
		if(entity) entity = JSON.parse(entity);

		if(entity.Visible == "") entity.Visible = 1;
		return {
			"entity": entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		entity.fields = me.getFields(entity);
		if(data["BloodBReqType_Id"])
			entity.entity.Id = data["BloodBReqType_Id"];
		return entity;
	};
	//表单保存处理
	breqtypeForm.onSave = Class.pt.onSave = function(editForm, submitData, callback) {
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
			//清空sessionData
			layui.sessionData("BloodBReqTypeSubmit", {
				key: "submitData",
				remove: true
			});
			//隐藏遮罩层
			if(data.success) {
				id = formtype == 'add' ? data.value.id : id;
				id += '';
			} else {
				layer.msg(data.msg);
			}
			if(callback) callback(data);
		});
	};
	/**初始化表单信息*/
	breqtypeForm.initForm = function() {
		var me = this;
	};
	Class.pt.getSelectFields = function() {
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
		me.config.selectFields = field.join(",");
		return me.config.selectFields;
	};
	//核心入口
	breqtypeForm.render = function(options) {
		var me = this;
		var inst = new Class(options);
		me.initForm();
		return inst;
	};

	//基础类公共方法合并
	breqtypeForm = $.extend(true, Class.prototype, breqtypeForm);

	//暴露接口
	exports('breqtypeForm', breqtypeForm);
});