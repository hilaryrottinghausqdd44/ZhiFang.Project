
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form'], function() {
	var layer = layui.layer,
		uxutil = layui.uxutil,
		$ = layui.jquery;
	//表格	
	var tableObj = {
		table: layui.table,
		form: layui.form,
		fields: {
			Id: "WXWeiXinPushMessageTemplate_Id"
		},
		current: null,
		delIndex: null,
		addUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_AddWXWeiXinPushMessageTemplate',
		selectUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_SearchWXWeiXinPushMessageTemplateByHQL?isPlanish=true',
		updateUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_UpdateWXWeiXinPushMessageTemplateByField',
		delUrl: uxutil.path.ROOT + '/ServerWCF/WXService.svc/ST_UDTO_DelWXWeiXinPushMessageTemplate',
		checkRowData: [], //选中数据
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
				[
					{ type: 'checkbox' },{
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
						field: 'WXWeiXinPushMessageTemplate_Name',
						title: '名称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'WXWeiXinPushMessageTemplate_SName',
						title: '简称',
						minWidth: 130,
						sort: true
					},
					{
						field: 'WXWeiXinPushMessageTemplate_Shortcode',
						title: '简码',
						minWidth: 130,
						//sort: true
					},
					{
						field: 'WXWeiXinPushMessageTemplate_PinYinZiTou',
						title: '拼音',
						minWidth: 130,
						//sort: true
					},
					{
						field: 'WXWeiXinPushMessageTemplate_Comment',
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
			area: ['475px', '380px'],
			content: $('#addModel'),
			yes: function (index, layero) {
				var entity = {};
				entity["Name"] = $("#Name").val();
				entity["SName"] = $("#SName").val();
				entity["Shortcode"] = $("#Shortcode").val();
				entity["PinYinZiTou"] = $("#PinYinZiTou").val();
				entity["Comment"] = $("#Comment").val();
				entity["IsUse"] =1;
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
		});
	};
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
			area: ['475px', '380px'],
			content: $('#addModel'),
			yes: function (index, layero) {
				var entity = {};
				entity["Id"] = list.WXWeiXinPushMessageTemplate_Id;
				entity["Name"] = $("#Name").val();
				entity["SName"] = $("#SName").val();
				entity["Shortcode"] = $("#Shortcode").val();
				entity["PinYinZiTou"] = $("#PinYinZiTou").val();
				entity["Comment"] = $("#Comment").val();
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
	//监听表格行工具事件
	tableObj.table.on('tool(table)', function (obj) {
		var data = obj.data, //获得当前行数据
			layEvent = obj.event;
		if (layEvent === 'edit') {
			tableObj.editClick(data);
		}
	});
	//监听行单击事件
	tableObj.table.on('row(table)', function(obj) {
		tableObj.checkRowData = [];
		tableObj.checkRowData.push(obj.data);
		//标注选中样式
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
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
});