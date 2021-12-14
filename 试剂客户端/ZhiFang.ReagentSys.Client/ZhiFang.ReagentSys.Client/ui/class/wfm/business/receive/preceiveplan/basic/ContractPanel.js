/**
 * 合同信息
 * @author liangyl
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.basic.ContractPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '合同信息',
	className: 'Shell.class.wfm.business.receive.preceiveplan.basic.CheckGrid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
	    //
		me.addEvents('checkClick');
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.ContentPanel = Ext.create('Shell.class.wfm.business.contract.basic.ContentPanel', {
			region: 'center',
			header: false,
			border: false,
			title: '合同信息',
			itemId: 'ContentPanel'
		});
		return [me.ContentPanel];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = {
			xtype: 'uxButtontoolbar',
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: [{
				width: 180,
				labelWidth: 35,
				labelAlign: 'right',
				xtype: 'uxCheckTrigger',
				itemId: 'PContractName',
				fieldLabel: '合同',
				className: me.className,
				classConfig: {
					title: '合同选择',
					width: 500
				}
			}, {
				xtype: 'textfield',
				itemId: 'PContractID',
				fieldLabel: '合同单位主键ID',
				hidden: true
			}]
		};

		return dockedItems;
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		//客户
		var PContractName = buttonsToolbar.getComponent('PContractName'),
			PContractID = buttonsToolbar.getComponent('PContractID');
		if(PContractName) {
			PContractName.on({
				check: function(p, record) {
					PContractName.setValue(record ? record.get('PContract_Name') : '');
					PContractID.setValue(record ? record.get('PContract_Id') : '');
					var Id = record ? record.get('PContract_Id') : '';
					var ContractName = record ? record.get('PContract_Name') : '';
					var PrincipalID =record ? record.get('PContract_PrincipalID') : '';
					var Principal =record ? record.get('PContract_Principal') : '';
					var Amount =record ? record.get('PContract_Amount') : '';
					
					var PayOrgID =record ? record.get('PContract_PayOrgID') : '';
					var PayOrg =record ? record.get('PContract_PayOrg') : '';
					var PClientID =record ? record.get('PContract_PClientID') : '';
					var PClientName =record ? record.get('PContract_PClientName') : '';
					if(Id) {
						me.onSearch(Id);
					} else {
						me.ContentPanel.clearData();
					}
					me.fireEvent('checkClick', Id, ContractName, PrincipalID, Principal, Amount,PayOrgID,PayOrg,PClientID,PClientName,p);
				}
			});
		}
	},
	onSearch: function(Id) {
		var me = this;
		me.ContentPanel.load(Id);
	},
		/**刷新按钮点击处理方法*/
	onRefreshClick: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(!buttonsToolbar) return;
		//客户
		var PContractID = buttonsToolbar.getComponent('PContractID').getValue();
		if(PContractID){
			me.onSearch(PContractID);
		}else{
			JShell.Msg.alert('请选择合同!');
		}
	}
});