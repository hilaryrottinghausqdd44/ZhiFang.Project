layui.extend({
	bloodBaseForm:'views/bloodtransfusion/sysbase/basic/bloodBaseForm'
}).define(['form', 'bloodBaseForm'], function(exports){
	"use strict";
	var $ = layui.$,
	    form = layui.form,
	    uxutil = layui.uxutil,
	    bloodBaseForm = layui.bloodBaseForm;
	    
	 
	var BloodUseTypeForm = function(){
		var me = this;
		bloodBaseForm.constructor.call(me);
	}
	
	BloodUseTypeForm.prototype = bloodBaseForm;
	
	var bloodUseTypeForm = new BloodUseTypeForm();
	
	exports("bloodUseTypeForm", bloodUseTypeForm);
	
})