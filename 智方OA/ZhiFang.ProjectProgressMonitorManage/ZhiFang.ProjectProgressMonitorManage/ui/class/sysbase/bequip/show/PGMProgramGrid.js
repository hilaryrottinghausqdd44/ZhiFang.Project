/**
 * 仪器历史版本
 * @author longfc
 * @version 2016-10-09
 */
Ext.define('Shell.class.sysbase.bequip.show.PGMProgramGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '仪器历史版本',
	width: 660,
	height: 480,
	/**获取数据服务路径*/
	selectUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PGMProgram_DataAddTime',
		direction: 'DESC'
	}, {
		property: 'PGMProgram_Title',
		direction: 'ASC'
	}],
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: true,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**文件下载服务路径*/
	downloadUrl: "/PDProgramManageService.svc/PGM_UDTO_DownLoadPGMProgramAttachment",
	PK: '',
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.PK != '') {
			me.loadByEEquipId(me.PK);
		}
	},
	initComponent: function() {
		var me = this;
		me.downloadUrl = (me.downloadUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.downloadUrl + "?operateType=0&id=";
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			text: '主键ID',
			dataIndex: 'PGMProgram_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '是否允许评论',
			dataIndex: 'PGMProgram_IsDiscuss',
			hidden: true,
			hideable: false
		}, {
			text: '程序信息',
			dataIndex: 'PGMProgram_Title',
			hidden: false,
			flex: 1,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = value;
				//showValue = "标题:" + value + "<br />";
				var showValue = "";
				var id = record.get("PGMProgram_Id");

				var row2Value = "发布人:" + record.get("PGMProgram_PublisherName") + "&nbsp;&nbsp;&nbsp;&nbsp;";
				row2Value = row2Value + "版本号:" + record.get("PGMProgram_VersionNo") + "&nbsp;&nbsp;&nbsp;&nbsp;";
				row2Value = row2Value + "编号:" + record.get("PGMProgram_No") + "<br />";

				showValue = showValue + "<b>说明:</b>" + record.get("PGMProgram_Memo") + "<br />";
				//font-weight:bold;
				var NewFileName = "<b>附件:</b><a href='" + me.downloadUrl + id + "'>" + record.get("PGMProgram_NewFileName") + "<a/>";
				var PublisherDateTime = "<b>发布时间:" + record.get("PGMProgram_PublisherDateTime") + "</b>";

				var result = "<table style='padding: 2 px 2 px;vertical-align:top;width:100%;border-bottom-style:solid; border-bottom-width:0px; border-color:darkgrey;border-collapse:separate;   border-spacing:5px;font-size:12px' border='0'>" +

					"<tr style='vertical-align:top;margin:4px;width:100%;font-weight:bold;'>" +
					"<td colspan='4' style='vertical-align:top;margin:2px;width:100%;'>" +
					"标题:" + value +
					"</td>" +
					"</tr>" +

					"<tr style='vertical-align:top;margin:4px;width:100%;font-weight:bold;'>" +
					"<td colspan='4'>" +
					row2Value +
					"</td>" +
					"</tr>" +

					"<tr style='vertical-align:top;margin:4px;width:100%;'>" +
					"<td colspan='4'>" +
					showValue +
					"</td>" +
					"</tr>" +

					"<tr style='vertical-align:top;margin:4px;width:100%;'>" +
					"<td colspan='3'>" +
					NewFileName +
					"</td>" +
					"<td align='right'>" +
					PublisherDateTime +
					"</td>" +
					"</tr>" +

					"</table>";
				return result;
			}
		}, {
			text: '编号',
			dataIndex: 'PGMProgram_No',
			hidden: true,
			width: 80,
			hideable: false
		}, {
			text: '版本号',
			dataIndex: 'PGMProgram_VersionNo',
			hidden: true,
			hideable: false
		}, {
			text: '状态',
			dataIndex: 'PGMProgram_Status',
			width: 70,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta) {
				var v = JcallShell.QMS.Enum.PGMProgramStatus[value];
				meta.style = 'font-weight:bold;color:' + JShell.QMS.Enum.PGMProgramStatusColor[value];
				return v;
			}
		}, {
			text: '上级字典树',
			dataIndex: 'PGMProgram_PBDictTree_CName',
			hidden: false,
			hidden: true,
			width: 120,
			hideable: false
		}, {
			text: '所属字典树',
			dataIndex: 'PGMProgram_SubBDictTree_CName',
			hidden: false,
			width: 120,
			hidden: true,
			hideable: false
		}, {
			text: '发布人',
			dataIndex: 'PGMProgram_PublisherName',
			hidden: true,
			width: 120,
			hideable: false
		}, {
			text: '说明',
			dataIndex: 'PGMProgram_Memo',
			hidden: true,
			width: 120,
			hideable: false
		}, {
			text: '附件名称',
			dataIndex: 'PGMProgram_NewFileName',
			hidden: true,
			width: 120,
			hideable: false
		}, {
			text: '发布时间',
			dataIndex: 'PGMProgram_PublisherDateTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		});

		return columns;
	},
	/**根据ID加载数据*/
	loadByEEquipId: function(id) {
		var me = this;
		me.defaultWhere = ' pgmprogram.Status=3 and pgmprogram.BEquip.Id=' + id;
		me.onSearch();
	}
});