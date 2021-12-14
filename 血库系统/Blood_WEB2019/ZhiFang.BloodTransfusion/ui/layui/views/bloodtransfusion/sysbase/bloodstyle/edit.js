layui.extend({
	uxutil:"ux/util",
	editForm: "views/bloodtransfusion/sysbase/bloodstyle/editForm"
}).use(['form', 'layer','uxutil','editForm'], function(){
	"use strict";
	
	var $ = layui.$;
	var form = layui.form;
	var layer = layui.layer;
	var uxutil = layui.uxutil;
	var editForm = layui.editForm;
	
	
	//初始化表单参数,通过回调函数通知下拉选择渲染完成后，初始化表单数据
    editForm.initForm(function(){
    	form.val("LAY-bloodstyle-editform-filter", editForm.editParams);	
    });

	//监听提交事件
    form.on('submit(save)', function(data){
    	var Url = editForm.editParams.Url;
    	var opType = editForm.editParams.formType;
    	var iframeIndex = $("#LAY-bloodstyle-submit").attr("iframeIndex");
    	var dataParams = (opType === 'add') ? 
    		editForm.getAddParams("bloodstyle", 'LAY-bloodstyle-editform-filter', data.field) : 
    		editForm.getEditParams("bloodstyle", 'LAY-bloodstyle-editform-filter',data.field);
        editForm.onSave(Url, dataParams, function(){
        	parent.layer.close(iframeIndex);
        });
        return false;
    })
    
})
