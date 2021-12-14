Ext.define("Shell.class.siteQuery.addEmpDept.select", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetDeptList?fields=CName,DeptNo',
    //pagingtoolbar:'basic',
    afterRender: function () {
        this.callParent(arguments);
    },

    initComponent: function () {
        var me = this;
        me.columns = [
            {
                dataIndex: 'DeptNo',
                text: '科室ID',
                width: 60,
                sortable: false
            },{
                dataIndex: 'CName',
                text: '科室名称',
                width: 100,
                sortable: false
            }
        ];
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            items: [
            { 
            	xtype: 'textfield',
            	name: 'CName', 
            	fieldLabel: '科室名称',
            	itemId:"QCNAME",
            	labelWidth: 70, 
            	width: 190,
            	value:''
            },
            {
                xtype: 'button', text: '查询',
                iconCls: 'button-search',
                listeners: {
                    click: function () {
                       var qcname= tooblar.getComponent("QCNAME").value;
                       if(qcname == '' || qcname == null){
                       		Shell.util.Msg.showWarning('请输入查询条件')
                       }else{
                       		var sql = "&Where=CName like '%"+qcname+"%'";
                       		me.selectUrl += sql;
                       		me.onSearch();
                       }
                    }
                }
            }]
        });
        //分页栏
        var pagingtoolbar = me.createPagingToolbar();
        return [pagingtoolbar,tooblar];
    },
});