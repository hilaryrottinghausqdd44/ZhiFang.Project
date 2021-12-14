/**
 * @name：modules/pre/order/apply/form 医嘱单信息
 * @author：Jcall
 * @version 2020-06-18
 */
layui.extend({
	uxutil:'ux/util',
	CommonSelectUser:'modules/common/select/user',
	CommonSelectDept:'modules/common/select/dept',
	CommonSelectEnum:'modules/common/select/enum',
	CommonSelectSickType:'modules/common/select/sicktype',
	CommonSelectChargeType:'modules/common/select/chargetype',
}).define(['uxutil','form','laydate','CommonSelectUser','CommonSelectDept','CommonSelectEnum','CommonSelectSickType','CommonSelectChargeType'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		laydate = layui.laydate,
		CommonSelectUser = layui.CommonSelectUser,
		CommonSelectDept = layui.CommonSelectDept,
		CommonSelectEnum = layui.CommonSelectEnum,
		CommonSelectSickType = layui.CommonSelectSickType,
		CommonSelectChargeType = layui.CommonSelectChargeType,
		uxutil = layui.uxutil,
		MOD_NAME = 'PreOrderApplyForm';
	
	//获取医嘱单信息服务地址
	var GET_ORDER_INFO_URL = uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOrderFormById";
	//获取患者就诊信息服务地址
	var GET_PATIENT_INFO_URL = uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisPatientById";
	//医嘱单信息
	var ORDER_INFO = {};
	//患者就诊信息
	var PATIENT_INFO = {};
	//医嘱单信息字段
	//医嘱单号,是否急查,医嘱执行时间,医嘱备注,收费类型
	var ORDER_INFO_FIELDS = [
		'PatID', 'OrderFormNo', 'Id', 'IsUrgent', 'OrderExecTime', 'FormMemo', 'ChargeID','ParItemCName'
	];
	//患者就诊信息字段
	//姓名,病历号,年龄,诊断信息,性别,年龄单位,科室,病区,医生,就诊类型,执行科室
	var ORDER_PATIENT_FIELDS = [
		'Id','CName','PatNo','Age','DiagName','GenderID','AgeUnitID',
		'DeptID', 'DistrictID', 'DoctorID', 'SickTypeID', 'ExecDeptID','Birthday'
	];
	
	//内部表单dom
	var FORM_DOM = [
	'<div class="layui-form" id="{formId}" lay-filter="{formId}">',
		'<div class="layui-row layui-col-space5">',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">医嘱单号</label>',
				'<div class="layui-input-block">',
					'<input type="text" name="OrderFormNo" placeholder="自动生成" readonly="true" autocomplete="off" class="layui-input">',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<input type="checkbox" name="IsUrgent" title="急查" value="0">',
			'</div>',
		'</div>',
		'<div class="layui-row layui-col-space5">',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">姓名</label>',
				'<div class="layui-input-block">',
					'<input type="text" name="CName" required lay-verify="required" autocomplete="off" class="layui-input">',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">病历号</label>',
				'<div class="layui-input-block">',
					'<input type="text" name="PatNo" required lay-verify="required" autocomplete="off" class="layui-input">',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">性别</label>',
				'<div class="layui-input-block">',
					'<select name="GenderID" id="GenderID" lay-filter="GenderID">',
						'<option value="">请选择</option>',
					'</select>',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">年龄</label>',
				'<div class="layui-input-inline" style="float:left;width:40px;">',
					'<input type="text" name="Age" required lay-verify="required" autocomplete="off" class="layui-input">',
				'</div>',
				'<div class="layui-form-mid layui-word-aux" style="padding:unset !important;">',
					'<div style="width:80px;">',
						'<select name="AgeUnitID" id="AgeUnitID" lay-filter="AgeUnitID">',
							'<option value="">请选择</option>',
						'</select>',
					'</div>',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">出生日期</label>',
				'<div class="layui-input-block">',
					'<input type="text" name="Birthday" id="Birthday" autocomplete="off" class="layui-input">',
				'</div>',
			'</div>',

			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">科室</label>',
				'<div class="layui-input-block">',
					'<select name="DeptID" id="DeptID" lay-filter="DeptID">',
						'<option value="">请选择</option>',
					'</select>',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">病区</label>',
				'<div class="layui-input-block">',
					'<select name="DistrictID" id="DistrictID" lay-filter="DistrictID">',
						'<option value="">请选择</option>',
					'</select>',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">医生</label>',
				'<div class="layui-input-block">',
					'<select name="DoctorID" id="DoctorID" lay-filter="DoctorID">',
						'<option value="">请选择</option>',
					'</select>',
				'</div>',
			'</div>',
			
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">就诊类型</label>',
				'<div class="layui-input-block">',
					'<select name="SickTypeID" id="SickTypeID" lay-filter="SickTypeID">',
						'<option value="">请选择</option>',
					'</select>',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">执行科室</label>',
				'<div class="layui-input-block">',
					'<select name="ExecDeptID" id="ExecDeptID" lay-filter="ExecDeptID">',
						'<option value="">请选择</option>',
					'</select>',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">执行时间</label>',
				'<div class="layui-input-block">',
					'<input type="text" name="OrderExecTime" id="OrderExecTime" autocomplete="off"  class="layui-input">',
				'</div>',
			'</div>',
			'<div class="layui-col-xs6 layui-col-sm4 layui-col-md3">',
				'<label class="layui-form-label">收费类型</label>',
				'<div class="layui-input-block">',
					'<select name="ChargeID" id="ChargeID" lay-filter="ChargeID">',
						'<option value="">请选择</option>',
						'<option value="1">国产</option>',
						'<option value="2">进口</option>',
					'</select>',
				'</div>',
			'</div>',
			
			'<div class="layui-col-xs12 layui-col-sm12 layui-col-md12">',
				'<label class="layui-form-label">诊断</label>',
				'<div class="layui-input-block">',
					'<textarea name="DiagName" placeholder="诊断信息" class="layui-textarea"></textarea>',
				'</div>',
			'</div>',
			'<div class="layui-col-xs12 layui-col-sm12 layui-col-md12">',
				'<label class="layui-form-label">医嘱备注</label>',
				'<div class="layui-input-block">',
					'<textarea name="FormMemo" placeholder="医嘱备注" class="layui-textarea"></textarea>',
				'</div>',
			'</div>',
			
			'<input type="text" name="OrderFormID" id="OrderFormID" class="layui-hide">',
			'<input type="text" name="PatID" id="PatID" class="layui-hide">',
		'</div>',
	'</div>',
	'<style>',
		'.layui-form-label{width:60px !important;}',
		'.layui-input-block{margin-left:76px !important;}',
	'</style>'
	];
	
	//医嘱单列表
	var PreOrderApplyForm = {
		formId:null,//表单ID
		//对外参数
		config:{
			domId:null,
			height:null,
			
			type:'add',//add:新增,edit:修改,show:查看
			orderId:null//医嘱单ID
		},
		//内部表单参数
		formConfig:{
			
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreOrderApplyForm.config,setings);
		me.formConfig = $.extend({},me.formConfig,PreOrderApplyForm.formConfig);
		
		me.formId = me.config.domId + "-form";
		
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = FORM_DOM.join("").replace(/{formId}/g,me.formId);
		$('#' + me.config.domId).append(html);
		
		//日期时间选择器
		laydate.render({ 
			elem:'#OrderExecTime',
			type:'datetime'
		});
		//日期时间选择器
		laydate.render({
			elem: '#Birthday',
			type: 'date'
		});
		//就诊类型、收费类型，基础表
		//年龄单位、性别，枚举
		//检验类型，写死3种
		
		//医生下拉框
		CommonSelectUser.render({
			domId:'DoctorID',
			code:'1001003',
			done:function(){
				
			}
		});
		
		//科室下拉框
		CommonSelectDept.render({
			domId:'DeptID',
			code:'1001101',
			done:function(){
				
			}
		});
		//病区下拉框
		CommonSelectDept.render({
			domId:'DistrictID',
			code:'1001102',
			done:function(){
				
			}
		});
		//执行科室下拉框
		CommonSelectDept.render({
			domId:'ExecDeptID',
			code:'1001104',
			done:function(){
				
			}
		});
		
		//年龄单位下拉框
		CommonSelectEnum.render({
			domId:'AgeUnitID',
			className:'AgeUnitType',
			done:function(){
				
			}
		});
		//性别下拉框
		CommonSelectEnum.render({
			domId:'GenderID',
			className:'GenderType',
			done:function(){
				
			}
		});
		
		//就诊类型
		CommonSelectSickType.render({
			domId:'SickTypeID',
			done:function(){
				
			}
		});
		//收费类型
		CommonSelectChargeType.render({
			domId:'ChargeID',
			done:function(){
				
			}
		});
		
		form.render();
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
	};
	//获取列表数据
	Class.prototype.getInfo = function(){
		var me = this,
			values = form.val(me.formId);
			
		var info = {
			LisPatient:{
				CName:values.CName,//姓名
				PatNo:values.PatNo,//病历号
				//Age:values.Age || 0,//年龄
				DiagName: values.DiagName,//诊断信息
				Birthday: values.Birthday
			},
			LisOrderForm:{
				OrderFormNo:values.OrderFormNo,//医嘱单号
				IsUrgent:values.IsUrgent ? 1 : 0,//是否急查
				OrderExecTime:values.OrderExecTime || null,//医嘱执行时间
				FormMemo:values.FormMemo,//医嘱备注
			}
		};
		//患者信息主键ID
		if(values.PatID){
			info.LisPatient.Id = values.PatID;
		}
		//医嘱单主键ID
		if(values.OrderFormID){
			info.LisOrderForm.Id = values.OrderFormID;
		}
		//患者就诊信息
		//性别下拉框
		if(values.GenderID){
			info.LisPatient.GenderID = values.GenderID;
			info.LisPatient.GenderName = $("#GenderID option:selected").text();
		}
		//年龄单位下拉框
		if(values.AgeUnitID){
			info.LisPatient.AgeUnitID = values.AgeUnitID;
			info.LisPatient.AgeUnitName = $("#AgeUnitID option:selected").text();
		}
		//科室下拉框
		if(values.DeptID){
			info.LisPatient.DeptID = values.DeptID;
			info.LisPatient.DeptName = $("#DeptID option:selected").text();
		}
		//病区下拉框
		if(info.DistrictID){
			info.LisPatient.DistrictID = values.DistrictID;
			info.LisPatient.DistrictName = $("#DistrictID option:selected").text();
		}
		//医生下拉框
		if(values.DoctorID){
			info.LisPatient.DoctorID = values.DoctorID;
			info.LisPatient.DoctorName = $("#DoctorID option:selected").text();
		}
		//就诊类型
		if(values.SickTypeID){
			info.LisPatient.SickTypeID = values.SickTypeID;
			info.LisPatient.SickType = $("#SickTypeID option:selected").text();
		}
		//执行科室下拉框
		if(values.ExecDeptID){
			info.LisPatient.ExecDeptID = values.ExecDeptID;
			//info.LisPatient.ExecDeptID = $("#ExecDeptID option:selected").text();
		}
		
		//医嘱单信息
		//收费类型
		if(values.ChargeID){
			info.LisOrderForm.ChargeID = values.ChargeID;
			info.LisOrderForm.ChargeOrderName = $("#ChargeID option:selected").text();
		}
		//患者就诊信息需要修改的字段
		info.LisPatientFields = ORDER_PATIENT_FIELDS.join(',');
		//医嘱单信息需要修改的字段
		info.LisOrderFormFields = ORDER_INFO_FIELDS.slice(1).join(',');
		
		//检验类型
		return info;
	};
	//加载医嘱单数据
	Class.prototype.loadOrderData = function(orderId,callback){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_ORDER_INFO_URL,
			data:{
				id:orderId,
				fields:'LisOrderForm_' + ORDER_INFO_FIELDS.join(',LisOrderForm_')
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				ORDER_INFO = data.value || {};
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//加载医嘱单数据
	Class.prototype.loadPatientData = function(patientId,callback){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		
		uxutil.server.ajax({
			url:GET_PATIENT_INFO_URL,
			data:{
				id:patientId,
				fields:'LisPatient_' + ORDER_PATIENT_FIELDS.join(',LisPatient_')
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				PATIENT_INFO = data.value || {};
				callback();
			}else{
				layer.msg(data.msg);
			}
		});
	};
	
	//初始化页面数据
	Class.prototype.initFormData = function(){
		var me = this,
			values = $.extend({},ORDER_INFO,PATIENT_INFO);
			
		if(values.OrderExecTime){
			values.OrderExecTime = uxutil.date.toString(values.OrderExecTime);
		}
		values.OrderFormID = ORDER_INFO.Id;
		values.PatID = PATIENT_INFO.Id;
			
		form.val(me.formId,values);
		
		//修改组件状态
		me.changeComponentStauts();
	};
	//数据更改
	Class.prototype.changeData = function(orderId){
		var me = this;
		me.config.orderId = orderId;
		//修改、查看状态，加载数据并显示
		if((me.config.type == 'edit' || me.config.type == 'show') && me.config.orderId){
			//加载医嘱单数据
			me.loadOrderData(me.config.orderId,function(){
				//加载医嘱单数据
				me.loadPatientData(ORDER_INFO.PatID,function(){
					//初始化页面数据
					me.initFormData();
				});
			});
		}
	};
	//修改组件状态
	Class.prototype.changeComponentStauts = function(){
		var me = this,
			form = $("#" + me.formId),
			inputs = form.find('input'),
			inputLength = inputs.length;
		
		if(me.config.type == 'show'){
			for(var i=0;i<inputLength;i++){
				$(inputs[i]).attr("readonly",true);
			}
		}
	};
	
	//核心入口
	PreOrderApplyForm.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//实例化表单
		me.form = form.render();
		//监听事件
		me.initListeners();
		
		//修改、查看状态，加载数据并显示
		if((me.config.type == 'edit' || me.config.type == 'show') && me.config.orderId){
			//加载医嘱单数据
			me.loadOrderData(me.config.orderId,function(){
				//加载医嘱单数据
				me.loadPatientData(ORDER_INFO.PatID,function(){
					//初始化页面数据
					me.initFormData();
				});
			});
		}
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,PreOrderApplyForm);
});
