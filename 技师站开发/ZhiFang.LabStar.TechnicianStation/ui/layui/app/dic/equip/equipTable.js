/**
	@name：仪器列表
	@author：zhangda
	@version 2019-08-14
 */
layui.extend({
    uxtable: 'ux/table'
}).define(['table', 'uxtable'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        uxtable = layui.uxtable;
    //小组列表功能参数配置
    var config = {
        //获取小组列表服务路径
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true',
        delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBEquip',
        elem: ''
    };
    var equipTable = {
        config: {
            //小组id，用于判断定位选择行
            PK: null,
            delIndex: null
        },
        //核心入口
        render: function (options) {
            var me = this;
            options.url = config.selectUrl;
            config.elem = options.id;
            var table_options = {
                elem: options.elem,
                id: options.id,
                toolbar: '',
                page: false,
                limit: 1000,
                //              limits: [10,50, 100, 200, 500, 1000],
                autoSort: true, //禁用前端自动排序
                loading: true,
                size: 'sm', //小尺寸的表格
                height: options.height ? options.height : 'full-220',
                cols: [[
                    { type: 'numbers', title: '行号' },
                    { field: 'LBEquip_Id', width: 60, title: '主键ID', sort: true, hide: true },
                    { field: 'LBEquip_EquipNo', minWidth: 80, title: '编码', sort: true },
                    { field: 'LBEquip_CName', minWidth: 150, flex: 1, title: '名称', sort: true },
                    { field: 'LBEquip_SName', width: 150, title: '简称', sort: true },
                    { field: 'LBEquip_EName', width: 150, title: '英文名称', sort: true },
                    { field: 'LBEquip_Shortcode', width: 150, title: '快捷码', sort: true },
                    { field: 'LBEquip_Computer', width: 150, title: '计算机', sort: true },
                    { field: 'LBEquip_ComProgram', width: 150, title: '程序名', sort: true },
                    { field: 'LBEquip_DispOrder', width: 100, title: '显示次序', sort: true, hide: true },
                    { field: 'LBEquip_LBSection_Id', width: 100, title: '检验小组', sort: true, hide: true }
                ]],
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
                    if (count == 0) $(".layui-table-main").html('<div class="layui-none">暂无相关数据</div>');
                    
                    //默认选择第一行
                    var rowIndex = 0;
                    for (var i = 0; i < res.data.length; i++) {
                        if (res.data[i].LBEquip_Id == equipTable.config.PK) {
                            rowIndex = res.data[i].LAY_TABLE_INDEX;
                            break;
                        }
                    }
                    if (config.delIndex != null) {
                        rowIndex = config.delIndex;
                        config.delIndex = null;
                    }
                    //默认选择第一行
                    doAutoSelect(table_options, rowIndex);
                    layui.event(options.id, "done", { res: res, count: count });
                },
                text: { none: '暂无相关数据' }
            };
            //标题
            if (options.title) {
                table_options.title = options.title;
            }
            if (options.url) {
                var fields = getStoreFields(table_options, true);
                table_options.url = options.url + '&fields=' + fields;
                table_options.initSort = options.initSort;
                if (options.defaultOrderBy) {
                    table_options.url += '&sort=' + options.defaultOrderBy;
                }
            }
            if (options.defaultToolbar) table_options.defaultToolbar = options.defaultToolbar;
            return uxtable.render(table_options);
        }
    };

    /**创建数据字段*/
    var getStoreFields = function (tableId, isString) {
        var columns = tableId.cols[0] || [],
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
    /***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
    var doAutoSelect = function (curTable, rowIndex) {
        curTable.key = curTable.id;
        var data = table.cache[curTable.key] || [];
        if (!data || data.length <= 0) return;
        rowIndex = rowIndex || 0;
        var tableDiv = $(curTable.elem + "+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
        var filter = $(curTable.elem).find('lay-filter');
        var obj = {
            tr: thatrow,
            data: data[rowIndex] || {},
            del: function () {
                table.cache[curTable.key][index] = [];
                tr.remove();
                curTable.scrollPatch();
            },
            updte: {}
        };
        layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
        thatrow.click();
    };

    //数据加载-对外公开 
    equipTable.onSearch = function (where) {
        var me = this;
        table.reload(config.elem, {
            where: {
                where: where
            }
        });
    };
    //设置删除数据的所在位置-删除定位
    equipTable.setDelIndex = function () {
        var me = this;
        config.delIndex = Number($(config.elem + "+div .layui-table-body table.layui-table tbody tr.layui-table-click").attr("data-index"));
    };
    //删除方法 -对外公开 
    equipTable.onDelClick = function (checkRowData) {
        var me = this;
        if (checkRowData.length == 0) {
            layer.msg('请选择一行！');
        } else {
            var id = checkRowData[0].LBEquip_Id;
            var url = config.delUrl + '?id=' + id;
            layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
                uxutil.server.ajax({
                    url: url
                }, function (data) {
                    layer.closeAll('loading');
                    if (data.success === true) {
                        layer.close(index);
                        layer.msg("删除成功！", { icon: 6, anim: 0, time: 2000 });
                        config.checkRowData = {};
                        //刷新数据
                        table.reload('table', {});
                    } else {
                        //layer.msg(data.result[0].ErrorInfo);
                        layer.msg("删除失败！", { icon: 5, anim: 6 });
                    }
                });
            });
        }
    };
    //暴露接口
    exports('equipTable', equipTable);
});