/**
	@name：功能按钮
	@author：zhangda
	@version 2021-04-26
 */
layui.extend({
    print: 'ux/print'
}).define(['form', 'table', 'uxutil', 'element', 'uxbase','print'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        uxbase = layui.uxbase,
        print = layui.print,
        table = layui.table,
        element = layui.element,
        form = layui.form;
    //每一种权限可能包含多个按钮（一起控制）
    //方案：平台：模块+按钮，角色-模块-按钮；技师站：人员+小组+角色；
    //登录后，根据人员获取小组+角色，实例化按钮；
    var app = {};
    //外部参数
    app.params = {
        //小组ID
        sectionId: null,
        //小组名称
        sectionCName: null,
        //员工ID
        userId: uxutil.cookie.get(uxutil.cookie.map.USERID),
        //员工名
        userName: uxutil.cookie.get(uxutil.cookie.map.USERNAME),
        //当前检验单
        testFormRecord: [],
        //模块ID
        TABMODULEID: null
    };
    //配置
    app.config = {
        LabID: uxutil.cookie.get(uxutil.cookie.map.LABID),
        CVParaCode: "NTestType_TesItem_PanicValue_Para",//危急值参数code
        CVParaMap: {
            NTestType_TesItem_PanicValue_0000: null,//开启危急值判断
            NTestType_TesItem_PanicValue_0001: null,//异常结果是否发送危急值
            NTestType_TesItem_PanicValue_0002: null,//危急值发送时间节点
            NTestType_TesItem_PanicValue_0003: null,//其他时间是否可以发送危急值
            NTestType_TesItem_PanicValue_0004: null,//危急值发送后是否电话通知
        },
        //检验主列表ID
        SampleListTableID:"SampleListTable"

    };
    //操作编码列表
    app.OPER_CODE_LIST = [];
    //功能按钮
    app.BUTTON_LIST = [
        {
            dataid: 'refresh', code: '', text: '刷新', style: '',
            class: 'layui-btn layui-btn-xs',
            befortIcon: ' <i class="layui-icon layui-icon-refresh"></i> ', afterIcon: '',
            callback: function (m) { m.onRefreshClick(); },
            menu: { id: '', list: [] }
        },
        {
            dataid: 'add', code: '1003', text: '新增(F1)', style: '',
            class: 'layui-btn layui-btn-xs',
            befortIcon: '<i class="iconfont">&#xe664;</i>&nbsp;', afterIcon: '',
            callback: function (m) { m.onAddClick(); },
            menu: { id: '', list: [] }
        },
        {
            dataid: 'save', code: '1003', text: '保存(F2)', style: '',
            class: 'layui-btn layui-btn-xs',
            befortIcon: '<i class="iconfont" style="font-weight: bold;">&#xe69d;</i>&nbsp;', afterIcon: '',
            callback: function (m) { m.onSaveClick(); },
            menu: { id: '', list: [] }
        },
        {
            dataid: 'saveAndAdd', code: '1003', text: '保存+(F3)', style: '',
            class: 'layui-btn layui-btn-xs',
            befortIcon: '<i class="iconfont">&#xe8ac;</i>&nbsp;', afterIcon: '',
            callback: function (m) { m.onSaveAndAddClick(); },
            menu: { id: '', list: [] }
        },
        {
            dataid: 'confirm', code: '1001', text: '检验确认', style: '',
            class: 'layui-btn layui-btn-xs layui-btn-normal',
            befortIcon: '<i class="iconfont">&#xe64f;</i>&nbsp;', afterIcon: '',
            callback: function (m) { m.onConfirmClick(); },
            menu: { id: '', list: [] }
        },
        {
            dataid: 'dropDown1', code: '1001', text: '', style: '',
            class: 'layui-btn layui-btn-xs layui-btn-normal drop-down',
            befortIcon: '', afterIcon: '<i class="layui-icon layui-icon-triangle-d" style="vertical-align:middle;"></i>',
            callback: function (m, item) { m.onDropDown(item); },
            menu: {
                id: 'menuDropDown1', class: 'btn_spread_menu', isPositionPrevBtn:true, list: [
                    { dataid: 'unConfirm', text: '取消确认', befortIcon: '<i class="iconfont">&#xe6cc;</i>&nbsp;', callback: function (m) { m.onUnConfirmClick(); } },
                    { dataid: 'confirmSet', text: '检验确认设置', befortIcon: '<i class="iconfont">&#xe615;</i>&nbsp;', callback: function (m) { m.confirmSetClick(0); } }
                ]
            }
        },
        {
            dataid: 'check', code: '1002', text: '审核', style: '',
            class: 'layui-btn layui-btn-xs layui-btn-normal',
            befortIcon: '<i class="iconfont">&#xe62c;</i>&nbsp;', afterIcon: '',
            callback: function (m) { m.onCheckClick(); },
            menu: { id: '', list: [] }
        },
        {
            dataid: 'dropDown2', code: '1002', text: '', style: '',
            class: 'layui-btn layui-btn-xs layui-btn-normal drop-down',
            befortIcon: '', afterIcon: '<i class="layui-icon layui-icon-triangle-d" style="vertical-align:middle;"></i>',
            callback: function (m, item) { m.onDropDown(item); },
            menu: {
                id: 'menuDropDown2', class: 'btn_spread_menu', isPositionPrevBtn: true, list: [
                    { dataid: 'unCheck', text: '取消审核', befortIcon: '<i class="iconfont">&#xe64d;</i>&nbsp;', callback: function (m) { m.onUnCheckClick(); } },
                    { dataid: 'checkSet', text: '审核设置', befortIcon: '<i class="iconfont">&#xe615;</i>&nbsp;', callback: function (m) { m.confirmSetClick(1); } },
                    { dataid: 'intellectCheckSet', text: '智能审核设置', befortIcon: '<i class="iconfont">&#xe64b;</i>&nbsp;', callback: function (m) { m.onIntellectCheckSetClick(); } }
                ]
            }
        },
        {
            dataid: 'print', code: '1003', text: '打印', style: '',
            class: 'layui-btn layui-btn-xs',
            befortIcon: '<i class="layui-icon">&#xe66d;</i>', afterIcon: '',
            callback: function (m) { m.onPrintClick(); },
            menu: { id: '', list: [] }
        },
        {
            dataid: 'dropDown3', code: '1003', text: '样本处理', style: '',
            class: 'layui-btn layui-btn-xs',
            befortIcon: '<i class="iconfont">&#xe644;</i>&nbsp;', afterIcon: '<i class="layui-icon layui-icon-triangle-d" style="vertical-align:middle;"></i>',
            callback: function (m, item) { m.onDropDown(item); },
            menu: {
                id: 'menuDropDown3', class: 'btn_spread_menu', isPositionPrevBtn: false, list: [
                    { dataid: 'routineToQC', text: '常规转质控', befortIcon: '<i class="iconfont">&#xe625;</i>&nbsp;', callback: function (m) { m.onRoutineToQCClick(); } },
                    { dataid: 'itemMerge', text: '项目合并(糖耐量)', befortIcon: '<i class="iconfont">&#xe65a;</i>&nbsp;', callback: function (m) { m.onItemMergeClick(); } },
                    { dataid: 'dilutedSampleResult', text: '稀释样本结果', befortIcon: '<i class="iconfont">&#xe6a0;</i>&nbsp;', callback: function (m) { m.onDilutedSampleResultClick(); } },
                    { dataid: 'mergeTestForm', text: '合并检验单', befortIcon: '<i class="iconfont">&#xe65a;</i>&nbsp;', callback: function (m) { m.onMergeTestFormClick(); } },
                    { dataid: 'delTestForm', text: '删除检验单', befortIcon: '<i class="iconfont">&#xe626;</i>&nbsp;', callback: function (m) { m.onDelTestFormClick(); } },
                    { dataid: 'delTestFormRecovery', text: '删除检验单恢复', befortIcon: '<i class="iconfont">&#xe6c0;</i>&nbsp;', callback: function (m) { m.onDelTestFormRecoveryClick(); } },
                    { dataid: 'repairPrintBarcode', text: '补打条码', befortIcon: '<i class="iconfont">&#xe634;</i>&nbsp;', callback: function (m) { m.onRepairPrintBarcodeClick(); } },
					{ dataid: 'repairPrintBarcodemodel', text: '补打条码模板设计', befortIcon: '<i class="iconfont">&#xe615;</i>&nbsp;', callback: function (m) { m.printBarcodeModel(); } },
                    { dataid: 'printListForChecking', text: '打印检验清单', befortIcon: '<i class="iconfont">&#xe634;</i>&nbsp;', callback: function (m) { m.onPrintListForCheckingClick(); } },
                    { dataid: 'operationRecord', text: '操作记录', befortIcon: '<i class="iconfont">&#xe61d;</i>&nbsp;', callback: function (m) { m.onOperationRecordClick(); } }
                ]
            }
        },
        {
            dataid: 'dropDown4', code: '1003', text: '批量处理', style: '',
            class: 'layui-btn layui-btn-xs',
            befortIcon: '<i class="iconfont">&#xe6fc;</i>&nbsp;', afterIcon: '<i class="layui-icon layui-icon-triangle-d" style="vertical-align:middle;"></i>',
            callback: function (m, item) { m.onDropDown(item); },
            menu: {
                id: 'menuDropDown4', class: 'btn_spread_menu', isPositionPrevBtn: false, list: [
                    { dataid: 'batchAdd', text: '批量新增检验单', befortIcon: '<i class="iconfont">&#xe738;</i>&nbsp;', callback: function (m) { m.onBatchAddClick(); } },
                    { dataid: 'batchEdit', text: '批量修改检验单', befortIcon: '<i class="iconfont">&#xe60b;</i>&nbsp;', callback: function (m) { m.onBatchEditClick(); } },
                    { dataid: 'batchConfirm', text: '批量检验确认(初审)-批量检验单审定', befortIcon: '<i class="iconfont">&#xe623;</i>&nbsp;', callback: function (m) { m.onBatchConfirmClick(); } },
                    { dataid: 'batchCheck', text: '批量审核检验单', befortIcon: '<i class="iconfont">&#xe670;</i>&nbsp;', callback: function (m) { m.onBatchCheckClick(); } },
                    /* { dataid: 'batchExport', text: '项目结果查看与输出', befortIcon: '<i class="iconfont">&#xe649;</i>&nbsp;', callback: function (m) { m.onBatchExportClick(); } }, */
                    { dataid: 'batchPrint', text: '批量打印报告', befortIcon: '<i class="iconfont">&#xe634;</i>&nbsp;', callback: function (m) { m.onBatchPrintClick(); } },
                    { dataid: 'batchEditSampleNo', text: '批量样本号修改', befortIcon: '<i class="iconfont">&#xe60b;</i>&nbsp;', callback: function (m) { m.onBatchEditSampleNoClick(); } },
                    { dataid: 'batchDislocationHandle', text: '批量样本错位处理', befortIcon: '<i class="iconfont">&#xe618;</i>&nbsp;', callback: function (m) { m.onBatchDislocationHandleClick(); } }
                ]
            }
        }
    ];
    //服务地址
    app.url = {
        //获取角色列表服务
        getRoleListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL',
        //获取角色操作权限列表 -- 平台
        getRoleOperListUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL',
        //确认处理服务
        onConfirmUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormConfirm',
        //取消确认服务
        unConfirmUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormConfirmCancel',
        //审核服务
        onCheckUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormCheck',
        //取消审核服务
        unCheckUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormCheckCancel',
        //删除检验单服务
        onDelTestFormUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestForm',
        //恢复检验单服务
        unDelTestFormUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormDeleteCancel',
        //样本单智能审核判定服务
        onSystemCheckUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormIntellectCheck',
        //获取常规检验参数分类列表. --个性设置->默认设置->出厂设置
        getParamListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryParaValueByParaTypeCode?isPlanish=true',
        //获得危急值服务  -- 用于判断是否存在未发送危急值
        getCVUrl: uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormMsgByHQL?isPlanish=true",
        //根据检验单调用BS报告程序获得PDF报告
        getReportPDFByTestFormIdsUrl: uxutil.path.REPORTFORMQUERYPRINTROOTPATH + '/ServiceWCF/LabStarService.svc/GetReportFormPdfByReportFormId',
        //更新检验单打印次数
        updateTestFormPrintCountUrl: uxutil.path.ROOT + "/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestFormPrintCount",
        //更新报告库报告打印次数
        updateReportFormPrintCountUrl: uxutil.path.REPORTFORMQUERYPRINTROOTPATH + "/ServiceWCF/ReportFormService.svc/ReportFormAddUpdatePrintTimes"
    };
    //初始化方法 -- 外部调用
    app.init = function (params) {
        var me = this;
        me.params = $.extend({}, me.params, params);
        me.initButtons();
        me.getParaList(me.config.CVParaCode, function (res) {
            if (res.success) {
                if (res.value && res.value.list && res.value.list.length > 0) {//存在个性化设置
                    $.each(res.value.list, function (i, item) {
                        me.config.CVParaMap[item["BPara_ParaNo"]] = item["BPara_ParaValue"];
                    });
                } else {
                    uxbase.MSG.onWarn("未找到配置参数!");
                }
            } else {
                uxbase.MSG.onError(res.msg || "个性化参数获取失败!");
            }
        });
    };
    //当前行切换 -- 外部调用
    app.testFormRecordChange = function (testFormRecord) {
        var me = this;
        me.params.testFormRecord = testFormRecord;
    };
    //初始化按钮
    app.initButtons = function () {
        var me = this;
        //获取角色列表
        me.getRoleList(function (roles) {
            //获取角色操作权限列表
            me.getRoleOperList(roles, function () {
                //获得可操作按钮
                me.getOperationButton(function (buttons) {
                    //创建按钮
                    me.creatButtons(buttons);
                });
            });
        });
    };
    //获取角色列表
    app.getRoleList = function (callback) {
        var me = this,
            url = me.url.getRoleListUrl,
            where = 'lbright.RoleID is not null and lbright.EmpID=' + me.params.userId + ' and lbright.LBSection.Id=' + me.params.sectionId;
        url += '?fields=LBRight_RoleID&where=' + where;

        uxutil.server.ajax({ url: url }, function (data) {
            if (data.success) {
                callback((data.value || {}).list || []);
            } else {
                uxbase.MSG.onError(data.msg);
            }
        });
    };
    //获取角色操作权限列表
    app.getRoleOperList = function (roles, callback) {
        var me = this,
            moduleid = me.params.TABMODULEID;

        if (!roles || roles.length == 0) {
            me.OPER_CODE_LIST = [];
            callback();
            return;
        }

        var roleList = [];
        for (var i in roles) {
            roleList.push(roles[i].RoleID);
        }
        var url = me.url.getRoleOperListUrl + '?isPlanish=true&fields=RBACRoleRight_RBACModuleOper_StandCode' +
            '&where=rbacroleright.RBACModuleOper.RBACModule.Id=' + moduleid +' and rbacroleright.RBACRole.Id in (' + roleList.join(',') + ')';

        uxutil.server.ajax({ url: url }, function (data) {
            if (data.success) {
                var list = (data.value || {}).list || [];
                me.OPER_CODE_LIST = [];
                for (var i in list) {
                    me.OPER_CODE_LIST.push(list[i].RBACRoleRight_RBACModuleOper_StandCode);
                }
                callback();
            } else {
                uxbase.MSG.onError(data.msg);
            }
        });
    };
    //获得该用户操作按钮
    app.getOperationButton = function (callback) {
        var me = this,
            buttons = me.BUTTON_LIST.concat(),//深复制
            OperCodeListString = me.OPER_CODE_LIST.join(',') + ',';

        for (var i = 0; i < buttons.length; i++) {
            var node = buttons[i],
                type = typeof (node);

            if (type == 'string') continue;

            if (type == 'object') {
                var code = node.code;

                if (code) {//外层带编码就不判断内部下拉菜单编码，以外层为准
                    var index = OperCodeListString.indexOf(code + ',');
                    //如果功能编码在角色操作权限中不存在，剔除功能按钮
                    if (index == -1) {
                        buttons.splice(i, 1);
                        i--;
                    }
                } else {//内部下拉菜单编码判断
                    var menu = node.menu;
                    if (menu) {
                        for (var j = 0; j < menu.length; j++) {
                            var mCode = menu[j].code;
                            if (mCode) {
                                var index = OperCodeListString.indexOf(mCode + ',');
                                //如果功能编码在角色操作权限中不存在，剔除功能按钮
                                if (index == -1) {
                                    menu.splice(j, 1);
                                    j--;
                                }
                            }
                        }
                    }
                }
            }
        }
        callback(buttons);
    };
    //创建按钮
    app.creatButtons = function (ButtonList) {
        var me = this,
            ButtonList = ButtonList || [],
                html = [],//按钮html
                menuHtml = [];//菜单html
        $.each(ButtonList, function (i, item) {
            //创建button
            html.push('<button type="button" data-id="' + item["dataid"] + '" class="' + item["class"] + '" style="' + item["style"] + '">' + item["befortIcon"] + item["text"] + item["afterIcon"] + '</button>');
            if (typeof (item["callback"]) == 'function') {
                var selector = 'button[data-id="' + item["dataid"] + '"]';
                //监听click事件执行callback
                $("#topToolBar").on('click', selector, function () {
                    item["callback"](me, item);
                });
            }
            //存在菜单
            if (item["menu"]["list"].length > 0) {
                var arr = [];
                $.each(item["menu"]["list"], function (j, itemJ) {
                    //创建菜单项
                    arr.push('<li data-id="' + itemJ["dataid"] + '">' + itemJ["befortIcon"] + itemJ["text"] + '</li>');
                    if (typeof (itemJ["callback"]) == 'function') {
                        var menuItemSelector = '#' + item["menu"]["id"] + ' li[data-id="' + itemJ["dataid"] + '"]';
                        //监听click事件执行callback
                        $("body").on('click', menuItemSelector, function () {
                            itemJ["callback"](me, itemJ);
                        });
                    }
                });
                menuHtml.push('<div id="' + item["menu"]["id"] + '" class="' + item["menu"]["class"] + '"><ul>' + arr.join('') + '</ul></div>');
            }
        });
        $("#topToolBar").html(html.join(''));
        $('body').append(menuHtml.join(''));
    };
    //下拉菜单显示隐藏
    app.onDropDown = function (item) {
        var me = this;
        window.event.stopPropagation();
        var btn = $('#topToolBar button[data-id="' + item["dataid"] + '"]');
        var left = item["menu"]["isPositionPrevBtn"] ? btn.prev().offset().left : btn.offset().left;
        var menuElemID = "#" + item["menu"]["id"];
        $(menuElemID).siblings(".btn_spread_menu").css("display", "none");
        $(menuElemID).css("left", left);
        $(menuElemID).slideToggle("fast");
    };
    //点击其他位置关闭菜单
    $(document).on('click', function () {
        $(".btn_spread_menu").slideUp("fast");
    });
    //刷新
    app.onRefreshClick = function () {
        var me = this;
        layui.event('topToolBar', 'onSearch', {});
    };
    //保存
    app.onSaveClick = function () {
        var me = this;
        layui.event('topToolBar', 'onSave', {});
    };
    //保存并新增
    app.onSaveAndAddClick = function () {
        var me = this;
        layui.event('topToolBar', 'onSaveAndAdd', {});
    };
    //新增
    app.onAddClick = function () {
        var me = this;
        layui.event('topToolBar', 'onAdd', {});
    };
    //检验确认 -- 姓名/性别/样本类型/存在项目结果 都存在才算检验单完成状态
    app.onConfirmClick = function () {
        var me = this;
        layer.confirm('确定要操作吗?', { icon: 3, title: '提示' }, function (index) {
            layer.close(index);
            //判断操作是否可行（只允许单条数据）
            if (me.params.testFormRecord.length != 1) {
                uxbase.MSG.onWarn('请选择一条数据!');
				return;
            }
            
            var record = me.params.testFormRecord[0],//选中行
                id = record.LisTestForm_Id,//检验单id
                StatusID = record.LisTestForm_MainStatusID,//状态
                FormInfoStatus = record.LisTestForm_FormInfoStatus,//检验单信息基本完成状态
                url = me.url.onConfirmUrl;
            //危急值处理
            var isHandle = me.onSendCV("检验单确认");
            if (isHandle) {
                me.openCVSendHtml(id);
                return;
            }
           //检验确认处理
            //if (FormInfoStatus != 1) {//后台判断
            //    uxbase.MSG.onWarn('当前检验单未完成，不允许执行该操作！');
            //    return;
            //}
			var local = uxutil.localStorage.get("LabStar_TS", true),
				userid = uxutil.cookie.get(uxutil.cookie.map.USERID);
            if (local && local[userid] && local[userid]['HandlerInfo']) {
				var HandlerInfo = local[userid]['HandlerInfo'];
				if(HandlerInfo["OperaterType"] == "0" && HandlerInfo['Id']){
					  app.execConfirmClick(id, HandlerInfo);
                }else if (HandlerInfo["OperaterType"] == "1" && HandlerInfo['Id']) {
                    if (HandlerInfo['Id'] && HandlerInfo.BeginTime && HandlerInfo.EndTime) {
                        var presentdatetime = new Date().getTime(),
                            bdt = new Date(HandlerInfo.BeginTime).getTime(),
                            edt = new Date(HandlerInfo.EndTime).getTime();
                        if (bdt <= presentdatetime && presentdatetime <= edt) {
                            app.execConfirmClick(id, HandlerInfo);
                        } else {
                            app.confirmSetClick(0);
                        }
                    } else {
                        app.confirmSetClick(0);
                    }
				}
            }else {
               app.confirmSetClick(0);
            }
        });
    };
    //检验确认服务调用
    app.execConfirmClick = function (id, HandlerInfo, isCheckTestFormInfo) {
        var me = this,
            id = id || null,
            HandlerInfo = HandlerInfo || {},
            isCheckTestFormInfo = (typeof isCheckTestFormInfo != null && typeof isCheckTestFormInfo != 'undefined') ? isCheckTestFormInfo : true,
            url = me.url.onConfirmUrl;
        var configs = {
            type: "POST",
            url: url,
            data: JSON.stringify({
                testFormID: id,
                empID: HandlerInfo.Id,
                empName: HandlerInfo.Name,
                memoInfo: HandlerInfo.memoInfo || '',
                isCheckTestFormInfo: isCheckTestFormInfo
            })
        };
        var loadIndex = layer.load();
        uxutil.server.ajax(configs, function (res) {
            //隐藏遮罩层
            layer.close(loadIndex);
            if (res.success) {
                uxbase.MSG.onSuccess('确认成功!');
                layui.event('common', "refreshTestFormListRecord", { id: id });
            } else {
                if (res.Code == 9) {
                    var html = [], strArr = [], ErrorInfo = $.parseJSON(res.ErrorInfo);
                    $.each(ErrorInfo.list, function (i, item) {
                        strArr.push('<p>' + item+'</p>');
                    });
                    html.push('<div class="layui-colla-item"><h2 class="layui-colla-title" style="height:36px;line-height:36px;">' + ErrorInfo.ErrorInfo + '原因：</h2><div class="layui-colla-content layui-show" style="color:red;max-height:200px;overflow-y:auto;">' + strArr.join('') + '</div></div>');
                    html.push('<div class="layui-colla-item"><h2 class="layui-colla-title">如果您确认检验，请在此备注原因</h2><div class="layui-colla-content layui-show"><textarea id="confirmReason" style="margin-top:5px;width:100%;height:48px;" placeholder="如果您确认检验，请在此备注原因!\r双击可选择初审说明"></textarea></div></div>');
                    html = '<div class="layui-collapse">' + html.join('') + '</div>';
                    layer.open({
                        type: 1,
                        title:'继续确认',
                        content: html,
                        area: '500px',
                        btn: ['检验确认', '放弃确认'],
                        yes: function (index, layero) {//确认按钮的回调
                            var memoInfo = $("#confirmReason").val();
                            if (!memoInfo) {
                                uxbase.MSG.onWarn('请备注原因!', { offset: '15px' });
                                return;
                            }
                            HandlerInfo["memoInfo"] = memoInfo;
                            me.execConfirmClick(id, HandlerInfo, false);
                            layer.close(index);
                        },
                        btn2: function (index, layero) {//取消按钮的回调
                            layer.close(index);
                        },
                        success: function () {
                            $("#confirmReason").off().on('dblclick', function () {
                                var elemID = $(this).attr("id"),
                                    value = $(this).val();
                                me.openPhrase(elemID, value, "CSSM", "初审说明");
                            });
                        }
                    });
                } else {
                    uxbase.MSG.onError(res.ErrorInfo);
                }
            }
        });
    };
    //弹出短语选择
    app.openPhrase = function (elemID, value, TypeCode, TypeName) {
        var me = this,
            sectionID = me.params.sectionId || null,
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
            content: 'basic/phrase/new/index.html?CName=' + TypeName + '&ObjectType=' + ObjectType + '&ObjectID=' + ObjectID + '&PhraseType=' + PhraseType + '&TypeName=' + TypeName + '&TypeCode=' + TypeCode + '&isAppendValue=1&&ISNEXTLINEADD=1',
            success: function (layero, index) {
                var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                //body.find('#CName').html("当前" + TypeName);
                body.find('#Comment').val(value);
                var iframeWin = parent.window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
                iframeWin.externalCallFun(function (v) { $("#" + elemID).val(v); });
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
    //取消检验确认
    app.onUnConfirmClick = function () {
        var me = this;
        layer.confirm('确定要操作吗?', { icon: 3, title: '提示' }, function (index) {
            layer.close(index);
            //判断操作是否可行（只允许单条数据）
            if (me.params.testFormRecord.length != 1) {
                uxbase.MSG.onWarn('请选择一条数据!');
				return;
            }
            var record = me.params.testFormRecord[0],//选中行
                id = record.LisTestForm_Id,//检验单id
                StatusID = record.LisTestForm_MainStatusID,//状态
                url = me.url.unConfirmUrl;
            if (StatusID != 2) {
                uxbase.MSG.onWarn('当前检验单不是检验确认状态，不允许执行该操作!');
                return;
            }
			var configs = {
				type: "POST",
				url: url,
				data: JSON.stringify({
					testFormID: id,
					memoInfo: '',
				})
			};
			var loadIndex = layer.load();
			uxutil.server.ajax(configs, function (res) {
				//隐藏遮罩层
				layer.close(loadIndex);
				if (res.success) {
                    uxbase.MSG.onSuccess('确认取消成功!');
					layui.event('common', "refreshTestFormListRecord", { id: id });
                } else {
                    uxbase.MSG.onError(res.ErrorInfo || res.msg);
				}
			});
        });
    };
    //检验人、审定人设置
    app.confirmSetClick = function (tabletype) {
        var me = this;
		layer.open({
			type: 2,
			area:  ['700px', '400px'],
			fixed: false,
			maxmin: false,
            title: '确认/审核设置',
            content: uxutil.path.ROOT + '/ui/layui/views/sample/setting/confirm/tab.html?SectionID=' + me.params.sectionId + "&sectionCName=" + me.params.sectionCName + "&TABTYPE=" + tabletype,
			end: function () {
				layui.event('bottomToolBar', "refreshbuttomToolBar", {});
			}
		});
    };
    //审核按钮实现
    app.onCheckClick = function () {
        var me = this;
        layer.confirm('确定要操作吗?', { icon: 3, title: '提示' }, function (index) {
            layer.close(index);
            //判断操作是否可行（只允许单条数据）
            if (me.params.testFormRecord.length != 1) {
                uxbase.MSG.onWarn('请选择一条数据!');
        		return;
            }

            var record = me.params.testFormRecord[0],//选中行
                id = record.LisTestForm_Id,//检验单id
                StatusID = record.LisTestForm_MainStatusID;//状态

            //危急值处理
            var isHandle = me.onSendCV("检验单审定");
            if (isHandle) {
                me.openCVSendHtml(id);
                return;
            }
            //审核处理
			var local = uxutil.localStorage.get("LabStar_TS", true),
				userid = uxutil.cookie.get(uxutil.cookie.map.USERID);
			if (local && local[userid] && local[userid]['CheckerInfo']) {
                var CheckerInfo = local[userid]['CheckerInfo'];
                var isTesterEqualChecker = CheckerInfo["isTesterEqualChecker"];//检验者和审核者不同
                if (CheckerInfo["OperaterType"] == "0" && CheckerInfo['Id']) {
                    if (isTesterEqualChecker && local[userid]['HandlerInfo'] && CheckerInfo["Id"] == local[userid]['HandlerInfo']["Id"]) {
                        uxbase.MSG.onWarn("审核失败，原因：检验者和审核者为同一个人,审核设置为不允许审核!");
                        return;
                    }
                    me.beforeCheckTips(id, CheckerInfo);
                } else if (CheckerInfo["OperaterType"] == "1" && CheckerInfo['Id']){
                    if (CheckerInfo['Id'] && CheckerInfo.BeginTime && CheckerInfo.EndTime) {
                        var presentdatetime = new Date().getTime(),
                            bdt = new Date(CheckerInfo.BeginTime).getTime(),
                            edt = new Date(CheckerInfo.EndTime).getTime();
                        if (bdt <= presentdatetime && presentdatetime <= edt) {
                            //app.execCheck(id, CheckerInfo);
                            if (isTesterEqualChecker && local[userid]['HandlerInfo'] && CheckerInfo["Id"] == local[userid]['HandlerInfo']["Id"]) {
                                uxbase.MSG.onWarn("审核失败，原因：检验者和审核者为同一个人,审核设置为不允许审核!");
                                return;
                            }
                            me.beforeCheckTips(id, CheckerInfo);
                        } else {
                            me.confirmSetClick(1);
                        }
                    } else {
                        me.confirmSetClick(1);
                    }
				}
            } else {
              me.confirmSetClick(1);
            }
        });
    };
    //执行审核
    app.execCheck = function (id, CheckerInfo,callback) {
        var me = this,
            url = me.url.onCheckUrl;
        var configs = {
            type: "POST",
            url: url,
            data: JSON.stringify({
                testFormID: id,
                empID: CheckerInfo.Id,
                empName: CheckerInfo.Name,
                memoInfo: CheckerInfo.memoInfo || '',
            })
        };
        var loadIndex = layer.load();
        uxutil.server.ajax(configs, function (res) {
            //隐藏遮罩层
            layer.close(loadIndex);
            if (res.success) {
                callback && callback();
                uxbase.MSG.onSuccess('审核成功!');
                layui.event('common', "refreshTestFormListRecord", { id: id });
            } else {
                uxbase.MSG.onError(res.ErrorInfo || res.msg);
            }
        });
    };
    //审定前提示 --如果存在系统提示（系统审核不通过信息）或者初审说明， 需要提示智能审核说明和初审说明
    app.beforeCheckTips = function (id, CheckerInfo) {
        var me = this,
            record = me.params.testFormRecord[0],
            StatusID = record["LisTestForm_MainStatusID"],//状态
            ZFSysCheckStatus = record["LisTestForm_ZFSysCheckStatus"],//智能审核状态
            ZFSysCheckInfo = record["LisTestForm_ZFSysCheckInfo"],//智能审核备注
            ConfirmInfo = record["LisTestForm_ConfirmInfo"],//检验确认备注说明
            id = id || null,
            CheckerInfo = CheckerInfo || {},
            html = [];//用于提示的html
        if (StatusID != 2) {
            uxbase.MSG.onWarn("只有检验确认数据允许审核!");
            return;
        }
        //没有任何说明 则直接过
        if ((ZFSysCheckStatus == 1 || !ZFSysCheckInfo) && !ConfirmInfo) {
            me.execCheck(id, CheckerInfo);
            return;
        } 

        if (ZFSysCheckInfo && ZFSysCheckStatus != 1) {//系统判定不通过内容 -- 智能审核失败
            var strArr = [];
            if (ZFSysCheckInfo.indexOf('[') != -1) {
                var ErrorInfo = $.parseJSON(ZFSysCheckInfo) || [];
                $.each(ErrorInfo, function (i, item) {
                    strArr.push('<p>' + item + '</p>');
                });
            } else {
                strArr.push(ZFSysCheckInfo);
            }
            html.push('<div class="layui-colla-item"><h2 class="layui-colla-title" style="height:36px;line-height:36px;">智能审核失败，原因：</h2><div class="layui-colla-content layui-show" style="color:red;max-height:200px;overflow:auto;">' + strArr.join('') + '</div></div>');
        }
        if (ConfirmInfo) {//初审说明 -- 智能审核失败后执行检验确认
            html.push('<div class="layui-colla-item"><h2 class="layui-colla-title" style="height:36px;line-height:36px;">检验确认(初审)说明，原因：</h2><div class="layui-colla-content layui-show" style="color:red;max-height:100px;overflow:auto;">' + ConfirmInfo.replace(/\n/g,'<br>') + '</div></div>');
        }
        //注明审核原因
        html.push('<div class="layui-colla-item"><h2 class="layui-colla-title">如果您确认通过，请注明原因：</h2><div class="layui-colla-content layui-show"><textarea id="CheckInfo" style="margin-top:5px;width:100%;height:48px;" placeholder="如果您确认审核，请在此备注原因!\r双击可选择终审说明"></textarea></div></div>');

        html = '<div class="layui-collapse">' + html.join('') + '</div>';
        //弹出提示，注明原因 再进行审核
        layer.open({
            type: 1,
            title: '审定前提示',
            content: html,
            area: '600px',
            btn: ['审核', '放弃审核'],
            yes: function (index, layero) {//审核按钮的回调
                CheckerInfo["memoInfo"] = $("#CheckInfo").val();
                me.execCheck(id, CheckerInfo, function () { layer.close(index); });
            },
            btn2: function (index, layero) {//取消按钮的回调
                layer.close(index);
            },
            success: function (layero, index) {
                $("#CheckInfo").off().on('dblclick', function () {
                    var elemID = $(this).attr("id"),
                        value = $(this).val();
                    me.openPhrase(elemID, value, "ZSSM", "终审说明");
                });
            }
        });
        return false;
    };
    //取消审核
    app.onUnCheckClick = function () {
        var me = this;
        layer.confirm('确定要操作吗?', { icon: 3, title: '提示' }, function (index) {
            layer.close(index);
            //判断操作是否可行（只允许单条数据）
            if (me.params.testFormRecord.length != 1) {
                uxbase.MSG.onWarn('请选择一条数据!');
        		return;
            }
            var record = me.params.testFormRecord[0],//选中行
                id = record.LisTestForm_Id,//检验单id
                StatusID = record.LisTestForm_MainStatusID,//状态
                url = me.url.unCheckUrl;
            if (StatusID != 3 ) {
                uxbase.MSG.onWarn('当前检验单不是审核状态，不允许执行该操作!');
                return;
            }
			var configs = {
				type: "POST",
				url: url,
				data: JSON.stringify({
					testFormID: id,
					memoInfo: '',
				})
			};
			var loadIndex = layer.load();
			uxutil.server.ajax(configs, function (res) {
				//隐藏遮罩层
				layer.close(loadIndex);
				if (res.success) {
                    uxbase.MSG.onSuccess('取消审核成功!');
					layui.event('common', "refreshTestFormListRecord", { id: id });
				} else {
                    uxbase.MSG.onError(res.ErrorInfo);
				}
			});
        });
    };
    //审核设置
    app.onCheckSetClick = function () {
       var me = this;
       me.confirmSetClick(1);
    };
    //智能审核设置
    app.onIntellectCheckSetClick = function () {
        var me = this,
            url = uxutil.path.ROOT + '/ui/layui/views/sample/setting/judge/judge.html?SectionID=' + me.params.sectionId + "&sectionCName=" + me.params.sectionCName;
        //me.confirmSetClick(2);
        layer.open({
            type: 2,
            area: ['600px', '400px'],
            fixed: false,
            maxmin: true,
            title: '智能审核设置',
            content: url,
            end: function () {
                //layui.event('bottomToolBar', "refreshbuttomToolBar", {});
            }
        });
    };
    //打印按钮处理
    app.onPrintClick = function () {
        var me = this,
            list = me.getCheckDataByTableID(me.config.SampleListTableID) || [],
            testformids = [],
            msg = [];

        if (!me.params.testFormRecord || me.params.testFormRecord.length == 0) {
            uxbase.MSG.onWarn('请选择一行数据进行操作!');
            return;
        }
        //不存在勾选数据 则当前选中数据
        if (list.length == 0) list.push(me.params.testFormRecord[0]);

        $.each(list, function (i, item) {
            if (item["LisTestForm_StatusID"] < 3) {
                msg.push("检验日期为：" + uxutil.date.toString(item["LisTestForm_GTestDate"], true) + ",样本号为：" + item["LisTestForm_GSampleNo"] + ",姓名为：" + item["LisTestForm_CName"]);
            } else {
                testformids.push(item["LisTestForm_Id"]);
            }
        });

        if (msg.length > 0) {
            if (testformids.length == 0) {
                uxbase.MSG.onWarn(msg.join('<br />') + "<br />上述样本状态不是已审核状态,只允许打印已审核样本报告!", { area: '500px' });
            } else {
                layer.confirm(msg.join('<br />') + "<br />上述样本状态不是已审核状态,只允许打印已审核样本报告,是否过滤未审核样本进行打印？", { area: '500px', icon: 3, title: '提示' }, function (index) {
                    me.onGetPDFByTestForm(testformids.join(), function (res) {
                        if (res.success) {
                            if (res.value && res.value.length > 0) {
                                var pdfurls = [], reportformids = [];
                                for (var i = 0; i < res.value.length; i++) {
                                    if (res.value[i].PDFPath) {
                                        reportformids.push(res.value[i].ReportFormID);
                                        pdfurls.push(res.value[i].PDFPath);
                                    }
                                }
                                //打印
                                print.instance.pdf.print(pdfurls, "检验单报告", function () {
                                    //更新打印次数
                                    me.onUpdateReportFormPrintCount(reportformids.join());
                                    me.onUpdateTestFormPrintCount(testformids.join(), function (updatetestformids) {
                                        //更新数据
                                        var list = updatetestformids ? updatetestformids.split(",") : [];
                                        for (var j = 0; j < list.length; j++) {
                                            layui.event('common', 'refreshTestFormListRecord', { id: list[j] });
                                        }
                                    });
                                }, true, function () { });
                            }
                        } else {
                            uxbase.MSG.onError(res.msg);
                        }
                    });
                    layer.close(index);
                });
            }
        } else {
            if (testformids.length == 0) return;

            me.onGetPDFByTestForm(testformids.join(), function (res) {
                if (res.success) {
                    if (res.value && res.value.length > 0) {
                        var pdfurls = [], reportformids = [];
                        for (var i = 0; i < res.value.length; i++) {
                            if (res.value[i].PDFPath) {
                                reportformids.push(res.value[i].ReportFormID);
                                pdfurls.push(res.value[i].PDFPath);
                            }
                        }
                        //打印
                        print.instance.pdf.print(pdfurls, "检验单报告", function () {
                            //更新打印次数
                            me.onUpdateReportFormPrintCount(reportformids.join());
                            me.onUpdateTestFormPrintCount(testformids.join(), function (updatetestformids) {
                                //更新数据
                                var list = updatetestformids ? updatetestformids.split(",") : [];
                                for (var j = 0; j < list.length; j++) {
                                    layui.event('common', 'refreshTestFormListRecord', { id: list[j] });
                                }
                            });
                        }, true, function () { });
                    }
                } else {
                    uxbase.MSG.onError(res.msg);
                }
            });
        }
    };
    //获得所选检验单报告PDF
    app.onGetPDFByTestForm = function (testformids, callback) {
        var me = this,
            url = me.url.getReportPDFByTestFormIdsUrl,
            labid = me.config.LabID || 0,
            testformids = testformids || null;

        if (!testformids) return;

        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify({ ReportFormIds: testformids, LabID: labid })
        }, function (res) {
            callback && callback(res);
        });
    };
    //更新检验单打印次数
    app.onUpdateTestFormPrintCount = function (testformids,callback) {
        var me = this,
            url = me.url.updateTestFormPrintCountUrl,
            testformids = testformids || null;

        if (!testformids) return;

        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify({ testFormID: testformids })
        }, function (res) {
            if (res.success) {
                callback && callback(testformids);
            }
        });
    };
    //更新报告库打印次数
    app.onUpdateReportFormPrintCount = function (reportformids) {
        var me = this,
            url = me.url.updateReportFormPrintCountUrl + "?reportformidstr=" + reportformids,
            reportformids = reportformids || null;

        if (!reportformids) return;

        uxutil.server.ajax({
            url: url
        }, function (res) {
            //不做处理
            if (res.success) { }
        });
    };
    //常规转质控
    app.onRoutineToQCClick = function () {
        var me = this;
        me.oprateIsValidOne(function () {
            var record = me.params.testFormRecord[0],//选中行
                TestFormID = record["LisTestForm_Id"],//检验单id
                SectionID = record['LisTestForm_LBSection_Id'],//小组id
                OrderFormID = record['LisTestForm_LisOrderForm_Id'],//医嘱单ID
                MainStatusID = record['LisTestForm_MainStatusID'];//检验单主状态
            //满足以下条件 :非核收样本(医嘱单ID为空)&&未检验确认样本MainStatusID=0&&检验单id和小组id不为空时弹出窗体,不满足条件不弹出
            if (!OrderFormID && MainStatusID == 0 && TestFormID && SectionID) {
                parent.layer.open({
                    title: '常规转质控',
                    type: 2,
                    content: 'handle/qc/index.html?sectionID=' + SectionID + '&testFormID=' + TestFormID+'&t=' + new Date().getTime(),
                    maxmin: true,
                    resize: true,
                    area: ['500px', '400px'],
                    success: function (layero, index) { },
                    end: function () {
                        layui.event('common', 'refreshTestFormListRecord', { id: TestFormID });
                    }
                });
            } else {
                uxbase.MSG.onWarn('该检验单不满足"常规转质控"条件!');
            }
        }, false);
    };
    //项目合并(糖耐量)
    app.onItemMergeClick = function () {
        var me = this;
        var win = $(window),
			maxWidth = win.width()-80,
			maxHeight = win.height()-10;
        parent.layer.open({
            title: '项目合并(糖耐量)',
            type: 2,
            content: 'handle/itemmerge/index.html?t=' + new Date().getTime(),
            maxmin: true,
            resize: true,
            area: [maxWidth+'px', maxHeight+'px'],
            success: function (layero, index) { },
            end: function () {
                layui.event('common', 'refreshTestFormList', {});
            }
        });
    };
    //稀释样本结果
    app.onDilutedSampleResultClick = function () {
         var me = this,
            records = me.params.testFormRecord,
            TestFormID = null;
        if (records.length != 1) {
            uxbase.MSG.onWarn('请选择一行数据进行操作!');
            return;
        }
        TestFormID = records[0]['LisTestForm_Id'];
    	if(!TestFormID){
            uxbase.MSG.onWarn('检验单的ID不存在!');
			return;
		}
        
        parent.layer.open({
            title: '稀释样本结果',
            type: 2,
            content: 'handle/dilution/index.html?TestFormID=' + TestFormID + '&t=' + new Date().getTime(),
            maxmin: true,
            resize: true,
            area: ['800px', '500px'],
            success: function (layero, index) { },
            end: function () {
                layui.event('common', 'refreshTestFormListRecord', { id: TestFormID });
//              layui.event('common', 'refreshTestFormList', {});
            }
        });
    };
    //合并检验单
    app.onMergeTestFormClick = function () {
        var me = this,
            sectionID = me.params.sectionId,
            sectionCName = me.params.sectionCName;
        if (!sectionID) return;
        parent.layer.open({
            title: '合并检验单',
            type: 2,
            content: 'handle/merge/index.html?sectionID=' + sectionID + '&sectionCName=' + sectionCName+'&t=' + new Date().getTime(),
            maxmin: true,
            resize: true,
            area: ['90%', '90%'],
            success: function (layero, index) { },
            end: function () {
                layui.event('common', 'refreshTestFormList', {  });
            }
        });
    };
    //删除检验单 -- 目前只支持单个删除
    app.onDelTestFormClick = function () {
        var me = this;
        me.oprateIsValidOne(function () {
            var record = me.params.testFormRecord[0],//选中行
                id = record['LisTestForm_Id'],//检验单id
                StatusID = record["LisTestForm_MainStatusID"],//状态
                url = me.url.onDelTestFormUrl;

            me.onDelTestFormCall(url, id, 0, 0);
        },true,"确定要删除该条检验单？");
    };
    //删除服务调用
    app.onDelTestFormCall = function (url, delIDList, receiveDeleteFlag,resultDeleteFlag) {
        var me = this,
            url = url || "",
            delIDList = delIDList || null,
            receiveDeleteFlag = receiveDeleteFlag || 0,
            resultDeleteFlag = resultDeleteFlag || 0;

        if (!url || !delIDList) return;

        var load = layer.load();
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify({ delIDList: delIDList, receiveDeleteFlag: receiveDeleteFlag, resultDeleteFlag: resultDeleteFlag })
        }, function (res) {
            layer.close(load);
            if (res.success) {
                uxbase.MSG.onSuccess('检验单删除成功!');
                layui.event('common', 'refreshTestFormListRecord', { id: delIDList });
            } else {
                if (res.Code == 9) {
                    layer.confirm(res.ErrorInfo+"是否仍要执行该操作？", { icon: 3, title: '提示' }, function (index) {
                        me.onDelTestFormCall(url, delIDList, 1, 1);
                        layer.close(index);
                    });
                } else {
                    uxbase.MSG.onError(res.ErrorInfo);
                }
            }
        });

    };
    //删除检验单恢复
    app.onDelTestFormRecoveryClick = function () {
        var me = this;
        me.oprateIsValidOne(function () {
            var record = me.params.testFormRecord[0],//选中行
                id = record['LisTestForm_Id'],//检验单id
                StatusID = record["LisTestForm_MainStatusID"],//状态
                url = me.url.unDelTestFormUrl;

            if (StatusID != -2) {
                uxbase.MSG.onWarn('只有已删除作废数据可执行该操作!');
                return;
            }
            var load = layer.load();
            uxutil.server.ajax({
                url: url,
                type: 'post',
                data: JSON.stringify({ testFormID: id, memoInfo: '' })
            }, function (res) {
                layer.close(load);
                if (res.success) {
                    uxbase.MSG.onSuccess('检验单恢复成功!');
                    layui.event('common', 'refreshTestFormListRecord', { id: id });
                } else {
                    uxbase.MSG.onError(res.ErrorInfo || res.msg);
                }
            });
        },true, "确定要恢复该条检验单？");
    };
    //补打条码
    app.onRepairPrintBarcodeClick = function () {
        var me = this;
		if (me.params.testFormRecord.length != 1) {
            uxbase.MSG.onWarn('请选择一条数据!');
			return;
		}
		var record = me.params.testFormRecord[0];//选中行
		if(!record.LisTestForm_BarCode){
            uxbase.MSG.onWarn('此样本单没有条码号!');
			return;
		}
		var url = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormById?isPlanish=true';
		url += "&fields=LisTestForm_Id,LisTestForm_BarCode,LisTestForm_CName,LisTestForm_PatNo,LisTestForm_LisPatient_GenderName,"
				+"LisTestForm_LisPatient_Birthday,LisTestForm_LisPatient_DistrictName,LisTestForm_LisPatient_WardName,"
				+"LisTestForm_LisPatient_Bed,LisTestForm_LisPatient_DeptName,LisTestForm_LisPatient_DoctorName";
		url +="&id="+ record.LisTestForm_Id;
		var loadindex = layer.load();
		uxutil.server.ajax({
			url:url,
			async:false
		},function(data){
			if(data.success){
				if(data.value){
					var value = data.value;
					var itemurl = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true';
					itemurl += "&fields=LisTestItem_PLBItem_CName,LisTestItem_LBItem_CName";
					itemurl+='&sort=[{"property":"LisTestItem_PLBItem_DispOrder","direction":"ASC"},{"property":"LisTestItem_LBItem_DispOrder","direction":"ASC"}]';
					itemurl += "&where=(listestitem.MainStatusID in (0,-1)) and (listestitem.LisTestForm.Id="+value.LisTestForm_Id+")";
					uxutil.server.ajax({
						url:itemurl,
						async:false
					},function(res){
						layer.close(loadindex);
						if(res.success){
							if(res.value){
								var res = res.value.list,
									namearr = [];
								for(var i=0;i<res.length;i++){
									var name = "";
									if(res[i].LisTestItem_PLBItem_CName && !res[i].LisTestItem_LBItem_CName){
										name = res[i].LisTestItem_PLBItem_CName;
									}else if(!res[i].LisTestItem_PLBItem_CName && res[i].LisTestItem_LBItem_CName){
										name = res[i].LisTestItem_LBItem_CName;
									}else if(res[i].LisTestItem_PLBItem_CName && res[i].LisTestItem_LBItem_CName){
										name = res[i].LisTestItem_PLBItem_CName;
									}
									if(name){
										var isadd = true;
										for(var a=0;a<namearr.length;a++){
											if(namearr[a] == name){
												isadd = false;
											}
										}
										if(isadd){
											namearr.push(name);
										}
									}									
								}
								value["ItemName"] = namearr.join(",");
								var list = [],modeldata=[[]];
								list.push(value);
								modeldata = JSON.stringify([list]).replace(RegExp("LisTestForm_", "g"), "").replace(RegExp("LisPatient_", "g"), "").replace(RegExp("LisPatient_", "g"), "");
								parent.layer.open({
									type: 2,
									area:  ['45%', '55%'],
									fixed: false,
									maxmin: false,
									title: '补打条码',
									content: uxutil.path.ROOT + '/ui/layui/views/system/comm/template/print/index.html?BusinessType=1&ModelType=4&ModelTypeName=补打条码',
									success:function(layero,index){
										var iframe = $(layero).find("iframe")[0].contentWindow;
										iframe.PrintDataStr = modeldata;
									},
									end: function () {
									}
								});
							}else{
                                uxbase.MSG.onWarn('未查询到检验项目信息!');
							}
						}else{
                            uxbase.MSG.onError(res.msg);
						}
					});
				}
			}else{
                layer.close(loadindex);
                uxbase.MSG.onError(data.msg);
			}
		});
    };
    //打印检验清单
    app.onPrintListForCheckingClick = function () {
        var me = this;
        layer.open({
        	type: 2,
        	area:  ['90%', '80%'],
        	fixed: false,
        	maxmin: false,
        	title: '打印检验清单',
        	content: uxutil.path.ROOT + '/ui/layui/views/sample/handle/print/testform/testform.html?sectionid='+me.params.sectionId,
        	end: function () {
        		
        	}
        });
    };
    //操作记录
    app.onOperationRecordClick = function () {
       var me = this,
			records = me.params.testFormRecord;
		if (records.length != 1) {
            uxbase.MSG.onWarn('请选择一行数据进行操作!');
			return;
		}
       layer.open({
       	type: 2,
       	area:  ['700px', '400px'],
       	fixed: false,
       	maxmin: false,
       	title: '操作记录',
       	content: uxutil.path.ROOT + '/ui/layui/views/sample/handle/operate/operate.html?testFormId='+records[0].LisTestForm_Id,
       	end: function () {
       		
       	}
       });
    };
    //批量新增检验单
    app.onBatchAddClick = function () {
        var me = this,
            records = me.params.testFormRecord,
            SectionID = me.params.sectionId;
        if (!SectionID) {
            if (records.length != 1) {
                uxbase.MSG.onWarn('请选择一行数据进行操作!');
                return;
            }
            SectionID = records[0]['LisTestForm_LBSection_Id'];
            if (!SectionID) {
                uxbase.MSG.onWarn('检验单的小组ID不存在!');
                return;
            }
        }
        parent.layer.open({
            title: '批量新增检验单',
            type: 2,
            content: 'batch/add/index.html?sectionID=' + SectionID + '&t=' + new Date().getTime(),
            maxmin: true,
            resize: true,
            area: ['600px', '460px'],
            success: function (layero, index) { },
            end: function () {
                layui.event('common', 'refreshTestFormList', {});
            }
        });
    };
    //批量修改检验单
    app.onBatchEditClick = function () {
        var me = this;
         var me = this;
        var sectionId = app.params.sectionId;
		if (!sectionId) {
			
			if (app.params.testFormRecord.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			sectionId= app.params.testFormRecord[0].get('LisTestForm_LBSection_Id');
			if (!sectionId) {
				JShell.Msg.error('检验单的小组ID不存在！');
				return;
			}
		}
		var win = $(window),
			maxWidth = win.width()-80,
			maxHeight = win.height()-10;
				
		layer.open({
			title:'批量修改检验单',
			type:2,
			content:'batch/edit/index.html?sectionId='+sectionId+'&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[maxWidth+'px',maxHeight+'px'],
			success: function (layero, index) {
                 setTimeout(function(){
	                var value = me.getSearchGTestDateValue();
                     var list = me.getCheckDataByTableID(me.config.SampleListTableID);
	                var obj = {datewhere:value,list:list};
	                var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.SampleInfoCheckStr = obj;
                },200);
            },
			end: function () {
				app.onRefreshClick();
			}
		});
    };
    //批量检验确认(初审)-批量检验单审定
    app.onBatchConfirmClick = function () {
        var me = this;
        var sectionId = app.params.sectionId;
		if (!sectionId) {
			
			if (app.params.testFormRecord.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			sectionId= app.params.testFormRecord[0].get('LisTestForm_LBSection_Id');
			if (!sectionId) {
				JShell.Msg.error('检验单的小组ID不存在！');
				return;
			}
		}
		var win = $(window),
			maxWidth = win.width()-80,
			maxHeight = win.height()-10;
				
		layer.open({
			title:'批量检验确认(初审)-批量检验单审定',
			type:2,
			content:'batch/examine/confirm/index.html?sectionId='+sectionId+'&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[maxWidth+'px',maxHeight+'px'],
			end: function () {
				app.onRefreshClick();
			}
		});
    };
    //批量审核检验单
    app.onBatchCheckClick = function () {
        var me = this;
        var sectionId = app.params.sectionId;
		if (!sectionId) {
			
			if (app.params.testFormRecord.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			sectionId= app.params.testFormRecord[0].get('LisTestForm_LBSection_Id');
			if (!sectionId) {
				JShell.Msg.error('检验单的小组ID不存在！');
				return;
			}
		}
		var win = $(window),
			maxWidth = win.width()-80,
			maxHeight = win.height()-10;
				
		layer.open({
			title:'批量检验单审核',
			type:2,
			content:'batch/examine/check/index.html?sectionId='+sectionId+'&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[maxWidth+'px',maxHeight+'px'],
			end: function () {
				app.onRefreshClick();
			}
		});
    };
    //批量导出结果
    app.onBatchExportClick = function () {
        var me = this;
        uxbase.MSG.onWarn('批量导出结果暂不支持!');
    };
    //批量打印报告
    app.onBatchPrintClick = function () {
        var me = this,
            SectionID = me.params.sectionId;
        if (!SectionID) {
            uxbase.MSG.onWarn('检验单的小组ID不存在!');
            return;
        }
        var win = $(window),
			maxWidth = win.width()-380,
			maxHeight = win.height()-80;
        layer.open({
			title:'批量打印报告',
			type:2,
			content:uxutil.path.REPORTFORMQUERYPRINTROOTPATH + "/ui_new/layui/class/labstar/batch/printreport/printreport.html?SECTIONNO="+SectionID+'&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[maxWidth+'px',maxHeight+'px'],
			success:function(layero,index){
			// setTimeout(function(){
			// 		var iframe = $(layero).find("iframe")[0].contentWindow;
			// 		iframe.initData(SectionID,function(data){
			// 			layer.close(index);
			// 			app.onRefreshClick();
			// 		});
			// 	},200);
			}
		});
        
    };
    //批量样本号修改
    app.onBatchEditSampleNoClick = function () {
        var me = this;
        var sectionId = app.params.sectionId;
		if (!sectionId) {
			
			if (app.params.testFormRecord.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			sectionId= app.params.testFormRecord[0].get('LisTestForm_LBSection_Id');
			if (!sectionId) {
				JShell.Msg.error('检验单的小组ID不存在！');
				return;
			}
		}
		var win = $(window),
			maxWidth = win.width()-100,
			maxHeight = win.height()-80,
			width = maxWidth > 820 ? 820 : maxWidth,
			height = maxHeight > 600 ? 600 : maxHeight;
				
		layer.open({
			title:'批量修改样本号',
			type:2,
			content:'batch/editsampleno/index.html?sectionId='+sectionId+'&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[width+'px',height+'px'],
			end: function () {
				app.onRefreshClick();
			}
		});
    };
    //批量样本错位处理
    app.onBatchDislocationHandleClick = function () {
        var me = this;
            var sectionId = app.params.sectionId;
		if (!sectionId) {
			
			if (app.params.testFormRecord.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			sectionId= app.params.testFormRecord[0].get('LisTestForm_LBSection_Id');
			if (!sectionId) {
				JShell.Msg.error('检验单的小组ID不存在！');
				return;
			}
		}
		var win = $(window),
			maxWidth = win.width()-80,
			maxHeight = win.height()-10;
				
		layer.open({
			title:'批量样本错位处理',
			type:2,
			content:'batch/dislocation/index.html?sectionId='+sectionId+'&t=' + new Date().getTime(),
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[maxWidth+'px',maxHeight+'px'],
			end: function () {
				app.onRefreshClick();
			}
		});
    };
    //判断操作是否可行（只允许单条数据）
    app.oprateIsValidOne = function (callback, isConfirm, msg) {
        var me = this,
            sectionId = me.params.sectionId,
            isConfirm = isConfirm || false,//是否执行confirm
            msg = msg || "确定要进行该操作？",//confirm提示文字
            records = me.params.testFormRecord;

        if (records.length != 1) {
            uxbase.MSG.onWarn('请选择一行数据进行操作!');
            return;
        }
        //小组
        if (!sectionId) {
            sectionId = records[0]['LisTestForm_LBSection_Id'];
            if (!sectionId) {
                uxbase.MSG.onWarn('检验单的小组ID不存在!');
                return;
            }
        }
        if (!isConfirm) {
            callback();
        } else {
            layer.confirm(msg, { icon: 3, title: '提示' }, function (index) {
                callback();
                layer.close(index);
            });
        }
    };
	app.printBarcodeModel = function(){
		var me = this;
		layer.open({
			type: 2,
			area:  ['95%', '90%'],
			fixed: false,
			maxmin: false,
			title: '模板设计',
			content: uxutil.path.ROOT + '/ui/layui/views/system/comm/template/index.html?type=2&BusinessType=1&ModelType=4',
			end: function () {
			}
		});
    };
    //仪器上传消息刷新处理
    app.onEquipResultMsgRefreshHandle = function (data) {
        var me = this,
            data = data || [];
        layui.event('topToolBar', 'onEquipResultMsgRefreshHandle', { data: data });
    };
    //发送危急值 --返回值：false:继续执行后续代码 true:不执行后续代码
    app.onSendCV = function (OperationNode) {
        var me = this,
            OperationNode = OperationNode || null,//操作节点  "检验单确认后"：检验确认，"检验单审定后"：审核
            records = me.params.testFormRecord;//当前检验单

        //未选择检验单 返回false
        if (records.length == 0) return false;
        //操作节点不是发送危急值时间节点 则退出
        if (!OperationNode || OperationNode != me.config.CVParaMap["NTestType_TesItem_PanicValue_0002"]) return false;
        //是否处理危急值
        var isHandle = me.isCVHandle("(ReportStatus=0 or ReportStatus is null) and LisTestForm.Id=" + records[0]["LisTestForm_Id"]);

        return isHandle;
    };
    //是否处理危急值
    app.isCVHandle = function (where) {
        var me = this,
            records = me.params.testFormRecord || [],//当前检验单id
            isSend = false;

        if (!records || records.length == 0) return isSend;
        //警示级别 是否是危急状态
        if (records[0]['LisTestForm_AlarmLevel'] == 4) {
            //当前检验单中是否存在未发送的危急值
            var url = me.url.getCVUrl + (where ? "&where=" + where : "");
            url += '&fields=LisTestFormMsg_Id,LisTestFormMsg_ReportStatus,LisTestFormMsg_LisTestForm_Id';
            url += '&sort=[{ "property": "LisTestFormMsg_GTestDate", "direction": "desc" },{ "property": "LisTestFormMsg_LisTestForm_GSampleNoForOrder", "direction": "desc" }]';
            var load = layer.load();
            uxutil.server.ajax({ url: url, async: false }, function (res) {
                layer.close(load);
                if (res.success) {
                    if (res.value && res.value.list.length > 0) isSend = true;
                }
            })
        }

        return isSend;
    };
    //弹出危急值发送页面
    app.openCVSendHtml = function (testformid) {
        var me = this,
            testformid = testformid || null;
        if (!testformid) return;

        layer.open({
            type: 2,
            area: ['700px', '300px'],
            fixed: false,
            maxmin: true,
            title: "危急值发送",
            content: 'cv/send/index.html?TESTFORMID=' + testformid,
            success: function (layero, index) { },
            end: function () {
                //"危急值发送电话通知"参数为true 并且该检验单存在已发送 未通知的危急值数据 则弹出电话通知界面
                if (me.config.CVParaMap["NTestType_TesItem_PanicValue_0004"] == 1) {
                    var isHandle = me.isCVHandle("ReportStatus=1 and PhoneStatus != 1 and LisTestForm.Id=" + testformid);
                    if (isHandle) {
                        me.openCVNoticeHtml(testformid);
                    }
                }
            }
        });
    };
    //弹出危急值电话通知页面
    app.openCVNoticeHtml = function (testformid) {
        var me = this,
            testformid = testformid || null;
        if (!testformid) return;

        layer.open({
            type: 2,
            area: ['800px', '300px'],
            fixed: false,
            maxmin: true,
            title: "危急值电话通知",
            content: 'cv/notice/index.html?TESTFORMID=' + testformid,
            success: function (layero, index) { }
        });
    };
    //获得检验界面查询检验日期值
    app.getSearchGTestDateValue = function () {
        var me = this,
            DateType = $("#DateType").val(),
            DateValue = $("#DateValue").val(),
            value = null;

        switch (DateType) {
            case "GTestDate":
                value = DateValue;
                break;
            default:
                break;
        }

        return value;
    };
    //根据列表ID获得列表选中数据
    app.getCheckDataByTableID = function (TableID) {
        var me = this,
            TableID = TableID || null,
            list = [];

        if (!TableID) return;

        list = table.checkStatus(TableID) ? table.checkStatus(TableID).data : [];

        return list;
    };

    //查询参数 个性化-》默认-》出厂
    app.getParaList = function (paraTypeCode, callBack) {
        var me = this,
            ParaFilds = [
                'ParaNo', 'CName', 'TypeCode', 'ParaType', 'ParaDesc', 'ParaEditInfo', 'SystemCode',
                'ShortCode', 'BVisible', 'BVisible', 'IsUse', 'ParaValue', 'Id', 'DispOrder'
            ],
            paraTypeCode = paraTypeCode || null,
            sectionId = me.params.sectionId,
            url = me.url.getParamListUrl + "&paraTypeCode=" + paraTypeCode + '&objectID=' + sectionId;
        if (!paraTypeCode || !sectionId) return;
        url += '&fields=BPara_ParaSource,BPara_' + ParaFilds.join(',BPara_');
        uxutil.server.ajax({ url: url }, function (res) {
            if (typeof callBack == "function") callBack(res);
        })
    };
    //暴露接口
    exports('topToolBar', app);
});
