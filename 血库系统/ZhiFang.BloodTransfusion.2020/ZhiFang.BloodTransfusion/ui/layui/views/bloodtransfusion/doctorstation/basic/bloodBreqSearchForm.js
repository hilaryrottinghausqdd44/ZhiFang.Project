/**
	@name：医嘱申请主单列表的查询表单
	@author：longfc
	@version 2019-07-02
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter',
	dateutil: '/views/modules/common/dateutil',
	formSelects: '/ux/other/formselects/dist/formSelects-v4.min',
	bloodFormSelects: '/views/modules/bloodtransfusion/bloodFormSelects'
}).define(['util', 'uxutil', 'laydate', 'dateutil', 'form', 'formSelects', "bloodFormSelects"], function (exports) {
	"use strict";

	var $ = layui.jquery;
	var form = layui.form;
	var uxutil = layui.uxutil;
	var util = layui.util;
	var laydate = layui.laydate;
	var dateutil = layui.dateutil;
	var formSelects = layui.formSelects;
	var bloodFormSelects = layui.bloodFormSelects;

	var bloodBreqSearchForm = {
		config: {
		}
	};
	/**初始化表单信息*/
	bloodBreqSearchForm.initForm = function (callback) {
		var me = this;
		var sdate = "";
		var initValue = "";
		var edate = util.toDateString("", "yyyy-MM-dd");
		if (edate) sdate = util.toDateString(dateutil.getNextDate(edate, -7), "yyyy-MM-dd");
		if (sdate && edate) initValue = sdate + " - " + edate;//2019-07-16 - 2019-08-31
		//查询日期范围
		laydate.render({
			elem: '#LAY-app-table-BloodBreqForm-Search-Date',
			type: 'date',
			value: initValue,
			range: true,
			done: function (value, date, endDate) {
				if(callback)callback('date',value);
			}
		});
		//科室选择下拉
		me.initDept();
		//医生选择下拉
		me.initDoctor();
		me.initBloodBReqType();
		me.initBloodUseType();
	};
	//允许下拉选择框手工编辑
	bloodBreqSearchForm.initEditSelect = function () {

	};
	//打开新增或编辑表单
	bloodBreqSearchForm.openForm = function (defaultParams, formtype, row, callback) {
		var me = this;
		var params = [];
		var reqFormNo = row ? row["BloodBReqForm_Id"] : "";
		
		params.push("hisDeptId=" + defaultParams.HisDeptId || "");
		params.push("deptId=" + defaultParams.DeptId || "");	
		params.push("hisDoctorId=" + defaultParams.HisDoctorId || "");
		params.push("doctorId=" + defaultParams.DoctorId || "");
		params.push("admId=" + defaultParams.AdmId || "");
		params.push("hisPatId=" + defaultParams.HisPatId || "");
		params.push("patNo=" + defaultParams.PatNo || "");
		params.push("cName=" + defaultParams.CName || "");
		params.push("formtype=" + formtype);
		params.push("reqFormNo=" + reqFormNo);		
		params=encodeURI(params.join("&"));//IE需要进行编码
		
		parent.layer.open({
			type: 2,
			title: formtype == "add" ? "新增用血申请" : "编辑用血申请",
			area: ['100%', '100%'],
			//content: '../edit/index.html?' + params,
			//Jcall 20191205 #start#
			content: '../edit/index.html?' + params + '&t=' + new Date().getTime(),
			//Jcall 20191205 #end#
			id: "LAY-app-form-open-edit-BloodBreqForm",
			btn: ['暂存', '确认提交', '关闭'],
			yes: function (index, layero) {
				//按钮【暂存】的回调
				me.onSave(index, layero, 1);
				return false; 
			},
			btn2: function (index, layero) {
				//Jcall 20191205 #start#
				var button = layero.find('.layui-layer-btn1'),
					className = "layui-btn-disabled";
				
				//禁用状态直接不处理
				if(button.hasClass(className)){
					return false;
				}
				button.addClass(className);
				setTimeout(function(){ 
					button.removeClass(className);
				},3000);
				//Jcall 20191205 #end#
				
				//按钮【确认提交】的回调
				me.onSave(index, layero, 2);
				return false; 
			},
			btn3: function (index, layero) {
				//按钮【关闭】的回调
				layer.close(index); //关闭弹层
			},
			end: function () {				
				//弹出的医嘱申请关闭后,触发父窗体的回调
				if (callback) callback();
			},
			cancel: function (index, layero) {
			
			}
		});
	};
	bloodBreqSearchForm.onSave = function (iframeIndex, layero, status) {
		var submitID = 'LAY-app-bloodBreqFormm-submit';
		var submit = layero.find('iframe').contents().find('#' + submitID);
		
		var tempData = {
			"iframeIndex": iframeIndex,
			"status": status
		};
		var settings = { key: "submitData", value: tempData };
		layui.sessionData("BloodBreqFormSubmit", settings);
		//触发新增或编辑表单的提交事件
		submit.trigger('click');
	};
	/**初始化部门下拉选择框*/
	bloodBreqSearchForm.initDept = function () {
		var config = bloodFormSelects.getFormSelectsConfig("Department");
		formSelects.config('selectDept', config, true);
		formSelects.data('selectDept', 'server', {
			url: uxutil.path.ROOT + config.selectUrl
		});
	};
	/**初始化医生下拉选择框*/
	bloodBreqSearchForm.initDoctor = function () {
		var config = bloodFormSelects.getFormSelectsConfig("Doctor");
		formSelects.config('selectDoctor', config, true);
		formSelects.data('selectDoctor', 'server', {
			url: uxutil.path.ROOT + config.selectUrl
		});
	};
	/**初始化就诊类型下拉选择框*/
	bloodBreqSearchForm.initBloodBReqType = function () {
		var config = bloodFormSelects.getFormSelectsConfig("BloodBReqType");
		formSelects.config('selectBloodBReqType', config, true);
		formSelects.data('selectBloodBReqType', 'server', {
			url: uxutil.path.ROOT + config.selectUrl
		});
	};
	/**初始化申请类型下拉选择框*/
	bloodBreqSearchForm.initBloodUseType = function () {
		var config = bloodFormSelects.getFormSelectsConfig("BloodUseType");
		formSelects.config('selectBloodUseType', config, true);
		formSelects.data('selectBloodUseType', 'server', {
			url: uxutil.path.ROOT + config.selectUrl
		});
	};
	//核心入口
	bloodBreqSearchForm.render = function (options) {
		var me = this;
		me.config = $.extend({}, me.config, options);
		//me.initForm();
		return me;
	};

	//暴露接口
	exports('bloodBreqSearchForm', bloodBreqSearchForm);
});
