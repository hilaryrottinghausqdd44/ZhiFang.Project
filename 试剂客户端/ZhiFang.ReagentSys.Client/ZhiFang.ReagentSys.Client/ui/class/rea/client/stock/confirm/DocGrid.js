/**
 * 验货单入库
 * @author liangyl
 * @version 2017-12-04
 */
Ext.define('Shell.class.rea.client.stock.confirm.DocGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '入库',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelST_UDTO_SearchBmsCenSaleDocConfirm',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaST_UDTO_SearchBmsCenSaleDocConfirmByField',
	/**默认加载数据*/
	defaultLoad: true,
	/**是否启用序号列*/
	hasRownumberer: false,
	defaultWhere: 'bmscensaledocconfirm.Status in  (1,3)',
	/**用户UI配置Key*/
	userUIKey: 'stock.confirm.DocGrid',
	/**用户UI配置Name*/
	userUIName: "验收入库列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	    me.enableControl(); //启用所有的操作功能
	
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BmsCenSaleDocConfirm_DataAddTime',
			text: '申请时间',align: 'center',
			width: 130,isDate: true,hasTime: true,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_SaleDocConfirmNo',
			text: '验收单号',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_ReaCompName',
			text: '供货方',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_AccepterName',
			text: '主验收人',width: 60,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_Id',text: '主键ID',hidden: true,	hideable: false,isKey: true
		}, {
			dataIndex: 'BmsCenSaleDocConfirm_ReaCompID',text: '供货方ID',hidden: true,hideable: false
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		
		//查询框信息
		me.searchInfo = {
			width:135,isLike:true,itemId: 'Search',
			emptyText:'验收单号',
			fields:['bmscensaledocconfirm.SaleDocConfirmNo']
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
    /**手工入库*/
	showAddForm: function() {
		var me = this,
			config = {
				resizable: false,
				formtype : 'add',
				listeners: {
					save:function(p){
						me.onSearch();
						
					}
				}
			};
		JShell.Win.open('Shell.class.rea.client.stock.Form', config).show();
	}
});