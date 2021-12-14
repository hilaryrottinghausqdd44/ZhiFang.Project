/**
 * 收款计划列表
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.change.EditGrid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.EditGrid',
	title: '收款计划列表',
	PK: null,
	/**通过文字*/
	OverName: '保存',
	CloseName: '关闭',
	/**退回文字*/
	BackName: '',
	/**变更的子状态*/
	Status: 1,
	/**选中行的变更状态*/
	PReceivePlanStatus: 5,
	width: 700,
	height: 380,
	defaultLoad: false,
	GradationID:'',
	GradationName:'',
	/**收款金额*/
	Amount: 0,
	/**未付款*/
	UnReceiveAmount:0,
	/**已付款*/
	ReceiveAmount:0,
	IsCheckStatus:false,
		/**付款单位*/
	PayOrgID:null,
	/**付款单位*/
	PayOrg:null,
	/**客户*/
	PClientID:null,
	/**客户*/
	PClientName:null,
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_ChangeApplyPReceivePlan',
	initComponent: function() {
		var me = this;
		me.defaultWhere = "preceiveplan.PPReceivePlanID=" + me.PK;
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.Action.delay(function() {
			me.onSearch();
		}, null, 500);
//		var PlanAmount= parseFloat(me.Amount)-parseFloat(me.ReceiveAmount);
		me.changeAmountText(me.Amount, '原始收款计划未收总额');

	},

	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this;
	    me.focus(false);
		if(me.store.getCount() > 0) {
			if(!me.IsValid()) {
				return;
			}
			var Save = me.getComponent('buttonsToolbar2').getComponent('Save');
			Save.disable();
			//先删除已变更过的数据，再保存列表所有数据
			if(me.PK) {
				me.getInfoById();
			}
		}
		else{
			JShell.Msg.error('没有要保存的数据!');
			return;
		}
	},
	Entity: function() {
		var me = this;
		var arr = [],
			PlanAmount = 0,
			entity = {};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		me.store.each(function(rec) {
			var id = rec.get(me.PKField);
			var ExpectReceiveDate = JShell.Date.toServerDate(rec.get('PReceivePlan_ExpectReceiveDate'));
			var ReceivePlanAmount = rec.get('PReceivePlan_ReceivePlanAmount');
            var UnReceiveAmount = rec.get('PReceivePlan_UnReceiveAmount');

			entity = {
				Status: me.Status,
				IsUse: 1,
				PPReceivePlanID: me.PK,
				UnReceiveAmount: UnReceiveAmount,
				ReceiveAmount: 0,
				ReceivePlanAmount: ReceivePlanAmount
			}
			if(ExpectReceiveDate) {
				entity.ExpectReceiveDate = ExpectReceiveDate;
			}
			if(rec.get('PReceivePlan_ReceiveGradationID')) {
				entity.ReceiveGradationID = rec.get('PReceivePlan_ReceiveGradationID');
				entity.ReceiveGradationName = rec.get('PReceivePlan_ReceiveGradationName');
			}
			if(rec.get('PReceivePlan_ReceiveManID')) {
				entity.ReceiveManID = rec.get('PReceivePlan_ReceiveManID');
				entity.ReceiveManName = rec.get('PReceivePlan_ReceiveManName');
			}
			if(me.PContractID) {
				entity.PContractID = me.PContractID;
				entity.PContractName = me.PContractName;
			}
		    if(me.PayOrgID) {
				entity.PayOrgID = me.PayOrgID;
			}
			if(me.PayOrg) {
				entity.PayOrgName = me.PayOrg;
			}
			if(me.PClientID) {
				entity.PClientID = me.PClientID;
			}
			if(me.PClientName) {
				entity.PClientName = me.PClientName;
			}
			entity.InputerID = userId;
			entity.InputerName = userName;
			arr.push(entity);
		});
		var params = {
			entity: arr,
			PPReceivePlanID: me.PK
		};
		return params;
	},
	onSave: function() {
		var me = this,
			msgInfo = '',
			url = '';
		var params = me.Entity(params);
		
		
		url = (me.addUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				me.disableControl();
				me.close();
			} else {
				var msg = data.msg;
				msgInfo = msgInfo + '失败';
				JShell.Msg.error(msgInfo + "<br />" + data.msg);
			}
		}, 500);
	},

	/**根据父收款计划ID查询已变更过的数据*/
	getInfoById: function() {
		var me = this,
			n = 0,
			url = JShell.System.Path.getRootUrl(me.selectUrl);

		var where = "preceiveplan.PPReceivePlanID=" + me.PK + " and preceiveplan.IsUse=1";
		url += '&fields=PReceivePlan_Id&where=' + where;

		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value && data.value.count > 0) {
					for(i = 0; i < data.value.list.length; i++) {
						var id = data.value.list[i].PReceivePlan_Id;
						n = n + 1;
						me.delOneById(id, n, data.value.list.length);
					}
				} else {
					me.onSave();
				}
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false, 2000, false);
	},

	/**删除一条数据*/
	delOneById: function(id, i, count) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(i == count) {
					me.onSave();
				}
			}
		}, false, 500, false);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '收款分期',
			dataIndex: 'PReceivePlan_ReceiveGradationName',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '收款金额',
			dataIndex: 'PReceivePlan_ReceivePlanAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			summaryType: 'sum',
			editor: {
				xtype: 'numberfield',
				minValue: 0,
				value: 0,
				allowBlank: false,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						records[0].set('PReceivePlan_UnReceiveAmount', newValue);
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '时间',
			dataIndex: 'PReceivePlan_ExpectReceiveDate',
			width: 100,
			sortable: false,
			menuDisabled: false,
			type: 'date',
			xtype: 'datecolumn',
			format: 'Y-m-d',
			editor: {
				xtype: 'datefield',
				allowBlank: false,
				format: 'Y-m-d'
			}
		}, {
			text: '责任人',
			dataIndex: 'PReceivePlan_ReceiveManName',
			width: 100,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '已收',
			dataIndex: 'PReceivePlan_ReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			text: '待收',
			dataIndex: 'PReceivePlan_UnReceiveAmount',
			width: 100,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable: false,
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var Status = record.get('PReceivePlan_Status');
					var Id = record.get(me.PKField);
					meta.tdAttr = 'data-qtip="<b>删除本行</b>"';
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.getSelectionModel().select(rowIndex);
					me.createDelRec(rowIndex, rec);
				}
			}]
		}, {
			text: '新增标志',
			dataIndex: 'add',
			hidden: true,
			width: 100,
			sortable: false
		}, {
			text: '计划内容ID',
			dataIndex: 'PReceivePlan_ReceiveGradationID',
			hidden: true,
			width: 100,
			sortable: false
		}, {
			text: '责任人ID',
			dataIndex: 'PReceivePlan_ReceiveManID',
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'PReceivePlan_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];

		return columns;
	}
});