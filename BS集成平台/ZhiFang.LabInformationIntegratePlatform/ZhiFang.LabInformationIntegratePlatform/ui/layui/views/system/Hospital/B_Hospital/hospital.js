/**
 * 医院字典
 * @author guohaixiang
 * @version 2019-12-11
 */

layui.extend({
	uxutil: 'ux/util',
	treeSelect: "../src/layui/plugIn/treeSelect/treeSelect"
}).use(['uxutil', 'table', 'form','treeSelect'], function() {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		treeSelect = layui.treeSelect,
		$ = layui.jquery;
	var treeData = null;
	var areaListOption = null;
	//表格	
	var tableObj = {
		table: layui.table,
		form: layui.form,
		fields: {
			Id: "BHospital_Id",
			IsUse: 'BHospital_IsUse',
			IsBloodSamplingPoint: 'BHospital_IsBloodSamplingPoint',
			IsReceiveSamplePoint: 'BHospital_IsReceiveSamplePoint',
			IsCooperation:'BHospital_IsCooperation'
		},
		current: null,
		delIndex: null,
		addUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_AddBHospital',
		selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',
		updateUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalByField',
		delUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospital',
		searchTreeGridBHospitalAreaUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchTreeGridBHospitalAreaByHQL?isPlanish=true&sort=[{property:"BHospitalArea_DispOrder",direction:"ASC"}]&where=IsUse=1 and PHospitalAreaID is null',
		checkRowData: [], //选中数据
		refresh: function() {
			var searchText = $("#search")[0].value;
			var areaId = $("#searchArea").val();
			tableObj.table.reload('table', {
				where: {
					time: new Date().getTime()
				}
			});
			tableObj.checkRowData = [];
			if(searchText != "") {
				$("#search").val(searchText);
			}
			$("#searchArea").html(areaListOption);
			if (areaId) {
				$("#searchArea").val(areaId);;
			}
			formObj.form.render('select'); //需要渲染一下;		
		}
	};
	//表单
	var formObj = {
		form: layui.form,
		type: 'edit',
		reset: function() { //重置
			if(formObj.type == 'edit') {
				if(tableObj.checkRowData.length > 0) {
					formObj.form.val("form", tableObj.checkRowData[tableObj.checkRowData.length - 1]);
					if (tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospital_AreaID && areaListOption.indexOf(tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospital_AreaID) != -1) {
						treeSelect.checkNode('AreaID', tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospital_AreaID);
					} else {
						treeSelect.revokeNode('AreaID', function (d) {});
					}
					if(tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospital_IsUse == "true") {
						if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
							$("#isUse").next('.layui-form-switch').children("em").html("启用");
							$("#isUse")[0].checked = true;
						}
					} else {
						if($("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#isUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
							$("#isUse").next('.layui-form-switch').children("em").html("禁用");
							$("#isUse")[0].checked = false;
						}
					}
					if(tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospital_IsBloodSamplingPoint == "true") {
						if(!$("#IsBloodSamplingPoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#IsBloodSamplingPoint").next('.layui-form-switch').addClass('layui-form-onswitch');
							$("#IsBloodSamplingPoint").next('.layui-form-switch').children("em").html("启用");
							$("#IsBloodSamplingPoint")[0].checked = true;
						}
					} else {
						if($("#IsBloodSamplingPoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#IsBloodSamplingPoint").next('.layui-form-switch').removeClass('layui-form-onswitch');
							$("#IsBloodSamplingPoint").next('.layui-form-switch').children("em").html("禁用");
							$("#IsBloodSamplingPoint")[0].checked = false;
						}
					}
					if(tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospital_IsReceiveSamplePoint == "true") {
						if(!$("#IsReceiveSamplePoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#IsReceiveSamplePoint").next('.layui-form-switch').addClass('layui-form-onswitch');
							$("#IsReceiveSamplePoint").next('.layui-form-switch').children("em").html("启用");
							$("#IsReceiveSamplePoint")[0].checked = true;
						}
					} else {
						if($("#IsReceiveSamplePoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#IsReceiveSamplePoint").next('.layui-form-switch').removeClass('layui-form-onswitch');
							$("#IsReceiveSamplePoint").next('.layui-form-switch').children("em").html("禁用");
							$("#IsReceiveSamplePoint")[0].checked = false;
						}
					}
					if (tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospital_IsCooperation == "true") {
						if (!$("#IsCooperation").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#IsCooperation").next('.layui-form-switch').addClass('layui-form-onswitch');
							$("#IsCooperation").next('.layui-form-switch').children("em").html("启用");
							$("#IsCooperation")[0].checked = true;
						}
					} else {
						if ($("#IsCooperation").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
							$("#IsCooperation").next('.layui-form-switch').removeClass('layui-form-onswitch');
							$("#IsCooperation").next('.layui-form-switch').children("em").html("禁用");
							$("#IsCooperation")[0].checked = false;
						}
					}
				}
			} else {
				$("#layForm")[0].reset();
				if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
					$("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
					$("#isUse").next('.layui-form-switch').children("em").html("启用");
					$("#isUse")[0].checked = true;
				}
				if(!$("#IsBloodSamplingPoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
					$("#IsBloodSamplingPoint").next('.layui-form-switch').addClass('layui-form-onswitch');
					$("#IsBloodSamplingPoint").next('.layui-form-switch').children("em").html("启用");
					$("#IsBloodSamplingPoint")[0].checked = true;
				}
				if(!$("#IsReceiveSamplePoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
					$("#IsReceiveSamplePoint").next('.layui-form-switch').addClass('layui-form-onswitch');
					$("#IsReceiveSamplePoint").next('.layui-form-switch').children("em").html("启用");
					$("#IsReceiveSamplePoint")[0].checked = true;
				}
				if (!$("#IsCooperation").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
					$("#IsCooperation").next('.layui-form-switch').addClass('layui-form-onswitch');
					$("#IsCooperation").next('.layui-form-switch').children("em").html("启用");
					$("#IsCooperation")[0].checked = true;
				}
			}
		},
		empty: function() { //清空
			$("#layForm")[0].reset();
			if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
				$("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
				$("#isUse").next('.layui-form-switch').children("em").html("启用");
				$("#isUse")[0].checked = true;
			}
			if(!$("#IsBloodSamplingPoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
				$("#IsBloodSamplingPoint").next('.layui-form-switch').addClass('layui-form-onswitch');
				$("#IsBloodSamplingPoint").next('.layui-form-switch').children("em").html("启用");
				$("#IsBloodSamplingPoint")[0].checked = true;
			}
			if(!$("#IsReceiveSamplePoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
				$("#IsReceiveSamplePoint").next('.layui-form-switch').addClass('layui-form-onswitch');
				$("#IsReceiveSamplePoint").next('.layui-form-switch').children("em").html("启用");
				$("#IsReceiveSamplePoint")[0].checked = true;
			}
			if (!$("#IsCooperation").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
				$("#IsCooperation").next('.layui-form-switch').addClass('layui-form-onswitch');
				$("#IsCooperation").next('.layui-form-switch').children("em").html("启用");
				$("#IsCooperation")[0].checked = true;
			}
		},
		disabledSaveBtn: function() {
			if(!$('#save').hasClass('layui-btn-disabled')) {
				$("#save").prop("disabled", "disabled");
				$('#save').addClass('layui-btn-disabled');
			}
		},
		enableSaveBtn: function() {
			if($('#save').hasClass('layui-btn-disabled')) {
				$("#save").removeProp("disabled");
				$('#save').removeClass('layui-btn-disabled');
			}
		}
	};
	init();

	function init() {
		var params = uxutil.params.get(false);
		if (params.IncludeBusiness == "1") {
			$("#setbusinessanalysis").show();
		} else {
			$("#setbusinessanalysis").hide();
		}
		$(".tableHeight").css("height", ($(window).height() - 30) + "px"); //设置表单容器高度
		searchTreeGridBHospitalArea();
	}
	//初始化表格
	function tableRender() {
		tableObj.table.render({
			elem: '#table',
			height: 'full-50',
			//size: 'sm', //小尺寸的表格
			defaultToolbar: ['filter'],
			toolbar: '#toolbar',
			url: tableObj.selectUrl,
			cols: [
				[{
						type: 'numbers',
						title: '序号'
					},
					{
						field: tableObj.fields.Id,
						width: 60,
						title: '主键ID',
						sort: true,
						hide: true
					},
					{
						field: 'BHospital_AreaID',
						title: '区域ID',
						minWidth: 130,
						sort: true,
						hide: true
					},
					{
						field: 'BHospital_LevelID',
						title: '医院级别ID',
						minWidth: 130,
						sort: true,
						hide: true
					},
					{
						field: 'BHospital_HTypeID',
						title: '医院类别ID',
						minWidth: 130,
						sort: true,
						hide: true
					},
					{
						field: 'BHospital_Name',
						title: '名称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'BHospital_EName',
						title: '英文名称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'BHospital_SName',
						title: '简称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'BHospital_Shortcode',
						title: '快捷码',
						minWidth: 130,
						//sort: true
					},
					{
						field: 'BHospital_PinYinZiTou',
						title: '汉字拼音字头',
						minWidth: 130,
						//sort: true
					},
					{
						field: 'BHospital_Comment',
						title: '备注',
						//sort: true,
						minWidth: 70
					},
					{
						field: 'BHospital_HospitalCode',
						title: '医院编码',
						minWidth: 130,
						//hide: true,
						sort: true
					},
					{
						field: 'BHospital_IconsID',
						title: '图标头像ID',
						minWidth: 100,
						hide: true,
						//sort: true
					},
					{
						field: 'BHospital_LevelName',
						title: '医院级别名称',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_HTypeName',
						title: '医院类别名称',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_Postion',
						title: '坐标位置',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_ProvinceName',
						title: '省份名称',
						minWidth: 100,
						sort: true
					},
					{
						field: 'BHospital_CityName',
						title: '城市名称',
						minWidth: 100,
						sort: true
					},
					{
						field: 'BHospital_CountyName',
						title: '区县名称',
						minWidth: 100,
						sort: true
					},
					{
						field: 'BHospital_IsBloodSamplingPoint',
						title: '是否采血点',
						minWidth: 100,
						// sort: true,
						templet: function(data) {
							var str = "";
							if(data.BHospital_IsBloodSamplingPoint.toString() == "true") {
								str = "<span style='color:red;'>是</span>";
							} else {
								str = "<span>否</span>";
							}
							return str;
						}
					}, {
						field: 'BHospital_IsReceiveSamplePoint',
						title: '是否接收标本',
						minWidth: 100,
						// sort: true,
						templet: function(data) {
							var str = "";
							if(data.BHospital_IsReceiveSamplePoint.toString() == "true") {
								str = "<span style='color:red;'>是</span>";
							} else {
								str = "<span>否</span>";
							}
							return str;
						}
					},{
						field: 'BHospital_IsCooperation',
						title: '是否合作共建',
						minWidth: 100,
						// sort: true,
						templet: function (data) {
							var str = "";
							if (data.BHospital_IsCooperation.toString() == "true") {
								str = "<span style='color:red;'>是</span>";
							} else {
								str = "<span>否</span>";
							}
							return str;
						}
					},
					{
						field: 'BHospital_ProvinceID',
						title: '省份ID',
						minWidth: 100,
						//sort: true,
						hide: true
					},
					{
						field: 'BHospital_CityID',
						title: '城市ID',
						minWidth: 100,
						//sort: true,
						hide: true
					},
					{
						field: 'BHospital_CountyID',
						title: '区县ID',
						minWidth: 100,
						//sort: true,
						hide: true
					},
					{
						field: 'BHospital_DealerID',
						title: '经销商ID',
						minWidth: 100,
						hide: true
						//sort: true
					},
					{
						field: 'BHospital_LinkMan',
						title: '联系人',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_LinkManPosition',
						title: '联系人职位',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_PhoneNum1',
						title: '电话1',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_PhoneNum2',
						title: '电话2',
						minWidth: 100
						//sort: true
					},
					{
						field: 'BHospital_Address',
						title: '地址',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_EMAIL',
						title: 'EMALL',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_MAILNO',
						title: '邮编',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_FaxNo',
						title: '传真',
						minWidth: 100,
						//sort: true
					},
					{
						field: 'BHospital_AutoFax',
						title: '自动传真',
						minWidth: 100,
						//sort: true
					},
					{
						field: tableObj.fields.IsUse,
						title: '是否使用',
						minWidth: 100,
						templet: '#switchTp',
						unresize: true
					}
				]
			],
			page: true,
			limit: 50,
			limits: [50, 100, 200, 500, 1000],
			autoSort: false, //禁用前端自动排序
			done: function(res, curr, count) {
				if(count > 0) {
					//$("#table+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
				}
			},
			response: function() {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function(res) { //res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			text: {
				none: '暂无相关数据'
			}
		});
		//区域-下拉框加载
		Arealist()
	}
	//监听table启用禁用/显示隐藏操作
	tableObj.form.on('switch(tableSwitch)', function(obj) {
		var name = obj.elem.name;
		var value = obj.elem.checked;
		var Id = obj.othis[0].parentElement.parentElement.parentElement.children[1].innerText;
		var loadIndex = layer.load(); //开启加载层
		$.ajax({
			type: 'post',
			dataType: 'json',
			contentType: "application/json",
			data: JSON.stringify({
				entity: {
					Id: Id,
					IsUse: value
				},
				fields: 'Id,IsUse'
			}),
			url: tableObj.updateUrl,
			success: function(res) {
				layer.close(loadIndex); //关闭加载层
				if(res.success) {
					//更新右边表单和本行数据
					var arr = tableObj.table.cache.table;
					for(var i in arr) {
						if(arr[i][tableObj.fields.Id] == Id) {
							arr[i][tableObj.fields.IsUse] = value.toString();
						}
					}
					if(tableObj.checkRowData.length > 0) {
						for(var j in tableObj.checkRowData) {
							if(tableObj.checkRowData[j][tableObj.fields.Id] == Id) {
								tableObj.checkRowData[j][tableObj.fields.IsUse] = value;
								if(j == tableObj.checkRowData.length - 1) {
									if(value) {
										if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
											$("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
											$("#isUse").next('.layui-form-switch').children("em").html("启用");
											$("#isUse")[0].checked = true;
										}
									} else {
										if($("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
											$("#isUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
											$("#isUse").next('.layui-form-switch').children("em").html("禁用");
											$("#isUse")[0].checked = false;
										}
									}
								}
							}
						}
					}
				} else {
					tableObj.refresh();
					if(value) {
						layer.msg("启用失败", {
							icon: 5,
							anim: 6
						});
					} else {
						layer.msg("禁用失败", {
							icon: 5,
							anim: 6
						});
					}
				}
			}
		});
		layui.stope(window.event);
	});
	//table上面的工具栏
	tableObj.table.on('toolbar(table)', function(obj) {
		switch(obj.event) {
			case 'add':
				if(tableObj.checkRowData.length > 0) {
					formObj.type = 'add'; //编辑还是新增
					formObj.reset();
					formObj.enableSaveBtn();
				} else {
					formObj.type = 'add'; //编辑还是新增
					formObj.enableSaveBtn();
				}
				break;
			case 'edit':
				if(tableObj.checkRowData.length === 0) {
					layer.msg('请选择一行！');
				} else {
					formObj.enableSaveBtn();
					formObj.type = 'edit'; //编辑还是新增
					//formObj.reset();
				}
				break;
			case 'del':
				if(tableObj.checkRowData.length === 0) {
					layer.msg('请选择一行！');
				} else {
					layer.confirm('确定删除选中项?', {
						icon: 3,
						title: '提示'
					}, function(index) {
						var loadIndex = layer.load(); //开启加载层
						var len = tableObj.checkRowData.length;
						for(var i = 0; i < tableObj.checkRowData.length; i++) {
							var Id = tableObj.checkRowData[i][tableObj.fields.Id];
							$.ajax({
								type: "get",
								url: tableObj.delUrl + "?Id=" + Id,
								async: true,
								dataType: 'json',
								success: function(res) {
									if(res.success) {
										len--;
										if(len == 0) {
											layer.close(loadIndex); //关闭加载层
											layer.close(index);
											layer.msg("删除成功！", {
												icon: 6,
												anim: 0
											});
											tableObj.refresh();
											formObj.disabledSaveBtn();
											formObj.empty();
										}
									} else {
										layer.msg("删除失败！", {
											icon: 5,
											anim: 6
										});
										tableObj.delIndex = null;
										layer.close(loadIndex); //关闭加载层
									}
								}
							});
						}
					});
				}
				break;
			case 'search':
				var url = tableObj.selectUrl;
				var val = $("#search")[0].value;
				var areaId = $("#searchArea").val();
				var where = "1=1"
				if (val != "") {
					where += " and Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
				}
				if (areaId) where += " and AreaID=" + areaId;
				
				tableObj.table.reload('table', {
					url: url + "&where=" + encodeURI(where),
					where: {
						time: new Date().getTime()
					}
				});
				tableObj.checkRowData = [];
				$("#search").val(val);
				$("#searchArea").html(areaListOption);
				if (areaId) {
					$("#searchArea").val(areaId);;
				}
				formObj.form.render('select'); //需要渲染一下;	
		};
	});
	//监听行单击事件
	tableObj.table.on('row(table)', function(obj) {
		tableObj.checkRowData = [];
		formObj.type = 'edit';
		tableObj.checkRowData.push(obj.data);
		//标注选中样式
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		formObj.reset();
		formObj.disabledSaveBtn();
	});
	//监听form表单
	formObj.form.on('submit(save)', function(data) {
		window.event.preventDefault();
		var loadIndex = layer.load(); //开启加载层
		var fields = ""; //发送修改的字段
		var postData = {}; //发送的数据
		delete data["field"]["AreaID"];
		//判断复选框是否选中 未选中的话手动添加字段
		if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
			data.field[tableObj.fields.IsUse] = "false"
		} else {
			data.field[tableObj.fields.IsUse] = "true"
		}
		//判断复选框是否选中 未选中的话手动添加字段
		if(!$("#IsBloodSamplingPoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
			data.field[tableObj.fields.IsBloodSamplingPoint] = "false"
		} else {
			data.field[tableObj.fields.IsBloodSamplingPoint] = "true"
		}
		//判断复选框是否选中 未选中的话手动添加字段
		if(!$("#IsReceiveSamplePoint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
			data.field[tableObj.fields.IsReceiveSamplePoint] = "false"
		} else {
			data.field[tableObj.fields.IsReceiveSamplePoint] = "true"
		}
		//判断复选框是否选中 未选中的话手动添加字段
		if (!$("#IsCooperation").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
			data.field[tableObj.fields.IsCooperation] = "false"
		} else {
			data.field[tableObj.fields.IsCooperation] = "true"
		}
		for(k in data.field) {
			if (k == "BHospital_IsBloodSamplingPoint" && data.field[k] == "") { }
			else if (k == "BHospital_EditerName" ) { }
			else if (k == "BHospital_DataUpdateTime") { }
			else if (k == "BHospital_IsReceiveSamplePoint" && data.field[k] == "") { }
			else if (k == "BHospital_IsCooperation" && data.field[k] == "") { }
			else if (k == "BHospital_AreaName" ) { }
			else if (k.indexOf("ID") != -1 && data.field[k] == "") {
				postData[k.split("_")[1]] = null;
				fields += k.split("_")[1] + ",";
			}
			else if (k == "BHospital_BusinessAnalysisDateType" && data.field[k] == "") { }
			else if (k == "BHospital_BranchId" && data.field[k] == "") { } 
			else if (k == "BHospital_BillNumber" && data.field[k] == "") { } 
			else if (k == "BHospital_PersonFixedCharges" && data.field[k] == "") { } 
			else {
				postData[k.split("_")[1]] = data.field[k];
				fields += k.split("_")[1] + ",";
			}
		}
		fields = fields.slice(0, fields.length - 1);
		if(formObj.type == 'edit') {
			$.ajax({
				type: 'post',
				dataType: 'json',
				contentType: "application/json",
				data: JSON.stringify({
					entity: postData,
					fields: fields
				}),
				url: tableObj.updateUrl,
				success: function(res) {
					layer.close(loadIndex); //关闭加载层
					if(res.success) {
						layer.msg("编辑成功！", {
							icon: 6,
							anim: 0
						});
						formObj.disabledSaveBtn();
						formObj.empty();
						tableObj.refresh();
					} else {
						layer.msg("编辑失败！", {
							icon: 5,
							anim: 6
						});
					}
				}
			});
		} else if(formObj.type == 'add') {
			delete postData.Id;
			$.ajax({
				type: 'post',
				dataType: 'json',
				contentType: "application/json",
				data: JSON.stringify({
					entity: postData
				}),
				url: tableObj.addUrl,
				success: function(res) {
					layer.close(loadIndex); //关闭加载层
					if(res.success) {
						layer.msg("新增成功！", {
							icon: 6,
							anim: 0
						});
						formObj.disabledSaveBtn();
						formObj.empty();
						var data = eval('(' + res.ResultDataValue + ')');
						tableObj.refresh();
					} else {
						layer.msg("新增失败！", {
							icon: 5,
							anim: 6
						});
					}
				}
			});
		}
	});
	//重置添加、修改不同操作
	$('#cancel').on('click', function() {
		if(tableObj.checkRowData.length == 0) {
			formObj.empty();
			return;
		}
		if(formObj.type == 'edit') {
			formObj.reset();
		} else if(formObj.type == 'add') {
			formObj.empty();
		}
	});
	//设置财务系统属性
	$('#setbusinessanalysis').on('click', function () {
		//阻止默认事件
		var device = layui.device();
		if (device.ie) {
			window.event.returnValue = false;
		} else {
			window.event.preventDefault();
		}
		var formData = $("#layForm").serialize().replace(/\+/g, " ").split("&");
		var data = {};
		$.each(formData, function (i, item) {
			data[item.split("=")[0]] = decodeURIComponent(item.split("=")[1]);
		});
		var where = "?";
			
		$.each(data, function (k, v) {
			if (k =="BHospital_Id") {
				if (v == null || v == "" || v == 0) {
					layer.msg("缺少医院ID,请添加!", {
						icon: 5,
						anim: 6
					});
					return false;
				} else {
					where += "HospitalID=" + v;
				}
			}
			if (k == "BHospital_HospitalCode") {
				if (v == null || v == "" || v == 0) {
					layer.msg("缺少医院编码,请添加!", {
						icon: 5,
						anim: 6
					});
					return false;
				} else {
					where += "&HospitalCode=" + v;
				}
			}
			if (k == "BHospital_Name") {
				if (v == null || v == "" || v == 0) {
					layer.msg("缺少医院名称,请添加!", {
						icon: 5,
						anim: 6
					});
					return false;
				} else {
					where += "&HospitalName=" + v;
				}
			}
		});
		layer.open({
			type: 2,
			area: ['60%', '90%'],
			fixed: false,
			maxmin: false,
			title: '设置财务系统属性',
			content: uxutil.path.BUSINESSANALYSIS_ROOT + '/ui/baseDataMange/hospitalFinanceSet.htm' + encodeURI(where),
			cancel: function (index, layero) {
			},
			success: function (layero, index) {
			},
			end: function () {
			}
		});
		
	});

	//监听排序事件
	tableObj.table.on('sort(table)', function(obj) {
		var field = obj.field; //排序字段
		var type = obj.type; //升序还是降序
		var url = tableObj.selectUrl;
		if(type == null) {
			tableObj.table.reload('table', {
				initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
				url: url,
				where: {
					time: new Date().getTime()
				}
			});
			if(val != "") {
				$("#search").val(val);
			}
			var areaId = $("#searchArea").val();
			$("#searchArea").html(areaListOption);
			if (areaId) {
				$("#searchArea").val(areaId);;
			}
			formObj.form.render('select'); //需要渲染一下;	
			return;
		}
		if(url.indexOf("sort") != -1) { //存在
			var start = url.indexOf("sort=[");
			var end = url.indexOf("]") + 1;
			var oldStr = url.slice(start, end);
			var newStr = 'sort=[{property:"' + field + '",direction:"' + type + '"}]';
			url = url.replace(oldStr, newStr);
		} else {
			url = url + '&sort=[{property:"' + field + '",direction:"' + type + '"}]';
		}
		tableObj.selectUrl = url; //修改默认的排序字段
		tableObj.checkRowData = [];
		var val = $("#search")[0].value;
		var areaId = $("#searchArea").val();
		var where = "1=1"
		if (val != "") {
			where += " and Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
		}
		if (areaId) where += " and AreaID=" + areaId;

		tableObj.table.reload('table', {
			url: url + "&where=" + where,
			where: {
				time: new Date().getTime()
			}
		});
		tableObj.checkRowData = [];
		$("#search").val(val);
		$("#searchArea").html(areaListOption);
		if (areaId) {
			$("#searchArea").val(areaId);;
		}
		formObj.form.render('select'); //需要渲染一下;	
	});
	//监听浏览器窗口
	window.onresize = function() {
		$(".tableHeight").css("height", ($(window).height() - 30) + "px"); //设置表单容器高度
	};

	function Arealist() {
		var me = this;
		var url = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaByHQL?isPlanish=true' + '&where=IsUse=1' +
			'&fields=BHospitalArea_Name,BHospitalArea_Id';
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = "<option value=''>请选择</option>";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].BHospitalArea_Id + "'>" + value.list[i].BHospitalArea_Name + "</option>";
					$("#searchArea").empty();
					$("#searchArea").append(tempAjax);

				}
				areaListOption = tempAjax;
				formObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};

	//级别-下拉框加载
	Levellist()

	function Levellist() {
		var me = this;
		var url = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalLevelByHQL?isPlanish=true' + '&where=IsUse=1' +
			'&fields=BHospitalLevel_Name,BHospitalLevel_Id';
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = "<option value=''>请选择(必填项)</option>";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].BHospitalLevel_Id + "'>" + value.list[i].BHospitalLevel_Name + "</option>";
					$("#LevelID").empty();
					$("#LevelID").append(tempAjax);
				}
				formObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};

	//类别-下拉框加载
	HTypelist()

	function HTypelist() {
		var me = this;
		var url = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeByHQL?isPlanish=true' + '&where=IsUse=1' +
			'&fields=BHospitalType_Name,BHospitalType_Id';
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = "<option value=''>请选择</option>";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].BHospitalType_Id + "'>" + value.list[i].BHospitalType_Name + "</option>";
					$("#HTypeID").empty();
					$("#HTypeID").append(tempAjax);

				}
				formObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};

	//省份-下拉框加载
	Provincelist()

	function Provincelist() {
		var me = this;
		var url = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=true' + '&where=IsUse=1' +
			'&fields=BProvince_Name,BProvince_Id';
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = "<option value=''>请选择(必填项)</option>";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].BProvince_Id + "'>" + value.list[i].BProvince_Name + "</option>";
					$("#ProvinceID").empty();
					$("#ProvinceID").append(tempAjax);

				}
				formObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};
	//城市-下拉框加载
	Citylist()

	function Citylist() {
		var me = this;
		var url = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCityByHQL?isPlanish=true' + '&where=IsUse=1' +
			'&fields=BCity_Name,BCity_Id';
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = "<option value=''>请选择(必填项)</option>";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].BCity_Id + "'>" + value.list[i].BCity_Name + "</option>";
					$("#CityID").empty();
					$("#CityID").append(tempAjax);

				}
				formObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};
	//区县-下拉框加载
	Countylist()

	function Countylist(where) {
		var me = this;
		var url = "";
		if (where) {
			url = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCountyByHQL?isPlanish=true' + '&where=IsUse=1 and bcounty.BCity.Id=' + where +
				'&fields=BCounty_Name,BCounty_Id';
		} else {
			url = uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCountyByHQL?isPlanish=true' + '&where=IsUse=1'+
				'&fields=BCounty_Name,BCounty_Id';
		}
		
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if (where && !value) {
					var tempAjax = "<option value=''>请选择(必填项)</option>";
					$("#CountyID").empty();
					$("#CountyID").append(tempAjax);
					formObj.form.render('select'); //需要渲染一下;
				}
				if(!value) return;
				var tempAjax = "<option value=''>请选择(必填项)</option>";
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].BCounty_Id + "'>" + value.list[i].BCounty_Name + "</option>";
					$("#CountyID").empty();
					$("#CountyID").append(tempAjax);
				}
				formObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};
	//分公司-下拉框加载
	HRDeptIdentitylist()

	function HRDeptIdentitylist() {
		var me = this;
		var deptIdentityurl = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL';
		deptIdentityurl += "?isPlanish=true&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName" +
			"&where=hrdeptidentity.TSysCode='ZF_LIIP_DeptDetail_Branch' and hrdeptidentity.SystemCode='ZF_LIIP' and hrdeptidentity.IdenTypeID='90020007'";
		uxutil.server.ajax({
			url: deptIdentityurl
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				var tempAjax = "<option value=''>请选择(必填项)</option>";
				if (!value) {
					$("#BranchId").empty();
					$("#BranchId").append(tempAjax);
					return;
				}
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].HRDeptIdentity_HRDept_Id + "'>" + value.list[i].HRDeptIdentity_HRDept_CName + "</option>";
					$("#BranchId").empty();
					$("#BranchId").append(tempAjax);
				}
				formObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});	

	};
	formObj.form.on('select(CountyID)', function(data) {
		$('#CountyName').val($("#CountyID").children('option:selected')[0].text);
	});
	formObj.form.on('select(CityID)', function (data) {
		$('#CityName').val($("#CityID").children('option:selected')[0].text);
		Countylist($("#CityID").children('option:selected')[0].value);
	});
	formObj.form.on('select(ProvinceID)', function(data) {
		$('#ProvinceName').val($("#ProvinceID").children('option:selected')[0].text);
	});
	formObj.form.on('select(HTypeID)', function(data) {
		$('#HtypeName').val($("#HTypeID").children('option:selected')[0].text);
	});
	formObj.form.on('select(LevelID)', function(data) {
		$('#LevelName').val($("#LevelID").children('option:selected')[0].text);
	});
	//formObj.form.on('select(AreaID)', function(data) {
	//	$('#AreaName').val($("#AreaID").children('option:selected')[0].text);
	//});
	//获得区域层级关系服务进行组装树形下拉选择器数据
	function searchTreeGridBHospitalArea() {
		var url = tableObj.searchTreeGridBHospitalAreaUrl;
		uxutil.server.ajax({
			url: url
		}, function (data) {
			tableRender();
			if (data) {
				var value = data["value"] ? data["value"]["list"] :[];
				var treeData = handleResultData(value);
				treeData = treeData;
				initTreeSelect(treeData);
			} else {
				layer.msg(data.msg);
			}
		});
	}
	//解析树返回数据
	function handleResultData(data) {
		var TreeData = [],
			data = data || [];
		for (var i = 0; i < data.length; i++) {
			var children = [];
			//if (data[i].IsChild) break;
			if (data[i].ChildHosps != null && data[i].ChildHosps.length > 0) {//存在下一级
				children = handleResultData(data[i].ChildHosps);
			}
			var obj = {
				name: data[i].Name,  //一级菜单
				id: data[i].Id,
				open: false,
				checked:false,
				children: children
			};
			TreeData.push(obj);
		}
		return TreeData;
	}
	searchTreeGridBHospitalArea();
	//树形下拉选择器 - 区域
	function initTreeSelect(data) {
		var treeObj = treeSelect.zTree('AreaID');
		if (treeObj) return;
		treeSelect.render({
			// 选择器
			elem: '#AreaID',
			// 数据
			data: data || [],
			//data是否是url
			isUrl:false,
			// 异步加载方式：get/post，默认get
			type: 'get',
			// 占位符
			placeholder: '区域选择(必填项)',
			// 是否开启搜索功能：true/false，默认false
			search: true,
			// 一些可定制的样式
			style: {
				//folder: {
				//	enable: true
				//},
				//line: {
				//	enable: true
				//}
			},
			// 点击回调
			click: function (d) {
				//console.log(d);
				$('#AreaName').val(d.current.name);
				$('#BHospital_AreaID').val(d.current.id);
			},
			// 加载完成后的回调函数
			success: function (d) {
				//console.log(d);
				//                //选中节点，根据id筛选
				////treeSelect.checkNode('tree', 3);
				//console.log($('#AreaID').val());
				////                获取zTree对象，可以调用zTree方法
				//var treeObj = treeSelect.zTree('AreaID');
				//console.log(treeObj);
				////                刷新树结构
				//treeSelect.refresh('AreaID');
			}
		});
	}
});