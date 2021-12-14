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
        ItemID: null,
        CName: null,
        DataTimeStamp: null,
        IsDefault: true
        
    };
    var hasSectionIdList = parent.itemAllot.hasSectionIdList;//已加入的小组
    var url = {
        getSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',//获得小组
        saveUrl: uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBSectionItem'
    };
    init();
    //初始化方法
    function init() {
        getParams();
        //存在小组则默认为否
        if (paramsObj.IsDefault == "false") {
            $("#IsDefault").next('.layui-form-switch').removeClass('layui-form-onswitch');
            $("#IsDefault").next('.layui-form-switch').children("em").html("否");
            $("#IsDefault")[0].checked = false;
        }
        getSectionInfo();
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
    //获得小组
    function getSectionInfo() {
        var where = "IsUse=true";
        if (hasSectionIdList.length > 0) {
            where += " and Id not in (" + hasSectionIdList.join(',') + ")";
        }
        uxutil.server.ajax({
            url: url.getSectionUrl + "&where=" + where
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        var html = "<option value=''>请选择</option>";
                        for (var i in data) {
                            html += '<option value="' + data[i].LBSection_Id + '">' + data[i].LBSection_CName + '</option>';
                        }
                        $("#SectionID").html(html);
                        form.render('select');
                    }
                }
            } else {
                layer.msg("小组查询失败！", { icon: 5, anim: 6 });
            }
        });
    }
    //保存
    $("#save").click(function () {
        var sectionId = $("#SectionID").val();
        var IsDefault = $("#IsDefault+div.layui-form-switch").hasClass("layui-form-onswitch");
        var DefultValue = $("#DefultValue").val();
        if (sectionId.length != "") {
            var arr = paramsObj.DataTimeStamp.split(",");
            var DataTimeStamp = [];
            for (var i in arr) {
                DataTimeStamp.push(Number(arr[i]));
            }
            var entity = {
                LBItem: { Id: paramsObj.ItemID, DataTimeStamp: DataTimeStamp },
                LBSection: { Id: sectionId, DataTimeStamp: DataTimeStamp },
                IsDefult: IsDefault,
                DefultValue: DefultValue,
                DispOrder: 0
            };
            load.loadShow();
            uxutil.server.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: entity }),
                url: url.saveUrl
            }, function (res) {
                load.loadHide();
                if (res.success) {
                    parent.layer.closeAll();
                } else {
                    layer.msg("保存失败！", { icon: 5, anim: 6 });
                }
            });
        } else {
            layer.msg("请选择小组！", { icon: 5, anim: 6 });
        }
    });
});