/**
   @Name：批量新增检验单
   @Author：zhangda
   @version 2021-05-12
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base',
    tableSelect: '../src/tableSelect/tableSelect'
}).use(['uxutil','uxbase', 'element', 'layer','laydate','form','table', 'tableSelect'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        laydate = layui.laydate,
        table = layui.table,
        tableSelect = layui.tableSelect,
        uxbase = layui.uxbase,
        uxutil = layui.uxutil;

    var app = {};
    //外部参数
    app.params = {
        sectionID: null
    };
    //服务地址
    app.url = {
        //新增服务地址
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestForm',
        //查询检验单服务地址
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormByHQL?isPlanish=true',
        //样本类型
        getSampleTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
        //用户 //医生 -- 平台
        getUserUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true',
    };
    //配置
    app.config = {
        //检验日期
        defaultDate: null,
        sampleCount:10
    };

    //初始化
    app.init = function () {
        var me = this,
            today = new Date();
        me.getParams();
        me.config.defaultDate = uxutil.date.toString(today, true);
        $("#GSampleNoForOrder").val(me.config.sampleCount);
        me.setGSampleNo();
        me.initDateListeners();
        me.initSelect();
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
        //下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
        $("input.layui-input+.layui-icon").on('click', function () {
            if (!$(this).hasClass("myDate") && !$(this).hasClass("myPhrase")) {
                $(this).prev('input.layui-input')[0].click();
                return false;//不加的话 不能弹出
            }
        });
        //监听+ 弹出短语
        $("#formInfo input.myPhrase+i.layui-icon").on("click", function () {
            var elemID = $(this).prev().attr("id"),
                value = $(this).prev().val(),
                TypeCode = $(this).prev().attr("data-typecode"),
                TypeName = $(this).prev().attr("data-typename");
            me.openPhrase(elemID, value, TypeCode, TypeName);
        });
        //保存
        $("#save").on('click', function () {
            me.onSaveClick();
        });
        //重置
        $("#reset").on('click', function () {
            me.onResetClick();
        });
        //关闭
        $("#cancel").on('click', function () {
            me.onCancelClick();
        });
    };
    //初始化yyyy-mm-dd
    app.initDate = function (id) {
        var me = this;
        //检测日期 yyyy-MM-dd
        laydate.render({//没有默认值
            elem: '#' + id,
            eventElem: '#' + id + '+i.layui-icon',
            type: 'date',
            show: true
        });
    };
    //监听新日期控件
    app.initDateListeners = function () {
        var me = this;
        //赋值日期框
        $("#formInfo input.myDate").val(me.config.defaultDate);
        //监听日期图标
        $("#formInfo input.myDate+i.layui-icon").on("click", function () {
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
                me.initDate(elemID);
                $("#" + elemID).attr("data-type", "date");
            } else {
                $("#" + elemID).attr("data-type", "text");
                var key = $("#" + elemID).attr("lay-key");
                $('#layui-laydate' + key).remove();
            }
        });
        //监听日期input -- 不弹出日期框
        $("#formInfo").on('focus', '.myDate', function () {
            var device = layui.device();
            if (device.ie) {
                window.event.returnValue = false;
            } else {
                window.event.preventDefault();
            }
            layui.stope(window.event);
            return false;
        });
    };
    //初始化下拉框
    app.initSelect = function () {
        var me = this,
            sectionID = me.params.sectionID;
        //初始化检验小组下拉框
        me.initSampleType("LisTestForm_GSampleTypeCName", "LisTestForm_GSampleTypeID");
    };
    //初始化检验小组下拉框
    app.initSection = function (CNameElemID, IdElemID) {
        var me = this,
            CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null,
            url = me.url.GetSectionUrl + '&sort=[{"property":"LBSection_DispOrder","direction":"ASC"}]' +
                '&fields=LBSection_Id,LBSection_CName,LBSection_UseCode,LBSection_DispOrder&where=lbsection.IsUse=1';
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBSection_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbsection.CName,lbsection.UseCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '小组名称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBSection_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBSection_CName', width: 200, title: '小组名称', sort: false },
                    { field: 'LBSection_UseCode', width: 120, title: '小组编码', sort: false },
                    { field: 'LBSection_DispOrder', width: 80, title: '排序', sort: false, hide: true }
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
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["LBSection_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBSection_Id"]);
                } else {
                    $(elem).val('');
                    if (IdElemID) $("#" + IdElemID).val('');
                }
            }
        });
    };
    //初始化样本类型下拉框
    app.initSampleType = function (CNameElemID, IdElemID) {
        var me = this,
            CNameElemID = CNameElemID || null,
            IdElemID = IdElemID || null,
            url = me.url.getSampleTypeUrl + '&sort=[{"property":"LBSampleType_DispOrder","direction":"ASC"}]' +
                '&fields=LBSampleType_Id,LBSampleType_CName,LBSampleType_UseCode,LBSampleType_DispOrder&where=lbsampletype.IsUse=1';
        if (!CNameElemID) return;
        tableSelect.render({
            elem: '#' + CNameElemID,	//定义输入框input对象 必填
            checkedKey: 'LBSampleType_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbsampletype.CName,lbsampletype.UseCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '样本类型名称/编码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBSampleType_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBSampleType_CName', width: 200, title: '样本类型名称', sort: false },
                    { field: 'LBSampleType_UseCode', width: 120, title: '样本类型编码', sort: false },
                    { field: 'LBSampleType_DispOrder', width: 80, title: '排序', sort: false, hide: true }
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
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["LBSampleType_CName"]);
                    if (IdElemID) $("#" + IdElemID).val(record["LBSampleType_Id"]);
                } else {
                    $(elem).val('');
                    if (IdElemID) $("#" + IdElemID).val('');
                }
            }
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
            content: uxutil.path.ROOT + '/ui/layui/views/sample/basic/phrase/new/index.html?CName=' + TypeName + '&ObjectType=' + ObjectType + '&ObjectID=' + ObjectID + '&PhraseType=' + PhraseType + '&TypeName=' + TypeName + '&TypeCode=' + TypeCode +'&isAppendValue=1&&ISNEXTLINEADD=1',
            success: function (layero, index) {
                var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                //body.find('#CName').html("当前" + TypeName);
                body.find('#Comment').val(value.replace(/\|/g, "\n"));
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
    //设置样本起始号
    app.setGSampleNo = function () {
        var me = this,
            GSample = $("#GSampleNo");
        me.getGSampleNo(function (list) {
            var GSampleNo = null;
            if (list.length == 0) {
                GSampleNo = 1;
            } else {
                $.each(list, function (i,item) {
                    if (me.isRealNum(item["LisTestForm_GSampleNo"])) {
                        GSampleNo = Number(item["LisTestForm_GSampleNo"]) + 1;
                        return false;
                    }
                });
            }
            if (GSampleNo == null) GSampleNo = 1;
            GSample.val(GSampleNo);
        });
    };
    /**获取默认样本起始号*/
    app.getGSampleNo = function (callback) {
        var me = this,
            sort = [{ "property": "LisTestForm_GTestDate", "direction": "desc" }, { "property": "LisTestForm_GSampleNoForOrder", "direction": "desc" }],
            url = me.url.selectUrl;

        url += '&fields=LisTestForm_GSampleNo';
        url += '&sort=' + JSON.stringify(sort);
        url += "&where=listestform.GTestDate='" + me.config.defaultDate + "' and listestform.LBSection.Id=" + me.params.sectionID;
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                var list = res.value ? res.value.list : [];
                callback(list);
            }
        });
    };
    /**判断值是否是数字
    * true:数值型的，false：非数值型
    * */
    app.isRealNum = function (val) {
        var me = this;
        // isNaN()函数 把空串 空格 以及NUll 按照0来处理 所以先去除
        if (val === "" || val == null) return false;
        if (!isNaN(val)) {
            return true;
        } else {
            return false;
        }
    };
    //保存
    app.onSaveClick = function () {
        var me = this,
            url = me.url.addUrl,
            load = layer.load(),
            sectionID = me.params.sectionID,
            msg = [],
            DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/, //判断是否是日期格式 yyyy-mm-dd
            GTestDate = $("#GTestDate").val(),//检测日期
            GSampleNo = $("#GSampleNo").val(),//样本起始号
            GSampleNoForOrder = $("#GSampleNoForOrder").val();//样本数量
        //验证
        if (GTestDate == "") msg.push("检测日期不能为空!");
        if (GTestDate != "" && (!uxutil.date.isValid(GTestDate) || !DATE_FORMAT.test(GTestDate))) msg.push("检测日期格式不正确!");
        if (GSampleNo == "") msg.push("样本起始号不能为空!");
        if (GSampleNoForOrder == "") msg.push("样本数量不能为空!");
        if (GSampleNoForOrder != "" && !me.isRealNum(GSampleNoForOrder)) msg.push("样本数量必须是数值!");
        if (msg.length > 0) {
            uxbase.MSG.onWarn(msg.join('<br>'));
            layer.close(load);
            return;
        }
        //其他信息
        var sampleInfo = me.getSampleInfo();
        if (!sampleInfo) {
            layer.close(load);
            return;
        }
        //发送数据
        var entity = {
            sampleInfo: JSON.stringify(sampleInfo),
            testDate: uxutil.date.toString(GTestDate, true),
            sectionID: sectionID,
            startSampleNo: GSampleNo,
            sampleCount: GSampleNoForOrder
        };
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify(entity)
        }, function (res) {
                layer.close(load);
                if (res.success) {
                    uxbase.MSG.onSuccess("保存成功!");
                    me.onResetClick();
                } else {
                    uxbase.MSG.onError(res.ErrorInfo || res.msg);
                }
        });
    };
    //获得保存样本其他信息
    app.getSampleInfo = function () {
        var me = this,
            GSampleTypeID = $("#LisTestForm_GSampleTypeID").val(),//样本类型ID
            GSampleTypeCName = $("#LisTestForm_GSampleTypeCName").val();//样本类型名称

        var entity = {
            GSampleInfo: $("#LisTestForm_GSampleInfo").val().replace(/\|/g, "\n"),//小组样本描述
            FormMemo: $("#LisTestForm_FormMemo").val().replace(/\|/g, "\n"),//检验样本备注
            SampleSpecialDesc: $("#LisTestForm_SampleSpecialDesc").val().replace(/\|/g, "\n"),//样本特殊性状
            TestComment: $("#LisTestForm_TestComment").val().replace(/\|/g, "\n"),//检验备注
            TestInfo: $("#LisTestForm_TestInfo").val().replace(/\|/g, "\n")//检验评语
        };
        if (GSampleTypeID) entity.GSampleTypeID = GSampleTypeID;
        if (GSampleTypeCName) entity.GSampleType = GSampleTypeCName;
        return entity;
    };
    //重置
    app.onResetClick = function () {
        var me = this;
        $("#formInfo :input").each(function (i, item) {
            $(this).val('');
        });
        //赋值日期框
        $("#formInfo input.myDate").val(me.config.defaultDate);
        //样本数量
        $("#GSampleNoForOrder").val(me.config.sampleCount);
        //赋值样本起始号
        me.setGSampleNo();
    };
    //关闭
    app.onCancelClick = function () {
        var me = this;
        var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
        parent.layer.close(index); //再执行关闭
    };
    //初始化
    app.init();
});