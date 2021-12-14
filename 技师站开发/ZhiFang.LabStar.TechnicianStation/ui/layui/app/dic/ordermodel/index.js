layui.extend({
	uxutil: 'ux/util'
}).use(['table', 'form', 'layer', 'element', 'uxutil'],function(){
	var $ = layui.$,
		table = layui.table,
		form = layui.form,
		layer = layui.layer,
		element = layui.element,
		uxutil = layui.uxutil;
	/* 疾病放到简码中 **/

	var app = {};

	//服务地址
	app.url = {
		AddOrderModelListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBOrderModel',
		EditOrderModelListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBOrderModelByField',
		DelOrderModelListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBOrderModel',
		GetOrderModelListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBOrderModelByHQL?isPlanish=true',
		GetOrderModelItemListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBOrderModelItemByHQL?isPlanish=true',
		DelOrderModelItemListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBOrderModelItem',
		GetDeptListUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true',
		GetUserListUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true',
	};
	//外部参数
	app.params = {
		OrderModelTypeArr:[1,2,3,4] //模板类型
	};
	//配置
	app.config = {
		//模板类型Map
		OrderModelTypeMap: {
			1: { text: "个人", icon: "layui-icon-username" },
			2: { text: "科室", icon: "layui-icon-user" },
			3: { text: "疾病", icon: "layui-icon-util" },
			4: { text: "全院", icon: "layui-icon-template-1" }
		},
		//当前模板类型
		OrderModelType:1,
		//加载层
		loading: null,
		//科室
		DeptData:[],
		//人员
		UserData: [],
		//菜单列表
		NavTableConfig: {},//配置项
		NavTableIns: null,//实例
		NavTablCheckRowData: [],//选择行
		//医嘱模板列表
		OrderModelTableConfig: {},//配置项
		OrderModelTableIns: null,//实例
		OrderModelTablCheckRowData:[],//选择行
		//医嘱模板项目列表
		OrderModelItemTableConfig: {},//配置项
		OrderModelItemTableIns: null,//实例
		//前处理系统编码
		SystemCode: "ZF_LAB_START"
	};
	//初始化方法
	app.init = function () {
		var me = this;
		me.initParams();
		me.FormHandleByOrderModelType();
		me.initTab();
		me.initDept(function (data) {
			me.config.DeptData = data;
			if (me.config.OrderModelType == 2) me.initNavTable(data);
		});
		me.initUser(function (data) {
			me.config.UserData = data;
			if (me.config.OrderModelType == 1) me.initNavTable(data);
		});
		me.initOrderModelTable();
		me.initOrderModelItemTable();
		me.initListeners();
	};
	//监听事件
	app.initListeners = function () {
		var me = this;
		//监听页签切换
		element.on('tab(Tab)', function (data) {
			me.config.OrderModelType = $(this).attr("lay-id");
			me.FormHandleByOrderModelType();
			$("#OrderModelCName").val("");
			me.onSearch(1);
		});
		//NavTable触发行单击事件
		table.on('row(NavTable)', function (obj) {
			//标注选中样式
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			me.config.NavTablCheckRowData = [];
			me.config.NavTablCheckRowData.push(obj["data"]);
			me.onSearch(1);
		});
		//OrderModelTable触发行单击事件
		table.on('row(OrderModelTable)', function (obj) {
			//标注选中样式
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			me.config.OrderModelTablCheckRowData = [];
			me.config.OrderModelTablCheckRowData.push(obj["data"]);
			me.onSearchOrderModelItem(1);
		});
		//查询
		$("#search").on('click', function () {
			me.onSearch(1);
		});
		//新增
		$("#add").on('click', function () {
			layer.open({
				type: 1,
				maxmin: true,
				//skin: 'layui-layer-rim', //加上边框
				area: ['550px', '260px'], //宽高
				content: $("#addEditFormBox"),
				success: function (layero, index) {

				},
				end: function () {
					me.clearForm();
				}
			});
		});
		//编辑
		$("#edit").on('click', function () {
			if (me.config.OrderModelTablCheckRowData.length != 1) return;
			layer.open({
				type: 1,
				maxmin: true,
				//skin: 'layui-layer-rim', //加上边框
				area: ['550px', '260px'], //宽高
				content: $("#addEditFormBox"),
				success: function (layero, index) {
					me.setValueForm();
				},
				end: function () {
					me.clearForm();
				}
			});
		});
		//选择项目
		$("#selectItem").on('click', function () {
			var CheckRowData = me.config.OrderModelTablCheckRowData;
			if (CheckRowData.length != 1) return;
			var flag = false;
			parent.layer.open({
				type: 2,
				area: me.screen($) < 2 ? ['85%', '70%'] : ['1200px', '600px'],
				fixed: false,
				maxmin: true,
				title: '选择项目',
				content: uxutil.path.ROOT + '/ui/layui/app/dic/ordermodel/item/index.html?OrderModelID=' + CheckRowData[0]["LBOrderModel_Id"] + '&OrderModelName=' + CheckRowData[0]["LBOrderModel_CName"],
				cancel: function (index, layero) {
					flag = true;
				},
				success: function (layero, index) {
					var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
				},
				end: function () {
					if (flag) return;
					me.onSearchOrderModelItem(1);
				}
			});
		});
		//删除
		$("#del").on('click', function () {
			if (me.config.OrderModelTablCheckRowData.length != 1) return;
			me.onDelClick();
		});
		//保存医嘱模板
		$("#save").on('click', function () {
			me.onSaveClick();
		});
		//关闭医嘱模板弹窗
		$("#close").on('click', function () {
			layer.closeAll('page'); //关闭所有页面层
		});
	};
	//获得参数
	app.initParams = function () {
		var me = this,
			params = uxutil.params.get();
		me.params = $.extend({}, me.params, params);
	};
	//初始化页签
	app.initTab = function () {
		var me = this,
			TabTitleHtml = [],
			OrderModelTypeArr = me.params.OrderModelTypeArr,
			OrderModelTypeMap = me.config.OrderModelTypeMap;

		$.each(OrderModelTypeArr, function (i, item) {
			if (TabTitleHtml.length > 0) {
				TabTitleHtml.push('<li lay-id="' + item + '"><i class="layui-icon ' + OrderModelTypeMap[item]["icon"] + '"></i> ' + OrderModelTypeMap[item]["text"] + '</li>');
			} else {
				me.config.OrderModelType = item;
				TabTitleHtml.push('<li lay-id="' + item + '" class="layui-this"><i class="layui-icon ' + OrderModelTypeMap[item]["icon"] + '"></i> ' + OrderModelTypeMap[item]["text"] + '</li>');
			}
		});

		$("#TabTitle").html(TabTitleHtml.join(""));
		element.init(); //更新全部
	};

	//初始化菜单列表
	app.initNavTable = function (data) {
		var me = this;
		me.config.NavTableConfig = {
			id: 'NavTable',
			elem: '#NavTable',
			height: 'full-100',
			url: '',
			toolbar: '',
			page: false,
			limit: 50,
			limits: [50, 100, 200, 500, 1000],
			autoSort: false, //禁用前端自动排序
			loading: false,
			size: 'sm', //小尺寸的表格
			cols: [[
				{ type: 'numbers', title: '行号' },
				{ field: 'Id', width: 100, title: '主键ID', sort: false, hide: true },
				{ field: 'Name', minWidth: 100, title: '名称', sort: false },
				{ field: 'Code', width: 100, title: '编码', sort: false }
			]],
			text: { none: '暂无相关数据' },
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) {//res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				me.config.NavTablCheckRowData = [];
				if (count == 0) return;
				setTimeout(function () {
					me.onClickFirstRow("NavTable");
				}, 0);
			}
		};
		//赋值url
		me.config.NavTableConfig.data = data || [];
		//初始化列表
		me.config.NavTableIns = table.render(me.config.NavTableConfig);
	};

	//初始化医嘱模板列表
	app.initOrderModelTable = function () {
		var me = this;
		me.config.OrderModelTableConfig = {
			id: 'OrderModelTable',
			elem: '#OrderModelTable',
			height: 'full-100',
			url: '',
			toolbar: '',
			page: true,
			limit: 50,
			limits: [50, 100, 200, 500, 1000],
			autoSort: false, //禁用前端自动排序
			defaultSort: [{ "property": "LBOrderModel_SCode", "direction": "asc" }, { "property": "LBOrderModel_DispOrder", "direction": "asc" }],//默认排序
			loading: false,
			size: 'sm', //小尺寸的表格
			cols: [[
				{ type: 'numbers', title: '行号' },
				{ field: 'LBOrderModel_Id', width: 100, title: '主键ID', sort: false, hide: true },
				{ field: 'LBOrderModel_POrderModelID', width: 100, title: '父模板ID', sort: false, hide: true },
				{ field: 'LBOrderModel_OrderModelTypeID', width: 100, title: '医嘱模板类型ID', sort: false, hide: true },
				{ field: 'LBOrderModel_OrderModelTypeName', width: 120, title: '医嘱模板类型', sort: false },
				{ field: 'LBOrderModel_CName', minWidth: 120, title: '医嘱模板名称', sort: false },
				{ field: 'LBOrderModel_DeptID', width: 100, title: '科室ID', sort: false, hide: true },
				{ field: 'LBOrderModel_UserID', width: 100, title: '用户ID', sort: false, hide: true },
				{ field: 'LBOrderModel_SName', width: 100, title: '简称', sort: false },
				{ field: 'LBOrderModel_SCode', width: 100, title: '简码', sort: false },
				{
					field: 'LBOrderModel_IsUse', width: 100, title: '是否使用', sort: false,
					templet: function (data) {
						var value = String(data["LBOrderModel_IsUse"]);
						if (value == "true")
							return '<span style="color:green;">是</span>';
						else
							return '<span style="color:red;">否</span>';

					}
				},
				{ field: 'LBOrderModel_DispOrder', width: 100, title: '显示次序', sort: false },
				{ field: 'LBOrderModel_OrderModelDesc', width: 160, title: '说明', sort: false }
			]],
			text: { none: '暂无相关数据' },
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) {//res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				me.config.OrderModelTablCheckRowData = [];
				if (count == 0) {
					me.onSearchOrderModelItem(1);
					return;
				}
				me.onClickFirstRow("OrderModelTable");
			}
		};
		//赋值url
		//me.config.OrderModelTableConfig.url = me.GetOrderModelListUrl();
		//初始化列表
		me.config.OrderModelTableIns = table.render(me.config.OrderModelTableConfig);
	};
	//默认选中第一行
	app.onClickFirstRow = function (tableid) {
		var me = this,
			tableid = tableid || null;
		if (!tableid) return;
		$("#" + tableid + "+div").find('.layui-table-main tr[data-index="0"]').click();
	};
	//获取查询Fields
	app.getStoreFields = function (isString, tableConfig) {
		var me = this,
			tableConfig = tableConfig || me.config.OrderModelTableConfig,
			columns = tableConfig.cols[0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	//查询医嘱模板列表
	app.onSearch = function (page) {
		var me = this,
			tableIns = me.config.OrderModelTableIns,
			instance = tableIns.config.instance,
			page = page || instance.layPage.find('.layui-laypage-curr>em:last-child').html() || 1,
			url = url || me.GetOrderModelListUrl();
		if (!url) return;
		//重载
		tableIns.reload({
			url: url,
			height: 'full-100',//不写height 高度会消失
			page: {
				curr: page //重新从第 page 页开始
			},
			where: {
				t: new Date().getTime()
			}
		});
	};
	//获得OrderModelTable列表查询地址
	app.GetOrderModelListUrl = function () {
		var me = this,
			url = me.url.GetOrderModelListUrl,
			OrderModelType = me.config.OrderModelType,
			OrderModelCName = $("#OrderModelCName").val(),//医嘱模板名称
			ShortCode = $("#ShortCode").val(),//简码
			Dept = (OrderModelType == 2 && me.config.NavTablCheckRowData.length > 0) ? me.config.NavTablCheckRowData[0]["Id"] : null,//科室
			User = (OrderModelType == 1 && me.config.NavTablCheckRowData.length > 0) ? me.config.NavTablCheckRowData[0]["Id"] : null,//人员
			where = ["OrderModelTypeID=" + me.config.OrderModelType];//其他条件 and
		
		//医嘱模板名称
		if (OrderModelCName) {
			where.push("CName like '%" + OrderModelCName + "%'");
		}
		//简码
		if (ShortCode) {
			where.push("SCode='" + ShortCode + "'");
		}
		//科室
		if (Dept) {
			where.push("DeptID='" + Dept + "'");
		}
		//人员
		if (User) {
			where.push("UserID='" + User + "'");
		}
		url += "&where=" + where.join(' and ');
		//查询字段
		url += '&fields=' + me.getStoreFields(true, me.config.OrderModelTableConfig).join(',');
		//默认排序
		url += '&sort=' + JSON.stringify(me.config.OrderModelTableConfig.defaultSort);

		return url;
	};

	//初始化医嘱模板项目列表
	app.initOrderModelItemTable = function () {
		var me = this;
		me.config.OrderModelItemTableConfig = {
			id: 'OrderModelItemTable',
			elem: '#OrderModelItemTable',
			height: 'full-100',
			url: '',
			data:[],
			toolbar: '',
			page: true,
			limit: 100,
			limits: [100, 200, 500, 1000, 1500],
			defaultSort: [{ "property": "LBOrderModelItem_DispOrder", "direction": "asc" }],//默认排序
			autoSort: false, //禁用前端自动排序
			loading: false,
			size: 'sm', //小尺寸的表格
			cols: [[
				{ type: 'numbers', title: '行号' },
				{ field: 'LBOrderModelItem_Id', width: 100, title: '主键ID', sort: false, hide: true },
				{ field: 'LBOrderModelItem_OrderModelID', width: 100, title: '医嘱模板ID', sort: false, hide: true },
				{ field: 'LBOrderModelItem_ItemID', width: 100, title: '项目ID', sort: false, hide: true },
				{ field: 'LBOrderModelItem_ItemName', minWidth: 120, title: '项目名称', sort: false },
				{
					field: 'LBOrderModelItem_IsUse', width: 100, title: '是否使用', sort: false,
					templet: function (data) {
						var value = String(data["LBOrderModelItem_IsUse"]);
						if (value == "true")
							return '<span style="color:green;">是</span>';
						else
							return '<span style="color:red;">否</span>';

					}
				},
				{ field: 'LBOrderModelItem_DispOrder', width: 100, title: '显示次序', sort: false, hide: true }
			]],
			text: { none: '暂无相关数据' },
			response: function () {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function (res) {//res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function (res, curr, count) {
				if (count == 0) return;
				me.onClickFirstRow("OrderModelItemTable");
			}
		};
		//赋值url
		//me.config.OrderModelItemTableConfig.url = me.GetOrderModelItemListUrl();
		//初始化列表
		me.config.OrderModelItemTableIns = table.render(me.config.OrderModelItemTableConfig);
	};
	//查询医嘱模板项目列表
	app.onSearchOrderModelItem = function (page) {
		var me = this,
			tableIns = me.config.OrderModelItemTableIns,
			instance = tableIns.config.instance,
			page = page || instance.layPage.find('.layui-laypage-curr>em:last-child').html() || 1,
			url = url || me.GetOrderModelItemListUrl();
		//重载
		if (table.cache[instance.key]) {
			tableIns.reload({
				url: url,
				data:[],
				height: 'full-100',//不写height 高度会消失
				page: {
				    curr: page //重新从第 page 页开始
				},
				where: {
					t: new Date().getTime()
				}
			});
		}
	};
	//获得OrderModelItemTable列表查询地址
	app.GetOrderModelItemListUrl = function (OrderModelID) {
		var me = this,
			url = me.url.GetOrderModelItemListUrl,
			OrderModelID = OrderModelID || (me.config.OrderModelTablCheckRowData.length > 0 ? me.config.OrderModelTablCheckRowData[0]["LBOrderModel_Id"] : null);
		if (!OrderModelID) return '';
		url += '&where=IsUse=1 and OrderModelID=' + OrderModelID;
		url += '&fields=' + me.getStoreFields(true, me.config.OrderModelItemTableConfig).join(',');
		url += '&sort=' + JSON.stringify(me.config.OrderModelItemTableConfig.defaultSort);

		return url;
	};

	//赋值表单
	app.setValueForm = function () {
		var me = this,
			CheckRowData = me.config.OrderModelTablCheckRowData;
		if (CheckRowData.length == 0) return;
		var data = JSON.parse(JSON.stringify(CheckRowData[0]).replace(RegExp("LBOrderModel_", "g"), ""));
		data.IsUse = String(data.IsUse) == "true" ? 1 : 0;
		form.val("addEditForm", data);
		form.render('select');
	};
	//清空表单
	app.clearForm = function () {
		var me = this;
		$("#addEditForm :input").each(function () {
			if ($(this).attr("id") == "OrderModelTypeID") return true;
			if ($(this).attr("id") == "IsUse") {
				$("#IsUse").val(1);
				return true;
			}
			$(this).val('');
		});
		form.render('select');
	};
	//获取新增编辑数据
	app.getEntity = function () {
		var me = this,
			msg = [],
			OrderModelType = me.config.OrderModelType,
			Id = $("#Id").val(),
			CName = $("#CName").val().trim(),
			OrderModelTypeID = $("#OrderModelTypeID").val(),
			OrderModelTypeName = me.config.OrderModelTypeMap[OrderModelTypeID].text,
			DeptID = $("#DeptID").val(),
			UserID = $("#UserID").val(),
			SName = $("#SName").val().trim(),
			SCode = $("#SCode").val().trim(),
			DispOrder = $("#DispOrder").val(),
			IsUse = $("#IsUse").val() == 1 ? true : false,
			OrderModelDesc = $("#OrderModelDesc").val().trim(),
			entity = {};

		entity = {
			CName: CName,
			OrderModelTypeID: OrderModelTypeID,
			OrderModelTypeName: OrderModelTypeName,
			SName: SName,
			SCode: SCode,
			DispOrder: DispOrder ? DispOrder : 0,
			IsUse: IsUse,
			OrderModelDesc: OrderModelDesc
		};
		
		if (OrderModelType == 1) {
			entity.UserID = UserID;
			if (!UserID) msg.push("个人模板的人员不能为空!");
		} else if (OrderModelType == 2) {
			entity.DeptID = DeptID;
			if (!DeptID) msg.push("科室模板的科室不能为空!");
		} else if (OrderModelType == 3) {
			if (!SCode) msg.push("疾病模板的简码不能为空!");
		}

		if (!CName) msg.push("模板名称不能为空!");
		if (isNaN(DispOrder)) msg.push("显示次序只能为数值型!");
		if (Id) entity.Id = Id;

		return { entity: entity, msg: msg };
	};
	//保存医嘱模板
	app.onSaveClick = function () {
		var me = this,
			data = me.getEntity(),
			msg = data.msg,
			entity = data.entity,
			type = entity.Id ? 'edit' : 'add',
			url = type == 'edit' ? me.url.EditOrderModelListUrl : me.url.AddOrderModelListUrl;

		if (msg.length > 0) {
			layer.msg(msg.join('<br >'), { icon: 0, anim: 0 });
			return;
		}

		if (type == 'edit') {
			var fields = [];
			for (var i in entity) {
				if (typeof entity[i] == "object") continue;
				fields.push(i);
			}
			entity = { entity: entity, fields: fields.join(",") };
		} else {
			entity = { entity: entity };
		}

		var config = {
			type: "POST",
			url: url,
			data: JSON.stringify(entity)
		};
		var loading = layer.load();
		uxutil.server.ajax(config, function (data) {
			layer.closeAll('loading');//隐藏遮罩层
			if (data.success) {
				layer.msg("保存成功!", { icon: 6, anim: 0 });
				layer.closeAll('page'); //关闭所有页面层
				me.onSearch();
			} else {
				var msg = type == 'add' ? '新增失败！' : '修改失败！';
				if (!data.msg) data.msg = msg;
				layer.msg(data.msg, { icon: 5, anim: 0 });
			}
		});


	};
	//删除
	app.onDelClick = function () {
		var me = this,
			CheckRowData = me.config.OrderModelTablCheckRowData,
			url = me.url.DelOrderModelListUrl;
		if (CheckRowData.length == 0) return;
		url += "?id=" + CheckRowData[0]["LBOrderModel_Id"];
		var loading = layer.load();
		me.onDelOrderModelItem(function () {
			uxutil.server.ajax({
				url: url
			}, function (data) {
				layer.closeAll('loading');
				if (data.success === true) {
					layer.msg("删除成功!", { icon: 6, anim: 0 });
					me.onSearch();
				} else {
					layer.msg("医嘱模板删除失败!", { icon: 5, anim: 0 });
				}
			});
		});

	};
	//删除医嘱模板项目
	app.onDelOrderModelItem = function (callback) {
		var me = this,
			ItemList = table.cache["OrderModelItemTable"],
			length = ItemList.length,
			successCount = 0,
			errorCount = 0,
			msg = [],
			url = me.url.DelOrderModelItemListUrl;

		if (length > 0) {
			$.each(ItemList, function (i, item) {
				setTimeout(function () {
					uxutil.server.ajax({
						url: url + "?id=" + item["LBOrderModelItem_Id"]
					}, function (data) {
						if (data.success === true) {
							successCount++;
						} else {
							errorCount++;
							msg.push(item["LBOrderModelItem_ItemName"] + "删除失败!");
						}
						if (errorCount + successCount == length) {
							if (errorCount == 0)
								callback();
							else
								layer.msg(msg.join("<br >"), { icon: 5, anim: 0 });
						}
					});
				}, i * 100);
				
			});
		} else {
			callback();
		}
	};

	//根据当前页签模板类型处理表单
	app.FormHandleByOrderModelType = function () {
		var me = this,
			OrderModelType = me.config.OrderModelType;

		if (OrderModelType == 1) {
			if ($("#NavTableBox").hasClass("layui-hide")) {
				$("#NavTableBox").removeClass("layui-hide");
				$("#OrderModelTableBox").attr("class", "layui-col-xs8");
			}
			me.initNavTable(me.config.UserData);
		} else if (OrderModelType == 2) {
			if ($("#NavTableBox").hasClass("layui-hide")) {
				$("#NavTableBox").removeClass("layui-hide");
				$("#OrderModelTableBox").attr("class", "layui-col-xs8");
			}
			me.initNavTable(me.config.DeptData);
		} else {
			if (!$("#NavTableBox").hasClass("layui-hide")) {
				$("#NavTableBox").addClass("layui-hide");
				$("#OrderModelTableBox").attr("class", "layui-col-xs12");
			}
			me.onSearch(1);
			//me.initNavTable([]);
		}

		$(".OrderModelType").removeClass("layui-hide").addClass("layui-hide");
		$(".OrderModelType" + OrderModelType).removeClass("layui-hide");
		$("#OrderModelTypeID").html("<option value='" + OrderModelType + "'>" + me.config.OrderModelTypeMap[OrderModelType].text + "</option>");
		form.render('select');

	};
	//初始化科室
	app.initDept = function (callback) {
		var me = this,
			DeptData = [],
			url = me.url.GetDeptListUrl;

		url = url + '&sort=[{"property":"HRDeptIdentity_DispOrder","direction":"ASC"}]' +
			'&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_UseCode' +
			"&where=hrdeptidentity.IsUse=1";// and hrdeptidentity.SystemCode='"+me.config.SystemCode+"' and hrdeptidentity.TSysCode='1001102'";

		uxutil.server.ajax({
			url: url
		}, function (res) {
			if (res.success) {
				if (res.ResultDataValue) {
					var data = JSON.parse(res.ResultDataValue).list,
						html = '<option value="">选择科室</option>';
					$.each(data, function (i, item) {
						DeptData.push({ Id: item.HRDeptIdentity_HRDept_Id, Name: item.HRDeptIdentity_HRDept_CName, Code: item.HRDeptIdentity_HRDept_UseCode });
						var code = item.HRDeptIdentity_HRDept_UseCode;
						html += '<option value=' + item.HRDeptIdentity_HRDept_Id + '>' + item.HRDeptIdentity_HRDept_CName + (code ? '【' + code + '】' : '') + '</option>';
					});
					$("#DeptID").html(html);
					form.render('select');
				}
			}
			callback(DeptData);
		});
	};
	//初始化用户
	app.initUser = function (callback) {
		var me = this,
			UserData = [],
			url = me.url.GetUserListUrl;

		url = url + '&sort=[{"property":"HREmpIdentity_DispOrder","direction":"ASC"}]' +
			"&fields=HREmpIdentity_HREmployee_Id,HREmpIdentity_HREmployee_CName,HREmpIdentity_HREmployee_StandCode" +
			"&where=hrempidentity.IsUse=1";// and hrempidentity.SystemCode='"+me.config.SystemCode+"' and hrempidentity.TSysCode='1001003'";

		uxutil.server.ajax({
			url: url
		}, function (res) {
			if (res.success) {
				if (res.ResultDataValue) {
					var data = JSON.parse(res.ResultDataValue).list,
						html = '<option value="">选择人员</option>';
					$.each(data, function (i, item) {
						UserData.push({ Id: item.HREmpIdentity_HREmployee_Id, Name: item.HREmpIdentity_HREmployee_CName, Code: item.HREmpIdentity_HREmployee_StandCode });
						var code = item.HREmpIdentity_HREmployee_StandCode;
						html += '<option value=' + item.HREmpIdentity_HREmployee_Id + '>' + item.HREmpIdentity_HREmployee_CName + (code ? '【' + code + '】' : '') +'</option>';
					});
					$("#UserID").html(html);
					form.render('select');
				}
			}
			callback(UserData);
		});
	};
	//判断浏览器大小方法
	app.screen = function ($) {
		//获取当前窗口的宽度
		var width = $(window).width();
		if (width > 1200) {
			return 3;   //大屏幕
		} else if (width > 992) {
			return 2;   //中屏幕
		} else if (width > 768) {
			return 1;   //小屏幕
		} else {
			return 0;   //超小屏幕
		}
	};

	app.init();
});