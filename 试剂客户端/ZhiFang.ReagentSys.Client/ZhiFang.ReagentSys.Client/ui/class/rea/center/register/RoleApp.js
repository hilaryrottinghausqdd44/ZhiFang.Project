/**
 * 角色选择
 * @author liangyl	
 * @version 2018-05-08
 */
Ext.define('Shell.class.rea.center.register.RoleApp', {
	extend: 'Ext.panel.Panel',
	title: '角色选择',

	layout:'border',
    bodyPadding:1,
	border:false,
	ROLETYPE:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
//	    me.AllRoleGrid.on({
//	    	itemdblclick:function(v, record) {
//	    		JShell.Action.delay(function(){
//	    			
//				},null,200);
//			}
//	    });
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
	    me.RoleGrid = Ext.create('Shell.class.rea.center.register.RoleGrid', {
            region:'north',
			itemId:'RoleGrid',
			ROLETYPE:me.ROLETYPE,
			split: true,height:300,
			collapsible: true,
			collapseMode:'mini',
			header:false
	    });
		me.AllRoleGrid = Ext.create('Shell.class.rea.center.register.AllRoleGrid', {
		    region:'center',
			itemId:'AllRoleGrid',
			ROLETYPE:me.ROLETYPE,
			header:false	
		});
		return [me.RoleGrid,me.AllRoleGrid];
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		if(me.hasLoadMask){me.body.mask(text);}//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
	},
	/**获取选择的权限*/
	getRoleId:function(){
		var me = this;
		var unrecords=me.AllRoleGrid.getSelectionModel().getSelection(),
			unlen = unrecords.length;	
		var Id='';
		if(unlen>0){
			for(var i =0 ;i<unlen;i++){
				Id+=','+unrecords[i].get('RBACRole_Id');
			}
			Id=0==Id.indexOf(",")?Id.substr(1):Id;
		}
		return Id;
	},
	/**验证角色是否已选*/
	onCheckRole:function(){
		var me = this;
		var isExect=true;
		var records=me.RoleGrid.store.data.items,
			len = records.length;
		var unrecords=me.AllRoleGrid.getSelectionModel().getSelection(),
			unlen = unrecords.length;	
	    var rec = me.RoleGrid.getSelectionModel().getSelection();
		if(rec.length == 0 && len>0){
			JShell.Msg.error('请先选择角色');
			isExect=false;
		}
		if(len==0 && unlen==0){
			JShell.Msg.error('请先选择角色');
			isExect=false;
		}
		return isExect;
	}
});