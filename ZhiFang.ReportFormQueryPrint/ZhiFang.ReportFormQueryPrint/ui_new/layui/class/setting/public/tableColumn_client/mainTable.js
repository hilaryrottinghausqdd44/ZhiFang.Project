/**
	@name：页面配置表格列--客户
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

    var config = { loadIndex: null, ModuleID:'' };

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
                "direction": 'DESC'
            }],
            //selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridListByGridCode?GridCode=dayQualityControlStatisticsGrid',
            selectUrl: uxutil.path.ROOT + '/ServiceWCF/DictionaryService.svc/GetBModuleGridListByModuleID',
            where: "",
            toolbar: false,
            defaultToolbar: [],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号', fixed: 'left' },
                { field: 'GridID', width: 80, title: '主键ID', sort: false, hide: true },
                { field: 'LabID', width: 80, title: '实验室ID', sort: false, hide: true },
                { field: 'LabNo', title: '实验室No', width: 120, sort: true, hide: true },
                { field: 'GridCode', title: '表单代码', width: 100, sort: true },
                { field: 'CName', title: '名称', width: 140, sort: true },
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
                { field: 'TypeID', title: '类型Id', width: 120, sort: true, hide: true },
                { field: 'TypeName', title: '类型名称', width: 120, sort: true, hide: true },
                { field: 'ClassID', title: '样式类型Id', width: 120, sort: true, hide: true },
                { field: 'ClassName', title: '样式类型名称', width: 120, sort: true, hide: true },
                { field: 'DispOrder', title: '显示次序', width: 120, sort: true, hide: true }
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
                
                if (config.loadIndex) layer.close(config.loadIndex);
                if (!res) return;
                var data = res.ResultDataValue ? eval("(" + res.ResultDataValue + ")") : {}; 
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
        if (config.ModuleID) {
            url += '&ModuleID=' + config.ModuleID
        } 
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
    mainTable.render = function (options, ModuleID) {
        var me = this;
        config.ModuleID = ModuleID;
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
    //暴露接口
    exports('mainTable', mainTable);
});
