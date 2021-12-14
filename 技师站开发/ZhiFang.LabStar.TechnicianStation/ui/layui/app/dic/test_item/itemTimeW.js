/**
 * 超时警告
 * @author zhangda
 * @version 2019-07-17
 */
var itemTimeW = new Object();
//项目信息
itemTimeW.itemData = {};
//超时警告type值
itemTimeW.itemTimeWType = 'show';
//超时警告选中数据
itemTimeW.itemTimeWData = [];
//服务路径地址
itemTimeW.url = {};
//设置服务路径
itemTimeW.setUrl = function () {
    itemTimeW.url = {
        getMaxNoByEntityFieldUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField',//获取指定实体字段的最大号
        getSampleTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true&sort=[{property:"LBSampleType_DispOrder",direction:"ASC"}]',
        getSickTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true&sort=[{property:"LBSickType_DispOrder",direction:"ASC"}]',
        getEnumTypeUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',//获得枚举 传递枚举类型名 
        //超时警告
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBItemTimeW',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemTimeWByHQL?isPlanish=true&sort=[{property:"LBItemTimeW_DispOrder",direction:"ASC"}]',
        delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBItemTimeW',
        updateUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemTimeWByField',
    };
}
//获得字典信息
itemTimeW.getServerData = function () {
    //获得样本类型
    uxutil.server.ajax({
        url: itemTimeW.url.getSampleTypeUrl + "&where=IsUse=1"
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                var html = "<option value=''>请选择</option>";
                if (data.length > 0) {
                    for (var i in data) {
                        html += '<option value="' + data[i].LBSampleType_Id + '">' + data[i].LBSampleType_CName + '</option>';
                    }
                    $("#itemTimeWSampleTypeID").html(html);
                    form.render('select');
                }
            }
        } else {
            layer.msg("样本类型字典查询失败！", { icon: 5, anim: 6 });
        }
    });
    //获得性别
    uxutil.server.ajax({
        url: itemTimeW.url.getSickTypeUrl + "&where=IsUse=1"
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                var html = "<option value=''>请选择</option>";
                if (data.length > 0) {
                    for (var i in data) {
                        html += '<option value="' + data[i].LBSickType_Id + '">' + data[i].LBSickType_CName + '</option>';
                    }
                    $("#itemTimeWSickTypeID").html(html);
                    form.render('select');
                }
            }
        } else {
            layer.msg("就诊类型字典查询失败！", { icon: 5, anim: 6 });
        }
    });
    //获得超时类型
    uxutil.server.ajax({
        url: itemTimeW.url.getEnumTypeUrl + '?classname=ItemOverTimeType&classnamespace=ZhiFang.Entity.LabStar'
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue);
                var html = "<option value=''>请选择</option>";
                for (var i in data) {
                    var color = data[i].BGColor;
                    if (color != "") {
                        html += '<option value="' + data[i].Id + '" style="color:' + color + '">' + data[i].Name + '</option>';
                    } else {
                        html += '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
                    }
                }
                $("#itemTimeWTimeWType").html(html);
                form.render('select');
            }
        } else {
            layer.msg("超时类型字典查询失败！", { icon: 5, anim: 6 });
        }
    });
}
//计算项目初始化--最先执行
itemTimeW.init = function () {
    itemTimeW.itemData = common.checkData[common.checkData.length - 1];
    itemTimeW.itemTimeWEmpty();
    itemTimeW.setUrl();
    itemTimeW.getServerData();
    itemTimeW.itemTimeWWhere = "ItemID=" + itemTimeW.itemData.LBItem_Id;
    itemTimeW.itemTimeWTableRender(itemTimeW.itemTimeWWhere);
}
//计算公式初始化
itemTimeW.itemTimeWTableRender = function (where) {
    table.render({
        elem: '#itemTimeWTable',
        height: 160,
        defaultToolbar: [],
        size: 'sm', //小尺寸的表格
        url: itemTimeW.url.selectUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBItemTimeW_Id',
                width: 60,
                title: '主键ID',
                hide: true
            }, {
                field: 'LBItemTimeW_ItemID',
                title: '项目Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemTimeW_SampleTypeID',
                title: '样本类型Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemTimeW_SickTypeID',
                title: '性别Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemTimeW_SectionID',
                title: '小组Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemTimeW_TimeWType',
                title: '超时类型',
                minWidth: 120
            }, {
                field: 'LBItemTimeW_TimeWValue',
                title: '超时时间',
                minWidth: 120,
                hide:true
            }, {
                field: 'LBItemTimeW_TimeWDesc',
                title: '超时时间',
                minWidth: 160
            }, {
                field: 'LBItemTimeW_IsUse',
                title: '是否使用',
                minWidth: 100,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBItemTimeW_IsUse.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBItemTimeW_DispOrder',
                title: '显示次序',
                minWidth: 100
            }
            ]
        ],
        page: false,
        limit: 1000,
        //limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            itemTimeW.itemTimeWData = [];//重置
            if (count > 0) {
                //默认选中第一行
                $("#itemTimeWTable+div .layui-table-body table.layui-table tbody tr:first").click();
            } else {
                itemTimeW.itemTimeWEmpty();
                itemTimeW.itemTimeWType = 'add';
                itemTimeW.showTypeSign();
                itemTimeW.SetDisabled(false);
                itemTimeW.isDelEnable(false);
                itemTimeW.isSaveEnable(true);
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
//获得显示次序
itemTimeW.getDispOrder = function (entityName, Id) {
    uxutil.server.ajax({
        url: itemTimeW.url.getMaxNoByEntityFieldUrl + '?entityName=' + entityName + '&entityField=DispOrder'
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                $("#" + Id).val(res.ResultDataValue);
            }
        } else {
            layer.msg("默认显示次序获取失败！", { icon: 5, anim: 6 });
        }
    });
}
//获得当前列表中最大的显示次序
itemTimeW.getDispOrderForThisTable = function (TableName, DispOrderName, ElemId) {
    var data = table.cache[TableName];
    var DispOrder = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i][DispOrderName] > DispOrder) {
            DispOrder = data[i][DispOrderName];
        }
    }
    $("#" + ElemId).val(Number(DispOrder) + 1);
}
//监听行单击事件
table.on('row(itemTimeWTable)', function (obj) {
    var data = obj.data;
    //标注选中样式
    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    //暂存选中数据
    if (itemTimeW.itemTimeWData.length > 0 && itemTimeW.itemTimeWData[0].LBItemTimeW_Id == data.LBItemTimeW_Id) return;
    itemTimeW.itemTimeWData = [];//重置
    itemTimeW.itemTimeWData.push(data);
    itemTimeW.itemTimeWType = "show";
    itemTimeW.itemTimeWReset();//赋值表单
});
//显示编辑新增标识
itemTimeW.showTypeSign = function () {
    var type = itemTimeW.itemTimeWType,
        text = type == 'add' ? "新增" : (type == 'edit' ? "编辑" : "查看");
    $("#itemTimeWType").html(text);
}
//超时警告赋值重置
itemTimeW.itemTimeWReset = function () {
    if (itemTimeW.itemTimeWType == 'edit' || itemTimeW.itemTimeWType == 'show') {
        itemTimeW.showTypeSign();
        if (itemTimeW.itemTimeWData.length > 0) {
            var data = itemTimeW.itemTimeWData[itemTimeW.itemTimeWData.length - 1];
            form.val("itemTimeWForm", data);
            if (data.LBItemTimeW_TimeWDesc != "") {
                var timeStr = data.LBItemTimeW_TimeWDesc;
                if (timeStr.indexOf("天") != -1) {
                    var day = timeStr.split("天")[0];
                    $("#itemTimeWDay").val(day);
                    timeStr = timeStr.split("天")[1];
                }
                if (timeStr.indexOf("小时") != -1) {
                    var hour = timeStr.split("小时")[0];
                    $("#itemTimeWHour").val(hour);
                    timeStr = timeStr.split("小时")[1];
                }
                if (timeStr.indexOf("分钟") != -1) {
                    var minute = timeStr.split("分钟")[0];
                    $("#itemTimeWMinute").val(minute);
                }
            } else {
                $("#itemTimeWDay").val("");
                $("#itemTimeWHour").val("");
                $("#itemTimeWMinute").val("");
            }
            //下拉框赋值
            for (var i in data) {
                $("#itemTimeWForm input[type=checkbox]").each(function () {
                    if (i == $(this).attr("name")) {
                        var flag = data[i] == "true" ? true : false;
                        if (flag) {
                            if (!$(this).next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                $(this).next('.layui-form-switch').addClass('layui-form-onswitch');
                                $(this).next('.layui-form-switch').children("em").html("是");
                                $(this)[0].checked = true;
                            }
                        } else {
                            if ($(this).next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                $(this).next('.layui-form-switch').removeClass('layui-form-onswitch');
                                $(this).next('.layui-form-switch').children("em").html("否");
                                $(this)[0].checked = false;
                            }
                        }
                    }
                });
            }
        }
        itemTimeW.SetDisabled(itemTimeW.itemTimeWType == 'show');
        itemTimeW.isDelEnable(true);
        itemTimeW.isSaveEnable(itemTimeW.itemTimeWType != 'show');
    }
}
//超时警告清空
itemTimeW.itemTimeWEmpty = function () {
    $("#itemTimeWForm")[0].reset();
}

//禁用处理
itemTimeW.SetDisabled = function (isDisabled) {
    $("#itemTimeWForm :input").each(function (i, item) {
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
//删除按钮是否禁用 del
itemTimeW.isDelEnable = function (bo) {
    if (bo)
        $("#delItemTimeW").removeClass("layui-btn-disabled").removeAttr('disabled', true);
    else
        $("#delItemTimeW").addClass("layui-btn-disabled").attr('disabled', true);
};
//保存按钮是否禁用 save
itemTimeW.isSaveEnable = function (bo) {
    if (bo)
        $("#saveItemTimeW").removeClass("layui-btn-disabled").removeAttr('disabled', true);
    else
        $("#saveItemTimeW").addClass("layui-btn-disabled").attr('disabled', true);
};
//重置超时警告
itemTimeW.resetitemTimeW = function () {
    if (itemTimeW.itemTimeWData.length == 0) {
        itemTimeW.itemTimeWEmpty();
        return;
    }
    if (itemTimeW.itemTimeWType == 'add') 
        itemTimeW.itemTimeWEmpty();
    else 
        itemTimeW.itemTimeWReset();
    itemTimeW.showTypeSign();
}
//点击超时警告新增按钮
$("#addItemTimeW").click(function () {
    itemTimeW.itemTimeWType = 'add';
    if (itemTimeW.itemTimeWData.length > 0) {
        itemTimeW.itemTimeWEmpty();
    }
    itemTimeW.getDispOrderForThisTable("itemTimeWTable", "LBItemTimeW_DispOrder", "itemTimeWDispOrder");
    itemTimeW.SetDisabled(false);
    itemTimeW.isDelEnable(false);
    itemTimeW.isSaveEnable(true);
});
//点击超时警告编辑按钮
$("#editItemTimeW").click(function () {
    if (itemTimeW.itemTimeWData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        itemTimeW.itemTimeWType = 'edit';
        itemTimeW.itemTimeWReset();
    }
});
//点击超时警告删除按钮
$("#delItemTimeW").click(function () {
    if (itemTimeW.itemTimeWData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        if (itemTimeW.itemTimeWType != 'edit') return;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var len = itemTimeW.itemTimeWData.length;
            for (var i = 0; i < itemTimeW.itemTimeWData.length; i++) {
                var Id = itemTimeW.itemTimeWData[i].LBItemTimeW_Id;
                uxutil.server.ajax({
                    url: itemTimeW.url.delUrl + "?Id=" + Id
                }, function (res) {
                    if (res.success) {
                        len--;
                        if (len == 0) {
                            layer.close(index);
                            layer.msg("删除成功！", { icon: 6, anim: 0 });
                            itemTimeW.itemTimeWData = [];//重置
                            table.reload('itemTimeWTable', {
                                url: itemTimeW.url.selectUrl + "&where=" + itemTimeW.itemTimeWWhere,
                                where: {
                                    time: new Date().getTime()
                                }
                            });
                        }
                    } else {
                        layer.msg("删除失败！", { icon: 5, anim: 6 });
                    }
                });
            }
        });
    }
});
//点击超时警告重置按钮
$("#resetItemTimeW").click(function () {
    itemTimeW.resetitemTimeW();
});
//超时警告保存
form.on('submit(saveItemTimeW)', function (data) {
    window.event.preventDefault();
    var fields = "";//发送修改的字段
    var postData = {};//发送的数据
    if (itemTimeW.itemTimeWType == 'show') return;
    //开关
    if (!$("#itemTimeWIsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
        data.field.LBItemTimeW_IsUse = "false";
    } else {
        data.field.LBItemTimeW_IsUse = "true";
    }
    for (var i in data.field) {
        postData[i.split("_")[1]] = typeof data.field[i] == "string" ? data.field[i].trim() : data.field[i];
        fields += i.split("_")[1] + ",";
    }
    postData.SampleTypeID = postData.SampleTypeID != "" ? postData.SampleTypeID : 0;
    postData.SectionID = postData.SectionID != "" ? postData.SectionID : 0;
    postData.SickTypeID = postData.SickTypeID != "" ? postData.SickTypeID : 0;
    var day = $("#itemTimeWDay").val();//天
    var hour = $("#itemTimeWHour").val();//小时
    var minute = $("#itemTimeWMinute").val();//分钟
    if (day != "") {
        if (isNaN(day) || parseInt(day, 10) != Number(day)) {
            layer.msg("请正确输入天数！", { icon: 5, anim: 6 });
        }
    } else {
        day = 0;
    }
    if (hour != "") {
        if (isNaN(hour) || parseInt(hour, 10) != Number(hour)) {
            layer.msg("请正确输入小时！", { icon: 5, anim: 6 });
        }
    } else {
        hour = 0;
    }
    if (minute != "") {
        if (isNaN(minute) || parseInt(minute, 10) != Number(minute)) {
            layer.msg("请正确输入分钟！", { icon: 5, anim: 6 });
        }
    } else {
        minute = 0;
    }
    postData.TimeWValue = day * 24 * 60 + hour * 60 + parseInt(minute);
    var TimeWDesc = "";
    if (day != 0) {
        TimeWDesc += day + "天";
    }
    if (hour != 0) {
        TimeWDesc += hour + "小时";
    }
    if (minute != 0) {
        TimeWDesc += minute + "分钟";
    }
    postData.TimeWDesc = TimeWDesc;
    //postData.UserID = "";//操作人--现在没有 有了加上
    postData.ItemID = itemTimeW.itemData.LBItem_Id;
    fields += "TimeWValue,TimeWDesc,ItemID";
    common.load.loadShow();
    if (itemTimeW.itemTimeWType == "edit") {
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData, fields: fields }),
            url: itemTimeW.url.updateUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("编辑成功！", { icon: 6, anim: 0 });
                itemTimeW.itemTimeWData = [];//重置
                table.reload('itemTimeWTable', {
                    url: itemTimeW.url.selectUrl + "&where=" + itemTimeW.itemTimeWWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
            } else {
                layer.msg("编辑失败！", { icon: 5, anim: 6 });
            }
        });
    } else if (itemTimeW.itemTimeWType == "add") {
        delete postData.Id;
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData }),
            url: itemTimeW.url.addUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("新增成功！", { icon: 6, anim: 0 });
                itemTimeW.itemTimeWData = [];//重置
                table.reload('itemTimeWTable', {
                    url: itemTimeW.url.selectUrl + "&where=" + itemTimeW.itemTimeWWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
            } else {
                layer.msg("新增失败！", { icon: 5, anim: 6 });
            }
        });
    }
})