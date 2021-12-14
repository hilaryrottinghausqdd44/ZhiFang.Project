/**
 * 模板人员维护(从人选模板)
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.user.UserTempApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '模板人员维护',
    width:1200,
    height:400,
    EmpId:'',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.Tree.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.EmpGrid.loadByDeptId(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('tid');
					me.EmpGrid.loadByDeptId(id);
				},null,500);
			}
		});
		me.EmpGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get('HREmployee_Id');
					me.EmpId=id;
					var hql='etempletemp.HREmployee.Id='+id;
					me.Grid.load(hql);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get('HREmployee_Id');
					me.EmpId=id;
					var hql='etempletemp.HREmployee.Id='+id;
					me.Grid.load(hql);
				},null,500);
			}
		});
		me.Grid.on({
			onAddClick:function(view,record){
				me.Grid.openForm(null,'Shell.class.qms.equip.templet.basic.CheckGrid',800);
			},
			accept: function(p) {
				var records = p.getSelectionModel().getSelection();
	            for(var i in records) {
				    var Id = records[i].get("ETemplet_Id");
                    me.Grid.loadEmpData(Id,me.EmpId,records[i]);
				}
		    	p.close();
	            me.Grid.onSearch();
			}
		});
		
		me.EmpGrid.store.on({
			load: function(store, records, successful) {
				if (!successful || !records || records.length <= 0) {
					me.Grid.clearData();
				}
			}
		});
	},
	
	initComponent:function(){
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Tree = Ext.create('Shell.class.sysbase.org.Tree', {
			region: 'west',
			header: false,
			/**是否显示根节点*/
        	rootVisible:false,
			split: true,
			collapsible: true,
			collapseMode:'mini',
			itemId: 'Tree',
			width:210
		});
		me.EmpGrid = Ext.create('Shell.class.qms.equip.templet.user.EmpGrid', {
			region: 'west',
			width:250,
			header: false,
			/**是否单选*/
	        checkOne:true,
		    split: true,
			collapsible: true,
			collapseMode:'mini',
			itemId: 'EmpGrid',
			/**默认加载*/
			defaultLoad:false
			
		});
		me.Grid = Ext.create('Shell.class.qms.equip.templet.user.Grid', {
			region: 'center',
			header: false,
			defaultWhere:'etempletemp.ETemplet.IsUse=1',
			itemId: 'Grid',
			/**默认每页数量*/
	        defaultPageSize: 200,
			/**默认加载*/
			defaultLoad:false
		});
		return [me.Tree,me.EmpGrid,me.Grid];
	}
});