/**
 * 仪器结果
 * @author gzj
 * @version 2020-03-22
 */
Ext.define('Shell.class.lts.sample.result.equip.App', {
	extend:'Shell.ux.panel.AppPanel',
	title:'仪器结果',
	SearchEquipUrl:'',
	//仪器样本单ID
	EquipFormID:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function () {
		var me = this;
		me.Grid = Ext.create('Shell.class.lts.sample.result.equip.Grid', {
			region: 'center', itemId: 'ReasultTab', header: false
		});
		return [me.Grid];
	},
	
	//查询数据
	onSearch:function(testFormRecord){
		var me = this,
			EquipFormID = testFormRecord.get("LisTestForm_EquipFormID");
		
		if(!EquipFormID){//仪器样本单ID不存在
			me.Grid.showErrorInView("检验单中不存在仪器样本单ID！");
			me.EquipFormID = null;
		}else{
			//与原先的不一致，重新加载数据
			if(EquipFormID != me.EquipFormID){
				me.EquipFormID = EquipFormID;
				//相关数据变化
				me.GridSearch(me.EquipFormID);
			}
		}
	},
	GridSearch: function (EquipFormId) {
		var me = this;
		me.Grid.defaultWhere = "EquipFormID=" + EquipFormId;
		me.Grid.onSearch();
	}
});