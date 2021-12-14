/**
 * 收款计划查看
 * @author liangyl
 * @version 2016-12-23
 */
Ext.define('Shell.class.wfm.business.contract.search.RcvedRecordGrid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.SimpleGridTree',
	/**合同ID*/
	PContractID: null,
	/**合同名称*/
	PContractName: '',
	/**合同销售负责人ID*/
	PrincipalID: null,
	/**合同销售负责人*/
	Principal: '',
	defaultLoad: false,
	/**合同总金额*/
	Amount: 0,
	/**编辑状态*/
	EditStatus: 3,
	BackEditStatus: 7,
	/**有效计划*/
	IsCheckStatus:true,
			/**付款单位*/
	PayOrgID:null,
	/**付款单位*/
	PayOrg:null,
	/**客户*/
	PClientID:null,
	/**客户*/
	PClientName:null,
	//=====================创建内部元素=======================
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
	   	columns = me.callParent(arguments);
		columns.splice(2, 0,  {
			text: '状态',
			dataIndex: 'Status',
			width: 65,
			sortable: false,
			renderer: function(value, meta) {
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('PReceivePlanStatus', v);
					if(info) {
						v = info.Name;
						meta.style = 'background-color:' + info.BGColor + ';color:' + info.FontColor + ';';
					}
				}
				return v;
			}
		});
		return columns;
	}
});