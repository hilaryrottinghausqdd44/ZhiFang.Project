/**
	@name：医嘱申请+(包括医嘱申请,上级审核,科主任审核)
	@author：longfc
	@version 2019-08-21
 */
layui.extend({
	uxutil: 'ux/util',
	uxdata: "ux/data",
	dataadapter: 'ux/dataadapter',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig',
	csserver: '/views/interface/csserver',
	clodopprint: '/ux/other/lodop/clodopprint',
	formSelects: '/ux/other/formselects/dist/formSelects-v4.min',
	bloodSelectData: '/views/modules/bloodtransfusion/bloodSelectData',
	bloodBreqSearchForm: '/views/bloodtransfusion/doctorstation/basic/bloodBreqSearchForm',
	breqFormApplyReviewTable: '/views/bloodtransfusion/doctorstation/applyandreview/breqFormApplyReviewTable',
	breqItemTable: '/views/bloodtransfusion/doctorstation/basic/breqItemTable',
	breqFormResultTable: '/views/bloodtransfusion/doctorstation/basic/breqFormResultTable',
	bloodClassDict: '/views/modules/bloodtransfusion/bloodClassDict',
	runParams: '/config/runParams'
}).use(['uxutil', 'layer', 'form', 'table', 'dataadapter', "cachedata", 'bloodsconfig', 'csserver',
	'bloodBreqSearchForm', 'breqFormApplyReviewTable', 'breqItemTable', 'breqFormResultTable', 'uxdata',
	'bloodSelectData', 'clodopprint', 'formSelects', "bloodClassDict", "runParams"
], function() {
	"use strict";

	//Jcall 20191205 #start#
	//按钮是否可点击
	var BUTTON_CAN_CLICK = true;
	//Jcall 20191205 #end#

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var uxdata = layui.uxdata;
	var layer = layui.layer;
	var form = layui.form;
	var table = layui.table;
	var csserver = layui.csserver;
	var clodopprint = layui.clodopprint;
	var bloodSelectData = layui.bloodSelectData;
	var bloodBreqSearchForm = layui.bloodBreqSearchForm;
	var breqFormTable = layui.breqFormApplyReviewTable;
	var breqItemTable = layui.breqItemTable;
	var breqFormResultTable = layui.breqFormResultTable;
	var formSelects = layui.formSelects;
	var bloodClassDict = layui.bloodClassDict;

	var cachedata = layui.cachedata;
	var runParams = layui.runParams;
	var bloodsconfig = layui.bloodsconfig;

	var height = $(document).height();
	//88为查询表单高度（140）
	var reqheight = (height - 88 - 50) / 2;

	//HIS传入的原始参数信息
	var hisParams = bloodsconfig.getData(bloodsconfig.cachekeys.HISPARAMS_KEY);
	//当前操作的医生信息
	var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);
	//用血说明信息
	var bloodUseDesc = "";
	//正在上传HIS中的申请单号数组
	var toHisArr = [];

	//开单医生申请等级用血量阀值范围
	var applyrGrade = {
		LowLimit: 800, //小于800不能开单
		UpperLimit: 10000000
	};
	//上级审核等级用血量阀值范围:(0<seniorGrade<800)
	var seniorGrade = {
		LowLimit: 0,
		UpperLimit: 800
	};
	//科主任审核等级用血量阀值范围:(800<=seniorGrade<1600)
	var directorGrade = {
		LowLimit: 800,
		UpperLimit: 1600
	};
	/**默认传入参数*/
	var defaultParams = {
		HisDeptId: "", //HIS医嘱申请科室Id		
		DeptId: "", //医嘱申请科室Id
		HisDoctorId: "", //HIS医嘱申请医生Id
		DoctorId: "", //医嘱申请医生Id
		HisPatId: "", //HIS医嘱申请患者Id
		AdmId: "", //His的就诊号
		PatNo: "", //医嘱申请患者住院号
		CName: "" //患者姓名
	};
	//申请主单列表当前行选择信息
	var curRowInfo = {
		HisDeptId: "", //HIS医嘱申请科室Id		
		DeptId: "", //医嘱申请科室Id
		HisDoctorId: "", //HIS医嘱申请医生Id
		DoctorId: "", //医嘱申请医生Id
		HisPatId: "", //HIS医嘱申请患者Id	
		AdmId: "", //His的就诊号
		PatNo: "", //医嘱申请患者住院号
		CName: "" //患者姓名
	};
	/**
	 * @description 初始化默认传入参数信息
	 */
	function initDefaultParams() {
		//先从接收传入参数获取默认信息,如果没有,就从登录缓存信息取
		var params = uxutil.params.get();
		//HIS医嘱申请科室Id
		if (params["hisDeptId"]) defaultParams.HisDeptId = params["hisDeptId"];
		//HIS医嘱申请科室Id
		if (params["deptId"]) defaultParams.DeptId = params["deptId"];
		//His的就诊号
		if (params["admId"]) defaultParams.AdmId = params["admId"];
		//患者Id
		if (params["hisPatId"]) defaultParams.HisPatId = params["hisPatId"];
		//患者病历号
		if (params["patNo"]) defaultParams.PatNo = params["patNo"];
		//患者姓名
		if (params["cname"]) defaultParams.CName = params["cname"];
		//医生信息
		if (params["hisDoctorId"]) defaultParams.HisDoctorId = params["hisDoctorId"];
		if (params["doctorId"]) defaultParams.DoctorId = params["doctorId"];
		//以当前操作的医生信息为准
		if (sysCurUserInfo) {
			if (sysCurUserInfo && sysCurUserInfo.HisDoctorId) defaultParams.HisDoctorId = sysCurUserInfo.HisDoctorId;
			if (sysCurUserInfo && sysCurUserInfo.DoctorId) defaultParams.DoctorId = sysCurUserInfo.DoctorId;
			if (!defaultParams.HisDeptId && sysCurUserInfo.HisDeptId) defaultParams.HisDeptId = sysCurUserInfo.HisDeptId;
			if (!defaultParams.DeptId && sysCurUserInfo.DeptId) defaultParams.DeptId = sysCurUserInfo.DeptId;
		}
		curRowInfo = JSON.parse(JSON.stringify(defaultParams));
	};
	/**
	 * @description 获取用血说明信息
	 * @param {Object} callback
	 */
	function getBloodUseDesc(callback) {
		if (bloodUseDesc && callback) callback(bloodUseDesc);

		var url = uxutil.path.ROOT +
			"/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDescById?isPlanish=true&id=100";
		url = url + "&fields=BloodUseDesc_Id,BloodUseDesc_Contents";
		url += '&t=' + new Date().getTime();
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if (data.success && data.value) {
				bloodUseDesc = data.value["BloodUseDesc_Contents"];
				if (callback) callback(bloodUseDesc);
			} else {
				var bloodUseDesc1 = "系统当前没有维护用血说明信息,请维护后再查阅!";
				if (callback) callback(bloodUseDesc1);
			}
		});
	};
	/**
	 * @description 初始化表单信息
	 */
	function initForm() {
		//查询条件默认值
		var patNo = curRowInfo.AdmId; //先取就诊号
		if (!patNo) patNo = curRowInfo.PatNo; //就诊号没有值,再取住院号
		if (patNo) $("#LAY-app-table-BloodBreqForm-Search-LikeSearch").val(patNo);
		bloodBreqSearchForm.initForm(function(type, value) {
			if (type == "date") {
				breqFormTable1.setRangeDateValue(value);
				onRefreshBreqFormTable();
			}
		});
		setHideSearchItems();
	};
	/**
	 *@description 按系统配置项设置用血申请列表查询项的显示/隐藏
	 */
	function setHideSearchItems() {
		//是否显示第二行查询
		var isHideSearchItem2 = true;
		//用血申请+,是否隐藏就诊类型查询项
		var bloodBReqTypeDiv = $('[lay-filter="div_filter_BloodBReqForm_BloodBReqType"]');
		if (bloodsconfig.HisInterface.ISHIDEBReqType == true) {
			bloodBReqTypeDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			isHideSearchItem2 = false;
			bloodBReqTypeDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		//用血申请+,是否隐藏申请类型查询项
		var bloodUseTypeDiv = $('[lay-filter="div_filter_BloodBReqForm_BloodUseType"]');
		if (bloodsconfig.HisInterface.ISHIDEBloodUseType == true) {
			bloodUseTypeDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			isHideSearchItem2 = false;
			bloodUseTypeDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		//用血申请+,是否隐藏模糊查询项
		var likeSearchDiv = $('[lay-filter="div_filter_BloodBReqForm_LikeSearch"]');
		if (bloodsconfig.HisInterface.ISHIDELikeSearch == true) {
			likeSearchDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			isHideSearchItem2 = false;
			likeSearchDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}

		//用血申请+,是否隐藏科室查询项
		var deptDiv = $('[lay-filter="div_filter_BloodBReqForm_Dept"]');
		if (bloodsconfig.HisInterface.ISHIDEPTNOOFSEARCH == true) {
			deptDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			isHideSearchItem2 = false;
			deptDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		//用血申请+,是否隐藏医生查询项
		var doctorDiv = $('[lay-filter="div_filter_BloodBReqForm_Doctor"]');
		if (bloodsconfig.HisInterface.ISHIDEDOCTORNOOFSEARCH == true) {
			doctorDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			isHideSearchItem2 = false;
			doctorDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		var searchItem2 = $('[lay-filter="div_filter_BloodBReqForm_SearchItem2"]');
		if (isHideSearchItem2 == true) {
			searchItem2.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			searchItem2.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
	};
	/**
	 * @description 申请主单按钮事件联动
	 */
	var onDocActive = {
		add: function() { //新增按钮
			//判断当前医生是否有开单权限
			onApplyDocGrade(function(result) {
				if (result == true) {
					onAdd();
				}
			});
		},
		edit: function() { //编辑按钮
			//判断当前医生是否有开单权限
			onApplyDocGrade(function(result) {
				if (result == true) {
					onEdit();
				}
			});
		},
		confirmApply: function() { //医嘱提交按钮
			//判断当前医生是否有开单权限
			onApplyDocGrade(function(result) {
				if (result == true) {
					onConfirmApply();
				}
			});
		},
		senior: function() { //上级审核按钮
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				onSenior(data[0]);
			} else {
				layer.alert('请选择用血申请单后再操作!', {
					btn: ['关闭'],
					time: 0
				});
			}
		},
		director: function() { //科主任审核按钮
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				onDirector(data[0]);
			} else {
				layer.alert('请选择用血申请单后再操作!', {
					btn: ['关闭'],
					time: 0
				});
			}
		},
		reupload: function() { //手工上传HIS
			onReuploadToHis();
		},
		refresh: function() {
			initBreqFormTable();
		},
		search: function() {
			onRefreshBreqFormTable();
		},
		previewPDF: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				openPreviewPDF(data[0]);
				//zfpreviewPDF(data[0]);
			} else {
				layer.alert('请选择用血申请单后再操作!', {
					btn: ['关闭'],
					time: 0
				});
			}
		},
		directPrint: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				zfprintPDF(data[0]);
			} else {
				layer.alert('请选择用血申请单后再操作!', {
					btn: ['关闭'],
					time: 0
				});
			}
		},
		deletes: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data; //得到选中的数据
			if (data.length === 0) {
				return layer.msg('请选择数据');
			}
			layer.confirm('确定要删除当前用血申请单吗？', function(index) {
				layer.close(index);
				onDelete(data[0]);
			});
		},
		obsolete: function() {//作废
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data; //得到选中的数据
			if (data.length === 0) {
				return layer.msg('请选择数据');
			}
			onObsolete(data[0]);
		},
		nailApprove: function(){ //钉钉审批
			onNailApprove();
		},
		bloodRecord: function() {
			onBloodRecord();
		},
		sendOutRecord: function() {
			onSendOutRecord();
		}
	};
	/**
	 * @description 判断当前医生是否有开单权限
	 */
	function onApplyDocGrade(callback) {
		if (!sysCurUserInfo) {
			layer.alert('获取当前医生信息为空,不能操作!', {
				btn: ['关闭'],
				icon: 1
			});
			return callback(false);
		}
		var lowLimit = sysCurUserInfo.LowLimit;
		var upperLimit = sysCurUserInfo.UpperLimit;
		if (!lowLimit) lowLimit = 0;
		if (!upperLimit) upperLimit = 0;
		upperLimit = parseFloat(upperLimit);
		lowLimit = parseFloat(lowLimit);
		//医生的用库申请量上限值小于申请量最低预设值
		if (upperLimit < applyrGrade.LowLimit) {
			var gradeName = sysCurUserInfo.GradeName;
			if (!gradeName) gradeName = "空";
			layer.alert('当前医生分配的等级为:' + gradeName + ',用血量权限范围为:' + lowLimit + '~' + upperLimit + ',小于用血申请量最低权限值:' +
				applyrGrade.LowLimit + ',当前医生不具有用血申请的权限,请联系系统管理员为当前医生分配用血申请权限后再操作!', {
					btn: ['关闭'],
					icon: 1
				});
			if (callback) return callback(false);
		}
		if (callback) return callback(true);
	};
	/**
	 *@description 新增处理
	 */
	function onAdd() {
		//判断是HIS调用还是血库系统登录
		var ishisCall = bloodsconfig.getData(bloodsconfig.cachekeys.ISHISCALL_KEY);
		//HIS调用需要判断患者是否存在用血同意书信息
		if (ishisCall == true) {
			onGetPatInfo(function(result) {
				if (result.isAdd == true) {
					addReq();
				} else {
					layer.alert(result.msg, {
						icon: 5,
						btn: ['关闭']
					});
					//layer.msg(result.msg);
				}
			});
		} else {
			addReq();
		}
	};
	/**
	 * @description 编辑处理
	 */
	function onEdit() {
		var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
		var data = checkStatus.data;
		if (data.length == 1) {
			var curRow = data[0];
			var statusId = "" + curRow["BloodBReqForm_BreqStatusID"];
			//暂存,上级审核退回可以继续编辑
			if (statusId != "1" && statusId != "4") {
				var breqStatusName = "" + curRow["BloodBReqForm_BreqStatusName"];
				layer.alert("当前用血申请单状态为:" + breqStatusName + ",不能修改!", {
					btn: ['关闭'],
					time: 0
				});
				return;
			}

			bloodBreqSearchForm.openForm(curRowInfo, "edit", data[0], function() {
				//获取医嘱申请的保存处理结果
				var result = cachedata.getCache("breqEditFormSave");
				if (!result) return;
				savaAfter(result);
			});
		} else {
			layer.alert('请选择用血申请单后再操作!', {
				btn: ['关闭'],
				time: 0
			});
		}
	};
	/***
	 * @description 新增医嘱申请
	 */
	function addReq() {
		//弹出用血说明提示窗体
		getBloodUseDesc(function(bloodUseDesc1) {
			parent.layer.open({
				title: '用血说明',
				content: bloodUseDesc1,
				area: ['100%', '100%'],
				yes: function(index, layero) {
					layer.close(index);
					var objParams = defaultParams;
					bloodBreqSearchForm.openForm(objParams, "add", null, function() {
						//获取医嘱申请的保存处理结果
						var result = cachedata.getCache("breqEditFormSave");
						//Jcall 20191205 #start#
						if (!result) return;
						//Jcall 20191205 #end#
						savaAfter(result);
					});
				},
				cancel: function(index, layero) {
					layer.close(index);
					var objParams = defaultParams;
					bloodBreqSearchForm.openForm(objParams, "add", null, function(success) {
						if (success == true) {
							onRefreshBreqFormTable();
						}
					});
				}
			});
		});
	};
	/**
	 * @description 确认提交
	 */
	function onConfirmApply() {
		if (!BUTTON_CAN_CLICK) return;

		var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
		var data = checkStatus.data;
		if (data.length == 1) {
			BUTTON_CAN_CLICK = false;
			var layerIndex = layer.msg('确认提交处理中', {
				time: 0,
				icon: 16,
				shade: 0.5
			});
			breqFormTable1.onConfirmApply(data[0], function(result) {
				BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
				if (layerIndex != null) layer.close(layerIndex);
				if (result.success == true) {
					savaAfter(result);
				}
			});
		} else {
			layer.alert('请选择用血申请单后再操作!', {
				btn: ['关闭'],
				time: 0
			});
		}
	};
	/**
	 * @description 确认提交后处理
	 * @param {Object} result
	 */
	function savaAfter(result) {
		if (result.success == true) {
			alertReviewTips(result);
		} else {
			onRefreshBreqFormTable();
		}
	};
	/**
	 * @description 申请保存后处理
	 * @param {Object} result
	 */
	function alertReviewTips(result) {
		//用血申请确认后是否自动完成审批,为“是”时，直接弹出提示信息并刷新列表
		var runPVal4 = cachedata.getCache("ConfirmedIsAutoCompleted");
		if (runPVal4 == "true" || runPVal4 == "1" || runPVal4 == true) {
			runPVal4 = true;
		} else if (runPVal4 == "false" || runPVal4 == "0" || runPVal4 == false) {
			runPVal4 = false;
		}
		if (runPVal4 == "" || runPVal4 == undefined) {
			runPVal4 = bloodsconfig.Common.ConfirmedIsAutoCompleted;
		}
		if (runPVal4 == true) {
			//自定义提示信息
			result.reviewTips += "<br/>" + bloodsconfig.AlertInfo.Doctor.ConfirmedInfo;
		}
		if (result.reviewTips && result.reviewTips.length > 0) {
			var info = result.reviewTips;
			layer.alert(info, {
				title: "审批提示",
				btn: ['关闭'],
				icon: 6,
				end: function(index) {
					if (runPVal4 == true) {
						onRefreshBreqFormTable();
					} else { //原来的连续审批处理
						continuousReview(result);
					}
				}
			});
		} else {
			onRefreshBreqFormTable();
		}
	};
	/**
	 * @description 判断是否需要连续审核
	 * @param {Object} result
	 */
	function continuousReview(result) {
		if (result.id) {
			breqFormTable1.LoadById(result.id, function(curRow) {
				if (curRow) {
					onSenior(curRow);
				} else {
					onRefreshBreqFormTable();
				}
			});
		} else {
			onRefreshBreqFormTable();
		}
	};
	/**
	 * @description 重新上传
	 */
	function onReuploadToHis() {
		if (!BUTTON_CAN_CLICK) return;

		var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
		var data = checkStatus.data;
		if (data.length == 1) {
			var curRow = data[0];
			var statusId = curRow["BloodBReqForm_BreqStatusID"];
			var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"]; //审批完成标志
			var content = "";

			if (checkCompleteFlag == "0" || checkCompleteFlag.toLowerCase() == "false") {
				content = '<div style="padding: 20px 10px;">当前用血申请单未审批完成,不能进行上传操作!</div>';
			}
			if (content == "") {
				var toHisFlag = "" + curRow["BloodBReqForm_ToHisFlag"]; //HIS数据标志
				if (toHisFlag == "1") {
					content = '<div style="padding: 20px 10px;">当前用血申请单已上传完成!</div>';
				}
			}
			//医嘱暂存,医嘱作废
			if (content.length <= 0 && (content == "1" || statusId == "10")) {
				var breqStatusName = "" + curRow["BloodBReqForm_BreqStatusName"];
				content = "当前用血申请单状态为:" + breqStatusName + ",不能上传!";
			}
			//判断申请单是否正在上传中
			var reqId = "" + curRow["BloodBReqForm_Id"];
			if (content.length <= 0 && toHisArr.indexOf(reqId) >= 0) {
				content = "当前用血申请单正在上传中...";
			}
			if (content.length > 0) {
				BUTTON_CAN_CLICK = true;
				parent.layer.open({
					type: 1,
					offset: "auto",
					content: content,
					btn: '关闭',
					btnAlign: 'c',
					shade: 0,
					yes: function() {
						layer.closeAll();
					}
				});
				return;
			}

			BUTTON_CAN_CLICK = false;
			var layerIndex = layer.msg('上传HIS处理中...请耐心等待!', {
				time: 0,
				icon: 16,
				shade: 0.5
			});
			reuploadToHis(curRow, function(result) {
				BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
				//if (layerIndex != null) layer.close(layerIndex);			
				if (result.success == true) {
					layer.closeAll();
					onRefreshBreqFormTable();
				}
			});
		} else {
			BUTTON_CAN_CLICK = true;
			layer.alert('请选择用血申请单后再操作!', {
				btn: ['关闭'],
				time: 0
			});
		}
	};
	/**
	 * @description 重新上传申请单给HIS
	 * @param {Object} curRow
	 * @param {Object} callback
	 */
	function reuploadToHis(curRow, callback) {
		var reqFormId = "" + curRow["BloodBReqForm_Id"];
		var statusId = curRow["BloodBReqForm_BreqStatusID"];
		var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"]; //审批完成标志
		var content = "";

		if (checkCompleteFlag == "0" || checkCompleteFlag.toLowerCase() == "false") {
			content = '<div style="padding: 20px 10px;">当前用血申请单未审批完成,不能进行上传操作!</div>';
		}
		if (content == "") {
			var toHisFlag = "" + curRow["BloodBReqForm_ToHisFlag"]; //HIS数据标志
			if (toHisFlag == "1") {
				content = '<div style="padding: 20px 10px;">当前用血申请单已上传完成!</div>';
			}
		}
		//医嘱暂存,医嘱作废
		if (content.length <= 0 && (content == "1" || statusId == "10")) {
			var breqStatusName = "" + curRow["BloodBReqForm_BreqStatusName"];
			content = "当前用血申请单状态为:" + breqStatusName + ",不能上传!";
		}
		//判断申请单是否正在上传中
		if (content.length <= 0 && toHisArr.indexOf(reqFormId) >= 0) {
			content = "当前用血申请单正在上传中...";
		}

		if (content.length > 0) {
			BUTTON_CAN_CLICK = true;
			parent.layer.open({
				type: 1,
				offset: "auto",
				content: content,
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
			return;
		}

		if (bloodsconfig.HisInterface.ISTOHISDATA == true) {
			//数组判断是否正在上传中
			BUTTON_CAN_CLICK = false;
			//将申请单号添加到正在上传数组中
			if (toHisArr.indexOf(reqFormId) < 0)
				toHisArr.push(reqFormId);
			breqFormTable1.onToHisData(curRow, function(result) {
				//移除上传中的申请单
				var index = toHisArr.indexOf(reqFormId);
				if (index >= 0) toHisArr.splice(index, 1);

				BUTTON_CAN_CLICK = true;
				if (result.success == true) layer.closeAll();
				if (callback) callback(result);
			});
		} else {
			BUTTON_CAN_CLICK = true;
			if (callback) callback(result);
		}
	};
	/**
	 * @description 初始化作废原因下拉选择框
	 * @param {Object} callback
	 */
	function initObsoleteInfo(callback) {
		bloodSelectData.dictList.BReqFormObsolete(function(html) {
			$("#BloodBReqForm_ObsoleteMemoId").html(html);
			//重新渲染select
			form.render('select');
			if (callback) callback(html);
		});
	};
	var breqFormTable1 = null;
	/**
	 * @description 申请主单列表配置信息
	 */
	function getBreqFormTableConfig() {
		//列表高度=申请明细信息列表高度-查询表单高度
		var height1 = reqheight + 30;
		//默认条件
		var defaultWhere = "";
		var arr = [];
		if (arr.length > 0) defaultWhere = "" + arr.join(" and ") + "";
		return {
			title: '申请主单信息',
			height: height1,
			elem: '#LAY-app-table-BloodBreqForm',
			id: "LAY-app-table-BloodBreqForm",
			toolbar: "",
			defaultToolbar: null,
			defaultWhere: defaultWhere,
			externalWhere: ""
		};
	};
	var breqItemTable1 = null;
	/**
	 * @description 申请明细列表配置信息
	 */
	function getBreqItemTableConfig() {
		var height1 = reqheight - 30;
		return {
			title: '申请明细信息',
			height: height1,
			elem: '#LAY-app-table-BloodBreqItem',
			id: "LAY-app-table-BloodBreqItem",
			toolbar: "",
			defaultToolbar: null,
			externalWhere: ""
		};
	};

	var breqFormResultTable1 = null;
	/**
	 * @description 申请主单列表配置信息
	 */
	function getbreqFormResultTableConfig() {
		var height1 = reqheight - 30;
		return {
			title: '检验结果信息',
			height: height1,
			elem: '#LAY-app-table-BloodBReqFormResult',
			id: "LAY-app-table-BloodBReqFormResult",
			toolbar: "",
			defaultToolbar: null,
			externalWhere: ""
		};
	};
	/**
	 * @description 申请主单列表监听
	 */
	function onBloodBreqFormTable() {
		//监听工具条
		table.on('tool(LAY-app-table-BloodBreqForm)', function(obj) {
			var data = obj.data;
			if (obj.event === 'editBloodBreqForm') {
				bloodBreqSearchForm.openForm(curRowInfo, "edit", data, function(success) {
					if (success == true) {
						onRefreshBreqFormTable();
					}
				});
			}
		});
		//监听行单击事件
		table.on('row(LAY-app-table-BloodBreqForm)', function(obj) {
			//各明细列表默认条件改变
			setCurRow(obj.data);
			//设置当前行为选中状态
			breqFormTable1.setRadioCheck(obj);
			//功能按钮显示/隐藏
			setButtonsHide(obj.data);
			setTimeout(function() {
				onRefreshDtlTable(obj);
			}, 300);
		});
		//监听行双击事件
		table.on('rowDouble(LAY-app-table-BloodBreqForm)', function(obj) {
			//是否弹出编辑或查看?
			var url = bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_UNPASSDESC_URL;
			var BReqFormFlag = obj.data["BloodBReqForm_BReqFormFlag"];
			var BReqFormID = obj.data["BloodBReqForm_Id"];
			var param = {sBreqFormID: BReqFormID};
			param = JSON.stringify(param);
			if (BReqFormFlag && BReqFormFlag != '2') return; 
			csserver.ajax({
			  url: url, 
			  data: param,
			  type:"POST"
			}, function(data) {
				if (data.success){
					layer.msg(data.ResultDataValue[0]["UnPassdesc"], {time: 5000});
				}
			})
		});
		//监听排序事件 
		table.on('sort(LAY-app-table-BloodBreqForm)', function(obj) {
			var sort = [];
			var cur = {
				"property": obj.field,
				"direction": obj.type
			};
			sort.push(cur);
			breqFormTable1.setSort(sort);
			onRefreshBreqFormTable();
		});
	};
	/**
	 * @description 判断当前医生是否有开单权限
	 */
	function onRefreshBreqFormTable() {
		//获取查询条件
		breqFormTable1.getSearchWhere();
		table.reload(breqFormTable1.config.id, breqFormTable1.config);
		toHisArr = []; //上传His数组清空
		BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
	};
	/**
	 * @description 申请主单联动刷新明细列表
	 * @param {Object} obj
	 */
	function onRefreshDtlTable(obj) {
		//更新各明细列表的查询条件
		var data = obj.data;
		var docId = data["BloodBReqForm_Id"];
		breqItemTable1.setReqFormNo(docId);
		table.reload(breqItemTable1.tableIns.config.id, breqItemTable1.config);

		breqFormResultTable1.setReqFormNo(docId);
		table.reload(breqFormResultTable1.tableIns.config.id, breqFormResultTable1.config);
	};
	//刷新申请明细列表
	function onRefreshBreqItemTable() {
		//获取查询条件
		breqItemTable1.getSearchWhere();
		table.reload(breqItemTable1.tableIns.config.id, breqItemTable1.config);
	};
	//刷新LIS检验结果列表
	function onRefreshItemResultTable() {
		//获取查询条件
		breqFormResultTable1.getSearchWhere();
		table.reload(breqFormResultTable1.tableIns.config.id, breqFormResultTable1.config);
	};
	//申请主单列表初始化
	function initBreqFormTable() {
		//申请主单列表初始化
		breqFormTable1 = breqFormTable.render(getBreqFormTableConfig());
		onBloodBreqFormTable();
	};
	//申请明细列表初始化
	function initBreqItemTable() {
		breqItemTable1 = breqItemTable.render(getBreqItemTableConfig());
		onBreqItemTable();
	};
	//申请明细列表监听
	function onBreqItemTable() {
		//监听工具条
		table.on('tool(LAY-app-table-BloodBreqItem)', function(obj) {
			var data = obj.data;
			if (obj.event === 'del') {}
		});
	};
	/**
	 * @description 初始申请状态列表
	 * @param {Object} callback
	 */
	function initBreqFormStatus(callback) {
		bloodClassDict.init("BreqFormStatus", function(resultData) {
			var html = '<option value="">请选择</option>';
			if (resultData.success) {
				var id = '';
				var name = '';
				var result = resultData.value || [];
				for (var i = 0; i < result.length; i++) {
					id = result[i]['Id'];
					name = result[i]['Name'];
					if (id == '2') //默认 确认提交
					{
						//html += '<option selected ="selected" value="' + id + '">' + name + '</option>';
						//longfc 2019-12-27
						html += '<option value="' + id + '">' + name + '</option>';
					} else {
						html += '<option value="' + id + '">' + name + '</option>';
					}
				}
			}
			var elemid = "#bloodbreqform_search_breqformstatus";
			$(elemid).empty().append(html);
			form.render('select');
			if (callback && typeof callback === 'function') callback();
		});
	};

	/**
	 * @description 检验结果列表初始化
	 */
	function initbreqFormResultTable() {
		breqFormResultTable1 = breqFormResultTable.render(getbreqFormResultTableConfig());
		onbreqFormResultTable();
	};
	//检验结果列表监听
	function onbreqFormResultTable() {
		//监听工具条
		table.on('LAY-app-table-BloodBReqFormResult)', function(obj) {
			var data = obj.data;
			if (obj.event === 'del') {}
		});
	};
	/**
	 * @description 初始化列表
	 */
	function initTable() {
		initBreqFormTable();
		setTimeout(function() {
			//申请明细列表初始化
			initBreqItemTable();
			//检验结果列表初始化
			initbreqFormResultTable();
		}, 500);
	};
	/**
	 * @description 初始化页面组件
	 */
	function initAll() {
		initDefaultParams();
		initForm();
		onSearchForm();
		initBreqFormStatus(function() {
			initTable();
		});
		//新增表单的下拉字典数据预加载
		bloodSelectData.loadAllDict();
		//初始化系统运行参数
		initRunParams();
	};
	/**
	 * @description 初始化系统运行参数
	 */
	function initRunParams() {
		runParams.initRunParams(function() {
			bloodsconfig = runParams.renderBloodsconfig(bloodsconfig);
			layui.bloodsconfig=bloodsconfig;
			//console.log(runPVal4);			
		});
	};
	/**
	 * @description 访问功能页面验证
	 * @param {Object} callback
	 */
	function initVerification(callback) {
		if (!sysCurUserInfo) {
			var params = "获取当前操作的医生信息为空!";
			var href = "/layui/views/bloodtransfusion/sysprompt/index.html?type=2";
			href = href + "&t=" + new Date().getTime() + "&info=" + params;
			location.href = uxutil.path.UI + href;
		}
		if (callback) callback();
	};
	initVerification(function() {
		initAll();
	});
	/**
	 * @description 作废处理
	 * @param {Object} data
	 */
	function onObsolete(data) {
		initObsoleteInfo(function(html) {
			if (!html) {
				layer.alert('获取医嘱作废原因信息为空,请维护医嘱作废原因信息后再操作!', {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
				return;
			}
			//选择作废原因
			parent.layer.open({
				type: 1,
				title: "作废操作",
				skin: 'layui-layer-rim', //加上边框
				area: ['420px', '385px'], //宽高
				content: $('#LAY-app-form-BReqFormObsolete'),
				btn: ['确认作废', '取消'],
				yes: function(index, layero) {
					var obsoleteMemoId = $("#BloodBReqForm_ObsoleteMemoId").val();
					var obsoleteMemo = $("#BloodBReqForm_ObsoleteMemoId").find("option:selected").text();
					if (!obsoleteMemoId) {
						layer.alert('请选择医嘱作废原因后再提交!', {
							icon: 5,
							btn: ['关闭'],
							time: 0
						});
						return;
					}
					var obsolete = {
						"ObsoleteMemoId": obsoleteMemoId,
						"ObsoleteMemo": obsoleteMemo
					};
					breqFormTable1.onObsolete(data, obsolete, function(result) {
						if (result.success == true) {
							onRefreshBreqFormTable();
							layer.close(index);
						}
					});
				},
				btn1: function(index, layero) {
					//按钮【取消】的回调
					layer.close(index);
				}
			});
		});
	};
	
	//钉钉审核更新标识
	function updateStatusforNailApprove(ReqFormID, callback){
		var url = bloodsconfig.getCsBaseUrl() + bloodsconfig.CSServer.CS_DINGDINGAPPROVE_URL;
 		var param = {"sBreqFormID": ReqFormID};
		param = JSON.stringify(param);
		csserver.ajax({
		  url: url, 
		  data: param,
		  type:"POST"
		}, function(data) {
			callback && typeof(callback) === 'function' && callback(data);
		})       
	}
	
	//钉钉审核上传
	function NailApproveUpload(callback){
		if (!BUTTON_CAN_CLICK) return;
		BUTTON_CAN_CLICK = false;
		//监听列表row的click事件调用setRadioCheck设置Radio的为选择状态所以下面的语句有效
		var content = '';	
		var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
		var data = checkStatus.data;
		var curRow = data.length === 1 ? data[0]: {};
		if (curRow["BloodBReqForm_Id"]) {
			var ReqFormID = curRow["BloodBReqForm_Id"]; //申请单号 
			var reqStatusID = curRow["BloodBReqForm_BreqStatusID"]; //审批完成标识
			var CompleteFlag = curRow["BloodBReqForm_CheckCompleteFlag"]; //审核完成标识
			var toHisFlag = "" + curRow["BloodBReqForm_ToHisFlag"]; //HIS数据标志
			var breqStatusName = "" + curRow["BloodBReqForm_BreqStatusName"];
	        //已审批完成
			if (CompleteFlag == "1" || CompleteFlag.toLowerCase() == "true") {
				content = '<div style="padding: 20px 10px;">当前用血申请单已经审批完成,不能进行钉钉审批操作!</div>';
			}
			//已经上传
			if (content.length <= 0 && toHisFlag == "1") {
				content = '<div style="padding: 20px 10px;">当前用血申请单已上传完成!</div>';
			}
			//医嘱作废
			if (content.length <= 0 && reqStatusID == "10") {
				content = "当前用血申请单状态为:" + breqStatusName + ",不能上传!";
			}
			//判断申请单是否正在上传中
			if (content.length <= 0 && toHisArr.indexOf(ReqFormID) >= 0) {
				content = "当前用血申请单正在上传中...";
			}
			if (content.length > 0) {
				BUTTON_CAN_CLICK = true;
				layer.open({
					type: 1,
					offset: "auto",
					content: content,
					btn: '关闭',
					btnAlign: 'c',
					shade: 0,
					yes: function() {
						layer.closeAll();
					}
				});
				return;
			}
			//记录申请ID
			if (toHisArr.indexOf(ReqFormID) < 0){
				toHisArr.push(ReqFormID);
			}
			//先调用his服务更新当前申请单状态,回调返回成功后上传his
            updateStatusforNailApprove(ReqFormID, function(data){
				//上传到his	
				if (data.success){
					breqFormTable1.onToHisData(curRow, function(result){
						BUTTON_CAN_CLICK = true;
						//移除上传中的申请单
						var index = toHisArr.indexOf(ReqFormID);
						if (index >= 0) toHisArr.splice(index, 1);
						var success = false;
						if (result) success = result.success;
						if (success == true) {
							if (callback) callback(result.success);
						} else {
							//layer.msg("调用服务返回给HIS失败!");
							layer.alert("调用服务返回给HIS失败!", {
								title: "上传HIS提示",
								btn: ['关闭'],
								icon: 5,
								end: function(index) {
									if (callback) callback(true);
								}
							});
						}
						onRefreshBreqFormTable();
					});
				} else{
					BUTTON_CAN_CLICK = true;
					layer.alert(data.ErrorInfo, {
						icon: 5,
						btn: ['关闭'],
						time: 0
					});	
					onRefreshBreqFormTable();
				}
           })
		} else {
			BUTTON_CAN_CLICK = true;
			layer.alert('请选择用血申请单后再操作!', {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
		};		
	}
	/*钉钉审批677*/
	function onNailApprove(callback){
		var content = "<p style='text-indent:20px'>1.请前往钉钉填写用血审批单，否则本次申请无效。</P>";
		content = content + "<p style='text-indent:20px'>2.只有钉钉审批完成后，输血科才予以处理。</P>";
		layer.open({type: 1,
					area: ['350px', '150px'],
					content: content,
					btn: ['确定','取消'],
					btnAlign: 'c',
					shade: 0,
					yes: function() {
						layer.closeAll();
						NailApproveUpload(callback);
					},
					btn2:function(){
						layer.closeAll();
					},
					cancel:function(){
						layer.closeAll();
					}
		});
	};
	
	/**物理删除处理*/
	function onDelete(data) {
		breqFormTable1.onDelete(data, function(result) {
			if (result.success == true) onRefreshBreqFormTable();
		});
	};
	/**
	 *@description 配血记录
	 */
	function onBloodRecord() {
		var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
		var data = checkStatus.data;
		if (data.length == 1) {
			onOpenRecords("1", data[0]);
		} else {
			layer.alert('请选择用血申请单后再操作!', {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
		}
	};
	/**
	 * @description 发血记录
	 */
	function onSendOutRecord() {
		var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
		var data = checkStatus.data;
		if (data.length == 1) {
			onOpenRecords("2", data[0]);
		} else {
			layer.alert('请选择用血申请单后再操作!', {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
		}
	};
	/**
	 * @description 查询表单事件监听
	 */
	function onSearchForm() {
		$('.layui-form .layui-form-item .layui-inline .layui-btn').on('click', function() {
			var type = $(this).data('type');
			onDocActive[type] ? onDocActive[type].call(this) : '';
		});

		$('#LAY-app-table-BloodBreqForm-Search-LikeSearch').on('keydown', function(event) {
			if (event.keyCode == 13) onRefreshBreqFormTable();
		});

		//打印次数查询 2019-12-16 by xhz
		form.on('select(bloodbreqform_filter_printtotal)', function(data) {
			onRefreshBreqFormTable();
		});

		//申请状态查询 2019-12-18 by xhz
		form.on('select(bloodbreqform_filter_breqformstatus)', function(data) {
			onRefreshBreqFormTable();
		});
	};
	/***
	 * @description 行选择改变后,各明细列表默认条件改变
	 * @param {Object} objRow
	 */
	function setCurRow(objRow) {
		curRowInfo["ReqFormNo"] = objRow["BloodBReqForm_Id"];
		curRowInfo["HisDoctorId"] = objRow["BloodBReqForm_HisDoctorId"];
		curRowInfo["HisDeptId"] = objRow["BloodBReqForm_HisDeptID"];
		curRowInfo["DoctorId"] = objRow["BloodBReqForm_DoctorNo"];
		curRowInfo["DeptId"] = objRow["BloodBReqForm_DeptNo"];
		curRowInfo["HisPatId"] = objRow["BloodBReqForm_PatID"];
		curRowInfo["AdmId"] = objRow["BloodBReqForm_AdmID"];
		curRowInfo["PatNo"] = objRow["BloodBReqForm_PatNo"];
		curRowInfo["CName"] = objRow["BloodBReqForm_CName"];
	};
	/***
	 * @description 行选择改变后,设置功能按钮的显示或隐藏
	 * @param {Object} curRow
	 */
	function setButtonsHide(curRow) {
		var isHideSenior = true; //是否隐藏上级审核按钮
		var isHideDirector = true; //是否隐藏科主任审核按钮
		var isHideRecord = true; //是否隐藏配血记录按钮及发血记录按钮
		if (curRow) {
			var statusId = "" + curRow["BloodBReqForm_BreqStatusID"];
			//是否审批完成标志
			var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"];
			//输血科审核标志
			var reqFormFlag = "" + curRow["BloodBReqForm_BReqFormFlag"];
			//提交申请,科主任审核退回可以进行上级审核
			if (statusId == "2" || statusId == "6") {
				isHideSenior = false;
			}
			//上级审核通过,医务处审批退回可以进行科主任审核
			else if (statusId == "3" || statusId == "8") {
				isHideDirector = false;
			}
			//如果医嘱申请已完成审批处理,都隐藏
			if (checkCompleteFlag == "1" || checkCompleteFlag.toLowerCase() == "true") {
				isHideSenior = true;
				isHideDirector = true;
			}
			//如果输血科审核受理通过
			if (reqFormFlag == "1") {
				isHideRecord = false;
			}
		}
		//审核按钮div
		if (isHideSenior == true && isHideDirector == true) {
			$('[lay-filter="div-senior-director"]').removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			$('[lay-filter="div-senior-director"]').removeClass("layui-inline layui-hide").addClass("layui-inline");
		}
		//上级审核按钮
		if (isHideSenior == true) {
			$("#LAY-app-form-div-senior").removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			$("#LAY-app-form-div-senior").removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		//科主任审核按钮
		if (isHideDirector == true) {
			$("#LAY-app-form-div-director").removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			$("#LAY-app-form-div-director").removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		//配血记录及发血记录是否显示或隐藏
		if (isHideRecord == true) {
			$('[lay-filter="div-blood-record"]').removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
			$('[lay-filter="div-send-out-record"]').removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			$('[lay-filter="div-blood-record"]').removeClass("layui-inline layui-hide").addClass("layui-inline");
			$('[lay-filter="div-send-out-record"]').removeClass("layui-inline layui-hide").addClass("layui-inline");
		}
		//测试
		//$('[lay-filter="div-blood-record"]').removeClass("layui-inline layui-hide").addClass("layui-inline");
		//$('[lay-filter="div-send-out-record"]').removeClass("layui-inline layui-hide").addClass("layui-inline");
	};
	/**
	 * @description 智方打印控件--直接打印PDF
	 * @param {Object} data
	 */
	function zfprintPDF(data) {
		if (!BUTTON_CAN_CLICK) return;

		var id = data["BloodBReqForm_Id"];
		BUTTON_CAN_CLICK = false;
		var layerIndex = layer.msg('正在打印中...', {
			icon: 16,
			shade: 0.01
		});
		getPDFJS(id, function(data) {
			var url = bloodsconfig.Common.PDF_SAVE_URL + id + ".pdf" + "?tt=" + new Date().getTime();
			zfprint.print(url, function(pdf) {
				BUTTON_CAN_CLICK = true;
				layer.close(layerIndex);
				breqFormTable1.updatePrintTotal(id, function() {
					onRefreshBreqFormTable();
				});
			});
		});
	};
	/**
	 * @description 智方打印控件--预览打印
	 * @param {Object} data
	 */
	function zfpreviewPDF(data) {
		if (!BUTTON_CAN_CLICK) return;

		var id = data["BloodBReqForm_Id"];
		BUTTON_CAN_CLICK = false;
		getPDFJS(id, function(data) {
			var url = bloodsconfig.Common.PDF_SAVE_URL + id + ".pdf"; //+ "?t=" + new Date().getTime();
			BUTTON_CAN_CLICK = true;
			zfprint.preview(url, function(pdf) {

			});
		});
	};

	/**
	 * @description 获取打印或预览的PDF文件
	 * @param {Object} id
	 * @param {Object} callback
	 */
	function getPDFJS(id, callback) {
		var url = uxutil.path.ROOT +
			"/BloodTransfusionManageService.svc/BT_UDTO_SearchBusinessReportOfPDFJSById?id=" +
			id + "&reaReportClass=Frx&breportType=1&operateType=1&frx=&t=" + new Date().getTime();
		var index = layer.load(1);
		uxutil.server.ajax({
			url: url,
			async: false
		}, function(data) {
			layer.close(index);
			if (callback) callback(data);
		});
	};
	/**
	 * @description 按PDF.JS直接打印用血申请单PDF
	 * @param {Object} data
	 */
	function onPrintPDFOfPDFJS(id, isprint) {
		var index = layer.load(1);
		getPDFJS(id, function(data) {
			layer.close(index);
			if (data.success == true) {
				var query = bloodsconfig.Common.PDF_SAVE_URL + id + ".pdf";
				window.localStorage.setItem("url_app", query);
				//注意此步的地址要写对
				var url = bloodsconfig.Common.PDFJS_URL + "?isprint=" + isprint + "&file=" + query + "&locale=zh_CH&t=" + new Date()
					.getTime();
				//为了防止浏览器拦截，用此方法
				$("#jump").attr("href", url);
				//打印次数更新
				if (isprint == true) {
					breqFormTable1.updatePrintTotal(id, function() {
						onRefreshBreqFormTable();
					});
				}
				$('#jump span').click();
			} else {
				layer.alert('获取PDF信息失败!' + data.msg, {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
			}
		});
	};

	//打印时setTimeout的执行动作
	var waitAction = null;
	//打印时循环重复等待多少次后结束循环
	var delayCounts = 10;
	/**
	 * @description 用血申请单PDF预览
	 * @param {Object} data
	 */
	function openPreviewPDF(data) {
		var id = data["BloodBReqForm_Id"];
		var url = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBusinessReportOfPdfById";
		var arr = [];
		arr.push("id=" + id);
		arr.push("reaReportClass=Frx"); //Frx模板
		arr.push("breportType=1"); //医嘱申请
		arr.push("operateType=1"); //直接打开文件
		arr.push("frx="); //用血申请单.frx
		arr.push("t=" + new Date().getTime()); //用血申请单.frx
		url = url + "?" + arr.join("&");

		//第二种方案:暂时性
		/**/
		parent.layer.open({
			title: "PDF预览打印",
			type: 2,
			area: ['100%', '100%'], //宽高
			content: url,
			btn: ['关闭'], //
			btn1: function(index, layero) {
				//window.print();
				layer.close(index);
			}
		});

		//第三种方案
		//onPrintPDFOfPDFJS(id,false);
	};
	/***
	 * @description 上级审核
	 * @param {Object} curRow
	 */
	function onSenior(curRow) {
		if (!BUTTON_CAN_CLICK) return;

		onOpenReview(curRow, "senior", function(result) {
			//if (result.success == true) {
			onRefreshBreqFormTable();
			//}
		});
	};
	/***
	 * @description 科主任审核
	 * @param {Object} curRow
	 */
	function onDirector(curRow) {
		if (!BUTTON_CAN_CLICK) return;

		onOpenReview(curRow, "director", function(result) {
			//if (result.success == true) {
			onRefreshBreqFormTable();
			//}
		});
	};
	/***
	 * @description 弹出审核处理
	 * @param {Object} curRow 用血申请单行记录
	 * @param {Object} reviewType 审核类型:(上级审核:senior;科主任审核:director)
	 * @param {Object} callback
	 */
	function onOpenReview(curRow, reviewType, callback) {
		layer.closeAll();

		var msgInfo = "";
		var result1 = {
			success: false,
			msg: ""
		};
		var reqTotal = "" + curRow["BloodBReqForm_ReqTotal"];
		if (!reqTotal) reqTotal = 0;
		reqTotal = parseFloat(reqTotal);
		var breqStatusID = "" + curRow["BloodBReqForm_BreqStatusID"];
		var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"];
		if (checkCompleteFlag == "1" || checkCompleteFlag.toLowerCase() == "true") {
			BUTTON_CAN_CLICK = true;
			layer.alert("当前用血申请单已审批完成,请不要重复操作!", {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
			return callback(result1);
		}
		var title = "审核操作";
		switch (reviewType) {
			case "senior":
				title = "上级审核";
				//提交申请,科主任审核退回可以进行上级审核
				if (breqStatusID != "2" && breqStatusID != "6") {
					msgInfo = "当前用血申请单状态为:" + curRow["BloodBReqForm_BreqStatusName"]; + ",不能进行上级审核!";
				}
				break;
			case "director":
				title = "科主任审核";
				//上级审核通过,医务处审批退回可以进行科主任审核
				if (breqStatusID != "3" && breqStatusID != "8") {
					msgInfo = "当前用血申请单状态为:" + curRow["BloodBReqForm_BreqStatusName"]; + ",不能进行科主任审核!";
				}
				break;
			default:
				msgInfo = "传入的审核类型系统未识别!";
				break;
		}
		if (msgInfo.length > 0) {
			BUTTON_CAN_CLICK = true;
			layer.msg(msgInfo, {
				time: 2000
			});
			return callback(result1);
		}
		//先清空审输入框信息
		$("#ReviewAccount").val("");
		$("#ReviewPwd").val("");
		$("#ReviewContent").val("");

		$("#LAY-app-label_24ReviewInfo").text("24小时内大量用血量为：" + reqTotal + " ml");
		var statusID = "";
		//在父窗口打开页面层
		parent.layer.open({
			type: 1,
			title: title,
			skin: 'layui-layer-rim', //加上边框
			area: ['460px', '320px'], //宽高
			content: $('#LAY-app-form-ReviewInfo'),
			btn: ['审核通过', '审核退回', '关闭'],
			btn1: function(index, layero) {
				if (!BUTTON_CAN_CLICK) return;

				//审核通过
				switch (reviewType) {
					case "senior": //上级审核通过
						statusID = 3;
						break;
					case "director":
						statusID = 5; //科主任审核通过
						break;
					default:
						break;
				}
				//Jcall 20191205 #start#
				$("#ReviewContent").val("同意");
				BUTTON_CAN_CLICK = false; //所有按钮置为不可点击状态
				//Jcall 20191205 #end#				
				if (statusID) {
					onVerifyReview(curRow, reviewType, statusID, index, function(result) {
						//Jcall 20191205 #start#
						BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
						//Jcall 20191205 #end#
						if (result == true) layer.close(index);
						if (result == true) layer.closeAll();
						//判断是否需要连续审核:上级审核通过并且需要继续科主任审核(大量用血申请量大于等于800)						
						if (statusID == 3 && reqTotal >= directorGrade.LowLimit) {
							var id = curRow["BloodBReqForm_Id"];
							breqFormTable1.LoadById(id, function(curRow2) {
								if (curRow2) {
									//需要添加延时处理才能连续显示
									uxutil.action.delay(function() {
										onOpenReview(curRow2, "director", function(result2) {
											if (callback) callback(result2);
										});
									}, 800);
								} else {
									if (callback) callback(result);
								}
							});
						} else {
							if (callback) callback(result);
						}
					});
				} else {
					BUTTON_CAN_CLICK = true;
					//return false;
					layer.close(index);
					layer.closeAll();
				}
			},
			btn2: function(index, layero) {
				//Jcall 20191205 #start#				
				if (!BUTTON_CAN_CLICK) return;

				$("#ReviewContent").val("不同意");
				BUTTON_CAN_CLICK = false; //所有按钮置为不可点击状态
				//Jcall 20191205 #end#
				var layerIndex = layer.msg('审核处理中', {
					time: 0, //Jcall 20200113，msg方法默认的消失时间是3秒，赋值成0就不消失直到主动调用方法close；
					icon: 16,
					shade: 0.01
				});
				//审核退回
				switch (reviewType) {
					case "senior": //上级审核退回
						statusID = 4;
						break;
					case "director": //科主任审核退回
						statusID = 6;
						break;
					default:
						break;
				}
				if (statusID) {
					onVerifyReview(curRow, reviewType, statusID, index, function(result) {
						//Jcall 20191205 #start#
						BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
						//Jcall 20191205 #end#
						if (result == true) layer.close(index);
						if (result == true) layer.closeAll();
						if (callback) callback(result);
					});
				} else {
					BUTTON_CAN_CLICK = true;
				}
				//return false;
			},
			btn3: function(index, layero) {
				//按钮【取消】的回调
				layer.close(index);
				result1.success = true;
				if (callback) callback(result1); //这里也没有回调
			}
		});
	};
	/***
	 * @description 依审核医生帐号及密码,获取到的医生信息
	 * @param {Object} account
	 * @param {Object} pwd
	 * @param {Object} reviewType 审核类型:(上级审核:senior;科主任审核:director)
	 * @param {Object} callback
	 */
	function getSysDoctorInfo(account, pwd, reviewType, callback) {
		var url = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_SYS_GetSysCurDoctorInfoByAccount";
		account = encodeURI(account); //IE需要进行编码
		url = url + '?account=' + account + '&pwd=' + pwd + "&t=" + new Date().getTime();
		var layerIndex = layer.msg('获取医生信息中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		//请求接口
		uxutil.server.ajax({
			url: url
		}, function(data) {
			//layer.closeAll('loading');
			layer.close(layerIndex);
			var success = data.success;
			if (success == undefined || success == null) success = data;
			if (success === true) {
				if (callback) callback(data.value || {});
			} else {
				var info = data.ErrorInfo || data.msg;
				layer.alert(info, {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
				if (callback) callback(null);
			}
		});
	};
	/***
	 * @description 审核验证处理
	 * @param {Object} curRow
	 * @param {Object} statusID 医嘱状态Id
	 * @param {Object} index 弹出页面层索引
	 * @param {Object} callback
	 */
	function onVerifyReview(curRow, reviewType, statusID, index, callback) {
		var account = $('#ReviewAccount').val();
		var pwd = $('#ReviewPwd').val();
		//获取审核医生的审核等级及审核用血量范围信息
		if (!account || !pwd) {
			BUTTON_CAN_CLICK = true;
			layer.alert("请输入审核人的帐号及密码后再操作!", {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
			return callback(false);
		}

		BUTTON_CAN_CLICK = false;
		getSysDoctorInfo(account, pwd, reviewType, function(curDoctorInfo) {
			//if (layerIndex != null) layer.close(layerIndex);
			if (!curDoctorInfo) {
				BUTTON_CAN_CLICK = true;
				layer.alert('获取审核医生信息为空,不能审核!', {
					btn: ['关闭'],
					time: 0,
					icon: 5
				});
				return callback(false);
			}
			var lowLimit = curDoctorInfo.LowLimit;
			var upperLimit = curDoctorInfo.UpperLimit;
			if (!lowLimit) lowLimit = 0;
			if (!upperLimit) upperLimit = 0;
			upperLimit = parseFloat(upperLimit);
			lowLimit = parseFloat(lowLimit);
			switch (reviewType) {
				case "senior": //上级审核用血申请量范围为(0-800)
					if (upperLimit < seniorGrade.UpperLimit) { //seniorGrade.LowLimit != lowLimit && seniorGrade.UpperLimit != upperLimit
						BUTTON_CAN_CLICK = true;
						layer.alert('医生审核用血申请量范围为:' + lowLimit + '~' + upperLimit + ',不能进行上级审核!', {
							btn: ['关闭'],
							time: 0,
							icon: 5
						});
						if (callback) {
							return callback(false);
						} else {
							return false;
						}
					} else {
						onReview(curRow, reviewType, statusID, index, curDoctorInfo, function(result) {
							if (callback) callback(result);
						});
					}
					break;
				case "director":
					if (upperLimit < directorGrade.UpperLimit) {
						BUTTON_CAN_CLICK = true;
						layer.alert('医生审核用血申请量范围为:' + lowLimit + '~' + upperLimit + ',不能进行科主任审核!', {
							btn: ['关闭'],
							icon: 5
						});
						if (callback) {
							return callback(false);
						} else {
							return false;
						}
					} else {
						onReview(curRow, reviewType, statusID, index, curDoctorInfo, function(result) {
							if (callback) callback(result);
						});
					}
					break;
				default:
					return callback(false);
					break;
			}

		});
	};
	/***
	 * @description 审核处理
	 * @param {Object} curRow
	 * @param {Object} statusID 医嘱状态Id
	 * @param {Object} index 弹出页面层索引
	 * @param {Object} curDoctorInfo 审核医生信息 
	 * @param {Object} callback
	 */
	function onReview(curRow, reviewType, statusID, index, curDoctorInfo, callback) {
		var content = $('#ReviewContent').val();
		var reqFormId = curRow["BloodBReqForm_Id"];
		var statusId = curRow["BloodBReqForm_BreqStatusID"];
		var reqTotal = curRow["BloodBReqForm_ReqTotal"];

		//Jcall 20191205 #start#
		if (!content) {
			BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
			layer.alert("审核意见不能为空!");
			return;
		}
		//Jcall 20191205 #end#
		if (toHisArr.indexOf(reqFormId) >= 0) {
			BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
			layer.msg("当前用血申请单正在审核中...!");
			return;
		}

		var empID = "",
			empName = "";
		//获取审核医生的医生ID及医生名称
		if (curDoctorInfo.DoctorId) empID = curDoctorInfo.DoctorId;
		if (curDoctorInfo.DoctorCName) empName = curDoctorInfo.DoctorCName;

		var entity = {
			"Id": reqFormId,
			"BreqStatusID": statusID,
			"ReqTotal": reqTotal
		};
		switch (reviewType) {
			case "senior":
				entity.SeniorID = empID;
				entity.SeniorName = empName;
				entity.SeniorMemo = content;
				break;
			case "director":
				entity.DirectorID = empID;
				entity.DirectorName = empName;
				entity.DirectorMemo = content;
				break;
			default:
				break;
		}
		BUTTON_CAN_CLICK = false;
		breqFormTable1.onReview(entity, function(result) {
			var success = result.success;
			BUTTON_CAN_CLICK = true;
			layer.close(index);
			layer.closeAll();
			if (callback) callback(result);
		});
	};
	/***
	 * @description 上级审核通过或科主任审核通过后,需要判断用血申请单是否审批完成,审批完成后,需要处理医嘱申请返回给HIS
	 * @param {Object} curRow
	 * @param {Object} reviewType
	 * @param {Object} statusID
	 * @param {Object} index
	 * @param {Object} curDoctorInfo
	 * @param {Object} callback
	 */
	function onToHisData(curRow, reviewType, statusID, index, curDoctorInfo, callback) {
		var reqTotal = curRow["BloodBReqForm_ReqTotal"];
		if (!reqTotal) reqTotal = 0;
		reqTotal = parseFloat(reqTotal);
		var upperLimit = 0;
		switch (reviewType) {
			case "senior":
				if (seniorGrade.UpperLimit) upperLimit = parseFloat(seniorGrade.UpperLimit);
				break;
			case "director":
				if (directorGrade.UpperLimit) upperLimit = parseFloat(directorGrade.UpperLimit);
				break;
			default:
				break;
		}
		if (!upperLimit) upperLimit = 0;
		//需要判断用血申请单是否审批完成,审批完成后,调用服务返回给HIS
		if (upperLimit != 0 && reqTotal < upperLimit) {
			//console.log("上传HIS处理中...请耐心等待!");
			layer.msg('上传HIS处理中...请耐心等待!', {
				time: 0,
				icon: 16,
				shade: 0.5
			});
			BUTTON_CAN_CLICK = false;
			var reqFormId = "" + curRow["BloodBReqForm_Id"];
			if (toHisArr.indexOf(reqFormId) < 0)
				toHisArr.push(reqFormId);
			breqFormTable1.onToHisData(curRow, function(result) {
				//移除上传中的申请单
				var index = toHisArr.indexOf(reqFormId);
				if (index >= 0) toHisArr.splice(index, 1);

				BUTTON_CAN_CLICK = true;
				var success = false;
				if (result) success = result.success;
				if (success == true) {
					if (callback) callback(result.success);
				} else {
					//layer.msg("调用服务返回给HIS失败!");
					layer.alert("调用服务返回给HIS失败!", {
						title: "上传HIS提示",
						btn: ['关闭'],
						icon: 5,
						end: function(index) {
							if (callback) callback(true);
						}
					});
				}
			});
		} else {
			layer.close(index);
			if (callback) callback(false);
		}
	};
	/***
	 * @description 新增医嘱申请时,按患者就诊号或病历号调用CS服务获取HIS病人信息,判断验证：
	 * （1）HIS调用时,判断患者信息是否存在用血同意书,只有存在用血同意书,才能新增医嘱申请()
	 * （2）如果医生未开相关医嘱(IsOrder=’N’)，提示：该患者尚未进行输血前感染项目检查，请按规定开立医嘱。
	 * （3）如果医生已经有相关医嘱，但是标本状态还未采集的(IsOrder=’Y’ and IsLabNoC=’N’)，提示：该患者的输 前感染项目检验标本尚未采集，请先完成采集状态
	 * @param {Object} callback
	 */
	function onGetPatInfo(callback) {
		var result1 = {
			isAdd: false, //是否满足开单条件
			isAgree: false, //是否有知情同意书
			isOrder: false, //医生是否已开了相关医嘱
			isLabNoC: false, //医生已经有相关医嘱,但检验标本是否已采集
			msg: ""
		};

		var sPatNo = ""; //患者就诊号或病历号
		if (defaultParams.AdmId) sPatNo = defaultParams.AdmId || "";
		if (!sPatNo) sPatNo = defaultParams.PatNo || "";
		if (!sPatNo) {
			result1.msg = "传入的患者病历号信息为空!";
			if (callback) callback(result1);
			return;
		}
		var url = bloodsconfig.getHisPatInfoUrl() + "?sPatNo=" + sPatNo;
		url += '&t=' + new Date().getTime();
		var layerIndex = layer.msg('提取患者信息中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		csserver.ajax({
			url: url
		}, function(data) {
			//layer.closeAll('loading');
			layer.close(layerIndex);
			var result = "";
			if (data && data.result && data.result.length > 0) result = JSON.parse(data.result[0]);
			if (result && result.data) result = result.data;

			if (result && result.length > 0) {
				var hisPatInfo = result[0];
				//可能HIS调用时,只传就诊号没传病历号,需要重新设置病历号参数值
				if (hisPatInfo.PatNo && hisPatInfo.PatNo != defaultParams.PatNo) {
					defaultParams.PatNo = hisPatInfo.PatNo;
				}
				//HisPatId值处理
				if (!defaultParams.HisPatId && hisPatInfo.PatID) {
					defaultParams.HisPatId = hisPatInfo.PatID;
				}

				//判断患者信息是否存在用血同意书
				var isAgree = hisPatInfo.IsAgree;
				if (!isAgree) isAgree = hisPatInfo.isagree;
				if (isAgree == true || isAgree == "1" || isAgree == "有" || isAgree == "true" || isAgree == "Y") {
					isAgree = true;
				}
				if (isAgree == true) {
					result1.isAdd = true;
					result1.isAgree = true;
				} else {
					result1.isAdd = false;
					result1.isAgree = false;
					result1.msg = '获取病人信息没有获取用血同意书信息!';
				}

				//判断医生是否已开了相关医嘱
				if (result1.isAdd == true) {
					var isOrder = hisPatInfo.IsOrder;
					if (isOrder == true || isOrder == "1" || isOrder == "有" || isOrder == "true" || isOrder == "Y") {
						isOrder = true;
					}
					if (isOrder == true) {
						result1.isAdd = true;
						result1.isOrder = true;
					} else {
						result1.isAdd = false;
						result1.isAgree = false;
						result1.msg += '该患者尚未进行输血前感染项目检查，请按规定开立医嘱。医嘱套名称：输血前感染8项（术前三项（丙/艾/梅）和乙肝三系(定量)(免疫学检验)）!';
					}
				}

				//判断医生已经有相关医嘱,但检验标本是否已采集
				if (result1.isAdd == true) {
					var isLabNoC = hisPatInfo.IsLabNoC;
					if (isLabNoC == true || isLabNoC == "1" || isLabNoC == "有" || isLabNoC == "true" || isLabNoC == "Y") {
						isLabNoC = true;
					}
					if (result1.isOrder == true && isLabNoC == true) {
						result1.isAdd = true;
						result1.isLabNoC = true;
					} else {
						result1.isAdd = false;
						result1.isLabNoC = false;
						result1.msg += '该患者的输血前感染项目检验标本尚未采集，请先完成采集状态。未采集医嘱是：术前三项（丙/艾/梅）和乙肝三系(定量)(免疫学检验) 标本未采集!';
					}
				}
			} else {
				result1.isAdd = false;
				result1.msg = '获取病人信息失败!' + data.msg;
			}
			if (callback) callback(result1);
		});
	};
	/**
	 * @description 查看配血记录或发血记录
	 * @param {Object} type 类型(1:配血记录;2:发血记录;)
	 */
	function onOpenRecords(type, data) {
		var id = data["BloodBReqForm_Id"];
		var url = '../previewpdf/index.html?';
		var title = "查看配血记录";
		if (type == "2") {
			title = "查看发血记录";
			//url = '../bloodpdf/index.html?';
		}

		var params = [];
		params.push("reqFormNo=" + id);
		params.push("type=" + type);
		params.push("t=" + new Date().getTime());
		url = url + params.join("&");
		parent.layer.open({
			type: 2,
			title: title,
			//skin: 'to-fix-select',
			area: ['100%', '100%'],
			content: url,
			id: "table-OpenRecords",
			btn: null
		});
	};
});
