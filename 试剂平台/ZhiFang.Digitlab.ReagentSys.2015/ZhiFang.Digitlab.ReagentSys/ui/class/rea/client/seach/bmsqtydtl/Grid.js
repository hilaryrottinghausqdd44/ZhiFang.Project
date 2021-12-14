/**
 * 库存查询
 * @author liangyl
 * @version 2017-11-10
 */
Ext.define('Shell.class.rea.client.seach.bmsqtydtl.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '库存列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsQtyDtl',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyDtlByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: false,
	features: [{
		ftype: 'summary'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	    me.enableControl(); //启用所有的操作功能
	
		//初始化检索监听
		me.initFilterListeners();
//		me.onSearch();
        JShell.Action.delay(function(){
			me.onSearch();
		},null,500);

	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsQtyDtl_CompanyName',hidden:true,sortable: true, text: '供应商',minWidth: 150,flex:1,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_LotNo',text: '货品批号',sortable: true, hidden:true,width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',text: '货品名称',sortable: true, minWidth: 150,flex:1,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_StorageName',hidden:true,sortable: true, text: '库房',width: 150,defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',text: '库存数量',sortable: true, width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',text: '包装单位',sortable: true, width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_ReaGoods_UnitMemo',text: '规格',width: 100,sortable: true, defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoods_RegistNoInvalidDate',isDate:true,text: '有效期',width: 100,sortable: true, defaultRenderer: true
		},{
			text: '单价',dataIndex: 'ReaBmsQtyDtl_Price',
			width: 100,sortable: true,
			summaryRenderer: function(value) {
				return '<div  style="text-align:right ;" ><strong>本页合计:</strong></div>';
			}
		}, {
			text: '总计',dataIndex: 'ReaBmsQtyDtl_SumTotal',
			width: 100,sortable: true,type: 'number',xtype: 'numbercolumn',summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		},{
			dataIndex: 'ReaBmsQtyDtl_SerialNo',sortable: true, text: '条码号',width: 100,defaultRenderer: true
		},  {
			dataIndex: 'ReaBmsQtyDtl_TaxRate',sortable: true, text: '税率',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_ZX1',sortable: true, text: '专项1',minWidth: 100,flex:1,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_ZX2',sortable: true, text: '专项2',minWidth: 100,flex:1,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_ZX3',sortable: true, text: '专项3',minWidth: 100,flex:1,defaultRenderer: true
		},{
			dataIndex: 'ReaBmsQtyDtl_Id',sortable: false, text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh','-',{
			fieldLabel: '供应商',labelWidth: 45,width: 195,
			labelAlign: 'right',//emptyText: '供应商',
			name: 'ReaBmsQtyDtl_CompanyName',itemId: 'ReaBmsQtyDtl_CompanyName',
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.cenorg.basic.CheckGrid',
			classConfig: {
				width: 350,checkOne:true,
				title: '供应商选择',defaultWhere: "reacenorg.OrgType=0" 
			}
		}, {
			fieldLabel: '供应商主键ID',itemId: 'ReaBmsQtyDtl_CompanyID',name: 'ReaBmsQtyDtl_CompanyID',
			xtype: 'textfield',hidden: true
		},{
			fieldLabel: '库房',labelWidth: 40,width: 175,
			labelAlign: 'right',//emptyText: '库房',
			name: 'ReaBmsQtyDtl_StorageName',itemId: 'ReaBmsQtyDtl_StorageName',
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.shelves.storage.CheckGrid',
			classConfig: {
				width: 350,checkOne:true,title: '库房选择'
			}
		}, {
			fieldLabel: '库房主键ID',itemId: 'ReaBmsQtyDtl_StorageID',name: 'ReaBmsQtyDtl_StorageID',
			xtype: 'textfield',hidden: true
		},{
			fieldLabel: '货品',labelWidth: 40,width: 175,
			labelAlign: 'right',//emptyText: '货品',
			name: 'ReaBmsQtyDtl_GoodsName',itemId: 'ReaBmsQtyDtl_GoodsName',
			xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.goods2.basic.CheckGrid',
			classConfig: {
				width: 500,checkOne:true,title: '货品选择'
//				defaultWhere: "pdict.PDictType.DictTypeCode='" + JcallShell.QMS.DictTypeCode.QRecordType + "'"
			}
		}, {
			fieldLabel: '货品主键ID',itemId: 'ReaBmsQtyDtl_GoodsID',name: 'ReaBmsQtyDtl_GoodsID',
			xtype: 'textfield',hidden: true
		},'-',{
			iconCls: 'button-show hand',
			text: '单个明细',
			tooltip: '单个明细',
			handler: function() {
			    var	records = me.getSelectionModel().getSelection();
				if(records.length != 1){
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var goodid=records[0].get('ReaBmsQtyDtl_GoodsID');
				me.onSeachDtlById(goodid);
			}
		}];
		
		//查询框信息
		me.searchInfo = {
			width:150,isLike:true,itemId: 'Search',
			emptyText:'条码号/货品名称',
			fields:['reabmsqtydtl.SerialNo','reabmsqtydtl.GoodsName']
		};
		items.push('->', {
			type: 'search',
			
			info: me.searchInfo
		});
		return items;
	},
	initFilterListeners:function(){
		var me=this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
			
		if(!buttonsToolbar) return;
		var CompanyName = buttonsToolbar.getComponent('ReaBmsQtyDtl_CompanyName'),
			CompanyID = buttonsToolbar.getComponent('ReaBmsQtyDtl_CompanyID');
		
		if(CompanyName){
			CompanyName.on({
				check: function(p, record) {
					CompanyName.setValue(record ? record.get('ReaCenOrg_CName') : '');
					CompanyID.setValue(record ? record.get('ReaCenOrg_Id') : '');
					p.close();
					me.onSearch();
				}
			});
		}
		var StorageName = buttonsToolbar.getComponent('ReaBmsQtyDtl_StorageName'),
			StorageID = buttonsToolbar.getComponent('ReaBmsQtyDtl_StorageID');
		if(StorageName){
			StorageName.on({
				check: function(p, record) {
					StorageName.setValue(record ? record.get('ReaStorage_CName') : '');
					StorageID.setValue(record ? record.get('ReaStorage_Id') : '');
					p.close();
					me.onSearch();
				}
			});
		}

	    var GoodsName = buttonsToolbar.getComponent('ReaBmsQtyDtl_GoodsName'),
			GoodsID = buttonsToolbar.getComponent('ReaBmsQtyDtl_GoodsID');
		if(GoodsName){
			GoodsName.on({
				check: function(p, record) {
					GoodsName.setValue(record ? record.get('ReaGoods_CName') : '');
					GoodsID.setValue(record ? record.get('ReaGoods_Id') : '');
					p.close();
					me.onSearch();
				}
			});
		}
	},
	/**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;
		var config = {
			dock: 'bottom',
			itemId: 'pagingToolbar'
		};
		if(me.defaultPageSize) config.defaultPageSize = me.defaultPageSize;
		if(me.pageSizeList) config.pageSizeList = me.pageSizeList;
		me.agingToolbarCustomItems = ['->', {
			xtype: 'label',
			itemId: 'SumTotal',
			hidden:true,
			style: "font-weight:bold;color:black;",
			text: '总计合计',
			margin: '0 20 0 0'
		}];
		//分页栏自定义功能组件
		if(me.agingToolbarCustomItems) config.customItems = me.agingToolbarCustomItems;
         
		return Ext.create('Shell.ux.toolbar.Paging', config);
	},
	//单个明细查询
	onSeachDtlById:function(id){
		var me=this,
			config = {
				resizable: false,
		        goodsId : id
			};
		JShell.Win.open('Shell.class.rea.client.seach.bmsqtydtl.BmsInDtGrid', config).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
            CompanyID = null,GoodsID=null,StorageID=null,search = null
			params = [];
			
		if(buttonsToolbar){
			CompanyID = buttonsToolbar.getComponent('ReaBmsQtyDtl_CompanyID').getValue();
			StorageID = buttonsToolbar.getComponent('ReaBmsQtyDtl_StorageID').getValue();
			GoodsID = buttonsToolbar.getComponent('ReaBmsQtyDtl_GoodsID').getValue();
			search = buttonsToolbar.getComponent('Search').getValue();
		}
		
		if(CompanyID){
			params.push("reabmsqtydtl.ReaCompanyID=" + CompanyID );
		}
		if(StorageID){
			params.push("reabmsqtydtl.StorageID=" + StorageID );
		}
		if(GoodsID){
			params.push("reabmsqtydtl.GoodsID=" + GoodsID );
		}
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
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
	}
});