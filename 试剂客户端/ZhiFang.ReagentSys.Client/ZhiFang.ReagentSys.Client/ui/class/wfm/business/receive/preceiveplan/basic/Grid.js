/**
 * 收款计划基础列表
 * @author liangyl	
 * @version 2016-12-5
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '收款计划基础列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPReceivePlanByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPReceivePlan',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePReceivePlanByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPReceivePlan',
	/**默认加载*/
	defaultLoad: true,
	/*收款计划内容*/
	ReceiveGradationName: 'CollectionStage',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	defaultOrderBy: [{
		property: 'PReceivePlan_ReceiveDate',
		direction: 'DESC'
	}],

    IsUse:true,
 	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		//使用中的数据才显示
		if(me.IsUse){
			if(me.defaultWhere){
				me.defaultWhere += ' and ';
			}
			me.defaultWhere += 'preceiveplan.IsUse=1';
		}
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},

	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
				buttonToolbarItems.unshift('refresh');
		return buttonToolbarItems;
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
				blankText   : "功能不能为空",
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '收款分期选择',
					defaultWhere: "pdict.BDictType.DictTypeCode='" + me.ReceiveGradationName + "'"
				},
				listeners: {
					check: function(p, record) {
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						records[0].set('PReceivePlan_ReceiveGradationID', record ? record.get('BDict_Id') : '');
						records[0].set('PReceivePlan_ReceiveGradationName', record ? record.get('BDict_CName') : '');
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
				allowBlank: false
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
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PReceivePlanStatus', function() {
			if(!JShell.System.ClassDict.PReceivePlanStatus) {
				JShell.Msg.error('未获取到收款计划状态，请刷新列表');
				return;
			}
			me.load(null, true, autoSelect);
		});
	}
});