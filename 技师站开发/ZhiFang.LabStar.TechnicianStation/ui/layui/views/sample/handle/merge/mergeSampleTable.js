/**
	@name：确定合并样本
	@author：zhangda
	@version 2021-05-12
 */
layui.extend({
    basicStatus: 'views/sample/basic/status',//状态公共方法
}).define(['table', 'form', 'element', 'uxutil','basicStatus'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        element = layui.element,
        basicStatus = layui.basicStatus;

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
            defaultParams: { "testFormRecord": null, isReadOnly: false, sectionID: null },
            /**默认配置*/
            defaultConfig: { },
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
                "property": 'LisTestForm_GSampleNoForOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true',
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
                setTimeout(function () {
                    if ($("#mergeSampleTable+div .layui-table-body table.layui-table tbody tr:first-child")[0])
                        $("#mergeSampleTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                }, 1);
            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //获取查询Url
    app.getLoadUrl = function (SectionID, GTestDate, GSampleNo, where) {
        var me = this,
            url = me.config.selectUrl,
            fields = me.getStoreFields(true);
        //查询字段
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + fields.join();
        url += "&where=listestform.LBSection.Id=" + SectionID + " and listestform.GTestDate='" + GTestDate + "' and listestform.GSampleNo='" + GSampleNo + "'" + where;
        //排序
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += (url.indexOf('?') == -1 ? '?' : '&') + 'sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获得动态的列
    app.getColumns = function (type) {
        var me = this;
        var col = [[
            { field: 'LisTestForm_Id', width: 80, title: '源检验单ID', sort: false, hide: true },
            { field: 'LisTestForm_LisPatient_Id', width: 80, title: '就诊信息ID', sort: false, hide: true },
            { field: 'LisTestForm_PrintCount', width: 80, title: '打印次数', sort: false, hide: true },
            { field: 'LisTestForm_CheckTime', width: 60, title: '审核时间', sort: false, hide: true },
            { field: 'LisTestForm_RedoStatus', width: 80, title: '复检', sort: false, hide: true },
            { field: 'LisTestForm_ESendStatus', width: 80, title: '仪器状态', sort: false, hide: true },
            { field: 'LisTestForm_ReportStatusID', width: 80, title: '报告状态', sort: false, hide: true },
            { field: 'LisTestForm_TestAllStatus', width: 80, title: '检验完成', sort: false, hide: true },
            { field: 'LisTestForm_ZFSysCheckStatus', width: 80, title: '智能审核', sort: false, hide: true },
            { field: 'LisTestForm_SectionID', width: 80, title: '小组', sort: false, hide: true },
            { field: 'LisTestForm_GTestDate', width: 80, title: '检验日期', sort: false },
            { field: 'LisTestForm_GSampleNo', width: 60, title: '样本号', sort: false },
            { field: 'LisTestForm_CName', width: 60, title: '姓名', sort: false },
            { field: 'LisTestForm_PatNo', width: 80, title: '病历号', sort: false },
            { field: 'LisTestForm_BarCode', width: 80, title: '条码号', sort: false },
            { field: 'LisTestForm_MainStatusID', width: 100, title: '样本单主状态', sort: false, templet: function (data) { return basicStatus.onStatusRenderer(data); } },

            { field: 'Direction', width: 40, title: '→', sort: false, align: 'center' },

            { field: 'LisTestForm_DSectionID', width: 80, title: '目标小组', sort: false, hide: true },
            { field: 'LisTestForm_DGTestDate', width: 80, title: '检验日期', sort: false },
            { field: 'LisTestForm_DGSampleNo', width: 60, title: '样本号', sort: false },
            { field: 'LisTestForm_DCName', width: 60, title: '姓名', sort: false },
            { field: 'LisTestForm_DPatNo', width: 80, title: '病历号', sort: false },
            { field: 'LisTestForm_DBarCode', width: 80, title: '条码号', sort: false },
            {
                field: 'IsExist', width: 60, title: '存在', sort: false, align: 'center', templet: function (data) {
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
            { field: 'LisTestForm_DId', width: 80, title: '目标检验单ID', sort: false, hide: true },
            { field: 'LisTestForm_LisPatient_DId', width: 80, title: '就诊信息ID', sort: false, hide: true },
            { field: 'LisTestForm_DPrintCount', width: 80, title: '打印次数', sort: false, hide: true },
            { field: 'LisTestForm_DCheckTime', width: 60, title: '审核时间', sort: false, hide: true },
            { field: 'LisTestForm_DRedoStatus', width: 80, title: '复检', sort: false, hide: true },
            { field: 'LisTestForm_DESendStatus', width: 80, title: '仪器状态', sort: false, hide: true },
            { field: 'LisTestForm_DReportStatusID', width: 80, title: '报告状态', sort: false, hide: true },
            { field: 'LisTestForm_DTestAllStatus', width: 80, title: '检验完成', sort: false, hide: true },
            { field: 'LisTestForm_DZFSysCheckStatus', width: 80, title: '智能审核', sort: false, hide: true },
            { field: 'LisTestForm_DMainStatusID', width: 100, title: '样本单主状态', sort: false, templet: function (data) { return basicStatus.onStatusRenderer(data,true); } },
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
            if (columns[i].field == "Direction") break;
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
        if (!me.tableIns) {
            me.tableIns = table.render(me.config);
        }
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
        table.on('row(mergeSampleTable)', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            layui.event('mergeSampleTable', 'rowclick', obj.data);
        });
    };
    /*** 查询  */
    app.onSearch = function (obj) {
        var me = this;
        var load = layer.load();
        //清空列表
        me.cleardata();
        layui.event('mergeSampleTable', 'cleardata', {});
        //源样本单
        var SampleList = [];
        var LBSection_ID = obj.LBSection_ID;
        //目标样本单
        var DSampleList = [];
        var swhere = "";
        //源样本单
        me.getSampleInfo(obj.LBSection_ID, obj.GTestDate, obj.GSampleNo, swhere, function (data) {
            if (data && data.value) SampleList = data.value.list;
        });
        //如果目标样本存在，状态MainStatusID=0检验中，其它状态不要合并，已经确认或者审定的检验单，不能进行合并
        var dwhere = " and listestform.MainStatusID>=0";
        //目标样本单
        me.getSampleInfo(obj.DLBSection_ID, obj.DGTestDate, obj.DGSampleNo, dwhere, function (data) {
            if (data && data.value) DSampleList = data.value.list;
        });
        //源样本不存在，没有必要合并
        if (SampleList.length == 0) {
            layer.close(load);
            return;
        }
        if (DSampleList.length == 0) {
            var GSampleNo = obj.DGSampleNo.replace(/，/g, ",");
            var DGSampleNo = GSampleNo.split(',');
            for (var i = 0; i < DGSampleNo.length; i++) {
                DSampleList.push({
                    LisTestForm_DSectionID: obj.DLBSection_ID,
                    LisTestForm_GTestDate: obj.DGTestDate,
                    LisTestForm_GSampleNo: DGSampleNo[i],
                    LisTestForm_CName: '',
                    LisTestForm_PatNo: '',
                    LisTestForm_LisPatient_DId: '',
                    LisTestForm_BarCode: '',
                    LisTestForm_Id: '',
                    LisTestForm_MainStatusID: '',
                    IsExist: '0',
                    LisTestForm_PrintCount: '',
                    LisTestForm_CheckTime: '',
                    LisTestForm_RedoStatus: '',
                    LisTestForm_ESendStatus: '',
                    LisTestForm_ReportStatusID: '',
                    LisTestForm_TestAllStatus: '',
                    LisTestForm_ZFSysCheckStatus: ''
                });
            }
        }
        for (var i = 0; i < DSampleList.length; i++) {
            var obj = {
                LisTestForm_DSectionID: obj.DLBSection_ID,
                LisTestForm_DGTestDate: uxutil.date.toString(DSampleList[i].LisTestForm_GTestDate, true),
                LisTestForm_DGSampleNo: DSampleList[i].LisTestForm_GSampleNo,
                LisTestForm_LisPatient_DId: DSampleList[i].LisTestForm_LisPatient_Id ? DSampleList[i].LisTestForm_LisPatient_Id : '',
                LisTestForm_DCName: DSampleList[i].LisTestForm_CName,
                LisTestForm_DPatNo: DSampleList[i].LisTestForm_PatNo,
                LisTestForm_DBarCode: DSampleList[i].LisTestForm_BarCode,
                LisTestForm_DId: DSampleList[i].LisTestForm_Id,
                IsExist: DSampleList[i].IsExist ? DSampleList[i].IsExist : '1',
                LisTestForm_DMainStatusID: DSampleList[i].LisTestForm_MainStatusID,
                LisTestForm_DPrintCount: DSampleList[i].LisTestForm_PrintCount,
                LisTestForm_DCheckTime: DSampleList[i].LisTestForm_CheckTime,
                LisTestForm_DRedoStatus: DSampleList[i].LisTestForm_RedoStatus,
                LisTestForm_DESendStatus: DSampleList[i].LisTestForm_ESendStatus,
                LisTestForm_DReportStatusID: DSampleList[i].LisTestForm_ReportStatusID,
                LisTestForm_DTestAllStatus: DSampleList[i].LisTestForm_TestAllStatus,
                LisTestForm_DTestAllStatus: DSampleList[i].LisTestForm_ZFSysCheckStatus
            }
            for (var j = 0; j < SampleList.length; j++) {
                SampleList[j].LisTestForm_SectionID = LBSection_ID;
                SampleList[j].LisTestForm_GTestDate = uxutil.date.toString(SampleList[j].LisTestForm_GTestDate, true);
                SampleList[j].Direction = "→";
                Object.assign(obj, SampleList[j]);
                SampleList.splice(j, 1);
                break;
            }
            //存在源样本才加数据
            if (obj.LisTestForm_Id) {
                me.tableIns.reload({
                    url: '',
                    data: [obj]
                });
            }
        }
    };
    /**根据检验小组，检验日期，样本号得到样本单*/
    app.getSampleInfo = function (SectionID, GTestDate, GSampleNo, where, callback) {
        var me = this,
            url = me.getLoadUrl(SectionID, GTestDate, GSampleNo, where);

        uxutil.server.ajax({
            url: url,
            async: false
        }, function (res) {
            if (res.success) {
                callback(res);
            }
        });
    };
    //清空列表
    app.cleardata = function () {
        var me = this;
        me.tableIns.reload({ url: '', data: [] });
    };
    //暴露接口
    exports('mergeSampleTable', app);
});
