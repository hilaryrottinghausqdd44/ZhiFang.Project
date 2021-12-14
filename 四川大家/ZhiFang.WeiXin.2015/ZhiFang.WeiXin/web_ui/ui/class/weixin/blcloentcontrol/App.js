/**
 * 实验室人员维护
 * @author GHX
 * @version 2021-01-10
 */
Ext.define("Shell.class.weixin.blcloentcontrol.App",{
	extend:'Shell.ux.panel.AppPanel',
	title:'实验室人员维护',
	 
	 afterRender:function(){
	 	var me = this;
		me.userGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					me.clientGrid.externalWhere = "businesslogicclientcontrol.Account='"+record.get("RBACUser_Account")+"' and businesslogicclientcontrol.Flag=1";
					me.clientGrid.clientGridRecord=record.data;					
					me.clientGrid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					me.clientGrid.externalWhere = "businesslogicclientcontrol.Account='"+record.get("RBACUser_Account")+"' and businesslogicclientcontrol.Flag=1";
					me.clientGrid.clientGridRecord=record.data;					
					me.clientGrid.onSearch();
				},null,500);
			},
		});
	 	me.callParent(arguments);	
	 },
	initComponent:function(){
		var me =this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
    	me.userGrid = Ext.create("Shell.class.weixin.blcloentcontrol.userGrid",{
    		region:'center',
			itemId: 'userGrid'
    	});
    	me.clientGrid = Ext.create("Shell.class.weixin.blcloentcontrol.clientGrid",{
    		region:'east',
    		width:'50%',
    		clientGridRecord:'',
			itemId:'clientGrid'
    	});
    	return [me.userGrid,me.clientGrid];
    }     
});
