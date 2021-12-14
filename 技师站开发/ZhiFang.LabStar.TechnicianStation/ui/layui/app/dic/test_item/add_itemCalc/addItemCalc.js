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
        //获得计算项目
        getCalcLBItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]',
        //获取全部项目
        getItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]',
        //获得小组
        getSectionUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true&where=IsUse=true',
        //保存服务
        saveUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddDelLBItemCalc'
    };
    //是否执行左侧列表中的done中部分内容
    app.leftTableflag = true;
    //定位Id
    app.position = {
        left: null,
        right: null
    };
    //常用字段
    app.fields = {
        Id: "LBItemCalc_Id",
        ItemId: 'LBItemCalc_LBItem_Id'
    };
    //左侧列表查询列
    app.leftTableColumn = [
        'LBItemCalc_Id',//计算内项目表主键Id
        'LBItemCalc_LBCalcItem_Id',//计算项目Id
        'LBItemCalc_LBItem_Id',//参与计算项目Id
        'LBItemCalc_LBItem_CName',//参与计算项目名称
        'LBItemCalc_LBItem_EName',//参与计算项目英文名
        'LBItemCalc_LBItem_SName',//参与计算项目简称
        'LBItemCalc_LBItem_Shortcode',//参与计算项目快捷码
        'LBItemCalc_LBItem_PinYinZiTou',//参与计算项目拼音字头
        'LBItemCalc_LBItem_GroupType',//参与计算项目组合类型
        'LBItemCalc_LBItem_DispOrder'//参与计算项目显示次序
    ];
    //左侧列表初始数据
    app.leftTableInitData = [];
    //左侧列表全部数据 -- 项目Id
    app.leftTableData = [];
    //初始化
    app.init = function () {
        var me = this;
        $(".fiexdHeight").css("height", ($(window).height() - 55) + "px");//设置中间容器高度
        me.getParams();
        $("#CName").html(me.paramsObj.CName);
        me.getServerData();
        me.initLeftTable();
        me.listeners();
    };
    //get参数
    app.paramsObj = {
        CalcItemID: null,//计算项目ID
        GroupType: null,//组合类型
        CName: null,//名称
        GroupTypeWhere: "(0)"//组合类型条件
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
    //获得小组
    app.getServerData = function () {
        var me = this;
        uxutil.server.ajax({
            url: me.url.getSectionUrl
        }, function (res) {
            if (res.success) {
                var html = "<option value=''>请选择</option><option value='-1'>不属于任何小组</option>";
                var data = (res.value && res.value.list.length > 0) ? res.value.list : [];
                for (var i in data) {
                    html += '<option value="' + data[i].LBSection_Id + '">' + data[i].LBSection_CName + '</option>';
                }
                $("#SectionID").html(html);
                form.render('select');
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //初始化左侧列表
    app.initLeftTable = function() {
        var me = this,
            url = me.url.getCalcLBItemUrl + "&fields=" + me.leftTableColumn.join() + "&where=CalcItemID=" + me.paramsObj.CalcItemID + "&t=" + new Date().getTime();
        table.render({
            elem: '#table',
            height: 'full-110',
            defaultToolbar: ['filter'],
            size: 'sm', 
            //data: data,
            //toolbar: '#toolbarComboItem',
            url: url,
            cols: [
                [{
                    type: 'checkbox',
                    fixed: 'left'
                }, {
                    field: app.fields.Id,
                    width: 60,
                    title: '主键ID',
                    sort: false,
                    hide: true
                }, {
                    field: 'LBItemCalc_LBCalcItem_Id',
                    title: '计算项目Id',
                    width: 130,
                    hide: true,
                    sort: false
                }, {
                    field: app.fields.ItemId,
                    title: '参加计算项目Id',
                    width: 130,
                    hide: true,
                    sort: false
                }, {
                    field: 'LBItemCalc_LBItem_CName',
                    title: '项目名称',
                    minWidth: 130,
                    sort: false
                }, {
                    field: 'LBItemCalc_LBItem_EName',
                    title: '英文名称',
                    width: 130,
                    sort: false
                }, {
                    field: 'LBItemCalc_LBItem_SName',
                    title: '简称',
                    width: 100,
                    sort: false
                }, {
                    field: 'LBItemCalc_LBItem_GroupType',
                    title: '组合类型',
                    width: 100,
                    sort: false,
                    templet: function (data) {
                        var str = "";
                        switch (data.LBItemCalc_LBItem_GroupType) {
                            case "0":
                                str = "单项";
                                break;
                            case "1":
                                str = "组合";
                                break;
                            case "2":
                                str = "组套";
                                break;
                        }
                        return str;
                    }
                }, {
                    field: 'LBItemCalc_LBItem_Shortcode',
                    title: '项目快捷码',
                    width: 100,
                    sort: false
                }, {
                    field: 'LBItemCalc_LBItem_PinYinZiTou',
                    title: '项目拼音字头',
                    width: 100,
                    sort: false
                }, {
                    field: 'LBItem_DataTimeStamp',
                    title: '时间戳',
                    width: 120,
                    hide: true,
                    sort: false
                }
                ]
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
                            app.leftTableData.push(res.data[i][app.fields.ItemId]);
                        }
                    }
                    app.initRightTable();
                }
                //定位
                if (app.position.left) {
                    var flag = false;
                    var index = null;
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i][app.fields.ItemId] == app.position.left) {
                            flag = true;
                            index = i + 1;
                        }
                    }
                    if (flag) {
                        $("#table+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").addClass('layui-table-click').siblings().removeClass('layui-table-click');
                        if (document.querySelector("#table+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")"))
                            document.querySelector("#table+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                    }
                    app.position.left = null;
                } else {
                    $("#table+div .layui-table-body table.layui-table tbody tr:first-child").addClass('layui-table-click').siblings().removeClass('layui-table-click');//选中第一条
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
            url = url || me.url.getItemUrl + "&where=IsUse=1 and GroupType in " + me.paramsObj.GroupTypeWhere + (me.leftTableData.length > 0 ? " and Id not in(" + me.leftTableData.join() + ")" : "") + "&t=" + new Date().getTime();
        var config = {
            elem: '#tableAll',
            height: 'full-110',
            defaultToolbar: ['filter'],
            size: 'sm', //小尺寸的表格
            //data: data,
            //toolbar: '#toolbarComboItem',
            url: url,
            cols: [
                [{
                    type: 'checkbox',
                    fixed: 'left'
                }, {
                    field: 'LBItem_Id',
                    width: 60,
                    title: '主键ID',
                    sort: true,
                    hide: true
                }, {
                    field: 'LBItem_CName',
                    title: '项目名称',
                    minWidth: 130,
                    sort: true
                }, {
                    field: 'LBItem_EName',
                    title: '英文名称',
                    width: 130,
                    sort: true
                }, {
                    field: 'LBItem_SName',
                    title: '简称',
                    width: 100,
                    sort: true
                }, {
                    field: 'LBItem_GroupType',
                    title: '组合类型',
                    width: 100,
                    sort: true,
                    templet: function (data) {
                        var str = "";
                        switch (data.LBItem_GroupType) {
                            case "0":
                                str = "单项";
                                break;
                            case "1":
                                str = "组合";
                                break;
                            case "2":
                                str = "套餐";
                                break;
                        }
                        return str;
                    }
                }, {
                    field: 'LBItem_Shortcode',
                    title: '快捷码',
                    width: 100,
                    sort: true
                }, {
                    field: 'LBItem_PinYinZiTou',
                    title: '拼音字头',
                    width: 100,
                    sort: true
                }, {
                    field: 'LBItem_DataTimeStamp',
                    title: '时间戳',
                    width: 120,
                    hide: true,
                    sort: true
                }
                ]
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
                        if (res.data[i]["LBItem_Id"] == app.position.right) {
                            flag = true;
                            index = i + 1;
                        }
                    }
                    if (flag) {
                        $("#tableAll+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").addClass('layui-table-click').siblings().removeClass('layui-table-click');
                        if (document.querySelector("#tableAll+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")"))
                            document.querySelector("#tableAll+div .layui-table-body table.layui-table tbody tr:nth-child(" + index + ")").scrollIntoView(false, { behavior: "smooth" });
                    }
                    app.position.right = null;
                } else {
                    $("#tableAll+div .layui-table-body table.layui-table tbody tr:first-child").addClass('layui-table-click').siblings().removeClass('layui-table-click');//选中第一条
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
        $("#addItem").click(function () {
            //获得选中的数据
            var checkData = table.checkStatus('tableAll').data;
            me.onAddClick(checkData);
        });
        //移除按钮操作
        $("#removeItem").click(function () {
            var checkData = table.checkStatus('table').data;
            me.onRemoveClick(checkData);
        });
        //重置按钮操作
        $("#resetItem").click(function () {
            layer.confirm('是否确认进行重置操作?', { icon: 3, title: '提示' }, function (index) {
                me.position.left = null;
                me.position.right = null;
                me.initLeftTable();
                layer.close(index);
            });
        });
        //保存按钮操作
        $("#saveItem").click(function () {
            me.onSaveClick();
        });
        //监听未加入项目表格查询
        form.on('submit(searchAll)', function (data) {
            var url = me.url.getItemUrl;
            if (data.field.SectionID != "") {//小组
                url += '&sectionID=' + data.field.SectionID;
            }
            var where = "IsUse=1 and GroupType in " + me.paramsObj.GroupTypeWhere + (me.leftTableData.length > 0 ? " and Id not in(" + me.leftTableData.join() + ")" : "");
            if (data.field.nameOrotherName != "") {//模糊查询
                var str = data.field.nameOrotherName;
                where += " and (CName like '%" + str + "%' or EName like '%" + str + "%' or SName like '%" + str + "%' or Shortcode like '%" + str + "%' or PinYinZiTou like '%" + str + "%')";
            }
            url += "&where=" + where + "&t=" + new Date().getTime();
            me.initRightTable(url);
            $("#nameOrotherName").val(data.field.nameOrotherName);
            $("#SectionID").val(data.field.SectionID);
        });
        //监听右侧列表排序事件
        table.on('sort(tableAll)', function (obj) {
            var field = obj.field,//排序字段
                type = obj.type,//升序还是降序
                searchText = $("#nameOrotherName").val(),
                SectionID = $("#SectionID").val(),
                url = me.url.getItemUrl;
            url += "&where=IsUse=1 and GroupType in " + me.paramsObj.GroupTypeWhere + (me.leftTableData.length > 0 ? " and Id not in(" + me.leftTableData.join() + ")" : "");
            if (type == null) return;
            if (searchText != "") {//模糊查询
                var str = searchText;
                url += " and (CName like '%" + str + "%' or EName like '%" + str + "%' or SName like '%" + str + "%' or Shortcode like '%" + str + "%' or PinYinZiTou like '%" + str + "%')";
            }
            if (SectionID) {
                url += '&sectionID=' + SectionID;
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
            me.initRightTable(url, { initSort: obj });
        });
        //监听左侧列表行双击事件
        table.on('rowDouble(table)', function (obj) {
            me.onRemoveClick([obj.data]);
        });
        //监听右侧列表行双击事件
        table.on('rowDouble(tableAll)', function (obj) {
            me.onAddClick([obj.data]);
        });
        //监听左侧列表行单击事件
        table.on('row(table)', function (obj) {
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');//标注选中样式
        });
        //监听右侧列表行单击事件
        table.on('row(tableAll)', function (obj) {
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');//标注选中样式
        });
    };
    //加入数据
    app.onAddClick = function (checkData) {
        var me = this,
            leftTableDataCache = table.cache.table || [],//左侧列表所有数据
            checkData = checkData || [],//选中待加入数据
            addData = [];//确认要加入的数据
        if (checkData.length == 0) {
            layer.msg("未选择数据！");
            return;
        }
        //左侧列表处理 -- 去重加入
        for (var i = 0; i < checkData.length; i++) {
            //判断是否存在
            var flag = (me.leftTableData.length == 0 || me.leftTableData.join(",").indexOf(checkData[i].LBItem_Id) == -1) ? true : false;
            if (flag) {
                var Obj = {
                    'LBItemCalc_LBCalcItem_Id': me.paramsObj.CalcItemID,
                    'LBItemCalc_LBItem_Id': checkData[i].LBItem_Id,
                    'LBItemCalc_LBItem_CName': checkData[i].LBItem_CName,
                    'LBItemCalc_LBItem_EName': checkData[i].LBItem_EName,
                    'LBItemCalc_LBItem_SName': checkData[i].LBItem_SName,
                    'LBItemCalc_LBItem_Shortcode': checkData[i].LBItem_Shortcode,
                    'LBItemCalc_LBItem_PinYinZiTou': checkData[i].LBItem_PinYinZiTou,
                    'LBItemCalc_LBItem_GroupType': checkData[i].LBItem_GroupType,
                    'LBItem_DataTimeStamp': checkData[i].LBItem_DataTimeStamp
                };
                leftTableDataCache.push(Obj);//加入到左侧列表缓存数据中
                me.leftTableData.push(checkData[i].LBItem_Id);//加入到左侧全部列表数据中
                addData.push(checkData[i].LBItem_Id);//加入到确认要加入的数据
                me.position.left = checkData[i]["LBItem_Id"];//左列表刚加入时定位
            }
        }
        //更新新左侧列表
        table.reload('table', {
            url: '',
            data: leftTableDataCache
        });
        var rightTableCacheData = table.cache.tableAll || [];
        //右侧列表处理
        for (var a = 0; a < addData.length; a++) {
            for (var b = rightTableCacheData.length - 1; b >= 0; b--) {
                if (addData[a] == rightTableCacheData[b].LBItem_Id) {
                    rightTableCacheData.splice(b, 1);
                    me.position.right = rightTableCacheData[b - 1] ? rightTableCacheData[b - 1]["LBItem_Id"] : null;//右列表刚加入时定位
                    break;
                }
            }
        }
        var nameOrotherName = $("#nameOrotherName").val();
        var SectionID = $("#SectionID").val();
        table.reload('tableAll', {
            url: '',
            data: rightTableCacheData
        });
        $("#nameOrotherName").val(nameOrotherName);
        $("#SectionID").val(SectionID);
    };
    //移除数据
    app.onRemoveClick = function (checkData) {
        var me = this,
            leftTableCacheData = table.cache.table || [],//左侧列表数据
            checkData = checkData || [];//获得选中的数据
        if (checkData.length == 0) {
            layer.msg("未选择数据！");
            return;
        }
        //左侧列表处理
        for (var j = 0; j < checkData.length; j++) {
            for (var i = leftTableCacheData.length - 1; i >= 0; i--) {
                if (leftTableCacheData[i][me.fields.ItemId] == checkData[j][me.fields.ItemId]) {
                    leftTableCacheData.splice(i, 1);
                    me.leftTableData.splice(me.leftTableData.indexOf(checkData[j][me.fields.ItemId]), 1);
                    me.position.left = leftTableCacheData[i - 1] ? leftTableCacheData[i - 1][me.fields.ItemId] : null;//左列表刚加入时定位
                    me.position.right = checkData[j][me.fields.ItemId];//右列表刚加入时定位
                    break;
                }
            }
        }
        //更新左侧列表
        table.reload('table', {
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
                if (newData[i] == oldData[j][app.fields.ItemId]) {//存在，是初始的数据
                    addFlag = false;
                    oldData.splice(j, 1);//删除掉还存在的数据 剩下的就是删除掉的数据
                    break;
                }
            }
            if (addFlag) {
                var DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
                addEntityList.push({
                    LBCalcItem: { Id: me.paramsObj.CalcItemID, DataTimeStamp: DataTimeStamp },
                    LBItem: { Id: newData[i], DataTimeStamp: DataTimeStamp }
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
        var configs = {
            type: "POST",
            url: me.url.saveUrl,
            data: JSON.stringify({ addEntityList: addEntityList, delIDList: delIDList.join() })
        };
        var loadIndex = layer.load();
        uxutil.server.ajax(configs, function (res) {
            //隐藏遮罩层
            layer.close(loadIndex);
            if (res.success) {
                parent.layer.closeAll();
            } else {
                layer.msg("保存失败!", { icon: 5, anim: 6 });
            }
        });
    };
    //初始化调用入口
    app.init();
});