/**
 * 文档查看页签
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.show.ShowTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '文档信息查看',
	border: false,
	IsDiscuss: true,
	FFileId: '',
	FTYPE: '1',
	PK: '',
	FileOperationIsLoad: false,
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	/**查看文档时是否需要添加文档阅读记录信息:1需要,0:不需要*/
	isAddFFileReadingLog: 1,
	/**查看文档时是否需要添加文档操作记录信息:1需要,0:不需要*/
	isAddFFileOperation: 0,
	defaultWhere: "",
	/**是否显示内容页签*/
	hasContent: true,
	/**是否显示文档详情页签*/
	hasFFileOperation: false,
	/**是否显示操作记录页签*/
	hasOperation: true,
	/**是否显示阅读记录页签*/
	hasReadingLog: true,
	/**是否显示修订记录页签*/
	hasRevise: false,
	/**PDF预览0下载按钮显示，1不显示*/
	DOWNLOAD:'',
	/**PDF预览0打印按钮显示，1不显示*/
	PRINT:'',
	/**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
    BUILTIN:'1',
	initComponent: function() {
		var me = this;
		me.items = [];
		me.FTYPE=me.FTYPE||"1";
		me.bodyPadding = 1;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.ContentForm.on({
			load: function(p, data) {
				me.setTitle(data.value.FFile_Title);
			}
		});
		me.FFileOperationForm.on({
			load: function(p, data) {
				me.setTitle(data.value.FFile_Title);
			}
		});
		me.ontabchange();
	},
	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;

		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'Interaction':
						me.Interaction.PK = me.PK;
						me.Interaction.FileId = me.FFileId;
						break;
					case 'OperationForm':
						me.OperationForm.PK = me.PK;
						me.OperationForm.FFileId = me.FFileId;
						break;
					case 'ReadlogForm':
						me.ReadlogForm.PK = me.PK;
						me.ReadlogForm.FFileId = me.FFileId;
						break;
					case 'ReviseRecGrid':
						me.ReviseRecGrid.PK = me.PK;
						break;
					default:
						break
				}
			}
		});
		me.setTitle();
	},
	/**加载文档内容信息*/
	loadFFileOperationForm: function() {
		var me = this;
		if(me.FileOperationIsLoad == false) {
			me.FFileOperationForm.load(me.FFileId);
			me.FileOperationIsLoad = true;
		}
	},
	createItems: function() {
		var me = this;
		var items = [];

		me.ContentForm = Ext.create('Shell.class.qms.file.basic.DetailedContent', {
			itemId: 'ContentForm',
			hasButtontoolbar: false,
			hidden:!me.hasContent,
			border: false,
			title: '内容',
			FTYPE: me.FTYPE,
			PK: me.PK,
			/**PDF预览0下载按钮显示，1不显示*/
			DOWNLOAD:me.DOWNLOAD,
			/**PDF预览0打印按钮显示，1不显示*/
			PRINT:me.PRINT,
			BUILTIN:me.BUILTIN,
			/**查看文档时是否需要添加文档阅读记录信息:1需要,0:不需要*/
			isAddFFileReadingLog: me.isAddFFileReadingLog,
			/**查看文档时是否需要添加文档操作记录信息:1需要,0:不需要*/
			isAddFFileOperation: me.isAddFFileOperation,
			FFileId: me.FFileId
		});
		
		me.FFileOperationForm = Ext.create('Shell.class.qms.file.basic.FFileDetailedForm', {
			title: '详细信息',
			header: false,
			hidden:!me.hasFFileOperation,
			itemId: 'FFileOperationForm',
			FTYPE : me.FTYE,
			PK : me.PK,
			FFileId : me.FFileId,
			/**查看文档时是否需要添加文档阅读记录信息:1需要,0:不需要*/
			isAddFFileReadingLog: me.isAddFFileReadingLog,
			/**查看文档时是否需要添加文档操作记录信息:1需要,0:不需要*/
			isAddFFileOperation: me.isAddFFileOperation,
			border: false,
			isShowForm: false
		});
		
		me.ReviseRecGrid = Ext.create('Shell.class.qms.file.file.revise.ReviseRecGrid', {
			title: '修订记录',
			header: false,
			itemId: 'ReviseRecGrid',
	        hidden:!me.hasRevise,
			PK: me.PK,
			border: true,
			isShowForm: false
		});
		
		me.Interaction = Ext.create('Shell.class.qms.file.interaction.App', {
			title: '交流',
			header: false,
			hidden: !me.IsDiscuss,
			itemId: 'Interaction',
			border: false,
			FileId: me.FFileId,
			isShowForm: false,
			defaultWhere: me.defaultWhere
		});
	    me.OperationForm = Ext.create('Shell.class.qms.file.operation.Grid', {
			title: '操作记录',
			header: false,
		    hasButtontoolbar: false,
		    hasPagingtoolbar: false,
			itemId: 'OperationForm',
			 /**默认每页数量*/
	        defaultPageSize: 500,
			PK: me.PK,
			hidden:!me.hasOperation,
			FFileId: me.FFileId,
			border: true,
			isShowForm: false
		});
		
		me.ReadlogForm = Ext.create('Shell.class.qms.file.readinglog.Grid', {
			title: '阅读记录',
			header: false,
			hasButtontoolbar: false,
			hasPagingtoolbar: false,
			itemId: 'ReadlogForm',
			 /**默认每页数量*/
	        defaultPageSize: 500,
	        hidden:!me.hasReadingLog,
			PK: me.PK,
			FFileId: me.FFileId,
			border: true,
			isShowForm: false
		});
		if(me.hasContent){
			items.push(me.ContentForm);
		}
		if(me.hasFFileOperation){
			items.push(me.FFileOperationForm);
		}
		if(me.hasRevise)items.push(me.ReviseRecGrid);
		if(me.IsDiscuss){
			items.push(me.Interaction);
		}
		if(me.hasOperation){
			items.push(me.OperationForm);
		}
		if(me.hasReadingLog){
			items.push(me.ReadlogForm);
		}	
		
		return items;
	}
});