layui.extend({
	uxutil:"ux/util",
	bloodSelectData: '/views/modules/bloodtransfusion/bloodSelectData',
	bloodBaseForm: '/views/bloodtransfusion/sysbase/basic/bloodBaseForm'
}).define(['uxutil', 'form', 'bloodSelectData','bloodBaseForm'], function(exports){
	"use strict";
	
	var $ = layui.$;
	var form = layui.form;
	var uxutil = layui.uxutil;
	var bloodSelectData = layui.bloodSelectData;
	var bloodBaseForm = layui.bloodBaseForm;
	
	
	var EditForm = function(){
		var me = this;
		bloodBaseForm.constructor.call(me);
	}
	
	EditForm.prototype = bloodBaseForm;
	
	
	EditForm.prototype.getSelectListBloodUnit = function(callback){
		var selData =  bloodSelectData;
		var selUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitByHQL?isPlanish=true&where=(bloodunit.Visible = 1)";
		selData.getOptionList("bloodUnit", selUrl, "BloodUnit_Id", "BloodUnit_CName", function(html){
			$("#Bloodstyle_BloodUnit_Id").html(html);
			typeof(callback) === "function" && callback();
		})
	}
	
	EditForm.prototype.getSelectListBloodClass = function(callback){
		var selData =  bloodSelectData;
		var selUrl = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassByHQL?isPlanish=true&where=(bloodclass.Visible = 1)";
		selData.getOptionList("bloodClass", selUrl, "BloodClass_Id", "BloodClass_CName", function(html){
			$("#Bloodstyle_BloodClass_Id").html(html);
			typeof(callback) === "function" && callback();
		})
	}
	
	EditForm.prototype.initForm = function(callback){
		var me = this, count = 0;
		me.getWindowParams();
		me.getSelectListBloodClass(function(){
			count++;
			(count === 2) && form.render("select") && (typeof callback === 'function' && callback());
		});
		me.getSelectListBloodUnit(function(){
			count++;
			(count === 2) && form.render("select") && (typeof callback === 'function' && callback());		
		});
	}
	
	var editForm = new EditForm();
	
	exports("editForm", editForm);
	
})