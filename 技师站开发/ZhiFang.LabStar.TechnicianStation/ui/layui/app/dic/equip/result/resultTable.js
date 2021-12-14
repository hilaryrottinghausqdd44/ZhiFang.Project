/**
	@name：仪器结果替换列表
	@author：zhangda
	@version 2019-08-21
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        uxtable = layui.uxtable;
    var config = { equipID: null, equipCName: '' };
    var resultTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
            elem: '',
            id: "",
            checkRowData:[],
            /**默认传入参数*/
            defaultParams: {},
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
                "property": 'LBEquipResultTH_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipResultTHByHQL?isPlanish=true',
            delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBEquipResultTH',
            editUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipResultTHByField',
            //获得样本类型id和名称
            getSampleTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
            getEnumTypeUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',//获得枚举 传递枚举类型名
            //获得性别id和名称
            //getGenderUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBGenderByHQL?isPlanish=true',
            where: "",
            toolbar: '',
            page: false,
            limit: 500,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size:'sm',//小表格
            cols: [[
                { type: 'numbers', title: '行号', fixed: 'left' },
                { field: 'LBEquipResultTH_Id', width: 150, title: '主键', sort: true, hide: true },
                { field: 'LBEquipResultTH_LBItem_Id', width: 150, title: '项目编号', sort: true, hide: true },
                {
                    field: 'LBEquipResultTH_LBItem_CName', minWidth: 150, flex: 1, title: '检验项目', sort: true,
                    templet: function (data) {
                        var str = "";
                        if (data.LBEquipResultTH_LBItem_Id == "") {
                            str = "所有";
                        } else {
                            str = data.LBEquipResultTH_LBItem_CName;
                        }
                        return str;
                    }
                },
                {
                    field: 'LBEquipResultTH_SampleTypeID', width: 150, title: '样本类型', sort: true,
                    templet: function (data) {
                        var str = "";
                        if (data.LBEquipResultTH_SampleTypeID) {
                            for (var i = 0; i < serverData.sampleData.length; i++) {
                                if (data.LBEquipResultTH_SampleTypeID == serverData.sampleData[i].Id) {
                                    str = serverData.sampleData[i].CName;
                                    break;
                                }
                            }
                        }
                        return str;
                    }
                },
                {
                    field: 'LBEquipResultTH_GenderID', width: 100, title: '性别', sort: true,
                    templet: function (data) {
                        var str = "";
                        if (data.LBEquipResultTH_GenderID) {
                            for (var i = 0; i < serverData.genderData.length; i++) {
                                if (data.LBEquipResultTH_GenderID == serverData.genderData[i].Id) {
                                    str = serverData.genderData[i].CName;
                                    break;
                                }
                            }
                        }
                        return str;
                    }
                },
                { field: 'LBEquipResultTH_CalcType', width: 150, title: '替换类型', sort: true },
                { field: 'LBEquipResultTH_SourceValue', width: 150, title: '原始值', sort: true },
                { field: 'LBEquipResultTH_ReportValue', width: 150, title: '报告值', sort: true },
                { field: 'LBEquipResultTH_AppValue', width: 150, title: '附加值', sort: true },
                { field: 'LBEquipResultTH_DispOrder', width: 150, title: '显示次序', sort: true, hide: true }
            ]],
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
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                return {
                    "code": res.success ? 0 : 1, //解析接口状态
                    "msg": res.ErrorInfo, //解析提示文本
                    "count": data.count || 0, //解析数据长度
                    "data": data.list || []
                };
            },
            done: function (res, curr, count) {
                if (count == 0) return;
                $("#resultTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //列表内后台数据
    var serverData = function () {
        //组合项目下拉框内容
        sampleData: []
        //小组下拉框内容
        genderData: []
    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, resultTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, resultTable, Class.pt, me);// table,
        return me;
    };
    Class.pt = Class.prototype;
    //获取查询Url
    Class.pt.getLoadUrl = function () {
        var me = this, arr = [];
        var url = me.config.selectUrl;
        url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
        //默认条件
        if (me.config.defaultWhere && me.config.defaultWhere != '') {
            arr.push(me.config.defaultWhere);
        }
        //内部条件
        if (me.config.internalWhere && me.config.internalWhere != '') {
            arr.push(me.config.config.internalWhere);
        }
        //外部条件
        if (me.config.externalWhere && me.config.externalWhere != '') {
            arr.push(me.config.externalWhere);
        }
        //传入的默认参数处理
        if (me.config.defaultParams) {

        }
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + JShell.String.encode(where);
        }
        var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
        if (defaultOrderBy && defaultOrderBy.length > 0) url += '&sort=' + JSON.stringify(defaultOrderBy);

        return url;
    };
    //获取查询Fields
    Class.pt.getStoreFields = function (isString) {
        var me = this,
            columns = me.config.cols[0] || [],
            length = columns.length,
            fields = [];
        for (var i = 0; i < length; i++) {
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
    resultTable.render = function (options) {
        var me = this;
        var inst = new Class(options);
        inst.tableIns = table.render(inst.config);
        inst.loadSampleData();
        inst.loadgenderData();
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    resultTable.onSearch = function (mytable, checkRow) {
        var me = this;
        var EquipID = checkRow.LBEquip_Id;
        config.equipID = EquipID;
        config.equipCName = checkRow.LBEquip_CName;
        var inst = new Class(me);
        var where = 'EquipID=' + EquipID;
        resultTable.where = where;
        resultTable.url = inst.getLoadUrl();
        resultTable.elem = "#" + mytable;
        resultTable.id = mytable;
        //      inst.config.where = where;
        table.reload(mytable, {
            url: inst.getLoadUrl(),
            where: {
                where: where
            }
        });
    };
    //获得样本类型
    Class.pt.loadSampleData = function () {
        var me = this;
        var url = me.config.getSampleTypeUrl + '&fields=LBSampleType_CName,LBSampleType_Id';
        url += '&where=(lbsampletype.IsUse=1)';
        //组合项目下拉框内容
        serverData.sampleData = [];
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                for (var i = 0; i < value.list.length; i++) {
                    serverData.sampleData.push({ Id: value.list[i].LBSampleType_Id, CName: value.list[i].LBSampleType_CName});
                }
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //获得性别
    Class.pt.loadgenderData = function () {
        var me = this;
        var url = me.config.getEnumTypeUrl + '?classname=GenderType&classnamespace=ZhiFang.Entity.LabStar';
        //组合项目下拉框内容
        serverData.genderData = [{ Id:'0',CName:"所有" }];
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    for (var i in data) {
                        serverData.genderData.push({ Id: data[i].Id, CName: data[i].Name });
                    }
                }
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //判断浏览器大小方法
    Class.pt.screen = function ($) {
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
    Class.pt.iniListeners = function () {
        var me = this;
        //仪器列表行 监听行单击事件
        table.on('row(resultTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        });
        //新增
        $('#addResultTH').on('click', function () {
            var tableData = table.cache[me.config.id];
            var DispOrder = 1;
            if (tableData.length > 0) {
                for (var i = 0; i < tableData.length;i++) {
                    if (tableData[i].LBEquipResultTH_DispOrder > DispOrder) {
                        DispOrder = tableData[i].LBEquipResultTH_DispOrder;
                    }
                }
            }
            var flag = false;
            layer.open({
                type: 2,
                area: me.screen($) < 2 ? ['85%', '70%'] : ['800px','520px'],
                fixed: false,
                maxmin: false,
                title: '新增结果特殊替换',
                content: 'result/resultForm/app.html?equipID=' + config.equipID + '&DispOrder=' + (Number(DispOrder) + 1),
                cancel: function (index, layero) {
                    flag = true;
                },
                success: function (layero, index) {
                    var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                    //body.find('#LBEquipResultTH_LBEquip_Id').val(config.equipID);
                    //body.find('#LBEquipResultTH_DispOrder').val(Number(DispOrder)+1);
                },
                end: function () {
                    if (flag) return;
                    table.reload(resultTable.id);
                }
            });
        });
        //编辑
        $('#editResultTH').on('click', function () {
            var flag = false;
            layer.open({
                type: 2,
                area: me.screen($) < 2 ? ['85%', '70%'] : ['800px', '520px'],
                fixed: false,
                maxmin: false,
                title: '编辑结果特殊替换',
                content: 'result/resultForm/app.html?equipID=' + config.equipID + '&PK=' + me.config.checkRowData[0].LBEquipResultTH_Id,
                cancel: function (index, layero) {
                    flag = true;
                },
                success: function (layero, index) {
                    var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                    //body.find('#LBEquipResultTH_LBEquip_Id').val(config.equipID);
                    //body.find('#LBEquipResultTH_Id').val(me.config.checkRowData[0].LBEquipResultTH_Id);
                },
                end: function () {
                    if (flag) return;
                    table.reload(resultTable.id);
                }
            });
        });
        //删除
        $('#delResultTH').on('click', function () {
            me.onDelClick();
        });
    };
    //删除方法
    Class.pt.onDelClick = function () {
        var me = this;
        var records = me.config.checkRowData;
        if (records.length == 0) {
            layer.msg('请选择数据！');
            return;
        }
        var id = records[0].LBEquipResultTH_Id;
        var url = me.config.delUrl + '?id=' + id;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var indexs = layer.load();//显示遮罩层
            uxutil.server.ajax({
                url: url
            }, function (data) {
                layer.close(indexs);//隐藏遮罩层
                if (data.success === true) {
                    layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0, time: 2000 });
                    me.config.checkRowData = [];
                    //刷新数据
                    table.reload(resultTable.id);
                } else {
                    //layer.msg(data.result[0].ErrorInfo);
                    layer.msg("删除失败！", { icon: 5, anim: 6 });
                }
            });
        });
    };
    //暴露接口
    exports('resultTable', resultTable);
});
