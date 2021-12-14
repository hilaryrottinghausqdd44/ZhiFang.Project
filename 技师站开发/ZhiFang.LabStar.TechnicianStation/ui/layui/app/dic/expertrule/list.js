/**
   @Name：专家规则明细
   @Author：zhangda
   @version 2021-11-30
 */
layui.extend({
}).define(['uxutil', 'uxbase', 'uxtable', 'form', 'table', 'tableSelect','CommonSelectEnum'], function (exports) {
    "use strict";

    var $ = layui.$,
        form = layui.form,
        tableSelect = layui.tableSelect,
        CommonSelectEnum = layui.CommonSelectEnum,
        uxutil = layui.uxutil,
        uxbase = layui.uxbase,
        uxtable = layui.uxtable,
        MOD_NAME = 'list';

    //小组项目服务地址
    var GET_SECTIONITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true';
    //项目服务地址
    var GET_ITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true';
    //获得枚举服务地址
    var GET_ENUMTYPE_URL = uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic';

    //获取专家规则明细服务地址
    var GET_EXPERT_RULE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBExpertRuleListByHQL?isPlanish=true';
    //新增专家规则明细服务地址
    var ADD_EXPERT_RULE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBExpertRuleList';
    //编辑专家规则明细服务地址
    var EDIT_EXPERT_RULE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBExpertRuleListByField';
    //删除专家规则明细服务地址
    var DEL_EXPERT_RULE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBExpertRuleList';

    //对比内容与对比值类型关系
    var ValueFieldTypeMap = {
        "0": "0",
        "1": "1",
        "2": "1",
        "3": "2",
        "4": "0",
        "5": "0",
        "6": "1",
        "7": "1",
        "8": "0",
        "9": "0",
        "10": "1",
        "11": "1"
    };
    //对比值类型与对比类型关系
    var ValueTypeMap = {
        "0": ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"],
        "1": ["2", "3", "20", "21"],
        "2": ["30", "31"]
    };
    //对比类型
    var CompTypeData = [];

    //当前行数据
    var CheckRowData = [];
    //列表定位数据ID
    var TablePositionDataId = null;
    //表单状态
    var FormType = 'add';
    //专家规则ID
    var ExpertRuleID = null;
    //专家规则所属检验小组
    var SectionID = null;
    //专家主规则所设项目ID
    var ItemID = null;
    //专家主规则所设项目名称
    var ItemName = null;

    //医嘱单列表
    var list = {
        //对外参数
        config: {
            tableId: null,
            height: null
        },
        //内部列表参数
        tableConfig: {
            elem: null,
            toolbar: null,
            skin: 'line',//行边框风格
            //even:true,//开启隔行背景
            size: 'sm',//小尺寸的表格
            defaultToolbar: null,
            height: 'full-60',
            defaultLoad: true,
            url: '',
            data: [],
            where: {},
            cols: [[
                { type: 'numbers', title: '行号' },
                { field: 'LBExpertRuleList_Id', width: 140, title: '主键', hide: true },
                { field: 'LBExpertRuleList_ExpertRuleID', width: 100, title: '专家规则ID', hide: true },
                { field: 'LBExpertRuleList_ItemID', width: 100, title: '项目ID', hide: true },
                { field: 'LBExpertRuleList_ItemName', width: 100, title: '对比项目' },
                { field: 'LBExpertRuleList_RuleName', minWidth: 100, title: '规则明细' },

                { field: 'LBExpertRuleList_SysRuleName', minWidth: 100, title: '系统规则明细', hide: true },
                { field: 'LBExpertRuleList_DispOrder', width: 60, title: '显示次序', hide: true },
                { field: 'LBExpertRuleList_ValueFieldType', width: 60, title: '对比内容', hide: true },
                { field: 'LBExpertRuleList_BRelatedItemValue', width: 60, title: '相关检验结果', hide: true },
                { field: 'LBExpertRuleList_BHisItemValue', width: 60, title: '历史检验结果', hide: true },
                { field: 'LBExpertRuleList_BCalcItemValue', width: 60, title: '计算结果', hide: true },
                { field: 'LBExpertRuleList_ValueType', width: 60, title: '对比值类型', hide: true },
                { field: 'LBExpertRuleList_CompType', width: 60, title: '对比类型', hide: true },
                { field: 'LBExpertRuleList_CompValue', width: 60, title: '对比值', hide: true },
                { field: 'LBExpertRuleList_CompFValue', width: 60, title: '对比数值1', hide: true },
                { field: 'LBExpertRuleList_CompFValue2', width: 60, title: '对比数值2', hide: true },
                { field: 'LBExpertRuleList_BValue', width: 60, title: '对比内容存在', hide: true },
                { field: 'LBExpertRuleList_BLastHisComp', width: 60, title: '仅判断最近一次历史结果', hide: true },
                { field: 'LBExpertRuleList_BLimitHisDate', width: 60, title: '限制历史对比日期', hide: true },
                { field: 'LBExpertRuleList_LimitHisDate', width: 60, title: '历史对比日期', hide: true },
                { field: 'LBExpertRuleList_CalcFormula', width: 60, title: '计算公式', hide: true },
                { field: 'LBExpertRuleList_DispOrder', width: 60, title: '显示次序', hide: true }
            ]],
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
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
        }
    };
    //构造器
    var Class = function (setings) {
        var me = this;
        me.config = $.extend({}, me.config, list.config, setings);
        me.tableConfig = $.extend({}, me.tableConfig, list.tableConfig);

        if (me.config.height) me.tableConfig.height = me.config.height;
        me.tableConfig.elem = "#" + me.config.tableId;
        //数据渲染完的回调
        me.tableConfig.done = function (res, curr, count) {
            if (count > 0) {
                if (TablePositionDataId) {
                    var index = null;
                    $.each(res.data, function (i, item) {
                        if (item["LBExpertRuleList_Id"] == TablePositionDataId) {
                            index = i;
                            return false;
                        }
                    });
                    if (index != null) {
                        $("#" + me.config.tableId + "+div").find('.layui-table-main tr[data-index="' + index + '"]').click()
                        if (document.querySelector("#" + me.config.tableId + "+div .layui-table-body table.layui-table tbody tr:nth-child(" + (index + 1) + ")").length > 0)
                            document.querySelector("#" + me.config.tableId + "+div .layui-table-body table.layui-table tbody tr:nth-child(" + (index + 1) + ")").scrollIntoView(false, { behavior: "smooth" });
                    }
                    TablePositionDataId = null;
                } else {
                    //默认选中第一行
                    me.onClickFirstRow();
                }
            } else {
                CheckRowData = [];
                setTimeout(function () { me.onAddRuleList();},10);
            }
        };

    };
    //初始化HTML
    Class.prototype.initHtml = function () {
        var me = this;
        //对比内容
        CommonSelectEnum.render({ domId: 'LBExpertRuleList_ValueFieldType', classNameSpace: 'ZhiFang.Entity.LabStar', className: 'CompValueField', defaultName: '', done: function () { } });
        //对比判断
        CommonSelectEnum.render({ domId: 'LBExpertRuleList_ValueType', classNameSpace: 'ZhiFang.Entity.LabStar', className: 'CompValueType', defaultName: '', done: function () {}});
        //获取对比类型
        uxutil.server.ajax({
            url: GET_ENUMTYPE_URL + '?classname=CompType&classnamespace=ZhiFang.Entity.LabStar'
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue),
                        html = [],
                        arr = ValueTypeMap["0"];
                    CompTypeData = data;
                    $.each(CompTypeData, function (i,item) {
                        if (arr.indexOf(item["Id"]) != -1)
                            html.push('<option value="' + item["Id"] + '">' + item["Name"]+'</option>');
                    });
                    $("#LBExpertRuleList_CompType").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //监听事件
    Class.prototype.initListeners = function () {
        var me = this;
        //触发行单击事件
        me.uxtable.table.on('row(' + me.config.tableId + ')', function (obj) {
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            obj.data.LBExpertRuleList_BRelatedItemValue = String(obj.data.LBExpertRuleList_BRelatedItemValue) == 'true' ? true : false;
            obj.data.LBExpertRuleList_BValue = String(obj.data.LBExpertRuleList_BValue) == 'true' ? true : false;
            obj.data.LBExpertRuleList_BLastHisComp = String(obj.data.LBExpertRuleList_BLastHisComp) == 'true' ? true : false;
            obj.data.LBExpertRuleList_BLimitHisDate = String(obj.data.LBExpertRuleList_BLimitHisDate) == 'true' ? true : false;
            CheckRowData = [];
            CheckRowData.push(obj.data);
            FormType = 'show';
            me.initFormType();
            me.ValueFieldTypeChangeHandle(obj.data.LBExpertRuleList_ValueFieldType);
            me.initItem(!obj.data.LBExpertRuleList_BRelatedItemValue ? SectionID : null);
            form.val("LBExpertRuleList_Form", obj.data);
        });
        //监听对比内容下拉框
        form.on('select(LBExpertRuleList_ValueFieldType)', function (data) {
            me.ValueFieldTypeChangeHandle(data.value);
            me.CreateRuleName();
            me.isEnableCalcFormulaByValueFieldType(data.value);
        });
        //监听相关检验结果复选
        form.on('checkbox(LBExpertRuleList_BRelatedItemValue)', function (data) {
            var checked = data.elem.checked;
            me.initItem(!checked && SectionID ? SectionID : null);
        });
        //监听对比判断下拉框
        form.on('select(LBExpertRuleList_CompType)', function (data) {
            me.CreateRuleName();
        });
        //监听对比值输入
        $("#LBExpertRuleList_CompValue").on('change', function () {
            me.CreateRuleName();
        });
        //监听计算公式输入
        $("#LBExpertRuleList_CalcFormula").on('change', function () {
            me.CreateRuleName();
        });
        //监听仅判断多少天内历史结果
        form.on('checkbox(LBExpertRuleList_BLimitHisDate)', function (data) {
            var checked = data.elem.checked;
            if (checked && !$("#LBExpertRuleList_LimitHisDate").val()) $("#LBExpertRuleList_LimitHisDate").val(2);
        });
        //新增规则按钮
        $("#AddRuleList").on('click', function () {
            me.onAddRuleList();
        });
        //编辑规则按钮
        $("#EditRuleList").on('click', function () {
            me.onEditRuleList();
        });
        //保存规则按钮
        form.on('submit(SaveRuleList)', function (data) {
            me.onSaveRuleList(data.field);
            return false;
        });
        //删除规则按钮
        $("#DelRuleList").on('click', function () {
            me.onDelRuleList();
        });
    };
    //查询处理
    Class.prototype.onSearch = function (LBExpertRuleID, LBExpertSectionID, LBItemID, LBItemName) {
        var me = this,
            url = GET_EXPERT_RULE_LIST_URL + "&where=lbexpertrulelist.LBExpertRule.Id=" + LBExpertRuleID + "&fields=" + me.getStoreFields(true).join() + "&sort=[{'property':'LBExpertRuleList_DispOrder','direction':'ASC'}]";

        ExpertRuleID = LBExpertRuleID;
        SectionID = LBExpertSectionID;
        ItemID = LBItemID;
        ItemName = LBItemName;

        me.uxtable.instance.reload({
            url: url,
            where: {
                t: new Date().getTime()
            }
        });
    };
    //默认选中第一行
    Class.prototype.onClickFirstRow = function () {
        var me = this;
        setTimeout(function () {
            $("#" + me.config.tableId + "+div").find('.layui-table-main tr[data-index="0"]').click();
        }, 0);

    };
    //获得列字段
    Class.prototype.getStoreFields = function (isString) {
        var me = this,
            columns = me.tableConfig["cols"][0] || [],
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

    //初始化对比项目  -- 只查询单项
    Class.prototype.initItem = function (sectionid) {
        var me = this,
            sectionid = sectionid || null,
            url = "";

        if (sectionid) {
            me.initSectionItemTableSelect(sectionid);
        } else {
            me.initItemTableSelect();
        }
    };
    //初始化小组项目下拉列表
    Class.prototype.initSectionItemTableSelect = function (sectionid) {
        var me = this,
            url = GET_SECTIONITEM_URL + "&where=lbsectionitem.LBItem.GroupType=0 and lbsectionitem.LBItem.IsUse=1 and lbsectionitem.LBSection.Id=" + sectionid +
                '&fields=LBSectionItem_Id,LBSectionItem_LBItem_Id,LBSectionItem_LBItem_CName,LBSectionItem_LBItem_SName,LBSectionItem_LBItem_Shortcode' +
                '&sort=[{property:"LBSectionItem_DispOrder",direction:"ASC"},{property:"LBSectionItem_LBItem_DispOrder",direction:"ASC"}]';
        if (!sectionid) return;

        $("#LBExpertRuleList_ItemName").val('');
        $("#LBExpertRuleList_ItemID").val('');
        tableSelect.render({
            elem: '#LBExpertRuleList_ItemName',	//定义输入框input对象 必填
            checkedKey: 'LBSectionItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbsectionitem.LBItem.CName,lbsectionitem.LBItem.SName,lbsectionitem.LBItem.Shortcode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/简称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
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
                    { field: 'LBSectionItem_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBSectionItem_LBItem_Id', width: 150, title: '项目主键ID', sort: false, hide: true },
                    { field: 'LBSectionItem_LBItem_CName', width: 200, title: '项目名称', sort: false },
                    { field: 'LBSectionItem_LBItem_SName', width: 80, title: '简称', sort: false },
                    { field: 'LBSectionItem_LBItem_Shortcode', width: 80, title: '快捷码', sort: false }
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
                    $(elem).val(record["LBSectionItem_LBItem_CName"]);
                    $("#LBExpertRuleList_ItemID").val(record["LBSectionItem_LBItem_Id"]);
                } else {
                    $(elem).val('');
                    $("#LBExpertRuleList_ItemID").val('');
                }
                me.CreateRuleName();
            }
        });
    };
    //初始化项目下拉列表
    Class.prototype.initItemTableSelect = function () {
        var me = this,
            url = GET_ITEM_URL + "&where=IsUse=1 and GroupType=0&fields=LBItem_Id,LBItem_CName,LBItem_SName,LBItem_Shortcode&sort=[{property:'LBItem_DispOrder',direction:'ASC'}]";

        $("#LBExpertRuleList_ItemName").val('');
        $("#LBExpertRuleList_ItemID").val('');
        tableSelect.render({
            elem: '#LBExpertRuleList_ItemName',	//定义输入框input对象 必填
            checkedKey: 'LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'CName,SName,Shortcode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/简称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
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
                    { field: 'LBItem_Id', width: 150, title: '项目主键ID', sort: false, hide: true },
                    { field: 'LBItem_CName', width: 200, title: '项目名称', sort: false },
                    { field: 'LBItem_SName', width: 80, title: '简称', sort: false },
                    { field: 'LBItem_Shortcode', width: 80, title: '快捷码', sort: false }
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
                    $(elem).val(record["LBItem_CName"]);
                    $("#LBExpertRuleList_ItemID").val(record["LBItem_Id"]);
                } else {
                    $(elem).val('');
                    $("#LBExpertRuleList_ItemID").val('');
                }
                //细则说明
                me.CreateRuleName();
            }
        });
    };
    //处理对比内容、对比值类型、对比类型联动关系
    Class.prototype.ValueFieldTypeChangeHandle = function (ValueFieldType) {
        var me = this,
            ValueFieldType = ValueFieldType || "0";

        //报告结果与历史报告值对比||结果状态与历史结果状态对比 禁用对比值
        if (ValueFieldType == 10 || ValueFieldType == 11) {
            $("#LBExpertRuleList_CompValue").prop("readonly", true);
            $("#LBExpertRuleList_CompValue").val('');
        } else {
            $("#LBExpertRuleList_CompValue").prop("readonly", false);
        }

        //修改对比值类型
        $("#LBExpertRuleList_ValueType").val(ValueFieldTypeMap[ValueFieldType]);
        //修改对比类型
        var CompTypeHtml = [], arr = ValueTypeMap[ValueFieldTypeMap[ValueFieldType]];
        $.each(CompTypeData, function (i, item) {
            if (arr.indexOf(item["Id"]) != -1)
                CompTypeHtml.push('<option value="' + item["Id"] + '">' + item["Name"] + '</option>');
        });
        $("#LBExpertRuleList_CompType").html(CompTypeHtml.join(''));
        form.render('select');
    };
    //生成规则明细说明
    Class.prototype.CreateRuleName = function (ValueFieldType, ItemName, CompType, CompValue) {
        var me = this,
            ValueFieldType = ValueFieldType || $("#LBExpertRuleList_ValueFieldType").val() || "0",
            ItemName = ItemName || $("#LBExpertRuleList_ItemName").val(),
            CompType = CompType || ($("#LBExpertRuleList_CompType>option:selected").length>0 ? $("#LBExpertRuleList_CompType>option:selected")[0].text : ""),
            CompValue = CompValue || $("#LBExpertRuleList_CompValue").val() || "对比值",
            CalcFormula = $("#LBExpertRuleList_CalcFormula").val(),
            RuleName = "";

        //不存在检验项目
        if (!ItemName) {
            $("#LBExpertRuleList_SysRuleName").val('');
            $("#LBExpertRuleList_RuleName").val('');
            return;
        }

        switch (ValueFieldType) {
            case "0":
            case "1":
            case "2":
                RuleName = ItemName + " " + CompType + " " + CompValue;
                break;
            case "3":
                RuleName = ItemName + " " + CompType;
                break;
            case "4":
                RuleName = CalcFormula ? CalcFormula + " " + CompType + " " + CompValue : "";
                break;
            case "5":
            case "6":
            case "7":
                RuleName = ItemName + " 历史结果 " + CompType + " " + CompValue;
                break;
            case "8":
                RuleName = ItemName + " 当前值-历史值之差 " + CompType + " " + CompValue;
                break;
            case "9":
                RuleName = ItemName + " 报告值/历史值 " + CompType + " " + CompValue;
                break;
            case "10":
                RuleName = ItemName + " 报告值 " + CompType + " 历史值";
                break;
            case "11":
                RuleName = ItemName + " 结果状态 " + CompType + " 历史结果状态";
                break;
            default:
                break;
        }
        if (RuleName != $("#LBExpertRuleList_SysRuleName").val()) {
            $("#LBExpertRuleList_SysRuleName").val(RuleName);
            $("#LBExpertRuleList_RuleName").val(RuleName);
        }
    };
    //根据对比内容判断对比值是否允许保存
    Class.prototype.isAllowSaveCompValue = function (ValueFieldType, CompValue) {
        var me = this,
            CompType = $("#LBExpertRuleList_CompType option:selected").text() || "",
            obj = true,
            msg = [];
        switch (ValueFieldType) {
            case "0":
            case "4":
            case "5":
            case "8":
            case "9":
                if (!CompValue) msg.push('请填写对比值!');
                if (CompValue && CompValue.indexOf(",") == -1 && CompValue.indexOf("，") == -1 && isNaN(CompValue)) msg.push('请填写正确的定量结果!');
                //存在两个值 最多两个值
                if (CompValue && (CompValue.indexOf(",") != -1 || CompValue.indexOf("，") != -1)) {
                    if (CompType.indexOf(",") != -1 || CompType.indexOf("，") != -1) {
                        var list = CompValue.indexOf(",") != -1 ? CompValue.split(",") : CompValue.split("，");
                        if (isNaN(list[0]) || isNaN(list[1])) {
                            msg.push('对比值中存在非定量结果!');
                        } else {
                            obj = { CompValue: list[0] + "," + list[1] };
                            obj["CompFValue"] = list[0];
                            obj["CompFValue2"] = list[1];
                        }
                    } else {
                        msg.push('对比值与对比判断不匹配!');
                    }
                } else {
                    if (CompType.indexOf(",") != -1 || CompType.indexOf("，") != -1) {
                        msg.push('对比值与对比判断不匹配!');
                    } else {
                        obj = { CompValue: CompValue };
                        obj["CompFValue"] = CompValue;
                        obj["CompFValue2"] = null;
                    } 

                }
                break;
            case "1":
            case "2":
            case "6":
            case "7":
                if (!CompValue) msg.push('请填写对比值!');
                break;
            case "3":
            case "10":
            case "11":
                break;
            default:
                break;
        }

        if (msg.length > 0) {
            uxbase.MSG.onWarn(msg.join(''));
            return false;
        }
        return obj;
    };
    //根据对比内容判断是否启用计算公式
    Class.prototype.isEnableCalcFormulaByValueFieldType = function (ValueFieldType) {
        var me = this,
            ValueFieldType = ValueFieldType || $("#LBExpertRuleList_ValueFieldType").val();
        //项目计算结果 启用计算公式
        if (ValueFieldType == 4)
            me.isEnableElem(true, "LBExpertRuleList_CalcFormula");
        else
            me.isEnableElem(false, "LBExpertRuleList_CalcFormula");
    };
    

    //获得保存数据参数
    Class.prototype.onGetSaveParams = function (data) {
        var me = this,
            entity = JSON.parse(JSON.stringify(data).replace(/LBExpertRuleList_/g, '')),
            entitys = {};
        //处理数据
        if (ExpertRuleID && FormType == 'add') entity.LBExpertRule = { Id: ExpertRuleID, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
        //相关检验结果
        entity.BRelatedItemValue = $("#LBExpertRuleList_BRelatedItemValue").prop('checked');
        //对比内容存在
        entity.BValue = $("#LBExpertRuleList_BValue").prop('checked');
        //仅判断最近一次历史结果
        entity.BLastHisComp = $("#LBExpertRuleList_BLastHisComp").prop('checked');
        //限制历史对比日期
        entity.BLimitHisDate = $("#LBExpertRuleList_BLimitHisDate").prop('checked');

        //限制历史对比日期选中则必填天数
        if (entity.BLimitHisDate) {
            if (!entity.LimitHisDate || isNaN(entity.LimitHisDate)) {
                uxbase.MSG.onWarn("限制历史对比日期天数未填写!");
                return false;
            }
        } else {
            entity.LimitHisDate = null;
        }
        //对比内容为项目计算结果则计算公式不能为空
        if (entity.ValueFieldType == 4) {
            if (!entity.CalcFormula) {
                uxbase.MSG.onWarn("对比内容为项目计算结果时，计算公式不能为空!");
                return false;
            }
        }
        //对比值保存判断
        var isAllowSave = me.isAllowSaveCompValue(entity.ValueFieldType, entity.CompValue);
        if (!isAllowSave) return isAllowSave;
        if (isAllowSave && typeof isAllowSave == "object") {
            entity = $.extend({}, entity, isAllowSave);
        }

        var fields = [];
        for (var i in entity) {
            if (entity[i] != null && typeof entity[i] == "object") continue;
            if (typeof entity[i] == "string") entity[i] = entity[i].trim();
            fields.push(i);
        }
        //编辑处理
        if (FormType == 'edit') {
            entity.Id = CheckRowData[0]["LBExpertRuleList_Id"];
            fields.push('Id');
            entitys = { entity: entity, fields: fields.join() };
        } else {
            entitys = { entity: entity };
        }

        return entitys;
    };

    //新增规则
    Class.prototype.onAddRuleList = function () {
        var me = this;
        FormType = 'add';
        me.onClear();
        me.initFormType();
        $("#LBExpertRuleList_DispOrder").val(me.uxtable.table.cache[me.config.tableId].length || 1);
        me.initItem(SectionID);
        me.isEnableCalcFormulaByValueFieldType();
        if (ItemID) $("#LBExpertRuleList_ItemID").val(ItemID);
        if (ItemName) $("#LBExpertRuleList_ItemName").val(ItemName);
        me.CreateRuleName();
    };
    //编辑规则
    Class.prototype.onEditRuleList = function () {
        var me = this;
        if (CheckRowData.length == 0) return;
        //之前是新增 则需要重新赋值
        if (FormType == 'add') {
            me.ValueFieldTypeChangeHandle(CheckRowData[0].LBExpertRuleList_ValueFieldType);
            me.initItem(!CheckRowData[0].LBExpertRuleList_BRelatedItemValue ? SectionID : null);
            form.val("LBExpertRuleList_Form", CheckRowData[0]);
        }
        FormType = 'edit';
        me.initFormType();
        me.isEnableCalcFormulaByValueFieldType();
    };
    //保存规则
    Class.prototype.onSaveRuleList = function (data) {
        var me = this;
        if (!ExpertRuleID) {
            uxbase.MSG.onWarn('请先选择一条专家规则!');
            return;
        }
        var url = FormType == 'add' ? ADD_EXPERT_RULE_LIST_URL : EDIT_EXPERT_RULE_LIST_URL;
        var params = me.onGetSaveParams(data);
        if (!url || !params) return;

        var load = layer.load(),//显示遮罩层
            config = {
                type: "POST",
                url: url,
                data: JSON.stringify(params)
            };
        uxutil.server.ajax(config, function (res) {
            //隐藏遮罩层
            layer.close(load);
            if (res.success) {
                uxbase.MSG.onSuccess("保存成功!");
                if (FormType == 'add')
                    TablePositionDataId = res.value.id;
                else if (FormType == 'edit')
                    TablePositionDataId = params.entity.Id;
                me.onSearch(ExpertRuleID);
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //删除规则
    Class.prototype.onDelRuleList = function () {
        var me = this;
        if (CheckRowData.length == 0) return;
        var url = DEL_EXPERT_RULE_LIST_URL + "?id=" + CheckRowData[0]["LBExpertRuleList_Id"];
        uxutil.server.ajax({ url, url }, function (res) {
            if (res.success) {
                uxbase.MSG.onSuccess("删除成功!");
                me.onSearch(ExpertRuleID);
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };

    //清空表单
    Class.prototype.onClear = function () {
        var me = this;
        $("#LBExpertRuleList_Form :input").each(function (i, item) {
            $(this).val('');
        });
        //默认值
        $("#LBExpertRuleList_BRelatedItemValue").prop("checked", false);
        $("#LBExpertRuleList_BValue").prop("checked", true);
        $("#LBExpertRuleList_BLastHisComp").prop("checked", false);
        $("#LBExpertRuleList_BLimitHisDate").prop("checked", false);
        $("#LBExpertRuleList_ValueFieldType").val("0");
        me.ValueFieldTypeChangeHandle("0");
        me.isEnableCalcFormulaByValueFieldType();
        form.render();
    };
    //禁用表单处理
    Class.prototype.SetDisabled = function (isDisabled, FormID) {
        var me = this;
        if (!FormID) return;
        $("#" + FormID + " :input").each(function (i, item) {
            if ($(item)[0].nodeName == 'BUTTON') return true;
            $(item).attr("disabled", isDisabled);
            if (isDisabled) {
                if (!$(item).hasClass("layui-disabled")) $(item).addClass("layui-disabled");
            } else {
                if ($(item).hasClass("layui-disabled")) $(item).removeClass("layui-disabled");
            }
        });
        form.render();
    };
    //是否启用某元素
    Class.prototype.isEnableElem = function(bo, elemID) {
        var me = this;
        if (!elemID) return;
        if (bo)
            $("#" + elemID).removeClass("layui-btn-disabled").removeAttr('disabled', true);
        else
            $("#" + elemID).addClass("layui-btn-disabled").attr('disabled', true);
    };
    //表单状态
    Class.prototype.initFormType = function() {
        var me = this;
        $("#LBExpertRuleList_FormType").html(FormType == 'add' ? '新增' : (FormType == 'edit' ? '编辑' : '查看'));
        switch (FormType) {
            case "add":
                me.SetDisabled(false, "LBExpertRuleList_Form");
                me.isEnableElem(true, "SaveRuleList");
                me.isEnableElem(false, "DelRuleList");
                break;
            case "edit":
                me.SetDisabled(false, "LBExpertRuleList_Form");
                me.isEnableElem(true, "SaveRuleList");
                me.isEnableElem(true, "DelRuleList");
                break;
            default:
                me.SetDisabled(true, "LBExpertRuleList_Form");
                me.isEnableElem(false, "SaveRuleList");
                me.isEnableElem(true, "DelRuleList");
                break;
        }
    };

    //核心入口
    list.render = function (options) {
        var me = new Class(options);

        if (!me.config.tableId) {
            window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.tableId缺失！");
            return me;
        }
        //初始化HTML
        me.initHtml();
        //实例化列表
        me.uxtable = uxtable.render(me.tableConfig);
        //监听事件
        me.initListeners();

        return me;
    };

    //暴露接口
    exports(MOD_NAME, list);
});