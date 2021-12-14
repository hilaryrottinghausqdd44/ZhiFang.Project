/**
   @Name：危急值查看
   @Author：zhangda
   @version 2021-09-06
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base',
    send: 'views/sample/cv/basic/send',
    notice: 'views/sample/cv/basic/notice',
    info: 'views/sample/cv/basic/info'
}).use(['uxutil','uxbase', 'element', 'form', 'layer', 'laydate', 'send','notice','info'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        laydate = layui.laydate,
        send = layui.send,
        notice = layui.notice,
        info = layui.info,
        uxbase = layui.uxbase,
        uxutil = layui.uxutil;

    //功能栏DOM
    var TOOLBAR_DOM = [
        '<div class="layui-form">',
        '<div class="layui-row">',
        '<div class="layui-col-sm3 layui-col-xs6">',
        '<div class="layui-form-item">',
        '<label class="layui-form-label">检验小组:</label>',
        '<div class="layui-input-block">',
        '<select id="SectionID" name="SectionID" lay-filter="SectionID"></select>',
        '</div>',
        '</div>',
        '</div>',
        '<div class="layui-col-sm3 layui-col-xs6">',
        '<div class="layui-form-item">',
        '<label class="layui-form-label">日期范围:</label>',
        '<div class="layui-input-block">',
        '<input type="text" id="DateRange" name="DateRange" autocomplete="off" class="layui-input myDate" />',
        '<i class="layui-icon layui-icon-date"></i>',
        '</div>',
        '</div>',
        '</div>',
        '<div class="layui-col-sm3 layui-col-xs6">',
        '<div class="layui-form-item">',
        '<label class="layui-form-label">病历号:</label>',
        '<div class="layui-input-block">',
        '<input type="text" id="PatNo" name="PatNo" placeholder="" autocomplete="off" class="layui-input" />',
        '</div>',
        '</div>',
        '</div>',
        '<div class="layui-col-sm5 layui-col-xs6">',
        '<div class="layui-form-item">',
        '<label class="layui-form-label">发送状态:</label>',
        '<div class="layui-input-block">',
        '<input type="radio" name="ReportStatus" value="0" title="未发送" checked>',
        '<input type="radio" name="ReportStatus" value="1" title="已发送">',
        '<input type="radio" name="ReportStatus" value="2" title="不发送">',
        '<input type="radio" name="ReportStatus" value="" title="全部">',
        '</div>',
        '</div>',
        '</div>',
        '<div class="layui-col-sm5 layui-col-xs6">',
        '<div class="layui-form-item">',
        '<label class="layui-form-label">通知状态:</label>',
        '<div class="layui-input-block">',
        '<input type="radio" name="PhoneStatus" value="0" title="未通知">',
        '<input type="radio" name="PhoneStatus" value="1" title="通知成功">',
        '<input type="radio" name="PhoneStatus" value="2" title="通知失败">',
        '<input type="radio" name="PhoneStatus" value="" title="全部" checked>',
        '</div>',
        '</div>',
        '</div>',
        '<div class="layui-col-sm2 layui-col-xs3">',
        '<div class="layui-form-item">',
        '<button type="button" class="layui-btn layui-btn-xs" id="search"><i class="layui-icon layui-icon-search"></i>查询</button>',
        '</div>',
        '</div>',
        '</div>',
        '</div>',
        '<style>',
        '.layui-input + .layui-icon{cursor: pointer;position: absolute;top:2px;right:6px;color:#009688;}',
        '.layui-form-radio {line-height: 20px!important;}',
        '.layui-form-label{width:60px;}',
        '.layui-input-block{margin-left:70px;}',
        '</style>'
    ];
    //当前登录人ID
    var USERID = uxutil.cookie.get(uxutil.cookie.map.USERID);
    //检验小组获取服务地址
    var GET_SECTION_Url = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true';
    //危急值发送实例
    var SendInstance = null;
    //危急值发送选中行
    var SendCheckRowData = null;
    //危急值电话通知实例
    var NoticeInstance = null;
    //危急值电话通知选中行
    var NoticeCheckRowData = null;
    //信息实例
    var InfoInstance = null;
    //当前页签
    var CurrentTab = 0;
    //加载页签
    var LoadTabArr = [{ index: 0 }];
    //初始化页面
    function initHtml() {
        //功能栏
        $('#toolbar').html(TOOLBAR_DOM.join(''));
        //初始化检验小组
        initSection();
        //初始化监听新日期控件
        initDateListeners();
        //初始化危急值发送列表
        SendInstance = send.render({
            domId: "CVSend",
            height: 'full-200',
            //模式 1：查看界面 2：检验界面调用
            model: 1,
            where: getWhere()
        });
        //初始化右侧信息
        InfoInstance = info.render({
            domId: "Info",
            height: null,
            values: {}
        });

        form.render();
        //初始化监听
        initListeners();
    };
    //初始化检验小组
    function initSection() {
        var html = ['<option value="">全部</option>'],
            url = GET_SECTION_Url;
        url += "&sort=[{property:'LBRight_LBSection_DispOrder',direction:'ASC'}]&where=lbright.RoleID is null and lbright.EmpID=" + USERID;
        url += "&fields=LBRight_LBSection_Id,LBRight_LBSection_CName,LBRight_LBSection_UseCode,LBRight_LBSection_DispOrder";

        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = res.value.list;
                    $.each(list, function (i, item) {
                        html.push('<option value="' + item["LBRight_LBSection_Id"] + '">' + item["LBRight_LBSection_CName"] + '</option>');
                    });
                    $("#SectionID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化日期范围
    function initDateRange() {
        laydate.render({//存在默认值
            elem: '#DateRange',
            eventElem: '#DateRange+i.layui-icon',
            type: 'date',
            range: true,
            show: true,
            done: function (value, date, endDate) { }
        });
    };
    //监听新日期控件
    function initDateListeners() {
        var today = new Date();
        //赋值日期框
        $("#DateRange").val(uxutil.date.toString(today, true) + " - " + uxutil.date.toString(today, true));
        //监听日期图标
        $("#DateRange+i.layui-icon").on("click", function () {
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
                initDateRange();
                $("#" + elemID).attr("data-type", "date");
            } else {
                $("#" + elemID).attr("data-type", "text");
                var key = $("#" + elemID).attr("lay-key");
                $('#layui-laydate' + key).remove();
            }
        });
        //监听日期input -- 不弹出日期框
        $("#toolbar").on('focus', '#DateRange', function () {
            preventDefault();
            layui.stope(window.event);
            return false;
        });
    };
    //阻止默认事件
    function preventDefault() {
        var device = layui.device();
        if (device.ie) {
            window.event.returnValue = false;
        } else {
            window.event.preventDefault();
        }
    };

    //获得查询条件
    function getWhere() {
        var SectionID = $("#SectionID").val(),
            DateRange = $("#DateRange").val(),
            PatNo = $("#PatNo").val(),
            ReportStatus = $("#toolbar input[name=ReportStatus]:checked").val(),
            PhoneStatus = $("#toolbar input[name=PhoneStatus]:checked").val(),
            where = [];
        if (SectionID)
            where.push("LisTestForm.LBSection.Id='" + SectionID +"'");
        if (DateRange && DateRange.indexOf(' - ') != -1)
            where.push("GTestDate>='" + DateRange.split(' - ')[0] + "' and GTestDate<'" + uxutil.date.toString(uxutil.date.getNextDate(DateRange.split(" - ")[1], 1), true) +"'");
        if (PatNo)
            where.push("LisTestForm.PatNo='" + PatNo + "'");
        if (ReportStatus != "")
            where.push("ReportStatus='" + ReportStatus + "'");
        if (PhoneStatus != "")
            where.push("PhoneStatus='" + PhoneStatus + "'");

        return where.join(' and ');
    }
    //监听事件
    function initListeners() {
        var me = this;
        //页签切换事件处理
        element.on('tab(tab)', function (data) {
            CurrentTab = data.index;//赋值当前页签
            var where = getWhere();
            var isLoad = false;
            $.each(LoadTabArr, function (i, item) {
                if (item["index"] == CurrentTab) {
                    isLoad = true;
                    return false;
                }
            });
            switch (String(CurrentTab)) {
                case "0"://危急值发送
                    if (!isLoad) {
                        //初始化危急值发送列表
                        SendInstance = send.render({
                            domId: "CVSend",
                            height: 'full-200',
                            //模式 1：查看界面 2：检验界面调用
                            model: 1,
                            where: where
                        });
                        LoadTabArr.push({ index: CurrentTab });
                    } else {
                        SendInstance.onSearch(where);
                    }
                    
                    break;
                case "1"://危急值电话通知
                    if (!isLoad) {
                        //初始化危急值电话通知列表
                        NoticeInstance = notice.render({
                            domId: "CVNotice",
                            height: 'full-225',
                            //模式 1：查看界面 2：检验界面调用
                            model: 1,
                            where: where
                        });
                        LoadTabArr.push({ index: CurrentTab });
                        //危急值电话通知列表单击
                        NoticeInstance.uxtable.table.on('row(' + NoticeInstance.tableId + ')', function (obj) {
                            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
                            NoticeCheckRowData = [];
                            NoticeCheckRowData.push(obj.data);
                            //信息表单赋值
                            InfoInstance.setValues(obj.data);
                        });
                    } else {
                        NoticeInstance.onSearch(where);
                    }
                    var ReportStatus = $("#toolbar input[name=ReportStatus]:checked").val();
                    if (ReportStatus == 0 && ReportStatus != "")
                        uxbase.MSG.onWarn("当前查询发送状态为：未发送!", { offset: '15px' });
                    else if (ReportStatus == "")
                        uxbase.MSG.onWarn("当前查询发送状态为：全部!", { offset: '15px' });
                    break;
                default:
                    break;
            };
        });
        //查询按钮处理
        $("#toolbar").on('click', '#search', function () {
            var where = getWhere();
            switch (String(CurrentTab)) {
                case "0"://危急值发送
                    SendInstance.onSearch(where);
                    break;
                case "1"://危急值电话通知
                    NoticeInstance.onSearch(where);
                    var ReportStatus = $("#toolbar input[name=ReportStatus]:checked").val();
                    if (ReportStatus == 0 && ReportStatus != "")
                        uxbase.MSG.onWarn("当前查询发送状态为：未发送!", { offset: '15px' });
                    else if (ReportStatus == "")
                        uxbase.MSG.onWarn("当前查询发送状态为：全部!", { offset: '15px' });
                    break;
                default:
                    break;
            };
        });
        //危急值发送列表单击
        SendInstance.uxtable.table.on('row(' + SendInstance.tableId + ')', function (obj) {
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            SendCheckRowData = [];
            SendCheckRowData.push(obj.data);
            //信息表单赋值
            InfoInstance.setValues(obj.data);
        });
    };
	//初始化
    function init() {
        //初始化页面
        initHtml();
	};
    //初始化
    init();
});