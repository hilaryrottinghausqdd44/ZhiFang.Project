/**
 * 附件列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.attachment.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '附件列表',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEAttachmentByHQL?isPlanish=true',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'EAttachment_FileName',
		direction: 'ASC'
	}, {
		property: 'EAttachment_FileExt',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	hasRefresh: true,
	PK: null,
	ReportDate: null,
	TempletID:null,
		/**月保养*/
	TempletType: '',
	/**月保养编码*/
	TempletTypeCode: '',
	/**默认每页数量*/
	defaultPageSize: 200,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere ='eattachment.IsUse=1';
		
		buttonToolbarItems = me.buttonToolbarItems || [];
		if(buttonToolbarItems.length > 0) {
			buttonToolbarItems.unshift('-');
		}
		buttonToolbarItems.unshift('refresh');
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this,
			columns = [];
		columns.push({
			text: '名称',
			dataIndex: 'EAttachment_FileNewName',
			width: 180,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '类型',
			dataIndex: 'EAttachment_FileExt',
			width: 40,
			sortable: false,
			defaultRenderer: true
		},  {
			text: '模板id',
			dataIndex: 'EAttachment_Id',
			width: 80,
			sortable: false,
			defaultRenderer: true,
			hidden: true
		}, {
			text: '模板id',
			dataIndex: 'EAttachment_ETemplet_Id',
			width: 80,
			sortable: false,
			hidden: true,
			defaultRenderer: true //,hidden: true
		});
		return columns;
	}
});