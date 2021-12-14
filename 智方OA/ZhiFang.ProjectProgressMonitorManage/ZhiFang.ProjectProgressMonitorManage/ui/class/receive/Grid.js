/**
 * 收款计划列表
 * @author Apollo
 * @version 2016-08-29
 */
Ext.define('Shell.class.receive.level.Grid', {
    extend: 'Shell.ux.grid.Panel',

    title: '平台客户级别列表',
    width: 800,
    height: 500,

    /**获取数据服务路径*/
    selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlanByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePReceivePlanByField',
    /**删除数据服务路径*/
    delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPReceivePlan',

    /**显示成功信息*/
    showSuccessInfo: false,
    /**消息框消失时间*/
    hideTimes: 3000,

    /**默认加载*/
    defaultLoad: true,
    /**默认每页数量*/
    defaultPageSize: 50,

    /**不加载时默认禁用功能按钮*/
    defaultDisableControl: true,
    /**后台排序*/
    remoteSort: true,
    /**带分页栏*/
    hasPagingtoolbar: true,
    /**带功能按钮栏*/
    hasButtontoolbar: true,
    /**是否启用序号列*/
    hasRownumberer: true,

    /**复选框*/
    multiSelect: true,
    selType: 'checkboxmodel',

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用新增按钮*/
    hasAdd: true,
    /**是否启用删除按钮*/
    hasDel: true,
    /**是否启用查询框*/
    hasSearch: true,

    /**查询栏参数设置*/
    searchToolbarConfig: {},

    defaultOrderBy: [{ property: 'SServiceClientlevel_DispOrder', direction: 'ASC' }],

    afterRender: function () {
        var me = this;
        me.callParent(arguments);
    },
    initComponent: function () {
        var me = this;

        //查询框信息
        me.searchInfo = {
            width: 220, emptyText: '名称', isLike: true,
            fields: ['sserviceclientlevel.Name']
        };

        //数据列
        me.columns = me.createGridColumns();

        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;

        var me = this;
        var columns = [{
            text: '名称', dataIndex: 'SServiceClientlevel_Name', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '简称', dataIndex: 'SServiceClientlevel_SName', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '编码', dataIndex: 'SServiceClientlevel_编码', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '快捷码', dataIndex: 'SServiceClientlevel_Shortcode', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '汉字拼音字头', dataIndex: 'SServiceClientlevel_PinYinZiTou', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '创建时间', dataIndex: 'SServiceClientlevel_DataAddTime', width: 130,
            isDate: true, hasTime: true
        }, {
            text: '备注', dataIndex: 'SServiceClientlevel_Memo', width: 150,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '次序', dataIndex: 'SServiceClientlevel_DispOrder', width: 50,
            defaultRenderer: true, align: 'center', type: 'int'
        }, {
            text: '主键ID', dataIndex: 'SServiceClientlevel_Id', isKey: true, hidden: true, hideable: false
        }];

        return columns;
    },
    /**@overwrite 新增按钮点击处理方法*/
    onAddClick: function () {
        this.fireEvent('addclick', this);
    },
    /**@overwrite 编辑按钮点击处理方法*/
    onEditClick: function () {
        this.fireEvent('editclick', this);
    }
});