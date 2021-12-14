/**
 * 普通用户的知识库信息查看应用
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.knowledgebase.show.App', {
	extend: 'Shell.class.qms.file.basic.ShowApp',
	title: '知识库信息',
	basicGrid: 'Shell.class.qms.file.knowledgebase.show.Grid',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var Grid = me.getComponent('Grid');
		Grid.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				Grid.openShowTabPanel(record);
			},
			onShowClick: function() {
				var me = this;
				var records = me.getSelectionModel().getSelection();
				if(records && records.length < 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				Grid.openShowTabPanel(records[0]);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.FTYPE='3';
		me.callParent(arguments);
	}
});