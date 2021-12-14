/**
   @Name：检验主界面
   @Author：zhangda
   @version 2021-04-01
*/
//模块配置--表单列表JSON对象
var MODULEJSONOBJ = null;
layui.extend({
    uxutil: 'ux/util',
    uxbase:'ux/base',
    tableSelect: '../src/tableSelect/tableSelect',//下级info.js需要
    basicStatus:'views/sample/basic/status',//状态公共方法
    topToolBar: 'views/sample/topToolBar',//功能按钮
    info: 'views/sample/info',//样本单信息
    ProEditor:'views/sample/tab/editor/ProEditor',
    SampleResultTable: 'views/sample/tab/sample/table',//检验结果
    images:'views/sample/tab/sample/images',//图片结果
    equipResult: 'views/sample/tab/equipResult/equipResult',//仪器结果
    extrctEquipResult: 'views/sample/tab/equipResult/extrct/equipResult',//重提仪器结果
	history:'views/sample/tab/history/history',//历史对比
	report:'views/sample/tab/report/report',//相关医嘱报告
	bottomToolBar:'views/sample/bottomToolBar'//底部信息
}).define(['uxutil', 'uxbase', 'table', 'form', 'element', 'laydate', 'tableSelect', 'basicStatus', 'topToolBar', 'info','ProEditor', 'SampleResultTable', 'images', 'equipResult', 'extrctEquipResult', 'history', 'report', 'bottomToolBar'], function (exports) {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        laydate = layui.laydate,
        form = layui.form,
        uxutil = layui.uxutil,
        uxbase = layui.uxbase,
        basicStatus = layui.basicStatus,
        topToolBar = layui.topToolBar,
        info = layui.info,
        ProEditor = layui.ProEditor,
        SampleResultTable = layui.SampleResultTable,
        images = layui.images,
        equipResult = layui.equipResult,
        extrctEquipResult = layui.extrctEquipResult,
        history = layui.history,
        report = layui.report,
        bottomToolBar = layui.bottomToolBar,
        table = layui.table,
        MOD_NAME = "SampleIndex";
    var app = {};
    //外部参数
    app.params = {
        sectionID: null,
        sectionCName: null,
        sectionProDLL:0,//小组专业编辑
        TABMODULEID: null
    };
    //全局配置
    app.config = {
        iTime: null,//定时器 -- 检验单行点击
        userID: null,//登录人ID
        localTotalName: 'LabStar_TS',// localStorage中存储小组的名称
        localSectionName: 'OpenedSectionList',// localStorage中存储小组的名称
        isReadOnly: false,//是否只查看
        searchDays: -2,//默认查询天数
        otherStatusDescHtml: '',
        SampleListTableIns: null,//检验单列表对象实例
        SampleListTableConfig: {},//样本单列表配置
        //检验单列表默认查询字段
        SampleListTableCols: [
            "LisTestForm_StatusID", "LisTestForm_GTestDate", "LisTestForm_CName", "LisTestForm_PatNo", "LisTestForm_GSampleNoForOrder", "LisTestForm_GSampleNo",
            "LisTestForm_BarCode", "LisTestForm_LisPatient_GenderName", "LisTestForm_GSampleType",
            "LisTestForm_Id", "LisTestForm_LBSection_Id", "LisTestForm_GSampleTypeID", "LisTestForm_LisOrderForm_Id", "LisTestForm_MainStatusID",
            "LisTestForm_EquipFormID", "LisTestForm_PrintCount", "LisTestForm_CheckTime", "LisTestForm_RedoStatus", "LisTestForm_UrgentState", "LisTestForm_SampleSpecialDesc",
            "LisTestForm_ESendStatus", "LisTestForm_ReportStatusID", "LisTestForm_TestAllStatus", "LisTestForm_ZFSysCheckStatus", "LisTestForm_ZFSysCheckInfo",
            "LisTestForm_ConfirmInfo", "LisTestForm_AlarmLevel", "LisTestForm_AlarmInfo", "LisTestForm_FormInfoStatus", "LisTestForm_IExamine","LisTestForm_MigrationFlag"
        ],
        sampleStatus: 'show',//样本单操作状态 查看/编辑/新增
        checkRowData: [], // 检验单列表点击行
        SaveAndAddClick: false, //是否点击了保存并新增
        NextSampleNoInfo: null,//保存向下的下一个样本号信息
        ReceiveAction: false,//是否是核收动作
        SampleResultTabIndex:1,//检验结果列表所在页签下标
        currTabIndex: 1,//当前显示结果页签下标
        isLoadTabArr: [], //已加载的结果页签
        positionID: null,
        isLoadSampleInfo:true,//是否刷新样本单信息部分 -- 通讯消息使用
        statusList: [{ id: '0', name: '检验中' }, { id: '2', name: '检验确认' }, { id: '3', name: '审核' }, { id: '-2', name: '作废' }],//主状态属性列表
        //栅格数
        gridNumber: [
            {//0
                "sampleTableBox": 'layui-col-md4 layui-col-xs12',
                "contentBox": 'layui-col-md8 layui-col-xs12',
                "testFormInfo": 'layui-col-md4 layui-col-xs12',
                "resultTabBox": 'layui-col-md8 layui-col-xs12'
            },
            {//1
                "sampleTableBox": 'layui-col-md4 layui-col-xs12',
                "contentBox": 'layui-col-md8 layui-col-xs12',
                "testFormInfo": 'layui-col-md3 layui-col-xs12',
                "resultTabBox": 'layui-col-md9 layui-col-xs12'
            }
        ],
        moduleJson: null
    };
    //样本单信息Dom元素
    app.sampleFormDom = {
        "LisTestForm_GTestDate": [//检测日期
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_GTestDate" name="LisTestForm_GTestDate" placeholder="必填项" autocomplete="off" class="layui-input myDate" />',
            '<i class="layui-icon layui-icon-date"></i>',
            '</div>'
        ],
        "LisTestForm_GSampleNo": [//样本号
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_GSampleNo" id="LisTestForm_GSampleNo" placeholder="必填项" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_BarCode": [//条码号
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_BarCode" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_SickTypeID": [//就诊类型
            '<div class="layui-input-block">',
            '<select name="LisTestForm_LisPatient_SickTypeID" id="LisTestForm_LisPatient_SickTypeID" lay-filter="LisTestForm_LisPatient_SickTypeID"></select>',
            '</div>'
        ],
        "LisTestForm_GSampleTypeID": [//样本类型
            '<div class="layui-input-block">',
            '<select name="LisTestForm_GSampleTypeID" id="LisTestForm_GSampleTypeID" lay-filter="LisTestForm_GSampleTypeID"></select>',
            '</div>'
        ],
        "LisTestForm_LisPatient_CName": [//姓名
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_CName" id="LisTestForm_LisPatient_CName" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_PatNo": [//病历号
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_PatNo" id="LisTestForm_LisPatient_PatNo" autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-layer"></i>',
            '</div>'
        ],
        "LisTestForm_UrgentState": [//加急标志
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_UrgentState" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_Testaim": [//送检目的
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_Testaim" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_FormMemo": [//样本备注
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_FormMemo" name="LisTestForm_FormMemo" data-typecode="YBBZ" data-typename="样本备注" autocomplete="off" class="layui-input myPhrase" />',
            '<i class="layui-icon layui-icon-add-1"></i>',
            '</div>'
        ],
        "LisTestForm_SampleSpecialDesc": [//特殊性状描述
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_SampleSpecialDesc" name="LisTestForm_SampleSpecialDesc" data-typecode="TSXZMS" data-typename="特殊性状描述" autocomplete="off" class="layui-input myPhrase" />',
            '<i class="layui-icon layui-icon-add-1"></i>',
            '</div>'
        ],
        "LisTestForm_CollectTime": [//采样时间
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_CollectTime" name="LisTestForm_CollectTime" data-type="datetime" autocomplete="off" class="layui-input myDate" />',
            '<i class="layui-icon layui-icon-date"></i>',
            '</div>'
        ],
        "LisTestForm_InceptTime": [//签收时间
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_InceptTime" name="LisTestForm_InceptTime" data-type="datetime" autocomplete="off" class="layui-input myDate" />',
            '<i class="layui-icon layui-icon-date"></i>',
            '</div>'
        ],
        "LisTestForm_ReceiveTime": [//核收时间
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_ReceiveTime" name="LisTestForm_ReceiveTime" data-type="datetime" autocomplete="off" class="layui-input myDate" />',
            '<i class="layui-icon layui-icon-date"></i>',
            '</div>'
        ],
        "LisTestForm_Checker": [//审核者:
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_Checker" id="LisTestForm_Checker" autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-triangle-d"></i>',
            '</div>'
        ],
        "LisTestForm_CheckerID": [//审核者ID:
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_CheckerID" id="LisTestForm_CheckerID" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_CheckTime": [//审核时间:
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_CheckTime" id="LisTestForm_CheckTime" data-type="datetime" autocomplete="off" class="layui-input myDate" />',
            '<i class="layui-icon layui-icon-date"></i>',
            '</div>'
        ],
        "LisTestForm_MainTester": [//检验者
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_MainTester" id="LisTestForm_MainTester" readonly autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-triangle-d"></i>',
            '</div>'
        ],
        "LisTestForm_MainTesterId": [//检验者ID
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_MainTesterId" id="LisTestForm_MainTesterId" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_TestComment": [//检验备注
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_TestComment" name="LisTestForm_TestComment" data-typecode="JYBZ" data-typename="检验备注" autocomplete="off" class="layui-input myPhrase" />',
            '<i class="layui-icon layui-icon-add-1"></i>',
            '</div>'
        ],
        "LisTestForm_TestInfo": [//检验评语
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_TestInfo" name="LisTestForm_TestInfo" data-typecode="JYPY" data-typename="检验评语" autocomplete="off" class="layui-input myPhrase" />',
            '<i class="layui-icon layui-icon-add-1"></i>',
            '</div>'
        ],
        "LisTestForm_TestType": [//检验类型
            '<div class="layui-input-block">',
            '<select name="LisTestForm_TestType" id="LisTestForm_TestType" lay-filter="LisTestForm_TestType"></select>',
            '</div>'
        ],
        "LisTestForm_Id": [//检验单ID
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_Id" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_GenderID": [//性别
            '<div class="layui-input-block">',
            '<select name="LisTestForm_LisPatient_GenderID" id="LisTestForm_LisPatient_GenderID" lay-filter="LisTestForm_LisPatient_GenderID"></select>',
            '</div>'
        ],
        "LisTestForm_LisPatient_Birthday": [//出生日期
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_LisPatient_Birthday" name="LisTestForm_LisPatient_Birthday" data-type="datetime" autocomplete="off" class="layui-input myDate" />',
            '<i class="layui-icon layui-icon-date"></i>',
            '</div>'
        ],
        "LisTestForm_LisPatient_Age": [//年龄
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_LisPatient_Age" name="LisTestForm_LisPatient_Age" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_AgeUnitID": [//年龄单位
            '<div class="layui-input-block">',
            '<select name="LisTestForm_LisPatient_AgeUnitID" id="LisTestForm_LisPatient_AgeUnitID" lay-filter="LisTestForm_LisPatient_AgeUnitID"></select>',
            '</div>'
        ],
        "LisTestForm_LisPatient_AgeDesc": [//年龄描述
            '<div class="layui-input-block">',
            '<input type="text" id="LisTestForm_LisPatient_AgeDesc" name="LisTestForm_LisPatient_AgeDesc" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_PatWeight": [//体重
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_PatWeight" id="LisTestForm_LisPatient_PatWeight" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_PatHeight": [//身高
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_PatHeight" id="LisTestForm_LisPatient_PatHeight" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_Hospital": [//送检单位
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_Hospital" id="LisTestForm_Hospital" readonly autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-triangle-d"></i>',
            '</div>'
        ],
        "LisTestForm_LisPatient_Bed": [//床号
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_Bed" id="LisTestForm_LisPatient_Bed" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_DistrictName": [//病区
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DistrictName" id="LisTestForm_LisPatient_DistrictName" readonly autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-triangle-d"></i>',
            '</div>'
        ],
        "LisTestForm_LisPatient_DistrictID": [//病区ID
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DistrictID" id="LisTestForm_LisPatient_DistrictID" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_DeptName": [//科室
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DeptName" id="LisTestForm_LisPatient_DeptName" readonly autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-triangle-d"></i>',
            '</div>'
        ],
        "LisTestForm_LisPatient_DeptID": [//科室ID
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DeptID" id="LisTestForm_LisPatient_DeptID" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_DoctorName": [//医生
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DoctorName" id="LisTestForm_LisPatient_DoctorName" readonly autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-triangle-d"></i>',
            '</div>'
        ],
        "LisTestForm_LisPatient_DoctorID": [//医生ID
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DoctorID" id="LisTestForm_LisPatient_DoctorID" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_DiagName": [//诊断
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DiagName" id="LisTestForm_LisPatient_DiagName" readonly autocomplete="off" class="layui-input" />',
            '<i class="layui-icon layui-icon-triangle-d"></i>',
            '</div>'
        ],
        "LisTestForm_LisPatient_DiagID": [//诊断ID
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_DiagID" id="LisTestForm_LisPatient_DiagID" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_HISComment": [//HIS备注
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_HISComment" id="LisTestForm_LisPatient_HISComment" autocomplete="off" class="layui-input" />',
            '</div>'
        ],
        "LisTestForm_LisPatient_Id": [//就诊信息ID
            '<div class="layui-input-block">',
            '<input type="text" name="LisTestForm_LisPatient_Id" autocomplete="off" class="layui-input" />',
            '</div>'
        ]
    };
    //过滤条件复选框属性列表
    app.STATUS_LIST = basicStatus.STATUS_LIST;
    //状态样色
    app.ColorMap = basicStatus.ColorMap;
    //主状态样式模板
    app.MainStatusTemplet = basicStatus.MainStatusTemplet;
    //颜色条模板
    app.ColorLineTemplet = basicStatus.ColorLineTemplet;
    //悬浮内容样式
    app.TipsTemplet = basicStatus.TipsTemplet;
    //服务地址
    app.url = {
        //样本单列表
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormCurPageByHQL?isPlanish=true',//'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true'
        //保存样本单与检验项目结果（都是编辑状态才可以调用该服务）
        onEditLisTestFormAndItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_EditLisTestFormAndItemByField',
        //核收服务
        onReceiveUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QuickReceiveBarCodeForm',
        //根据样本号获取下一个顺序样本号
        onQueryNextSampleNoByCurSampleNo: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryNextSampleNoByCurSampleNo',
        //通过样本号和其他条件查询Lis_TestForm定位分页
        onQueryLisTestFormCurPageBySampleNo: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormCurPageBySampleNo'
    };
    //初始化
    app.init = function () {
        var me = this;
        me.setGridNumber();
        me.config.userID = uxutil.cookie.get(uxutil.cookie.map.USERID);
        var loading = layer.load();
        me.getParams();
        //判断是否是只读模式
        if (me.params.sectionID)
            me.config.isReadOnly = false;
        else
            me.config.isReadOnly = true;
        //判断该小组是否是大文本
        if (me.params.sectionProDLL == 1) {
            if ($("#ProDLLTabLi").hasClass("layui-hide")) $("#ProDLLTabLi").removeClass("layui-hide");
        } else {
            if (!$("#ProDLLTabLi").hasClass("layui-hide")) $("#ProDLLTabLi").addClass("layui-hide");
        }
        topToolBar.init({ sectionId: me.params.sectionID, sectionCName: me.params.sectionCName, userId: me.config.userID, TABMODULEID: me.params.TABMODULEID });
        bottomToolBar.init();
        me.initStatusBar();
        me.initStatusDesc();
        me.initDateListeners();
        me.initModuleConfig(loading);
        images.render({ testFormRecord: null, isReadOnly: me.config.isReadOnly, sectionID: me.params.sectionID });//结果图片
        me.initListeners();
    };
    //获得参数
    app.getParams = function () {
        var me = this,
            params = uxutil.params.get();
        me.params = $.extend({}, me.params, params);
    };
    //初始化配置项
    app.initModuleConfig = function (loading) {
        var me = this;
        if (!MODULEJSONOBJ) return;
        //初始化表单
        if (MODULEJSONOBJ["FormCode"])
            $.each(MODULEJSONOBJ["FormCode"], function (i, item) {
                if (item["code"].indexOf("SampleForm") != -1) {
                    uxbase.CONFIGITEM.initFormConfig([{
                        Code: item["code"], IsClear: false, NotHandle: true, Elem: '#sampleForm', CallBack: function (list) {
                            var htmlstr = [];
                            $.each(list, function (a, itemA) {
                                htmlstr.push('<div class="layui-form-item' + (String(itemA["BModuleFormControlList_IsDisplay"]) == "true" ? "" : " layui-hide") +'"><label class="layui-form-label">' + itemA["BModuleFormControlList_Label"] + '</label>' + me.sampleFormDom[itemA["BModuleFormControlList_MapField"]].join('') + '</div>');
                            });
                            $("#sampleForm").prepend(htmlstr.join(''));
                            $.each(list, function (b, itemB) {
                                if ($("#sampleForm :input[name=" + itemB["BModuleFormControlList_MapField"] + "]").hasClass("layui-disabled") && String(itemB["BModuleFormControlList_IsReadOnly"]) != "true") {
                                    $("#sampleForm :input[name=" + itemB["BModuleFormControlList_MapField"] + "]").prop("disabled", false).removeClass("layui-disabled");
                                } else if (!$("#sampleForm :input[name=" + itemB["BModuleFormControlList_MapField"] + "]").hasClass("layui-disabled") && String(itemB["BModuleFormControlList_IsReadOnly"]) == "true") {
                                    $("#sampleForm :input[name=" + itemB["BModuleFormControlList_MapField"] + "]").prop("disabled", true).addClass("layui-disabled").addClass("default_readonly");//default_readonly 用于info
                                }
                                
                            });
                            info.init({ sectionId: me.params.sectionID, userId: me.config.userID });
                            form.render();
                        }
                    }]);
                }
            });
        //初始化列表
        if (MODULEJSONOBJ["GridCode"]) {
            $.each(MODULEJSONOBJ["GridCode"], function (i, item) {
                if (item["code"].indexOf("SampleTable") != -1) {
                    uxbase.CONFIGITEM.initGridConfig([{
                        Code: item["code"], Elem: '#SampleListTable', CallBack: function (list) {
                            me.initSampleListTable(loading, list);
                        }
                    }]);
                } else if (item["code"].indexOf("SampleResultTable") != -1) {
                    SampleResultTable.config.GridCode = item["code"];
                }
            });
        }
    };
    //监听
    app.initListeners = function () {
        var me = this,
            iTime = null;
        // 窗体大小改变时，调整高度显示
        $(window).resize(function () {
            clearTimeout(iTime);
            iTime = setTimeout(function () {
                me.setGridNumber();
                me.setDomHeight();
            }, 500);
        });
        //刷新样本单列表
        layui.onevent('common', 'refreshTestFormList', function (data) {
            var page = null;
            if (data["page"]) page = data["page"];
            me.onSearch(page);
        });
        //刷新样本单列表当前行
        layui.onevent('common', 'refreshTestFormListRecord', function (data) {
            var id = data["id"];
            if (!id) return;
            me.onChangeRecodeDataById("LisTestForm_Id", id, function () {
                //保存并新增状态
                if (me.config.SaveAndAddClick) me.onDownAdd();
            });
        });
        //刷新 -- 重置排序 从第一页开始
        layui.onevent('topToolBar', 'onSearch', function (data) {
            var tableIns = me.config.SampleListTableIns;
            me.onSearch(1, null, tableIns.config.initSort, tableIns.config.defaultSort);
        });
        //新增
        layui.onevent('topToolBar', 'onAdd', function (data) {
            me.onAddClick();
        });
        //保存
        layui.onevent('topToolBar', 'onSave', function (data) {
            me.onSaveClick();
        });
        //保存并新增
        layui.onevent('topToolBar', 'onSaveAndAdd', function (data) {
            me.onSaveAndAddClick();
        });
        //仪器上传消息刷新处理
        layui.onevent('topToolBar', 'onEquipResultMsgRefreshHandle', function (data) {
            var list = data["data"] || [];
            me.onEquipResultMsgRefreshHandle(list);
        });
        //检验单信息新增保存成功
        layui.onevent('info', 'add', function (data) {
            var id = data["id"];
            //根据检验单id刷新检验单列表
            me.onSearchByTestFormId(id);
            //保存并新增状态
            //if (me.config.SaveAndAddClick) me.onDownAdd();
        });
        //更新底部检验者审核者信息
        layui.onevent('bottomToolBar', 'refreshbuttomToolBar', function () {
            bottomToolBar.onConfigChange();
        });
        //更新底部项目总数
        layui.onevent('bottomToolBar', 'refreshbottomItemTotal', function (num) {
            bottomToolBar.updateItemTotal(num);
        });
        //更新检验结果列表
        layui.onevent('SampleResultTable', 'onSearch', function () {
            if (!$("#SampleResultListTabLi").hasClass("layui-hide")) {
                for (var i = 0; i < me.config.isLoadTabArr.length; i++) {
                    if (me.config.isLoadTabArr[i].index == me.config.SampleResultTabIndex) {
                        me.config.isLoadTabArr.splice(i, 1);
                        break;
                    }
                }
            }
        });
        //监听键盘按下事件
        $(document).keydown(function (event) {
            switch (event.keyCode) {
                case 13://回车 -- 样本号
                    //判断焦点是否在该输入框
                    if (document.activeElement == document.getElementById("FieldValue")) me.onSearch(1);//查询
                    //核收
                    if (document.activeElement == document.getElementById("receiveValue")) { 
                        var value = $("#receiveValue").val();
                        me.onReceive('', value);
                    }
                    break;
                case 112://F1 -- 新增
                    me.preventDefault();
                    me.onAddClick();
                    break;
                case 113://F2 -- 保存
                    me.preventDefault();
                    me.onSaveClick();
                    break;
                case 114://F3 -- 保存并新增
                    me.preventDefault();
                    me.onSaveAndAddClick();
                    break;
                case 38://上键
                    me.preventDefault();
                    if (document.activeElement.tagName != 'BUTTON' || document.activeElement.tagName != 'INPUT') {
                        var index = $("#SampleListTable+div .layui-table-body table.layui-table tbody tr.layui-table-click").index();
                        if (index <= 0) return;
                        var prevElem = $("#SampleListTable+div .layui-table-body table.layui-table tbody tr").eq((index - 1));
                        if (prevElem.length > 0) {
                            prevElem.click();
                            document.querySelector("#SampleListTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                        }
                    }
                    break;
                case 40://下键
                    me.preventDefault();
                    if (document.activeElement.tagName != 'BUTTON' || document.activeElement.tagName != 'INPUT') {
                        var index = $("#SampleListTable+div .layui-table-body table.layui-table tbody tr.layui-table-click").index();
                        if (index < 0) return;
                        var nextElem = $("#SampleListTable+div .layui-table-body table.layui-table tbody tr").eq((index + 1));
                        if (nextElem.length > 0) {
                            nextElem.click();
                            document.querySelector("#SampleListTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + (index+2) + ")").scrollIntoView(false, { behavior: "smooth" });
                        }
                    }
                    break;
            }
        });
        //监听时间范围按钮
        me.dateRangeBtnListeners();
        //监听状态复选框选中
        me.StatusBarListeners();
        //监听状态说明中的其他按钮
        $("#otherStatusDesc").on('click', function () {
            layer.open({
                type: 1,
                title: '其他-状态说明',
                content: me.config.otherStatusDescHtml
            });
        });
        //样本单列表查询按钮
        $("#SampleListSearch").on('click', function () {
            me.onSearch(1);
        });
        //核收按钮
        $("#SampleListSearch2").on('click', function () {
            var value = $("#receiveValue").val();
            me.onReceive('',value);
        });
        //样本单列表底部查询按钮
        $("#SampleListSearch3").on('click', function () {
            me.onSearch(1);
        });
        //样本单列表底部模糊匹配复选
        form.on('checkbox(isLikeSearch)', function (data) {
            me.onSearch(1);
        });
        //监听样本单列表中tips存在
        var tipHandleTime = null;//tip定时器
        var tipIndex = null;//tip弹出层index
        //鼠标停留弹出层提示
        $("#SampleListTableBox").on('mouseover', '.tips', function () {
            var that = this,
                tips = $(that).attr('data-title');
            tipHandleTime = setTimeout(function () {
                tipIndex = layer.tips(tips, that, {
                    tips: [3, '#fff'],
                    //area:'260px',
                    area: '400px',
                    skin: 'myTip', //自定义class名称
                    time: null
                });
            }, 1000);
        });
        //鼠标移除弹出层关闭
        $("#SampleListTableBox").on('mouseout', '.tips', function () {
            clearTimeout(tipHandleTime);
            if (tipIndex) layer.close(tipIndex);
        });
        //icon 前存在icon 则点击该icon 等同于点击input
        $(document).on('click', 'input.layui-input+.layui-icon', function () {
            if (!$(this).hasClass("myDate")) {
                $(this).prev('input.layui-input')[0].click();
                return false;//不加的话 不能弹出
            }
        });
        //监听+ 弹出短语
        $("#sampleFormBox").on("click",'input.myPhrase+i.layui-icon', function () {
            var elemID = $(this).prev().attr("id"),
                value = $(this).prev().val(),
                TypeCode = $(this).prev().attr("data-typecode"),
                TypeName = $(this).prev().attr("data-typename");
            me.openPhrase(elemID, value, TypeCode, TypeName);
        });
        //样本结果页签
        element.on('tab(ResultTab)', function (obj) {
            me.config.currTabIndex = obj.index;//记录当前页签
            me.onResultTabHandle(obj.index);
        });
        //监听样本单列表单击
        table.on('row(SampleListTable)', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj["data"]);
            //自动新增处理
            var isAutoAdd = $("#isAutoAdd").prop('checked');
            if (!isAutoAdd) $("#isAutoAddInput").val(obj["data"]["LisTestForm_GSampleNo"]);
            //延迟处理
            clearTimeout(me.config.iTime);
            me.config.iTime = setTimeout(function () {
                //加载页签记录清空
                me.config.isLoadTabArr = [];
                //结果页签处理
                me.onResultTabHandle();
                //检验单信息处理
                var StatusID = obj["data"]["LisTestForm_MainStatusID"];
                if (me.config.isReadOnly || StatusID != 0) {
                    me.config.sampleStatus = 'show';
                    info.isShow([obj["data"]]);
                } else {
                    if (me.config.isLoadSampleInfo) {
                        me.config.sampleStatus = 'edit';
                        info.isEdit([obj["data"]]);
                    } else {
                        me.config.isLoadSampleInfo = true;
                        //如果之前是新增状态
                        if (me.config.sampleStatus == 'add') SampleResultTable.isAdd();
                    }
                }
                //保存并新增
                if (me.config.SaveAndAddClick || me.config.ReceiveAction) me.onDownAdd();
                //更新底部历史例数
                bottomToolBar.updateHistoryTotal(obj["data"]["LisTestForm_IExamine"]);
                //功能按钮
                topToolBar.testFormRecordChange([obj["data"]]);
            }, 300);
        });
        //监听样本单列表排序
        table.on('sort(SampleListTable)', function (obj) {
            var tableIns = me.config.SampleListTableIns,
                instance = tableIns.config.instance,
                field = obj.field, //当前排序的字段名
                type = obj.type, //当前排序类型：desc（降序）、asc（升序）、null（空对象，置为升序）
                url = me.getSampleListTableUrl(),
                sort = [];

            if (type == null) {
                type = 'asc';
                instance.sortKey["sort"] = type;
            }

            if (field == "LisTestForm_GTestDate") {
                //点击检验日期:
                //顺序 检验日期顺序 + 样本号顺序  更改为 检验日期顺序 + 样本号顺序
                //逆序 检验日期逆序 + 样本号逆序  更改为 检验日期逆序 + 样本号顺序
                sort = [{ "property": "LisTestForm_GTestDate", "direction": type }, { "property": "LisTestForm_GSampleNoForOrder", "direction": 'asc' }];
                tableIns.config.GTestDateSortType = type;
            } else if (field == "LisTestForm_GSampleNoForOrder") {
                //点击样本号：
                //顺序 检验日期(原本的排序保持) + 样本号顺序
                //逆序 检验日期(原本的排序保持) + 样本号逆序
                sort = [{ "property": "LisTestForm_GTestDate", "direction": tableIns.config.GTestDateSortType }, { "property": "LisTestForm_GSampleNoForOrder", "direction": type }];
            } else {
                //其他 则直接追加
                sort = [{ "property": field, "direction": type }];
            }
            tableIns.config.nowSort = sort;//记录当前排序
            url += "&sort=" + JSON.stringify(sort);
            me.onSearch(null, url);
        });
        //监听自动新增checkbox
        form.on('checkbox(isAutoAdd)', function (data) {
            var checked = data.elem.checked;
            if (checked) {
                $("#isAutoAddInput").val('');
                $("#isAutoAddInput").attr('placeholder', '自动新增');
                //自动新增隐藏核收后向下
                if (!$("#receiveDownBox").hasClass("layui-hide")) $("#receiveDownBox").addClass("layui-hide");
            } else {
                if (me.config.checkRowData.length > 0) $("#isAutoAddInput").val($("#LisTestForm_GSampleNo").val());
                $("#isAutoAddInput").attr('placeholder', '');
                if ($("#receiveDownBox").hasClass("layui-hide")) $("#receiveDownBox").removeClass("layui-hide");
            }
        });
        //监听样本单信息是否隐藏复选框
        form.on('checkbox(testFormInfo)', function (data) {
            var checked = data.elem.checked;
            if (checked) {//显示
                if ($("#testFormInfo").hasClass("layui-hide")) {
                    $("#testFormInfo").removeClass("layui-hide");
                    $("#resultTabBox").attr("class", "layui-col-md8 layui-col-xs12");
                }
            } else {//隐藏
                if (!$("#testFormInfo").hasClass("layui-hide")) {
                    $("#testFormInfo").addClass("layui-hide");
                    $("#resultTabBox").attr("class", "layui-col-md12 layui-col-xs12");
                }
            }
        });
        //监听图形结果是否隐藏复选框
        form.on('checkbox(imageInfo)', function (data) {
            var checked = data.elem.checked,
                testFormRecord = me.config.checkRowData.length > 0 ? me.config.checkRowData[0] : null,//当前选中行
                isReadOnly = me.config.isReadOnly || false,//是否只读模式
                sectionId = me.params.sectionID;//当前小组
            if (checked) {//显示
                images.search({ testFormRecord: testFormRecord, isReadOnly: isReadOnly, sectionID: sectionId });//结果图片
                if ($("#imageInfo").hasClass("layui-hide")) {
                    $("#imageInfo").removeClass("layui-hide");
                    $("#SampleResultTableBox").attr("class", "layui-col-md9 layui-col-xs12");
                }
            } else {//隐藏
                if (!$("#imageInfo").hasClass("layui-hide")) {
                    $("#imageInfo").addClass("layui-hide");
                    $("#SampleResultTableBox").attr("class", "layui-col-md12 layui-col-xs12");
                }
            }
        });
    };
    //设置dom元素高度
    app.setDomHeight = function () {
        var me = this,
            height = parseFloat($("#baseDomHeight").css("height")) - 35 + 'px',
            imagesContentHeight = $(window).height() - 233 + "px",
            ProEditorHeight = $(window).height() - 110 + "px";
        $("#sampleFormBox").css("height", height);
        $("#resultTabBox").css("height", height);
        if ($("#addImageBox").hasClass("layui-hide")) imagesContentHeight = $(window).height() - 138 + "px";
        $("#imagesContent").css("height", imagesContentHeight);
        $("#ProEditor").css("height", ProEditorHeight);
    }
    //结果页签处理
    app.onResultTabHandle = function (currTabIndex) {
        var me = this,
            isLoad = false,//是否已加载过
            currTabIndex = currTabIndex || me.config.currTabIndex,//当前显示页签下标
            testFormRecord = me.config.checkRowData.length > 0 ? me.config.checkRowData[0] : null,//当前选中行
            isReadOnly = me.config.isReadOnly || false,//是否只读模式
            sectionId = me.params.sectionID,//当前小组id
            sectionCName = me.params.sectionCName;//当前小组名称
        //判断是否加载过
        for (var i = 0; i < me.config.isLoadTabArr.length; i++) {
            if (me.config.isLoadTabArr[i].index == currTabIndex) {
                isLoad = true;
                break;
            }
        }
        switch (currTabIndex) {
            case 0://大文本
                if (!isLoad) ProEditor.render({ domId: 'ProEditor', sectionid: sectionId, testformrecord: testFormRecord });
                break;
            case 1://检验结果
                if (!isLoad) {
                    SampleResultTable.render({ elem: '#SampleResultTable', title: '检验结果项目列表', id: 'SampleResultTable', defaultParams: { testFormRecord: testFormRecord, isReadOnly: isReadOnly, sectionID: sectionId, sectionCName: sectionCName } });
                    //结果图片
                    if (!$("#imageInfo").hasClass("layui-hide")) images.search({ testFormRecord: testFormRecord, isReadOnly: isReadOnly, sectionID: sectionId });
                }
                break;
            case 2://历史对比
                if (!isLoad) history.init(testFormRecord, isReadOnly, sectionId);
                break;
            case 3://相关医嘱报告
                if (!isLoad) report.init(testFormRecord, isReadOnly, sectionId);
                break;
            case 4://仪器结果
                if (!isLoad) equipResult.init(testFormRecord, isReadOnly, sectionId);
                break;
            case 5://重提仪器结果
                if (!isLoad) extrctEquipResult.init(testFormRecord, isReadOnly, sectionId);
                break;
            default:
                break;
        }
        //没有加载过 则加入到已加载中
        if (!isLoad) me.config.isLoadTabArr.push({ index: currTabIndex });
    };
    //新增
    app.onAddClick = function () {
        var me = this;
        if (!me.params.sectionID) return;
        if (me.config.isReadOnly) {
            uxbase.MSG.onWarn("当前状态为只读，无法新增!");
            return;
        }
        me.config.sampleStatus = 'add';
        //检验单信息+结果信息置空
        info.isAdd();
        SampleResultTable.isAdd();
    };
    //保存
    app.onSaveClick = function () {
        var me = this;
        //保存检验单和项目结果信息
        me.onSave();
    };
    //保存并新增
    app.onSaveAndAddClick = function () {
        var me = this;
        if (me.config.SaveAndAddClick) return;
        //保存并新增标记
        me.config.SaveAndAddClick = true;
        //保存检验单和项目结果信息
        me.onSave();
    };
    //保存后向下+ 的处理
    app.onDownAdd = function () {
        var me = this,
            checkRowData = me.config.checkRowData;
        if (checkRowData.length == 0) return;
        //获得下一个样本号
        me.getNextSampleNoBySampleNo(checkRowData[0]["LisTestForm_GSampleNo"], function (NextSampleNo, loadIndex) {
            if (!NextSampleNo) {
                if (loadIndex) layer.close(loadIndex);
                return;
            }
            //是否找到并选中了
            var GTestDate = uxutil.date.toString(checkRowData[0]["LisTestForm_GTestDate"],true),
                IsCheckSuccess = me.onCheckBySampleNoAndGTestDate(NextSampleNo, GTestDate);
            if (IsCheckSuccess) {
                if (loadIndex) layer.close(loadIndex);
                //保存并新增标记
                me.config.SaveAndAddClick = false;
                //核收后向下标记
                me.config.ReceiveAction = false;
            } else {
                //没找到则请求服务
                //查询下一个样本号数据条件
                var where = [
                    "SectionID='" + me.params.sectionID + "'",
                    "GSampleNo='" + NextSampleNo + "'",
                    "GTestDate='" + GTestDate + "'"
                ];
                //当前界面查询条件
                var oldWhere = me.getWhere() || false;
                if (oldWhere instanceof Array == false) return oldWhere;
                me.onQueryLisTestFormCurPageByWhere(where.join(' and '), oldWhere.join(' and '), { "SampleNo": NextSampleNo, "GTestDate": GTestDate }, loadIndex);
            }
        });
        
    };
    //在当前页查找是否存在该样本号+检验日期的数据 存在则选中返回true 不存在返回false
    app.onCheckBySampleNoAndGTestDate = function (SampleNo, GTestDate) {
        var me = this,
            SampleNo = SampleNo || null,
            GTestDate = GTestDate || null,
            tableid = me.config.SampleListTableIns.config.id,
            tableCache = table.cache[tableid],
            index = null;
        if (!SampleNo || !GTestDate) return false;
        //在当前页查找 存在则选中 不存在则请求服务 返回数据不为空则刷新列表选中下一个样本号数据 仍不存在则直接新增  （作废样本不包含）
        $.each(tableCache, function (i, item) {
            //查询样本号 同一天 并且样本号=样本号
            if (uxutil.date.toString(item["LisTestForm_GTestDate"], true) == GTestDate && item["LisTestForm_GSampleNo"] == SampleNo && item["LisTestForm_StatusID"] != -2) {
                index = i;
                return false;
            }
        });
        if (index != null) {
            $("#" + tableid + "+div .layui-table-body table.layui-table tbody tr:eq(" + index + ")")[0].click();
            document.querySelector("#" + tableid + "+div .layui-table-body table.layui-table tbody tr:nth-child(" + (index+1) + ")").scrollIntoView(false, { behavior: "smooth" });
            return true;
        } else {
            return false;
        }
    };
    //根据样本号获取下一个样本号
    app.getNextSampleNoBySampleNo = function (SampleNo,callback) {
        var me = this,
            url = me.url.onQueryNextSampleNoByCurSampleNo,
            SampleNo = SampleNo || null,
            NextSampleNo = false;
        if (!SampleNo) return false;
        //保存到后台
        var load = layer.load();
        uxutil.server.ajax({
            url: url,
            //async:false,
            type: 'post',
            data: JSON.stringify({ curSampleNo: SampleNo })
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) NextSampleNo = res.ResultDataValue
            }
            callback && callback(NextSampleNo, load);
        });
    };
    //根据条件查询并返回该页数据
    app.onQueryLisTestFormCurPageByWhere = function (where, oldWhere, NextSampleNoInfo, loadIndex) {
        var me = this,
            url = me.url.onQueryLisTestFormCurPageBySampleNo,
            where = where || null,
            oldWhere = oldWhere || null,
            loadIndex = loadIndex || null,
            NextSampleNoInfo = NextSampleNoInfo || null,
            tableIns = me.config.SampleListTableIns,
            instance = tableIns.config.instance,
            limit = $("#" + tableIns.config.id + "+div").find('.layui-table-page').find("select").val() || tableIns.config.limit || 10;
        
        if (!where) return;
        url += "?isPlanish=true";
        url += "&sort=" + JSON.stringify(tableIns.config.nowSort);
        url += '&fields=' + me.getStoreFields(true).join(',');
        url += '&where=' + where;
        url += '&oldWhere=' + oldWhere;
        uxutil.server.ajax({
            url: url.indexOf("limit" == -1) ? url + "&limit=" + limit : url//添加limit参数
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    me.config.NextSampleNoInfo = NextSampleNoInfo;
                    var currData = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                    //该条数据所在页码
                    var page = currData.page;
                    //数据
                    var list = currData.list;
                    //重载表格
                    if (table.cache[instance.key]) {
                        tableIns.reload({
                            url: url,
                            //data: list, -- 使用data无法加载数据 所以调用了两次服务
                            height: 'full-205',//不写height 高度会消失
                            //initSort: initSort,//记录初始排序，如果不设的话，将无法标记表头的排序状态
                            page: {
                                curr: page //重新从第 page 页开始
                            },
                            where: {
                                t: new Date().getTime()
                            }
                        });
                    }
                } else {
                    me.onAddClick();
                    //保存并新增标记
                    me.config.SaveAndAddClick = false;
                    //核收后向下标记
                    me.config.ReceiveAction = false;
                }
            } else {
                uxbase.MSG.onError(res.msg);
            }
            if (loadIndex) layer.close(loadIndex);
        });

    };
    //保存检验单和项目结果信息
    app.onSave = function () {
        var me = this,
            testFormRecord = me.config.checkRowData,//当前行数据
            status = me.config.sampleStatus;//检验单操作状态

        if (!me.params.sectionID) {
            uxbase.MSG.onWarn("当前小组ID为空，无法保存!");
            return;
        }
        //根据操作状态 执行相关操作
        if (status == 'add') {
            info.onAddSave(function (testformid) {
                SampleResultTable.onAddSave(testformid);
            });
        } else if (status == 'edit') {
            var infoParams = info.getEditSaveParams() || false;
            var resultParams = SampleResultTable.getEditSaveParams() || false;
            if (infoParams && resultParams) me.onEditLisTestFormAndItem(infoParams, resultParams);
        } else if (status == 'show') {
            //保存时需提示什么状态无法保存
            var status = "",
                MainStatusID = (testFormRecord && testFormRecord.length > 0) ? testFormRecord[0]["LisTestForm_MainStatusID"] : null;
            for (var i = 0; i < me.config.statusList.length; i++) {
                if (MainStatusID == me.config.statusList[i].id) {
                    status = me.config.statusList[i].name;
                    break;
                }
            }
            if (status == "") 
                uxbase.MSG.onWarn("当前检验单状态只允许查看，无法保存!");
            else
                uxbase.MSG.onWarn("当前检验单状态只允许查看，无法保存,该检验单状态为：" + status);
        } else {
            uxbase.MSG.onWarn("没有需要保存信息!");
        }
    };
    //编辑检验单和项目结果信息
    app.onEditLisTestFormAndItem = function (infoParams, resultParams) {
        var me = this,
            url = me.url.onEditLisTestFormAndItemUrl,
            infoParams = infoParams,
            resultParams = resultParams,
            params = $.extend({}, infoParams, resultParams);

        var load = layer.load();
        //保存到后台
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify(params)
        }, function (res) {
            if (res.success) {
                uxbase.MSG.onSuccess("保存成功!");
                var id = me.config.checkRowData[0]["LisTestForm_Id"];
                if (!id) {
                    if (load) layer.close(load);
                    return;
                } 
                me.onChangeRecodeDataById("LisTestForm_Id", id, function () {
                    //保存并新增状态
                    if (me.config.SaveAndAddClick) {
                        me.onDownAdd();
                        if (load) layer.close(load);
                    }
                });
            } else {
                if (load) layer.close(load);
                uxbase.MSG.onError(res.ErrorInfo);
            }
        });
    };
    //初始化状态栏
    app.initStatusBar = function () {
        var me = this,
            html = "";
        $.each(me.STATUS_LIST, function (i, item) {
            html += '<div class="layui-inline">'+
                '<input type="checkbox" name="' + item["name"] + '" data-index=' + i + ' lay-filter="' + item["name"] + '" title="<b style=\'padding:0 2px;color:#fff;background-color:' + item["color"] + ';font-weight:normal;vertical-align:baseline;\'>' + item["iconText"] + '</b>' + item["text"] + '" lay-skin="primary" ' + (item["checked"] ? 'checked' : '')+' >'+
                '</div >';
        });
        $("#StatusBar").html(html);
        form.render('checkbox');
    };
    //监听时间范围按钮
    app.dateRangeBtnListeners = function () {
        var me = this;
        $("#dateRangeBtn").on('click', 'button', function () {
            var value = $(this).attr('data-value');
            if (isNaN(value)) { // 不是数字则直接置为空 查全部
                $("#DateValue").val("");
            } else {
                var today = new Date(),
                    startDate = uxutil.date.toString(uxutil.date.getNextDate(today, value), true),
                    endDate = uxutil.date.toString(today, true);
                $("#DateValue").val(startDate + ' - ' + endDate);
            }
            me.onSearch(1);
        });
    };
    //监听状态栏
    app.StatusBarListeners = function () {
        var me = this;
        $.each(me.STATUS_LIST, function (i,item) {
            form.on('checkbox(' + item["name"]+')', function (data) {
                me.onSearch(1);
            });
        });
    };
    //初始化状态说明
    app.initStatusDesc = function () {
        var me = this,
            mainHtml = "说明:",
            otherHtml = "";
        $.each(me.ColorMap, function (i, item) {
            if (item["type"] != 'main' || item["text"] == "智能审核失败") return true;
            if (item["text"] == "智能审核通过") {
                var fail = me.ColorMap["智能审核失败"];
                mainHtml += '<span style="color:' + item["color"] + ';background:' + item["background"] + ';border:' + item["border"] + ';padding:' + item["padding"] + ';">' + item["iconText"] + '</span> <span style="color:' + fail["color"] + ';background:' + fail["background"] + ';border:' + fail["border"] + ';padding:' + fail["padding"] + ';">' + fail["iconText"] + '</span>' + item["text"] + '/失败 ';
                return true;
            }
            mainHtml += '<span style="color:' + item["color"] + ';background:' + item["background"] + ';border:' + item["border"] + ';padding:' + item["padding"] +';">' + item["iconText"]+'</span>' + item["text"]+' ';
        });
        mainHtml += '<button type="button" id="otherStatusDesc" class="layui-btn layui-btn-xs" style="height:18px;line-height:18px;vertical-align:baseline;">其他</button>';
        $("#StatusDesc").css({ "font-size": '10px', "overflow": "hidden", "white-space": "nowrap","text-overflow": "ellipsis"});
        $("#StatusDesc").html(mainHtml);
        //其他状态说明
        $.each(me.ColorMap, function (i, item) {
            if (item["type"] != 'other') return true;
            var marRight = 5;
            if (item["iconText"].indexOf("icon") != -1) marRight = 3;
            otherHtml += '<div style="padding-bottom:5px;"><span style="margin-right:' + marRight+'px;color:' + item["color"] + ';background:' + item["background"] + ';border:' + item["border"] + ';padding:' + item["padding"] +';">' + item["iconText"] + '</span>' + item["text"] + '</div>';
        });
        otherHtml = '<div style="padding: 5px 10px;">' + otherHtml + '</div>';
        me.config.otherStatusDescHtml = otherHtml;
    };
    //初始化样本单列表
    app.initSampleListTable = function (loading,cols) {
        var me = this,
            cols = cols || [
                { type: 'checkbox', width: 26 },
                //{ type: 'numbers', title: '行号' },
                { field: 'LisTestForm_StatusID', width: 110, title: '状态', sort: false, templet: function (data) { return basicStatus.onStatusRenderer(data); } },
                { field: 'LisTestForm_GTestDate', width: 70, title: '检验日期', sort: true, templet: function (data) { return me.onGTestDateRenderer(data); } },
                { field: 'LisTestForm_CName', width: 70, title: '姓名', sort: true, templet: function (data) { return me.onCNameRenderer(data); } },
                { field: 'LisTestForm_PatNo', width: 80, title: '病历号', sort: true, hide: true },
                { field: 'LisTestForm_GSampleNoForOrder', width: 80, title: '样本号', sort: true, templet: function (data) { return me.onGSampleNoForOrderRenderer(data); } },
                { field: 'LisTestForm_GSampleNo', width: 80, title: '样本号排序', sort: true, hide: true, templet: function (data) { return me.onGSampleNoRenderer(data); } },
                { field: 'LisTestForm_BarCode', width: 80, title: '条码号', sort: true },
                { field: 'LisTestForm_LisPatient_GenderName', width: 60, title: '性别', sort: true },
                { field: 'LisTestForm_GSampleType', width: 80, title: '样本类型', sort: true },
                //以下不显示
                { field: 'LisTestForm_Id', width: 80, title: '主键ID', sort: false, hide: true },
                { field: 'LisTestForm_LBSection_Id', width: 80, title: '小组ID', sort: false, hide: true },
                { field: 'LisTestForm_GSampleTypeID', width: 80, title: '样本类型ID', sort: false, hide: true },
                { field: 'LisTestForm_LisOrderForm_Id', width: 80, title: '医嘱单ID', sort: false, hide: true },
                { field: 'LisTestForm_MainStatusID', width: 80, title: '检验主状态', sort: false, hide: true },
                { field: 'LisTestForm_EquipFormID', width: 80, title: '仪器样本单ID', sort: false, hide: true },
                { field: 'LisTestForm_PrintCount', width: 80, title: '打印次数', sort: false, hide: true },
                { field: 'LisTestForm_CheckTime', width: 80, title: '审核时间', sort: false, hide: true },
                { field: 'LisTestForm_RedoStatus', width: 80, title: '复检', sort: false, hide: true },
                { field: 'LisTestForm_UrgentState', width: 80, title: '急诊', sort: false, hide: true },
                { field: 'LisTestForm_SampleSpecialDesc', width: 80, title: '标注样本', sort: false, hide: true },
                { field: 'LisTestForm_ESendStatus', width: 80, title: '仪器状态', sort: false, hide: true },
                { field: 'LisTestForm_ReportStatusID', width: 80, title: '报告状态', sort: false, hide: true },
                { field: 'LisTestForm_TestAllStatus', width: 80, title: '检验完成', sort: false, hide: true },
                { field: 'LisTestForm_ZFSysCheckStatus', width: 80, title: '只能审核', sort: false, hide: true },
                { field: 'LisTestForm_ZFSysCheckInfo', width: 120, title: '系统判定不通过内容', sort: false, hide: true },
                { field: 'LisTestForm_ConfirmInfo', width: 120, title: '初审说明', sort: false, hide: true },
                { field: 'LisTestForm_AlarmLevel', width: 120, title: '警示级别', sort: false, hide: true },
                { field: 'LisTestForm_AlarmInfo', width: 120, title: '警示提示', sort: false, hide: true },
                { field: 'LisTestForm_FormInfoStatus', width: 80, title: '检验单信息基本完成状态', sort: false, hide: true },
                { field: 'LisTestForm_IExamine', width: 80, title: '历史例数', sort: false, hide: true }
            ];

        //样本单列表列
        me.config.SampleListTableConfig = {
            elem: '#SampleListTable',
            height: 'full-205',
            url: '',
            toolbar: '',
            page: true,
            limit: 100,
            limits: [100, 200, 500, 1000, 1500],
            autoSort: false, //禁用前端自动排序
            initSort: { field: 'LisTestForm_GTestDate', type: 'desc' },//type如果大写的话 不能识别
            defaultSort: [{ "property": "LisTestForm_GTestDate", "direction": "desc" }, { "property": "LisTestForm_GSampleNoForOrder", "direction": "asc" }],//默认排序
            GTestDateSortType: "DESC",//记录检测日期排序
            nowSort: [{ "property": "LisTestForm_GTestDate", "direction": "desc" }, { "property": "LisTestForm_GSampleNoForOrder", "direction": "asc" }],//当前排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [cols],
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                //var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                //是否将url更换为初始查询的服务地址
                if (true) {
                    var initUrl = me.getSampleListTableUrl();
                    //排序不存在
                    if (initUrl && initUrl != 'clear' && initUrl.indexOf("sort=") == -1) me.config.SampleListTableIns ? initUrl += "&sort=" + JSON.stringify(me.config.SampleListTableIns.config.nowSort) : initUrl;
                    if (initUrl && initUrl != 'clear') me.config.SampleListTableIns.config.instance.config.instance.config.url = initUrl;
                }
                if (loading) layer.close(loading);
				//更新底部样本总数
				bottomToolBar.updateSampleTotal(count);
                me.setDomHeight();
                if (count == 0) {
                    //添加初始排序后 不显示无数据文本 手动添加
                    if ($("#SampleListTable+div .layui-table-main .layui-none").length == 0)
                        $("#SampleListTable+div .layui-table-main").append('<div class="layui-none">暂无相关数据</div>');
                    //更新底部历史例数
                    bottomToolBar.updateHistoryTotal(0);
                    me.config.checkRowData = [];
                    //加载页签记录清空
                    me.config.isLoadTabArr = [];
                    //结果页签处理
                    me.onResultTabHandle();
                    if (me.config.isReadOnly) {
                        me.config.sampleStatus = 'show';
                        info.isShow();
                    } else {
                        me.config.sampleStatus = 'add';
                        if (me.config.isLoadSampleInfo) {
                            info.isAdd();
                            me.config.isLoadSampleInfo = true;
                        }
                    }
                    //清除保存并新增标记
                    me.config.SaveAndAddClick = false;
                    //清除核收后向下标记
                    me.config.ReceiveAction = false;
                    //头部工具栏更新信息
                    topToolBar.testFormRecordChange([]);
                    //样本号置空
                    $("#isAutoAddInput").val('');
                    return;
                }
                if (me.config.positionID != null) {//新增修改定位
                    var flag = false;
                    var index = null;
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i]["LisTestForm_Id"] == me.config.positionID) {
                            flag = true;
                            index = i + 1;
                            break;
                        }
                    }
                    if (flag) {
                        if ($("#SampleListTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")")[0]) {
                            setTimeout(function () {
                                $("#SampleListTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")")[0].click();
                                document.querySelector("#SampleListTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                            }, 0);

                        }
                    } else {
                        if ($("#SampleListTable+div .layui-table-body table.layui-table tbody tr:first-child")[0]) {
                            setTimeout(function () {
                                $("#SampleListTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                            }, 0);
                        }
                        uxbase.MSG.onWarn('该页找不到之前操作数据,无法定位！', { offset: '15px' });
                    }
                    me.config.positionID = null;
                } else if (me.config.NextSampleNoInfo != null) {//保存向下+
                    me.onCheckBySampleNoAndGTestDate(me.config.NextSampleNoInfo["SampleNo"], me.config.NextSampleNoInfo["GTestDate"]);
                    me.config.NextSampleNoInfo = null;
                } else {
                    if ($("#SampleListTable+div .layui-table-body table.layui-table tbody tr:first-child")[0]) {
                        setTimeout(function () {
                            $("#SampleListTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                        }, 0);
                    }
                }
            }
        };
        //赋值url
        me.config.SampleListTableConfig.url = me.getSampleListTableUrl();
        //
        if (me.config.SampleListTableConfig.url == "clear") {
            me.config.SampleListTableConfig.url = "";
            me.config.SampleListTableConfig.data = [];
        }
        //排序不存在 添加默认排序
        if (me.config.SampleListTableConfig.url.indexOf("sort=") == -1 && me.config.SampleListTableConfig.url != "")
            me.config.SampleListTableConfig.url += "&sort=" + JSON.stringify(me.config.SampleListTableConfig.defaultSort);
        //初始化列表
        me.config.SampleListTableIns = table.render(me.config.SampleListTableConfig);
    };
    //样本单列表更新一行数据 -- fields:{ "LisTestForm_Id": '5598045837466289641',"LisTestForm_CName": "123" }, key: "LisTestForm_Id"
    app.updateRowItem = function (fields, key) {
        var me = this,
            that = me.config.SampleListTableIns.config.instance,
            list = table.cache[that.key] || [],
            len = list.length,
            index = null;

        for (var i = 0; i < len; i++) {
            if (list[i][key] == fields[key]) {
                index = i;
                break;
            }
        }

        if (index == null) {//不存在
            return false;
        } else {
            var tr = that.layBody.find('tr[data-index="' + index + '"]'),
                data = list[index],
                cacheData = table.cache[that.key][index];
            //将变化的字段值赋值到data  覆盖原先值
            data = $.extend({}, data, fields);

            fields = fields || {};
            layui.each(fields, function (ind, value) {
                if (ind in data) {
                    var templet, td = tr.children('td[data-field="' + ind + '"]');
                    data[ind] = value;
                    cacheData[ind] = value;
                    that.eachCols(function (i, item2) {
                        if (item2.field == ind && item2.templet) {
                            templet = item2.templet;
                        }
                    });
                    td.children(".layui-table-cell").html(function () {
                        return templet ? function () {
                            return typeof templet === 'function'
                                ? templet(data)
                                : laytpl($(templet).html() || value).render(data)
                        }() : value;
                    }());
                    td.data('content', value);
                }
            });
            if (!me.config.SaveAndAddClick) $(tr).click();//不是保存并新增
            return true;
        }
    };
    //根据ID更新该行数据
    app.onChangeRecodeDataById = function (PKField, id, callback) {
        var me = this,
            url = me.url.selectUrl,
            where = [];

        var PKFieldArr = PKField.split('_');
        if (PKFieldArr.length == 1) {
            where.push(PKFieldArr[0] + "='" + id + "'");
        } else if (PKFieldArr.length == 2) {
            where.push(PKFieldArr[0].toLocaleLowerCase() + "." + PKFieldArr[1] + "='" + id + "'");
        }
        if (where.length > 0) {
            url += '&where=' + where.join(' and ');
        }
        //查询字段
        url += '&fields=' + me.getStoreFields(true).join(',');
        var load = layer.load();
        uxutil.server.ajax({ url: url }, function (data) {
            layer.close(load);
            if (data.success) {
                me.updateRowItem(data.value.list[0], PKField);
                callback && callback(me);
            } else {
                //不做处理
            }
        });
    },
    //获取查询Fields
    app.getStoreFields = function (isString) {
        var me = this,
            tableIns = me.config.SampleListTableIns,
            columns = tableIns ? (tableIns.config.cols[0] || []) : (me.config.SampleListTableConfig.cols[0] || []),
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
    //检验日期处理
    app.onGTestDateRenderer = function (record) {
        var me = this,
            value = record["LisTestForm_GTestDate"],
            v = uxutil.date.toString(value, true) || '';

        return v;
    };
    //姓名处理
    app.onCNameRenderer = function (record) {
        var me = this,
            v = record["LisTestForm_CName"];

        return v;
    };
    //样本号处理
    app.onGSampleNoForOrderRenderer = function (record) {
        var me = this,
            v = record["LisTestForm_GSampleNo"];

        return  v;
    };
    //样本号顺序处理
    app.onGSampleNoRenderer = function (record) {
        var me = this,
            v = record['LisTestForm_GSampleNoForOrder'];
        return v;
    };
    //查询
    app.onSearch = function (page, url, initSort, sortList) {
        var me = this,
            tableIns = me.config.SampleListTableIns,
            instance = tableIns.config.instance,
            page = page || instance.layPage.find('.layui-laypage-curr>em:last-child').html() || 1,
            initSort = initSort || instance.sortKey || tableIns.config.initSort,
            sortList = sortList || tableIns.config.nowSort,
            url = url || me.getSampleListTableUrl();
        if (!url) return;
        if (url == "clear") {
            if (table.cache[instance.key]) {
                tableIns.reload({
                    url: '',
                    data:[],
                    height: 'full-205',//不写height 高度会消失
                    initSort: initSort,//记录初始排序，如果不设的话，将无法标记表头的排序状态
                    page: {
                        curr: 1 //重新从第 page 页开始
                    },
                    where: {
                        t: new Date().getTime()
                    }
                });
                return;
            }
        }
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
                height: 'full-205',//不写height 高度会消失
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
    //根据检验单id查询
    app.onSearchByTestFormId = function (id) {
        var me = this,
            tableIns = me.config.SampleListTableIns,
            instance = tableIns.config.instance,
            limit = $("#" + tableIns.config.id + "+div").find('.layui-table-page').find("select").val() || tableIns.config.limit || 10,
            id = id || null,
            url = me.getSampleListTableUrl(id);
        if (!url) return;
        //添加定位
        me.config.positionID = id;
        //排序不存在 添加默认排序
        if (url.indexOf("sort=") == -1) url += "&sort=" + JSON.stringify(tableIns.config.nowSort);
        //获得数据
        uxutil.server.ajax({
            url: url.indexOf("limit" == -1) ? url + "&limit=" + limit : url//添加limit参数
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var currData = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                    //var currData = JSON.parse(res.ResultDataValue);
                    //该条数据所在页码
                    var page = currData.page;
                    //数据
                    var list = currData.list;
                    //重载表格
                    if (table.cache[instance.key]) {
                        tableIns.reload({
                            url: url,
                            //data: list, -- 使用data无法加载数据 所以调用了两次服务
                            height: 'full-205',//不写height 高度会消失
                            //initSort: initSort,//记录初始排序，如果不设的话，将无法标记表头的排序状态
                            page: {
                                curr: page //重新从第 page 页开始
                            },
                            where: {
                                t: new Date().getTime()
                            }
                        });
                    }
                }
            }
        });
    };
    //获得检验单列表查询地址
    app.getSampleListTableUrl = function (id) {
        var me = this,
            url = me.url.selectUrl,
            id = id || null;
        //检验单id
        if (id) url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

        var where = me.getWhere() || false;
        if (where instanceof Array == false) return where;

        url += "&where=" + where.join(' and ');
        //查询字段
        url += '&fields=' + me.arrConcatNoRepeat(me.config.SampleListTableCols, me.getStoreFields(true)).join(',');

        return encodeURI(url);
    };
    //获得查询条件
    app.getWhere = function () {
        var me = this,
            dateType = $("#DateType").val(),//查询日期类型
            date = $("#DateValue").val(),//查询日期值
            FieldValueType = $("#FieldValueType").val(),//样本号类型下拉
            FieldValue = $("#FieldValue").val(),//样本号类型下拉对应值
            isLikeSearch = $("#isLikeSearch").prop('checked'),//是否模糊查询
            statusWhere = [],//状态栏条件 or
            where = [];//其他条件 and

        //检验小组
        if (me.params.sectionID) {
            where.push('listestform.LBSection.Id=' + me.params.sectionID);
        } else {
            var LocalSection = me.getLocalSection();
            if (LocalSection)
                where.push('listestform.LBSection.Id in (' + LocalSection + ')');
            else {
                //全部页签 没有任何打开小组 
                uxbase.MSG.onWarn('不存在任何小组，无法查询!');
                return "clear";
            }
        }
        //查询日期
        if (date) {
            //验证日期是否正确
            var msg = [];
            if (date.indexOf(" - ") == -1) {
                msg.push("日期格式不正确!");
            }
            //验证是否都是日期
            var start = date.split(" - ")[0],
                end = date.split(" - ")[1],
                DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/; //判断是否是日期格式
            if (!uxutil.date.isValid(start) || !DATE_FORMAT.test(start) || !uxutil.date.isValid(end) || !DATE_FORMAT.test(end)) {
                msg.push("日期格式不正确!");
            }
            //验证开始日期是否大于结束日期
            if (new Date(start).getTime() > new Date(end).getTime()) {
                msg.push("开始日期不能大于结束日期!");
            }
            //判断日期格式是否正确
            if (me.isDateString(start)) msg.push(me.isDateString(start));
            if (me.isDateString(end)) msg.push(me.isDateString(end));
            if (msg.length > 0) {
                uxbase.MSG.onWarn(msg.join("<br>"));
                return false;
            }
            var startDate = date.split(" - ")[0],
                endDate = uxutil.date.toString(uxutil.date.getNextDate(date.split(" - ")[1], 1), true);
            where.push("listestform." + dateType + ">='" + startDate + "' and listestform." + dateType + "<'" + endDate + "'");
        }
        //查询状态
        $("#StatusBar input[type=checkbox]:checked").each(function (i, item) {
            statusWhere.push(me.STATUS_LIST[$(item).attr('data-index')].where);
        });
        if (statusWhere.length > 0) where.push('(' + statusWhere.join(' or ') + ')');
        //列表底部查询
        if (FieldValue) {
            where.push('listestform.' + FieldValueType + (isLikeSearch ? " like '%" + FieldValue + "%'" : "='" + FieldValue + "'"));
        }

        return where;
    };
    //两个单数组合并去重
    app.arrConcatNoRepeat = function (arr1,arr2) {
        var me = this,
            arr1 = arr1 || [],
            arr2 = arr2 || [];

        for (var i = 0; i < arr2.length; i++) {
            if (arr1.indexOf(arr2[i]) == -1) arr1.push(arr2[i]);
        }

        return arr1;
    };
    //初始化新日期控件
    app.initDate = function () {
        var me = this,
            today = new Date();
        //查询日期
        laydate.render({//存在默认值
            elem: '#DateValue',
            eventElem:'#DateValue+i.layui-icon',
            type: 'date',
            range: true,
            show:true,
            //value: uxutil.date.toString(uxutil.date.getNextDate(today, me.config.searchDays), true) + " - " + uxutil.date.toString(today, true),
            done: function (value, date, endDate) { }
        });
    };
    //监听新日期控件
    app.initDateListeners = function () {
        var me = this,
            today = new Date();
        //赋值日期框
        $("#DateValue").val(uxutil.date.toString(uxutil.date.getNextDate(today, me.config.searchDays), true) + " - " + uxutil.date.toString(today, true));
        //监听日期图标
        $("#DateValue+i.layui-icon").on("click", function () {
            var elemID = $(this).prev().attr("id");
            if ($("#" + elemID).hasClass("layui-disabled")) return false;
            var key = $("#" + elemID).attr("lay-key");
            if ($('#layui-laydate' + key).length > 0) {
                $("#" + elemID).attr("data-type", "date");
            } else {
                $("#" + elemID).attr("data-type", "text");
            }
            var datatype = $("#" + elemID).attr("data-type");
            if (datatype == "text") {
                me.initDate();
                $("#" + elemID).attr("data-type", "date");
            } else {
                $("#" + elemID).attr("data-type", "text");
                var key = $("#" + elemID).attr("lay-key");
                $('#layui-laydate' + key).remove();
            }
        });
        //监听日期input -- 不弹出日期框
        $("#baseDomHeight").on('focus', '#DateValue', function () {
            me.preventDefault();
            layui.stope(window.event);
            return false;
        });
    };
    //弹出短语选择
    app.openPhrase = function (elemID, value, TypeCode, TypeName) {
        var me = this,
            sectionID = me.params.sectionID || null,
            elemID = elemID || null,
            value = value || "",
            //短语表配置
            TypeCode = TypeCode || null,
            TypeName = TypeName || null,
            ObjectType = 1,//针对类型1：小组样本 2：检验项目
            ObjectID = sectionID,
            PhraseType = "SamplePhrase";//枚举
        if (!sectionID) {
            uxbase.MSG.onWarn("小组ID不能为空，请选择小组!");
            return;
        }
        if (!TypeCode) {
            uxbase.MSG.onWarn("缺少TypeCode参数!");
            return;
        }
        if (!TypeName) {
            uxbase.MSG.onWarn("缺少TypeName参数!");
            return;
        }
        parent.layer.open({
            type: 2,
            area: ['600px', '420px'],
            fixed: false,
            maxmin: true,
            title: TypeName,
            content: 'basic/phrase/new/index.html?CName=' + TypeName + '&ObjectType=' + ObjectType + '&ObjectID=' + ObjectID + '&PhraseType=' + PhraseType + '&TypeName=' + TypeName + '&TypeCode=' + TypeCode +'&isAppendValue=1&&ISNEXTLINEADD=1',
            success: function (layero, index) {
                var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                //body.find('#CName').html("当前" + TypeName);
                body.find('#Comment').val(value.replace(/\|/g, "\n")); //使用“|”替换换行符 保存时再将“|”换为换行符保存
                var iframeWin = parent.window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
                iframeWin.externalCallFun(function (v) { $("#" + elemID).val(v.replace(/\n/g, "\|")); });
            },
            //cancel: function (index, layero) {
            //    var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
            //    var val = body.find('#Comment').val();
            //    $("#" + elemID).val(val);
            //    parent.layer.close(index);
            //    return false;
            //}  
        });
    };
    //核收数据
    app.onReceive = function (fieldName, filedVlaue, sectionID, receiveDate, sampleNo) {
        var me = this,
            isAutoAdd = $("#isAutoAdd").prop('checked'),//是否自动新增
            fieldName = fieldName || null,//核收字段名 --为空则默认条码号BarCode核收
            filedVlaue = filedVlaue || null,//核收字段值  --必填
            sectionID = sectionID || me.params.sectionID || null,//小组id --必填
            receiveDate = receiveDate || (!isAutoAdd ? (me.config.checkRowData.length > 0 ? me.config.checkRowData[0]["LisTestForm_GTestDate"] : null) : null),//核收日期 核收到哪个日期 不传默认当天
            sampleNo = sampleNo || $("#isAutoAddInput").val() || null,//指定样本号 可以为空
            url = me.url.onReceiveUrl;

        if (!sectionID) {
            uxbase.MSG.onWarn("请先选择一个小组!");
            return;
        }
        if (!filedVlaue) {
            uxbase.MSG.onWarn("请输入核收条码!");
            return;
        }
        var load = layer.load();
        //保存到后台
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify({fieldName: fieldName,filedVlaue: filedVlaue,sectionID: sectionID,receiveDate: receiveDate,sampleNo: sampleNo})
        }, function (res) {
            layer.close(load);
            //清空核收条码
            $("#receiveValue").val("");
            if (res.success) {
                if (res.ResultDataValue) {
                    //核收后向下选中 并且不是隐藏状态
                    if (!$("#receiveDownBox").hasClass("layui-hide") && $("#receiveDown").prop('checked')) me.config.ReceiveAction = true;
                    //根据检验单id刷新检验单列表
                    me.onSearchByTestFormId(res.ResultDataValue);
                } else {
                    uxbase.MSG.onWarn("条码号【" + filedVlaue + "】,未核收到数据!");
                }
            } else {
                uxbase.MSG.onError("条码号【" + filedVlaue + "】," + res.ErrorInfo);
            }
        });
    };
    //获得local中的小组
    app.getLocalSection = function () {
        var me = this,
            sectionIDs = [];
        var local = uxutil.localStorage.get(me.config.localTotalName, true);
        if (local) {
            if (local[me.config.userID]) {//存在当前等录人记录
                if (local[me.config.userID][me.config.localSectionName] && local[me.config.userID][me.config.localSectionName].length > 0) {//local中存储打开的小组
                    $.each(local[me.config.userID][me.config.localSectionName], function (i, item) {
                        sectionIDs.push(item["Id"]);
                    });
                }
            }
        }
        return sectionIDs.join();
    };
    //阻止默认事件
    app.preventDefault = function () {
        var me = this,
            device = layui.device();
        if (device.ie) {
            window.event.returnValue = false;
        } else {
            window.event.preventDefault();
        }
    };
    //判断浏览器大小方法
    app.screen = function () {
        var me = this,
            width = $(window).width();//获取当前窗口的宽度
        if (width > 1400) {//大屏幕
            return 1;
        } else {
            return 0;
        }
    };
    //判断日期格式是否正确
    //返回值是错误信息, 无错误信息即表示合法日期字符串
    app.isDateString = function(strDate) {
        var strSeparator = "-";   //日期分隔符  
        var strDateArray;
        var intYear;
        var intMonth;
        var intDay;
        var boolLeapYear;
        var ErrorMsg = "";    //出错信息
        strDateArray = strDate.split(strSeparator);

        //没有判断长度,其实2008-8-8也是合理的//strDate.length != 10 || 
        if (strDateArray.length != 3) {
            ErrorMsg += "日期格式必须为: yyyy-MM-dd";
            return ErrorMsg;
        }

        intYear = parseInt(strDateArray[0], 10);
        intMonth = parseInt(strDateArray[1], 10);
        intDay = parseInt(strDateArray[2], 10);

        if (isNaN(intYear) || isNaN(intMonth) || isNaN(intDay)) {
            ErrorMsg += "日期格式错误: 年月日必须为纯数字";
            return ErrorMsg;
        }

        if (intMonth > 12 || intMonth < 1) {
            ErrorMsg += "日期格式错误: 月份必须介于1和12之间";
            return ErrorMsg;
        }

        if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7
            || intMonth == 8 || intMonth == 10 || intMonth == 12)
            && (intDay > 31 || intDay < 1)) {
            ErrorMsg += "日期格式错误: 大月的天数必须介于1到31之间";
            return ErrorMsg;
        }

        if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11)
            && (intDay > 30 || intDay < 1)) {
            ErrorMsg += "日期格式错误: 小月的天数必须介于1到31之间";
            return ErrorMsg;
        }

        if (intMonth == 2) {
            if (intDay < 1) {
                ErrorMsg += "日期格式错误: 日期必须大于或等于1";
                return ErrorMsg;
            }

            boolLeapYear = false;
            if ((intYear % 100) == 0) {
                if ((intYear % 400) == 0)
                    boolLeapYear = true;
            }
            else {
                if ((intYear % 4) == 0)
                    boolLeapYear = true;
            }

            if (boolLeapYear) {
                if (intDay > 29) {
                    ErrorMsg += "日期格式错误: 闰年的2月份天数不能超过29";
                    return ErrorMsg;
                }
            } else {
                if (intDay > 28) {
                    ErrorMsg += "日期格式错误: 非闰年的2月份天数不能超过28";
                    return ErrorMsg;
                }
            }
        }

        return ErrorMsg;
    }
    //根据浏览器大小设置栅格
    app.setGridNumber = function () {
        var me = this,
            number = me.screen(),
            gridNumber = me.config.gridNumber[number];
        //设置栅格
        $("#sampleTableBox").attr("class", gridNumber["sampleTableBox"]);
        $("#contentBox").attr("class", gridNumber["contentBox"]);
        
        if ($("#testFormInfo").hasClass("layui-hide"))
            $("#resultTabBox").attr("class", 'layui-col-md12 layui-col-xs12');
        else {
            $("#testFormInfo").attr("class", gridNumber["testFormInfo"]);
            $("#resultTabBox").attr("class", gridNumber["resultTabBox"]);
        }
    };
    //仪器上传数据刷新处理
    app.onEquipResultMsgRefreshHandle = function (list) {
        var me = this,
            list = list || [],
            record = me.config.checkRowData.length > 0 ? me.config.checkRowData[0] : null,
            formtype = info.params.formtype,
            IsEditData = false;//是否修改过数据
        
        
        if (formtype == 'add') {
            //新增状态 保留检验单信息 刷新检验单列表 保留新增状态
            me.config.isLoadSampleInfo = false;
            me.onSearch();
        } else {//编辑状态和查看状态
            //是否存在选中行 -- 也可以理解成当前样本单列表是否存在数据
            if (record) {
                IsEditData = info.getIsEditDataByCompare();
                if (IsEditData) {//正在编辑（修改过数据）
                    //保留检验单信息 刷新检验单列表 定位到之前选中数据
                    me.config.isLoadSampleInfo = false;
                }
                //没有修改过数据//全部刷新 定位到之前选中数据
                me.onSearchByTestFormId(record["LisTestForm_Id"]);
            } else {//不存在数据情况下
                //直接全部刷新
                me.onSearch(1);
            }
        }
        //console.log(list);
        //console.log(record);
        //console.log(formtype);
        //console.log(IsEditData);
    };

    //调用init
    $(document).ready(function () {
        var task = setInterval(function () {
            if (MODULEJSONOBJ) {
                app.init();
                clearInterval(task);
                task = null;
            }
        },100);
    });
    //暴露接口
    exports(MOD_NAME, app);
});