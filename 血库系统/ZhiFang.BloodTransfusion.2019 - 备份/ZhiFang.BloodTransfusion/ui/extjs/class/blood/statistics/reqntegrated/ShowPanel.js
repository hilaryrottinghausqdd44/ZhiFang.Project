/**
 * 输血申请综合查询:右侧区域信息
 * @author longfc
 * @version 2020-02-27
 */
Ext.define('Shell.class.blood.statistics.reqntegrated.ShowPanel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '血液制品信息',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.onListenersPanel();
	},
	initComponent: function() {
		var me = this;

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var clientHeight = document.body.clientHeight;
		var height1=100;
		if(clientHeight>=846){
			height1=120;
		}
		//申请血液制品信息
		me.ReqDtlGrid = Ext.create('Shell.class.blood.statistics.reqntegrated.req.DtlGrid', {
			region: 'north',
			height: height1,
			header: false,
			border: false,
			itemId: 'ReqDtlGrid',
			split: true,
			collapsible: false
		});
		//样本信息
		me.ReceiGrid = Ext.create('Shell.class.blood.statistics.reqntegrated.recei.DtlGrid', {
			region: 'center',
			height: 100,
			header: false,
			border: false,
			itemId: 'ReceiGrid',
			split: true,
			collapsible: false
		});
		//出库信息
		me.OutDtlGrid = Ext.create('Shell.class.blood.statistics.reqntegrated.out.DtlGrid', {
			region: 'south',
			height: height1,
			header: false,
			border: false,
			itemId: 'OutDtlGrid',
			split: true,
			collapsible: false
		});
		//血袋回收信息
		me.RecoverGrid = Ext.create('Shell.class.blood.statistics.reqntegrated.recover.DtlGrid', {
			region: 'south',
			height: height1,
			header: false,
			border: false,
			itemId: 'RecoverGrid',
			split: true,
			collapsible: false
		});
		//血袋入库信息
		me.InDtlGrid = Ext.create("Shell.class.blood.statistics.reqntegrated.in.DtlGrid", {
			region: 'south',
			height: height1,
			header: false,
			border: false,
			itemId: 'InDtlGrid'
		});
		return [me.ReqDtlGrid, me.ReceiGrid, me.OutDtlGrid, me.RecoverGrid, me.InDtlGrid];
	},
	onListenersPanel: function(record) {
		var me = this;

	},
	loadData: function(record) {
		var me = this;
		var reqFormId = record.get("BloodBReqForm_Id");
		me.ReqDtlGrid.PK = reqFormId;

		me.ReceiGrid.PK = reqFormId;
		me.ReceiGrid.bReqVO=me.getbReqVO(record);
		
		me.OutDtlGrid.PK = reqFormId;
		me.RecoverGrid.PK = reqFormId;
		me.InDtlGrid.PK = reqFormId;
		
		me.ReqDtlGrid.onSearch();
		me.ReceiGrid.onSearch();
		me.OutDtlGrid.onSearch();
		me.RecoverGrid.onSearch();
		me.InDtlGrid.onSearch();
	},
	getbReqVO:function(record){
		var me=this;
		var bReqVO={
			"PatNo":record.get("BloodBReqForm_PatNo"),
			"CName":record.get("BloodBReqForm_CName"),
			"Sex":record.get("BloodBReqForm_Sex"),
			"DeptNo":record.get("BloodBReqForm_DeptNo"),
			"Bed":record.get("BloodBReqForm_Bed")
		};
		return bReqVO;
	},
	onNodata:function(){
		var me = this;
		var reqFormId ="";
		me.ReqDtlGrid.PK = reqFormId;
		me.ReceiGrid.PK = reqFormId;
		me.ReceiGrid.bReqVO=null;
		me.OutDtlGrid.PK = reqFormId;
		me.RecoverGrid.PK = reqFormId;
		me.InDtlGrid.PK = reqFormId;
		
		me.ReqDtlGrid.onSearch();
		me.ReceiGrid.onSearch();
		me.OutDtlGrid.onSearch();
		me.RecoverGrid.onSearch();
		me.InDtlGrid.onSearch();
	}
});
