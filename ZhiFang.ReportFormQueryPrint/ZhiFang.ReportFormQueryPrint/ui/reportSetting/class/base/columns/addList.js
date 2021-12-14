Ext.define("Shell.reportSetting.class.base.columns.addList", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetAllColumnsSetting',
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
                dataIndex: 'ColumnName',
                text: '字段名',
                width: 120,
                sortable: false
            }, {
                dataIndex: 'Width',
                text: '宽度',
                width: 40,
                sortable: false
            },{
                dataIndex: 'Render',
                text: '自定义方法',
                width: 80,
                sortable: false
            }, {
                dataIndex: 'Type',
                text: '类型',
                width: 60,
                sortable: false
            }, {
                dataIndex: 'ColID',
                text: '类型',
                width: 60,
                sortable: false,
                hidden: true
            }
        ];
        me.callParent(arguments);
    }
});