/**
 * 普通用户新闻查看列表
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.news.show.Grid', {
	extend: 'Shell.class.qms.file.show.Grid',
	title: '新闻查看列表',
	hasRefresh: true,
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	hasOperation: false,
	hasCheckBDictTree: true,
	remoteSort: false,
	/*推送操作列是否隐藏**/
	hiddenWeiXinMessagePush: true,
	defaultStatusValue: "5",
	hiddenFFileStatus: true,
	/**是否显示内容页签*/
	hasContent: true,
	/**是否显示文档详情页签*/
	hasFFileOperation: false,
	/**是否显示操作记录页签*/
	hasOperation: false,
	/**是否显示阅读记录页签*/
	hasReadingLog: false,
	FFileDateTypeList: [
		["ffile.DrafterDateTime", '起草时间'],
		["ffile.PublisherDateTime", '发布时间']
	],
	/**PDF预览0下载按钮显示，1不显示*/
	DOWNLOAD:'',
	/**PDF预览0打印按钮显示，1不显示*/
	PRINT:'',
    /**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
    BUILTIN:'1',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列,添加查看附件列
	 * @author liangyl
     * @version 2018-05-22
	 * */
	createNewColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);

	    columns.splice(16,0,{
			xtype: 'actioncolumn',
			text: '附件',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('FFile_Id');
					me.onSetAttachment(rec);
					//me.showAttachment(id);
				}
			}]
		});
		return columns;
	},
	/**预览时添加控制，打印或者下载
	 * @author liangyl
     * @version 2018-08-22
	 * */
	onSetAttachment: function(rec) {
		var me = this;
		var maxWidth = document.body.clientWidth - 20;
		var height = document.body.clientHeight - 20;
		var config = {
		    height:height,
		    width:maxWidth,
			 /**PDF预览0下载按钮显示，1不显示*/
			DOWNLOAD:me.DOWNLOAD,
			/**PDF预览0打印按钮显示，1不显示*/
			PRINT:me.PRINT,
		    /**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
            BUILTIN:me.BUILTIN,
			SUB_WIN_NO:'11'
		};
		var id = rec.get('FFile_Id');
		if(id && id != null) {
			config.PK = id;
		}
		JShell.Win.open('Shell.class.qms.file.basic.AttachmentTab', config).show();
	}
});