/**
 * 员工维护
 * @author liangyl
 * @version 2018-11-09
 */
Ext.define('Shell.class.qms.equip.user.App',{
    extend:'Shell.class.sysbase.user.App',
    title:'员工维护',
    
	createItems:function(){
		var me = this;
		
		me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			width: 200,
			header: false,
			itemId: 'Tree',
			split: true,
			collapsible: true
		});
		me.Grid = Ext.create('Shell.class.qms.equip.user.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.qms.equip.user.Form', {
			region: 'east',
			width: 200,
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.Tree,me.Grid,me.Form];
	},
	
	/**保单保存后处理*/
	afterFormSave:function(data){
		var me = this;
		var record = me.Grid.store.findRecord('HREmployee_Id',data.Id);
		var rec = {
			HREmployee_CName:data.CName,
			HREmployee_MobileTel:data.MobileTel,
			HREmployee_UseCode:data.UseCode,
			HREmployee_HRDept_CName:data.HRDept.CName,
			HREmployee_IsUse:data.IsUse,
			HREmployee_Id:data.Id,
			HREmployee_ManagerName:data.ManagerName
		};
		
		if(!record){
			me.Grid.store.insert(0,rec);
		}else{
			record.set(rec);
			record.commit();
		}
		
		//选中当前数据
		var num = me.Grid.store.find(me.Grid.PKField, data.Id);
		me.Grid.getSelectionModel().select(num);
	}
});