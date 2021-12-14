/**
	@name：评语短语表格
	@author：zhangda
	@version 2019-11-26
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        uxtable = layui.uxtable;

    var config = { CName: null, ObjectType: null, ObjectID: null, PhraseType: null, TypeCode: null, TypeName: null, SampleTypeID:null, isAppendValue: null };

    var CommentPhraseTable = {
        searchInfo: {
            isLike: true,
            fields: ['']
        },
        config: {
            elem: '',
            id: "",
            checkRowData: [],
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
                "property": 'LBPhrase_DispOrder',
                "direction": 'ASC'
            }],
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true',
            addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase',
            delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase',
            where: "",
            toolbar: '',
            page: false,
            limit: 500,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号', fixed: 'left' },
                { field: 'LBPhrase_Id', width: 150, title: '主键', sort: true, hide: true },
                { field: 'LBPhrase_CName', minWidth: 200, title: '评语', sort: true },
                { field: 'LBPhrase_PinYinZiTou', width: 200, title: '拼音字头', sort: true },
                { field: 'LBPhrase_DispOrder', width: 100, title: '显示次序', sort: true }
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
                //无数据处理
                if (count == 0) return;
                //触发点击事件
                $("#CommentPhraseTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

            }
        },
        set: function (options) {
            var me = this;
            if (options) me.config = $.extend({}, me.config, options);
        }

    };
    //构造器
    var Class = function (options) {
        var me = this;
        me.config = $.extend({}, CommentPhraseTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, CommentPhraseTable, Class.pt, me);// table,
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
            arr.push(me.config.internalWhere);
        }
        //外部条件
        if (me.config.externalWhere && me.config.externalWhere != '') {
            arr.push(me.config.externalWhere);
        }
        //传入的默认参数处理
        if (me.config.defaultParams) {

        }
        if (config.ObjectType) arr.push("ObjectType='" + config.ObjectType + "'");
        if (config.ObjectID) arr.push("ObjectID='" + config.ObjectID + "'");
        if (String(config.SampleTypeID) != 'null')
            arr.push("SampleTypeID is null or SampleTypeID='" + config.SampleTypeID + "'");
        else
            arr.push("SampleTypeID is null");
        arr.push("PhraseType='" + config.PhraseType + "' and TypeCode='" + config.TypeCode+"'");
        var where = arr.join(") and (");
        if (where) where = "(" + where + ")";

        if (where) {
            url += '&where=' + JSON.stringify(where);
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
    CommentPhraseTable.render = function (options, CName, ObjectType, ObjectID, PhraseType, TypeCode, TypeName, SampleTypeID, isAppendValue) {
        var me = this;
        config.CName = CName;
        config.ObjectType = ObjectType;
        config.ObjectID = ObjectID;
        config.PhraseType = PhraseType;
        config.TypeCode = TypeCode;
        config.TypeName = TypeName;
        config.SampleTypeID = SampleTypeID;
        config.isAppendValue = isAppendValue;
        var inst = new Class(options);
        inst.config.url = inst.getLoadUrl();
        if (config.CName) {//赋值title
            for (var i in inst.config.cols[0]) {
                if (inst.config.cols[0][i].field == "LBPhrase_CName") {
                    inst.config.cols[0][i].title = config.CName;
                }
            }
        }
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    CommentPhraseTable.onSearch = function (mytable) {
        var me = this;
        var inst = new Class(me);
        CommentPhraseTable.url = inst.getLoadUrl();
        CommentPhraseTable.elem = "#" + mytable;
        CommentPhraseTable.id = mytable;
        table.reload(mytable, {
            url: inst.getLoadUrl()
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
    //监听
    Class.pt.iniListeners = function () {
        var me = this;
        //监听行单击事件
        table.on('row(CommentPhraseTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        });
        //监听行双击事件
        table.on('rowDouble(CommentPhraseTable)', function (obj) {
            var str = "";
            var Comment = $("#Comment").val();
            if (Comment == "" || config.isAppendValue.toString() == "false") {
                str = obj.data.LBPhrase_CName;
            } else {
                str = Comment + "\n"+ obj.data.LBPhrase_CName;
            }
            $("#Comment").val(str);
            //判断是否关闭
            var IsClose = $("#IsClose").prop("checked");
            if (IsClose) {
                layui.event("close", "close", { value: str });
            }
        });
        //监听排序事件
        table.on('sort(CommentPhraseTable)', function (obj) {
            var field = obj.field;//排序字段
            var type = obj.type;//升序还是降序
            //修改默认的排序字段
            me.config.sort = [{
                "property": field,
                "direction": type
            }];
            table.reload('CommentPhraseTable', {
                initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
                url: me.getLoadUrl(),
                height: me.config.height,
                where: {
                    time: new Date().getTime()
                }
            });
        });
        //新增短语
        $("#addLBPhrase").click(function () {
            me.addLBPhraseClick();
        });
        //删除短语
        $("#delLBPhrase").click(function () {
            me.delLBPhraseClick();
        });
        //过滤
        $("#search").blur(function () {
            me.searchFun();
        });
        //监听回车按下事件--查询
        $(document).keydown(function (event) {
            switch (event.keyCode) {
                case 13:
                    //判断焦点是否在该输入框
                    if (document.activeElement == document.getElementById("search")) {
                        me.searchFun();
                    }
            }
        });
    };
    //新增短语
    Class.pt.addLBPhraseClick = function () {
        var me = this;
        if ($("#LBPhrase_CName").val() == "") return;
        var indexs = layer.load(),
            ObjectType = config.ObjectType,
            ObjectID = config.ObjectID,
            PhraseType = config.PhraseType,
            TypeCode = config.TypeCode,
            TypeName = config.TypeName,
            DispOrder = table.cache[me.config.id].length + 1;
        setTimeout(function () {
            var entity = {
                PhraseType: PhraseType,
                TypeCode: TypeCode,
                TypeName: TypeName,
                CName: $("#LBPhrase_CName").val(),
                Shortcode: $("#LBPhrase_Shortcode").val(),
                PinYinZiTou: $("#LBPhrase_PinYinZiTou").val(),
                IsUse: true,
                DispOrder: DispOrder
            };
            if (ObjectType) entity.ObjectType = ObjectType;
            if (ObjectID) entity.ObjectID = ObjectID;
            var config = {
                type: "POST",
                url: CommentPhraseTable.config.addUrl,
                data: JSON.stringify({ entity: entity })
            };
            uxutil.server.ajax(config, function (data) {
                layer.close(indexs);//隐藏遮罩层
                if (data.success) {
                    table.reload(me.config.id, {
                        url: me.getLoadUrl(),
                        height: me.config.height,
                        where: {
                            time: new Date().getTime()
                        }
                    });
                    me.clearForm();
                } else {
                    layer.msg(data.msg, { icon: 5, anim: 6 });
                }
            });
        }, 100);
    }
    //删除短语
    Class.pt.delLBPhraseClick = function () {
        var me = this;
        var id = me.config.checkRowData[0].LBPhrase_Id;
        if (!id) return;
        var url = me.config.delUrl + '?id=' + id;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var loadIndex = layer.load();//显示遮罩层
            uxutil.server.ajax({
                url: url
            }, function (data) {
                layer.close(loadIndex);//隐藏遮罩层
                if (data.success === true) {
                    layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0, time: 2000 });
                    table.reload(me.config.id, {
                        url: me.getLoadUrl(),
                        height: me.config.height,
                        where: {
                            time: new Date().getTime()
                        }
                    });
                } else {
                    layer.msg("删除失败！", { icon: 5, anim: 6 });
                }
            });
        });
    }
    //清空表单
    Class.pt.clearForm = function () {
        var me = this;
        $("#LBPhrase_CName").val("");
        $("#LBPhrase_Shortcode").val("");
        $("#LBPhrase_PinYinZiTou").val("");
    }
    //过滤
    Class.pt.searchFun = function () {
        var me = this;
        var val = $("#search").val();
        if (val == "" && me.config.internalWhere == "") return;
        if (val == "") {
            me.config.internalWhere = "";
        } else {
            me.config.internalWhere = "CName like '%" + val + "%' or Shortcode like '%" + val + "%' or PinYinZiTou like '%" + val + "%'";
        }
        table.reload(me.config.id, {
            url: me.getLoadUrl(),
            height: me.config.height,
            where: {
                time: new Date().getTime()
            }
        });
    }
    //暴露接口
    exports('CommentPhraseTable', CommentPhraseTable);
});
