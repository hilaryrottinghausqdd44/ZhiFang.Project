/**
 * 实验室仪器维护(管理员专用 )
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.equip.lab.ManageApp', {
	extend:'Shell.ux.panel.AppPanel',
    title:'实验室仪器维护',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.CenOrgGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.CenOrgGrid.PKField);
					me.Form.LabId = id;
					me.Grid.defaultWhere =  'testequiplab.Lab.Id=' + id;
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.CenOrgGrid.PKField);
					me.Form.LabId = id;
					me.Grid.defaultWhere =  'testequiplab.Lab.Id=' + id;
					me.Grid.onSearch();
				},null,500);
			},
			nodata:function(p){
				me.Grid.clearData();
				me.Form.clearData();
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
			addclick:function(p){
				me.Form.isAdd();
			},
			nodata:function(p){
				me.Form.clearData();
			}
		});
		me.Form.on({
			save:function(p,id){
				me.Grid.onSearch();
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
		
		me.CenOrgGrid = Ext.create('Shell.class.rea.cenorg.SimpleGrid',{
			region: 'west',
			header: false,
			itemId: 'CenOrgGrid',
			split: true,
			collapsible: true,
			defaultLoad:true,
			title:'实验室列表',
			width:230,
			defaultWhere:'cenorg.CenOrgType.ShortCode=3',
			columns:[{
				dataIndex: 'CenOrg_OrgNo',
				text: '机构编号',
				width: 60,
				defaultRenderer: true
			},{
				dataIndex: 'CenOrg_CName',
				text: '中文名',
				width: 100,
				defaultRenderer: true
			},{
				dataIndex: 'CenOrg_Id',
				text: '主键ID',
				hidden: true,
				hideable: false,
				isKey: true
			}]
		});
		
		me.Grid = Ext.create('Shell.class.rea.equip.lab.Grid',{
			region: 'center',
			header: false,
			itemId: 'Grid',
			defaultLoad:false
		});
		
		me.Form = Ext.create('Shell.class.rea.equip.lab.Form',{
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		});
		
		return [me.CenOrgGrid,me.Grid,me.Form];
	}
});