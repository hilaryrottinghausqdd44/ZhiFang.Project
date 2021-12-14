/**
 * 变更后的收款计划
 * @author liangyl	
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.changeaudit.ChangeGrid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.Grid',
	title: '变更后的收款计划列表',
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**底部工具栏*/
	hasBottomToolbar: true,
	/**通过文字*/
	OverName: '审核通过',
	/**退回文字*/
	BackName: '审核不通过',
	/**审核通过状态*/
	OverStatus: 3,
	/**父收款审核通过状态*/
	OverPlanStatus: 6,
	/**审核不通过状态*/
	BackStatus: 1,
	/**父收款审核不通过状态*/
	BackPlanStatus: 3,

	PPReceivePlanID: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	defaultLoad: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.push({
			xtype: 'label',
			text: '变更后的计划',
			style: "font-weight:bold;color:blue;",
			margin: '0 0 0 10'
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(0, 0, {
			text: '合同',
			dataIndex: 'PReceivePlan_PContractName',
			hidden:true,
			width: 150,
			hideable: false
		},{
			text: '合同Id',
			dataIndex: 'PReceivePlan_PContractID',
			hidden:true,
			width: 150,
			hideable: false
		});
		columns.splice(3, 0, {
			text: '变更金额',
			dataIndex: 'PReceivePlan_UnReceiveAmount',
			width: 80,
			hidden:true,
			sortable: false,
			menuDisabled: false,
			xtype: 'numbercolumn',
			type: 'float',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				meta.style = 'font-weight:bold;';
				return value;
			}
		});
		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasBottomToolbar) items.push(me.createBottomtoolbar());
		return items;
	},
		/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		if (items.length == 0) {
			if (me.hasRefresh) items.push('refresh');
			if (me.hasAdd) items.push('add');
			if (me.hasEdit) items.push('edit');
			if (me.hasDel) items.push('del');
			if (me.hasShow) items.push('show');
			if (me.hasSave) items.push('save');
			if (me.hasSearch) items.push('->', {
				type: 'search',
				info: me.searchInfo
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			height:27,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**工具栏*/
	createBottomtoolbar: function() {
		var me = this,
			items = [];

		if(items.length == 0) {
			items.push('->');
			//通过按钮
			if(me.OverName) {
				items.push({
					text: me.OverName,
					tooltip: me.OverName,
					iconCls: 'button-save',
					handler: function() {
						me.onSaveClick(true);
					}
				});
			}
			//退回按钮
			if(me.BackName) {
				items.push({
					text: me.BackName,
					tooltip: me.BackName,
					iconCls: 'button-save',
					handler: function() {
						me.onSaveClick(false);
					}
				});
			}
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'PReceivePlanStatus', function() {
			if(!JShell.System.ClassDict.PReceivePlanStatus) {
				JShell.Msg.error('未获取到收款计划状态，请刷新列表');
				return;
			}
			me.defaultWhere = "preceiveplan.IsUse=1 and preceiveplan.PPReceivePlanID=" + me.PPReceivePlanID;
			me.load(null, true, autoSelect);
		});
	},

	/**获取当前用户所负责的客户Ids数组*/
	onSaveClick: function(isSubmit) {
		var me = this,
			url = '';
		if(isSubmit) {
			url = '/SingleTableService.svc/ST_UDTO_ChangeSubmitPReceivePlan';
		} else {
			url = '/SingleTableService.svc/ST_UDTO_UnChangeSubmitPReceivePlan';
		}
		url += '?PPReceivePlanID=' + me.PPReceivePlanID;
		url = JShell.System.Path.getRootUrl(url);
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				JShell.Msg.error(msg);
			}
		});
	}

});