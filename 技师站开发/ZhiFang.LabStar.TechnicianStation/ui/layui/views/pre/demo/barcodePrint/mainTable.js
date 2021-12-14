/**
	@name：病人信息表格
	@author：zhangda
	@version 2020-08-19
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form;
    var config = {
        loadIndex: null
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
                "property": 'LisOrderForm_OrderTime',
                "direction": 'DESC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOrderFormByHQL?isPlanish=true',
            where: "",
            toolbar:'#Toolbar1',
            defaultToolbar: ['filter'],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [
                []
            ],
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
                if (config.loadIndex) {
                    layer.close(config.loadIndex);
                    config.loadIndex = null;
                }
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
        me = $.extend(true, {}, mainTable, Class.pt, me);
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
        //if (config.SectionID) arr.push("SectionID='" + config.SectionID + "'");
        //if (config.TranRuleTypeID) arr.push("lbtranruletype.Id='" + config.TranRuleTypeID + "'");
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + JSON.stringify(where);
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
        var me = this,
            inst = new Class(options),
            cols = [
            { type: 'checkbox' },
            { type: 'numbers', title: '行号' },
            { field: 'LisOrderForm_Id', width: 80, title: '主键ID', sort: false, hide: true },
            { field: 'LisOrderForm_IsUrgent', width: 80, title: '急查标志', sort: false, hide: true },
            { field: 'LisOrderForm_LisPatient_PatNo', width: 80, title: '病历号', sort: false },
            { field: 'LisOrderForm_LisPatient_OrderTime', width: 80, title: '条码号', sort: false },
            { field: 'LisOrderForm_LisPatient_CName', title: '姓名', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_GenderID', title: '性别Id', width: 120, sort: false, hide: true },
            { field: 'LisOrderForm_LisPatient_GenderName', title: '性别', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_Age', title: '年龄', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_AgeUnitID', title: '年龄单位Id', width: 120, sort: false, hide: true },
            { field: 'LisOrderForm_LisPatient_AgeUnitName', title: '年龄单位', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_Bed', title: '床位', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_SampleTypeID', title: '采样组', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_Bed', title: '采样管', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_Bed', title: '采样管颜色', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_SampleTypeID', title: '样本类型Id', width: 120, sort: false, hide: true },
            { field: 'LisOrderForm_LisPatient_SampleTypeID', title: '样本类型', width: 120, sort: false },
            { field: 'LisOrderForm_ParItemCName', title: '医嘱项目', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_DeptID', title: '科室Id', width: 120, sort: false, hide: true },
            { field: 'LisOrderForm_LisPatient_DeptName', title: '科室', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_DoctorID', title: '医生Id', width: 120, sort: false, hide: true },
            { field: 'LisOrderForm_LisPatient_DoctorName', title: '医生', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_DoctorName', title: '采样人', width: 120, sort: false },
            { field: 'LisOrderForm_LisPatient_DoctorName', title: '旧开单时间', width: 120, sort: false },
            { field: 'LisOrderForm_PhotoURL', title: '图片路径', width: 120, sort: false }
        ];
        switch (options["tabIndex"]) {
            case 1:
                cols.push({ title: '操作', toolbar: '#tableOperation2', width: 160, align: 'center', fixed: 'right' });
                break;
            default:
                cols.push({ title: '操作', toolbar: '#tableOperation1', width: 160, align: 'center', fixed: 'right' });
                break;
        }
        inst.config.cols[0] = cols;
        inst.tableIns = table.render(inst.config);
        me.set(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    mainTable.onSearch = function (mytable, options) {
        var me = this;
        if (options) config = $.extend({}, config, options);
        var inst = new Class(me);
        config.loadIndex = layer.load();
        mainTable.url = inst.getLoadUrl();
        mainTable.config.elem = "#" + mytable;
        mainTable.config.id = mytable;
        table.reload(mytable, {
            url: '',//inst.getLoadUrl(),
            data: [{"LisOrderForm_LisPatient_OrderTime":'123'}]
        });
    };
    //判断浏览器大小方法
    Class.pt.screen = function () {
        var width = $(window).width();//获取当前窗口的宽度
        if (width > 1200)
            return 3;   //大屏幕
        else if (width > 992)
            return 2;   //中屏幕
        else if (width > 768)
            return 1;   //小屏幕
        else
            return 0;   //超小屏幕
    };
    Class.pt.iniListeners = function () {
        var me = this;
        //监听头工具栏事件
        table.on('toolbar(mainTable)', function (obj) {
            var checkStatus = table.checkStatus(obj.config.id);
            switch (obj.event) {
                case 'add':
                    break;
            };
        });
        //监听工具条 
        table.on('tool(mainTable)', function (obj) {
            var data = obj.data, //获得当前行数据
                layEvent = obj.event;

            if (layEvent === 'edit') { //修改

            } else if (layEvent === 'noConfirmToVoid') { //未确定条码作废

            } else if (layEvent === 'supplement') { //补录

            } else if (layEvent === 'cancel') { //撤销

            } else if (layEvent === 'confirmToVoid') { //已确定条码作废

            }
        });
        //监听行单击事件
        table.on('row(mainTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            layui.event("rowClick", "click", obj.data);
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
