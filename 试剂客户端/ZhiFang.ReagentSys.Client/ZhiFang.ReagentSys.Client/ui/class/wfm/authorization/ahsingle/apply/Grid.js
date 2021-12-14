/**
 * 单站点授权
 * @author longfc
 * @version 2016-12-10
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.apply.Grid', {
	extend: 'Shell.class.wfm.authorization.ahsingle.basic.Grid',
	title: '单站点授权列表',
	width: 800,
	height: 500,
	//defaultStatusValue:3,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('Status');
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '暂存');
				var sh = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '商务授权退回');
				if(Status == info.Id.toString() || Status == sh.Id.toString()) {
					var editPanel = 'Shell.class.wfm.authorization.ahsingle.apply.EditTabPanel';
					me.openEditForm(record, editPanel);
				} else {
					me.openShowForm(record);
				}
			}
		});
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.callParent(arguments);
		buttonToolbarItems.splice(1, 0, '-', 'add');
		return buttonToolbarItems;
	},
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.wfm.authorization.ahsingle.apply.Form', {
			SUB_WIN_NO: '1', //内部窗口编号
			//resizable:false,
			title: '单站点授权',
			formtype: 'add',
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = 'IsUse=1 and ApplyID=' + JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		me.callParent(arguments);
	},
	/**修改*/
	openEditForm: function(record, editPanel) {
		var me = this;
		var id = record.get(me.PKField);
		var PClientID = record.get("PClientID");
		var maxWidth = document.body.clientWidth * 0.76;
		var height = document.body.clientHeight - 8;
		if(PClientID == "" || PClientID == undefined)
			PClientID = null;
		JShell.Win.open(editPanel, {
			SUB_WIN_NO: '101',
			width: 595,
			height: 465,
			PK: id,
			PClientID: PClientID,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	}
});