/**
	@name：页面配置列表项--客户
	@author：wangyz
	@version 2021-05-29
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form;

    var config = { GridCode: '', BModuleGridControlListIds: [], loadIndex: null };

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
            selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridControlSetByGridCode?isPlanish=true',
            addUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/AddBModuleGridControlSet',
            editUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/UpdateBModuleGridControlSet',
            delUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/deleteBModuleGridControlSet',
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
                { type: 'numbers', title: '行号' },
                { field: 'GridControSetlID', width: 80, title: '主键ID', sort: false, hide: false },
                { field: 'GridControlID', width: 80, title: 'B_Module_GridControlList表主键ID', sort: false, hide: true },
                { field: 'LabID', width: 80, title: '实验室ID', sort: false, hide: true },
                { field: 'LabNo', title: '实验室No', width: 120, sort: true, hide: true },
                { field: 'GridCode', title: '列表代码', width: 120, sort: true, hide: true },
                { field: 'CName', title: '名称', width: 160, sort: true, hide: true },
                { field: 'ColName', title: '列头名称', width: 120, sort: true, edit: 'text' },
                { field: 'Width', title: '宽度', width: 120, sort: true, edit: 'text' },
                { field: 'DispOrder', title: '显示次序', width: 120, sort: true, edit: 'text' },
                { field: 'IsOrder', title: '是否排序', width: 120, sort: true, templet: '#IsOrderTpl' },
                { field: 'IsHide', title: '是否隐藏', width: 120, sort: true, templet: '#IsHideTpl' },
                { field: 'IsUse', title: '是否使用', width: 120, sort: true, templet: '#IsUseTpl' },
                { field: 'Tab', width: 100, title: '用于判断行是否有修改数据', hide: true, sort: false }
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
                var list = [];
                config.BModuleGridControlListIds = [];
                if (data && data.list) list = data.list;
                $.each(list, function (i, item) {
                    config.BModuleGridControlListIds.push(item.GridControlID);
                });
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                if (config.loadIndex) layer.close(config.loadIndex);
                //无数据处理
                if (count == 0) return;
                //设置表格中复选框样式
                $("select[name='IsUse']").parent('div.layui-table-cell').css({ 'overflow': 'visible', 'padding': '0 15px' });
                $("select[name='IsOrder']").parent('div.layui-table-cell').css({ 'overflow': 'visible', 'padding': '0 15px' });
                $("select[name='IsHide']").parent('div.layui-table-cell').css({ 'overflow': 'visible', 'padding': '0 15px' });
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
		arr.push("QFuncID is null");
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
    mainTable.onSearch = function (mytable, options) {
        var me = this;
        if (options) config = $.extend({}, config, options);
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
            var layEvent = obj.event;
            if (layEvent == 'add') {//新增
                if (config.GridCode) me.addClick();
            } else if (layEvent === 'del') { //删除
                me.delClick();
            } else if (layEvent === 'edit') { //编辑
                me.editClick();
            }
        });
        //是否使用
        form.on('checkbox(IsUse)', function (data) {
            debugger;
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].Tab;
            if (rowObj) delete rowObj.IsUse;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (index == dataindex) {
                    value.Tab = true;
                    value.IsUse = data.elem.checked;
                }
            });
        });
        //是否排序
        form.on('checkbox(IsOrder)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].Tab;
            if (rowObj) delete rowObj.IsOrder;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (index == dataindex) {
                    value.Tab = true;
                    value.IsOrder = data.elem.checked;
                }
            });
        });
        //是否隐藏
        form.on('checkbox(IsHide)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr');
            var dataindex = elem.attr("data-index");
            var tableCache = table.cache[me.config.id];
            //改变后的数据
            var rowObj = tableCache[dataindex].Tab;
            if (rowObj) delete rowObj.IsHide;
            if (!rowObj) rowObj = {};
            $.each(tableCache, function (index, value) {
                if (index == dataindex) {
                    value.Tab = true;
                    value.IsHide = data.elem.checked;
                }
            });
        });
        //监听单元格编辑
        table.on('edit(' + me.config.id + ')', function (obj) {
            var value = obj.value, //得到修改后的值
                data = obj.data,//得到所在行所有键值
                field = obj.field; //得到字段
            var tableCache = table.cache[me.config.id];
            var dataindex = obj.tr[0].dataset.index;
            tableCache[dataindex].Tab = true;
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
        var me = this,
            where = "IsUse <> 0 and GridCode in ('" + config.GridCode + "')";
        if (config.BModuleGridControlListIds.length > 0) where += " and GridControlID not in (" + config.BModuleGridControlListIds.join(",") + ")";
        layer.open({
            type: 2,
            btn: ['追加选中项'],
            title: ['追加配置项', 'font-weight:bold;'],
            skin: 'layui-layer-lan',
            area: ['90%', '90%'],
            content: uxutil.path.ROOT + '/ui_new/layui/class/setting/public/tableColumn/main/app.html?toolbar=false&defaultToolbar=&externalWhere=' + where + '&isHideColumnToolbar=true&isInitLaboratory=false&isInitComponentType=false&isSearch=true',
            yes: function (index, layero) {
                var iframeWin = window[layero.find('iframe')[0]['name']],
                    checkData = iframeWin.layui.table.checkStatus('mainTable').data,
                    len = checkData.length,
                    successCount = 0,
                    failCount = 0;
                if (len == 0) {
                    layer.msg("请选择需要追加的配置项！");
                    return;
                }
                var loadIndex = layer.load();
                $.each(checkData, function (i, item) {
                    var params = [
                         {
                            GridControlID: item.GridControlID,
                            LabID: item.LabID,
                            LabNo: item.LabNo,
                            GridCode: item.GridCode,
                            CName: item.CName,
                            ColName: item.ColName,
                            Width: item.Width,
                            DispOrder: item.DispOrder,
                            IsUse: item.IsUse,
                            IsOrder: item.IsOrder,
                            IsHide: item.IsHide
                        }
                    ];
                    var configs = {
                        type: "POST",
                        url: me.config.addUrl,
                        data: JSON.stringify({ columnsTemplate: params })
                    };
                    uxutil.server.ajax(configs, function (data) {
                        if (data.success) {
                            successCount++;
                        } else {
                            failCount++;
                        }
                        if (successCount + failCount == len) {
                            table.reload(me.config.id, {});
                            layer.msg("追加完毕！成功：" + successCount + "个，失败：" + failCount + "个", { time: 3000 });
                            layer.close(index);
                            layer.close(loadIndex);
                        }
                    });
                });
            },
            success: function (layero, index) {
                var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                $(body).css("padding", "0 5px");

            }
        });
    };
    //编辑
    Class.pt.editClick = function () {
        var me = this,
            tableCache = table.cache[me.config.id],
            arr = [];
        if (!tableCache || tableCache.length == 0) {
            layer.msg("没有修改数据不需要保存！");
            return;
        }
        $.each(tableCache, function (i, item) {
            if (item.Tab) arr.push(item);
        });
        var len = arr.length,
            successCount = 0,
            failCount = 0;
        if (len == 0) {
            layer.msg("没有修改数据不需要保存！");
            return;
        }
        var loadIndex = layer.load();
        $.each(arr, function (i, item) {
            debugger;
            var params = [
                 {
                GridControSetlID: item.GridControSetlID,
                    ColName: item.ColName,
                        Width: item.Width,
                            DispOrder: item.DispOrder,
                                IsUse: item.IsUse,
                                    IsOrder: item.IsOrder,
                                        IsHide: item.IsHide
                }
                //, fields: 'GridControSetlID,ColName,Width,DispOrder,IsUse,IsOrder,IsHide'
            ];
            var configs = {
                type: "POST",
                url: me.config.editUrl,
                data: JSON.stringify({ bModuleGridControlSetList:params })
            };
            uxutil.server.ajax(configs, function (data) {
                if (data.success) {
                    successCount++;
                } else {
                    failCount++;
                }
                if (successCount + failCount == len) {
                    table.reload(me.config.id, {});
                    layer.msg("保存完毕！成功：" + successCount + "个，失败：" + failCount + "个", { time: 3000 });
                    layer.close(loadIndex);
                }
            });
        });
    };
    //删除
    Class.pt.delClick = function () {
        var me = this,
            checkData = table.checkStatus(me.config.id).data;
        if (checkData && checkData.length == 0) {
            layer.msg("请选择需要删除的数据！");
            return;
        }
        var len = checkData.length,
            successCount = 0,
            failCount = 0;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var indexs = layer.load();//显示遮罩层
            $.each(checkData, function (i, item) {
                var url = me.config.delUrl + '?GridControSetlID=' + item.GridControSetlID;
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
                        layer.msg("追加完毕！成功：" + successCount + "个，失败：" + failCount + "个", { time: 3000 });
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
        if (!params.entity.TypeID) return;
        if (type == "add") delete params.entity.GridControSetlID;
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
            if (index) layer.close(index);
            if (data.success) {
                layer.msg("保存成功！", { icon: 6, anim: 0 });
                me.onSearchClick();
                Class.pt.clear();
            } else {
                var msg = type == 'add' ? '新增失败！' : '修改失败！';
                if (!data.msg) data.msg = msg;
                layer.msg(data.msg, { icon: 5, anim: 6 });
            }
        });
    };
    //暴露接口
    exports('mainTable', mainTable);
});
