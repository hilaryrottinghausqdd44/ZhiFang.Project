Ext.define("Shell.reportSetting.class.base.select.addList", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetAllSelectSetting',
    //pagingtoolbar:'basic',
    afterRender: function () {
        this.callParent(arguments);
    },

    initComponent: function () {
        var me = this;
        me.columns = [
            {
                dataIndex: 'CName',
                text: '列名',
                width: 50,
                sortable: false
            }, {
                dataIndex: 'SelectName',
                text: '字段名',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'TextWidth',
                text: '文字宽度',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'Width',
                text: '总体宽度',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'Type',
                text: '类型',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'SID',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'Xtype',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'Mark',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'Listeners',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }, {
                dataIndex: 'JsCode',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }
        ];
        me.callParent(arguments);
    }
});