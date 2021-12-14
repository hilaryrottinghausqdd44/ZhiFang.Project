/**
 * 入库查询
 * @author liangyl
 * @version 2018-01-17
 */
Ext.define('Shell.class.rea.client.stock.seach.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '入库查询',
    requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsInDtl_DispOrder',
		direction: 'ASC'
	},{
		property: 'ReaBmsInDtl_DataAddTime',
		direction: 'ASC'
	},{
		property: 'ReaBmsInDtl_GoodsCName',
		direction: 'ASC'
	}],
	/**默认选中*/
	autoSelect: true,
	/**默认选中*/
	autoSelect: false,
	features: [{ftype: 'summary'}],
	columnCountKey:'1',
	btnCountKey:'1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onShowBtn(false);
	},
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			dataIndex: 'ReaBmsInDtl_GoodsCName',
			text: '产品名称',width: 150,
			columnCountKey:me.columnCountKey,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		},{
			dataIndex: 'ReaBmsInDtl_GoodsQty',text: '入库数量',columnCountKey:me.columnCountKey,
			width: 85,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_GoodsUnit',text: '单位',columnCountKey:me.columnCountKey,
			width: 80,defaultRenderer: true
		}, {
			dataIndex: '',text: '规格',columnCountKey:me.columnCountKey,
			width: 100,defaultRenderer: true
		},  {
			dataIndex: 'ReaBmsInDtl_Price',text: '单价',width: 85,type: 'float',
			align: 'right',columnCountKey:me.columnCountKey,
			summaryRenderer: function(value) {
				return '<div  style="text-align:right" ><strong style="color:#0000FF">本页合计:</strong></div>';
			}
		}, {
			dataIndex: 'ReaBmsInDtl_SumTotal',sortable: false,
			text: '总计金额',align: 'right',width: 100,columnCountKey:me.columnCountKey,
			type: 'number',xtype: 'numbercolumn',summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		},{
			dataIndex: 'ReaBmsInDtl_Id',sortable: false,
			text: '入库明细单号',align: 'right',width: 100,
			defaultRenderer: true
		},{
			dataIndex: '',text: '有效期',
			width: 85,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_LotNo',text: '批号',
			width: 100,defaultRenderer: true
		},{
			dataIndex: '',text: '操作日期',
			width: 100,defaultRenderer: true
		},{
			dataIndex: '',text: '生产日期',
			width: 100,defaultRenderer: true
		},{
			dataIndex: '',text: '产地',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_StorageName',text: '库房名称',
			width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_TaxRate',text: '税率',
			width: 100,type: 'int',align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: '',text: '验收人',
			width: 80,defaultRenderer: true
		},{
			dataIndex: '',text: '验收时间',
			width: 135,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsInDtl_CompanyName',text: '供应商',
			width: 150,defaultRenderer: true
		},{
			dataIndex: '',text: '来源单号',
			width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsInDtl_PlaceName',text: '货架名称',width: 100,defaultRenderer: true
		},{
			dataIndex: '',text: '生产厂商',width: 100,defaultRenderer: true
		}, {
			dataIndex: '',text: '厂商物料编码',width: 100,defaultRenderer: true
		},  {
			dataIndex: '',text: '注册证号',width: 100,defaultRenderer: true
		},  {
			dataIndex: '',text: '注册证有效期',width: 85,defaultRenderer: true
		},  {
			dataIndex: '',text: '适用机型',width: 100,defaultRenderer: true
		},   {
			dataIndex: 'ReaBmsInDtl_Id',sortable: false,text: '主键ID',hidden: true,hideable: false,isKey: true
		}];
		for(var i = 0; i < columns.length; i++) {
			if(columns[i].editor) {
				columns[i].editor.listeners = {
					beforeedit: function(editor, e) {
						return me.canEdit;
					}
				}
			}
		}
		return columns;
	},
    /**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
//		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =  [];
		buttonToolbarItems.push({
			xtype: 'uxdatearea',btnCountKey:me.btnCountKey,
			itemId: 'date',x:5,y:5,	
			labelWidth: 55,labelAlign: 'right',
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		},{
			fieldLabel: '供应商',labelWidth: 43,width: 150,
			labelAlign: 'right',//emptyText: '供应商',
			name: 'CompanyName',itemId: 'CompanyName',btnCountKey:me.btnCountKey,
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.cenorg.basic.CheckGrid',
			x:280,y:5,	
			classConfig: {
				width: 350,checkOne:true,
				title: '供应商选择',defaultWhere: "reacenorg.OrgType=0" 
			}
		}, {
			fieldLabel: '供应商主键ID',itemId:'CompanyID',name: 'CompanyID',
			xtype: 'textfield',hidden: true,btnCountKey:'2'
		}, {
			fieldLabel: '库房',labelWidth: 35,width: 146,
			labelAlign: 'right',//emptyText: '库房',
			name: 'StorageName',itemId: 'StorageName',btnCountKey:me.btnCountKey,
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.shelves.storage.CheckGrid',
			x:450,y:5,	
			classConfig: {
				width: 350,checkOne:true,title: '库房选择'
			}
		}, {
			fieldLabel: '批号',itemId: 'GoodsLotNo',name: 'GoodsLotNo',
			xtype:'textfield',labelAlign: 'right',labelWidth: 55,width: 165,
			x:600,y:5	,btnCountKey:me.btnCountKey
		},{text:'查询',tooltip:'入库明细查询',iconCls:'button-search',
				x:780,y:5,btnCountKey:me.btnCountKey,
				handler:function(){
//					me.onShowColumn(true);
//					me.onSearch();
				}
			},'-',{text:'清空',tooltip:'清空',iconCls:'button-cancel',
				x:835,y:5,btnCountKey:me.btnCountKey,
				handler:function(){
				}
			},'-',{text:'导出Excel',tooltip:'导出Excel',iconCls:'file-excel',
			   x:890,y:5,btnCountKey:me.btnCountKey,
			    handle:function(){
					
				}
			},{
			xtype: 'checkboxfield',
			boxLabel: '高级查询',
			x:980,y:5,iconCls: 'button-down',
			name: 'cboIShow',btnCountKey:me.btnCountKey,
			itemId: 'cboIShow',
			listeners: {
				change: function(field, newValue, oldValue, e) {
					var buttonsToolbar = me.getComponent('buttonsToolbar');
					if(newValue){
						me.onShowBtn(true);
						buttonsToolbar.setHeight(80);
					}else{
						me.onShowBtn(false);
						buttonsToolbar.setHeight(30);
					}
				}
			}
		}, {
			fieldLabel: '库房主键ID',itemId: 'StorageID',name: 'StorageID',
			xtype:'textfield',hidden:true,btnCountKey:'2'
		},{
			fieldLabel: '货架主键ID',itemId: 'PlaceID',name: 'PlaceID',
			xtype:'textfield',hidden:true,btnCountKey:'2'	
		},{
			fieldLabel: '价格范围',itemId: 'beginPrice',name: 'beginPrice',
			xtype:'numberfield',x:5,y:30,labelAlign: 'right',labelWidth: 55,width: 152
		},{
			itemId: 'tabPrice',name: 'tabPrice',
			xtype:'displayfield',x:158,y:30,fieldLabel:'',value:'-'
		},{
			fieldLabel: '',itemId: 'endPrice',name: 'endPrice',
			xtype:'numberfield',x:165,y:30,width: 93
		},{
			fieldLabel: '入库类型',name: 'typeName', labelWidth: 60,width: 167,
			itemId: 'typeName',labelAlign: 'right',//emptyText: '货架',
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.shelves.place.CheckGrid',
			x:263,y:30,	
			classConfig: {
				title: '入库类型选择'
			}
		},{
			fieldLabel: '货架',name: 'PlaceName', labelWidth: 55,width: 165,
			itemId: 'PlaceName',labelAlign: 'right',//emptyText: '货架',
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.shelves.place.CheckGrid',
			x:430,y:30,		
			classConfig: {
				title: '货架选择'
			}
		},{
			fieldLabel: '试剂名称',itemId:'GoodsCName',name: 'GoodsCName',
			x:600,y:30,
			xtype: 'textfield',labelAlign: 'right',labelWidth: 55,width: 165
		},{
			fieldLabel: '入库人员',name: 'EmpName', labelWidth: 60,width: 167,
			itemId: 'EmpName',labelAlign: 'right',//emptyText: '货架',
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.shelves.place.CheckGrid',
			x:770,y:55,	
			classConfig: {
				title: '入库人员选择'
			}
		},{
			fieldLabel: '厂商产品编码',itemId:'ProdGoodsNo',name: 'ProdGoodsNo',
			x:5,y:55,
			xtype: 'textfield',labelAlign: 'right',labelWidth: 80,width: 253
		},{
			fieldLabel: '生产厂商',itemId:'Company',name: 'Company',x:355,y:30,
			xtype: 'textfield',x:268,y:55,labelAlign: 'right',labelWidth: 55,width: 163
		},{
			fieldLabel: '产品编号',itemId:'GoodsNo',name: 'GoodsNo',
			x:430,y:55,
			xtype: 'textfield',labelAlign: 'right',labelWidth: 55,width: 165
		},{
			fieldLabel: '简称',itemId:'GoodsSName',name: 'GoodsSName',
			x:600,y:55,
			xtype: 'textfield',labelAlign: 'right',labelWidth: 55,width: 165
		});
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
            height:30,
            /**布局方式*/
	        layout: 'absolute',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	 /**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll(); //清空数据
		if(!me.defaultLoad) return false;
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
	},	
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:[]
		};
		
		return items;
	},
		/**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;
		var config = {
			dock: 'bottom',
			itemId:'pagingToolbar',
			store: me.store
		};
		if (me.defaultPageSize) config.defaultPageSize = me.defaultPageSize;
		if (me.pageSizeList) config.pageSizeList = me.pageSizeList;
		me.agingToolbarCustomItems=['->',{
			xtype: 'label',
			itemId:'labText',
			style: "font-weight:bold;color:#0000FF;",
	        text: '总计合计:',
	        margin: '0 0 0 10'
		}];
		//分页栏自定义功能组件
		if (me.agingToolbarCustomItems) config.customItems = me.agingToolbarCustomItems;

		return Ext.create('Shell.ux.toolbar.Paging', config);
	},
	 /**显示隐藏的数据列*/
	onShowColumn: function(isShow) {
		var me = this;
		var len = me.columns.length;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var CompanyID=buttonsToolbar.getComponent('CompanyID').getValue();
		var StorageID=buttonsToolbar.getComponent('StorageID').getValue();
		var LotNo=buttonsToolbar.getComponent('GoodsLotNo').getValue();
		for (var i = 0; i < len; i++) {
			if(isShow){
				me.columns[i].show();
			}else{
				if(me.columns[i].xtype != 'rownumberer'){
					me.columns[i].hide();
				}
				if(CompanyID && me.columns[i].dataIndex=='ReaBmsInDtl_CompanyName'){
					me.columns[i].show();
				}
				if(StorageID && me.columns[i].dataIndex=='ReaBmsInDtl_StorageName'){
					me.columns[i].show();
				}
				if(LotNo && me.columns[i].dataIndex=='ReaBmsInDtl_LotNo'){
					me.columns[i].show();
				}
				if (me.columns[i].columnCountKey) {
				    me.columns[i].show();
			    }
			}
		}
	},
	/**显示隐藏查询条件*/
	onShowBtn: function(isShow) {
		var me=this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var len =buttonsToolbar.items.length;
			var CompanyID=buttonsToolbar.getComponent('CompanyID');
		var StorageID=buttonsToolbar.getComponent('StorageID');
		var LotNo=buttonsToolbar.getComponent('LotNo'); 
		for(var i=0;i<len;i++){
			if(!isShow){
				if(buttonsToolbar.items.items[i].btnCountKey=='1'){
					buttonsToolbar.items.items[i].show();
			    }else{
			    	buttonsToolbar.items.items[i].hide();
			    }
			}else{
				if(buttonsToolbar.items.items[i].btnCountKey!='2'){
				   buttonsToolbar.items.items[i].show();
				}
			}
		}
	}
});