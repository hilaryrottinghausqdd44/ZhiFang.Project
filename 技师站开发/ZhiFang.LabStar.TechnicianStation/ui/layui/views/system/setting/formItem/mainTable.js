/**
	@name：页面配置表单项 -- 客户
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

    var config = { loadIndex: null };
     
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
                "property": 'BModuleFormList_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleFormListByHQL?isPlanish=true',
            addUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_AddBModuleFormList',
            editUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormListByField',
            delUrl: uxutil.path.ROOT + '/ServerWCF/ModuleConfigService.svc/ST_UDTO_DelBModuleFormList',
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
                { type: 'numbers', title: '行号', fixed: 'left' },
                { field: 'BModuleFormList_Id', width: 80, title: '主键ID', sort: false, hide: true },
                { field: 'BModuleFormList_LabID', width: 80, title: '实验室ID', sort: false, hide: true },
                { field: 'BModuleFormList_LabNo', title: '实验室No', width: 120, sort: true, hide: true },
                { field: 'BModuleFormList_FormCode', title: '表单代码', width: 120, sort: true },
                { field: 'BModuleFormList_CName', title: '名称', width: 120, sort: true },
                {
                    field: 'BModuleFormList_IsUse', title: '是否使用', width: 100, sort: true,
                    templet: function (data) {
                        var str = "";
                        if (String(data.BModuleFormList_IsUse) == "true")
                            str = "<span style='color:green;'>是</span>";
                        else
                            str = "<span style='color:red;'>否</span>";
                        return str;
                    }
                },
                { field: 'BModuleFormList_TypeID', title: '类型Id', width: 120, sort: true, hide: true },
                { field: 'BModuleFormList_TypeName', title: '类型名称', width: 120, sort: true, hide: true },
                { field: 'BModuleFormList_ClassID', title: '样式类型Id', width: 120, sort: true, hide: true },
                { field: 'BModuleFormList_ClassName', title: '样式类型名称', width: 120, sort: true, hide: true },
                { field: 'BModuleFormList_SourceCodeUrl', title: '源代码地址', width: 140, sort: true, hide: false },
                { field: 'BModuleFormList_SourceCode', title: '源代码', width: 200, sort: true, hide: false },
                { field: 'BModuleFormList_DispOrder', title: '显示次序', width: 120, sort: true, hide: true },
                { title: '操作', width: 140, align: 'center', toolbar: '#tableOperation' }
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
            parseData: function (res) {//res即为原始返回的数据\n
                if (config.loadIndex) layer.close(config.loadIndex);
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                //无数据处理
                if (count == 0) {
                    layui.event("noData", "noData", {});
                    return;
                };
                 //默认选择第一行
                var rowIndex = 0;
                for (var i = 0; i < res.data.length; i++) {
                    if (res.data[i].BModuleFormList_Id == mainTable.config.PK) {
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
        //if (config.FormCode) arr.push(encodeURI("FormCode like '%" + config.FormCode + "%'"));
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
    mainTable.render = function (options) {
        var me = this;
        var inst = new Class(options);
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    mainTable.onSearch = function (mytable) {
        var me = this;
        var inst = new Class(me);
        mainTable.url = inst.getLoadUrl();
        mainTable.config.elem = "#" + mytable;
        mainTable.config.id = mytable;
        config.loadIndex = layer.load();
        table.reload(mytable, {
            url: inst.getLoadUrl()
        });
    };
    Class.pt.iniListeners = function () {
        var me = this;
        //监听表格头工具栏
        table.on('toolbar(mainTable)', function (obj) {
   			// 头工具栏现在只保留了新增（删除和修改放到了行工具栏里面）
            var layEvent = obj.event,
                checkData = table.checkStatus(me.config.id).data;
            if (layEvent == 'add') {//新增
                me.addClick();
            } else if (layEvent === 'edit') { //修改
                if (checkData && checkData.length != 1) {
                    layer.msg("请勾选一条数据进行修改！");
                    return;
                }
                me.editClick(checkData[0]);
            } else if (layEvent === 'del') { //删除
                if (checkData && checkData.length == 0) {
                    layer.msg("请勾选需要删除的数据！");
                    return;
                }
                me.delClick(checkData);
            }
        });
        //监听表格行工具事件
        table.on('tool(mainTable)', function (obj) {
            var data = obj.data, //获得当前行数据
                layEvent = obj.event;
            if (layEvent === 'del') { //删除
                var arr = [];
                arr.push(data);
                me.delClick(arr);
            } else if (layEvent === 'edit') { //编辑
                me.editClick(data);
            }
        });
        //监听行单击事件
        table.on('row(mainTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            layui.event('rightTableOnSearch', 'search', obj.data);
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
            title: ['新增表单集合'],
            skin: 'IEQA-class',
            area: ['60%', '65%'],
            content: $('#addModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["LabID"] = uxutil.cookie.get(uxutil.cookie.map.LABID);
                entity["FormCode"] = $("#FormCode").val();
                entity["CName"] = $("#CName").val();
                entity["TypeID"] = $("#TypeID").val() ? $("#TypeID").val() : 0;
                entity["TypeName"] = $("#TypeID").val() ? $("#TypeID option:selected").text() : "";
                entity["ClassID"] = $("#ClassID").val() ? $("#ClassID").val() : 0;
                entity["ClassName"] = $("#ClassID").val() ? $("#ClassID option:selected").text() : "";
                entity["SourceCodeUrl"] = $("#SourceCodeUrl").val();
                entity["SourceCode"] = $("#SourceCode").val();
                entity["IsUse"] = $("#IsUse").prop("checked") ? 1 : 0;
                entity["DispOrder"] = ($("#DispOrder").val() && !isNaN($("#DispOrder").val())) ? $("#DispOrder").val() : 0;
                me.onSaveClick({ entity: entity }, 'add', index);
            },
            cancel: function (index, layero) {
                me.clear();
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
        list.BModuleFormList_IsUse = String(list.BModuleFormList_IsUse) == "true" ? true : false;
        form.val('addModalForm', list);
        layer.open({
            type: 1,
            btn: ['保存'],
            title: ['编辑表单集合'],
            skin: 'IEQA-class',
            area: ['60%', '65%'],
            content: $('#addModal'),
            yes: function (index, layero) {
                var entity = {};
                entity["Id"] = $("#FormID").val() ? $("#FormID").val() : 0;
                entity["LabID"] = uxutil.cookie.get(uxutil.cookie.map.LABID);
                entity["FormCode"] = $("#FormCode").val();
                entity["CName"] = $("#CName").val();
                entity["TypeID"] = $("#TypeID").val() ? $("#TypeID").val() : 0;
                entity["TypeName"] = $("#TypeID").val() ? $("#TypeID option:selected").text() : "";
                entity["ClassID"] = $("#ClassID").val() ? $("#ClassID").val() : 0;
                entity["ClassName"] = $("#ClassID").val() ? $("#ClassID option:selected").text() : "";
                entity["SourceCodeUrl"] = $("#SourceCodeUrl").val();
                entity["SourceCode"] = $("#SourceCode").val();
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
            cancel: function (index, layero) {
                me.clear();
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
                var url = me.config.delUrl + '?id=' + item.BModuleFormList_Id;
                uxutil.server.ajax({
                    url: url
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
        if (type == "add") {
        	delete delete params.entity.Id;
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
                // 新增,修改列表增加定位
                if(type == 'add'){
                	 mainTable.config.PK = data.value.id;
                }else {
                	mainTable.config.PK = editId;
                }
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
        form.render();
    }
    
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
