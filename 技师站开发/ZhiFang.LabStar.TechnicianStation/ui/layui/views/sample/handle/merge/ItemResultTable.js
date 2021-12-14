/**
	@name：项目结果源
	@author：zhangda
	@version 2021-05-12
 */
layui.extend({
}).define(['table', 'form', 'element', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        element = layui.element;

    var app = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
            elem: '',
            id: "",
            checkRowData: [],
            /**默认传入参数*/
            defaultParams: {  },
            /**默认配置*/
            defaultConfig: {  },
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
                "property": 'LisTestItem_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
            where: "",
            toolbar: '',
            page: false,
            limit: 10000,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[]],
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
                if (count == 0) return;
                if ($("#ItemResultTable+div .layui-table-body table.layui-table tbody tr:first-child")[0])
                    $("#ItemResultTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //获取查询Url
    app.getLoadUrl = function (TestFormID, GTestDate) {
        var me = this,
            url = me.config.selectUrl,
            fields = ['LisTestItem_LBItem_Id', 'LisTestItem_LBItem_CName', 'LisTestItem_ReportValue', 'LisTestItem_Id'];
        //查询字段
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + fields.join();
        url += "&where=listestitem.LisTestForm.Id=" + TestFormID + " and listestitem.GTestDate='" + GTestDate + "'";
        //排序
        //var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        //if (defaultOrderBy && defaultOrderBy.length > 0) url += (url.indexOf('?') == -1 ? '?' : '&') + 'sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获得动态的列
    app.getColumns = function (type) {
        var me = this;
        var col = [[
            { type: 'checkbox', width: 26 },
            { field: 'LisTestItem_LBItem_Id', width: 80, title: '项目编号', sort: false, hide: true },
            { field: 'LisTestItem_LBItem_CName', width: 200, title: '项目名称', sort: false },
            { field: 'LisTestItem_ReportValue', width: 80, title: '源项目值', sort: false },
            { field: 'Direction', width: 60, title: '→', sort: false, align: 'center' },
            { field: 'LisTestItem_DLBItem_Id', width: 80, title: '目标项目编号', sort: false, hide: true },
            { field: 'LisTestItem_DReportValue', width: 80, title: '目标项目值', sort: false },
            {
                field: 'IsExist', width: 80, title: '存在', sort: false, align: 'center', templet: function (data) {
                    var that = this;
                    var str = data[that.field];
                    if (data["IsExist"] != 1) {
                        str = "<span style='color:red;'>否</span>";
                    } else {
                        str = "<span style='color:green;'>是</span>";
                    }
                    return str;
                }
            },
            { field: 'LisTestItem_Id', width: 60, title: '检验单项目结果ID', sort: false, hide: true },
            { field: 'LisTestItem_DId', width: 100, title: '目标检验单结果ID', sort: false, hide: true }
        ]];
        return col;
    };
    //获取查询Fields
    app.getStoreFields = function (isString) {
        var me = this,
            columns = me.getColumns()[0] || [],
            length = columns.length,
            fields = [];
        for (var i = 0; i < length; i++) {
            if (columns[i].field == "Tab") continue;
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
    app.render = function (options) {
        var me = this;
        me.set(options);
        me.config.url = '';
        me.config.cols = me.getColumns();
        //第一次加载
        if (!me.tableIns) me.tableIns = table.render(me.config);
        //监听
        me.iniListeners();
        return me;
    };
    //判断浏览器大小方法
    app.screen = function ($) {
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
    //监听
    app.iniListeners = function () {
        var me = this;
        //监听行单击事件
        table.on('row(ItemResultTable)', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
        });
    };
    app.onSearch = function(TestFormID, GTestDate, DTestFormID, DGTestDate) {
        var me = this,
            data = [];
        var load = layer.load();
        //清空列表
        me.cleardata();
        //源样本项目
        var ItemList = [];
        //目标样本项目
        var DItemList = [];
        //源样本项目
        me.getItemInfo(TestFormID, GTestDate, function (data) {
            if (data && data.value) ItemList = data.value.list;
        });
        //目标样本项目
        if (DTestFormID) {
            me.getItemInfo(DTestFormID, DGTestDate, function (data) {
                if (data && data.value) DItemList = data.value.list;
            });
        }
        for (var i = 0; i < ItemList.length; i++) {
            var ItemID = ItemList[i].LisTestItem_LBItem_Id;
            for (var j = 0; j < DItemList.length; j++) {
                if (ItemID == DItemList[j].LisTestItem_LBItem_Id) {
                    var obj = {
                        LisTestItem_DReportValue: DItemList[j].LisTestItem_ReportValue,
                        LisTestItem_DId: DItemList[j].LisTestItem_Id,
                        LisTestItem_DLBItem_Id: DItemList[j].LisTestItem_LBItem_Id,
                        IsExist: '1'
                    }
                    Object.assign(ItemList[i], obj);
                    DItemList.splice(j, 1);
                    break;
                }
            }
            ItemList[i].LAY_CHECKED = true;
            data.push(ItemList[i]);
        }
        layer.close(load);
        me.tableIns.reload({
            url: '',
            data: data
        });
    };
    /**根据样本单ID，检验日期,得到样本单项目*/
    app.getItemInfo = function(TestFormID, GTestDate, callback) {
        var me = this,
            url = me.getLoadUrl(TestFormID, GTestDate);

        uxutil.server.ajax({
            url: url,
            async: false
        }, function (res) {
            if (res.success) {
                callback(res);
            }
        });
    }
    //清空列表
    app.cleardata = function () {
        var me = this;
        me.tableIns.reload({ url: '', data: [] });
    };
    //暴露接口
    exports('ItemResultTable', app);
});
