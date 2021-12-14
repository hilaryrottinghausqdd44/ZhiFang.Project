/**
	@name：医嘱申请
	@author：longfc
	@version 2019-06-21
 */
layui.extend({
	uxutil: 'ux/util',
	uxdata: 'ux/data',
	dataadapter: 'ux/dataadapter',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig',
	runParams: '/config/runParams',
	csserver: '/views/interface/csserver',
	formSelects: '/ux/other/formselects/dist/formSelects-v4.min',
	bloodSelectData: '/views/modules/bloodtransfusion/bloodSelectData',
	breqEditForm: '/views/bloodtransfusion/doctorstation/edit/breqEditForm',
	breqItemEditTable: '/views/bloodtransfusion/doctorstation/edit/breqItemEditTable',
	breqFormResultEditTable: '/views/bloodtransfusion/doctorstation/edit/breqFormResultEditTable'
}).use(['util', 'layer', 'laydate', 'table', 'form', 'dataadapter',"cachedata","runParams", 'bloodsconfig', 'csserver', "breqEditForm","formSelects", 'breqItemEditTable', 'breqFormResultEditTable', 'uxdata',  'bloodSelectData', 'laydate'
], function() {
	"use strict";

	//Jcall 20191205 #start#
	//按钮是否可点击
	var BUTTON_CAN_CLICK = true;
	//Jcall 20191205 #end#

	var $ = layui.jquery;
	var util = layui.util;
	var uxutil = layui.uxutil;
	var csserver = layui.csserver;
	var form = layui.form;
	var laydate = layui.laydate;

	var bloodSelectData = layui.bloodSelectData;
	var formSelects = layui.formSelects;
	var breqEditForm = layui.breqEditForm;
	var table = layui.table;
	var breqItemEditTable = layui.breqItemEditTable;
	var breqFormResultEditTable = layui.breqFormResultEditTable;
	
	var cachedata = layui.cachedata;
	var runParams = layui.runParams;
	var bloodsconfig = layui.bloodsconfig;
	
	var height = $(document).height();
	//HIS调用时,可以查看到获取的原始病人信息
	var hisPatInfo = null;
	//新增医嘱申请时的默认值
	var addDefaultVaule = {};

	/**默认传入参数*/
	var defaultParams = {
		HisDeptId: "", //HIS医嘱申请科室Id		
		DeptId: "", //医嘱申请科室Id
		HisDoctorId: "", //HIS医嘱申请医生Id
		DoctorId: "", //医嘱申请医生Id
		AdmId: "", //His的就诊号
		HisPatId: "", //HIS医嘱申请患者Id		
		PatNo: "", //医嘱申请患者住院号
		CName: "", //患者姓名		
		ReqFormNo: "", //申请单号
		Formtype: "", //编辑类型
	};
	//初始化默认传入参数信息
	function initDefaultParams() {
		//先重置表单"保存"按钮的处理结果值
		var result2 = {
			"reviewTips": "",
			"id": "",
			"autoupload": false,
			"success": ""
		};
		cachedata.setCache("breqEditFormSave", result2);
		//接收传入参数
		var params = uxutil.params.get();
		//科室Id
		if (params["hisDeptId"]) defaultParams.HisDeptId = params["hisDeptId"];
		//HIS医嘱申请科室Id
		if (params["deptId"]) defaultParams.DeptId = params["deptId"];
		//医生Id
		if (params["hisDoctorId"]) defaultParams.HisDoctorId = params["hisDoctorId"];
		//医生Id
		if (params["doctorId"]) defaultParams.DoctorId = params["doctorId"];
		//His的就诊号
		if (params["admId"]) defaultParams.AdmId = params["admId"];
		//患者Id
		if (params["hisPatId"]) defaultParams.HisPatId = params["hisPatId"];
		//患者住院号
		if (params["patNo"]) defaultParams.PatNo = params["patNo"];
		//患者姓名
		if (params["cName"]) defaultParams.CName = params["cName"];
		//医嘱申请单号
		if (params["reqFormNo"]) defaultParams.ReqFormNo = params["reqFormNo"];
		//医嘱申请单号
		if (params["formtype"]) defaultParams.Formtype = params["formtype"];
	};

	var breqEditForm1 = null;
	//申请明细列表配置信息
	function getbreqEditFormConfig() {
		return {
			title: '申请信息',
			elem: '#LAY-app-form-BloodBreqForm',
			id: "LAY-app-form-BloodBreqForm",
			formfilter: "LAY-app-form-BloodBreqForm",
			formtype: defaultParams.Formtype,
			defaultParams: defaultParams,
			PK: defaultParams.ReqFormNo || ""
		};
	};
	/**初始化表单信息*/
	function initReqForm() {
		var config = getbreqEditFormConfig();
		breqEditForm1 = breqEditForm.render(config);
		onbreqEditForm();
	};
	//申请表单监听
	function onbreqEditForm() {
		//紧急用血标志勾选上时,患者AB才能编辑及选择
		layui.form.on('checkbox(BloodBReqForm_BUseTimeTypeID)', function(data) {
			breqEditForm1.setPatABOOfBUseTimeTypeID(data.elem.checked);
		});
		//监听申请类型选择事件,判断申请类型是否为 紧急用血和普通用血 2020-10-14 by Xiehz modify
		layui.form.on('select(BloodBReqForm_UseTypeID)', function(data) {
			breqEditForm1.setReqFormBUseTimeTypeID(data.elem);
		});		
		//表单提交事件监听
		layui.form.on('submit(LAY-app-bloodBreqFormm-submit)', function(formData) {
			//Jcall 20191205 #start#
			if (!BUTTON_CAN_CLICK) return;
			//Jcall 20191205 #end#

			//先验证血制品明细输入值是否正确
			var result2 = breqItemEditTable1.onSaveVerification(breqItemEditTable1);
			if (result2.success == false) {
				layer.msg(result2.msg);
				return false;
			}

			var breqItemSubmit = breqItemEditTable1.getBreqItemSubmit(breqItemEditTable1);
			var resultTableSubmit = breqFormResultEditTable1.getBreqItemSubmit(breqFormResultEditTable1);
			if (breqItemSubmit.addBreqItemList.length <= 0 && breqItemSubmit.editBreqItemList.length <= 0) {
				//layer.msg("血制品申请信息为空!");
				layer.alert('血制品申请信息为空!', {
					title: "提交提示信息",
					btn: ['关闭'],
					icon: 5
				});
				return false;
			}
			var pk = defaultParams.ReqFormNo || "";
			var submitData = {
				"PK": pk,
				"formtype": pk.length > 0 ? "edit" : "add",
				"formData": formData,
				"breqItemSubmit": breqItemSubmit,
				"resultTableSubmit": resultTableSubmit
			};
			//Jcall 20191205 #start#
			BUTTON_CAN_CLICK = false; //所有按钮置为不可点击状态
			//Jcall 20191205 #end#

			var layerIndex = parent.layer.msg('提交保存中,请耐心等待...', {
				time: 0,
				icon: 16,
				shade: 0.6
			});
			submitData.layerIndex = layerIndex;
			//表单保存处理
			breqEditForm1.onSave(breqEditForm1, submitData, function(result) {
				//Jcall 20191205 #start#
				BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
				//if (button) button.removeClass(className);
				//退回保存提交处理,但不关闭
				if (result.success == false && result.isClose == false) {
					return false;
				}
				//Jcall 20191205 #end#
				var result2 = {
					"reviewTips": "",
					"id": "",
					"autoupload": false,
					"success": ""
				};
				//医嘱申请保存结果存储到父窗体里 2019-12-02 by xhz
				result2["entity"] =result["entity"];//提交的实体信息
				result2["id"] = result.value.id; //返回ID
				result2["success"] = result.success; //返回是否成功失败状态
				result2["autoupload"] = result["autoupload"]; //是否需要自动上传
				result2["reviewTips"] = result.reviewTips; //确认提交后的审批提示信息
				cachedata.setCache("breqEditFormSave", result2);
			});
		});
	};
	//按患者就诊号或病历号调用CS服务获取HIS病人信息
	function onGetPatInfo(callback) {
		var sPatNo = ""; //患者就诊号或病历号
		if (defaultParams.AdmId) sPatNo = defaultParams.AdmId || "";

		if (!sPatNo) sPatNo = defaultParams.PatNo || "";
		if (!sPatNo) return;

		var url = bloodsconfig.getHisPatInfoUrl()  + "?sPatNo=" + sPatNo;
		url += '&t=' + new Date().getTime();
		var layerIndex = parent.layer.msg('患者信息处理中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		csserver.ajax({
			url: url
		}, function(data) {
			if (layerIndex != null) parent.layer.close(layerIndex);
			var result = "";
			var objResult = {};
			if (data && data.result && data.result.length > 0) result = JSON.parse(data.result[0]);
			if (result && result.data) result = result.data;

			if (result && result.length > 0) { // && $.isPlainObject(result)
				//hisPatInfo = JSON.parse(data.result[0]);
				//console.log(hisPatInfo);
				objResult = setFormVal(result[0]);
				//查看患者原始信息，如果不存在患者ABO时，显示按钮
				showPatInfoAlert(result);
			} else {
				layer.alert('获取患者信息失败!' + data.msg, {
					btn: ['关闭'],
					time: 0
				});
			}
			if (callback) callback(objResult);
		});
	};
	/**
	 * 如果非抢救用血，并且患者ABO或患者Rh(D)值为空时提示
	 * @param {Object} result 患者原始信息
	 */
	function showPatInfoAlert(result) {
		var bUseTimeTypeID = result[0]["BUseTimeTypeID"]; //是否抢救用血
		var hisABOCode = result[0]["hisABOCode"];
		var hisRhCode = result[0]["hisRhCode"];
		if (bUseTimeTypeID) {
			if (bUseTimeTypeID == "1" || bUseTimeTypeID == "是" || bUseTimeTypeID == "true") {
				bUseTimeTypeID = true;
			} else {
				bUseTimeTypeID = false;
			}
		} else {
			bUseTimeTypeID = false;
		}
		//如果非抢救用血
		if (bUseTimeTypeID == false && (!hisABOCode || !hisRhCode)) {
			var info = "";
			if (!hisABOCode) {
				info = "提取患者ABO结果为空！<br>";
			}
			if (!hisRhCode) {
				info = "提取患者Rh(D)结果为空！<br>";
			}
			parent.layer.alert(info, {
				title: '非抢救用血申请提示',
				btn: ['关闭'],
				icon: 5
			});
			$("#LAY-app-form-div-searchPatInfo").removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
	}
	//HIS调用时,可以查看到获取的原始病人信息
	function showHisPatInfo() {
		var me = this;
		var arr = "";
		if (hisPatInfo) {
			var str = JSON.stringify(hisPatInfo);
			layer.open({
				type: 1,
				offset: "auto",
				area: ['98%', '52%'],
				content: str,
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
		} else {
			layer.msg('读取HIS的病人信息为空!');
		}
	};
	//新增时给病人表单项赋值
	function setFormVal(result) {
		var isRender = false;
		var obj = {};
		layui.each(result, function(key, value) {
			//先判断表单录入项是否存在对照
			var mapkey = ("" + key).toLowerCase();
			var name = bloodsconfig.PatInfoMap[mapkey];
			if (!name) return;
			obj[name] = value;
		});
		return obj;
	};

	var breqItemEditTable1 = null;
	//申请明细列表配置信息
	function getbreqItemEditTableConfig() {
		return {
			title: '申请明细信息',
			height: 180,
			elem: '#LAY-app-table-BloodBreqItem',
			id: "LAY-app-table-BloodBreqItem",
			toolbar: "",
			defaultParams: defaultParams,
			ReqFormNo: defaultParams.ReqFormNo || "",
			defaultToolbar: null,
			externalWhere: ""
		};
	};
	var breqFormResultEditTable1 = null;
	//检验结果信息列表配置信息
	function getbreqFormResultEditTableConfig() {
		//180为申请明细信息列表高度;其他工具栏高度:30*2;输血前项目表单项默认预留行高:38*2
		var height1 = height - 180 - 38 * 2 - 30 * 2; // -(30+25)*2
		return {
			title: '检验结果信息',
			height: height1,
			elem: '#LAY-app-table-BloodBReqFormResult',
			id: "LAY-app-table-BloodBReqFormResult",
			toolbar: "",
			defaultParams: defaultParams,
			ReqFormNo: defaultParams.ReqFormNo || "",
			defaultToolbar: null,
			externalWhere: "",
			parseDataAfter: function(result) {
				parseDataAfter(result);
			}
		};
	};
	//检验结果信息数据加载完成后处理
	function parseDataAfter(result) {
		var items = [];
		if (result && result.data && result.data.length > 0) {
			//表单项显示名称模板style="color:#FF5722;font-weight:bold" style="color:#FF5722"
			var FORM_LABEL = '<div class="layui-inline"><label class="layui-form-label" >{LormLabelValue}</label>';
			//表单项显示组件模板#FF5722; style="width:120px;"
			var FORM_INPUT =
				'<div class="layui-input-inline"><input type="text" value="{InputValue} {ItemUnit}" readonly="readonly" autocomplete="off" placeholder="" class="layui-input"></div>';
			//FORM_INPUT=FORM_INPUT+' <div class="layui-form-mid layui-word-aux">{ItemUnit}</div>';	
			FORM_INPUT = FORM_INPUT + '</div>';
			for (var i = 0; i < result.data.length; i++) {
				var isPreEvaluationItem = "" + result.data[i]["BloodBReqFormResult_IsPreTrransfusionEvaluationItem"];
				//if(i<=5)isPreEvaluationItem="1";
				if (isPreEvaluationItem == "1" || isPreEvaluationItem == "true") {
					var itemCName = result.data[i]["BloodBReqFormResult_BTestItemCName"];
					var itemResult = result.data[i]["BloodBReqFormResult_ItemResult"];
					var itemUnit = result.data[i]["BloodBReqFormResult_ItemUnit"];
					var formlabel = FORM_LABEL.replace(/\{LormLabelValue\}/g, itemCName);
					var forminput = FORM_INPUT.replace(/\{InputValue\}/g, itemResult);
					forminput = forminput.replace(/\{ItemUnit\}/g, itemUnit);

					items.push(formlabel);
					items.push(forminput);
				}
			}
		}
		var form_item = $('[lay-filter="form_item_IsPreTrransfusionEvaluationItem"]');
		form_item.html("").val("");
		if (items.length > 0) {
			var html = items.join('');
			form_item.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
			form_item.html(html).show(); //		
		} else {
			form_item.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
			form_item.hide();
		}
	};
	//初始化或刷新血制品列表
	function initBreqItemEditTable() {
		breqItemEditTable1 = breqItemEditTable.render(getbreqItemEditTableConfig());
		onBreqItemEditTable();
	};
	//血制品列表监听
	function onBreqItemEditTable() {
		//监听复选框选择
		table.on('checkbox(LAY-app-table-BloodBreqItem)', function(obj) {

		});
		//监听单元格编辑(不监听时,table对应的缓存申请量不生效)
		table.on('edit(LAY-app-table-BloodBreqItem)', function(obj) {
			//同步更新缓存对应的值
			obj.update({
				"BloodBreqItem_BReqCount": obj.data["BloodBreqItem_BReqCount"]
			});
		});
		//监听工具条
		table.on('tool(LAY-app-table-BloodBreqItem)', function(obj) {
			var data = obj.data;
			if (obj.event === 'del') {
				breqItemEditTable1.delete(obj);
			}
		});
	};
	//血制品列表事件监听
	function onBreqItemEditTableBtns() {
		var active = {
			refreshBloodstyle: function() {
				onRefreshBreqItemEditTable();
			},
			addChooseBloodstyle: function() {
				breqItemEditTable1.openChooseWin("", function(dataTable) {
					if (dataTable && dataTable.length > 0) {
						var tableIns = breqItemEditTable1.tableIns;
						var config = {
							url: "",
							data: dataTable
						};
						table.reload(tableIns.config.id, config);
					}
				});
			},
			searchCurQty: function() {
				onSearchCurQty();
			}
		};
		$('.layui-form .layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
	};
	//LIS结果列表初始化
	function initbreqFormResultEditTable() {
		breqFormResultEditTable1 = breqFormResultEditTable.render(getbreqFormResultEditTableConfig());
	};
	//LIS结果列表事件监听
	function onbreqFormResultEditTableBtns() {
		//按钮事件
		var active = {
			//重新获取
			getLis: function() {

			},
			refreshLis: function() {
				onRefreshbreqFormResultEditTable();
			}
		};
		$('.layui-form .layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
	};

	//刷新申请明细列表
	function onRefreshBreqItemEditTable() {
		//获取查询条件
		if (!defaultParams.ReqFormNo) return;

		breqItemEditTable1.config.defaultParams = defaultParams;
		breqItemEditTable1.setReqFormNo(defaultParams.ReqFormNo);
		breqItemEditTable1.getSearchWhere();
		table.reload(breqItemEditTable1.tableIns.config.id, breqItemEditTable1.config);
		onBreqItemEditTable();
	};
	//刷新检验结果列表
	function onRefreshbreqFormResultEditTable() {
		//获取查询条件
		//if(!defaultParams.ReqFormNo) return;
		breqFormResultEditTable1.setReqFormNo(defaultParams.ReqFormNo);
		breqFormResultEditTable1.config.defaultParams = defaultParams;
		breqFormResultEditTable1.getSearchWhere();
		table.reload(breqFormResultEditTable1.tableIns.config.id, breqFormResultEditTable1.config);
	};

	function loadData() {
		formLoad();
		onRefreshBreqItemEditTable();
		onRefreshbreqFormResultEditTable();
	};

	function formLoad() {
		var formtype = defaultParams.Formtype || "add";
		if (formtype == "add") {
			//传入的病历号参数值
			var patNo1 = defaultParams.PatNo;
			var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);
			//医嘱申请表单默认值处理
			addDefaultVaule["BloodBReqForm_BUseTimeTypeID"] = false; //0:否;1:是; 
			if (defaultParams.AdmId) addDefaultVaule["BloodBReqForm_AdmID"] = defaultParams.AdmId;
			if (defaultParams.HisPatId) addDefaultVaule["BloodBReqForm_PatID"] = defaultParams.HisPatId;
			if (defaultParams.PatNo) addDefaultVaule["BloodBReqForm_PatNo"] = defaultParams.PatNo;
			if (defaultParams.CName) addDefaultVaule["BloodBReqForm_CName"] = defaultParams.CName;
			if (defaultParams.HisDeptId) {
				addDefaultVaule["BloodBReqForm_HisDeptID"] = defaultParams.HisDeptId;
			}
			if (sysCurUserInfo && sysCurUserInfo.DeptId) {
				addDefaultVaule["BloodBReqForm_DeptNo"] = sysCurUserInfo.DeptId;
			}
			if (defaultParams.HisDoctorId) {
				addDefaultVaule["BloodBReqForm_HisDoctorId"] = defaultParams.HisDoctorId;
			}
			if (sysCurUserInfo && sysCurUserInfo.DoctorId) {
				addDefaultVaule["BloodBReqForm_ApplyID"] = defaultParams.DoctorId;
				addDefaultVaule["BloodBReqForm_DoctorNo"] = sysCurUserInfo.DoctorId;
			}
			var curdate = util.toDateString("", "yyyy-MM-dd HH:mm:ss"); //yyyy-MM-dd HH:mm:ss
			addDefaultVaule["BloodBReqForm_ApplyTime"] = curdate;

			//新增时,依传入的患者病历号+姓名调用公共服务获取病人信息
			onGetPatInfo(function(obj) {
				//装换急诊用血数据类型
				if (obj["BloodBReqForm_BUseTimeTypeID"] == '1' || obj["BloodBReqForm_BUseTimeTypeID"] == 'true') {
					obj["BloodBReqForm_BUseTimeTypeID"] = true;
					addDefaultVaule["BloodBReqForm_BUseTimeTypeID"] = true; //0:否;1:是; 
				}
				//重新修正病历号赋值,因为传入的是就诊号
				breqFormResultEditTable1.config.PatNo = obj["BloodBReqForm_PatNo"];
				defaultParams.PatNo = obj["BloodBReqForm_PatNo"];
				addDefaultVaule["BloodBReqForm_PatNo"] = obj["BloodBReqForm_PatNo"];
				var vals = $.extend({}, obj, addDefaultVaule);
				breqEditForm1.defaultValues = vals;
				breqEditForm1.setValues(vals);
				//HisPatId值处理
				if (!defaultParams.HisPatId) {
					defaultParams.HisPatId = obj["BloodBReqForm_PatID"];
				}
				//可能HIS调用时,只传就诊号没传病历号,需要重新设置病历号参数值,重新获取患者的医嘱项目结果信息
				if (patNo1 != obj["BloodBReqForm_PatNo"])
					onRefreshbreqFormResultEditTable();
			});
		} else {
			if (defaultParams.ReqFormNo) {
				//编辑表单加载
				breqEditForm1.load(defaultParams.ReqFormNo, function(data) {});
			}
		}
	};
	//查看当前库存
	function onSearchCurQty() {
		var params = [];
		params.push("t=" + new Date().getTime());
		layer.open({
			type: 2,
			title: "库存信息",
			area: ['90%', '92%'],
			content: '../stock/index.html?' + params.join("&"),
			id: "table-SearchCurQty",
			btn: null,
			end: function() {},
			cancel: function() {}
		});
	};
	//初始化
	function initTable() {
		//申请明细列表初始化
		initBreqItemEditTable();
		//LIS结果列表初始化
		initbreqFormResultEditTable();
	};
	//初始化
	function initAll() {
		initDefaultParams();		
		initTable();
		initReqForm();
		onBreqItemEditTableBtns();
		onbreqFormResultEditTableBtns();
		//加载数据
		loadData();
		//查看病人原始信息
		$("#LAY-app-form-btn-searchPatInfo").on("click", function(event) {
			showHisPatInfo();
		});
		//初始化系统运行参数
		initRunParams();
	};
	//初始化系统运行参数
	function initRunParams() {
		runParams.initRunParams(function() {
			bloodsconfig=runParams.renderBloodsconfig(bloodsconfig);
			layui.bloodsconfig=bloodsconfig;
		});
	};
	initAll();

});
