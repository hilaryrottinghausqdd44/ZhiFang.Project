/**
 * 文档修订列表
 * @author 
 * @version 2016-07-12
 */
Ext.define('Shell.class.qms.file.file.revise.Grid', {
	extend: 'Shell.class.qms.file.basic.Grid',
	title: '文档修订列表',
	width: 1200,
	height: 800,

	checkOne: true,
	hasReset: true,
	hasAdd: false,
	/**文档状态默认为发布*/
	defaultStatusValue: "5",
	/**文件的操作记录类型*/
	fFileOperationType: 1,
	/**是否隐藏文档状态选择项*/
	hiddenFFileStatus: true,
	/**文档的交流类型:对查询应用(show)的交流应用获取交流记录做默认时间(交流的addtime大于等于发布时间)过滤，起草等(edit)不需要 未完成*/
	interactionType: "show",
	hasDel: false,
	hideDelColumn: true,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.PublisherDateTime',
	/**列表的默认查询条件--是否只查询当前登录者的数据*/
	isSearchUSERID: false,
	/**文档状态值*/
	fFileStatus: 1,
	/**
	 * 功能按钮是否隐藏:组件是否隐藏,只起草,仅审核,仅批准,自动发布
	 * 第一个参数为功能按钮是否显示或隐藏
	 * 第二个参数为只起草选择项是否显示或隐藏
	 * 第三个参数为仅审核选择项是否显示或隐藏
	 * 第四个参数为仅批准选择项是否显示或隐藏
	 * 第五个参数为发布选择项是否显示或隐藏
	 * */
	hiddenRadiogroupChoose: [false, false, true, true, false],
	/**功能按钮默认选中*/
	checkedRadiogroupChoose: [true, false, false, false],
	/**基本应用的文档确认(通过/同意)操作按钮是否显示*/
	HiddenAgreeButton: false,
	/**基本应用的文档确认(通过/同意)操作按钮显示名称*/
	AgreeButtonText: "确认提交",
	/**基本应用的文档确认(通过/同意)操作按钮的功能类型*/
	AgreeOperationType: 1,

	/**基本应用的文档确认(不通过/不同意)操作按钮是否显示*/
	HiddenDisagreeButton: true,
	/**基本应用的文档确认(不通过/不同意)操作按钮显示名称*/
	DisagreeButtonText: "暂存",
	/**基本应用的文档确认(不通过/不同意)操作按钮的功能类型*/
	DisagreeOperationType: 1,
	/**提交并发布的操作按钮是否显示*/
	HiddenPublishButton: false,
	/**隐藏阅读人信息*/
	HiddenFFileReadingLog: true,
	/**概要是否默认清空，外部传入，true-清空，false-不清空*/
	ISCLEAR:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.DisagreeOfGridText = "撤消提交";
		me.AgreeOfGridText = "确认提交";
		me.HiddenAgreeOfGrid = true;
		me.HiddenDisagreeOfGrid = true;
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "') or(ffile.EndTime>='" + dt + "'))";

		me.callParent(arguments);
	},
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '文档ID',
			dataIndex: 'FFile_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '文档标题',
			dataIndex: 'FFile_Title',
			width: 130,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '文档状态',
			dataIndex: 'FFile_Status',
			width: 70,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = JcallShell.QMS.Enum.FFileStatus[value];
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.FFileOperationTypeColor[value];
				return v;
			}
		}, {
			text: '类型',
			dataIndex: 'FFile_BDictTree_CName',
			hidden: false,
			width: 110,
			hideable: false
		}, {
			text: '修订号',
			dataIndex: 'FFile_ReviseNo',
			hidden: false,
			width: 60,
			hideable: false
		}, {
			text: '修订人',
			dataIndex: 'FFile_Revisor_CName',
			hidden: false,
			width: 80,
			hideable: false
		}, {
			text: '修订时间',
			dataIndex: 'FFile_ReviseTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		},{
			text: '原始文档ID',
			dataIndex: 'FFile_OriginalFileID',
			hidden: true,
			width: 80,
			hideable: false
		});
		columns.push({
			text: '发布人',
			dataIndex: 'FFile_PublisherName',
			width: 100,
			sortable: false,
			menuDisabled: true
		}, {
			text: '发布时间',
			dataIndex: 'FFile_PublisherDateTime',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			text: '是否允许评论',
			dataIndex: 'FFile_IsDiscuss',
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '修订',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'button-add hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var status = rec.get('FFile_Status');
					switch(status) {
						case "5": //状态为
							me.formtype = "add";
							me.setAppOperationType();
							me.openFFileForm(rec, 1);
							break;
						default:
							JShell.Msg.error('只有文档状态为【已发布】才允许修订');
							break;
					}
				}
			}]
		});
		//是否有交流列
		columns.push(me.createInteraction());
		//是否有操作记录查看列
		columns.push(me.createOperation());
		//阅读记录列
		columns.push(me.createreadinglog());
		me.createOtherColumn(columns);
		return columns;
	},
	/**打开新增或编辑文档表单*/
	openFFileForm: function(record, fFileStatus) {
		var me = this;
		var id = "",
			OriginalFileID = "",
			BDictTreeId = me.BDictTreeId,
			BDictTreeCName = me.BDictTreeCName;
		if(record != null) {
			id = record.get('FFile_Id');
			BDictTreeId = record.get('FFile_BDictTree_Id');
			BDictTreeCName = record.get('FFile_BDictTree_CName');
			OriginalFileID = record.get('FFile_OriginalFileID');
			if(!OriginalFileID) {
				OriginalFileID = id;
			}
		}
	     //如果修订时间不为空，显示修订记录页
		var ReviseTime = record.get('FFile_OriginalFileID'); 
		var hasRevise = ReviseTime ? true : false;
		var formtype = 'add';
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight - 10;
		var config = {
			showSuccessInfo: false,
			SUB_WIN_NO: me.FTYPE.toString() + "1",
			height: height,
			width: maxWidth,
			zindex: 10,
			zIndex: 10,
			hasReset: me.hasReset,
			title: me.title || "编辑文档",
			formtype: formtype || 'add',
			BDictTreeId: BDictTreeId,
			BDictTreeCName: BDictTreeCName,
			fFileOperationType: me.fFileOperationType,
			fFileStatus: fFileStatus,
			HiddenAgreeButton: me.HiddenAgreeButton,
			AgreeButtonText: me.AgreeButtonText,
			AgreeOperationType: me.AgreeOperationType,
			HiddenDisagreeButton: me.HiddenDisagreeButton,
			DisagreeButtonText: me.DisagreeButtonText,
			DisagreeOperationType: me.DisagreeOperationType,
			FTYPE: me.FTYPE,
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			AppOperationType: me.AppOperationType,
			hiddenRadiogroupChoose: me.hiddenRadiogroupChoose,
			isAddFFileReadingLog: 0,
			isAddFFileOperation: 1,
			hasRevise:hasRevise,
			ISCLEAR:me.ISCLEAR,
			listeners: {
				save: function(win, e) {
					if(e.success) {
						me.onSearch();
						JShell.Action.delay(function() {
							win.close();
						}, null, 1000);
					} else {
						JShell.Msg.error(e.msg);
					}
				}
			}
		};
		var form = 'Shell.class.qms.file.file.revise.AddTabPanel';
		if(id && id != null) {
			config.formtype = formtype || 'edit';
			switch(me.AppOperationType) {
				case JcallShell.QMS.Enum.AppOperationType.新增修订文档:
					config.formtype = 'add';
					break;
				case JcallShell.QMS.Enum.AppOperationType.编辑修订文档:
					config.formtype = 'edit';
					break;
				default:
					config.formtype = formtype || 'edit';
					break;
			}
			config.PK = id;
			config.FFileId = id;
			title: me.title || "编辑文档";
			config.fFileOperationType = me.fFileOperationType;
			config.OriginalFileID = OriginalFileID;
		}
		JShell.Win.open(form, config).show();
	}
});