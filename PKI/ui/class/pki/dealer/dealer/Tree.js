/**
 * 经销商选择树
 * @author liangyl
 * @version 2017-07-18
 */
Ext.define('Shell.class.pki.dealer.dealer.Tree', {
	extend: 'Shell.ux.tree.Panel',
	title: '经销商树',
	width: 280,
	height: 320,
	multiSelect: false,
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/BS_UDTO_GetBDealerFrameListTree?fields=BDealer_UseCode,BDealer_Name,BDealer_BBillingUnit_Name,BDealer_Id',
	/**默认加载数据*/
	defaultLoad: true,
	searchWidth: 120,
	rootVisible: true,
	root: {
		text: '所有经销商',
		iconCls: 'main-package-16',
		id: 0,
		tid: 0,
		leaf: false,
		expanded: false
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.store.on({
			load: function(store, records, successful, eOpts) {
				me.getSelectionModel().select(me.root.tid);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.topToolbar = me.topToolbar || ['-','->',{
			iconCls:'button-right',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		me.callParent(arguments);
	},
	
	createDockedItems: function() {
		var me = this;
		var dockedItems = me.callParent(arguments);
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		return dockedItems;
	}

});