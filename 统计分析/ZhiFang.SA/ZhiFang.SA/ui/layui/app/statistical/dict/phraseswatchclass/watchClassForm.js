/**
	@name：phraseswatchclass.watchClassTable 质量指标类型列表
	@author：longfc
	@version 2019-05-14
 */
layui.extend({
	//uxutil: 'ux/util'
}).define(["uxutil", "layer", "form"], function(exports) {
	"use strict";

	var $ = layui.$;
	var uxutil = layui.uxutil;
	var form = layui.form;
	var layer = layui.layer;

	var watchClassForm = {
		config: {
			elem: '',
			formfilter: '', //即 class="layui-form" 所在元素对应的 lay-filter="" 对应的值
			PK: "",
			formtype: "",
			/**load处理后的回调*/
			afterLoad: function(data) {

			}
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, watchClassForm.config, options);
		return me.render();
	};
	Class.pt=Class.prototype;
	//默认配置
	Class.pt.config = {
		fields: "PhrasesWatchClass_QIndicatorTypeId,PhrasesWatchClass_ParentID,PhrasesWatchClass_Id,PhrasesWatchClass_CName,PhrasesWatchClass_QIndicatorTypeCName,PhrasesWatchClass_ShortCode,PhrasesWatchClass_DispOrder,PhrasesWatchClass_Visible",
		lastData: "",
		selectUrl: uxutil.path.ROOT +"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassById?isPlanish=true",
		/**新增服务地址*/
		addUrl: uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_AddPhrasesWatchClass",
		/**修改服务地址*/
		editUrl: uxutil.path.ROOT +"/ReaStatisticalAnalysisService.svc/RS_UDTO_UpdatePhrasesWatchClassByField"
	};
	//获取查询Fields
	Class.pt.getFields = function(isString) {
		var me = this;
		return me.config.fields;
	};
	Class.pt.load = function(id) {
		var me = this;
		if (!id) id = me.config.PK;
		if (!id) return;

		var afterLoad = me.config.afterLoad;
		me.config.PK = id;
		var url = me.config.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getFields(true);
		url += '&id=' + id;

		uxutil.server.ajax({
			url: url
		}, function(data) {
			var result = "";
			if (data.success) {
				result = me.changeData(data.value || {});
			} else {
				//清空表单信息
				result = {};
			}
			me.setValues(result);
			if (afterLoad) {
				afterLoad(result);
			}
		});
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
	//对表单赋值处理
	Class.pt.setValues = function(result) {
		var me = this;
		form.val(me.config.formfilter, result);
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

		if (type == 'add') {
			me.isAdd();
		} else if (type == 'edit') {
			if (id) {
				me.isEdit(id);
			}
		} else if (type == 'show') {
			if (id) {
				me.isShow(id);
			} else {
				me.setReadOnly(true);
			}
		}
	};
	//表单保存处理
	Class.pt.onSave = function(formtype, data, id, callback) {
		var me = this;
		var addUrl = uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_AddPhrasesWatchClass";
		var editUrl = uxutil.path.ROOT + "/ReaStatisticalAnalysisService.svc/RS_UDTO_UpdatePhrasesWatchClassByField";
		var url = formtype == 'add' ? addUrl : editUrl;
		var params = formtype == 'add' ? me.getAddParams(data.field) : me.getEditParams(data.field);
		if (!params) return;
		if (id && !params.entity.Id) params.entity.Id = id;
		if (!id) id = params.entity.Id;
		params = JSON.stringify(params);
		//显示遮罩层
		var config = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				id = formtype == 'add' ? data.value.id : id;
				id += '';
			} else {
				layer.msg(data.msg);
			}
			if (callback) callback(data);
		});
	};
	/**@overwrite 获取新增的数据*/
	Class.pt.getAddParams = function(field) {
		var me = this;
		var entity = {
			"CName": field.PhrasesWatchClass_CName,
			"QIndicatorTypeCName": field.PhrasesWatchClass_QIndicatorTypeCName,
			"ShortCode": field.PhrasesWatchClass_ShortCode,
			"DispOrder": field.PhrasesWatchClass_DispOrder,
			"Visible": field.PhrasesWatchClass_Visible
		};
		if (field.PhrasesWatchClass_QIndicatorTypeId) {
			entity.QIndicatorTypeId = field.PhrasesWatchClass_QIndicatorTypeId;
		}
		if (field.PhrasesWatchClass_ParentID) {
			entity.ParentID = field.PhrasesWatchClass_ParentID;
		}
		return {
			entity: entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	Class.pt.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		entity.fields = 'Id,QIndicatorTypeId,QIndicatorTypeCName,ParentID,CName,ShortCode,DispOrder,Visible';
		if (data["PhrasesWatchClass_Id"])
			entity.entity.Id = data["PhrasesWatchClass_Id"];
		return entity;
	};
	//初始渲染
	Class.pt.render = function(options) {
		var me = this;
		if (options) me.config = $.extend({}, me.config, options);
		var inst = form.render();
		if (me.config.PK) {
			me.load();
		}
		//暂时用这种方式继承
		inst = $.extend({}, Class.pt, inst);
		return inst;
	};
	//new watchClassForm
	watchClassForm.newClass = function(options) {
		var me = this;
		var inst = new Class(options);
		return inst;
	};
	//对外公开返回对象
	Class.pt.result = function(that){
		that=that||new Class();
		return {
			/* load:that.load,
			isAdd:that.isAdd,
			isEdit:that.isEdit,
			isShow:that.isShow, */
			onSave:that.onSave
		}
	};
	//核心入口
	watchClassForm.render = function(options) {
		var me = this;
		var inst = new Class(options);
		//watchClassForm = inst;
		return inst;
	};
	//暴露接口
	exports('watchClassForm', watchClassForm);
});
