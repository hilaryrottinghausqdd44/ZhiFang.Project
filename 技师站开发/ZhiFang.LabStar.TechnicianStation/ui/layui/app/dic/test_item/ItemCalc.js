/**
 * 计算项目
 * @author zhangda
 * @version 2019-07-11
 */
var itemCalc = new Object();
//项目信息
itemCalc.itemData = {};
//计算项目type值
itemCalc.itemCalcType = 'show';
//计算项目选中数据
itemCalc.itemCalcData = [];
//服务路径地址
itemCalc.url = {};
//设置服务路径
itemCalc.setUrl = function () {
    itemCalc.url = {
        getMaxNoByEntityFieldUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField',//获取指定实体字段的最大号
        getSampleTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true&sort=[{property:"LBSampleType_DispOrder",direction:"ASC"}]',
        //getGenderUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBGenderByHQL?isPlanish=true&sort=[{property:"LBGender_DispOrder",direction:"ASC"}]',
        //getAgeUnitUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBAgeUnitByHQL?isPlanish=true&sort=[{property:"LBAgeUnit_DispOrder",direction:"ASC"}]',
        getEnumTypeUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',//获得枚举 传递枚举类型名 
        //计算项目
        selectItemCalcUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]&fields=LBItemCalc_Id,LBItemCalc_LBCalcItem_Id,LBItemCalc_LBItem_Id,LBItemCalc_LBItem_CName,LBItemCalc_LBItem_EName,LBItemCalc_LBItem_SName,LBItemCalc_LBItem_Shortcode,LBItemCalc_LBItem_PinYinZiTou,LBItemCalc_LBItem_DispOrder',
        //计算公式
        addItemCalcFormulaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBItemCalcFormula',
        selectItemCalcFormulaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcFormulaByHQL?isPlanish=true&sort=[{property:"LBItemCalcFormula_DispOrder",direction:"ASC"}]',
        delItemCalcFormulaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBItemCalcFormula',
        updateItemCalcFormulaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemCalcFormulaByField',
    };
}
//获得字典信息
itemCalc.getServerData = function () {
    //获得样本类型
    uxutil.server.ajax({
        url: itemCalc.url.getSampleTypeUrl + "&where=IsUse=1"
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                var html = "<option value=''>请选择</option>";
                if (data.length > 0) {
                    for (var i in data) {
                        html += '<option value="' + data[i].LBSampleType_Id + '">' + data[i].LBSampleType_CName + '</option>';
                    }
                    $("#itemCalcFormulaSampleTypeID").html(html);
                    form.render('select');
                }
            }
        } else {
            layer.msg("样本类型字典查询失败！", { icon: 5, anim: 6 });
        }
    });
    //获得性别
    uxutil.server.ajax({
        url: itemCalc.url.getEnumTypeUrl + '?classname=GenderType&classnamespace=ZhiFang.Entity.LabStar'
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
                $("#itemCalcFormulaGenderID").html(html);
                form.render('select');

            }
        } else {
            layer.msg("性别枚举查询失败！", { icon: 5, anim: 6 });
        }
    });
    //获得年龄单位
    uxutil.server.ajax({
        url: itemCalc.url.getEnumTypeUrl + '?classname=AgeUnitType&classnamespace=ZhiFang.Entity.LabStar'
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
                $("#itemCalcFormulaAgeUnitID").html(html);
                form.render('select');
            }
        } else {
            layer.msg("年龄单位字典查询失败！", { icon: 5, anim: 6 });
        }
    });
}
//计算项目初始化--最先执行
itemCalc.init = function () {
    itemCalc.itemData = common.checkData[common.checkData.length - 1];
    if (itemCalc.itemData.LBItem_IsCalcItem.toString() != "true") {
        return;
    }
    //清空表单
    itemCalc.itemCalcEmpty();

    $("#itemCalcFormulaCalcType option:first-child").attr("select", "select");//默认值
    itemCalc.setUrl();
    itemCalc.getServerData();
    itemCalc.itemCalcFormulaWhere = "ItemID=" + itemCalc.itemData.LBItem_Id;
    itemCalc.itemCalcFormulaTableRender(itemCalc.itemCalcFormulaWhere);
    itemCalc.itemCalcWhere = "CalcItemID=" + itemCalc.itemData.LBItem_Id;
    itemCalc.itemCalcTableRender(itemCalc.itemCalcWhere);
}
//计算公式初始化
itemCalc.itemCalcFormulaTableRender = function (where) {
    table.render({
        elem: '#itemCalcFormulaTable',
        height: 100,
        defaultToolbar: [],
        size: 'sm', //小尺寸的表格
        url: itemCalc.url.selectItemCalcFormulaUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBItemCalcFormula_Id',
                width: 60,
                title: '主键ID',
                hide: true
            }, {
                field: 'LBItemCalcFormula_ItemID',
                title: '项目Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemCalcFormula_SampleTypeID',
                title: '样本类型Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemCalcFormula_GenderID',
                title: '性别Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemCalcFormula_SectionID',
                title: '小组Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemCalcFormula_IsDefault',
                title: '默认',
                minWidth: 100,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBItemCalcFormula_IsDefault.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBItemCalcFormula_CalcFormula',
                title: '计算公式',
                minWidth: 200,
                hide:true
            }, {
                field: 'LBItemCalcFormula_CalcFormulaInfo',
                title: '计算公式描述',
                minWidth: 200
            }, {
                field: 'LBItemCalcFormula_FormulaCondition',
                title: '计算条件',
                minWidth: 120,
                hide:true
            }, {
                field: 'LBItemCalcFormula_FormulaConditionInfo',
                title: '计算条件描述',
                minWidth: 200
            }, {
                field: 'LBItemCalcFormula_HAge',
                title: '年龄范围上限',
                width: 120
            }, {
                field: 'LBItemCalcFormula_LAge',
                title: '年龄范围下限',
                width: 120
            }, {
                field: 'LBItemCalcFormula_HValueComp',
                title: '范围高限对比类型',
                width: 160
            }, {
                field: 'LBItemCalcFormula_AgeUnitID',
                title: '年龄单位',
                width: 100,
                hide:true
            }, {
                field: 'LBItemCalcFormula_UWeight',
                title: '体重上限',
                width: 80
            }, {
                field: 'LBItemCalcFormula_LWeight',
                title: '体重下限',
                width: 80
            }, {
                field: 'LBItemCalcFormula_IsUse',
                title: '是否使用',
                width: 100,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBItemCalcFormula_IsUse.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBItemCalcFormula_CalcType',
                title: '计算类型',
                width: 140,
                templet: function (data) {
                    var str = "<span>数值计算</span> ";
                    if (data.LBItemCalcFormula_CalcType == 1) {
                        str = "<span  style='color:red'>仅替换结果得到报告值</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBItemCalcFormula_IsKeepInvalid',
                title: '无效结果保留',
                width: 100,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBItemCalcFormula_IsKeepInvalid.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBItemCalcFormula_DispOrder',
                title: '显示次序',
                width: 100
            }
            ]
        ],
        page: false,
        limit: 1000,
        //limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            itemCalc.itemCalcData = [];
            if (count > 0) {
                //默认选中第一行
                $("#itemCalcFormulaTable+div .layui-table-body table.layui-table tbody tr:first").click();
            } else {
                itemCalc.itemCalcEmpty();
                itemCalc.itemCalcType = 'add';
                itemCalc.showTypeSign();
                itemCalc.SetDisabled(false);
                itemCalc.isDelEnable(false);
                itemCalc.isSaveEnable(true);
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
itemCalc.getDispOrder = function (entityName, Id) {
    uxutil.server.ajax({
        url: itemCalc.url.getMaxNoByEntityFieldUrl + '?entityName=' + entityName + '&entityField=DispOrder'
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
itemCalc.getDispOrderForThisTable = function (TableName, DispOrderName, ElemId) {
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
table.on('row(itemCalcFormulaTable)', function (obj) {
    var data = obj.data;
    //标注选中样式
    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    //暂存选中数据
    if (itemCalc.itemCalcData.length > 0 && itemCalc.itemCalcData[0].LBItemCalcFormula_Id == data.LBItemCalcFormula_Id) return;
    itemCalc.itemCalcData = [];//重置
    itemCalc.itemCalcData.push(data);
    itemCalc.itemCalcType = "show";
    itemCalc.itemCalcReset();//赋值表单
});
//显示编辑新增标识
itemCalc.showTypeSign = function() {
    var type = itemCalc.itemCalcType,
        text = type == 'add' ? "新增" : (type == 'edit' ? "编辑" : "查看");
    $("#itemCalcType").html(text);
}
//计算公式赋值重置
itemCalc.itemCalcReset = function () {
    if (itemCalc.itemCalcType == 'edit' || itemCalc.itemCalcType == 'show') {
        itemCalc.showTypeSign();
        if (itemCalc.itemCalcData.length > 0) {
            var data = itemCalc.itemCalcData[itemCalc.itemCalcData.length - 1];
            //年龄 体重
            data.LBItemCalcFormula_LAge = data.LBItemCalcFormula_LAge != 0 ? data.LBItemCalcFormula_LAge : "";
            data.LBItemCalcFormula_HAge = data.LBItemCalcFormula_HAge != 0 ? data.LBItemCalcFormula_HAge : "";
            data.LBItemCalcFormula_LWeight = data.LBItemCalcFormula_LWeight != 0 ? data.LBItemCalcFormula_LWeight : "";
            data.LBItemCalcFormula_UWeight = data.LBItemCalcFormula_UWeight != 0 ? data.LBItemCalcFormula_UWeight : "";
            form.val("itemCalcFormulaForm", data);
            //下拉框赋值
            for (var i in data) {
                $("#itemCalcFormulaForm input[type=checkbox]").each(function () {
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
        itemCalc.SetDisabled(itemCalc.itemCalcType == 'show');
        itemCalc.isDelEnable(true);
        itemCalc.isSaveEnable(itemCalc.itemCalcType != 'show');
    }
}
//禁用处理
itemCalc.SetDisabled = function (isDisabled) {
    $("#itemCalcFormulaForm :input").each(function (i, item) {
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
itemCalc.isDelEnable = function (bo) {
    if (bo)
        $("#delItemCalcFormula").removeClass("layui-btn-disabled").removeAttr('disabled', true);
    else
        $("#delItemCalcFormula").addClass("layui-btn-disabled").attr('disabled', true);
};
//保存按钮是否禁用 save
itemCalc.isSaveEnable = function (bo) {
    if (bo)
        $("#saveItemCalcFormula").removeClass("layui-btn-disabled").removeAttr('disabled', true);
    else
        $("#saveItemCalcFormula").addClass("layui-btn-disabled").attr('disabled', true);
};
//计算公式清空
itemCalc.itemCalcEmpty = function () {
    $("#itemCalcFormulaForm")[0].reset();
    $("#itemCalcFormulaForm select[xm-select]").each(function () {
        if ($(this).attr("id") == "itemCalcFormulaCalcType") {
            $(this).children("option").eq(0).attr("select", "select");
            return true;//跳过
        }
    });
}
//重置计算公式
itemCalc.resetItemCalc = function () {
    if (itemCalc.itemCalcData.length == 0) {
        itemCalc.itemCalcEmpty();
        return;
    }
    if (itemCalc.itemCalcType == 'edit') {
        itemCalc.itemCalcReset();
        itemCalc.showTypeSign();
    } else if (itemCalc.itemCalcType == 'add') {
        itemCalc.itemCalcEmpty();
        itemCalc.showTypeSign();
    }
}
//点击计算公式新增按钮
$("#addItemCalcFormula").click(function () {
    itemCalc.itemCalcType = 'add';
    if (itemCalc.itemCalcData.length > 0) {
        itemCalc.itemCalcEmpty();
    }
    itemCalc.getDispOrderForThisTable("itemCalcFormulaTable", "LBItemCalcFormula_DispOrder", "itemCalcFormulaDispOrder");
    itemCalc.SetDisabled(false);
    itemCalc.isDelEnable(false);
    itemCalc.isSaveEnable(true);
});
//点击计算公式编辑按钮
$("#editItemCalcFormula").click(function () {
    if (itemCalc.itemCalcData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        itemCalc.itemCalcType = 'edit';
        itemCalc.itemCalcReset();
    }
});
//点击计算公式删除按钮
$("#delItemCalcFormula").click(function () {
    if (itemCalc.itemCalcData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        if (itemCalc.itemCalcType == 'show') return;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var len = itemCalc.itemCalcData.length;
            for (var i = 0; i < itemCalc.itemCalcData.length; i++) {
                var Id = itemCalc.itemCalcData[i].LBItemCalcFormula_Id;
                uxutil.server.ajax({
                    url: itemCalc.url.delItemCalcFormulaUrl + "?Id=" + Id
                }, function (res) {
                    if (res.success) {
                        len--;
                        if (len == 0) {
                            layer.close(index);
                            layer.msg("删除成功！", { icon: 6, anim: 0 });
                            itemCalc.itemCalcData = [];//重置
                            table.reload('itemCalcFormulaTable', {
                                url: itemCalc.url.selectItemCalcFormulaUrl + "&where=" + itemCalc.itemCalcFormulaWhere,
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
//点击计算公式重置按钮
$("#resetItemCalcFormula").click(function () {
    itemCalc.resetItemCalc();
});
//计算公式保存
form.on('submit(saveItemCalcFormula)', function (data) {
    window.event.preventDefault();
    var fields = "";//发送修改的字段
    var postData = {};//发送的数据
    if (itemCalc.itemCalcType == 'show') return false;
    //开关
    if (!$("#itemCalcFormulaIsDefault").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
        data.field.LBItemCalcFormula_IsDefault = "false";
    } else {
        data.field.LBItemCalcFormula_IsDefault = "true";
    }
    if (!$("#itemCalcFormulaIsKeepInvalid").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
        data.field.LBItemCalcFormula_IsKeepInvalid = "false";
    } else {
        data.field.LBItemCalcFormula_IsKeepInvalid = "true";
    }
    if (!$("#itemCalcFormulaIsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
        data.field.LBItemCalcFormula_IsUse = "false";
    } else {
        data.field.LBItemCalcFormula_IsUse = "true";
    }

    for (var i in data.field) {
        postData[i.split("_")[1]] = typeof data.field[i] == "string" ? data.field[i].trim() : data.field[i];
        fields += i.split("_")[1] + ","; 
    }
    postData.SampleTypeID = postData.SampleTypeID != "" ? postData.SampleTypeID : 0;
    postData.SectionID = postData.SectionID != "" ? postData.SectionID : 0;
    postData.GenderID = postData.GenderID != "" ? postData.GenderID : 0;
    postData.AgeUnitID = postData.AgeUnitID != "" ? postData.AgeUnitID : 0;

    if (postData.LAge != "" && isNaN(postData.LAge)) {//年龄下限
        layer.msg("请正确输入年龄值!", { icon: 5, anim: 6 });
        return;
    }
    postData.LAge = postData.LAge != "" ? postData.LAge : 0;
    if (postData.HAge != "" && isNaN(postData.HAge)) {//年龄上限
        layer.msg("请正确输入年龄值!", { icon: 5, anim: 6 });
        return;
    }
    postData.HAge = postData.HAge != "" ? postData.HAge : 0;
    if (postData.LWeight != "" && isNaN(postData.LWeight)) {//体重下限
        layer.msg("请正确输入体重数值!", { icon: 5, anim: 6 });
        return;
    }
    postData.LWeight = postData.LWeight != "" ? postData.LWeight : 0;
    if (postData.UWeight != "" && isNaN(postData.UWeight)) {//体重上限
        layer.msg("请正确输入体重数值!", { icon: 5, anim: 6 });
        return;
    }
    postData.UWeight = postData.UWeight != "" ? postData.UWeight : 0;
    
    fields = fields.slice(0, fields.length - 1);
    //计算公式 计算条件写入
    postData.CalcFormula = itemCalc.getStrByDesc(postData.CalcFormulaInfo);
    postData.FormulaCondition = itemCalc.getStrByDesc(postData.FormulaConditionInfo);
    common.load.loadShow();
    if (itemCalc.itemCalcType == "edit") {
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData, fields: fields }),
            url: itemCalc.url.updateItemCalcFormulaUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("编辑成功！", { icon: 6, anim: 0 });
                itemCalc.itemCalcData = [];//重置
                table.reload('itemCalcFormulaTable', {
                    url: itemCalc.url.selectItemCalcFormulaUrl + "&where=" + itemCalc.itemCalcFormulaWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
            } else {
                layer.msg("编辑失败！", { icon: 5, anim: 6 });
            }
        });
    } else if (itemCalc.itemCalcType == "add") {
        delete postData.Id;
        var DataTimeStamp = [0, 0, 0, 0, 0, 0, 41, 186];
        postData.LBItem = { Id: itemCalc.itemData.LBItem_Id, DataTimeStamp: DataTimeStamp };
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData }),
            url: itemCalc.url.addItemCalcFormulaUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("新增成功！", { icon: 6, anim: 0 });
                itemCalc.itemCalcData = [];//重置
                table.reload('itemCalcFormulaTable', {
                    url: itemCalc.url.selectItemCalcFormulaUrl + "&where=" + itemCalc.itemCalcFormulaWhere,
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
//根据计算公式描述/计算条件描述 写入 计算公式/计算条件
itemCalc.getStrByDesc = function (desc) {//描述，要查询的数据
    var data = table.cache.itemCalcTable;
    var desc = desc;
    if (desc == "") return desc; 
    if (data.length == 0) return desc;
    var patt = /\[[^\]]+\]/g;
    var arr = desc.match(patt);//获得所有的简称
    for (var i = 0; i < arr.length; i++) {
        var SName = arr[i].substring(1,arr[i].length-1);
        for (var j = 0; j < data.length; j++) {
            if (SName == data[j].LBItemCalc_LBItem_SName) {
                desc = desc.replace(SName, data[j].LBItemCalc_LBItem_Id);
                break;
            }
        }
    }
    return desc;
}
/***计算内项目***/
//计算公式初始化
itemCalc.itemCalcTableRender = function (where) {
    table.render({
        elem: '#itemCalcTable',
        height: 150,
        defaultToolbar: ["filter"],
        toolbar: '#toolbarItemCalc',
        size: 'sm', //小尺寸的表格
        url: itemCalc.url.selectItemCalcUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBItemCalc_Id',
                width: 60,
                title: '主键ID',
                sort: true,
                hide: true
            }, {
                field: 'LBItemCalc_LBCalcItem_Id',
                title: '计算项目Id',
                width: 130,
                hide: true,
                sort: true
            }, {
                field: 'LBItemCalc_LBItem_Id',
                title: '项目Id',
                width: 130,
                hide: true,
                sort: true
            }, {
                field: 'LBItemCalc_LBItem_CName',
                title: '名称',
                minWidth: 100,
                sort: true
            }, {
                field: 'LBItemCalc_LBItem_SName',
                title: '简称',
                width: 100,
                sort: true
            }, {
                field: 'LBItemCalc_LBItem_Shortcode',
                title: '快捷码',
                width: 100,
                sort: true
            }, {
                field: 'LBItemCalc_LBItem_EName',
                title: '英文名',
                width: 100,
                sort: true
            }, {
                field: 'LBItemCalc_LBItem_PinYinZiTou',
                title: '拼音字头',
                width: 100,
                hide: true,
                sort: true
            }, {
                field: 'LBItemCalc_LBItem_DispOrder',
                title: '显示次序',
                width: 80,
                hide: true,
                sort: true
            },
            {
                title: '操作',
                width: 140,
                align: 'center',
                toolbar: '#itemCalcTableOperation'
            }
            ]
        ],
        page: false,
        limit: 100,
        limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            if (count > 0) {
                //默认选中第一行
                $("#itemCalcTable+div .layui-table-body table.layui-table tbody tr:first").click();
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
//计算内项目上面的工具栏
table.on('toolbar(itemCalcTable)', function (obj) {
    var data = itemCalc.itemData;
    switch (obj.event) {
        case 'add':
            var flag = false;
            parent.layer.open({
                type: 2,
                title: ['新增参与计算检验项'],
                //skin: 'layui-layer-molv',
                area: ['1200px', '600px'],
                content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/add_itemCalc/addItemCalc.html?CalcItemID=' + data.LBItem_Id + '&CName=' + data.LBItem_CName + '&GroupType=' + data.LBItem_GroupType,
                cancel: function (index, layero) {
                    flag = true;
                },
                end: function () {
                    if (!flag) {
                        layer.msg("保存成功!", { icon: 6, anim: 0 });
                        itemCalc.itemCalcTableRender(itemCalc.itemCalcWhere);
                    }
                }
            });
            break;
    };
});
//监听行双击事件
table.on('rowDouble(itemCalcTable)', function (obj) {
    if (itemCalc.itemCalcType == 'show') {
        layer.msg('查看状态不支持写入计算公式', { icon: 0, anim: 0, time:2000 });
        return;
    }
    var data = obj.data;
    var SName = data.LBItemCalc_LBItem_SName != "" ? data.LBItemCalc_LBItem_SName : data.LBItemCalc_LBItem_Id;
    var oldCalcFormulaInfo = $("#CalcFormulaInfo").val();
    //var oldCalcFormula = $("#CalcFormula").val();
    $("#CalcFormulaInfo").val(oldCalcFormulaInfo+"[" + SName + "]");
    //$("#CalcFormula").val(oldCalcFormula+"[" + data.LBItemCalc_LBItem_Id + "]");
    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
});
//监听行工具事件
table.on('tool(itemCalcTable)', function (obj) {
    var data = obj.data; //获得当前行数据
    var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
    if (layEvent == 'write') {//写入计算公式条件
        var oldFormulaConditionInfo = $("#FormulaConditionInfo").val();
        //var oldFormulaCondition = $("#FormulaCondition").val();
        var SName = data.LBItemCalc_LBItem_SName != "" ? data.LBItemCalc_LBItem_SName : data.LBItemCalc_LBItem_Id;
        $("#FormulaConditionInfo").val(oldFormulaConditionInfo+"[" + SName + "]");
        //$("#FormulaCondition").val(oldFormulaCondition+"[" + data.LBItemCalc_LBItem_Id + "]");
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    }
});