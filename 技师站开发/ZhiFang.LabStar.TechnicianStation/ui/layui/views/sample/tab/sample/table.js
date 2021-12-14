/**
	@name：检验结果项目列表
	@author：zhangda
	@version 2021-04-23
 */
layui.extend({
}).define(['table', 'form', 'element', 'uxutil','uxbase'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        uxbase = layui.uxbase,
        table = layui.table,
        form = layui.form,
        element = layui.element,
        uxtable = layui.uxtable;
    
    var app = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
		config: {
            elem: '',
            id: "",
            //配置列Code
            GridCode: null,
            //配置列内容
            Cols: null,
            checkRowData: [],
            //检验单结果列表默认查询字段
            SampleResultTableCols: [
                "LisTestItem_PLBItem_Id", "LisTestItem_PLBItem_CName", "LisTestItem_LisOrderItem_HisItemName", "LisTestItem_LBItem_Id", "LisTestItem_LBItem_CName","LisTestItem_LBItem_SName",
                "LisTestItem_LBItem_EName", "LisTestItem_LBItem_Prec", "LisTestItem_ReportValue", "LisTestItem_QuanValue", "LisTestItem_OriglValue",
                "LisTestItem_ResultStatus", "LisTestItem_ResultStatusCode", "LisTestItem_Unit", "LisTestItem_RefRange", "LisTestItem_PreValue", "LisTestItem_PreValueComp",
                "LisTestItem_PreCompStatus", "LisTestItem_PreValue2", "LisTestItem_PreValue3", "LisTestItem_ResultComment", "LisTestItem_RedoStatus", "LisTestItem_RedoValues",
                "LisTestItem_RedoDesc", "LisTestItem_Id", "LisTestItem_LisOrderItem_Id", "LisTestItem_PLBItem_DispOrder", "LisTestItem_LBItem_DispOrder","LisTestItem_LisTestForm_Id"
            ],
            /**默认传入参数*/
            defaultParams: { "testFormRecord": null, isReadOnly: false, sectionID: null,sectionCName:null },
            /**默认配置*/
            defaultConfig: {//showType: 1:所有项目显示 2：组合项目显示 //total:全部数据 //groupData：根据组合项目分组,//type:用于判断是否新增
                showType: 1, total: [], groupData: {}, type: 'show',
                //结果状态类型
                ResultStatusDictType: 'ResultStatus',
                //结果状态信息
                ResultStatusList: [],
            },
            /**默认数据条件*/
            defaultWhere: '',
            /**内部数据条件*/
            internalWhere: '',
            /**外部数据条件*/
            externalWhere: '',
            /**是否默认加载*/
            defaultLoad: false,
            /**列表当前排序*/
            sort: [{
				"property": 'LisTestItem_PLBItem_DispOrder',
                "direction": 'ASC'
            },{
				"property": 'LisTestItem_LBItem_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
            //editUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_EditBatchLisTestItemResult',
            delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestItem',
            getResultStatusColorUrl: '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDictByHQL?isPlanish=true&sort=[{property:"LBDict_DispOrder",direction:"ASC"}]&fields=LBDict_Id,LBDict_CName,LBDict_DictCode,LBDict_EName,LBDict_SName,LBDict_IsUse,LBDict_DispOrder,LBDict_ColorValue,LBDict_ColorDefault',
            //获得小组默认项目
            getSectionDefaultItemsUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionDefultSingleItemVO',
            //新增默认项目保存服务
            onAddSaveUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItem',
            where: "",
            toolbar: '',
            page: false,
            limit: 10000,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
			cols: [[]],
            text: { none: '暂无相关数据' },
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {};
                //结果说明 换行符替换为|
                if (data.list.length > 0) {
                    $.each(data.list, function (i, item) {
                        if (item["LisTestItem_ResultComment"].indexOf("\n") != -1)
                            data.list[i]["LisTestItem_ResultComment"] = item["LisTestItem_ResultComment"].replace(/\n/g, "\|");
                    });
                }
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
				layui.event('bottomToolBar', "refreshbottomItemTotal", count);
                if (count == 0) {
                    app.config.checkRowData = [];
                    return;
                }
                setTimeout(function () {
                    if ($("#SampleResultTable+div .layui-table-body table.layui-table tbody tr:first-child")[0])
                        $("#SampleResultTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
                }, 0);
            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //获取查询Url
    app.getLoadUrl = function () {
        var me = this, arr = [];
        var url = me.config.selectUrl;
        //查询字段
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.arrConcatNoRepeat(me.config.SampleResultTableCols, me.getStoreFields(true)).join(',');
        //排序
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += (url.indexOf('?') == -1 ? '?' : '&') + 'sort=' + JSON.stringify(defaultOrderBy);
        //默认条件
        if (me.config.defaultWhere && me.config.defaultWhere != '') {
            arr.push(me.config.defaultWhere);
        }
        //内部条件
        if (me.config.internalWhere && me.config.internalWhere != '') {
            arr.push(me.config.internalWhere);
        }
        //外部条件
        if (me.config.externalWhere && me.config.externalWhere != '') {
            arr.push(me.config.externalWhere);
        }
        //传入的默认参数处理
        if (me.config.defaultParams && me.config.defaultParams["testFormRecord"] && me.config.defaultParams["testFormRecord"]["LisTestForm_Id"]) {
            arr.push('listestitem.MainStatusID in (0,-1)');
            arr.push('listestitem.LisTestForm.Id=' + me.config.defaultParams["testFormRecord"]["LisTestForm_Id"]);
            arr.push('listestitem.LBItem.SpecialType <> 2');
        } else {
            return '';
        }
        var where = arr.join(') and (');
        if (where) where = '(' + where + ')';
        
        if (where) {
            url += '&where=' + where;
        }

        return url;
    };
    //获得动态的列
    app.getColumns = function (type,cols) {
        var me = this,
            type = type || me.config.defaultConfig.showType,
            isUnEdit = me.isUnEdit() || false;//是否不能编辑
        var col = cols || me.config.Cols || [
            { type: 'checkbox', width: 26 },
            { field: 'LisTestItem_PLBItem_Id', width: 80, title: '组合项目主键', sort: false, hide: true },
            { field: 'LisTestItem_PLBItem_CName', width: 100, title: '组合项目', sort: false, hide: type == 2 ? true : false },
            { field: 'LisTestItem_LisOrderItem_HisItemName', width: 80, title: '采样项目', sort: false, hide: true },
            { field: 'LisTestItem_LBItem_Id', width: 80, title: '检验项目主键', sort: false, hide: true },
            { field: 'LisTestItem_LBItem_CName', width: 120, title: '检验项目', sort: false, templet: function (data) { var that = this || { field: 'LisTestItem_LBItem_CName' }; return me.setGridColumnsStyle(data, that); } },
            { field: 'LisTestItem_LBItem_SName', width: 80, title: '简称', sort: false, templet: function (data) { var that = this || { field: 'LisTestItem_LBItem_SName' }; return me.setGridColumnsStyle(data, that); } },
            { field: 'LisTestItem_LBItem_EName', width: 80, title: '英文', sort: false, hide: true, templet: function (data) { var that = this || { field: 'LisTestItem_LBItem_EName' }; return me.setGridColumnsStyle(data, that); } },
            { field: 'LisTestItem_LBItem_Prec', width: 60, title: '精度', sort: false, hide: true },
            { field: 'LisTestItem_ReportValue', width: 80, title: '报告值', sort: false, edit: (isUnEdit ? false : 'text'), event: 'seepage', templet: function (data) { var that = this || { field:'LisTestItem_ReportValue' }; return me.setGridColumnsStyle(data, that); } },
            { field: 'LisTestItem_QuanValue', width: 80, title: '定量结果', sort: false, hide: true },
            { field: 'LisTestItem_OriglValue', width: 80, title: '原始值', sort: false },
            { field: 'LisTestItem_ResultStatus', width: 80, title: '结果状态', sort: false, templet: function (data) { var that = this || { field: 'LisTestItem_ResultStatus' }; return me.setGridColumnsStyle(data, that); } },
            { field: 'LisTestItem_ResultStatusCode', width: 80, title: '检验结果状态码', sort: false, hide: true, templet: function (data) { var that = this || { field: 'LisTestItem_ResultStatusCode' }; return me.setGridColumnsStyle(data, that); } },
            { field: 'LisTestItem_Unit', width: 60, title: '结果单位', sort: false },
            { field: 'LisTestItem_RefRange', width: 100, title: '参考范围', sort: false },
            { field: 'LisTestItem_PreValue', width: 80, title: '上次结果', sort: false },
            { field: 'LisTestItem_PreValueComp', width: 80, title: '前值对比', sort: false },
            { field: 'LisTestItem_PreCompStatus', width: 100, title: '历史对比', sort: false },
            { field: 'LisTestItem_PreValue2', width: 80, title: '历史值2', sort: false, hide: true },
            { field: 'LisTestItem_PreValue3', width: 80, title: '历史值3', sort: false, hide: true },
            { field: 'LisTestItem_ResultComment', width: 100, title: '结果说明', sort: false, edit: (isUnEdit ? false : 'text') },
            { field: 'LisTestItem_RedoStatus', width: 80, title: '复检状态', sort: false, hide: true },
            { field: 'LisTestItem_RedoValues', width: 80, title: '复检参考结果', sort: false, hide: true },
            { field: 'LisTestItem_RedoDesc', width: 80, title: '复检原因', sort: false, hide: true },
            { field: 'LisTestItem_Id', width: 80, title: '主键ID', sort: false, hide: true },
            { field: 'LisTestItem_LisOrderItem_Id', width: 80, title: '医嘱单项目ID', sort: false, hide: true },
            { field: 'LisTestItem_PLBItem_DispOrder', width: 100, title: '组合项目排序', sort: false, hide: true },
            { field: 'LisTestItem_LBItem_DispOrder', width: 80, title: '项目排序', sort: false, hide: true },
            { field: 'LisTestItem_LisTestForm_Id', width: 80, title: '检验单ID', sort: false, hide: true },
            {
                field: 'LisTestItem_BAlarmColor', width: 80, title: '是否采用特殊提示色', sort: false, hide: true,
                templet: function (data) {
                    var str = "";
                    if (!data["LisTestItem_BAlarmColor"]) return str;
                    if (data["LisTestItem_BAlarmColor"].toString() == 'false') {
                        str = "否";
                    } else {
                        str = "是";
                    }
                    return str;
                }
            },
            {
                field: 'LisTestItem_AlarmColor', width: 80, title: '结果警示特殊颜色', sort: false, hide: true,
                templet: function (data) {
                    if (!data["LisTestItem_AlarmColor"]) return "";
                    var str = "<span style='background:" + data["LisTestItem_AlarmColor"] + ";'>" + data["LisTestItem_AlarmColor"] + "</span>";
                    return str;
                }
            },
            { field: 'LisTestItem_AlarmLevel', width: 80, title: '警示级别', sort: false, hide: true },
            { field: 'LisTestItem_AlarmInfo', width: 80, title: '警示提示', sort: false, hide: true },
            { field: 'Tab', width: 100, title: '用于判断行是否有修改数据', hide: true, sort: false }
        ];

        $.each(col, function (i, item) {
            switch (item["field"]) {
                case "LisTestItem_PLBItem_CName":
                    col[i]["hide"] = type == 2 ? true : false;
                    break;
                case "LisTestItem_ReportValue":
                case "LisTestItem_ResultComment":
                    col[i]["edit"] = isUnEdit ? false : 'text';
                    break;
            }
        });
        return [col];
    };
    //获得数据data
    app.getData = function (callback) {
        var me = this,
            url = me.getLoadUrl();
        //置空之前记录的检验项目
        me.config.defaultConfig.groupData = {};
        me.config.defaultConfig.total = [];
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\n/g, "\\n").replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n")) : {},
                        list = data.list || [];
                    //结果说明 换行符替换为|
                    if (list.length > 0) {
                        $.each(list, function (i, item) {
                            if (item["LisTestItem_ResultComment"].indexOf("\n") != -1)
                                list[i]["LisTestItem_ResultComment"] = item["LisTestItem_ResultComment"].replace(/\n/g, "\|");
                        });
                    }
                    //分组数据 -- 不存在组合项目 则加到other （其他中）
                    me.dataHandle(list);
                }
            }
            me.showByShowType();//显示隐藏tab页
        });

    };
    //处理数据
    app.dataHandle = function (list) {
        var me = this,
            list = list || [];
        //置空之前记录的检验项目
        me.config.defaultConfig.groupData = {};
        me.config.defaultConfig.total = [];
        if (list.length == 0) return;
        //分组数据 -- 不存在组合项目 则加到other （其他中）
        $.each(list, function (i, item) {
            var PItem = item["LisTestItem_PLBItem_Id"] ? item["LisTestItem_PLBItem_Id"] : 'other',
                PItemCName = item["LisTestItem_PLBItem_CName"] ? item["LisTestItem_PLBItem_CName"] : '其他';
            if (!me.config.defaultConfig.groupData[PItem]) me.config.defaultConfig.groupData[PItem] = { text: PItemCName, list: [] };
            me.config.defaultConfig.groupData[PItem]["list"].push(item);
            me.config.defaultConfig.total.push(item);
        });
    };
    //获取查询Fields
    app.getStoreFields = function (isString) {
        var me = this,
            columns = me.getColumns()[0] || [],
            length = columns.length,
            fields = [];
        for (var i = 0; i < length; i++) {
            if (columns[i].field == "Tab") continue;
            if (columns[i].field) {
                var obj = isString ? columns[i].field : {
                    name: columns[i].field,
                    type: columns[i].type ? columns[i].type : 'string'
                };
                fields.push(obj);
            }
        }
        return fields;
    };
    //核心入口
    app.render = function (options) {
        var me = this;
        me.set(options);
        me.config.url = '';
        //第一次加载
        if (!me.tableIns) {
            if (me.config.GridCode) {
                uxbase.CONFIGITEM.initGridConfig([{
                    Code: me.config.GridCode, Elem: '#SampleResultTable', CallBack: function (list) {
                        me.config.Cols = list;
                        me.config.cols = me.getColumns(null, list);
                        me.getResultStatusColor();
                        me.tableIns = table.render(me.config);
                        //监听
                        me.iniListeners();
                        //获得数据
                        me.getData();
                        if (me.config.defaultConfig.type == 'add') me.onAddClick();
                    }
                }]);
            } else {
                me.config.cols = me.getColumns();
                me.getResultStatusColor();
                me.tableIns = table.render(me.config);
                //监听
                me.iniListeners();
                //获得数据
                me.getData();
                if (me.config.defaultConfig.type == 'add') me.onAddClick();
            }
            
        } else {
            me.config.cols = me.getColumns();
            //获得数据
            me.getData();
            if (me.config.defaultConfig.type == 'add') me.onAddClick();
        }
        return me;
    };
    //新增
    app.isAdd = function () {
        var me = this;
        me.config.defaultParams["testFormRecord"] = null;
        //查询本小组默认项目
        me.getDefaultItemBySection(function (items) {
            me.dataHandle(items);
            me.showByShowType();
        });
        return me;
    };
    //对外公开-数据加载
    app.onSearch = function () {
        var me = this;
        table.reload(mytable, {
            url: me.getLoadUrl(),
            where: {
                time: new Date().getTime()
            }
        });
    };
    //根据显示方式展现
    app.showByShowType = function () {
        var me = this,
            TabTitleDom = $("#SampleResultTableGroupTitle");
        if (me.config.defaultConfig.showType == 2) {//按组合项目展现
            if (me.config.defaultConfig.total.length > 0) {//存在检验项目数据处理
                var li = [],
                    start = null;
                for (var t in me.config.defaultConfig.groupData) {
                    if (start == null) start = t;
                    li.push("<li lay-id='" + t + "'><i class='iconfont'>&#xe63c;</i>&nbsp;" + me.config.defaultConfig.groupData[t].text + "</li>");
                }
                $("#SampleResultTableGroupTitle").html(li.join(''));
                element.tabChange("SampleResultTableGroupTab", start);//会去执行页签切换 中的重载表格
                element.init();
                if (TabTitleDom.hasClass("layui-hide")) TabTitleDom.removeClass("layui-hide");
            } else {
                if (!TabTitleDom.hasClass("layui-hide")) TabTitleDom.addClass("layui-hide");
                me.tableIns.reload({
                    url: '',
                    cols: me.getColumns(),
                    height: 'full-112',
                    data: me.config.defaultConfig.total
                });
            }
        } else {//按所有项目展现
            if (!TabTitleDom.hasClass("layui-hide")) TabTitleDom.addClass("layui-hide");
            me.tableIns.reload({
                url: '',
                cols: me.getColumns(),
                height: 'full-112',
                data: me.config.defaultConfig.total
            });
        }
    };
    //新增检验项目
    app.onAddClick = function () {
        var me = this,
            sectionId = me.config.defaultParams["sectionID"],
            testFormId = me.config.defaultParams["testFormRecord"] && me.config.defaultParams["testFormRecord"]["LisTestForm_Id"] ? me.config.defaultParams["testFormRecord"]["LisTestForm_Id"] : null,
            msg = [];
        if (!sectionId) msg.push("请先选择小组进行新增!");
        if (me.config.defaultParams.isReadOnly) msg.push("只读模式不允许对检验单删除检验项目!");
        if (!testFormId) msg.push("请先选择检验单进行新增!");
        if (!me.config.defaultParams["testFormRecord"] || me.config.defaultParams["testFormRecord"]["LisTestForm_MainStatusID"] != 0) msg.push("只允许检验中的检验单进行新增检验项目!");
        if (msg.length > 0) {
            uxbase.MSG.onWarn(msg.join("<br>"));
            return;
        }
        if (me.config.defaultConfig.type == 'add') me.config.defaultConfig.type = 'show';
        parent.layer.open({
            type: 2,
            area: me.screen($) > 2 ? ['1200px', '600px'] : ['90%', '80%'],
            fixed: false,
            maxmin: true,
            title: '新增删除检验项目',
            content: uxutil.path.ROOT + '/ui/layui/views/sample/tab/sample/add/app.html?sectionId=' + sectionId + '&testFormId=' + testFormId,
            end: function () {
                me.getData();
            }
        });
    };
    //删除按钮点击处理方法
    app.onDelClick = function () {
        var me = this,
            records = table.checkStatus(me.config.id).data,
            msg1 = [],
            msg2 = [];
        if (!me.config.defaultParams["sectionID"]) msg1.push("请先选择小组进行删除检验项目!");
        if (me.config.defaultParams.isReadOnly) msg1.push("只读模式不允许对检验单删除检验项目!");
        //删除未保存项目
        if (!me.config.defaultParams["testFormRecord"] && records.length != 0) {
            $.each(records, function (a, itemA) {
                $.each(me.config.defaultConfig.total, function (b, itemB) {
                    if (itemA["LisTestItem_LBItem_Id"] == itemB["LisTestItem_LBItem_Id"]) {
                        me.config.defaultConfig.total.splice(b, 1);
                        return false;
                    }
                });
            });
            me.dataHandle(me.config.defaultConfig.total);
            me.showByShowType();
            return;
        }
        //已保存项目删除处理
        if (!me.config.defaultParams["testFormRecord"] || !me.config.defaultParams["testFormRecord"]["LisTestForm_Id"]) msg1.push("请先选择检验单进行删除检验项目!");
        if (!me.config.defaultParams["testFormRecord"] || me.config.defaultParams["testFormRecord"]["LisTestForm_MainStatusID"] != 0) msg1.push("只允许检验中的检验单进行删除检验项目!");
        if (records.length <= 0) msg1.push("请勾选需要删除的检验项目!");
        if (msg1.length > 0) {
            uxbase.MSG.onWarn(msg1.join("<br>"));
            return;
        }

        var delIDList = [],
            LisTestFormID = records[0]['LisTestItem_LisTestForm_Id'];
        $.each(records, function (i,item) {
            if (item["LisTestItem_LisOrderItem_Id"]) msg2.push(item["LisTestItem_LBItem_CName"] + " 是医嘱项目;");
            if (item["LisTestItem_ReportValue"]) msg2.push(item["LisTestItem_LBItem_CName"] + " 已出检验结果=" + item["LisTestItem_ReportValue"] + ";");
            delIDList.push(item["LisTestItem_Id"]);
        });
        if (msg2.length > 0) {
            msg2.push("建议不要删除,是否仍要删除？");
        } else {
            msg2.push("确定要删除吗？");
        }
        layer.confirm(msg2.join("<br/>"), { icon: 3, title: '提示' }, function (index) {
            me.delBatchLisTestItem(LisTestFormID, delIDList);
            layer.close(index);
        });
    };
    //批量删除项目
    app.delBatchLisTestItem = function (LisTestFormID, delIDList) {
        var me = this,
            url = (me.config.delUrl.slice(0, 4) == 'http' ? '' : uxutil.path.ROOT) + me.config.delUrl,
            LisTestFormID = LisTestFormID || null,
            delIDList = delIDList || [];
        if (LisTestFormID == null || delIDList.length == 0) return;
        var load = layer.load();
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify({ testFormID: LisTestFormID, delIDList: delIDList.join(",") })
        }, function (res) {
                layer.close(load);
                if (res.success) {
                    uxbase.MSG.onSuccess("检验项目删除成功!");
                    layui.event('common', 'refreshTestFormListRecord', { id: LisTestFormID });
                } else {
                    uxbase.MSG.onError("检验项目删除失败!");
                }
        });
    };
    //获得编辑保存信息
    app.getEditSaveParams = function () {
        var me = this,
            testFormId = me.config.defaultParams["testFormRecord"] && me.config.defaultParams["testFormRecord"]["LisTestForm_Id"] ? me.config.defaultParams["testFormRecord"]["LisTestForm_Id"] : null,
            records = me.config.defaultConfig.total || [],
            list = [],
            msg = [];
        if (!me.config.defaultParams["sectionID"]) msg.push("请先选择小组进行保存检验项目!");
        if (me.config.defaultParams.isReadOnly) msg.push("只读模式不允许对检验单保存检验项目!");
        if (!testFormId) msg.push("请先选择检验单进行保存检验项目!");
        if (!me.config.defaultParams["testFormRecord"] || me.config.defaultParams["testFormRecord"]["LisTestForm_MainStatusID"] != 0) msg.push("只允许检验中的检验单进行保存检验项目!");
        //获得修改数据
        $.each(records, function (i, item) {
            if (item["Tab"]) {//被修改数据
                list.push({
                    Id: item["LisTestItem_Id"],
                    ReportValue: item['LisTestItem_ReportValue'],
                    ResultComment: item['LisTestItem_ResultComment'].replace(/\|/g, "\n"),
                    LBItem: {
                        Id: item['LisTestItem_LBItem_Id'],
                        CName: item['LisTestItem_LBItem_CName'],
                        Prec: item['LisTestItem_LBItem_Prec'],
                        DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
                    }
                });
            }
        });
        if (msg.length > 0) {
            uxbase.MSG.onWarn(msg.join("<br>"));
            return false;
        }
        return { listTestItemResult: list, testItemFileds:'ResultComment' };
    };
    //新增时保存默认项目
    app.onAddSave = function (testFormId) {
        var me = this,
            testFormId = testFormId || null,
            tableCache = me.config.defaultConfig.total,
            url = me.config.onAddSaveUrl,
            entity = {
                testFormID: testFormId,
                listAddTestItem: [],
                isRepPItem: true,
                testItemFileds:'ReportValue,ResultComment'
            };
        if (!testFormId || tableCache.length == 0) {
            uxbase.MSG.onSuccess("保存成功!");
            return;
        }
        $.each(tableCache, function (i, item) {
            var obj = {
                LBItem: { Id: item["LisTestItem_LBItem_Id"], DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
                ReportValue: item["LisTestItem_ReportValue"],
                ResultComment: item["LisTestItem_ResultComment"].replace(/\|/g, "\n")
            };
            if (item["LisTestItem_PLBItem_Id"]) obj["PLBItem"] = { Id: item["LisTestItem_PLBItem_Id"], DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
            entity["listAddTestItem"].push(obj);
        });
        var load = layer.load();
        uxutil.server.ajax({ type: "POST", url: url, data: JSON.stringify(entity) }, function (res) {
            //隐藏遮罩层
            layer.close(load);
            if (res.success) {
                uxbase.MSG.onSuccess("保存成功!");
                me.getData();
            } else {
                uxbase.MSG.onError(res.ErrorInfo || "保存失败!");
            }
        });
    };
    //判断是否可操作
    app.isUnEdit = function () {
        var me = this,
            status = me.config.defaultParams["testFormRecord"] && me.config.defaultParams["testFormRecord"]["LisTestForm_MainStatusID"] ? me.config.defaultParams["testFormRecord"]["LisTestForm_MainStatusID"] : null,
            isReadOnly = me.config.defaultParams.isReadOnly;
        //主状态不是检验中或者只读模式 不允许操作
   //     if (isReadOnly || status != 0) {
   //         if (!$("#addLisTestItem").hasClass("layui-hide")) $("#addLisTestItem").addClass("layui-hide");
			//if (!$("#saveLisTestItem").hasClass("layui-hide")) $("#saveLisTestItem").addClass("layui-hide");
			//if (!$("#delLisTestItem").hasClass("layui-hide")) $("#delLisTestItem").addClass("layui-hide");
   //         return true;
   //     } else {
   //         if ($("#addLisTestItem").hasClass("layui-hide")) $("#addLisTestItem").removeClass("layui-hide");
			//if ($("#saveLisTestItem").hasClass("layui-hide")) $("#saveLisTestItem").removeClass("layui-hide");
			//if ($("#delLisTestItem").hasClass("layui-hide")) $("#delLisTestItem").removeClass("layui-hide");
   //         return false;
   //     }

        
    };
    //获得结果状态颜色
    app.getResultStatusColor = function () {
        var me = this,
            url = (me.config.getResultStatusColorUrl.slice(0, 4) == 'http' ? '' : uxutil.path.ROOT) + me.config.getResultStatusColorUrl + "&where=IsUse=1 and DictType='" + me.config.defaultConfig.ResultStatusDictType + "'";
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                me.config.defaultConfig.ResultStatusList = res.value.list;
            } else {
                me.config.defaultConfig.ResultStatusList = [];
            }
        });
    };
    //处理列表状态颜色
    app.setGridColumnsStyle = function (record,that) {
        var me = this,
            that = that,
            str = '',
            value = record[that.field] || "",
            ResultStatusList = me.config.defaultConfig.ResultStatusList,
            DIVWidth = '100%',
            ICONWidth = [],
            BAlarmColor = String(record["LisTestItem_BAlarmColor"]),
            RedoStatus = record["LisTestItem_RedoStatus"],
            RedoStatusStr = '<span style="margin-right:1px;border:none;background-color:#2F4056;"><a style="color:#fff;">复</a></span>',//复检
            AlarmLevel = record['LisTestItem_AlarmLevel'],
            AlarmLevelStr = '<span style="margin-right:1px;border:none;background-color:transparent;position: relative;top: -1px;"><a style="color:#FFFFFF;">{text}</a></span>',
            text = null,
            icons = [];
        if (that.field == "LisTestItem_LBItem_CName") {
            //复检标志
            if (RedoStatus == '1') {//复检
                ICONWidth.push('12px');
                icons.push(RedoStatusStr);
            } else if (RedoStatus == '2') {//建议复检
                ICONWidth.push('12px');
                icons.push("<i class='layui-icon layui-icon-refresh-1' style='font-size:12px;color:#E69139;'></i>");
            }
            //警示级别标志
            switch (String(AlarmLevel)) {
                case "1":
                    text = "<img width='14' style='vertical-align: bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-1.png' />";
                    AlarmLevelStr = AlarmLevelStr.replace(/{text}/g, text);
                    break;
                case "2":
                    text = "<img width='14' style='vertical-align: bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/1-2.png' />";
                    AlarmLevelStr = AlarmLevelStr.replace(/{text}/g, text);
                    break;
                case "3":
                    text = "<img width='14' style='vertical-align: bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-4.png' />";
                    AlarmLevelStr = AlarmLevelStr.replace(/{text}/g, text);
                    break;
                case "4":
                    text = "<img width='14' style='vertical-align: bottom;' src='" + uxutil.path.ROOT + "/ui/layui/images/warn/2-6.png' />";
                    AlarmLevelStr = AlarmLevelStr.replace(/{text}/g, text);
                    break;
                default:
                    break;
            }
            if (text) {
                ICONWidth.push('14px');
                icons.push(AlarmLevelStr);
            }
        }
        //处理背景色DIV宽度
        if (ICONWidth.length > 0) DIVWidth = 'calc(' + DIVWidth + ' - ' + ICONWidth.join(' - ') + ' - ' + Number(ICONWidth.length) + 'px)';

        //颜色处理
        if (BAlarmColor == "true") {
            var fontColor = '#fff';
            if (record["LisTestItem_AlarmColor"] == "#fff" || record["LisTestItem_AlarmColor"] == "#ffffff") fontColor = '#000';
            str = "<div style='display:inline-block;width:"+DIVWidth+";color:" + fontColor + ";background-color:" + record["LisTestItem_AlarmColor"] + ";'>" + value + "</div>";
        } else {
            if (ResultStatusList.length > 0) {
                var ResultStatusCode = record["LisTestItem_ResultStatusCode"];
                $.each(ResultStatusList, function (i,item) {
                    if (ResultStatusCode == item["LBDict_DictCode"] && item["LBDict_ColorValue"]) {
                        var fontColor = '#fff';
                        if (item["LBDict_ColorValue"] == "#fff" || item["LBDict_ColorValue"] == "#ffffff") fontColor = '#000';
                        str = "<div style='display:inline-block;width:" + DIVWidth +";color:" + fontColor + ";background-color:" + item["LBDict_ColorValue"] + ";'>" + value + "</div>";
                        return false;
                    }
                });
            }
        }
        if (str && icons.length > 0) str = icons.join('') + str;
        return str || icons.join('') + value;
    };
    //两个单数组合并去重
    app.arrConcatNoRepeat = function (arr1, arr2) {
        var me = this,
            arr1 = arr1 || [],
            arr2 = arr2 || [];

        for (var i = 0; i < arr2.length; i++) {
            if (arr1.indexOf(arr2[i]) == -1) arr1.push(arr2[i]);
        }

        return arr1;
    };
    //判断浏览器大小方法
    app.screen = function ($) {
        //获取当前窗口的宽度
        var width = $(window).width();
        if (width > 1200) {
            return 3;   //大屏幕
        } else if (width > 992) {
            return 2;   //中屏幕
        } else if (width > 768) {
            return 1;   //小屏幕
        } else {
            return 0;   //超小屏幕
        }
    };
    //监听
    app.iniListeners = function () {
        var me = this;
        //监听单元格编辑
        table.on('edit(' + me.config.id + ')', function (obj) {
            var value = obj.value, //得到修改后的值
                data = obj.data,//得到所在行所有键值
                field = obj.field; //得到字段
            //添加修改标记
            $.each(me.config.defaultConfig.total, function (i,item) {
                if (item["LisTestItem_Id"] == data["LisTestItem_Id"])
                    item["Tab"] = "edit";
            });
        });
        //监听可编辑表格的 回车键盘操作向下换行
        $("#SampleResultTable").parent().off("keydown").on("keydown", "td[data-field=LisTestItem_ReportValue] .layui-table-edit,td[data-field=LisTestItem_ResultComment] .layui-table-edit", function (event) {
            //响应回车键按下的处理
            var e = event || window.event || arguments.callee.caller.arguments[0];
            //捕捉按键
            if (e.shiftKey) return;
            var key = $(e.target).parent().attr("data-key");
            if (typeof key != 'string') return;
            if (e && e.keyCode == 13) {//回车向下
                var nextElem = $(e.target).parents('tr').next("tr").find("td[data-key=" + key + "][data-edit='text']");
                if (nextElem[0]) nextElem.click();
                return false;
            }
        });
        //监听报告值、结果说明单元格双击事件
        $("#SampleResultTable").parent().off("dblclick").on("dblclick", 'td[data-field=LisTestItem_ReportValue] .layui-table-edit,td[data-field=LisTestItem_ResultComment] .layui-table-edit', function () {
            var field = $(this).parent('td').attr("data-field"),
                itemid = $(this).parents('tr').find('td[data-field=LisTestItem_LBItem_Id]>div').html(),
                typecode = $(this).parent().attr("data-field") == 'LisTestItem_ReportValue' ? 'XMJGDY' : 'XMJGSM',
                typename = $(this).parent().attr("data-field") == 'LisTestItem_ReportValue' ? '项目结果短语' : '项目结果说明',
                IsNextLineAdd = typecode == 'XMJGDY' ? 0 : 1,
                IsReplace = typecode == 'XMJGDY' ? 0 : 1;
            me.openPhrase($(this).val(), typecode, typename, itemid, IsNextLineAdd, IsReplace, function (val) {
                $.each(table.cache[me.config.id], function (i,item) {
                    if (item["LisTestItem_LBItem_Id"] == itemid) {
                        table.cache[me.config.id][i][field] = val;
                        table.cache[me.config.id][i]["Tab"] = "edit";
                        me.updateRowItem(table.cache[me.config.id][i], "LisTestItem_LBItem_Id");
                        return false;
                    }
                });
            });
        });
        //监听行单击事件
        table.on('row(SampleResultTable)', function (obj) {
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
		});
		//新增
        $("#addLisTestItem").off().on('click', function () {
            layui.event('topToolBar', 'onSave', {});//保存检验单信息+检验结果
            me.config.defaultConfig.type = 'add';//用于保存后弹出新增页面
        });
        //删除
        $("#delLisTestItem").off().on('click', function () {
            me.onDelClick();
        });
        //保存
        $("#saveLisTestItem").off().on('click', function () {
            layui.event('topToolBar', 'onSave', {});//保存检验单信息+检验结果
        });
        //展现方式切换
        form.on('radio(itemShowType)', function (data) {
            me.config.defaultConfig.showType = data.value;
            me.showByShowType();
        });
        //组合项目分组页签
        element.on('tab(SampleResultTableGroupTab)', function (data) {
            var layid = $(this).attr("lay-id"),
                tableData = [];
            if (!layid) return;
            tableData = me.config.defaultConfig.groupData[layid].list;
            me.tableIns.reload({
                url: '',
                cols: me.getColumns(),
                height: me.config.defaultConfig.showType == 2 ? 'full-135' : 'full-112',
                data: tableData
            });
        });
        //项目复检按钮监听
        $("#TestItemAnewTestbut").off().on("click", function () {
            window.event.stopPropagation();
            var btn = $(this);
            var menuElemID = "#itemRecheckMenuDropDown";
            var menuHeight = $(menuElemID).css("height");
            var left = btn.offset().left;
            var top = parseFloat(btn.offset().top) - parseFloat(menuHeight) - 3 + "px";
            $(menuElemID).siblings(".btn_spread_menu").css("display", "none");
            $(menuElemID).css({ "left": left, "top": top });
            $(menuElemID).toggle();
        });
		//选中项目复检 1
		$("#checkItemRecheck").click(function(){
			var testformrecode = me.config.defaultParams["testFormRecord"],
                itemdata = table.checkStatus('SampleResultTable').data,
                rowitemdata = me.config.checkRowData;
			if(!testformrecode){
                uxbase.MSG.onWarn("请选择检验单!");
                return;
			}
			var id = testformrecode.LisTestForm_Id;//检验单id
			var StatusID = testformrecode.LisTestForm_MainStatusID;
            if (!id) {
                uxbase.MSG.onWarn("此检验单无检验单ID!");
				return;
			}
			if (StatusID != 0 ) {
                uxbase.MSG.onWarn("只有检验中数据可执行该操作!");
			    return;
            }            
            if (itemdata.length <= 0) {
                if (rowitemdata.length == 0) {
                    uxbase.MSG.onWarn("请选择检验项目数据!");
                    return;
                }
            }
            var recheckdata = itemdata.length <= 0 ? rowitemdata : itemdata;
            me.recheckclick(id, testformrecode.LisTestForm_GSampleTypeID, recheckdata,1);
		});
		//选中项目取消复检 2
		$("#checkItemUnRecheck").click(function(){
            var testformrecode = me.config.defaultParams["testFormRecord"],
                itemdata = table.checkStatus('SampleResultTable').data,
                rowitemdata = me.config.checkRowData,
                list = [];
			if(!testformrecode){
                uxbase.MSG.onWarn("请选择检验单!");
                return;
			}
			var id = testformrecode.LisTestForm_Id;//检验单id
			var StatusID = testformrecode.LisTestForm_MainStatusID;
			if(!id){
                uxbase.MSG.onWarn("此检验单无检验单ID!");
				return;
			}
			if (StatusID != 0 ) {
                uxbase.MSG.onWarn("只有检验中数据可执行该操作!");
			    return;
			}
			
            if (itemdata.length <= 0) {
                if (rowitemdata.length == 0) {
                    uxbase.MSG.onWarn("请选择检验项目数据!");
                    return;
                }
            }
            var recheckdata = itemdata.length <= 0 ? rowitemdata : itemdata,
                list = [],
                msg = [];
            $.each(recheckdata, function (i, item) {
                if (item["LisTestItem_RedoStatus"] != 1)
                    msg.push(item["LisTestItem_LBItem_CName"]);
                else
                    list.push(item);
            });
            if (msg.length > 0) {
                if (list.length == 0) {
                    uxbase.MSG.onWarn("选中项目不存在已复检项目!");
                    return;
                }
                layer.confirm(msg.join(',') + "<br>不是复检项目，是否略过仅取消已复检项目？", { icon: 3, title: '提示' }, function (index) {
                    me.recheckclick(id, testformrecode.LisTestForm_GSampleTypeID, list, 2);
                    layer.close(index);
                });
            } else {
                me.recheckclick(id, testformrecode.LisTestForm_GSampleTypeID, recheckdata, 2);
            }
		});
		//整单复检 3
		$("#testFormRecheck").click(function(){
            var testformrecode = me.config.defaultParams["testFormRecord"],
                itemdata = table.cache.SampleResultTable,
                list = me.config.checkRowData.length > 0 ? me.config.checkRowData : null;
			if(!testformrecode){
                uxbase.MSG.onWarn("请选择检验单!");
                return;
			}
			var id = testformrecode.LisTestForm_Id;//检验单id
			var StatusID = testformrecode.LisTestForm_MainStatusID;
			if(!id){
                uxbase.MSG.onWarn("此检验单无检验单ID!");
				return;
			}
			if (StatusID != 0 ) {
                uxbase.MSG.onWarn("只有检验中数据可执行该操作!");
			    return;
			}
			if(itemdata.length <= 0){
                uxbase.MSG.onWarn("无检验项目数据无法复检!");
				return;
			}
            me.recheckclick(id, testformrecode.LisTestForm_GSampleTypeID, list,3);
		});
		//整单取消复检
		$("#testFormUnRecheck").click(function(){
			var  testformrecode = me.config.defaultParams["testFormRecord"],
			     itemdata = table.cache.SampleResultTable;
			if(!testformrecode){
                uxbase.MSG.onWarn("请选择检验单!");
                return;
			}
			var id = testformrecode.LisTestForm_Id;//检验单id
			var StatusID = testformrecode.LisTestForm_MainStatusID;
			if(!id){
                uxbase.MSG.onWarn("此检验单无检验单ID!");
				return;
			}
			if (StatusID != 0 ) {
                uxbase.MSG.onWarn("只有检验中数据可执行该操作!");
			    return;
			}
			if(itemdata.length <= 0){
                uxbase.MSG.onWarn("无检验项目数据无法取消复检!");
				return;
			}
            me.recheckclick(id, testformrecode.LisTestForm_GSampleTypeID,null,4);
		});
		//当前项目复检
		$("#currentItemRecheck").click(function(){
			var  testformrecode = me.config.defaultParams["testFormRecord"],
			     itemdata = me.config.checkRowData;
            if (!testformrecode) {
                uxbase.MSG.onWarn("请选择检验单!");
				return;
			}
			var id = testformrecode.LisTestForm_Id;//检验单id
			var StatusID = testformrecode.LisTestForm_MainStatusID;
            if (!id) {
                uxbase.MSG.onWarn("此检验单无检验单ID!");
				return;
			}
            if (StatusID != 0) {
                uxbase.MSG.onWarn("只有检验中数据可执行该操作!");
			    return;
			}
			if(itemdata.length != 1){
                uxbase.MSG.onWarn("请选择一条检验项目数据!");
				return;
			}
            me.recheckclick(id, testformrecode.LisTestForm_GSampleTypeID,itemdata,5);
		});
		//项目结果查看并导出
		$("#itemResultShowAndLoad").click(function(){
			var itemdata = me.config.checkRowData;
			if(!me.config.defaultParams.sectionID){
                uxbase.MSG.onWarn("当前无打开小组页签不可操作!");
				return;
			}
			layer.open({
				type: 2,
				area: ['60%', '80%'],
				fixed: false,
				maxmin: false,
				title: '项目结果查看并导出',
				content: uxutil.path.ROOT + '/ui/layui/views/sample/tab/sample/export/export.html?sectionno='+me.config.defaultParams.sectionID+"&sectionname="+me.config.defaultParams.sectionCName,
				success:function(layero,index){
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.fireEventSaveInfoFun(itemdata,function(){
						layer.close(index);
					});
				},
				end: function () {
				}
			}); 
		});
		//查看项目详细信息
		$("#iteminfo").click(function(){
			var itemdata = me.config.checkRowData;
			if(itemdata.length ==0){
                uxbase.MSG.onWarn("请选择检验结果项目!");
				return false;
			}
			//项目id
			var itemid = itemdata[0].LisTestItem_LBItem_Id;
			//检验单id
			var testformid = itemdata[0].LisTestItem_LisTestForm_Id;
			//组合项目id
			var itempid = itemdata[0].LisTestItem_PLBItem_Id;
			if(!me.config.defaultParams.sectionID){
                uxbase.MSG.onWarn("当前无打开小组页签不可操作!");
				return;
			}
			layer.open({
				type: 2,
				area: ['90%', '90%'],
				fixed: false,
				maxmin: false,
				title: '查看项目详细信息',
				content: uxutil.path.ROOT + '/ui/layui/views/sample/tab/sample/dlinfo/index.html?id='+itemid+"&testformid="+testformid+"&pid="+itempid
			}); 
		});
    };

    //查询小组默认项目及默认值
    app.getDefaultItemBySection = function (callback) {
        var me = this,
            items = [],
            sectionid = me.config.defaultParams.sectionID || null,
            url = me.config.getSectionDefaultItemsUrl;
        if (!sectionid) return [];
        //组合项目、项目、默认值
        url += "?isPlanish=true";
        url += "&sectionID=" + sectionid;
        url += "&fields=LBSectionSingleItemVO_LBParItem_Id,LBSectionSingleItemVO_LBParItem_CName,LBSectionSingleItemVO_LBItem_Id,LBSectionSingleItemVO_LBItem_CName,LBSectionSingleItemVO_LBItem_SName,LBSectionSingleItemVO_LBItem_EName,LBSectionSingleItemVO_LBItem_Prec,LBSectionSingleItemVO_LBSectionItem_DefultValue";
        var load = layer.load();
        uxutil.server.ajax({ url: url }, function (res) {
            layer.close(load);
            if (res.success) {
                if (res.value && res.value.list && res.value.list.length > 0) {
                    $.each(res.value.list, function (i,item) {
                        items.push({
                            LisTestItem_PLBItem_Id: item["LBSectionSingleItemVO_LBParItem_Id"],
                            LisTestItem_PLBItem_CName: item["LBSectionSingleItemVO_LBParItem_CName"],
                            LisTestItem_LBItem_Id: item["LBSectionSingleItemVO_LBItem_Id"],
                            LisTestItem_LBItem_CName: item["LBSectionSingleItemVO_LBItem_CName"],
                            LisTestItem_LBItem_SName: item["LBSectionSingleItemVO_LBItem_SName"],
                            LisTestItem_LBItem_EName: item["LBSectionSingleItemVO_LBItem_EName"],
                            LisTestItem_LBItem_Prec: item["LBSectionSingleItemVO_LBItem_Prec"],
                            LisTestItem_ReportValue: item["LBSectionSingleItemVO_LBSectionItem_DefultValue"],
                            LisTestItem_ResultComment: ""
                        });
                    });
                }
            } else {
                uxbase.MSG.onError(res.ErrorInfo);
            }
            callback(items);
        });
    };
    //列表更新一行数据 -- fields:{ "LisTestItem_Id": '5598045837466289641',"LisTestItem_ReportValue": "123" }, key: "LisTestItem_Id"
    app.updateRowItem = function (fields, key) {
        var me = this,
            that = me.tableIns.config.instance,
            list = table.cache[that.key] || [],
            len = list.length,
            index = null;

        for (var i = 0; i < len; i++) {
            if (list[i][key] == fields[key]) {
                index = i;
                break;
            }
        }

        if (index == null) {//不存在
            return false;
        } else {
            var tr = that.layBody.find('tr[data-index="' + index + '"]'),
                data = list[index],
                cacheData = table.cache[that.key][index];
            //将变化的字段值赋值到data  覆盖原先值
            data = $.extend({}, data, fields);

            fields = fields || {};
            layui.each(fields, function (ind, value) {
                if (ind in data) {
                    var templet, td = tr.children('td[data-field="' + ind + '"]');
                    data[ind] = value;
                    cacheData[ind] = value;
                    that.eachCols(function (i, item2) {
                        if (item2.field == ind && item2.templet) {
                            templet = item2.templet;
                        }
                    });
                    td.children(".layui-table-cell").html(function () {
                        return templet ? function () {
                            return typeof templet === 'function'
                                ? templet(data)
                                : laytpl($(templet).html() || value).render(data)
                        }() : value;
                    }());
                    td.data('content', value);
                }
            });
            return true;
        }
    };
    //弹出短语选择
    app.openPhrase = function (value, TypeCode, TypeName, ItemID, IsNextLineAdd,IsReplace,callback) {
        var me = this,
            //sectionID = me.config.defaultParams.sectionID || null,
            ItemID = ItemID || null,
            IsNextLineAdd = IsNextLineAdd || 0,//是否换行加入值
            //短语表配置
            TypeCode = TypeCode || null,
            TypeName = TypeName || null,
            ObjectType = 2,//针对类型1：小组样本 2：检验项目
            ObjectID = ItemID,
            PhraseType = "ItemPhrase",//枚举
            value = value || "",
            IsReplace = IsReplace || 0,//是否使用“|”替换换行符 保存时再将“|”换为换行符保存
            SampleTypeID = me.config.defaultParams["testFormRecord"] && me.config.defaultParams["testFormRecord"]["LisTestForm_GSampleTypeID"] ? me.config.defaultParams["testFormRecord"]["LisTestForm_GSampleTypeID"] : null;
        if (!ItemID) {
            uxbase.MSG.onWarn("项目不能为空!");
            return;
        }
        if (!TypeCode) {
            uxbase.MSG.onWarn("缺少TypeCode参数!");
            return;
        }
        if (!TypeName) {
            uxbase.MSG.onWarn("缺少TypeName参数!");
            return;
        }
        parent.layer.open({
            type: 2,
            area: ['600px', '420px'],
            fixed: false,
            maxmin: true,
            title: TypeName,
            content: uxutil.path.ROOT + '/ui/layui/views/sample/basic/phrase/new/index.html?CName=' + TypeName + '&ObjectType=' + ObjectType + '&ObjectID=' + ObjectID + '&PhraseType=' + PhraseType + '&TypeName=' + TypeName + '&TypeCode=' + TypeCode + '&SampleTypeID=' + SampleTypeID + '&isAppendValue=1&ISNEXTLINEADD=' + IsNextLineAdd,
            success: function (layero, index) {
                var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                body.find('#Comment').val(IsReplace ? value.replace(/\|/g, "\n") : value);
                var iframeWin = parent.window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
                iframeWin.externalCallFun(function (v) { callback(IsReplace ? v.replace(/\n/g, "\|") : v); });
            }
        });
    };
    //复检
    app.recheckclick = function (TestFormId, SampleTypeID,TestItemLIst,type){
		var me = this;
		layer.open({
			type: 2,
			area: ['400px', '250px'],
			fixed: false,
			maxmin: false,
			title: '复检',
			content: uxutil.path.ROOT + '/ui/layui/views/sample/tab/sample/recheck/recheck.html?type='+type,
			success:function(layero,index){
				var iframe = $(layero).find("iframe")[0].contentWindow;
                iframe.fireEventSaveInfoFun(TestFormId, SampleTypeID,TestItemLIst,function(){
					layer.close(index);
                    layui.event('common', 'refreshTestFormListRecord', { id: TestFormId });
                    uxbase.MSG.onSuccess("操作成功!");
				});
			},
			end: function () {
			}
		});
	};
    //暴露接口
    exports('SampleResultTable', app);
});
