Ext.define("Shell.class.opd.edit.ReportFormEdit", {
     extend: 'Shell.ux.grid.Panel',
    /**默认选中第一行*/
    autoSelect: false,
    /**默认加载数据*/
    defaultLoad: true,
    records:'',//选中列数据
    /**获取列表数据服务*/
    selectUrl: "/ServiceWCF/ReportFormService.svc/GetReportFormFullByReportFormID?ReportFormID=",
    celledit:function () {
        return Ext.create('Ext.grid.plugin.CellEditing', {
            clickToEdit: 1
        });
    },
    afterRender: function () {
    	var me = this;
        me.callParent(arguments);
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
                dataIndex: 'SectionName',
                text: 'SectionName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'TestTypeName',
                text: 'TestTypeName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'SampleTypeNo',
                text: 'SampleTypeNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'SampletypeName',
                text: 'SampletypeName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'SecretType',
                text: 'SecretType',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'PatNo',
                text: 'PatNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'CName',
                text: 'CName',
                sortable: false,
                editor: true
            },{
                dataIndex: 'GenderNo',
                text: 'GenderNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'GenderName',
                text: 'GenderName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'Age',
                text: 'Age',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'AgeUnitNo',
                text: 'AgeUnitNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'AgeUnitName',
                text: 'AgeUnitName',
                sortable: false,
                editor: true
            },  {
                dataIndex: 'DistrictNo',
                text: 'DistrictNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'DistrictName',
                text: 'DistrictName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'WardNo',
                text: 'WardNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'WardName',
                text: 'WardName',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'Bed',
                text: 'Bed',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'DeptNo',
                text: 'DeptNo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'DeptName',
                text: 'DeptName',
                sortable: false,
                editor: true
            },{
                dataIndex: 'SerialNo',
                text: 'SerialNo',
                sortable: false,
                editor: true
            },{
                dataIndex: 'FormComment',
                text: 'FormComment',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'FormMemo',
                text: 'FormMemo',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'SickTypeNo',
                text: 'SickTypeNo',
                sortable: false,
                editor: true
            },{
                dataIndex: 'SickTypeName',
                text: 'SickTypeName',
                sortable: false,
                editor: true
            },{
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
            }, {
                dataIndex: 'ZDY6',
                text: 'ZDY6',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY7',
                text: 'ZDY7',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY8',
                text: 'ZDY8',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY9',
                text: 'ZDY9',
                sortable: false,
                editor: true
            }, {
                dataIndex: 'ZDY10',
                text: 'ZDY10',
                sortable: false,
                editor: true
            },{
                dataIndex: 'ZDY11',
                text: 'ZDY11',
                sortable: false,
                editor: true
            }       
        ];
        me.callParent(arguments);
    }
});