/**
   @Name：专业编辑
   @Author：zhangda
   @version 2021-11-22
 */
layui.extend({
	uxeditor: 'modules/common/editor'
}).define(['form', 'element', 'uxutil', 'uxbase','uxeditor'], function (exports) {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        uxbase = layui.uxbase,
		uxutil = layui.uxutil,
		uxeditor = layui.uxeditor,
        MOD_NAME = 'ProEditor';

	var TEMPLATE_DOM = [
		'<fieldset class="layui-elem-field layui-field-title">',
		'<legend id="{domId}-legend" style="color:#333;">{项目名称}</legend>',
		'<div class="layui-field-box">',
		'<div id="{domId}-{itemId}-editor-div"></div>',
		'</div>',
		'</fieldset>'
	];
	//小组项目获取服务地址
	var GET_SECTION_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemVOByHQL?isPlanish=true';
	//获得该检验单下大文本项目
	var GET_PROEDITOR_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true';
	//查询项目快捷模板服务地址
	var GET_ITEM_TEMPLATE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemExpByHQL?isPlanish=true';

	//新增检验结果服务地址
	var ADD_LISTESTITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddLisTestItem';
	//编辑检验结果服务地址
	var EDIT_LISTESTITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestItemByField';
	//删除检验结果服务地址
	var DEL_LISTESTITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_DelLisTestItem';

	//大文本项目
	var ProEditorItemList = [];
	//项目快捷模板
	var ProEditorItemTemplate = [];
	//项目编辑器Map
	var TestItemEditorIndexList = [];
	//是否可以保存
	var IsEnableSave = true;

	//保存总数
	var SaveCount = 0;
	//保存成功数
	var SaveSuccessCount = 0;
	//保存失败数
	var SaveErrorCount = 0;

	//医嘱单列表
	var app = {
		//对外参数
		config: {
			domId: null,
			sectionid: null,
			testformrecord: null
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, app.config, setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function () {
		var me = this;
		if (ProEditorItemList.length == 0) return;

		$.each(ProEditorItemList, function (i, itemI) {
			var obj = null,//快捷模板属性
				editorid = me.config.domId + "-" + itemI["LisTestItem_LBItem_Id"] + "-editor-div",
				html = TEMPLATE_DOM.join("").replace(/{domId}/g, me.config.domId).replace(/{itemId}/g, itemI["LisTestItem_LBItem_Id"]).replace(/{项目名称}/g, itemI["LisTestItem_LBItem_CName"]);
			$('#' + me.config.domId).append(html);
			$.each(ProEditorItemTemplate, function (j, itemJ) {
				if (itemI["LisTestItem_LBItem_Id"] == itemJ["LBItemExp_LBItem_Id"]) {
					obj = itemJ;
					return false;
				}
			});
			me.initEditor(itemI["LisTestItem_Id"], itemI["LisTestItem_ReportInfo"], itemI["LisTestItem_LBItem_Id"], itemI["LisTestItem_LisOrderItem_Id"], (String(itemI["LisTestItem_LBItem_IsPartItem"]) == "true" ? true : false), editorid, obj);
		});
	};
	//初始化编辑器
	Class.prototype.initEditor = function (testitemid, reportinfo, itemid,orderitemid, ispartitem, editorid, obj) {
		var me = this,
			testitemid = testitemid || null,
			reportinfo = reportinfo || "",//特殊报告值内容
			orderitemid = orderitemid || null,
			ispartitem = ispartitem || false,
			editorid = editorid || null,
			obj = obj || null;

		if (!editorid) return;
		//当前div字体
		var FontSize = Number($("#" + editorid).css("font-size")) || 12,
			EditorHeight = obj ? ((FontSize * Number(obj["LBItemExp_DispHeight"])) + (Number(obj["LBItemExp_DispHeight"]) * 10)) : ((FontSize * 3)+(3 * 10)),//10是每行的下外边距
			tools = me.getEditorTools(obj) || {},
			tool = [];

		if (tools["sup"]) tool.push("sup");
		if (tools["sub"]) tool.push("sub");

		for (var i in tools) {
			if (i != "sup" && i != "sub") tool.push(i);
		}

		$.extend(uxeditor.config, {
			tool: tool,
			tools: tools
		});
		//构建一个默认的编辑器
		var editorindex = uxeditor.build(editorid, { height: (EditorHeight+20) });//20是编辑框的内边距（padding:10）
		TestItemEditorIndexList.push({ testitemid: testitemid, itemid: itemid, orderitemid: orderitemid, ispartitem: ispartitem, editorindex: editorindex });
		//编辑器赋值
		$("#" + me.config.domId + "-" + itemid + "-editor-div+div.layui-layedit").find("iframe")[0].contentWindow.document.body.innerHTML = reportinfo;
	};
	//获得编辑器工具tools
	Class.prototype.getEditorTools = function (obj) {
		var me = this,
			IsHyperText = obj ? String(obj["LBItemExp_IsHyperText"]) === 'true' : false,//采用上下标超文本
			IsTemplate = obj ? String(obj["LBItemExp_IsTemplate"]) === 'true' : false,//采用快捷模板
			TemplateInfo = obj["LBItemExp_TemplateInfo"],//模板内容
			arr = [],
			tools = {};

		if (IsHyperText) {
			tools["sup"] = '<i class="layui-icon layedit-tool-holiday" title="上标" lay-command="Superscript" layedit-event="sup"">上标</i>';
			tools["sub"] = '<i class="layui-icon layedit-tool-holiday" title="下标" lay-command="Subscript" layedit-event="sub"">下标</i>';
		}
		if (IsTemplate) {
			TemplateInfo = TemplateInfo.replace(/<p>/g, " ").replace(/<\/p>/g, " ").replace(/&nbsp;/g, " ").replace(/\s+/g, " ").trim();
			arr = TemplateInfo.split(" ");
			$.each(arr, function (i, item) {
				//上下标处理
				var str = item;
				if (str.substr(0, 6) == "<\/sub>" || str.substr(0, 6) == "<\/sup>") str = str.slice(6, str.length);
				//有结束没有开始
				if (str.indexOf("<\/sup>") != -1 && (str.indexOf("<sup>") == -1 || (str.indexOf("<sup>") != -1 && (str.indexOf("<\/sup>") < str.indexOf("<sup>"))))) str = "<sup>" + str;
				if (str.indexOf("<\/sub>") != -1 && (str.indexOf("<sub>") == -1 || (str.indexOf("<sub>") != -1 && (str.indexOf("<\/sub>") < str.indexOf("<sub>"))))) str = "<sub>" + str;
				//有开始没有结束
				if (str.indexOf("<sup>") != -1 && (str.indexOf("<\/sup>") == -1 || (str.indexOf("<\/sup>") != -1 && (str.lastIndexOf("<sup>") > str.lastIndexOf("<\/sup>"))))) str = str + "</sup>";
				if (str.indexOf("<sub>") != -1 && (str.indexOf("<\/sub>") == -1 || (str.indexOf("<\/sub>") != -1 && (str.lastIndexOf("<sub>") > str.lastIndexOf("<\/sub>"))))) str = str + "</sub>";

				tools[str.replace(/<[^>]+>|&[^>]+;/g, "")] = '<i class="layui-icon layedit-tool-holiday" title="' + str + '" lay-command="" layedit-event="text"">' + str + '</i>';
			});
		}

		return tools;
	};
	//监听事件
	Class.prototype.initListeners = function () {
		var me = this;
		//保存按钮处理 -- 换行改为/r/n
		$("#ProEditorSave").off().on('click', function () {
			if (!IsEnableSave) return;
			IsEnableSave = false;
			SaveCount = TestItemEditorIndexList.length;
			SaveSuccessCount = 0;
			SaveErrorCount = 0;
			for (var i = 0; i < TestItemEditorIndexList.length; i++) {
				var testitemid = TestItemEditorIndexList[i]["testitemid"],
					itemid = TestItemEditorIndexList[i]["itemid"],
					orderitemid = TestItemEditorIndexList[i]["orderitemid"],
					ispartitem = TestItemEditorIndexList[i]["ispartitem"],
					editorindex = TestItemEditorIndexList[i]["editorindex"],
					editorhtml = uxeditor.getContent(editorindex);

				me.onSaveLisTestItem(testitemid, itemid, orderitemid, ispartitem, editorhtml, function () {
					if (SaveSuccessCount + SaveErrorCount == SaveCount) {
						IsEnableSave = true;
						me.onSearch();
						layer.closeAll('loading'); //关闭加载层
						uxbase.MSG.onWarn("保存成功，成功个数：" + SaveSuccessCount + "，失败个数：" + SaveErrorCount);
						layui.event('SampleResultTable', 'onSearch', {  });
					}
				});
			}
		});
		//刷新
		$("#RefreshEditor").off().on('click', function () {
			me.onSearch();
		});
		//显示配置按钮处理
		$("#ShowItemTemplate").off().on('click', function () {
			if (!me.config.sectionid) return;
			layer.open({
				title: '项目快捷模板维护',
				type: 2,
				content: uxutil.path.UI + '/layui/app/dic/itemtemplate/index.html?SECTIONID=' + me.config.sectionid,
				maxmin: true,
				toolbar: true,
				resize: true,
				area: ['90%', '90%']
			});
		});
	};
	//刷新按钮处理
	Class.prototype.onSearch = function () {
		var me = this;
		app.render({ domId: me.config.domId, sectionid: me.config.sectionid, testformrecord: me.config.testformrecord });
	};
	//保存的检验结果数据
	Class.prototype.onSaveLisTestItem = function (testitemid, itemid, orderitemid, ispartitem, editorhtml,callback) {
		var me = this,
			testitemid = testitemid || null,
			itemid = itemid || null,
			orderitemid = orderitemid || null,
			ispartitem = ispartitem || false,
			editorhtml = editorhtml || "",
			data = { Id: testitemid, itemid: itemid, ReportValue: editorhtml.replace(/<[^>]+>|&[^>]+;/g, ""), ReportInfo: editorhtml, ReportInfoPrint: editorhtml.replace(/<p>/g, "").replace(/<\/p>/g, "\r\n").replace(/&nbsp;/g, " ") };

		if (!itemid) {
			SaveCount--;
			return;
		}

		if ((testitemid && editorhtml) || (testitemid && !editorhtml && ispartitem) || (testitemid && !editorhtml && !ispartitem && orderitemid)) {//编辑
			me.onEditLisTestItem(data, function () {
				callback();
			});
		} else if (testitemid && !editorhtml && !ispartitem && !orderitemid) {//删除
			me.onDelLisTestItem(data, function () {
				callback();
			});
		} else if (!testitemid && editorhtml) {//新增
			me.onAddLisTestItem(data, function () {
				callback();
			});
		} else {
			SaveSuccessCount++;
		}
	};
	//新增大文本项目结果
	Class.prototype.onAddLisTestItem = function (data, callback) {
		var me = this,
			data = data || {},
			url = ADD_LISTESTITEM_URL,
			params = {
				entity: {
					LisTestForm: { Id: me.config.testformrecord["LisTestForm_Id"], DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
					LBItem: { Id: data.itemid, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
					ReportValue: data.ReportValue,
					ReportInfo: data.ReportInfo,
					ReportInfoPrint: data.ReportInfoPrint
				}
			};

		var load = layer.load();
		//保存到后台
		uxutil.server.ajax({
			url: url,
			type: 'post',
			data: JSON.stringify(params)
		}, function (res) {
			if (load) layer.close(load);
			if (res.success) {
				SaveSuccessCount++;
			} else {
				SaveErrorCount++;
			}
			callback && callback();
		});
	};
	//编辑大文本项目结果
	Class.prototype.onEditLisTestItem = function (data, callback) {
		var me = this,
			data = data || {},
			url = EDIT_LISTESTITEM_URL,
			params = {
				entity: {
					Id: data.Id,
					ReportValue: data.ReportValue,
					ReportInfo: data.ReportInfo,
					ReportInfoPrint: data.ReportInfoPrint
				},
				fields:"Id,ReportValue,ReportInfo,ReportInfoPrint"
			};

		var load = layer.load();
		//保存到后台
		uxutil.server.ajax({
			url: url,
			type: 'post',
			data: JSON.stringify(params)
		}, function (res) {
			if (load) layer.close(load);
			if (res.success) {
				SaveSuccessCount++;
			} else {
				SaveErrorCount++;
			}
			callback && callback();
		});
	};
	//删除大文本项目结果
	Class.prototype.onDelLisTestItem = function (data, callback) {
		var me = this,
			data = data || {},
			url = DEL_LISTESTITEM_URL + "?id=" + data.Id;

		if (!data.Id) return;
		var load = layer.load();
		//保存到后台
		uxutil.server.ajax({
			url: url
		}, function (res) {
			if (load) layer.close(load);
			if (res.success) {
				SaveSuccessCount++;
			} else {
				SaveErrorCount++;
			}
			callback && callback();
		});
	};

	//获得该检验单下大文本项目
	Class.prototype.getProEditorItem = function (callback) {
		var me = this,
			where = ['listestitem.MainStatusID in (0,-1)', 'listestitem.LisTestForm.Id=' + me.config.testformrecord["LisTestForm_Id"], 'listestitem.LBItem.SpecialType in (1,2)'],
			fields = ['LisTestItem_Id', 'LisTestItem_LBItem_Id', 'LisTestItem_LBItem_CName', 'LisTestItem_LBItem_IsPartItem','LisTestItem_LisOrderItem_Id','LisTestItem_ReportInfo'];
		uxutil.server.ajax({
			url: GET_PROEDITOR_ITEM_URL + "&where=" + where.join(' and ') + "&fields=" + fields.join(',')
		}, function (res) {
			if (res.success) {
				var list = [];
				if (res.ResultDataValue) {
					list = res.value.list;
				}
				callback && callback(list);
			} else {
				me.clear();
			}
		});
	};
	//获得该小组下大文本项目
	Class.prototype.getProEditorItemBySection = function (callback) {
		var me = this,
			where = ['lbsection.Id =' + me.config.sectionid, 'lbitem.SpecialType != 0'],
			fields = ['LBSectionItemVO_LBItem_Id', 'LBSectionItemVO_LBItem_CName','LBSectionItemVO_LBItem_IsPartItem'];

		if (!me.config.sectionid) return;
		uxutil.server.ajax({
			url: GET_SECTION_ITEM_URL + "&where=" + where.join(' and ') + "&fields=" + fields.join(',')
		}, function (res) {
			if (res.success) {
				if (res.ResultDataValue) {
					var list = res.value.list;
					callback && callback(list);
				} else {
					me.clear();
				}
			} else {
				me.clear();
			}
		});
	};
	//获得快捷模板
	Class.prototype.getProEditorItemTemplate = function (itemids,callback) {
		var me = this,
			where = ['ItemID in (' + itemids + ')', 'SectionID=' + me.config.sectionid, 'IsUse=true'],
			fields = ['LBItemExp_Id', 'LBItemExp_LBItem_Id', 'LBItemExp_DispHeight', 'LBItemExp_IsHyperText', 'LBItemExp_IsTemplate', 'LBItemExp_TemplateInfo'];
		uxutil.server.ajax({
			url: GET_ITEM_TEMPLATE_URL + "&where=" + where.join(' and ') + "&fields=" + fields.join(',')
		}, function (res) {
			if (res.success) {
				if (res.ResultDataValue) {
					var list = res.value.list;
					callback && callback(list);
				}
			}
		});
	};
	//清空处理
	Class.prototype.clear = function () {
		var me = this;
		$("#" + me.config.domId).html('<div style="text-align:center;">不存在大文本项目!</div>');
		if (!$("#ProEditorSave").hasClass("layui-hide")) $("#ProEditorSave").addClass("layui-hide");
		if (!$("#RefreshEditor").hasClass("layui-hide")) $("#RefreshEditor").addClass("layui-hide");
		if (!$("#ShowItemTemplate").hasClass("layui-hide")) $("#ShowItemTemplate").addClass("layui-hide");
		ProEditorItemList = [];
		ProEditorItemTemplate = [];
		TestItemEditorIndexList = [];
	};

	//核心入口
	app.render = function (options) {
		var me = new Class(options);
		if (!me.config.sectionid || !me.config.testformrecord) {
			me.clear();
			return;
		}
		//获得该检验单下大文本项目
		me.getProEditorItem(function (list) {
			ProEditorItemList = list;
			me.getProEditorItemBySection(function (sectionProEditorItemList) {
				$.each(sectionProEditorItemList, function (i, itemI) {
					var flag = false;//是否存在
					$.each(ProEditorItemList, function (j, itemJ) {
						if (itemI["LBSectionItemVO_LBItem_Id"] == itemJ["LisTestItem_LBItem_Id"]) {
							flag = true;
							return false;
						}
					});
					//不存在则加入
					if (!flag) {
						ProEditorItemList.push({
							LisTestItem_Id: "",
							LisTestItem_LBItem_Id: itemI["LBSectionItemVO_LBItem_Id"],
							LisTestItem_LBItem_CName: itemI["LBSectionItemVO_LBItem_CName"],
							LisTestItem_LBItem_IsPartItem: itemI["LBSectionItemVO_LBItem_IsPartItem"],//是否允许为空 true:可以为空 不删除，false:删除
							LisTestItem_LisOrderItem_Id:"",//医嘱项目 存在则不能删除
							LisTestItem_ReportInfo:""
						});
					}
				});

				$("#" + me.config.domId).html('');
				if ($("#ProEditorSave").hasClass("layui-hide")) $("#ProEditorSave").removeClass("layui-hide");
				if ($("#RefreshEditor").hasClass("layui-hide")) $("#RefreshEditor").removeClass("layui-hide");
				if ($("#ShowItemTemplate").hasClass("layui-hide")) $("#ShowItemTemplate").removeClass("layui-hide");
				ProEditorItemTemplate = [];
				TestItemEditorIndexList = [];
				var items = [];
				$.each(ProEditorItemList, function (i, item) {
					items.push(item["LisTestItem_LBItem_Id"]);
				});
				//获得快捷模板
				me.getProEditorItemTemplate(items.join(), function (TemplateList) {
					ProEditorItemTemplate = TemplateList;
					me.initHtml();
				});
			});
		});
		//监听事件
		me.initListeners();

		return me;
	};

	//暴露接口
	exports(MOD_NAME, app);
});