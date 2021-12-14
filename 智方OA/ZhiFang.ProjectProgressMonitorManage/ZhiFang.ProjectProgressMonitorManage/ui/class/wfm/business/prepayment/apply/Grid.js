/**
 * 还款列表
 * @author liangyl
 *第一种是还款人填写申请然后财务审批然后完成
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.prepayment.apply.Grid', {
	extend: 'Shell.class.wfm.business.prepayment.basic.Grid',
	title: '还款列表',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPRepaymentByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePRepaymentByField',
	/**默认员工类型*/
	defaultUserType: 'ApplyManID',
	/**默认显示状态*/
	defaultStatusValue: '',
	TemporaryStatus: true,
	/**默认加载数据*/
	defaultLoad: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('PRepayment_Status');
				//审核不通过或者暂存可以编辑
				if(Status == '3' ||  Status == '1') {
					me.openEditForm(id, 2);
				} else {
					me.openShowForm(id);
				}
			}
		});
		me.onAddButtons();
	},

	/**获取状态列表*/
	getStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#0A0A0A;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			var style = ['font-weight:bold;text-align:center'];
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(6, 0, {
			xtype: 'actioncolumn',
			text: '操作记录',
			align: 'center',
			width: 60,
			hideable: false,
			sortable: false,
			menuDisabled: true,
//			hidden:true,
//			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openOperationGrid(rec);
				}
			}]
		});
		return columns;
	},

	onAddButtons: function(list) {
		var me = this,
			arr = list || [],
			len = arr.length,
			items = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		buttonsToolbar.insert(1, ['-', {
			xtype: 'button',
			iconCls: 'button-add',
			text: '还款申请',
			tooltip: '还款申请',
			handler: function(but) {
				me.onAddClick();
			}
		}]);
	},
	/**新增任务*/
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.prepayment.apply.Form', {
			title: '还款申请',
			formtype: "add",
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**修改*/
	openEditForm: function(id, Status) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.prepayment.apply.Form', {
			PK: id,
			formtype: "edit",
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**打开操作记录列表*/
	openOperationGrid: function(rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var config = {
			title: '操作记录',
			formtype: 'show',
			itemId: 'Operate',
			hasLoadMask: false,
			width:350,
			height:280,
			PK: id
		};
		var win = JShell.Win.open('Shell.class.wfm.business.prepayment.operate.Panel', config).show();
	}
});