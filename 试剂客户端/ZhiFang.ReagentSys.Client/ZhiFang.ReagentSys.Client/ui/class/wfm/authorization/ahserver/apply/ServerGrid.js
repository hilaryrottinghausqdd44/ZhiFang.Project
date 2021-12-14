/**
 * 服务器授权编辑
 * @author longfc
 * @version 2016-12-27
 */
Ext.define('Shell.class.wfm.authorization.ahserver.apply.ServerGrid', {
	extend: 'Shell.class.wfm.authorization.ahserver.basic.ServerGrid',
	title: '服务器授权编辑',
	width: 800,
	height: 500,
	/**是否有日期范围*/
	hasDates: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				if(!JShell.System.ClassDict.LicenceStatus) {
					JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceStatus', function() {});
				}
				var id = record.get(me.PKField);
				var Status = record.get('Status');
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '暂存');
				var sh = JShell.System.ClassDict.getClassInfoByName('LicenceStatus', '商务授权退回');
				if(Status == info.Id.toString() || Status == sh.Id.toString()) {
					var editPanel = 'Shell.class.wfm.authorization.ahserver.apply.EditPanel';
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
		var maxWidth = document.body.clientWidth * 0.72;
		var height = document.body.clientHeight - 8;
		JShell.Win.open('Shell.class.wfm.authorization.ahserver.apply.App', {
			SUB_WIN_NO: '201', //内部窗口编号
			height: height,
			width: maxWidth,
			//resizable:false,
			title: '服务器授权申请',
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
	}
});