/**
 * 服务器授权
 * @author longfc	
 * @version 2016-12-26
 */
Ext.define('Shell.class.wfm.authorization.ahserver.twoaudit.ServerGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ServerGrid',
	title: '服务器授权审核',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: false,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchSpecialApprovalAHServerLicenceByHQL',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('Status');
				var IsSpecially = record.get('IsSpecially');
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '商务授权通过');
				if(Status == info.Id) {
					var editPanel = 'Shell.class.wfm.authorization.ahserver.twoaudit.EditPanel';
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
		//me.defaultWhere = 'IsUse=1 and IsSpecially=1 and ((TwoAuditID=null and Status=4 and IsSpecially=1) or (TwoAuditID=' + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) + "))";
		me.defaultWhere = 'IsUse=1 and IsSpecially=1';
		me.callParent(arguments);
	}
});