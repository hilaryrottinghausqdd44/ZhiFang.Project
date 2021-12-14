/**
   @Name：样本信息合并
   @Author：zhangda
   @version 2021-05-12
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base',
    tableSelect: '../src/tableSelect/tableSelect',
    mergeSampleTable: 'views/sample/handle/merge/mergeSampleTable',
    ItemResultTable: 'views/sample/handle/merge/ItemResultTable'
}).use(['uxutil','uxbase', 'element', 'form','table','laydate', 'layer', 'tableSelect','mergeSampleTable','ItemResultTable'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        table = layui.table,
        laydate = layui.laydate,
        tableSelect = layui.tableSelect,
        mergeSampleTable = layui.mergeSampleTable,
        ItemResultTable = layui.ItemResultTable,
        uxbase = layui.uxbase,
        uxutil = layui.uxutil;

    var app = {};
    //外部参数
    app.params = {
        sectionID: null,
        sectionCName: null
    };
    //服务地址
    app.url = {
        GetSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',
        MergeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormInfoMerge'
    };
    //配置
    app.config = {

    };

    //初始化
    app.init = function () {
        var me = this;
        me.getParams();
        if (!me.params.sectionID) uxbase.MSG.onWarn("缺少小组参数!");
        me.initDateListeners();
        me.initSelect();
        mergeSampleTable.render({ elem: '#mergeSampleTable', title: '确定合并样本', id: 'mergeSampleTable', height: '150' });
        ItemResultTable.render({ elem: '#ItemResultTable', title: '项目结果源', id: 'ItemResultTable', height: 'full-240' });
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
        //icon 前存在icon 则点击该icon 等同于点击input
        $("input.layui-input+.layui-icon").on('click', function () {
            if (!$(this).hasClass("myDate")) {
                $(this).prev('input.layui-input')[0].click();
                return false;//不加的话 不能弹出
            }
        });
        //确定查询按钮
        $("#confirm").off().on('click',function () {
            me.onConfirmClick();
        });
        //执行合并按钮
        $("#merge").off().on('click',function () {
            me.onMergeClick();
        });
        //项目结果列表清空
        layui.onevent('mergeSampleTable', 'cleardata', function (data) {
            ItemResultTable.cleardata();
        });
        //合并样本列表单击
        layui.onevent('mergeSampleTable', 'rowclick', function (data) {
            var data = data;
            me.loadSearch(data);
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
        var me = this,
            today = new Date();
        //赋值日期框
        $("#mergeForm input.myDate").val(uxutil.date.toString(today, true));
        //监听日期图标
        $("#mergeForm input.myDate+i.layui-icon").on("click", function () {
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
        $("#mergeForm").on('focus', '.myDate', function () {
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
            sectionID = me.params.sectionID,
            sectionCName = me.params.sectionCName;
        //检验小组赋值
        $("#LBSection_ID").val(sectionID);
        $("#LBSection_CName").val(sectionCName);
        $("#DLBSection_ID").val(sectionID);
        $("#DLBSection_CName").val(sectionCName);
        //初始化检验小组下拉框
        me.initSection("LBSection_CName", "LBSection_ID");
        me.initSection("DLBSection_CName", "DLBSection_ID");
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
    //确认按钮
    app.onConfirmClick = function () {
        var me = this,
            msg = [],
            obj = {};
        obj.isSampleNoMerge = $("#isSampleNoMerge").prop("checked");
        obj.isSerialNoMerge = $("#isSerialNoMerge").prop("checked");
        obj.mergeType = $("#mergeForm input[type=radio][name=mergeType]:checked").val();
        obj.LBSection_ID = $("#LBSection_ID").val();
        obj.LBSection_CName = $("#LBSection_CName").val();
        obj.GTestDate = $("#GTestDate").val();
        obj.GSampleNo = $("#GSampleNo").val();
        obj.DLBSection_ID = $("#DLBSection_ID").val();
        obj.DLBSection_CName = $("#DLBSection_CName").val();
        obj.DGTestDate = $("#DGTestDate").val();
        obj.DGSampleNo = $("#DGSampleNo").val();
        if (!obj.LBSection_ID) msg.push("请选择源样本小组!");
        if (!obj.GTestDate) msg.push("请选择源样本检测日期!");
        if (!obj.GSampleNo) msg.push("请选择源样本样本号!");
        if (!obj.DLBSection_ID) msg.push("请选择目标样本小组!");
        if (!obj.DGTestDate) msg.push("请选择目标样本检测日期!");
        if (!obj.DGSampleNo) msg.push("请选择目标样本样本号!");
        if (obj.GSampleNo == obj.DGSampleNo && obj.LBSection_ID == obj.DLBSection_ID && obj.GTestDate == obj.DGTestDate) msg.push('源样本与目标样本合同条件不能相同!');
        if (msg.length != 0) {
            uxbase.MSG.onWarn(msg.join('<br>'));
            return;
        }
        mergeSampleTable.onSearch(obj);
    };
    //合并按钮
    app.onMergeClick = function () {
        var me = this,
            isSampleNoMerge = $("#isSampleNoMerge").prop("checked"),
            isSerialNoMerge = $("#isSerialNoMerge").prop("checked"),
            mergeType = $("#mergeForm input[type=radio][name=mergeType]:checked").val(),
            isDelFormTestItem = $("#isDelFormTestItem").prop("checked"),
            isDelFormTestForm = $("#isDelFormTestForm").prop("checked");

        //源项目合并后,如果没有项目,删除源样本单
        if (isDelFormTestForm) {
            var isExec = me.isVerify();
            if (!isExec) return;
        }

        var entity = {
            mergeType: mergeType,
            isSampleNoMerge: isSampleNoMerge ? 1 : 0,
            isSerialNoMerge: isSerialNoMerge ? 1 : 0
        };
        var parms = me.getSaveParams();
        if (!parms) {
            uxbase.MSG.onWarn("请先确定合并样本!");
            return;
        }
        entity.fromTestFormID = parms.FromTestFormID;
        if (parms.toTestForm) entity.toTestForm = parms.toTestForm;//目标检验单实体

        if (parms.StrFromTestItemID && mergeType != "1") {
            entity.strFromTestItemID = parms.StrFromTestItemID;
        } else {
            entity.strFromTestItemID = "";
        }

        entity.isDelFormTestItem = isDelFormTestItem ? 1 : 0;
        entity.isDelFormTestForm = isDelFormTestForm ? 1 : 0;
        if (mergeType == "2" && entity.toTestForm.Id == 0) {
            uxbase.MSG.onWarn("仅合并项目信息时，目标样本不能为空!");
            return;
        }
        //存在条码号
        if (entity.toTestForm.BarCode) {
            layer.confirm('目标样本条码号为【' + entity.toTestForm.BarCode + "】,是否继续合并？", { icon: 3, title: '提示' }, function (index) {
                me.onMergeSave(entity);
                layer.close(index);
            });
        } else {
            me.onMergeSave(entity);
        }
    };
    //合并保存
    app.onMergeSave = function (entity) {
        var me = this,
            url = me.url.MergeUrl,
            entity = entity || null;

        if (!entity) return;
        var load = layer.load();
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify(entity)
        }, function (res) {
            layer.close(load);
            if (res.success) {
                uxbase.MSG.onSuccess("合并成功!");
                me.clearData();
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    /**项目结果源数据加载*/
    app.loadSearch = function (record) {
        var me = this,
            Id = record['LisTestForm_Id'],//源样本单ID
            GTestDate = record['LisTestForm_GTestDate'],//源样本单检验日期
            DId = record['LisTestForm_DId'],//目标样本单ID
            DGTestDate = record['LisTestForm_DGTestDate'];//目标样本单ID
        ItemResultTable.onSearch(Id, GTestDate, DId, DGTestDate);
    };
	/**删除源样本校验
	 * 源样本：源样本可以是MainStatusID>=0,=0时才能删除源样本* */
    app.isVerify = function () {
        var me = this,
            isExec = true,
            recs = table.cache["mergeSampleTable"] || [];
        if (recs.length == 0) return false;
        for (var i = 0; i < recs.length; i++) {
            var MainStatusID = recs[i]['LisTestForm_MainStatusID'];
            if (!MainStatusID) MainStatusID = 0;
            MainStatusID = Number(MainStatusID);
            if (MainStatusID != 0) {
                uxbase.MSG.onWarn("源样本主单状态不能删除!");
                isExec = false;
                break
            }
        }
        return isExec;
    };
	/**获取保存的参数 * */
    app.getSaveParams = function () {
        var me = this,
            Params = {},
            res = mergeSampleTable.config.checkRowData;//获取确认合并样本选择行
        if (res.length == 0) return;
        //源样本单ID
        var fromTestFormID = res[0]['LisTestForm_Id'];
        //目标样本单信息
        var toTestForm = {};
        if (res[0]['LisTestForm_DId']) toTestForm.Id = res[0]['LisTestForm_DId'];//目标样本单ID
        if (res[0]['LisTestForm_DGSampleNo']) toTestForm.GSampleNo = res[0]['LisTestForm_DGSampleNo'];//目标样本单样本号
        if (res[0]['LisTestForm_DGTestDate']) toTestForm.GTestDate = uxutil.date.toServerDate(res[0]['LisTestForm_DGTestDate']);//目标样本单检验日期
        if (res[0]['LisTestForm_DSectionID']) toTestForm.LBSection = { Id: res[0]['LisTestForm_DSectionID'], DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] }; //目标样本单条码号
        if (res[0]['LisTestForm_LisPatient_DId']) toTestForm.LisPatient = { Id: res[0]['LisTestForm_LisPatient_DId'], DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };//就诊信息Id
        if (res[0]['LisTestForm_DCName']) toTestForm.CName = res[0]['LisTestForm_DCName'];//目标样本单姓名
        if (res[0]['LisTestForm_DPatNo']) toTestForm.PatNo = res[0]['LisTestForm_DPatNo'];//目标样本单病历号
        if (res[0]['LisTestForm_DBarCode']) toTestForm.BarCode = res[0]['LisTestForm_DBarCode'];//目标样本单条码号
        if (res[0]['LisTestForm_DMainStatusID'] != "") toTestForm.MainStatusID = res[0]['LisTestForm_DMainStatusID'];//目标样本单主状态

        Params.FromTestFormID = fromTestFormID;
        Params.toTestForm = toTestForm;

        var strFromTestItemID = "";
        //样本单项目LisTestItem_Id
        var records = table.checkStatus('ItemResultTable').data;
        if (records.length > 0) {
            for (var i = 0; i < records.length; i++) {
                if (i > 0) strFromTestItemID += ",";
                strFromTestItemID += records[i]['LisTestItem_Id'];
            }
        }
        Params.StrFromTestItemID = strFromTestItemID;
        return Params;
    };
    //清空列表
    app.clearData = function () {
        var me = this;
        mergeSampleTable.cleardata();
        ItemResultTable.cleardata();
    };
    //初始化
    app.init();
});