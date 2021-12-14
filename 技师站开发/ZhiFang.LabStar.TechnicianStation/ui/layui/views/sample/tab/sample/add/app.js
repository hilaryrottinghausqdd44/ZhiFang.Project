/**
 * 追加项目
 * @author GHX
 * @version 2021-04-20
 * @author zhangda
 * @version 2021-10-19
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base'
}).use(['uxutil','uxbase', 'table', 'form', 'element'], function() {
	"use strict";
	var $ = layui.jquery,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
		uxbase = layui.uxbase,
		uxutil = layui.uxutil;
	var app = {};
	//服务地址
	app.url = {
		//获得左侧列表数据
		getLeftTableDataUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarService.svc/LS_UDTO_DZQueryLisTestItemByHQL?isPlanish=true',
		//获取右侧列表数据
		getRightTableDataUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true',
		//获取组合项目列表数据
		getGroupItemTableDataUrl: uxutil.path.ROOT +
			'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemGroupByHQL?isPlanish=true',
		//保存服务
		saveUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItem',
	};
	app.cols = {
		left: [
			[{
				type: "checkbox",
				width: 30
			}, {
				field: 'LisTestItem_Id',
				width: 60,
				title: '主键ID',
				sort: false,
				hide: true
			}, {
				field: 'LisTestItem_PLBItem_Id',
				title: '组合项目ID',
				width: 130,
				hide: true,
				sort: false
			}, {
				field: 'LisTestItem_PLBItem_CName',
				title: '组合项目名称',
				width: 100,
				sort: false
			}, {
				field: 'LisTestItem_PLBItem_SName',
				title: '组合项目简称',
				width: 60,
				sort: false,
				hide: true
			}, {
				field: 'LisTestItem_PLBItem_Shortcode',
				title: '组合项目快捷码',
				hide: true,
				width: 60,
				sort: false
			}, {
				field: 'LisTestItem_PLBItem_PinYinZiTou',
				title: '组合项目拼音字头',
				hide: true,
				width: 60,
				sort: false
			}, {
				field: 'LisTestItem_LBItem_Id',
				title: '项目ID',
				width: 90,
				hide: true,
				sort: false
			}, {
				field: 'LisTestItem_LBItem_CName',
				title: '项目名称',
				minWidth: 100,
				sort: false
			}, {
				field: 'LisTestItem_LBItem_SName',
				title: '项目简称',
				width: 60,
				sort: false
			}, {
				field: 'LisTestItem_LBItem_Shortcode',
				title: '项目快捷码',
				hide: true,
				width: 60,
				sort: false
			}, {
				field: 'LisTestItem_LBItem_PinYinZiTou',
				title: '项目拼音字头',
				hide: true,
				width: 60,
				sort: false
			}, {
				field: 'LisTestItem_LBItem_IsOrderItem',
				title: '医嘱项目',
				width: 60,
				sort: false,
				templet: function(data) {
					if (String(data["LisTestItem_LBItem_IsOrderItem"]) == "true") {
						return '是';
					} else {
						return '否';
					}
				}
			}, {
				field: 'LisTestItem_DataAddTime',
				title: '是否在用',
				width: 60,
				sort: false,
				templet: function(data) {
					if (data["LisTestItem_DataAddTime"]) {
						return '在用';
					} else {
						return '新增';
					}
				}
			}, {
				field: 'LisTestItem_ReportValue',
				title: '报告值',
				width: 60,
				sort: false
			}, {
				field: 'LisTestItem_PLBItem_DispOrder',
				title: '组合项目排序',
				width: 130,
				hide: true,
				sort: false
			}, {
				field: 'LisTestItem_LBItem_DispOrder',
				title: '项目排序',
				minWidth: 130,
				hide: true,
				sort: false
			}]
		],
		right: [
			[{
				type: "checkbox",
				width: 30
			}, {
				field: 'LBSectionItem_Id',
				width: 60,
				title: '主键ID',
				sort: false,
				hide: true
			}, {
				field: 'LBSectionItem_LBItem_Id',
				title: '项目ID',
				width: 130,
				hide: true,
				sort: false
			}, {
				field: 'LBSectionItem_LBItem_CName',
				title: '项目名称',
				minWidth: 100,
				sort: false
			}, {
				field: 'LBSectionItem_LBItem_SName',
				title: '项目简称',
				width: 100,
				sort: false
			}, {
				field: 'LBSectionItem_LBItem_Shortcode',
				title: '快捷码',
				hide: false,
				width: 100,
				sort: false
			}, {
				field: 'LBSectionItem_LBItem_PinYinZiTou',
				title: '拼音字头',
				hide: true,
				width: 130,
				sort: false
			}, {
				field: 'LBSectionItem_LBItem_IsOrderItem',
				title: '医嘱项目',
				width: 60,
				sort: false,
				templet: function(data) {
					if (String(data["LBSectionItem_LBItem_IsOrderItem"]) == "true") {
						return '<span style="background-color:#7CE9BE;color:black">是</span>';
					} else {
						return '<span style="color:red">否</span>';
					}
				}
			}, {
				field: 'LBSectionItem_LBItem_GroupType',
				title: '组合类型',
				hide: true,
				width: 60,
				sort: false
			}, {
				field: 'LBSectionItem_DataAddTime',
				title: '是否在用',
				width: 60,
				sort: false,
				templet: function(data) {
					if (data["LBSectionItem_DataAddTime"] == "在用") {
						return '在用';
					} else if (data["LBSectionItem_DataAddTime"] == "新增") {
						return '新增';
					} else {
						return "";
					}
				}
			}, {
				field: 'LBSectionItem_DefultValue',
				title: '默认值',
				width: 60,
				sort: false
			}]
		],
		groupItem: [
			[{
					type: "numbers",
					title: "序号"
				},
				{
					field: 'LBItemGroup_Id',
					width: 60,
					title: '主键ID',
					sort: false,
					hide: true
				}, {
					field: 'LBItemGroup_LBItem_Id',
					title: '项目ID',
					width: 130,
					hide: true,
					sort: false
				}, {
					field: 'LBItemGroup_LBItem_CName',
					title: '项目名称',
					minWidth: 100,
					sort: false
				}, {
					field: 'LBItemGroup_LBItem_SName',
					title: '项目简称',
					width: 80,
					sort: false
				}, {
					field: 'LBItemGroup_LBItem_Shortcode',
					title: '快捷码',
					width: 80,
					sort: false
				} , {
					field: 'LBItemGroup_LBItem_PinYinZiTou',
					title: '拼音字头',
					width: 80,
					sort: false,
					hide: true
				} , {
					field: 'LBItemGroup_LBItem_IsOrderItem',
					title: '医嘱项目',
					width: 80,
					sort: false,
					templet: function(data) {
						if (String(data["LBItemGroup_LBItem_IsOrderItem"]) == "true") {
							return '是';
						} else {
							return '否';
						}
					}
				}
			]
		]
	};
	//左侧列表初始数据
	app.leftTableInitData = [];
	//左侧列表全部数据
	app.leftTableAllData = [];
	//get参数
	app.paramsObj = {
		sectionId: null, //小组ID10000000039
		testFormId: null, //检验单ID5598045837466289641
	};
	//右侧列表实例
	app.RightTableIns = null;
	//该小组所有项目
	app.AllSectionItemData = [];
	//初始化
	app.init = function() {
		var me = this;
		$(".fiexdHeight").css("height", ($(window).height() - 75) + "px"); //设置中间容器高度
		me.getParams();
		me.getSectionItem();
		me.initLeftTable();
		me.listeners();
	};
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.SECTIONID) {
			me.paramsObj.sectionId = params.SECTIONID;
		}
		if (params.TESTFORMID) {
			me.paramsObj.testFormId = params.TESTFORMID;
		}
	};
	//初始化左侧列表
	app.initLeftTable = function() {
		var me = this,
			url = me.url.getLeftTableDataUrl + "&fields=" + me.GetLeftTableFields(me.cols.left[0],true) + "&TestFormId=" + me.paramsObj.testFormId +'&sort=[{"property": "LisTestItem_PLBItem_DispOrder","direction": "ASC"},{"property": "LisTestItem_LBItem_DispOrder","direction": "ASC"}]'+ "&t=" + new Date().getTime();
			//url = uxutil.path.ROOT + '/ui/layui/views/sample/tab/sample/add/json/prograss.js';
		me.leftTableflag = true;
		table.render({
			elem: '#leftTable',
			height: 'full-80',
			defaultToolbar: ['filter'],
			size: 'sm',
			page: false,
			//data: data,
			url: url,
			cols: me.cols.left,
			limit: 99999,
			autoSort: true, //禁用前端自动排序
			text: {
				none: '暂无相关数据'
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
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function(res, curr, count) {
				if (app.leftTableflag) {
					app.leftTableflag = false;
					app.leftTableInitData = res.data;
					app.leftTableAllData = res.data;
					app.initRightTable();
				}
				res.data.forEach(function(item, index) {
					var ItemId = item["LisTestItem_LBItem_Id"],
						PItemId = item["LisTestItem_PLBItem_Id"];
					if (PItemId && ItemId != PItemId) {
						$('div[lay-id="leftTable"]').
						find('tr[data-index="' + index + '"]').
						css('background-color', '#FFC3A5');
					}
					if (String(item["LisTestItem_LBItem_IsOrderItem"]) == "true") {
						$('div[lay-id="leftTable"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LisTestItem_LBItem_IsOrderItem"]').
						css('background-color', '#7CE9BE');
					}
					if (item["LisTestItem_DataAddTime"]) {
						$('div[lay-id="leftTable"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LisTestItem_DataAddTime"]').
						css('background-color', '#ADE3F7');
					} else {
						$('div[lay-id="leftTable"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LisTestItem_DataAddTime"]').
						css('background-color', '#BDFFDE');
					}
				});

				if (res.data.length > 0)
					document.querySelector("#leftTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + res.data.length + ")").scrollIntoView(false, { behavior: "smooth" });
			}
		});
	};
	//初始化右侧列表
	app.initRightTable = function(url, options) {
		var me = this,
			options = options || {}, //列表配置
			url = url || me.url.getRightTableDataUrl + "&fields=" + me.GetLeftTableFields(me.cols.right[0], true) + "&where=lbsectionitem.LBItem.GroupType in (0,1) and lbsectionitem.LBItem.IsUse=true and lbsectionitem.LBSection.Id=" + me.paramsObj.sectionId + "and lbsectionitem.LBItem.IsOrderItem=true" +'&sort=[{property:"LBSectionItem_LBItem_GroupType",direction:"DESC"},{property:"LBSectionItem_DispOrder",direction:"ASC"},{property:"LBSectionItem_LBItem_DispOrder",direction:"ASC"}]' +"&t="+ new Date().getTime();
			//url = uxutil.path.ROOT + '/ui/layui/views/sample/tab/sample/add/json/prograss2.js';
		me.RightTableIns = table.render({
			elem: '#rightTable',
			height: 'full-80',
			defaultToolbar: ['filter'],
			size: 'sm', //小尺寸的表格
			//data: data,
			//toolbar: '#toolbarComboItem',
			url: url,
			cols: me.cols.right,
			autoSort: false, //禁用前端自动排序
			page: true,
			limit: 100,
			limits: [50, 100, 200, 500, 1000],
			text: {
				none: '暂无相关数据'
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
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				if (data.list && data.list.length > 0) {
					$.each(data.list, function(i, str1) {
						var str = "";
						$.each(me.leftTableAllData, function(i2, str2) {
							if (str1.LBSectionItem_LBItem_Id == str2.LisTestItem_LBItem_Id ||
								str1.LBSectionItem_LBItem_Id == str2.LisTestItem_PLBItem_Id) { //单项 或者 组合
								if (str2.LisTestItem_Id) {
									str = "在用";
								} else {
									str = "新增";
								}
							}
						});
						str1.LBSectionItem_DataAddTime = str;
					});
				};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function(res, curr, count) {
				res.data.forEach(function(item, index) {
					var IsComItem = item["LBSectionItem_LBItem_GroupType"];
					if (IsComItem == 1) {
						$('div[lay-id="rightTable"]').
						find('tr[data-index="' + index + '"]').
						css('background-color', '#FFC3A5');
					}
					if (String(item["LBSectionItem_LBItem_IsOrderItem"]) == "true") {
						$('div[lay-id="rightTable"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LBSectionItem_LBItem_IsOrderItem"]').
						css('background-color', '#7CE9BE');
					}
					if (item["LBSectionItem_DataAddTime"] == "在用") {
						$('div[lay-id="rightTable"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LBSectionItem_DataAddTime"]').
						css('background-color', '#ADE3F7');
					} else if (item["LBSectionItem_DataAddTime"] == "新增") {
						$('div[lay-id="rightTable"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LBSectionItem_DataAddTime"]').
						css('background-color', '#BDFFDE');
					}
				});
			}
		})
	};
	//初始化组合子项目列表
	app.initGroupItemTable = function(list) {
		var me = this,
			list = list || []; //数据

		table.render({
			elem: '#groupItemTable',
			height: 'full-80',
			defaultToolbar: ['filter'],
			size: 'sm', //小尺寸的表格
			url: '',
			data: list,
			cols: me.cols.groupItem,
			autoSort: false, //禁用前端自动排序
			page: false,
			limit: 9999999,
			text: {
				none: '暂无相关数据'
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
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};

				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function(res, curr, count) {
				res.data.forEach(function(item, index) {
					if (String(item["LBItemGroup_LBItem_IsOrderItem"]) == "true") {
						$('div[lay-id="groupItemTable"]').
						find('tr[data-index="' + index + '"]').
						find('td[data-field="LBItemGroup_LBItem_IsOrderItem"]').
						css('background-color', '#7CE9BE');
					}
				});
			}
		})
	};
	//获得小组所有项目
	app.getSectionItem = function () {
		var me = this,
			url = me.url.getRightTableDataUrl;
		url += "&fields=" + me.GetLeftTableFields(me.cols.right[0], true);
		url += "&where=lbsectionitem.LBItem.GroupType=0 and lbsectionitem.LBItem.IsUse=1 and lbsectionitem.LBSection.Id=" + me.paramsObj.sectionId;
		url += '&sort=[{property:"LBSectionItem_DispOrder",direction:"ASC"},{property:"LBSectionItem_LBItem_DispOrder",direction:"ASC"}]';
		url += "&t=" + new Date().getTime();
		uxutil.server.ajax({ url: url }, function (res) {
			if (res.success) {
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue).list : [];
				me.AllSectionItemData = data;
			}
		});
	};
	//查找子项目小组默认值
	app.getSonItemDefaultValue = function (itemid) {
		var me = this,
			itemid = itemid || null,
			DefaultValue = "",
			AllSectionItemData = me.AllSectionItemData;
		if (!itemid) return "";
		$.each(AllSectionItemData, function (i,item) {
			if (item["LBSectionItem_LBItem_Id"] == itemid) {
				if (item["LBSectionItem_DefultValue"]) {
					DefaultValue = item["LBSectionItem_DefultValue"];
					return false;
				}
			}
		});

		return DefaultValue;
	};
	//获取查询Fields
	app.GetLeftTableFields = function(col, isString) {
		var me = this,
			columns = col || [],
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
	//列表更新一行数据 -- fields:{ "LisTestItem_Id": '5598045837466289641',"LisTestItem_ReportValue": "123" }, key: "LisTestItem_Id"
	app.updateRowItem = function (fields, key) {
		var me = this,
			that = me.RightTableIns.config.instance,
			list = table.cache[that.key] || [],
			len = list.length,
			index = null;

		for (var i = 0; i < len; i++) {
			if (list[i][key] == fields[key]) {
				index = i;
				break;
			}
		}

		if (index == null) {//不存在
			return false;
		} else {
			var tr = that.layBody.find('tr[data-index="' + index + '"]'),
				data = list[index],
				cacheData = table.cache[that.key][index];
			//将变化的字段值赋值到data  覆盖原先值
			data = $.extend({}, data, fields);

			fields = fields || {};
			layui.each(fields, function (ind, value) {
				if (ind in data) {
					var templet, td = tr.children('td[data-field="' + ind + '"]');
					data[ind] = value;
					cacheData[ind] = value;
					that.eachCols(function (i, item2) {
						if (item2.field == ind && item2.templet) {
							templet = item2.templet;
						}
					});
					td.children(".layui-table-cell").html(function () {
						return templet ? function () {
							return typeof templet === 'function'
								? templet(data)
								: laytpl($(templet).html() || value).render(data)
						}() : value;
					}());
					td.data('content', value);
				}
			});
			return true;
		}
	};
	//获得右侧列表当前数据行数
	app.getRowIndex = function (data) {
		var list = table.cache.rightTable || [], //右侧列表所有数据
			data = data || null,
			index = null;
		if (!data) return;
		for (var i = 0; i < list.length; i++) {
			if (data["LBSectionItem_LBItem_Id"] == list[i]["LBSectionItem_LBItem_Id"]) {
				index = i;
				break;
			}
		}
		return index;
	};
	//监听事件
	app.listeners = function() {
		var me = this;
		$("#existSearchText").bind("input propertychange", function(event) {
			var value = $.trim($("#existSearchText").val());
			if (value) {
				var data = [];
				$.each(me.leftTableInitData, function(i, str) {
					if (str.LisTestItem_PLBItem_CName.indexOf(value) != -1 ||
						str.LisTestItem_PLBItem_SName.indexOf(value) != -1 ||
						str.LisTestItem_PLBItem_Shortcode.indexOf(value) != -1 ||
						str.LisTestItem_PLBItem_PinYinZiTou.indexOf(value) != -1 ||
						str.LisTestItem_LBItem_CName.indexOf(value) != -1 ||
						str.LisTestItem_LBItem_SName.indexOf(value) != -1 ||
						str.LisTestItem_LBItem_Shortcode.indexOf(value) != -1 ||
						str.LisTestItem_LBItem_PinYinZiTou.indexOf(value) != -1) {
						data.push(str);
					}
				})
				table.reload('leftTable', {
					url: '',
					data: data
				});
			} else {
				table.reload('leftTable', {
					url: '',
					data: me.leftTableInitData
				});
			}
		});
		//监听未加入项目表格查询
		form.on('submit(search)', function(data) {
			var url = me.url.getRightTableDataUrl + "&fields=" + me.GetLeftTableFields(me.cols.right[0], true);
			var where =	"(lbsectionitem.LBItem.GroupType in (0,1) and lbsectionitem.LBItem.IsUse=true and lbsectionitem.LBSection.Id="+me.paramsObj.sectionId+")";
			if (data.field.searchText != "") { //模糊查询
				var str = data.field.searchText;
				where += " and (lbsectionitem.LBItem.CName like '%" + str + "%' or lbsectionitem.LBItem.SName like '%" + str +
					"%' or lbsectionitem.LBItem.Shortcode like '%" + str + "%' or lbsectionitem.LBItem.PinYinZiTou like '%" + str + "%')";
			}
			if (data.field.IsOrderItem == "on") {
				where += " and lbsectionitem.LBItem.IsOrderItem=true";
			}
			url += "&where=" + where +
				'&sort=[{property:"LBSectionItem_DispOrder",direction:"ASC"},{property:"LBSectionItem_LBItem_DispOrder",direction:"ASC"}]' +
				"&t=" + new Date().getTime();
			me.initRightTable(encodeURI(url));
			$("#searchText").val(data.field.searchText);
		});
		//加入按钮操作
		$("#add").click(function() {
			//获得选中的数据
			var checkData = table.checkStatus('rightTable').data;
			me.onAddClick(checkData);
		});
		//移除按钮操作
		$("#remove").click(function() {
			var checkData = table.checkStatus('leftTable').data;
			me.onRemoveClick(checkData);
		});
		//删除组合项目按钮操作
		$("#removeAll").click(function() {
			var checkData = table.checkStatus('leftTable').data;
			me.onRemoveAllClick(checkData);
		});
		//重置按钮操作
		$("#reset").click(function() {
			layer.confirm('是否确认进行重置操作?', {
				icon: 3,
				title: '提示'
			}, function(index) {
				me.initLeftTable();
				layer.close(index);
			});
		});
		//保存按钮操作
		$("#save").click(function() {
			me.onSaveClick();
		});
		//监听左侧列表行双击事件
		table.on('rowDouble(leftTable)', function(obj) {
			me.onRemoveClick([obj.data]);
		});
		//监听右侧列表行双击事件
		table.on('rowDouble(rightTable)', function(obj) {
			var GroupType = obj.data.LBSectionItem_LBItem_GroupType;
			if (GroupType == 0) {
				me.onAddClick([obj.data]);
			} else if (GroupType == 1) {
				setTimeout(function() {
					me.onAddClick([obj.data]);
				}, 200);
			}
		});
		//监听左侧列表行单击事件
		table.on('row(leftTable)', function(obj) {
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
		});
		//监听右侧列表行单击事件
		table.on('row(rightTable)', function(obj) {
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //标注选中样式
			var IsComItem = obj.data.LBSectionItem_LBItem_GroupType;
			if (IsComItem == 1) {
				if ($("#groupItemTableDiv").hasClass("layui-hide")) {
					$("#groupItemTableDiv").removeClass("layui-hide");
				}
				if ($("#rightTalbeDiv").hasClass("layui-col-sm12")) {
					$("#rightTalbeDiv").removeClass("layui-col-sm12");
				}
				if (!$("#rightTalbeDiv").hasClass("layui-col-sm9")) {
					$("#rightTalbeDiv").addClass("layui-col-sm9");
				}
				if (!$("#groupItemTableDiv").hasClass("layui-col-sm3")) {
					$("#groupItemTableDiv").addClass("layui-col-sm3");
				}
				me.getSubItemByPItemID(obj.data.LBSectionItem_LBItem_Id, true, function (list) {
					me.initGroupItemTable(list);
				});
			} else {
				if (!$("#groupItemTableDiv").hasClass("layui-hide")) {
					$("#groupItemTableDiv").addClass("layui-hide");
				}
				if ($("#rightTalbeDiv").hasClass("layui-col-sm9")) {
					$("#rightTalbeDiv").removeClass("layui-col-sm9");
				}
				if (!$("#rightTalbeDiv").hasClass("layui-col-sm12")) {
					$("#rightTalbeDiv").addClass("layui-col-sm12");
				}
			}
		});
		//监听右侧列表排序事件
		table.on('sort(rightTable)', function(obj) {
			var field = obj.field, //排序字段
				type = obj.type, //升序还是降序
				searchText = $("#searchText").val(),
				IsOrderItem = $("#IsOrderItem").val(),
				url = me.url.getRightTableDataUrl + "&fields=" + me.GetLeftTableFields(me.cols.right[0], true);

			if (type == null) return;
			var where =
				"lbsectionitem.LBItem.GroupType in (0,1) and lbsectionitem.LBItem.IsUse=true and lbsectionitem.LBSection.Id=" +
				me.paramsObj.sectionId;
			if (searchText != "") { //模糊查询
				var str = data.field.searchText;
				where += " and (lbitem.CName like '%" + str + "%' or lbitem.EName like '%" + str +
					"%' or lbitem.SName like '%" + str + "%' or lbitem.Shortcode like '%" + str + "%' or lbitem.UseCode like '%" +
					str + "%')";
			}
			if (IsOrderItem == "on") {
				where += " and lbsectionitem.LBItem.IsOrderItem=true";
			}
			url += "&where=" + where;
			if (url.indexOf("sort") != -1) { //存在
				var start = url.indexOf("sort=[");
				var end = url.indexOf("]") + 1;
				var oldStr = url.slice(start, end);
				var newStr = 'sort=[{property:"' + field + '",direction:"' + type + '"}]';
				url = url.replace(oldStr, newStr);
			} else {
				url = url + '&sort=[{property:"' + field + '",direction:"' + type + '"}]';
			}
			url += "&t=" + new Date().getTime();
			me.initRightTable(encodeURI(url));
		});
	};
	//获得组合项目下的子项目
	app.getSubItemByPItemID = function (groupitemid, async,callback) {
		var me = this,
			groupitemid = groupitemid || null,
			async = async || false,
			data = data || [],
			url = me.url.getGroupItemTableDataUrl;

		if (!groupitemid) return;

		url += "&fields=" + me.GetLeftTableFields(me.cols.groupItem[0], true);
		url += "&where=(LBItem.IsUse=true and GroupItemID=" + groupitemid + ")";
		url += '&sort=[{property:"LBItemGroup_DispOrder",direction:"ASC"},{property:"LBItemGroup_LBItem_DispOrder",direction:"ASC"}]';
		url += "&t=" + new Date().getTime();

		var load = layer.load();
		uxutil.server.ajax({ url: url, async: false }, function (res) {
			layer.close(load);
			if (res.success) {
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue).list : [];
				$.each(data, function (i, item) {
					item["LBItemGroup_DefultValue"] = me.getSonItemDefaultValue(item["LBItemGroup_LBItem_Id"]);
				});
				callback && callback(data);
			} else {
				callback && callback([]);
			}
		});

	};
	//加入数据
	app.onAddClick = function(checkData) {
		var me = this,
			leftTableDataCache = table.cache.leftTable || [], //左侧列表所有数据
			checkData = checkData || [], //选中待加入数据
			addData = [], //确认要加入的数据
			updateRowData = [];//需要更新右侧列表数据
		var LBItemIdField = ""; //选过来的项目id字段
		if (checkData.length == 0) {
			uxbase.MSG.onWarn("未选择数据!");
			return;
		}
		for (var j = 0; j < checkData.length; j++) {
			var GroupType = checkData[j].LBSectionItem_LBItem_GroupType,
				data = [];//加入的数据
			if (GroupType == 0) {//单项目
				data = [checkData[j]];
				LBItemIdField = "LBSectionItem_";
			} else if (GroupType == 1) {//组合项目
				me.getSubItemByPItemID(checkData[j].LBSectionItem_LBItem_Id, false, function (list) {
					data = list;
				});
				LBItemIdField = "LBItemGroup_";
			}

			for (var i = 0; i < data.length; i++) {
				var flag = true; //是否加入到新增数据中
				for (var a = 0; a < leftTableDataCache.length; a++) {
					if (data[i][LBItemIdField + "LBItem_Id"] == leftTableDataCache[a].LisTestItem_LBItem_Id) {
						flag = false;
						if (GroupType == 1) {
							if (leftTableDataCache[a].LisTestItem_Id) {
								//需要后台更新 记录
								me.leftTableAllData[a].LisTestItem_Tab = 'update';
							}
							//更新表中数据
							me.leftTableAllData[a].LisTestItem_PLBItem_Id = checkData[j].LBSectionItem_LBItem_Id;
							me.leftTableAllData[a].LisTestItem_PLBItem_CName = checkData[j].LBSectionItem_LBItem_CName;
							me.leftTableAllData[a].LisTestItem_PLBItem_SName = checkData[j].LBSectionItem_LBItem_SName;
							me.leftTableAllData[a].LisTestItem_PLBItem_Shortcode = checkData[j].LBSectionItem_LBItem_Shortcode;
							me.leftTableAllData[a].LisTestItem_PLBItem_PinYinZiTou = checkData[j].LBSectionItem_LBItem_PinYinZiTou;
						} else {
							break;
						}
					}
				}
				if (flag) {
					var addentity = {
						LisTestItem_Id: "",
						LisTestItem_PLBItem_Id: GroupType == 0 ? "" : checkData[j].LBSectionItem_LBItem_Id,
						LisTestItem_PLBItem_CName: GroupType == 0 ? "" : checkData[j].LBSectionItem_LBItem_CName,
						LisTestItem_PLBItem_SName: GroupType == 0 ? "" : checkData[j].LBSectionItem_LBItem_SName,
						LisTestItem_PLBItem_Shortcode: GroupType == 0 ? "" : checkData[j].LBSectionItem_LBItem_Shortcode,
						LisTestItem_PLBItem_PinYinZiTou: GroupType == 0 ? "" : checkData[j].LBSectionItem_LBItem_PinYinZiTou,
						LisTestItem_LBItem_Id: data[i][LBItemIdField + "LBItem_Id"],
						LisTestItem_LBItem_CName: data[i][LBItemIdField + "LBItem_CName"],
						LisTestItem_LBItem_SName: data[i][LBItemIdField + "LBItem_SName"],
						LisTestItem_LBItem_Shortcode: data[i][LBItemIdField + "LBItem_Shortcode"],
						LisTestItem_LBItem_PinYinZiTou: data[i][LBItemIdField + "LBItem_PinYinZiTou"],
						LisTestItem_LBItem_IsOrderItem: data[i][LBItemIdField + "LBItem_IsOrderItem"],
						LisTestItem_ReportValue: data[i][LBItemIdField + "DefultValue"] || "",
						LisTestItem_DataAddTime: '',
						LisTestItem_Tab: 'add'
					};
					addData.push(addentity);
					me.leftTableAllData.push(addentity);

					updateRowData.push({ "LBSectionItem_LBItem_Id": addentity["LisTestItem_LBItem_Id"], "LBSectionItem_DataAddTime": "新增" });
					//组合项目 需要将其加入
					if (GroupType == 1) {
						var isadd = false;
						for (var b = 0; b < updateRowData.length; b++) {
							if (checkData[j]["LBSectionItem_LBItem_Id"] == updateRowData[b]["LBSectionItem_LBItem_Id"]) {
								isadd = true;
								break;
							}
						}
						if (!isadd) updateRowData.push({ "LBSectionItem_LBItem_Id": checkData[j]["LBSectionItem_LBItem_Id"], "LBSectionItem_DataAddTime": "新增" });
					}
				}
			}
		}

		//更新左侧列表
		table.reload('leftTable', {
			url: '',
			data: app.leftTableAllData
		});
		//更新右侧列表
		for (var k = 0; k < updateRowData.length; k++) {
			me.updateRowItem(updateRowData[k], "LBSectionItem_LBItem_Id");
			var index = me.getRowIndex(updateRowData[k]);
			if (index != null) $('div[lay-id="rightTable"]').find('tr[data-index="' + index + '"]').find('td[data-field="LBSectionItem_DataAddTime"]').css('background-color', '#BDFFDE');
		}
		//table.reload('rightTable', {});
		
	};
	//移除数据
	app.onRemoveClick = function (checkData) {
		var me = this,
			msg = [],
			leftTableCacheData = table.cache.leftTable || [], //左侧列表数据
			updateRowData = [],//需要更新右侧列表数据
			PItemID = [],//删除中需要更新右侧列表中的组合项目
			checkData = checkData || []; //获得选中的数据
		if (checkData.length == 0) {
			uxbase.MSG.onWarn("未选择数据!");
			return;
		}
		//在用的检验项目,请在检验界面删除！
		for (var a = 0; a < checkData.length; a++) {
			if (checkData[a].LisTestItem_Id) msg.push(checkData[a].LisTestItem_LBItem_CName);
		}
		if (msg.length > 0) {
			uxbase.MSG.onWarn(msg.join('，') + "<br>上述检验项目为在用项目，请在检验界面删除!");
			return;
		}
		//左侧列表处理
		for (var i = 0; i < checkData.length; i++) {
			for (var j = me.leftTableAllData.length - 1; j >= 0; j--) {
				if (me.leftTableAllData[j].LisTestItem_LBItem_Id == checkData[i].LisTestItem_LBItem_Id) {
					updateRowData.push({ "LBSectionItem_LBItem_Id": me.leftTableAllData[j].LisTestItem_LBItem_Id, "LBSectionItem_DataAddTime": "" });
					if (me.leftTableAllData[j].LisTestItem_PLBItem_Id && PItemID.indexOf(me.leftTableAllData[j].LisTestItem_PLBItem_Id) == -1)
						PItemID.push(me.leftTableAllData[j].LisTestItem_PLBItem_Id);
					me.leftTableAllData.splice(j, 1);
				}
			}
		}
		//更新左侧列表
		table.reload('leftTable', {
			url: '',
			data: me.leftTableAllData
		});
		//判断左侧列表中的组合项目
		for (var b = 0; b < me.leftTableAllData.length; b++) {
			if (PItemID.indexOf(me.leftTableAllData[b].LisTestItem_PLBItem_Id) != -1) {
				delete PItemID[PItemID.indexOf(me.leftTableAllData[b].LisTestItem_PLBItem_Id)];
				break;
			}
		}
		for (var c = 0; c < PItemID.length; c++) {
			if (PItemID[c]) updateRowData.push({ "LBSectionItem_LBItem_Id": PItemID[c], "LBSectionItem_DataAddTime": "" });
		}
		//更新右侧列表
		for (var k = 0; k < updateRowData.length; k++) {
			me.updateRowItem(updateRowData[k], "LBSectionItem_LBItem_Id");
			var index = me.getRowIndex(updateRowData[k]);
			if (index != null) $('div[lay-id="rightTable"]').find('tr[data-index="' + index + '"]').find('td[data-field="LBSectionItem_DataAddTime"]').css('background-color', 'transparent');
		}
		//table.reload('rightTable', {});
		//$("#search").click();
	};
	//移除组合数据
	app.onRemoveAllClick = function(checkData) {
		var me = this,
			msg = [],
			leftTableCacheData = table.cache.leftTable || [], //左侧列表数据
			updateRowData = [],//需要更新右侧列表数据
			PItemID = [],//删除中需要更新右侧列表中的组合项目
			checkData = checkData || []; //获得选中的数据
		if (checkData.length == 0) {
			uxbase.MSG.onWarn("未选择数据!");
			return;
		}
		//在用的检验项目,请在检验界面删除！
		for (var a = 0; a < checkData.length; a++) {
			if (checkData[a].LisTestItem_Id) msg.push(checkData[a].LisTestItem_LBItem_CName);
		}
		if (msg.length > 0) {
			uxbase.MSG.onWarn(msg.join('，') + "<br>上述检验项目为在用项目，请在检验界面删除!");
			return;
		}

		//左侧列表处理
		for (var i = 0; i < checkData.length; i++) {
			if (checkData[i].LisTestItem_PLBItem_Id == "") {//单项  --直接删除
				for (var j = 0; j < me.leftTableAllData.length; j++) {
					if (me.leftTableAllData[j].LisTestItem_LBItem_Id == checkData[i].LisTestItem_LBItem_Id) {
						updateRowData.push({ "LBSectionItem_LBItem_Id": me.leftTableAllData[j].LisTestItem_LBItem_Id, "LBSectionItem_DataAddTime": "" });
						me.leftTableAllData.splice(j, 1);
						break;
					}
				}
			} else {//组合项目 --组合删除
				for (var j = me.leftTableAllData.length - 1; j >= 0; j--) {
					if (me.leftTableAllData[j].LisTestItem_PLBItem_Id == checkData[i].LisTestItem_PLBItem_Id && !me.leftTableAllData[j].LisTestItem_Id) {
						updateRowData.push({ "LBSectionItem_LBItem_Id": me.leftTableAllData[j].LisTestItem_LBItem_Id, "LBSectionItem_DataAddTime": "" });
						if (me.leftTableAllData[j].LisTestItem_PLBItem_Id && PItemID.indexOf(me.leftTableAllData[j].LisTestItem_PLBItem_Id) == -1)
							PItemID.push(me.leftTableAllData[j].LisTestItem_PLBItem_Id);
						me.leftTableAllData.splice(j, 1);
					}
				}
			}
		}
		//更新左侧列表
		table.reload('leftTable', {
			url: '',
			data: me.leftTableAllData
		});
		//判断左侧列表中的组合项目
		for (var b = 0; b < me.leftTableAllData.length; b++) {
			if (PItemID.indexOf(me.leftTableAllData[b].LisTestItem_PLBItem_Id) != -1) {
				delete PItemID[PItemID.indexOf(me.leftTableAllData[b].LisTestItem_PLBItem_Id)];
				break;
			}
		}
		for (var c = 0; c < PItemID.length; c++) {
			if (PItemID[c]) updateRowData.push({ "LBSectionItem_LBItem_Id": PItemID[c], "LBSectionItem_DataAddTime": "" });
		}
		//更新右侧列表
		for (var k = 0; k < updateRowData.length; k++) {
			me.updateRowItem(updateRowData[k], "LBSectionItem_LBItem_Id");
			var index = me.getRowIndex(updateRowData[k]);
			if (index != null) $('div[lay-id="rightTable"]').find('tr[data-index="' + index + '"]').find('td[data-field="LBSectionItem_DataAddTime"]').css('background-color', 'transparent');
		}
		//table.reload('rightTable', {});
			//$("#search").click();
	};
	//保存数据
	app.onSaveClick = function() {
		var me = this,
			testFormId = me.paramsObj.testFormId,
			addUpdateList = [],
			data = app.leftTableAllData;//新增和修改数据都放
		if (!testFormId) {
			uxbase.MSG.onWarn("主单ID不可为空!");
			return;
		}
		for(var i = 0;i<data.length;i++){
			if(data[i].LisTestItem_Tab && (data[i].LisTestItem_Tab == "add" ||  data[i].LisTestItem_Tab == "update")){
				var obj = {};
				if (data[i].LisTestItem_PLBItem_Id) obj["PLBItem"] = { Id: data[i].LisTestItem_PLBItem_Id, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
				if (data[i].LisTestItem_LBItem_Id) obj["LBItem"] = { Id: data[i].LisTestItem_LBItem_Id, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
				obj["ReportValue"] = data[i].LisTestItem_ReportValue;
				addUpdateList.push(obj);
			}
		}
		if (addUpdateList.length == 0) {
			uxbase.MSG.onWarn("没有需要保存的数据!");
			return;
		} else {
			var configs = {
				type: "POST",
				url: me.url.saveUrl,
				data: JSON.stringify({
					testFormID: testFormId,
					listAddTestItem: addUpdateList,
					isRepPItem: true,
					testItemFileds: 'ReportValue'
				})
			};
			var loadIndex = layer.load();
			uxutil.server.ajax(configs, function(res) {
				//隐藏遮罩层
				layer.close(loadIndex);
				if (res.success) {
					parent.layer.closeAll();
				} else {
					uxbase.MSG.onError("保存失败!");
				}
			});
		}		
	};
	//初始化调用入口
	app.init();
});
