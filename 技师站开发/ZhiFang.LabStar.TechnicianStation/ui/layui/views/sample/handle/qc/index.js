/**
   @Name：常规转质控
   @Author：zhangda
   @version 2021-05-11
 */
layui.extend({
    uxutil: 'ux/util',
    uxbase: 'ux/base',
    tableSelect: '../src/tableSelect/tableSelect'
}).use(['uxutil','uxbase', 'element', 'layer','form', 'tableSelect'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
        form = layui.form,
        tableSelect = layui.tableSelect,
        uxbase = layui.uxbase,
        uxutil = layui.uxutil;

    var app = {};
    //外部参数
    app.params = {
        sectionID: null,
        testFormID: null
    };
    //服务地址
    app.url = {
	    /***获取样本单数据服务路径*/
        GetTestFormUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormById?isPlanish=true',
        /***获得质控样本 */
        GetQCSampleUrl: uxutil.path.ROOT + '/ServerWCF/LabStarQCService.svc/SearchQCMaterialbySectionEquip',
        /***检查样本是否符合转换条件 */
        CheckQCUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_CheckSampleConvertStatus',
        /***常规转质控保存服务地址*/
        SaveUrl: uxutil.path.ROOT + '/ServerWCF/LabStarQCService.svc/TestFormConvatQCItem',
        //修改数据服务路径
        EditUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestFormByField',
        //根据id获得样本类型信息
        GetSampleTypeByIdUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeById?isPlanish=true',
        //获得枚举服务路径
        GetEnumUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic'
    };
    //配置
    app.config = {  };

    //初始化
    app.init = function () {
        var me = this,
            msg = [];
        me.getParams();
        if (!me.params.sectionID) msg.push("缺少小组参数!");
        if (!me.params.testFormID) msg.push("缺少检验单ID参数!");
        if (msg.length > 0) {
            uxbase.MSG.onWarn(msg.join('<br>'));
            return;
        }
        me.getTestFormData();
        me.initQCSample();
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
        //转换质控样本
        $("#save").off().on('click', function () {
            me.onSaveClick();
        });
        //取消
        $("#cancel").off().on('click', function () {
            var index = parent.layer.getFrameIndex(window.name); //获取当前弹窗的Id
            parent.layer.close(index); 关闭
        });
    };
    //获得样本单查询字段
    app.getTestFormFields = function () {
        var me = this,
            fields = [];
        $("#testFormInfo :input").each(function (i, item) {
            fields.push($(item).attr("name"));
        });
        return fields;
    };
    //获得样本单数据
    app.getTestFormData = function () {
        var me = this,
            id = me.params.testFormID,
            url = me.url.GetTestFormUrl;
        if (!id) return;
        //查询字段
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getTestFormFields().join(',');
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = res.value;
                    data["LisTestForm_GTestDate"] = data["LisTestForm_GTestDate"].split(" ")[0].replace(RegExp("/", "g"), "-");
                    form.val("testFormInfo", data);
                    me.getSampleTypeInfoById(data["LisTestForm_GSampleTypeID"], function (values) {
                        $("#LisTestForm_GSampleTypeCName").val(values["LBSampleType_CName"]);
                    });
                    me.getEnum("ZhiFang.Entity.LabStar", "GenderType", function (values) {
                        $.each(values, function (i,item) {
                            if (item["Id"] == data["LisTestForm_GenderID"]) {
                                $("#LisTestForm_GenderCName").val(item["Name"]);
                                return false;
                            }
                        });
                    });
                }
            }
        });
    };
    //根据样本类型Id获得样本类型具体信息
    app.getSampleTypeInfoById = function (id,callback) {
        var me = this,
            url = me.url.GetSampleTypeByIdUrl,
            id = id || null;
        if (!id) return;
        //查询字段
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=LBSampleType_CName,LBSampleType_Id';
        url += "&id=" + id;
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = res.value;
                    callback(data);
                }
            }
        });
    };
    //获得枚举
    app.getEnum = function (classnamespace, classname,callBack) {
        var me = this,
            url = me.url.GetEnumUrl,
            classnamespace = classnamespace || null,
            classname = classname || null;

        if (!classnamespace || !classname) return;
        uxutil.server.ajax({
            url: url + '?classname=' + classname+'&classnamespace=' + classnamespace
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    callBack(data);
                }
            } else {
                uxbase.MSG.onError(res.msg);
            }
        });
    };
    //获得质控样本
    app.initQCSample = function () {
        var me = this,
            SectionId = me.params.sectionID,
            url = me.url.GetQCSampleUrl;

        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=LBQCMaterial_Id,LBQCMaterial_LBEquip_CName,LBQCMaterial_EquipModule,LBQCMaterial_CName,LBQCMaterial_ConcLevel';
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'SectionId=' + SectionId;
        tableSelect.render({
            elem: '#LisTestForm_QCMaterial',	//定义输入框input对象 必填
            checkedKey: 'LBQCMaterial_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: '',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '暂不开放',	//搜索输入框的提示文字 默认关键词搜索
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
                    { field: 'LBQCMaterial_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBQCMaterial_LBEquip_CName', width: 100, title: '仪器', sort: false },
                    { field: 'LBQCMaterial_EquipModule', width: 80, title: '模块', sort: false },
                    { field: 'LBQCMaterial_CName', width: 120, title: '质控物', sort: false },
                    { field: 'LBQCMaterial_ConcLevel', width: 80, title: '浓度水平', sort: false }
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
                    var record = data.data[0],
                        name = record["LBQCMaterial_LBEquip_CName"] + (record["LBQCMaterial_EquipModule"] != "" ? "/" + record["LBQCMaterial_EquipModule"] : "") + (record["LBQCMaterial_CName"] != "" ? "/" + record["LBQCMaterial_CName"] : "") + (record["LBQCMaterial_ConcLevel"] ? "/" + record["LBQCMaterial_ConcLevel"] : "");

                    $(elem).val(name);
                    $("#LisTestForm_QCMaterialID").val(record["LBQCMaterial_Id"]);
                } else {
                    $(elem).val('');
                    $("#LisTestForm_QCMaterialID").val('');
                }
            }
        });
    };
    //转换质控样本
    app.onSaveClick = function () {
        var me = this,
            msg = [],
            sectionID = me.params.sectionID,
            testFormID = me.params.testFormID,
            QCMatID = $("#LisTestForm_QCMaterialID").val();
        if (!sectionID) msg.push("缺少小组参数!");
        if (!testFormID) msg.push("缺少检验单ID参数!");
        if (!QCMatID) msg.push("请选择质控物!");
        if (msg.length > 0) {
            uxbase.MSG.onWarn(msg.join('<br>'));
            return;
        }
        me.getTurnQcCheck(function (msg) {//正确的msg为空
            if (!msg) {
                me.onUpdate();
            } else {
                layer.confirm('是否放弃转换项目结果为空或者项目结果不能进行转化的项目?', { icon: 3, title: '提示' }, function (index) {
                    me.onUpdate();
                    layer.close(index);
                });
            }
        });
    };
    /**判断常规转为质控数据校验*/
    app.getTurnQcCheck = function (callback) {
        var me = this,
            QCMatID = $("#LisTestForm_QCMaterialID").val(),
            TestFormID = me.params.testFormID,
            url = me.url.CheckQCUrl + '?TestFormID=' + TestFormID;
        if (QCMatID) url += '&QCMatID=' + QCMatID;
        var load = layer.load();
        uxutil.server.ajax({
            url: url
        }, function (res) {
             layer.close(load);
             callback(res.msg);
        });
    };
    /**转换质控样本保存*/
    app.onUpdate = function () {
        var me = this,
            QCMatID = $("#LisTestForm_QCMaterialID").val(),
            TestFormID = me.params.testFormID,
            url = me.url.SaveUrl + '?QCMatId=' + QCMatID + '&TestFormId=' + TestFormID;

        var load = layer.load();
        uxutil.server.ajax({
            url: url
        }, function (res) {
            layer.close(load);
            if (res.success) {
                //转入质控表成功后,删除原样本
                var IsDel = $("#isDel").prop("checked");
                //如果原检验单有结果的项目都转换成功，并且标记删除源检验单的情况下，原检验单打标记：删除作废MainStatusID=-2 
                if (IsDel) {
                    if (res.BoolFlag) {	//返回参数中的BoolFlag为true表示有结果的项目都转换成功了
                        me.updateOne();
                    } else {
                        layer.confirm('原检验单只有部分项目转换成成功,是否删除作废?', { btn: ['确定', '取消'], icon: 3, title: '提示' }, function () {
                            me.updateOne();
                        }, function () {
                            JShell.Msg.alert('转换质控样本成功');
                            return;
                        });
                    }
                } else {
                    uxbase.MSG.onSuccess("转换质控样本成功!");
                }
            } else {
                uxbase.MSG.onError(res.ErrorInfo);
            }
        });
    };
    /**检验单打删除标记*/
    app.updateOne = function () {
        var me = this,
            id = me.params.testFormID,
            url = me.url.EditUrl;
        if (!id) return;
        var load = layer.load();
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify({ entity: { Id: id, MainStatusID: -2 }, fields:'Id,MainStatusID' })
        }, function (res) {
            layer.close(load);
            if (res.success) {
                uxbase.MSG.onSuccess("转换质控样本成功!");
            } else {
                uxbase.MSG.onError("转换质控样本失败!");
            }
        });
    };
    //初始化
    app.init();
});