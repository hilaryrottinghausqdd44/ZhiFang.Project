/**
	@name：医嘱申请编辑表单
	@author：longfc
	@version 2019-07-01
 */
layui.extend({
	//uxutil: 'ux/util'
	//dataadapter: 'ux/dataadapter',
	bloodsconfig: '/config/bloodsconfig',
	insertSelect: '/ux/other/insertselect/insertSelect',
	formSelects: '/ux/other/formselects/dist/formSelects-v4.min',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodSelectData: '/views/modules/bloodtransfusion/bloodSelectData',
	bloodFormSelects: '/views/modules/bloodtransfusion/bloodFormSelects'
}).define(['util', 'uxutil', 'dataadapter', 'laydate', 'form', "layer", 'bloodsconfig', 'formSelects',
	"cachedata", "bloodSelectData", "bloodFormSelects", "insertSelect"
], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var form = layui.form;
	var layer = layui.layer;
	var util = layui.util;
	var uxutil = layui.uxutil;
	var dataadapter = layui.dataadapter;
	var laydate = layui.laydate;
	var formSelects = layui.formSelects;
	var bloodFormSelects = layui.bloodFormSelects;
	var bloodSelectData = layui.bloodSelectData;
	var bloodsconfig = layui.bloodsconfig;
	var insertSelect = layui.insertSelect;
	var cachedata = layui.cachedata;

	var breqEditForm = {
		//新增时外部传入的表单项默认值
		defaultValues: "",
		config: {
			//申请单号
			PK: "",
			//表单类型
			formtype: "",
			elem: '#LAY-app-form-BloodBreqForm',
			id: "LAY-app-form-BloodBreqForm",
			formfilter: "LAY-app-form-BloodBreqForm",
			/**查询表单数据项 */
			selectFields: "",
			lastData: "",
			/**新增用血申请时,患者Abo及Rh是否有值(从Lis获取) */
			patABOAndRhHasVal: true,
			selectUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBreqFormById?isPlanish=true",
			/**新增服务地址 BT_UDTO_AddBloodBReqFormAndDtl */
			addUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqFormAndDtl",
			/**修改服务地址*/
			editUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormAndDtlByField"
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, me.config, breqEditForm.config, options);
		var inst = $.extend(true, {}, breqEditForm, me);
		return inst;
	};
	//重新处理表单获取的数据
	breqEditForm.changeResult = function(result) {
		var me = this;
		//急查标志值处理
		var bUseTimeTypeID = result["BloodBReqForm_BUseTimeTypeID"];
		if(bUseTimeTypeID) {
			if(bUseTimeTypeID == "1" || bUseTimeTypeID == "是" || bUseTimeTypeID == "true") {
				bUseTimeTypeID = true;
			} else {
				bUseTimeTypeID = false;
			}
		} else {
			bUseTimeTypeID = false;
		}
		result["BloodBReqForm_BUseTimeTypeID"] = bUseTimeTypeID;
		return result;
	};
	//表单新增初始化
	breqEditForm.isAdd = function() {
		var me = this;
	};
	//表单编辑初始化
	breqEditForm.isEdit = function(id) {
		var me = this;
	};
	//表单查看初始化
	breqEditForm.isShow = function(id) {
		var me = this;
	};
	/**初始化信息数据*/
	breqEditForm.initInfo = function() {
		var me = this,
			type = me.config.formtype,
			id = me.config.PK;

		if(type == 'add') {
			me.isAdd();
		} else if(type == 'edit') {
			if(id) {
				me.isEdit(id);
			}
		} else if(type == 'show') {
			if(id) {
				me.isShow(id);
			}
		}
	};
	/**
	 * 紧急用血标志
	 * @param {Object} checked
	 */
	breqEditForm.setPatABOOfBUseTimeTypeID = function(checked) {
		var me = this;
		/**
		 * 如果患者Abo及患者Rh从LIS获取值为空时:
		 * (1)如果"是否允许患者ABO及患者Rh手工选择"设置为"是",患者ABO及患者Rh可以手工选择;
		 * (2)"抢救用血"勾选上时,患者ABO,患者Rh默认为"未知";
		 * (3)非抢救用血，保存时,如果患者ABO或Rh为空,提示"非抢救用血申请,需等待ABO及RH血型结果"; 
		 */
		if(me.config.patABOAndRhHasVal == false) {
			var valStr = "";
			if(checked == true) {
				valStr = "未知";
			}
			$('[name="BloodBReqForm_PatABO"]').val(valStr);
			$('[name="BloodBReqForm_PatRh"]').val(valStr);
		}

		form.render('select');
	};
	//获提交的Fields
	breqEditForm.getFields = function(entity, isString) {
		var me = this;
		var fields = [];
		//确认申请的更新信息由后台封装处理
		var noFields = ["ApplyID", "ApplyName", "ReqTime", "ApplyTime"];
		layui.each(entity, function(key, value) {
			if(noFields.indexOf(key) < 0)
				fields.push(key);
		});
		return fields.join(',');
	};
	breqEditForm.load = function(id, callback) {
		var me = this;
		if(!id) id = me.config.PK;
		if(!id) return;

		var afterLoad = me.config.afterLoad;
		me.config.PK = id;
		var url = me.config.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getSelectFields();
		url += '&id=' + id;
		url += '&t=' + new Date().getTime();
		uxutil.server.ajax({
			url: url
		}, function(data) {
			var result = {};
			if(data.success) {
				result = me.changeResult(data.value || {});
				if(result.BloodBReqForm_DataTimeStamp) delete result.BloodBReqForm_DataTimeStamp;
			} else {
				//清空表单信息
				result = {};
			}
			me.setValues(result);
			if(afterLoad) {
				afterLoad(result);
			}
			if(callback) callback(result);
		});
	};
	//对表单赋值处理
	breqEditForm.setValues = function(result) {
		var me = this;
		var isSetReadonly = false;
		//判断是HIS调用还是血库系统登录
		var ishisCall = bloodsconfig.getData(bloodsconfig.cachekeys.ISHISCALL_KEY);
		if(ishisCall == true) {
			isSetReadonly = true;
		}
		//表单类型为修改或查看时,病人信息不能修改
		if(isSetReadonly == false && me.config.formtype != "add") {
			isSetReadonly = true;
		}
		//患者Abo及患者Rh是否有值处理
		if(!result["BloodBReqForm_PatABO"]) {
			me.config.patABOAndRhHasVal = false;
		}
		//患者Abo及患者Rh是否有值处理
		if(!result["BloodBReqForm_PatRh"]) {
			me.config.patABOAndRhHasVal = false;
		}
		//如果是抢救用血时,患者ABO,患者Rh从LIS获取值为空时,默认为"未知"
		var buseTimeTypeID = "" + result["BloodBReqForm_BUseTimeTypeID"];
		if(me.config.patABOAndRhHasVal == false && (buseTimeTypeID == "1" || buseTimeTypeID == "true")) {
			result["BloodBReqForm_PatABO"] = "未知";
			result["BloodBReqForm_PatRh"] = "未知";
		}
		me.insertSelectRender(result);
		me.setPatInfoReadonly(isSetReadonly);
		result = me.formatDateTime(result);
		//根据患者性别信息处理孕及产是否显示
		me.setPatInfoHideOfSex(result);
		var filter = me.config.formfilter || "LAY-app-form-BloodBreqForm";
		me.inintAllSelect(function() {
			setTimeout(function() {
				form.val(filter, result);
				//紧急标志值
				var checked = false;
				if(result["BloodBReqForm_BUseTimeTypeID"]) checked = result["BloodBReqForm_BUseTimeTypeID"];
				if(checked == "true" || checked == "1" || checked == "on") checked = true;
				if(checked == "false" || checked == "0") checked = false;
				me.setPatABOOfBUseTimeTypeID(checked);
				form.render('select', filter);
			}, 300);
			//me.consoleSelectVal();
		});
	};
	/***
	 * 根据患者性别信息处理孕及产是否显示
	 * @param {Object} result
	 */
	breqEditForm.setPatInfoHideOfSex = function(result) {
		var me = this;
		var hidden = true;
		if(result["BloodBReqForm_Sex"] == "女") {
			hidden = false;
		}
		if(hidden == true) {
			$('#BloodBReqForm_Gravida2').hide();
			$('#BloodBReqForm_Birth2').hide();
		}
	};
	/***
	 * 医嘱申请时,患者信息是否设置为只读
	 * @param {Object} isReadonly
	 */
	breqEditForm.setPatInfoReadonly = function(isReadonly) {
		var me = this;
		if(isReadonly == undefined) isReadonly = true;
		$('[name="BloodBReqForm_PatID"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_PatNo"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_CName"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_Birthday"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_AgeALL"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_Bed"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_PatHeight"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_PatWeight"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_AddressType"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_Diag"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_WardNo"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_DeptNo"]').attr("readonly", isReadonly);
		$('[name="BloodBReqForm_WristBandNo"]').attr("readonly", isReadonly);
		//紧急用血设置为只读
		if(bloodsconfig.HisInterface.ISBUSETIMETYPEIDREADONLY) {
			$('input[type=checkbox][name="BloodBReqForm_BUseTimeTypeID"]').attr("readonly", true);
			$('input[type=checkbox][name="BloodBReqForm_BUseTimeTypeID"]').attr("disabled", true);
		}
		//单选项
		$('input[type=radio][name="BloodBReqForm_Sex"]').attr("disabled", isReadonly);
		if(isReadonly == true) {
			$('[name="BloodBReqForm_PatID"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_PatNo"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_CName"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_Birthday"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_AgeALL"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_Bed"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_PatHeight"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_PatWeight"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_AddressType"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_WardNo"]').addClass("layui-disabled");
			$('[name="BloodBReqForm_DeptNo"]').attr("disabled", true);
			$('[name="BloodBReqForm_Diag"]').attr("disabled", true);
			$('[name="BloodBReqForm_WristBandNo"]').attr("disabled", true);
		}
		//是否允许患者ABO及患者Rh手工选择
		var disabledVal = true;
		if(me.config.patABOAndRhHasVal == false && bloodsconfig.HisInterface.ISALLOWPATABOANDRHOPT == true) {
			disabledVal = false;
		}
		$('[name="BloodBReqForm_PatABO"]').attr("disabled", disabledVal);
		$('[name="BloodBReqForm_PatRh"]').attr("disabled", disabledVal);
	};
	/**@overwrite 格式化日期值*/
	breqEditForm.formatDateTime = function(result) {
		var me = this;
		var format = "yyyy-MM-dd";
		//格式化出生日期
		var birthday = result["BloodBReqForm_Birthday"];
		if(birthday) { //&& birthday.indexOf("-") < 0
			birthday = util.toDateString(birthday, format);
			if(!birthday) birthday = "";
			result["BloodBReqForm_Birthday"] = birthday;
		}
		//格式化入院日期
		var toHosdate = result["BloodBReqForm_ToHosdate"];
		if(toHosdate) { // && toHosdate.indexOf("-") < 0
			toHosdate = util.toDateString(toHosdate, format);
			if(!toHosdate) toHosdate = "";
			result["BloodBReqForm_ToHosdate"] = toHosdate;
		}
		//格式化申请日期
		var applyTime = result["BloodBReqForm_ApplyTime"];
		if(me.config.formtype != "add" && applyTime && applyTime.indexOf("-") < 0) { // 
			applyTime = util.toDateString(applyTime, "yyyy-MM-dd HH:mm:ss");
			if(!applyTime) applyTime = "";
			result["BloodBReqForm_ApplyTime"] = applyTime;
		}
		//格式化预用日期
		if(result["BloodBReqForm_UseTime"]) {
			result["BloodBReqForm_UseTime"] = util.toDateString(result["BloodBReqForm_UseTime"], format);
		}
		return result;
	};
	/**@overwrite 获取新增的数据*/
	breqEditForm.getAddParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data).replace(/BloodBReqForm_/g, "");
		if(entity) entity = JSON.parse(entity);

		//特殊处理急查标志值(0:否;1:是;)
		var bUseTimeTypeID = entity["BUseTimeTypeID"];
		var useTimeType = $('[lay-filter="BloodBReqForm_BUseTimeTypeID"]');
		if(useTimeType) {
			bUseTimeTypeID = useTimeType[0].checked;
		}
		if(bUseTimeTypeID) {
			bUseTimeTypeID = "" + bUseTimeTypeID;
			if(bUseTimeTypeID == "1" || bUseTimeTypeID == "是" || bUseTimeTypeID == "true" || bUseTimeTypeID == "on") {
				bUseTimeTypeID = 1;
			} else {
				bUseTimeTypeID = 0;
			}
		} else {
			bUseTimeTypeID = 0;
		}
		entity.BUseTimeTypeID = bUseTimeTypeID;
		if(entity.Birthday) entity.Birthday = uxutil.date.toServerDate(entity.Birthday);
		if(entity.ToHosdate) entity.ToHosdate = uxutil.date.toServerDate(entity.ToHosdate);
		if(entity.ApplyTime) entity.ApplyTime = uxutil.date.toServerDate(entity.ApplyTime);
		if(entity.ReqTime) entity.ReqTime = uxutil.date.toServerDate(entity.ReqTime);
		if(entity.UseTime) entity.UseTime = uxutil.date.toServerDate(entity.UseTime);
		//原CS申请时间
		if(!entity.ReqTime) entity.ReqTime = entity.ApplyTime;
		//病人信息处理
		if(!entity.PatID) entity.PatID = entity.PatNo;
		if(!entity.PatHeight) entity.PatHeight = null;
		if(!entity.PatWeight) entity.PatWeight = null;
		if(entity.Visible == "") entity.Visible = 1;

		if(entity.BUseTimeTypeID == "") entity.BUseTimeTypeID = 0; //急查标志Id
		if(entity.BReqTypeID == "") entity.BReqTypeID = null; //就诊类型Id
		if(entity.UseTypeID == "") entity.UseTypeID = null; //申请类型Id

		//申请人Id
		if(!entity.ApplyID) {
			entity.ApplyID = entity.DoctorNo;
		}
		if(!entity.ApplyName) {
			//var applyName = $("#BloodBReqForm_DoctorNo").find("option:selected").text();
			var applyName = $('[lay-filter="BloodBReqForm_DoctorNo"]').find("option:selected").text();
			if(!applyName) applyName = "";
			entity.ApplyName = applyName; //formSelects.value('BloodBReqForm_DoctorNo', 'nameStr');
		}
		return {
			"entity": entity
		};
		return entity;
	};
	/**@overwrite 获取修改的数据*/
	breqEditForm.getEditParams = function(data) {
		var me = this;
		var entity = me.getAddParams(data);
		entity.fields = me.getFields(entity.entity);
		if(data["BloodBReqForm_Id"])
			entity.entity.Id = data["BloodBReqForm_Id"];
		return entity;
	};
	//表单保存处理Class.pt.onSave =
	breqEditForm.onSave = function(editForm, submitData, callback) {
		var me = editForm || this;
		var submitData2 = layui.sessionData("BloodBreqFormSubmit").submitData;
		//保存遮罩层:在index.js的"表单提交事件监听"
		var layerIndex = submitData.layerIndex;
		var addUrl = me.config.addUrl;
		var editUrl = me.config.editUrl;
		var formtype = submitData.formtype || me.config.formtype;
		var id = submitData.PK || me.config.PK;
		var formData = submitData.formData || {};
		var breqItemSubmit = submitData.breqItemSubmit;
		var resultTableSubmit = submitData.resultTableSubmit;
		//当前操作的医生信息
		var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);

		var url = formtype == 'add' ? addUrl : editUrl;
		var params = formtype == 'add' ? me.getAddParams(formData.field) : me.getEditParams(formData.field);
		//配置类信息
		var bloodsConfigVO = {
			"Common": bloodsconfig.Common,
			"CSServer": bloodsconfig.CSServer,
			"HisInterface": bloodsconfig.HisInterface
		};
		params.bloodsConfigVO = bloodsConfigVO;

		//默认返回信息
		var result1 = {
			success: false,
			autoupload: false,
			isClose: false,
			reviewTips: "", //确认提交后的审批提示信息
			value: {
				id: ""
			}
		};
		if(!params) {
			//if(layerIndex2 != null) layer.close(layerIndex2);
			if(layerIndex != null) layer.close(layerIndex);
			if(layerIndex != null) parent.layer.close(layerIndex);
			layer.msg("封装保存参数信息失败,不能提交!");
			if(callback) {
				return callback(result1);
			} else {
				return;
			}
		}
		/**
		 * 患者ABO及患者Rh验证判断处理
		 * 非抢救用血，保存时,如果患者ABO或Rh为空,提示"非抢救用血申请,需等待ABO及RH血型结果"
		 */
		if(params.entity.BUseTimeTypeID == 0) {
			if(!params.entity.PatABO) {
				//if(layerIndex2 != null) layer.close(layerIndex2);
				if(layerIndex != null) layer.close(layerIndex);
				if(layerIndex != null) parent.layer.close(layerIndex);
				layer.alert('非抢救用血申请,需等待ABO及RH血型结果!', {
					title: "患者ABO提示信息",
					btn: ['关闭'],
					icon: 5
				});
				if(callback) {
					return callback(result1);
				} else {
					return;
				}
			}
			if(!params.entity.PatRh) {
				//if(layerIndex2 != null) layer.close(layerIndex2);
				if(layerIndex != null) layer.close(layerIndex);
				if(layerIndex != null) parent.layer.close(layerIndex);
				layer.alert('非抢救用血申请,需等待ABO及RH血型结果!', {
					title: "患者Rh(D)提示信息",
					btn: ['关闭'],
					icon: 5
				});
				if(callback) {
					return callback(result1);
				} else {
					return;
				}
			}
		}

		if(id && !params.entity.Id) params.entity.Id = id;
		//新增
		if(!params.entity.Id) params.entity.Id = "-1";
		params.curDoctor = sysCurUserInfo;
		params.addBreqItemList = breqItemSubmit.addBreqItemList;
		params.addResultList = resultTableSubmit.addResultList;
		if(formtype == "edit") {
			params.editBreqItemList = breqItemSubmit.editBreqItemList;
			params.editResultList = resultTableSubmit.editResultList;
		};
		var bautoupload = false; //记录是否自动上传 2019-12-02 by xhz		
		var status = submitData2.status;
		//紧急用血自动完成审核 2019-11-29 by xhz  (formtype == "add" ) && 
		if(params.entity.BUseTimeTypeID == 1 && status == 2 &&
			bloodsconfig.HisInterface.ISBUSETIMETYPEIDAUTOUPLOADADD) {
			status = '9'; //审批完成
			bautoupload = true; //需要自动上传
			params.entity.CheckCompleteFlag = "1"; //审核完成
		}
		//当前弹出的新增或编辑的iframe索引
		var iframeIndex = submitData2.iframeIndex;

		var userInfo = bloodsconfig.getCurOper();
		var empID = userInfo.empID;
		var empName = userInfo.empName;
		params.empID = empID;
		params.empName = empName;
		params.entity.BreqStatusID = status;

		var params2 = JSON.stringify(params);
		var config = {
			type: "POST",
			url: url,
			data: params2
		};
		uxutil.server.ajax(config, function(result) {
			//if(layerIndex2 != null) layer.close(layerIndex2);
			if(layerIndex != null) layer.close(layerIndex);
			if(layerIndex != null) parent.layer.close(layerIndex);
			//清空sessionData
			layui.sessionData("BloodBreqFormSubmit", {
				key: "submitData",
				remove: true
			});
			if(result.success) {
				id = formtype == 'add' ? result.value.id : id;
				id += '';
				//确认提交后的审批提示信息
				if(params.entity.BreqStatusID == "2") {
					if(result.ErrorInfo && result.ErrorInfo.length > 0) {
						result["reviewTips"] = result.ErrorInfo;
					} else if(result.msg && result.msg.length > 0) {
						result["reviewTips"] = result.msg;
					}
				} else {
					result["reviewTips"] = "";
				}
				result["isClose"] = true; //是否关闭弹出的登记页面
				result["autoupload"] = bautoupload; //记录是否自动上传 2019-12-02 by xhz 
				//关闭当前iframe
				layer.close(iframeIndex);
				//关闭layer所有的上级iframe层
				top.layer.closeAll('iframe');
				if(callback) callback(result);
			} else {
				//console.log(result);
				var msg = result.ErrorInfo;
				if(!msg) msg = result.msg;
				layer.alert(msg, {
					title: "保存提示信息",
					btn: ['关闭'],
					icon: 5,
					end: function(index,layero) {
						result["reviewTips"] = "";
						result["isClose"] = false; 
						result["autoupload"] = bautoupload;
						if(callback) callback(result);
					}
				});

			}

		});
	};
	/**初始化表单信息*/
	breqEditForm.initForm = function() {
		var me = this;
		laydate.render({
			elem: '#BloodBReqForm_Birthday',
			type: "date"
			//,format: 'yyyy-MM-dd HH:mm'
		});
		//入院日期
		laydate.render({
			elem: '#BloodBReqForm_ToHosdate',
			type: "date"
			//,format: 'yyyy-MM-dd HH:mm'
		});
		//申请日期
		laydate.render({
			elem: '#BloodBReqForm_ApplyTime',
			type: "datetime"
			//,format: 'yyyy-MM-dd HH:mm'
		});
		//预用日期
		laydate.render({
			elem: '#BloodBReqForm_UseTime',
			type: "date"
			//,format: 'yyyy-MM-dd HH:mm'
		});
		//允许下拉选择框手工编辑
		me.initEditSelect();

		//用血申请登记时,是否隐藏医生录入项
		var doctorNoDiv = $('[lay-filter="div_filter_BloodBReqForm_DoctorNo"]');
		if(bloodsconfig.HisInterface.ISHIDEDOCTORNOOFADD == true) {
			doctorNoDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			doctorNoDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		//用血申请登记时,是否隐藏入院日期录入项
		var toHosdateDiv = $('[lay-filter="div_filter_BloodBReqForm_ToHosdate"]');
		if(bloodsconfig.HisInterface.ISHIDETOHOSDETEOFADD == true) {
			toHosdateDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			toHosdateDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}

	};
	breqEditForm.inintAllSelect = function(callback) {
		var me = this;
		var total = 6; //需要加载的字典总个数
		var curCount = 0; //当前加载完成的总数
		//申请科室选择下拉
		me.initDept(function() {
			curCount = curCount + 1;
			if(curCount == total) {
				if(callback) callback();
			}
		});
		//申请科室选择下拉
		me.initWardType(function() {
			curCount = curCount + 1;
			if(curCount == total) {
				if(callback) callback();
			}
		});
		//申请医生选择下拉
		me.initDoctor(function() {
			curCount = curCount + 1;
			if(curCount == total) {
				if(callback) callback();
			}
		});
		//初始化就诊类型下拉选择框
		me.initBloodBReqType(function() {
			curCount = curCount + 1;
			if(curCount == total) {
				if(callback) callback();
			}
		});
		//初始化申请类型下拉选择框
		me.initBloodUseType(function() {
			curCount = curCount + 1;
			if(curCount == total) {
				if(callback) callback();
			}
		});
		//医嘱申请的输血目的
		me.initUsePurpose(function() {
			curCount = curCount + 1;
			if(curCount == total) {
				if(callback) callback();
			}
		});
		//用血方式
		// me.initBloodWay(function() {
		// 	curCount = curCount + 1;
		// 	if(curCount == total) {
		// 		if(callback) callback();
		// 	}
		// });
	};
	breqEditForm.getSelectFields = function() {
		var me = this;
		var field = [];
		field.push("BloodBReqForm_Id");
		field.push("BloodBReqForm_PatNo");
		field.push("BloodBReqForm_CName");
		field.push("BloodBReqForm_Sex");

		field.push("BloodBReqForm_Birthday");
		field.push("BloodBReqForm_AgeALL");
		field.push("BloodBReqForm_PatHeight");
		field.push("BloodBReqForm_PatWeight");

		field.push("BloodBReqForm_Gravida");
		field.push("BloodBReqForm_Birth");
		field.push("BloodBReqForm_Diag");
		field.push("BloodBReqForm_AddressType");
		field.push("BloodBReqForm_BeforUse");

		field.push("BloodBReqForm_DeptNo");
		field.push("BloodBReqForm_DoctorNo");
		field.push("BloodBReqForm_BUseTimeTypeID");
		field.push("BloodBReqForm_BReqTypeID");
		field.push("BloodBReqForm_UseTypeID");

		field.push("BloodBReqForm_ToHosdate");
		field.push("BloodBReqForm_ApplyTime");
		field.push("BloodBReqForm_UseTime");
		field.push("BloodBReqForm_WristBandNo");
		field.push("BloodBReqForm_Bed");

		field.push("BloodBReqForm_Bodytpe");
		field.push("BloodBReqForm_Bpress");
		field.push("BloodBReqForm_Breathe");
		field.push("BloodBReqForm_Anesth");
		field.push("BloodBReqForm_BloodWay");

		field.push("BloodBReqForm_UsePurpose");
		field.push("BloodBReqForm_PatABO");
		field.push("BloodBReqForm_PatRh");
		field.push("BloodBReqForm_HisABOCode");
		field.push("BloodBReqForm_HisrhCode");

		field.push("BloodBReqForm_PatID");
		field.push("BloodBReqForm_HisDeptID");
		field.push("BloodBReqForm_HisDoctorId");
		field.push("BloodBReqForm_ApplyID");
		field.push("BloodBReqForm_ApplyName");

		field.push("BloodBReqForm_Visible");
		field.push("BloodBReqForm_ReqTime");
		field.push("BloodBReqForm_BreqStatusID");
		field.push("BloodBReqForm_AdmID");

		field.push("BloodBReqForm_Pulse");
		field.push("BloodBReqForm_Harm");
		field.push("BloodBReqForm_OrganTransplant");
		field.push("BloodBReqForm_MarrowTransplantation");
		field.push("BloodBReqForm_WardNo");
		field.push("BloodBReqForm_HisWardNo");
		field.push("BloodBReqForm_BLPreEvaluation");

		//field.push("name:BloodBReqForm_CheckCompleteFlag");
		me.config.selectFields = field.join(",");
		return me.config.selectFields;
	};
	//允许下拉选择框手工编辑
	breqEditForm.initEditSelect = function() {
		var me = this;
		//临床诊断
		$('#BloodBReqForm_Diag').editableSelect();
		//输血目的
		//$('#BloodBReqForm_UsePurpose').editableSelect();

		//患者ABO
		//$('#BloodBReqForm_PatABO').editableSelect();
		//患者Rh
		//$('#BloodBReqForm_PatRh').editableSelect();
		//申请ABO
		//$('#BloodBReqForm_HisABOCode').editableSelect();	
		//申请Rh(D)
		//$('#BloodBReqForm_HisrhCode').editableSelect();		
		//隐藏生成无用的div下拉选择框class="layui-unselect layui-form-select"->class="layui-input layui-unselect"
		$('div[class="layui-unselect layui-form-select"]').hide();
	};
	breqEditForm.getOptionList = function(arr) {
		var html = "<option value=''></option>";
		for(var i = 0; i < arr.length; i++) {
			html += '<option value="' + arr[i] + '">' + arr[i] + '</option>';
		}
		return html;
	};
	breqEditForm.insertSelectRender = function(result) {
		var me = this;
		//患者ABO
		var listPatABO = ['A', 'B', 'AB', 'O', '未知', '疑难'];
		var patABO = result["BloodBReqForm_PatABO"];
		//当前的结果项不存在患者ABO时,自动追加
		if(patABO && result && listPatABO.indexOf(patABO) < 0) {
			listPatABO.push(patABO);
			var html = me.getOptionList(listPatABO);
			if(html && html.length > 0) $('#BloodBReqForm_PatABO').empty().append(html);
		}
		//患者ABO在可编辑选择时,允许自动追加
		if(bloodsconfig.HisInterface.ISALLOWPATABOANDRHOPT == true) {
			insertSelect.render({
				elem: '#BloodBReqForm_PatABO',
				data: listPatABO
			});
		}

		//患者Rh
		var listPatRh = ['阴性', '阳性'];
		var patRh = result["BloodBReqForm_PatRh"];
		//当前的结果项不存在患者Rh时,自动追加
		if(patRh && result && listPatRh.indexOf(patRh) < 0) {
			listPatRh.push(patRh);
			var html = me.getOptionList(listPatRh);
			if(html && html.length > 0) $('#BloodBReqForm_PatRh').empty().append(html);
		}
		//患者Rh在可编辑选择时,允许自动追加
		if(bloodsconfig.HisInterface.ISALLOWPATABOANDRHOPT == true) {
			insertSelect.render({
				elem: '#BloodBReqForm_PatRh',
				data: listPatRh
			});
		}
		//申请ABO
		insertSelect.render({
			elem: '#BloodBReqForm_HisABOCode',
			data: ['A', 'B', 'AB', 'O', '未知', '疑难']
		});
		//申请Rh(D)
		insertSelect.render({
			elem: '#BloodBReqForm_HisrhCode',
			data: ['阴性', '阳性']
		});
		form.render('select');
	};
	breqEditForm.consoleSelectVal = function() {
		var me = this;
		console.log("BloodBReqForm_DeptNo:" + $('[lay-filter="BloodBReqForm_DeptNo"]').val());
		console.log("BloodBReqForm_DoctorNo:" + $('[lay-filter="BloodBReqForm_DoctorNo"]').val());
		console.log("BloodBReqForm_BReqTypeID:" + $('[lay-filter="BloodBReqForm_BReqTypeID"]').val());
		console.log("BloodBReqForm_UseTypeID:" + $('[lay-filter="BloodBReqForm_UseTypeID"]').val());
	};
	/**初始化部门下拉选择框*/
	breqEditForm.initDept = function(callback) {
		var me = this;
		bloodSelectData.dictList.Department(function(html) {
			var filter = "BloodBReqForm_DeptNo";
			$('[lay-filter="' + filter + '"]').empty().append(html);
			if(me.defaultValues && me.defaultValues["BloodBReqForm_DeptNo"]) {
				setTimeout(function() {
					$('[lay-filter="' + filter + '"]').val(me.defaultValues["BloodBReqForm_DeptNo"]);
				}, 300);
			}
			if(callback) callback();
		});
	};
	/**初始化病区下拉选择框*/
	breqEditForm.initWardType = function(callback) {
		var me = this;
		bloodSelectData.dictList.WardType(function(html) {
			var filter = "BloodBReqForm_WardNo";
			$('[lay-filter="' + filter + '"]').empty().append(html);
			if(me.defaultValues && me.defaultValues["BloodBReqForm_WardNo"]) {
				setTimeout(function() {
					$('[lay-filter="' + filter + '"]').val(me.defaultValues["BloodBReqForm_WardNo"]);
				}, 300);
			}
			if(callback) callback();
		});
	};
	/**初始化医生下拉选择框*/
	breqEditForm.initDoctor = function(callback) {
		var me = this;
		bloodSelectData.dictList.Doctor(function(html) {
			var filter = "BloodBReqForm_DoctorNo";
			$('[lay-filter="' + filter + '"]').empty().append(html);
			if(me.defaultValues && me.defaultValues["BloodBReqForm_DoctorNo"]) {
				setTimeout(function() {
					$('[lay-filter="' + filter + '"]').val(me.defaultValues["BloodBReqForm_DoctorNo"]);
				}, 300);
			}
			if(callback) callback();
		});
	};
	/**初始化就诊类型下拉选择框*/
	breqEditForm.initBloodBReqType = function(callback) {
		var me = this;
		bloodSelectData.dictList.BloodBReqType(function(html) {
			$('[lay-filter="BloodBReqForm_BReqTypeID"]').empty().append(html);
			if(me.defaultValues && me.defaultValues["BloodBReqForm_BReqTypeID"]) {
				setTimeout(function() {
					$('[lay-filter="BloodBReqForm_BReqTypeID"]').val(me.defaultValues["BloodBReqForm_BReqTypeID"]);
				}, 300);
			}
			if(callback) callback();
		});
	};
	/**初始化申请类型下拉选择框*/
	breqEditForm.initBloodUseType = function(callback) {
		bloodSelectData.dictList.BloodUseType(function(html) {
			$('[lay-filter="BloodBReqForm_UseTypeID"]').empty().append(html);
			if(callback) callback();
		});
	};
	/**初始化输血目的下拉选择框*/
	breqEditForm.initUsePurpose = function(callback) {
		bloodSelectData.dictList.UsePurpose(function(html) {
			if(!html) {
				html =
					'<option value=""></option><option value="增加携氧能力">增加携氧能力</option><option value="增加凝血因子">增加凝血因子</option>';
			}
			$('[lay-filter="BloodBReqForm_UsePurpose"]').empty().append(html);
			if(callback) callback();
		});
	};
	/**初始化用血方式的下拉选择框*/
	breqEditForm.initBloodWay = function(callback) {
		bloodSelectData.dictList.BloodWay(function(html) {
			if(!html) {
				html = '<option value=""></option><option value="1">异型输血</option><option value="2">自体输血</option>';
			}
			$('[lay-filter="BloodBReqForm_BloodWay"]').empty().append(html);
			if(callback) callback();
		});
	};
	//核心入口
	breqEditForm.render = function(options) {
		var me = this;
		var inst = new Class(options);
		inst.initForm();
		return inst;
	};

	//暴露接口
	exports('breqEditForm', breqEditForm);
});