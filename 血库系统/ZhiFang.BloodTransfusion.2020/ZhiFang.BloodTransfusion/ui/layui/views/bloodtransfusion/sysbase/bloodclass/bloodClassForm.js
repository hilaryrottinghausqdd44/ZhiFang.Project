layui.extend({
	bloodBaseForm:"views/bloodtransfusion/sysbase/basic/bloodBaseForm",
	formActionType:"views/bloodtransfusion/sysbase/bloodclass/formActionType"
}).define(['form', 'bloodBaseForm', 'formActionType'], function(exports){
	"use strict";
	var $ = layui.$,
	    form = layui.form,
	    uxutil = layui.uxutil,
	    bloodBaseForm = layui.bloodBaseForm,
	    formActionType = layui.formActionType; 
	    
	var BloodClassForm = function(){
		var me = this;
		bloodBaseForm.constructor.call(me);
		me.PK = '';
		//服务url配置
		var urlRoot = uxutil.path.ROOT;
		me.urlConfig = {
			urlAdd: urlRoot + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodClass",
			urlEdit: urlRoot + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClassByField",
			urlDel: urlRoot + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodClass",
			urlSel: urlRoot + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassByHQL?isPlanish=true",
			urlSelById: urlRoot + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassById?isPlanish=true"
		};
		
		//表单配置
		me.formConfig = {
			form_table_name: 'bloodclass',
			form_filter: 'bloodclass_form_filter',
			form_submit_id: 'bloodclass_form_submit',
			form_submit_filter: 'bloodclass_form_submit_filter',
			form_fields: '',
			form_selFields: ''
		}; 
		
		//检索配置
		me.searchConfig = {
			search_select_visible_id: 'search_select_visible',
			search_text_mixed_id: 'search_text'
		};
		
		//按钮配置
		me.buttonConfig = {
			button_search_id: 'btn_search',
			button_add_id: 'btn_add',
			button_eidt_id: 'btn_eidt'
		};
		
	};
	
	//实例是不能改变原型对象的
    BloodClassForm.prototype = bloodBaseForm;
    //先创建实例
    var bloodClassForm = new BloodClassForm();
    //根据ID查询数据
	bloodClassForm.QueryById = function(id, callback){
		var me = this;
		//初始化查询变量
		var url = me.urlConfig.urlSelById;
		var filter = me.formConfig.form_filter;
		var tableName = me.formConfig.form_table_name;
		var selFields = me.formConfig.form_selFields || me.getSelectFields(filter);
	    me.Query(url, selFields, id, callback);	
	   	!me.formConfig.form_selFields && selFields && (me.formConfig.form_selFields = selFields); 
	};
	//新增
	bloodClassForm.doNewAdd = function(){
		var me = this;
		//设置编辑状态
		formActionType.setAdd();
		//初始化表单字段
		var filter = me.formConfig.form_filter;
		var fields = me.formConfig.form_fields || me.getFormFields(filter);
		me.InitFormFields(filter, fields);
		!me.formConfig.form_fields && fields && (me.formConfig.form_fields = fields);
	};
	
	//编辑
	bloodClassForm.doEdit = function(id, data){
		var me = this;
		var type = typeof id === 'object';
		//初始化表单字段
		var filter = me.formConfig.form_filter;
		var callback = function(obj){
		    data = {};
		    obj.success && (data = obj.value);
		   //设置编辑状态
		   formActionType.setEdit();
		   me.InitFormFields(filter, data);
		}
		type && !data &&(data = id, 
			id = '',
		    formActionType.setEdit(), 
		    me.InitFormFields(filter, data));
		!type && id &&(me.QueryById(id, callback));

	};
	
	bloodClassForm.doBrowser = function(id, data){
		var me = this;
		var filter = me.formConfig.form_filter;
		var type = typeof id === 'object';
		type && !data && (data = id, 
			id = '', 
			formActionType.setBrowser(), 
			me.InitFormFields(filter, data));
		id && (me.QueryById(id, function(result){
		    	data = {};
		    	obj.success && (data = obj.value);
		    	formActionType.setBrowser();//恢复浏览状态
		    	me.InitFormFields(filter, data);
		   }))

	};
	
	//保存数据
	bloodClassForm.doSave = function(data, callback){
	  	var me = this;
	  	var filer = me.formConfig.form_filter;
	  	var urlAdd = me.urlConfig.urlAdd;
	  	var urlEdit = me.urlConfig.urlEdit;
	  	var tableName = me.formConfig.form_table_name;
	  	var ActionType = formActionType.getActionType();
	  	var ActUrl = ActionType === formActionType.ACTION_ADD ? urlAdd : urlEdit; 
	  	var params = ActionType === formActionType.ACTION_ADD ? 
	      	me.getAddParams(tableName, filer, data) : me.getEditParams(tableName, filer, data);
	    me.onSave(ActUrl, params, callback);
	};
	
	exports("bloodClassForm", bloodClassForm);
	
})