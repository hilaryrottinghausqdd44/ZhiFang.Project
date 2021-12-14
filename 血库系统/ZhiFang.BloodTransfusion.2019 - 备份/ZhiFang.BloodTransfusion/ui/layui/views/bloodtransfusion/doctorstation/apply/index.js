/**
	@name：医嘱申请
	@author：longfc
	@version 2019-06-21
 */
layui.extend({
	uxutil: 'ux/util',
	uxdata: "ux/data",
	dataadapter: 'ux/dataadapter',
	cachedata: '/views/modules/bloodtransfusion/cachedata',
	bloodsconfig: '/config/bloodsconfig',
	runParams: '/config/runParams',
	csserver: '/views/interface/csserver',
	clodopprint: '/ux/other/lodop/clodopprint',
	bloodSelectData: '/views/modules/bloodtransfusion/bloodSelectData',
	bloodBreqSearchForm: '/views/bloodtransfusion/doctorstation/basic/bloodBreqSearchForm',
	breqFormApplyTable: '/views/bloodtransfusion/doctorstation/apply/breqFormApplyTable',
	breqItemTable: '/views/bloodtransfusion/doctorstation/basic/breqItemTable',
	breqFormResultTable: '/views/bloodtransfusion/doctorstation/basic/breqFormResultTable'
}).use(['uxutil', 'dataadapter', 'table', 'layer', 'form', 'uxdata', "cachedata", 'bloodsconfig', 'runParams',
	'csserver', 'bloodBreqSearchForm', 'breqFormApplyTable', 'breqItemTable', 'breqFormResultTable', 'bloodSelectData',
	'clodopprint'
], function() {
	"use strict";

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
	var breqFormTable = layui.breqFormApplyTable;
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
	//用血说明信息
	var bloodUseDesc = "";

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
	/**获取用血说明信息*/
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
	/**初始化表单信息*/
	function initForm() {
		//查询条件默认值
		var patNo = curRowInfo.AdmId;
		if (!patNo) patNo = curRowInfo.PatNo;
		if (patNo) $("#LAY-app-table-BloodBreqForm-Search-LikeSearch").val(patNo);
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
		add: function() {
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
		},
		edit: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				var curRow = data[0];
				var statusId = "" + curRow["BloodBReqForm_BreqStatusID"];
				//暂存,上级审核退回可以继续编辑
				if (statusId != "1" && statusId != "4") {
					var breqStatusName = "" + curRow["BloodBReqForm_BreqStatusName"];
					layer.alert("当前用库申请单状态为:" + breqStatusName + ",不能修改!", {
						icon: 5,
						btn: ['关闭'],
						time: 0
					});
					return;
				}
				bloodBreqSearchForm.openForm(curRowInfo, "edit", data[0], function() {
					//获取医嘱申请的保存处理结果
					var success = cachedata.getCache("breqEditFormSave");
					if (success.success == true) {
						onRefreshBreqFormTable();
					}
				});
			} else {
				layer.alert('请选择用库申请单后再操作!', {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
			}
		},
		confirmApply: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				breqFormTable1.onConfirmApply(data[0], function(result) {
					if (result.success == true) {
						onRefreshBreqFormTable();
					}
				});
			} else {
				layer.alert('请选择用库申请单后再操作!', {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
			}
		},
		refresh: function() {
			initBreqFormTable();
		},
		search: function() {
			onRefreshBreqFormTable();
		},
		reupload: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				onReUpload(data[0], function(result) {
					if (result.success == true) {
						onRefreshBreqFormTable();
					}
				});
			} else {
				layer.alert('请选择用库申请单后再操作!', {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
			}
		},
		printData: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				openPreviewPDF(data[0]);
			} else {
				layer.alert('请选择用库申请单后再操作!', {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
			}
		},
		directPrint: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data;
			if (data.length == 1) {
				onDirectPrint(data[0]);
			} else {
				layer.alert('请选择用库申请单后再操作!', {
					icon: 5,
					btn: ['关闭'],
					time: 0
				});
			}
		},
		delete: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data; //得到选中的数据
			if (data.length === 0) {
				return layer.msg('请选择数据');
			}
			layer.confirm('确定要删除当前用库申请单吗？', function(index) {
				layer.close(index);
				onDelete(data[0]);
			});
		},
		obsolete: function() {
			var checkStatus = table.checkStatus("LAY-app-table-BloodBreqForm");
			var data = checkStatus.data; //得到选中的数据
			if (data.length === 0) {
				return layer.msg('请选择数据');
			}
			layer.confirm('确定要作废当前用库申请单吗？', function(index) {
				layer.close(index);
				onObsolete(data[0]);
			});
		}
	};
	/**重新上传申请单给HIS*/
	function onReUpload(curRow, callback) {
		var reqFormId = curRow["BloodBReqForm_Id"];
		var statusId = curRow["BloodBReqForm_BreqStatusID"];
		var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"]; //审批完成标志
		var content = "";
		if (checkCompleteFlag == "0" || checkCompleteFlag.toLowerCase() == "false") {
			content = '<div style="padding: 20px 10px;">当前医嘱未审批完成,不能进行上传操作!</div>';
		}
		if (content == "") {
			var toHisFlag = "" + curRow["BloodBReqForm_ToHisFlag"]; //HIS数据标志
			if (toHisFlag == "1") {
				content = '<div style="padding: 20px 10px;">当前医嘱申请已上传完成!</div>';
			}
		}
		if (content.length > 0) {
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
		if (bloodsconfig.HisInterface.ISTOHISDATA == true) {
			breqFormTable1.onToHisData(curRow, function(result) {
				if (callback) callback(result);
			});
		}
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
	/**物理删除处理*/
	function onDelete(data) {
		breqFormTable1.onDelete(data, function(result) {
			if (result.success == true) onRefreshBreqFormTable()
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
	};
	var breqFormTable1 = null;
	//申请主单列表配置信息
	function getBreqFormTableConfig() {
		//列表高度=申请明细信息列表高度-查询表单高度
		var height1 = reqheight + 30;
		//默认条件
		var defaultWhere = "";
		var arr = [];
		if (curRowInfo.PatNo) {
			//arr.push("bloodbreqform.PatNo='" + curRowInfo.PatNo + "'");
		}
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
			setTimeout(function() {
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
	//初始化页面组件
	function initAll() {
		//新增表单的下拉字典数据预加载
		bloodSelectData.loadAllDict();
		initDefaultParams();
		initForm();
		onSearchForm();
		initTable();
		//初始化系统运行参数
		initRunParams();
	}
	//初始化系统运行参数
	function initRunParams() {
		runParams.initRunParams(function() {
			bloodsconfig=runParams.renderBloodsconfig(bloodsconfig);
		});
	}
	//访问功能页面验证
	function initVerification(callback) {
		if (!sysCurUserInfo) {
			var params = "获取当前操作的医生信息为空!";
			var href = "/layui/views/bloodtransfusion/sysprompt/index.html?type=2";
			href = href + "&t=" + new Date().getTime() + "&info=" + params;
			location.href = uxutil.path.UI + href;
		}
		if (callback) callback();
	}
	initVerification(function() {
		initAll();
	});

	function initPrintInfo() {

	};
	//医嘱申请PDF预览
	function openPreviewPDF(data) {
		var id = data["BloodBReqForm_Id"];
		var url = uxutil.path.ROOT + "/BloodTransfusionManageService.svc/BT_UDTO_SearchBusinessReportOfPdfById";
		var arr = [];
		arr.push("id=" + id);
		arr.push("reaReportClass=Frx"); //Frx模板
		arr.push("breportType=1"); //医嘱申请
		arr.push("operateType=1"); //直接打开文件
		arr.push("frx="); //用库申请单.frx
		arr.push("t=" + new Date().getTime()); //用库申请单.frx

		url = url + "?" + arr.join("&");
		//
		doPrint(url);
	};
	/***
	 * window.open方式预览及打印
	 * 谷歌及火狐可以进行预览及显示打印,IE只能预览,IE需要右键菜单后才能打印
	 * @param {Object} url
	 */
	function doPrint(url) {
		var height = $(document).height() - 10;
		var width = $(document).width() - 10;
		var wind = window.open(url, 'newwindow', 'height=' + height + ', width=' + width +
			', top=5, left=0, toolbar=no, menubar=yes, scrollbars=no, resizable=no,location=n o, status=no');
		//wind.print();
	};
	/***
	 * 新增医嘱申请
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
						if (result.success == true) {
							//需要自动上传
							if (result.autoupload && result.id) {
								breqFormTable1.LoadById(result.id, function(data) {
									onReUpload(data, function(result2) {
										if (result2.success == true) {
											onRefreshBreqFormTable();
										}
									})
								})
							} else {
								onRefreshBreqFormTable();
							}
						}
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
	/***
	 * 新增医嘱申请时,按患者就诊号或病历号调用CS服务获取HIS病人信息,判断验证：
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
});
