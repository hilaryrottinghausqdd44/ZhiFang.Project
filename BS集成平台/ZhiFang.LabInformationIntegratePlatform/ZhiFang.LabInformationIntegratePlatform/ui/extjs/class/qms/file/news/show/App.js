/**
 * 普通用户的新闻信息查看应用
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.news.show.App', {
	extend: 'Shell.class.qms.file.basic.ShowApp',
	title: '新闻信息',
	basicGrid: 'Shell.class.qms.file.news.show.Grid',
	/**PDF预览0下载按钮显示，1不显示*/
	DOWNLOAD:'',
	/**PDF预览0打印按钮显示，1不显示*/
	PRINT:'',
	/**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
    BUILTIN:'1',
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
		me.FTYPE='2';
		me.callParent(arguments);
	}
});