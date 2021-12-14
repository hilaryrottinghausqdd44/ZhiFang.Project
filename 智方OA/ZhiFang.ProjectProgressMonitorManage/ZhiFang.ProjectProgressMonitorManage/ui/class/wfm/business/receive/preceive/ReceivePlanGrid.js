/**
 * 收款计划
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.ReceivePlanGrid',{
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.Grid',
    title: '收款计划',
    /**带功能按钮栏*/
	hasButtontoolbar: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**付款单位ID*/
	PayOrgID:null,
	PayOrgName:'',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PReceivePlan_ExpectReceiveDate',
		direction: 'DESC'
	}],
	defaultWhere:'preceiveplan.UnReceiveAmount>0 and preceiveplan.Status =3',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	/**默认加载数据*/
	defaultLoad: false,
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
		buttonToolbarItems =  [{
			xtype: 'label',
			text: '收款计划',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 0 10'
		}];
		return buttonToolbarItems;
	},
		/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if (items.length == 0) {
			if (me.hasRefresh) items.push('refresh');
			if (me.hasAdd) items.push('add');
			if (me.hasEdit) items.push('edit');
			if (me.hasDel) items.push('del');
			if (me.hasShow) items.push('show');
			if (me.hasSave) items.push('save');
			if (me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
		    height:26,
//		    border:false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '合同',
			dataIndex: 'PReceivePlan_PContractName',
			width: 150,
			sortable: false,
			defaultRenderer: true
		},{
			text: '用户',
			dataIndex: 'PReceivePlan_PClientName',
			width: 150,
			sortable: false,
			defaultRenderer: true
		},{
			text: '合同总额',
			dataIndex: 'PReceivePlan_PContract_Amount',
			width: 80,
			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '计划收款内容',
			dataIndex: 'PReceivePlan_ReceiveGradationName',
			width: 100,
			sortable: false,
	    	defaultRenderer: true
		},  {
			text: '计划收款时间',
			dataIndex: 'PReceivePlan_ExpectReceiveDate',
			width: 85,
			sortable: false,
			menuDisabled: false,
			type: 'date',
			xtype: 'datecolumn',
			format: 'Y-m-d',
			editor: {
				xtype: 'datefield',
				allowBlank: false,
				format: 'Y-m-d'
			}
		}, {
			text: '金额',
			dataIndex: 'PReceivePlan_ReceivePlanAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '已收',
			dataIndex: 'PReceivePlan_ReceiveAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '未收',
			dataIndex: 'PReceivePlan_UnReceiveAmount',
			width: 80,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		},{
			text: '合同id',
			dataIndex: 'PReceivePlan_PContractID',
			width: 100,
			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '合同',
			dataIndex: 'PReceivePlan_PContractName',
			width: 100,
			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '客户ID',
			dataIndex: 'PReceivePlan_PClientID',
			width: 100,
			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '收款负责人',
			dataIndex: 'PReceivePlan_ReceiveManID',
			width: 100,
			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '负责人',
			dataIndex: 'PReceivePlan_ReceiveManName',
			width: 70,
//			hidden:true,
			sortable: false,
			defaultRenderer: true
		},{
			text: 'Id',
			dataIndex: 'PReceivePlan_Id',
			width: 100,
			hidden:true,
			sortable: false,
			isKey:true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
        me.load(null, true, autoSelect);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			PClient = null,
			search = null,
			params = [];
		//付款单位
		if(me.PayOrgID) {
			params.push("preceiveplan.PayOrgID='" + me.PayOrgID + "'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	}
});