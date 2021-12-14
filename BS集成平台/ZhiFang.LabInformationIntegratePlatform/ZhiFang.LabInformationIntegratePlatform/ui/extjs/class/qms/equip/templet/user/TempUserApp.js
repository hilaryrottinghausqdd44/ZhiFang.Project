/**
 * 模板人员维护(以模板选人)
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.user.TempUserApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '模板人员维护',
	layout: {
		type: 'border',
		regionWeights: {
			west: 2,
			north: 1
		}
	},
	width: 1000,
	height: 800,
	ETempletId:'',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.EquipGrid.on({
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get("ETemplet_Id");
					me.ETempletId=id;
					me.Grid.ETempletId=id;
					var hql='etempletemp.ETemplet.Id='+id;
                    me.Grid.load(hql);
				}, null, 500);
			},
			itemdblclick:function(view,record){
				JShell.Action.delay(function() {
					var id = record.get("ETemplet_Id");
					me.ETempletId=id;
					me.Grid.ETempletId=id;
					var hql='etempletemp.ETemplet.Id='+id;
                    me.Grid.load(hql);
				}, null, 500);
			},
			nodata:function(grid){
				me.Grid.clearData();
			}
		});
		me.Grid.on({
			itemdblclick:function(view,record){
//				me.Grid.onEditClick(record.get('ETempletEmp_Id'),'edit');
			},
			onAddClick:function(view,record){
				me.Grid.openForm(null,'Shell.class.qms.equip.templet.user.check.CheckApp',700);
			},
			save:function(win){
			
			},
			accept: function(p) {
				var Grid = p.getComponent('Grid');
				var records = Grid.getSelectionModel().getSelection();
	            for(var i in records) {
				    var Id = records[i].get("HREmployee_Id");
				    me.Grid.loadEmpData(me.ETempletId,Id);
				}
		    	p.close();
	            me.Grid.onSearch();
			}
		});
		
	},

	initComponent: function() {
		var me = this;
		me.title = me.title || "仪器维护";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.EquipGrid = Ext.create('Shell.class.qms.equip.templet.user.EquipGrid', {
			border: true,
			title: '仪器模板列表',
			region: 'center',
			/**默认加载数据*/
	        defaultLoad: true,
			header: false,
			itemId: 'EquipGrid'
		});
		me.Grid = Ext.create('Shell.class.qms.equip.templet.user.Grid', {
			border: true,
			title: '模板人员列表',
			region: 'east',
			split: true,
			collapsible: true,
			collapseMode:'mini',
		    width:360,
			IsCandidaShow:true,
			split: true,
			header: false,
			itemId: 'Grid'
		});
		return [ me.EquipGrid,me.Grid];
	}
});