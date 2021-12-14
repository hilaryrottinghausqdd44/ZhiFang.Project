/**
   @Name：日志类型
   @Author：zhangda
   @version 2021-07-16
 */
layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'layer', 'element', 'form', 'table', 'laydate'], function () {
    "use strict";
    var $ = layui.$,
        layer = layui.layer,
        element = layui.element,
        form = layui.form,
        table = layui.table,
        laydate = layui.laydate,
        uxutil = layui.uxutil;

    var app = {};

    //服务地址
    app.url = {
        SELECTURL: uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchSLogByHQL?isPlanish=true',
        GETCLASSDIC: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic'
    };
    //配置
    app.config = {
        userId: uxutil.cookie.get(uxutil.cookie.map.USERID),//当前账户Id  
        checkRowData: [],
        mainTableConfig: {},
        mainTableTableIns: null,
        loading: null
    };

    //初始化
    app.init = function () {
        var me = this;
        me.config.loading = layer.load();
        me.initDataAddTime();
        me.getClassDic('ZhiFang.Entity.LIIP', 'ZF_SLog_LogType', function (data) {
            var html = [];
            $.each(data, function (i, item) {
                html.push('<option value="' + item["Id"] + '">' + item["Name"] + '</option>');
            });
            $("#LogType").html(html.join(''));
            form.render('select');
            me.initMainTable();
            me.initListeners();
        });
    };
    //监听
    app.initListeners = function () {
        var me = this;
        //触发行单击事件
        table.on('row(mainTable)', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj["data"]);
        });
        //监听上传日期改变
        $("#DataAddTime").on("change", function () {
            me.onSearch();
        });
        //监听变更状态改变
        form.on('select(LogType)', function (data) {
            me.onSearch();
        });
        //监听导出excel按钮
        $("#Export").on('click', function () {
            var data = table.cache[me.config.mainTableConfig["id"]];
            table.exportFile(me.config.mainTableConfig["id"], data, 'xls');
        });
        //监听全部导出excel按钮
        $("#ExportAll").on('click', function () {
            var data = me.getAllData();
            table.exportFile(me.config.mainTableConfig["id"], data, 'xls');
        });
    };
    //初始化上传日期
    app.initDataAddTime = function () {
        var me = this,
            today = new Date();
        laydate.render({//没有默认值
            elem: '#DataAddTime',
            type: 'date',
            range: true,
            value: uxutil.date.toString(uxutil.date.getNextDate(today, -3), true) + " - " + uxutil.date.toString(today, true),
            done: function (value, date, endDate) {
                me.onSearch(value);
            }
        });
    };
    //初始化mainTable
    app.initMainTable = function () {
        var me = this;
        //列表配置
        me.config.mainTableConfig = {
            id: 'mainTable',
            elem: '#mainTable',
            height: 'full-70',
            url: '',
            data: me.config.json,
            toolbar: '',
            page: true,
            limit: 100,
            limits: [100, 200, 500, 1000, 1500],
            autoSort: false, //禁用前端自动排序
            initSort: { field: 'SLog_DataAddTime', type: 'desc' },//type如果大写的话 不能识别
            defaultSort: [{ "property": "SLog_DataAddTime", "direction": "desc" }],//默认排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号' },
                { field: 'SLog_LogID', width: 100, title: '主键ID', sort: false, hide: true },
                { field: 'SLog_IP', width: 100, title: 'IP', sort: false, hide: true },
                { field: 'SLog_OperateType', width: 100, title: '操作类型ID', sort: false, hide: true },
                { field: 'SLog_OperateName', width: 120, title: '日志类型', sort: false },
                { field: 'SLog_EmpID', width: 100, title: '操作人ID', sort: false, hide: true },
                { field: 'SLog_EmpName', width: 120, title: '操作人', sort: false },
                { field: 'SLog_Comment', minWidth: 200, title: '日志描述', sort: false },
                { field: 'SLog_InfoLevel', width: 120, title: '信息等级', sort: false, hide: true },
                { field: 'SLog_DataAddTime', width: 120, title: '操作时间', sort: false, templet: function (data) { var that = this || { field: 'SLog_DataAddTime' }; return me.GridColumnsHandle(data, that); } }
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
                if (me.config.loading) {
                    layer.close(me.config.loading);
                    me.config.loading = null;
                }
                if (count == 0) {
                    //添加初始排序后 不显示无数据文本 手动添加
                    if ($("#mainTable+div .layui-table-main .layui-none").length == 0)
                        $("#mainTable+div .layui-table-main").append('<div class="layui-none">暂无相关数据</div>');
                    me.config.checkRowData = [];
                    return;
                }
                if ($("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0]) {
                    setTimeout(function () {
                        $("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                    }, 0);
                }
            }
        };
        //赋值url
        me.config.mainTableConfig.url = me.getMainTableUrl();
        //排序不存在 添加默认排序
        if (me.config.mainTableConfig.url.indexOf("sort=") == -1) me.config.mainTableConfig.url += "&sort=" + JSON.stringify(me.config.mainTableConfig.defaultSort);
        //初始化列表
        me.config.mainTableTableIns = table.render(me.config.mainTableConfig);
    };
    //处理列表时间
    app.GridColumnsHandle = function (record, that) {
        var me = this,
            that = that,
            value = record[that.field] || "",
            str = value.replace(RegExp("/", "g"), "-").replace(RegExp("T", "g"), " ");
        return str || value;
    };
    //获取查询Fields
    app.getStoreFields = function (isString) {
        var me = this,
            tableIns = me.config.mainTableTableIns,
            columns = tableIns ? (tableIns.config.cols[0] || []) : (me.config.mainTableConfig.cols[0] || []),
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
    //查询
    app.onSearch = function (DataAddTime) {
        var me = this,
            tableIns = me.config.mainTableTableIns,
            instance = tableIns.config.instance,
            page = page || instance.layPage.find('.layui-laypage-curr>em:last-child').html() || 1,
            initSort = initSort || instance.sortKey || tableIns.config.initSort,
            sortList = sortList || tableIns.config.defaultSort,
            url = url || me.getMainTableUrl(DataAddTime);
        if (!url) return;
        //排序标记处理
        if (!initSort["field"] && initSort["sort"]) {
            initSort["field"] = initSort["sort"];
            delete initSort.sort;
        }
        //排序不存在
        if (url.indexOf("sort=") == -1) url += "&sort=" + JSON.stringify(sortList);
        //重载
        if (table.cache[instance.key]) {
            me.config.loading = layer.load();
            tableIns.reload({
                url: url,
                height: 'full-70',//不写height 高度会消失
                initSort: initSort,//记录初始排序，如果不设的话，将无法标记表头的排序状态
                page: {
                    curr: page //重新从第 page 页开始
                },
                where: {
                    t: new Date().getTime()
                }
            });
        }
    };
    //获得mainTable列表查询地址
    app.getMainTableUrl = function (DataAddTime) {
        var me = this,
            url = me.url.SELECTURL,
            today = new Date(),
            msg = "",//提示信息
            DataAddTime = DataAddTime || $("#DataAddTime").val() || uxutil.date.toString(uxutil.date.getNextDate(today, -7), true) + " - " + uxutil.date.toString(today, true),//上传日期
            LogType = $("#LogType").val(),//日志类型
            where = [];//其他条件 and
        if (DataAddTime) {
            //验证日期是否正确
            if (DataAddTime.indexOf(" - ") == -1) {
                msg = "日期格式不正确!";
            }
            //验证是否都是日期
            var start = DataAddTime.split(" - ")[0],
                end = DataAddTime.split(" - ")[1],
                DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式
            if (!uxutil.date.isValid(start) || !DATE_FORMAT.test(start) || !uxutil.date.isValid(end) || !DATE_FORMAT.test(end)) {
                msg = "日期格式不正确!";
            }
            //验证开始日期是否大于结束日期
            if (new Date(start).getTime() > new Date(end).getTime()) {
                msg = "开始日期不能大于结束日期!";
            }
            if (msg != "") {
                layer.msg(msg, { icon: 0, anim: 0 });
                return false;
            }
            var startDate = start,
                endDate = uxutil.date.toString(uxutil.date.getNextDate(end, 1), true);
            where.push("DataAddTime>='" + startDate + "' and  DataAddTime < '" + endDate + "'");
        }
        //变更状态
        if (LogType) {
            where.push("OperateType=" + LogType);
        }
        if (where.length > 0) url += "&where=" + where.join(' and ');
        //查询字段
        url += '&fields=' + me.getStoreFields(true).join(',');

        return url;
    };
    //获得查询条件全部数据（不分页 -- 用于全部导出Excel）
    app.getAllData = function () {
        var me = this,
            data = null,
            url = me.getMainTableUrl();
        var load = layer.load();
        uxutil.server.ajax({ url: url, async: false }, function (res) {
            layer.close(load);
            if (res.success) {
                if (res.ResultDataValue) data = JSON.parse(res.ResultDataValue).list;
            }
        });
        return data;
    };
    //获得枚举
    app.getClassDic = function (classnamespace, classname, callBack) {
        var me = this,
            url = me.url.GETCLASSDIC,
            classnamespace = classnamespace || null,
            classname = classname || null;
        if (!url || !classnamespace || !classname) return;
        url += "?classnamespace=" + classnamespace + "&classname=" + classname;

        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    callBack(data);
                }
            } else {
                layer.msg("未找到日志类型枚举类型!", { icon: 0, anim: 0 });
            }
        });
    };
    //初始化
    app.init();
});