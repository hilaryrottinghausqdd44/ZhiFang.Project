/**
 * 财务付款记录
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.FinanceReceiveGrid',{
	extend: 'Shell.ux.grid.Panel',
	title: '财务付款记录',
     /**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPFinanceReceiveByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePFinanceReceiveByField',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPFinanceReceive',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PFinanceReceive_ReceiveDate',
		direction: 'DESC'
	}],
		/**带功能按钮栏*/
	hasButtontoolbar: true,
		/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认加载数据*/
	defaultLoad: true,
//	/**付款单位ID*/
//	PayOrgID:null,
	defaultWhere:'pfinancereceive.IsUse=1',
	checkOne: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = [];
		  me.buttonToolbarItems = ['refresh','-',{
			xtype: 'label',
			text: '财务收款记录',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 0 10'
		}];	
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
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
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '收款单位',
			dataIndex: 'PFinanceReceive_PayOrgName',
			width: 180,
			sortable: false,
			defaultRenderer: true
//			hidden: true
		}, {
			text: '收款时间',
			dataIndex: 'PFinanceReceive_ReceiveDate',
			width: 100,
			sortable: true,
			defaultRenderer: true,
			isDate: true
		}, {
			text: '收款金额',
			dataIndex: 'PFinanceReceive_ReceiveAmount',
			width: 100,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '已分配金额',
			dataIndex: 'PFinanceReceive_SplitAmount',
			width: 100,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		},{
			text: '负责人',
			dataIndex: 'PFinanceReceive_InputerName',
			width: 70,
			sortable: false,
			defaultRenderer: true
//			hidden: true
		},{
			text: '编号',
			dataIndex: 'PFinanceReceive_Id',
			width: 170,
			isKey:true,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '付款单位',
			dataIndex: 'PFinanceReceive_PayOrgID',
			width: 150,
			sortable: false,
			defaultRenderer: true,
			hidden: true
		},{
			text: '执行公司Id',
			dataIndex: 'PFinanceReceive_CompnameID',
			width: 170,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '执行公司',
			dataIndex: 'PFinanceReceive_ComponeName',
			width: 150,
			sortable: false,
			defaultRenderer: true,
			hidden: true
		}];
		return columns;
	},
		/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
        me.load(null, true, autoSelect);
	},
	/**@overwrite 改变返回的数据
	 * 显示没有分配完成的财务收款记录
	 * */
	changeResult: function(data) {
		var list = [],
		result = {};
		if(data.value) {
			var redata = data.value.list;
            var ReceiveAmount=0,SplitAmount=0;
			for(var i = 0; i < redata.length; i++) {
				
				//收款金额
				ReceiveAmount=redata[i].PFinanceReceive_ReceiveAmount;
				//已分配金额
				SplitAmount=redata[i].PFinanceReceive_SplitAmount;
				if(parseFloat(ReceiveAmount) > parseFloat(SplitAmount)) {
					list.push(redata[i]);
				}
			}
			result.list = list;
		}
		return result;
	}
});