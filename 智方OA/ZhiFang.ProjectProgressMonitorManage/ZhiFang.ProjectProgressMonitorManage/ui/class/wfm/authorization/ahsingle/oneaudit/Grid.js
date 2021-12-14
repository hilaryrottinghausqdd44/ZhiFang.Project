/**
 * 单站点授权审核
 * @author longfc	
 * @version 2016-12-15
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.oneaudit.Grid', {
	extend: 'Shell.class.wfm.authorization.ahsingle.basic.Grid',
	
	title: '单站点授权审核',
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
					var editPanel='Shell.class.wfm.authorization.ahsingle.oneaudit.EditPanel';
					me.openEditForm(record,editPanel);
				} else {
					me.openShowForm(record);
				}
			}
		});
		//me.loadLicenceTypeData();
		//me.loadStatusData();
	},
	/**获取状态列表*/
	getLicenceStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
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
		me.defaultWhere = 'IsUse=1 and Status!=1';		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(9, 0, {
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
						var editPanel='Shell.class.wfm.authorization.ahsingle.oneaudit.EditPanel';
						me.openEditForm(rec,editPanel);
					} else {
						me.openShowForm(rec);
					}
				}
			}]
		});
		return columns;
	}
});