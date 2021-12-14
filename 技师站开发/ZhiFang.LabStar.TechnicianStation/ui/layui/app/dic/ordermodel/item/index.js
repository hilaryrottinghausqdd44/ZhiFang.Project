layui.extend({
    uxutil: 'ux/util'
}).use(['uxutil', 'table', 'form', 'element'], function () {
    var $ = layui.jquery,
        layer = layui.layer,
        form = layui.form,
        table = layui.table,
        uxutil = layui.uxutil;

    var app = {};
    //服务地址
    app.url = {
        //获得左侧列表数据
        getLeftTableDataUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBOrderModelItemByHQL?isPlanish=true&sort=[{"property": "LBOrderModelItem_DispOrder","direction": "ASC"}]',
        //获取右侧列表数据
        getRightTableDataUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]',
        //新增医嘱项目服务
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBOrderModelItem',
        //删除医嘱项目服务
        delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBOrderModelItem',
    };
    //是否执行左侧列表中的done中部分内容
    app.leftTableflag = true;
    //定位Id
    app.position = {
        left: null,
        right:null
    };
    //常用字段
    app.fields = {
        Id: "LBOrderModelItem_Id",
        LeftTableId: 'LBOrderModelItem_ItemID',
        RightTableId: 'LBItem_Id'
    };
    //左侧列表查询列
    app.leftTableColumn = [
        'LBOrderModelItem_Id',
        'LBOrderModelItem_ItemID',
        'LBOrderModelItem_ItemName'
    ];
    //右侧列表查询列
    app.rightTableColumn = [
        'LBItem_Id',
        'LBItem_CName',
        'LBItem_SName',
        'LBItem_UseCode'
    ];
    //左侧列表初始数据
    app.leftTableInitData = [];
    //左侧列表全部数据 -- 项目Id
    app.leftTableData = [];
    //get参数
    app.paramsObj = {
        OrderModelID: null,
        OrderModelName: null
    };
    //初始化
    app.init = function () {
        var me = this;
        $(".fiexdHeight").css("height", ($(window).height() - 55) + "px");//设置中间容器高度
        me.getParams();
        $("#CName").html(me.paramsObj.OrderModelName);
        me.initLeftTable();
        me.listeners();
    };
    //获得参数
    app.getParams = function () {
        var me = this,
            params = location.search.split("?")[1].split("&");
        //参数赋值
        for (var j in me.paramsObj) {
            for (var i = 0; i < params.length; i++) {
                if (j.toUpperCase() == params[i].split("=")[0].toUpperCase()) {
                    me.paramsObj[j] = decodeURIComponent(params[i].split("=")[1]);
                }
            }
        }
    };
    //初始化左侧列表
    app.initLeftTable = function() {
        var me = this,
            url = me.url.getLeftTableDataUrl + "&fields=" + me.leftTableColumn.join() + "&where=OrderModelID=" + me.paramsObj.OrderModelID + "&t=" + new Date().getTime();
        me.leftTableflag = true;
        table.render({
            elem: '#leftTable',
            height: 'full-110',
            defaultToolbar: ['filter'],
            size: 'sm', 
            //data: data,
            //toolbar: '#toolbarComboItem',
            url: url,
            cols: [
                [{
                    type: 'checkbox'
                }, {
                    field: app.fields.Id,
                    width: 60,
                    title: '质控项目主键ID',
                    sort: false,
                    hide: true
                }, {
                    field: app.fields.LeftTableId,
                    title: '项目编号',
                    width: 130,
                    hide: true,
                    sort: false
                }, {
                    field: 'LBOrderModelItem_ItemName',
                    title: '项目名称',
                    minWidth: 130,
                    sort: false
                }]
            ],
            limit: 99999,
            autoSort: true, //禁用前端自动排序
            done: function (res, curr, count) {
                if (app.leftTableflag) {
                    app.leftTableflag = false;
                    app.leftTableInitData = [];
                    app.leftTableData = [];
                    if (res.data.length > 0) {
                        for (var i in res.data) {
                            app.leftTableInitData.push(res.data[i]);
                            app.leftTableData.push(res.data[i][app.fields.LeftTableId]);
                        }
                    }
                    app.initRightTable();
                }
                //定位
                if (app.position.left) {
                    var flag = false;
                    var index = null;
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i][app.fields.LeftTableId] == app.position.left) {
                            flag = true;
                            index = i + 1;
                        }
                    }
                    if (flag) {
                        $("#leftTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").addClass('layui-table-click').siblings().removeClass('layui-table-click');
                        if (document.querySelector("#leftTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")"))
                            document.querySelector("#leftTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                    }
                    app.position.left = null;
                } else {
                    $("#leftTable+div .layui-table-body table.layui-table tbody tr:first-child").addClass('layui-table-click').siblings().removeClass('layui-table-click');//选中第一条
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
    };
    //初始化右侧列表
    app.initRightTable = function (url,options) {
        var me = this,
            options = options || {},//列表配置
            url = url || me.url.getRightTableDataUrl + "&fields=" + me.rightTableColumn.join() + "&where=IsUse=1 and IsOrderItem=1" + (me.leftTableData.length > 0 ? " and Id not in(" + me.leftTableData.join() + ")" : "") + "&t=" + new Date().getTime();
        var config = {
            elem: '#rightTable',
            height: 'full-110',
            defaultToolbar: ['filter'],
            size: 'sm', //小尺寸的表格
            //data: data,
            //toolbar: '#toolbarComboItem',
            url: url,
            cols: [
                [{
                    type: 'checkbox'
                }, {
                    field: app.fields.RightTableId,
                    width: 60,
                    title: '主键ID',
                    sort: false,
                    hide: true
                }, {
                    field: 'LBItem_CName',
                    title: '项目名称',
                    minWidth: 130,
                    sort: false
                }, {
                    field: 'LBSectionItemVO_LBItem_SName',
                    title: '项目简称',
                    width: 100,
                    sort: false
                }, {
                    field: 'LBItem_UseCode',
                    title: '用户编码',
                    width: 100,
                    sort: false
                }]
            ],
            autoSort: false, //禁用前端自动排序
            page: true,
            limit: 100,
            limits: [50, 100, 200, 500, 1000],
            done: function (res, curr, count) {
                //定位
                if (app.position.right) {
                    var flag = false;
                    var index = null;
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i][app.fields.RightTableId] == app.position.right) {
                            flag = true;
                            index = i + 1;
                        }
                    }
                    if (flag) {
                        $("#rightTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").addClass('layui-table-click').siblings().removeClass('layui-table-click');
                        if (document.querySelector("#rightTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")"))
                            document.querySelector("#rightTable+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                    }
                    app.position.right = null;
                } else {
                    $("#rightTable+div .layui-table-body table.layui-table tbody tr:first-child").addClass('layui-table-click').siblings().removeClass('layui-table-click');//选中第一条
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
        };
        config = $.extend({}, config, options);
        table.render(config);
    };
    //监听事件
    app.listeners = function () {
        var me = this;
        //加入按钮操作
        $("#add").click(function () {
            //获得选中的数据
            var checkData = table.checkStatus('rightTable').data;
            me.onAddClick(checkData);
        });
        //移除按钮操作
        $("#remove").click(function () {
            var checkData = table.checkStatus('leftTable').data;
            me.onRemoveClick(checkData);
        });
        //重置按钮操作
        $("#reset").click(function () {
            layer.confirm('是否确认进行重置操作?', { icon: 3, title: '提示' }, function (index) {
                me.position.left = null;
                me.position.right = null;
                me.initLeftTable();
                layer.close(index);
            });
        });
        //保存按钮操作
        $("#save").click(function () {
            me.onSaveClick();
        });
        //监听未加入项目表格查询
        form.on('submit(search)', function (data) {
            var url = me.url.getRightTableDataUrl + "&fields=" + me.rightTableColumn.join();
            var where = "IsUse=1 and IsOrderItem=1" + (me.leftTableData.length > 0 ? " and Id not in(" + me.leftTableData.join() + ")" : "");
            if (data.field.searchText != "") {//模糊查询
                var str = data.field.searchText;
                where += " and (CName like '%" + str + "%' or EName like '%" + str + "%' or SName like '%" + str + "%' or Shortcode like '%" + str + "%' or UseCode like '%" + str + "%')";
            }
            url += "&where=" + where + "&t=" + new Date().getTime();
            me.initRightTable(encodeURI(url));
            $("#searchText").val(data.field.searchText);
        });
        //监听右侧列表排序事件
        table.on('sort(rightTable)', function (obj) {
            var field = obj.field,//排序字段
                type = obj.type,//升序还是降序
                searchText = $("#searchText").val(),
                url = me.url.getRightTableDataUrl + "&fields=" + me.rightTableColumn.join();
            if (type == null) return;
            url += "&where=IsUse=1 and IsOrderItem=1" + (me.leftTableData.length > 0 ? " and Id not in(" + me.leftTableData.join() + ")" : "");

            if (searchText != "") {//模糊查询
                var str = searchText;
                url += " and (LBItem.CName like '%" + str + "%' or LBItem.EName like '%" + str + "%' or LBItem.SName like '%" + str + "%' or LBItem.Shortcode like '%" + str + "%' or LBItem.UseCode like '%" + str + "%')";
            }
            if (url.indexOf("sort") != -1) {//存在
                var start = url.indexOf("sort=[");
                var end = url.indexOf("]") + 1;
                var oldStr = url.slice(start, end);
                var newStr = 'sort=[{property:"' + field + '",direction:"' + type + '"}]';
                url = url.replace(oldStr, newStr);
            } else {
                url = url + '&sort=[{property:"' + field + '",direction:"' + type + '"}]';
            }
            url += "&t=" + new Date().getTime();
            me.initRightTable(encodeURI(url), { initSort: obj });
        });
        //监听左侧列表行双击事件
        table.on('rowDouble(leftTable)', function (obj) {
            me.onRemoveClick([obj.data]);
        });
        //监听右侧列表行双击事件
        table.on('rowDouble(rightTable)', function (obj) {
            me.onAddClick([obj.data]);
        });
        //监听左侧列表行单击事件
        table.on('row(leftTable)', function (obj) {
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');//标注选中样式
        });
        //监听右侧列表行单击事件
        table.on('row(rightTable)', function (obj) {
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');//标注选中样式
        });
    };
    //加入数据
    app.onAddClick = function (checkData) {
        var me = this,
            leftTableDataCache = table.cache.leftTable || [],//左侧列表所有数据
            checkData = checkData || [],//选中待加入数据
            addData = [];//确认要加入的数据
        if (checkData.length == 0) {
            layer.msg("未选择数据！");
            return;
        }
        //左侧列表处理 -- 去重加入
        for (var i = 0; i < checkData.length; i++) {
            //判断是否存在
            var flag = (me.leftTableData.length == 0 || me.leftTableData.join(",").indexOf(checkData[i][app.fields.RightTableId]) == -1) ? true : false;
            if (flag) {
                var Obj = {
                    'LBOrderModelItem_ItemID': checkData[i][app.fields.RightTableId],
                    'LBOrderModelItem_ItemName': checkData[i].LBItem_CName,
                    'LBOrderModelItem_Id': checkData[i].LBOrderModelItem_Id
                };
                leftTableDataCache.push(Obj);//加入到左侧列表缓存数据中
                me.leftTableData.push(checkData[i][app.fields.RightTableId]);//加入到左侧全部列表数据中
                addData.push(checkData[i][app.fields.RightTableId]);//加入到确认要加入的数据
                me.position.left = checkData[i][me.fields.RightTableId];//左列表刚加入时定位
            }
        }
        //更新新左侧列表
        table.reload('leftTable', {
            url: '',
            data: leftTableDataCache
        });
        var rightTableCacheData = table.cache.rightTable || [];
        //右侧列表处理
        for (var a = 0; a < addData.length; a++) {
            for (var b = rightTableCacheData.length - 1; b >= 0; b--) {
                if (addData[a] == rightTableCacheData[b][app.fields.RightTableId]) {
                    rightTableCacheData.splice(b, 1);
                    me.position.right = rightTableCacheData[b - 1] ? rightTableCacheData[b - 1][me.fields.RightTableId] : null;//右列表刚加入时定位
                    break;
                }
            }
        }
        $("#search").click();
        //var searchText = $("#searchText").val();
        //table.reload('rightTable', {
        //    url: '',
        //    data: rightTableCacheData
        //});
        //$("#searchText").val(searchText);
    };
    //移除数据
    app.onRemoveClick = function (checkData) {
        var me = this,
            leftTableCacheData = table.cache.leftTable || [],//左侧列表数据
            checkData = checkData || [];//获得选中的数据
        if (checkData.length == 0) {
            layer.msg("未选择数据！");
            return;
        }
        //左侧列表处理
        for (var j = 0; j < checkData.length; j++) {
            for (var i = leftTableCacheData.length - 1; i >= 0; i--) {
                if (leftTableCacheData[i][me.fields.LeftTableId] == checkData[j][me.fields.LeftTableId]) {
                    leftTableCacheData.splice(i, 1);
                    me.leftTableData.splice(me.leftTableData.indexOf(checkData[j][me.fields.LeftTableId]), 1);
                    me.position.left = leftTableCacheData[i - 1] ? leftTableCacheData[i - 1][me.fields.LeftTableId] : null;//左列表刚加入时定位
                    me.position.right = checkData[j][me.fields.LeftTableId];//右列表刚加入时定位
                    break;
                }
            }
        }
        //更新左侧列表
        table.reload('leftTable', {
            url: '',
            data: leftTableCacheData
        });
        //更新右侧列表
        $("#search").click();
    };
    //保存数据
    app.onSaveClick = function () {
        var me = this;
        var oldData = JSON.parse(JSON.stringify(me.leftTableInitData));
        var newData = JSON.parse(JSON.stringify(me.leftTableData));
        //传递信息
        var addEntityList = [];//新增的子项 array
        var delIDList = [];//删除的子项 string
        //计算新增和删除的数据
        for (var i = 0; i < newData.length; i++) {
            var addFlag = true;//是新增的
            for (var j = oldData.length - 1; j >= 0; j--) {
                if (newData[i] == oldData[j][app.fields.LeftTableId]) {//存在，是初始的数据
                    addFlag = false;
                    oldData.splice(j, 1);//删除掉还存在的数据 剩下的就是删除掉的数据
                    break;
                }
            }
            if (addFlag) {
                var DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
                addEntityList.push({
                    OrderModelID: me.paramsObj.OrderModelID,
                    ItemID: newData[i],
                    IsUse: true
                });
            }
        }
        //赋值删除的数据
        for (var a = 0; a < oldData.length; a++) {
            delIDList.push(oldData[a][app.fields.Id]);
        }
        if (delIDList.length == 0 && addEntityList.length == 0) {
            layer.msg("数据未更新，无需保存!");
            return;
        }
        var load = layer.load();
        //删除医嘱项目
        me.onDelOrderModelItem(delIDList, function (msg) {
            //新增医嘱项目
            me.onAddOrderModelItem(addEntityList, msg, function (msg) {
                layer.close(load);
                parent.layer.closeAll();
                parent.layer.msg(msg.join("<br>"), { icon: 6, anim: 0, time: 3000 });
            });
        });
        

        //var configs = {
        //    type: "POST",
        //    url: me.url.saveUrl,
        //    data: JSON.stringify({ addEntityList: addEntityList, delIDList: delIDList.join() })
        //};
        //var loadIndex = layer.load();
        //uxutil.server.ajax(configs, function (res) {
        //    //隐藏遮罩层
        //    layer.close(loadIndex);
        //    if (res.success) {
        //        parent.layer.closeAll();
        //    } else {
        //        layer.msg("保存失败!", { icon: 5, anim: 6 });
        //    }
        //});
    };
    //删除医嘱模板项目
    app.onDelOrderModelItem = function (delIDList,callback) {
        var me = this,
            ItemList = delIDList,
            length = ItemList.length,
            successCount = 0,
            errorCount = 0,
            msg = [],
            url = me.url.delUrl;

        if (length > 0) {
            $.each(ItemList, function (i, item) {
                setTimeout(function () {
                    uxutil.server.ajax({
                        url: url + "?id=" + item
                    }, function (data) {
                        if (data.success === true) {
                            successCount++;
                        } else {
                            errorCount++;
                        }
                        if (errorCount + successCount == length) {
                            msg.push("删除：成功个数:" + successCount + "，失败个数:" + errorCount);
                            callback(msg);
                        }
                    });
                }, i * 100);

            });
        } else {
            callback(msg);
        }
    };
    //新增医嘱模板项目
    app.onAddOrderModelItem = function (addEntityList, msg, callback) {
        var me = this,
            addEntityList = addEntityList,
            length = addEntityList.length,
            successCount = 0,
            errorCount = 0,
            msg = msg || [],
            url = me.url.addUrl;

        if (length > 0) {
            $.each(addEntityList, function (i, item) {
                setTimeout(function () {
                    var config = {
                        type: "POST",
                        url: url,
                        data: JSON.stringify({ entity: item })
                    };
                    uxutil.server.ajax(config, function (data) {
                        if (data.success === true) {
                            successCount++;
                        } else {
                            errorCount++;
                        }
                        if (errorCount + successCount == length) {
                            msg.push("新增：成功个数:" + successCount + "，失败个数:" + errorCount);
                            callback(msg);
                        }
                    });
                }, i * 100);

            });
        } else {
            callback(msg);
        }
    };
    //初始化调用入口
    app.init();
});