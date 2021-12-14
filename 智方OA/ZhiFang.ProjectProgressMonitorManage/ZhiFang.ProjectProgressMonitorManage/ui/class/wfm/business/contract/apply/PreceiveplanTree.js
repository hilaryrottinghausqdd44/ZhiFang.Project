/**
 * 收款计划
 * @author liangyl	
 * @version 2017-03-14
 */
Ext.define('Shell.class.wfm.business.contract.apply.PreceiveplanTree', {
    extend: 'Shell.class.wfm.business.receive.preceiveplan.apply.GridTree',
    /**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePReceivePlanByField',
    /**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPReceivePlan',
    /**合同ID*/
	PContractID: null,
	/**合同名称*/
	PContractName: '',
	/**合同销售负责人ID*/
	PrincipalID: null,
	/**合同销售负责人*/
	Principal: '',
	/**付款单位*/
	PayOrgID: null,
	/**付款单位*/
	PayOrg: null,
	/**客户*/
	PClientID: null,
	/**客户*/
	PClientName: null,

	defaultLoad: false,
	/**合同总金额*/
	Amount: 0,
	/**使用中*/
	Status: 3,
	/**编辑状态*/
	EditStatus: 3,
	/**是否用在合同签署页*/
	IsContractPanel: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.load();
   },
   /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=PReceivePlan_ReceiveGradationName,PReceivePlan_ReceivePlanAmount,PReceivePlan_ExpectReceiveDate,PReceivePlan_ReceiveManName,PReceivePlan_ReceiveAmount,PReceivePlan_UnReceiveAmount,PReceivePlan_Status,add,PReceivePlan_ReceiveGradationID,PReceivePlan_ReceiveManID,PReceivePlan_Id';
		me.defaultWhere='(preceiveplan.IsUse=1) and (preceiveplan.PContractID='+me.PContractID+')';
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	/**新增保存*/
	AddSaveInfo:function(Id){
		var me=this;
		me.PContractID=Id;
		me.onSaveClick(true);
	},
	/**修改保存*/
	EditSaveInfo:function(Id){
		var me=this;
		var roonodes = me.getRootNode().childNodes; //获取主节点
		if(roonodes.length == 0) return;
		/**根据合同ID查询数据*/
		if(Id){
			 me.getInfoById(Id);	
		}
	},
	/**根据合同ID查询数据*/
	getInfoById: function(Id) {
		var me = this,
			n = 0,
			url = JShell.System.Path.getRootUrl('/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlanByHQL?isPlanish=true');
	
		var where = "preceiveplan.PContractID=" + Id + " and preceiveplan.IsUse=1";
		url += '&fields=PReceivePlan_Id&where=' + where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value && data.value.count > 0) {
					me.delErrorCount = 0;
					me.delCount = 0;
					me.delLength = data.value.list.length;
					for(i = 0; i < data.value.list.length; i++) {
						var id = data.value.list[i].PReceivePlan_Id;
						me.delOneById(id);
					}
				}
				else {
					me.onSaveClick(true);
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false, 2000, false);
	},
	/**删除一条数据*/
	delOneById: function(id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.delCount++;
			} else {
				me.delErrorCount++;
			}
			if (me.delCount + me.delErrorCount == me.delLength) {
				if (me.delErrorCount == 0){
					me.onSaveClick(true);
				}else{
					JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
				}
			}
		}, false, 500, false);
	},
	/**收款计划赋值*/
	onSaveReceivePlan:function(Form,PK){
		var me=this;
		var values = Form.getForm().getValues();
		//合同ID
		me.PContractID=PK;
		//合同名称
		me.PContractName=values.PContract_Name;
		/**合同销售负责人ID*/
		me.PrincipalID=values.PContract_PrincipalID;
		/**合同销售负责人*/
		me.Principal=values.PContract_Principal;
		/**付款单位id*/
		me.PayOrgID=values.PContract_PayOrgID;
		/**付款单位*/
		me.PayOrg=values.PContract_PayOrg;
		/**客户ID*/
		me.PClientID=values.PContract_PClientID;
		/**客户*/
		me.PClientName=values.PContract_PClientName;
		/**合同总金额*/
		me.Amount=values.PContract_Amount;
		me.changeAmountText(values.PContract_Amount,'合同金额:');
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.onMinusClick();
		me.selectNode(me.selectId);
		me.enableControl();
		var Save = me.getComponent('topToolbar').getComponent('Save');
		Save.hide();
		me.columns[8].hide();
		me.columns[5].hide();
		me.columns[1].show();
		me.columns[0].hide();
	},
	   /**创建数据列*/
    createGridColumns: function () {
        var me=this,
        columns = me.callParent(arguments);
			columns.push({
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable: false,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					meta.tdAttr = 'data-qtip="<b>删除本行</b>"';
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.createDelRec(rowIndex, rec);
				}
			}]
		});
		return columns;
   }
});