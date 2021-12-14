/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.authorization.ahserver.oneaudit.ServerGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ServerGrid',
	title: '服务器授权审核',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	//defaultStatusValue:'2',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('Status');
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '申请');
				var tp = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '特批授权退回');
				if(Status == info.Id || Status == tp.Id) {
					var editPanel = 'Shell.class.wfm.authorization.ahserver.oneaudit.EditPanel';
					me.openEditForm(record, editPanel);
				} else {
					me.openShowForm(record);
				}
			}
		});
	},
	/**获取状态列表*/
	getLicenceStatusData: function(statusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in statusList) {
			var obj = statusList[i];
			if(obj.Id != 1) {
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
		//(状态!=暂存 and OneAuditID is null) or (状态!=暂存 and OneAuditID=当前登录者)
		//me.defaultWhere = 'IsUse=1 and ((OneAuditID is null and Status!=1) or (OneAuditID=' + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + " and Status!=1))";
		me.defaultWhere = 'ahserverlicence.IsUse=1 and ahserverlicence.Status!=1';
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(12, 0, {
			xtype: 'actioncolumn',
			text: '审核',
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
					var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '申请');
					var tp = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '特批授权退回');
					if(Status == info.Id || Status == tp.Id) {
						var editPanel = 'Shell.class.wfm.authorization.ahserver.oneaudit.EditPanel';
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