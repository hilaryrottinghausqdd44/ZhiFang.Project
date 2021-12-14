/**
	@name：页面配置表单项
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

    var config = { FormID: null, FormCode: null, CName: null, isHideColumnToolbar: false, isSaveSuccessLoadTable:false ,
    	systemHost: ''
    };

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
                "property": 'BModuleFormControlList_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlListByHQL?isPlanish=true',
            addUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_AddBModuleFormControlList',
            editUrl:'/ServerWCF/ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormControlListByField',
            delUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_DelBModuleFormControlList',
            where: "",
            toolbar: '#Toolbar',
            defaultToolbar: ['filter'],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'checkbox' },
                { type: 'numbers', title: '行号'},
                { field: 'BModuleFormControlList_Id', width: 80, title: '主键ID', sort: false, hide: true },
                { field: 'BModuleFormControlList_LabID', width: 80, title: '实验室ID', sort: false, hide: true },
                { field: 'BModuleFormControlList_LabNo', title: '实验室No', width: 120, sort: true, hide: true },
                { field: 'BModuleFormControlList_FormID', title: '表单集合Id', width: 120, sort: true, hide:true },
                { field: 'BModuleFormControlList_FormCode', title: '表单代码', width: 120, sort: true },
                { field: 'BModuleFormControlList_CName', title: '名称', width: 120, sort: true },
                { field: 'BModuleFormControlList_TypeID', title: '控件类型Id', width: 120, sort: true, hide: true},
                { field: 'BModuleFormControlList_TypeName', title: '控件类型', width: 120, sort: true },
                { field: 'BModuleFormControlList_Cols', title: '所占栅格', width: 100, sort: true },
                { field: 'BModuleFormControlList_MapField', title: '对应属性', width: 120, sort: true },
                { field: 'BModuleFormControlList_Label', title: '显示名称', width: 120, sort: true },
                { field: 'BModuleFormControlList_DefaultValue', title: '默认值', width: 120, sort: true },
                { field: 'BModuleFormControlList_ValueField', title: '值属性', width: 120, sort: true },
                { field: 'BModuleFormControlList_TextField', title: '显示属性', width: 120, sort: true },
                { field: 'BModuleFormControlList_URL', title: '数据服务地址', width: 120, sort: true },
                { field: 'BModuleFormControlList_CallBackFunc', title: '回调函数', width: 120, sort: true },
                { field: 'BModuleFormControlList_ClassName', title: '控件Class名', width: 120, sort: true },
                { field: 'BModuleFormControlList_StyleContent', title: '控件样式', width: 120, sort: true },
                { field: 'BModuleFormControlList_DataJSON', title: '数据集', width: 120, sort: false },
                {
                    field: 'BModuleFormControlList_IsUse', title: '是否使用', width: 120, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.BModuleFormControlList_IsUse) == "true") 
                            str = "<span style='color:green;'>是</span>";
                        else 
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                {
                    field: 'BModuleFormControlList_IsHasNull', title: '是否存在空值', width: 120, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.BModuleFormControlList_IsHasNull) == "true")
                            str = "<span style='color:green;'>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                 {
                    field: 'BModuleFormControlList_IsReadOnly', title: '是否只读', width: 120, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.BModuleFormControlList_IsReadOnly) == "true")
                            str = "<span style='color:green;'>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                }, {
                    field: 'BModuleFormControlList_IsDisplay', title: '是否显示', width: 120, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.BModuleFormControlList_IsDisplay) == "true")
                            str = "<span style='color:green;'>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                { field: 'BModuleFormControlList_DispOrder', title: '显示次序', width: 120, sort: true }
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
                //无数据处理
                if (count == 0) return;
                 //默认选择第一行
                var rowIndex = 0;
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].BModuleFormControlList_Id == mainTable.config.PK) {
                        rowIndex = res.data[i].LAY_TABLE_INDEX;
                        break;
                    }
                }
                // 当新增的数据过多出现滚动条时，再次新增的数据在最后，可以定位到该数据
                if ($("#mainTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + rowIndex + ")")[0]) {
                    setTimeout(function () {
//                      $("#mainTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + rowIndex + ")")[0].click();
                        document.querySelector("#mainTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + (rowIndex+1) + ")").scrollIntoView(false, { behavior: "smooth" });
                    }, 0);

                }
                doAutoSelect(mainTable.config, rowIndex);
                //触发点击事件
                // $("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

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
        var url = (config.systemHost || uxutil.path.ROOT) + me.config.selectUrl;
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
        if (config.FormCode) arr.push("FormCode='" + config.FormCode + "'");
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
                if (!config.FormCode) {
                    layer.msg("请先选择表单集合!");
                    return;
                }
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
            title: ['新增配置项'],
            skin: 'IEQA-class',
            area: ['90%', '90%'],
            content: $('#modal'),
            yes: function (index, layero) {
                var formData = $("#modalForm").serialize().split("&"),
                    entity = {};
                $.each(formData, function (i, item) {
                    entity[item.split("=")[0].split("_")[1]] = decodeURIComponent(item.split("=")[1]);
                });
                entity["Id"] = config.FormID ? config.FormID : 0;
                entity["FormCode"] = config.FormCode ? config.FormCode : "";
                entity["CName"] = config.CName ? config.CName : "";
                entity["DataJSON"] = $("#DataJSON").val();
                entity["CallBackFunc"] = $("#CallBackFunc").val();
                entity["LabID"] = uxutil.cookie.get(uxutil.cookie.map.LABID);
                entity["TypeName"] = $("#TypeID option:selected").text();
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["IsHasNull"] = $("#IsHasNull").prop("checked") ? 1 : 0;
                entity["IsReadOnly"] = $("#IsReadOnly").prop("checked") ? 1 : 0;
                entity["IsDisplay"] = $("#IsDisplay").prop("checked") ? 1 : 0;
                entity["DispOrder"] = (entity["DispOrder"] && !isNaN(entity["DispOrder"])) ? entity["DispOrder"] : 0;
                entity["Cols"] = (entity["Cols"] && !isNaN(entity["Cols"])) ? entity["Cols"] : 12;
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
        list.BModuleFormControlList_IsUse = String(list.BModuleFormControlList_IsUse) == "true" ? true : false;
        list.BModuleFormControlList_IsHasNull = String(list.BModuleFormControlList_IsHasNull) == "true" ? true : false;
        list.BModuleFormControlList_IsReadOnly = String(list.BModuleFormControlList_IsReadOnly) == "true" ? true : false;
        list.BModuleFormControlList_IsDisplay = String(list.BModuleFormControlList_IsDisplay) == "true" ? true : false;
        form.val('modalForm', list);
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['编辑配置项'],
            skin: 'IEQA-class',
            area: ['90%', '90%'],
            content: $('#modal'),
            yes: function (index, layero) {
                window.event.preventDefault();
                var formData = $("#modalForm").serialize().split("&"),
                    entity = {};
                $.each(formData, function (i, item) {
                    entity[item.split("=")[0].split("_")[1]] = decodeURIComponent(item.split("=")[1]);
                });
                // 和新增相比少了3个字段
                entity["Id"] = $("#FormControlID").val();
//              entity["FormCode"] =config.FormCode ? config.FormCode : "";
                entity["CName"] = config.CName ? config.CName : "";
                
                entity["DataJSON"] = $("#DataJSON").val();
                entity["CallBackFunc"] = $("#CallBackFunc").val();
                entity["LabID"] = uxutil.cookie.get(uxutil.cookie.map.LABID);
                entity["TypeName"] = $("#TypeID option:selected").text();
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["IsHasNull"] = $("#IsHasNull").prop("checked") ? 1 : 0;
                entity["IsReadOnly"] = $("#IsReadOnly").prop("checked") ? 1 : 0;
                entity["IsDisplay"] = $("#IsDisplay").prop("checked") ? 1 : 0;
                entity["DispOrder"] = (entity["DispOrder"] && !isNaN(entity["DispOrder"])) ? entity["DispOrder"] : 0;
                entity["Cols"] = (entity["Cols"] && !isNaN(entity["Cols"])) ? entity["Cols"] : 12;
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
            id = data.BModuleFormControlList_Id;
        if (!id) return;
        var url = config.systemHost + me.config.delUrl + '?id=' + id;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            //显示遮罩层
            var indexs = layer.load();
            uxutil.server.ajax({
                url: url
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
            url = type == 'add' ? config.systemHost + me.config.addUrl : config.systemHost + me.config.editUrl,
            params = data;
        if (!url) return;
//      if (!params.entity.TypeID) return; // 控件暂时没有
        if (type == 'add' && !params.entity.FormCode) return;
        if (type == "add") {
        	delete params.entity.Id;
        }else {
        	var editId = params.entity.Id;
        }
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
                Class.pt.clear();
                if (config.isSaveSuccessLoadTable){
                	if(type == 'add') {
                		mainTable.config.PK = data.value.id;
                	}else {
                		mainTable.config.PK = editId;
                	}
                	table.reload(me.config.id, {});
                }
                else{
                	layui.event("leftTableSarch", "search", {});
                }
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
            if ($(this).attr("name") != "BModuleFormControlList_TypeID") $(this).val("");
        });
        form.render();
    };
    /***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
    var doAutoSelect = function (curTable, rowIndex) {
        curTable.key = curTable.id;
        var data = table.cache[curTable.key] || [];
        if (!data || data.length <= 0) return;
        rowIndex = rowIndex || 0;
        var tableDiv = $(curTable.elem + "+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
        var filter = $(curTable.elem).find('lay-filter');
        var obj = {
            tr: thatrow,
            data: data[rowIndex] || {},
            del: function () {
                table.cache[curTable.key][index] = [];
                tr.remove();
                curTable.scrollPatch();
            },
            updte: {}
        };
        layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
        thatrow.click();
    };
    //暴露接口
    exports('mainTable', mainTable);
});
