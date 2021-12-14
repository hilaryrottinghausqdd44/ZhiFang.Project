/**
 * 知识库管理列表
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.knowledgebase.manage.Grid', {
	extend: 'Shell.class.qms.file.manage.Grid',
	title: '知识库管理',
	/**是否禁用按钮是否显示*/
	HiddenButtonLock: false,
	/**是否置顶按钮是否显示*/
	HiddenButtonDoTop: false,
	/**撤销置顶按钮是否显示*/
	HiddenButtonDoNoTop: false,
	/**文档状态默认为发布*/
	defaultStatusValue: "",
	isManageApp: true,
	/*文档状态**/
	FFileStatusList: [
		[0, JShell.All.ALL],
		["1", '暂时存储'],
		["5", '已发布']
	],
	FFileDateTypeList: [
		["ffile.DrafterDateTime", '起草时间'],
		["ffile.PublisherDateTime", '发布时间']
	],
	initComponent: function() {
		var me = this;
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "'))";
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];
		columns = me.initDefaultColumn(columns);
		columns.push(me.createreFFileNo(), me.createreVersionNo());

		columns.push(me.createreIsUse());
		columns.push(me.createEditBDictTreeColumn());
		columns.push(me.createEditColumn());
		//修改发布信息列
		columns.push(me.createPublisher());
		//是否有交流列
		columns.push(me.createInteraction());
		//是否有操作记录查看列
		columns.push(me.createOperation());
		//阅读记录列
		columns.push(me.createreadinglog());
		columns.push(me.createreKeyword());
		columns.push(me.createrePublisherName());
		columns.push(me.createrePublisherDateTime());

		//me.createOtherColumn(columns);
		return columns;
	},
	/**打开编辑表单*/
	openFFileForm: function(record, fFileStatus, formtype) {
		var me = this;
		var Grid = me.getComponent('Grid');
		var id = "",
			OriginalFileID = "",BDictTreeId=me.BDictTreeId,BDictTreeCName=me.BDictTreeCName;
		if(record != null) {
			id = record.get('FFile_Id');
			BDictTreeId=record.get('FFile_BDictTree_Id');
			BDictTreeCName=record.get('FFile_BDictTree_CName');
			OriginalFileID = record.get('FFile_OriginalFileID');
			if(!OriginalFileID) {
				OriginalFileID = id;
			}
		}
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight - 10;
		var AgreeOperationType = fFileStatus == 5 ? 5 : 1;
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: me.FTYPE.toString()+"1",
			height: height,
			width: maxWidth,
			zindex: 10,
			zIndex: 10,
			hasReset: me.hasReset,
			title: "编辑知识库",
			BDictTreeId: BDictTreeId,
			BDictTreeCName: BDictTreeCName,
			formtype: 'edit',
			fFileStatus: fFileStatus,
			AgreeOperationType: AgreeOperationType,
			FTYPE: me.FTYPE,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			AppOperationType: me.AppOperationType,
			listeners: {
				save: function(win, e) {
					if(e.success) {
						me.onSearch();
						win.close();
					}
				}
			}
		};
		var form = 'Shell.class.qms.file.manage.EditTabPanel';
		if(id && id != null) {
			config.formtype = formtype || 'edit';
			config.PK = id;
			config.FFileId = id;
			title: "编辑知识库";
			config.OriginalFileID = OriginalFileID;
		}
		JShell.Win.open(form, config).show();
	}
});