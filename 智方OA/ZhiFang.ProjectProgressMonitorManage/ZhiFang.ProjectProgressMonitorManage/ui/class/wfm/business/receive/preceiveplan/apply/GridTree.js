/**
 * 列表树
 * @author liangyl
 * @version 2016-12-23
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.apply.GridTree', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.SimpleGridTree',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPReceivePlan',

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
//	/**合同总金额*/
//	Amount: 0,
	/**是否用在合同签署页*/
	IsContractPanel: false,
    IsEdit:false,
	initComponent: function() {
		var me = this;
		//列表字段
		me.columns = me.createGridColumns();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		//获取树列表
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '收款分期',
			xtype: 'treecolumn',
			dataIndex: 'text',
			width: 150,
			sortable: false
		}, {
			text: '收款分期',
			dataIndex: 'ReceiveGradationName',
			width: 100,
			sortable: false,
			hidden:true,
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

						var roonodes = me.getRootNode().childNodes; //获取主节点
						for(var i = 0; i < roonodes.length; i++) { //从节点中取出子节点依次遍历
							var rootnode = roonodes[i];
							if(ReceiveGradationName == rootnode.data.ReceiveGradationName) {
								JShell.Msg.error('收款分期已存在,请重新选择!');
								bo = false;
								return;
							}
						}
						if(bo == true) {
							records[0].set('ReceiveGradationID', record ? record.get('PDict_Id') : '');
							records[0].set('ReceiveGradationName', record ? record.get('PDict_CName') : '');
							me.getView().refresh();
//							records[0].set('text', record ? record.get('PDict_CName') : '');
						}
						p.close();
					}
				}
			}
		}, {
			text: '收款金额',
			dataIndex: 'ReceivePlanAmount',
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
						records[0].set('UnReceiveAmount', newValue);
						me.getView().refresh();
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
			dataIndex: 'ExpectReceiveDate',
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
			dataIndex: 'ReceiveManName',
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
						records[0].set('ReceiveManID', record ? record.get('HREmployee_Id') : '');
						records[0].set('ReceiveManName', record ? record.get('HREmployee_CName') : '');
						p.close();
					}
				}
			}
		}, {
			text: '已收',
			dataIndex: 'ReceiveAmount',
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
			dataIndex: 'UnReceiveAmount',
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
			dataIndex: 'Status',
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
					var Status = record.get('Status');
					var Id = record.get(me.PKField);
					if(!Id) {
						meta.tdAttr = 'data-qtip="<b>删除本行</b>"';
						return 'button-del hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
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
			dataIndex: 'ReceiveGradationID',
			hidden: true,
			width: 100,
			sortable: false
		}, {
			text: '责任人ID',
			dataIndex: 'ReceiveManID',
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];
		return columns;
	},
	createDockedItems: function() {
		var me = this;

		var items = [{
			iconCls: 'button-refresh',
			itemId: 'refresh',
			tooltip: '刷新数据',
			handler: function() {
				me.onRefreshClick();
			}
		}, '-', {
			iconCls: 'button-arrow-in',
			itemId: 'minus',
			tooltip: '全部收缩',
			handler: function() {
				me.onMinusClick();
			}
		}, {
			iconCls: 'button-arrow-out',
			itemId: 'plus',
			tooltip: '全部展开',
			handler: function() {
				me.onPlusClick();
			}
		}, '-', {
			text: '新增',
			tooltip: '新增',
			iconCls: 'button-add',
			itemId: 'Add',
			handler: function(but, e) {
				me.IsEdit=true;
				me.changeEdit();
				me.createAddRec();

			}
		}, '-', {
			text: '保存',
			tooltip: '保存',
			iconCls: 'button-save',
			itemId: 'Save',
			handler: function(but, e) {
				me.onSaveClick(true);
			}
		}, '-', {
			xtype: 'label',
			text: '',
			itemId: 'AmountText',
			name: 'AmountText',
			style: "font-weight:bold;color:red;",
			margin: '0 10 0 10'
		}];

		return [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: items
		}];
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function(isSubmit) {
		var me = this,
			PlanAmount = 0,
			i = 0;
		me.focus(false);
		if(isSubmit) {
			var roonodes = me.getRootNode().childNodes; //获取主节点
			if(roonodes.length > 0) {
				if(!me.IsValid('1')) {
					return;
				}
			}
			if(roonodes.length == 0) return;
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = roonodes.length;
			for(var i = 0; i < roonodes.length; i++) { //从节点中取出子节点依次遍历
				var record = roonodes[i];
				me.onSave(record, i,roonodes);
			}
		}
	},
	changeEdit:function(){
		var me=this;
		  me.on({
		    beforeedit : function(editor, e) { 
		    	if(me.IsEdit){ return true; }
		    	else{
		    		 if(e.colIdx== 1 && e.record.data.direct == 0){ 
			            return true; 
			        }else if(e.colIdx == 2 && e.record.data.direct == 1){ 
			            return true; 
			        }else{ 
			            return false; 
			        } 
		    	}
		    } 
		}); 
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.enableControl(); 
		var Add = me.getComponent('topToolbar').getComponent('Add');
		var Save = me.getComponent('topToolbar').getComponent('Save');
		if(records && records.length>0) {
			me.IsEdit=false;
			Add.disable();
			if(me.IsContractPanel == false) {
				if(!Save) return;
				Save.disable();
			}
			me.columns[8].hide();
			me.columns[1].hide();
			me.columns[0].show();
		} else {
			me.IsEdit=true;
			Add.enable();
			Save.enable();
			me.columns[8].show();
			me.columns[1].show();
			me.columns[0].hide();
		}
        me.changeEdit();
	},
	onSave: function(rec, i,roonodes) {
		var me = this,
			msgInfo = '',
			url = '',
			AmountCount = 0,n=0;
		var id = rec.get(me.PKField);
		var ExpectReceiveDate = JShell.Date.toServerDate(rec.get('ExpectReceiveDate'));
		var ReceivePlanAmount = rec.get('ReceivePlanAmount');
		var entity = {
			Status: me.Status,
			IsUse: 1,
			ReceivePlanAmount: ReceivePlanAmount
		}
		if(ExpectReceiveDate) {
			entity.ExpectReceiveDate = ExpectReceiveDate;
		}
		if(rec.get('ReceiveGradationID')) {
			entity.ReceiveGradationID = rec.get('ReceiveGradationID');
			entity.ReceiveGradationName = rec.get('ReceiveGradationName');
		}
		if(rec.get('ReceiveManID')) {
			entity.ReceiveManID = rec.get('ReceiveManID');
		}
		if(rec.get('ReceiveManName')) {
			entity.ReceiveManName = rec.get('ReceiveManName');
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
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				if(me.saveErrorCount == 0){
					me.fireEvent('save');
				}
			}
		}, false);
	},
	/**增加一行*/
	createAddRec: function() {
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var obj = {
			ReceiveGradationID: me.GradationID,
			ReceiveGradationName: me.GradationName,
			ContractNumber: '',
			ReceivePlanAmount: 0,
			ExpectReceiveDate: '',
			ReceiveManName: me.Principal,
			ReceiveManID: me.PrincipalID,
			ReceiveAmount: me.ReceiveAmount,
			UnReceiveAmount: 0,
			Id: '',
			InputerID: userId,
			InputerName: userName,
			add: '1',
			leaf: true,
			text: '',
			tid: '1',
			expanded: true,
			icon: null
		};
		var pnode = me.store.getRootNode();
		pnode.appendChild([obj]);	//添加子节点
		pnode.set('leaf',false);
		pnode.expand();
	},
	/**删除本行*/
	createDelRec: function(i, rec) {
		var me = this;
		rec.remove();

	},
	/**验证*/
	IsValid: function(str) {
		var me = this,
			PlanAmount = 0,
			isbool = true,
			Msg = '',
			Count = 0;
		var roonodes = me.getRootNode().childNodes; //获取主节点
		for(var i = 0; i < roonodes.length; i++) { //从节点中取出子节点依次遍历
			var rec = roonodes[i];
			var ReceivePlanAmount = rec.get('ReceivePlanAmount');
			PlanAmount = PlanAmount + parseFloat(ReceivePlanAmount);
			var ReceiveGradationName = rec.get('ReceiveGradationName');
			ReceiveGradationName.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
			
			if(!ReceiveGradationName) {
				JShell.Msg.error('收款分期不能为空!');
				isbool = false;
				return;
			}
			var ExpectReceiveDate = rec.get('ExpectReceiveDate');
			if(!ExpectReceiveDate) {
				//				Msg=Msg+'收款时间不能为空!'+'<br/>'
				JShell.Msg.error('收款时间不能为空!');
				isbool = false;
				return;
			}
			if(ReceivePlanAmount.toString() == '0' || ReceivePlanAmount.toString() == '0.00') {
				//				Msg=Msg+'收款金额不能等于0!'+'<br/>'
				JShell.Msg.error('收款金额不能等于0!');
				isbool = false;
				return;
			}
		}
	    Count = Number(PlanAmount.toFixed(2));
	    var AmountStr=me.Amount+'';
	    var CountStr=Count+'';
		if(AmountStr!= CountStr) {
			if(str != '1') {
				Msg = '新收款计划总额:' + Count + '不等于原始收款计划未收总额:' + me.Amount + ",请校验!";
			} else {
				Msg = '收款总额:' + Count + '不等于合同金额:' + me.Amount + ",请校验!";
			}
			JShell.Msg.error(Msg);
			isbool = false;
			return;
		}
		return isbool;
	},
	onAddClick: function() {
		var me = this;
		me.createAddRec();
	},
	//显示合同金额
	changeAmountText: function(Amount, str) {
		var me = this;
		var buttonsToolbar = me.getComponent('topToolbar');
		if(!buttonsToolbar) return;
		var AmountText = buttonsToolbar.getComponent('AmountText');
		if(Amount) {
			AmountText.setText(str + Amount);
		}
	}
});