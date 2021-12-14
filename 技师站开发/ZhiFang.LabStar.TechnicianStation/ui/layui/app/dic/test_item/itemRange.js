/**
 * 检验项目参考范围
 * @author zhangda
 * @version 2019-07-08
 */
var itemRange = new Object();
//项目信息
itemRange.itemData = {};
//项目参考值选中数据
itemRange.itemRangeData = [];
//项目参考值描述结果判断选中数据
itemRange.itemRangeExpResultData = [];
//是否获取过字典
itemRange.isGetDict = false;
//对应字典类型
itemRange.dictType = 'ResultStatus';
//描述结果判断当前页签
itemRange.currTabIndex = 0;
//是否加载过默认描述结果判断当前页签
itemRange.isLoadDefultResultTab = false;
//项目参考值type值
itemRange.itemRangeType = 'show';
//保存总数
itemRange.saveCount = 0;
//服务路径地址
itemRange.url = {};
//设置服务路径
itemRange.setUrl = function () {
    itemRange.url = {
        getMaxNoByEntityFieldUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField',//获取指定实体字段的最大号
        //参考值
        addItemRangeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBItemRange',
        delItemRangeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBItemRange',
        selectItemRangeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeByHQL?isPlanish=true&sort=[{property:"LBItemRange_DispOrder",direction:"ASC"}]',
        updateItemRangeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemRangeByField',
        getSampleTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true&sort=[{property:"LBSampleType_DispOrder",direction:"ASC"}]',
        getDeptUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true',//科室 -- 平台
        getDiagUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDiagByHQL?isPlanish=true',//诊断
        getPhyPeriodUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhyPeriodByHQL?isPlanish=true&sort=[{property:"LBPhyPeriod_DispOrder",direction:"ASC"}]',//生理期
        getCollectPartUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBCollectPartByHQL?isPlanish=true&sort=[{property:"LBCollectPart_DispOrder",direction:"ASC"}]',//采样部位
        getDictUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDictByHQL?isPlanish=true&sort=[{property:"LBDict_DispOrder",direction:"ASC"}]&fields=LBDict_Id,LBDict_CName,LBDict_DictCode,LBDict_EName,LBDict_SName,LBDict_IsUse,LBDict_DispOrder',
        getEnumTypeUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',//获得枚举 传递枚举类型名 
        //参考值扩展
        addItemRangeExpUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBItemRangeExp',
        delItemRangeExpUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBItemRangeExp',
        selectItemRangeExpUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeExpByHQL?isPlanish=true&sort=[{property:"LBItemRangeExp_DispOrder",direction:"ASC"}]',
        updateItemRangeExpUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemRangeExpByField'
    };
}
//参考范围初始化--最先执行
itemRange.init = function () {
    itemRange.itemData = common.checkData[common.checkData.length - 1];
    if (itemRange.itemData.LBItem_GroupType != 0) {//单项加载
        return;
    }
    //清空表单
    itemRange.itemRangeEmpty();
    itemRange.itemRangeExpResultEmpty();

    itemRange.setUrl();

    itemRange.itemRangeData = [];
    itemRange.itemRangeExpResultData = [];

    laydate.render({//没有默认值
        elem: '#BCollectTime',
        type: 'time'
    });
    laydate.render({//没有默认值
        elem: '#ECollectTime',
        type: 'time'
    });
    //颜色选择器初始化
    itemRange.colorpickerFun("AlarmColor1");
    itemRange.colorpickerFun("AlarmColor2");
    itemRange.colorpickerFun("AlarmColor3");
    itemRange.colorpickerFun("AlarmColor4");
    itemRange.colorpickerFun("AlarmColor5");
    itemRange.colorpickerFun("AlarmColor6");
    //是否获取过字典
    if (!itemRange.isGetDict) {
        itemRange.getServerData();
    } else {
        //渲染之后加载
        if (itemRange.currTabIndex == 0) {
            itemRange.itemRangeResultWhere = "ItemID=" + itemRange.itemData.LBItem_Id + " and JudgeType=1";
            itemRange.itemRangeResultTableRender(itemRange.itemRangeResultWhere);
        } else if (itemRange.currTabIndex == 1 && !itemRange.isLoadDefultResultTab) {
            itemRange.itemRangeResultWhere = "ItemID is null and JudgeType=1";
            itemRange.itemRangeResultTableRender(itemRange.itemRangeResultWhere);
        }
        itemRange.itemRangeAfterWhere = "ItemID=" + itemRange.itemData.LBItem_Id + " and JudgeType=0";
        itemRange.itemRangeAfterEmpty();
        itemRange.itemRangeAfterInit(itemRange.itemRangeAfterWhere);
    }
    itemRange.itemRangeWhere = "ItemID=" + itemRange.itemData.LBItem_Id;
    itemRange.itemRangeTableRender(itemRange.itemRangeWhere);
}
//获得字典信息
itemRange.getServerData = function () {
    //获得样本类型
    uxutil.server.ajax({
        url: itemRange.url.getSampleTypeUrl + "&where=IsUse=1"
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                var html = "<option value=''>请选择</option>";
                if (data.length > 0) {
                    for (var i in data) {
                        html += '<option value="' + data[i].LBSampleType_Id + '">' + data[i].LBSampleType_CName + '</option>';
                    }
                    $("#itemRangeSampleTypeID").html(html);
                    form.render('select');
                }
            }
        } else {
            layer.msg("样本类型字典查询失败！", { icon: 5, anim: 6 });
        }
    });
    //送检科室
    uxutil.server.ajax({
        url: itemRange.url.getDeptUrl + '&sort=[{"property":"HRDeptIdentity_DispOrder","direction":"ASC"}]' +
            '&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_UseCode' +
            "&where=hrdeptidentity.IsUse=1 and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.TSysCode='1001101'"
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list,
                    html = ['<option value="">请选择</option>'];
                $.each(data, function (i, item) {
                    html.push('<option value="' + item["HRDeptIdentity_HRDept_Id"] + '">' + item["HRDeptIdentity_HRDept_CName"] + '</option>');
                });
                $("#itemRangeDeptID").html(html.join(''));
                form.render('select');
            }
        }
    });
    //临床诊断
    uxutil.server.ajax({
        url: itemRange.url.getDiagUrl + '&sort=[{"property":"LBDiag_DispOrder","direction":"ASC"}]' +
            '&fields=LBDiag_Id,LBDiag_CName,LBDiag_UseCode' +
            '&where=lbdiag.IsUse=1'
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list,
                    html = ['<option value="">请选择</option>'];
                $.each(data, function (i, item) {
                    html.push('<option value="' + item["LBDiag_Id"] + '">' + item["LBDiag_CName"] + '</option>');
                });
                $("#itemRangeDiagID").html(html.join(''));
                form.render('select');
            }
        }
    });
    //采样部位
    uxutil.server.ajax({
        url: itemRange.url.getCollectPartUrl + "&where=IsUse=1" +
            '&fields=LBCollectPart_Id,LBCollectPart_CName,LBCollectPart_UseCode'
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list,
                    html = ['<option value="">请选择</option>'];
                $.each(data, function (i, item) {
                    html.push('<option value="' + item["LBCollectPart_Id"] + '">' + item["LBCollectPart_CName"] + '</option>');
                });
                $("#itemRangeCollectPartID").html(html.join(''));
                form.render('select');
            }
        }
    });
    //生理期
    uxutil.server.ajax({
        url: itemRange.url.getPhyPeriodUrl + "&where=IsUse=1" +
            '&fields=LBPhyPeriod_Id,LBPhyPeriod_CName,LBPhyPeriod_UseCode'
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list,
                    html = ['<option value="">请选择</option>'];
                $.each(data, function (i, item) {
                    html.push('<option value="' + item["LBPhyPeriod_Id"] + '">' + item["LBPhyPeriod_CName"] + '</option>');
                });
                $("#itemRangePhyPeriodID").html(html.join(''));
                form.render('select');
            }
        }
    });
    //获得性别
    uxutil.server.ajax({
        url: itemRange.url.getEnumTypeUrl + '?classname=GenderType&classnamespace=ZhiFang.Entity.LabStar'
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
                $("#itemRangeGenderID").html(html);
                form.render('select');

            }
        } else {
            layer.msg("性别枚举查询失败！", { icon: 5, anim: 6 });
        }
    });
    //获得年龄单位
    uxutil.server.ajax({
        url: itemRange.url.getEnumTypeUrl + '?classname=AgeUnitType&classnamespace=ZhiFang.Entity.LabStar'
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
                $("#itemRangeAgeUnitID").html(html);
                form.render('select');
            }
        } else {
            layer.msg("年龄单位字典查询失败！", { icon: 5, anim: 6 });
        }
    });
    //获得字典
    uxutil.server.ajax({
        url: itemRange.url.getDictUrl + "&where=IsUse=1 and DictType='" + itemRange.dictType + "'"
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                var html = "<option value=''>请选择</option>";
                if (data.length > 0) {
                    for (var i in data) {
                        html += '<option value="' + data[i].LBDict_DictCode + '">' + data[i].LBDict_CName + '</option>';
                    }
                    $("#itemRangeExpResultStatus1").html(html);
                    $("#itemRangeExpResultStatus2").html(html);
                    $("#itemRangeExpResultStatus3").html(html);
                    $("#itemRangeExpResultStatus4").html(html);
                    $("#itemRangeExpResultStatus5").html(html);
                    $("#itemRangeExpResultStatus6").html(html);
                    form.render('select');
                    //渲染之后加载
                    if (itemRange.currTabIndex == 0) {
                        itemRange.itemRangeResultWhere = "ItemID=" + itemRange.itemData.LBItem_Id + " and JudgeType=1";
                        itemRange.itemRangeResultTableRender(itemRange.itemRangeResultWhere);
                    } else if (itemRange.currTabIndex == 1 && !itemRange.isLoadDefultResultTab) {
                        itemRange.itemRangeResultWhere = "ItemID is null and JudgeType=1";
                        itemRange.itemRangeResultTableRender(itemRange.itemRangeResultWhere);
                    }
                    itemRange.itemRangeAfterWhere = "ItemID=" + itemRange.itemData.LBItem_Id + " and JudgeType=0";
                    itemRange.itemRangeAfterEmpty();
                    itemRange.itemRangeAfterInit(itemRange.itemRangeAfterWhere);
                }
            } else {
                layer.msg("结果状态未在字典中维护！", { icon: 5, anim: 6 });
            }
        } else {
            layer.msg("字典查询失败！", { icon: 5, anim: 6 });
        }
    });
    //获得警示级别
    uxutil.server.ajax({
        url: itemRange.url.getEnumTypeUrl + '?classname=TestFormReportValueAlarmLevel&classnamespace=ZhiFang.Entity.LabStar',
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
                $("#rangeAfter select[name=LBItemRangeExp_AlarmLevel]").html(html);
                $("#ItemRangeExpResultAlarmLevel").html(html);
                form.render('select');
            }
        } else {
            layer.msg("年龄单位字典查询失败！", { icon: 5, anim: 6 });
        }
    });
    itemRange.isGetDict = true;
}
//参考值表格初始化
itemRange.itemRangeTableRender = function (where) {
    table.render({
        elem: '#itemRangeTable',
        height: 110,
        defaultToolbar: [],
        size: 'sm', //小尺寸的表格
        url: itemRange.url.selectItemRangeUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBItemRange_Id',
                width: 60,
                title: '主键ID',
                hide: true
            }, {
                field: 'LBItemRange_ItemID',
                title: '项目Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemRange_SampleTypeID',
                title: '样本类型Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemRange_GenderID',
                title: '性别Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemRange_SectionID',
                title: '小组Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemRange_EquipID',
                title: '仪器Id',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBItemRange_DeptID',
                title: '送检科室Id',
                minWidth: 100,
                hide: true
            }, {
                field: 'LBItemRange_IsDefault',
                title: '缺省',
                minWidth: 60,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBItemRange_IsDefault.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBItemRange_RefRange',
                title: '参考范围描述',
                minWidth: 120
            }, {
                field: 'LBItemRange_ConditionName',
                title: '条件说明',
                minWidth: 200
            }, {
                field: 'LBItemRange_DispOrder',
                title: '判定次序',
                minWidth: 80
            }, {
                field: 'LBItemRange_LowAge',
                title: '年龄低限',
                minWidth: 80,
                hide: true
            }, {
                field: 'LBItemRange_HighAge',
                title: '年龄高限',
                minWidth: 80,
                hide: true
            }, {
                field: 'LBItemRange_LValueComp',
                title: '范围低限对比类型',
                minWidth: 160,
                hide: true
            }, {
                field: 'LBItemRange_LValue',
                title: '范围低限',
                minWidth: 80
            }, {
                field: 'LBItemRange_HValueComp',
                title: '范围高限对比类型',
                minWidth: 160,
                hide: true
            }, {
                field: 'LBItemRange_HValue',
                title: '范围高限',
                minWidth: 80
            }, {
                field: 'LBItemRange_LLValueComp',
                title: '异常低对比类型',
                minWidth: 140,
                hide: true
            }, {
                field: 'LBItemRange_LLValue',
                title: '异常低限',
                minWidth: 80
            }, {
                field: 'LBItemRange_HHValueComp',
                title: '异常高对比类型',
                minWidth: 140,
                hide: true
            }, {
                field: 'LBItemRange_HHValue',
                title: '异常高限',
                minWidth: 80
            }, {
                field: 'LBItemRange_DiagMethod',
                title: '默认检验方法',
                minWidth: 140,
                hide: true
            }, {
                field: 'LBItemRange_RedoLValueComp',
                title: '复检低线对比类型',
                minWidth: 160,
                hide: true
            }, {
                field: 'LBItemRange_RedoLValue',
                title: '复检低限',
                minWidth: 100,
                hide: true
            }, {
                field: 'LBItemRange_RedoHValueComp',
                title: '复检高线对比类型',
                minWidth: 160,
                hide: true
            }, {
                field: 'LBItemRange_RedoHValue',
                title: '复检高限',
                minWidth: 100,
                hide: true
            }, {
                field: 'LBItemRange_InvalidLValueComp',
                title: '无效低线对比类型',
                minWidth: 160,
                hide: true
            }, {
                field: 'LBItemRange_InvalidLValue',
                title: '无效低限',
                minWidth: 80
            }, {
                field: 'LBItemRange_InvalidHValueComp',
                title: '复检高线对比类型',
                minWidth: 160,
                hide: true
            }, {
                field: 'LBItemRange_InvalidHValue',
                title: '无效高限',
                minWidth: 80
            }
            ]
        ],
        page: false,
        limit: 1000,
        //limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            if (count > 0) {
                //默认选中第一行
                $("#itemRangeTable+div .layui-table-body table.layui-table tbody tr:first").click();
            } else {
                itemRange.itemRangeType = 'add';
                itemRange.SetDisabled(false, "itemRangeForm");
                itemRange.isEnableElem(true, "saveItemRange");
                itemRange.isEnableElem(false, "delItemRange");
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
table.on('row(itemRangeTable)', function (obj) {
    var data = obj.data;
    //标注选中样式
    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    //暂存选中数据
    if (itemRange.itemRangeData.length > 0 && itemRange.itemRangeData[0].LBItemRange_Id == data.LBItemRange_Id) return;
    itemRange.itemRangeData = [];//重置
    itemRange.itemRangeData.push(data);
    itemRange.itemRangeType = "show";
    itemRange.resetItemRange();//赋值表单
});
//清空/赋值颜色选择器
itemRange.colorpickerFun = function (Id, colorVal) {
    if (!Id) return;
    var colorVal = colorVal || false;
    if (colorVal) {
        //颜色
        colorpicker.render({
            elem: '#colorpicker_' + Id
            , color: colorVal
            , done: function (color) {
                $('#' + Id).val(color);
            }
        });
    } else {
        //颜色
        colorpicker.render({
            elem: '#colorpicker_' + Id
            , done: function (color) {
                $('#' + Id).val(color);
            }
        });
    }
}
//获得显示次序
itemRange.getDispOrder = function (entityName, Id) {
    uxutil.server.ajax({
        url: itemRange.url.getMaxNoByEntityFieldUrl + '?entityName=' + entityName + '&entityField=DispOrder'
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
itemRange.getDispOrderForThisTable = function (TableName, DispOrderName,ElemId) {
    var data = table.cache[TableName];
    var DispOrder = 0;
    for (var i = 0; i < data.length;i++) {
        if (data[i][DispOrderName] > DispOrder) {
            DispOrder = data[i][DispOrderName];
        }
    }
    $("#" + ElemId).val(Number(DispOrder) + 1);
}
//获得当前日期
itemRange.getNewDate = function (seperator, hasTime) {
    var date = new Date();
    var seperator = seperator || "-";
    var hasTime = hasTime || false;//是否要包含当前时间
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();
    if (hasTime) {
        var currentdate = year + seperator + month + seperator + strDate + " " + hours + ":" + minutes + ":" + seconds;
        return currentdate;
    } else {
        var currentdate = year + seperator + month + seperator + strDate;
        return currentdate;
    }
}
//项目参考值结果单位下拉监听 --同步输入框
form.on('select(ItemRangeUnitSelect)', function (data) {
    $("#ItemRangeUnitInput").val(data.value);
});
//监听低值同步参考范围
$('#ItemRangeLValue').bind('input propertychange', function () {
    itemRange.setReferenceRange();
});
//监听高值同步参考范围
$('#ItemRangeHValue').bind('input propertychange', function () {
    itemRange.setReferenceRange();
});
//监听低值下拉大于等于同步参考范围
form.on('select(LValueComp)', function (data) {
    itemRange.setReferenceRange();
});
//监听高值下拉大于等于同步参考范围
form.on('select(HValueComp)', function (data) {
    itemRange.setReferenceRange();
});  
//参考范围根据高低值生成
itemRange.setReferenceRange = function () {
    var LValue = $("#ItemRangeLValue").val();
    var HValue = $("#ItemRangeHValue").val();
    if (LValue != "" && HValue != "") {
        $("#ItemRangeRefRange").val(LValue + " - " + HValue);
    } else if (LValue == "" && HValue != "") {
        var ItemRangeHValueComp = $("#ItemRangeHValueComp>option:selected").val();
        $("#ItemRangeRefRange").val(ItemRangeHValueComp+" "+HValue);
    } else if (LValue != "" && HValue == "") {
        var ItemRangeLValueComp = $("#ItemRangeLValueComp>option:selected").val();
        $("#ItemRangeRefRange").val(ItemRangeLValueComp + " " + LValue);
    } else {
        $("#ItemRangeRefRange").val("");
    }
}
//项目参考值重置
itemRange.itemRangeReset = function () {
    if (itemRange.itemRangeType == 'edit' || itemRange.itemRangeType == 'show') {
        if (itemRange.itemRangeData.length > 0) {
            var data = itemRange.itemRangeData[itemRange.itemRangeData.length - 1];
            data.LBItemRange_BCritical = String(data.LBItemRange_BCritical) == 'true' ? true : false;
            form.val("itemRangeForm", data);
            //下拉框赋值
            for (var i in data) {
                $("#itemRangeForm input[type=checkbox][lay-skin='switch']").each(function () {
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
                if (i == "LBItemRange_BCollectTime" || i == "LBItemRange_ECollectTime") {
                    if (data[i] != "") {
                        $("#" + i.split("_")[1]).val(data[i].split(" ")[1]);
                    } else {
                        $("#" + i.split("_")[1]).val("");
                    }
                    
                }
            }
        }
        itemRange.SetDisabled(itemRange.itemRangeType == 'show', "itemRangeForm");
        itemRange.isEnableElem(itemRange.itemRangeType != 'show', "saveItemRange");
        itemRange.isEnableElem(true, "delItemRange");
    }
}
//项目参考值清空
itemRange.itemRangeEmpty = function () {
    $("#itemRangeForm")[0].reset();
}
//重置项目参考值
itemRange.resetItemRange = function () {
    if (itemRange.itemRangeData.length == 0) {
        itemRange.itemRangeEmpty();
        return;
    }
    if (itemRange.itemRangeType == 'add') 
        itemRange.itemRangeEmpty();
     else 
        itemRange.itemRangeReset();
}
//点击项目参考值新增按钮
$("#addItemRange").click(function () {
    itemRange.itemRangeType = 'add';
    if (itemRange.itemRangeData.length > 0) {
        itemRange.itemRangeEmpty()
        //有参考范围 缺省默认否
        if (table.cache.itemRangeTable.length > 0) {
            $("#IsDefault").next('.layui-form-switch').removeClass('layui-form-onswitch');
            $("#IsDefault").next('.layui-form-switch').children("em").html("否");
            $("#IsDefault")[0].checked = false;
        }
    }
    itemRange.getDispOrderForThisTable("itemRangeTable", "LBItemRange_DispOrder", "itemRangeDispOrder");
    itemRange.SetDisabled(false, "itemRangeForm");
    itemRange.isEnableElem(true, "saveItemRange");
    itemRange.isEnableElem(false, "delItemRange");
});
//点击项目参考值编辑按钮
$("#editItemRange").click(function () {
    if (itemRange.itemRangeData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        itemRange.itemRangeType = 'edit';
        itemRange.itemRangeReset();
    }
});
//点击项目参考值删除按钮
$("#delItemRange").click(function () {
    if (itemRange.itemRangeData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var len = itemRange.itemRangeData.length;
            for (var i = 0; i < itemRange.itemRangeData.length; i++) {
                var Id = itemRange.itemRangeData[i].LBItemRange_Id;
                uxutil.server.ajax({
                    url: itemRange.url.delItemRangeUrl + "?Id=" + Id
                }, function (res) {
                    if (res.success) {
                        len--;
                        if (len == 0) {
                            layer.close(index);
                            layer.msg("删除成功！", { icon: 6, anim: 0 });
                            itemRange.itemRangeData = [];//重置
                            table.reload('itemRangeTable', {
                                url: itemRange.url.selectItemRangeUrl + "&where=" + itemRange.itemRangeWhere,
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
//点击项目参考值重置按钮
$("#resetItemRange").click(function () {
    itemRange.resetItemRange();
});
//项目参考值保存
form.on('submit(saveItemRange)', function (data) {
    window.event.preventDefault();
    if (itemRange.itemRangeType == 'show') return false;
    var fields = "";//发送修改的字段
    var postData = {};//发送的数据
    if (!$("#IsDefault").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
        data.field.LBItemRange_IsDefault = "false";
    } else {
        data.field.LBItemRange_IsDefault = "true";
    }
    data.field.LBItemRange_BCritical = $("#BCritical").prop('checked');
    for (var i in data.field) {
        if (i == "no") {//去除结果单位下拉框
            continue;
        }
        //采样时间
        if (i.split("_")[1] == "BCollectTime" || i.split("_")[1] == "ECollectTime") {
            if (data.field[i] == "") {
                continue;
            } else {
                postData[i.split("_")[1]] = uxutil.date.toServerDate(itemRange.getNewDate("-", false) + " " + data.field[i]);
                fields += i.split("_")[1] + ",";
                continue;
            }
        }
        postData[i.split("_")[1]] = typeof data.field[i] == "string" ? data.field[i].trim() : data.field[i];
        fields += i.split("_")[1] + ",";
    }
    
    postData.SectionID = postData.SectionID != "" ? postData.SectionID : null;
    postData.EquipID = postData.EquipID != "" ? postData.EquipID : null;
    postData.DeptID = postData.DeptID != "" ? postData.DeptID : null;
    postData.DiagID = postData.DiagID != "" ? postData.DiagID : null;
    postData.CollectPartID = postData.CollectPartID != "" ? postData.CollectPartID : null;
    postData.PhyPeriodID = postData.PhyPeriodID != "" ? postData.PhyPeriodID : null;
    postData.GenderID = postData.GenderID != "" ? postData.GenderID : null;
    //postData.DeptID = postData.DeptID != "" ? postData.DeptID : 0;
    postData.AgeUnitID = postData.AgeUnitID != "" ? postData.AgeUnitID : null;
    postData.SampleTypeID = postData.SampleTypeID != "" ? postData.SampleTypeID : null;
    if (postData.LowAge != "" && isNaN(postData.LowAge)) {//年龄低限
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.LowAge = postData.LowAge != "" ? postData.LowAge : null;

    if (postData.HighAge != "" && isNaN(postData.HighAge)) {//年龄高限
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.HighAge = postData.HighAge != "" ? postData.HighAge : null;

    if (postData.LValue != "" && isNaN(postData.LValue)) {//范围低值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.LValue = postData.LValue != "" ? postData.LValue : null;

    if (postData.HValue != "" && isNaN(postData.HValue)) {//范围高值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.HValue = postData.HValue != "" ? postData.HValue : null;

    if (postData.LLValue != "" && isNaN(postData.LLValue)) {//异常低值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.LLValue = postData.LLValue != "" ? postData.LLValue : null;

    if (postData.HHValue != "" && isNaN(postData.HHValue)) {//异常高值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.HHValue = postData.HHValue != "" ? postData.HHValue : null;

    if (postData.RedoLValue != "" && isNaN(postData.RedoLValue)) {//复检低值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.RedoLValue = postData.RedoLValue != "" ? postData.RedoLValue : null;

    if (postData.RedoHValue != "" && isNaN(postData.RedoHValue)) {//复检高值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.RedoHValue = postData.RedoHValue != "" ? postData.RedoHValue : null;

    if (postData.InvalidLValue != "" && isNaN(postData.InvalidLValue)) {//无效低值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.InvalidLValue = postData.InvalidLValue != "" ? postData.InvalidLValue : null;

    if (postData.InvalidHValue != "" && isNaN(postData.InvalidHValue)) {//无效高值
        layer.msg("请正确输入高低值!", { icon: 5, anim: 6 });
        return;
    }
    postData.InvalidHValue = postData.InvalidHValue != "" ? postData.InvalidHValue : null;
    //项目属性
    var DataTimeStamp = [0, 0, 0, 0, 0, 0, 41, 186];
    postData.LBItem = { Id: itemRange.itemData.LBItem_Id, DataTimeStamp: DataTimeStamp };
    //生成条件说明
    postData.ConditionName = itemRange.setConditionName();
    fields += "ConditionName,LBItem_Id,LBItem_DataTimeStamp";
    common.load.loadShow();
    if (itemRange.itemRangeType == "edit") {
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData, fields: fields }),
            url: itemRange.url.updateItemRangeUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("编辑成功！", { icon: 6, anim: 0 });
                itemRange.itemRangeData = [];//重置
                table.reload('itemRangeTable', {
                    url: itemRange.url.selectItemRangeUrl + "&where=" + itemRange.itemRangeWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
            } else {
                layer.msg("编辑失败！", { icon: 5, anim: 6 });
            }
        });
    } else if (itemRange.itemRangeType == "add") {
        delete postData.Id;
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData }),
            url: itemRange.url.addItemRangeUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("新增成功！", { icon: 6, anim: 0 });
                itemRange.itemRangeData = [];//重置
                table.reload('itemRangeTable', {
                    url: itemRange.url.selectItemRangeUrl + "&where=" + itemRange.itemRangeWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
            } else {
                layer.msg("新增失败！", { icon: 5, anim: 6 });
            }
        });
    }
});
//生成条件说明
itemRange.setConditionName = function () {
    var ConditionName = "";
    //检验小组
    var Section = $("#itemRangeSectionID>option:selected");
    if (Section.length > 0 && Section[0].value) ConditionName += "检验小组=" + Section[0].text + " ";
    //检验仪器
    var Equip = $("#itemRangeEquipID>option:selected");
    if (Equip.length > 0 && Equip[0].value) ConditionName += "检验仪器=" + Equip[0].text + " ";
    //样本类型
    var SampleType = $("#itemRangeSampleTypeID>option:selected");
    if (SampleType.length > 0 && SampleType[0].value) ConditionName += "样本类型=" + SampleType[0].text + " ";
    //性别
    var Gender = $("#itemRangeGenderID>option:selected");
    if (Gender.length > 0 && Gender[0].value) ConditionName += "性别=" + Gender[0].text + " ";
    //年龄 + 年龄单位
    var LowAge = $("#ItemRangeLowAge").val();
    var HighAge = $("#ItemRangeHighAge").val(); 
    var AgeUnitID = $("#itemRangeAgeUnitID>option:selected");
    if (LowAge != "" && HighAge != "") {
        ConditionName += "年龄=" + LowAge + " - " + HighAge;
    } else if (LowAge == "" && HighAge != "") {
        ConditionName += "年龄<=" +HighAge;
    } else if (LowAge != "" && HighAge == "") {
        ConditionName += "年龄>=" + LowAge;
    }
    if (AgeUnitID != "") {
        if (LowAge != "" || HighAge != "") {
            if (AgeUnitID.length > 0 && AgeUnitID[0].value) ConditionName += AgeUnitID[0].text + " ";
        }
    }
    //采样开始时间
    var BCollectTime = $("#BCollectTime").val();
    if (BCollectTime != "") ConditionName += "采样开始时间=" + BCollectTime + " ";
    //采样截止时间
    var ECollectTime = $("#ECollectTime").val();
    if (ECollectTime != "") ConditionName += "采样截止时间=" + ECollectTime + " ";
    return ConditionName;
}
/***************************************描述结果判断***********************/
//项目参考值描述结果判断type值LBItemRangeExp_BAlarmColor
itemRange.itemRangeExpResultType = 'show';
//描述结果判断页签切换
element.on('tab(resultJudgTab)', function (obj) {
    itemRange.currTabIndex = obj.index;
    itemRange.itemRangeExpResultEmpty();
    itemRange.itemRangeResultWhere = itemRange.currTabIndex == 0 ? "ItemID=" + itemRange.itemData.LBItem_Id + " and JudgeType=1" :"ItemID is null and JudgeType=1";
    itemRange.itemRangeResultTableRender(itemRange.itemRangeResultWhere);
});
//描述结果判断表格初始化
itemRange.itemRangeResultTableRender = function (where) {
    table.render({
        elem: '#itemRangeResultTable',
        height: 160,
        defaultToolbar: [],
        size: 'sm', //小尺寸的表格
        //toolbar: '#toolbarItemRange',
        url: itemRange.url.selectItemRangeExpUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBItemRangeExp_Id',
                width: 60,
                title: '主键ID',
                hide: true
            }, {
                field: 'LBItemRangeExp_ItemID',
                title: '项目Id',
                width: 130,
                hide: true
            }, {
                field: 'LBItemRangeExp_DispOrder',
                title: '判定次序',
                width: 100
            }, {
                field: 'LBItemRangeExp_JudgeValue',
                title: '判定值',
                width: 100,
                sort: true
            }, {
                field: 'LBItemRangeExp_ResultStatus',
                title: '结果状态',
                width: 100,
                hide:true
            }, {
                field: 'LBItemRangeExp_ResultReport',
                title: '报告值',
                width: 100
            }, {
                field: 'LBItemRangeExp_IsAddReport',
                title: '报告值是否替换',
                width: 140,
                templet: function (data) {
                    var str = "<span>否</span> ";
                    if (data.LBItemRangeExp_IsAddReport.toString() == "true") {
                        str = "<span  style='color:red'>是</span>"
                    }
                    return str;
                }
            }, {
                field: 'LBItemRangeExp_BAlarmColor',
                title: '警示级别',
                width: 100,
                hide: true
            }, {
                field: 'LBItemRangeExp_ResultComment',
                title: '结果说明',
                minWidth: 140
            }, {
                field: 'LBItemRangeExp_AlarmColor',
                title: '特殊提示色',
                width: 100,
                hide:true
            }
            ]
        ],
        page: false,
        limit: 1000,
        //limits: [10, 15, 20, 25, 30],
        autoSort: false, //禁用前端自动排序
        done: function (res, curr, count) {
            if (count > 0) {
                //默认选中第一行
                $("#itemRangeResultTable+div .layui-table-body table.layui-table tbody tr:first").click();
            } else {
                itemRange.itemRangeExpResultType = 'add';
                itemRange.itemRangeExpResultData = [];//重置
                itemRange.SetDisabled(false, "ItemRangeExpResult");
                itemRange.isEnableElem(true, "saveItemRangeExpResult");
                itemRange.isEnableElem(false, "delItemRangeExpResult");
            }
            if (itemRange.currTabIndex == 1) itemRange.isLoadDefultResultTab = true;
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
table.on('row(itemRangeResultTable)', function (obj) {
    var data = obj.data;
    //标注选中样式
    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
    //暂存选中数据
    if (itemRange.itemRangeExpResultData.length > 0 && itemRange.itemRangeExpResultData[0].LBItemRangeExp_Id == data.LBItemRangeExp_Id) return;
    itemRange.itemRangeExpResultData = [];//重置
    itemRange.itemRangeExpResultData.push(data);
    itemRange.itemRangeExpResultType = "show";
    itemRange.resetItemRangeExpResult();//赋值表单
});
//描述结果判断重置赋值
itemRange.itemRangeExpResultReset = function () {
    if (itemRange.itemRangeExpResultType == 'edit' || itemRange.itemRangeExpResultType == 'show') {
        if (itemRange.itemRangeExpResultData.length > 0) {
            var data = itemRange.itemRangeExpResultData[itemRange.itemRangeExpResultData.length - 1];
            form.val("ItemRangeExpResult", data);
            //开关赋值
            var BAlarmColor = itemRange.itemRangeExpResultData[itemRange.itemRangeExpResultData.length - 1].LBItemRangeExp_BAlarmColor;
            if (BAlarmColor.toString() == "true") {
                if (!$("#BAlarmColor6").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    $("#BAlarmColor6").next('.layui-form-switch').addClass('layui-form-onswitch');
                    $("#BAlarmColor6").next('.layui-form-switch').children("em").html("是");
                    $("#BAlarmColor6")[0].checked = true;
                }
            } else {
                if ($("#BAlarmColor6").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    $("#BAlarmColor6").next('.layui-form-switch').removeClass('layui-form-onswitch');
                    $("#BAlarmColor6").next('.layui-form-switch').children("em").html("否");
                    $("#BAlarmColor6")[0].checked = false;
                }
            }
            //单选框赋值
            var IsAddReportResult = itemRange.itemRangeExpResultData[itemRange.itemRangeExpResultData.length - 1].LBItemRangeExp_IsAddReport;
            $("#IsAddReportForResult input[type=radio]").removeAttr("checked");
            if (IsAddReportResult.toString() == "true") {
                $("#IsAddReportForResult input[type=radio][value=1]").prop("checked","checked");
            } else {
                $("#IsAddReportForResult input[type=radio][value=0]").prop("checked", "checked");
            }
            
            form.render('radio'); //更新全部   
            //颜色转换
            var AlarmColor6 = itemRange.itemRangeExpResultData[itemRange.itemRangeExpResultData.length - 1].LBItemRangeExp_AlarmColor
            if (AlarmColor6 != "") {
                itemRange.colorpickerFun("AlarmColor6", AlarmColor6);//赋值颜色器
            } else {
                itemRange.colorpickerFun("AlarmColor6");
                $("#AlarmColor6").val("");
            }
        }
        itemRange.SetDisabled(itemRange.itemRangeExpResultType == 'show', "ItemRangeExpResult");
        itemRange.isEnableElem(itemRange.itemRangeExpResultType != 'show', "saveItemRangeExpResult");
        itemRange.isEnableElem(true, "delItemRangeExpResult");
    }
}
//描述结果判断清空
itemRange.itemRangeExpResultEmpty = function () {
    $("#ItemRangeExpResult")[0].reset();
    $("#ItemRangeExpResult input[name=LBItemRangeExp_IsAddReport][value=0]").attr("checked", true);
    itemRange.colorpickerFun("AlarmColor6");
}
//重置描述结果判断
itemRange.resetItemRangeExpResult = function () {
    if (itemRange.itemRangeExpResultData.length == 0) {
        itemRange.itemRangeExpResultEmpty();
        return;
    }
    if (itemRange.itemRangeExpResultType == 'add') 
        itemRange.itemRangeExpResultEmpty();
    else
        itemRange.itemRangeExpResultReset();
}
//点击描述结果判断新增
$("#addItemRangeExpResult").click(function () {
    itemRange.itemRangeExpResultType = 'add';
    if (itemRange.itemRangeExpResultData.length > 0) {
        itemRange.itemRangeExpResultEmpty();
    }
    itemRange.getDispOrderForThisTable("itemRangeResultTable", "LBItemRangeExp_DispOrder", "ItemRangeExpResultDispOrder");
    itemRange.SetDisabled(false, "ItemRangeExpResult");
    itemRange.isEnableElem(true, "saveItemRangeExpResult");
    itemRange.isEnableElem(false, "delItemRangeExpResult");
});
//点击描述结果判断编辑
$("#editItemRangeExpResult").click(function () {
    if (itemRange.itemRangeExpResultData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        itemRange.itemRangeExpResultType = 'edit';
        itemRange.itemRangeExpResultReset();
    }
});
//点击描述结果判断删除
$("#delItemRangeExpResult").click(function () {
    if (itemRange.itemRangeExpResultData.length === 0) {
        layer.msg('请选择一行！');
    } else {
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var len = itemRange.itemRangeExpResultData.length;
            for (var i = 0; i < itemRange.itemRangeExpResultData.length; i++) {
                var Id = itemRange.itemRangeExpResultData[i].LBItemRangeExp_Id;
                uxutil.server.ajax({
                    url: itemRange.url.delItemRangeExpUrl + "?Id=" + Id
                }, function (res) {
                    if (res.success) {
                        len--;
                        if (len == 0) {
                            layer.close(index);
                            layer.msg("删除成功！", { icon: 6, anim: 0 });
                            itemRange.itemRangeExpResultData = [];//重置
                            table.reload('itemRangeResultTable', {
                                url: itemRange.url.selectItemRangeExpUrl + "&where=" + itemRange.itemRangeResultWhere,
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
//点击描述结果判断重置
$("#resetItemRangeExpResult").click(function () {
    itemRange.resetItemRangeExpResult();
});
//描述结果判断保存
form.on('submit(saveItemRangeExpResult)', function (data) {
    window.event.preventDefault();
    var fields = "";//发送修改的字段
    var postData = {};//发送的数据
    if (!$("#BAlarmColor6").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
        data.field.LBItemRangeExp_BAlarmColor = "false";
    } else {
        data.field.LBItemRangeExp_BAlarmColor = "true";
    }
    data.field.LBItemRangeExp_AlarmLevel = data.field.LBItemRangeExp_AlarmLevel == "" ? null : data.field.LBItemRangeExp_AlarmLevel;
    for (var i in data.field) {
        postData[i.split("_")[1]] = typeof data.field[i] == "string" ? data.field[i].trim() : data.field[i];
        fields += i.split("_")[1] + ",";
    }
    postData.JudgeType = 1;
    fields += "JudgeType";
    //项目描述结果判断
    if (itemRange.currTabIndex == 0) {
        var arr = itemRange.itemData.LBItem_DataTimeStamp.split(",");
        var DataTimeStamp = [];
        for (var i in arr) {
            DataTimeStamp.push(Number(arr[i]));
        }
        postData.LBItem = { Id: itemRange.itemData.LBItem_Id, DataTimeStamp: DataTimeStamp };
        fields += "LBItem_Id,LBItem_DataTimeStamp";
    }
    common.load.loadShow();
    if (itemRange.itemRangeExpResultType == "edit") {
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData, fields: fields }),
            url: itemRange.url.updateItemRangeExpUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("编辑成功！", { icon: 6, anim: 0 });
                itemRange.itemRangeExpResultData = [];//重置
                table.reload('itemRangeResultTable', {
                    url: itemRange.url.selectItemRangeExpUrl + "&where=" + itemRange.itemRangeResultWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
            } else {
                layer.msg("编辑失败！", { icon: 5, anim: 6 });
            }
        });
    } else if (itemRange.itemRangeExpResultType == "add") {
        delete postData.Id;
        uxutil.server.ajax({
            type: 'post',
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify({ entity: postData }),
            url: itemRange.url.addItemRangeExpUrl
        }, function (res) {
            common.load.loadHide();
            if (res.success) {
                layer.msg("新增成功！", { icon: 6, anim: 0 });
                itemRange.itemRangeExpResultData = [];//重置
                table.reload('itemRangeResultTable', {
                    url: itemRange.url.selectItemRangeExpUrl + "&where=" + itemRange.itemRangeResultWhere,
                    where: {
                        time: new Date().getTime()
                    }
                });
            } else {
                layer.msg("新增失败！", { icon: 5, anim: 6 });
            }
        });
    }
});
//表单验证
form.verify({
    ZDY_required: function (value, item) { //value：表单的值、item：表单的DOM对象
        if (value == "") {
            var label = $(item).parents(".layui-form-item").children(".layui-form-label").html(),
                msg = "";
            if (label) {
                msg = label + "不能为空！";
            } else {
                msg = '必填项不能为空';
            }
            return msg;
        }
    },
    ZDY_number: function (value, item) {
        if (!value || isNaN(value)) {
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
/***************************************参考范围后处理***********************/
//赋值
itemRange.itemRangeAfterInit = function (where) {
    itemRange.itemRangeAfterEmpty();
    //获得参考范围后处理
    uxutil.server.ajax({
        url: itemRange.url.selectItemRangeExpUrl + "&where=" + where
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                var html = "";
                if (data.length > 0) {
                    //是否是追加报告值根据第一条数据变化
                    if (String(data[0].LBItemRangeExp_IsAddReport) == "true")
                        $("#IsAddReportForAfter>input[name=IsAddReport][value=1]").prop("checked", true);
                    else
                        $("#IsAddReportForAfter>input[name=IsAddReport][value=0]").prop("checked", true);
                    for (var i in data) {
                        $("#rangeAfter>form").each(function () {
                            if ($(this).attr("data-type") == data[i].LBItemRangeExp_JudgeValue) {
                                form.val($(this).attr("lay-filter"), data[i]);
                                //开关赋值
                                var BAlarmColor = data[i].LBItemRangeExp_BAlarmColor;
                                var checkbox = $(this).find("input[type=checkbox]");
                                if (BAlarmColor.toString() == "true") {
                                    if (!checkbox.next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                        checkbox.next('.layui-form-switch').addClass('layui-form-onswitch');
                                        checkbox.next('.layui-form-switch').children("em").html("是");
                                        checkbox[0].checked = true;
                                    }
                                } else {
                                    if (checkbox.next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                                        checkbox.next('.layui-form-switch').removeClass('layui-form-onswitch');
                                        checkbox.next('.layui-form-switch').children("em").html("否");
                                        checkbox[0].checked = false;
                                    }
                                }
                                var colorId = $("#" + $(this).attr("Id")).find(".colorpickerStyle>input").attr("Id");
                                //颜色转换
                                if (data[i].LBItemRangeExp_AlarmColor != "") {
                                    $("#" + colorId).val(data[i].LBItemRangeExp_AlarmColor);
                                    itemRange.colorpickerFun(colorId, data[i].LBItemRangeExp_AlarmColor);//赋值颜色器
                                } else {
                                    itemRange.colorpickerFun(colorId);
                                    $("#" + colorId).val("");
                                }
                            }
                        });
                    }
                } else {
                    //如果没有设置过则默认显示替换报告值
                    $("#IsAddReportForAfter>input[name=IsAddReport][value=0]").prop("checked", true);
                }
            } else {
                //如果没有设置过则默认显示替换报告值
                $("#IsAddReportForAfter>input[name=IsAddReport][value=0]").prop("checked", true);
            }
        } else {
            layer.msg("参考范围后处理数据查询失败！", { icon: 5, anim: 6 });
            //如果没有设置过则默认显示替换报告值
            $("#IsAddReportForAfter>input[name=IsAddReport][value=0]").prop("checked", true);
        }
        form.render();
    });
}
//监听替换/追加报告纸单选
form.on('radio(IsAddReport)', function (data) {
    //if (!itemRange.itemRangeAfterWhere) {
    //    layer.msg("参考范围后处理数据查询失败,请检查是否配置结果状态字典！", { icon: 5, anim: 6 });
    //    return;
    //}
    //var where = itemRange.itemRangeAfterWhere;// + "and IsAddReport=" + data.value;
    //itemRange.itemRangeAfterInit(where);
})
//清空项目参考值范围后处理
itemRange.itemRangeAfterEmpty = function () {
    $("#LBItemRangeExpForm1")[0].reset();
    $("#LBItemRangeExpForm2")[0].reset();
    $("#LBItemRangeExpForm3")[0].reset();
    $("#LBItemRangeExpForm4")[0].reset();
    $("#LBItemRangeExpForm5")[0].reset();
    $("#rangeAfter input[type=hidden]").val("");
    //颜色选择器初始化
    itemRange.colorpickerFun("AlarmColor1");
    itemRange.colorpickerFun("AlarmColor2");
    itemRange.colorpickerFun("AlarmColor3");
    itemRange.colorpickerFun("AlarmColor4");
    itemRange.colorpickerFun("AlarmColor5");
}
//获得保存数据
$("#itemRangeAfterSave").click(function () {
    //报告是替换还是追加
    var IsAddReport = $("#IsAddReportForAfter input[name=IsAddReport]:checked").val();
    var arr1 = $("#LBItemRangeExpForm1").serialize().split("&");
    var arr2 = $("#LBItemRangeExpForm2").serialize().split("&");
    var arr3 = $("#LBItemRangeExpForm3").serialize().split("&");
    var arr4 = $("#LBItemRangeExpForm4").serialize().split("&");
    var arr5 = $("#LBItemRangeExpForm5").serialize().split("&");
    var arr = [arr1, arr2, arr3, arr4, arr5];
    var entityList = [];
    for (var i = 0; i < arr.length; i++) {
        var index = 0;
        var type = "";
        var entity = {};
        for (var j = 0; j < arr[i].length; j++) {
            if (arr[i][j].split("=")[1] == "") {
                if (arr[i][j].split("=")[0].split("_")[1] == "Id") {
                    type = 'add';
                } else if (arr[i][j].split("=")[0].split("_")[1] == "JudgeValue") {
                    //不做处理
                } else {
                    index++;
                }
            }
            if (arr[i][j].split("=")[0].split("_")[1] == "ResultReport" || arr[i][j].split("=")[0].split("_")[1] == "ResultComment" || arr[i][j].split("=")[0].split("_")[1] == "ResultStatus" || arr[i][j].split("=")[0].split("_")[1] == "AlarmColor") {
                entity[arr[i][j].split("=")[0].split("_")[1]] = decodeURIComponent(arr[i][j].split("=")[1]);
                continue;
            }
            entity[arr[i][j].split("=")[0].split("_")[1]] = arr[i][j].split("=")[1];
        }
        var flag = false;
        for (var a in entity) {
            if (a == "BAlarmColor") {
                flag = true;
                entity[a] = true;
            }
        }
        if (!flag) {
            entity.BAlarmColor = false;
        }
        var DataTimeStampArr = itemRange.itemData.LBItem_DataTimeStamp.split(",");
        var DataTimeStamp = [];
        for (var k in DataTimeStampArr) {
            DataTimeStamp.push(Number(DataTimeStampArr[k]));
        }
        entity.LBItem = { Id: itemRange.itemData.LBItem_Id, DataTimeStamp: DataTimeStamp };
        entity.JudgeType = 0;
        entity.IsAddReport = IsAddReport;
        if (type == 'add') {
            if (index < 5) {//新增
                delete entity.Id;
                entity.type = 'add';
                entity.AlarmLevel = entity.AlarmLevel == "" ? null : entity.AlarmLevel;
                entityList.push(entity);
            }
        } else { 
            if (index < 5) {//编辑
                entity.type = 'edit';
                entity.AlarmLevel = entity.AlarmLevel == "" ? null : entity.AlarmLevel;
                entityList.push(entity);
            } else {//删除
                entityList.push({ Id: entity.Id,type:'del'});
            }
        }
    }
    itemRange.saveItemRangeAfter(entityList, IsAddReport);
});
//保存参考范围后处理
itemRange.saveItemRangeAfter = function (entityList, IsAddReport) {
    var entityList = entityList;
    if (!entityList || entityList.length == 0) return;
    //保存总数
    itemRange.saveCount = entityList.length;
    common.load.loadShow();
    for (var i = 0; i < entityList.length; i++) {
        var entity = entityList[i];
        itemRange.saveItemRangeAfterHandle(entity,i);
    }
}
////保存参考范围后处理
itemRange.saveItemRangeAfterHandle = function (entity,index) {
    var entity = entity || null,
        index = index;
    if (!entity) return;
    setTimeout(function () {
        if (entity.type == 'add') {
            delete entity.type;
            uxutil.server.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: entity }),
                url: itemRange.url.addItemRangeExpUrl
            }, function (res) {
                itemRange.saveCount--;
                if (res.success) {
                    if (itemRange.saveCount == 0) {
                        layer.msg("保存成功！", { icon: 6, anim: 0 });
                    }
                } else {
                    layer.msg("新增失败！", { icon: 5, anim: 6 });
                }
                if (itemRange.saveCount == 0) {
                    common.load.loadHide();
                    itemRange.itemRangeAfterInit(itemRange.itemRangeAfterWhere);
                }
            });
        } else if (entity.type == 'edit') {
            delete entity.type;
            var fields = "AlarmLevel,IsAddReport,AlarmColor,BAlarmColor,Id,JudgeType,JudgeValue,ResultComment,ResultReport,ResultStatus,LBItem_Id,LBItem_DataTimeStamp";
            uxutil.server.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: entity, fields: fields }),
                url: itemRange.url.updateItemRangeExpUrl
            }, function (res) {
                itemRange.saveCount--;
                if (res.success) {
                    if (itemRange.saveCount == 0) {
                        layer.msg("保存成功！", { icon: 6, anim: 0 });
                    }
                } else {
                    layer.msg("编辑失败！", { icon: 5, anim: 6 });
                }
                if (itemRange.saveCount == 0) {
                    itemRange.itemRangeAfterInit(itemRange.itemRangeAfterWhere);// + "and IsAddReport=" + IsAddReport
                    common.load.loadHide();
                }
            });
        } else if (entity.type == 'del') {
            var Id = entity.Id;
            uxutil.server.ajax({
                url: itemRange.url.delItemRangeExpUrl + "?Id=" + Id
            }, function (res) {
                itemRange.saveCount--;
                if (res.success) {
                    if (itemRange.saveCount == 0) {
                        layer.msg("保存成功！", { icon: 6, anim: 0 });
                    }
                } else {
                    layer.msg("删除失败！", { icon: 5, anim: 6 });
                }
                if (itemRange.saveCount == 0) {
                    itemRange.itemRangeAfterInit(itemRange.itemRangeAfterWhere);// + "and IsAddReport=" + IsAddReport
                    common.load.loadHide();
                }
            });
        }
    }, index * 100);
};

//禁用处理
itemRange.SetDisabled = function (isDisabled, FormID) {
    if (!FormID) return;
    $("#" + FormID+" :input").each(function (i, item) {
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
//是否禁用某元素
itemRange.isEnableElem = function (bo, elemID) {
    if (!elemID) return;
    if (bo)
        $("#" + elemID).removeClass("layui-btn-disabled").removeAttr('disabled', true);
    else
        $("#" + elemID).addClass("layui-btn-disabled").attr('disabled', true);
};