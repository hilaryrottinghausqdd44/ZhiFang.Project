/**
	@name：医嘱申请-医务处审批
	@author：longfc
	@version 2019-07-06
 */
layui.extend({
	uxutil: 'ux/util',
	dataadapter: 'ux/dataadapter',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	runParams: '/config/runParams',
	bloodsconfig: '/config/bloodsconfig',
	bloodBreqSearchForm: '/views/bloodtransfusion/doctorstation/basic/bloodBreqSearchForm',
	breqFormMedicalTable: '/views/bloodtransfusion/doctorstation/medical/breqFormMedicalTable',
	breqItemTable: '/views/bloodtransfusion/doctorstation/basic/breqItemTable',
	breqFormResultTable: '/views/bloodtransfusion/doctorstation/basic/breqFormResultTable',
}).use(['uxutil', 'layer', 'form', 'table', 'dataadapter', 'cachedata', 'runParams', 'bloodsconfig', 'bloodBreqSearchForm','breqFormMedicalTable', 'breqItemTable', 'breqFormResultTable'
], function() {
	"use strict";

	//Jcall 20191205 #start#
	//按钮是否可点击
	var BUTTON_CAN_CLICK = true;
	//Jcall 20191205 #end#

	var $ = layui.jquery;
	var uxutil = layui.uxutil;
	var layer = layui.layer;
	var form = layui.form;
	var table = layui.table;

	var bloodBreqSearchForm = layui.bloodBreqSearchForm;
	var breqFormTable = layui.breqFormMedicalTable;
	var breqItemTable = layui.breqItemTable;
	var breqFormResultTable = layui.breqFormResultTable;
	
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
	//正在上传HIS中的申请单号数组
	var toHisArr = [];

	//医生审核等级用血量阀值范围
	var docGrade = {
		LowLimit: 1601,
		UpperLimit: 1000000000
	};
	/**默认传入参数*/
	var defaultParams = {
		HisDeptId: "", //HIS医嘱申请科室Id		
		DeptId: "", //医嘱申请科室Id
		HisDoctorId: "", //HIS医嘱申请医生Id
		DoctorId: "", //医嘱申请医生Id
		AdmId: "", //His的就诊号
		HisPatId: "", //HIS医嘱申请患者Id		
		PatNo: "", //医嘱申请患者住院号
		CName: "" //患者姓名
	};
	//申请主单列表当前行选择信息
	var curRowInfo = {
		HisDeptId: "", //HIS医嘱申请科室Id		
		DeptId: "", //医嘱申请科室Id
		HisDoctorId: "", //HIS医嘱申请医生Id
		DoctorId: "", //医嘱申请医生Id
		AdmId: "", //His的就诊号
		HisPatId: "", //HIS医嘱申请患者Id		
		PatNo: "", //医嘱申请患者住院号
		CName: "" //患者姓名
	};
	//初始化默认传入参数信息
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
	/**初始化表单信息*/
	function initForm() {
		//查询条件默认值
		var patNo = curRowInfo.AdmId;
		if (!patNo) patNo = curRowInfo.PatNo;
		//if (patNo) $("#LAY-app-table-BloodBreqForm-Search-LikeSearch").val(patNo);
		//申请医生默认值

		bloodBreqSearchForm.initForm(function(type, value) {
			if (type == "date") {
				breqFormTable1.setRangeDateValue(value);
				onRefreshBreqFormTable();
			}
		});
		setHideSearchItems();
	};
	/**
	 * 按系统配置项设置用血申请列表查询项的显示/隐藏
	 */
	function setHideSearchItems() {
		//用血申请+,是否隐藏科室查询项
		var deptDiv = $('[lay-filter="div_filter_BloodBReqForm_Dept"]');
		if (bloodsconfig.HisInterface.ISHIDEPTNOOFSEARCH == true) {
			deptDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			deptDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
		//用血申请+,是否隐藏医生查询项
		var doctorDiv = $('[lay-filter="div_filter_BloodBReqForm_Doctor"]');
		if (bloodsconfig.HisInterface.ISHIDEDOCTORNOOFSEARCH == true) {
			doctorDiv.removeClass("layui-inline layui-show").addClass("layui-inline layui-hide");
		} else {
			doctorDiv.removeClass("layui-inline layui-hide").addClass("layui-inline layui-show");
		}
	};
	//申请主单按钮事件联动
	var onDocActive = {
		pass: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				ondocGrade(data[0], function(result) {
					if (result == true) onPass(data[0]);
				});
			} else {
				layer.alert('请选中用血申请单后再操作!');
			}
		},
		return: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				ondocGrade(data[0], function(result) {
					if (result == true) onReturn(data[0]);
				});
			} else {
				layer.alert('请选中用血申请单后再操作!');
			}
		},
		refresh: function() {
			initBreqFormTable();
		},
		search: function() {
			onRefreshBreqFormTable();
		},
		obsolete: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length === 0) {
				return layer.alert('请选择数据');
			}
			layer.confirm('确定要作废当前用血申请单吗？', function(index) {
				layer.close(index);
				onObsolete(data[0]);
			});
		}
	};
	/***
	 * 医生审核等级验证处理 医务处审批的用血申请量范围为(>1600
	 * @param {Object} curRow
	 * @param {Object} callback
	 */
	function ondocGrade(curRow, callback) {
		if (!sysCurUserInfo) {
			layer.alert('获取当前医生信息为空,不能操作!', {
				icon: 5,
				btn: ['关闭']
			});
			return callback(false);
		}
		var lowLimit = sysCurUserInfo.LowLimit;
		var upperLimit = sysCurUserInfo.UpperLimit;
		if (!lowLimit) lowLimit = 0;
		if (!upperLimit) upperLimit = 0;
		upperLimit = parseFloat(upperLimit);
		lowLimit = parseFloat(lowLimit);
		if (upperLimit < docGrade.LowLimit) {
			layer.alert('当前医生审核的用血申请量范围为:' + lowLimit + '~' + upperLimit + ',不能审核!', {
				btn: ['关闭'],
				icon: 5
			});
			return callback(false);
		}
		return callback(true);
	};
	/**审核通过处理*/
	function onPass(curRow) {
		if (!BUTTON_CAN_CLICK) return;

		var reqFormId = "" + curRow["BloodBReqForm_Id"];
		var statusId = "" + curRow["BloodBReqForm_BreqStatusID"];
		//科主任审核通过可以进行 医务处审批
		if (statusId != "5") {
			BUTTON_CAN_CLICK = true;
			var breqStatusName = "" + curRow["BloodBReqForm_BreqStatusName"];
			layer.alert("当前用血申请单状态为:" + breqStatusName + ",不能修改!", {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
			return;
		}
		var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"];
		if (checkCompleteFlag == "1" || checkCompleteFlag.toLowerCase() == "true") {
			BUTTON_CAN_CLICK = true;
			layer.alert("当前用血申请单已审批完成,请不要重复操作!", {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
			return;
		}
		//判断申请单是否正在上传中
		if (toHisArr.indexOf(reqFormId) >= 0) {
			BUTTON_CAN_CLICK = true;
			layer.msg("当前用血申请单正在上传中", {
				time: 2000
			});
			return;
		}
		BUTTON_CAN_CLICK = false;
		/* var layerIndex = layer.msg('审核处理中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		}); */
		onReview(curRow, 7, function(result) {
			//layer.close(layerIndex);
			BUTTON_CAN_CLICK = true;

			//判断审核的用血申请单是否完成,医嘱申请完成后,自动调用服务返回医嘱申请信息给HIS
			if (result.success == true) {
				BUTTON_CAN_CLICK = true;
				onRefreshBreqFormTable();
			} else {
				//Jcall 20191205 #start#
				BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
				//Jcall 20191205 #end#
			}
		});
	};
	/**审核不通过处理*/
	function onReturn(curRow) {
		if (!BUTTON_CAN_CLICK) return;

		var statusId = "" + curRow["BloodBReqForm_BreqStatusID"];
		//科主任审核通过可以进行 医务处审批
		if (statusId != "5") {
			BUTTON_CAN_CLICK = true;
			var breqStatusName = "" + curRow["BloodBReqForm_BreqStatusName"];
			layer.alert("当前用血申请单状态为:" + breqStatusName + ",不能修改!", {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
			return;
		}
		var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"];
		if (checkCompleteFlag == "1" || checkCompleteFlag.toLowerCase() == "true") {
			BUTTON_CAN_CLICK = true;
			layer.alert("当前用血申请单已审批完成,请不要重复操作!", {
				icon: 5,
				btn: ['关闭'],
				time: 0
			});
			return;
		}
		BUTTON_CAN_CLICK = false;
		onReview(curRow, 8, function(result) {
			//Jcall 20191205 #start#
			BUTTON_CAN_CLICK = true; //所有按钮置为可点击状态
			//Jcall 20191205 #end#
			if (result.success == true) onRefreshBreqFormTable();
		});
	};
	/**审核处理*/
	function onReview(curRow, statusID, callback) {
		//页面层
		parent.layer.open({
			type: 1,
			title: "医科处审批",
			skin: 'layui-layer-rim', //加上边框
			area: ['420px', '225px'], //宽高
			content: $('#LAY-app-form_formPassPass'),
			btn: ['确定', '关闭'],
			yes: function(index, layero) {
				//按钮【确定】的回调
				var content = $('#LAY-app-form_txtPass').val();
				var reqFormId = curRow["BloodBReqForm_Id"];
				var statusId = curRow["BloodBReqForm_BreqStatusID"];
				var reqTotal = curRow["BloodBReqForm_ReqTotal"];
				var userInfo = bloodsconfig.getCurOper();
				var empID = userInfo.empID;
				var empName = userInfo.empName;
				var entity = {
					"Id": reqFormId,
					"BreqStatusID": statusID,
					"ReqTotal": reqTotal,
					"MedicalID": empID,
					"MedicalName": empName,
					"MedicalMemo": content
				};

				//Jcall 20191205 #start#				
				BUTTON_CAN_CLICK = false; //所有按钮置为不可点击状态
				//Jcall 20191205 #end#
				//Jcall 20200113 #start#
				var layerIndex = layer.msg('审核处理中', {
					time: 0,
					icon: 16,
					shade: 0.5
				});
				//Jcall 20200113 #start#
				breqFormTable1.onReview(entity, function(result) {
					BUTTON_CAN_CLICK = true;
					layer.close(index);
					//Jcall 20200113 #start#
					if (layerIndex != null) {
						layer.close(layerIndex);
					}
					//Jcall 20200113 #start#
					if (callback) callback(result);
				});
			},
			btn2: function(index, layero) {
				//按钮【取消】的回调
				BUTTON_CAN_CLICK = true;
				layer.close(index);
				//layer.closeAll();
			}
		});
	};
	/**初始化作废原因下拉选择框*/
	function initObsoleteInfo(callback) {
		bloodSelectData.dictList.BReqFormObsolete(function(html) {
			$("#BloodBReqForm_ObsoleteMemoId").html(html);
			//重新渲染select
			form.render('select');
			if (callback) callback(html);
		});
	};
	/**作废处理*/
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
			layer.open({
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
	/**查询表单事件监听*/
	function onSearchForm() {
		$('.layui-form .layui-form-item .layui-inline .layui-btn').on('click', function() {
			var type = $(this).data('type');
			onDocActive[type] ? onDocActive[type].call(this) : '';
		});
		$('#LAY-app-table-BloodBreqForm-Search-LikeSearch').on('keydown', function(event) {
			if (event.keyCode == 13) onRefreshBreqFormTable();
		});
		//申请状态查询
		form.on('select(bloodbreqform_filter_breqformstatus)', function(data) {
			onRefreshBreqFormTable();
		});
	};

	function setCurRow(objRow) {
		curRowInfo["ReqFormNo"] = objRow["BloodBReqForm_Id"];
		curRowInfo["HisDoctorId"] = objRow["BloodBReqForm_HisDoctorId"];
		curRowInfo["HisDeptId"] = objRow["BloodBReqForm_HisDeptID"];
		curRowInfo["DoctorId"] = objRow["BloodBReqForm_DoctorNo"];
		curRowInfo["DeptId"] = objRow["BloodBReqForm_DeptNo"];
		curRowInfo["AdmId"] = objRow["BloodBReqForm_AdmID"];
		curRowInfo["HisPatId"] = objRow["BloodBReqForm_PatID"];
		curRowInfo["PatNo"] = objRow["BloodBReqForm_PatNo"];
		curRowInfo["CName"] = objRow["BloodBReqForm_CName"];
	};
	var breqFormTable1 = null;
	//申请主单列表配置信息
	function getBreqFormTableConfig() {
		//列表高度=申请明细信息列表高度-查询表单高度
		var height1 = reqheight + 30;
		var defaultWhere = [];
		/* //医嘱状态不等于暂存
		defaultWhere.push("bloodbreqform.BreqStatusID!=1");
		//医嘱状态不等于提交申请
		defaultWhere.push("bloodbreqform.BreqStatusID!=2");
		//医嘱状态不等于上级审核通过
		defaultWhere.push("bloodbreqform.BreqStatusID!=3");
		//医嘱状态不等于上级审核退回
		defaultWhere.push("bloodbreqform.BreqStatusID!=4");
		//医嘱状态不等于科主任审核退回
		defaultWhere.push("bloodbreqform.BreqStatusID!=6"); */

		//同一患者一天内(上级审核通过)申请备血量达到或超过1600毫升的
		defaultWhere.push("bloodbreqform.ReqTotal>=1600");
		defaultWhere = "(" + defaultWhere.join(" and ") + ")";
		return {
			title: '申请主单信息',
			height: height1,
			elem: '#LAY-app-table-BloodBreqForm',
			id: "LAY-app-table-BloodBreqForm",
			toolbar: "",
			defaultToolbar: null,
			/**默认数据条件*/
			defaultWhere: defaultWhere,
			externalWhere: ""
		};
	};
	var breqItemTable1 = null;
	//申请明细列表配置信息
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
	//申请主单列表配置信息
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
	//申请主单列表监听
	function onBloodBreqFormTable() {
		//监听工具条
		table.on('tool(LAY-app-table-BloodBreqForm)', function(obj) {
			var data = obj.data;
			if (obj.event === 'editBloodBreqForm') {
				bloodBreqSearchForm.openForm("edit", data["BloodBReqForm_Id"], function(success) {
					if (success == true) {
						onRefreshBreqFormTable();
					}
				});
			}
		});
		//监听行单击事件
		var check_row = null;
		table.on('row(LAY-app-table-BloodBreqForm)', function(obj) {
			//Jcall 20191205 #start#行点击时判断该条数据是否能进行“通过/退回”处理
			//BloodBReqForm_ToHisFlag 0未处理;1上传成功;2上传失败;null未处理
			if (obj.data.BloodBReqForm_ToHisFlag == "1") {
				$("#button_pass").hide();
				$("#button_apply").hide();
			} else {
				$("#button_pass").show();
				$("#button_apply").show();
			}
			//Jcall 20191205 #end#

			//各明细列表默认条件改变
			setCurRow(obj.data);
			//设置当前行为选中状态
			breqFormTable1.setRadioCheck(obj);
			setTimeout(function() {
				setReviewInfo(obj);
				onRefreshDtlTable(obj);
			}, 300);
		});
		//监听行双击事件
		table.on('rowDouble(LAY-app-table-BloodBreqForm)', function(obj) {
			//是否弹出编辑或查看?
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

	//刷新申请主单列表
	function onRefreshBreqFormTable() {
		//获取查询条件
		breqFormTable1.getSearchWhere();
		table.reload(breqFormTable1.config.id, breqFormTable1.config);
	};
	//申请主单联动刷新明细列表
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
	//检验结果列表初始化
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
	//初始化列表
	function initTable() {
		initBreqFormTable();
		setTimeout(function() {
			//申请明细列表初始化
			initBreqItemTable();
			//检验结果列表初始化
			initbreqFormResultTable();
		}, 500);
	};
	//行选择改变后,赋值审核提示信息
	function setReviewInfo(obj) {
		var filter = "LAY-app-form-ReviewInfo";
		var flag = obj.data["BloodBReqForm_CheckCompleteFlag"];
		if (flag == "1" || flag == "true") {
			flag = "1";
		} else {
			flag = "0";
		}
		obj.data["BloodBReqForm_CheckCompleteFlag"] = flag;
		form.val(filter, obj.data);
	};
	//打开审核提示信息
	function openReviewWin() {
		layer.open({
			type: 1,
			title: "审核信息",
			skin: 'layui-layer-rim', //加上边框
			area: ['300px', '96%'], //宽高
			shade: 0,
			shadeClose: true,
			offset: 'rb', //快捷设置右下角
			content: $('#LAY-app-form-ReviewInfo')
		});
	};
	//初始化页面组件
	function initAll() {
		initDefaultParams();
		initForm();
		onSearchForm();
		initTable();
		openReviewWin();
		//初始化系统运行参数
		initRunParams();
	}
	//初始化系统运行参数
	function initRunParams() {
		runParams.initRunParams(function() {
			bloodsconfig=runParams.renderBloodsconfig(bloodsconfig);
		});
	}
	/***
	 * 访问功能页面验证 需要具有医务处审核等级权限的医生才能使用
	 * @param {Object} callback
	 */
	function initVerification(callback) {
		var info = [];
		var params = uxutil.params.get();
		var isNeedV = true;
		//是否需要验证审批权限
		if (params["isNeedV"]) isNeedV = params["isNeedV"];
		if (!isNeedV || isNeedV == "false") {
			if (callback) return callback();
		}

		if (!sysCurUserInfo) {
			info.push("获取当前操作的医生信息为空!");
		} else {
			//当前医生的用血量审核阀值范围
			var lowLimit = sysCurUserInfo.LowLimit;
			var upperLimit = sysCurUserInfo.UpperLimit;
			if (!lowLimit) lowLimit = 0;
			if (!upperLimit) upperLimit = 0;
			upperLimit = parseFloat(upperLimit);
			lowLimit = parseFloat(lowLimit);
			if (upperLimit < docGrade.LowLimit) {
				//医务处审批的用血申请量范围为>1600
				info.push('当前医生审核的用血申请量范围为:' + lowLimit + '~' + upperLimit + ',不能进行医务处审批操作!');
			}
		}

		if (info.length > 0) {
			var href = "/layui/views/bloodtransfusion/sysprompt/index.html?type=2";
			href = href + "&t=" + new Date().getTime() + "&info=" + info.join("<br />");
			location.href = uxutil.path.UI + href;
		}

		if (callback) callback();
	}
	initVerification(function() {

		initAll();
	});
});
