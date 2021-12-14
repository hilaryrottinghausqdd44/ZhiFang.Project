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
        DataTimeStamp: null
    };
    
    var hasEquipIdList = parent.itemAllot.hasEquipIdList;//已加入的仪器

    var url = {
        addSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddDelLBSectionItem',
        getEquipUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true&fields=LBEquip_Id,LBEquip_CName,LBEquip_LBSection_Id',
        saveUrl: uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBEquipItem'
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
    //获得仪器
    function getEquipInfo() {
        var where = "IsUse=true";
        if (hasEquipIdList.length > 0 ) {
            where += " and Id not in (" + hasEquipIdList.join(',') + ")";
        }
        uxutil.server.ajax({
            url: url.getEquipUrl + "&where=" + where
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    if (data.length > 0) {
                        var html = "<option value=''>请选择</option>";
                        for (var i in data) {
                            var SectionId = data[i].LBEquip_LBSection_Id != "" ? data[i].LBEquip_LBSection_Id : 0;
                            html += '<option data-sectionId=' + SectionId + ' value="' + data[i].LBEquip_Id + '">' + data[i].LBEquip_CName + '</option>';
                        }
                        $("#EquipID").html(html);
                        form.render('select');
                    }
                }
            } else {
                layer.msg("仪器查询失败！", { icon: 5, anim: 6 });
            }
        });
    }
    //保存
    $("#save").click(function () {
        var EquipID = $("#EquipID").val();
        var CompCode = $("#CompCode").val();
        if (EquipID != "") {
            var SectionID = $("#EquipID option[value=" + EquipID + "]")[0].dataset.sectionid;
            var arr = paramsObj.DataTimeStamp.split(",");
            var DataTimeStamp = [];
            for (var i in arr) {
                DataTimeStamp.push(Number(arr[i]));
            }
            var entity = {
                LBItem: { Id: paramsObj.ItemID, DataTimeStamp: DataTimeStamp },
                LBEquip: { Id: EquipID, DataTimeStamp: DataTimeStamp },
                CompCode: CompCode,
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
                if (res.success) {
                    if (SectionID && SectionID > 0) {
                        var addEntityList = [{
                            LBItem: { Id: paramsObj.ItemID, DataTimeStamp: DataTimeStamp },
                            LBSection: { Id: SectionID, DataTimeStamp: DataTimeStamp },
                            IsDefult: false,
                            DispOrder: 0
                        }];
                        uxutil.server.ajax({
                            type: 'post',
                            dataType: 'json',
                            contentType: "application/json",
                            data: JSON.stringify({ addEntityList: addEntityList, isCheckEntityExist: true, delIDList: "" }),
                            url: url.addSectionUrl
                        }, function (res) {
                            load.loadHide();
                            parent.layer.closeAll();
                            if (res.success) {
                                parent.itemAllot.sectionItemTableRender("ItemID=" + paramsObj.ItemID);
                            }
                        });
                    } else {
                        load.loadHide();
                        parent.layer.closeAll();
                    }
                } else {
                    load.loadHide();
                    layer.msg("保存失败！", { icon: 5, anim: 6 });
                }
            });
        } else {
            layer.msg("请选择仪器！", { icon: 5, anim: 6 });
        }
    });
});