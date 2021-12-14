/**
 * 单站点授权批准
 * @author longfc	
 * @version 2016-12-15
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.twoaudit.Grid', {
	extend: 'Shell.class.wfm.authorization.ahsingle.basic.Grid',

	title: '单站点授权批准',
	
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchSpecialApprovalAHSingleLicenceByHQL',
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	defaultWhere: '',
	/**隐藏授权类型*/
	hiddenLicenceTypeId:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('Status');
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '商务授权通过');
				if(Status == info.Id) {
					var editPanel = 'Shell.class.wfm.authorization.ahsingle.twoaudit.EditPanel';
					me.openEditForm(record, editPanel);
				} else {
					me.openShowForm(record);
				}
			}
		});
	},
	/**获取状态列表*/
	getLicenceStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			if(obj && obj.Id > 3 && obj.Id != 5) {
				var style = ['font-weight:bold;text-align:center'];
				if(obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
			}
		}
		return data;
	},
	initComponent: function() {
		var me = this;
		//me.defaultWhere = 'IsUse=1 and ((TwoAuditID=null and Status=4 and LicenceKey is null) or (TwoAuditID=' + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + "))";
		//单站点审批,现在只针对同一用户,同一网卡.对同一程序或仪器,它临时授权天数(包括当前)累计超过60天,需要特批
		me.defaultWhere = 'IsUse=1';
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(9, 0, {
			xtype: 'actioncolumn',
			text: '审批',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var Status = rec.get('Status');
					var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '商务授权通过');
					if(Status == info.Id) {
						var editPanel = 'Shell.class.wfm.authorization.ahsingle.twoaudit.EditPanel';
						me.openEditForm(rec, editPanel);
					} else {
						me.openShowForm(rec);
					}
				}
			}]
		});
		return columns;
	}
});