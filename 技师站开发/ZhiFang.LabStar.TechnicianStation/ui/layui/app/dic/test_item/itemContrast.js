/**
 * 项目对照
 * @author zhangda
 * @version 2021-10-12
 */
var itemContrast = new Object();
//项目信息
itemContrast.itemData = {};
//项目对照type值
itemContrast.itemContrastType = 'show';
//项目对照选中数据
itemContrast.itemContrastData = [];
//是否是第一次
itemContrast.isFirst = true;
//对接系统
itemContrast.linkSystem = [];
//新增Dom
itemContrast.addDom = [
    '<div class="layui-card">',
    '<div class="layui-card-body">',
    '<div class="layui-form">',
    '<div class="layui-form-item">',
    '<label class="layui-form-label">对照类型</label>',
    '<div class="layui-input-block">',
    '<select name="LBItemCodeLink_TransTypeID" id="LBItemCodeLink_TransTypeID" lay-filter="LBItemCodeLink_TransTypeID">',
    '<option value="0">进出</option>',
    '<option value="1">进</option>',
    '<option value="2">出</option>',
    '</select>',
    '</div>',
    '</div>',
    '<div class="layui-form-item">',
    '<label class="layui-form-label">对接系统</label>',
    '<div class="layui-input-block">',
    '<select name="LBItemCodeLink_LinkSystemName" id="LBItemCodeLink_LinkSystemName" lay-filter="LBItemCodeLink_LinkSystemName">',
    '</select>',
    '</div>',
    '</div>',
    '<div class="layui-form-item">',
    '<label class="layui-form-label">对照编码</label>',
    '<div class="layui-input-block">',
    '<input type="text" name="LBItemCodeLink_LinkDicDataCode" id="LBItemCodeLink_LinkDicDataCode" autocomplete="off" class="layui-input"/>',
    '</div>',
    '</div>',
    '<div class="layui-form-item">',
    '<label class="layui-form-label">对照名称</label>',
    '<div class="layui-input-block">',
    '<input type="text" name="LBItemCodeLink_LinkDicDataName" id="LBItemCodeLink_LinkDicDataName" autocomplete="off" class="layui-input"/>',
    '</div>',
    '</div>',
    '</div>',
    '</div>',
    '</div>',
    '<style>',
    '.layui-form-select dl{ top:28px; }',
    '.layui-form-selectup dl { top: auto; bottom: 28px; }',
    '</style>'
];
//服务路径地址
itemContrast.url = {};
//设置服务路径
itemContrast.setUrl = function () {
    var me = this;
    me.url = {
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBItemCodeLink',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCodeLinkByHQL?isPlanish=true&sort=[{property:"LBItemCodeLink_DispOrder",direction:"ASC"}]',
        delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBItemCodeLink',
        addOrUpdateUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddOrUPDateLBItemCodeLink',
        //获得对接系统（暂时是就诊类型）
        GET_SICKTYPEURL: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true&sort=[{property:"LBSickType_DispOrder",direction:"ASC"}]',
    };
}
//项目对照初始化--最先执行
itemContrast.init = function () {
    var me = this;
    me.itemData = common.checkData[common.checkData.length - 1];
    me.setUrl();
    me.itemContrastWhere = "DicDataID=" + me.itemData.LBItem_Id;
    me.initSystem(function () {
        me.itemContrastTableRender(me.itemContrastWhere);
    });
    me.initListener();
}
//获得对接系统
itemContrast.initSystem = function (callBack) {
    var me = this,
        url = me.url.GET_SICKTYPEURL + "&where=IsUse=1&fields=LBSickType_Id,LBSickType_Shortcode,LBSickType_CName";
    if (!itemContrast.isFirst) {
        callBack();
        return;
    }
    uxutil.server.ajax({
        url: url
    }, function (res) {
        me.isFirst = false;
        if (res.success === true) {
            if (res.ResultDataValue) {
                var data = $.parseJSON(res.ResultDataValue).list;
                $.each(data, function (i, item) {
                    me.linkSystem.push({ Id: item["LBSickType_Id"], Code: item["LBSickType_Shortcode"], Name: item["LBSickType_CName"] });
                });
            }
        }
        callBack();
    });
};
//项目对照初始化
itemContrast.itemContrastTableRender = function (where) {
    table.render({
        elem: '#itemContrastTable',
        height: 'full-120',
        defaultToolbar: [],
        toolbar: '#itemContrastTableOperation',
        size: 'sm', //小尺寸的表格
        url: itemContrast.url.selectUrl + "&where=" + where,
        cols: [
            [
            {type: 'checkbox', width: 36 },
            {field: 'LBItemCodeLink_LabID', width: 120, title: '实验室ID', hide: true },
            {field: 'LBItemCodeLink_Id',width: 60,title: '主键ID',hide: true},
            {field: 'LBItemCodeLink_LinkSystemID',title: '对接系统ID',width: 100,hide: true},
            {field: 'LBItemCodeLink_LinkSystemCode',title: '对接系统编码',width: 100,hide: true},
            {field: 'LBItemCodeLink_LinkSystemName',title: '对接系统名称',width: 120,hide: false},
            {field: 'LBItemCodeLink_DicDataID',title: '项目主键ID',width: 120,hide: true},
            {field: 'LBItemCodeLink_DicDataCode',title: '项目编码',width: 120,hide: true},
            {field: 'LBItemCodeLink_DicDataName',title: '项目名称',width: 120},
            {field: 'LBItemCodeLink_LinkDicDataCode',title: '对照编码',edit: 'text',width: 120},
            {field: 'LBItemCodeLink_LinkDicDataName',title: '对照名称',edit: 'text',minWidth: 120},
            {field: 'LBItemCodeLink_TransTypeID',title: '对照类型',width: 120,templet: '#TransTypeIDTool'},
            {field: 'LBItemCodeLink_IsUse',title: '是否使用',width: 100,hide: true,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data["LBItemCodeLink_IsUse"].toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            },
            {field: 'LBItemCodeLink_DispOrder',title: '显示次序',width: 100,hide: false},
            {field: 'LBItemCodeLink_Comment',title: '备注',width: 200,hide: true},
            {field: 'Tab', width: 100, title: '用于判断行是否有修改数据', hide: true, sort: false }
            ]
        ],
        page: false,
        limit: 1000,
        //limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            itemContrast.itemContrastData = [];//重置
            if (count > 0) {
                //默认选中第一行
                $("#itemContrastTable+div .layui-table-body table.layui-table tbody tr:first").click();
            } else {
                itemContrast.itemContrastType = 'add';
            }
            $("select[name='TransTypeID']").parent('div.layui-table-cell').css('overflow', 'visible');
            //匹配对照类型下拉数据
            var that = this.elem.next();
            for (var i = 0; i < res.data.length; i++) {
                var TransTypeID = res.data[i].LBItemCodeLink_TransTypeID;
                var trRow = that.find(".layui-table-box tbody tr[data-index='" + i + "']");
                $(trRow).find("td").each(function () {
                    var fieldName = $(this).attr("data-field");
                    var selectJq = $(this).find("select");
                    //选中值
                    if (selectJq.length == 1) {
                        if (TransTypeID == res.data[i][fieldName]) {
                            $(this).children().children().val(TransTypeID == null ? 0 : TransTypeID);
                        }
                    }
                });
            }
            form.render('select');
            //$("select[name='TransTypeID']+div.layui-form-select").find("input").css('height', '30px'); //小表格存在下拉框时设置
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
            var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : { count:0,list:[] };
            //如果不存在某对接系统 则加入一条该系统数据
            $.each(itemContrast.linkSystem, function (a, itemA) {
                var flag = false;//是否已存在
                $.each(data.list, function (b, itemB) {
                    if (itemA["Id"] == itemB["LBItemCodeLink_LinkSystemID"]) {
                        flag = true;
                        return false;
                    }
                });
                //不存在加入空数据
                if (!flag) {
                    data.list.push({
                        LBItemCodeLink_LinkSystemID: itemA["Id"],
                        LBItemCodeLink_LinkSystemCode: itemA["Code"],
                        LBItemCodeLink_LinkSystemName: itemA["Name"],
                        LBItemCodeLink_DicDataID: itemContrast.itemData.LBItem_Id,
                        LBItemCodeLink_DicDataCode: itemContrast.itemData.LBItem_Shortcode || "",
                        LBItemCodeLink_DicDataName: itemContrast.itemData.LBItem_CName,
                        LBItemCodeLink_LinkDicDataCode: "",
                        LBItemCodeLink_LinkDicDataName: "",
                        LBItemCodeLink_TransTypeID: 0,
                        LBItemCodeLink_IsUse: 1,
                        LBItemCodeLink_DispOrder: data.list.length+1
                    });
                }
            });
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
//监听
itemContrast.initListener = function () {
    //监听行单击事件
    table.on('row(itemContrastTable)', function (obj) {
        var data = obj.data;
        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        //暂存选中数据
        if (itemContrast.itemContrastData.length > 0 && itemContrast.itemContrastData[0].LBItemCodeLink_Id == data.LBItemCodeLink_Id) return;
        itemContrast.itemContrastData = [];//重置
        itemContrast.itemContrastData.push(data);
        itemContrast.itemContrastType = "show";
    });
    //监听单元格编辑 编辑以后给列打上标记
    table.on('edit(itemContrastTable)', function (obj) {
        var value = obj.value, //得到修改后的值
            data = obj.data,//得到所在行所有键值
            field = obj.field; //得到字段
        var tableCache = table.cache["itemContrastTable"];
        var dataindex = obj.tr[0].dataset.index;
        tableCache[dataindex].Tab = true;
    });
    //监听列表内下拉框修改
    form.on('select(TransTypeID)', function (data) {
        //这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
        var elem = data.othis.parents('tr');
        var dataindex = elem.attr("data-index");
        var tableCache = table.cache["itemContrastTable"];

        $.each(tableCache, function (index, value) {
            if (index == dataindex) {
                tableCache[index].LBItemCodeLink_TransTypeID = data.value;
                tableCache[index].Tab = true;

            }
        });
    });
    //table上面的工具栏
    table.on('toolbar(itemContrastTable)', function (obj) {
        switch (obj.event) {
            case 'add':
                itemContrast.onAddClick();
                break;
            case 'save':
                itemContrast.onSaveClick();
                break;
            case 'del':
                itemContrast.onDelClick();
                break;
        };
    });
};
//新增
itemContrast.onAddClick = function () {
    var me = this;
    layer.open({
        type: 1,
        title: '追加对照',
        content: me.addDom.join(''),
        btn: ['确认', '取消'],
        yes: function (index, layero) {
            var entity = {
                LinkSystemID: $("#LBItemCodeLink_LinkSystemName").val(),//对接系统ID
                LinkSystemCode: $("#LBItemCodeLink_LinkSystemName option:selected").attr("data-code"),//对接系统编码
                LinkSystemName: $("#LBItemCodeLink_LinkSystemName option:selected").text(),//对接系统名称
                DicDataID: me.itemData.LBItem_Id,
                DicDataCode: me.itemData.LBItem_Shortcode,
                DicDataName: me.itemData.LBItem_CName,
                LinkDicDataCode: $("#LBItemCodeLink_LinkDicDataCode").val(),
                LinkDicDataName: $("#LBItemCodeLink_LinkDicDataName").val(),
                TransTypeID: $("#LBItemCodeLink_TransTypeID").val(),
                DispOrder: 1,
                IsUse: 1
            };
            me.onAddSave(entity, function () {
                table.reload('itemContrastTable', {
                    url: me.url.selectUrl + "&where=" + me.itemContrastWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
                layer.close(index);
                layer.msg("追加成功！", { icon: 6, anim: 0 });
            });
        },
        btn2: function (index) {
            layer.close(index);
        },
        success: function (layero, index) {
            var html = [];
            $.each(me.linkSystem, function (i,item) {
                html.push('<option value="' + item["Id"] +'" data-code="'+ item["Code"] +'">'+ item["Name"] +'</option>');
            });
            $("#LBItemCodeLink_LinkSystemName").html(html.join(''));
            form.render();
        }
    });
};
//新增服务
itemContrast.onAddSave = function (entity,callBack) {
    var me = this,
        url = me.url.addUrl,
        entity = entity || null;
    if (!entity) return;
    var configs = {
        type: "POST",
        url: url,
        data: JSON.stringify({ entity: entity })
    };
    var load = layer.load();
    uxutil.server.ajax(configs, function (data) {
        layer.close(load);
        if (data.success) {
            callBack();
        } else {
            layer.msg(data.ErrorInfo || "追加失败！", { icon: 5, anim: 0 });
        }
    });

};
//删除
itemContrast.onDelClick = function () {
    var me = this,
        checkData = table.checkStatus('itemContrastTable').data;
    if (checkData.length === 0) {
        layer.msg('请勾选一行！');
    } else {
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var load = layer.load();//开启加载层
            var len = checkData.length, successCount = 0, errorCount = 0;
            $.each(checkData, function (i, item) {
                var url = me.setUrl.delUrl + '?id=' + item.LBItemCodeLink_Id;
                uxutil.server.ajax({
                    url: url
                }, function (data) {
                    if (data.success === true) {
                        successCount++;
                    } else {
                        errorCount++;
                    }
                    if (successCount + errorCount == len) {
                        layer.close(load);
                        layer.msg("删除完毕！成功：" + successCount + "个，失败：" + errorCount + "个", { icon: 6, anim: 0, time: 3000 });
                        me.itemContrastData = [];//重置
                        table.reload('itemContrastTable', {
                            url: me.url.selectUrl + "&where=" + me.itemContrastWhere,
                            where: {
                                time: new Date().getTime()
                            }
                        });
                    }
                });
            });
        });
    }
};
//保存
itemContrast.onSaveClick = function () {
    var me = this,
        saveUrl = me.url.addOrUpdateUrl,
        delUrl = me.url.delUrl,
        tableCache = table.cache["itemContrastTable"],
        msg = [],
        updateList = [],
        delList = [];

    if (tableCache.length == 0) {
        layer.msg("没有修改数据不需要保存!", { icon: 0, anim: 0 });
        return;
    }
    $.each(tableCache, function (a, itemA) {
        $.each(updateList, function (B, itemB) {
            if ((itemA["LBItemCodeLink_LinkDicDataCode"] != "" && itemA["LBItemCodeLink_LinkDicDataCode"] != null) && itemA["LBItemCodeLink_LinkDicDataCode"] == itemB["LBItemCodeLink_LinkDicDataCode"]) {
                msg.push("同项目对照编码不可重复!");
                return false;
            }
        });
        if (msg.length > 0) return false;
        if (itemA["Tab"] && (itemA["LBItemCodeLink_LinkDicDataCode"] != "" && itemA["LBItemCodeLink_LinkDicDataCode"] != null))
            updateList.push(itemA);
        else if (itemA["Tab"] && itemA["LBItemCodeLink_Id"] && (itemA["LBItemCodeLink_LinkDicDataCode"] == "" || itemA["LBItemCodeLink_LinkDicDataCode"] == null))
            delList.push(itemA);
    });
    if (msg.length > 0) {
        layer.msg(msg.join(), { icon: 0, anim: 0 });
        return;
    }
    if (updateList.length == 0 && delList.length == 0) {
        layer.msg("没有修改数据不需要保存!", { icon: 0, anim: 0 });
        return;
    }
    //发送服务
    var count = updateList.length + delList.length, delCount = delList.length, successCount = 0, errorCount = 0;
    //遮罩
    var loadIndex = layer.load();
    //新增+编辑
    $.each(updateList, function (c, itemC) {
        var entity = {
            //LabID: itemC.LabID,
            Id: itemC.LBItemCodeLink_Id,
            LinkSystemID: itemC.LBItemCodeLink_LinkSystemID,//对接系统ID
            LinkSystemCode: itemC.LBItemCodeLink_LinkSystemCode,//对接系统编码
            LinkSystemName: itemC.LBItemCodeLink_LinkSystemName,//对接系统名称
            DicDataID: itemC.LBItemCodeLink_DicDataID,
            DicDataCode: itemC.LBItemCodeLink_DicDataCode,
            DicDataName: itemC.LBItemCodeLink_DicDataName,
            LinkDicDataCode: itemC.LBItemCodeLink_LinkDicDataCode,
            LinkDicDataName: itemC.LBItemCodeLink_LinkDicDataName,
            TransTypeID: itemC.LBItemCodeLink_TransTypeID ? itemC.LBItemCodeLink_TransTypeID : 0,
            DispOrder: itemC.LBItemCodeLink_DispOrder,
            IsUse: 1
        };
        var configs = {
            type: "POST",
            url: saveUrl,
            data: JSON.stringify({ entity: entity })
        };
        uxutil.server.ajax(configs, function (data) {
            if (data.success) {
                successCount++;
            } else {
                errorCount++;
            }
            if (successCount + errorCount == count) {
                layer.close(loadIndex);
                table.reload('itemContrastTable', {
                    url: me.url.selectUrl + "&where=" + me.itemContrastWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
                layer.msg("保存完毕：" + count + "个，删除：" + delCount + "，失败：" + errorCount + "个", { icon: 0, anim:0, time: 3000 });
            }
        });
    });
    //删除
    $.each(delList, function (d, itemD) {
        var url = delUrl + '?id=' + itemD["LBItemCodeLink_Id"];
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data.success) {
                successCount++;
            } else {
                errorCount++;
            }
            if (successCount + errorCount == count) {
                layer.close(loadIndex);
                table.reload('itemContrastTable', {
                    url: me.url.selectUrl + "&where=" + me.itemContrastWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
                layer.msg("保存完毕：" + count + "个，删除：" + delCount + "，失败：" + errorCount + "个", { icon: 0, anim: 0, time: 3000 });
            }
        });
    });
};