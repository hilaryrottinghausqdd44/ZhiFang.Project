layui.extend({
	uxutil:'ux/util'
}).define(['form', 'uxutil'], function(exports){
	"use strict";
	
	var $ = layui.$;
	var form = layui.form;
	var uxutil = layui.uxutil;
	
	function BloodBaseForm(){
		var me = this;
		me.UrlParams = {}; //表单参数
	}
	
	//判断是否为数组
	BloodBaseForm.prototype.IsArray = function(data)
	{
		if (Array.isArray) {
			return Array.isArray(data);
		} else {
			return  Object.prototype.toString.call(data) === "[object Array]"
		}
		
	}
	
	//获取表单通过url传递的参数
	BloodBaseForm.prototype.getUrlParams = function()
	{
		var me = this;
		me.UrlParams = uxutil.params.get();
	}
	
	//初始化表单字段
	BloodBaseForm.prototype.InitFormFields = function(filter, InitFieldsObj)
	{
	   form.val(filter, InitFieldsObj);
	}
	
	//查询数据
	BloodBaseForm.prototype.Query = function(Url, fields, id, callback)
	{
	    var me = this;
	    var url = Url;
	    var type = typeof id === 'function';
	    var Fields = me.IsArray(fields) ? fields.join(',') : fields;
	    if ((!url) || (typeof url != 'string')) return; 
	    url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + Fields;
	    type && (callback = id, id = ''); 
	    if ((id) && (!type)) {
	    	url += '&id=' + id;
	    	url += '&t=' + new Date().getTime();
	    }
	    uxutil.server.ajax({
			url: url
		}, function(data) {
			if ((callback) && (typeof callback === 'function')) callback(data);
		});
	}
	
	//获取表单字段
	BloodBaseForm.prototype.getFormFields = function(filter){
			var fields = {} 
		    ,formElem = $('.layui-form[lay-filter="' + filter +'"]')
		    ,fieldElem = $(formElem[0]).find('input,select,textarea'); //获取所有表单域
		    
		    if (!filter) return fields;
		    layui.each(fieldElem, function(key, item){
		        item.name = (item.name || '').replace(/^\s*|\s*&/, '');
                if(item.name)
                {
                	//用于支持数组 name
					if(/^.*\[\]$/.test(item.name)) {
						var key = item.name.match(/^(.*)\[\]$/g)[0];
						nameIndex[key] = nameIndex[key] | 0;
						item.name = item.name.replace(/^(.*)\[\]$/, '$1[' + (nameIndex[key]++) + ']');
					}
		            fields[item.name] = "";	
                };
		    });
		    return fields;
	}

    //获取查询字段
    BloodBaseForm.prototype.getSelectFields = function(filter){
    	var me = this; 
    	var fields = []; 
    	var fieldName = '';
  		var formfields = me.getFormFields(filter);
		layui.each(formfields, function(key, item){
		 		fieldName = key;
		 		if (fieldName){
                  fields.push(fieldName);
		 		}
		});
		return fields;
    }
    
	//获取参数,返回一个数组，包含两个元素，一个是对象，另一个是数组
	BloodBaseForm.prototype.getParams = function(tableName, filter, field){
		if ((!tableName) || (typeof tableName != 'string')) return;
		var me = this;
		var entity = {};
		var fieldName = "";
		var fieldArr = [];
		var paramsArr = [];
		var reg = new RegExp(tableName + '_', 'i');
		var fields = me.getFormFields(filter);
		layui.each(fields, function(key, item){
		 		fieldName = key ? key.replace(reg, "") : "";
		 		if (fieldName){
		 			fieldArr.push(fieldName);
		 			entity[fieldName] = field[key] ? field[key] : "";
		 		}
		});
		paramsArr.push(entity);
		paramsArr.push(fieldArr);
		return paramsArr;
	}

	//获取新增参数
	BloodBaseForm.prototype.getAddParams = function(tableName, filter, field){
		var me = this;
		var entity;
		var params = me.getParams(tableName, filter, field);
        entity = params[0];
        //新增Id需要删除   
        if (entity.hasOwnProperty('Id')) delete entity.Id;
		return {
			entity: entity
		};	
	}
	
	//获取编辑的实体参数
    BloodBaseForm.prototype.getEditParams = function(tableName, filter, field) {
		var me = this;
		var entity;
		var fieldList;
		var params = me.getParams(tableName, filter, field);
		entity = params[0];
        fieldList = params[1].join(',');
 		return {
			entity: entity,
			fields: fieldList
		};	
	};
	
     //提交数据
	BloodBaseForm.prototype.onSave = function(url, data, callback){
    	var me = this,
	    	params = JSON.stringify(data);
			var config = {
				type: "POST",
				url: url,
				data: params
			};
			uxutil.server.ajax(config, function(data) {
				//隐藏遮罩层
				if (data.success) {
				    layer.msg("保存成功！");
				    typeof callback === 'function' && callback(data);
				} else {
					layer.msg(data.msg);
				}
			});	 	
	}
	
	var bloodBaseForm = new BloodBaseForm();
	
	exports("bloodBaseForm", bloodBaseForm);
	
})
	
