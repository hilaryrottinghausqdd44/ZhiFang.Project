/**
	@name：模块表单表格配置应用关系
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

    var config = { ModuleID: null };

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
            selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleModuleFormGridLink?isPlanish=true',
            addUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/AddBModuleModuleFormGridLink',
            editUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/UpdateBModuleModuleFormGridLink',
            delUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/deleteBModuleModuleFormGridLinkByModuleID',
            //新增集合
            addFormListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/AddBModuleFormList',
            addGridListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/AddBModuleGridList',
            addChartListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/AddBModuleChartList',
            //编辑集合
            editFormListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/UpdateBModuleFormList',
            editGridListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/UpdateBModuleGridList',
            editChartListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/UpdateBModuleChartList',
            //根据Id获得具体集合信息
            getFormListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleFormList',
            getGridListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridList',
            getChartListUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleChartList',
            where: "",
            toolbar: '#Toolbar',
            defaultToolbar: ['filter'],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [],
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
        if (config.ModuleID) arr.push("ModuleID='" + config.ModuleID + "'");
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + where;
        }
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获取列
    Class.pt.getCols = function (role) {
        var me = this,
            role = role || '',
            cols = [];
        cols = [
            { type: 'checkbox' },
            { type: 'numbers', title: '行号' },
            { field: 'ModuleFormGridLinkID', width: 80, title: '主键ID', sort: false, hide: true },
            { field: 'LabID', width: 80, title: '实验室ID', sort: false, hide: true },
            { field: 'LabNo', title: '实验室No', width: 120, sort: true, hide: true },
            { field: 'ModuleID', width: 80, title: '模块ID', sort: false, hide: true },
            { field: 'FormID', title: '表单集合Id', width: 120, sort: true, hide: true },
            { field: 'FormCode', title: '表单代码', width: 140, sort: true, hide: true },
            { field: 'GridID', title: '表格集合Id', width: 120, sort: true, hide: true },
            { field: 'GridCode', title: '表格代码', width: 140, sort: true, hide: true },
            { field: 'ChartID', title: '图表集合Id', width: 120, sort: true, hide: true },
            { field: 'ChartCode', title: '图表代码', width: 140, sort: true, hide: true },
            { field: 'CName', title: '名称', width: 140, sort: true, hide: role != 'system' ? true : false },
            { field: 'ShortName', title: '简称', width: 140, sort: true },
            { field: 'ShortCode', title: '简码', width: 140, sort: true, hide: true },
            {
                field: '', title: '类型', width: 120, sort: true,
                templet: function (data) {
                    var str = "";
                    if (data.FormCode)
                        str = "<span style='color:green;'>表单配置</span>";
                    else if (data.GridCode)
                        str = "<span style='color:red;'>表格配置</span>";
                    else if (data.ChartCode)
                        str = "<span style='color:blue;'>图表配置</span>";
                    return str;
                }
            },
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
            { field: 'DispOrder', title: '显示次序', width: 120, sort: true, hide: true },
            { title: '操作', width: 100, align: 'center', toolbar: '#tableOperation', fixed: 'right' }
        ];
        return [cols];
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
    mainTable.render = function (options) {
        var me = this;
        var inst = new Class(options);
        inst.config.cols = Class.pt.getCols(options.role);
        me.config.cols = Class.pt.getCols(options.role);
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    mainTable.onSearch = function (mytable, ModuleID) {
        var me = this;
        if (ModuleID) config.ModuleID = ModuleID;
        var inst = new Class(me);
        mainTable.url = inst.getLoadUrl();
        mainTable.config.elem = "#" + mytable;
        mainTable.config.id = mytable;
        table.reload(mytable, {
            url: inst.getLoadUrl()
        });
    };
    //监听
    Class.pt.iniListeners = function () {
        var me = this;
        //监听表格头工具栏
        table.on('toolbar(mainTable)', function (obj) {
            var layEvent = obj.event,
                checkData = table.checkStatus(me.config.id).data;
            switch (layEvent) {
                case "addList"://新增集合
                    me.addListClick();
                    break;
                case "editList"://修改集合
                    if (checkData && checkData.length != 1)
                        layer.msg("请勾选一条数据进行该集合修改！");
                    else
                        me.editListClick(checkData[0]);
                    break;
                case "add"://新增配置项
                    if (config.ModuleID) 
                        me.addClick();
                    else
                        layer.msg("请选择模块节点!");
                    break;
                case "edit"://修改勾选配置项
                    if (checkData && checkData.length != 1) 
                        layer.msg("请勾选一条数据进行修改！");
                    else
                        me.editClick(checkData[0]);
                    break;
                case "del"://删除勾选配置项
                    if (checkData && checkData.length == 0) 
                        layer.msg("请勾选需要删除的数据！");
                    else
                        me.delClick(checkData);
                    break;

            }
        });
        //监听表格行工具事件
        table.on('tool(mainTable)', function (obj) {
            var data = obj.data, //获得当前行数据
                layEvent = obj.event;
            if (layEvent === 'set') { //设置配置项
                me.setConfigItem(data);
            }
        });
        //监听行双击事件
        table.on('rowDouble(mainTable)', function (obj) {
            var data = obj.data; //获得当前行数据
            me.setConfigItem(data);
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
            if (!table.cache[me.config.id] || table.cache[me.config.id].length == 0) return;
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
        //监听集合类型下拉框
        form.on('select(addListModalFormListType)', function (data) {
            var value = data.value; //得到被选中的值
            if (value == "FormCode") {
                $("#addListModalFormTypeID").html('<option value="">请选择</option><option value = "1" >查询</option ><option value="2">编辑</option>');
                $("#addListModalFormClassID").html('<option value="">请选择</option><option value = "1" >普通</option ><option value="2">收缩</option>');
            } else if (value == "GridCode") {
                $("#addListModalFormTypeID").html('<option value="">请选择</option><option value = "1" >普通分页</option ><option value="2">合计</option><option value="3">固定列</option>');
                $("#addListModalFormClassID").html('<option value="">请选择</option>');
            }
            form.render('select');
        });  
    };
    //新增集合
    Class.pt.addListClick = function () {
        var me = this;
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['新增集合', 'font-weight:bold;'],
            skin: 'layui-layer-lan',
            area: ['650px', '400px'],
            content: $('#addListModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["LabID"] = $("#addListModalFormLabID").val() ? $("#addListModalFormLabID").val() : 0;
                entity["LabNo"] = $("#addListModalFormLabID").val() ? $("#addListModalFormLabID option:selected").attr("data-labno") : "";
                entity[$("#addListModalFormListType").val()] = $("#addListModalFormCode").val();
                entity["CName"] = $("#addListModalFormCName").val();
                entity["ShortCode"] = $("#addListModalFormShortCode").val();
                entity["ShortName"] = $("#addListModalFormShortName").val();
                entity["TypeID"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID").val() : 0;
                entity["TypeName"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID option:selected").text() : "";
                entity["ClassID"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID").val() : 0;
                entity["ClassName"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID option:selected").text() : "";
                entity["SourceCodeUrl"] = $("#SourceCodeUrl").val();
                entity["SourceCode"] = $("#SourceCode").val();
                entity["IsUse"] = $("#addListModalFormIsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#addListModalFormDispOrder").val() && !isNaN($("#addListModalFormDispOrder").val())) ? $("#addListModalFormDispOrder").val() : 0;
                var url = "";
                switch ($("#addListModalFormListType").val()) {
                    case "FormCode":
                        url = me.config.addFormListUrl;
                        break;
                    case "GridCode":
                        url = me.config.addGridListUrl;
                        break;
                    case "ChartCode":
                        url = me.config.addChartListUrl;
                        break;
                    default:
                        url = me.config.addFormListUrl;
                        break;
                }
                //显示遮罩层
                var indexs = layer.load();
                var configs = {
                    type: "POST",
                    url: url,
                    data: JSON.stringify({ entity:entity })
                };
                uxutil.server.ajax(configs, function (data) {
                    //隐藏遮罩层
                    layer.close(indexs);
                    if (data.success) {
                        layer.msg("新增集合成功,请到具体模块进行新增配置项!", { icon: 6, anim: 0, time: 3000 });
                        layui.event('loadList', 'load', { type: $("#addListModalFormListType").val() });
                        if (index) layer.close(index);
                    } else {
                        layer.msg("新增集合失败！", { icon: 5, anim: 6 });
                    }
                });
            },
            end: function () {
                me.clear();
            }
        });
    };
    //修改集合
    Class.pt.editListClick = function (data) {
        var me = this,
            Id = null,
            IdField = null,
            type = null;
        if (data["FormID"] && data["FormID"] != 0) {
            Id = data["FormID"];
            IdField = 'FormID';
            type = 'FormCode';
        } else if (data["GridID"] && data["GridID"] != 0) {
            Id = data["GridID"];
            IdField = 'GridID';
            type = 'GridCode';
        } else if (data["ChartID"] && data["ChartID"] != 0) {
            Id = data["ChartID"];
            IdField = 'ChartCID';
            type = 'ChartCode';
        }
        if (!Id) return;
        me.getListInfoByListID(Id, type, function (list) {
            form.val('addListModalForm', list);
            form.render();
        });
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['修改集合', 'font-weight:bold;'],
            skin: 'layui-layer-lan',
            area: ['650px', '400px'],
            content: $('#addListModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["LabID"] = $("#addListModalFormLabID").val() ? $("#addListModalFormLabID").val() : 0;
                entity["LabNo"] = $("#addListModalFormLabID").val() ? $("#addListModalFormLabID option:selected").attr("data-labno") : "";
                entity[IdField] = Id;
                entity[$("#addListModalFormListType").val()] = $("#addListModalFormCode").val();
                entity["CName"] = $("#addListModalFormCName").val();
                entity["ShortCode"] = $("#addListModalFormShortCode").val();
                entity["ShortName"] = $("#addListModalFormShortName").val();
                entity["TypeID"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID").val() : 0;
                entity["TypeName"] = $("#addListModalFormTypeID").val() ? $("#addListModalFormTypeID option:selected").text() : "";
                entity["ClassID"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID").val() : 0;
                entity["ClassName"] = $("#addListModalFormClassID").val() ? $("#addListModalFormClassID option:selected").text() : "";
                entity["SourceCodeUrl"] = $("#SourceCodeUrl").val();
                entity["SourceCode"] = $("#SourceCode").val();
                entity["IsUse"] = $("#addListModalFormIsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#addListModalFormDispOrder").val() && !isNaN($("#addListModalFormDispOrder").val())) ? $("#addListModalFormDispOrder").val() : 0;
                var url = "";
                switch ($("#addListModalFormListType").val()) {
                    case "FormCode":
                        url = me.config.editFormListUrl;
                        break;
                    case "GridCode":
                        url = me.config.editGridListUrl;
                        break;
                    case "ChartCode":
                        url = me.config.editChartListUrl;
                        break;
                    default:
                        url = me.config.editFormListUrl;
                        break;
                }
                var fields = "";
                $.each(entity, function (j, itemJ) {
                    if (!fields)
                        fields = j;
                    else
                        fields += "," + j;
                });
                //显示遮罩层
                var indexs = layer.load();
                var configs = {
                    type: "POST",
                    url: url,
                    data: JSON.stringify({ entity: entity, fields: fields })
                };
                uxutil.server.ajax(configs, function (data) {
                    //隐藏遮罩层
                    layer.close(indexs);
                    if (data.success) {
                        layer.msg("修改集合成功,请查看关联模块内容!", { icon: 6, anim: 0, time: 3000 });
                        layui.event('loadList', 'load', { type: $("#addListModalFormListType").val() });
                        if (index) layer.close(index);
                    } else {
                        layer.msg("修改集合失败！", { icon: 5, anim: 6 });
                    }
                });
            },
            success: function () {
                $("#addListModalFormListType").addClass("layui-disabled").attr("disabled", "disabled");
                $("#addListModalFormCode").addClass("layui-disabled").attr("disabled", "disabled");
            },
            end: function () {
                $("#addListModalFormListType").removeClass("layui-disabled").attr("disabled", false);
                $("#addListModalFormCode").removeClass("layui-disabled").attr("disabled", false);
                me.clear();
            }
        });
    };
    //根据集合类型，Id获得集合具体信息
    Class.pt.getListInfoByListID = function (Id,type,callBack) {
        var me = this,
            Id = Id || null,
            url = '',
            type = type || 'FormCode';
        if (!Id) return;
        if (type == 'ChartCode')
            url = me.config.getChartListUrl + "?fields=LabID,LabNo,ChartID,ChartCode,TypeID,TypeName,ClassID,ClassName,CName,ShortName,ShortCode,IsUse,DispOrder,SourceCodeUrl,SourceCode,Memo";
        else if (type == 'GridCode'){
            url = me.config.getGridListUrl + "?fields=LabID,LabNo,GridID,GridCode,TypeID,TypeName,ClassID,ClassName,CName,ShortName,ShortCode,IsUse,DispOrder,SourceCodeUrl,SourceCode,Memo&page=1&limit=9999";       
            url+="&where=(GridID="+Id+")";
        }
        else if (type == 'FormCode'){
            url = me.config.getFormListUrl + "?fields=LabID,LabNo,FormID,FormCode,TypeID,TypeName,ClassID,ClassName,CName,ShortName,ShortCode,IsUse,DispOrder,SourceCodeUrl,SourceCode,Memo&page=1&limit=9999";
            url+="&where=(FormID="+Id+")";
        }
        var loadIndex = layer.load();
        uxutil.server.ajax({
            url: url + "&Id=" + Id
        }, function (res) {
                layer.close(loadIndex);
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = {
                        addListModalFormLabID: res["value"].list[0]["LabID"],
                        addListModalFormListType: type,
                        addListModalFormCode: res["value"].list[0][type],
                        addListModalFormCName: res["value"].list[0]["CName"],
                        addListModalFormShortCode: res["value"].list[0]["ShortCode"],
                        addListModalFormShortName: res["value"].list[0]["ShortName"],
                        addListModalFormTypeID: res["value"].list[0]["TypeID"],
                        addListModalFormClassID: res["value"].list[0]["ClassID"],
                        addListModalFormDispOrder: res["value"].list[0]["DispOrder"],
                        addListModalFormIsUse: res["value"].list[0]["IsUse"],
                        SourceCodeUrl: res["value"].list[0]["SourceCodeUrl"],
                        SourceCode: res["value"].list[0]["SourceCode"]
                    };
                    if (typeof (callBack) == 'function') callBack(list);
                }
            } else {
                layer.msg("获得集合失败！");
            }
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
            area: ['650px', '290px'],
            content: $('#addModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["ModuleID"] = config.ModuleID;
                entity["ModuleFormGridLinkID"] = $("#Id").val() ? $("#Id").val() : 0;
                entity["LabID"] = $("#LabID").val() ? $("#LabID").val() : 0;
                entity["LabNo"] = $("#LabID").val() ? $("#LabID option:selected").attr("data-labno") : "";
                entity["FormID"] = $("#FormID").val() ? $("#FormID").val() : null;
                entity["FormCode"] = $("#FormID").val() ? $("#FormID option:selected").attr("data-code") : "";
                entity["GridID"] = $("#GridID").val() ? $("#GridID").val() : null;
                entity["GridCode"] = $("#GridID").val() ? $("#GridID option:selected").attr("data-code") : "";
                entity["ChartID"] = $("#ChartID").val() ? $("#ChartID").val() : null;
                entity["ChartCode"] = $("#ChartID").val() ? $("#ChartID option:selected").attr("data-code") : "";
                if ($("#FormID").val()) {
                    entity["CName"] = $("#FormID option:selected").text();
                    entity["ShortCode"] = $("#FormID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#FormID option:selected").attr("data-shortname");
                } else if ($("#GridID").val()) {
                    entity["CName"] = $("#GridID option:selected").text();
                    entity["ShortCode"] = $("#GridID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#GridID option:selected").attr("data-shortname");
                } else if ($("#ChartID").val()) {
                    entity["CName"] = $("#ChartID option:selected").text();
                    entity["ShortCode"] = $("#ChartID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#ChartID option:selected").attr("data-shortname");
                } else {
                    entity["CName"] = "";
                    entity["ShortCode"] = "";
                    entity["ShortName"] = "";
                }
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#DispOrder").val() && !isNaN($("#DispOrder").val())) ? $("#DispOrder").val() : 0;
                me.onSaveClick({ entity: entity }, 'add', index);
            },
            end: function () {
                me.clear();
            }
        });
    };
    //修改
    Class.pt.editClick = function (data) {
        var me = this;
        var list = JSON.parse(JSON.stringify(data));
        list.IsUse = String(list.IsUse) == "true" ? true : false;
		list.Id = list.ModuleFormGridLinkID;
        form.val('addModalForm', list);
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['修改配置项', 'font-weight:bold;'],
            skin: 'layui-layer-lan',
            area: ['650px', '290px'],
            content: $('#addModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["ModuleID"] = config.ModuleID;
                entity["ModuleFormGridLinkID"] = $("#Id").val() ? $("#Id").val() : 0;
                entity["LabID"] = $("#LabID").val() ? $("#LabID").val() : 0;
                entity["LabNo"] = $("#LabID").val() ? $("#LabID option:selected").attr("data-labno") : "";
                entity["FormID"] = $("#FormID").val() ? $("#FormID").val() : null;
                entity["FormCode"] = $("#FormID").val() ? $("#FormID option:selected").attr("data-code") : "";
                entity["GridID"] = $("#GridID").val() ? $("#GridID").val() : null;
                entity["GridCode"] = $("#GridID").val() ? $("#GridID option:selected").attr("data-code") : "";
                entity["ChartID"] = $("#ChartID").val() ? $("#ChartID").val() : null;
                entity["ChartCode"] = $("#ChartID").val() ? $("#ChartID option:selected").attr("data-code") : "";
                if ($("#FormID").val()) {
                    entity["CName"] = $("#FormID option:selected").text();
                    entity["ShortCode"] = $("#FormID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#FormID option:selected").attr("data-shortname");
                } else if ($("#GridID").val()) {
                    entity["CName"] = $("#GridID option:selected").text();
                    entity["ShortCode"] = $("#GridID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#GridID option:selected").attr("data-shortname");
                } else if ($("#ChartID").val()) {
                    entity["CName"] = $("#ChartID option:selected").text();
                    entity["ShortCode"] = $("#ChartID option:selected").attr("data-shortcode");
                    entity["ShortName"] = $("#ChartID option:selected").attr("data-shortname");
                } else {
                    entity["CName"] = "";
                    entity["ShortCode"] = "";
                    entity["ShortName"] = "";
                }
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#DispOrder").val() && !isNaN($("#DispOrder").val())) ? $("#DispOrder").val() : 0;
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
    }
    //删除
    Class.pt.delClick = function (checkData) {
        var me = this;
        var len = checkData.length,
            successCount = 0,
            failCount = 0;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var indexs = layer.load();//显示遮罩层
            $.each(checkData, function (i, item) {
                var url = me.config.delUrl + '?id=' + item.ModuleFormGridLinkID;
                uxutil.server.ajax({
                    type: "POST",
                    url: url,
                    data: JSON.stringify({ModuleFormGridLinkID:[item.ModuleFormGridLinkID]}),
                }, function (data) {
                    if (data.success === true) {
                        successCount++;
                    } else {
                        failCount++;
                    }
                    if (successCount + failCount == len) {
                        table.reload(me.config.id, {});
                        layer.close(indexs);
                        layer.close(index);
                        layer.msg("删除完毕！成功：" + successCount + "个，失败：" + failCount + "个", { time: 3000 });
                    }
                });
            });
        });
    };
    //表单保存处理
    Class.pt.onSaveClick = function (data, type, index) {
        var me = this,
            url = type == 'add' ? me.config.addUrl : me.config.editUrl,
            params = data;
        if (!url) return;
        if (!params.entity.ModuleID) return;
        if (type == "add") delete params.entity.ModuleFormGridLinkID;
        if ((params.entity.FormID && params.entity.GridID) || (params.entity.FormID && params.entity.ChartID) || (params.entity.ChartID && params.entity.GridID)) {
            layer.msg("表单集合、表格集合、图表集合不能同时存在!");
            return;
        }
        if (!params.entity.FormID && !params.entity.GridID && !params.entity.ChartID) {
            layer.msg("必须存在一种配置项!");
            return;
        }
        if(params.entity.FormID && params.entity.FormID!=-1){
            
            params.entity.GridID=-1;
            params.entity.ChartID=-1;
        }
        if(params.entity.GridID && params.entity.GridID!=-1){
            
            params.entity.FormID=-1;
            params.entity.ChartID=-1;
        }
        
        
        params = JSON.stringify({ModuleFormGridLink:[params.entity]});
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
                table.reload(me.config.id, {});
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
        $("#addModalForm :input").each(function (i, item) {
            $(this).val("");
        });
        $("#addListModalForm :input").each(function (i, item) {
            if ($(this).attr("id") == "addListModalFormListType")
                $(this).val("FormCode");
            else
                $(this).val("");
        });
        form.render();
    };
    //设置配置项
    Class.pt.setConfigItem = function (data) {
        var me = this,
            url = "",
            options = {};
        if (me.config.role == 'system') {
            if (data["FormCode"]) {
                url = uxutil.path.ROOT + '/ui_new/layui/class/setting/public/formItem/main/app.html?isSaveSuccessLoadTable=true&isSearch=false';
                options = { FormID: data.FormID, FormCode: data.FormCode, CName: data.CName };
            } else if (data["GridCode"]) {
                url = uxutil.path.ROOT + '/ui_new/layui/class/setting/public/tableColumn/main/app.html?isSaveSuccessLoadTable=true&isSearch=false';
                options = { GridID: data.GridID, GridCode: data.GridCode, CName: data.CName };
            } else if (data["ChartCode"]) {
                layer.msg("暂不支持图表设置配置项!");
                return;
            }
        } else {
            if (data["FormCode"]) {
                url = uxutil.path.ROOT + '/ui_new/layui/class/setting/public/formItem_client/main/app.html?isSearch=false';
                options = { FormCode: data.FormCode };
            } else if (data["GridCode"]) {
                url = uxutil.path.ROOT + '/ui_new/layui/class/setting/public/tableColumn_client/main/app.html?isSearch=false';
                options = { GridCode: data.GridCode };
            } else if (data["ChartCode"]) {
                layer.msg("暂不支持图表设置配置项!");
                return;
            }
        }
        layer.open({
            type: 2,
            title: ['设置配置项', 'font-weight:bold;'],
            skin: 'layui-layer-lan',
            area: ['1000px', '600px'],
            content: url,
            success: function (layero, index) {
                var indexs = layer.load();
                var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                $(body).css("padding", "0 5px");
                setTimeout(function () {
                    var iframeWin = window[layero.find('iframe')[0]['name']];
                    iframeWin.layui.mainTable.onSearch('mainTable', options);
                    layer.close(indexs);
                },500);
            }
        });
    };
    //暴露接口
    exports('mainTable', mainTable);
});
