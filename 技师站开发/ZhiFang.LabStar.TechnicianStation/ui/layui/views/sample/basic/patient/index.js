/**
   @Name：检验主界面
   @Author：zhangda
   @version 2021-04-01
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base'
}).define(['uxutil','uxbase', 'element', 'layer', 'form', 'table'], function (exports) {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        table = layui.table,
        uxbase = layui.uxbase,
        uxutil = layui.uxutil;

    var app = {};
    //参数
    app.params = { PatNo: null, CName: null };
    //服务地址
    app.url = {
        //获得就诊信息
        getPatientUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisPatientByHQL?isPlanish=true'
    };
    //配置
    app.config = {
        mainTableIns: null,
        mainTableConfig: null,
        checkRowData: [],
        replacePatientType: 1,//就诊信息替换方式：1：完全替换（默认） 2：已有设置不替换，替换没有设置的 3：不替换
    };

    //初始化
    app.init = function () {
        var me = this;
        me.getParams();
        $("#PatNo").val(me.params.PatNo ? me.params.PatNo : "");
        $("#CName").val(me.params.CName ? me.params.CName : "");
        me.initMainTable();
        me.initListeners();
    };
    //获得参数
    app.getParams = function () {
        var me = this,
            params = uxutil.params.get();
        me.params = $.extend({}, me.params, params);
    };
    //监听
    app.initListeners = function () {
        var me = this;
        //监听就诊信息替换方式
        form.on('select(Type)', function (data) {
            me.config.replacePatientType = data.value;
        });
        //查询按钮监听
        $("#search").click(function () {
            me.onSearch();
        });
        //监听样本单列表单击
        table.on('row(mainTable)', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj["data"]);
        });
    };
    //初始化列表
    app.initMainTable = function () {
        var me = this;
        me.config.mainTableConfig = {
            elem: '#mainTable',
            id: 'mainTable',
            height: 'full-60',
            toolbar: '',
            url: '',
            data: [],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 500, 1000],
            initSort: { field: 'LisPatient_PartitionDate', type: 'desc' },//type如果大写的话 不能识别
            defaultSort: [{ "property": "LisPatient_PartitionDate", "direction": "desc" }],//默认排序
            nowSort: [{ "property": "LisPatient_PartitionDate", "direction": "desc" }],//默认排序
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers' },
                { field: 'LisPatient_PartitionDate', width: 140, title: '医嘱日期', sort: false },
                { field: 'LisPatient_PatNo', width: 100, title: '病历号', sort: false },
                { field: 'LisPatient_CName', width: 100, title: '姓名', sort: false },
                { field: 'LisPatient_GenderName', width: 100, title: '性别', sort: false },
                { field: 'LisPatient_Birthday', width: 140, title: '出生日期', sort: false },
                { field: 'LisPatient_SickType', width: 100, title: '就诊类型', sort: false },
                { field: 'LisPatient_DiagName', width: 100, title: '诊断', sort: false },
                { field: 'LisPatient_DeptName', width: 100, title: '科室', sort: false },
                { field: 'LisPatient_DoctorName', width: 100, title: '医生', sort: false },

                { field: 'LisPatient_GenderID', width: 100, title: '性别ID', sort: false, hide: true },
                { field: 'LisPatient_PatHeight', width: 100, title: '身高', sort: false, hide: true },
                { field: 'LisPatient_PatWeight', width: 100, title: '体重', sort: false, hide: true },
                { field: 'LisPatient_FolkID', width: 100, title: '民族ID', sort: false, hide: true },
                { field: 'LisPatient_FolkName', width: 100, title: '民族', sort: false, hide: true },
                { field: 'LisPatient_PatAddress', width: 100, title: '地址', sort: false, hide: true },
                { field: 'LisPatient_PatPhoto', width: 100, title: '病人相片', sort: false, hide: true },
                { field: 'LisPatient_PhoneCode', width: 100, title: '电话', sort: false, hide: true },
                { field: 'LisPatient_WeChatNo', width: 100, title: '微信号', sort: false, hide: true },
                { field: 'LisPatient_EMailAddress', width: 100, title: '邮箱', sort: false, hide: true },
                { field: 'LisPatient_PatType', width: 100, title: '患者类型', sort: false, hide: true },
                { field: 'LisPatient_IDCardNo', width: 100, title: '身份证号', sort: false, hide: true },
                { field: 'LisPatient_HisPatNo', width: 100, title: '医院病人唯一编号', sort: false, hide: true },
                { field: 'LisPatient_PatCardNo', width: 100, title: '就诊卡号', sort: false, hide: true },
                { field: 'LisPatient_InPatNo', width: 100, title: '住院号', sort: false, hide: true },
                { field: 'LisPatient_ExamNo', width: 100, title: '体检号', sort: false, hide: true },
                { field: 'LisPatient_MedicareNo', width: 100, title: '医保卡号', sort: false, hide: true },
                { field: 'LisPatient_UnionPayNo', width: 100, title: '银联卡号', sort: false, hide: true },
                { field: 'LisPatient_HealthCardNo', width: 100, title: '居民健康卡号', sort: false, hide: true },
                { field: 'LisPatient_PowerCardNo', width: 100, title: '一卡通卡号', sort: false, hide: true },

                { field: 'LisPatient_Age', width: 100, title: '年龄', sort: false, hide: true },
                { field: 'LisPatient_AgeUnitID', width: 100, title: '年龄单位ID', sort: false, hide: true },
                { field: 'LisPatient_AgeUnitName', width: 100, title: '年龄单位', sort: false, hide: true },
                { field: 'LisPatient_AgeDesc', width: 100, title: '年龄描述', sort: false, hide: true },
                { field: 'LisPatient_SickTypeID', width: 100, title: '就诊类型ID', sort: false, hide: true },
                { field: 'LisPatient_DiagID', width: 100, title: '诊断ID', sort: false, hide: true },
                { field: 'LisPatient_DoctorID', width: 100, title: '医生ID', sort: false, hide: true },
                { field: 'LisPatient_DoctorTell', width: 100, title: '医生嘱托', sort: false, hide: true },
                { field: 'LisPatient_ExecDeptID', width: 100, title: '执行科室ID', sort: false, hide: true },
                { field: 'LisPatient_DeptID', width: 100, title: '科室ID', sort: false, hide: true },
                { field: 'LisPatient_VisitTimes', width: 100, title: '住院次数', sort: false, hide: true },
                { field: 'LisPatient_VisitDate', width: 100, title: '住院日期', sort: false, hide: true },
                { field: 'LisPatient_DistrictID', width: 100, title: '病区ID', sort: false, hide: true },
                { field: 'LisPatient_DistrictName', width: 100, title: '病区', sort: false, hide: true },
                { field: 'LisPatient_WardID', width: 100, title: '病房ID', sort: false, hide: true },
                { field: 'LisPatient_WardName', width: 100, title: '病房', sort: false, hide: true },
                { field: 'LisPatient_Bed', width: 100, title: '病床', sort: false, hide: true },
                { field: 'LisPatient_HISComment', width: 100, title: '临床HIS备注', sort: false, hide: true },
                { field: 'LisPatient_PatComment', width: 100, title: '临床病人备注', sort: false, hide: true },
                { field: 'LisPatient_ID', width: 100, title: '主键ID', sort: false, hide: true }
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
                if (count == 0) {
                    //添加初始排序后 不显示无数据文本 手动添加
                    if ($("#mainTable+div .layui-table-main .layui-none").length == 0)
                        $("#mainTable+div .layui-table-main").append('<div class="layui-none">暂无相关数据</div>');
                    me.config.checkRowData = [];
                    return;
                }
                setTimeout(function () {
                    if ($("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0])
                        $("#mainTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                }, 0);
            }
        };
        me.config.mainTableConfig.url = me.getLoadUrl();
        if (me.config.mainTableConfig.url) me.config.mainTableConfig.url += "&sort=" + JSON.stringify(me.config.mainTableConfig.defaultSort);
        me.config.mainTableIns = table.render(me.config.mainTableConfig);
    };
    //获取查询Fields
    app.getStoreFields = function (isString) {
        var me = this,
            tableIns = me.config.mainTableIns,
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
    //获得查询Url
    app.getLoadUrl = function () {
        var me = this,
            url = me.url.getPatientUrl,
            PatNo = $("#PatNo").val(),//病历号
            CName = $("#CName").val(),//姓名
            isLike = $("#isLike").prop('checked'),//是否模糊查询
            where = [];//其他条件 and

        if (PatNo) where.push('PatNo' + (isLike ? " like '%" + PatNo + "%'" : "='" + PatNo + "'"));
        if (CName) where.push('CName' + (isLike ? " like '%" + CName + "%'" : "='" + CName + "'"));
        //不存在查询条件
        if (where.length == 0) {
            uxbase.MSG.onWarn("请输入查询条件进行查询!");
            return '';
        }
        url += "&where=" + encodeURI(where.join(' and '));
        //查询字段
        url += '&fields=' + me.getStoreFields(true).join(',');

        return url;

    };
    //查询
    app.onSearch = function (page, url, initSort, sortList) {
        var me = this,
            tableIns = me.config.mainTableIns,
            instance = tableIns.config.instance,
            page = page || instance.layPage.find('.layui-laypage-curr>em:last-child').html() || 1,
            initSort = initSort || instance.sortKey || tableIns.config.initSort,
            sortList = sortList || tableIns.config.nowSort,
            url = url || me.getLoadUrl();
        if (!url) return;
        //排序标记处理
        if (!initSort["type"] && initSort["sort"]) {
            initSort["type"] = initSort["sort"];
            delete initSort.sort;
        }
        //排序不存在
        if (url.indexOf("sort=") == -1) url += "&sort=" + JSON.stringify(sortList);
        //重载
        if (table.cache[instance.key]) {
            tableIns.reload({
                url: url,
                height: 'full-60',//不写height 高度会消失
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
    //初始化
    app.init();
    //暴露接口
    exports('patient', app.config);
});