/**
	@name：查询结果列表
	@author：zhangda
	@version 2019-10-28
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        uxtable = layui.uxtable;

    var QuerySQLTable = {
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
            //sort: [{
            //    "property": 'LBQCRule_DispOrder',
            //    "direction": 'ASC'
            //}],
            //selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarQCService.svc/QC_UDTO_SearchLBQCRuleByHQL?isPlanish=true',
            where: "",
            toolbar: '',
            page: true,
            limit: 200,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号', fixed: 'left' }
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
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
                    return;
                }
                //触发点击事件
                $("#QuerySQLTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

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
        me.config = $.extend({}, QuerySQLTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, QuerySQLTable, Class.pt, me);// table,
        return me;
    };
    Class.pt = Class.prototype;
    //获得动态的列名
    Class.pt.getCols = function (data) {
        var me = this;
        var cols = [[{ type: 'numbers', title: '行号', fixed: 'left' }]];
        if (data.count > 0) {
            for (var i in data.list[0]) {
                cols[0].push({ field: i, width: 150, title: i });
            }
        } else {
            var list = data.fields.split(",");
            for (var i in list) {
                cols[0].push({ field: list[i], width: 150, title: list[i] });
            }
        }
        return cols;
    }
    //核心入口
    QuerySQLTable.render = function (options) {
        var me = this;
        var inst = new Class(options);
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    QuerySQLTable.onSearch = function (mytable,data) {
        var me = this;
        var list = [];
        if (data.count > 0) list = data.list;
        var inst = new Class(me);
        QuerySQLTable.elem = "#" + mytable;
        QuerySQLTable.id = mytable;
        table.reload(mytable, {
            cols: inst.getCols(data),
            data: list
        });
    };
    //判断浏览器大小方法
    Class.pt.screen = function ($) {
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
    Class.pt.iniListeners = function () {
        var me = this;
        //监听行单击事件
        table.on('row(QuerySQLTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            layui.event("QuerySQLTableClick", "click", { id: obj.data.LBQCRule_Id });
        });
    };
    //暴露接口
    exports('QuerySQLTable', QuerySQLTable);
});
