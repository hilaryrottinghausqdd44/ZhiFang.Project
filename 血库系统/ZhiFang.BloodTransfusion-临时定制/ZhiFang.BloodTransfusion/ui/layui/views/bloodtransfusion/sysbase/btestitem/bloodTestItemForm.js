layui.extend({
	bloodBaseForm:'views/bloodtransfusion/sysbase/basic/bloodBaseForm'
}).define(['form', 'table', 'bloodBaseForm'], function(exports){
	"use strict";
	
	var $ = layui.$,
	    form  = layui.form,
	    table = layui.table,
	    layer = layui.layer,
	    uxutil = layui.uxutil,
	    bloodBaseForm = layui.bloodBaseForm;
	
    var BloodTestItemForm = function(){
		var me = this;
		bloodBaseForm.constructor.call(me);
	}
	
	BloodTestItemForm.prototype = bloodBaseForm;
	
	var bloodTestItemForm = new BloodTestItemForm();

	exports('bloodTestItemForm', bloodTestItemForm)
})