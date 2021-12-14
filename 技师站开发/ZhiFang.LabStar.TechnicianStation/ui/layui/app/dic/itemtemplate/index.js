/**
   @Name：项目快捷模板
   @Author：zhangda
   @version 2021-11-05
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base',
    uxtable: 'ux/table',
    uxeditor: 'modules/common/editor'
}).use(['uxutil', 'uxbase', 'uxtable', 'form', 'table','uxeditor'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        table = layui.table,
        uxtable = layui.uxtable,
        uxbase = layui.uxbase,
        uxeditor = layui.uxeditor,
        uxutil = layui.uxutil;

    //外部参数
    var PARAMS = uxutil.params.get(true);
    //小组
    var SECTIONID = PARAMS.SECTIONID || null;
    //小组项目获取服务地址
    var GET_SECTION_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemVOByHQL?isPlanish=true';
    //查询项目快捷模板服务地址
    var GET_ITEM_TEMPLATE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemExpByHQL?isPlanish=true';
    //新增项目快捷模板服务地址
    var ADD_ITEM_TEMPLATE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBItemExp';
    //编辑项目快捷模板服务地址
    var EDIT_ITEM_TEMPLATE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemExpByField';
    //列表实例
    var TableInstance = null;
    //列表id
    var TableID = "maintable";
    //小组项目数据
    var SectionItemList = [];
    //小组项目加载状态
    var SectionItemLoadStatus = false;
    //项目快捷模板加载状态
    var ItemTemplateLoadStatus = false;
    //项目快捷模板数据
    var ItemTemplateList = [];
    //项目快捷模板查询字段
    var ItemTemplateFields = 'LBItemExp_Id,LBItemExp_LBItem_Id,LBItemExp_LBItem_CName,LBItemExp_DispOrder,LBItemExp_DispHeight,LBItemExp_IsHyperText,LBItemExp_IsTemplate,LBItemExp_TemplateInfo,LBItemExp_IsUse';
    //当前行数据
    var CheckRowData = [];
    //保存数据总数
    var SaveCount = 0;
    //保存数据总数
    var SaveSuccessCount = 0;
    //保存数据总数
    var SaveErrorCount = 0;
    //编辑器
    var editorIndex = null;

    
    //查询小组项目
    function getSectionItem() {
        var url = GET_SECTION_ITEM_URL + "&where=lbsection.Id='" + SECTIONID + "' and lbitem.SpecialType!=0";
        url += "&fields=LBSectionItemVO_LBItem_Id,LBSectionItemVO_LBItem_CName";
        url += "&sort=[{'property':'LBSectionItem_DispOrder','direction':'ASC'},{'property':'LBItem_DispOrder','direction':'ASC'}]";
        uxutil.server.ajax({
            url: url
        }, function (res) {
            SectionItemLoadStatus = true;
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = $.parseJSON(res.ResultDataValue).list;
                    SectionItemList = JSON.parse(JSON.stringify(list));;
                }
                if (SectionItemLoadStatus && ItemTemplateLoadStatus) autoSave();
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //查询已存在项目快捷模板
    function getItemTemplate() {
        var url = GET_ITEM_TEMPLATE_URL + "&where=SectionID='" + SECTIONID + "'";
        url += "&fields=" + ItemTemplateFields;
        url += "&sort=[{'property':'LBItemExp_DispOrder','direction':'ASC'}]";
        uxutil.server.ajax({
            url: url
        }, function (res) {
            ItemTemplateLoadStatus = true;
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = $.parseJSON(res.ResultDataValue).list;
                    ItemTemplateList = JSON.parse(JSON.stringify(list));
                }
                if (SectionItemLoadStatus && ItemTemplateLoadStatus) autoSave();
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //项目快捷模板表中不存在的小组大文本项目自动保存
    function autoSave() {
        var Savelist = [], load = layer.load();

        $.each(SectionItemList, function (i, itemi) {
            var flag = false;
            $.each(ItemTemplateList, function (j, itemj) {
                if (itemj["LBItemExp_LBItem_Id"] == itemi["LBSectionItemVO_LBItem_Id"]) {
                    flag = true;
                    return false;
                }
            });
            if (!flag) {
                Savelist.push({
                    SectionID: SECTIONID,
                    LBItem: { Id: itemi["LBSectionItemVO_LBItem_Id"], DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 1] },
                    DispOrder: (ItemTemplateList.length > 0 ? (Number(ItemTemplateList[ItemTemplateList.length - 1]["LBItemExp_DispOrder"]) + Savelist.length + 2) : (Savelist.length + 1)),
                    ExpType:0,
                    DispHeight: 3,
                    IsHyperText: 0,
                    IsTemplate: 0,
                    IsUse:1
                });
            }
        });
        //保存未添加项目
        if (Savelist.length > 0) {
            SaveCount = Savelist.length;//设置保存总数
            SaveErrorCount = 0;//设置保存失败数
            SaveSuccessCount = 0;//设置保存成功数
            $.each(Savelist, function (i,item) {
                addSaveOne(item, i, function () {
                    layer.close(load);
                    initTable();
                });
            });
        } else {
            layer.close(load);
            initTable();
        }
    };
    //新增一行数据
    function addSaveOne(entity, index, callback) {
        setTimeout(function () {
            var config = {
                type: "POST",
                url: ADD_ITEM_TEMPLATE_URL,
                data: JSON.stringify({ entity: entity })
            };
            uxutil.server.ajax(config, function (res) {
                if (res.success) {
                    SaveSuccessCount++;
                } else {
                    SaveErrorCount++;
                }
                if (SaveSuccessCount + SaveErrorCount == SaveCount) {
                    callback && callback();
                }
            });
        }, 100 * index);
    };
    //初始化列表
    function initTable() {
        TableInstance = uxtable.render({
            elem: '#' + TableID,
            height: 'full-40',
            url: GET_ITEM_TEMPLATE_URL + "&where=SectionID='" + SECTIONID + "'&fields=" + ItemTemplateFields +"&sort=[{'property':'LBItemExp_DispOrder','direction':'ASC'}]",
            toolbar: '#toolbar',
            defaultToolbar:[],
            page: false,
            limit: 1000,
            limits: [100, 200, 500, 1000, 1500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号' },
                { field: 'LBItemExp_Id', width: 140, title: '主键', hide: true },
                { field: 'LBItemExp_LBItem_Id', width: 140, title: '项目编号', hide: true },
                { field: 'LBItemExp_LBItem_CName', minWidth: 120, title: '项目名称' },
                { field: 'LBItemExp_DispOrder', width: 60, title: '显示次序', edit:'text' },
                { field: 'LBItemExp_DispHeight', width: 130, title: '录入框高度(字体倍数)', edit: 'text' },
                { field: 'LBItemExp_IsHyperText', width: 70, title: '显示上下标', align: 'center', templet: '#IsHyperTextTp' },
                { field: 'LBItemExp_IsTemplate', width: 90, title: '显示快捷模板', align: 'center', templet: '#IsTemplateTp' },
                { field: 'LBItemExp_IsUse', width: 50, title: '在用',align:'center', templet: '#IsUseTp' },
                { field: 'LBItemExp_TemplateInfo', width: 100, title: '模板内容', hide: true },
                { field: 'Tab', width: 100, title: '用于判断行是否有修改数据', hide: true }
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                if (count == 0) return;
                $("#" + TableID+"+div").find('.layui-table-main tr[data-index="0"]').click();
            }
        });
        //初始化监听
        initListeners();
    };
    //列表保存
    function onTableSave() {
        var tablecache = table.cache[TableID] || [],
            Savelist = [],
            load = layer.load();
        
        if (tablecache.length == 0) {
            uxbase.MSG.onWarn("没有修改的数据不需要保存!");
            layer.close(load);
            return;
        }
        
        $.each(tablecache, function (i, item) {
            if (item["Tab"]) {
                Savelist.push({
                    Id: item["LBItemExp_Id"],
                    DispOrder: item["LBItemExp_DispOrder"],
                    DispHeight: item["LBItemExp_DispHeight"],
                    IsHyperText: item["LBItemExp_IsHyperText"],
                    IsTemplate: item["LBItemExp_IsTemplate"],
                    IsUse: item["LBItemExp_IsUse"]
                });
            }
        });
        //保存需要修改数据
        if (Savelist.length > 0) {
            SaveCount = Savelist.length;//设置保存总数
            SaveErrorCount = 0;//设置保存失败数
            SaveSuccessCount = 0;//设置保存成功数
            $.each(Savelist, function (i, item) {
                updateSaveOne(item, i, function () {
                    layer.close(load);
                    uxbase.MSG.onWarn("成功数量：" + SaveSuccessCount + "个，失败数量：" + SaveErrorCount+"个");
                });
            });
        } else {
            uxbase.MSG.onWarn("没有修改的数据不需要保存!");
            layer.close(load);
            return;
        }
    };
    //更新一行数据
    function updateSaveOne(entity, index, callback) {
        setTimeout(function () {
            var fields = [];
            for (var i in entity) {
                fields.push(i);
            }
            var config = {
                type: "POST",
                url: EDIT_ITEM_TEMPLATE_URL,
                data: JSON.stringify({ entity: entity, fields: fields.join() })
            };
            uxutil.server.ajax(config, function (res) {
                if (res.success) {
                    SaveSuccessCount++;
                } else {
                    SaveErrorCount++;
                }
                if (SaveSuccessCount + SaveErrorCount == SaveCount) {
                    callback && callback();
                }
            });
        }, 100 * index);
    };
    //初始化富文本编辑器
    function initEditor() {
        $.extend(uxeditor.config, {
            tool: [
                'sup', 'sub'//, 'CO2'
            ],
            tools: {
                sup: '<i class="layui-icon layedit-tool-holiday" title="上标" lay-command="Superscript" layedit-event="sup"">上标</i>',
                sub: '<i class="layui-icon layedit-tool-holiday" title="下标" lay-command="Subscript" layedit-event="sub"">下标</i>'
                //,CO2: '<i class="layui-icon layedit-tool-holiday" title="CO2" lay-command="" layedit-event="text"">CO<sup>2</sup>&nbsp;</i>'
            }
        });
        var height = $(window).height();
        //构建一个默认的编辑器
        editorIndex = uxeditor.build('editor-div', { height: (height - 160) });
    };
    //初始化页面
    function initHtml() {
        getSectionItem();
        getItemTemplate();
    };
    //监听事件
    function initListeners() {
        //单击行事件
        TableInstance.table.on('row(' + TableID + ')', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            CheckRowData = [];
            CheckRowData.push(obj["data"]);
            //赋值模板
            $("#editor-div+div.layui-layedit").find("iframe")[0].contentWindow.document.body.innerHTML = obj["data"]["LBItemExp_TemplateInfo"] || "";
            //清除编辑器工具选中样式
            $("#editor-div+div.layui-layedit").find(".layui-layedit-tool>i").removeClass("layedit-tool-active");

        });
        //监听列表头工具栏
        table.on('toolbar(' + TableID + ')', function (obj) {
            switch (obj.event) {
                case 'save':
                    onTableSave();//列表保存
                    break;
            };
        });
        //监听单元格编辑
        TableInstance.table.on('edit(' + TableID + ')', function (obj) {
            var value = obj.value, //得到修改后的值
                data = obj.data,//得到所在行所有键值
                field = obj.field, //得到字段
                tablecache = table.cache[TableID];
            //添加修改标记
            $.each(tablecache, function (i, item) {
                if (item["LBItemExp_Id"] == data["LBItemExp_Id"]) item["Tab"] = "edit";
            });
        });
        //显示上下标
        form.on('checkbox(IsHyperText)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr'),
                dataindex = elem.attr("data-index"),
                tableCache = table.cache[TableID];

            $.each(tableCache, function (i, item) {
                if (item["LAY_TABLE_INDEX"] == dataindex) {
                    item["LBItemExp_IsHyperText"] = data.elem.checked;
                    item["Tab"] = "edit";
                }
            });
        });
        //显示快捷模板
        form.on('checkbox(IsTemplate)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr'),
                dataindex = elem.attr("data-index"),
                tableCache = table.cache[TableID];

            $.each(tableCache, function (i, item) {
                if (item["LAY_TABLE_INDEX"] == dataindex) {
                    item["LBItemExp_IsTemplate"] = data.elem.checked;
                    item["Tab"] = "edit";
                }
            });
        });
        //在用
        form.on('checkbox(IsUse)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr'),
                dataindex = elem.attr("data-index"),
                tableCache = table.cache[TableID];

            $.each(tableCache, function (i, item) {
                if (item["LAY_TABLE_INDEX"] == dataindex) {
                    item["LBItemExp_IsUse"] = data.elem.checked;
                    item["Tab"] = "edit";
                }
            });
        });
        //模板内容保存按钮
        $("#Save").on('click', function () {
            if (CheckRowData.length == 0) return;
            var html = uxeditor.getContent(editorIndex);
            var entity = { Id: CheckRowData[0]["LBItemExp_Id"], TemplateInfo: html };
            SaveCount = 1;//设置保存总数
            SaveErrorCount = 0;//设置保存失败数
            SaveSuccessCount = 0;//设置保存成功数
            updateSaveOne(entity, 0, function () {
                if (SaveSuccessCount != 0) {
                    TableInstance.updateRowItem({ LBItemExp_Id: CheckRowData[0]["LBItemExp_Id"], LBItemExp_TemplateInfo: html },"LBItemExp_Id");
                    uxbase.MSG.onSuccess("保存成功!");
                } else {
                    uxbase.MSG.onError("保存失败!");
                }
            });
        });
    };
	//初始化
    function init() {
        if (!SECTIONID) return;
        //初始化页面
        initHtml();
        //初始化富文本编辑器
        initEditor();
	};
    //初始化
    init();
});