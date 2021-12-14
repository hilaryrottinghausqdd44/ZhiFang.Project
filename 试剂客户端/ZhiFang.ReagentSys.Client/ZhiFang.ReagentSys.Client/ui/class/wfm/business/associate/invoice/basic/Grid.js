/**
 * 发票列表
 * @author liangyl	
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.invoice.basic.Grid', {
    extend: 'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title: '发票列表',
    width: 800,
    height: 500,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPInvoiceByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPInvoice',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePInvoiceByField',

    /**默认加载*/
    defaultLoad: true,

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
    IsCheckShow:false,
    defaultOrderBy: [{ property: 'PInvoice_DispOrder', direction: 'ASC' }],
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initFilterListeners();
        me.on({
            itemdblclick: function (view, record) {
                var id = record.get(me.PKField);
                me.openShowForm(id);//查询信息
            }
        });
    },
    initComponent: function () {
        var me = this;
	   
	    me.buttonToolbarItems = me.createButtonToolbarItems();

        //数据列
        me.columns = me.createGridColumns();
		
		//创建数据集
//		me.store = me.createStore();
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [ {
			text: '开票日期',
			dataIndex: 'PInvoice_InvoiceDate', isDate: true, width: 85,
			hideable: false,defaultRenderer: true
		}, {
			text: '金额',
			dataIndex: 'PInvoice_InvoiceAmount', width: 55,
			hideable: false,defaultRenderer: true
		},{
			text: '主键ID',
			dataIndex: 'PInvoice_Id',
			isKey: true,
			hidden: true,
			hideable: false
		},{
		    text: '类型',
		    dataIndex: 'PInvoice_InvoiceTypeName',
			width: 135,
			sortable: false,
			menuDisabled: false
		}, {
		    text: '备注',
		    dataIndex: 'PInvoice_InvoiceMemo',
		    width: 150,
		    sortable: false,
		    menuDisabled: false
		}, {
		    text: '内容',
		    dataIndex: 'PInvoice_InvoiceContentTypeName',
		    width: 35,
		    sortable: false,
		    menuDisabled: false
		}, {
			text: '客户ID',
			dataIndex: 'PInvoice_ClientID',
			width:70,
			sortable: true,
//			hidden:true,
			menuDisabled: false
		},{
			text: '客户',
			dataIndex: 'PInvoice_ClientName',
			width:120,
			sortable: true,
			menuDisabled: false
		},{
			text: '付款单位ID',
			dataIndex: 'PInvoice_PayOrgID',
			width:70,
			sortable: true,
//			hidden:true,
			menuDisabled: false
		}, {
			text: '付款单位',
			dataIndex: 'PInvoice_PayOrgName',
			//flex: 1,
			width:120,
			sortable: true,
			menuDisabled: false
		} ,{
			text: '对比人',
			dataIndex: 'PInvoice_ContrastCName',
			width:80,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		},  {
			text: '对比人Id',
			dataIndex: 'PInvoice_ContrastId',
			hidden: true,
			hideable: false
		}, {
			text: '审核人Id',
			dataIndex: 'PInvoice_CheckId',
			hidden: true,
			hideable: false
		},  {
			text: '发票类型',
			dataIndex: 'PInvoice_PInvoice_InvoiceTypeName',
			//flex: 1,
			width:100,
//			hidden:true,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		},{
			text: '项目类别',
			dataIndex: 'PInvoice_ProjectTypeName',
			width:80,
//			hidden:true,
			sortable: false,
			menuDisabled: false,
			defaultRenderer: true
		}];

        return columns;
    },
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			InvoiceTypeID=null,
			IsContras=false,
			IsCheck=false,
			IsPayOrg=false,
			params = [];
			
		me.internalWhere = '';

		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
			InvoiceTypeID = buttonsToolbar.getComponent('InvoiceTypeID').getValue();
			if(me.IsCheckShow){
				 IsCheck= buttonsToolbar.getComponent('IsCheck').getValue();
			}
            IsClient= buttonsToolbar.getComponent('IsClient').getValue();
            IsPayOrg= buttonsToolbar.getComponent('IsPayOrg').getValue();

		}
//		//省份
//		if(ProvinceID){
//			params.push("pinvoice.ProvinceID='" + ProvinceID + "'");
//		}
		if(InvoiceTypeID){
			params.push("pinvoice.InvoiceTypeID='" + InvoiceTypeID + "'");
		}


	var tempWhere = "";

		tempWhere = tempWhere + (IsClient == false ? " or pinvoice.ClientID is null" : " or pinvoice.ClientID is not null");

		tempWhere = tempWhere + (IsPayOrg == false ? " or pinvoice.PayOrgID is null" : " or pinvoice.PayOrgID is not null");
		
		if(me.IsCheckShow) {
			tempWhere = tempWhere + (IsCheck == false ? " or pinvoice.CheckId is null" : " or pinvoice.CheckId is not null");
		}

		if(tempWhere.length > 0) {
			tempWhere = tempWhere.substring(3, tempWhere.length);
			params.push("(" + tempWhere + ")");
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	},
		/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		//查询框信息
		me.searchInfo = {
			width:150,emptyText:'客户名称/付款单位',isLike:true,itemId:'search',
			fields:['pinvoice.PayOrgName','pinvoice.ClientName']
		};
		buttonToolbarItems.unshift('refresh', '-',{
			width:160,labelWidth:55,labelAlign:'right',emptyText:'发票类型',
			hidden:true,
			xtype:'uxCheckTrigger',itemId:'InvoiceTypeName',fieldLabel:'发票类型',
			className:'Shell.class.wfm.business.invoice.basic.DictCheckGrid'
		},{
			xtype:'textfield',itemId:'InvoiceTypeID',fieldLabel:'发票类型主键ID',hidden:true
		},{
			xtype: 'checkbox',
			boxLabel: '已对比客户',
			itemId: 'IsClient',
			checked: false,
			labelSeparator: '',
			labelWidth: 0,
			height:20,
			width: 90,
			style: {
				marginLeft: '5px'
			},
			listeners: {
				change: function(field, newValue, oldValue) {
					
				}
			}
		},{
			xtype: 'checkbox',
			boxLabel: '已对比付款单位',
			itemId: 'IsPayOrg',
			checked: false,
			labelSeparator: '',
			labelWidth: 0,
			height:20,
			width: 110,
			listeners: {
				change: function(field, newValue, oldValue) {
					
				}
			}
		});
		if(me.IsCheckShow){
			buttonToolbarItems.push({
				xtype: 'checkbox',
				boxLabel: '已审核',
				itemId: 'IsCheck',height:20,
				checked: false,
				listeners: {
					change: function(field, newValue, oldValue) {
						
					}
				}
			})
		};
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		
		return buttonToolbarItems;
	},
	/**监听*/
	initFilterListeners:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');

		var InvoiceTypeName = buttonsToolbar.getComponent('InvoiceTypeName'),
			InvoiceTypeID = buttonsToolbar.getComponent('InvoiceTypeID');
		
		if(!InvoiceTypeName) return;
		
		InvoiceTypeName.on({
			check: function(p, record) {
				InvoiceTypeName.setValue(record ? record.get('BDict_CName') : '');
				InvoiceTypeID.setValue(record ? record.get('BDict_Id') : '');
				me.onSearch();
				p.close();
			}
		});
		var  IsCheck= buttonsToolbar.getComponent('IsCheck');
		var  IsClient= buttonsToolbar.getComponent('IsClient');
		var  IsPayOrg= buttonsToolbar.getComponent('IsPayOrg');
		if(me.IsCheckShow){
	        IsCheck.on({
	        	change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
	        });
        }
        IsClient.on({
        	change: function(com, newValue, oldValue, eOpts) {
				me.onSearch();
			}
        });
        IsPayOrg.on({
        	change: function(com, newValue, oldValue, eOpts) {
				me.onSearch();
			}
        });
		
	},
	/**发票查看*/
	openShowForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.invoice.basic.ContentPanel', {
			resizable: true,
			width:800,
			height:410,
			PK: id
		}).show();
	}
});