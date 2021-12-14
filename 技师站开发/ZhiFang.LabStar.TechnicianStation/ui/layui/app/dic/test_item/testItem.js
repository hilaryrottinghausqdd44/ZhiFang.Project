/**
 * 检验项目
 * @author zhangda
 * @version 2019-06-25
 */
var common = new Object();
//选中数据
common.checkData = [];
//遮罩
common.load = {
    loadIndex: null,
    loadShow: function () {
        if (common.load.loadIndex == null) {
            common.load.loadIndex = layer.open({ type: 3 });
        }
    },
    loadHide: function () {
        if (common.load.loadIndex != null) {
            layer.close(common.load.loadIndex);
            common.load.loadIndex = null;
        }
    }
};
//依赖layui
var layer = layui.layer,
    $ = layui.jquery,
    table = layui.table,
    form = layui.form,
    element = layui.element,
    laydate = layui.laydate,
    colorpicker = layui.colorpicker,
    formSelects = layui.formSelects,
    uxutil = layui.uxutil;

$(function () {
    //定位参数
    var tablePositionInfo = {
        limit: null,//项目列表每页条数
        where: '1=1',
        sectionID:null,//小组条件
        itemID: null,//定位数据id
        curr: 1,//页码
        delIndex:null//删除数据位置
    };
    //表格
    var tableObj = {
        fields: {
            Id: "LBItem_Id",
            IsUse: 'LBItem_IsUse',
            IsOrderItem: 'LBItem_IsOrderItem',
            IsSampleItem: 'LBItem_IsSampleItem',
            IsCalcItem: 'LBItem_IsCalcItem',
            IsChargeItem: 'LBItem_IsChargeItem',
            IsUnionItem: 'LBItem_IsUnionItem',
            IsPrint: 'LBItem_IsPrint',
            IsPartItem: 'LBItem_IsPartItem'
        },
        current: null,
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBItem',
        //返回页码和该页数据  --//根据id，每页条数，排序获得数据所在页码与该页数据
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]',
        updateUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemByField',
        delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBItem',
        getSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true&where=IsUse=true',//获得小组
        getSpecialtyUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?isPlanish=true&sort=[{property:"LBSpecialty_DispOrder",direction:"ASC"}]',//获得专业
        getEquipUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true&fields=LBEquip_Id,LBEquip_CName&where=IsUse=true',
        getEnumTypeUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',//获得枚举 传递枚举类型名 
        getPinYinZiTouUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou?chinese=',//获得拼音字头
        getMaxNoByEntityFieldUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField',//获取指定实体字段的最大号
        //组合项目
        delSubItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBItemGroup',//删除组合项目中的子项
        selectSubItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true&sort=[{property:"LBItemGroup_LBItem_DispOrder",direction:"ASC"}]&fields=LBItemGroup_Id,LBItemGroup_DataAddTime,LBItemGroup_LBGroup_Id,LBItemGroup_LBItem_Id,LBItemGroup_LBItem_CName,LBItemGroup_LBItem_EName,LBItemGroup_LBItem_SName,LBItemGroup_LBItem_GroupType,LBItemGroup_LBItem_ItemCharge,LBItemGroup_LBItem_DispOrder',
        copyComboItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddCopyLBItemGroup',//复制组合项目子项
        refresh: function () {//项目信息表格刷新
            //$("#search").click();
            common.checkData = [];
            table.reload('table', {
                height: 'full-95',
                where: {
                    time: new Date().getTime()
                }
	        });
        },
        //获得页码与该页数据并刷新
        refreshByItemId: function () {
            if (!tablePositionInfo.itemID || !tablePositionInfo.limit) {
                layer.msg("查询页码缺少必要的条件！", { icon: 5, anim: 6 });
                return;
            }
            var url = tableObj.selectUrl;
            if (tablePositionInfo.sectionID == null) {
                url += '&itemID=' + tablePositionInfo.itemID + '&limit=' + tablePositionInfo.limit + '&where=' + tablePositionInfo.where;;
            } else {
                url += '&sectionID=' + tablePositionInfo.sectionID+'&itemID=' + tablePositionInfo.itemID + '&limit=' + tablePositionInfo.limit + '&where=' + tablePositionInfo.where;
            }
            uxutil.server.ajax({
                url: encodeURI(url)
            }, function (res) {
                if (res.success) {
                    if (res.ResultDataValue) {
                        //var data = JSON.parse(res.ResultDataValue).list;
                        //该条数据所在页码
                        var page = JSON.parse(res.ResultDataValue).page;
                        common.checkData = [];
                        table.reload('table', {
                            height: 'full-95',
                            where: {
                                time: new Date().getTime()
                            }, page: {
                                curr: page
                            }
                        });
                    }
                } else {
                    layer.msg(res.msg, { icon: 5, anim: 6 });
                }
            });
        }

    };
    var tab = {
        index: 0,//当前页签
        allTabIndex:[0,1,2,3,4,5,6,7]//全部页签
    };
    //表单
    var formObj = {
        type: 'edit',
        reset: function () { //重置
            if (formObj.type == 'edit' || formObj.type == 'show') {
                showTypeSign();
                if (common.checkData.length > 0) {
                    var data = common.checkData[common.checkData.length - 1];
                    form.val("form", data);
                    itemAttrByGroupType(data.LBItem_GroupType);
                    for (var i in data) {
                        $("#formInfo select").each(function () {
                            if (i == $(this).attr("_name")) {
                                $(this).val(data[i]);
                            }
                        });
                        $("#formInfo input[type=checkbox]").each(function () {
                            if (i == $(this).attr("name")) {
                                var flag = data[i] == "true" ? true : false;
                                setSwitchFun(i.split("_")[1], flag, false);
                            }
                        });
                    }
                }
                SetDisabled(formObj.type == 'show');
            } else {
                showTypeSign();
                SetDisabled(false);
                $("#formInfo")[0].reset();
                clearFormSelects();
            }
        },
        empty: function () { //清空
            $("#formInfo")[0].reset();
            clearFormSelects();
            hideTypeSign();
        }
    };
    init();
    function init() {
        common.load.loadShow();
        $(".tableHeight").css("height", ($(window).height() - 15) + "px");//设置表单容器高度
        $(".container").css("height", (parseFloat($(".tableHeight").css("height")) - 100) + "px");
        $(".container2").css("height", (parseFloat($(".tableHeight").css("height")) - 245) + "px");
        //$(".container3").css("height", (parseFloat($(".tableHeight").css("height")) - 280) + "px");
        tableRender();
        getServerData();
    }
    //发送服务获得基础数据
    function getServerData() {
        //获得小组
        uxutil.server.ajax({
            url: tableObj.getSectionUrl
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        var html = "";
                        for (var i in data) {
                            html += '<option value="' + data[i].LBSection_Id + '">' + data[i].LBSection_CName + '</option>';
                        }
                        $("#itemRangeSectionID").html("<option value=''>请选择</option>" + html);
                        $("#itemCalcFormulaSectionID").html("<option value=''>请选择</option>" + html);
                        $("#itemTimeWSectionID").html("<option value=''>请选择</option>" + html);
                        $("#SectionID").html("<option value=''>请选择</option><option value='-1'>不属于任何小组</option>" + html);
                        form.render('select');
                    }
                }
            } else {
                layer.msg("小组查询失败！", { icon: 5, anim: 6 });
            }
        });
        //获得专业
        uxutil.server.ajax({
            url: tableObj.getSpecialtyUrl + "&where=IsUse=1"
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        var html = "<option value=''>请选择</option>";
                        for (var i in data) {
                            html += '<option value="' + data[i].LBSpecialty_Id + '">' + data[i].LBSpecialty_CName + '</option>';
                        }
                        $("#SpecialtyID").html(html);
                        form.render('select');
                    }
                }
            } else {
                layer.msg("专业查询失败！", { icon: 5, anim: 6 });
            }
        });
        //获得仪器
        uxutil.server.ajax({
            url: tableObj.getEquipUrl
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        var html = "<option value=''>请选择</option>";//<option value='-1'>不属于任何仪器</option>
                        for (var i in data) {
                            html += '<option value="' + data[i].LBEquip_Id + '">' + data[i].LBEquip_CName + '</option>';
                        }
                        //formSelect.updateSelect("EquipID", html);

                        $("#itemRangeEquipID").html(html);//参考值
                        form.render('select');
                    }
                }
            } else {
                layer.msg("仪器查询失败！", { icon: 5, anim: 6 });
            }
        });
        //获得专业功能类型枚举
        uxutil.server.ajax({
            url: tableObj.getEnumTypeUrl + '?classname=SectionFunType&classnamespace=ZhiFang.Entity.LabStar'
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    //if (data.length > 0) {
                    var html = "";
                    for (var i in data) {
                        var color = data[i].BGColor;
                        if (color != "") {
                            html += '<option value="' + data[i].Id + '" style="color:' + color + '">' + data[i].Name + '</option>';
                        } else {
                            html += '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
                        }
                    }
                    $("#SectionFun").html(html);
                    $("#SectionFun").val(0);
                    form.render('select');
                    //}
                }
            } else {
                layer.msg("专业功能类型查询失败！", { icon: 5, anim: 6 });
            }
        });
        //获得结果类型枚举
        uxutil.server.ajax({
            url: tableObj.getEnumTypeUrl + '?classname=ResultValueType&classnamespace=ZhiFang.Entity.LabStar'
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    //if (data.length > 0) {
                    var html = "";
                    for (var i in data) {
                        var color = data[i].BGColor;
                        if (color != "") {
                            html += '<option value="' + data[i].Id + '" style="color:' + color + '">' + data[i].Name + '</option>';
                        } else {
                            html += '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
                        }
                    }
                    $("#ValueType").html(html);
                    $("#ValueType").val(2);
                    form.render('select');
                    //}
                }
            } else {
                layer.msg("结果类型查询失败！", { icon: 5, anim: 6 });
            }
        });
        //项目特殊属性
        uxutil.server.ajax({
            url: tableObj.getEnumTypeUrl + '?classname=ItemSpecProperty&classnamespace=ZhiFang.Entity.LabStar'
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    var html = "";
                    for (var i in data) {
                        var color = data[i].BGColor;
                        if (color != "") {
                            html += '<option value="' + data[i].Id + '" style="color:' + color + '">' + data[i].Name + '</option>';
                        } else {
                            html += '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
                        }
                    }
                    $("#LBItem_SpecialType").html(html);
                    form.render('select');
                }
            } else {
                layer.msg("项目特殊属性查询失败！", { icon: 5, anim: 6 });
            }
        });
    }
    //监听查询
    form.on('submit(search)', function (data) {
        common.checkData = [];
        var url = tableObj.selectUrl;
        if (data.field.SectionID != "") {//小组
            url += '&sectionID=' + data.field.SectionID;
            tablePositionInfo.sectionID = data.field.SectionID;
        } else {
            tablePositionInfo.sectionID = null;
        }
        var where = '1=1';
        if (data.field.nameOrotherName != "") {//模糊查询
            var str = data.field.nameOrotherName;
            where += " and (CName like '%" + str + "%' or EName like '%" + str + "%' or SName like '%" + str + "%' or Shortcode like '%" + str + "%' or PinYinZiTou like '%" + str + "%')";
        }
        if (data.field.radio != "") {//组合类型
            switch (data.field.radio) {
                case "0":
                case "1":
                case "2":
                    where += " and GroupType=" + data.field.radio;
                    break;
                case "3":
                    where += " and IsOrderItem=1";//医嘱
                    break;
                case "4":
                    where += " and IsCalcItem=1";//计算
                    break;
                case "5":
                    where += " and IsChargeItem=1";//计费
                    break;
            }
        }
        tablePositionInfo.where = where;//纪录where条件 用于新增修改定位
        url += "&where=" + where;
        table.reload('table', {
            url: encodeURI(url),
            height: 'full-95',
            where: {
                time: new Date().getTime()
            },page: {
                curr: 1 //重新从第 1 页开始
            }
        });
    });
    //监听回车按下事件--查询项目
    $(document).keydown(function (event) {
        switch (event.keyCode) {
            case 13:
                //判断焦点是否在该输入框
                if (document.activeElement == document.getElementById("nameOrotherName")) {
                    $("#search").click();
                }
        }
    });
    //监听小组下拉框查询
    form.on('select(SectionID)', function (data) {
        common.checkData = [];
        var url = tableObj.selectUrl;
        if (data.value != '') url += '&sectionID=' + data.value;
        var where = '1=1';
        if ($("#nameOrotherName").val() != "") {//模糊查询
            var str = $("#nameOrotherName").val();
            where += " and (CName like '%" + str + "%' or EName like '%" + str + "%' or SName like '%" + str + "%' or Shortcode like '%" + str + "%' or PinYinZiTou like '%" + str + "%')";
        }
        var radio = $("#searchTable input[name=radio]:checked").val();
        if (radio != "") {//组合类型
            switch (radio) {
                case "0":
                case "1":
                case "2":
                    where += " and GroupType=" + radio;
                    break;
                case "3":
                    where += " and IsOrderItem=1";//医嘱
                    break;
                case "4":
                    where += " and IsCalcItem=1";//计算
                    break;
                case "5":
                    where += " and IsChargeItem=1";//计费
                    break;
            }
        }
        url += "&where=" + where;
        table.reload('table', {
            url: encodeURI(url),
            height: 'full-95',
            where: {
                time: new Date().getTime()
            }, page: {
                curr: 1 //重新从第 1 页开始
            }
        });
    });
    form.on('radio(radio)', function (data) {
        $("#search").click();
    });  
    //初始化表格
    var tableConfig;//表格config信息
    function tableRender() {
        tableConfig = table.render({
            elem: '#table',
            height: 'full-95',
            defaultToolbar: [],
            size: 'sm', //小尺寸的表格
            //toolbar: '#toolbar',
            url: tableObj.selectUrl,
            cols: [
                [{
                    title:'序号',
                    type: 'numbers'
                }, {
                    field: tableObj.fields.Id,
                    width: 60,
                    title: '主键ID',
                    sort: true,
                    hide: true
                },{
                    field: 'LBItem_ItemNo',
                    title: '项目编码',
                    width: 80,
                    sort: true
                },{
                    field: 'LBItem_CName',
                    title: '项目名称',
                    minWidth: 130,
                    sort: true
                },{
                    field: 'LBItem_EName',
                    title: '英文名称',
                    minWidth: 130,
                    sort: true
                },{
                    field: 'LBItem_SName',
                    title: '简称',
                    minWidth: 100,
                    sort: true
                },{
                    field: 'LBItem_Shortcode',
                    title: '快捷码',
                    minWidth: 100,
                    sort: true
                },{
                    field: 'LBItem_PinYinZiTou',
                    title: '汉字拼音字头',
                    minWidth: 120,
                    sort: true
                },{
                    field: 'LBItem_ItemCharge',
                    title: '项目价格(元)',
                    minWidth: 120,
                    sort: true
                },{
                    field: 'LBItem_GroupType',
                    title: '组合类型',
                    minWidth: 100,
                    sort: true,
                    templet: function (data) {
                        var str = "";
                        switch (data.LBItem_GroupType) {
                            case "0":
                                str = "单项";
                                break;
                            case "1":
                                str = "组合";
                                break;
                            case "2":
                                str = "套餐";
                                break;
                        }
                        return str;
                    }
                },{
                    field: 'LBItem_IsOrderItem',
                    title: '是否医嘱项目',
                    minWidth: 120,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data.LBItem_IsOrderItem.toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                },{
                    field: 'LBItem_IsSampleItem',
                    title: '是否采样项目',
                    minWidth: 120,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data.LBItem_IsSampleItem.toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                },{
                    field: 'LBItem_IsCalcItem',
                    title: '是否计算项目',
                    minWidth: 120,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data.LBItem_IsCalcItem.toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                },{
                    field: 'LBItem_IsChargeItem',
                    title: '是否收费项目',
                    minWidth: 120,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data.LBItem_IsChargeItem.toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                },{
                    field: 'LBItem_IsUnionItem',
                    title: '是否合并项目',
                    minWidth: 120,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data.LBItem_IsUnionItem.toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                },{
                    field: 'LBItem_IsPrint',
                    title: '是否报告项目',
                    minWidth: 120,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data.LBItem_IsPrint.toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                },{
                    field: 'LBItem_IsPartItem',
                    title: '是否辅助项目',
                    minWidth: 120,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data.LBItem_IsPartItem.toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                },{
                    field: tableObj.fields.IsUse,
                    title: '是否使用',
                    minWidth: 100,
                    sort: true,
                    templet: function (data) {
                        var str = "<span>否</span> ";
                        if (data[tableObj.fields.IsUse].toString() == "true") {
                            str = "<span  style='color:red'>是</span>"
                        }
                        return str;
                    }
                }, {
                    field: 'LBItem_DispOrder',
                    title: '显示次序',
                    minWidth: 100,
                    sort: true
                },{
                    field: 'LBItem_Comment',
                    title: '备注',
                    minWidth: 200
                }
                ]
            ],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            done: function (res, curr, count) {
                if (common.load.loadIndex) {
                    common.load.loadHide();
                }
                if (count > 0) {
                    if (tableObj.current != null) {//新增修改定位
                        var flag = false;
                        var index = null;
                        for (var i = 0; i < res.data.length; i++) {
                            if (res.data[i][tableObj.fields.Id] == tableObj.current) {
                                flag = true;
                                index = i + 1;
                            }
                        }
                        if (flag) {
                            $("#table+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").click();
                            document.querySelector("#table+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                        } else {
                            $("#table+div .layui-table-body table.layui-table tbody tr:first").click();
                            layer.msg('本页未找到之前操作数据,无法选中！', { offset: '15px', time: 3000, icon: 0, anim: 0 });
                        }
                        tableObj.current = null;
                    } else if (tablePositionInfo.delIndex != null) {//删除定位
                        var len = res.data.length;
                        var index;
                        if (tablePositionInfo.curr == curr) {//是否是当前页
                            if (tablePositionInfo.delIndex <= len) {//判断当前位置是否存在数据，不存在则定位到最后一条
                                index = tablePositionInfo.delIndex;
                            } else {
                                index = len;
                            }
                        } else {
                            index = len;
                        }
                        $("#table+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").click();
                        document.querySelector("#table+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                        tablePositionInfo.delIndex = null;
                    } else {//不用定位
                        //默认选中第一行
                        $("#table+div .layui-table-body table.layui-table tbody tr:first").click();
                    }
                }
                //设置limit
                tablePositionInfo.curr = curr;
                tablePositionInfo.limit = tableConfig.config.limit;
            },
            response: function () {
                return {
                    statusCode: true, //成功状态码
                    statusName: 'code', //code key
                    msgName: 'msg ', //msg key
                    dataName: 'data' //data key
                }
            },
            parseData: function (res) { //res 即为原始返回的数据
                if (!res.success) {
                    layer.msg(res.ErrorInfo, { icon: 5, anim: 6 });
                }
                if (res.ResultDataValue == "") { 
                    tabIsShow("");
                }
                if (!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            text: {
                none: '暂无相关数据'
            }
        });
    }
    //监听行单击事件
    table.on('row(table)', function (obj) {
        var data = obj.data;
        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        //暂存选中数据
        if (common.checkData.length>0 && common.checkData[0][tableObj.fields.Id] == data[tableObj.fields.Id]) return;
        common.checkData = [];//重置
        common.checkData.push(data);
        $(".itemName").html(data.LBItem_CName);
        formObj.type = "show";
        formObj.reset();//赋值表单
        isSaveEnable(false);
        tabIsShow(data);
    });
    //禁用处理
    function SetDisabled(isDisabled) {
        $("#formInfo :input").each(function (i, item) {
            if ($(item)[0].nodeName == 'BUTTON') return true;
            $(item).attr("disabled", isDisabled);
            if (isDisabled) {
                if (!$(item).hasClass("layui-disabled")) $(item).addClass("layui-disabled");
            } else {
                if ($(item).hasClass("layui-disabled")) $(item).removeClass("layui-disabled");
            }
        });
        if (!isDisabled) itemAttrByGroupType(common.checkData[0]["LBItem_GroupType"]);
        form.render();
    }
    //获取指定实体字段的最大号
    function getMaxNumber(entityName, entityField, callback) {
        var entityName = entityName || null,
            entityField = entityField || null,
            url = tableObj.getMaxNoByEntityFieldUrl;
        if (!entityName || !entityName) return;
        url += '?entityName=' + entityName + '&entityField=' + entityField;
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    if (typeof callback == 'function') callback(res.ResultDataValue);
                }
            } else {
                layer.msg(res.msg || "获取指定实体字段的最大号失败！", { icon: 5, anim: 6 });
            }
        });
    };
    //监听排序事件
    table.on('sort(table)', function (obj) {
        var field = obj.field;//排序字段
        var type = obj.type;//升序还是降序
        var url = tableObj.selectUrl;
        if (type == null) {
            //重载表格
            table.reload('table', {
                height: 'full-95',
                //initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
                url: tableObj.selectUrl,
                where: {
                    time: new Date().getTime()
                }
            });
            return;
        }
        if (url.indexOf("sort") != -1) {//存在
            var start = url.indexOf("sort=[");
            var end = url.indexOf("]")+1;
            var oldStr = url.slice(start, end);
            var newStr = 'sort=[{property:"' + field + '",direction:"' + type+'"}]';
            url = url.replace(oldStr, newStr);
        } else {
            url = url + '&sort=[{property:"' + field + '",direction:"' + type +'"}]';
        }
        //设置排序
        tableObj.selectUrl = url;
        //重载表格
        table.reload('table', {
            height: 'full-95',
            initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
            url: tableObj.selectUrl,
            where: {
                time: new Date().getTime()
            }
        });
    });
    //监听浏览器窗口
    window.onresize = function () {
        setTimeout(function () {
            $(".tableHeight").css("height", ($(window).height() - 15) + "px");
            $(".container").css("height", (parseFloat($(".tableHeight").css("height")) - 100) + "px");
            $(".container2").css("height", (parseFloat($(".tableHeight").css("height")) - 245) + "px");
            //$(".container3").css("height", (parseFloat($(".tableHeight").css("height")) - 280) + "px");
        }, 100);
        
    }
    //判断选项卡是否显示--根据选中数据判断
    function tabIsShow(data) {
        //切换数据时选项卡重置
        if (data == "") {
            common.checkData = [];
            formObj.empty();
            hideTab(tab.allTabIndex);
            showTab([0]);
            tabChange(0);
            tab.index = 0;
            return;
        }
        var haveTabs = [];
        if (data.LBItem_GroupType == 0) {//单项
            haveTabs = [0, 1, 3, 4, 6, 7];
        } else if (data.LBItem_GroupType == 1) {
            haveTabs = [0, 1, 2, 4, 7];
        } else if (data.LBItem_GroupType == 2) {
            haveTabs = [0, 1, 2, 4, 7];
        }
        if (data.LBItem_IsCalcItem.toString() == "true") {
            haveTabs.push(5);
        }
        //显示隐藏页签
        var noExistTabIndex = [];//不存在的页签
        for (var j = 0; j < tab.allTabIndex.length; j++) {
            var isFlag = false;
            for (var k = 0; k < haveTabs.length; k++) {
                if (tab.allTabIndex[j] == haveTabs[k]) {
                    isFlag = true;
                }
            }
            if (!isFlag) {
                noExistTabIndex.push(tab.allTabIndex[j]);
            }
        }
        showTab(haveTabs);
        hideTab(noExistTabIndex);
        //当前页签是否存在
        var isExist = false;
        for (var i = 0; i < haveTabs.length; i++) {
            if (tab.index == haveTabs[i]) {
                isExist = true;
            }
        }
        if (!isExist) {
            tabChange(0);//不存在则重置当前页签
            tab.index = 0;
            return;
        }
        $("#tab li[data-index=" + tab.index + "]").click();
    }
    //根据index切换tab选项卡
    function tabChange(index) {
        if (!$("#tab ul>li[data-index=" + index + "]").hasClass("layui-this")) {
            $("#tab ul>li[data-index=" + index + "]").siblings().removeClass("layui-this");
            $("#tab .layui-tab-content>div[data-index=" + index + "]").siblings().removeClass("layui-show");
            $("#tab ul>li[data-index=" + index + "]").addClass("layui-this");
            $("#tab .layui-tab-content>div[data-index=" + index + "]").addClass("layui-show");
        }
    }
    //显示选项卡
    function showTab(indexArr) {
        if (indexArr.length > 0) {
            for (var i = 0; i < indexArr.length; i++) {
                $("#tab ul>li").each(function () {
                    if ($(this).attr("data-index") == indexArr[i]) {
                        if ($(this).hasClass("layui-hide")) {
                            $(this).removeClass("layui-hide");
                            $("#tab .layui-tab-content>div[data-index=" + indexArr[i] + "]").removeClass("layui-hide");
                        }
                    }
                });
            }
        }
    }
    //隐藏选项卡
    function hideTab(indexArr) {
        if (indexArr.length > 0) {
            for (var i = 0; i < indexArr.length; i++) {
                $("#tab ul>li").each(function () {
                    if ($(this).attr("data-index") == indexArr[i]) {
                        if (!$(this).hasClass("layui-hide")) {
                            $(this).addClass("layui-hide");
                            $("#tab .layui-tab-content>div[data-index=" + indexArr[i] + "]").addClass("layui-hide");
                        }
                    }
                });
            }
        }
    }
    //切换tab页签监听
    element.on('tab(tab)', function (data) {
        tab.index = data.index;
        var checkData = common.checkData[common.checkData.length - 1];
        switch (data.index) {
            case 1:
                if (!checkData.LBItem_Id) {
                    layer.msg("未选择项目！", { icon: 5, anim: 6 });
                    return;
                }
                itemAllot.init();
                break;
            case 2:
                if (!checkData.LBItem_Id) {
                    layer.msg("未选择项目！", { icon: 5, anim: 6 });
                    return;
                }
                comboItemInit();
                break;
            case 3:
                if (!checkData.LBItem_Id) {
                    layer.msg("未选择项目！", { icon: 5, anim: 6 });
                    return;
                }
                itemRange.init();
                break;
            case 4:
                if (!checkData.LBItem_Id) {
                    layer.msg("未选择项目！", { icon: 5, anim: 6 });
                    return;
                }
                itemContrast.init();
                break;
            case 5:
                if (!checkData.LBItem_Id) {
                    layer.msg("未选择项目！", { icon: 5, anim: 6 });
                    return;
                }
                itemCalc.init();
                break;
            case 6:
                if (!checkData.LBItem_Id) {
                    layer.msg("未选择项目！", { icon: 5, anim: 6 });
                    return;
                }
                itemPhrase.init();
                break;
            case 7:
                if (!checkData.LBItem_Id) {
                    layer.msg("未选择项目！", { icon: 5, anim: 6 });
                    return;
                }
                itemTimeW.init();
                break;
        }
    });
    /*
     * * * * * * * * * * * * * * * * 检验项目--表单 * * * * * * * * * * * * * * * * *
     */
    //显示编辑新增标识
    function showTypeSign() {
        var type = formObj.type,
            text = type == 'add' ? "新增" : (type == 'edit' ? "编辑" : "查看");

        if ($("#formType").hasClass("layui-hide")) {
            $("#formType").removeClass("layui-hide").html(text);
        } else {
            $("#formType").html(text);
        }
    }
    //隐藏编辑新增标识
    function hideTypeSign() {
        if (!$("#formType").hasClass("layui-hide")) {
            $("#formType").addClass("layui-hide");
        }
    }
    //结果单位下拉框监听 同步输入框
    form.on('select(Unit)', function (data) {
        $("#InputUnit").val(data.value);
    });
    //监听名称输入同步拼音字头
    var CNameOldStr = "";
    $('#CName').bind('input propertychange', function () {
        var CNameNewStr = $(this).val();
        getPinYinZiTou($(this).val(), function (str) {
            if ($("#SName").val() == CNameOldStr || $("#SName").val() == "") {
                $("#SName").val(CNameNewStr);
                CNameOldStr = CNameNewStr;
            }
            if ($("#Shortcode").val() == $("#PinYinZiTou").val()) {
                $("#Shortcode").val(str);
            }
            $("#PinYinZiTou").val(str);
        });
    });
    //获得拼音字头
    function getPinYinZiTou(chinese, callBack) {
        if (chinese == "") {
            if (typeof (callBack) == "function") {
                callBack(chinese);
            }
            return;
        }
        uxutil.server.ajax({
            url: tableObj.getPinYinZiTouUrl + encodeURI(chinese)
        }, function (res) {
            if (res.success) {
                if (typeof (callBack) == "function") {
                    callBack(res.ResultDataValue);
                }
            } else {
                layer.msg("拼音字头获得失败！", { icon: 5, anim: 6 });
            }
        });
    }
    //监听组合类型单选框
    form.on('radio(GroupType)', function (data) {
        itemAttrByGroupType(data.value);
    });
    //组合项目联动关系
    function itemAttrByGroupType(GroupType) {
        switch (GroupType) {
            case "0":
                setSwitchFun("IsOrderItem", true, true);//医嘱
                setSwitchFun("IsSampleItem", true, true, true);//采样
                setSwitchFun("IsChargeItem", true, true);//收费
                setSwitchFun("IsCalcItem", false, true);//计算
                setSwitchFun("IsPrint", true, true);//报告
                setSwitchFun("IsUnionItem", false, true,true);//合并
                setSwitchFun("IsPartItem", false, true);//辅助
                break;
            case "1":
                setSwitchFun("IsOrderItem", true, true);//医嘱
                setSwitchFun("IsSampleItem", true, true, true);//采样
                setSwitchFun("IsChargeItem", true, true);//收费
                setSwitchFun("IsCalcItem", false, true, true);//计算
                setSwitchFun("IsPrint", true, true, true);//报告
                setSwitchFun("IsUnionItem", false, true);//合并
                setSwitchFun("IsPartItem", false, true,true);//辅助
                break;
            case "2":
                setSwitchFun("IsOrderItem", false, true, true);//医嘱
                setSwitchFun("IsSampleItem", false, true, true);//采样
                setSwitchFun("IsChargeItem", false, true, true);//收费
                setSwitchFun("IsCalcItem", false, true, true);//计算
                setSwitchFun("IsPrint", true, true, true);//报告
                setSwitchFun("IsUnionItem", false, true, true);//合并
                setSwitchFun("IsPartItem", false, true, true);//辅助
                break;
        }
    }
    //开关选中/取消，禁用/取消
    function setSwitchFun(id, isCheck,isUseDisabled, isDisabled) {
        if (id) {
            var isDisabled = isDisabled || false;
            var isUseDisabled = isUseDisabled || false;
            var elem = $("#" + id);
            if (isCheck) {
                if (!elem.next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    elem.next('.layui-form-switch').addClass('layui-form-onswitch');
                    elem.next('.layui-form-switch').children("em").html("是");
                    elem[0].checked = true;
                }
            } else {
                if (elem.next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    elem.next('.layui-form-switch').removeClass('layui-form-onswitch');
                    elem.next('.layui-form-switch').children("em").html("否");
                    elem[0].checked = false;
                }
            }
            if (isUseDisabled) {
                if (isDisabled) {
                    if (!elem.prop("disabled")) {
                        elem.prop("disabled", "disabled");
                        elem.next('.layui-form-switch').addClass("layui-disabled");
                        elem.addClass("layui-disabled");
                    }
                } else {
                    if (elem.prop("disabled")) {
                        elem.removeProp("disabled");
                        elem.next('.layui-form-switch').removeClass("layui-disabled");
                        elem.removeClass("layui-disabled");
                    }
                }
            }
        }
    }
    //监听form表单
    form.on('submit(save)', function (data) {
        window.event.preventDefault();
        if (formObj.type == 'show') return false;
        var fields = "";//发送修改的字段
        var postData = {};//发送的数据
        //判断复选框是否选中 未选中的话手动添加字段  --开关手动加字段
        for (var a in tableObj.fields) {
            if (a != "Id") {
                if (!$("#" + a).next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    data.field[tableObj.fields[a]] = "false"
                } else {
                    data.field[tableObj.fields[a]] = "true"
                }
            }
        }
        for (var k in data.field) {
            if (k == "no") {//去除结果单位下拉框
                continue;
            }
            if (k.split("_")[1] == "ItemCharge") {
                if (data.field[k] != "") {
                    var reg = /^\d+(?=\.{0,1}\d+$|$)/;
                    if (!reg.test(data.field[k])) {
                        layer.msg("请正确输入价格!", { icon: 5, anim: 6 });
                        return;
                    }
                } else {
                    continue;
                }
            }
            if (k.split("_")[1] == "SpecialtyID" && data.field[k] == "") {
                postData[k.split("_")[1]] = null;
                fields += k.split("_")[1] + ",";
                continue;
            }
            if (k.split("_")[1] == "ValueType" && data.field[k] == "") {
                postData[k.split("_")[1]] = null;
                fields += k.split("_")[1] + ",";
                continue;
            }
            postData[k.split("_")[1]] = typeof data.field[k] == "string" ? data.field[k].trim() : data.field[k];
            fields += k.split("_")[1] + ",";
        }
        fields = fields.slice(0, fields.length - 1);
        if (formObj.type == 'edit') {
            common.load.loadShow();
            uxutil.server.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: postData, fields: fields }),
                url: tableObj.updateUrl
            }, function (res) {
                common.load.loadHide();
                if (res.success) {
                    layer.msg("编辑成功！", { icon: 6, anim: 0 });
                    formObj.empty();
                    tableObj.current = postData.Id;
                    tablePositionInfo.itemID = postData.Id;//用于定位
                    tableObj.refreshByItemId();
                } else {
                    layer.msg("编辑失败！", { icon: 5, anim: 6 });
                }
            });
        } else if (formObj.type == 'add') {
            common.load.loadShow();
            delete postData.Id;
            uxutil.server.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: postData }),
                url: tableObj.addUrl,
            }, function (res) {
                common.load.loadHide();
                if (res.success) {
                    layer.msg("新增成功！", { icon: 6, anim: 0 });
                    formObj.empty();
                    var data = eval('(' + res.ResultDataValue + ')');
                    tableObj.current = data.id;
                    tablePositionInfo.itemID = data.id;//用于定位
                    tableObj.refreshByItemId();
                } else {
                    layer.msg("新增失败！", { icon: 5, anim: 6 });
                }
            });
        }
    });
    //重置添加、修改不同操作
    $('#cancel').on('click', function () {
        if (common.checkData.length == 0) {
            formObj.empty();
            return;
        }
        if (formObj.type == 'edit' || formObj.type == 'show') {
            formObj.reset();
        } else if (formObj.type == 'add') {
            formObj.empty();
            showTypeSign();
        }
    });
    //添加表单
    $("#add").on('click', function () {
        formObj.type = 'add';//编辑还是新增
        //显示次序赋值
        getMaxNumber('LBItem', 'DispOrder', function (value) {
            $("#DispOrder").val(value);
        });
        //采样次序赋值
        getMaxNumber('LBItem', 'CollectSort', function (value) {
            $("#LBItem_CollectSort").val(value);
        });
        if (common.checkData.length > 0) formObj.reset();
        isDelEnable(false);
        isSaveEnable(true);
    });
    //编辑表单
    $("#edit").on('click', function () {
        if (common.checkData.length === 0) {
            layer.msg('请选择一行！');
        } else {
            formObj.type = 'edit';//编辑还是新增
            formObj.reset();
            isDelEnable(true);
            isSaveEnable(true);
        }
    });
    //删除
    $("#del").on('click', function () {
        if (common.checkData.length === 0) {
            layer.msg('请选择一行！');
        } else {
            var pageCount = table.cache.table.length;
            layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
                //获得删除数据的位置--多选删除需要调整 取最后删除的一个
                tablePositionInfo.delIndex = Number($("#table+div .layui-table-body table.layui-table tbody tr.layui-table-click").attr("data-index")) + 1;
                var len = common.checkData.length;
                for (var i = 0; i < common.checkData.length; i++) {
                    var Id = common.checkData[i][tableObj.fields.Id];
                    uxutil.server.ajax({
                        url:tableObj.delUrl + "?Id=" + Id
                    }, function (res) {
                        if (res.success) {
                            len--;
                            if (len == 0) {
                                layer.close(index);
                                layer.msg("删除成功！", { icon: 6, anim: 0 });
                                if (pageCount > 1) {
                                    tableObj.refresh();
                                } else {
                                    table.reload('table', {
                                        height: 'full-95',
                                        where: {
                                            time: new Date().getTime()
                                        }, page: {
                                            curr: tablePositionInfo.curr > 1 ? tablePositionInfo.curr - 1 : tablePositionInfo.curr
                                        }
                                    });
                                }
                                formObj.empty();
                            }
                        } else {
                            layer.msg("删除失败！", { icon: 5, anim: 6 });
                        }
                    });
                }
            });
        }
    })
    //项目排序调整
    $("#sort").click(function () {
        var flag = false;
        var searchWhere = "";
        if (tablePositionInfo.sectionID != null) {
            searchWhere += "&sectionID=" + tablePositionInfo.sectionID
        }
        if (tablePositionInfo.where != null) {
            searchWhere += "&where=" + tablePositionInfo.where;
        }
        parent.layer.open({
            type: 2,
            title: ['项目排序调整'],
            area: ['1200px', '620px'],
            content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/item_sort/app.html?' + searchWhere,
            cancel: function (index, layero) {
                flag = true;
            },
            end: function () {
                if (!flag) {
                    //layer.msg("保存成功!", { icon: 6, anim: 0 });
                    $("#search").click();
                }
            }
        });
    });
    //采样排序调整
    $("#collectSortAdjust").click(function () {
        var flag = false;
        var searchWhere = "";
        if (tablePositionInfo.sectionID != null) {
            searchWhere += "&sectionID=" + tablePositionInfo.sectionID
        }
        if (tablePositionInfo.where != null) {
            searchWhere += "&where=" + tablePositionInfo.where;
        }
        parent.layer.open({
            type: 2,
            title: ['采样排序调整'],
            area: ['1200px', '620px'],
            content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/collect_sort/app.html?' + searchWhere,
            cancel: function (index, layero) {
                flag = true;
            },
            end: function () {
                if (!flag) {
                    //layer.msg("保存成功!", { icon: 6, anim: 0 });
                    $("#search").click();
                }
            }
        });
    });
    //导入
    $("#import").on('click', function () {
        layer.open({
            type: 2,
            title: '选择导入文件',
            area: ['400px', '160px'],
            content: uxutil.path.LAYUI + '/views/system/comm/import/file/index.html?ENTITYNAME=LBItem',
            success: function (layero, index) {
                //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
                var iframeWin = window[layero.find('iframe')[0]['name']];
                iframeWin.externalCallFun(function () { tableObj.refresh(); layer.close(index); });
            }
        });
    });
    //清空表单中的所有formSelects --部分有默认值
    function clearFormSelects() {
        $("#formInfo select").each(function () {
            if ($(this).attr("id") == "SectionFun") {
                $(this).val(10);
            } else if ($(this).attr("id") == "ValueType") {
                $(this).val(2);
            } else if ($(this).attr("id") == "LBItem_SpecialType") {
                $(this).val(0);
            } else {
                $(this).val('');
            }
        });
    }
    //删除按钮是否禁用 del
    function isDelEnable(bo) {
        if (bo)
            $("#del").removeClass("layui-btn-disabled").removeAttr('disabled', true);
        else
            $("#del").addClass("layui-btn-disabled").attr('disabled', true);
    };
    //保存按钮是否禁用 save
    function isSaveEnable(bo) {
        if (bo)
            $("#save").removeClass("layui-btn-disabled").removeAttr('disabled', true);
        else
            $("#save").addClass("layui-btn-disabled").attr('disabled', true);
    };
    /*
     * * * * * * * * * * * * * * * * 组合项目 * * * * * * * * * * * * * * * * *
     */
    //组合项目初始化
    function comboItemInit() {
        var data = common.checkData[common.checkData.length - 1];

        if (data.LBItem_GroupType == 0) {//单项则不加载
            return;
        }
        comboItemTableRender(data[tableObj.fields.Id]);
        copyItemRender(data.LBItem_GroupType, data[tableObj.fields.Id]);
    }
    //初始化组合项目的子项表格
    function comboItemTableRender(comBoItemId) {
        table.render({
            elem: '#comboItemTable',
            height: 'full-120',
            defaultToolbar: ['filter'],
            size: 'sm', //小尺寸的表格
            toolbar: '#toolbarComboItem',
            url: tableObj.selectSubItemUrl + "&where=lbgroup.Id=" + comBoItemId,
            cols: [
                [{
                    type: 'checkbox',
                    fixed: 'left'
                }, {
                    field: 'LBItemGroup_Id',
                    width: 60,
                    title: '主键ID',
                    sort: false,
                    hide: true
                }, {
                    field: 'LBItemGroup_LBGroup_Id',
                    title: '组合项目Id',
                    minWidth: 130,
                    hide:true,
                    sort: false
                }, {
                    field: 'LBItemGroup_LBItem_Id',
                    title: '子项目Id',
                    minWidth: 130,
                    hide:true,
                    sort: false
                }, {
                    field: 'LBItemGroup_LBItem_CName',
                    title: '项目名称',
                    minWidth: 130,
                    sort: false
                }, {
                    field: 'LBItemGroup_LBItem_EName',
                    title: '英文名称',
                    minWidth: 130,
                    sort: false
                }, {
                    field: 'LBItemGroup_LBItem_SName',
                    title: '简称',
                    minWidth: 100,
                    sort: false
                }, {
                    field: 'LBItemGroup_LBItem_GroupType',
                    title: '组合类型',
                    minWidth: 100,
                    sort: false,
                    templet: function (data) {
                        var str = "";
                        switch (data.LBItemGroup_LBItem_GroupType) {
                            case "0":
                                str = "单项";
                                break;
                            case "1":
                                str = "组合";
                                break;
                            case "2":
                                str = "组套";
                                break;
                        }
                        return str;
                    }
                }, {
                    field: 'LBItemGroup_LBItem_ItemCharge',
                    title: '价格(元)',
                    minWidth: 100,
                    sort: false
                }, {
                    field: 'LBItemGroup_LBItem_DispOrder',
                    title: '显示次序',
                    minWidth: 100,
                    sort: false
                }
                ]
            ],
            page: true,
            limit: 50,
            limits: [50, 100, 200, 300, 500],
            autoSort: false, //禁用前端自动排序
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
            text: {
                none: '暂无相关数据'
            }
        });
    }
    //comboItemTable上面的工具栏
    table.on('toolbar(comboItemTable)', function (obj) {
        var data = common.checkData[common.checkData.length - 1];
        if (!data) {
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        switch (obj.event) {
            case 'edit':
                var flag = false;
                parent.layer.open({
                    type: 2,
                    title: ['选择子项'],
                    //skin: 'layui-layer-molv',
                    area: ['1200px', '600px'],
                    content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/edit_subitem/editSubItem.html?groupItemID=' + data[tableObj.fields.Id] + '&CName=' + data.LBItem_CName + '&GroupType=' + data.LBItem_GroupType,
                    cancel: function (index, layero) {
                        flag = true;
                    },
                    end: function () {
                        if (!flag) {
                            layer.msg("保存成功!", { icon: 6, anim: 0 });
                            comboItemTableRender(data[tableObj.fields.Id]);
                        }
                    }
                });
                break;
            case 'del':
                var checkData = table.checkStatus('comboItemTable').data;
                if (checkData.length === 0) {
                    layer.msg('请选择一行！');
                } else {
                    layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
                        common.load.loadShow();
                        var len = checkData.length;
                        for (var i = 0; i < checkData.length; i++) {
                            var Id = checkData[i].LBItemGroup_Id;
                            uxutil.server.ajax({
                                url: tableObj.delSubItemUrl + "?Id=" + Id
                            }, function (res) {
                                if (res.success) {
                                    len--;
                                    if (len == 0) {
                                        layer.close(index);
                                        layer.msg("删除成功！", { icon: 6, anim: 0 });
                                        comboItemTableRender(data[tableObj.fields.Id]);
                                        common.load.loadHide();
                                    }
                                } else {
                                    layer.msg("删除失败！", { icon: 5, anim: 6 });
                                }
                            });
                        }
                    });
                }
                break;
            case 'copy':
                layer.open({
                    type: 1,
                    btn: ['保存'],
                    title: ['复制项目子项'],
                    //skin: 'layui-layer-molv',
                    area: ['500px', '200px'],
                    content: $('#copyItemModal'),
                    success: function () {
                        $("#copyItemModal").parent().css("overflow", "visible");
                    },
                    yes: function (index, layero) {
                        var copyItemId = $("#item").val();
                        if (copyItemId != "") {
                            common.load.loadShow();
                            uxutil.server.ajax({
                                url: tableObj.copyComboItemUrl + "?fromGroupItemID=" + copyItemId + "&toGroupItemID=" + data[tableObj.fields.Id]
                            }, function (res) {
                                common.load.loadHide();
                                if (res.success) {
                                    layer.msg("复制成功！", { icon: 6, anim: 0 });
                                    layer.close(index);
                                    comboItemTableRender(data[tableObj.fields.Id]);
                                } else {
                                    layer.msg("复制失败", { icon: 5, anim: 6 });
                                }
                            });
                        } else {
                            layer.msg("请选择项目！", { icon: 5, anim: 6 });
                        }
                    }
                });
                break;
        };
    });
    //可以复制的其他组合项目初始化
    function copyItemRender(GroupType, comBoItemId) {
        uxutil.server.ajax({
            url: tableObj.selectUrl + '&where=GroupType=' + GroupType + ' and Id not in (' + comBoItemId + ')'
        }, function (res) {
            if (res.success) {
                var html = "<option value=''>请选择</option>";
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        for (var i in data) {
                            html += '<option value="' + data[i].LBItem_Id + '">' + data[i].LBItem_CName + '</option>';
                        }
                    }
                }
                $("#item").html(html);
                form.render('select');
            } else {
                layer.msg("可复制的组合项目获取失败！", { icon: 5, anim: 6 });
            }
        });
    }

    //表单验证
    form.verify({
        ZDY_Prec: function (value, item) {
            if (value == "" || isNaN(value) || value < 0 || value > 6) {
                var label = $(item).parents(".layui-form-item").children(".layui-form-label").html(),
                    msg = "";
                if (label) {
                    msg = label + "只能填写数字,并且值范围为0-6！";
                } else {
                    msg = '只能填写数字,并且值范围为0-6！';
                }
                return msg;
            }
        }
    }); 
});