layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form', 'element', 'laydate'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        form = layui.form,
        table = layui.table,
        uxutil = layui.uxutil;
    //遮罩
    var load = {
        loadIndex: null,
        loadShow: function () {
            if (load.loadIndex == null) {
                load.loadIndex = layer.open({ type: 3 });
            }
        },
        loadHide: function () {
            if (load.loadIndex != null) {
                layer.close(load.loadIndex);
                load.loadIndex = null;
            }
        }
    };
    var paramsObj = {
        ItemId:null,
        Id: null,
        TypeCode: null,
        TypeName: null,
        type: null,
        DisOrder:null
    };
    var url = {
        getPinYinZiTouUrl:uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou?chinese=',//获得拼音字头
        getSampleTypeUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true&sort=[{property:"LBSampleType_DispOrder",direction:"ASC"}]',

        getItemPhraseUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true',
        updateUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhraseByField',
        addUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase'
    };
    init();
    //初始化方法
    function init() {
        getParams();
        getEquipInfo();
    }
    //获得参数
    function getParams() {
        var params = location.search.split("?")[1].split("&");
        //参数赋值
        for (var j in paramsObj) {
            for (var i = 0; i < params.length; i++) {
                if (j.toUpperCase() == params[i].split("=")[0].toUpperCase()) {
                    paramsObj[j] = decodeURIComponent(params[i].split("=")[1]);
                }
            }
        }
    }
    //获得数据
    function getEquipInfo() {
        load.loadShow();
        var SampleTypeID = 0;
        if (paramsObj.type == 'edit') {
            uxutil.server.ajax({
                url: url.getItemPhraseUrl + "&where=Id=" + paramsObj.Id
            }, function (res) {
                if (res.success) {
                    if (res.ResultDataValue) {
                        var data = JSON.parse(res.ResultDataValue).list;
                        var html = "";
                        if (data.length > 0) {
                            //赋值
                            SampleTypeID = data[0].LBPhrase_SampleTypeID;
                            $("#SampleTypeID").val(SampleTypeID);
                            $("#CName").val(data[0].LBPhrase_CName);
                            $("#Shortcode").val(data[0].LBPhrase_Shortcode);
                            $("#PinYinZiTou").val(data[0].LBPhrase_PinYinZiTou);
                            $("#DispOrder").val(data[0].LBPhrase_DispOrder);
                            $("#Comment").val(data[0].LBPhrase_Comment);
                            if (data[0].LBPhrase_IsUse.toString() == "true") {
                                if (!$("#IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                    $("#IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                                    $("#IsUse").next('.layui-form-switch').children("em").html("是");
                                }
                            } else {
                                if ($("#IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                    $("#IsUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
                                    $("#IsUse").next('.layui-form-switch').children("em").html("否");
                                }
                            }
                            form.render();
                        }
                    }
                } else {
                    layer.msg("样本类型字典查询失败！", { icon: 5, anim: 6 });
                }
            });
        } else if (paramsObj.type == 'add') {
            $("#DispOrder").val(paramsObj.DisOrder);
        }
        //获得样本类型
        uxutil.server.ajax({
            url: url.getSampleTypeUrl + "&where=IsUse=1"
        }, function (res) {
            load.loadHide();
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    var html = "<option value=''>请选择</option>";
                    if (data.length > 0) {
                        for (var i in data) {
                            html += '<option value="' + data[i].LBSampleType_Id + '">' + data[i].LBSampleType_CName + '</option>';
                        }
                        $("#SampleTypeID").html(html);
                        if (paramsObj.type == 'edit') {
                            $("#SampleTypeID").val(SampleTypeID);
                        }
                        form.render('select');
                    }
                }
            } else {
                layer.msg("样本类型字典查询失败！", { icon: 5, anim: 6 });
            }
        });
    }
    //监听名称输入同步拼音字头
    $('#CName').bind('input propertychange', function () {
        getPinYinZiTou($(this).val(), function (str) {
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
            url: url.getPinYinZiTouUrl + encodeURI(chinese)
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
    //保存
    $("#save").click(function () {
        var SampleTypeID = $("#SampleTypeID").val();
        var CName = $("#CName").val();
        if (CName == "") {
            layer.msg("请填写必填项！", { icon: 5, anim: 6 });
            return;
        }
        var Shortcode = $("#Shortcode").val();
        var PinYinZiTou = $("#PinYinZiTou").val();
        var DispOrder = $("#DispOrder").val();
        var IsUse = $("#IsUse+div.layui-form-switch").hasClass("layui-form-onswitch");
        var Comment = $("#Comment").val();
        var entity = {
            SampleTypeID: SampleTypeID != '' ? SampleTypeID : null,
            CName: CName,
            Shortcode: Shortcode,
            PinYinZiTou: PinYinZiTou,
            DispOrder: DispOrder,
            IsUse: IsUse,
            Comment: Comment,
            TypeCode: paramsObj.TypeCode,
            TypeName: paramsObj.TypeName,
            ObjectType: 2,
            ObjectID: paramsObj.ItemId,
            PhraseType:"ItemPhrase"
        };
        load.loadShow();
        if (paramsObj.type == 'add') {
            uxutil.server.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: entity }),
                url: url.addUrl
            }, function (res) {
                load.loadHide();
                if (res.success) {
                    parent.layer.closeAll();
                } else {
                    layer.msg("新增失败！", { icon: 5, anim: 6 });
                }
            });
        } else if (paramsObj.type == 'edit') {
            entity.Id = paramsObj.Id;
            var fields = "Id,SampleTypeID,CName,Shortcode,PinYinZiTou,DispOrder,IsUse,Comment,TypeName,ObjectType,ObjectID,PhraseType";
            uxutil.server.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: entity, fields: fields }),
                url: url.updateUrl
            }, function (res) {
                load.loadHide();
                if (res.success) {
                    parent.layer.closeAll();
                } else {
                    layer.msg("编辑失败！", { icon: 5, anim: 6 });
                }
            });
        }
    });
});