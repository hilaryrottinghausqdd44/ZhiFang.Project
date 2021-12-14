Ext.define("Shell.class.opd.edit.ReportMicroEdit", {
     extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    records:'',//选中列数据
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/ReportFormService.svc/GetReportMicroFullByReportFormID?ReportFormID=',
    
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
                dataIndex: 'OrderNo',
                text: 'OrderNo',
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
                dataIndex: 'ItemNo',
                text: 'ItemNo',
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
                dataIndex: 'DescNo',
                text: 'DescNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'DescName',
                text: 'DescName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'MicroStepName',
                text: 'MicroStepName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ReportValue',
                text: 'ReportValue',
                sortable: false
            }, {
                dataIndex: 'MicroName',
                text: 'MicroName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'MicroEame',
                text: 'MicroEame',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'MicroDesc',
                text: 'MicroDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'MicroResultDesc',
                text: 'MicroResultDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ItemDesc',
                text: 'ItemDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'AntiName',
                text: 'AntiName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'AntiEName',
                text: 'AntiEName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'Suscept',
                text: 'Suscept',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'SusQuan',
                text: 'SusQuan',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'SusDesc',
                text: 'SusDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'AntiUnit',
                text: 'AntiUnit',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'RefRange',
                text: 'RefRange',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ResultState',
                text: 'ResultState',
                sortable: false,
                editor: true
            },  {
                dataIndex: 'EquipName',
                text: 'EquipName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'CheckType',
                text: 'CheckType',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF1',
                text: 'PYJDF1',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF2',
                text: 'PYJDF2',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF3',
                text: 'PYJDF3',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF4',
                text: 'PYJDF4',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF5',
                text: 'PYJDF5',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF6',
                text: 'PYJDF6',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF7',
                text: 'PYJDF7',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF8',
                text: 'PYJDF8',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF9',
                text: 'PYJDF9',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF10',
                text: 'PYJDF10',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF11',
                text: 'PYJDF11',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF12',
                text: 'PYJDF12',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF13',
                text: 'PYJDF13',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF14',
                text: 'PYJDF14',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF15',
                text: 'PYJDF15',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF16',
                text: 'PYJDF16',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF17',
                text: 'PYJDF17',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF18',
                text: 'PYJDF18',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF19',
                text: 'PYJDF19',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PYJDF20',
                text: 'PYJDF20',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF1',
                text: 'TPF1',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF2',
                text: 'TPF2',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF3',
                text: 'TPF3',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF4',
                text: 'TPF4',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF5',
                text: 'TPF5',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF6',
                text: 'TPF6',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF7',
                text: 'TPF7',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF8',
                text: 'TPF8',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF9',
                text: 'TPF9',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TPF10',
                text: 'TPF10',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TestComment',
                text: 'TestComment',
                sortable: false,
                editor: true
            }          
        ];
        me.callParent(arguments);
    }
});