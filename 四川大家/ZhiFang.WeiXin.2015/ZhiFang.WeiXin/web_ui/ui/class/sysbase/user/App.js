/**
 * 员工维护
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'员工维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.Grid.DeptId = id;
					me.Grid.DeptName = record.get('text');
					me.Grid.DeptDataTimeStamp = record.get('value').DataTimeStamp;
					me.Grid.loadByDeptId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.Grid.DeptId = id;
					me.Grid.DeptName = record.get('text');
					me.Grid.DeptDataTimeStamp = record.get('value').DataTimeStamp;
					me.Grid.loadByDeptId(id);
				},null,500);
			}
		});
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				},null,500);
			},
			addclick:function(){
				var Id = me.Grid.DeptId;
				var Name = me.Grid.DeptName;
				var DataTimeStamp = me.Grid.DeptDataTimeStamp;
				
				me.Form.isAdd(Id,Name,DataTimeStamp);
			},
			nodata:function(){
				me.Form.clearData();
			}
		});
		
		me.Form.on({
			save:function(p,data){
				//me.Grid.onSearch();
				me.afterFormSave(data);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
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
		me.Grid = Ext.create('Shell.class.sysbase.user.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		me.Form = Ext.create('Shell.class.sysbase.user.Form', {
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