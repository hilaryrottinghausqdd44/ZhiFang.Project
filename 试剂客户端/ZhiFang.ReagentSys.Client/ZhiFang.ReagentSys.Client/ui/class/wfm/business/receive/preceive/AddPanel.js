/**
 * 商务收款
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceive.AddPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '商务收款',
		/**付款单位ID*/
	PayOrgID:null,
//	 layout: 'anchor',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.ReceivePlanGrid.on({
			itemclick: function(v, record) {
				JShell.Action.delay(function() {
					me.getReceivePlanInfo(record);
				}, null, 500);
			},
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					me.getReceivePlanInfo(record);
				}, null, 500);
			},
			nodata: function(p) {
				me.EndPanel.Grid.PReceivePlanId=null;
				me.EndPanel.Grid.PContractID=null;
            	me.EndPanel.Grid.PContractName='';
    	     	me.EndPanel.Grid.ReceiveManID=null;
    	     	me.EndPanel.Grid.ReceiveManName='';
    	     	me.EndPanel.Grid.UnReceiveAmount=0;
    	        me.EndPanel.Grid.ReceivePlanReceiveAmount=0;
    	     	me.EndPanel.Grid.PClientID=null;
    	     	me.EndPanel.Grid.PClientName='';
				me.EndPanel.Grid.clearData();
			}
		});
		me.EndPanel.Grid.on({
			save:function(p){//新增
				p.close();
				me.FinanceReceiveGrid.onSearch();
			},
			backsave:function(){//撤回
				me.FinanceReceiveGrid.onSearch();
			},
			onAddClick:function(){
				//财务收款
				var FinanceReceiveGridId=null,ReceiveAmount=0,SplitAmount=0;
				var CompnameID=null,ComponeName='',ReceiveDate=null;
				//选择一行财务收款
		    	var records = me.FinanceReceiveGrid.getSelectionModel().getSelection();
				if(records.length == 1) {
					FinanceReceiveGridId = records[0].get(me.FinanceReceiveGrid.PKField);
					ReceiveAmount=records[0].get('PFinanceReceive_ReceiveAmount');
					SplitAmount=records[0].get('PFinanceReceive_SplitAmount');
				    CompnameID=records[0].get('PFinanceReceive_CompnameID');
				    ComponeName=records[0].get('PFinanceReceive_ComponeName');
	                ReceiveDate=records[0].get('PFinanceReceive_ReceiveDate');
				}
				var strDate=JcallShell.Date.toString(ReceiveDate, true);
				var obj={
					FinanceReceiveGridId:FinanceReceiveGridId,
					ReceiveAmount:ReceiveAmount,
					SplitAmount:SplitAmount,
					CompnameID:CompnameID,
					ComponeName:ComponeName,
					ReceiveDate:strDate
				};
				me.EndPanel.Grid.openEditForm(obj);
			}
		});
	},
	//从付款计划中得到合同,收款计划ID,收款负责人,未付,客户
    getReceivePlanInfo:function(record){
    	var me=this;
    	var id = record.get('PReceivePlan_Id');
		//合同ID
		var PContractID = record.get('PReceivePlan_PContractID');
		me.EndPanel.Grid.PContractID=PContractID;
        //合同
		var PContractName = record.get('PReceivePlan_PContractName');
		me.EndPanel.Grid.PContractName=PContractName;
		//收款计划
		me.EndPanel.Grid.PReceivePlanId=id;
		//收款负责人ID
		var ReceiveManID = record.get('PReceivePlan_ReceiveManID');
		me.EndPanel.Grid.ReceiveManID=ReceiveManID;
		//收款负责人
		var ReceiveManName = record.get('PReceivePlan_ReceiveManName');
		me.EndPanel.Grid.ReceiveManName=ReceiveManName;
		//未付
		var UnReceiveAmount = record.get('PReceivePlan_UnReceiveAmount');
		me.EndPanel.Grid.UnReceiveAmount=UnReceiveAmount;
		
		//收款金额
		var ReceivePlanReceiveAmount=record.get('PReceivePlan_ReceiveAmount');
	    me.EndPanel.Grid.ReceivePlanReceiveAmount=ReceivePlanReceiveAmount;
	   //客户
		var PClientName = record.get('PReceivePlan_PClientName');
		me.EndPanel.Grid.PClientName=PClientName;
		//客户Id
		var PClientID = record.get('PReceivePlan_PClientID');					
		me.EndPanel.Grid.PClientID=PClientID;
    },
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.FinanceReceiveGrid = Ext.create('Shell.class.wfm.business.receive.preceive.FinanceReceiveGrid', {
			region: 'north',
			split: true,
			header: false,
			maxHeight: 240,
			height:180,
//			anchor: '100% 33%',
			/**付款单位ID*/
           	PayOrgID:me.PayOrgID,
			title: '财务收款记录',
			collapsible: true,
			checkOne: false,
			itemId: 'FinanceReceiveGrid'
		});
		me.ReceivePlanGrid = Ext.create('Shell.class.wfm.business.receive.preceive.ReceivePlanGrid', {
			region: 'center',
			minHeight:120,
//			anchor: '100% 33%',
			header: false,
			split: true,
			title: '付款计划',
			/**付款单位ID*/
           	PayOrgID:me.PayOrgID,
			itemId: 'ReceivePlanGrid'
		});
		me.EndPanel = Ext.create('Shell.class.wfm.business.receive.preceive.EndPanel', {
			region: 'south',
			split: true,
			header: false,
			maxHeight: 240,
			height:200,
			border:false,
//			anchor: '100% 34%',
			title: '商务收款记录',
			collapsible: true,
			itemId: 'EndPanel'
		});
		return [me.FinanceReceiveGrid,me.ReceivePlanGrid, me.EndPanel];
	}
});