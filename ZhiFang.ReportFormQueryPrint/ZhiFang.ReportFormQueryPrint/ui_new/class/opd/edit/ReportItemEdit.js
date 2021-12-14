Ext.define("Shell.class.opd.edit.ReportItemEdit", {
     extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    records:'',//选中列数据
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/ReportFormService.svc/GetReportItemFullByReportFormID?ReportFormID=',
    celledit:function () {
        return Ext.create('Ext.grid.plugin.CellEditing', {
            clickToEdit: 1
        });
    },
    afterRender: function () {
        this.callParent(arguments);
    },
	createStore:function(){
		var me = this,
			url = me.selectUrl,
			type = me.pagingtoolbar,
			data = me.data,
			config = {};
		config.fields = me.getStoreFields();
		
			config.proxy = {
				type:'ajax',
				url:'',
				reader:{type:'json',totalProperty:'count',root:'list'},
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText),
						success = result.success;
					if (!success && me.showErrorInfo) { me.showError(result.ErrorInfo); }
					return me.responseToList(response);
				}
			};
			config.listeners = {//数据集监听
			    beforeload:function(){return me.onBeforeLoad();},
			    load:function(store,records,successful){
			    	me.fireEvent('afterload',me,records || [],successful);
			    	me.onAfterLoad(records,successful);
			    }
			};
		if(type == 'basic'){
			config.pageSize = me.infinityPageSize;
		}else if(type == 'number' || type == 'sliding' || type == 'progressbar' || type == 'simple'){
			config.pageSize = me.defaultPageSize;
			config.remoteSort = me.remoteSort;
		}
			
		return Ext.create('Ext.data.Store',config);
	},
    initComponent: function () {
        var me = this;
        me.plugins = [me.celledit()];
        me.selectUrl += me.records.ReportFormID;
        me.columns = [
            {
                dataIndex: 'ReportPublicationID',
                text: 'ReportPublicationID',
                sortable: false
            },{
                dataIndex: 'SectionNo',
                text: 'SectionNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TestTypeNo',
                text: 'TestTypeNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'SampleNo',
                text: 'SampleNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'OrderNo',
                text: 'OrderNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ParItemNo',
                text: 'ParItemNo',
                sortable: false
            }, {
                dataIndex: 'ItemNo',
                text: 'ItemNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ParitemName',
                text: 'ParitemName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ItemCname',
                text: 'ItemCname',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ItemEname',
                text: 'ItemEname',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ReportValue',
                text: 'ReportValue',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ReportDesc',
                text: 'ReportDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ItemValue',
                text: 'ItemValue',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ItemUnit',
                text: 'ItemUnit',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ResultStatus',
                text: 'ResultStatus',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'RefRange',
                text: 'RefRange',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ItemDesc',
                text: 'ItemDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY1',
                text: 'ZDY1',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY2',
                text: 'ZDY2',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY3',
                text: 'ZDY3',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY4',
                text: 'ZDY4',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY5',
                text: 'ZDY5',
                sortable: false,
                editor: true
            }         
        ];
        me.callParent(arguments);
    }
});