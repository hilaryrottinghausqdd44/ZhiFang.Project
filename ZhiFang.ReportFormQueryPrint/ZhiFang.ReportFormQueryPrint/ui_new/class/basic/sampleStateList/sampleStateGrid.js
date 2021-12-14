Ext.define("Shell.class.basic.sampleStateList.sampleStateGrid", {
    extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: false,
    appType: '',
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/ReportFormService.svc/SampleStateTailAfter',
    multiSelect: true,
    pagingtoolbar: 'number',
    /**默认每页数量*/
    defaultPageSize: 20,
    layout: 'fit',
    //selModel: new Ext.selection.CheckboxModel({ checkOnly: true }),
    initComponent: function () {
        var me = this;
        me.columns = [
             {
                dataIndex: 'State',
                text: '状态',
                width: 100,
                sortable: false
            },{
                dataIndex: 'Operator',
                editor: true,
                text: '操作人员',
                width: 100,
                sortable: false
            }, {
                dataIndex: 'OperatorTime',
                editor: true,
                text: '操作时间',
                width: 100,
                sortable: false
            }, {
                dataIndex: 'Explain',
                editor: true,
                text: '说明',
                width: 100,
                sortable: false,
                renderer:function (v, meta, record) {
					var result = '';
					if(v == "已完成"){
						meta.style="background-color:#33CC52";
						result = v;
					}else{
						meta.style="background-color:red";
						result = v;
					}
					return result;
				}
            },{
                dataIndex: 'Comment',
                text: '备注',
                editor: true,
                width: 100,
                sortable: false
            }
        ];
        me.callParent(arguments);
    }
    
    
});