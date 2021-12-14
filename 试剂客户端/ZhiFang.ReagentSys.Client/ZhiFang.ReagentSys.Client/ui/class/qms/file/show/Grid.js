/**
 * 普通用户QMS文档查看列表
 * @author longfc
 * @version 2016-09-26
 */
Ext.define('Shell.class.qms.file.show.Grid', {
	extend: 'Shell.class.qms.file.basic.Grid',
	title: '文档查看列表',
	width: 1200,
	height: 800,
	/**文档状态默认为发布*/
	defaultStatusValue: "5",
	/**文件的操作记录类型*/
	fFileOperationType: 6,
	/**文档的交流类型:对查询应用(show)的交流应用获取交流记录做默认时间(交流的addtime大于等于发布时间)过滤，起草等(edit)不需要 未完成*/
	interactionType: "show",
	hasDel: false,
	hideDelColumn: true,

	remoteSort: false,
	isSearchChildNode: true,
	hasCheckBDictTree: false,
	
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.PublisherDateTime',
	hasInteraction: true,
	/**是否显示内容页签*/
	hasContent: true,
	/**是否显示文档详情页签*/
	hasFFileOperation: false,
	/**是否显示操作记录页签*/
	hasOperation: false,
	/**是否显示阅读记录页签*/
	hasReadingLog: false,
	/**获取我的阅读文件信息数据服务路径*/
	selectUrl: '/CommonService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'FFile_IsTop',
		direction: 'DESC'
	}, {
		property: 'FFile_PublisherDateTime',
		direction: 'DESC'
	}, {
		property: 'FFile_BDictTree_Id',
		direction: 'ASC'
	}, {
		property: 'FFile_Title',
		direction: 'ASC'
	}],

	initComponent: function() {
		var me = this;
		me.FTYPE = me.FTYPE || "";
		me.createdefaultWhere();
		me.callParent(arguments);
	},
	/**overwrite*/
	createdefaultWhere: function() {
		var me = this;
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || "(ffile.IsUse=1) and ((ffile.BeginTime is null and ffile.EndTime is null) or (ffile.BeginTime<='" + dt + "')  or (ffile.EndTime>='" + dt + "'))";

	},
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];
		columns = me.initDefaultColumn(columns);

		if(me.FTYPE == '1') {
			columns.push(me.createreVersionNo());
		}
		columns.push(me.createreKeyword());
		columns.push({
			text: '起草时间',
			dataIndex: 'FFile_DrafterDateTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '审核时间',
			dataIndex: 'FFile_CheckerDateTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '审批时间',
			dataIndex: 'FFile_ApprovalDateTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, me.createreIsUse());
		//是否有交流列
		if(me.hasInteraction) {
			columns.push(me.createInteraction());
		}
		columns.push(me.createrePublisherName(), me.createrePublisherDateTime());
		return columns;
	},
	/**
	 * overwrite
	 * 创建文档标题列
	 */
	createreFFileTitle: function() {
		var me = this;
		return {
			text: '标题',
			dataIndex: 'FFile_Title',
			flex: 1,
			sortable: true,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var IsTop = record.get("FFile_IsTop");
				if(IsTop == "true") {
					value = "<b style='color:red;'>【置顶】</b>" + value;
				}
				return value;
			}
		};
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	}
});