/**
 * 供货总单列表-基础
 * @author Jcall
 * @version 2017-02-17
 */
Ext.define('Shell.class.rea.sale.basic.DocGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '供货总单列表-基础',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true',
	/**获取数据服务路径*/
	selectDtlUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDoc',
	/**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocByField',
	
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenSaleDoc_OperDate',direction:'DESC'}],
	
	/**默认单据状态*/
	defaultStatusValue:0,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.defaultWhere = me.defaultWhere;
		if(me.defaultWhere){
			me.defaultWhere += ' and ';
		}
		me.defaultWhere += '(bmscensaledoc.DeleteFlag=0 or bmscensaledoc.DeleteFlag is null)';
		
		//查询框信息
		me.searchInfo = me.searchInfo || {
			width: 160,
			emptyText: '供货单号',
			itemId:'search',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo']
		};
		me.buttonToolbarItems = me.buttonToolbarItems || ['refresh','-',{
			fieldLabel:'单据状态',xtype:'uxSimpleComboBox',
			itemId:'status',allowBlank:false,value:me.defaultStatusValue,
			width:140,labelWidth:55,labelAlign:'right',hasStyle:true,
			data:JcallShell.REA.Enum.getList('BmsCenSaleDoc_Status',true,true),
			listeners:{change:function(){me.onSearch();}}
		},'-','->', {
			type: 'search',
			info: me.searchInfo
		}];
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_OrderDocNo',
			text: '订货单号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '交流',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowInteraction(rec.get(me.PKField));
				}
			}]
		},{
			dataIndex: 'BmsCenSaleDoc_UrgentFlag',
			text: '紧急标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_Status',
			text: '单据状态',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_Status['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_IOFlag',
			text: '提取标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_IOFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Comp_CName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_UserName',
			text: '操作人员',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_PrintTimes',
			text: '打印次数',
			align:'center',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Memo',
			text: '备注',
			width: 200,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenSaleDoc_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDoc_Comp_Id',
			text: '供货方ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDoc_Lab_Id',
			text: '订货方ID',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		me.internalWhere = me.getInternalWhere();

		return me.callParent(arguments);;
	},
	/**获取内部条件*/
	getInternalWhere:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			status = buttonsToolbar.getComponent('status'),
			search = buttonsToolbar.getComponent('search'),
			where = [];
		
		if(status){
			var value = status.getValue();
			if(value){
				where.push('bmscensaledoc.Status=' + value);
			}
		}
		if(search){
			var value = search.getValue();
			if(value){
				var searchWhere = me.getSearchWhere(value);
				if(searchWhere){
					where.push('(' + searchWhere + ')');
				}
				
			}
		}
		
		return where.join(" and ");
	},
	/**显示交流页面*/
	onShowInteraction:function(id){
		var me = this;
		
		JShell.Win.open('Shell.class.rea.sale.interaction.App', {
			PK: id
		}).show();
	}
});