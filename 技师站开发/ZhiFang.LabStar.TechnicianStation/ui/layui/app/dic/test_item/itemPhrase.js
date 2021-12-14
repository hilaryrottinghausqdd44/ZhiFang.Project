/**
 * 项目短语
 * @author zhangda
 * @version 2019-07-16
 */
var itemPhrase = new Object();
//项目信息
itemPhrase.itemData = {};
//小组选中数据
itemPhrase.itemPhraseSectionData = [];
//对应字典类型
itemPhrase.dictType = '项目短语';
//样本类型
itemPhrase.sampleTypeMap = {};
//字典信息
itemPhrase.info = [];
//服务路径地址
itemPhrase.url = {};
//设置服务路径
itemPhrase.setUrl = function () {
    itemPhrase.url = {
        //getDictUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDictByHQL?isPlanish=true&sort=[{property:"LBDict_DispOrder",direction:"ASC"}]&fields=LBDict_Id,LBDict_LBDictType_Id,LBDict_LBDictType_DictTypeCode,LBDict_CName,LBDict_DictCode,LBDict_EName,LBDict_SName,LBDict_Shortcode,LBDict_PinYinZiTou,LBDict_DictInfo,LBDict_Ilevel,LBDict_IColor,LBDict_IColorDefault,LBDict_DictValue,LBDict_DictValueDefault,LBDict_Comment,LBDict_IsUse,LBDict_DispOrder',
        getEnumTypeUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',//获得枚举 传递枚举类型名 
        //获得短语
        getItemPhraseUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true&sort=[{property:"LBPhrase_DispOrder",direction:"ASC"}]',
        delItemPhraseUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase',
        //获得样本类型
        getSampleTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
    };
}
//初始化--最先执行
itemPhrase.init = function () {
    itemPhrase.itemData = common.checkData[common.checkData.length - 1];
    if (itemPhrase.itemData.LBItem_GroupType != 0) {//单项加载
        return;
    }
    itemPhrase.setUrl();
    itemPhrase.getSampleType();
    itemPhrase.getDictInfo();
}
//获取所有样本类型信息
itemPhrase.getSampleType = function () {
    var url = itemPhrase.url.getSampleTypeUrl;
    url += '&sort=[{property:"LBSampleType_DispOrder",direction:"ASC"}]';
    url += '&where=IsUse=1';
    url += '&fields=LBSampleType_Id,LBSampleType_CName';
    itemPhrase.sampleTypeMap = {};
    //获得样本类型
    uxutil.server.ajax({
        url: url
    }, function (res) {
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue).list;
                if (data.length > 0) {
                    for (var i in data) {
                        itemPhrase.sampleTypeMap[data[i].LBSampleType_Id] = data[i].LBSampleType_CName;
                    }
                }
            }
        } else {
            layer.msg("样本类型字典查询失败！", { icon: 5, anim: 6 });
        }
    });
};
//获得字典信息
itemPhrase.getDictInfo = function () {
    uxutil.server.ajax({
        url: itemPhrase.url.getEnumTypeUrl + '?classname=ItemPhrase&classnamespace=ZhiFang.Entity.LabStar'
    }, function (res) {
        itemPhrase.info = [];
        if (res.success) {
            if (res.ResultDataValue) {
                var data = JSON.parse(res.ResultDataValue);
                if (data.length > 0) {
                    for (var i in data) {
                        itemPhrase.info.push({ Id: data[i].Id, Name: data[i].Name, Code: data[i].Code, Memo: data[i].Memo });
                    }
                }
                itemPhrase.dynamicRender(itemPhrase.info);//根据字典创建table
            }
        } else {
            layer.msg("项目短语枚举查询失败！", { icon: 5, anim: 6 });
        }
    });
}
//动态创建table
itemPhrase.dynamicRender = function (data) {
    var data = data;
    if (data.length == 0) return;
    var html = "";
    for (var i in data) {
        html += '<span class="layui-badge layui-bg-green marT15" style="display:inline-block;width:100%">' + data[i].Name + '</span><table id="itemPhrase' + i+1 + '" lay-filter="itemPhrase' + i+1 + '"></table>';
    }
    $("#phraseTableBox").html(html);
    for (var j in data) {
        var where = "ObjectID=" + itemPhrase.itemData.LBItem_Id + " and TypeCode='" + data[j].Code +"' and ObjectType=2";
        itemPhrase.itemPhraseTableRender("itemPhrase" + j + 1, where, data[j].Name, data[j].Code);
    }
}
//获得当前列表中最大的显示次序
itemPhrase.getDispOrderForThisTable = function (TableName, DispOrderName) {
    var data = table.cache[TableName];
    var DispOrder = 0;
    for (var i = 0; i < data.length; i++) {
        if (data[i][DispOrderName] > DispOrder) {
            DispOrder = data[i][DispOrderName];
        }
    }
    return Number(DispOrder) + 1;
}
//table初始化
itemPhrase.itemPhraseTableRender = function (tableId, where, TypeName, TypeCode) {
    itemPhrase[tableId] = [];//选中数据
    table.render({
        elem: '#' + tableId,
        height: 160,
        defaultToolbar: [],
        size: 'sm', //小尺寸的表格
        toolbar: '#itemPhraseTableOperation',
        url: itemPhrase.url.getItemPhraseUrl + "&where=" + where,
        cols: [
            [{
                field: 'LBPhrase_Id',
                width: 60,
                title: '主键ID',
                hide: true
            }, {
                field: 'LBPhrase_TypeCode',
                title: '短语类型编码',
                minWidth: 130,
                hide: true
            }, {
                field: 'LBPhrase_TypeName',
                title: '短语类型名称',
                minWidth: 130
            }, {
                field: 'LBPhrase_ObjectType',
                title: '针对类型',
                minWidth: 130,
                templet: function (data) {
                    var str = "";
                    if (data.LBPhrase_ObjectType == 1) {
                        str = "<span  style='color:red'>小组样本</span>";
                    } else if (data.LBPhrase_ObjectType == 2) {
                        str = "<span  style='color:red'>检验项目</span>";
                    }
                    return str;
                }
            }, {
                field: 'LBPhrase_ObjectID',
                title: '对象Id',
                minWidth: 120,
                hide:true
            }, {
                field: 'LBPhrase_SampleTypeID',
                title: '样本类型',
                minWidth: 120,
                templet: function (data) {
                    return itemPhrase.sampleTypeMap[data.LBPhrase_SampleTypeID] || "";
                }
            }, {
                field: 'LBPhrase_CName',
                title: '短语名称',
                minWidth: 100
            }, {
                field: 'LBPhrase_Shortcode',
                title: '快捷码',
                minWidth: 120
            }, {
                field: 'LBPhrase_PinYinZiTou',
                title: '拼音字头',
                minWidth: 160
            }, {
                field: 'LBPhrase_IsUse',
                title: '是否使用',
                minWidth: 160,
                templet: function (data) {
                    var str = "";
                    if (data.LBPhrase_IsUse == "true") {
                        str = "<span style='color:red'>是</span>";
                    } else if (data.LBPhrase_IsUse == "false") {
                        str = "<span>否</span>";
                    }
                    return str;
                }
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
                $("#" + tableId + "+div .layui-table-body table.layui-table tbody tr:first").click();
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
    //监听行单击事件
    table.on('row(' + tableId + ')', function (obj) {
        var data = obj.data;
        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        //暂存选中数据
        if (itemPhrase[tableId].length > 0 && itemPhrase[tableId][0].LBPhrase_Id == data.LBPhrase_Id) return;
        itemPhrase[tableId] = [];//重置
        itemPhrase[tableId].push(data);
    });
    //监听头部工具栏
    table.on('toolbar(' + tableId + ')', function (obj) {
        switch (obj.event) {
            case 'add':
                var flag = false;
                var DisOrder = itemPhrase.getDispOrderForThisTable(tableId, "LBPhrase_DispOrder");
                layer.open({
                    type: 2,
                    title: ['新增' + TypeName],
                    //skin: 'layui-layer-molv',
                    area: ['560px', '340px'],
                    content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/phrase_form/phraseForm.html?ItemId=' + itemPhrase.itemData.LBItem_Id + '&TypeCode=' + TypeCode + "&TypeName=" + TypeName+"&type=add&DisOrder=" + DisOrder,
                    cancel: function (index, layero) {
                        flag = true;
                    },
                    end: function () {
                        if (!flag) {
                            layer.msg("新增成功!", { icon: 6, anim: 0 });
                            table.reload(tableId, {
                                url: itemPhrase.url.getItemPhraseUrl + "&where=" + where,
                                where: {
                                    time: new Date().getTime()
                                }
                            });
                        }
                    }
                });
                break;
            case 'edit':
                if (itemPhrase[tableId].length === 0) {
                    layer.msg('请选择一行！');
                    return;
                }
                var flag = false;
                layer.open({
                    type: 2,
                    title: ['编辑' + TypeName],
                    //skin: 'layui-layer-molv',
                    area: ['560px', '340px'],
                    content: uxutil.path.ROOT + '/ui/layui/app/dic/test_item/phrase_form/phraseForm.html?ItemId=' + itemPhrase.itemData.LBItem_Id + "&Id=" + itemPhrase[tableId][0].LBPhrase_Id + '&TypeCode=' + TypeCode + "&TypeName=" + TypeName+"&type=edit",
                    cancel: function (index, layero) {
                        flag = true;
                    },
                    end: function () {
                        if (!flag) {
                            layer.msg("编辑成功!", { icon: 6, anim: 0 });
                            table.reload(tableId, {
                                url: itemPhrase.url.getItemPhraseUrl + "&where=" + where,
                                where: {
                                    time: new Date().getTime()
                                }
                            });
                        }
                    }
                });
                break;
            case 'del':
                if (itemPhrase[tableId].length === 0) {
                    layer.msg('请选择一行！');
                } else {
                    layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
                        var len = itemPhrase[tableId].length;
                        for (var i = 0; i < itemPhrase[tableId].length; i++) {
                            var Id = itemPhrase[tableId][i].LBPhrase_Id;
                            uxutil.server.ajax({
                                url: itemPhrase.url.delItemPhraseUrl + "?Id=" + Id
                            }, function (res) {
                                if (res.success) {
                                    len--;
                                    if (len == 0) {
                                        layer.close(index);
                                        layer.msg("删除成功！", { icon: 6, anim: 0 });
                                        table.reload(tableId, {
                                            url: itemPhrase.url.getItemPhraseUrl + "&where=" + where,
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
                break;
        };
    });
}
