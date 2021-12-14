/**
 * 帮助系统基本列表
 * @author longfc
 * @version 2016-11-22
 */
Ext.define('Shell.class.qms.file.help.basic.Grid', {
	extend: 'Shell.class.qms.file.manage.Grid',
	title: '帮助信息发布',
	width: 1200,
	height: 800,
	FTYPE: '5',
	checkOne: true,
	hasShow: true,
	/**文档状态默认为全部*/
	defaultStatusValue: "",
	/**是否显示内容页签*/
	hasContent: true,
	/**是否显示文档详情页签*/
	hasFFileOperation: false,
	hiddenFFileStatus: true,
	/*文档日期范围类型默认值**/
	defaultFFileDateTypeValue: 'ffile.PublisherDateTime',
	/**是否隐藏日期查询选择项*/
	hiddenDateSearch: true,
	/**是否有更新树节点列*/
	hasEditBDictTreeColumn: false,
	/*文档状态**/
	FFileStatusList: [
		[0, JShell.All.ALL],
		["5", '已发布']
	],
	FFileDateTypeList: [
		["ffile.PublisherDateTime", '发布时间']
	],
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'FFile_IsUse',
		direction: 'ASC'
	}, {
		property: 'FFile_BDictTree_Id',
		direction: 'ASC'
	}, {
		property: 'FFile_No',
		direction: 'ASC'
	}, {
		property: 'FFile_Title',
		direction: 'DESC'
	}],
	/**默认查询条件是否需要Type*/
	hasSearchType: false,
	/**overwrite*/
	createdefaultWhere: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || "ffile.IsUse=1";
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**overwrite*/
	createSearchInfo: function() {
		var me = this;
		me.searchInfo = {
			width: '40%',
			minwidth: 160,
			maxwidth: 360,
			emptyText: '类型/文档编码/标题',
			isLike: true,
			itemId: 'search',
			style: {
				marginRight: '10px'
			},
			fields: ['ffile.No', 'ffile.Title', 'ffile.BDictTree.CName']
		};
	},
	/**创建数据列*/
	createNewColumns: function() {
		var me = this;
		var columns = [];

		columns.push(me.createreBDictTreeCName());
		//columns.push(me.createreKeyword());
		columns.push(me.createreFFileNo());
		//columns.push(me.createreFFileSummary());
		columns.push({
			text: '主键ID',
			dataIndex: 'FFile_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '树类型ID',
			dataIndex: 'FFile_BDictTree_Id',
			hidden: true,
			hideable: false
		}, {
			text: '状态',
			dataIndex: 'FFile_Status',
			hidden: true,
			dataIndex: 'FFile_Status',
			width: 70,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = JcallShell.QMS.Enum.FFileStatus[value];
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.FFileOperationTypeColor[value];
				return v;
			}
		});
		columns.push(me.createreFFileTitle());
		columns.push(me.createreIsUse());
		if(me.isManageApp == true) {
			columns.push(me.createEditBDictTreeColumn());
			//是否有操作记录查看列
			//columns.push(me.createOperation());
		}
		columns.push(me.createrePublisherName());
		//columns.push(me.createrePublisherDateTime());

		return columns;
	},
	/*创建类型*/
	createreBDictTreeCName: function() {
		var me = this;
		return {
			text: '类型',
			dataIndex: 'FFile_BDictTree_CName',
			hidden: false,
			width: 95,
			hideable: false
		};
	},
	/*创建编号列*/
	createreFFileNo: function() {
		var me = this;
		return {
			text: '文档编码',
			dataIndex: 'FFile_No',
			hidden: false,
			width: 185,
			hideable: false
		};
	},
	/*创建文档标题列*/
	createreFFileTitle: function() {
		var me = this;
		return {
			text: '文档标题',
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
	/**依模块Id查看帮助内容*/
	openShowTabPanel: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth - 280;
		var height = document.body.clientHeight - 60;
		var defaultWhere = "";
		var id = record.get('FFile_Id');
		var isSearchForId = true;
		//按模块Id
		if(id == "" || id == null) {
			id = record.get('FFile_No');
			isSearchForId = false;
		}
		JShell.Win.open(('Shell.class.qms.file.help.show.PanelForId'), {
			PK: id,
			isSearchForId: true,
			height: height,
			width: maxWidth,
			FTYPE: "5",
			listeners: {
				save: function(win) {
					win.close();
				}
			}
		}).show();
	}
});