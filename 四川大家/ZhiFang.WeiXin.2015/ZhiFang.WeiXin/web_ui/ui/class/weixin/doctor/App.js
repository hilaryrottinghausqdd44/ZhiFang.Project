/**
 * 医生维护
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.doctor.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'医生维护',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
        var BHospitalAreaID = me.HospitalGrid.getComponent('buttonsToolbar').getComponent('BHospital_AreaID');
		me.HospitalGrid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var Id=record.get('BHospital_Id');
					if(!Id && BHospitalAreaID.getValue()){
					   Id=me.getHospitalIDS();
					}
					me.Grid.HospitalID=Id;
					me.Grid.AreaID=BHospitalAreaID.getValue();
					me.Grid.onSearch();
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var Id=record.get('BHospital_Id');
				    if(!Id  && BHospitalAreaID.getValue()){
					   Id=me.getHospitalIDS();
					}
					me.Grid.HospitalID=Id;
					me.Grid.AreaID=BHospitalAreaID.getValue();
				    me.Grid.onSearch();
				},null,500);
			}
		});
	},
	
	//找出列表里所有的医院id
	getHospitalIDS:function(){
		var me=this;
		var IDS='';
		me.HospitalGrid.store.each(function(record) {
			if(record.get('BHospital_Id')){
				 IDS+=","+record.get('BHospital_Id');
			}
        });
        IDS=0==IDS.indexOf(",")?IDS.substr(1):IDS;
		return IDS;
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.HospitalGrid = Ext.create('Shell.class.weixin.doctor.HospitalGrid', {
			region: 'west',
			width:350,
			split: true,
			collapsible: true,
			header: false,
			itemId: 'HospitalGrid',
			collapseMode:'mini'
		});
		me.Grid = Ext.create('Shell.class.weixin.doctor.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		return [me.HospitalGrid,me.Grid];
	}
});