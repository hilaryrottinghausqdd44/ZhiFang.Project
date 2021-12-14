/**
 * 小组仪器分配
 * @author zhangda
 * @version 2019-07-15
 */
var itemAllot = new Object();
//项目信息
itemAllot.itemData = {};
//小组选中数据
itemAllot.itemAllotSectionData = [];
//服务路径地址
itemAllot.url = {};
//设置服务路径
itemAllot.setUrl = function () {
    itemAllot.url = {
        //获得所有小组
        getSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true&where=IsUse=true',//获得小组
        //获得所属小组
        selectItemSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true&sort=[{property:"LBSectionItem_LBSection_DispOrder",direction:"ASC"}]&fields=LBSectionItem_Id,LBSectionItem_LBItem_Id,LBSectionItem_LBSection_Id,LBSectionItem_LBSection_SName,LBSectionItem_LBSection_CName,LBSectionItem_IsDefult,LBSectionItem_DefultValue,LBSectionItem_DispOrder,LBSectionItem_DataAddTime',
        //取消小组分配
        delItemSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DeleteLBSectionItem',
        //获得所属仪器
        selectItemEquipUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemByHQL?isPlanish=true&sort=[{property:"LBEquipItem_LBEquip_DispOrder",direction:"ASC"}]&fields=LBEquipItem_Id,LBEquipItem_LBItem_Id,LBEquipItem_LBEquip_Id,LBEquipItem_LBEquip_CName,LBEquipItem_LBEquip_SName,LBEquipItem_LBEquip_LBSection_Id,LBEquipItem_LBEquip_Computer,LBEquipItem_LBEquip_ComProgram,LBEquipItem_CompCode,LBEquipItem_IsReserve,LBEquipItem_DataAddTime',
        //取消仪器分配
        delItemEquipUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DeleteLBEquipItem',
        //修改仪器
        updateItemEquipUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipItemByField'
    };
}
//初始化--最先执行
itemAllot.init = function () {
    itemAllot.itemData = common.checkData[common.checkData.length - 1];
    if(itemAllot.itemData.LBItem_GroupType > 0){
        if (!$("#equipTableBox").hasClass("layui-hide")){
            $("#equipTableBox").addClass("layui-hide");
        }
	}else{
        if ($("#equipTableBox").hasClass("layui-hide")){
            $("#equipTableBox").removeClass("layui-hide");
        }
    }
    itemAllot.setUrl();
    itemAllot.itemSectionWhere = "ItemID=" + itemAllot.itemData.LBItem_Id;
    itemAllot.sectionItemTableRender(itemAllot.itemSectionWhere);
    itemAllot.getServerData();
}
//存放所有小组Id和名称
itemAllot.SectionData = [];
//获得所有小组
itemAllot.getServerData = function () {
    uxutil.server.ajax({
        url: itemAllot.url.getSectionUrl
    }, function (res) {
        if (res.success) {
            itemAllot.SectionData = [];
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                if (data.length > 0) {
                    for (var i in data) {
                        itemAllot.SectionData.push({ Id: data[i].LBSection_Id, CName: data[i].LBSection_CName });
                    }
                    itemAllot.itemEquipWhere = "ItemID=" + itemAllot.itemData.LBItem_Id;
                    itemAllot.equipItemTableRender(itemAllot.itemEquipWhere);
                }
            }
        } else {
            layer.msg("小组查询失败！", { icon: 5, anim: 6 });
        }
    });
}
//小组初始化
itemAllot.sectionItemTableRender = function (where) {
    table.render({
        elem: '#sectionItemTable',
        height: 260,
        defaultToolbar: [],
        size: 'sm', //小尺寸的表格
        toolbar: '#toolbarItemSection',
        url: itemAllot.url.selectItemSectionUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBSectionItem_Id',
                width: 60,
                title: '主键ID',
                hide: true
            }, {
                field: 'LBSectionItem_LBItem_Id',
                title: '项目Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBSectionItem_LBSection_Id',
                title: '小组Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBSectionItem_LBSection_CName',
                title: '小组名称',
                minWidth: 120
            }, {
                field: 'LBSectionItem_LBSection_SName',
                title: '小组简称',
                minWidth: 120
            }, {
                field: 'LBSectionItem_IsDefult',
                title: '是否缺省',
                minWidth: 100,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBSectionItem_IsDefult.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBSectionItem_DefultValue',
                title: '缺省值',
                minWidth: 120
            }, {
                field: 'LBSectionItem_DataAddTime',
                title: '新增时间',
                minWidth: 160
            }
            ]
        ],
        page: false,
        limit: 1000,
        //limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            //已存在的小组
            itemAllot.hasSectionIdList = [];
            if (count > 0) {
                //默认选中第一行
                $("#sectionItemTable+div .layui-table-body table.layui-table tbody tr:first").click();
                //获得已有仪器
                for (var i in res.data) {
                    itemAllot.hasSectionIdList.push(res.data[i].LBSectionItem_LBSection_Id);
                }
            }
        },
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
//监听行单击事件
table.on('row(sectionItemTable)', function (obj) {
    var data = obj.data;
    //标注选中样式
    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    //暂存选中数据
    if (itemAllot.itemAllotSectionData.length > 0 && itemAllot.itemAllotSectionData[0].LBSectionItem_Id == data.LBSectionItem_Id) return;
    itemAllot.itemAllotSectionData = [];//重置
    itemAllot.itemAllotSectionData.push(data);
});
//小组上面的工具栏
table.on('toolbar(sectionItemTable)', function (obj) {
    switch (obj.event) {
        case 'add':
            //存在小组--新增时缺省默认false
            if (table.cache.sectionItemTable.length > 0) {
                var IsDefault = "false";
            } else {
                var IsDefault = "true";
            }
            var flag = false;
            layer.open({
                type: 2,
                title: ['分配小组'],
                //skin: 'layui-layer-molv',
                area: ['400px', '310px'],
                content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/allot_section/allotSection.html?ItemID=' + itemAllot.itemData.LBItem_Id + '&CName=' + itemAllot.itemData.LBItem_CName + "&DataTimeStamp=" + itemAllot.itemData.LBItem_DataTimeStamp + "&IsDefault=" + IsDefault,
                cancel: function (index, layero) {
                    flag = true;
                },
                end: function () {
                    if (!flag) {
                        layer.msg("保存成功!", { icon: 6, anim: 0 });
                        itemAllot.sectionItemTableRender(itemAllot.itemSectionWhere);
                    }
                }
            });
            break;
        case 'del':
            if (itemAllot.itemAllotSectionData.length > 0) {
                //判断是否能取消分配
                var DataAddTime = itemAllot.itemAllotSectionData[itemAllot.itemAllotSectionData.length - 1].LBSectionItem_DataAddTime;
                if (itemAllot.isToday(DataAddTime)) {//今天分配的直接取消
                    itemAllot.cancelSectionAllot();
                } else {
                    layer.confirm('确定取消分配吗?', { icon: 3, title: '提示' }, function (index) {
                        itemAllot.cancelSectionAllot();
                    });
                }
            }
            break;
    };
});
//取消小组分配
itemAllot.cancelSectionAllot = function () {
    var delIdStr = "";
    for (var i = 0; i < itemAllot.itemAllotSectionData.length; i++) {
        var Id = itemAllot.itemAllotSectionData[i].LBSectionItem_Id;
        if (i == 0) {
            delIdStr += Id;
        } else {
            delIdStr += "," + Id;
        }
    }
    itemAllot.delFun(itemAllot.url.delItemSectionUrl, delIdStr, function () {
        layer.closeAll();
        layer.msg("删除成功！", { icon: 6, anim: 0 });
        itemAllot.itemAllotSectionData = [];//重置
        table.reload('sectionItemTable', {
            url: itemAllot.url.selectItemSectionUrl + "&where=" + itemAllot.itemSectionWhere,
            where: {
                time: new Date().getTime()
            }
        });
    });
}

//仪器选中数据
itemAllot.itemAllotEquipData = [];
//仪器初始化
itemAllot.equipItemTableRender = function (where) {
    table.render({
        elem: '#equipItemTable',
        height: 'full-400',
        defaultToolbar: [],
        size: 'sm', //小尺寸的表格
        toolbar: '#toolbarItemEquip',
        url: itemAllot.url.selectItemEquipUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBEquipItem_Id',
                width: 60,
                title: '主键ID',
                hide: true
            }, {
                field: 'LBEquipItem_LBItem_Id',
                title: '项目Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBEquipItem_LBEquip_Id',
                title: '仪器Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBEquipItem_LBEquip_CName',
                title: '仪器名称',
                minWidth: 120
            }, {
                field: 'LBEquipItem_LBEquip_SName',
                title: '仪器简称',
                minWidth: 120
            }, {
                field: 'LBEquipItem_LBEquip_LBSection_Id',
                title: '检验小组',
                minWidth: 120,
                templet: function (data) {
                    var CName = "";
                    for (var i in itemAllot.SectionData) {
                        if (itemAllot.SectionData[i].Id == data.LBEquipItem_LBEquip_LBSection_Id) {
                            CName = itemAllot.SectionData[i].CName;
                            break;
                        }
                    }
                    return CName;
                }
            }, {
                field: 'LBEquipItem_CompCode',
                title: '仪器对照通道号',
                edit:'text',
                minWidth: 140
            }, {
                field: 'LBEquipItem_LBEquip_Computer',
                title: '计算机',
                minWidth: 120
            }, {
                field: 'LBEquipItem_LBEquip_ComProgram',
                title: '程序名',
                minWidth: 120
            }, {
                field: 'LBEquipItem_DataAddTime',
                title: '新增时间',
                minWidth: 160
            }
            ]
        ],
        page: false,
        limit: 1000,
        //limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            //已存在的仪器
            itemAllot.hasEquipIdList = [];
            if (count > 0) {
                //默认选中第一行
                $("#equipItemTable+div .layui-table-body table.layui-table tbody tr:first").click();
                //获得已有仪器
                for (var i in res.data) {
                    itemAllot.hasEquipIdList.push(res.data[i].LBEquipItem_LBEquip_Id);
                }
            }
        },
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
//监听行单击事件
table.on('row(equipItemTable)', function (obj) {
    var data = obj.data;
    //标注选中样式
    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    //暂存选中数据
    if (itemAllot.itemAllotEquipData.length > 0 && itemAllot.itemAllotEquipData[0].LBEquipItem_Id == data.LBEquipItem_Id) return;
    itemAllot.itemAllotEquipData = [];//重置
    itemAllot.itemAllotEquipData.push(data);
});
//仪器上面的工具栏
table.on('toolbar(equipItemTable)', function (obj) {
    switch (obj.event) {
        case 'add':
            var flag = false;
            layer.open({
                type: 2,
                title: ['分配仪器'],
                //skin: 'layui-layer-molv',
                area: ['400px', '260px'],
                content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/allot_equip/allotEquip.html?ItemID=' + itemAllot.itemData.LBItem_Id + '&CName=' + itemAllot.itemData.LBItem_CName + "&DataTimeStamp=" + itemAllot.itemData.LBItem_DataTimeStamp,
                cancel: function (index, layero) {
                    flag = true;
                },
                end: function () {
                    if (!flag) {
                        layer.msg("保存成功!", { icon: 6, anim: 0 });
                        itemAllot.equipItemTableRender(itemAllot.itemEquipWhere);
                    }
                }
            });
            break;
        case 'del':
            if (itemAllot.itemAllotEquipData.length > 0) {
                //判断是否能取消分配
                var DataAddTime = itemAllot.itemAllotEquipData[itemAllot.itemAllotEquipData.length - 1].LBEquipItem_DataAddTime;
                if (itemAllot.isToday(DataAddTime)) {//今天分配的直接取消
                    itemAllot.cancelEquipAllot();
                } else {
                    layer.confirm('确定取消分配吗?', { icon: 3, title: '提示' }, function (index) {
                        itemAllot.cancelEquipAllot();
                    });
                }
            }
            break;
    };
});

//取消仪器分配
itemAllot.cancelEquipAllot = function () {
    var delIdStr = "";
    for (var i = 0; i < itemAllot.itemAllotEquipData.length; i++) {
        var Id = itemAllot.itemAllotEquipData[i].LBEquipItem_Id;
        if (i == 0) {
            delIdStr += Id;
        } else {
            delIdStr += "," + Id;
        }
    }
    itemAllot.delFun(itemAllot.url.delItemEquipUrl, delIdStr, function () {
        layer.closeAll();
        layer.msg("删除成功！", { icon: 6, anim: 0 });
        itemAllot.itemAllotEquipData = [];//重置
        table.reload('equipItemTable', {
            url: itemAllot.url.selectItemEquipUrl + "&where=" + itemAllot.itemEquipWhere,
            where: {
                time: new Date().getTime()
            }
        });
    });
}
//监听单元格编辑
table.on('edit(equipItemTable)', function (obj) {
    var entity = {
        Id: obj.data.LBEquipItem_Id,
        CompCode: obj.value
    };
    common.load.loadShow();
    uxutil.server.ajax({
        type: 'post',
        dataType: 'json',
        contentType: "application/json",
        data: JSON.stringify({ entity: entity, fields: 'Id,CompCode' }),
        url: itemAllot.url.updateItemEquipUrl
    }, function (res) {
        common.load.loadHide();
        if (res.success) {
            layer.msg("通道号修改成功!", { icon: 6, anim: 0 });
        } else {
            layer.msg("通道号修改失败！", { icon: 5, anim: 6 });
        }
    });
});
//删除---取消分配
itemAllot.delFun = function (url, delIDList, callBack) {
    common.load.loadShow();
    uxutil.server.ajax({
        type: 'post',
        dataType: 'json',
        contentType: "application/json",
        url: url,
        data: JSON.stringify({ delIDList: delIDList })
    }, function (res) {
        common.load.loadHide();
        if (res.success) {
            if (typeof callBack) {
                callBack();
            }
        } else {
            layer.msg(res.ErrorInfo, { icon: 5, anim: 6 });
        }
    });
}
//判断是否是今天
itemAllot.isToday = function (date) {
    var year = new Date().getFullYear();
    var month = new Date().getMonth() + 1;
    var strDate = new Date().getDate();
    var today = year + "-" + month + "-" + strDate + " 00:00:00";
    if (new Date(date).getTime() > new Date(today).getTime()) {
        return true;
    } else {
        return false;
    }
}