/**
   @Name：专家规则
   @Author：zhangda
   @version 2021-11-26
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base',
    uxtable: 'ux/table',
    tableSelect: '../src/tableSelect/tableSelect',
    CommonSelectEnum: 'modules/common/select/enum',
    list:'app/dic/expertrule/list'
}).use(['uxutil', 'uxbase', 'uxtable', 'form', 'table', 'laydate', 'element', 'tableSelect','CommonSelectEnum','list'], function () {
    "use strict";
    var $ = layui.$,
        layer = layui.layer,
        form = layui.form,
        table = layui.table,
        tableSelect = layui.tableSelect,
        laydate = layui.laydate,
        element = layui.element,
        CommonSelectEnum = layui.CommonSelectEnum,
        list = layui.list,
        uxtable = layui.uxtable,
        uxbase = layui.uxbase,
        uxutil = layui.uxutil;

    //外部参数
    var PARAMS = uxutil.params.get(true);

    //小组获取服务地址
    var GET_SECTION_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';
    //小组项目服务地址
    var GET_SECTIONITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true';
    //检验仪器服务地址
    var GET_EQUIP_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true';
    //检验科室服务地址
    var GET_DEPT_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true';
    //样本类型服务地址
    var GET_SAMPLETYPE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true';
    //临床诊断服务地址
    var GET_DIAG_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDiagByHQL?isPlanish=true';
    //生理期
    var GET_PHYPERIOD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhyPeriodByHQL?isPlanish=true';
    //采样部位服务地址
    var GET_COLLECTPART_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBCollectPartByHQL?isPlanish=true';
    //获取指定实体字段的最大号
    var GET_MAXNO_BYENTITYFIELD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField';

    //获取专家规则服务地址
    var GET_EXPERT_RULE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBExpertRuleByHQL?isPlanish=true';
    //新增专家规则服务地址
    var ADD_EXPERT_RULE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBExpertRule';
    //编辑专家规则服务地址
    var EDIT_EXPERT_RULE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBExpertRuleByField';
    //删除专家规则服务地址
    var DEL_EXPERT_RULE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBExpertRule';

    //当前页签
    var CurrentTab = 0;

    //警示级别Map
    var AlarmLevelMap = {
        '0': { type: 'other', text: '警示', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-1.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '1': { type: 'other', text: '警告', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/1-2.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '2': { type: 'other', text: '严重警告', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-4.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '3': { type: 'other', text: '危急', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-6.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
        '4': { type: 'other', text: '错误', iconText: "<img width='14' style='vertical-align: text-bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-5.png' />", color: '#FFFFFF', background: 'transparent', border: 'none', padding: '0' },
    };

    //规则明细实例
    var ListInstance = null;

    //列表实例
    var TableInstance = null;
    //列表初始配置
    var TableConfig = null;
    //列表id
    var TableID = "maintable";
    //当前行数据
    var CheckRowData = [];
    //列表定位数据ID
    var TablePositionDataId = null;
    //表单状态
    var FormType = 'add';
    //初始化小组下拉内容
    function initSection() {
        var html = [],
            firstsectionid = null,
            url = GET_SECTION_URL + "&where=IsUse=1&fields=LBSection_Id,LBSection_CName&sort=[{'property':'LBSection_DispOrder','direction':'ASC'}]";

        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = $.parseJSON(res.ResultDataValue).list;
                    $.each(list, function (i, item) {
                        if (i == 0) firstsectionid = item["LBSection_Id"];
                        html.push('<option value="' + item["LBSection_Id"] + '">' + item["LBSection_CName"]+'</option>');
                    });
                    $("#LBExpertRule_LBSection_Id").html(html.join(''));
                    html.unshift('<option value="">全部</option>');
                    $("#SectionFilter").html(html.join(''));
                    initItem(firstsectionid);
                    form.render('select');
                }
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //初始化项目
    function initItem(sectionid) {
        var sectionid = sectionid || null,
            url = GET_SECTIONITEM_URL + "&where=lbsectionitem.LBItem.GroupType=0 and lbsectionitem.LBItem.IsUse=1 and lbsectionitem.LBSection.Id=" + sectionid +
                '&fields=LBSectionItem_Id,LBSectionItem_LBItem_Id,LBSectionItem_LBItem_CName,LBSectionItem_LBItem_SName,LBSectionItem_LBItem_Shortcode'+
                '&sort=[{property:"LBSectionItem_DispOrder",direction:"ASC"},{property:"LBSectionItem_LBItem_DispOrder",direction:"ASC"}]' +
                "&t=" + new Date().getTime();
        if (!sectionid) url = "";
        $("#LBExpertRule_LBItem_CName").val('');
        $("#LBExpertRule_LBItem_Id").val('');
        tableSelect.render({
            elem: '#LBExpertRule_LBItem_CName',	//定义输入框input对象 必填
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
                    $("#LBExpertRule_LBItem_Id").val(record["LBSectionItem_LBItem_Id"]);
                } else {
                    $(elem).val('');
                    $("#LBExpertRule_LBItem_Id").val('');
                }
            }
        });
    };
    //初始化检验仪器
    function initEquip() {
        var url = GET_EQUIP_URL + "&where=IsUse=1" +
            "&fields=LBEquip_Id,LBEquip_CName" +
            "&sort=[{ property: 'LBEquip_DispOrder', direction: 'ASC' }]";

        uxutil.server.ajax({ url: url }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = JSON.parse(res.ResultDataValue).list;
                    var html = ['<option value="">请选择</option>'];
                    $.each(list, function (i, item) {
                        html.push('<option value="' + item["LBEquip_Id"] + '">' + item["LBEquip_CName"] + '</option>');
                    });
                    $("#LBExpertRule_EquipID").html(html.join(''));
                    form.render('select');
                }
            } else {
                layer.msg("仪器查询失败！", { icon: 5, anim: 6 });
            }
        });
    };
    //初始化送检科室
    function initDept() {
        var url = GET_DEPT_URL + "&where=hrdeptidentity.IsUse=1 and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.TSysCode='1001101'" +
            "&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_UseCode" +
            "&sort=[{ property: 'HRDeptIdentity_DispOrder', direction: 'ASC' }]";

        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = JSON.parse(res.ResultDataValue).list,
                        html = ['<option value="">请选择</option>'];
                    $.each(list, function (i, item) {
                        html.push('<option value="' + item["HRDeptIdentity_HRDept_Id"] + '">' + item["HRDeptIdentity_HRDept_CName"] + '</option>');
                    });
                    $("#LBExpertRule_DeptID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化样本类型
    function initSampleType() {
        var url = GET_SAMPLETYPE_URL + "&where=IsUse=1" +
            "&fields=LBSampleType_Id,LBSampleType_CName" +
            "&sort=[{ property: 'LBSampleType_DispOrder', direction: 'ASC' }]";

        uxutil.server.ajax({ url: url }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = JSON.parse(res.ResultDataValue).list;
                    var html = ['<option value="">请选择</option>'];
                    $.each(list, function (i, item) {
                        html.push('<option value="' + item["LBSampleType_Id"] + '">' + item["LBSampleType_CName"] + '</option>');
                    });
                    $("#LBExpertRule_SampleTypeID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化临床诊断
    function initDiag() {
        var url = GET_DIAG_URL + "&where=IsUse=1" +
            "&fields=LBDiag_Id,LBDiag_CName" +
            "&sort=[{ property: 'LBDiag_DispOrder', direction: 'ASC' }]";

        uxutil.server.ajax({ url: url }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = JSON.parse(res.ResultDataValue).list;
                    var html = ['<option value="">请选择</option>'];
                    $.each(list, function (i, item) {
                        html.push('<option value="' + item["LBDiag_Id"] + '">' + item["LBDiag_CName"] + '</option>');
                    });
                    $("#LBExpertRule_DiagID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化生理期
    function initPhyPeriod() {
        var url = GET_PHYPERIOD_URL + "&where=IsUse=1" +
            "&fields=LBPhyPeriod_Id,LBPhyPeriod_CName" +
            "&sort=[{ property: 'LBPhyPeriod_DispOrder', direction: 'ASC' }]";

        uxutil.server.ajax({ url: url }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = JSON.parse(res.ResultDataValue).list;
                    var html = ['<option value="">请选择</option>'];
                    $.each(list, function (i, item) {
                        html.push('<option value="' + item["LBPhyPeriod_Id"] + '">' + item["LBPhyPeriod_CName"] + '</option>');
                    });
                    $("#LBExpertRule_PhyPeriodID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化采样部位
    function initCollectPart() {
        var url = GET_COLLECTPART_URL + "&where=IsUse=1" +
            "&fields=LBCollectPart_Id,LBCollectPart_CName" +
            "&sort=[{ property: 'LBCollectPart_DispOrder', direction: 'ASC' }]";

        uxutil.server.ajax({ url: url }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var list = JSON.parse(res.ResultDataValue).list;
                    var html = ['<option value="">请选择</option>'];
                    $.each(list, function (i, item) {
                        html.push('<option value="' + item["LBCollectPart_Id"] + '">' + item["LBCollectPart_CName"] + '</option>');
                    });
                    $("#LBExpertRule_CollectPartID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化采样时间
    function initCollectTime() {
        //采样开始时间
        laydate.render({ elem: '#LBExpertRule_BCollectTime', type: 'time' });
        //采样结束时间
        laydate.render({ elem: '#LBExpertRule_ECollectTime', type: 'time' });
    };
    //获得保存数据参数
    function onGetSaveParams(data) {
        var entity = JSON.parse(JSON.stringify(data).replace(/LBExpertRule_/g, '')),
            entitys = {};
        //处理数据
        //是否采用
        entity.IsUse = $("#LBExpertRule_IsUse").prop('checked');
        //检验小组
        if (entity.LBSection_Id) entity.LBSection = { Id: entity.LBSection_Id || null, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
        //检验项目
        if (entity.LBItem_Id) entity.LBItem = { Id: entity.LBItem_Id || null, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
        //检验仪器
        entity.EquipID = entity.EquipID != "" ? entity.EquipID : null;
        //送检科室
        entity.DeptID = entity.DeptID != "" ? entity.DeptID : null;
        //生理期
        entity.PhyPeriodID = entity.PhyPeriodID != "" ? entity.PhyPeriodID : null;
        //采样部位
        entity.CollectPartID = entity.CollectPartID != "" ? entity.CollectPartID : null;
        //样本类型
        entity.SampleTypeID = entity.SampleTypeID != "" ? entity.SampleTypeID : null;
        //临床诊断
        entity.DiagID = entity.DiagID != "" ? entity.DiagID : null;
        //性别
        entity.GenderID = entity.GenderID != "" ? entity.GenderID : null;
        //年龄单位
        entity.AgeUnitID = entity.AgeUnitID != "" ? entity.AgeUnitID : null;
        //年龄低限
        entity.LowAge = entity.LowAge != "" ? entity.LowAge : null;
        //年龄高限
        entity.HighAge = entity.HighAge != "" ? entity.HighAge : null;
        //采样开始时间
        entity.BCollectTime = entity.BCollectTime != "" ? entity.BCollectTime : null;
        //采样结束时间
        entity.ECollectTime = entity.ECollectTime != "" ? entity.ECollectTime : null;
        //条件说明
        entity.ConditionName = setConditionName();

        delete entity.LBSection_Id;
        delete entity.LBItem_CName;
        delete entity.LBItem_Id;
        delete entity.RuleDetialExplain;
        delete entity.LBExpertRule_ExpertRuleListInfo;
        
        var fields = [];
        for (var i in entity) {
            if (entity[i] != null && typeof entity[i] == "object") continue;
            if (typeof entity[i] == "string") entity[i] = entity[i].trim();
            fields.push(i);
        }
        //编辑处理
        if (FormType == 'edit') {
            entity.Id = CheckRowData[0]["LBExpertRule_Id"];
            fields.push('Id');
            if (entity.LBSection && entity.LBSection.Id) {
                fields.push('LBSection_Id');
                fields.push('LBSection_DataTimeStamp');
            }
            if (entity.LBItem && entity.LBItem.Id) {
                fields.push('LBItem_Id');
                fields.push('LBItem_DataTimeStamp');
            }
            entitys = { entity: entity, fields: fields.join() };
        } else {
            entitys = { entity:entity };
        }

        return entitys;
    };
    //复制规则
    function onCopyRule() {
        layer.open({
            type: 2,
            area: ['600px', '450px'],
            fixed: false,
            maxmin: false,
            title: '复制专家规则',
            content: 'copy/index.html',
            cancel: function (index, layero) {
                layer.close(index);
            }
        });
    };
    //新增规则
    function onAddRule() {
        FormType = 'add';
        onClear();
        initFormType();
        if ($("#SectionFilter").val()) $("#LBExpertRule_LBSection_Id").val($("#SectionFilter").val());
        initItem($("#LBExpertRule_LBSection_Id").val());
        onGetMaxNoByEntityField('LBExpertRule', 'DispOrder', function (value) { $("#LBExpertRule_DispOrder").val(value || 0); });
        form.render();
    };
    //编辑规则
    function onEditRule() {
        if (CheckRowData.length == 0) return;
        if (FormType == 'add') {
            form.val("LBExpertRule_Form", CheckRowData[0]);
            var ExpertRuleListInfo = CheckRowData[0]["LBExpertRule_ExpertRuleListInfo"];
            if (ExpertRuleListInfo.indexOf(',') != -1) {
                $("#LBExpertRule_ExpertRuleListInfo").val(ExpertRuleListInfo.replace(/,/g, '/r/n'));
            }
        }
        FormType = 'edit';
        initFormType();
    };
    //保存规则
    function onSaveRule(data) {
        var url = FormType == 'add' ? ADD_EXPERT_RULE_URL : EDIT_EXPERT_RULE_URL;
        var params = onGetSaveParams(data);
        if (!url || !params) return;
        update(url, params, function (res) {
            if (res.success) {
                uxbase.MSG.onSuccess("保存成功!");
                if (FormType == 'add') 
                    TablePositionDataId = res.value.id;
                else if (FormType == 'edit')
                    TablePositionDataId = params.entity.Id;
                onSearch();
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //删除规则
    function onDelRule() {
        if (CheckRowData.length == 0) return;
        var url = DEL_EXPERT_RULE_URL + "?id=" + CheckRowData[0]["LBExpertRule_Id"];
        uxutil.server.ajax({ url, url }, function (res) {
            if (res.success) {
                uxbase.MSG.onSuccess("删除成功!");
                onSearch();
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //清空表单
    function onClear() {
        $("#LBExpertRule_Form :input").each(function (i,item) {
            $(this).val('');
        });
        //默认值
        $("#LBExpertRule_LBSection_Id").val($("#LBExpertRule_LBSection_Id>option").eq(0).attr("value"));
        $("#LBExpertRule_AlarmLevel").val($("#LBExpertRule_AlarmLevel>option").eq(0).attr("value"));
        $("#LBExpertRule_ResultType").val($("#LBExpertRule_ResultType>option").eq(0).attr("value"));
        $("#LBExpertRule_RuleRelation").val($("#LBExpertRule_RuleRelation>option").eq(0).attr("value"));
        $("#LBExpertRule_IsUse").prop("checked", true);
        form.render();
    };
    //禁用表单处理
    function SetDisabled(isDisabled, FormID) {
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
    function isEnableElem(bo, elemID) {
        if (!elemID) return;
        if (bo)
            $("#" + elemID).removeClass("layui-btn-disabled").removeAttr('disabled', true);
        else
            $("#" + elemID).addClass("layui-btn-disabled").attr('disabled', true);
    };
    //表单状态
    function initFormType() {
        $("#LBExpertRule_FormType").html(FormType == 'add' ? '新增' : (FormType == 'edit' ? '编辑' : '查看'));
        switch (FormType) {
            case "add":
                SetDisabled(false, "LBExpertRule_Form");
                isEnableElem(true, "SaveRule");
                isEnableElem(false, "DelRule");
                break;
            case "edit":
                SetDisabled(false, "LBExpertRule_Form");
                isEnableElem(true, "SaveRule");
                isEnableElem(true, "DelRule");
                break;
            default:
                SetDisabled(true,"LBExpertRule_Form");
                isEnableElem(false, "SaveRule");
                isEnableElem(true, "DelRule");
                break;
        }
    };
    //生成条件说明
    function setConditionName() {
        var ConditionName = "";
        //检验小组
        var Section = $("#LBExpertRule_LBSection_Id>option:selected");
        if (Section.length > 0 && Section[0].value) ConditionName += "检验小组=" + Section[0].text + " ";
        //检验仪器
        var Equip = $("#LBExpertRule_EquipID>option:selected");
        if (Equip.length > 0 && Equip[0].value) ConditionName += "检验仪器=" + Equip[0].text + " ";
        //样本类型
        var SampleType = $("#LBExpertRule_SampleTypeID>option:selected");
        if (SampleType.length > 0 && SampleType[0].value) ConditionName += "样本类型=" + SampleType[0].text + " ";
        //性别
        var Gender = $("#LBExpertRule_GenderID>option:selected");
        if (Gender.length > 0 && Gender[0].value) ConditionName += "性别=" + Gender[0].text + " ";
        //年龄 + 年龄单位
        var LowAge = $("#LBExpertRule_LowAge").val();
        var HighAge = $("#LBExpertRule_HighAge").val();
        var AgeUnitID = $("#LBExpertRule_AgeUnitID>option:selected");
        if (LowAge != "" && HighAge != "") {
            ConditionName += "年龄=" + LowAge + " - " + HighAge;
        } else if (LowAge == "" && HighAge != "") {
            ConditionName += "年龄<=" + HighAge;
        } else if (LowAge != "" && HighAge == "") {
            ConditionName += "年龄>=" + LowAge;
        }
        if (AgeUnitID != "") {
            if (LowAge != "" || HighAge != "") {
                if (AgeUnitID.length > 0 && AgeUnitID[0].value) ConditionName += AgeUnitID[0].text + " ";
            }
        }
        //采样开始时间
        var BCollectTime = $("#LBExpertRule_BCollectTime").val();
        if (BCollectTime != "") ConditionName += "采样开始时间=" + BCollectTime + " ";
        //采样截止时间
        var ECollectTime = $("#LBExpertRule_ECollectTime").val();
        if (ECollectTime != "") ConditionName += "采样截止时间=" + ECollectTime + " ";
        return ConditionName;
    };
    //获得最大显示次序
    function onGetMaxNoByEntityField(entityName, entityField, callback) {
        var entityName = entityName || null,
            entityField = entityField || null,
            url = GET_MAXNO_BYENTITYFIELD_URL + '?entityName=' + entityName + '&entityField=' + entityField;
        if (!entityName || !entityField) return;

        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    if (typeof callback == 'function') callback(res.ResultDataValue);
                }
            }
        });
    };
    //初始化主列表
    function initTable() {
        TableConfig = {
            elem: '#' + TableID,
            height: 'full-40',
            url: '',
            toolbar: '',
            defaultToolbar: [],
            page: false,
            limit: 1000,
            limits: [100, 200, 500, 1000, 1500],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号' },
                { field: 'LBExpertRule_Id', width: 140, title: '主键', hide: true },
                { field: 'LBExpertRule_LBSection_Id', width: 100, title: '检验小组',hide:true },
                { field: 'LBExpertRule_LBSection_CName', width: 100, title: '检验小组' },
                { field: 'LBExpertRule_GName', width: 100, title: '规则组' },
                {
                    field: 'LBExpertRule_CName', minWidth: 100, title: '规则名称',
                    templet: function (data) {
                        var str = data["LBExpertRule_CName"];
                        if (data["LBExpertRule_AlarmLevel"]) str = AlarmLevelMap[data["LBExpertRule_AlarmLevel"]]["iconText"] + " " + str;
                        return str;
                    }
                },
                { field: 'LBExpertRule_DispOrder', width: 60, title: '显示次序' },
                { field: 'LBExpertRule_IsUse', width: 60, title: '采用', align: 'center', templet: '#IsUseTp' },
                { field: 'LBExpertRule_ItemAlarmInfo', width: 120, title: '项目提示' },
                { field: 'LBExpertRule_ExpertRuleListInfo', width: 200, title: '规则明细' },
                //表单信息
                { field: 'LBExpertRule_LBItem_Id', width: 100, title: '检验项目ID', hide: true },
                { field: 'LBExpertRule_LBItem_CName', width: 100, title: '检验项目', hide: true },
                { field: 'LBExpertRule_AlarmLevel', width: 100, title: '警示级别', hide: true },
                { field: 'LBExpertRule_ResultType', width: 100, title: '结果类型', hide: true },
                { field: 'LBExpertRule_RuleRelation', width: 100, title: '规则内关系', hide: true },
                { field: 'LBExpertRule_ItemAlarmInfo', width: 100, title: '项目警示', hide: true },
                { field: 'LBExpertRule_AlarmInfo', width: 100, title: '检验单警示', hide: true },
                { field: 'LBExpertRule_Comment', width: 100, title: '规则备注', hide: true },
                { field: 'LBExpertRule_EquipID', width: 100, title: '检验仪器', hide: true },
                { field: 'LBExpertRule_DeptID', width: 100, title: '送检科室', hide: true },
                { field: 'LBExpertRule_SampleTypeID', width: 100, title: '样本类型', hide: true },
                { field: 'LBExpertRule_DiagID', width: 100, title: '临床诊断', hide: true },
                { field: 'LBExpertRule_PhyPeriodID', width: 100, title: '生理期', hide: true },
                { field: 'LBExpertRule_CollectPartID', width: 100, title: '采样部位', hide: true },
                { field: 'LBExpertRule_BCollectTime', width: 100, title: '采样开始时间', hide: true },
                { field: 'LBExpertRule_ECollectTime', width: 100, title: '采样结束时间', hide: true },
                { field: 'LBExpertRule_GenderID', width: 100, title: '性别', hide: true },
                { field: 'LBExpertRule_LowAge', width: 100, title: '年龄低限', hide: true },
                { field: 'LBExpertRule_HighAge', width: 100, title: '年龄高限', hide: true },
                { field: 'LBExpertRule_AgeUnitID', width: 100, title: '年龄单位', hide: true }
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                if (count == 0) {
                    CheckRowData = [];
                    onAddRule();
                    if (CurrentTab == 1) ListInstance.onSearch(null, $("#SectionFilter").val(),null,null);
                    return;
                }
                if (TablePositionDataId) {
                    var index = null;
                    $.each(res.data, function (i,item) {
                        if (item["LBExpertRule_Id"] == TablePositionDataId) {
                            index = i;
                            return false;
                        }
                    });
                    if (index != null) {
                        $("#" + TableID + "+div").find('.layui-table-main tr[data-index="' + index + '"]').click();
                        if (document.querySelector("#" + TableID+"+div .layui-table-body table.layui-table tbody tr:nth-child(" + (index+1) + ")").length > 0)
                            document.querySelector("#" + TableID + "+div .layui-table-body table.layui-table tbody tr:nth-child(" + (index + 1) + ")").scrollIntoView(false, { behavior: "smooth" });
                    }
                    TablePositionDataId = null;
                } else {
                    $("#" + TableID + "+div").find('.layui-table-main tr[data-index="0"]').click();
                }
            }
        };

        TableConfig.url = getTableUrl();

        TableInstance = uxtable.render(TableConfig);
        //初始化监听
        initListeners();
    };
    //查询
    function onSearch() {
        var url = getTableUrl();
        TableInstance.table.reload(TableID,{
            url: url,
            where: {
                t: new Date().getTime()
            }
        });
    };
    //获取主列表查询Url
    function getTableUrl() {
        var url = GET_EXPERT_RULE_URL,
            where = [],
            fields = getStoreFields(true),
            sort = [{ "property": "LBExpertRule_LBSection_DispOrder", "direction": "ASC" }, { "property": "LBExpertRule_GName", "direction": "ASC" }, { "property": "LBExpertRule_DispOrder", "direction": "ASC" }],
            sectionid = $("#SectionFilter").val();

        if (sectionid)
            where.push("lbexpertrule.LBSection.Id='" + sectionid + "'");

        if (where.length > 0)
            url += "&where=" + where.join(" and ");
        if (fields.length > 0)
            url += "&fields=" + fields.join();
        if (sort.length > 0)
            url += "&sort=" + JSON.stringify(sort);

        return url;
    };
    //获取查询Fields
    function getStoreFields(isString) {
        var me = this,
            tableIns = TableInstance,
            columns = tableIns ? (tableIns.config.cols[0] || []) : (TableConfig.cols[0] || []),
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
    //更新数据-post
    function update(url, entity, callback) {
        var params = JSON.stringify(entity),
            load = layer.load(),//显示遮罩层
            config = {
                type: "POST",
                url: url,
                data: params
            };
        uxutil.server.ajax(config, function (res) {
            //隐藏遮罩层
            layer.close(load);
            callback && callback(res);
        })
    };
    //设置表单高度
    function setDomHeight() {
        var winHeight =$(window).height();
        
        $("#tab").css("height", (winHeight-12)+"px");
    };
    //初始化页面
    function initHtml() {
        //检验小组
        initSection();
        //检验仪器
        initEquip();
        //送检科室
        initDept();
        //样本类型
        initSampleType();
        //临床诊断
        initDiag();
        //生理期
        initPhyPeriod();
        //采样部位
        initCollectPart();
        //警示级别
        CommonSelectEnum.render({ domId: 'LBExpertRule_AlarmLevel', classNameSpace: 'ZhiFang.Entity.LabStar', className: 'TestFormReportValueAlarmLevel', defaultName: '', done: function () { } });
        //性别
        CommonSelectEnum.render({ domId: 'LBExpertRule_GenderID', classNameSpace: 'ZhiFang.Entity.LabStar', className: 'GenderType', defaultName: '请选择', done: function () { } });
        //年龄单位
        CommonSelectEnum.render({ domId: 'LBExpertRule_AgeUnitID', classNameSpace: 'ZhiFang.Entity.LabStar', className: 'AgeUnitType', defaultName: '请选择', done: function () { } });
        //采样时间
        initCollectTime();
        //主列表
        initTable();
        //初始化规则明细
        ListInstance = list.render({ tableId: 'expertrulelisttable', height: 'full-340' });
    };
    //监听事件
    function initListeners() {
        var iTime = null;
        // 窗体大小改变时，调整高度显示
        $(window).resize(function () {
            clearTimeout(iTime);
            iTime = setTimeout(function () {
                setDomHeight();
            }, 500);
        });
        //input后存在icon 则点击该icon 等同于点击input
        $(document).on('click', 'input.layui-input+.layui-icon', function () {
            $(this).prev('input.layui-input')[0].click();
            return false;//不加的话 不能弹出
        });
        //单击行事件
        TableInstance.table.on('row(' + TableID + ')', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            obj.data.LBExpertRule_IsUse = String(obj.data.LBExpertRule_IsUse) == 'true' ? true : false;
            CheckRowData = [];
            CheckRowData.push(obj.data);
            FormType = 'show';
            initFormType();
            initItem(obj.data["LBExpertRule_LBSection_Id"]);
            form.val("LBExpertRule_Form", obj.data);
            var ExpertRuleListInfo = CheckRowData[0]["LBExpertRule_ExpertRuleListInfo"];
            if (ExpertRuleListInfo.indexOf(',') != -1) {
                $("#LBExpertRule_ExpertRuleListInfo").val(ExpertRuleListInfo.replace(/,/g, '\r'));
            }
            if (CurrentTab == 1) ListInstance.onSearch(obj.data["LBExpertRule_Id"], obj.data["LBExpertRule_LBSection_Id"], obj.data["LBExpertRule_LBItem_Id"], obj.data["LBExpertRule_LBItem_CName"]);
        });
        //检验小组过滤
        form.on('select(SectionFilter)', function (data) {
            onSearch();
        });
        //采用
        form.on('checkbox(IsUse)', function (data) {
            //这里是当选中的时候 把选中的值赋值给表格的当前行的缓存数据 否则提交到后台的时候选中的值是空的
            var elem = data.othis.parents('tr'),
                dataindex = elem.attr("data-index"),
                tableCache = table.cache[TableID];
            var entity = {
                entity: { Id: tableCache[dataindex]["LBExpertRule_Id"], IsUse: tableCache[dataindex]["LBExpertRule_IsUse"] },
                fields:'Id,IsUse'
            };
            update(EDIT_EXPERT_RULE_URL, entity, function (res) {
                if (res.success) {
                    uxbase.MSG.onSuccess("修改成功!");
                } else {
                    uxbase.MSG.onError(res.msg);
                }
            });
        });
        //专家规则表单中检验小组选择
        form.on('select(LBExpertRule_LBSection_Id)', function (data) {
            initItem(data.value);
        });
        //复制规则按钮
        $("#CopyRule").on('click', function () {
            onCopyRule();
        });
        //新增规则按钮
        $("#AddRule").on('click', function () {
            onAddRule();
        });
        //编辑规则按钮
        $("#EditRule").on('click', function () {
            onEditRule();
        });
        //保存规则按钮
        form.on('submit(SaveRule)', function (data) {
            onSaveRule(data.field);
            return false;
        });
        //删除规则按钮
        $("#DelRule").on('click', function () {
            onDelRule();
        });
        //监听页签切换
        element.on('tab(tab)', function (data) {
            CurrentTab = data.index;
            switch (CurrentTab) {
                case 0:
                    break;
                case 1:
                    var ExpertRuleID = CheckRowData.length > 0 ? CheckRowData[0]["LBExpertRule_Id"] : null,
                        SectionID = CheckRowData.length > 0 ? CheckRowData[0]["LBExpertRule_LBSection_Id"] : null,
                        ItemID = CheckRowData.length > 0 ? CheckRowData[0]["LBExpertRule_LBItem_Id"] : null,
                        ItemName = CheckRowData.length > 0 ? CheckRowData[0]["LBExpertRule_LBItem_CName"] : null;
                    ListInstance.onSearch(ExpertRuleID, SectionID, ItemID, ItemName);
                    break;
                default:
                    break;
            }
        });
    };
	//初始化
    function init() {
        //初始化页面
        initHtml();
        //设置表单高度
        setDomHeight();
        //表单验证
        form.verify({
            ZDY_number: function (value, item) {
                if (value && isNaN(value)) {
                    var label = $(item).parents(".layui-form-item").children(".layui-form-label").html(),
                        msg = "";
                    if (label) {
                        msg = label + "只能填写数字！";
                    } else {
                        msg = '只能填写数字';
                    }
                    return msg;
                }
            }
        });
	};
    //初始化
    init();
});