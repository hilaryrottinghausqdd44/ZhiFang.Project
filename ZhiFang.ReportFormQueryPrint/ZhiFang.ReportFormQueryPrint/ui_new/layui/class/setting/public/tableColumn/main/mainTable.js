/**
	@name：页面配置表格列
	@author：guohx
	@version 2020-07-29
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form;

    var config = { GridID: null, GridCode: null, CName: null, isHideColumnToolbar: false, isSaveSuccessLoadTable:false };

    var mainTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
            elem: '',
            id: "",
            checkRowData: [],
            /**默认传入参数*/
            defaultParams: {},
            /**默认数据条件*/
            defaultWhere: '',
            /**内部数据条件*/
            internalWhere: '',
            /**外部数据条件*/
            externalWhere: '',
            /**是否默认加载*/
            defaultLoad: false,
            /**列表当前排序*/
            sort: [{
                "property": 'DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridControlListByGridCode?isPlanish=true',
            addUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/AddBModuleGridControlList',
            editUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/UpdateBModuleGridControlList',
            delUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/deleteBModuleGridControlList',
            where: "",
            toolbar: '#tableToolbar',
            defaultToolbar: ['filter'],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'checkbox' },
                { type: 'numbers', title: '行号' },
                { field: 'GridControlID', width: 80, title: '主键ID', sort: false, hide: true },
                { field: 'LabID', width: 80, title: '实验室ID', sort: false, hide: true },
                { field: 'LabNo', title: '实验室No', width: 120, sort: true, hide: true },
                { field: 'GridID', title: '列表集合主键', width: 120, sort: true, hide:true },
                { field: 'GridCode', title: '列表代码', width: 120, sort: true },
                { field: 'CName', title: '列表名称', width: 120, sort: true },
                { field: 'TypeID', title: '控件类型Id', width: 120, sort: true, hide: true },
                { field: 'TypeName', title: '控件类型', width: 120, sort: true },
                { field: 'MapField', title: '对应属性', width: 120, sort: true },
                { field: 'ColName', title: '列头名称', width: 120, sort: true },
                { field: 'StyleContent', title: '控件样式', width: 120, sort: true },
                { field: 'ColData', title: '数据扩展', width: 120, sort: false },
                { field: 'Width', title: '宽度', width: 120, sort: true },
                { field: 'MinWidth', title: '最小宽度', width: 120, sort: true },
                { field: 'Edit', title: '编辑类型', width: 120, sort: true },
                { field: 'Toolbar', title: '工具条模板', width: 120, sort: true },
                { field: 'Align', title: '排序方式', width: 120, sort: true },
                { field: 'Fixed', title: '固定列', width: 120, sort: true },
                {
                    field: 'IsOrder', title: '是否排序', width: 120, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.IsOrder) == "true")
                            str = "<span style='color:green;'>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                { field: 'OrderType', title: '排序类型', width: 80, sort: true },
                {
                    field: 'IsUse', title: '是否使用', width: 120, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.IsUse) == "true")
                            str = "<span style='color:green;'>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                {
                    field: 'IsHide', title: '是否隐藏', width: 120, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.IsHide) == "true")
                            str = "<span style='color:green;'>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                { field: 'DispOrder', title: '显示次序', width: 120, sort: true }
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
                var data = res.ResultDataValue ? eval('('+res.ResultDataValue+')') : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                //无数据处理
                if (count == 0) return;
                //触发点击事件
                $("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, mainTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, mainTable, Class.pt, me);// table,
        return me;
    };
    Class.pt = Class.prototype;
    //获取查询Url
    Class.pt.getLoadUrl = function () {
        var me = this, arr = [];
        var url = me.config.selectUrl;
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
        //默认条件
        if (me.config.defaultWhere && me.config.defaultWhere != '') {
            arr.push(me.config.defaultWhere);
        }
        //内部条件
        if (me.config.internalWhere && me.config.internalWhere != '') {
            arr.push(me.config.config.internalWhere);
        }
        //外部条件
        if (me.config.externalWhere && me.config.externalWhere != '') {
            arr.push(me.config.externalWhere);
        }
        //传入的默认参数处理
        if (me.config.defaultParams) {

        }
        if (config.GridCode) arr.push("GridCode='" + config.GridCode + "'");
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + where;
        }
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获取查询Fields
    Class.pt.getStoreFields = function (isString) {
        var me = this,
            columns = me.config.cols[0] || [],
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
    //核心入口
    mainTable.render = function (options, isHideColumnToolbar, isSaveSuccessLoadTable) {
        var me = this;
        if (isHideColumnToolbar) config.isHideColumnToolbar = isHideColumnToolbar;
        config.isSaveSuccessLoadTable = isSaveSuccessLoadTable;
        var inst = new Class(options);
        if (!config.isHideColumnToolbar) inst.config.cols[0].push({ title: '操作', width: 150, align: 'center', toolbar: '#tableOperation', fixed: 'right' });
        inst.tableIns = table.render(inst.config);
        me.set(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    mainTable.onSearch = function (mytable, options) {
        var me = this;
        var inst = new Class(me);
        if (options) config = $.extend({}, config, options);
        mainTable.url = inst.getLoadUrl();
        mainTable.config.elem = "#" + mytable;
        mainTable.config.id = mytable;
        table.reload(mytable, {
            url: inst.getLoadUrl()
        });
    };
    Class.pt.iniListeners = function () {
        var me = this;
        //监听表格行工具事件
        table.on('tool(mainTable)', function (obj) {
            var data = obj.data, //获得当前行数据
                layEvent = obj.event;
            if (layEvent === 'del') { //删除
                me.delClick(data);
            } else if (layEvent === 'edit') { //编辑
                me.editClick(data);
            }
        });
        //监听表格头工具栏
        table.on('toolbar(mainTable)', function (obj) {
            var layEvent = obj.event;
            if (layEvent == 'add') {//新增
                me.addClick();
            }
        });
        //监听行单击事件
        table.on('row(mainTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        });
        //监听排序事件
        table.on('sort(mainTable)', function (obj) {
            var field = obj.field;//排序字段
            var type = obj.type;//升序还是降序
            //修改默认的排序字段
            me.config.sort = [{
                "property": field,
                "direction": type
            }];
            table.reload('mainTable', {
                initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
                url: me.getLoadUrl(),
                where: {
                    time: new Date().getTime()
                }
            });
        });
    };
    //新增
    Class.pt.addClick = function () {
        var me = this;
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['新增配置项', 'font-weight:bold;'],
            skin: 'layui-layer-lan',
            area: ['740px', '390px'],
            content: $('#modal'),
            yes: function (index, layero) {
                var formData = $("#modalForm").serialize().split("&"),
                    entity = {};
                $.each(formData, function (i, item) {
                    entity[item.split("=")[0]] = decodeURIComponent(item.split("=")[1]);
                });
                entity["GridID"] = config.GridID ? config.GridID : 0;
                entity["GridCode"] = config.GridCode ? config.GridCode : "";
                entity["CName"] = config.CName ? config.CName : "";
                entity["ColData"] = $("#ColData").val();
                entity["LabID"] = entity["LabID"] ? entity["LabID"] : 0;
                entity["LabNo"] = $("#LabID").val() ? $("#LabID option:selected").attr("data-labno") : "";
                entity["TypeName"] = $("#TypeID option:selected").text();
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["IsOrder"] = $("#IsOrder").prop("checked") ? 1 : 0;
                entity["IsHide"] = $("#IsHide").prop("checked") ? 1 : 0;
                entity["DispOrder"] = (entity["DispOrder"] && !isNaN(entity["DispOrder"])) ? entity["DispOrder"] : 0;
                me.onSaveClick({ entity: entity },'add',index);
            },
            end: function () {
                me.clear();
            } 
        });
    };
    //编辑
    Class.pt.editClick = function (data) {
        var me = this,
            list = JSON.parse(JSON.stringify(data));
        list.IsUse = String(list.IsUse) == "true" ? true : false;
        list.IsOrder = String(list.IsOrder) == "true" ? true : false;
        list.IsHide = String(list.IsHide) == "true" ? true : false;
        form.val('modalForm', list);
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['编辑配置项', 'font-weight:bold;'],
            skin: 'layui-layer-lan',
            area: ['740px', '390px'],
            content: $('#modal'),
            yes: function (index, layero) {
                window.event.preventDefault();
                var formData = $("#modalForm").serialize().split("&"),
                    entity = {};
                $.each(formData, function (i, item) {
                    entity[item.split("=")[0]] = decodeURIComponent(item.split("=")[1]);
                });
                entity["ColData"] = $("#ColData").val();
                entity["LabID"] = entity["LabID"] ? entity["LabID"] : 0;
                entity["LabNo"] = $("#LabID").val() ? $("#LabID option:selected").attr("data-labno") : "";
                entity["TypeName"] = $("#TypeID option:selected").text();
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["IsOrder"] = $("#IsOrder").prop("checked") ? 1 : 0;
                entity["IsHide"] = $("#IsHide").prop("checked") ? 1 : 0;
                entity["DispOrder"] = (entity["DispOrder"] && !isNaN(entity["DispOrder"])) ? entity["DispOrder"] : 0;
                var fields = "";
                $.each(entity, function (j, itemJ) {
                    if (!fields)
                        fields = j;
                    else
                        fields += "," + j;
                });
                me.onSaveClick({ entity: entity, fields: fields }, 'edit', index);
            },
            end: function () {
                me.clear();
            }
        });
    };
    //删除
    Class.pt.delClick = function (data) {
        var me = this,
            id = data.GridControlID;
        if (!id) return;
        var url = me.config.delUrl;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            //显示遮罩层
            var indexs = layer.load();
            uxutil.server.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify({GridControlListID:[id]}),
            }, function (data) {
                layer.close(indexs);
                if (data.success === true) {
                    layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 });
                    if (config.isSaveSuccessLoadTable)
                        table.reload(me.config.id, {});
                    else
                        layui.event("leftTableSarch", "search", {});
                } else {
                    layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
                }
            });
        });
    };
    //表单保存处理
    Class.pt.onSaveClick = function (data, type, index) {
        var me = this,
            url = type == 'add' ? me.config.addUrl : me.config.editUrl,
            params = data;
        if (!url) return;
        if (!params.entity.TypeID) return;
        if (type == 'add' && !params.entity.GridCode) return;
        if (type == "add") delete params.entity.GridControlID;
        params = JSON.stringify({ModuleGridControlList:[params.entity]});
        
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
                Class.pt.clear();
                if (config.isSaveSuccessLoadTable)
                    table.reload(me.config.id, {});
                else
                    layui.event("leftTableSarch", "search", {});
            } else {
                var msg = type == 'add' ? '新增失败！' : '修改失败！';
                if (!data.msg) data.msg = msg;
                layer.msg(data.msg, { icon: 5, anim: 6 });
            }
        });
    };
    //清空表单
    Class.pt.clear = function () {
        var me = this;
        $("#modalForm :input").each(function (i, item) {
            if ($(this).attr("name") != "TypeID") $(this).val("");
        });
        form.render();
    };
    //暴露接口
    exports('mainTable', mainTable);
});
