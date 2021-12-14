Ext.define("Shell.class.opd.edit.ReportMarrowEdit", {
     extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    records:'',//选中列数据
    /**获取列表数据服务*/
    selectUrl: '/ServiceWCF/ReportFormService.svc/GetReportMarrowFullByReportFormID?ReportFormID=',
    
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
            },{
                dataIndex: 'ParItemNo',
                text: 'ParItemNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ItemNo',
                text: 'ItemNo',
                sortable: false
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
            },  {
                dataIndex: 'BloodDesc',
                text: 'BloodDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'MarrowNum',
                text: 'MarrowNum',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'MarrowPercent',
                text: 'MarrowPercent',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'MarrowDesc',
                text: 'MarrowDesc',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'RefRange',
                text: 'RefRange',
                sortable: false,
                editor: true
            },  {
                dataIndex: 'EquipName',
                text: 'EquipName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ResultStatus',
                text: 'ResultStatus',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'DiagMethod',
                text: 'DiagMethod',
                sortable: false,
                editor: true
            }      
        ];
        me.callParent(arguments);
    }
});