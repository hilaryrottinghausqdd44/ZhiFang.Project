layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	bloodusedescForm: '/views/bloodtransfusion/sysbase/bloodusedesc/bloodusedescForm'
}).use(['uxutil', 'dataadapter', 'form', 'layedit', 'bloodusedescForm'], function() {
	"use strict";

	var $ = layui.jquery;
	var docHeight = $(document).height();
	
	var form = layui.form;
	var layedit = layui.layedit;
	var bloodusedescForm = layui.bloodusedescForm;
	var bloodusedescForm1 = null;
	var layeditIndex=0;
	
	function initContents() {
		//表单高度
		$('#LAY-app-form-BloodUseDesc').css("height",docHeight);	
		layeditIndex = layedit.build('BloodUseDesc_Contents', {
			height:docHeight - 200, //设置编辑器高度
			tool: [
				'strong' //加粗
				, 'italic' //斜体
				, 'underline' //下划线
				, 'del' //删除线
				, '|' //分割线
				, 'left' //左对齐
				, 'center' //居中对齐
				, 'right' //右对齐
				, 'link' //超链接
				, 'unlink' //清除链接
				, 'face' //表情
			]
		});
	};
	//表单监听
	function onbloodusedescForm() {
		//表单提交事件监听
		layui.form.on('submit(LAY-app-submit-BloodUseDesc)', function(formData) {
			formData.field["BloodUseDesc_Contents"]=layedit.getContent(layeditIndex);
			//表单保存处理
			bloodusedescForm1.onSave(bloodusedescForm1, formData, function(data) {
			});
		});
		
	};

	function getBloodusedescFormConfig() {
		var config = {
			title: '用血说明信息',
			formtype: "edit",
			elem: '#LAY-app-form-BloodUseDesc',
			id: "LAY-app-form-BloodUseDesc",
			formfilter: "LAY-app-form-BloodUseDesc",
			layeditIndex:layeditIndex,
			PK: "100"
		};
		return config;
	};

	function init() {
		initContents();
		bloodusedescForm1 = bloodusedescForm.render(getBloodusedescFormConfig());		
		bloodusedescForm1.load(100);
		onbloodusedescForm();
	};

	init();
});