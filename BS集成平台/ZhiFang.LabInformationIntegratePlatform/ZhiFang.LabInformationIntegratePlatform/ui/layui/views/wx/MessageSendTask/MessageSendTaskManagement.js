
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'upload','tree','table', 'form'], function() {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		tree = layui.tree,
		upload = layui.upload,
		$ = layui.jquery;
	//表格	
	var tableObj = {
		table: layui.table,
		form: layui.form,
		fields: {
			Id: "WXMessageSendTask_Id"
		},
		current: null,
		delIndex: null,
		addUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_AddWXMessageSendTask',
		selectUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_SearchWXMessageSendTaskByHQL?isPlanish=true',
		updateUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_UpdateWXMessageSendTaskByField',
		delUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_DelWXMessageSendTask',
		peopleurl: uxutil.path.ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true&fields=HREmployee_CName,HREmployee_MobileTel,HREmployee_UseCode,HREmployee_HRDept_CName,HREmployee_IsUse,HREmployee_Id",
		sendOutUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/WXMessageSendOut',
		checkRowData: [], //选中数据
		uploadFileName: "",
		uploadFileURL: "",
		uploadFileSize:"",
		refresh: function() {
			var searchText = $("#search")[0].value;
			tableObj.table.reload('table', {
				where: {
					time: new Date().getTime()
				}
			});
			tableObj.checkRowData = [];
			if(searchText != "") {
				$("#search").val(searchText);
			}
		}
	};
	
	init();

	function init() {
		$(".tableHeight").css("height", ($(window).height() - 30) + "px"); //设置表单容器高度
		tableRender();
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
				[{ type: 'radio' },{
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
						field: 'WXMessageSendTask_HospitalID',
						title: '医院ID',
						minWidth: 130,
						sort: true,
						hide: true
					},
					{
						field: 'WXMessageSendTask_HospitalName',
						title: '医院名称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'WXMessageSendTask_TaskName',
						title: '任务名称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'WXMessageSendTask_TaskTypeName',
						title: '任务类型名称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'WXMessageSendTask_SendTypeName',
						title: '发送方式',
						minWidth: 130,
						//sort: true
					},
					{
						field: 'WXMessageSendTask_Count',
						title: '发送计数',
						minWidth: 130,
						//sort: true
					},
					{
						field: 'WXMessageSendTask_Comment',
						title: '备注',
						//sort: true,
						minWidth: 70
					},
					{ title: '操作', width: 100, align: 'center', toolbar: '#tableOperation' }
				]
			],
			page: true,
			limit: 50,
			limits: [50, 100, 200, 500, 1000],
			autoSort: false, //禁用前端自动排序
			done: function(res, curr, count) {
				if(count > 0) {
					$("#table+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
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
	}
	//清空
	tableObj.clear = function () {
		$("#layForm :input").each(function (i, item) {
			$(this).val("");
		});
		tableObj.form.render();
	}
	tableObj.addClick = function () {
		tableObj.clear();//清空
		layer.open({
			type: 1,
			btn: ['保存'],
			title: ['新增', 'font-weight:bold;'],
			skin: 'layui-layer-lan',
			area: ['650px', '560px'],
			content: $('#addModel'),
			yes: function (index, layero) {
				var entity = {};
				entity["TaskName"] = $("#TaskName").val();
				entity["HospitalID"] = $("#BHospital").val() ? $("#BHospital").val() : 0;
				entity["Comment"] = $("#Comment").val();
				entity["HospitalName"] = $("#BHospital").val() ? $("#BHospital option:selected").text() : "";
				entity["HospitalCode"] = $("#BHospital").val() ? $("#BHospital option:selected").attr("data-HospitalCode") : "";
				entity["TaskTypeID"] = $("#TaskTypeID").val() ? $("#TaskTypeID").val() : 0;
				entity["TaskTypeName"] = $("#TaskTypeID").val() ? $("#TaskTypeID option:selected").text() : "";
				entity["SendTypeID"] = $("#SendTypeID").val() ? $("#SendTypeID").val() : 0;
				entity["SendTypeName"] = $("#SendTypeID").val() ? $("#SendTypeID option:selected").text() : "";
				entity["PMTID"] = $("#PMTID").val() ? $("#PMTID").val() : 0;
				entity["Context"] = $("#Context").val();
				entity["AttachmentName"] = uploadFileName;
				entity["AttachmentURL"] = uploadFileURL;
				entity["AttachmentSize"] = uploadFileSize;
				tableObj.onSaveClick({ entity: entity }, 'add', index);
			},
			cancel: function (index, layero) {
				tableObj.clear();
			}
		});
	}
	//保存处理
	tableObj.onSaveClick = function (data, type, index) {
		var url = type == 'add' ? tableObj.addUrl : tableObj.updateUrl,
			params = data;
		if (!url) return;
		params = JSON.stringify(params);
		//显示遮罩层
		var indexs = layer.load();
		var configs = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(configs, function (data) {
			//隐藏遮罩层
			layer.close(indexs);
			if (data.success) {
				if (index) layer.close(index);
				layer.msg("保存成功！", { icon: 6, anim: 0 });
				tableObj.refresh();
			} else {
				var msg = type == 'add' ? '新增失败！' : '修改失败！';
				if (!data.msg) data.msg = msg;
				layer.msg(data.msg, { icon: 5, anim: 6 });
			}
			uploadFileName = "";
			uploadFileURL = "";
			uploadFileSize = "";
		});
	};
	//指定允许上传的文件类型
	upload.render({
		elem: '#test3', multiple: true
		, url: uxutil.path.ROOT + '/ServerWCF/WXService.svc/WXMessageSendTaskUpLoadFile' //改成您自己的上传接口
		, accept: 'file' //普通文件
		, done: function (res) {
			if (res.success) {
				var fileinfo = res.BoolInfo.split(',');
				uploadFileName = fileinfo[0];
				uploadFileURL = fileinfo[1];
				uploadFileSize = fileinfo[2];
				$("#uploadname").text(uploadFileName);
			} else {
				layer.msg("上传失败!", { icon: 5, anim: 6 });
			}
		}
	});
	//修改
	tableObj.editClick = function (data) {
		var list = JSON.parse(JSON.stringify(data));
		tableObj.clear();
		tableObj.form.val('layForm', list);
		layer.open({
			type: 1,
			btn: ['保存'],
			title: ['编辑', 'font-weight:bold;'],
			skin: 'layui-layer-lan',
			area: ['650px', '560px'],
			content: $('#addModel'),
			yes: function (index, layero) {
				var entity = {};
				if (uploadFileName) {
					entity["AttachmentName"] = uploadFileName;
				}
				if (uploadFileURL) {
					entity["AttachmentURL"] = uploadFileURL;
				}
				if (uploadFileSize) {
					entity["AttachmentSize"] = uploadFileSize;
				}
				entity["TaskName"] = $("#TaskName").val();
				entity["HospitalID"] = $("#BHospital").val() ? $("#BHospital").val() : 0;
				entity["Comment"] = $("#Comment").val();
				entity["HospitalName"] = $("#BHospital").val() ? $("#BHospital option:selected").text() : "";
				entity["HospitalCode"] = $("#BHospital").val() ? $("#BHospital option:selected").attr("data-HospitalCode") : "";
				entity["TaskTypeID"] = $("#TaskTypeID").val() ? $("#TaskTypeID").val() : 0;
				entity["TaskTypeName"] = $("#TaskTypeID").val() ? $("#TaskTypeID option:selected").text() : "";
				entity["SendTypeID"] = $("#SendTypeID").val() ? $("#SendTypeID").val() : 0;
				entity["SendTypeName"] = $("#SendTypeID").val() ? $("#SendTypeID option:selected").text() : "";
				entity["PMTID"] = $("#PMTID").val() ? $("#PMTID").val() : 0;
				entity["Context"] = $("#Context").val();
				entity["Id"] = list.WXMessageSendTask_Id;
				var fields = "";
				$.each(entity, function (j, itemJ) {
					if (!fields)
						fields = j;
					else
						fields += "," + j;
				});
				tableObj.onSaveClick({ entity: entity, fields: fields }, 'edit', index);
			},
			cancel: function (index, layero) {
				tableObj.clear();
			}
		});
	}
	//table上面的工具栏
	tableObj.table.on('toolbar(table)', function (obj) {

		var checkData = tableObj.checkRowData;
		switch(obj.event) {
			case 'add':
				tableObj.addClick();
				break;
			case 'edit':
				if (checkData && checkData.length != 1) {
					layer.msg("请勾选一条数据进行修改！");
					return;
				}
				tableObj.editClick(checkData[0]);
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
				if($("#search")[0].value == "") {
					tableObj.table.reload('table', {
						url: tableObj.selectUrl,
						where: {
							time: new Date().getTime()
						}
					});
					tableObj.checkRowData = [];
				} else {
					var val = $("#search")[0].value;
					var url = "";
					if(tableObj.selectUrl.indexOf("where") != -1) {
						var where = " and Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
						url = tableObj.selectUrl.replace(')', where);
					} else {
						url = encodeURI(tableObj.selectUrl + "&where=Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'");
					}
					tableObj.table.reload('table', {
						url: url,
						where: {
							time: new Date().getTime()
						}
					});
					tableObj.checkRowData = [];
					$("#search").val(val);
				}
		};
	});
	//监听行单击事件
	tableObj.table.on('row(table)', function(obj) {
		tableObj.checkRowData = [];
		tableObj.checkRowData.push(obj.data);
		//标注选中样式
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		
	});
	//初始化弹出的医院部门人员
	tableObj.openhosptalpepoleinit = function () {
		tableObj.depttree();
		tableObj.table.render({
			elem: '#peopletable',
			height: 'full-330',
			//size: 'sm', //小尺寸的表格
			defaultToolbar: ['filter'],
			url:tableObj.peopleurl,
			cols: [
				[{ type: 'radio' }, {
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
					field: 'HREmployee_CName',
					title: '人员',
					minWidth: 130,
					sort: true
				}
				]
			],
			page: true,
			limit: 50,
			limits: [50, 100, 200, 500, 1000],
			autoSort: false, //禁用前端自动排序
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) { //res即为原始返回的数据
				if (!res) return;
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
	};
	//医院部门树
	tableObj.depttree = function () {
		var url = uxutil.path.ROOT +"/ServerWCF/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id,HRDept_DataTimeStamp,HRDept_UseCode&node=0&id=0";
		var configs = {
			type: "get",
			url: url
		};
		uxutil.server.ajax(configs, function (data) {
			//隐藏遮罩层
			if (data.success) {
				var df = data.ResultDataValue.replace(/Tree/g, "children");
				df = df.replace(/text/g, "title");
				df = df.replace(/tid/g, "id");
				var data1 = JSON.parse(df);
				tree.render({
					elem: '#test1' //默认是点击节点可进行收缩
					, data: [{ spread:true,title: "ALL", children: data1.children }]
					, click: function (obj) {
						//console.log(obj.data); //得到当前点击的节点数据
						//console.log(obj.state); //得到当前节点的展开状态：open、close、normal
						//console.log(obj.elem); //得到当前节点元素
						//console.log(obj.data.children); //当前节点下是否有子节点
						tableObj.table.reload('peopletable', {
							initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
							url: tableObj.peopleurl,
							where: {
								where: 'id='+obj.data.id
							}
						});
					}
				});
			} else {
				layer.msg("失败！", { icon: 5, anim: 6 });
			}
		});
		
	};
	//医院人员选择
	tableObj.openhosptalpepole = function (data) {
		tableObj.openhosptalpepoleinit();
		layer.open({
			type: 1,
			btn: ['发送'],
			title: ['选择', 'font-weight:bold;'],
			skin: 'layui-layer-lan',
			area: ['700px', '660px'],
			content: $('#hosptalpeople'),
			yes: function (index, layero) {
				var checkdata = tableObj.table.checkStatus("peopletable"); //人员表格所选择的人
				tableObj.sendout(data, checkdata.data[0]);
				layer.close(index);
			}
		});
	};
	//监听表格行工具事件
	tableObj.table.on('tool(table)', function (obj) {
		var data = obj.data, //获得当前行数据
			layEvent = obj.event;
		if (layEvent === 'sendout') {
			if (data.WXMessageSendTask_SendTypeID=="3") {
				tableObj.openhosptalpepole(data);
			} else {
				layer.confirm('确定发送?', {
					icon: 3,
					title: '提示'
				}, function (index) {
						layer.close(index);
						tableObj.sendout(data);
				});
			}
		}
	});
	//发送服务
	tableObj.sendout = function (r, people) {
		var id = r.WXMessageSendTask_Id;
		var empid = 0;
		if (people) {
			empid = people.HREmployee_Id;
		}
		var url =tableObj.sendOutUrl + "?id=" + id + "&peopleId=" + empid;
		var configs = {
			type: "get",
			url: url
		};
		uxutil.server.ajax(configs, function (data) {
			//隐藏遮罩层
			if (data.success) {
				layer.msg("保存成功！", { icon: 6, anim: 0 });
				tableObj.refresh();
			} else {
				layer.msg("失败！", { icon: 5, anim: 6 });
			}
		});
	};
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
		if(val != "") {
			if(url.indexOf("where") != -1) {
				var where = " and Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
				url = url.replace(')', where);
			} else {
				url = encodeURI(url + "&where=Name like '%" + val + "%' or SName like '%" + val + "%' or EName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'");
			}
		}
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
	});
	//医院 -下拉框加载
	Arealist();
	PMTlist();
	function Arealist() {
		var me = this;
		var url = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true' + '&where=IsUse=1' +
			'&fields=BHospital_Id,BHospital_Name,BHospital_HospitalCode';
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
					tempAjax += "<option value='" + value.list[i].BHospital_Id + "' data-HospitalCode ='" + value.list[i].BHospital_HospitalCode+"'>" + value.list[i].BHospital_Name + "</option>";
					$("#BHospital").empty();
					$("#BHospital").append(tempAjax);
				}
				tableObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};
	function PMTlist() {
		var url = uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_SearchWXWeiXinPushMessageTemplateByHQL?isPlanish=true' + '&where=IsUse=1' +
			'&fields=WXWeiXinPushMessageTemplate_Id,WXWeiXinPushMessageTemplate_Name';
		uxutil.server.ajax({
			url: url
		}, function (data) {
			if (data) {
				var value = data[uxutil.server.resultParams.value];
				if (value && typeof (value) === "string") {
					if (isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if (!value) return;
				var tempAjax = "<option value=''>请选择</option>";
				for (var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].WXWeiXinPushMessageTemplate_Id + "' >" + value.list[i].WXWeiXinPushMessageTemplate_Name + "</option>";
					$("#PMTID").empty();
					$("#PMTID").append(tempAjax);
				}
				tableObj.form.render('select'); //需要渲染一下;
			} else {
				layer.msg(data.msg);
			}
		});
	};

});