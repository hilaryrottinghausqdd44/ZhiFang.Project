/**
 * 合同管理列表
 * @author Apollo
 * @version 2016-08-29
 */
Ext.define('Shell.class.contract.Grid', {
    extend: 'Shell.ux.grid.Panel',

    title: '合同管理列表',
    width: 800,
    height: 500,

    /**获取数据服务路径*/
    selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField',
    /**删除数据服务路径*/
    delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPContract',

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

    defaultOrderBy: [{ property: 'PContract_DispOrder', direction: 'ASC' }],

    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        //初始化检索监听
        me.on({
            itemdblclick: function (view, record) {
                var id = record.get(me.PKField);
                
                alert(id);
            }
        });
    },
    initComponent: function () {
        var me = this;

        //查询框信息
        me.searchInfo = {
            width: 220, emptyText: '客户名称', isLike: true,
            fields: ['pcontract.PClientName']
        };

        //数据列
        me.columns = me.createGridColumns();

        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            text: '合同编号', dataIndex: 'PContract_ContractNumber', width: 200,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '客户名称', dataIndex: 'PContract_PClientName', width: 200,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '合同名称', dataIndex: 'PContract_Name', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '项目类别', dataIndex: 'PContract_Content', width: 70,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '合同状态', dataIndex: 'PContract_ContractStatus', width: 70,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '合同总额', dataIndex: 'PContract_Amount', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '签署人', dataIndex: 'PContract_SignMan', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '销售负责人', dataIndex: 'PContract_Principal', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '申请人', dataIndex: 'PContract_ApplyMan', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '申请时间', dataIndex: 'PContract_ApplyDate', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '评审人', dataIndex: 'PContract_ReviewMan', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '评审时间', dataIndex: 'PContract_ReviewDate', width: 100,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '创建时间', dataIndex: 'PContract_DataAddTime', width: 130,
            isDate: true, hasTime: true
        }, {
            text: '备注', dataIndex: 'PContract_Memo', width: 150,
            sortable: false, menuDisabled: true, defaultRenderer: true
        }, {
            text: '主键ID', dataIndex: 'PContract_Id', isKey: true, hidden: true, hideable: false
        }];

        return columns;
    },
    /**@overwrite 新增按钮点击处理方法*/
    onAddClick: function () {
        var me = this;
        JShell.Win.open('Shell.class.contract.AddPanel', {
            title: '合同新增页面',
            resizable: false,
            FormConfig: {
            },
            listeners: {
                save: function (p, id) {
                    p.close();
                    me.onSearch();
                }
            }
        }).show();
    },
    /**@overwrite 编辑按钮点击处理方法*/
    onEditClick: function () {
        this.fireEvent('editclick', this);
    },
    /**修改*/
    openEditForm: function (id) {
        var me = this;
        JShell.Win.open('Shell.class.wfm.task.apply.EditPanel', {
            //resizable: false,
            TaskId: id,
            listeners: {
                save: function (p, id) {
                    p.close();
                    me.onSearch();
                }
            }
        }).show();
    }

});