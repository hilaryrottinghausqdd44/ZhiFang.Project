/**
 * 还款列表
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.prepayment.apply2.Grid', {
	extend: 'Shell.class.wfm.business.prepayment.basic.Grid',
	title: '还款列表',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPRepaymentByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePRepaymentByField',
	/**默认员工类型*/
	defaultUserType: '',
	/**默认显示状态*/
	defaultStatusValue: '',
	TemporaryStatus: true,
	hasStatus:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('PRepayment_Status');
				//审核不通过可以编辑
				me.openShowForm(id);
			}
		});
		me.onAddButtons();
	},

	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PRepaymentStatus',function(){
			if(!JShell.System.ClassDict.PRepaymentStatus){
    			JShell.Msg.error('未获取到还款状态，请刷新列表');
    			return;
    		}
    	});
    	me.defaultWhere = 'prepayment.Status=4';
    	me.load(null, true, autoSelect);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(5, 0, {
			xtype: 'actioncolumn',
			text: '编辑',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PRepayment_Id');
					me.openEditForm(id);
				}
			}]
		});
		return columns;
	},
	/**获取状态列表*/
	getStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;text-align:center']);
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
		JShell.Win.open('Shell.class.wfm.business.prepayment.apply2.Form', {
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
	openEditForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.prepayment.apply2.Form', {
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
	}
});