/**
	@name：医院列表
	@author：guohaixiang
	@version 2019-12-12
 */
layui.extend({
}).define(['table', 'form', 'uxutil'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        form = layui.form,
        uxtable = layui.uxtable;

    var config = { RuleType: null };

    var HospitalTable = {
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
            /*sort: [{
                "property": 'LBQCRuleBase_DataAddTime',
                "direction": 'DESC'
            }],*/
            selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true',
            where: "",
            defaultToolbar: ['filter'],
            toolbar: '#toolbar',
            page: true,
            limit: 50,
            limits: [50, 100, 200, 500, 1000],
            autoSort: false, //禁用前端自动排序
            loading: false,
            //size: 'sm', //小尺寸的表格
            cols: [[
                { type: 'numbers', title: '行号', fixed: 'left' },
                { field: 'BHospital_Id', width: 150, title: '主键', sort: true, hide: true },
                { field: 'BHospital_Name', width: 120, title: '医院名称', sort: true },
                { field: 'BHospital_EName', Width: 120, title: '英文名称', sort: true },
                { field: 'BHospital_HospitalCode', Width: 120, title: '医院编码', sort: true }
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
                if (count == 0) {
                    //layui.event("", "data", {  });
                    return;
                }
                //触发点击事件
                $("#HospitalTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();

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
        me.config = $.extend({}, HospitalTable.config, me.config, options);
        if (me.config.defaultLoad) me.config.url = me.getLoadUrl();
        me = $.extend(true, {}, HospitalTable, Class.pt, me);// table,
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
    HospitalTable.render = function (options) {
        var me = this;
        config.RuleType = $("#RuleType").val();
        var inst = new Class(options);
        inst.config.url = inst.getLoadUrl();
        inst.tableIns = table.render(inst.config);
        //监听
        inst.iniListeners();
        return inst;
    };
    //对外公开-数据加载
    HospitalTable.onSearch = function (mytable, RuleType) {
        var me = this;
        config.RuleType = RuleType;
        var inst = new Class(me);
        HospitalTable.url = inst.getLoadUrl();
        HospitalTable.elem = "#" + mytable;
        HospitalTable.id = mytable;
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
    Class.pt.iniListeners = function () {
        var me = this;
        //监听行单击事件
        table.on('row(HospitalTable)', function (obj) {
            me.config.checkRowData = [];
            me.config.checkRowData.push(obj.data);
            //标注选中样式
            obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
            layui.event("HospitalTableClick", "click", { id: obj.data.BHospital_Id,name:obj.data.BHospital_Name });
        });
        //监听排序事件
        table.on('sort(HospitalTable)', function (obj) {
            var field = obj.field;//排序字段
            var type = obj.type;//升序还是降序
            //修改默认的排序字段
            me.config.sort = [{
                "property": field,
                "direction": type
            }];
            table.reload('HospitalTable', {
                initSort: obj, //记录初始排序，如果不设的话，将无法标记表头的排序状态
                url: me.getLoadUrl(),
                where: {
                    time: new Date().getTime()
                }
            });
        });
    };
    //table上面的工具栏
	table.on('toolbar(HospitalTable)', function(obj) {
		switch(obj.event) {			
			case 'search':
				if($("#search")[0].value == ""){
					table.reload('table',{
                        url: HospitalTable.selectUrl,
                        where: {
                            time: new Date().getTime()
                        }
					});
				}else{
					var val = $("#search")[0].value;
					var url = "";
                    if (HospitalTable.config.selectUrl.indexOf("where") != -1) {
                        var where = " and Name like '%" + val + "%' or EName like '%" + val + "%' or HospitalCode like '%" + val + "%' ";
                        url = HospitalTable.config.selectUrl.replace(')', where);
                    } else {
                        url = encodeURI(HospitalTable.config.selectUrl + "&where=Name like '%" + val + "%' or EName like '%" + val + "%' or HospitalCode like '%" + val + "%' ");
                    }
					table.reload('HospitalTable',{
                        url: url,
                        where: {
                            time: new Date().getTime()
                        }
					});
					$("#search").val(val);
				}
		};
	});
    //暴露接口
    exports('HospitalTable', HospitalTable);
});
