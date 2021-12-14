/**
 * 收款计划列表
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.apply.Grid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.EditGrid',
	title: '收款计划列表',
	defaultLoad: false,
	/**通过文字*/
	OverName: '',
	/**退回文字*/
	BackName: '',
	/**使用中*/
	Status: 3,
	/**编辑状态*/
	EditStatus: 3,
	/**合同总金额*/
	Amount: 0,
	/**带分页栏*/
	hasPagingtoolbar: true,
	IsCheckStatus: true,
	/**是否用在合同签署页*/
	IsContractPanel: false,
	/**付款单位*/
	PayOrgID:null,
	/**付款单位*/
	PayOrg:null,
	/**客户*/
	PClientID:null,
	/**客户*/
	PClientName:null,
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.callParent(arguments);
		buttonToolbarItems.splice(4, 0, '-', {
			text: '保存',
			tooltip: '保存',
			iconCls: 'button-save',
			itemId: 'Save',
			handler: function(but, e) {
				me.onSaveClick(true);
			}
		});
		return buttonToolbarItems;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this,
			PlanAmount = 0,
			i = 0;
		if(isSubmit) {
			if(me.store.getCount() > 0) {
				if(!me.IsValid('1')) {
					return;
				}
			}
			me.store.each(function(record) {
				i = i + 1;
				//新增
				if(record.get('add') == '1') {
					me.onSave(record, i);
				}
			});
		}
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(5, 0, {
			text: '状态',
			dataIndex: 'PReceivePlan_Status',
			width: 85,
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
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(records.length > 0) {
			var Add = me.getComponent('buttonsToolbar').getComponent('Add');
			Add.disable();
			if(me.IsContractPanel == false) {
				var Save = me.getComponent('buttonsToolbar').getComponent('Save');
				if(!Save) return;
				Save.disable();
			} else {
				var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
				if(!buttonsToolbar2) return;
				buttonsToolbar2.disable();
			}

			me.columns[8].hide();
		} else {
			me.columns[8].show();
		}
	},
	onSave: function(rec, i) {
		var me = this,
			msgInfo = '',
			url = '',
			AmountCount = 0;

		var id = rec.get(me.PKField);
		var ExpectReceiveDate = JShell.Date.toServerDate(rec.get('PReceivePlan_ExpectReceiveDate'));
		var ReceivePlanAmount = rec.get('PReceivePlan_ReceivePlanAmount');
		var entity = {
			Status: me.Status,
			IsUse: 1,
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
		}
		if(rec.get('PReceivePlan_ReceiveManName')) {
			entity.ReceiveManName = rec.get('PReceivePlan_ReceiveManName');
		}
		if(me.PContractID) {
			entity.PContractID = me.PContractID;
		}
		if(me.PContractName) {
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
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		entity.InputerID = userId;
		entity.InputerName = userName;
		var params = {
			entity: entity
		};
		url = (me.addUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.disableControl();
				if(me.store.getCount() == i) {
					JShell.Action.delay(function() {
						me.onSearch();
					}, null, 500);
				}
			} else {
				var msg = data.msg;
				msgInfo = msgInfo + '失败';
				JShell.Msg.error(msgInfo + "<br />" + data.msg);
			}
		}, false);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '收款分期',
			dataIndex: 'PReceivePlan_ReceiveGradationName',
			width: 100,
			sortable: false,
			editor: {
				xtype: 'uxCheckTrigger',
				allowBlank: false,
				blankText: "功能不能为空",
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '收款分期选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + me.ReceiveGradationName + "'"
				},
				listeners: {
					check: function(p, record) {
						var bo = true;
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						var ReceiveGradationName = record ? record.get('PDict_CName') : '';
						me.store.each(function(rec) {
							if(rec.get('PReceivePlan_ReceiveGradationName') == ReceiveGradationName) {
								JShell.Msg.error('收款分期已存在,请重新选择!');
								bo = false;
								return;
							}
						});
						if(bo == true) {
							records[0].set('PReceivePlan_ReceiveGradationID', record ? record.get('PDict_Id') : '');
							records[0].set('PReceivePlan_ReceiveGradationName', record ? record.get('PDict_CName') : '');
						}
						p.close();
					}
				}
			}
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
				format: 'Y-m-d',
				listeners:{
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						if(newValue != null && newValue != "") {
							newValue = JcallShell.Date.getDate(newValue,true);
						}
						record.set('ExpectReceiveDate', newValue);
						me.getView().refresh();
					},
					focus:function(com,The,eOpts){
						var record = com.ownerCt.editingPlugin.context.record;
						var newValue=record.get('ExpectReceiveDate');
						if(newValue != null && newValue != "") {
							newValue = JcallShell.Date.getDate(newValue,true);
						}
						com.setValue(newValue);
						record.set('ExpectReceiveDate', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '责任人',
			dataIndex: 'PReceivePlan_ReceiveManName',
			width: 100,
			sortable: false,
			editor: {
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.user.CheckApp',
				classConfig: {
					title: '责任人选择'
				},
				listeners: {
					check: function(p, record) {
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						records[0].set('PReceivePlan_ReceiveManID', record ? record.get('HREmployee_Id') : '');
						records[0].set('PReceivePlan_ReceiveManName', record ? record.get('HREmployee_CName') : '');
						p.close();
					}
				}
			}
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
			text: '状态',
			dataIndex: 'PReceivePlan_Status',
			width: 85,
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
					if(!Id) {
						meta.tdAttr = 'data-qtip="<b>删除本行</b>"';
						return 'button-del hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.getSelectionModel().select(rowIndex);
					var Id = rec.get(me.PKField);
					if(!Id) {
						me.createDelRec(rowIndex, rec);
					}
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