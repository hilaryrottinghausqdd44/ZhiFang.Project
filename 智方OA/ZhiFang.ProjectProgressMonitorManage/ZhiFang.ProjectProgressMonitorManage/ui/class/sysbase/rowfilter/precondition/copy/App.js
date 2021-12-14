/***
 * 将模块服务的预置条件项复制新增到选择的模块服务
 * @author longfc
 * @version 2017-08-22
 */
Ext.define('Shell.class.sysbase.rowfilter.precondition.copy.App', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '将模块服务的预置条件项复制新增到选择的模块服务',
	header: true,
	border: false,
	width: 780,
	/**默认加载数据时启用遮罩层*/
	hasLoadMask: true,
	layout: {
		type: 'border'
	},
	/**模块ID*/
	moduleId: null,
	/**预置条件Id*/
	preconditionId: null,
	/**预置条件实体编码*/
	entityCode: '',
	/**将某一预置条件下选择的行过滤条件复制新增到指定的预置条件项*/
	copyRowFilterUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_CopyRBACRowFilterOfPreconditionsIdStr',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.RowfilterGrid.on({
			accept: function(p, records) {
				me.onCopyAccept(records);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		var defaultWhere1 = "rbacpreconditions.IsUse=1 and rbacpreconditions.Id!=" + me.preconditionId + " and rbacpreconditions.EntityCode='" + me.entityCode + "'";
		me.PreconditionsGrid = Ext.create('Shell.class.sysbase.rowfilter.precondition.copy.PreconditionsGrid', {
			width: 390,
			itemId: 'PreconditionsGrid',
			name: 'PreconditionsGrid',
			title: '预置条件项选择',
			region: 'west',
			checkOne: false,
			hasAcceptButton: false,
			defaultWhere: defaultWhere1,
			split: true
		});
		var defaultWhere2 = "rbacrowfilter.IsPreconditions=1 and rbacrowfilter.RBACPreconditions.Id=" + me.preconditionId;
		me.RowfilterGrid = Ext.create('Shell.class.sysbase.rowfilter.precondition.copy.RowfilterGrid', {
			itemId: 'RowfilterGrid',
			region: 'center',
			title: '行过滤条件选择',
			checkOne: false,
			defaultWhere: defaultWhere2,
			split: true
		});
		var appInfos = [me.PreconditionsGrid, me.RowfilterGrid];
		return appInfos;
	},
	onCopyAccept: function(records) {
		var me = this;

		var records = me.PreconditionsGrid.getSelectionModel().getSelection();
		var rowRecords = me.RowfilterGrid.getSelectionModel().getSelection();
		if(!records || records.length < 1) {
			JShell.Msg.error(" 请选择模块服务行后再操作!");
			return;
		}
		if(!rowRecords || rowRecords.length < 1) {
			JShell.Msg.error(" 请选择行过滤条件后再操作!");
			return;
		}
		var preconditionsIdStr = "";
		for(var i = 0; i < records.length; i++) {
			preconditionsIdStr += records[i].get('RBACPreconditions_Id');
			if(i < records.length - 1) preconditionsIdStr += ",";
		}
		var rowfilterIdStr = "";
		for(var i = 0; i < rowRecords.length; i++) {
			rowfilterIdStr += rowRecords[i].get('RBACRowFilter_Id');
			if(i < rowRecords.length - 1) rowfilterIdStr += ",";
		}

		if(preconditionsIdStr && rowfilterIdStr) {
			var url = JShell.System.Path.getRootUrl(me.copyRowFilterUrl) + "?preconditionsIdStr=" + preconditionsIdStr + "&rowfilterIdStr=" + rowfilterIdStr;
			me.showMask(me.saveText); //显示遮罩层
			JShell.Server.get(url, function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					me.fireEvent('copyAccept', me);
					JShell.Msg.alert("模块服务的预置条件项复制到指定的模块服务成功", null, 1000);
				} else {
					me.fireEvent('saveerror', me);
					JShell.Msg.error(data.msg);
				}
			});
		}
	}
});