/**
 * 权限树
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.qms.equip.templet.Tree',{
    extend:'Shell.ux.tree.Panel',
	
	title:'小组',
	width:300,
	height:500,
	
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id,HRDept_DataTimeStamp',
	/**默认加载数据*/
	defaultLoad:true,
	/**根节点*/
	root:{
		text:'所有组织机构',
		iconCls:'main-package-16',
		id:0,
		tid:0,
		leaf:false,
		expanded:false
	},
	/**是否显示根节点*/
	rootVisible:false,
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.store.on({
			load:function(){
				me.getSelectionModel().select(me.root.tid);
			}
		});
	},
	initComponent:function(){
		var me = this;
		
		me.topToolbar = me.topToolbar || ['-','->',{
			iconCls:'button-right',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		
		me.callParent(arguments);
	},
	
	createDockedItems:function(){
		var me = this;
		var dockedItems = me.callParent(arguments);
		
		dockedItems[0].items = dockedItems[0].items.concat(me.topToolbar);
		
		return dockedItems;
	},
	changeData:function(data){
    	return data;
	}
});
	